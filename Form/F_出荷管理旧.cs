using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GrapeCity.Win.BarCode.ValueType;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using u_net.Public;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static u_net.Public.FunctionClass;
using static u_net.CommonConstants;
using static u_net.Public.OriginalClass;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using System.Reflection.Metadata.Ecma335;

namespace u_net
{
    public partial class F_出荷管理旧 : MidForm
    {
        public const string strSecondOrder = "受注コード";   // 並べ替え第２項目

        public string str出荷コード開始 = "";
        public string str出荷コード終了 = "";
        public string str受注コード開始 = "";
        public string str受注コード終了 = "";
        public DateTime dte出荷予定日1 = DateTime.MinValue;
        public DateTime dte出荷予定日2 = DateTime.MinValue;
        public string str型番 = "";
        public string str型番詳細 = "";
        public int lngシリアル番号指定;
        public int lngシリアル番号開始;
        public int lngシリアル番号終了;
        public string str発送先名 = "";
        public string str顧客コード = "";
        public string str顧客名 = "";
        public int lng作業終了指定;
        public DateTime dte作業終了日開始 = DateTime.MinValue;
        public DateTime dte作業終了日終了 = DateTime.MinValue;

        private string sourceSQL;                           // 一覧のレコードソースのSQL
        private int intWindowHeight = 0;                        // 現在保持している高さ
        private int intWindowWidth = 0;                         // 現在保持している幅
        private bool bln履歴表示 = false;                           // 履歴表示する？



        private Control? previousControl;
        private SqlConnection? cn;

        public F_出荷管理旧()
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



        //全表示設定
        private void InitializeFilter()
        {
            str出荷コード開始 = "";
            str出荷コード終了 = "";
            dte出荷予定日1 = DateTime.MinValue;
            dte出荷予定日2 = DateTime.MinValue;
            str型番 = "";
            lngシリアル番号指定 = 0;
            lngシリアル番号開始 = -1;
            lngシリアル番号終了 = -1;
            str発送先名 = "";
            str顧客コード = "";
            str顧客名 = "";
            lng作業終了指定 = 0;
            dte作業終了日開始 = DateTime.MinValue;
            dte作業終了日終了 = DateTime.MinValue;
            bln履歴表示 = 履歴トグル.Checked;

        }

        private void Form_Load(object sender, EventArgs e)
        {
            //テスト用
            //dataGridView1.ColumnCount = 50;
            //dataGridView1.RowCount = 1000;
            //return;
            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");
            //実行中フォーム起動

            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(CommonConstants.LoginUserCode, this);

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

            // DataGirdViewのTypeを取得
            System.Type dgvtype = typeof(DataGridView);
            // プロパティ設定の取得
            System.Reflection.PropertyInfo dgvPropertyInfo = dgvtype.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            // 対象のDataGridViewにtrueをセットする
            dgvPropertyInfo.SetValue(dataGridView1, true, null);

            ////setall();
            初期表示ボタン_Click(sender, e);
            Cleargrid(dataGridView1);
            fn.WaitForm.Close();




        }

