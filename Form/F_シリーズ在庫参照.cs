using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Pao.Reports;
using u_net.Public;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Management;

namespace u_net
{
    public partial class F_シリーズ在庫参照 : MidForm
    {
        public DateTime TargetDay;

        


        int intWindowHeight = 0;
        int intWindowWidth = 0;

        private Control? previousControl;
        private SqlConnection? cn;
        public F_シリーズ在庫参照()
        {
            InitializeComponent();
        }
        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        public long CurrentAdjust
        {
            get { return Convert.ToInt64(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[6].Value); }
        }

        public DateTime CurrentDay
        {
            get { return Convert.ToDateTime(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[3].Value); }
        }

        public string CurrentSeries
        {
            get { return dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString(); }
        }

        public string CurrentSeriesName
        {
            get { return dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value.ToString(); }
        }

        public long CurrentStock
        {
            get { return Convert.ToInt64(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[7].Value); }
        }


        private void Form_Load(object sender, EventArgs e)
        {

            //実行中フォーム起動
            string LoginUserCode = "000";//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(LoginUserCode, this);

            MyApi myapi = new MyApi();
            int xSize, ySize, intpixel, twipperdot;

            //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
            intpixel = myapi.GetLogPixel();
            twipperdot = myapi.GetTwipPerDot(intpixel);

            intWindowHeight = this.Height;
            intWindowWidth = this.Width;

            // DataGridViewの設定
            dataGridView1.AllowUserToResizeColumns = true;
            dataGridView1.Font = new Font("MS ゴシック", 10);
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 210, 255);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView1.GridColor = Color.FromArgb(230, 230, 230);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("MS ゴシック", 9);
            dataGridView1.DefaultCellStyle.Font = new Font("MS ゴシック", 10);
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;


            myapi.GetFullScreen(out xSize, out ySize);

            int x = 10, y = 10;

            this.Size = new Size(this.Width, ySize * myapi.GetTwipPerDot(intpixel) - 1200);
            //accessのmovesizeメソッドの引数の座標単位はtwipなので以下で

            this.Size = new Size(this.Width, ySize - 1200 / twipperdot);

            this.StartPosition = FormStartPosition.Manual; // 手動で位置を指定
            int screenWidth = Screen.PrimaryScreen.Bounds.Width; // プライマリスクリーンの幅
            x = (screenWidth - this.Width) / 2;
            this.Location = new Point(x, y);

            Connect();

            TargetDay = DateTime.Parse(FunctionClass.GetServerDate(cn).ToString("yyyy/MM/dd"));
            今日の日付.Text = TargetDay.ToString("yyyy/MM/dd");
            現在日.Text = TargetDay.ToString("yyyy/MM/dd");

            Cleargrid(dataGridView1);
            DoUpdate();
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            try
            {
                if (this.Height > 800)
                {
                    dataGridView1.Height = dataGridView1.Height + (this.Height - intWindowHeight);
                    intWindowHeight = this.Height;  // 高さ保存

                    dataGridView1.Width = dataGridView1.Width + (this.Width - intWindowWidth);
                    intWindowWidth = this.Width;    // 幅保存
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.Name + "_Form_Resize - " + ex.Message);
            }
        }

        public int DoUpdate()
        {
            int result = -1;
            try
            {
                result = Filtering(TargetDay);
                //   DrawGrid();
                if (result >= 0)
                {
                    this.表示件数.Text = result.ToString();
                }
                else
                {
                    this.表示件数.Text = null; // Nullの代わりにC#ではnullを使用
                }

                FormatGrid(dataGridView1);

            }
            catch (Exception ex)
            {
                result = -1;
                MessageBox.Show(this.Name + "_DoUpdate - " + ex.Message);
            }

            return result;
        }

