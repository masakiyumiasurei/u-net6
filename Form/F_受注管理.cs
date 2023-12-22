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

namespace u_net
{
    public partial class F_受注管理 : MidForm
    {
        const string strSecondOrder = "受注コード";

        public F_受注管理サブ frmSub;
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
            //ble履歴表示 = bool.Parse(履歴トグル.Text);
            ble履歴表示 = false;
        }

        public void SetRecordSource(Form formObject, string whereString)
        {
            try
            {
                if (this.ble履歴表示)
                {
                    if (string.IsNullOrEmpty(whereString))
                    {
                        //formObject.RecordSource = "SELECT * FROM SalesOrderList_History" + (string.IsNullOrEmpty(formObject.OrderBy) ? "" : " ORDER BY " + formObject.OrderBy);
                    }
                    else
                    {
                        //formObject.RecordSource = "SELECT * FROM SalesOrderList_History WHERE " + whereString + (string.IsNullOrEmpty(formObject.OrderBy) ? "" : " ORDER BY " + formObject.OrderBy);
                    }
                }
                else
                {
                    if (string.IsNullOrEmpty(whereString))
                    {
                        //formObject.RecordSource = "SELECT * FROM SalesOrderList" + (string.IsNullOrEmpty(formObject.OrderBy) ? "" : " ORDER BY " + formObject.OrderBy);
                    }
                    else
                    {
                        //formObject.RecordSource = "SELECT * FROM SalesOrderList WHERE " + whereString + (string.IsNullOrEmpty(formObject.OrderBy) ? "" : " ORDER BY " + formObject.OrderBy);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SetRecordSource - {ex.GetType().Name} : {ex.Message}");
            }
        }


        private void Form_Load(object sender, EventArgs e)
        {
            ////LocalSetting ls = new LocalSetting();
            //////ウィンドウサイズを調整する
            ////ls.LoadPlace(CommonConstants.LoginUserCode, this);

            ////F_受注管理 frmTarget = Application.OpenForms.OfType<F_受注管理>().FirstOrDefault();

            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");
            //実行中フォーム起動
            //string LoginUserCode = "000";//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
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
                if (!resizing)
                {
                    resizing = true;

                    this.SuspendLayout();

                    //this.サブ.Height = this.サブ.Height + (this.Height - intWindowHeight);

                    this.ResumeLayout();

                    intWindowHeight = this.Height; // 高さ保存
                    intWindowWidth = this.Width;   // 幅保存

                    resizing = false;
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
                //   DrawGrid();
                if (result >= 0)
                {
                    //this.表示件数.Text = result.ToString();
                    this.有効件数.Text = result.ToString();
                }
                else
                {
                    //this.表示件数.Text = null; // Nullの代わりにC#ではnullを使用
                    this.有効件数.Text = null; // Nullの代わりにC#ではnullを使用
                }
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
                    strWhere += "'" + str受注コード1 + "' <= 受注コード and 受注コード <= '" + str受注コード2 + "'";
                }

                if (dte受注日1 != DateTime.MinValue)
                {
                    strWhere += "'" + dte受注日1 + "' <= 受注日 and 受注日 <= '" + dte受注日2 + "'";
                }

                if (dte出荷予定日1 != DateTime.MinValue)
                {
                    strWhere += "'" + dte出荷予定日1 + "' <= 出荷予定日 and 出荷予定日 <= '" + dte出荷予定日2 + "'";
                }

                if (dte受注納期1 != DateTime.MinValue)
                {
                    strWhere += "'" + dte受注納期1 + "' <= 受注納期 AND 受注納期 <= '" + dte受注納期2 + "'";
                }

                if (!string.IsNullOrEmpty(str注文番号))
                {
                    strWhere += "注文番号 like '%" + str注文番号 + "%'";
                }

                if (!string.IsNullOrEmpty(str顧客コード))
                {
                    strWhere += "顧客コード = '" + str顧客コード + "'";
                }

                if (!string.IsNullOrEmpty(str自社担当者コード))
                {
                    strWhere += "自社担当者コード = '" + str自社担当者コード + "'";
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

                //frmSub.ServerFilter = strWhere;

                if (ble履歴表示)
                {
                    //frmSub.RecordSource = "SalesOrderList_History";
                    //this.有効件数 = FunctionClass.GetRecordCount(cn, frmSub.RecordSource, frmSub.ServerFilter + " and 無効日 is null and 承認者コード is not null");
                    //this.合計数量 = (this.有効件数 == 0) ? 0 :DSum("受注数量", this.RecordSource, this.ServerFilter + " and 無効日 is null and 承認者コード is not null");
                    //this.合計金額 = (this.有効件数 == 0) ? 0 :DSum("受注金額", this.RecordSource, this.ServerFilter + " and 無効日 is null and 承認者コード is not null");
                    //this.税込合計金額 = (this.有効件数 == 0) ? 0 :DSum("税込受注金額", this.RecordSource, this.ServerFilter + " and 無効日 is null and 承認者コード is not null");
                }
                else
                {
                    //frmSub.RecordSource = "SalesOrderList";
                    //this.有効件数 = FunctionClass.GetRecordCount(cn, frmSub.RecordSource, frmSub.ServerFilter + " and 無効日 is null and 承認者コード is not null");
                    //this.合計数量 = (this.有効件数 == 0) ? 0 :DSum("受注数量", this.RecordSource, this.ServerFilter + " and 無効日 is null and 承認者コード is not null");
                    //this.合計金額 = (this.有効件数 == 0) ? 0 :DSum("受注金額", this.RecordSource, this.ServerFilter + " and 無効日 is null and 承認者コード is not null");
                    //this.税込合計金額 = (this.有効件数 == 0) ? 0 :DSum("税込受注金額", this.RecordSource, this.ServerFilter + " and 無効日 is null and 承認者コード is not null");
                }

                //frmSub.OrderBy = frmSub.OrderBy;
                //frmSub.OrderByOn = true;

                string query1 = "SELECT 受注コード, 受注版数 AS 版, 受注日, 出荷予定日, 受注納期, 注文番号, 顧客名, 自社担当者名, 受注金額 " + 
                                           " , CASE WHEN 確定日時 IS NOT NULL THEN '■' ELSE '' END AS 確定 " + 
                                           " , CASE WHEN 承認者コード IS NOT NULL THEN '■' ELSE '' END AS 承認 " + 
                                           " , CASE WHEN 出荷完了日 IS NOT NULL THEN '■' ELSE '' END AS 出荷 " + 
                                           " , CASE WHEN 完了承認者コード IS NOT NULL THEN '■' ELSE '' END AS 完了 " + 
                                           " FROM V受注管理 ";
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

                Connect();
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
                dataGridView1.Columns[0].Width = 1800 / twipperdot;
                dataGridView1.Columns[1].Width = 400 / twipperdot;
                dataGridView1.Columns[2].Width = 2000 / twipperdot;
                dataGridView1.Columns[3].Width = 2000 / twipperdot;
                dataGridView1.Columns[4].Width = 2000 / twipperdot;
                dataGridView1.Columns[5].Width = 1500 / twipperdot;
                dataGridView1.Columns[6].Width = 1500 / twipperdot;
                dataGridView1.Columns[7].Width = 2200 / twipperdot;
                dataGridView1.Columns[8].Width = 1500 / twipperdot;
                dataGridView1.Columns[9].Width = 400 / twipperdot;
                dataGridView1.Columns[10].Width = 400 / twipperdot;
                dataGridView1.Columns[11].Width = 400 / twipperdot;
                dataGridView1.Columns[12].Width = 400 / twipperdot;

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
                return dataGridView1.RowCount;
                //return 1;
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
            this.Close();
        }

        private void コマンド抽出_Click(object sender, EventArgs e)
        {
            try
            {
                //if (frmSub != null)
                //{
                //    frmSub.Focus(); // サブフォームにフォーカスを設定

                //    // 受注管理_抽出フォームを開く
                //    F_受注管理_抽出 form = new F_受注管理_抽出();
                //    form.ShowDialog();
                //}
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
            //if (frmSub != null)
            //{
            //    frmSub.Focus(); // サブフォームにフォーカスを設定

            //    // 受注管理出力フォームを開く
            //    F_受注管理出力 form = new F_受注管理出力();
            //    form.ShowDialog();
            //}
            this.dataGridView1.Focus();
            F_受注管理出力 form = new F_受注管理出力();
            form.ShowDialog();
        }

        private void コマンド初期化_Click(object sender, EventArgs e)
        {
            try
            {
                //if (frmSub != null)
                //{
                //    frmSub.Focus(); // サブフォームにフォーカスを設定
                //    // 初期化処理
                //    InitializeFilter();
                //    ble受注完了承認指定 = true;
                //    byt受注完了承認 = 2;
                //    Filtering();
                //}

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
            //if (frmSub != null)
            //{
            //    frmSub.Focus(); // サブフォームにフォーカスを設定

            //    // 全表示処理
            //    InitializeFilter();
            //    Filtering();
            //}
            this.dataGridView1.Focus();
            InitializeFilter();
            DoUpdate();
        }

        private void コマンド更新_Click(object sender, EventArgs e)
        {
            //if (frmSub != null)
            //{
            //    frmSub.Focus(); // サブフォームにフォーカスを設定
            //    frmSub.Requery(); // サブフォームを再クエリ
            //}
            this.dataGridView1.Focus();
            DoUpdate();
        }

        private void コマンド検索_Click(object sender, EventArgs e)
        {
            //if (frmSub != null)
            //{
            //    frmSub.Focus(); // サブフォームにフォーカスを設定

            //    // 別のフォームを開く
            //    F_検索コード form = new F_検索コード(this, "");
            //    form.ShowDialog();
            //}
            this.dataGridView1.Focus();
            F_検索コード form = new F_検索コード(this, "");
            form.ShowDialog();
        }

        private void Form_KeyDown(int KeyCode, int Shift)
        {
            //FunctionKeyDown(KeyCode, Shift);
        }

        private void Form_Unload(object sender, FormClosedEventArgs e)
        {
            LocalSetting ls = new LocalSetting();
            // ウィンドウの配置情報を保存する
            ls.SavePlace(CommonConstants.LoginUserCode, this);

            //if (frmSub != null)
            //{
            //    frmSub.Visible = false;
            //}
        }

        private void Form_Open(int Cancel)
        {
            try
            {
                //API.SetFormIcon(this.Handle, Application.StartupPath + "\\list.ico");

                //frmSub = this.サブ.Form; // YourSubFormに適切な型が必要です

                intWindowHeight = this.Height;
                intWindowWidth = this.Width;

                // 初期設定
                InitializeFilter();
                this.ble受注完了承認指定 = true;
                this.byt受注完了承認 = 2;

                //// 初期ソート設定
                //if (frmSub != null)
                //{
                //    frmSub.OrderBy = "受注コード DESC";
                //    frmSub.CurrentOrder = frmSub.Controls["受注コードボタン"];
                //    frmSub.CurrentOrder.ForeColor = System.Drawing.Color.FromArgb(0, 0, 255);
                //}

                ////Filtering;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"初期化に失敗しました。\n{this.Name}を開くことができません。\n\n{ex.GetType().Name} : {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void コマンド受注_Click(object sender, EventArgs e)
        {
            //if (frmSub != null)
            //{
            //    frmSub.Focus(); // サブフォームにフォーカスを設定

            //    // 受注フォームを開く
            //    F_受注 form = new F_受注();
            //    form.ShowDialog();
            //}
            this.dataGridView1.Focus();
            F_受注 form = new F_受注();
            form.ShowDialog();

        }

        private void コマンド顧客_Click(object sender, EventArgs e)
        {
            //if (frmSub != null)
            //{
            //    frmSub.Focus(); // サブフォームにフォーカスを設定

            //    // 顧客コードを取得し、Nz関数でNullの場合にデフォルトの値を設定
            //    string customerCode = frmSub != null ? frmSub.顧客コード ?? "デフォルト値" : "デフォルト値";

            //    // 顧客フォームを開く
            //    F_顧客 form = new F_顧客();
            //    form.ShowDialog();
            //}
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"次ページボタンのクリック中にエラーが発生しました。\n\n{ex.GetType().Name} : {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 初期表示ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                //if (frmSub != null)
                //{
                //    frmSub.Focus(); // サブフォームにフォーカスを設定

                //    // 初期表示処理
                //    InitializeFilter();
                //    ble受注完了承認指定 = true;
                //    byt受注完了承認 = 2;
                //    Filtering();
                //}
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
            }
            catch (Exception ex)
            {
                MessageBox.Show($"前ページボタンのクリック中にエラーが発生しました。\n\n{ex.GetType().Name} : {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
    }
}