        private void Form_Resize(object sender, EventArgs e)
        {
            try
            {
                this.SuspendLayout();

                //this.サブ.Height = this.サブ.Height + (this.Height - intWindowHeight);

                this.ResumeLayout();

                intWindowHeight = this.Height; // 高さ保存
                intWindowWidth = this.Width;   // 幅保存

            }
            catch (Exception ex)
            {
                MessageBox.Show($"リサイズ中にエラーが発生しました。\n\n{ex.GetType().Name} : {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public int DoUpdate()
        {
            int result = -1;
            try
            {
                Filtering();

            }
            catch (Exception ex)
            {
                result = -1;
                MessageBox.Show(this.Name + "_DoUpdate - " + ex.Message);
            }

            return result;
        }

        //private void Filtering()
        public void Filtering()
        {
            try
            {
                // 画面描画を抑止する

                DateTime dtePrevious; // 前回出勤日
                DateTime dteNext;     // 次回出勤日
                string strWhere = string.Empty;

                if (str出荷コード開始 != "")
                {
                    strWhere = FunctionClass.WhereString(strWhere, $"'{str出荷コード開始}' <= 出荷コード AND 出荷コード <= '{str出荷コード終了}'");
                }

                if (dte出荷予定日1 != DateTime.MinValue)
                {
                    strWhere = FunctionClass.WhereString(strWhere, $"'{dte出荷予定日1}' <= 出荷予定日 AND 出荷予定日 <= '{dte出荷予定日2}'");
                }

                if (str受注コード開始 != "")
                {
                    strWhere = FunctionClass.WhereString(strWhere, $"'{str受注コード開始}' <= 受注コード AND 受注コード <= '{str受注コード終了}'");
                }

                if (str型番詳細 == "")
                {
                    if (str型番 != "")
                    {
                        strWhere = FunctionClass.WhereString(strWhere, $"型番 LIKE '%{ConvLiteral(str型番)}%'");
                    }
                }
                else
                {
                    strWhere = FunctionClass.WhereString(strWhere, str型番詳細);
                }

                switch (lngシリアル番号指定)
                {
                    case 1:
                        if (lngシリアル番号開始 == -1)
                        {
                            strWhere = FunctionClass.WhereString(strWhere, "出荷シリアル番号1 IS NOT NULL");
                        }
                        else
                        {
                            strWhere = FunctionClass.WhereString(strWhere, $"((出荷シリアル番号1 <= {lngシリアル番号開始} AND {lngシリアル番号開始} <= 出荷シリアル番号2 or " +
                                $"出荷シリアル番号1 <= {lngシリアル番号終了} AND {lngシリアル番号終了} <= 出荷シリアル番号2) or " +
                                $"({lngシリアル番号開始} <= 出荷シリアル番号1 AND 出荷シリアル番号2 <= {lngシリアル番号終了}))");
                        }
                        break;
                    case 2:
                        strWhere = FunctionClass.WhereString(strWhere, "出荷シリアル番号1 IS NULL");
                        break;
                }
                if (str発送先名 != "")
                {
                    strWhere = FunctionClass.WhereString(strWhere, $"発送先名 LIKE '%{ConvLiteral(str発送先名)}%'");
                }

                if (str顧客コード != "")
                {
                    strWhere = FunctionClass.WhereString(strWhere, $"顧客コード='{str顧客コード}'");
                }

                switch (lng作業終了指定)
                {
                    case 1:
                        if (dte作業終了日開始 == DateTime.MinValue)
                        {
                            strWhere = FunctionClass.WhereString(strWhere, "出荷終了日 is not null");
                        }
                        else
                        {
                            strWhere = FunctionClass.WhereString(strWhere, $"'{dte作業終了日開始}' <= 出荷終了日 and 出荷終了日 <= '{dte作業終了日終了}'");
                        }
                        break;
                    case 2:
                        strWhere = FunctionClass.WhereString(strWhere, "出荷終了更新日 is null");
                        break;
                }

                if (!bln履歴表示) strWhere = FunctionClass.WhereString(strWhere, "無効日 is null");

                // フォーム更新
                Connect();
                string sql = "SELECT 現品票出力 as 出力,出荷コード,受注コード," +
                            "受注版数 as 版,IIf([PaymentConfirmation] = 0, '', '■') as 入," +
                            "出荷予定日,品名,型番," +
                            "出荷数量 as 数量," +
                            "format(出荷シリアル番号1,'00000000') as シリアル番号1," +
                            "format(出荷シリアル番号2,'00000000') as シリアル番号2," +
                            "発送先名,外観目視検査者頭文字 as 目,梱包作業者頭文字 as 梱," +
                            "出荷終了日 as 作業終了日,納品書,無効日 " +
                            "FROM uv_出荷管理 ";

                sourceSQL = sql + $"where {strWhere}"; //ORDER BY ";

                string sqlsum = "";

                if (bln履歴表示)
                {
                    有効件数.Text = FunctionClass.GetRecordCount(cn, "uv_出荷管理", $" {strWhere} and 無効日 is null").ToString();
                    if (有効件数.Text != "0")
                    {
                        sqlsum = $"SELECT SUM(出荷数量) AS num FROM uv_出荷管理 where {strWhere} and 無効日 is null";
                        合計数量.Text = GetScalar<int>(cn, sqlsum).ToString("#,0");

                        sqlsum = $"SELECT SUM(金額) AS num FROM uv_出荷管理 where {strWhere} and 無効日 is null";
                        合計金額.Text = GetScalar<int>(cn, sqlsum).ToString("#,0");
                    }

                }
                else
                {
                    有効件数.Text = FunctionClass.GetRecordCount(cn, "uv_出荷管理", $" {strWhere} ").ToString();
                    if (有効件数.Text != "0")
                    {
                        sqlsum = $"SELECT SUM(出荷数量) AS num FROM uv_出荷管理 where {strWhere} ";
                        合計数量.Text = GetScalar<int>(cn, sqlsum).ToString("#,0");

                        sqlsum = $"SELECT SUM(金額) AS num FROM uv_出荷管理 where {strWhere} ";
                        合計金額.Text = GetScalar<int>(cn, sqlsum).ToString("#,0");
                    }
                }

                Connect();
                DataGridUtils.SetDataGridView(cn, sourceSQL, this.dataGridView1);

                MyApi myapi = new MyApi();
                int xSize, ySize, intpixel, twipperdot;

                // 1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
                intpixel = myapi.GetLogPixel();
                twipperdot = myapi.GetTwipPerDot(intpixel);

                intWindowHeight = this.Height;
                intWindowWidth = this.Width;

                // DataGridViewの設定
                // dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200); // 薄い黄色
                dataGridView1.Columns[1].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200); // 薄い黄色

                dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                dataGridView1.ColumnHeadersHeight = 25;

                // 0列目はaccessでは行ヘッダのため、ずらす
                dataGridView1.Columns[0].Width = 37;
                dataGridView1.Columns[1].Width = 74;
                dataGridView1.Columns[2].Width = 84;
                dataGridView1.Columns[3].Width = 23;
                dataGridView1.Columns[4].Width = 23;
                dataGridView1.Columns[5].Width = 87;
                dataGridView1.Columns[6].Width = 114;
                dataGridView1.Columns[7].Width = 212;
                dataGridView1.Columns[8].Width = 45;
                dataGridView1.Columns[9].Width = 82;
                dataGridView1.Columns[10].Width = 82;
                dataGridView1.Columns[11].Width = 230;
                dataGridView1.Columns[12].Width = 20;
                dataGridView1.Columns[13].Width = 20;
                dataGridView1.Columns[14].Width = 87;
                dataGridView1.Columns[15].Width = 20;
                dataGridView1.Columns[16].Visible=false;//無効日　2重線を引くため

                dataGridView1.Columns[8].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                if (strWhere == "")
                {
                    初期表示ボタン.ForeColor = Color.FromArgb(255, 0, 0);
                    本日出荷分ボタン.ForeColor = Color.FromArgb(0, 0, 0);
                    前日出荷分ボタン.ForeColor = Color.FromArgb(0, 0, 0);
                    翌日出荷分ボタン.ForeColor = Color.FromArgb(0, 0, 0);
                }
                else
                {
                    初期表示ボタン.ForeColor = Color.FromArgb(0, 0, 0);

                    dtePrevious = DateTime.Now.AddDays(-1);
                    FunctionClass func = new FunctionClass();

                    //前日が休日なら営業日までさかのぼる
                    while (func.OfficeClosed(cn, dtePrevious) == 1)
                    {
                        dtePrevious = dtePrevious.AddDays(-1);
                    }

                    //翌日が休日なら営業日まで進める
                    dteNext = DateTime.Now.AddDays(+1);
                    while (func.OfficeClosed(cn, dteNext) == 1)
                    {
                        dteNext = dtePrevious.AddDays(+1);
                    }

                    if (dte出荷予定日1.Date == DateTime.Now.Date && dte出荷予定日2.Date == DateTime.Now.Date)
                    {
                        本日出荷分ボタン.ForeColor = Color.FromArgb(255, 0, 0);
                        前日出荷分ボタン.ForeColor = Color.FromArgb(0, 0, 0);
                        翌日出荷分ボタン.ForeColor = Color.FromArgb(0, 0, 0);
                    }
                    else if (dte出荷予定日1.Date == dtePrevious.Date && dte出荷予定日2.Date == dtePrevious.Date)
                    {
                        本日出荷分ボタン.ForeColor = Color.FromArgb(0, 0, 0);
                        前日出荷分ボタン.ForeColor = Color.FromArgb(255, 0, 0);
                        翌日出荷分ボタン.ForeColor = Color.FromArgb(0, 0, 0);
                    }
                    else if (dte出荷予定日1.Date == dteNext.Date && dte出荷予定日2.Date == dteNext.Date)
                    {
                        本日出荷分ボタン.ForeColor = Color.FromArgb(0, 0, 0);
                        前日出荷分ボタン.ForeColor = Color.FromArgb(0, 0, 0);
                        翌日出荷分ボタン.ForeColor = Color.FromArgb(255, 0, 0);
                    }
                    else
                    {
                        本日出荷分ボタン.ForeColor = Color.FromArgb(0, 0, 0);
                        前日出荷分ボタン.ForeColor = Color.FromArgb(0, 0, 0);
                        翌日出荷分ボタン.ForeColor = Color.FromArgb(0, 0, 0);
                    }
                }

                return;
                //return 1;
            }
            catch (Exception ex)
            {
                switch (ex.HResult)
                {
                    case 2757:
                        MessageBox.Show("抽出の条件式が誤っている可能性があります。", "");
                        break;
                    default:
                        Console.WriteLine($"_Filter - {ex.HResult}: {ex.Message}");
                        break;
                }
            }
        }

        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //無効日がnullの行に線を引く
            if (dataGridView1.Rows[e.RowIndex].Cells[16].Value != null &&
        !string.IsNullOrEmpty(dataGridView1.Rows[e.RowIndex].Cells[16].Value.ToString()))
            {
                // 描画範囲を調整する
                Rectangle rowBounds = new Rectangle(
                    e.RowBounds.Left,
                    e.RowBounds.Top,
                    dataGridView1.RowHeadersWidth + dataGridView1.Columns.GetColumnsWidth(DataGridViewElementStates.Visible) - 1,
                    e.RowBounds.Height);

                using (Pen p = new Pen(Color.Red, 2))
                {
                    // 線を描画
                    e.Graphics.DrawLine(p, rowBounds.Left, rowBounds.Top + rowBounds.Height / 2,
                        rowBounds.Right, rowBounds.Top + rowBounds.Height / 2);
                }
            }
        }

        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            int value;

            if (e.ColumnIndex == 0 )
            {
                if (e.Value != null && !string.IsNullOrWhiteSpace(e.Value.ToString()))
                {
                    e.CellStyle.ForeColor = Color.Red; // 赤                    
                }
                else
                {
                    e.CellStyle.ForeColor = Color.Black; // 黒                    
                }
                e.Value = "■"; // 値を"■"に設定
            }
            else if(e.ColumnIndex == 15)
            {
                switch (e.Value)
                {
                    case "指定用紙":
                        e.CellStyle.ForeColor = Color.Red; // 赤
                        break;

                    case "自社用紙":
                        e.CellStyle.ForeColor = Color.Black;
                        break;

                    default:
                        e.CellStyle.ForeColor = Color.Purple;
                        break;
                }
                e.Value = "■"; // 値を"■"に設定
            }
        }

