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
using GrapeCity.Win.BarCode.ValueType;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using u_net.Public;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static u_net.Public.FunctionClass;
using static u_net.CommonConstants;
using static u_net.Public.OriginalClass;
using System.Data.Common;

namespace u_net
{
    public partial class F_受注管理 : MidForm
    {
        const string strSecondOrder = "受注コード";

        public string str検索コード;
        public string str受注コード1;
        public string str受注コード2;
        public DateTime dte受注日1;
        public DateTime dte受注日2;
        public DateTime dte出荷予定日1;
        public DateTime dte出荷予定日2;
        public DateTime dte受注納期1;
        public DateTime dte受注納期2;
        public string str注文番号;
        public string str顧客コード;
        public string str顧客名;
        public string str自社担当者コード;
        public bool ble受注承認指定;
        public byte byt受注承認;
        public bool ble出荷指定;
        public byte byt出荷;
        public DateTime dte出荷完了日1;
        public DateTime dte出荷完了日2;
        public bool ble受注完了承認指定;
        public byte byt受注完了承認;
        public byte byt無効日;
        public bool ble履歴表示;
        public Button CurrentOrder;
        private bool resizing = false;

        int intWindowHeight = 0;
        int intWindowWidth = 0;

        private Control? previousControl;
        private SqlConnection? cn;

        public string CurrentCode
        {
            // 現在選択されているデータのコードを取得する
            get
            {
                return dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value?.ToString();
            }
        }
        public string CurrentEdition
        {
            // 現在選択されているデータの版数を取得する
            get
            {
                return dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[1].Value?.ToString();
            }
        }
        public F_受注管理()
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

        //コード検索
        public override void SearchCode(string codeString)
        {
            //検索コード保持
            this.str検索コード = codeString;

            InitializeFilter();
            this.str受注コード1 = codeString;
            this.str受注コード2 = codeString;
            ////Filtering;
            DoUpdate();
        }

        //全表示設定
        private void InitializeFilter()
        {
            str受注コード1 = "";
            str受注コード2 = "";
            dte受注日1 = DateTime.MinValue;
            dte受注日2 = DateTime.MinValue;
            dte出荷予定日1 = DateTime.MinValue;
            dte出荷予定日2 = DateTime.MinValue;
            dte受注納期1 = DateTime.MinValue;
            dte受注納期2 = DateTime.MinValue;
            str注文番号 = "";
            str顧客コード = "";
            str顧客名 = "";
            str自社担当者コード = "";
            ble受注承認指定 = false;
            byt受注承認 = 1;
            ble出荷指定 = false;
            byt出荷 = 1;
            dte出荷完了日1 = DateTime.MinValue;
            dte出荷完了日2 = DateTime.MinValue;
            ble受注完了承認指定 = false;
            byt受注完了承認 = 1;
            byt無効日 = 2;
            ble履歴表示 = 履歴トグル.Checked;
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

            myapi.GetFullScreen(out xSize, out ySize);

            int x = 10, y = 10;

            this.Size = new Size(this.Width, ySize * myapi.GetTwipPerDot(intpixel) - 1200);
            //accessのmovesizeメソッドの引数の座標単位はtwipなので以下で

            this.Size = new Size(this.Width, ySize - 1200 / twipperdot);

            this.StartPosition = FormStartPosition.Manual; // 手動で位置を指定
            int screenWidth = Screen.PrimaryScreen.Bounds.Width; // プライマリスクリーンの幅
            x = (screenWidth - this.Width) / 2;
            this.Location = new Point(x, y);

            ////setall();
            InitializeFilter();
            this.ble受注完了承認指定 = true;
            this.byt受注完了承認 = 2;
            DoUpdate();
            Cleargrid(dataGridView1);
            fn.WaitForm.Close();
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            try
            {
                try
                {
                    dataGridView1.Height += this.Height - intWindowHeight;
                    intWindowHeight = this.Height; // Save the new height

                    dataGridView1.Width += this.Width - intWindowWidth;
                    intWindowWidth = this.Width; // Save the new width
                }
                catch (Exception ex)
                {
                    Debug.Print($"{Name}_Form_Resize - {ex.HResult} : {ex.Message}");
                }
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
                result = Filtering();

            }
            catch (Exception ex)
            {
                result = -1;
                MessageBox.Show(this.Name + "_DoUpdate - " + ex.Message);
            }

            return result;
        }

