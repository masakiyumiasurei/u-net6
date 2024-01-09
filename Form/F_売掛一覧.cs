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
using Microsoft.Identity.Client.NativeInterop;
using Newtonsoft.Json.Linq;

namespace u_net
{
    public partial class F_売掛一覧 : MidForm
    {

        public string str顧客名 { get; set; }
        public string str担当者名 { get; set; }
        public long lng完了指定 { get; set; }
        public string str締日 { get; set; }
        public DateTime dtm支払日開始 { get; set; }
        public DateTime dtm支払日終了 { get; set; }
        public bool salEXT { get; set; }
        public string strSortItem { get; set; }
        public bool blnSortCondition { get; set; }

        private int intSelectionMode; // グリッドの選択モード        
        private int intWindowHeightMax;
        private int intWindowWidthMax;
        private int intKeyCode; // 保存キーコード
        private int intButton;


        int intWindowHeight = 0;
        int intWindowWidth = 0;

        private Control? previousControl;
        private SqlConnection? cn;
        public F_売掛一覧()
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



        public int AcceptanceNotificationReceived
        {
            get
            {
                return string.IsNullOrEmpty(dataGridView1.SelectedRows[0].Cells[11].Value?.ToString()) ? 0 : -1;
            }
        }

        public string CustomerCode
        {
            get
            {
                return dataGridView1.SelectedRows[0].Cells[0].Value?.ToString();
            }
        }


        public DateTime SalesMonth
        {
            get
            {
                return Convert.ToDateTime(Nz(売掛年月.Text));
            }
        }


        // Nz関数の代用
        private string Nz(object value)
        {
            return value == null ? "" : value.ToString();
        }

        // IsNull関数の代用
        private bool IsNull(object value)
        {
            return value == null || Convert.IsDBNull(value) || string.IsNullOrEmpty((string?)value);
        }


        private void Form_Load(object sender, EventArgs e)
        {

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

            dataGridView2.AllowUserToResizeColumns = true;
            dataGridView2.Font = new Font("MS ゴシック", 10);
            dataGridView2.DefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 210, 255);
            dataGridView2.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView2.GridColor = Color.FromArgb(230, 230, 230);
            dataGridView2.ColumnHeadersDefaultCellStyle.Font = new Font("MS ゴシック", 9);
            dataGridView2.DefaultCellStyle.Font = new Font("MS ゴシック", 10);
            dataGridView2.DefaultCellStyle.ForeColor = Color.Black;

            dataGridView2.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView2.AllowUserToAddRows = false;
            dataGridView2.AllowUserToDeleteRows = false;
            dataGridView2.ReadOnly = true;
            dataGridView2.Enabled = false;

            // 一番左の選択列を非表示に
            dataGridView2.RowHeadersVisible = false;

            dataGridView2.ClearSelection();



            Connect();

            using (SqlCommand cmd = new SqlCommand("SP売掛一覧_売掛年月", cn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();

                // レコードセットを設定
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);


                売掛年月.DisplayMember = "売掛年月";
                売掛年月.ValueMember = "売掛年月";
                売掛年月.DataSource = dataTable;



            }

            売掛年月.DrawMode = DrawMode.OwnerDrawFixed;



            SetInitial();


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


        private void SetInitial()
        {
            lng完了指定 = 0;
            radioButton3.Checked = true;
            str締日 = "";
        }


        public bool DoUpdate()
        {
            if (string.IsNullOrEmpty(売掛年月.Text)) return false;

            FunctionClass fn = new FunctionClass();
            fn.DoWait("集計しています...");

            bool result = true;
            try
            {
                Filtering(SalesMonth, str顧客名, str担当者名, lng完了指定, str締日, dtm支払日開始, dtm支払日終了);
                GetTotal();

            }
            catch (Exception ex)
            {
                result = false;
                MessageBox.Show(this.Name + "_DoUpdate - " + ex.Message);
            }

            fn.WaitForm.Close();

            return result;
        }