        private void DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
                this.dataGridView1.Focus();
                F_受注 targetform = new F_受注();
                targetform.varOpenArgs = $"{dataGridView1.SelectedRows[0].Cells[2].Value?.ToString()}," +
                    $"{dataGridView1.SelectedRows[0].Cells[3].Value?.ToString()}";
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
            try
            {
                this.dataGridView1.Focus(); // サブフォームにフォーカスを設定
                F_出荷管理_抽出 targetform = new F_出荷管理_抽出();
                targetform.MdiParent = this.MdiParent;
                targetform.FormClosed += (s, args) => { this.Enabled = true; };
                this.Enabled = false;

                targetform.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"フォームの抽出中にエラーが発生しました。\n\n{ex.GetType().Name} : {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void コマンド出力_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Focus();
            F_出力 targetform = new F_出力();
            targetform.DataGridView = dataGridView1;
            targetform.MdiParent = this.MdiParent;
            targetform.FormClosed += (s, args) => { this.Enabled = true; };
            this.Enabled = false;

            targetform.Show();

        }

        private void コマンド更新_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Focus();
            Filtering();
            Cleargrid(dataGridView1);
        }

        private void コマンド検索_Click(object sender, EventArgs e)
        {
            MessageBox.Show("検索は現在開発中です", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void FunctionKeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {

                    case Keys.F1:
                        if (コマンド抽出.Enabled)
                        {
                            コマンド抽出.Focus();
                            コマンド抽出_Click(sender, e);
                        }
                        break;
                    case Keys.F2:
                        if (コマンド検索.Enabled)
                        {
                            コマンド検索.Focus();
                            コマンド検索_Click(sender, e);
                        }
                        break;
                    case Keys.F3:
                        if (コマンド更新.Enabled)
                        {
                            コマンド更新.Focus();
                            コマンド更新_Click(sender, e);
                        }
                        break;
                    case Keys.F4:

                        break;
                    case Keys.F5:
                        if (コマンド受注.Enabled)
                        {
                            コマンド受注.Focus();
                            コマンド受注_Click(sender, e);
                        }
                        break;
                    case Keys.F6:
                        if (コマンド現品票.Enabled)
                        {
                            コマンド現品票.Focus();
                            コマンド現品票_Click(sender, e);
                        }
                        break;
                    case Keys.F7:
                        if (コマンド現品票印刷.Enabled)
                        {
                            コマンド現品票印刷.Focus();
                            コマンド現品票印刷_Click(sender, e);
                        }
                        break;
                    case Keys.F8:
                        if (コマンド現品票全印刷.Enabled)
                        {
                            コマンド現品票全印刷.Focus();
                            コマンド現品票全印刷_Click(sender, e);
                        }
                        break;
                    case Keys.F9:
                        if (コマンド印刷.Enabled)
                        {
                            コマンド印刷.Focus();
                            コマンド印刷_Click(sender, e);
                        }
                        break;
                    case Keys.F10:
                        if (コマンド出力.Enabled)
                        {
                            コマンド出力.Focus();
                            コマンド出力_Click(sender, e);
                        }
                        break;
                    case Keys.F11:

                        break;
                    case Keys.F12:
                        if (コマンド終了.Enabled)
                        {
                            コマンド終了.Focus();
                            コマンド終了_Click(sender, e);
                        }
                        break;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{Name}_FunctionKeyDown - {ex.GetType().Name} : {ex.Message}");
            }
        }



        private void Form_Unload(object sender, FormClosedEventArgs e)
        {
            LocalSetting ls = new LocalSetting();
            // ウィンドウの配置情報を保存する
            ls.SavePlace(CommonConstants.LoginUserCode, this);
        }


        private void コマンド受注_Click(object sender, EventArgs e)
        {

            this.dataGridView1.Focus();
            F_受注 targetform = new F_受注();
            targetform.varOpenArgs = $"{dataGridView1.SelectedRows[0].Cells[2].Value?.ToString()}," +
                $"{dataGridView1.SelectedRows[0].Cells[3].Value?.ToString()}";
            targetform.MdiParent = this.MdiParent;
            targetform.FormClosed += (s, args) => { this.Enabled = true; };
            this.Enabled = false;

            targetform.Show();

        }

        private void 初期表示ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1 != null)
                {
                    dataGridView1.Focus(); // サブフォームにフォーカスを設定

                    // 初期表示処理
                    InitializeFilter();
                    lng作業終了指定 = 2;
                    Filtering();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"初期表示ボタンのクリック中にエラーが発生しました。\n\n{ex.GetType().Name} : {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 履歴トグル_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Focus();
            bln履歴表示 = 履歴トグル.Checked;
            Filtering();
        }