        private int Filtering(DateTime targetDate)
        {

            Connect();
            
            try
            {

                using (SqlCommand cmd = new SqlCommand("SPシリーズ在庫参照", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@TargetDate", SqlDbType.DateTime)).Value = targetDate;

                    SqlDataReader reader = cmd.ExecuteReader();

                    // レコードセットを設定
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);
                    dataGridView1.DataSource = dataTable;


                }


                MyApi myapi = new MyApi();
                int xSize, ySize, intpixel, twipperdot;

                //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
                intpixel = myapi.GetLogPixel();
                twipperdot = myapi.GetTwipPerDot(intpixel);

                intWindowHeight = this.Height;
                intWindowWidth = this.Width;

                // DataGridViewの設定
                dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200); // 薄い黄色

                // 列の幅を設定 もとは恐らくtwipのためピクセルに直す

                //0列目はaccessでは行ヘッダのため、ずらす
                //dataGridView1.Columns[0].Width = 500 / twipperdot;
                dataGridView1.Columns[0].Width = 1100 / twipperdot; //1150
                dataGridView1.Columns[1].Width = 2000 / twipperdot;
                dataGridView1.Columns[2].Width = 500 / twipperdot;
                dataGridView1.Columns[3].Width = 1300 / twipperdot;
                dataGridView1.Columns[4].Width = 1100 / twipperdot;
                dataGridView1.Columns[5].Width = 1100 / twipperdot;
                dataGridView1.Columns[6].Width = 1100 / twipperdot;
                dataGridView1.Columns[7].Width = 1100 / twipperdot;//1300
                dataGridView1.Columns[8].Width = 1100 / twipperdot;
                dataGridView1.Columns[9].Width = 1100 / twipperdot;
                dataGridView1.Columns[10].Width = 1100 / twipperdot;

                return dataGridView1.RowCount;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Filtering - " + ex.Message);
                return -1;
            }
        }



        private static bool FormatGrid(DataGridView dataGridView)
        {
            try
            {
                int savedRow;

                // DataGridViewに対する初期処理
                dataGridView.SuspendLayout();

                // 描画を抑止する
                dataGridView.SuspendLayout();

                // 現在行を保持する
                savedRow = dataGridView.CurrentRow.Index;

                for (int row = 0; row < dataGridView.Rows.Count; row++)
                {
                    // 行番号を表示する
                    //dataGridView.Rows[row].Cells[0].Value = row + 1;

                    // 他の行のフォーマット処理
                    //dataGridView.Rows[row].Cells[1].Style.BackColor = Color.FromArgb(250, 250, 150);

                    if (Convert.ToInt32(dataGridView.Rows[row].Cells[9].Value) <= Convert.ToInt32(dataGridView.Rows[row].Cells[10].Value))
                    {
                        // 在庫警告がある場合の処理

                        // 在庫警告があるシリーズの全ての日付に対して"■"を表示
                        for (int row2 = 0; row2 < dataGridView.Rows.Count; row2++)
                        {
                            if (dataGridView.Rows[row2].Cells[0].Value.ToString() == dataGridView.Rows[row].Cells[0].Value.ToString())
                            {
                                dataGridView.Rows[row2].Cells[2].Value = "■";
                            }
                        }

                        // 在庫警告がある日付の残数を強調表示する
                        dataGridView.Rows[row].Cells[9].Style.ForeColor = Color.Red;
                    }
                }

                // カーソル位置の復元などの後処理
                dataGridView.CurrentCell = dataGridView.Rows[savedRow].Cells[0];
                dataGridView.ResumeLayout();

                // 処理が成功したことを示すために true を返す
                return true;
            }
            catch (Exception ex)
            {
                // エラーハンドリング
                Console.WriteLine("FormatGrid - " + ex.Message);
                return false;
            }
        }





        private void DataGridView1_CellPainting(object sender,
    DataGridViewCellPaintingEventArgs e)
        {
            //列ヘッダーかどうか調べる
            if (e.ColumnIndex < 0 && e.RowIndex >= 0)
            {
                dataGridView1.SuspendLayout();
                //セルを描画する
                e.Paint(e.ClipBounds, DataGridViewPaintParts.All);

                //行番号を描画する範囲を決定する
                //e.AdvancedBorderStyleやe.CellStyle.Paddingは無視
                Rectangle indexRect = e.CellBounds;
                indexRect.Inflate(-2, -2);
                //行番号を描画する
                TextRenderer.DrawText(e.Graphics,
                    (e.RowIndex + 1).ToString(),
                    e.CellStyle.Font,
                    indexRect,
                    e.CellStyle.ForeColor,
                    TextFormatFlags.Right | TextFormatFlags.VerticalCenter);
                //描画が完了したことを知らせる
                e.Handled = true;
                dataGridView1.ResumeLayout();

            }
        }

        //ダブルクリックでシリーズフォームを開く　入庫コードを渡す
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // ヘッダー行でない場合
            {
                string selectedData = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(); // 1列目のデータを取得

                F_シリーズ targetform = new F_シリーズ();

                targetform.args = selectedData;
                targetform.ShowDialog();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // ヘッダー行でない場合
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }


        private bool sorting;
        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            if (!sorting)
            {
                sorting = true;

                // DataGridViewのソートが完了したら、先頭行を選択する
                if (dataGridView1.Rows.Count > 0)
                {
                    Cleargrid(dataGridView1);

                }

                sorting = false;
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            // Shiftキーが押されているときは何もしない
            if (e.Shift)
            {
                e.SuppressKeyPress = true;
            }
        }

        //選択行をクリアして先頭を表示して先頭行を選択
        private void Cleargrid(DataGridView dataGridView)
        {
            dataGridView.ClearSelection();

            if (dataGridView.Rows.Count > 0)
            {
                dataGridView.Rows[0].Selected = true;
                dataGridView.FirstDisplayedScrollingRowIndex = 0; // 先頭行を表示
            }
        }



       



        private void F_シリーズ在庫参照_FormClosing(object sender, FormClosingEventArgs e)
        {
            string LoginUserCode = "000";//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            LocalSetting test = new LocalSetting();
            test.SavePlace(LoginUserCode, this);
        }

        private void コマンド終了_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        private void コマンド更新_Click(object sender, EventArgs e)
        {
            DoUpdate();
            Cleargrid(dataGridView1);
        }

    

       

        private void コマンドシリーズ_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // DataGridView1で選択された行が存在する場合
                string selectedData = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); // 1列目のデータを取得

                // シリーズフォームを作成し、引数を設定して表示
                F_シリーズ targetform = new F_シリーズ();
                targetform.args = selectedData;
                targetform.ShowDialog();
            }
            else
            {
                // ユーザーが行を選択していない場合のエラーハンドリング
                MessageBox.Show("行が選択されていません。");
            }
        }


        private void コマンド再計算_Click(object sender, EventArgs e)
        {

            Connect();

            using (SqlCommand cmd = new SqlCommand("SPシリーズ在庫再計算", cn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TargetDate", SqlDbType.DateTime)).Value = TargetDay;

                SqlDataReader reader = cmd.ExecuteReader();


            }
        }

        private void コマンド締め_Click(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();

            try
            {

                // 商品在庫明細にフォーカスを設定
                dataGridView1.Focus();

                // メッセージボックスでユーザに確認
                DialogResult result = MessageBox.Show("今日以前の在庫を締めますか？" + Environment.NewLine +
                                                      "今日の在庫は締められません。",
                                                      "締めコマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                // ユーザがNoを選択した場合は終了
                if (result == DialogResult.No)
                    return;

                
                fn.DoWait("しばらくお待ちください...");



                // CloseStockメソッドの呼び出し
                if (CloseStock(TargetDay) != 0)
                {
                    MessageBox.Show("在庫の締め処理が失敗しました。" + Environment.NewLine +
                                    "表示内容は正確ではありません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                fn.WaitForm.Close();
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_コマンド締め_Click - " + ex.Message);
                MessageBox.Show("エラーが発生しました。" + Environment.NewLine +
                                "[" + this.Text + "]を終了します。",
                                "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);

                fn.WaitForm.Close();
            }
       
        }


        private int CloseStock(DateTime targetDate)
        {
            try
            {
                Connect();
                using (SqlCommand command = new SqlCommand("SPシリーズ在庫締め処理", cn))
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TargetDate", targetDate);

                    command.ExecuteNonQuery();
                }
                

                return 0; // 成功
            }
            catch (Exception ex)
            {
                Console.WriteLine("エラーが発生しました: " + ex.Message);
                return 1; // エラー
            }
        }

        private void コマンド補正_Click(object sender, EventArgs e)
        {
            DateTime today = DateTime.Parse(今日の日付.Text);
            DateTime currentDate = DateTime.Parse(現在日.Text);

            if (today < currentDate)
            {
                MessageBox.Show("補正を実行できません。" + Environment.NewLine +
                                "今日の日付時点の在庫に対して補正を実行してください。", "警告",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            else
            {
                F_シリーズ在庫補正 targetform = new F_シリーズ在庫補正();
                targetform.strシリーズコード = CurrentSeries;
                targetform.strシリーズ名 = CurrentSeriesName;
                targetform.dtm確認日 = CurrentDay;
                targetform.lng在庫数量 = CurrentStock;
                targetform.lng補正数量 = CurrentAdjust;

                targetform.ShowDialog();

                DoUpdate();
            }
        }



        private void シリーズコード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Control control = (Control)sender;
                string strCode = control.Text.Trim();

                if (string.IsNullOrEmpty(strCode))
                {
                    return;
                }

                strCode = strCode.PadLeft(8, '0');

                if (strCode != control.Text)
                {
                    control.Text = strCode;
                }
            }
        }




        private void 現在日_Validated(object sender, EventArgs e)
        {


            DateTime today = DateTime.Parse(今日の日付.Text);
            DateTime currentDate = DateTime.Parse(現在日.Text);

            if (today < currentDate)
            {
                // 未来の日付は指定できません
                現在日.Text = null;
                MessageBox.Show("未来の日付は指定できません。", "警告",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                現在日.Text = 今日の日付.Text;
            }

            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");

            try
            {

                dataGridView1.Focus();



                TargetDay = DateTime.Parse(現在日.Text);

                DoUpdate();


                fn.WaitForm.Close();


            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_現在日_AfterUpdate - " + ex.Message);
                fn.WaitForm.Close();
                MessageBox.Show("エラーが発生しました。" + Environment.NewLine +
                                "[" + this.Text + "]を終了します。",
                                "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 現在日戻るボタン_Click(object sender, EventArgs e)
        {
            if (DateTime.TryParse(現在日.Text, out DateTime currentDate))
            {
                // 現在の日付を1日進める
                DateTime prevDate = currentDate.AddDays(-1);

                // 進めた日付をテキストボックスに設定
                現在日.Text = prevDate.ToString("yyyy/MM/dd");
                現在日_Validated(sender, e);
            }
        }


        private F_カレンダー dateSelectionForm;

      
        private void 現在日選択ボタン_Click(object sender, EventArgs e)
        {
            dateSelectionForm = new F_カレンダー();
            if (dateSelectionForm.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                現在日.Text = selectedDate;
                現在日_Validated(sender, e);
            }
        }

        private void 現在日進むボタン_Click(object sender, EventArgs e)
        {
            if (DateTime.TryParse(現在日.Text, out DateTime currentDate))
            {
                // 現在の日付を1日進める
                DateTime nextDate = currentDate.AddDays(1);

                // 進めた日付をテキストボックスに設定
                現在日.Text = nextDate.ToString("yyyy/MM/dd");
                現在日_Validated(sender, e);
            }

        }

        
    }
}