        //private void Filtering()
        private int Filtering()
        {
            try
            {
                DateTime dtePrevious;//前回出勤日
                string strWhere = string.Empty;

                if (!string.IsNullOrEmpty(str受注コード1))
                {
                    strWhere = FunctionClass.WhereString(strWhere,
                        "'" + str受注コード1 + "' <= 受注コード and 受注コード <= '" + str受注コード2 + "'");
                }

                if (dte受注日1 != DateTime.MinValue)
                {
                    strWhere = FunctionClass.WhereString(strWhere, "'" + dte受注日1 + "' <= 受注日 and 受注日 <= '" + dte受注日2 + "'");
                }

                if (dte出荷予定日1 != DateTime.MinValue)
                {
                    strWhere = FunctionClass.WhereString(strWhere,
                        "'" + dte出荷予定日1 + "' <= 出荷予定日 and 出荷予定日 <= '" + dte出荷予定日2 + "'");
                }

                if (dte受注納期1 != DateTime.MinValue)
                {
                    strWhere = FunctionClass.WhereString(strWhere,
                        "'" + dte受注納期1 + "' <= 受注納期 AND 受注納期 <= '" + dte受注納期2 + "'");
                }

                if (!string.IsNullOrEmpty(str注文番号))
                {
                    strWhere = FunctionClass.WhereString(strWhere, "注文番号 like '%" + str注文番号 + "%'");
                }

                if (!string.IsNullOrEmpty(str顧客コード))
                {
                    strWhere = FunctionClass.WhereString(strWhere, "顧客コード = '" + str顧客コード + "'");
                }

                if (!string.IsNullOrEmpty(str自社担当者コード))
                {
                    strWhere = FunctionClass.WhereString(strWhere, "自社担当者コード = '" + str自社担当者コード + "'");
                }

                if (ble受注承認指定)
                {
                    switch (byt受注承認)
                    {
                        case 1:
                            strWhere = FunctionClass.WhereString(strWhere, "承認者コード is not null");
                            break;
                        case 2:
                            strWhere = FunctionClass.WhereString(strWhere, "承認者コード is null");
                            break;
                    }
                }

                if (ble出荷指定)
                {
                    switch (byt出荷)
                    {
                        case 1:
                            if (Convert.ToDouble(dte出荷完了日1) == 0)
                            {
                                strWhere = FunctionClass.WhereString(strWhere, "出荷完了日 is not null");
                            }
                            else
                            {
                                strWhere = FunctionClass.WhereString(strWhere, "'" + dte出荷完了日1 + "'<=出荷完了日 and 出荷完了日<='" + dte出荷完了日2 + "'");
                            }
                            break;
                        case 2:
                            strWhere = FunctionClass.WhereString(strWhere, "出荷完了日 is null");
                            break;
                    }
                }

                if (ble受注完了承認指定)
                {
                    switch (byt受注完了承認)
                    {
                        case 1:
                            strWhere = FunctionClass.WhereString(strWhere, "完了承認者コード is not null");
                            break;
                        case 2:
                            strWhere = FunctionClass.WhereString(strWhere, "完了承認者コード is null");
                            break;
                    }
                }

                if (!this.ble履歴表示)
                {
                    switch (byt無効日)
                    {
                        case 1:
                            strWhere = FunctionClass.WhereString(strWhere, "無効日 is not null");
                            break;
                        case 2:
                            strWhere = FunctionClass.WhereString(strWhere, "無効日 is null");
                            break;
                    }
                }

                //改版依頼中の受注データは表示しない
                strWhere = FunctionClass.WhereString(strWhere, "not(1<受注版数 and 承認者コード is null)");

                string query1 = "SELECT 受注コード, 受注版数 AS 版, 受注日, 出荷予定日, 受注納期, 注文番号, 顧客名, 自社担当者名, FORMAT(受注金額, N'#,0') AS 受注金額 " +
                                           " , CASE WHEN 確定日時 IS NOT NULL THEN '■' ELSE '' END AS 確定 " +
                                           " ,  承認者コード  AS 承認 " +
                                           " , CASE WHEN 出荷完了日 IS NOT NULL THEN '■' ELSE '' END AS 出荷 " +
                                           " , CASE WHEN 完了承認者コード IS NOT NULL THEN '■' ELSE '' END AS 完了  " +
                                           ", 無効日 ";

                Connect();
                string sql = "";

                if (this.ble履歴表示)
                {
                    query1 += " FROM SalesOrderList_History ";

                    //有効件数が表示件数になっている　おかしいのでは？？　表示件数とは異なる
                    this.有効件数.Text = GetRecordCount(cn, "SalesOrderList_History",
                        $" {strWhere} and 無効日 is null and 承認者コード is not null").ToString();
                    if (this.有効件数.Text == "0")
                    {
                        sql = $"SELECT SUM(受注数量) AS num FROM SalesOrderList_History where {strWhere} and 無効日 is null and 承認者コード is not null";
                        this.合計数量.Text = GetScalar<int>(cn, sql).ToString("#,0");

                        sql = $"SELECT SUM(受注金額) AS num FROM SalesOrderList_History where {strWhere} and 無効日 is null and 承認者コード is not null";
                        this.合計金額.Text = GetScalar<int>(cn, sql).ToString("#,0");

                        sql = $"SELECT SUM(税込受注金額) AS num FROM SalesOrderList_History where {strWhere} and 無効日 is null and 承認者コード is not null";
                        this.税込合計金額.Text = GetScalar<int>(cn, sql).ToString("#,0");

                    }
                }
                else
                {
                    query1 += " FROM SalesOrderList ";

                    this.有効件数.Text = GetRecordCount(cn, "SalesOrderList",
                        $" {strWhere} and 無効日 is null and 承認者コード is not null").ToString();
                    if (this.有効件数.Text != "0")
                    {
                        sql = $"SELECT SUM(受注数量) AS num FROM SalesOrderList where {strWhere} and 無効日 is null and 承認者コード is not null";
                        this.合計数量.Text = GetScalar<int>(cn, sql).ToString("#,0");

                        sql = $"SELECT SUM(受注金額) AS num FROM SalesOrderList where {strWhere} and 無効日 is null and 承認者コード is not null";
                        this.合計金額.Text = GetScalar<int>(cn, sql).ToString("#,0");

                        sql = $"SELECT SUM(税込受注金額) AS num FROM SalesOrderList where {strWhere} and 無効日 is null and 承認者コード is not null";
                        this.税込合計金額.Text = GetScalar<int>(cn, sql).ToString("#,0");

                    }
                    else
                    {
                        this.有効件数.Text = "0";
                        this.合計数量.Text = "0";
                        this.合計金額.Text = "0";
                        this.税込合計金額.Text = "0";
                    }
                }

                string query2 = "";
                // SQL文の構築
                if (string.IsNullOrEmpty(strWhere))
                {
                    query2 = query1 + " ORDER BY 受注コード DESC";
                }
                else
                {
                    query2 = query1 + $" WHERE {strWhere} ORDER BY 受注コード DESC";
                }

                DataGridUtils.SetDataGridView(cn, query2, this.dataGridView1);

                MyApi myapi = new MyApi();
                int xSize, ySize, intpixel, twipperdot;

                // 1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
                intpixel = myapi.GetLogPixel();
                twipperdot = myapi.GetTwipPerDot(intpixel);

                intWindowHeight = this.Height;
                intWindowWidth = this.Width;

                // DataGridViewの設定
                dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200); // 薄い黄色
                dataGridView1.Columns[1].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200); // 薄い黄色

                dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                dataGridView1.ColumnHeadersHeight = 25;

                // 0列目はaccessでは行ヘッダのため、ずらす
                dataGridView1.Columns[0].Width = 1600 / twipperdot;
                dataGridView1.Columns[1].Width = 400 / twipperdot;
                dataGridView1.Columns[2].Width = 1600 / twipperdot;
                dataGridView1.Columns[3].Width = 1600 / twipperdot;
                dataGridView1.Columns[4].Width = 1600 / twipperdot;
                dataGridView1.Columns[5].Width = 3000 / twipperdot;
                dataGridView1.Columns[6].Width = 4500 / twipperdot;
                dataGridView1.Columns[7].Width = 1400 / twipperdot;
                dataGridView1.Columns[8].Width = 1400 / twipperdot;
                dataGridView1.Columns[9].Width = 400 / twipperdot;　　//確定
                dataGridView1.Columns[10].Width = 400 / twipperdot;　　//承認
                dataGridView1.Columns[11].Width = 400 / twipperdot;    //出荷
                dataGridView1.Columns[12].Width = 400 / twipperdot;　　//完了
                dataGridView1.Columns[13].Visible = false; //無効日
                //dataGridView1.Columns[14].Width = 0 / twipperdot;　//承認者コード　色変更のため追加