        private void コマンド現品票全印刷_Click(object sender, EventArgs e)
        {
            //出荷検査記録 13年以上前の検査表が必要？
        }

        private void コマンド現品票印刷_Click(object sender, EventArgs e)
        {
            //出荷検査記録 13年以上前の検査表が必要？
        }

        private void コマンド現品票_Click(object sender, EventArgs e)
        {
            MessageBox.Show("存在しないフォームを参照しています", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void コマンド印刷_Click(object sender, EventArgs e)
        {
            //出荷管理 必要？
        }

        private void 前日出荷分ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dtePrevious = DateTime.Now.AddDays(-1); // 前回出勤日（今回は前日）


                InitializeFilter();
                Connect();
                FunctionClass func = new FunctionClass();

                while (func.OfficeClosed(cn, dtePrevious) == 1)
                {
                    dtePrevious = dtePrevious.AddDays(-1);
                }

                dte出荷予定日1 = dtePrevious;
                dte出荷予定日2 = dtePrevious;

                // DoFilter メソッドの具体的な実装が不明なので、仮の実装としてメソッドを呼び出す
                Filtering();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"前日出荷分ボタン_Click - {ex.Message}");
            }
        }

        private void 本日出荷分ボタン_Click(object sender, EventArgs e)
        {
            InitializeFilter();
            dte出荷予定日1 = DateTime.Now.Date;
            dte出荷予定日2 = DateTime.Now.Date;
            Filtering();
        }

        private void 翌日出荷分ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                DateTime dteNext = DateTime.Now.AddDays(+1); // 翌日出勤日


                InitializeFilter();
                Connect();
                FunctionClass func = new FunctionClass();

                while (func.OfficeClosed(cn, dteNext) == 1)
                {
                    dteNext = dteNext.AddDays(+1);
                }

                dte出荷予定日1 = dteNext;
                dte出荷予定日2 = dteNext;

                // DoFilter メソッドの具体的な実装が不明なので、仮の実装としてメソッドを呼び出す
                Filtering();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"前日出荷分ボタン_Click - {ex.Message}");
            }
        }

        private void 最終製品検査記録プレビューボタン_Click(object sender, EventArgs e)
        {
            //出荷検査記録 13年以上前の検査表が必要？
        }

        private void 最終製品検査記録印刷ボタン_Click(object sender, EventArgs e)
        {

        }
    }
}