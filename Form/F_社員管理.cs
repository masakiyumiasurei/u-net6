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
    public partial class F_社員管理 : MidForm
    {
        public string str検索コード = CommonConstants.CH_EMPLOYEE;

        public string str社員コード開始 = "";
        public string str社員コード終了 = "";
        public long lng社員区分;
        public string str氏名 = "";
        public string str電話番号 = "";
        public long lng退社指定;
        public string str担当者名 = "";
        public int lng削除指定 = 0;
        //public DateTime dtm更新日開始 = DateTime.MinValue;
        //public DateTime dtm更新日終了 = DateTime.MinValue;
        //public string str更新者名 = "";
        public string strSearchCode = "";
        public bool empEXT;

        //public int intComposedChipMount = 0;
        //public int intIsUnit = 0;
        //public int lngDiscontinued = 0;


        int intWindowHeight = 0;
        int intWindowWidth = 0;

        private Control? previousControl;
        private SqlConnection? cn;

        public F_社員管理()
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
            str社員コード開始 = strSearchCode;
            str社員コード終了 = strSearchCode;
            if (DoUpdate() == -1)
            {
                MessageBox.Show("エラーが発生しました。");
            }
        }

        private void InitializeFilter()
        {
            this.str社員コード開始 = "";
            this.str社員コード終了 = "";
            this.lng社員区分 = 0;
            this.str氏名 = "";
            this.str電話番号 = "";
            this.lng退社指定 = 1;
        }

        private void Form_Load(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");

            foreach (Control control in Controls)
            {
                control.PreviewKeyDown += OriginalClass.ValidateCheck;
            }

            //実行中フォーム起動

            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(CommonConstants.LoginUserCode, this);

            MyApi myapi = new MyApi();
            int xSize, ySize, intpixel, twipperdot;

            //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
            //intpixel = myapi.GetLogPixel();
            //twipperdot = myapi.GetTwipPerDot(intpixel);

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
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            //myapi.GetFullScreen(out xSize, out ySize);

            //int x = 10, y = 10;

            //this.Size = new Size(this.Width, ySize * myapi.GetTwipPerDot(intpixel) - 1200);
            ////accessのmovesizeメソッドの引数の座標単位はtwipなので以下で

            //this.Size = new Size(this.Width, ySize - 1200 / twipperdot);

            //this.StartPosition = FormStartPosition.Manual; // 手動で位置を指定
            //int screenWidth = Screen.PrimaryScreen.Bounds.Width; // プライマリスクリーンの幅
            //x = (screenWidth - this.Width) / 2;
            //this.Location = new Point(x, y);

            InitializeFilter();
            DoUpdate();
            Cleargrid(dataGridView1);
            fn.WaitForm.Close();
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            try
            {
                dataGridView1.Height = dataGridView1.Height + (this.Height - intWindowHeight);
                intWindowHeight = this.Height;  // 高さ保存

                dataGridView1.Width = dataGridView1.Width + (this.Width - intWindowWidth);
                intWindowWidth = this.Width;    // 幅保存

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

                // 社員区分
                switch (lng社員区分)
                {
                    case 1:
                        filter += "パート IS NULL AND ";
                        break;
                    case 2:
                        filter += "パート IS NOT NULL AND ";
                        break;
                }

                // 氏名指定
                if (!string.IsNullOrEmpty(str氏名))
                {
                    filter += string.Format("氏名 LIKE '%{0}%' AND ", str氏名);
                }

                // 社員コード開始指定
                if (!string.IsNullOrEmpty(str社員コード開始))
                {
                    filter += string.Format("社員コード開始 >= '{0}' AND ", str社員コード開始);
                }

                // 社員コード終了指定
                if (!string.IsNullOrEmpty(str社員コード終了))
                {
                    filter += string.Format("社員コード終了 <= '{0}' AND ", str社員コード終了);
                }

                // 電話番号指定
                if (!string.IsNullOrEmpty(str電話番号))
                {
                    filter += string.Format("電話番号 LIKE '%{0}%' AND ", str電話番号);
                }

                // 社員区分
                switch (lng退社指定)
                {
                    case 1:
                        filter += "退社 IS NULL AND ";
                        break;
                    case 2:
                        filter += "退社 IS NOT NULL AND ";
                        break;
                }

                if (!string.IsNullOrEmpty(filter))
                {
                    filter = filter.Substring(0, filter.Length - 5); // 最後の " AND " を削除
                }

                string query = "SELECT * FROM V社員管理 WHERE 1=1 AND " + filter + " ORDER BY ふりがな ";

                Connect();
                DataGridUtils.SetDataGridView(cn, query, this.dataGridView1);


                MyApi myapi = new MyApi();
                int xSize, ySize, intpixel, twipperdot;

                ////1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
                intpixel = myapi.GetLogPixel();
                twipperdot = myapi.GetTwipPerDot(intpixel);

                intWindowHeight = this.Height;
                intWindowWidth = this.Width;

                //// DataGridViewの設定
                dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200); // 薄い黄色                                                                                                  

                // 列の幅を設定 もとは恐らくtwipのためピクセルに直す

                dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                dataGridView1.ColumnHeadersHeight = 25;

                //0列目はaccessでは行ヘッダのため、ずらす
                dataGridView1.Columns[0].Width = 1000 / twipperdot; //1150
                dataGridView1.Columns[1].Width = 400 / twipperdot;
                dataGridView1.Columns[2].Width = 2000 / twipperdot;
                dataGridView1.Columns[3].Width = 2500 / twipperdot;
                dataGridView1.Columns[4].Width = 1900 / twipperdot;
                dataGridView1.Columns[5].Width = 1900 / twipperdot;
                dataGridView1.Columns[6].Width = 3000 / twipperdot;
                dataGridView1.Columns[7].Width = 400 / twipperdot;//1300

                return dataGridView1.RowCount;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Filtering - " + ex.Message);
                return -1;
            }
        }

        private void DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //行番号を表示する
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
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.CellStyle.Font, indexRect,
                    e.CellStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);

                //描画が完了したことを知らせる
                e.Handled = true;
                dataGridView1.ResumeLayout();
            }
        }

        //ダブルクリックで仕入先フォームを開く　仕入先コードを渡す
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.Button != MouseButtons.Left) return; // 左ボタンのダブルクリック以外は無視

            if (e.RowIndex >= 0) // ヘッダー行でない場合
            {
                string selectedData = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(); // 1列目のデータを取得

                F_社員 targetform = new F_社員();

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
            this.Close();
        }

        private void コマンド印刷_Click(object sender, EventArgs e)
        {
            dataGridView1.Focus(); // DataGridViewにフォーカスを設定

            MessageBox.Show("現在開発中です。", "印刷コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド抽出_Click(object sender, EventArgs e)
        {
            dataGridView1.Focus();
            F_社員管理_抽出 form = new F_社員管理_抽出();
            form.ShowDialog();
        }

        private void コマンド入出力_Click(object sender, EventArgs e)
        {
            dataGridView1.Focus(); // DataGridViewにフォーカスを設定

            MessageBox.Show("現在開発中です。", "入出力コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド初期化_Click(object sender, EventArgs e)
        {
            try
            {
                FunctionClass fn = new FunctionClass();
                fn.DoWait("しばらくお待ちください...");

                InitializeFilter();
                DoUpdate();
                Cleargrid(dataGridView1);
                fn.WaitForm.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーが発生しました。", "初期化コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void コマンド更新_Click(object sender, EventArgs e)
        {
            try
            {
                FunctionClass fn = new FunctionClass();
                fn.DoWait("しばらくお待ちください...");

                InitializeFilter();
                DoUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーが発生しました。", "更新コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void コマンド検索_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。\n コードで検索するときに使用します。", "検索コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド社員_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // DataGridView1で選択された行が存在する場合
                string selectedData = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); // 1列目のデータを取得

                // 仕入先フォームを作成し、引数を設定して表示
                F_社員 targetform = new F_社員();
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
                        if (this.コマンド更新.Enabled) コマンド更新_Click(null, null);
                        break;
                    case Keys.F5:
                        if (this.コマンド社員.Enabled) コマンド社員_Click(null, null);
                        break;
                    case Keys.F6:
                        if (this.コマンドメール.Enabled) コマンドメール_Click(null, null);
                        break;
                    case Keys.F9:
                        if (this.コマンド印刷.Enabled) コマンド印刷_Click(null, null);
                        break;
                    case Keys.F10:
                        if (this.コマンド保守.Enabled) コマンド保守_Click(null, null);
                        break;
                    case Keys.F11:
                        if (this.コマンド入出力.Enabled) コマンド入出力_Click(null, null);
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

                                // 仕入先フォームを作成し、引数を設定して表示
                                F_仕入先 targetform = new F_仕入先();
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

        private void F_社員管理_FormClosing(object sender, FormClosingEventArgs e)
        {
            LocalSetting ls = new LocalSetting();
            ls.SavePlace(CommonConstants.LoginUserCode, this);
        }

        private void コマンドメール_Click(object sender, EventArgs e)
        {
            string toEmail = dataGridView1.SelectedRows[0].Cells["電子メールアドレス"].Value?.ToString();

            if (string.IsNullOrEmpty(toEmail))
            {
                MessageBox.Show("メールアドレスが設定されていません。", "メールコマンド",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return;
            }

            try
            {
                string mailtoLink = "mailto:" + toEmail;
                System.Diagnostics.Process.Start(new ProcessStartInfo(mailtoLink) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("メールを起動できませんでした。\nエラー: " + ex.Message, "メールコマンド", MessageBoxButtons.OK);
            }
        }

        private void コマンド保守_Click(object sender, EventArgs e)
        {
            dataGridView1.Focus(); // DataGridViewにフォーカスを設定

            MessageBox.Show("現在開発中です。", "印刷コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}