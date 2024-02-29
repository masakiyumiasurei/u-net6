using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using Microsoft.Data.SqlClient;
using u_net.Public;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace u_net
{
    public partial class F_購買申請管理 : MidForm
    {
        public DateTime dtm申請日開始 = DateTime.MinValue;
        public DateTime dtm申請日終了 = DateTime.MinValue;
        public DateTime dtm購買納期開始 = DateTime.MinValue;
        public DateTime dtm購買納期終了 = DateTime.MinValue;
        public DateTime dtm出荷予定日開始 = DateTime.MinValue;
        public DateTime dtm出荷予定日終了 = DateTime.MinValue;
        public string str基本型式名 = "";
        public string strシリーズ名 = "";
        public string str申請者コード = "";
        public string str申請者名 = "";
        public long lng承認指定;
        public long lng終了指定;
        public long lng完了指定;
        public long lng削除指定;
        public bool binShowCompleted;//完了データを表示させる
        public bool binShowDeleted;//削除データを表示させる
        public bool appEXT;
        public object objParent;

        private decimal TotalMoney;

        private DataGridView gridobject;
        private int intSelectionMode;//グリッドの選択モード
        private int intWindowHeight;//現在保持している高さ
        private int intWindowWidth;//現在保持している幅
        private int intButton;//保存マウスボタン

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

        //現在選択されているデータのコードを取得する
        public string CurrentCode
        {
            get
            {
                if (gridobject.CurrentRow != null && gridobject.Rows.Count > 0)
                {
                    return 購買申請明細.Rows[購買申請明細.CurrentRow.Index].Cells[0].Value.ToString() ?? string.Empty;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        //現在選択されているデータの版数を取得する
        public string CurrentEdition
        {
            get
            {
                if (gridobject.CurrentRow != null && gridobject.Rows.Count > 0)
                {
                    return 購買申請明細.Rows[購買申請明細.CurrentRow.Index].Cells[1].Value.ToString() ?? string.Empty;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                control.PreviewKeyDown += OriginalClass.ValidateCheck;
            }

            //実行中フォーム起動
            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");

            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(CommonConstants.LoginUserCode, this);

            MyApi myapi = new MyApi();
            int xSize, ySize, intpixel, twipperdot;

            //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
            intpixel = myapi.GetLogPixel();
            twipperdot = myapi.GetTwipPerDot(intpixel);

            intWindowHeight = this.Height;
            intWindowWidth = this.Width;

            gridobject = this.購買申請明細;

            // DataGridViewの設定
            gridobject.AllowUserToResizeColumns = true;
            gridobject.Font = new Font("MS ゴシック", 9);
            gridobject.ColumnHeadersDefaultCellStyle.Font = new Font("MS ゴシック", 9);
            gridobject.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.False;
            gridobject.RowsDefaultCellStyle.Font = new Font("MS ゴシック", 9);
            gridobject.DefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 210, 255);
            gridobject.DefaultCellStyle.SelectionForeColor = Color.Black;
            gridobject.DefaultCellStyle.WrapMode = DataGridViewTriState.False;
            gridobject.GridColor = Color.FromArgb(230, 230, 230);
            gridobject.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            gridobject.BackgroundColor = Color.FloralWhite;
            gridobject.MultiSelect = false;
            gridobject.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridobject.ScrollBars = ScrollBars.Both;
            int intSelectionMode = (int)gridobject.SelectionMode;
            gridobject.ScrollBars = ScrollBars.Both;

            //ダブルバッファ処理設定
            System.Type dgvtype = typeof(DataGridView);
            System.Reflection.PropertyInfo dgvPropertyInfo = dgvtype.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            dgvPropertyInfo.SetValue(gridobject, true, null);


            // 抽出条件を初期化する
            InitializeFilter();

            // リストを更新する
            if (!DoUpdate())
            {
                //MessageBox.Show($"Initialization failed. [{Name}] will be closed.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                MessageBox.Show($"初期化に失敗しました。[{Name}]を終了します。", "初期処理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                // Assuming this is a Form, close it
                Close();
                return;
            }

            // Redraw the grid
            gridobject.Invalidate();

            myapi.GetFullScreen(out xSize, out ySize);

            int x = 10, y = 10;

            this.Size = new Size(this.Width, ySize * myapi.GetTwipPerDot(intpixel) - 1200);
            //accessのmovesizeメソッドの引数の座標単位はtwipなので以下で

            this.Size = new Size(this.Width, ySize - 1200 / twipperdot);

            this.StartPosition = FormStartPosition.Manual; // 手動で位置を指定
            int screenWidth = Screen.PrimaryScreen.Bounds.Width; // プライマリスクリーンの幅
            x = (screenWidth - this.Width) / 2;
            this.Location = new Point(x, y);


            fn.WaitForm.Close();
            Cleargrid(購買申請明細);
        }

        private void Form_Unload(object sender, FormClosingEventArgs e)
        {
            LocalSetting ls = new LocalSetting();
            // ウィンドウの配置情報を保存する
            ls.SavePlace(CommonConstants.LoginUserCode, this);
        }


        private void InitializeFilter()
        {
            this.dtm申請日開始 = DateTime.MinValue;
            this.dtm申請日終了 = DateTime.MinValue;
            this.dtm購買納期開始 = DateTime.MinValue;
            this.dtm購買納期終了 = DateTime.MinValue;
            this.dtm出荷予定日開始 = DateTime.MinValue;
            this.dtm出荷予定日終了 = DateTime.MinValue;
            this.str基本型式名 = "";
            this.strシリーズ名 = "";
            this.str申請者コード = "";
            this.str申請者名 = "";
            this.lng承認指定 = 0;
            this.lng終了指定 = 1;
            this.lng完了指定 = 1;
            this.lng削除指定 = 1;
        }

        public bool DoUpdate()
        {
            bool result = false;

            try
            {
                gridobject.SuspendLayout();

                if (SetGrid() && FormatGrid())
                {
                    return true;
                }
                else
                {
                    // Handle the error or logging as needed
                    Debug.WriteLine($"{nameof(F_購買申請管理)}_DoUpdate - An error occurred during update.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                // Handle the exception or logging as needed
                Debug.WriteLine($"{nameof(F_購買申請管理)}_DoUpdate - {ex.GetType().Name}: {ex.Message}");
                return false;
            }
            finally
            {
                gridobject.ResumeLayout();
            }
        }


        private bool SetGrid()
        {
            Connect();

            string strSQL = null;
            string strFilter = null;
            bool result = false;
            DataTable dt = new DataTable();

            try
            {
                // 申請日指定
                if (dtm申請日開始 != DateTime.MinValue && dtm申請日終了 != DateTime.MinValue)
                {
                    strFilter = FunctionClass.WhereString(strFilter, $"'{dtm申請日開始.ToString("yyyy/MM/dd")}'<=申請日 and 申請日<='{dtm申請日終了.ToString("yyyy/MM/dd")}'");
                }

                // 購買納期指定
                if (dtm購買納期開始 != DateTime.MinValue && dtm購買納期終了 != DateTime.MinValue)
                {
                    strFilter = FunctionClass.WhereString(strFilter, $"'{dtm購買納期開始.ToString("yyyy/MM/dd")}'<=購買納期 and 購買納期<='{dtm購買納期終了.ToString("yyyy/MM/dd")}'");
                }

                // 出荷予定日指定
                if (dtm出荷予定日開始 != DateTime.MinValue && dtm出荷予定日終了 != DateTime.MinValue)
                {
                    strFilter = FunctionClass.WhereString(strFilter, $"'{dtm出荷予定日開始.ToString("yyyy/MM/dd")}'<=出荷予定日 and 出荷予定日<='{dtm出荷予定日終了.ToString("yyyy/MM/dd")}'");
                }

                // 基本型式名指定
                if (!string.IsNullOrEmpty(str基本型式名))
                {
                    strFilter = FunctionClass.WhereString(strFilter, $"基本型式名 LIKE '%{str基本型式名}%'");
                }

                // シリーズ名指定
                if (!string.IsNullOrEmpty(strシリーズ名))
                {
                    strFilter = FunctionClass.WhereString(strFilter, $"シリーズ名 LIKE '%{strシリーズ名}%'");
                }

                // 申請者コード
                if (!string.IsNullOrEmpty(str申請者コード))
                {
                    strFilter = FunctionClass.WhereString(strFilter, $"申請者コード = '{str申請者コード}'");
                }

                // 申請者名
                if (!string.IsNullOrEmpty(str申請者名))
                {
                    strFilter = FunctionClass.WhereString(strFilter, $"申請者名 LIKE '%{str申請者名}%'");
                }

                // 承認指定
                switch (lng承認指定)
                {
                    case 1:
                        strFilter = FunctionClass.WhereString(strFilter, "承認 IS NULL");
                        break;
                    case 2:
                        strFilter = FunctionClass.WhereString(strFilter, "承認 IS NOT NULL");
                        break;
                }

                // 終了指定
                switch (lng終了指定)
                {
                    case 1:
                        strFilter = FunctionClass.WhereString(strFilter, "終了 IS NULL");
                        break;
                    case 2:
                        strFilter = FunctionClass.WhereString(strFilter, "終了 IS NOT NULL");
                        break;
                }

                // 完了指定
                switch (lng完了指定)
                {
                    case 1:
                        strFilter = FunctionClass.WhereString(strFilter, "完了 IS NULL");
                        break;
                    case 2:
                        strFilter = FunctionClass.WhereString(strFilter, "完了 IS NOT NULL");
                        break;
                }

                // 削除指定
                switch (lng削除指定)
                {
                    case 1:
                        strFilter = FunctionClass.WhereString(strFilter, "削除 IS NULL");
                        break;
                    case 2:
                        strFilter = FunctionClass.WhereString(strFilter, "削除 IS NOT NULL");
                        break;
                }

                // SQL文の構築
                if (string.IsNullOrEmpty(strFilter))
                {
                    //strSQL = "SELECT * FROM V購買申請管理 ORDER BY 購買申請コード DESC";
                    strSQL = "SELECT * FROM V購買申請管理 ORDER BY 購買申請コード DESC";
                }
                else
                {
                    //strSQL = $"SELECT * FROM V購買申請管理 WHERE {strFilter} ORDER BY 購買申請コード DESC";
                    strSQL = $"SELECT * FROM V購買申請管理 WHERE {strFilter}  ORDER BY 購買申請コード DESC";
                }

                using (SqlCommand command = new SqlCommand(strSQL, cn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dt);
                    }
                }

                if (dt.Rows.Count <= 0)
                {
                    appEXT = true;
                    gridobject.DataSource = null;
                }
                else
                {
                    gridobject.DataSource = dt;
                    appEXT = false;
                }

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{GetType().Name}_SetGrid - {ex.GetType().Name} : {ex.Message}");
            }
            finally
            {
                this.表示件数.Text = (gridobject.RowCount - gridobject.Rows.GetRowCount(DataGridViewElementStates.Frozen)).ToString();
            }

            return result;
        }

        public bool FormatGrid()
        {
            try
            {
                TotalMoney = 0;

                if (gridobject.Rows.Count <= 0)
                {
                    合計金額.Text = "0";
                    税込合計金額.Text = "0";
                    return true;
                }

                Connect();

                // 描画を抑止する
                gridobject.SuspendLayout();

                // 合計金額を求める(行および列の初期値は0)
                for (int row = 0; row < gridobject.Rows.Count; row++)
                {
                    // 承認のチェック
                    if (gridobject.Rows[row].Cells[12].Value.ToString() == "■")
                    {
                        // 削除のチェック
                        if (gridobject.Rows[row].Cells[16].Value.ToString() != "■")
                        {
                            // 小計のチェック
                            if (gridobject.Rows[row].Cells[10].Value.ToString() != "")
                            {
                                // 合計金額
                                TotalMoney += Convert.ToInt32(gridobject.Rows[row].Cells[10].Value);
                            }
                        }
                    }
                }



                MyApi myapi = new MyApi();
                int xSize, ySize, intpixel, twipperdot;

                ////1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
                intpixel = myapi.GetLogPixel();
                twipperdot = myapi.GetTwipPerDot(intpixel);

                intWindowHeight = this.Height;
                intWindowWidth = this.Width;

                //// DataGridViewの設定
                gridobject.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200); // 薄い黄色
                gridobject.Columns[1].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200);

                //0列目はaccessでは行ヘッダのため、ずらす
                gridobject.Columns[0].Width = 1400 / twipperdot; //購買申請コード
                gridobject.Columns[1].Width = 400 / twipperdot; //版数
                gridobject.Columns[2].Width = 1300 / twipperdot; //申請日
                gridobject.Columns[3].Width = 1300 / twipperdot; //購買納期
                gridobject.Columns[4].Width = 1300 / twipperdot; //出荷予定日
                gridobject.Columns[5].Width = 2300 / twipperdot; //基本型式名
                gridobject.Columns[6].Width = 1500 / twipperdot; //シリーズ名
                gridobject.Columns[7].Width = 2000 / twipperdot; //ロット番号
                gridobject.Columns[8].Width = 800 / twipperdot; //数量
                gridobject.Columns[9].Width = 1200 / twipperdot; //材料単価
                gridobject.Columns[10].Width = 1200 / twipperdot; //小計
                gridobject.Columns[11].Visible=false; //申請者コード
                gridobject.Columns[12].Width = 1200 / twipperdot; //申請者名
                gridobject.Columns[13].Width = 300 / twipperdot; //承
                gridobject.Columns[14].Width = 300 / twipperdot; //完
                gridobject.Columns[15].Width = 300 / twipperdot; //製
                gridobject.Columns[16].Width = 300 / twipperdot; //終
                gridobject.Columns[17].Width = 300 / twipperdot; //削
                gridobject.Columns[18].Visible = false;
                gridobject.Columns[19].Visible = false;
                gridobject.Columns[20].Visible = false;
                gridobject.Columns[21].Visible = false;
                gridobject.Columns[22].Visible = false;
                gridobject.Columns[23].Visible = false;
                gridobject.Columns[24].Visible = false;
                gridobject.Columns[25].Visible = false;

                gridobject.Columns[8].DefaultCellStyle.Format = "N0";
                gridobject.Columns[10].DefaultCellStyle.Format = "N0";
                gridobject.Columns[7].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                gridobject.Columns[8].DefaultCellStyle.Alignment= DataGridViewContentAlignment.MiddleRight;
                gridobject.Columns[9].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                gridobject.Columns[10].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                //材料単価はセルごとにフォーマットが異なるので、cellformattingで行う

                // カーソル位置の復元などの後処理
                gridobject.ResumeLayout();

                // Retrieve 最新税率
                string strSQL = "SELECT 消費税率 FROM T消費税 WHERE (適用日 = (SELECT MAX(適用日) AS 適用日 FROM T消費税 AS T消費税_1))";

                DataTable dataTable = new DataTable();

                using (SqlCommand command = new SqlCommand(strSQL, cn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(dataTable);
                    }
                }

                Debug.Print(TotalMoney.ToString());
                合計金額.Text = TotalMoney.ToString("#,0");

                if (dataTable.Rows.Count > 0)
                {
                    decimal 消費税率 = Convert.ToDecimal(dataTable.Rows[0]["消費税率"]);

                    // 税込合計金額の計算
                    decimal TotalMoneyIncludingTax = TotalMoney * (1 + 消費税率);

                    Debug.Print(TotalMoneyIncludingTax.ToString());

                    // 端数を切り捨てて表示
                    ////税込合計金額.Text = Math.Floor(TotalMoneyIncludingTax).ToString("#,0");
                    string[] tmp = TotalMoneyIncludingTax.ToString("#,0").Split(".");
                    税込合計金額.Text = tmp[0];
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(F_購買申請管理)}_FormatGrid - {ex.GetType().Name}: {ex.Message}");
                return false;
            }
            finally
            {
                gridobject.ResumeLayout();
            }
        }

        private void 購買申請明細_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            //材料単価の表示調整
            if (e.ColumnIndex == 9 && e.Value != null)
            {
                // e.Valueは、対象のセルの値
                decimal value;
                if (Decimal.TryParse(e.Value.ToString(), out value))
                {
                    // 小数部分が0かどうかをチェック
                    if (value == Math.Floor(value))
                    {
                        // 小数部分が0の場合は整数形式で表示
                        e.Value = String.Format("{0:N0}", value);
                    }
                    else
                    {
                        // 小数部分が0でない場合は小数第1位まで表示
                        e.Value = String.Format("{0:N1}", value);
                    }
                }
            }
        }
        private void DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
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
                selectedData += "," + 購買申請明細.Rows[e.RowIndex].Cells[1].Value.ToString(); // 2列目のデータを取得

                F_購買申請 targetform = new F_購買申請();

                targetform.args = selectedData;
                targetform.ShowDialog();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex >= 0) // ヘッダー行でない場合
            //{
            //    購買申請明細.ClearSelection();
            //    購買申請明細.Rows[e.RowIndex].Selected = true;
            //}
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
            //dataGridView.ClearSelection();

            //if (dataGridView.Rows.Count > 0)
            //{
            //    dataGridView.Rows[0].Selected = true;
            //    dataGridView.FirstDisplayedScrollingRowIndex = 0; // 先頭行を表示
            //}
        }

        private void コマンド終了_Click(object sender, EventArgs e)
        {
            購買申請明細.Focus();
            this.Close();
        }


        private void コマンド購買申請_Click(object sender, EventArgs e)
        {
            購買申請明細.Focus();
            F_購買申請 form = new F_購買申請();
            form.ShowDialog();
        }

        private void コマンド保守_Click(object sender, EventArgs e)
        {
            購買申請明細.Focus(); // DataGridViewにフォーカスを設定
            MessageBox.Show("機能が定義されていません。", "保守コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド印刷_Click(object sender, EventArgs e)
        {
            購買申請明細.Focus(); // DataGridViewにフォーカスを設定
            objParent = this;
            MessageBox.Show("現在開発中です。", "印刷コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド抽出_Click(object sender, EventArgs e)
        {
            購買申請明細.Focus();
            objParent = this;
            F_購買申請管理_抽出 form = new F_購買申請管理_抽出();
            form.ShowDialog();
        }

        private void コマンド入出力_Click(object sender, EventArgs e)
        {
            購買申請明細.Focus(); // DataGridViewにフォーカスを設定
            objParent = this;
            MessageBox.Show("機能が定義されていません。", "入出力コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド初期化_Click(object sender, EventArgs e)
        {
            購買申請明細.Focus(); // DataGridViewにフォーカスを設定
            objParent = this;
            MessageBox.Show("現在開発中です。", "初期化コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void コマンド更新_Click(object sender, EventArgs e)
        {
            try
            {
                購買申請明細.Focus();
                gridobject.SuspendLayout();
                ////objParent = this;
                if (SetGrid() && FormatGrid())
                {

                }
                else
                {
                    Debug.WriteLine($"{nameof(F_購買申請管理)}_コマンド更新_Click - An error occurred during update.");
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"{Name}_コマンド更新_Click - {ex.HResult}: {ex.Message}");
                MessageBox.Show("更新に失敗しました。", "更新コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                gridobject.ResumeLayout();
            }
        }

        private void コマンド検索_Click(object sender, EventArgs e)
        {
            購買申請明細.Focus();
            objParent = this;

            MessageBox.Show("現在開発中です。", "検索コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void F_購買申請管理_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Space: //コンボボックスならドロップダウン
                        {
                            Control activeControl = this.ActiveControl;
                            if (activeControl is System.Windows.Forms.ComboBox)
                            {
                                System.Windows.Forms.ComboBox activeComboBox = (System.Windows.Forms.ComboBox)activeControl;
                                activeComboBox.DroppedDown = true;
                            }
                        }
                        break;
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
                        if (コマンド初期化.Enabled)
                        {
                            コマンド初期化.Focus();
                            コマンド初期化_Click(sender, e);
                        }
                        break;
                    case Keys.F4:
                        if (コマンド更新.Enabled)
                        {
                            コマンド更新.Focus();
                            コマンド更新_Click(sender, e);
                        }
                        break;
                    case Keys.F5:
                        if (コマンド購買申請.Enabled)
                        {
                            コマンド購買申請.Focus();
                            コマンド購買申請_Click(sender, e);
                        }
                        break;
                    case Keys.F6:
                        break;
                    case Keys.F7:
                        break;
                    case Keys.F8:
                        break;
                    case Keys.F9:
                        if (コマンド印刷.Enabled)
                        {
                            コマンド印刷.Focus();
                            コマンド印刷_Click(sender, e);
                        }
                        break;
                    case Keys.F10:
                        if (コマンド保守.Enabled)
                        {
                            コマンド保守.Focus();
                            コマンド保守_Click(sender, e);
                        }
                        break;
                    case Keys.F11:
                        if (コマンド入出力.Enabled)
                        {
                            コマンド入出力.Focus();
                            コマンド入出力_Click(sender, e);
                        }
                        break;
                    case Keys.F12:
                        if (コマンド終了.Enabled)
                        {
                            コマンド終了.Focus();
                            コマンド終了_Click(sender, e);
                        }
                        break;
                    case Keys.Return:
                        if (ActiveControl == 購買申請明細)
                        {
                            F_購買申請 form = new F_購買申請();
                            form.args = CurrentCode + "," + CurrentEdition;
                            form.ShowDialog();
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{Name}_FunctionKeyDown - {ex.GetType().Name} : {ex.Message}");
            }
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

        private void Next3Button_Click(object sender, EventArgs e)
        {
            InitializeFilter();

            lng終了指定 = 0;
            lng完了指定 = 0;

            if (DateTime.Now.Day >= 21)
            {
                dtm購買納期開始 = new DateTime(DateTime.Now.AddMonths(3).Year, DateTime.Now.AddMonths(3).Month, 21);
                dtm購買納期終了 = new DateTime(DateTime.Now.AddMonths(4).Year, DateTime.Now.AddMonths(4).Month, 20);
            }
            else
            {
                dtm購買納期開始 = new DateTime(DateTime.Now.AddMonths(2).Year, DateTime.Now.AddMonths(2).Month, 21);
                dtm購買納期終了 = new DateTime(DateTime.Now.AddMonths(3).Year, DateTime.Now.AddMonths(3).Month, 20);
            }

            if (!DoUpdate())
            {
                MessageBox.Show("抽出処理は失敗しました。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Next4Button_Click(object sender, EventArgs e)
        {
            InitializeFilter();

            lng終了指定 = 0;
            lng完了指定 = 0;

            if (DateTime.Now.Day >= 21)
            {
                dtm購買納期開始 = new DateTime(DateTime.Now.AddMonths(4).Year, DateTime.Now.AddMonths(4).Month, 21);
                dtm購買納期終了 = new DateTime(DateTime.Now.AddMonths(5).Year, DateTime.Now.AddMonths(5).Month, 20);
            }
            else
            {
                dtm購買納期開始 = new DateTime(DateTime.Now.AddMonths(3).Year, DateTime.Now.AddMonths(3).Month, 21);
                dtm購買納期終了 = new DateTime(DateTime.Now.AddMonths(4).Year, DateTime.Now.AddMonths(4).Month, 20);
            }

            if (!DoUpdate())
            {
                MessageBox.Show("抽出処理は失敗しました。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void 購買申請明細_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            gridobject.SuspendLayout();
            gridobject.ResumeLayout(true);
        }

        private void 購買申請明細_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            gridobject.SuspendLayout();
            gridobject.ResumeLayout(false);
        }

        private void 前月ボタン_Click(object sender, EventArgs e)
        {
            InitializeFilter();

            this.lng終了指定 = 0;
            this.lng完了指定 = 0;

            DateTime currentDate = DateTime.Now;

            if (currentDate.Day >= 21)
            {
                dtm購買納期開始 = new DateTime(currentDate.AddMonths(-1).Year, currentDate.AddMonths(-1).Month, 21);
                dtm購買納期終了 = new DateTime(currentDate.Year, currentDate.Month, 20);
            }
            else
            {
                dtm購買納期開始 = new DateTime(currentDate.AddMonths(-2).Year, currentDate.AddMonths(-2).Month, 21);
                dtm購買納期終了 = new DateTime(currentDate.AddMonths(-1).Year, currentDate.AddMonths(-1).Month, 20);
            }

            if (!DoUpdate())
            {
                MessageBox.Show("抽出処理は失敗しました。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void 前々月ボタン_Click(object sender, EventArgs e)
        {
            InitializeFilter();

            this.lng終了指定 = 0;
            this.lng完了指定 = 0;

            DateTime currentDate = DateTime.Now;

            if (currentDate.Day >= 21)
            {
                dtm購買納期開始 = new DateTime(currentDate.AddMonths(-2).Year, currentDate.AddMonths(-2).Month, 21);
                dtm購買納期終了 = new DateTime(currentDate.AddMonths(-1).Year, currentDate.AddMonths(-1).Month, 20);
            }
            else
            {
                dtm購買納期開始 = new DateTime(currentDate.AddMonths(-3).Year, currentDate.AddMonths(-3).Month, 21);
                dtm購買納期終了 = new DateTime(currentDate.AddMonths(-2).Year, currentDate.AddMonths(-2).Month, 20);
            }

            if (!DoUpdate())
            {
                MessageBox.Show("抽出処理は失敗しました。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void 当月ボタン_Click(object sender, EventArgs e)
        {
            InitializeFilter();

            this.lng終了指定 = 0;
            this.lng完了指定 = 0;

            DateTime currentDate = DateTime.Now;

            if (currentDate.Day >= 21)
            {
                dtm購買納期開始 = new DateTime(currentDate.Year, currentDate.Month, 21);
                dtm購買納期終了 = new DateTime(currentDate.AddMonths(1).Year, currentDate.AddMonths(1).Month, 20);
            }
            else
            {
                dtm購買納期開始 = new DateTime(currentDate.AddMonths(-1).Year, currentDate.AddMonths(-1).Month, 21);
                dtm購買納期終了 = new DateTime(currentDate.Year, currentDate.Month, 20);
            }

            if (!DoUpdate())
            {
                MessageBox.Show("抽出処理は失敗しました。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void 翌月ボタン_Click(object sender, EventArgs e)
        {
            InitializeFilter();

            this.lng終了指定 = 0;
            this.lng完了指定 = 0;

            DateTime currentDate = DateTime.Now;

            if (currentDate.Day >= 21)
            {
                dtm購買納期開始 = new DateTime(currentDate.AddMonths(1).Year, currentDate.AddMonths(1).Month, 21);
                dtm購買納期終了 = new DateTime(currentDate.AddMonths(2).Year, currentDate.AddMonths(2).Month, 20);
            }
            else
            {
                dtm購買納期開始 = new DateTime(currentDate.Year, currentDate.Month, 21);
                dtm購買納期終了 = new DateTime(currentDate.AddMonths(1).Year, currentDate.AddMonths(1).Month, 20);
            }

            if (!DoUpdate())
            {
                MessageBox.Show("抽出処理は失敗しました。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void 翌々月ボタン_Click(object sender, EventArgs e)
        {
            InitializeFilter();

            this.lng終了指定 = 0;
            this.lng完了指定 = 0;

            DateTime currentDate = DateTime.Now;

            if (currentDate.Day >= 21)
            {
                dtm購買納期開始 = new DateTime(currentDate.AddMonths(2).Year, currentDate.AddMonths(2).Month, 21);
                dtm購買納期終了 = new DateTime(currentDate.AddMonths(3).Year, currentDate.AddMonths(3).Month, 20);
            }
            else
            {
                dtm購買納期開始 = new DateTime(currentDate.AddMonths(1).Year, currentDate.AddMonths(1).Month, 21);
                dtm購買納期終了 = new DateTime(currentDate.AddMonths(2).Year, currentDate.AddMonths(2).Month, 20);
            }

            if (!DoUpdate())
            {
                MessageBox.Show("抽出処理は失敗しました。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void コマンド購買申請_Click_1(object sender, EventArgs e)
        {
            購買申請明細.Focus();
            objParent = this;
            F_購買申請 form = new F_購買申請();
            form.args = CurrentCode + "," + CurrentEdition;
            form.ShowDialog();
        }

        
    }
}