                //    Font fontSO = new Font(this.dataGridView1.DefaultCellStyle.Font.FontFamily
                //, this.dataGridView1.DefaultCellStyle.Font.Size , FontStyle.Strikeout);

                //    // 赤色のペンを作成
                //    Pen redPen = new Pen(Color.Red, 2);

                //    // dataGridView1 の特定の行に対してスタイルを適用
                //    foreach (DataGridViewRow row in dataGridView1.Rows)
                //    {
                //        if (row.Index == YOUR_SPECIFIC_ROW_INDEX) // YOUR_SPECIFIC_ROW_INDEX は特定の行のインデックスに置き換えてください
                //        {
                //            foreach (DataGridViewCell cell in row.Cells)
                //            {
                //                cell.Style.Font = fontSO; // 打ち消し線のフォントを適用
                //                //cell.Style.ForeColor = Color.Red; // テキストの色を赤色に設定
                //                cell.Style.SelectionForeColor = Color.Red; // 選択時のテキストの色も赤色に設定
                //            }
                //        }
                //    }                                
                //this.dataGridView1.Rows[0].Cells[0].Style.Font = fontSO;



                if (strWhere == "")
                {
                    初期表示ボタン.ForeColor = Color.FromArgb(255, 0, 0);
                    本日受注分ボタン.ForeColor = Color.FromArgb(0, 0, 0);
                    前日受注分ボタン.ForeColor = Color.FromArgb(0, 0, 0);
                }
                else
                {
                    初期表示ボタン.ForeColor = Color.FromArgb(0, 0, 0);
                    dtePrevious = DateTime.Now.AddDays(-1);
                    FunctionClass func = new FunctionClass();
                    do
                    {
                        dtePrevious = dtePrevious.AddDays(-1);
                    } while (func.OfficeClosed(cn, dtePrevious) == 1);

                    if (dte受注日1.Date == DateTime.Now.Date && dte受注日2.Date == DateTime.Now.Date)
                    {
                        本日受注分ボタン.ForeColor = Color.FromArgb(255, 0, 0);
                        前日受注分ボタン.ForeColor = Color.FromArgb(0, 0, 0);
                    }
                    else if (dte受注日1.Date == dtePrevious.Date && dte受注日2.Date == dtePrevious.Date)
                    {
                        本日受注分ボタン.ForeColor = Color.FromArgb(0, 0, 0);
                        前日受注分ボタン.ForeColor = Color.FromArgb(255, 0, 0);
                    }
                    else
                    {
                        本日受注分ボタン.ForeColor = Color.FromArgb(0, 0, 0);
                        前日受注分ボタン.ForeColor = Color.FromArgb(0, 0, 0);
                    }
                }

                //なぜか表示件数は有効件数なので。。。
                // this.有効件数.Text = dataGridView1.RowCount.ToString();


