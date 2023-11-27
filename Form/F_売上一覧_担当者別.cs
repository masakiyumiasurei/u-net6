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
    public partial class F_売上一覧_担当者別 : MidForm
    {
        public string str集計年度;

        int intWindowHeight = 0;
        int intWindowWidth = 0;

        public string args = "";

        private Control? previousControl;
        private SqlConnection? cn;
        public F_売上一覧_担当者別()
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

            using (SqlCommand cmd = new SqlCommand("SP売上年度", cn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();

                // レコードセットを設定
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);

                集計年度.DisplayMember = "売上年度";
                集計年度.ValueMember = "売上年度";
                集計年度.DataSource = dataTable;


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

       

        private void F_売上一覧_担当者別_FormClosing(object sender, FormClosingEventArgs e)
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
                if (string.IsNullOrEmpty(str集計年度))
                {
                    MessageBox.Show("出力できるデータがありません。\n集計年度を指定してください。",
                                    "出力コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    集計年度.Focus();
                    return;
                }

                string strSQL;

                // SQLを作成する
                strSQL = $"EXEC SP売上一覧_担当者別 '{str集計年度}'";

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

        private void コピーボタン_Click(object sender, EventArgs e)
        {
            try
            {
                // 選択されているセルのデータを取得
                DataObject dataObject = dataGridView1.GetClipboardContent();

                // クリップボードにコピー
                Clipboard.SetDataObject(dataObject);

                MessageBox.Show("クリップボードへコピーしました。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                // コピーに失敗した場合はエラーメッセージを表示
                Console.WriteLine("クリップボードへのコピーに失敗しました。" + ex.Message);

            }
        }

        private void 集計年月_SelectedIndexChanged(object sender, EventArgs e)
        {
            

            str集計年度 = 集計年度.Text;

            if (string.IsNullOrEmpty(集計年度.Text)) return;


            FunctionClass fn = new FunctionClass();
            fn.DoWait("集計しています...");

            if (Filtering(集計年度.Text))
            {
                コピーボタン.Enabled = true;
            }
            else
            {
                コピーボタン.Enabled = false;
                MessageBox.Show($"エラーが発生しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }




            fn.WaitForm.Close();
        }






        private  bool Filtering(string yearString)
        {
            bool success = false;

            Connect();

            try
            {

                
               
                using (SqlCommand command =  new SqlCommand("SP売上一覧_担当者別", cn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SalesYear", yearString);


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

                    // 各月ごとの金額合計行を追加する処理（AddTotalRow 関数）を呼び出す
                    AddTotalRow(dataGridView1);

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
                    dataGridView1.Columns[0].Width = 1000 / twipperdot; //1150
                    dataGridView1.Columns[1].Width = 1300 / twipperdot;
                    dataGridView1.Columns[2].Width = 1300 / twipperdot;
                    dataGridView1.Columns[3].Width = 1300 / twipperdot;
                    dataGridView1.Columns[4].Width = 1300 / twipperdot;
                    dataGridView1.Columns[5].Width = 1300 / twipperdot;
                    dataGridView1.Columns[6].Width = 1300 / twipperdot;
                    dataGridView1.Columns[7].Width = 1300 / twipperdot;//1300
                    dataGridView1.Columns[8].Width = 1300 / twipperdot;
                    dataGridView1.Columns[9].Width = 1300 / twipperdot;
                    dataGridView1.Columns[10].Width = 1300 / twipperdot;
                    dataGridView1.Columns[11].Width = 1300 / twipperdot;
                    dataGridView1.Columns[12].Width = 1300 / twipperdot;
                    dataGridView1.Columns[13].Width = 1300 / twipperdot;



                }
                
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return success;
        }

        private static void AddTotalRow(DataGridView dataGridView)
        {
            try
            {
                int rowCount = dataGridView.Rows.Count;
                int colCount = dataGridView.Columns.Count;


                BindingSource bindingSource = (BindingSource)dataGridView.DataSource;
                bindingSource.AddNew();


                // 合計行に表示する文字列
                dataGridView.Rows[rowCount].Cells[0].Value = "(合計)";

                // 列ごとの合計金額を計算し、表示する
                for (int col = 1; col <= colCount; col++)
                {
                    long sum = 0;

                    // 列ごとに合計金額を計算
                    for (int row = 0; row < rowCount; row++)
                    {
                        // データグリッドビューのセルの値が数値であることを仮定
                        object cellValue = dataGridView.Rows[row].Cells[col].Value;
                        if (cellValue != null && cellValue.ToString() != "")
                        {
                            sum += Convert.ToInt64(cellValue);
                        }

                    }

                    // 合計をセルに表示
                    dataGridView.Rows[rowCount].Cells[col].Value = sum;

                    // セルのフォーマットを設定して桁区切りにする
                    dataGridView.Columns[col].DefaultCellStyle.Format = "#,###,###,##0";
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }



        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            AddTotalRow(dataGridView1);
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex > 0)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);
            }
        }




     
    }
}