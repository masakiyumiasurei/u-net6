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

namespace u_net
{
    public partial class F_部品集合管理 : MidForm
    {
        public bool UpdateOn = false;                       // アクティブ時に更新させるかどうか
        public string str部品集合コード開始 = string.Empty;
        public string str部品集合コード終了 = string.Empty;
        public string str分類名 = string.Empty;
        public string str集合名 = string.Empty;
        public DateTime dtm更新日開始 = DateTime.MinValue;
        public DateTime dtm更新日終了 = DateTime.MinValue;
        public string str更新者名 = string.Empty;
        public int lng確定指定 = 0;
        public int lng承認指定 = 0;
        public int lng削除指定 = 0;
        public string filterString = string.Empty;           // 抽出条件文字列（WHERE句）
        public string sortString = string.Empty;             // ソート文字列

        private int LngDataCount = 0;
        private int IntSelectionMode = 0;                    // グリッドの選択モード
        private int IntWindowHeight = 0;                     // 現在保持している高さ
        private int IntWindowWidth = 0;                      // 現在保持している幅
        private int IntButton = 0;


        private Control? previousControl;
        private SqlConnection? cn;
        public F_部品集合管理()
        {
            InitializeComponent();
        }

        public string CurrentCode
        {
            get
            {
                return string.IsNullOrEmpty(dataGridView1.SelectedRows[0].Cells[0].Value.ToString()) ? ""
                    : dataGridView1.SelectedRows[0].Cells[0].Value.ToString();
            }
        }
        public int CurrentEdition
        {
            get
            {
                int result;
                return int.TryParse(dataGridView1.SelectedRows[0].Cells[1].Value.ToString(), out result) ? result : 0  ;   //int.TryParse(部品集合版数.Text, out result) ? result : 0;
            }
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
            if (DoUpdate() == -1)
            {
                MessageBox.Show("エラーが発生しました。");
            }
        }

