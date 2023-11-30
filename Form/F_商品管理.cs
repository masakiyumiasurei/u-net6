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
    public partial class F_商品管理 : MidForm
    {
        public string str検索コード = "ORD";
        public string str基本型式名 = "";
        public string strシリーズ名 = "";
        public DateTime dtm更新日開始 = DateTime.MinValue;
        public DateTime dtm更新日終了 = DateTime.MinValue;
        public string str更新者名 = "";
        public int intComposedChipMount = 0;
        public int intIsUnit = 0;
        public int lngDiscontinued = 0;
        public int lngDeleted = 0;      

        int intWindowHeight = 0;
        int intWindowWidth = 0;

        private Control? previousControl;
        private SqlConnection? cn;
        public F_商品管理()
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
        public override void SearchCode(string searchcode)
        {
            MessageBox.Show(searchcode);
            DoUpdate();
            //this.textBox.Text = s;
        }
        private void InitializeFilter()
        {
            this.str基本型式名 = "";
            this.strシリーズ名 = "";
            this.dtm更新日開始 = DateTime.MinValue;
            this.dtm更新日終了 = DateTime.MinValue;
            this.str更新者名 = "";
            this.intIsUnit = 1;
            this.lngDiscontinued = 1;
            this.lngDeleted = 1;
        }
        private void Form_Load(object sender, EventArgs e)
        {
            //this.q商品管理TableAdapter.Fill(this.newDataSet.Q商品管理);
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
            dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200); // 薄い黄色
            dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            // 列の幅を設定 もとは恐らくtwipのためピクセルに直す

            //0列目はaccessでは行ヘッダのため、ずらす
            //dataGridView1.Columns[0].Width = 500 / twipperdot;
            dataGridView1.Columns[0].Width = 1250 / twipperdot; //1150
            dataGridView1.Columns[1].Width = 3500 / twipperdot;
            dataGridView1.Columns[2].Width = 1500 / twipperdot;
            dataGridView1.Columns[3].Width = 500 / twipperdot;
            dataGridView1.Columns[4].Width = 1350 / twipperdot;
            dataGridView1.Columns[5].Width = 1350 / twipperdot;
            dataGridView1.Columns[6].Width = 2200 / twipperdot;
            dataGridView1.Columns[7].Width = 1400 / twipperdot;//1300
            dataGridView1.Columns[8].Width = 500 / twipperdot;
            dataGridView1.Columns[9].Width = 500 / twipperdot;
            dataGridView1.Columns[10].Width = 500 / twipperdot;
            dataGridView1.Columns[11].Width = 500 / twipperdot;

            myapi.GetFullScreen(out xSize, out ySize);

            int x = 10, y = 10;

            this.Size = new Size(this.Width, ySize * myapi.GetTwipPerDot(intpixel) - 1200);
            //accessのmovesizeメソッドの引数の座標単位はtwipなので以下で

            this.Size = new Size(this.Width, ySize - 1200 / twipperdot);

            this.StartPosition = FormStartPosition.Manual; // 手動で位置を指定
            int screenWidth = Screen.PrimaryScreen.Bounds.Width; // プライマリスクリーンの幅
            x = (screenWidth - this.Width) / 2;
            this.Location = new Point(x, y);

            //実行中フォーム起動              
            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(CommonConstants.LoginUserCode, this);

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

                // 基本型式名
                if (!string.IsNullOrEmpty(str基本型式名))
                {
                    filter += "基本型式名 LIKE '%" + str基本型式名 + "%' AND ";
                }
                // シリーズ名
                if (!string.IsNullOrEmpty(strシリーズ名))
                {
                    filter += "シリーズ名 LIKE '%" + strシリーズ名 + "%' AND ";
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
                // チップマウントデータが構成されているかどうか
                switch (intComposedChipMount)
                {
                    case 1:
                        filter += "構成 IS NULL AND ";
                        break;
                    case 2:
                        filter += "構成 IS NOT NULL AND ";
                        break;
                }
                // ユニットかどうか
                switch (intIsUnit)
                {
                    case 1:
                        filter += "ユニ IS NULL AND ";
                        break;
                    case 2:
                        filter += "ユニ IS NOT NULL AND ";
                        break;
                }
                // 廃止
                switch (lngDiscontinued)
                {
                    case 1:
                        filter += "廃止 IS NULL AND ";
                        break;
                    case 2:
                        filter += "廃止 IS NOT NULL AND ";
                        break;
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

                string query = "SELECT 商品コード, 基本型式名, シリーズ名, 在庫管理, 在庫数量, 在庫下限数量, " +
                    "更新日時, 更新者名, 廃止, 削除, ユニ, 構成 " +
                    "FROM (SELECT M商品.商品コード, M商品.商品名 AS 基本型式名, Mシリーズ.シリーズ名, " +
                    "CASE WHEN M商品.シリーズコード IS NOT NULL THEN '○' ELSE NULL END AS 在庫管理, " +
                    "Mシリーズ.在庫数量, Mシリーズ.在庫下限数量, M商品.更新日時, M社員.氏名 AS 更新者名, " +
                    "CASE WHEN M商品.Discontinued = 0 THEN NULL ELSE '■' END AS 廃止, " +
                    "CASE WHEN M商品.無効日時 IS NOT NULL THEN '■' ELSE NULL END AS 削除, " +
                    "CASE WHEN M商品.IsUnit <> 0 THEN '■' ELSE NULL END AS ユニ, " +
                    "CASE WHEN ItemCode IS NOT NULL THEN '■' ELSE NULL END AS 構成 " +
                    "FROM M商品 LEFT OUTER JOIN ItemCode_ComposedMountChip ON M商品.商品コード = ItemCode_ComposedMountChip.ItemCode " +
                    "LEFT OUTER JOIN Mシリーズ ON M商品.シリーズコード = Mシリーズ.シリーズコード " +
                    "LEFT OUTER JOIN M社員 ON M商品.更新者コード = M社員.社員コード) AS T " +
                    "WHERE " + filter;

                Connect();
                DataGridUtils.SetDataGridView(cn, query, this.dataGridView1);

                //using (var command = new SqlCommand(query, cn))
                //{
                //    // クエリの結果を取得するためのデータアダプターを使用してデータを取得
                //    using (var adapter = new SqlDataAdapter(command))
                //    {
                //        var dataTable = new DataTable();
                //        adapter.Fill(dataTable);

                //        // DataTable を DataGridView にバインド
                //        dataGridView1.DataSource = null; // データソースをクリア
                //        dataGridView1.Rows.Clear();     // DataGridView内の行をクリア

                //        dataGridView1.Refresh();
                //        dataGridView1.Invalidate();
                //        dataGridView1.DataSource = dataTable;
                //    }
                //}


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

        //ダブルクリックで商品フォームを開く　商品コードを渡す
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.Button != MouseButtons.Left) return; // 左ボタンのダブルクリック以外は無視

            if (e.RowIndex >= 0) // ヘッダー行でない場合
            {
                string selectedData = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(); // 1列目のデータを取得

                F_商品 targetform = new F_商品();

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

        private void コマンド終了_Click(object sender, EventArgs e)
        {
            LocalSetting test = new LocalSetting();
            test.SavePlace(CommonConstants.LoginUserCode, this);
            this.Close();
        }

        private void コマンド保守_Click(object sender, EventArgs e)
        {
            if (ActiveControl == コマンド保守)
            {
                if (previousControl != null)
                {
                    previousControl.Focus();
                }
            }
            MessageBox.Show("このコマンドは使用できません。", "保守コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド抽出_Click(object sender, EventArgs e)
        {
            dataGridView1.Focus();
            F_商品管理_抽出 form = new F_商品管理_抽出();
            form.ShowDialog();
        }

        private void コマンド入出力_Click(object sender, EventArgs e)
        {
            dataGridView1.Focus(); // DataGridViewにフォーカスを設定

            int selectedRowIndex = dataGridView1.CurrentCell.RowIndex;

            if (selectedRowIndex >= 5)
            {
                dataGridView1.FirstDisplayedScrollingRowIndex = selectedRowIndex - 5;
            }
        }

        private void コマンド初期化_Click(object sender, EventArgs e)
        {
            if (ActiveControl == コマンド初期化)
            {
                if (previousControl != null)
                {
                    previousControl.Focus();
                }
            }
            MessageBox.Show("このコマンドは使用できません。", "初期化コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド全表示_Click(object sender, EventArgs e)
        {
            InitializeFilter();
            DoUpdate();
            Cleargrid(dataGridView1);
        }

        private void コマンド更新_Click(object sender, EventArgs e)
        {
            if (this.ActiveControl == this.コマンド更新)
            {
                if (previousControl != null)
                {
                    previousControl.Focus();
                }
                DoUpdate();
                Cleargrid(dataGridView1);
            }
        }

        private void コマンド検索_Click(object sender, EventArgs e)
        {            
            F_検索コード form = new F_検索コード(this, "ORD");
            form.ShowDialog();
        }

        private void コマンド商品_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // DataGridView1で選択された行が存在する場合
                string selectedData = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); // 1列目のデータを取得

                // 商品フォームを作成し、引数を設定して表示
                F_商品 targetform = new F_商品();
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
                        if (this.コマンド全表示.Enabled) コマンド全表示_Click(null, null);
                        break;
                    case Keys.F5:
                        if (this.コマンド商品.Enabled) コマンド商品_Click(null, null);
                        break;
                    case Keys.F9:
                        if (this.コマンド入出力.Enabled) コマンド入出力_Click(null, null);
                        break;
                    case Keys.F10:
                        if (this.コマンド保守.Enabled) コマンド保守_Click(null, null);
                        break;
                    case Keys.F11:
                        if (this.コマンド更新.Enabled) コマンド更新_Click(null, null);
                        break;
                    case Keys.F12:
                        if (this.コマンド終了.Enabled) コマンド終了_Click(null, null);
                        break;
                    case Keys.Return:
                        if (this.ActiveControl == this.dataGridView1)
                        {
                            if (dataGridView1.SelectedRows.Count > 0)
                            {
                                // DataGridView1で選択された行が存在する場合
                                string selectedData = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); // 1列目のデータを取得

                                // 商品フォームを作成し、引数を設定して表示
                                F_商品 targetform = new F_商品();
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
    }
}