                return dataGridView1.RowCount;
                //return 1;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Filtering - " + ex.Message);
                return -1;
            }
        }


        private void dataGridView1_RowPostPaint(object sender, DataGridViewRowPostPaintEventArgs e)
        {
            //無効日がnullの行に線を引く
            if (dataGridView1.Rows[e.RowIndex].Cells[13].Value != null &&
        !string.IsNullOrEmpty(dataGridView1.Rows[e.RowIndex].Cells[13].Value.ToString()))
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
            if (e.ColumnIndex == 10 && e.Value != null)
            {
                if (int.TryParse(e.Value.ToString(), out value))
                {
                    if (value == 1)
                    {
                        //dataGridView1.Rows[e.RowIndex].Cells[10].Style.ForeColor = Color.Red; // 赤
                        e.CellStyle.ForeColor = Color.Red;
                    }
                    else if (value == 0)
                    {
                        e.CellStyle.ForeColor = Color.Purple; // 紫
                    }
                    else
                    {
                        e.CellStyle.ForeColor = Color.Black; // 黒
                    }                    
                }
                e.Value = "■"; // 値を"■"に設定
            }
            else if (e.ColumnIndex == 9 || e.ColumnIndex == 11 || e.ColumnIndex == 12)
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
                string selectedData = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(); // 1列目のデータを取得

                if (!string.IsNullOrEmpty(dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString()))
                {
                    selectedData += "," + dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                }
                F_受注 targetform = new F_受注();

                targetform.varOpenArgs = selectedData;
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

        private void コマンド抽出_Click(object sender, EventArgs e)
        {
            try
            {

                this.dataGridView1.Focus(); // サブフォームにフォーカスを設定
                // 受注管理_抽出フォームを開く
                F_受注管理_抽出 form = new F_受注管理_抽出();
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"フォームの抽出中にエラーが発生しました。\n\n{ex.GetType().Name} : {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void コマンド出力_Click(object sender, EventArgs e)
        {
            
            this.dataGridView1.Focus();
            F_受注管理出力 form = new F_受注管理出力();
            form.ShowDialog();
        }

        private void コマンド初期化_Click(object sender, EventArgs e)
        {
            try
            {
                this.dataGridView1.Focus();
                InitializeFilter();
                ble受注完了承認指定 = true;
                byt受注完了承認 = 2;
                DoUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"初期化中にエラーが発生しました。\n\n{ex.GetType().Name} : {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void コマンド全表示_Click(object sender, EventArgs e)
        {

            this.dataGridView1.Focus();
            InitializeFilter();
            DoUpdate();
        }

        private void コマンド更新_Click(object sender, EventArgs e)
        {

            this.dataGridView1.Focus();
            DoUpdate();
        }

        private void コマンド検索_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Focus();
            F_検索コード form = new F_検索コード(this, "A");
            form.ShowDialog();
        }



        private void FunctionKeyDown(object sender, KeyEventArgs e)
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
                        if (コマンド全表示.Enabled)
                        {
                            コマンド全表示.Focus();
                            コマンド全表示_Click(sender, e);
                        }
                        break;
                    case Keys.F5:
                        if (コマンド受注.Enabled)
                        {
                            コマンド受注.Focus();
                            コマンド受注_Click(sender, e);
                        }
                        break;
                    case Keys.F6:
                        if (コマンド顧客.Enabled)
                        {
                            コマンド顧客.Focus();
                            コマンド顧客_Click(sender, e);
                        }
                        break;
                    case Keys.F7:
                        break;
                    case Keys.F8:
                        break;
                    case Keys.F9:
                        break;
                    case Keys.F10:
                        if (コマンド出力.Enabled)
                        {
                            コマンド出力.Focus();
                            コマンド出力_Click(sender, e);
                        }
                        break;
                    case Keys.F11:
                        if (コマンド更新.Enabled)
                        {
                            コマンド更新.Focus();
                            コマンド更新_Click(sender, e);
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
                        if (ActiveControl == this.dataGridView1)
                        {
                            F_受注 form = new F_受注();
                            form.varOpenArgs = $"{CurrentCode},{CurrentEdition}";
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
        private void Form_Unload(object sender, FormClosedEventArgs e)
        {
            LocalSetting ls = new LocalSetting();
            // ウィンドウの配置情報を保存する
            ls.SavePlace(CommonConstants.LoginUserCode, this);

        }


        private void コマンド受注_Click(object sender, EventArgs e)
        {
            this.dataGridView1.Focus();
            F_受注 form = new F_受注();
            form.varOpenArgs = $"{CurrentCode},{CurrentEdition}";
            form.ShowDialog();
        }

        private void コマンド顧客_Click(object sender, EventArgs e)
        {
            string param;
            param = $" -sv:{ServerInstanceName} -open:customer";
            GetShell(param);
        }

        private void フォームヘッダー_DblClick(object sender, EventArgs e)
        {
            try
            {
                //if (frmSub != null)
                //{
                //    frmSub.Focus(); // サブフォームにフォーカスを設定
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show($"フォームヘッダーのダブルクリック中にエラーが発生しました。\n\n{ex.GetType().Name} : {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void フォームヘッダー_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                //if (frmSub != null)
                //{
                //    frmSub.Focus(); // サブフォームにフォーカスを設定
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show($"フォームヘッダーのマウスダウン中にエラーが発生しました。\n\n{ex.GetType().Name} : {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                    ble受注完了承認指定 = true;
                    byt受注完了承認 = 2;
                    Filtering();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"初期表示ボタンのクリック中にエラーが発生しました。\n\n{ex.GetType().Name} : {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 前ページボタン_Click(object sender, EventArgs e)
        {
            try
            {
                //if (frmSub != null)
                //{
                //    frmSub.Focus(); // サブフォームにフォーカスを設定

                //    // Page Up キーを送信
                //    keybd_event((byte)VK_PRIOR, 0, 0, UIntPtr.Zero);
                //    keybd_event((byte)VK_PRIOR, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
                //}
                dataGridView1.Focus();
                SendKeys.SendWait("{PGUP}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"前ページボタンのクリック中にエラーが発生しました。\n\n{ex.GetType().Name} : {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 次ページボタン_Click(object sender, EventArgs e)
        {
            try
            {
                //if (frmSub != null)
                //{
                //    frmSub.Focus(); // サブフォームにフォーカスを設定

                //    // Page Down キーを送信
                //    keybd_event((byte)VK_NEXT, 0, 0, UIntPtr.Zero);
                //    keybd_event((byte)VK_NEXT, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
                //}

                dataGridView1.Focus();
                SendKeys.SendWait("{PGDN}");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"次ページボタンのクリック中にエラーが発生しました。\n\n{ex.GetType().Name} : {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 前日受注分ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                //if (frmSub != null)
                //{
                //    frmSub.Focus(); // サブフォームにフォーカスを設定

                //    Connect();
                //    // 初期化処理
                //    InitializeFilter();

                //    // 前日の日付を取得
                //    DateTime dtePrevious = DateTime.Today.AddDays(-1);

                //    // 営業日が見つかるまで前日の日付を更新
                //    while (FunctionClass.OfficeClosed(cn, dtePrevious))
                //    {
                //        dtePrevious = dtePrevious.AddDays(-1);
                //    }

                //    // 受注日を更新
                //    this.dte受注日1 = dtePrevious;
                //    this.dte受注日2 = dtePrevious;

                //    // フィルタリング処理
                //    Filtering();
                //}

                // サブフォームにフォーカスを設定
                this.dataGridView1.Focus();

                Connect();
                // 初期化処理
                InitializeFilter();

                // 前日の日付を取得
                DateTime dtePrevious = DateTime.Today.AddDays(-1);

                // 営業日が見つかるまで前日の日付を更新
                FunctionClass func = new FunctionClass();
                while (func.OfficeClosed(cn, dtePrevious) == 1)
                {
                    dtePrevious = dtePrevious.AddDays(-1);
                }

                // 受注日を更新
                this.dte受注日1 = dtePrevious;
                this.dte受注日2 = dtePrevious;

                // フィルタリング処理
                DoUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"前日受注分ボタンのクリック中にエラーが発生しました。\n\n{ex.GetType().Name} : {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 本日受注分ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                //if (frmSub != null)
                //{
                //    frmSub.Focus(); // サブフォームにフォーカスを設定

                //    // 初期化処理
                //    InitializeFilter();

                //    // 本日の日付を設定
                //    this.dte受注日1 = DateTime.Today;
                //    this.dte受注日2 = DateTime.Today;

                //    // フィルタリング処理
                //    Filtering();
                //}
                this.dataGridView1.Focus();

                // 初期化処理
                InitializeFilter();

                // 本日の日付を設定
                this.dte受注日1 = DateTime.Today;
                this.dte受注日2 = DateTime.Today;

                // フィルタリング処理
                DoUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"本日受注分ボタンのクリック中にエラーが発生しました。\n\n{ex.GetType().Name} : {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 履歴トグル_Validated(object sender, EventArgs e)
        {
            try
            {
                //if (frmSub != null)
                //{
                //    frmSub.Focus(); // サブフォームにフォーカスを設定

                //    // 履歴表示を更新
                //    this.ble履歴表示 = this.履歴トグル.Text;

                //    // フィルタリング処理
                //    Filtering();
                //}

                this.dataGridView1.Focus();
                // 履歴表示を更新
                //this.ble履歴表示 = this.履歴トグル.Text;

                // フィルタリング処理
                DoUpdate();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"履歴トグルの更新後にエラーが発生しました。\n\n{ex.GetType().Name} : {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 履歴トグル_CheckedChanged(object sender, EventArgs e)
        {
            dataGridView1.Focus();
            ble履歴表示 = 履歴トグル.Checked;
            Filtering();
        }

        private void F_受注管理_KeyDown(object sender, KeyEventArgs e)
        {
            FunctionKeyDown(sender, e);
        }

        
    }
}