        private void InitializeFilter()
        {
            str部品集合コード開始 = string.Empty;
            str部品集合コード終了 = string.Empty;
            str集合名 = string.Empty;
            dtm更新日開始 = DateTime.MinValue;
            dtm更新日終了 = DateTime.MinValue;
            str更新者名 = string.Empty;
            lng確定指定 = 0;
            lng承認指定 = 0;
            lng削除指定 = 1;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");
            //実行中フォーム起動

            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(CommonConstants.LoginUserCode, this);

            MyApi myapi = new MyApi();
            int xSize, ySize, intpixel, twipperdot;

            //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
            intpixel = myapi.GetLogPixel();
            twipperdot = myapi.GetTwipPerDot(intpixel);

            IntWindowHeight = this.Height;
            IntWindowWidth = this.Width;

            // DataGridViewの設定
            //dataGridView1.AllowUserToResizeColumns = true;
            //dataGridView1.Font = new Font("MS ゴシック", 10);
            //dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 210, 255);
            //dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            //dataGridView1.GridColor = Color.FromArgb(230, 230, 230);
            //dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("MS ゴシック", 9);
            //dataGridView1.DefaultCellStyle.Font = new Font("MS ゴシック", 10);
            //dataGridView1.DefaultCellStyle.ForeColor = Color.Black;

            //dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200); // 薄い黄色
            //dataGridView1.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;


            //0列目はaccessでは行ヘッダのため、ずらす
            //dataGridView1.Columns[0].Width = 500 / twipperdot;
            //dataGridView1.Columns[0].Width = 1100 / twipperdot; //1150
            //dataGridView1.Columns[1].Width = 300 / twipperdot;
            //dataGridView1.Columns[2].Width = 5000;
            //dataGridView1.Columns[3].Width = 0 / twipperdot;
            //dataGridView1.Columns[4].Width = 2000 / twipperdot;
            //dataGridView1.Columns[5].Width = 1500 / twipperdot;
            //dataGridView1.Columns[6].Width = 1500 / twipperdot;
            //dataGridView1.Columns[7].Width = 2200 / twipperdot;//1300
            //dataGridView1.Columns[8].Width = 1500 / twipperdot;
            //dataGridView1.Columns[9].Width = 300 / twipperdot;

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
            fn.WaitForm.Close();
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            try
            {
                if (this.Height > 800)
                {
                    dataGridView1.Height = dataGridView1.Height + (this.Height - IntWindowHeight);
                    IntWindowHeight = this.Height;  // 高さ保存

                    dataGridView1.Width = dataGridView1.Width + (this.Width - IntWindowWidth);
                    IntWindowWidth = this.Width;    // 幅保存
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
                    this.表示件数.Text = null;
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

                // 仕入先コード指定
                if (!string.IsNullOrEmpty(str部品集合コード開始))
                {
                    filter += string.Format(" and (部品集合コード BETWEEN '{0}' AND '{1}') ",
                                                                  str部品集合コード開始, str部品集合コード終了);
                }

                // 仕入先名指定
                if (!string.IsNullOrEmpty(str分類名))
                {
                    filter += string.Format(" and GP= '{0}'  ", str分類名);
                }

                // 担当者名指定
                if (!string.IsNullOrEmpty(str集合名))
                {
                    filter += string.Format(" and 集合名 LIKE '%{0}%' ", str集合名);
                }

                // 担当者メールアドレス指定
                if (dtm更新日開始 != DateTime.MinValue && dtm更新日終了 != DateTime.MinValue)
                {
                    filter += string.Format(" and '{0}' <= 更新日時 AND 更新日時 <= '{1}' ", dtm更新日開始, dtm更新日終了);
                }
                // 確定指定
                switch (lng確定指定)
                {
                    case 1:
                        filter += " and 確定 IS NULL  ";
                        break;
                    case 2:
                        filter += " and 確定 IS NOT NULL  ";
                        break;
                }
                // 承認指定
                switch (lng承認指定)
                {
                    case 1:
                        filter += " and 承認 IS NULL  ";
                        break;
                    case 2:
                        filter += " and 承認 IS NOT NULL  ";
                        break;
                }
                // 削除
                switch (lng削除指定)
                {
                    case 1:
                        filter += " and 削除 IS NULL  ";
                        break;
                    case 2:
                        filter += " and 削除 IS NOT NULL  ";
                        break;
                }
                //if (!string.IsNullOrEmpty(filter))
                //{
                //    filter = filter.Substring(0, filter.Length - 5); // 最後の " AND " を削除
                //}

                string query = "SELECT * FROM V部品集合管理 WHERE 1=1 " + filter + " ORDER BY 部品集合コード DESC ";

                Connect();
                DataGridUtils.SetDataGridView(cn, query, this.dataGridView1);

                MyApi myapi = new MyApi();
                int xSize, ySize, intpixel, twipperdot;

                ////1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
                intpixel = myapi.GetLogPixel();
                twipperdot = myapi.GetTwipPerDot(intpixel);

                IntWindowHeight = this.Height;
                IntWindowWidth = this.Width;

                //// DataGridViewの設定
                dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200); // 薄い黄色

                dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                dataGridView1.ColumnHeadersHeight = 25;

                //0列目はaccessでは行ヘッダのため、ずらす

                dataGridView1.Columns[0].Width = 1200 / twipperdot; //1150
                dataGridView1.Columns[1].Width = 400 / twipperdot;
                dataGridView1.Columns[2].Width = 500 / twipperdot;
                dataGridView1.Columns[3].Width = 5000 / twipperdot;
                dataGridView1.Columns[4].Width = 2300 / twipperdot;
                dataGridView1.Columns[5].Width = 2500 / twipperdot;
                dataGridView1.Columns[6].Width = 400 / twipperdot;
                dataGridView1.Columns[7].Width = 400 / twipperdot;//1300
                dataGridView1.Columns[8].Width = 400 / twipperdot;
                // dataGridView1.Columns[9].Width = 310 / twipperdot;

                return dataGridView1.RowCount;
                //return 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Filtering - " + ex.Message);
                return -1;
            }
        }

        private void DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //列ヘッダーかどうか調べる
            if (e.ColumnIndex < 0 && e.RowIndex >= 0)
            {
                //dataGridView1.SuspendLayout();
                //セルを描画する
                e.Paint(e.ClipBounds, DataGridViewPaintParts.All);

                //行番号を描画する範囲を決定する
                //e.AdvancedBorderStyleやe.CellStyle.Paddingは無視
                Rectangle indexRect = e.CellBounds;
                indexRect.Inflate(-2, -2);

                //行番号を描画する
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.CellStyle.Font, indexRect,
                    e.CellStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);

