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
    public partial class F_文書管理 : MidForm
    {
        public string str文書コード開始 = "";
        public string str文書コード終了 = "";
        public string str文書名 = "";
        public string str件名 = "";
        public string str発信者名 = "";
        public DateTime dtm確定日開始 = DateTime.MinValue;
        public DateTime dtm確定日終了 = DateTime.MinValue;
        public DateTime dtm期限日開始 = DateTime.MinValue;
        public DateTime dtm期限日終了 = DateTime.MinValue;
        public int lng確定指定 = 0;
        public int lng承認指定 = 0;
        public int lng完了承認指定 = 0;
        public int lng削除指定 = 1;

        public string strSearchCode = "";

        public string CurrentCode
        {
            get
            {
                return string.IsNullOrEmpty(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value?.ToString()) ? ""
                    : dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value?.ToString();
            }
        }

        public string CurrentEdition
        {
            get
            {
                return string.IsNullOrEmpty(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value?.ToString()) ? ""
                    : dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value?.ToString();
            }
        }

        int intWindowHeight = 0;
        int intWindowWidth = 0;

        private Control? previousControl;
        private SqlConnection? cn;
        public F_文書管理()
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
            str文書コード開始 = "";
            str文書コード終了 = "";
            dtm確定日開始 = DateTime.MinValue; // または 0 に相当する値を設定
            dtm確定日終了 = DateTime.MinValue; // または 0 に相当する値を設定
            str文書名 = "";
            str件名 = "";
            str発信者名 = "";
            lng確定指定 = 0;
            lng承認指定 = 0;
            lng完了承認指定 = 0;
            lng削除指定 = 1;
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            try
            {
                //dataGridView1.Height = dataGridView1.Height + (this.Height - intWindowHeight);
                //intWindowHeight = this.Height;  // 高さ保存

                //dataGridView1.Width = dataGridView1.Width + (this.Width - intWindowWidth);
                //intWindowWidth = this.Width;    // 幅保存

            }
            catch (Exception ex)
            {
                MessageBox.Show(this.Name + "_Form_Resize - " + ex.Message);
            }
        }

        private void F_支払管理_Load(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");
            //実行中フォーム起動

            //フォームサイズ読込
            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(CommonConstants.LoginUserCode, this);

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

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;

            //ダブルバッファ処理設定
            System.Type dgvtype = typeof(DataGridView);
            System.Reflection.PropertyInfo dgvPropertyInfo = dgvtype.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            dgvPropertyInfo.SetValue(dataGridView1, true, null);



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

            fn.WaitForm.Close();
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

                if (!string.IsNullOrEmpty(str文書名))
                {
                    filter += string.Format(" and 文書名 LIKE '%{0}%' ", str文書名);
                }


                if (!string.IsNullOrEmpty(str件名))
                {
                    filter += string.Format(" and 件名 LIKE '%{0}%' ", str件名);
                }

     

                if (!string.IsNullOrEmpty(str発信者名))
                {
                    filter += string.Format(" and 発信者名 LIKE '%{0}%' ", str発信者名);
                }



                if (dtm確定日開始 != DateTime.MinValue && dtm確定日終了 != DateTime.MinValue)
                {
                    filter += string.Format(" and 確定日 BETWEEN '{0}' AND '{1}' ", dtm確定日開始, dtm確定日終了);
                }





                switch (lng確定指定)
                {
                    case 1:
                        filter += " and 確定 IS NULL ";
                        break;
                    case 2:
                        filter += " and 確定 IS NOT NULL ";
                        break;
                }

                switch (lng完了承認指定)
                {
                    case 1:
                        filter += " and 完了承認 IS NULL ";
                        break;
                    case 2:
                        filter += " and 完了承認 IS NOT NULL ";
                        break;
                }

                switch (lng削除指定)
                {
                    case 1:
                        filter += " and 削除 IS NULL ";
                        break;
                    case 2:
                        filter += " and 削除 IS NOT NULL ";
                        break;
                }


                string query = "SELECT * FROM V文書管理 WHERE 1=1 " + filter + " ORDER BY 文書コード DESC ";

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
                dataGridView1.Columns[1].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200); // 薄い黄色

                dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                dataGridView1.ColumnHeadersHeight = 25;

                //0列目はaccessでは行ヘッダのため、ずらす

                dataGridView1.Columns[0].Width = 1300 / twipperdot; //1150
                dataGridView1.Columns[1].Width = 350 / twipperdot;
                dataGridView1.Columns[2].Width = 2200 / twipperdot;
                dataGridView1.Columns[3].Width = 4100 / twipperdot;
                dataGridView1.Columns[4].Width = 1100 / twipperdot;
                dataGridView1.Columns[5].Width = 1200 / twipperdot;
                dataGridView1.Columns[6].Width = 1200 / twipperdot;
                dataGridView1.Columns[7].Width = 300 / twipperdot;//1300
                dataGridView1.Columns[8].Width = 300 / twipperdot;
                dataGridView1.Columns[9].Width = 300 / twipperdot;
                dataGridView1.Columns[10].Width = 300 / twipperdot;
                dataGridView1.Columns[11].Width = 300 / twipperdot;
                dataGridView1.Columns[12].Width = 300 / twipperdot;
                dataGridView1.Columns[13].Width = 300 / twipperdot;
                dataGridView1.Columns[14].Width = 300 / twipperdot;
                dataGridView1.Columns[15].Width = 300 / twipperdot;
                dataGridView1.Columns[16].Width = 300 / twipperdot;
   

                return dataGridView1.RowCount;

            }
            catch (Exception ex)
            {
                MessageBox.Show("Filtering - " + ex.Message);
                return -1;
            }
        }


        private void コマンド終了_Click(object sender, EventArgs e)
        {
            LocalSetting ls = new LocalSetting();
            ls.SavePlace(CommonConstants.LoginUserCode, this);
            this.Close();
        }

        private void コマンド抽出_Click(object sender, EventArgs e)
        {
            F_文書管理_抽出 form = new F_文書管理_抽出();
            form.ShowDialog();
        }

        private void コマンド初期化_Click(object sender, EventArgs e)
        {
            InitializeFilter();
            DoUpdate();
            Cleargrid(dataGridView1);
        }

        private void コマンド更新_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。", "更新コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void コマンド検索_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。", "検索コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void F_文書管理_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.F1:
                        if (this.コマンド抽出.Enabled) コマンド抽出_Click(sender, e);
                        break;
                    case Keys.F2:
                        if (this.コマンド検索.Enabled) コマンド検索_Click(sender, e);
                        break;
                    case Keys.F3:
                        if (this.コマンド初期化.Enabled) コマンド初期化_Click(sender, e);
                        break;
                    case Keys.F4:
                        if (this.コマンド更新.Enabled) コマンド更新_Click(sender, e);
                        break;
                    case Keys.F5:
                        if (this.コマンド文書参照.Enabled) コマンド文書参照_Click(sender, e);
                        break;
                    case Keys.F6:
                        if (this.コマンドリンク.Enabled) コマンドリンク_Click(sender, e);
                        break;
                    case Keys.F9:
                        if(this.コマンド印刷.Enabled) コマンド印刷_Click(sender, e);
                        break;
                    case Keys.F10:
                        if (this.コマンド保守.Enabled) コマンド保守_Click(sender, e);
                        break;
                    case Keys.F11:
                        if (this.コマンド入出力.Enabled) コマンド入出力_Click(sender, e);
                        break;
                    case Keys.F12:
                        if (this.コマンド終了.Enabled) コマンド終了_Click(sender, e);
                        break;
                    case Keys.Return:
                        if (this.ActiveControl == this.dataGridView1)
                        {
                            if (dataGridView1.SelectedRows.Count > 0)
                            {
                                // DataGridView1で選択された行が存在する場合

                                F_文書 targetform = new F_文書();
                                targetform.args = CurrentCode + "," + CurrentEdition;
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

        private bool ascending = true;

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


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.Button != MouseButtons.Left) return; // 左ボタンのダブルクリック以外は無視

            if (e.RowIndex >= 0) // ヘッダー行でない場合
            {

                F_文書 targetform = new F_文書();
                targetform.args = CurrentCode + "," + CurrentEdition;
                targetform.ShowDialog();
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // ヘッダー行でない場合
            {
                dataGridView1.ClearSelection();
                dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }

        private void F_支払管理_FormClosing(object sender, FormClosingEventArgs e)
        {
            //フォームサイズ保存
            string LoginUserCode = CommonConstants.LoginUserCode;
            LocalSetting test = new LocalSetting();
            test.SavePlace(LoginUserCode, this);
        }

        private void コマンド文書参照_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // DataGridView1で選択された行が存在する場合

                F_文書 targetform = new F_文書();
                targetform.args = CurrentCode + "," + CurrentEdition;
                targetform.ShowDialog();
            }
            else
            {
                // ユーザーが行を選択していない場合のエラーハンドリング
                MessageBox.Show("行が選択されていません。");
            }
        }

        private void コマンドリンク_Click(object sender, EventArgs e)
        {
            try
            {

                Connect();

                // 本データのコードを取得
                string strDocumentCode = CurrentCode;

                // 本データがグループに登録済みかどうかを判断する
                int result = FunctionClass.DetectGroupMember(cn, strDocumentCode);

                switch (result)
                {
                    case 0:
                        // グループに登録されていない場合
                        F_グループ targetform = new F_グループ();

                        targetform.args = strDocumentCode;
                        targetform.ShowDialog();
                        break;
                    case 1:
                        // グループに登録されている場合
                        F_リンク targetform2 = new F_リンク();

                        targetform2.args = strDocumentCode;
                        targetform2.ShowDialog();
                        break;
                    case -1:
                        // エラーの場合
                        Console.WriteLine("エラーのため実行できません。");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("エラーが発生しました。" + Environment.NewLine + Environment.NewLine +
                                  ex.HResult + " : " + ex.Message);
            }
        }

        private void コマンド印刷_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。", "印刷コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド保守_Click(object sender, EventArgs e)
        {

        }

        private void コマンド入出力_Click(object sender, EventArgs e)
        {
            MessageBox.Show("このコマンドは使用できません。", "入出力コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}