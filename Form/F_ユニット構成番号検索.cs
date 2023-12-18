using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data.OleDb;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using u_net.Public;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Drawing.Imaging;
using System.Drawing.Printing;

namespace u_net
{
    public partial class F_ユニット構成番号検索 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "ユニット構成番号検索";
        private int selected_frame = 0;

        public F_ユニット構成番号検索()
        {
            this.Text = "ユニット構成番号検索";       // ウィンドウタイトルを設定
            this.MaximizeBox = false;  // 最大化ボタンを無効化
            this.MinimizeBox = false; //最小化ボタンを無効化

            InitializeComponent();

        }
        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        public void CommonConnect()
        {
            CommonConnection connectionInfo = new CommonConnection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        //SqlConnection cn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter();


        public string CurrentCode
        {
            get
            {
                return Nz(製品コード.Text);
            }
        }

        public int CurrentEdition
        {
            get
            {
                return string.IsNullOrEmpty(製品版数.Text) ? 0 : Int32.Parse(製品版数.Text);
            }
        }

        private string Nz(object value)
        {
            return value == null ? "" : value.ToString();
        }
        private void Form_Load(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                control.PreviewKeyDown += OriginalClass.ValidateCheck;
            }

            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");



            MyApi myapi = new MyApi();
            int xSize, ySize, intpixel, twipperdot;

            //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
            intpixel = myapi.GetLogPixel();
            twipperdot = myapi.GetTwipPerDot(intpixel);

            try
            {
                this.SuspendLayout();

                int intWindowHeight = this.Height;
                int intWindowWidth = this.Width;


                int commaIndex = args.IndexOf(",");
                if (commaIndex != -1)
                {
                   製品コード.Text = args.Substring(0, commaIndex);
                }

                int startIndex = commaIndex + 1;
                if (startIndex > 0 && startIndex < args.Length)
                {
                    製品版数.Text = Convert.ToInt32(args.Substring(startIndex)).ToString();
                }


                UpdatedCode(CurrentCode);

                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("初期化に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            finally
            {
                this.ResumeLayout();
                fn.WaitForm.Close();
            }
        }

        private void Form_Unload(object sender, FormClosingEventArgs e)
        {
           


        }


        private void UpdatedCode(string codeString)
        {
            
            OriginalClass ofn = new OriginalClass();
            ofn.SetComboBox(製品版数, "SELECT 製品版数 as Display, 製品版数 as Value FROM M製品 WHERE 製品コード=" + codeString + " ORDER BY 製品版数 DESC");

            // 製品情報を表示する
            SetProductInfo(this.CurrentCode, this.CurrentEdition);

            // ユニット一覧を設定する
            SetUnitList(this.CurrentCode, this.CurrentEdition);

            // 対象ユニットにフォーカスを設定する
            対象ユニット.Focus();
        
        }

        private void SetProductInfo(string productCode, int productEdition)
        {
            try
            {
                // SQL クエリの作成
                string strSQL = "SELECT 品名, シリーズ名 FROM M製品 " +
                                "WHERE 製品コード=@ProductCode AND 製品版数=@ProductEdition";

                Connect();

                using (SqlCommand command = new SqlCommand(strSQL, cn))
                {
                    // パラメータの設定
                    command.Parameters.AddWithValue("@ProductCode", productCode);
                    command.Parameters.AddWithValue("@ProductEdition", productEdition);

                    // SQL クエリの実行
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // データが存在する場合
                            品名.Text = reader["品名"].ToString();
                            シリーズ名.Text = reader["シリーズ名"].ToString();
                        }
                        else
                        {
                            // データが存在しない場合の処理を追加する
                            品名.Text = null;
                            シリーズ名.Text = null;
                        }
                    }
                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SetProductInfo - {ex.GetType().Name} : {ex.Message}");
                // エラー処理を追加する
            }
        }

        private void SetUnitList(string productCode, int productEdition)
        {

            string strSQL = "SELECT ユニットコード AS コード, ユニット版数 AS E, ユニット名, ユニット型番 " +
                            "FROM V製品ユニット一覧 " +
                            "WHERE 製品コード=" + productCode + " AND 製品版数=" + productEdition + " " +
                            "ORDER BY ユニット型番";

            Connect();

            DataGridUtils.SetDataGridView(cn, strSQL, 対象ユニット);

            対象ユニット.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            対象ユニット.MultiSelect = true;
            対象ユニット.RowHeadersVisible = false;
            対象ユニット.AllowUserToAddRows = false;
            対象ユニット.AllowUserToDeleteRows = false;
            対象ユニット.ReadOnly = true;

            MyApi myapi = new MyApi();
            int xSize, ySize, intpixel, twipperdot;

            //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
            intpixel = myapi.GetLogPixel();
            twipperdot = myapi.GetTwipPerDot(intpixel);

            //0列目はaccessでは行ヘッダのため、ずらす
            対象ユニット.Columns[0].Width = 1000 / twipperdot;
            対象ユニット.Columns[1].Width = 400 / twipperdot;
            対象ユニット.Columns[2].Width = 2750 / twipperdot;
            対象ユニット.Columns[3].Width = 2750 / twipperdot;

            

        }


            private void 重複検索ボタン_Click(object sender, EventArgs e)
            {
                FunctionClass fn = new FunctionClass();


            try
            {
                fn.DoWait("検索しています...");

                Connect();

                // 検索処理
                string strKey = "";
                int lngi = 1;

                // 対象ユニットの処理
                foreach (DataGridViewRow row in 対象ユニット.Rows)
                {
                    if (row.Selected)
                    {
                        if (strKey == "")
                        {
                            strKey = $"ユニットコード='{row.Cells["コード"].Value}' AND ユニット版数={row.Cells["E"].Value}";
                        }
                        else
                        {
                            strKey += $" OR ユニットコード='{row.Cells["コード"].Value}' AND ユニット版数={row.Cells["E"].Value}";
                        }
                    }
                    lngi++;
                }

                if (strKey == "")
                {
                    strKey = "1=0";
                }

                string strSQL = $"SELECT * FROM (SELECT 構成番号, COUNT(構成番号) AS 重複数 FROM dbo.Mユニット明細 WHERE {strKey} GROUP BY 構成番号) AS A WHERE 1 < A.重複数 ORDER BY A.構成番号";

                // 検索結果の設定
                DataGridUtils.SetDataGridView(cn, strSQL, 検索結果);

 
                // データがない場合
                if(検索結果.RowCount <= 0)
                {
                    MessageBox.Show("検索結果はありません。", "重複検索", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                検索結果.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                検索結果.MultiSelect = true;
                検索結果.RowHeadersVisible = false;
                検索結果.AllowUserToAddRows = false;
                検索結果.AllowUserToDeleteRows = false;
                検索結果.ReadOnly = true;

                MyApi myapi = new MyApi();
                int xSize, ySize, intpixel, twipperdot;

                //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
                intpixel = myapi.GetLogPixel();
                twipperdot = myapi.GetTwipPerDot(intpixel);

                //0列目はaccessでは行ヘッダのため、ずらす
                検索結果.Columns[0].Width = 1200 / twipperdot;
                検索結果.Columns[1].Width = 1200 / twipperdot;

            }
            catch (Exception ex)
            {
                Debug.Print($"{this.Name}_重複検索_Click - {ex.GetType().Name}: {ex.Message}");
                MessageBox.Show("エラーが発生しました。", "重複検索", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // フォームを閉じる
                fn.WaitForm.Close();
            }
        }

        private void 空き検索ボタン_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。", "空き検索", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void 末尾検索ボタン_Click(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();


            try
            {
                fn.DoWait("検索しています...");

                Connect();

                // 検索処理
                string strKey = "";
                int lngi = 1;

                // 対象ユニットの処理
                foreach (DataGridViewRow row in 対象ユニット.Rows)
                {
                    if (row.Selected)
                    {
                        if (strKey == "")
                        {
                            strKey = $"ユニットコード='{row.Cells["コード"].Value}' AND ユニット版数={row.Cells["E"].Value}";
                        }
                        else
                        {
                            strKey += $" OR ユニットコード='{row.Cells["コード"].Value}' AND ユニット版数={row.Cells["E"].Value}";
                        }
                    }
                    lngi++;
                }

                if(strKey == "")
                {
                    strKey = "1=0";
                }

                string strSQL = "SELECT B.英字部 + LTRIM(STR(MAX(B.数字部))) AS 末尾構成番号 " +
                            "FROM (SELECT A.英字部, " +
                            "REVERSE(LEFT(REVERSE(A.残り), PATINDEX('%[0-9]%', REVERSE(A.残り)) - 1)) AS 第二英字部, " +
                            "CAST(REVERSE(REPLACE(REVERSE(A.残り), LEFT(REVERSE(A.残り), PATINDEX('%[0-9]%', REVERSE(A.残り)) - 1), '')) AS int) AS 数字部 " +
                            "FROM (SELECT 構成番号, " +
                            "LEFT(構成番号, PATINDEX('%[0-9]%', 構成番号) - 1) AS 英字部, " +
                            "REPLACE(構成番号, LEFT(構成番号, PATINDEX('%[0-9]%', 構成番号) - 1), '') AS 残り " +
                            "FROM dbo.Mユニット明細 " +
                            $"WHERE {strKey} AND PATINDEX('%[0-9]%', 構成番号) <> 0) AS A" +
                            ") AS B GROUP BY B.英字部 " +
                            "ORDER BY 末尾構成番号";

                // 検索結果の設定
                DataGridUtils.SetDataGridView(cn, strSQL, 検索結果);


                // データがない場合
                if (検索結果.RowCount <= 0)
                {
                    MessageBox.Show("検索結果はありません。", "末尾検索", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                検索結果.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                検索結果.MultiSelect = true;
                検索結果.RowHeadersVisible = false;
                検索結果.AllowUserToAddRows = false;
                検索結果.AllowUserToDeleteRows = false;
                検索結果.ReadOnly = true;

                MyApi myapi = new MyApi();
                int xSize, ySize, intpixel, twipperdot;

                //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
                intpixel = myapi.GetLogPixel();
                twipperdot = myapi.GetTwipPerDot(intpixel);

                //0列目はaccessでは行ヘッダのため、ずらす
                検索結果.Columns[0].Width = 2400 / twipperdot;

            }
            catch (Exception ex)
            {
                Debug.Print($"{this.Name}_末尾検索_Click - {ex.GetType().Name}: {ex.Message}");
                MessageBox.Show("エラーが発生しました。", "末尾検索", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // フォームを閉じる
                fn.WaitForm.Close();
            }
        }

        private void 閉じるボタン_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
