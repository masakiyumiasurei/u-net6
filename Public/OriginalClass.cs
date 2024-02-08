using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Linq;
using TextBox = System.Windows.Forms.TextBox;
using ComboBox = System.Windows.Forms.ComboBox;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using GrapeCity.Win.MultiRow;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Media;

namespace u_net.Public
{
    internal class OriginalClass
    {
        /// <summary>
        /// 渡したパスを実行する
        /// </summary>
        /// <param name="url">実行するパス</param>
        public static void OpenUrl(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo
                {
                    FileName = url,
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine("エラー: " + ex.Message);
            }
        }


        /// <summary>
        /// URL の妥当性を確認するためのカスタムロジックを実装
        /// このロジックは URL の形式に合ったものである必要があります
        /// 有効な URL であるかどうかの判定を行います
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsValidUrl(string url)
        {            
            try
            {
                Uri uri = new Uri(url);
                return uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps;
            }
            catch (UriFormatException)
            {
                return false;
            }
        }



        public const string ApiUrl = "https://zipcloud.ibsnet.co.jp/api/search";

        /// <summary>
        /// 郵便番号から住所を返す
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        public static async Task<string> GetAddressFromZipCode(string zipCode)
        {
            using (var httpClient = new HttpClient())
            {
                var apiUrl = $"{ApiUrl}?zipcode={zipCode}";

                try
                {
                    var response = await httpClient.GetStringAsync(apiUrl);
                    var result = JObject.Parse(response);
                    if (result["results"][0]["address1"].ToString() != null)
                    {
                        return result["results"][0]["address1"].ToString() + result["results"][0]["address2"].ToString() + result["results"][0]["address3"].ToString();
                    }
                    else
                    {
                        return "";
                    }
                }
                catch (Exception ex)
                {
                    return "エラー: " + ex.Message;
                }
            }
        }

        /// <summary>
        /// 郵便番号の書式かどうかを返す
        /// </summary>
        /// <param name="zipCode"></param>
        /// <returns></returns>
        public static bool IsValidZipCode(string zipCode)
        {
            // 郵便番号の正規表現パターン (例: "123-4567")
            string pattern = @"^\d{3}-?\d{4}$";

            return System.Text.RegularExpressions.Regex.IsMatch(zipCode, pattern);
        }

        /// <summary>
        /// コンボボックスのアイテムに指定の値が含まれているかチェック
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="value">判定する値</param>
        /// <returns></returns>        
        public static string ComboBoxContainsValue(ComboBox comboBox, string value)
        {
            foreach (var item in comboBox.Items)
            {
                if (item is DataRowView rowView)
                {
                    string itemText = rowView["display"].ToString();  // rowView.Row.Field<string>(1); // 2列目を取得
                    
                    //MessageBox.Show(itemText);

                    if (itemText == value)
                    {
                        string str = rowView.Row.Field<string>(1);
                        return str; // 2列目を取得
                    }
                }
            }
            return "";
        }



        /// <summary>
        /// 画面キャプチャを返す
        /// </summary>
        /// <param name="outputPath">保存先</param>
        public static void CaptureScreen(string outputPath)
        {
            // 画面のサイズを取得
            Rectangle bounds = Screen.GetBounds(Point.Empty);

            // スクリーンショットを撮るためのBitmapオブジェクトを作成
            using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
            {
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(Point.Empty, Point.Empty, bounds.Size);
                }

                // デスクトップに保存
                bitmap.Save(outputPath, ImageFormat.Png);
            }
        }

