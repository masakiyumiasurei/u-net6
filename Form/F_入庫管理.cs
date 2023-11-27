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
    public partial class F_入庫管理 : MidForm
    {
        public string str入庫コード開始;
        public string str入庫コード終了;
        public DateTime dtm入庫日開始;
        public DateTime dtm入庫日終了;
        public string str入庫者名;
        public string str集計年月;
        public string str支払年月;
        public string str発注コード;
        public string str仕入先コード;
        public string str仕入先名;
        public long lng確定指定;
        public long lng棚卸指定;
        public long lng削除指定;
        public bool innerEXT;


        int intWindowHeight = 0;
        int intWindowWidth = 0;

        private Control? previousControl;
        private SqlConnection? cn;
        public F_入庫管理()
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
    
        private void InitializeFilter()
        {
            lng確定指定 = 0;
            lng棚卸指定 = 1;
            lng削除指定 = 1;
        }
        private void Form_Load(object sender, EventArgs e)
        {

            //実行中フォーム起動
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

            InitializeFilter();
            DoUpdate();
            Cleargrid(dataGridView1);
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
                result = Filtering();
                //   DrawGrid();
                if (result >= 0)
                {
                    this.表示件数.Text = result.ToString();
                }
                else
                {
                    this.表示件数.Text = null; // Nullの代わりにC#ではnullを使用
                }
            }
            catch (Exception ex)
            {
                result = -1;
                MessageBox.Show(this.Name + "_DoUpdate - " + ex.Message);
            }

            return result;
        }

        private int Filtering()
        {
            try
            {
                string filter = string.Empty;

                // 入庫者名指定
                if (dtm入庫日開始 != DateTime.MinValue && dtm入庫日終了 != DateTime.MinValue)
                {
                    filter += string.Format("'{0}'<=入庫日 AND 入庫日<='{1}' AND ",dtm入庫日開始.ToString("yyyy/MM"), dtm入庫日終了.ToString("yyyy/MM"));
                }



                // 入庫者名指定
                if (!string.IsNullOrEmpty(str入庫者名))
                {
                    filter += string.Format("入庫者名 like '%{0}%' AND ", str入庫者名);
                }

                // 集計年月指定
                if (!string.IsNullOrEmpty(str集計年月))
                {
                    filter += string.Format("集計年月 = '{0}' AND ", str集計年月);
                }

                // 支払年月指定
                if (!string.IsNullOrEmpty(str支払年月))
                {
                    filter += string.Format("支払年月 = '{0}' AND ", str支払年月);
                }

                // 発注コード指定
                if (!string.IsNullOrEmpty(str発注コード))
                {
                    filter += string.Format("支払年月 = '{0}' AND ", str発注コード);
                }


                // 仕入先名指定
                if (!string.IsNullOrEmpty(str仕入先名))
                {
                    filter += string.Format("仕入先名 LIKE '%{0}%' AND ", str仕入先名);
                }


        

                // 使用
                switch (lng確定指定)
                {
                    case 1:
                        filter += "確定 IS NULL AND ";
                        break;
                    case 2:
                        filter += "確定 IS NOT NULL AND ";
                        break;
                }


                // 廃止
                switch (lng棚卸指定)
                {
                    case 1:
                        filter += "棚卸 IS NULL AND ";
                        break;
                    case 2:
                        filter += "棚卸 IS NOT NULL AND ";
                        break;
                }



                // 削除
                switch (lng削除指定)
                {
                    case 1:
                        filter += "削除 IS NULL AND ";
                        break;
                    case 2:
                        filter += "削除 IS NOT NULL AND ";
                        break;
                }
                if (!string.IsNullOrEmpty(filter))
                {
                    filter = filter.Substring(0, filter.Length - 5); // 最後の " AND " を削除
                }

                string query = "SELECT * FROM V入庫管理 WHERE 1=1 AND " + filter + " ORDER BY 入庫コード DESC ";

                Connect();
                DataGridUtils.SetDataGridView(cn, query, this.dataGridView1);


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
                dataGridView1.Columns[0].Width = 1400 / twipperdot; //1150
                dataGridView1.Columns[1].Width = 1300 / twipperdot;
                dataGridView1.Columns[2].Width = 1500 / twipperdot;
                dataGridView1.Columns[3].Width = 1200 / twipperdot;
                dataGridView1.Columns[4].Width = 1200 / twipperdot;
                dataGridView1.Columns[5].Width = 1400 / twipperdot;
                dataGridView1.Columns[6].Width = 400 / twipperdot;
                dataGridView1.Columns[7].Width = 4000 / twipperdot;//1300
                dataGridView1.Columns[8].Width = 400 / twipperdot;
                dataGridView1.Columns[9].Width = 400 / twipperdot;
                dataGridView1.Columns[10].Width = 400 / twipperdot;

                return dataGridView1.RowCount;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Filtering - " + ex.Message);
                return -1;
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

        //ダブルクリックで入庫フォームを開く　入庫コードを渡す
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.Button != MouseButtons.Left) return; // 左ボタンのダブルクリック以外は無視

            //if (e.RowIndex >= 0) // ヘッダー行でない場合
            //{
            //    string selectedData = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(); // 1列目のデータを取得

            //    F_入庫 targetform = new F_入庫();

            //    targetform.args = selectedData;
            //    targetform.ShowDialog();
            //}
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

       

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                switch (e.KeyCode)
                {
                    case Keys.F1:
                        if (this.コマンド抽出.Enabled) コマンド抽出_Click(null, null);
                        break;
                    case Keys.F2:
                        if (this.コマンド検索.Enabled) コマンド検索_Click(null, null);
                        break;
                    case Keys.F3:
                        if (this.コマンド初期化.Enabled) コマンド初期化_Click(null, null);
                        break;
                    case Keys.F4:
                        if (this.コマンド更新.Enabled) コマンド更新_Click(null, null);
                        break;
                    case Keys.F5:
                        if (this.コマンド入庫.Enabled) コマンド入庫_Click(null, null);
                        break;
                    case Keys.F6:
                        if (this.コマンド発注.Enabled) コマンド発注_Click(null, null);
                        break;
                    case Keys.F9:
                        if (this.コマンド印刷.Enabled) コマンド印刷_Click(null, null);
                        break;
                    case Keys.F10:
                        if (this.コマンド入出力.Enabled) コマンド入出力_Click(null, null);
                        break;
                    case Keys.F11:
                        if (this.コマンド保守.Enabled) コマンド保守_Click(null, null);
                        break;
                    case Keys.F12:
                        if (this.コマンド終了.Enabled) コマンド終了_Click(null, null);
                        break;
                    //case Keys.Return:
                    //    if (this.ActiveControl == this.dataGridView1)
                    //    {
                    //        if (dataGridView1.SelectedRows.Count > 0)
                    //        {
                    //            // DataGridView1で選択された行が存在する場合
                    //            string selectedData = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); // 1列目のデータを取得

                    //            // 入庫フォームを作成し、引数を設定して表示
                    //            F_入庫 targetform = new F_入庫();
                    //            targetform.args = selectedData;
                    //            targetform.ShowDialog();
                    //        }
                    //        else
                    //        {
                    //            // ユーザーが行を選択していない場合のエラーハンドリング
                    //            MessageBox.Show("行が選択されていません。");
                    //        }
                    //    }
                    //    break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("KeyDown - " + ex.Message);
            }
        }

        private void F_入庫管理_FormClosing(object sender, FormClosingEventArgs e)
        {
            string LoginUserCode = CommonConstants.LoginUserCode;//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            LocalSetting test = new LocalSetting();
            test.SavePlace(LoginUserCode, this);
        }

        private void コマンド終了_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void コマンド抽出_Click(object sender, EventArgs e)
        {
            dataGridView1.Focus();
            F_入庫管理_抽出 form = new F_入庫管理_抽出();
            form.ShowDialog();
        }



        private void コマンド初期化_Click(object sender, EventArgs e)
        {
            //InitializeFilter();
            //DoUpdate();
            //Cleargrid(dataGridView1);

            dataGridView1.Focus(); // DataGridViewにフォーカスを設定

            MessageBox.Show("現在開発中です。", "初期化マンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }





        private void コマンド検索_Click(object sender, EventArgs e)
        {
            dataGridView1.Focus(); // DataGridViewにフォーカスを設定

            MessageBox.Show("現在開発中です。", "検索マンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private void コマンド更新_Click(object sender, EventArgs e)
        {
            DoUpdate();
            Cleargrid(dataGridView1);
        }




        private void コマンド保守_Click(object sender, EventArgs e)
        {
            dataGridView1.Focus(); // DataGridViewにフォーカスを設定

            MessageBox.Show("機能が未定義のため、コマンドは使用できません。", "保守コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド入庫_Click(object sender, EventArgs e)
        {

        }

        private void コマンド発注_Click(object sender, EventArgs e)
        {

        }

        private void コマンド印刷_Click(object sender, EventArgs e)
        {
            dataGridView1.Focus(); // DataGridViewにフォーカスを設定

            MessageBox.Show("現在開発中です。", "印刷コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド入出力_Click(object sender, EventArgs e)
        {
            dataGridView1.Focus(); // DataGridViewにフォーカスを設定

            MessageBox.Show("現在開発中です。", "入出力コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}