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
    public partial class F_購買申請管理 : MidForm
    {
        public string str検索コード = CommonConstants.CH_MAKER;
        public string strメーカーコード開始 = "";
        public string strメーカーコード終了 = "";
        public string strSearchCode = "";
        public string strメーカー名 = "";
        public string str担当者名 = "";
        public string str担当者メールアドレス = "";
        public DateTime dtm更新日開始 = DateTime.MinValue;
        public DateTime dtm更新日終了 = DateTime.MinValue;
        public string str更新者名 = "";
        //public int intComposedChipMount = 0;
        //public int intIsUnit = 0;
        //public int lngDiscontinued = 0;
        public int lngDeleted = 0;

        int intWindowHeight = 0;
        int intWindowWidth = 0;

        private Control? previousControl;
        private SqlConnection? cn;
        public F_購買申請管理()
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
        public override void SearchCode(string codeString)
        {
            strSearchCode = codeString;
            strメーカーコード開始 = strSearchCode;
            strメーカーコード終了 = strSearchCode;
            if (DoUpdate() == -1)
            {
                MessageBox.Show("エラーが発生しました。");
            }

        }
        private void InitializeFilter()
        {
            this.strメーカー名 = "";
            this.str担当者名 = "";
            this.str担当者メールアドレス = "";
            this.dtm更新日開始 = DateTime.MinValue;
            this.dtm更新日終了 = DateTime.MinValue;
            this.str更新者名 = "";
            this.lngDeleted = 1;
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
            購買申請明細.AllowUserToResizeColumns = true;
            購買申請明細.Font = new Font("MS ゴシック", 10);
            購買申請明細.DefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 210, 255);
            購買申請明細.DefaultCellStyle.SelectionForeColor = Color.Black;
            購買申請明細.GridColor = Color.FromArgb(230, 230, 230);
            購買申請明細.ColumnHeadersDefaultCellStyle.Font = new Font("MS ゴシック", 9);
            購買申請明細.DefaultCellStyle.Font = new Font("MS ゴシック", 10);
            購買申請明細.DefaultCellStyle.ForeColor = Color.Black;


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
            Cleargrid(購買申請明細);
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            try
            {
                if (this.Height > 800)
                {
                    購買申請明細.Height = 購買申請明細.Height + (this.Height - intWindowHeight);
                    intWindowHeight = this.Height;  // 高さ保存

                    購買申請明細.Width = 購買申請明細.Width + (this.Width - intWindowWidth);
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

                // メーカーコード指定
                if (!string.IsNullOrEmpty(strメーカーコード開始))
                {
                    filter += string.Format("(メーカーコード BETWEEN '{0}' AND '{1}') AND ",
                                                                  strメーカーコード開始, strメーカーコード終了);
                }

                // メーカー名指定
                if (!string.IsNullOrEmpty(strメーカー名))
                {
                    string[] arr1 = strメーカー名.Split(' ');
                    foreach (var var1 in arr1)
                    {
                        string str1 = var1.ToString();
                        if (!string.IsNullOrEmpty(str1))
                        {
                            filter += string.Format("メーカー名 LIKE '%{0}%' AND ", str1);
                        }
                    }
                }

                // 担当者名指定
                if (!string.IsNullOrEmpty(str担当者名))
                {
                    filter += string.Format("担当者名 LIKE '%{0}%' AND ", str担当者名);
                }

                // 担当者メールアドレス指定
                if (!string.IsNullOrEmpty(str担当者メールアドレス))
                {
                    filter += string.Format("担当者メールアドレス LIKE '%{0}%' AND ", str担当者メールアドレス);
                }
                // 更新日時
                if (dtm更新日開始 != DateTime.MinValue)
                {
                    filter += "'" + dtm更新日開始 + "' <= 更新日時 AND 更新日時 <= '" + dtm更新日終了 + "' AND ";
                }
                // 更新者名
                if (!string.IsNullOrEmpty(str更新者名))
                {
                    filter += "更新者名 = '" + str更新者名 + "' AND ";
                }


                // 削除
                switch (lngDeleted)
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

                string query = "SELECT * FROM Vメーカー管理 WHERE 1=1 AND " + filter + " ORDER BY メーカーコード DESC ";

                Connect();
                DataGridUtils.SetDataGridView(cn, query, this.購買申請明細);


                MyApi myapi = new MyApi();
                int xSize, ySize, intpixel, twipperdot;

                //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
                intpixel = myapi.GetLogPixel();
                twipperdot = myapi.GetTwipPerDot(intpixel);

                intWindowHeight = this.Height;
                intWindowWidth = this.Width;

                // DataGridViewの設定
                購買申請明細.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200); // 薄い黄色

                // 列の幅を設定 もとは恐らくtwipのためピクセルに直す

                //0列目はaccessでは行ヘッダのため、ずらす
                //dataGridView1.Columns[0].Width = 500 / twipperdot;
                購買申請明細.Columns[0].Width = 1200 / twipperdot; //1150
                購買申請明細.Columns[1].Width = 4000 / twipperdot;
                購買申請明細.Columns[2].Visible = false;
                購買申請明細.Columns[3].Width = 1600 / twipperdot;
                購買申請明細.Columns[4].Width = 1600 / twipperdot;
                購買申請明細.Columns[5].Width = 2000 / twipperdot;
                購買申請明細.Columns[6].Width = 3000 / twipperdot;
                購買申請明細.Columns[7].Width = 2200 / twipperdot;//1300
                購買申請明細.Columns[8].Width = 1200 / twipperdot;
                購買申請明細.Columns[9].Width = 400 / twipperdot;


                return 購買申請明細.RowCount;
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
                購買申請明細.SuspendLayout();
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
                購買申請明細.ResumeLayout();

            }
        }

        //ダブルクリックでメーカーフォームを開く　メーカーコードを渡す
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.Button != MouseButtons.Left) return; // 左ボタンのダブルクリック以外は無視

            if (e.RowIndex >= 0) // ヘッダー行でない場合
            {
                string selectedData = 購買申請明細.Rows[e.RowIndex].Cells[0].Value.ToString(); // 1列目のデータを取得

                F_メーカー targetform = new F_メーカー();

                targetform.args = selectedData;
                targetform.ShowDialog();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // ヘッダー行でない場合
            {
                購買申請明細.ClearSelection();
                購買申請明細.Rows[e.RowIndex].Selected = true;
            }
        }


        private bool sorting;
        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            if (!sorting)
            {
                sorting = true;

                // DataGridViewのソートが完了したら、先頭行を選択する
                if (購買申請明細.Rows.Count > 0)
                {
                    Cleargrid(購買申請明細);

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

        private void コマンド終了_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void コマンドメール_Click(object sender, EventArgs e)
        {







        }

        private void コマンド印刷_Click(object sender, EventArgs e)
        {
            購買申請明細.Focus(); // DataGridViewにフォーカスを設定

            MessageBox.Show("現在開発中です。", "印刷コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド抽出_Click(object sender, EventArgs e)
        {
            購買申請明細.Focus();
            F_メーカー管理_抽出 form = new F_メーカー管理_抽出();
            form.ShowDialog();
        }

        private void コマンド入出力_Click(object sender, EventArgs e)
        {
            購買申請明細.Focus(); // DataGridViewにフォーカスを設定

            MessageBox.Show("現在開発中です。", "入出力コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド初期化_Click(object sender, EventArgs e)
        {
            InitializeFilter();
            DoUpdate();
            Cleargrid(購買申請明細);
        }

        private void コマンド全表示_Click(object sender, EventArgs e)
        {
            InitializeFilter();
            DoUpdate();
            Cleargrid(購買申請明細);
        }

        private void コマンド更新_Click(object sender, EventArgs e)
        {

            DoUpdate();
            Cleargrid(購買申請明細);

        }

        private void コマンド検索_Click(object sender, EventArgs e)
        {
            F_検索コード form = new F_検索コード(this, null);
            form.ShowDialog();
        }

        private void コマンドメーカー_Click(object sender, EventArgs e)
        {
            if (購買申請明細.SelectedRows.Count > 0)
            {
                // DataGridView1で選択された行が存在する場合
                string selectedData = 購買申請明細.SelectedRows[0].Cells[0].Value.ToString(); // 1列目のデータを取得

                // メーカーフォームを作成し、引数を設定して表示
                F_メーカー targetform = new F_メーカー();
                targetform.args = selectedData;
                targetform.ShowDialog();
            }
            else
            {
                // ユーザーが行を選択していない場合のエラーハンドリング
                MessageBox.Show("行が選択されていません。");
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
                        if (this.コマンド更新.Enabled) コマンド全表示_Click(null, null);
                        break;
                    case Keys.F5:
                        if (this.コマンド購買申請.Enabled) コマンドメーカー_Click(null, null);
                        break;
                    case Keys.F6:
                        if (this.button2.Enabled) コマンドメール_Click(null, null);
                        break;
                    case Keys.F9:
                        if (this.コマンド印刷.Enabled) コマンド更新_Click(null, null);
                        break;
                    case Keys.F10:
                        if (this.コマンド入出力.Enabled) コマンド印刷_Click(null, null);
                        break;
                    case Keys.F11:
                        if (this.コマンド保守.Enabled) コマンド入出力_Click(null, null);
                        break;
                    case Keys.F12:
                        if (this.コマンド終了.Enabled) コマンド終了_Click(null, null);
                        break;
                    case Keys.Return:
                        if (this.ActiveControl == this.購買申請明細)
                        {
                            if (購買申請明細.SelectedRows.Count > 0)
                            {
                                // DataGridView1で選択された行が存在する場合
                                string selectedData = 購買申請明細.SelectedRows[0].Cells[0].Value.ToString(); // 1列目のデータを取得

                                // メーカーフォームを作成し、引数を設定して表示
                                F_メーカー targetform = new F_メーカー();
                                targetform.args = selectedData;
                                targetform.ShowDialog();
                            }
                            else
                            {
                                // ユーザーが行を選択していない場合のエラーハンドリング
                                MessageBox.Show("行が選択されていません。");
                            }
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("KeyDown - " + ex.Message);
            }
        }

        private void F_メーカー管理_FormClosing(object sender, FormClosingEventArgs e)
        {
            string LoginUserCode = "000";//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            LocalSetting test = new LocalSetting();
            test.SavePlace(LoginUserCode, this);
        }


        private bool ascending = true;

        //顧客名ラベルをクリックで顧客名カナでソートする
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 1)
            {
                if (ascending)
                {
                    購買申請明細.Sort(購買申請明細.Columns[2], System.ComponentModel.ListSortDirection.Ascending);
                }
                else
                {
                    購買申請明細.Sort(購買申請明細.Columns[2], System.ComponentModel.ListSortDirection.Descending);
                }
                ascending = !ascending;
            }
        }
    }
}