                //描画が完了したことを知らせる
                e.Handled = true;
                //dataGridView1.ResumeLayout();

            }
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.Button != MouseButtons.Left) return; // 左ボタンのダブルクリック以外は無視

            if (e.RowIndex >= 0) // ヘッダー行でない場合
            {
                string selectedData = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(); // 1列目のデータを取得

                F_部品集合 targetform = new F_部品集合();

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

        private void コマンド抽出_Click(object sender, EventArgs e)
        {
            dataGridView1.Focus();
            F_製品管理_抽出 form = new F_製品管理_抽出();
            form.ShowDialog();
        }

        private void コマンド入出力_Click(object sender, EventArgs e)
        {
            //dataGridView1.Focus(); // DataGridViewにフォーカスを設定

            MessageBox.Show("現在開発中です。", "入出力コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド初期化_Click(object sender, EventArgs e)
        {
            InitializeFilter();
            DoUpdate();
            Cleargrid(dataGridView1);
        }
                

        private void コマンド更新_Click(object sender, EventArgs e)
        {

            DoUpdate();
            Cleargrid(dataGridView1);

        }

        private void コマンド検索_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。\n コードで検索するときに使用します。",
                "検索コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                switch (e.KeyCode)
                {
                    case Keys.F1:
                        //if (this.コマンド抽出.Enabled) コマンド抽出_Click(null, null);
                        break;
                    case Keys.F2:
                        //if (this.コマンド検索.Enabled) コマンド検索_Click(null, null);
                        break;
                    case Keys.F3:
                        //if (this.コマンド初期化.Enabled) コマンド初期化_Click(null, null);
                        break;
                    case Keys.F4:
                        //if (this.コマンド更新.Enabled) コマンド更新_Click(null, null);
                        break;
                    case Keys.F5:
                        //if (this.コマンド仕入先.Enabled) コマンド仕入先_Click(null, null);
                        break;
                    case Keys.F6:
                    //if (this.コマンドメール.Enabled) コマンドメール_Click(null, null);
                    //break;
                    case Keys.F9:
                        //if (this.コマンド印刷.Enabled) コマンド印刷_Click(null, null);
                        break;
                    case Keys.F10:
                        //if (this.コマンド入出力.Enabled) コマンド入出力_Click(null, null);
                        break;
                    case Keys.F11:
                    //if (this.コマンド入出力.Enabled) コマンド入出力_Click(null, null);
                    //break;
                    case Keys.F12:
                        //if (this.コマンド終了.Enabled) コマンド終了_Click(null, null);
                        break;
                    case Keys.Return:
                        //if (this.ActiveControl == this.dataGridView1)
                        //{
                        //    if (dataGridView1.SelectedRows.Count > 0)
                        //    {
                        //        // DataGridView1で選択された行が存在する場合
                        //        string selectedData = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); // 1列目のデータを取得

                        //        // 仕入先フォームを作成し、引数を設定して表示
                        //        F_仕入先 targetform = new F_仕入先();
                        //        targetform.args = selectedData;
                        //        targetform.ShowDialog();
                        //    }
                        //    else
                        //    {
                        //        // ユーザーが行を選択していない場合のエラーハンドリング
                        //        MessageBox.Show("行が選択されていません。");
                        //    }
                        //}
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("KeyDown - " + ex.Message);
            }
        }

        private void F_仕入先管理_FormClosing(object sender, FormClosingEventArgs e)
        {
            LocalSetting ls = new LocalSetting();
            ls.SavePlace(CommonConstants.LoginUserCode, this);
        }

        private bool ascending = true;
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (dataGridView1.Columns[e.ColumnIndex].Name == "仕入先名")
            //{
            //    if (ascending)
            //    {
            //        dataGridView1.Sort(dataGridView1.Columns["仕入先名フリガナ"], System.ComponentModel.ListSortDirection.Ascending);
            //    }
            //    else
            //    {
            //        dataGridView1.Sort(dataGridView1.Columns["仕入先名フリガナ"], System.ComponentModel.ListSortDirection.Descending);
            //    }
            //    ascending = !ascending;
            //}
        }

              

        private void コマンド保守_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。\n コードで検索するときに使用します。",
                "保守コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド印刷_Click(object sender, EventArgs e)
        {
            IReport paoRep = ReportCreator.GetPreview();

            paoRep.LoadDefFile("../../../Reports/部品集合管理.prepd");

        }

        private void コマンド印刷明細_Click(object sender, EventArgs e)
        {
            IReport paoRep = ReportCreator.GetPreview();

            paoRep.LoadDefFile("../../../Reports/部品集合明細一覧.prepd");
        }

        private void コマンド部品購買設定_Click(object sender, EventArgs e)
        {
            F_部品購買設定 fm =new F_部品購買設定();
            fm.args = $"{CurrentCode}, {CurrentEdition}";
            fm.ShowDialog();
        }

        private void コマンド参照_Click(object sender, EventArgs e)
        {
            F_部品集合 fm = new F_部品集合();
            fm.args = $"{CurrentCode}, {CurrentEdition}";
            fm.ShowDialog();
        }
    }
}