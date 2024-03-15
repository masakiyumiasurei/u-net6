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
    public partial class F_売上一覧_区分別 : MidForm
    {
        public string str売上地区コード;
        public string str担当者コード;

        int intWindowHeight = 0;
        int intWindowWidth = 0;

        public string args = "";

        private Control? previousControl;
        private SqlConnection? cn;
        public F_売上一覧_区分別()
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

            System.Type dgvtype = typeof(DataGridView);
            System.Reflection.PropertyInfo dgvPropertyInfo = dgvtype.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            dgvPropertyInfo.SetValue(dataGridView1, true, null);

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

            using (SqlCommand cmd = new SqlCommand("SP売掛一覧_売掛年月", cn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();

                // レコードセットを設定
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);
             

                年月度.DisplayMember = "売掛年月";
                年月度.ValueMember = "売掛年月";
                年月度.DataSource = dataTable;

     

            }


        }

        private void Form_Resize(object sender, EventArgs e)
        {
            try
            {
                //if (this.Height > 800)
                //{
                //    dataGridView1.Height = dataGridView1.Height + (this.Height - intWindowHeight);
                //    intWindowHeight = this.Height;  // 高さ保存

                //    dataGridView1.Width = dataGridView1.Width + (this.Width - intWindowWidth);
                //    intWindowWidth = this.Width;    // 幅保存
                //}
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

       

        private void F_売上一覧_区分別_FormClosing(object sender, FormClosingEventArgs e)
        {
            string LoginUserCode = CommonConstants.LoginUserCode;//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            LocalSetting test = new LocalSetting();
            test.SavePlace(LoginUserCode, this);
        }

       
      


        private  bool Filtering(DateTime SalesMonth,string SalesUserCode)
        {
            bool success = false;

            Connect();

            try
            {

                FunctionClass fn = new FunctionClass();


                using (SqlCommand command =  new SqlCommand("SP売上一覧_区分別", cn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SalesMonth", SalesMonth);
                    command.Parameters.AddWithValue("@SalesUserCode", fn.Zn(SalesUserCode));


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
                    dataGridView1.Columns[0].Width = 1000 / twipperdot; //1150
                    dataGridView1.Columns[1].Width = 2600 / twipperdot;
                    dataGridView1.Columns[2].Visible = false;
                    dataGridView1.Columns[3].Width = 300 / twipperdot;
                    dataGridView1.Columns[4].Width = 1150 / twipperdot;
                    dataGridView1.Columns[5].Width = 1150 / twipperdot;
                    dataGridView1.Columns[6].Width = 1150 / twipperdot;
                    dataGridView1.Columns[7].Width = 1145 / twipperdot;//1300
                    dataGridView1.Columns[8].Width = 1145 / twipperdot;
                    dataGridView1.Columns[9].Width = 1100 / twipperdot;
                    dataGridView1.Columns[10].Width = 1150 / twipperdot;
                    dataGridView1.Columns[11].Width = 1150 / twipperdot;
                    dataGridView1.Columns[12].Width = 1150 / twipperdot;
                    dataGridView1.Columns[13].Width = 1145 / twipperdot;
                    dataGridView1.Columns[14].Width = 1145 / twipperdot;
                    dataGridView1.Columns[15].Width = 1100 / twipperdot;

                    for (int col = 4; col <= 15; col++)
                    {
                        dataGridView1.Columns[col].DefaultCellStyle.Format = "#,###,###,##0";
                    }

                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return success;
        }





        private bool Filtering2(DateTime SalesMonth, string SalesUserCode)
        {
            bool success = false;

            Connect();

            try
            {
                FunctionClass fn = new FunctionClass();


                using (SqlCommand command = new SqlCommand("SP売上一覧_区分別合計", cn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SalesMonth", SalesMonth);
                    command.Parameters.AddWithValue("@SalesUserCode", fn.Zn(SalesUserCode));


                    // データベースからデータを取得して DataGridView に設定
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        //dataGridView1.DataSource = dataTable;

                        BindingSource bindingSource = new BindingSource();
                        bindingSource.DataSource = dataTable;

                        // DataGridView に BindingSource をバインド
                        dataGridView2.DataSource = bindingSource;
                    }


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
                    dataGridView2.Columns[0].Width = 1500 / twipperdot; //1150
                    dataGridView2.Columns[1].Width = 1500 / twipperdot;
                    dataGridView2.Columns[2].Width = 1500 / twipperdot;
                    dataGridView2.Columns[3].Width = 1500 / twipperdot;
                    dataGridView2.Columns[4].Width = 1500 / twipperdot;
                    dataGridView2.Columns[5].Width = 1500 / twipperdot;
                    dataGridView2.Columns[6].Width = 1500 / twipperdot;

                    //for(int col = 1; col <= 6; col++)
                    //{
                    //    dataGridView2.Columns[col].DefaultCellStyle.Format = "#,###,###,##0";
                    //}
                    dataGridView2.Rows[0].DefaultCellStyle.Format = "#,###,###,##0";
                    dataGridView2.Rows[1].DefaultCellStyle.Format = "#,###,###,##0";
                    dataGridView2.Rows[2].DefaultCellStyle.Format = "#,0.0";

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

            if (string.IsNullOrEmpty(年月度.Text)) return false;

            FunctionClass fn = new FunctionClass();
            fn.DoWait("集計しています...");

            bool result = true;
            try
            {
                Filtering(DateTime.Parse(年月度.Text),str担当者コード);
                Filtering2(DateTime.Parse(年月度.Text), str担当者コード);

            }
            catch (Exception ex)
            {
                result = false;
                MessageBox.Show(this.Name + "_DoUpdate - " + ex.Message);
            }

            fn.WaitForm.Close();

            return result;
        }



        private void 年月度_SelectedIndexChanged(object sender, EventArgs e)
        {
            DoUpdate();
        }

        private void コマンド印刷_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。", "印刷コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド検索_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。", "検索コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド入出力_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。", "入出力コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド更新_Click(object sender, EventArgs e)
        {
            DoUpdate();
        }

        private void コマンド初期化_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。", "初期化コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド抽出_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(年月度.Text))
            {
                MessageBox.Show("抽出する前に集計年度を設定してください。", "抽出コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                年月度.Focus();
                return;
            }

            dataGridView1.Focus();
            F_売上一覧_区分別_抽出 targetform = new F_売上一覧_区分別_抽出();
            targetform.MdiParent = this.MdiParent;
            targetform.FormClosed += (s, args) => { this.Enabled = true; };
            this.Enabled = false;

            targetform.Show();

        }

        private void コマンド顧客_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string selectedData = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); // 1列目のデータを取得

                string trimmedAndReplaced = selectedData.TrimEnd().Replace(" ", "_");

                string replacedServerInstanceName = CommonConstants.ServerInstanceName.Replace(" ", "_");

                string param = $" -sv:{replacedServerInstanceName} -open:saleslistbyparentcustomer, {trimmedAndReplaced},1";
                FunctionClass.GetShell(param);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // ヘッダー行でない場合
            {
                string selectedData = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(); // 1列目のデータを取得

                string trimmedAndReplaced = selectedData.TrimEnd().Replace(" ", "_");

                string replacedServerInstanceName = CommonConstants.ServerInstanceName.Replace(" ", "_");

                string param = $" -sv:{replacedServerInstanceName} -open:saleslistbyparentcustomer, {trimmedAndReplaced},1";
                FunctionClass.GetShell(param);
            }
        }













        private void dataGridView1_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            try
            {
                // カスタムの比較ロジックを実装

                decimal curRow1 = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex1].Cells[e.Column.Index].Value);
                decimal curRow2 = Convert.ToDecimal(dataGridView1.Rows[e.RowIndex2].Cells[e.Column.Index].Value);

                int intSortSettings = 1; // intSortSettings の値は適切な値に設定してください

                if (curRow1 < curRow2)
                {
                    e.SortResult = -1 * intSortSettings;
                }
                else if (curRow2 < curRow1)
                {
                    e.SortResult = 1 * intSortSettings;
                }
                else
                {
                    e.SortResult = 0;
                }

                e.Handled = true; // イベントが処理されたことを示す
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_dataGridView1_SortCompare - " + ex.Message);
                // エラーハンドリングが必要な場合はここで適切な処理を追加してください
            }
        }
    }
}