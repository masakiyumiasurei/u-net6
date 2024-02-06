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
using static u_net.Public.FunctionClass;
using static u_net.Public.OriginalClass;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace u_net
{
    public partial class F_入金管理 : MidForm
    {
        public string str入金コード開始;
        public string str入金コード終了;
        public DateTime dtm入金日開始;
        public DateTime dtm入金日終了;
        public string str顧客コード;
        public string str顧客名;
        public string str入金金額開始;
        public string str入金金額終了;
        public int lng請求指定;
        public int lng削除指定;
        public string strSearchCode;
        public bool monEXT;

        private const string FORM_CAPTION = "入金管理";
        private DataGridView gridobject;
        private int intOperationMode; // 操作モード（0:通常、1:領収書選択）
        private int intSelectionMode; // グリッドの選択モード     
        private int intSortSettings; // FlexGridのソート状態（昇順:1,降順:-1）     
        private int intWindowHeightMax;
        private int intWindowWidthMax;
        private int intKeyCode; // 保存キーコード
        private int intButton; // 保存マウスボタン
        public object objParent;

        int intWindowHeight = 0;
        int intWindowWidth = 0;

        private Control? previousControl;
        private SqlConnection? cn;

        public F_入金管理()
        {
            InitializeComponent();
        }

        public int DataCount
        {
            get
            {
                int dataCount = 一覧.Rows.Count;
                return dataCount < 0 ? 0 : dataCount;
            }
        }

        public string customerCode
        {
            // 現在選択されているデータのコードを取得する
            get
            {
                return 一覧.SelectedRows[0].Cells[2].Value?.ToString();
            }
        }

        public string CurrentCode
        {
            // 現在選択されているデータのコードを取得する
            get
            {
                return 一覧.SelectedRows[0].Cells[0].Value?.ToString();
            }
        }

        public bool IsDeleted
        {
            // 現在選択されているデータが削除されているかどうかを取得する
            get
            {
                return !string.IsNullOrEmpty(Nz(一覧.Rows[一覧.CurrentRow.Index].Cells[11].Value?.ToString(), ""));
            }
        }

        private int OperationMode
        {
            get
            {
                return intOperationMode;
            }

            set
            {
                switch (value)
                {
                    case 0:
                        this.Text = FORM_CAPTION;
                        break;
                    case 1:
                        this.Text = FORM_CAPTION + " - 領収書発行モード";
                        break;
                    // 他のケースがあれば必要に応じて追加
                    default:
                        throw new ArgumentOutOfRangeException(nameof(value), "無効な操作モードです。");
                }

                intOperationMode = value;
            }
        }

        private void ChangeOperationMode()
        {
            FunctionClass fn = new FunctionClass();
            fn.DoWait("更新しています...");
            DoUpdate();
            Cleargrid(一覧);
            fn.WaitForm.Close();

            //操作モードを更新する
            OperationMode = (OperationMode + 1) % 2;
        }

        //領収済みを解除する
        //codes - 入金コード一覧（デリミタ付）
        //戻り値 - 生成した領収データの領収コード
        private bool DoReceiptCancel(string codes)
        {
            bool result = false;

            Connect();

            using (SqlTransaction transaction = cn.BeginTransaction())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("ReceiptCancel", cn, transaction))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Codes", codes); // パラメータ名は適切なものに置き換える

                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    result = true;
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("DoReceiptCancel - ADO.NET SQL Error! Err.No: {0} SQL State: {1}", ex.Number, ex.State);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("DoReceiptCancel - General Error: {0}", ex.Message);
                }
            }
            return result;
        }

        //領収処理を実行する
        //codes - 入金コード一覧（デリミタ付）
        //戻り値 - 生成した領収データの領収コード
        private string DoReceiptProcess(string codes)
        {
            string resultValue = "";

            Connect();

            using (SqlTransaction transaction = cn.BeginTransaction())
            {
                try
                {
                    using (SqlCommand command = new SqlCommand("ReceiptProcess", cn, transaction))
                    {
                        command.CommandType = CommandType.StoredProcedure;
                        command.Parameters.AddWithValue("@Codes", codes); // パラメータ名は適切なものに置き換える

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                reader.Read();
                                resultValue = reader[0].ToString();
                            }
                        }
                    }

                    transaction.Commit();
                }
                catch (SqlException ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("DoReceiptProcess - ADO.NET SQL Error! Err.No: {0} SQL State: {1}", ex.Number, ex.State);
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    Console.WriteLine("DoReceiptProcess - General Error: {0}", ex.Message);
                }
            }

            return resultValue;
        }

        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        //抽出条件を初期化する
        private void InitializeFilter()
        {
            str入金コード開始 = "";
            str入金コード終了 = "";
            dtm入金日開始 = DateTime.MinValue; // 適切な初期値に変更する必要があります
            dtm入金日終了 = DateTime.MinValue; // 適切な初期値に変更する必要があります
            str顧客名 = "";
            //   str顧客コード = "";
            str入金金額開始 = "";
            str入金金額終了 = "";
            lng請求指定 = 1;
            lng削除指定 = 1;
            str顧客コード="";
        
    }

        //フィルタ設定その１　－　全表示（但し、削除データは除く）
        private void SetFilter1()
        {
            str入金コード開始 = "";
            str入金コード終了 = "";
            dtm入金日開始 = DateTime.MinValue; // 適切な初期値に変更する必要があります
            dtm入金日終了 = DateTime.MinValue; // 適切な初期値に変更する必要があります
            str顧客名 = "";
            str入金金額開始 = "";
            str入金金額終了 = "";
            lng請求指定 = 0;
            lng削除指定 = 1;
        }

        //領収アイコンを更新する（領収済みとする）
        private void UpdateReceiptIcon()
        {
            int lngRowIndex = 0;

            foreach (DataGridViewRow row in 一覧.Rows)
            {
                if (row.Cells[6].Value?.ToString() == "□")
                {
                    row.Cells[6].Value = "■";
                }
                else if (row.Cells[6].Value?.ToString() == "×")
                {
                    row.Cells[6].Value = "";
                }
                else
                {
                    row.HeaderCell.Value = null; // 他の場合はヘッダーセルをクリア
                }

                lngRowIndex++;
            }
        }

        private void Form_Load(object sender, EventArgs e)
        {
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

            // DataGridViewの設定
            一覧.AllowUserToResizeColumns = true;
            一覧.Font = new Font("MS ゴシック", 10);
            一覧.DefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 210, 255);
            一覧.DefaultCellStyle.SelectionForeColor = Color.Black;
            一覧.GridColor = Color.FromArgb(230, 230, 230);
            一覧.ColumnHeadersDefaultCellStyle.Font = new Font("MS ゴシック", 9);
            一覧.DefaultCellStyle.Font = new Font("MS ゴシック", 10);
            一覧.DefaultCellStyle.ForeColor = Color.Black;
            一覧.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

            System.Type dgvtype = typeof(DataGridView);
            System.Reflection.PropertyInfo dgvPropertyInfo = dgvtype.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            dgvPropertyInfo.SetValue(一覧, true, null);


            //myapi.GetFullScreen(out xSize, out ySize);

            //int x = 10, y = 10;

            //this.Size = new Size(this.Width, ySize * myapi.GetTwipPerDot(intpixel) - 1200);
            ////accessのmovesizeメソッドの引数の座標単位はtwipなので以下で

            //this.Size = new Size(this.Width, ySize - 1200 / twipperdot);

            //this.StartPosition = FormStartPosition.Manual; // 手動で位置を指定
            //int screenWidth = Screen.PrimaryScreen.Bounds.Width; // プライマリスクリーンの幅
            //x = (screenWidth - this.Width) / 2;
            //this.Location = new Point(x, y);

            intSortSettings = 1;

            this.一覧.Focus();

            InitializeFilter();
            DoUpdate();
            Cleargrid(一覧);

            fn.WaitForm.Close();
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            try
            {
                一覧.Height = 一覧.Height + (this.Height - intWindowHeight);
                intWindowHeight = this.Height;  // 高さ保存

                一覧.Width = 一覧.Width + (this.Width - intWindowWidth);
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
                result = GridSet();
                if (result >= 0)
                {
                    this.表示件数.Text = result.ToString();

                    //if (GridDrawn())  //getsetで処理するので不要
                    //{
                    //    return result;
                    //}
                    //else
                    //{
                    //    //描画処理時のエラー　何を返すか？
                    //    return result;
                    //}
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

        //選択中の入金コードを取得する
        private string[] GetCreditSlipCode(string flag)
        {
            int lngRowIndex;
            int lngArrayIndex;
            List<string> code = new List<string>();

            lngArrayIndex = -1;
            lngRowIndex = 0;
            gridobject = 一覧;
            do
            {
                // DataGridView の場合、Cells[列のインデックス].Value でセルの値にアクセスできます
                if (gridobject.Rows[lngRowIndex].Cells[6].Value?.ToString() == flag)
                {
                    lngArrayIndex++;
                    code.Add(gridobject.Rows[lngRowIndex].Cells[0].Value?.ToString());
                }
                lngRowIndex++;
            } while (lngRowIndex < gridobject.Rows.Count);

            return code.ToArray();
        }

        private int GridSet()
        {
            try
            {
                string strSQL = "";
                string strFilter = "";
                string strSource = "";
                string[] arr1;
                string str1 = "";

                //入金コード指定
                if (!string.IsNullOrEmpty(str入金コード開始))
                {
                    strFilter = WhereString(strFilter, "(入金コード BETWEEN '" + str入金コード開始 + "' AND '" + str入金コード終了 + "')");
                }

                // 入金日指定
                if (dtm入金日開始 != DateTime.MinValue && dtm入金日終了 != DateTime.MinValue)
                {
                    strFilter = WhereString(strFilter, "入金日 BETWEEN '" + dtm入金日開始 + "' AND '" + dtm入金日終了 + "'");
                }

                // 顧客コード指定
                if (!string.IsNullOrEmpty(str顧客コード))
                {
                    strFilter = WhereString(strFilter, "顧客コード='" + str顧客コード + "'");
                }

                // 購買コード指定
                if (!string.IsNullOrEmpty(str顧客名))
                {
                    strSource = str顧客名;
                    arr1 = strSource.Split();

                    foreach (object var1 in arr1)
                    {
                        str1 = var1.ToString();
                        if (!string.IsNullOrEmpty(str1))
                        {
                            strFilter = WhereString(strFilter, "顧客名 LIKE '%" + str1 + "%'");
                        }
                    }
                }

                // 入金金額指定
                if (!string.IsNullOrEmpty(str入金金額開始) && !string.IsNullOrEmpty(str入金金額終了))
                {
                    strFilter = WhereString(strFilter, "合計金額 BETWEEN " + str入金金額開始 + " AND " + str入金金額終了);
                }

                // 購買指定
                switch (lng請求指定)
                {
                    case 1:
                        strFilter = WhereString(strFilter, "請求 IS NULL");
                        break;
                    case 2:
                        strFilter = WhereString(strFilter, "請求 IS NOT NULL");
                        break;
                }

                // 削除指定
                switch (lng削除指定)
                {
                    case 1:
                        strFilter = WhereString(strFilter, "削除 IS NULL");
                        break;
                    case 2:
                        strFilter = WhereString(strFilter, "削除 IS NOT NULL");
                        break;
                }

                if (strFilter == "")
                {
                    strSQL = "SELECT * FROM V入金管理 ORDER BY 入金コード DESC";
                }
                else
                {
                    strSQL = "SELECT * FROM V入金管理 WHERE " + strFilter + " ORDER BY 入金コード DESC";
                }

                Connect();
                DataGridUtils.SetDataGridView(cn, strSQL, this.一覧);

                MyApi myapi = new MyApi();
                int xSize, ySize, intpixel, twipperdot;

                //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
                intpixel = myapi.GetLogPixel();
                twipperdot = myapi.GetTwipPerDot(intpixel);

                intWindowHeight = this.Height;
                intWindowWidth = this.Width;

                // DataGridViewの設定
                一覧.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200); // 薄い黄色
                一覧.Columns[1].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200); // 薄い黄色

                // 列の幅を設定 もとは恐らくtwipのためピクセルに直す

                //0列目はaccessでは行ヘッダのため、ずらす
                //dataGridView1.Columns[0].Width = 500 / twipperdot;
                一覧.Columns[0].Width = 1300 / twipperdot; //1150
                一覧.Columns[1].Width = 1400 / twipperdot;
                一覧.Columns[2].Width = 1200 / twipperdot;
                一覧.Columns[3].Width = 7000 / twipperdot;
                一覧.Columns[4].Visible = false;
                一覧.Columns[5].Width = 1500 / twipperdot;
                一覧.Columns[6].Width = 400 / twipperdot;
                一覧.Columns[7].Width = 400 / twipperdot;//1300
                一覧.Columns[8].Width = 400 / twipperdot;

                //金額列　カンマと右付け
                一覧.Columns[5].DefaultCellStyle.Format = "#,###,###,##0";
                一覧.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                return 一覧.RowCount;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Filtering - " + ex.Message);
                return -1;
            }
        }

        private bool GridDrawn()
        {
            try
            {
                一覧.SuspendLayout();

                // 行ヘッダーに行番号を表示する
                for (int i = 0; i < 一覧.Rows.Count; i++)
                {
                    一覧.Rows[i].HeaderCell.Value = (i + 1).ToString();
                }

                // 列1と列2のセルの背景色を設定  load時に行ってるので不要
                for (int i = 0; i < 一覧.Rows.Count; i++)
                {
                    一覧.Rows[i].Cells[0].Style.BackColor = Color.FromArgb(250, 250, 150);
                    一覧.Rows[i].Cells[1].Style.BackColor = Color.FromArgb(250, 250, 150);
                }

                // 列11の値に応じてセルの値とスタイルを設定
                //直接SQLに埋め込んだので不要　＝＞　数値を文字列に変換していたので、datagridviewに登録時に変換エラーになるため
                for (int i = 0; i < 一覧.Rows.Count; i++)
                {
                    if (一覧.Rows[i].Cells[10].Value != null &&
                        !string.IsNullOrEmpty(一覧.Rows[i].Cells[10].Value.ToString()))
                    {
                        int value = int.Parse(一覧.Rows[i].Cells[10].Value.ToString());

                        一覧.Columns[5].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }
                }

                // カーソルを復元する
                if (一覧.SelectedCells.Count > 0)
                {
                    DataGridViewCell firstSelectedCell = 一覧.SelectedCells[0];
                    一覧.CurrentCell = firstSelectedCell;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.Name + "_GridDrawn - " + ex.Message);
                return false;
            }
            finally
            {
                一覧.ResumeLayout();
            }
        }

        public override void SearchCode(string codeString)
        {
            // 検索コード保持
            strSearchCode = codeString;

            str入金コード開始 = strSearchCode;
            str入金コード終了 = strSearchCode;
            DoUpdate();
        }


        private void DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //列ヘッダーかどうか調べる
            if (e.ColumnIndex < 0 && e.RowIndex >= 0)
            {
                一覧.SuspendLayout();
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
                一覧.ResumeLayout();

            }
        }

        private void Cleargrid(DataGridView dataGridView)
        {
            dataGridView.ClearSelection();

            if (dataGridView.Rows.Count > 0)
            {
                dataGridView.Rows[0].Selected = true;
                dataGridView.CurrentCell = dataGridView.Rows[0].Cells[0];
                dataGridView.FirstDisplayedScrollingRowIndex = 0; // 先頭行を表示
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
                        if (this.コマンド入金.Enabled) コマンド入金_Click(null, null);
                        break;
                    case Keys.F6:
                        if (this.コマンド領収書.Enabled) コマンド領収書_Click(null, null);
                        break;
                    case Keys.F7:
                        if (this.コマンド顧客.Enabled) コマンド顧客_Click(null, null);
                        break;
                    case Keys.F8:
                        if (this.コマンド操作切替.Enabled) コマンド操作切替_Click(null, null);
                        break;
                    case Keys.F9:
                        if (this.コマンド印刷.Enabled) コマンド印刷_Click(null, null);
                        break;
                    case Keys.F11:
                        //if (this.コマンド入出力.Enabled) コマンド入出力_Click(null, null);
                        break;
                    case Keys.F10:
                        if (this.コマンド入出力.Enabled) コマンド入出力_Click(null, null);
                        break;
                    case Keys.F12:
                        if (this.コマンド終了.Enabled) コマンド終了_Click(null, null);
                        break;
                    case Keys.Return:
                        if (this.ActiveControl == this.一覧)
                        {
                            if (一覧.SelectedRows.Count > 0)
                            {
                                // DataGridView1で選択された行が存在する場合
                                string selectedData = 一覧.SelectedRows[0].Cells[0].Value.ToString(); // 1列目のデータを取得

                                F_入金 targetform = new F_入金();
                                targetform.args = CurrentCode;
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

        private void コマンド終了_Click(object sender, EventArgs e)
        {
            一覧.Focus();
            this.Close();
        }

        private void コマンド抽出_Click(object sender, EventArgs e)
        {
            一覧.Focus();
            objParent = this;
            F_入金管理_抽出 form = new F_入金管理_抽出();
            form.ShowDialog();
        }

        private void コマンド初期化_Click(object sender, EventArgs e)
        {
            一覧.Focus();
            FunctionClass fn = new FunctionClass();
            fn.DoWait("初期化しています...");
            InitializeFilter();
            DoUpdate();
            Cleargrid(一覧);

            一覧.Focus(); // DataGridViewにフォーカスを設定
            fn.WaitForm.Close();
            //MessageBox.Show("現在開発中です。", "初期化マンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド検索_Click(object sender, EventArgs e)
        {
            一覧.Focus(); // DataGridViewにフォーカスを設定

            F_検索コード form = new F_検索コード(this, "B");
            form.ShowDialog();
        }

        private void コマンド更新_Click(object sender, EventArgs e)
        {
            一覧.Focus();
            FunctionClass fn = new FunctionClass();
            fn.DoWait("更新しています...");
            DoUpdate();
            Cleargrid(一覧);
            fn.WaitForm.Close();
        }

        private void コマンド印刷_Click(object sender, EventArgs e)
        {
            一覧.Focus(); // DataGridViewにフォーカスを設定

            MessageBox.Show("現在開発中です。", "印刷コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド入出力_Click(object sender, EventArgs e)
        {
            一覧.Focus(); // DataGridViewにフォーカスを設定

            MessageBox.Show("現在開発中です。", "入出力コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド入金_Click(object sender, EventArgs e)
        {
            一覧.Focus();
            F_入金 form = new F_入金();
            form.args = CurrentCode;
            form.ShowDialog();
        }

        private void コマンド顧客_Click(object sender, EventArgs e)
        {
            一覧.Focus(); // DataGridViewにフォーカスを設定

            string param;
            param = $" -sv:{CommonConstants.ServerInstanceName} -open:customer";
            GetShell(param);
        }

        private void コマンド領収書_Click(object sender, EventArgs e)
        {
            try
            {
                // 領収コード
                string receiptCode;
                // 対象の入金コード一覧を取得する
                string[] creditSlipCode = GetCreditSlipCode("□");
                string codeString = "";

                foreach (string ft in creditSlipCode)
                {
                    codeString += "," + ft;
                }

                // 入金一覧文字列の先頭のデリミタを削除する
                codeString = codeString.Substring(1);

                // 領収処理を実行する
                receiptCode = DoReceiptProcess(codeString);

                // 領収済み表示更新
                UpdateReceiptIcon();

                bool shainFlg = false;

                var intRes = MessageBox.Show("社印は必要ですか？", "社印の有無確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (intRes == DialogResult.Yes)
                {
                    shainFlg=true;
                }
               
                // 領収書のプレビュー
                領収書印刷("ReceiptCode='" + receiptCode + "'", shainFlg);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(this.Name + "_コマンド領収書_Click - " + ex.Message);
                MessageBox.Show("エラーのため実行できません。", "領収書コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 領収書印刷(string filterString, bool shainFlg)
        {
            IReport paoRep = ReportCreator.GetPreview();
            if (shainFlg)
            {
                paoRep.LoadDefFile("../../../Reports/領収書.prepd");
            }
            else
            {
                paoRep.LoadDefFile("../../../Reports/領収書社員なし.prepd");
            }
            Connect();

            DataRowCollection report;

            string sqlQuery = "SELECT * FROM V領収書 WHERE " + filterString;

            using (SqlCommand command = new SqlCommand(sqlQuery, cn))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataSet dataSet = new DataSet();
                    adapter.Fill(dataSet);
                    report = dataSet.Tables[0].Rows;
                }
            }

            //最大行数
            int maxRow = 37;
            //現在の行
            int CurRow = 0;
            //行数
            int RowCount = maxRow;
            if (report.Count > 0)
            {
                RowCount = report.Count;
            }
            int page = 1;
            double maxPage = Math.Ceiling((double)RowCount / maxRow);
            DateTime now = DateTime.Now;

            int lenB;
            int i = 0;
            //描画すべき行がある限りページを増やす  1件しか印刷しない
            while (RowCount > 0)
            {
                RowCount -= maxRow;

                paoRep.PageStart();


                if (CurRow >= report.Count) break;

                DataRow targetRow = report[CurRow];

                //paoRep.Write("明細番号", (CurRow + 1).ToString(), i + 1);  //連番にしたい時はこちら。明細番号は歯抜けがあるので
                paoRep.Write("DestinationZipCode", targetRow["DestinationZipCode"].ToString() != "" ? targetRow["DestinationZipCode"].ToString() : " ", i + 1);
                paoRep.Write("DestinationAddress1", targetRow["DestinationAddress1"].ToString() != "" ? targetRow["DestinationAddress1"].ToString() : " ", i + 1);
                paoRep.Write("DestinationAddress2", targetRow["DestinationAddress2"].ToString() != "" ? targetRow["DestinationAddress2"].ToString() : " ", i + 1);
                paoRep.Write("DestinationName1", targetRow["DestinationName1"].ToString() != "" ? targetRow["DestinationName1"].ToString() : " ", i + 1);
                paoRep.Write("DestinationName2", targetRow["DestinationName2"].ToString() != "" ? targetRow["DestinationName2"].ToString() : " ", i + 1);
                paoRep.Write("ReceiptDay", targetRow["ReceiptDay"].ToString() != "" ? targetRow.Field<DateTime>("ReceiptDay").ToString("yyyy年MM月dd日") : " ", i + 1);
                paoRep.Write("領収日1", targetRow["ReceiptDay"].ToString() != "" ? targetRow.Field<DateTime>("ReceiptDay").ToString("yyyy年MM月dd日") : " ", i + 1);
                paoRep.Write("領収日2", targetRow["ReceiptDay"].ToString() != "" ? targetRow.Field<DateTime>("ReceiptDay").ToString("yyyy年MM月dd日") : " ", i + 1);
                paoRep.Write("送付状摘要", targetRow["Summary"].ToString() != "" ? targetRow["Summary"].ToString() : " ", i + 1);
                paoRep.Write("SummaryCopy", targetRow["Summary"].ToString() != "" ? targetRow["Summary"].ToString() : " ", i + 1);
                paoRep.Write("領収先名前1", targetRow["ReceiptName"].ToString() != "" ? targetRow["ReceiptName"].ToString() : " ", i + 1);
                paoRep.Write("領収先名前2", targetRow["ReceiptName"].ToString() != "" ? targetRow["ReceiptName"].ToString() : " ", i + 1);
                paoRep.Write("領収金額合計", targetRow["Receipt"].ToString() != "" ? targetRow["Receipt"].ToString() : " ", i + 1);
                paoRep.Write("控え領収金額合計", targetRow["Receipt"].ToString() != "" ? targetRow["Receipt"].ToString() : " ", i + 1);
                paoRep.Write("領収コード1", targetRow["ReceiptCode"].ToString() != "" ? targetRow["ReceiptCode"].ToString() : " ", i + 1);
                paoRep.Write("領収コード2", targetRow["ReceiptCode"].ToString() != "" ? targetRow["ReceiptCode"].ToString() : " ", i + 1);
                paoRep.Write("但し書き1", targetRow["Proviso"].ToString() != "" ? targetRow["Proviso"].ToString() : " ", i + 1);
                paoRep.Write("但し書き2", targetRow["Proviso"].ToString() != "" ? targetRow["Proviso"].ToString() : " ", i + 1);
                paoRep.Write("DetailsSummary1", targetRow["DetailsSummary"].ToString() != "" ? targetRow["DetailsSummary"].ToString() : " ", i + 1);
                paoRep.Write("DetailsSummary2", targetRow["DetailsSummary"].ToString() != "" ? targetRow["DetailsSummary"].ToString() : " ", i + 1);

                paoRep.z_Objects.SetObject("領収先名前1", i + 1);
                if (targetRow["ReceiptName"].ToString().Length > 20)
                {
                    paoRep.z_Objects.z_Text.z_FontAttr.Size = 8;
                }
                else
                {
                    paoRep.z_Objects.z_Text.z_FontAttr.Size = 12;
                }

                paoRep.z_Objects.SetObject("領収先名前2", i + 1);
                if (targetRow["ReceiptName"].ToString().Length > 20)
                {
                    paoRep.z_Objects.z_Text.z_FontAttr.Size = 8;
                }
                else
                {
                    paoRep.z_Objects.z_Text.z_FontAttr.Size = 12;
                }

                CurRow++;
                //}
                i += 1;
                page++;
                
                paoRep.PageEnd();

            }

            paoRep.Output();
        }

        private void コマンド領収解除_Click(object sender, EventArgs e)
        {
            try
            {
                一覧.Focus();

                DialogResult result = MessageBox.Show("解除しますか？", "領収解除コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    return;
                }

                string receiptCode;                      // 領収コード
                string[] creditSlipCode = GetCreditSlipCode("×"); // 対象の入金コード一覧を取得する
                string codeString = string.Join(",", creditSlipCode); // 入金一覧文字列を作成
                codeString = codeString.Substring(1);   // 先頭のデリミタを削除

                if (!DoReceiptCancel(codeString))      // 領収済みを取り消す
                {
                    throw new Exception("エラーのため実行できません。");
                }

                UpdateReceiptIcon();                    // 領収済み表示更新
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                MessageBox.Show("エラーのため実行できません。", "領収解除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void コマンド操作切替_Click(object sender, EventArgs e)
        {
            ChangeOperationMode();
        }

        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            LocalSetting test = new LocalSetting();
            test.SavePlace(CommonConstants.LoginUserCode, this);
        }

        private void 一覧_SortCompare(object sender, DataGridViewSortCompareEventArgs e)
        {
            try
            {
                if (e.Column.Index == 0) // 列のインデックスは適切なものに置き換える
                {
                    decimal cellValue1, cellValue2;
                    if (decimal.TryParse(e.CellValue1?.ToString(), out cellValue1) && decimal.TryParse(e.CellValue2?.ToString(), out cellValue2))
                    {
                        int compareResult = cellValue1.CompareTo(cellValue2);
                        e.SortResult = compareResult * intSortSettings;
                        e.Handled = true;
                    }
                    else
                    {
                        // エラー処理
                        e.SortResult = 0;
                        e.Handled = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void 一覧_Click(object sender, EventArgs e)
        {
            gridobject = 一覧;
            if (OperationMode == 1)
            {
                string currentValue = gridobject.Rows[gridobject.CurrentCell.RowIndex].Cells[6].Value?.ToString();

                switch (currentValue)
                {
                    case "":
                        gridobject.Rows[gridobject.CurrentCell.RowIndex].Cells[6].Value = "□";
                        break;
                    case "□":
                        gridobject.Rows[gridobject.CurrentCell.RowIndex].Cells[6].Value = "";
                        break;
                    case "■":
                        gridobject.Rows[gridobject.CurrentCell.RowIndex].Cells[6].Value = "×";
                        break;
                    case "×":
                        gridobject.Rows[gridobject.CurrentCell.RowIndex].Cells[6].Value = "■";
                        break;
                    default:
                        break;
                }
            }
        }

        private void 一覧_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if ((intButton & (int)MouseButtons.Left) <= 0 || 一覧.CurrentRow == null)
                {
                    intButton = (int)MouseButtons.Left; // 保存マウスボタン初期化
                    return;
                }

                F_入金 form = new F_入金();
                form.args = CurrentCode;
                form.ShowDialog();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void 一覧_CellEnter(object sender, DataGridViewCellEventArgs e)
        {

            一覧.Invalidate();
        }

        private void 一覧_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            一覧.SuspendLayout();
        }

        private bool ascending = true;
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 3)
            {
                if (ascending)
                {
                    一覧.Sort(一覧.Columns[4], System.ComponentModel.ListSortDirection.Ascending);
                }
                else
                {
                    一覧.Sort(一覧.Columns[4], System.ComponentModel.ListSortDirection.Descending);
                }
                ascending = !ascending;
            }
        }


        private void 一覧_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                // マウスボタン確保
                intButton = (int)e.Button;

                // 並べ替え前の表示
                DataGridView gridobject = (DataGridView)sender;

                if (gridobject.HitTest(e.X, e.Y).RowIndex == -1)
                {
                    gridobject.SuspendLayout();
                    gridobject.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
                    //gridobject.CurrentCell = gridobject.Rows[gridobject.FixedRows].Cells[gridobject.HitTest(e.X, e.Y).ColumnIndex];
                    gridobject.ResumeLayout();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void 一覧_MouseUp(object sender, MouseEventArgs e)
        {
            try
            {
                if ((e.Button & MouseButtons.Left) <= 0)
                {
                    return;
                }

                DataGridView gridobject = (DataGridView)sender;

                if (gridobject.HitTest(e.X, e.Y).RowIndex == 0)
                {
                    gridobject.SuspendLayout();

                    // 対象列が顧客名のときは顧客名のふりがなで並べ替える
                    if (gridobject.HitTest(e.X, e.Y).ColumnIndex == 4)
                    {
                        gridobject.CurrentCell = gridobject.Rows[0].Cells[5];
                    }

                    switch (gridobject.HitTest(e.X, e.Y).ColumnIndex)
                    {
                        case 9:
                            // カスタムソート
                            // カスタム値が定数化されていない場合、この部分を適切な処理に変更してください
                            gridobject.Sort(gridobject.Columns[9], ListSortDirection.Ascending);
                            break;
                        default:
                            // 通常のソート
                            if (intSortSettings == 1)
                            {
                                gridobject.Sort(gridobject.Columns[gridobject.HitTest(e.X, e.Y).ColumnIndex], ListSortDirection.Ascending);
                            }
                            else
                            {
                                gridobject.Sort(gridobject.Columns[gridobject.HitTest(e.X, e.Y).ColumnIndex], ListSortDirection.Descending);
                            }
                            break;
                    }

                    // ソート順序（昇順/降順）入れ替え
                    intSortSettings *= -1;

                    gridobject.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    gridobject.FirstDisplayedScrollingRowIndex = 0; // カーソルを先頭行へ移動させる

                    if (!GridDrawn())
                    {
                        MessageBox.Show("エラーが発生しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    gridobject.ResumeLayout();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }
    }
}