        private bool Filtering(DateTime salesMonth, string customerName, string coverUserName, long completedCode, string closedDay, DateTime fixedDateS, DateTime fixedDateE)
        {
            bool success = false;

            Connect();

            try
            {

                FunctionClass fn = new FunctionClass();


                using (SqlCommand command = new SqlCommand("SP売掛一覧_顧客別_抽出", cn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@SalesMonth", salesMonth);
                    command.Parameters.AddWithValue("@CustomerName", fn.Zn(customerName));
                    command.Parameters.AddWithValue("@CoverUserName", fn.Zn(coverUserName));
                    command.Parameters.AddWithValue("@CompletedCode", fn.Zn(completedCode));
                    command.Parameters.AddWithValue("@ClosedDay", fn.Zn(closedDay));
                    command.Parameters.AddWithValue("@FixedDateS", (fixedDateS == DateTime.MinValue) ? null : fixedDateS);
                    command.Parameters.AddWithValue("@FixedDateE", (fixedDateE == DateTime.MinValue) ? null : fixedDateE);



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
                    dataGridView1.Columns[0].Width = 1050 / twipperdot;
                    dataGridView1.Columns[1].Width = 3950 / twipperdot;
                    dataGridView1.Columns[2].Width = 600 / twipperdot;
                    dataGridView1.Columns[3].Width = 1400 / twipperdot;
                    dataGridView1.Columns[4].Width = 1240 / twipperdot;
                    dataGridView1.Columns[5].Width = 1240 / twipperdot;
                    dataGridView1.Columns[6].Width = 1240 / twipperdot;
                    dataGridView1.Columns[7].Width = 1400 / twipperdot;
                    dataGridView1.Columns[8].Width = 330 / twipperdot;
                    dataGridView1.Columns[9].Width = 450 / twipperdot;
                    dataGridView1.Columns[10].Width = 1300 / twipperdot;
                    dataGridView1.Columns[11].Width = 330 / twipperdot;
                    dataGridView1.Columns[12].Width = 1300 / twipperdot;
                    dataGridView1.Columns[13].Visible = false;


                    for (int col = 3; col <= 7; col++)
                    {
                        dataGridView1.Columns[col].DefaultCellStyle.Format = "#,###,###,##0";
                        dataGridView1.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (string.IsNullOrEmpty(row.Cells[8].Value?.ToString()) && DateTime.Parse(row.Cells[12].Value.ToString()) < DateTime.Today)
                        {
                            row.DefaultCellStyle.BackColor = Color.Pink;
                        }
                        else
                        {
                            row.DefaultCellStyle.BackColor = Color.White;
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

        private void GetTotal()
        {


            dataGridView2.Columns.Clear(); // 既存の列をクリア

            for (int i = 0; i < 5; i++)
            {
                dataGridView2.Columns.Add($"Column{i + 1}", $"Column{i + 1}");
                dataGridView2.Columns[i].DefaultCellStyle.Format = "#,###,###,##0";
                dataGridView2.Columns[i].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            // DataGridView2 の列の表題を設定
            dataGridView2.Columns[0].HeaderText = "売上金額";
            dataGridView2.Columns[1].HeaderText = "回収金額";
            dataGridView2.Columns[2].HeaderText = "手数料金額";
            dataGridView2.Columns[3].HeaderText = "相殺金額";
            dataGridView2.Columns[4].HeaderText = "残高金額";



            // 各列の合計値を計算
            int[] sumValues = new int[5];

            for (int i = 3; i <= 7; i++) // 3列目以降を対象に
            {
                sumValues[i - 3] = dataGridView1.Rows.Cast<DataGridViewRow>().Sum(row => Convert.ToInt32(row.Cells[i].Value));
            }

            // DataGridView2 に合計値を挿入
            dataGridView2.Rows.Add(new object[] { sumValues[0], sumValues[1], sumValues[2], sumValues[3], sumValues[4] });



            MyApi myapi = new MyApi();
            int xSize, ySize, intpixel, twipperdot;

            //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
            intpixel = myapi.GetLogPixel();
            twipperdot = myapi.GetTwipPerDot(intpixel);

            intWindowHeight = this.Height;
            intWindowWidth = this.Width;

            //0列目はaccessでは行ヘッダのため、ずらす
            dataGridView2.Columns[0].Width = 1400 / twipperdot;
            dataGridView2.Columns[1].Width = 1240 / twipperdot;
            dataGridView2.Columns[2].Width = 1240 / twipperdot;
            dataGridView2.Columns[3].Width = 1240 / twipperdot;
            dataGridView2.Columns[4].Width = 1400 / twipperdot;

            dataGridView2.ClearSelection();


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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // ヘッダー行でない場合
            {
                if (string.IsNullOrEmpty(dataGridView1.SelectedRows[0].Cells[8].Value?.ToString()))
                {
                    F_入金 targetform = new F_入金();
                    targetform.ShowDialog();
                }
                else
                {
                    F_売掛明細 targetform = new F_売掛明細();
                    targetform.ShowDialog();
                }


            }

        }


        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (string.IsNullOrEmpty(row.Cells[8].Value?.ToString()) && DateTime.Parse(row.Cells[12].Value.ToString()) < DateTime.Today)
                {
                    row.DefaultCellStyle.BackColor = Color.Pink;
                }
                else
                {
                    row.DefaultCellStyle.BackColor = Color.White;
                }



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
                        if (this.コマンド詳細.Enabled) コマンド詳細_Click(null, null);
                        break;
                    case Keys.F7:
                        if (this.コマンド顧客.Enabled) コマンド顧客_Click(null, null);
                        break;
                    case Keys.F8:
                        if (this.コマンド売掛資料.Enabled) コマンド売掛資料_Click(null, null);
                        break;

                    case Keys.F9:
                        if (this.コマンド印刷.Enabled) コマンド印刷_Click(null, null);
                        break;
                    case Keys.F11:
                        if (this.コマンド入出力.Enabled) コマンド入出力_Click(null, null);
                        break;
                    case Keys.F10:
                        if (this.コマンド検収.Enabled) コマンド検収_Click(null, null);
                        break;
                    case Keys.F12:
                        if (this.コマンド終了.Enabled) コマンド終了_Click(null, null);
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
            this.Close();
        }

        private void コマンド抽出_Click(object sender, EventArgs e)
        {
            F_売掛一覧_抽出 targetform = new F_売掛一覧_抽出();
            targetform.ShowDialog();
        }

        private void コマンド検索_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。", "検索コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド初期化_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(売掛年月.Text))
                    return;

                SetInitial();

                if (!DoUpdate())
                {
                    MessageBox.Show("初期化に失敗しました。", "初期化コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"コマンド初期化_Click エラー: {ex.Message}");
            }
        }

        private void コマンド更新_Click(object sender, EventArgs e)
        {
            try
            {

                if (string.IsNullOrEmpty(売掛年月.Text))
                {
                    MessageBox.Show("売掛年月を指定してください。", "更新コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    売掛年月.Focus();
                    return;
                }

                // 確認
                DialogResult result = MessageBox.Show("表示データを最新の情報に更新します。\nよろしいですか？", "更新コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.No)
                    return;

                if (DoUpdate())
                {
                    コマンド印刷.Enabled = dataGridView1.RowCount > 0;
                }
                else
                {
                    MessageBox.Show("更新に失敗しました。", "更新コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"コマンド更新_Click エラー: {ex.Message}");
            }
        }

        private void コマンド入金_Click(object sender, EventArgs e)
        {
            F_入金 form = new F_入金();
            form.ShowDialog();
        }

        private void コマンド詳細_Click(object sender, EventArgs e)
        {
            F_売掛明細 targetform = new F_売掛明細();
            targetform.ShowDialog();
        }

        private void コマンド顧客_Click(object sender, EventArgs e)
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

        private void コマンド売掛資料_Click(object sender, EventArgs e)
        {
            IReport paoRep = ReportCreator.GetPreview();

            paoRep.LoadDefFile("../../../Reports/売上一覧表_売掛資料.prepd");

            //最大行数
            int maxRow = 51;
            //現在の行
            int CurRow = 0;
            //行数
            int RowCount = maxRow;
            if (dataGridView1.RowCount > 0)
            {
                RowCount = dataGridView1.RowCount - 1;
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

                //ヘッダー
                paoRep.Write("売掛年月", SalesMonth.ToString("yyyy年M月") ?? " ");
                paoRep.Write("売上件数", dataGridView1.RowCount.ToString() ?? " ");
                paoRep.Write("売上金額合計", string.Format("{0:#,0}", dataGridView2.Rows[0].Cells[0].Value) != "" ? string.Format("{0:#,0}", dataGridView2.Rows[0].Cells[0].Value) : " ");

                //フッダー
                paoRep.Write("現在日時", now.ToString("yyyy年M月d日"));
                paoRep.Write("ページ表示", (page + "/" + maxPage + " ページ").ToString());

                //明細
                for (var i = 0; i < maxRow; i++)
                {
                    if (CurRow >= dataGridView1.RowCount-1) break;

                    DataGridViewRow targetRow = dataGridView1.Rows[CurRow];

                    paoRep.Write("行番号", (CurRow + 1).ToString(), i + 1);
                    paoRep.Write("顧客コード", targetRow.Cells["顧客コード"].Value.ToString() != "" ? targetRow.Cells["顧客コード"].Value.ToString() : " ", i + 1);
                    paoRep.Write("顧客名", targetRow.Cells["顧客名"].Value.ToString() != "" ? targetRow.Cells["顧客名"].Value.ToString() : " ", i + 1);
                    paoRep.Write("売上金額", string.Format("{0:#,0}", targetRow.Cells["売上金額"].Value) != "" ? string.Format("{0:#,0}", targetRow.Cells["売上金額"].Value) : " ", i + 1);
                   



                    paoRep.z_Objects.SetObject("顧客名", i + 1);
                    lenB = Encoding.Default.GetBytes(targetRow.Cells["顧客名"].Value.ToString()).Length;
                    if (26 < lenB)
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

        private void コマンド印刷_Click(object sender, EventArgs e)
        {
            IReport paoRep = ReportCreator.GetPreview();

            paoRep.LoadDefFile("../../../Reports/売上一覧表.prepd");

            //最大行数
            int maxRow = 51;
            //現在の行
            int CurRow = 0;
            //行数
            int RowCount = maxRow;
            if (dataGridView1.RowCount > 0)
            {
                RowCount = dataGridView1.RowCount - 1;
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

                //ヘッダー
                paoRep.Write("売掛年月", SalesMonth.ToString("yyyy年M月") ?? " ");
                paoRep.Write("売上件数", dataGridView1.RowCount.ToString() ?? " ");
                paoRep.Write("売上金額合計", string.Format("{0:#,0}", dataGridView2.Rows[0].Cells[0].Value) != "" ? string.Format("{0:#,0}", dataGridView2.Rows[0].Cells[0].Value) : " ");

                //フッダー
                paoRep.Write("現在日時", now.ToString("yyyy年M月d日"));
                paoRep.Write("ページ表示", (page + "/" + maxPage + " ページ").ToString());

                //明細
                for (var i = 0; i < maxRow; i++)
                {
                    if (CurRow >= dataGridView1.RowCount-1) break;

                    DataGridViewRow targetRow = dataGridView1.Rows[CurRow];

                    paoRep.Write("行番号", (CurRow + 1).ToString(), i + 1);
                    paoRep.Write("顧客コード", targetRow.Cells["顧客コード"].Value.ToString() != "" ? targetRow.Cells["顧客コード"].Value.ToString() : " ", i + 1);
                    paoRep.Write("顧客名", targetRow.Cells["顧客名"].Value.ToString() != "" ? targetRow.Cells["顧客名"].Value.ToString() : " ", i + 1);
                    paoRep.Write("担当者名", targetRow.Cells["担当者名"].Value.ToString() != "" ? targetRow.Cells["担当者名"].Value.ToString() : " ", i + 1);
                    paoRep.Write("売上金額", string.Format("{0:#,0}", targetRow.Cells["売上金額"].Value) != "" ? string.Format("{0:#,0}", targetRow.Cells["売上金額"].Value) : " ", i + 1);
                    paoRep.Write("支払日", targetRow.Cells["支払日"].Value.ToString() != "" ? targetRow.Cells["支払日"].Value.ToString() : " ", i + 1);



                    paoRep.z_Objects.SetObject("顧客名", i + 1);
                    lenB = Encoding.Default.GetBytes(targetRow.Cells["顧客名"].Value.ToString()).Length;
                    if (22 < lenB)
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



        private void コマンド入出力_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。", "入出力コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド検収_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                try
                {
                    Connect();

                    using (SqlCommand command = new SqlCommand("UpdateAcceptanceNotification", cn))
                    {
                        command.CommandType = System.Data.CommandType.StoredProcedure;

                        command.Parameters.AddWithValue("@CustomerCode", CustomerCode);
                        command.Parameters.AddWithValue("@SalesMonth", SalesMonth);
                        command.Parameters.AddWithValue("@Received", AcceptanceNotificationReceived == 0 ? -1 : 0);
                        command.Parameters.AddWithValue("@CreateUserCode", CommonConstants.LoginUserCode);
                        command.Parameters.AddWithValue("@CreateUserFullName", CommonConstants.LoginUserFullName);

                        command.ExecuteNonQuery();
                    }


                    if (AcceptanceNotificationReceived == 0)
                    {
                        dataGridView1.SelectedRows[0].Cells[11].Value = "■";
                    }
                    else
                    {
                        dataGridView1.SelectedRows[0].Cells[11].Value = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("更新できませんでした。\n" + ex.Message, "検収通知コマンド", MessageBoxButtons.OK);
                }

            }
        }


        private void 売掛年月_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DoUpdate())
            {
                コマンド抽出.Enabled = true;
                コマンド初期化.Enabled = true;
                コマンド詳細.Enabled = true;
                コマンド売掛資料.Enabled = 0 < dataGridView1.RowCount;
                コマンド印刷.Enabled = 0 < dataGridView1.RowCount;
                コマンド更新.Enabled = true;
            }
            else
            {
                MessageBox.Show("更新に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private void 売掛年月_KeyDown(object sender, KeyEventArgs e)
        {

            string strCode;

            if (e.KeyCode == Keys.Return)
            {
                strCode = 売掛年月.Text;
                if (string.IsNullOrEmpty(strCode))
                    return;

                strCode = strCode.Replace("0", " ").Replace("/", "/ ").Trim();
                if (strCode != 売掛年月.Text)
                    売掛年月.Text = strCode;
            }

        }

        private void 売掛年月_KeyPress(object sender, KeyPressEventArgs e)
        {

            if (e.KeyChar == ' ')
            {
                ComboBox comboBox = sender as ComboBox;
                if (comboBox != null)
                {
                    comboBox.DroppedDown = true;
                }
                e.Handled = true;
            }

        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                lng完了指定 = 1;

                if (string.IsNullOrEmpty(売掛年月.Text)) return;

                if (!DoUpdate())
                {
                    MessageBox.Show("更新に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                lng完了指定 = 2;

                if (string.IsNullOrEmpty(売掛年月.Text)) return;

                if (!DoUpdate())
                {
                    MessageBox.Show("更新に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked)
            {
                lng完了指定 = 0;

                if (string.IsNullOrEmpty(売掛年月.Text)) return;

                if (!DoUpdate())
                {
                    MessageBox.Show("更新に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
        }

        private void 売掛年月_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 100, 70 }, new string[] { "売掛年月", "未回収" });
            売掛年月.Invalidate();
            売掛年月.DroppedDown = true;
        }

        
    }
}