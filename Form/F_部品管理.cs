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
    public partial class F_部品管理 : MidForm
    {
        public string str部品コード1 { get; set; }
        public string str部品コード2 { get; set; }
        public string str分類記号 { get; set; }
        public string str形状 { get; set; }
        public string str品名 { get; set; }
        public string str型番 { get; set; }
        public string strメーカー名 { get; set; }
        public string str仕入先名 { get; set; }
        public string strChemSherpaVersion { get; set; }
        public long lng単価指定 { get; set; }
        public long lngRohsStatusCode { get; set; }
        public long lngJampAis1 { get; set; }
        public long lngNonInclusionCertification1 { get; set; }
        public long lngRohsDocument1 { get; set; }
        public long lngJampAis2 { get; set; }
        public long lngNonInclusionCertification2 { get; set; }
        public long lngRohsDocument2 { get; set; }
        public long lng使用指定 { get; set; }
        public long lng廃止指定 { get; set; }
        public string str更新者名 { get; set; }
        public long lng削除指定 { get; set; }
        public string FilterString { get; set; }
        public string SortString { get; set; }
        public string strSearchCode { get; set; }

        int intWindowHeight = 0;
        int intWindowWidth = 0;

        private Control? previousControl;
        private SqlConnection? cn;
        public F_部品管理()
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
            str部品コード1 = strSearchCode;
            str部品コード2 = strSearchCode;
            if(DoUpdate() == -1)
            {
                MessageBox.Show("エラーが発生しました。");
            }

        }
        private void InitializeFilter()
        {
            str分類記号 = "";
            str形状 = "";
            str品名 = "";
            str型番 = "";
            strメーカー名 = "";
            str仕入先名 = "";
            lng単価指定 = 0;
            lng使用指定 = 0;
            lngRohsStatusCode = 0;
            lngJampAis1 = 0;
            // lngJampAis1 = null; // Uncomment if lngJampAis1 is a nullable type
            lngNonInclusionCertification1 = 0;
            lngRohsDocument1 = 0;
            lngJampAis2 = 0;
            lngNonInclusionCertification2 = 0;
            lngRohsDocument2 = 0;
            strChemSherpaVersion = "";
            lng削除指定 = 1;
        }
        private void Form_Load(object sender, EventArgs e)
        {

            System.Type dgvtype = typeof(DataGridView);
            System.Reflection.PropertyInfo dgvPropertyInfo = dgvtype.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            dgvPropertyInfo.SetValue(dataGridView1, true, null);

            //実行中フォーム起動
            string LoginUserCode = CommonConstants.LoginUserCode;
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

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;


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
                string filter = " AND ";

                // 部品コード指定
                if (!string.IsNullOrEmpty(str部品コード1) && !string.IsNullOrEmpty(str部品コード2))
                {
                    filter += string.Format("(部品コード BETWEEN '{0}' AND '{1}') AND ",
                                                                  str部品コード1, str部品コード2);
                }



                // 分類記号指定
                if (!string.IsNullOrEmpty(str分類記号))
                {
                    filter += string.Format("分類記号 = '{0}' AND ", str分類記号);
                }

                // 形状指定
                if (!string.IsNullOrEmpty(str形状))
                {
                    filter += string.Format("形状 = '{0}' AND ", str形状);
                }

                // 品名指定
                if (!string.IsNullOrEmpty(str品名))
                {
                    filter += string.Format("品名 LIKE '%{0}%' AND ", str品名);
                }

                // 型番指定
                if (!string.IsNullOrEmpty(str型番))
                {
                    filter += string.Format("型番 LIKE '%{0}%' AND ", str型番);
                }

                // メーカー名指定
                if (!string.IsNullOrEmpty(strメーカー名))
                {
                    filter += string.Format("メーカー名 LIKE '%{0}%' AND ", strメーカー名);
                }

                // 仕入先名指定
                if (!string.IsNullOrEmpty(str仕入先名))
                {
                    filter += string.Format("仕入先名 LIKE '%{0}%' AND ", str仕入先名);
                }



                // RoHS対応状態
                if (lngRohsStatusCode != 0)
                {
                    filter += string.Format("RohsStatusCode = {0} AND ", lngRohsStatusCode);
                }


                // chemSHERPAのバージョン
                if (!string.IsNullOrEmpty(strChemSherpaVersion))
                {
                    filter += string.Format("chemV LIKE '%{0}%' AND ", strChemSherpaVersion);
                }

                // 単価指定
                switch (lng単価指定)
                {
                    case 1:
                        filter += "単価 IS NOT NULL AND ";
                        break;
                    case 2:
                        filter += "単価 IS NULL AND ";
                        break;
                }


                // 使用
                switch (lng使用指定)
                {
                    case 1:
                        filter += "使用 IS NOT NULL AND ";
                        break;
                    case 2:
                        filter += "使用 IS NULL AND ";
                        break;
                }


                // 廃止
                switch (lng廃止指定)
                {
                    case 1:
                        filter += "廃止 IS NULL AND ";
                        break;
                    case 2:
                        filter += "廃止 IS NOT NULL AND ";
                        break;
                }


                // 更新者名
                if (!string.IsNullOrEmpty(str更新者名))
                {
                    filter += string.Format("更新者名 LIKE '%{0}%' AND ", str更新者名);
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

                string query = "SELECT * FROM V部品管理 WHERE 1=1 " + filter + " ORDER BY 部品コード DESC ";

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
                dataGridView1.Columns[0].Width = 1300 / twipperdot; //1150
                dataGridView1.Columns[1].Width = 300 / twipperdot;
                dataGridView1.Columns[2].Width = 440 / twipperdot;
                dataGridView1.Columns[3].Width = 300 / twipperdot;
                dataGridView1.Columns[4].Width = 2800 / twipperdot;
                dataGridView1.Columns[5].Width = 2800 / twipperdot;
                dataGridView1.Columns[6].Width = 2600 / twipperdot;
                dataGridView1.Columns[7].Width = 2600 / twipperdot;//1300
                dataGridView1.Columns[8].Width = 1000 / twipperdot;
                dataGridView1.Columns[9].Width = 600 / twipperdot;
                dataGridView1.Columns[10].Width = 1000 / twipperdot;
                dataGridView1.Columns[11].Width = 400 / twipperdot;
                dataGridView1.Columns[12].Width = 1230 / twipperdot;
                dataGridView1.Columns[13].Width = 1400 / twipperdot;
                dataGridView1.Columns[14].Width = 300 / twipperdot;

                //金額列　カンマと右付け
                dataGridView1.Columns["単価"].DefaultCellStyle.Format = "#,###,###,##0";
                dataGridView1.Columns["単価"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                dataGridView1.Columns["更新日時"].DefaultCellStyle.Format = "yyyy/MM/dd";

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

        //ダブルクリックで部品フォームを開く　部品コードを渡す
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.Button != MouseButtons.Left) return; // 左ボタンのダブルクリック以外は無視

            if (e.RowIndex >= 0) // ヘッダー行でない場合
            {
                string selectedData = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(); // 1列目のデータを取得

                F_部品 targetform = new F_部品();

                targetform.args = selectedData;
                targetform.MdiParent = this.MdiParent;
                targetform.FormClosed += (s, args) => { this.Enabled = true; };
                this.Enabled = false;

                targetform.Show();

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


        private void コマンド抽出_Click(object sender, EventArgs e)
        {
            dataGridView1.Focus();
            F_部品管理_抽出 targetform = new F_部品管理_抽出();
            targetform.MdiParent = this.MdiParent;
            targetform.FormClosed += (s, args) => { this.Enabled = true; };
            this.Enabled = false;

            targetform.Show();

        }



        private void コマンド初期化_Click(object sender, EventArgs e)
        {
            InitializeFilter();
            DoUpdate();
            Cleargrid(dataGridView1);
        }



 

        private void コマンド検索_Click(object sender, EventArgs e)
        {
            F_検索コード targetform = new F_検索コード(this, null);
            targetform.MdiParent = this.MdiParent;
            targetform.FormClosed += (s, args) => { this.Enabled = true; };
            this.Enabled = false;

            targetform.Show();

        }



        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {

                switch (e.KeyCode)
                {
                    case Keys.F1:
                        e.Handled = true;
                        if (this.コマンド抽出.Enabled) コマンド抽出_Click(null, null);
                        break;
                    case Keys.F2:
                        e.Handled = true;
                        if (this.コマンド検索.Enabled) コマンド検索_Click(null, null);
                        break;
                    case Keys.F3:
                        e.Handled = true;
                        if (this.コマンド初期化.Enabled) コマンド初期化_Click(null, null);
                        break;
                    case Keys.F4:
                        e.Handled = true;
                        if (this.コマンド更新.Enabled) コマンド更新_Click(null, null);
                        break;
                    case Keys.F5:
                        e.Handled = true;
                        if (this.コマンド部品.Enabled) コマンド部品_Click(null, null);
                        break;
                    case Keys.F6:
                        e.Handled = true;
                        if (this.コマンド入出履歴.Enabled) コマンド入出履歴_Click(null, null);
                        break;
                    case Keys.F9:
                        e.Handled = true;
                        if (this.コマンド印刷プレビュー.Enabled) コマンド印刷プレビュー_Click(null, null);
                        break;
                    case Keys.F10:
                        e.Handled = true;
                        if (this.コマンド出力.Enabled) コマンド出力_Click(null, null);
                        break;
                    case Keys.F11:
                        e.Handled = true;
                        if (this.コマンド保守.Enabled) コマンド保守_Click(null, null);
                        break;
                    case Keys.F12:
                        e.Handled = true;
                        if (this.コマンド終了.Enabled) コマンド終了_Click(null, null);
                        break;
                    case Keys.Return:
                        e.Handled = true;
                        if (this.ActiveControl == this.dataGridView1)
                        {
                            if (dataGridView1.SelectedRows.Count > 0)
                            {
                                // DataGridView1で選択された行が存在する場合
                                string selectedData = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); // 1列目のデータを取得

                                // 部品フォームを作成し、引数を設定して表示
                                F_部品 targetform = new F_部品();
                                targetform.args = selectedData;
                                targetform.MdiParent = this.MdiParent;
                                targetform.FormClosed += (s, args) => { this.Enabled = true; };
                                this.Enabled = false;

                                targetform.Show();

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

        private void F_部品管理_FormClosing(object sender, FormClosingEventArgs e)
        {
            string LoginUserCode = CommonConstants.LoginUserCode;
            LocalSetting test = new LocalSetting();
            test.SavePlace(LoginUserCode, this);
        }

        private void コマンド更新_Click(object sender, EventArgs e)
        {
            DoUpdate();
            Cleargrid(dataGridView1);
        }

        private void コマンド部品_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // DataGridView1で選択された行が存在する場合
                string selectedData = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); // 1列目のデータを取得

                // 部品フォームを作成し、引数を設定して表示
                F_部品 targetform = new F_部品();
                targetform.args = selectedData;
                targetform.MdiParent = this.MdiParent;
                targetform.FormClosed += (s, args) => { this.Enabled = true; };
                this.Enabled = false;

                targetform.Show();

            }
            else
            {
                // ユーザーが行を選択していない場合のエラーハンドリング
                MessageBox.Show("行が選択されていません。");
            }
        }

        private void コマンド入出履歴_Click(object sender, EventArgs e)
        {
            F_入出庫履歴 targetform = new F_入出庫履歴();

            targetform.MdiParent = this.MdiParent;
            targetform.FormClosed += (s, args) => { this.Enabled = true; };
            this.Enabled = false;

            targetform.Show();

        }

        private void コマンド印刷プレビュー_Click(object sender, EventArgs e)
        {


            IReport paoRep = ReportCreator.GetPreview();

            string appPath = System.IO.Path.GetDirectoryName(Application.ExecutablePath) + "\\";
            paoRep.LoadDefFile($"{appPath}Reports/部品管理.prepd");
            //paoRep.LoadDefFile("../../../Reports/部品管理.prepd");

            //最大行数
            int maxRow = 30;
            //現在の行
            int CurRow = 0;
            //行数
            int RowCount = maxRow;
            if(dataGridView1.RowCount > 0)
            {
                RowCount = dataGridView1.RowCount;
            }

            int page = 1;
            double maxPage = Math.Ceiling((double)RowCount / maxRow);

            DateTime now = DateTime.Now;

            int lenB;

            //描画すべき行がある限りページを増やす
            while (RowCount > 0)
            {
                RowCount -= maxRow;

                paoRep.PageStart();

                //フッダー
                paoRep.Write("出力日時", now.ToString("yyyy/MM/dd HH:mm:ss"));
                paoRep.Write("ページ数", (page + "/" + maxPage + " ページ").ToString());

                //明細
                for (var i = 0; i < maxRow; i++)
                {
                    if (CurRow >= dataGridView1.RowCount) break;

                    DataGridViewRow targetRow = dataGridView1.Rows[CurRow];

                    paoRep.Write("行", (CurRow + 1).ToString(), i + 1);
                    paoRep.Write("部品コード", targetRow.Cells["部品コード"].Value.ToString() != "" ? targetRow.Cells["部品コード"].Value.ToString()  : " ", i + 1);
                    paoRep.Write("分類", targetRow.Cells["分類記号"].Value.ToString() != "" ? targetRow.Cells["分類記号"].Value.ToString() : " ", i + 1);
                    paoRep.Write("形状", targetRow.Cells["形状"].Value.ToString() != "" ? targetRow.Cells["形状"].Value.ToString() : " ", i + 1);
                    paoRep.Write("品名", targetRow.Cells["品名"].Value.ToString() != "" ? targetRow.Cells["品名"].Value.ToString() : " ", i + 1);
                    paoRep.Write("型番", targetRow.Cells["型番"].Value.ToString() != "" ? targetRow.Cells["型番"].Value.ToString() : " ", i + 1);
                    paoRep.Write("メーカー名", targetRow.Cells["メーカー名"].Value.ToString() != "" ? targetRow.Cells["メーカー名"].Value.ToString() : " ", i + 1);
                    paoRep.Write("仕入先名", targetRow.Cells["仕入先名"].Value.ToString() != "" ? targetRow.Cells["仕入先名"].Value.ToString() : " " , i + 1);
                    paoRep.Write("単価", string.Format("{0:#,0.00}", targetRow.Cells["単価"].Value) != "" ? string.Format("{0:#,0.00}", targetRow.Cells["単価"].Value) : " ", i + 1);
                    paoRep.Write("Ro", targetRow.Cells["RoHS"].Value.ToString() != "" ? targetRow.Cells["RoHS"].Value.ToString() : " ", i + 1);
                    paoRep.Write("廃", targetRow.Cells["廃止"].Value.ToString() == "■" ? "■" : " ", i + 1);
                    paoRep.Write("削", targetRow.Cells["削除"].Value.ToString() == "■" ? "■" : " ", i + 1);

                    paoRep.z_Objects.SetObject("品名", i + 1);
                    lenB = Encoding.Default.GetBytes(targetRow.Cells["品名"].Value.ToString()).Length;
                    if (40 < lenB)
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 6;
                    }
                    else if (30 < lenB)
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 8;
                    }
                    else
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 10;
                    }


                    paoRep.z_Objects.SetObject("型番", i + 1);
                    lenB = Encoding.Default.GetBytes(targetRow.Cells["型番"].Value.ToString()).Length;
                    if (40 < lenB)
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 6;
                    }
                    else if (30 < lenB)
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 8;
                    }
                    else
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 10;
                    }


                    paoRep.z_Objects.SetObject("メーカー名", i + 1);
                    lenB = Encoding.Default.GetBytes(targetRow.Cells["メーカー名"].Value.ToString()).Length;
                    if(20 < lenB)
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 6;
                    }
                    else if(16 < lenB)
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 8;
                    }
                    else
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 10;
                    }

                    paoRep.z_Objects.SetObject("仕入先名", i + 1);
                    lenB = Encoding.Default.GetBytes(targetRow.Cells["仕入先名"].Value.ToString()).Length;
                    if (24 < lenB)
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 6;
                    }
                    else if (20 < lenB)
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 8;
                    }
                    else
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 10;
                    }

                    CurRow++;


                }

                page++;

                paoRep.PageEnd();

            }



            paoRep.Output();



        }

        private void コマンド出力_Click(object sender, EventArgs e)
        {
            F_出力 targetform = new F_出力();
            targetform.DataGridView = dataGridView1;
            targetform.MdiParent = this.MdiParent;
            targetform.FormClosed += (s, args) => { this.Enabled = true; };
            this.Enabled = false;

            targetform.Show();

        }

        private void コマンド保守_Click(object sender, EventArgs e)
        {
            dataGridView1.Focus(); // DataGridViewにフォーカスを設定

            MessageBox.Show("現在開発中です。", "保守コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        //private bool ascending = true;

        ////顧客名ラベルをクリックで顧客名カナでソートする
        //private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        //{
        //    if (e.ColumnIndex == 1)
        //    {
        //        if (ascending)
        //        {
        //            dataGridView1.Sort(dataGridView1.Columns[2], System.ComponentModel.ListSortDirection.Ascending);
        //        }
        //        else
        //        {
        //            dataGridView1.Sort(dataGridView1.Columns[2], System.ComponentModel.ListSortDirection.Descending);
        //        }
        //        ascending = !ascending;
        //    }
        //}


    }
}