        public static void CaptureActiveForm(string outputPath)
        {            // アクティブなフォームを取得
            Form activeForm = Form.ActiveForm;

            if (activeForm != null)
            {
                // アクティブなフォームのサイズを取得
                Rectangle bounds = activeForm.Bounds;

                // スクリーンショットを撮るためのBitmapオブジェクトを作成
                using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(activeForm.Location, Point.Empty, bounds.Size);
                    }

                    // ファイルに保存
                    bitmap.Save(outputPath, ImageFormat.Png);
                }
            }
            else
            {
                // アクティブなフォームがない場合のエラーハンドリング
                MessageBox.Show("アクティブなフォームが見つかりません。");
            }
        }

        public static void PrintScreen(string imagePath)
        {
            PrintDocument printDocument = new PrintDocument();

            printDocument.DefaultPageSettings.Landscape = true;

            printDocument.PrintPage += (sender, e) =>
            {
                Image image = Image.FromFile(imagePath);
                e.Graphics.DrawImage(image, e.MarginBounds);
            };

            PrintDialog printDialog = new PrintDialog();
            printDialog.Document = printDocument;

            if (printDialog.ShowDialog() == DialogResult.OK)
            {
                printDocument.Print();
            }
        }


        //指定された値がnullまたはDBNull.Valueの場合に、デフォルトの値（通常は0や空の文字列など）を返す
        public static T Nz<T>(T value, T defaultValue)
        {
            return (value == null || value.Equals(DBNull.Value)) ? defaultValue : value;
        }



        private SqlConnection cn;
        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }


        /// <summary>
        /// コンボボックスのソースをセットする。SQLにはDisplayとValueのカラムを指定する必要がある
        /// </summary>
        /// <param name="comboBox">対象のコンボボックス</param>
        /// <param name="sqlQuery">ソースのクエリ</param>
        public void SetComboBox(ComboBox comboBox, string sqlQuery)
        {
            try
            {
                Connect();


                using (SqlCommand command = new SqlCommand(sqlQuery, cn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        comboBox.DataSource = dataTable;
                        comboBox.DisplayMember = "Display";
                        comboBox.ValueMember = "Value";
                        comboBox.DataSource = dataTable;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("データの読み込み中にエラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// multirowのコンボボックスのソースをセットする。SQLにはDisply とValueのカラムを指定する必要がある
        /// </summary>
        /// <param name="comboBox"></param>
        /// <param name="sqlQuery"></param>
        public void SetComboBox(ComboBoxCell comboBox, string sqlQuery)
        {
            try
            {
                Connect();


                using (SqlCommand command = new SqlCommand(sqlQuery, cn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        comboBox.DataSource = dataTable;
                        comboBox.DisplayMember = "Display";
                        comboBox.ValueMember = "Value";
                        comboBox.DataSource = dataTable;
                    }
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("データの読み込み中にエラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (cn != null && cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }
        }
        /// <summary>
        /// 複数列のコンボボックスの値を描画する
        /// </summary>
        /// <param name="cb">対象コンボボックス</param>
        /// <param name="e"></param>
        /// <param name="fieldWidth">枠幅</param>
        /// <param name="fieldName">カラム名</param>
        public static void SetComboBoxAppearance(ComboBox cb, DrawItemEventArgs e, int[] fieldWidth, String[] fieldName)
        {
            DataTable dt = (DataTable)cb.DataSource;

            Pen p = new Pen(Color.Gray);
            Brush b = new SolidBrush(e.ForeColor);

           // cb.DropDownWidth = 550;
            //背景の描画
            e.DrawBackground();

           int width = 0;

            for (int i = 0; i < fieldName.Length; i++)
            {
                //テキストの描画
                e.Graphics.DrawString(Convert.ToString(dt.Rows[e.Index][fieldName[i]]), e.Font, b, width, e.Bounds.Y);

                //縦線の描画
                e.Graphics.DrawLine(p, width + fieldWidth[i], e.Bounds.Top, width + fieldWidth[i], e.Bounds.Bottom);

                width = width + fieldWidth[i];
            }

            //こいつがドロップダウンの悪さをしている模様！
            //クリック１回目：開いてはいるが見えない？
            //クリック２回目：閉じた
            //クリック３回目以降：普通に開く
           // cb.DropDownWidth = width;

            //選択状態の描画
            if (Convert.ToBoolean(e.State & DrawItemState.Selected)) ControlPaint.DrawFocusRectangle(e.Graphics, e.Bounds);
        }

        /// <summary>
        /// コンボボックスの表示時に、複数列を表示する
        /// </summary>
        /// <param name="cb">コンボボックス</param>
        /// <param name="e"></param>
        /// <param name="fieldWidth">表示幅</param>
        /// <param name="fieldName">カラム名</param>
        public static void SetComboBoxAppearance(ComboBoxEditingControl cb, DrawItemEventArgs e, int[] fieldWidth, String[] fieldName)
        {
            DataTable dt = (DataTable)cb.DataSource;

            Pen p = new Pen(Color.Gray);
            Brush b = new SolidBrush(e.ForeColor);

            e.DrawBackground();

            int width = 0;

            for (int i = 0; i < fieldName.Length; i++)
            {
                e.Graphics.DrawString(Convert.ToString(dt.Rows[e.Index][fieldName[i]]), e.Font, b, width, e.Bounds.Y);

                e.Graphics.DrawLine(p, width + fieldWidth[i], e.Bounds.Top, width + fieldWidth[i], e.Bounds.Bottom);

                width = width + fieldWidth[i];
            }

          // cb.DropDownWidth = width;

            if (Convert.ToBoolean(e.State & DrawItemState.Selected)) ControlPaint.DrawFocusRectangle(e.Graphics, e.Bounds);
        }

        /// <summary>
        /// 数値かどうか判定する　数値ならtrue
        /// </summary>
        /// <param name="varValue">判定したい対象</param>
        /// <returns></returns>
        
        public static bool IsNumeric(object varValue)
        {
            return double.TryParse(varValue.ToString(), out _);
        }

        public static void ValidateCheck(object sender, PreviewKeyDownEventArgs e)
        {
            Control control = sender as Control;


            if (e.KeyCode == Keys.Tab)
            {
                // Tab キーが押されたときに Validating イベントを無効にする
                control.CausesValidation = false;



            }
            else if (e.KeyCode == Keys.Return)
            {
                // Validating イベントを有効に戻す
                control.CausesValidation = true;

            }

        }
        /// <summary>
        /// SQLの結果を返す 
        /// </summary>
        /// <typeparam name="T">返す型が不明なのでジェネリックで定義する</typeparam>
        /// <param name="connection"></param>
        /// <param name="sql"></param>
        /// <returns></returns>
        public static T GetScalar<T>(SqlConnection connection, string sql)
        {
            T result =  default(T);

            try
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    object scalarResult = command.ExecuteScalar();
                    if (scalarResult != DBNull.Value)
                    {
                        result = (T)Convert.ChangeType(scalarResult, typeof(T));
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error executing SQL query: {ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// 音声ファイルを実行する
        /// </summary>
        /// <param name="fileName">ファイル名を渡す</param>
       public static void PlaySound(string fileName)
        {
            try
            {
                string fullPath = Path.Combine(Application.StartupPath, fileName);
                SoundPlayer player = new SoundPlayer(fullPath);
                player.Play();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }



    }
}
