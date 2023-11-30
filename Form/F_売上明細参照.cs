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
using u_net.Public;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;


namespace u_net
{
    public partial class F_売上明細参照 : MidForm
    {
        public string str顧客コード;
        public string str売上年月開始;
        public string str売上年月終了;

        int intWindowHeight = 0;
        int intWindowWidth = 0;

        public string args = "";

        private Control? previousControl;
        private SqlConnection? cn;
        public F_売上明細参照()
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

      
        private void Form_Load(object sender, EventArgs e)
        {

            string LoginUserCode = CommonConstants.LoginUserCode;//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(LoginUserCode, this);

            MyApi myapi = new MyApi();
            int xSize, ySize, intpixel, twipperdot;

            //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
            intpixel = myapi.GetLogPixel();
            twipperdot = myapi.GetTwipPerDot(intpixel);

            intWindowHeight = this.Height;
            intWindowWidth = this.Width;


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



            if (!string.IsNullOrEmpty(args))
            {
                顧客コード.Text = args;
                str顧客コード = args;
                顧客名.Text = FunctionClass.GetCustomerName(cn, str顧客コード);
            }

            





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

        


        private void コマンド終了_Click(object sender, EventArgs e)
        {
            this.Close();
        }

       

        private void F_売上明細参照_FormClosing(object sender, FormClosingEventArgs e)
        {
            string LoginUserCode = CommonConstants.LoginUserCode;//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            LocalSetting test = new LocalSetting();
            test.SavePlace(LoginUserCode, this);
        }

        private void コマンド出力_Click(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Focus();

                // 表示されているデータがない場合はユーザーに知らせる
                if (string.IsNullOrEmpty(str顧客コード) || string.IsNullOrEmpty(str売上年月開始) || string.IsNullOrEmpty(str売上年月終了))
                {
                    MessageBox.Show("出力できるデータがありません。\n抽出条件を指定してください。",
                                    "出力コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    売上年月.Focus();
                    return;
                }

                string strSQL;

                // SQLを作成する
                strSQL = $"EXEC SP売上明細参照 '{str顧客コード}', {str売上年月開始} , {str売上年月終了}";

                //F_出力 fm = new F_出力();
                //fm.args = strSQL;
                //fm.ShowDialog();

            }
            catch (Exception ex)
            {
                // エラー処理
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


      




        private  bool Filtering(string CustomerCode,string SalesMonthS,string SalesMonthE)
        {
            bool success = false;

            Connect();

            try
            {

                FunctionClass fn = new FunctionClass();
               
                using (SqlCommand command =  new SqlCommand("SP売上明細参照", cn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@CustomerCode", fn.Zn(CustomerCode));
                    command.Parameters.AddWithValue("@SalesMonthS", fn.Zn(SalesMonthS));
                    command.Parameters.AddWithValue("@SalesMonthE", fn.Zn(SalesMonthE));

                    // データベースからデータを取得して DataGridView に設定
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        //dataGridView1.DataSource = dataTable;

                        BindingSource bindingSource = new BindingSource();
                        bindingSource.DataSource = dataTable;

                        // DataGridView に BindingSource をバインド
                        dataGridView1.DataSource = bindingSource;
                    }


                    表示件数.Text = dataGridView1.RowCount.ToString();



                    // DataGridView に対する他の処理をここに追加する可能性があります

                    success = true;




                    MyApi myapi = new MyApi();
                    int xSize, ySize, intpixel, twipperdot;

                    //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
                    intpixel = myapi.GetLogPixel();
                    twipperdot = myapi.GetTwipPerDot(intpixel);

                    intWindowHeight = this.Height;
                    intWindowWidth = this.Width;

                    //0列目はaccessでは行ヘッダのため、ずらす
                    //dataGridView1.Columns[0].Width = 500 / twipperdot;
                    dataGridView1.Columns[0].Width = 1250 / twipperdot; 
                    dataGridView1.Columns[1].Width = 1200 / twipperdot;
                    dataGridView1.Columns[2].Width = 3600 / twipperdot;
                    dataGridView1.Columns[3].Width = 3600 / twipperdot;
                    dataGridView1.Columns[4].Width = 1200 / twipperdot;
                    dataGridView1.Columns[5].Width = 600 / twipperdot;
                    dataGridView1.Columns[6].Width = 600 / twipperdot;
                    dataGridView1.Columns[7].Width = 1500 / twipperdot;

                    dataGridView1.Columns[4].DefaultCellStyle.Format = "#,###,###,##0";
                    dataGridView1.Columns[5].DefaultCellStyle.Format = "#,###,###,##0";
                    dataGridView1.Columns[7].DefaultCellStyle.Format = "#,###,###,##0";


                    var TotalAmount = 0;
                    var TotalMoney = 0;

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
           
                        if (!row.IsNewRow && row.Cells.Count > 5 && row.Cells[5].Value != null)
                        {
                  
                            TotalAmount += Convert.ToInt32(row.Cells[5].Value);
                        }

                        if (!row.IsNewRow && row.Cells.Count > 7 && row.Cells[7].Value != null)
                        {
                            TotalMoney += Convert.ToInt32(row.Cells[7].Value);
                        }
                    }

                    合計数量.Text = TotalAmount.ToString();
                    税込合計金額.Text = TotalMoney.ToString("#,###,###,##0");

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return success;
        }




        public bool DoUpdate()
        {

            //if (string.IsNullOrEmpty(売上年月.Text)) return false;


            bool result = true;
            try
            {
                Filtering(str顧客コード,str売上年月開始,str売上年月終了);


            }
            catch (Exception ex)
            {
                result = false;
                MessageBox.Show(this.Name + "_DoUpdate - " + ex.Message);
            }


            return result;
        }











        private void コマンド更新_Click(object sender, EventArgs e)
        {
            DoUpdate();
        }

        private void 売上年月_SelectedIndexChanged(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");

            str売上年月開始 = 売上年月.Text;
            str売上年月終了 = 売上年月.Text;


            DoUpdate();
            


            fn.WaitForm.Close();
        }


        private F_検索 SearchForm;


  
        private void 顧客コード検索ボタン_Click(object sender, EventArgs e)
        {
            SearchForm = new F_検索();
            SearchForm.FilterName = "顧客名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                string SelectedCode = SearchForm.SelectedCode;

                顧客コード.Text = SelectedCode;
                顧客コード_Validated(sender, e);
            }
        }

        private void 顧客コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                顧客コード検索ボタン_Click(sender, e);
                e.Handled = true; // イベントの処理が完了したことを示す
            }
        }

        private void 顧客コード_KeyDown(object sender, KeyEventArgs e)
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

        private void 顧客コード_DoubleClick(object sender, EventArgs e)
        {
            SearchForm = new F_検索();
            SearchForm.FilterName = "顧客名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                string SelectedCode = SearchForm.SelectedCode;

                顧客コード.Text = SelectedCode;
                顧客コード_Validated(sender, e);
            }
        }



        // Nz メソッドの代替
        private T Nz<T>(T value)
        {
            if (value == null)
            {
                return default(T);
            }
            return value;
        }

        private void 顧客コード_Validated(object sender, EventArgs e)
        {

            Connect();

            str顧客コード = Nz(顧客コード.Text);

            顧客名.Text = FunctionClass.GetCustomerName(cn,str顧客コード);

            売上年月.SelectedValue = DBNull.Value;

            OriginalClass ofn = new OriginalClass();
            ofn.SetComboBox(売上年月, "SELECT 売上年月日 as Display, 売上年月日 as Value FROM V売上明細参照_年月一覧 WHERE 顧客コード='" + str顧客コード + "' ORDER BY 売上年月日 DESC");



        }
    }
}