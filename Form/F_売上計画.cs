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
using DocumentFormat.OpenXml.Bibliography;
//using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Data.SqlClient;
using Pao.Reports;
using u_net.Public;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace u_net
{
    public partial class F_売上計画 : MidForm
    {
        const int LAST_YEARS = 5;
        const string SUM_CODE = "99999999";

        public string str顧客コード;
        public string str顧客名;
        public string strSalesmanCode;
        public int intTheYear;

        public long lngSectionHeight;
        public long lngBackColor;

        int intWindowHeight = 0;
        int intWindowWidth = 0;

        private Control? previousControl;
        private SqlConnection? cn;
        public F_売上計画()
        {
            InitializeComponent();
        }

        public string CustomerCode
        {
            get
            {
                if (dataGridView1.CurrentRow.Index != dataGridView1.RowCount - 1)
                {
                    return dataGridView1.CurrentRow.Cells[0].Value?.ToString();
                }
                else
                {
                    return "";
                }
            }
        }


        public string SalesmanCode
        {
            get
            {
                return strSalesmanCode;
            }

            set
            {
                Connect();
                strSalesmanCode = value;
                自社担当者コード.SelectedValue = strSalesmanCode;
                自社担当者名.Text = FunctionClass.GetUserFullName(cn, strSalesmanCode);
            }
        }

        public string SalesmanName
        {
            get
            {
                return Nz(自社担当者コード.SelectedValue);
            }
        }

        public int TheYear
        {
            get
            {
                return intTheYear;
            }

            set
            {
                intTheYear = value;
            }
        }


        private string Nz(object value)
        {
            return value == null ? "" : value.ToString();
        }

        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }


        private void Form_Load(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                control.PreviewKeyDown += OriginalClass.ValidateCheck;
            }

            string LoginUserCode = CommonConstants.LoginUserCode;
            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(LoginUserCode, this);


            lngSectionHeight = 12;


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

            //dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
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


            Connect();

            using (SqlCommand cmd = new SqlCommand("SP売上年度", cn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();

                // レコードセットを設定
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);

                集計年度.DisplayMember = "売上年度";
                集計年度.ValueMember = "売上年度";
                集計年度.DataSource = dataTable;


            }

            OriginalClass ofn = new OriginalClass();
            ofn.SetComboBox(自社担当者コード, "SELECT [社員コード] as Value, 社員コード as Display, 氏名 AS Display2 FROM M社員 WHERE ([パート] = 0) AND (退社 IS NULL) AND (ふりがな <> N'ん') AND (部 = N'営業部') AND (削除日時 IS NULL) ORDER BY ふりがな");
            自社担当者コード.DrawMode = DrawMode.OwnerDrawFixed;
            自社担当者コード.SelectedValue = CommonConstants.LoginUserCode;
            strSalesmanCode = CommonConstants.LoginUserCode;

            TheYear = DateTime.Now.Year;


            if (DoUpdate())
            {

                コマンド更新.Enabled = true;
                コマンドコピー.Enabled = true;
                コマンド出力.Enabled = true;

            }
            else
            {
                MessageBox.Show("エラーが発生しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                コマンドコピー.Enabled = false;
                コマンド更新.Enabled = false;
                コマンド出力.Enabled = false;
            }


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
        public bool DoUpdate()
        {
            if (string.IsNullOrEmpty(集計年度.Text)) return false;

            FunctionClass fn = new FunctionClass();
            fn.DoWait("集計しています...");

            bool result = true;
            try
            {
                SetGrid(TheYear,SalesmanCode,str顧客コード,str顧客名);
                //AddTotalRow(dataGridView1);

                if (dataGridView1.RowCount > 0)
                {
                    dataGridView1.Rows[0].Selected = true;
                    dataGridView1.FirstDisplayedScrollingRowIndex = 0;
                }

            }
            catch (Exception ex)
            {
                result = false;
                MessageBox.Show(this.Name + "_DoUpdate - " + ex.Message);
            }

            fn.WaitForm.Close();

            return result;
        }

        private bool SetGrid(int year,string SalesmanCode,string CustomerCode = null,string CustomerName = null)
        {
            bool success = false;

            Connect();

            try
            {

                FunctionClass fn = new FunctionClass();


                using (SqlCommand command = new SqlCommand("SP売上計画表", cn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SalesYear", year);
                    command.Parameters.AddWithValue("@SalesmanCode", fn.Zn(SalesmanCode));
                    command.Parameters.AddWithValue("@CustomerCode", fn.Zn(CustomerCode));
                    command.Parameters.AddWithValue("@CustomerName", fn.Zn(CustomerName));

                    // データベースからデータを取得して DataGridView に設定
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        //dataGridView1.DataSource = dataTable;

                        BindingSource bindingSource = new BindingSource();
                        bindingSource.DataSource = dataTable;

                        // DataGridView に BindingSource をバインド
                        dataGridView1.DataSource = bindingSource;
                    }


                    表示件数.Text = dataGridView1.RowCount.ToString();


                    success = true;


                    MyApi myapi = new MyApi();
                    int xSize, ySize, intpixel, twipperdot;

                    //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
                    intpixel = myapi.GetLogPixel();
                    twipperdot = myapi.GetTwipPerDot(intpixel);

                    intWindowHeight = this.Height;
                    intWindowWidth = this.Width;

                    //0列目はaccessでは行ヘッダのため、ずらす
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Width = 4000 / twipperdot;
                    dataGridView1.Columns[2].Visible = false;
                    dataGridView1.Columns[3].Visible = false;
                    dataGridView1.Columns[4].Width = 1500 / twipperdot;
                    dataGridView1.Columns[5].Width = 1000 / twipperdot;
                    dataGridView1.Columns[6].Width = 1500 / twipperdot;
                    dataGridView1.Columns[6].DefaultCellStyle.Format = "#,###,###,##0";
                    dataGridView1.Columns[6].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;

                    for (int col = 7; col <= 18; col++)
                    {
                        dataGridView1.Columns[col].Width = 1350 / twipperdot;
                        dataGridView1.Columns[col].DefaultCellStyle.Format = "#,###,###,##0";
                        dataGridView1.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }



                    string strCustomerName = string.Empty;
                    string strSalesDivisionName = string.Empty;
                    string strCustomerCode = string.Empty; 

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        // 縦に連続している顧客名をなくす
                        if (row.Cells[1].Value.ToString() == strCustomerName)
                        {
                            row.Cells[1].Value = "";
                        }
                        else
                        {
                            strCustomerName = row.Cells[1].Value.ToString();
                        }


                        // 縦に連続している売上区分名をなくす
                        if (row.Cells[4].Value.ToString() == strSalesDivisionName)
                        {
                            row.Cells[4].Value = "";
                        }
                        else
                        {
                            strSalesDivisionName = row.Cells[4].Value.ToString();
                        }


                        for (int col = 6; col <= 18; col++)
                        {
                            if(row.Index % 2 == 0)
                            {
                                row.Cells[col].Style.BackColor = Color.FromArgb(230, 230, 230);
                            }

                            
                        }


                        

                    }

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (row.Cells[0].Value.ToString() != strCustomerCode)
                        {
                            SetLastProceeds2(TheYear - 1, LAST_YEARS, row.Cells[0].Value.ToString(), row.Index + 3, 1);
                            strCustomerCode = row.Cells[0].Value.ToString();
                        }
                    }


                    

                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return success;
        }



        private void SetLastProceeds2(int lastYear, int number, string customerCode, int rowIndex, int colIndex)
        {
            try
            {
                DataTable dt = GetSalesProceeds(lastYear - (number - 1), lastYear, customerCode);

                int rowindex = rowIndex;

                foreach (DataRow row in dt.Rows)
                {
                    string year = (row["売上年度"] != DBNull.Value) ? row["売上年度"].ToString() : "0";

                    string salesAmount = (row["売上金額"] != DBNull.Value)
                        ? Convert.ToDecimal(row["売上金額"]).ToString("###,###,##0")
                        : "0";

                    string formattedRow = $"{year}年 : {salesAmount,11}";

                    dataGridView1.Rows[rowindex].Cells[colIndex].Value = formattedRow;
                    rowindex++;
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"{this.Name}_SetLastProceeds2 - {ex.GetType().Name} : {ex.Message}");
            }
        }


        private DataTable GetSalesProceeds(int year1, int year2, string code)
        {
            DataTable dt = new DataTable();

            Connect();

            try
            {
                using (SqlCommand cmd = new SqlCommand("SP売上計画_年度別実績金額", cn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    // パラメータの設定
                    cmd.Parameters.AddWithValue("@SalesYearStart", year1);
                    cmd.Parameters.AddWithValue("@SalesYearEnd", year2);
                    cmd.Parameters.AddWithValue("@SalesmanCode", DBNull.Value);
                    cmd.Parameters.AddWithValue("@CustomerCode", string.IsNullOrEmpty(code) ? DBNull.Value : (object)code);
                    cmd.Parameters.AddWithValue("@CustomerName", DBNull.Value);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"{this.GetType().Name}_GetSalesProceeds - {ex.GetType().Name} : {ex.Message}");
            }

            return dt;
        }


        private static void AddTotalRow(DataGridView dataGridView)
        {
            try
            {
                int rowCount = dataGridView.Rows.Count;
                int colCount = dataGridView.Columns.Count;


                BindingSource bindingSource = (BindingSource)dataGridView.DataSource;
                bindingSource.AddNew();


                // 合計行に表示する文字列
                dataGridView.Rows[rowCount].Cells[0].Value = "(合計)";

                // 列ごとの合計金額を計算し、表示する
                for (int col = 2; col <= colCount; col++)
                {
                    long sum = 0;

                    // 列ごとに合計金額を計算
                    for (int row = 0; row < rowCount; row++)
                    {
                        // データグリッドビューのセルの値が数値であることを仮定
                        object cellValue = dataGridView.Rows[row].Cells[col].Value;
                        if (cellValue != null && cellValue.ToString() != "")
                        {
                            sum += Convert.ToInt64(cellValue);
                        }

                    }

                    // 合計をセルに表示
                    dataGridView.Rows[rowCount].Cells[col].Value = sum;

                    // セルのフォーマットを設定して桁区切りにする
                    dataGridView.Columns[col].DefaultCellStyle.Format = "#,###,###,##0";
                    dataGridView.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }



        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            //AddTotalRow(dataGridView1);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.RowIndex == -1 && e.ColumnIndex > 0)
            //{
            //    dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);
            //}
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

            try
            {

                if(dataGridView1.CurrentCell.RowIndex < 0 || dataGridView1.CurrentCell.ColumnIndex < 6)
                {
                    return;
                }
                
                MessageBox.Show($"{SalesmanCode} : {TheYear}");

    
            }
            catch (Exception ex)
            {
                // エラー処理
                Console.WriteLine($"Error: {ex.Message}");
            }

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            Form_KeyDown(sender, e);
        }


        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    //case Keys.Return:
                    //    SelectNextControl(ActiveControl, true, true, true, true);
                    //    break;
                    case Keys.F1:
                        if (this.コマンド抽出.Enabled) コマンド抽出_Click(null, null);
                        break;
                    case Keys.F2:
                        if (this.コマンド検索.Enabled) コマンド検索_Click(null, null);
                        break;
                    case Keys.F3:
                        if (this.コマンド初期化.Enabled) コマンド初期化_Click(null, null);
                        //datagidviewの並び替えが行われるため
                        e.Handled = true;
                        break;
                    case Keys.F4:
                        if (this.コマンド更新.Enabled) コマンド更新_Click(null, null);
                        
                        break;
                    case Keys.F5:
                        if (this.コマンド顧客参照.Enabled) コマンド顧客参照_Click(null, null);
                        break;
                    case Keys.F6:
                        if (this.コマンドコピー.Enabled) コマンドコピー_Click(null, null);
                        break;
                    case Keys.F7:
                        if (this.コマンド重要顧客.Enabled) コマンド重要顧客_Click(null, null);
                        break;
                    case Keys.F8:
                        break;
                    case Keys.F9:
                        if (this.コマンド出力.Enabled) コマンド出力_Click(null, null);
                        break;
                    case Keys.F10:
                        if (this.コマンド更新.Enabled) コマンド更新_Click(null, null);
                        break;
                    case Keys.F11:
                        
                        break;
                    case Keys.F12:
                        if (this.コマンド終了.Enabled) コマンド終了_Click(null, null);
                        break;
                    case Keys.D0:
                    case Keys.D1:
                    case Keys.D2:
                    case Keys.D3:
                    case Keys.D4:
                    case Keys.D5:
                    case Keys.D6:
                    case Keys.D7:
                    case Keys.D8:
                    case Keys.D9:
                    case Keys.NumPad0:
                    case Keys.NumPad1:
                    case Keys.NumPad2:
                    case Keys.NumPad3:
                    case Keys.NumPad4:
                    case Keys.NumPad5:
                    case Keys.NumPad6:
                    case Keys.NumPad7:
                    case Keys.NumPad8:
                    case Keys.NumPad9:
                    case Keys.Back:
                    case Keys.Return:
                        // フォーカスが一覧にあるときのみ処理を続行する
                        if (dataGridView1.SelectedCells.Count <= 0)
                        {
                            e.Handled = true;
                            return;
                        }

                        // 金額区分が「計画」のときでかつ
                        // カレントセルが「合計」に所属するセル以外のときかつ
                        // 売上区分の合計欄セル以外のとき処理を続行する
                        if (Convert.ToString(dataGridView1[5, dataGridView1.CurrentCell.RowIndex].Value) != "計画"
                            || Convert.ToString(dataGridView1[0, dataGridView1.CurrentCell.RowIndex].Value) == "99999999"
                            || Convert.ToInt32(dataGridView1[3, dataGridView1.CurrentCell.RowIndex].Value) == 0
                            || GetMonth(Convert.ToString(dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].HeaderText)) <= 0)
                        {
                            e.Handled = true;
                            return;
                        }

                        // 現在のセルの内容を直接編集する
                        string cellValue = Convert.ToString(dataGridView1.CurrentCell.Value);

                        switch (e.KeyCode)
                        {
                            case Keys.Back:
                                if (cellValue != "0")
                                {
                                    if (cellValue.Length == 1)
                                    {
                                        cellValue = "0";
                                    }
                                    else
                                    {
                                        cellValue = cellValue.Substring(0, cellValue.Length - 1);
                                    }
                                }
                                dataGridView1.CurrentCell.Style.BackColor = Color.FromArgb(255, 255, 150); // 編集したことを示すフラグ
                                break;
                            case Keys.Return:
                                // 登録処理
                                if (SavePlanData())
                                {
                                    // 未編集状態とする（背景色を既定値に戻す）
                                    dataGridView1.CurrentCell.Style.BackColor = Color.FromArgb(230, 230, 230);
                                    // カレント行の合計金額を設定する
                                    SetSumRow(dataGridView1.CurrentCell.RowIndex);
                                    // カレント列の合計金額を設定する                            
                                    SetSumCol(dataGridView1.CurrentCell.ColumnIndex);
                                    // カレント列の全顧客の計画金額の合計を設定する
                                    SetSumColAll(dataGridView1.CurrentCell.ColumnIndex);
                                    // 合計列の合計金額を設定する
                                    SetSumCol(6);
                                    // 合計セクションの区分合計を設定する
                                    SetSectionSumCol(dataGridView1.CurrentCell.ColumnIndex);
                                    // 合計セクションの対象売上区分の合計列を設定する
                                    SetSectionSumRow(6);
                                    // 合計セクションの合計列の各売上区分合計を設定する
                                    SetSectionSumCol(6);
                                }
                                break;
                            default:
                                if (cellValue == "0" || dataGridView1.CurrentCell.Style.BackColor != Color.FromArgb(255, 255, 150))
                                {
                                    cellValue = e.KeyCode.ToString().Substring(e.KeyCode.ToString().Length - 1);

                                }
                                else
                                {
                                    cellValue = cellValue + e.KeyCode.ToString().Substring(e.KeyCode.ToString().Length - 1);
                                }
                                dataGridView1.CurrentCell.Style.BackColor = Color.FromArgb(255, 255, 150); // 編集したことを示すフラグ
                                break;
                        }

                        dataGridView1.CurrentCell.Value = cellValue;
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("KeyDown - " + ex.Message);
            }
        }


        private bool SavePlanData()
        {
            try
            {
                int intYear;
                int intMonth;
                string strSalesmanCode;
                string strCustomerCode;
                int intSalesDivisionCode;
                long lngPlannedAmount;
                string strSQL1;
                string strSQL2;

                Connect();

                // 登録に必要なパラメータ値を取得する
                strSalesmanCode = this.SalesmanCode;
                strCustomerCode = dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[0].Value?.ToString();
                intMonth = GetMonth(dataGridView1.Columns[dataGridView1.CurrentCell.ColumnIndex].HeaderText);

                // 「売上計画」テーブルに格納する「年」は「年度」ではないため、
                // 表示されている「年度」の「年」への変換が必要
                intYear = (intMonth >= 4) ? this.TheYear : this.TheYear + 1;
                intSalesDivisionCode = Convert.ToInt32(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[3].Value);
                lngPlannedAmount = Convert.ToInt64(dataGridView1.Rows[dataGridView1.CurrentCell.RowIndex].Cells[dataGridView1.CurrentCell.ColumnIndex].Value);

                // 既存データの削除用SQLを作成する
                strSQL1 = $"DELETE FROM T売上計画 WHERE 年={intYear} AND 月={intMonth} AND 顧客コード='{strCustomerCode}' AND 自社担当者コード='{strSalesmanCode}' AND 売上区分コード={intSalesDivisionCode}";

                // 新規データの挿入用SQLを作成する
                strSQL2 = $"INSERT INTO T売上計画  " +
                          $"SELECT REPLACE(STR(ISNULL(MAX(CAST(売上計画コード AS int)), 0) + 1, 8, 0), ' ', '0'), {intYear}, {intMonth}, " +
                          $"'{strSalesmanCode}', '{strCustomerCode}', {intSalesDivisionCode}, {lngPlannedAmount}, GETDATE(), '{CommonConstants.LoginUserCode}' FROM T売上計画";

                //Debug.Print(strSQL1);
                //Debug.Print(strSQL2);

                // SQLを実行
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandText = strSQL1;
                    cmd.ExecuteNonQuery(); // 既存データの削除

                    cmd.CommandText = strSQL2;
                    cmd.ExecuteNonQuery(); // 新規データの挿入
                }
                

                return true;
            }
            catch (Exception ex)
            {
                Debug.Print($"SavePlanData - {ex.Message}");
                return false;
            }
        }



        private void SetSumRow(int rowIndex)
        {
            try
            {
                long lngSum = 0;

                for (int lngCol = 7; lngCol < dataGridView1.Columns.Count; lngCol++)
                {
                    lngSum += Convert.ToInt64(dataGridView1.Rows[rowIndex].Cells[lngCol].Value);
                }

                dataGridView1.Rows[rowIndex].Cells[6].Value = lngSum;
            }
            catch (Exception ex)
            {
                Debug.Print($"SetSumRow - {ex.Message}");
            }
        }

        private void SetSumCol(int colIndex)
        {
            try
            {
                int lngRowBegin = 0;
                int lngRowEnd = 0;
                int lngRow = 0;
                long lngSum = 0;
                string strCode = string.Empty; // 比較用顧客コード

                strCode = dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString();

                // 計算範囲の始点を得る
                for (lngRow = dataGridView1.CurrentRow.Index; lngRow >= 0; lngRow--)
                {
                    if (dataGridView1.Rows[lngRow].Cells[0].Value.ToString() != strCode)
                        break;
                }
                lngRowBegin = lngRow + 1;

                // 計算範囲の終点を得る
                for (lngRow = dataGridView1.CurrentRow.Index; lngRow < dataGridView1.Rows.Count - 1; lngRow++)
                {
                    if ((dataGridView1.Rows[lngRow].Cells[0].Value.ToString() != strCode || lngRow == lngRowBegin) && lngRow < dataGridView1.Rows.Count - 1)
                        break;
                }
                lngRowEnd = lngRow - (2 + 2);

                // 計画値のみ合計していく
                for (lngRow = lngRowBegin; lngRow <= lngRowEnd; lngRow += 2)
                {
                    lngSum += Convert.ToInt64(dataGridView1.Rows[lngRow].Cells[colIndex].Value);
                }

                // 合計金額を設定する
                dataGridView1.Rows[lngRowEnd + 2].Cells[colIndex].Value = lngSum;
            }
            catch (Exception ex)
            {
                Debug.Print($"SetSumCol - {ex.Message}");
            }
        }


        private void SetSumColAll(int colIndex)
        {
            try
            {
                int lngRowBegin = 0;
                int lngRow = 0;
                decimal curSum = 0;

                // 計算範囲の始点を得る
                for (lngRow = dataGridView1.CurrentRow.Index; lngRow >= 0; lngRow -= Convert.ToInt32(lngSectionHeight))
                {
                    if (lngRow - lngSectionHeight < 0)
                        break;
                }
                lngRowBegin = lngRow;

                // 全顧客の対象売上区分計画金額を加算していく
                for (lngRow = lngRowBegin; dataGridView1.Rows[lngRow].Cells[0].Value.ToString() != SUM_CODE; lngRow += Convert.ToInt32(lngSectionHeight))
                {
                    curSum += Convert.ToDecimal(dataGridView1.Rows[lngRow].Cells[colIndex].Value);
                }

                // 合計金額を設定する
                dataGridView1.Rows[lngRow].Cells[colIndex].Value = curSum;
            }
            catch (Exception ex)
            {
                Debug.Print($"SetSumColAll - {ex.Message}");
            }
        }

        private void SetSectionSumCol(int colIndex)
        {
            try
            {
                int rowBegin = dataGridView1.FirstDisplayedScrollingRowIndex;

                // 合計セクションの計算範囲の始点を得る
                while (rowBegin < dataGridView1.RowCount && dataGridView1.Rows[rowBegin].Cells[0].Value.ToString() != SUM_CODE)
                {
                    rowBegin++;
                }

                // 計算範囲の終点を得る
                int rowEnd = rowBegin;
                while ((dataGridView1.Rows[rowEnd].Cells[0].Value.ToString() == SUM_CODE || rowEnd == rowBegin) && rowEnd < dataGridView1.RowCount - 1)
                {
                    rowEnd++;
                }
                rowEnd -= (1 + 2);

                // 計画値のみ合計していく
                Decimal curSum = 0;
                for (int row = rowBegin; row <= rowEnd; row += 2)
                {
                    curSum += Convert.ToDecimal(dataGridView1.Rows[row].Cells[colIndex].Value);
                }

                // 合計金額を設定する
                dataGridView1.Rows[rowEnd + 2].Cells[colIndex].Value = curSum;
            }
            catch (Exception ex)
            {
                Console.WriteLine("SetSectionSumCol - " + ex.Message);
            }
        }

        private void SetSectionSumRow(int colIndex)
        {
            try
            {
                int rowIndex = dataGridView1.CurrentRow.Index;

                // 計算対象の行インデックスを得る
                while (rowIndex + lngSectionHeight < dataGridView1.RowCount - 1)
                {
                    rowIndex += Convert.ToInt32(lngSectionHeight);
                }

                // 行の合計金額を設定する
                decimal curSum = 0;
                for (int col = colIndex + 1; col < dataGridView1.ColumnCount; col++)
                {
                    curSum += Convert.ToDecimal(dataGridView1.Rows[rowIndex].Cells[col].Value);
                }

                dataGridView1.Rows[rowIndex].Cells[colIndex].Value = curSum;
            }
            catch (Exception ex)
            {
                Console.WriteLine("SetSectionSumRow - " + ex.Message);
            }
        }



        private int GetMonth(string columnName)
        {
            switch (columnName)
            {
                case "４月":
                    return 4;
                case "５月":
                    return 5;
                case "６月":
                    return 6;
                case "７月":
                    return 7;
                case "８月":
                    return 8;
                case "９月":
                    return 9;
                case "１０月":
                    return 10;
                case "１１月":
                    return 11;
                case "１２月":
                    return 12;
                case "１月":
                    return 1;
                case "２月":
                    return 2;
                case "３月":
                    return 3;
                default:
                    return -1; // 不正な引数が渡されたときは-1を返す
            }
        }


        public void SetValue(long Value)
        {
            dataGridView1.CurrentCell.Value = Value;
        }


        private void コマンド更新_Click(object sender, EventArgs e)
        {
            try
            {

                // 確認
                DialogResult result = MessageBox.Show("表示データを最新の情報に更新しますか？", "更新コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                {
                    return;
                }


                // リストを更新する

                if (DoUpdate())
                {
                    

                        コマンドコピー.Enabled = true;
                        コマンド出力.Enabled = true;
                        コマンド更新.Enabled = true;
                    
                      
                }
                else
                {
                    MessageBox.Show("エラーが発生しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    コマンドコピー.Enabled = false;
                    コマンド出力.Enabled = false;
                    コマンド更新.Enabled = false;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"コマンド更新_Click エラー: {ex.Message}");
            }
        }

        private void コマンド終了_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void コマンド検索_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。", "検索コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド初期化_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。", "初期化コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void コマンド顧客参照_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                string selectedData = dataGridView1.SelectedRows[0].Cells[0].Value.ToString(); // 1列目のデータを取得

                string trimmedAndReplaced = selectedData.TrimEnd().Replace(" ", "_");

                string replacedServerInstanceName = CommonConstants.ServerInstanceName.Replace(" ", "_");

                string param = $" -sv:{replacedServerInstanceName} -open:saleslistbyparentcustomer, {trimmedAndReplaced},1";
                FunctionClass.GetShell(param);
            }
        }

        private void コマンドコピー_Click(object sender, EventArgs e)
        {
            try
            {
                // 選択されているセルのデータを取得
                DataObject dataObject = dataGridView1.GetClipboardContent();

                // クリップボードにコピー
                Clipboard.SetDataObject(dataObject);

                MessageBox.Show("クリップボードへコピーしました。", "成功", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                // コピーに失敗した場合はエラーメッセージを表示
                Console.WriteLine("クリップボードへのコピーに失敗しました。" + ex.Message);

            }
        }


        private void コマンド抽出_Click(object sender, EventArgs e)
        {
            //F_売上計画_抽出 targetform = new F_売上計画_抽出();
            //targetform.ShowDialog();
        }

        private void コマンド重要顧客_Click(object sender, EventArgs e)
        {
            //F_売上計画_重要顧客設定 targetform = new F_売上計画_重要顧客設定();
            //targetform.TheYear = TheYear;
            //targetform.SalesmanCode = SalesmanCode;
            //targetform.ShowDialog();
        }

        private void コマンド出力_Click(object sender, EventArgs e)
        {

        }

        private void コマンド登録_Click(object sender, EventArgs e)
        {
            SavePlanData2();
        }

        private void SavePlanData2()
        {
            int intYear;
            int intMonth;
            string strSalesmanCode;
            string strCustomerCode;
            int intSalesDivisionCode;
            long lngPlannedAmount;
            string strSQL;

            strSalesmanCode = this.SalesmanCode;

            Connect();

            using (SqlCommand cmd = new SqlCommand())
            {
                cmd.Connection = cn;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (row.Cells[6].Value?.ToString() == "計画")
                    {
                        for (int lngCol = 7; lngCol < dataGridView1.Columns.Count; lngCol++)
                        {
                            strCustomerCode = row.Cells[0].Value?.ToString();
                            intMonth = GetMonth(dataGridView1.Columns[lngCol].HeaderText);

                            // 「売上計画」テーブルに格納する「年」は「年度」ではないため、
                            // 表示されている「年度」の「年」への変換が必要
                            intYear = (intMonth >= 4) ? this.TheYear : this.TheYear + 1;

                            intSalesDivisionCode = Convert.ToInt32(row.Cells[3].Value);
                            lngPlannedAmount = Convert.ToInt64(row.Cells[lngCol].Value);

                            strSQL = "INSERT INTO T売上計画 " +
                                     "(売上計画コード, 年, 月, 担当者コード, 顧客コード, 売上区分コード, 計画金額, 登録日時, 登録者コード) " +
                                     "VALUES " +
                                     "(REPLACE(STR(ISNULL(MAX(CAST(売上計画コード AS int)), 0) + 1, 8, 0), ' ', '0'), " +
                                     $"{intYear}, {intMonth}, '{strSalesmanCode}', '{strCustomerCode}', " +
                                     $"{intSalesDivisionCode}, {lngPlannedAmount}, GETDATE(), '{CommonConstants.LoginUserCode}')";

                            Debug.Print(strSQL);

                            // SqlCommandを使用してSQLを実行する (実際には適切なエラーハンドリングも必要です)
                            cmd.CommandText = strSQL;
                            cmd.ExecuteNonQuery();
                        }
                    }
                }

            }
        }


        private void 集計年度_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                TheYear = int.Parse(集計年度.SelectedValue.ToString());


                if (DoUpdate())
                {
                   
                    コマンド更新.Enabled = true;
                    コマンドコピー.Enabled = true;
                    コマンド出力.Enabled = true;

                }
                else
                {
                    MessageBox.Show("エラーが発生しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    コマンド更新.Enabled = false;
                    コマンドコピー.Enabled = false;
                    コマンド出力.Enabled = false;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"集計年月_AfterUpdate エラー: {ex.Message}");
            }
        }

        private void 自社担当者コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 50, 250 }, new string[] { "Display", "Display2" });
            自社担当者コード.Invalidate();
            自社担当者コード.DroppedDown = true;
        }

        private void 自社担当者コード_TextChanged(object sender, EventArgs e)
        {
            if (自社担当者コード.SelectedValue == null)
            {
                自社担当者名.Text = null;
            }
        }

        private void 自社担当者コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            自社担当者名.Text = ((DataRowView)自社担当者コード.SelectedItem)?.Row.Field<String>("Display2")?.ToString();


            try
            {
                strSalesmanCode = string.IsNullOrEmpty(自社担当者コード.SelectedValue?.ToString()) ? null : 自社担当者コード.SelectedValue?.ToString();

                // 集計年度が指定されていないときは何もしない
                if (string.IsNullOrEmpty(TheYear.ToString()))
                {
                    return;
                }


                // リストを更新する
                if (DoUpdate())
                {

                    コマンド更新.Enabled = true;
                    コマンドコピー.Enabled = true;
                    コマンド出力.Enabled = true;

                }
                else
                {
                    MessageBox.Show("エラーが発生しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    コマンド更新.Enabled = false;
                    コマンドコピー.Enabled = false;
                    コマンド出力.Enabled = false;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"エラーが発生しました。\n{ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void F_売上計画_FormClosing(object sender, FormClosingEventArgs e)
        {
            string LoginUserCode = CommonConstants.LoginUserCode;
            LocalSetting test = new LocalSetting();
            test.SavePlace(LoginUserCode, this);
        }




        private void コマンドコピー_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■選択セルをクリップボードへコピーします。";
        }

        private void コマンドコピー_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        
    }



}