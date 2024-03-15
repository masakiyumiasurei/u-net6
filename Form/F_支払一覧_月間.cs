using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;
using Pao.Reports;
using u_net.Public;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace u_net
{
    public partial class F_支払一覧_月間 : MidForm
    {

        public DateTime dtm集計年月 { get; set; }


        int intWindowHeight = 0;
        int intWindowWidth = 0;

        private Control? previousControl;
        private SqlConnection? cn;
        public F_支払一覧_月間()
        {
            InitializeComponent();
        }


        public string PayeeCode
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

        public string PayeeName
        {
            get
            {
                if (dataGridView1.CurrentRow.Index != dataGridView1.RowCount - 1)
                {
                    return dataGridView1.CurrentRow.Cells[1].Value?.ToString();
                }
                else
                {
                    return "";
                }
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

            System.Type dgvtype = typeof(DataGridView);
            System.Reflection.PropertyInfo dgvPropertyInfo = dgvtype.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            dgvPropertyInfo.SetValue(dataGridView1, true, null);


            foreach (Control control in Controls)
            {
                control.PreviewKeyDown += OriginalClass.ValidateCheck;
            }

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

            using (SqlCommand cmd = new SqlCommand("SP集計年月", cn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();

                // レコードセットを設定
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);

                集計年月.DisplayMember = "年月";
                集計年月.ValueMember = "集計年月";
                集計年月.DataSource = dataTable;

                //集計年月.SelectedIndex = -1;
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
            if (string.IsNullOrEmpty(集計年月.Text)) return true;

            FunctionClass fn = new FunctionClass();
            fn.DoWait("集計しています...");

            bool result = true;
            try
            {
                SetGrid(dtm集計年月);


                if (dataGridView1.RowCount > 0)
                {
                    AddTotalRow(dataGridView1);

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

        private bool SetGrid(DateTime PayDay, string SalesmanCode = null)
        {
            bool success = false;

            Connect();

            try
            {

                FunctionClass fn = new FunctionClass();


                using (SqlCommand command = new SqlCommand("SP支払一覧_月間", cn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayMonth", PayDay);


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
                    dataGridView1.Columns[0].Width = 1100 / twipperdot;
                    dataGridView1.Columns[1].Width = 3500 / twipperdot;
                    dataGridView1.Columns[14].Width = 1500 / twipperdot;



                    for (int col = 2; col <= 14; col++)
                    {
                        dataGridView1.Columns[col].Width = 1300 / twipperdot;
                        dataGridView1.Columns[col].DefaultCellStyle.Format = "#,###,###,##0";
                        dataGridView1.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }



                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return success;
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
            AddTotalRow(dataGridView1);

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1 && e.ColumnIndex >= 0)
            {
                dataGridView1.Rows.RemoveAt(dataGridView1.Rows.Count - 1);
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

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {

            DialogResult result = MessageBox.Show("月間一覧上の金額は誤差が発生している可能性があります。\n続行しますか？", "支払コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return;
            }

            F_支払 targetform = new F_支払();
            targetform.MdiParent = this.MdiParent;
            targetform.FormClosed += (s, args) => { this.Enabled = true; };
            this.Enabled = false;

            targetform.Show();

        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            //Form_KeyDown(sender, e);
        }


        private void Form_KeyDown(object sender, KeyEventArgs e)
        {

            try
            {

                switch (e.KeyCode)
                {
                    case Keys.Return:
                        SelectNextControl(ActiveControl, true, true, true, true);
                        break;
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
                        if (this.コマンド支払先.Enabled) コマンド支払先_Click(null, null);
                        break;
                    case Keys.F6:
                        if (this.コマンド支払.Enabled) コマンド支払_Click(null, null);
                        break;
                    case Keys.F7:
                        if (this.コマンド明細参照.Enabled) コマンド明細参照_Click(null, null);
                        break;
                    case Keys.F8:
                        if (this.コマンド支払通知.Enabled) コマンド支払通知_Click(null, null);
                        break;

                    case Keys.F9:
                        if (this.コマンド印刷.Enabled) コマンド印刷_Click(null, null);
                        break;
                    case Keys.F10:
                        if (this.コマンド出力.Enabled) コマンド出力_Click(null, null);
                        break;
                    case Keys.F11:
                        if (this.コマンド入出力.Enabled) コマンド入出力_Click(null, null);
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

        private void コマンド抽出_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。", "抽出コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
                    if (dataGridView1.RowCount > 0)
                    {
                        コマンド支払先.Enabled = true;
                        コマンド明細参照.Enabled = true;
                        コマンド支払.Enabled = true;
                        // コマンド支払通知.Enabled = true;
                        コマンド印刷.Enabled = true;
                        コマンド入出力.Enabled = true;
                        コマンド出力.Enabled = true;
                        コピーボタン.Enabled = true;
                    }
                    else
                    {
                        コマンド支払.Enabled = false;
                        コマンド支払先.Enabled = false;
                        // コマンド支払通知.Enabled = false;
                        コマンド印刷.Enabled = false;
                        コマンド入出力.Enabled = false;
                        コマンド出力.Enabled = false;
                        コピーボタン.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("エラーが発生しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    コマンド支払.Enabled = false;
                    コマンド支払先.Enabled = false;
                    // コマンド支払通知.Enabled = false;
                    コマンド印刷.Enabled = false;
                    コマンド入出力.Enabled = false;
                    コマンド出力.Enabled = false;
                    コピーボタン.Enabled = false;
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

        private void コマンド支払先_Click(object sender, EventArgs e)
        {
            F_仕入先 targetform = new F_仕入先();
            targetform.args = PayeeCode;
            targetform.MdiParent = this.MdiParent;
            targetform.FormClosed += (s, args) => { this.Enabled = true; };
            this.Enabled = false;

            targetform.Show();

        }

        private void コマンド明細参照_Click(object sender, EventArgs e)
        {
            F_支払明細参照 targetform = new F_支払明細参照();
            targetform.MdiParent = this.MdiParent;
            targetform.FormClosed += (s, args) => { this.Enabled = true; };
            this.Enabled = false;

            targetform.Show();

        }


        private void コマンド印刷_Click(object sender, EventArgs e)
        {
            IReport paoRep = ReportCreator.GetPreview();

            paoRep.LoadDefFile("../../../Reports/支払一覧表_月間.prepd");

            //最大行数
            int maxRow = 27;
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

            DataGridViewRow totalRow = dataGridView1.Rows[dataGridView1.Rows.Count - 1];

            //描画すべき行がある限りページを増やす
            while (RowCount > 0)
            {
                RowCount -= maxRow;

                paoRep.PageStart();

                //ヘッダー
                DateTime dtm集計年月 = DateTime.ParseExact(集計年月.Text, "yyyy/MM", null);

                paoRep.Write("タイトル", dtm集計年月.ToString("yyyy年M月") + "支払一覧表");

                for (var i = 1; i <= 12; i++)
                {
                    paoRep.Write("項目" + (i).ToString(), string.Format("{0:#,0}", totalRow.Cells[i + 1].Value) != "" ? string.Format("{0:#,0}", totalRow.Cells[i + 1].Value) : " ");

                    paoRep.z_Objects.SetObject("項目" + (i).ToString());
                    lenB = Encoding.Default.GetBytes(string.Format("{0:#,0}", totalRow.Cells[i + 1].Value)).Length;
                    if (8 < lenB)
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 6;
                    }
                    else
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 9;
                    }

                }
                paoRep.Write("支払合計金額", string.Format("{0:#,0}", totalRow.Cells[14].Value) != "" ? string.Format("{0:#,0}", totalRow.Cells[14].Value) : " ");

                paoRep.z_Objects.SetObject("支払合計金額");
                lenB = Encoding.Default.GetBytes(string.Format("{0:#,0}", totalRow.Cells[14].Value)).Length;
                if (9 < lenB)
                {
                    paoRep.z_Objects.z_Text.z_FontAttr.Size = 6;
                }
                else
                {
                    paoRep.z_Objects.z_Text.z_FontAttr.Size = 9;
                }

                //フッダー
                paoRep.Write("出力日時", now.ToString("yyyy/MM/dd HH:mm:ss"));
                paoRep.Write("ページ", (page + "/" + maxPage + " ページ").ToString());

                //明細
                for (var i = 0; i < maxRow; i++)
                {
                    if (CurRow >= dataGridView1.RowCount) break;

                    DataGridViewRow targetRow = dataGridView1.Rows[CurRow];

                    paoRep.Write("行番号", (CurRow + 1).ToString(), i + 1);
                    paoRep.Write("支払先名", targetRow.Cells["支払先名"].Value.ToString() != "" ? targetRow.Cells["支払先名"].Value.ToString() : " ", i + 1);

                    paoRep.Write("材料費(SPM)", string.Format("{0:#,0}", targetRow.Cells[2].Value) != "" ? string.Format("{0:#,0}", targetRow.Cells[2].Value) : " ", i + 1);
                    paoRep.Write("材料費(OEM)", string.Format("{0:#,0}", targetRow.Cells[3].Value) != "" ? string.Format("{0:#,0}", targetRow.Cells[3].Value) : " ", i + 1);
                    paoRep.Write("材料費(CMPS)", string.Format("{0:#,0}", targetRow.Cells[4].Value) != "" ? string.Format("{0:#,0}", targetRow.Cells[4].Value) : " ", i + 1);
                    paoRep.Write("材料費(加工)", string.Format("{0:#,0}", targetRow.Cells[5].Value) != "" ? string.Format("{0:#,0}", targetRow.Cells[5].Value) : " ", i + 1);
                    paoRep.Write("材料費(その他)", string.Format("{0:#,0}", targetRow.Cells[6].Value) != "" ? string.Format("{0:#,0}", targetRow.Cells[6].Value) : " ", i + 1);
                    paoRep.Write("材料費(外注費)", string.Format("{0:#,0}", targetRow.Cells[7].Value) != "" ? string.Format("{0:#,0}", targetRow.Cells[7].Value) : " ", i + 1);
                    paoRep.Write("派遣費", string.Format("{0:#,0}", targetRow.Cells[8].Value) != "" ? string.Format("{0:#,0}", targetRow.Cells[8].Value) : " ", i + 1);
                    paoRep.Write("人件費", string.Format("{0:#,0}", targetRow.Cells[9].Value) != "" ? string.Format("{0:#,0}", targetRow.Cells[9].Value) : " ", i + 1);
                    paoRep.Write("製造費(電気料)", string.Format("{0:#,0}", targetRow.Cells[10].Value) != "" ? string.Format("{0:#,0}", targetRow.Cells[10].Value) : " ", i + 1);
                    paoRep.Write("製造費(製造費)", string.Format("{0:#,0}", targetRow.Cells[11].Value) != "" ? string.Format("{0:#,0}", targetRow.Cells[11].Value) : " ", i + 1);
                    paoRep.Write("一般管理販売費", string.Format("{0:#,0}", targetRow.Cells[12].Value) != "" ? string.Format("{0:#,0}", targetRow.Cells[12].Value) : " ", i + 1);
                    paoRep.Write("営業外費用", string.Format("{0:#,0}", targetRow.Cells[13].Value) != "" ? string.Format("{0:#,0}", targetRow.Cells[13].Value) : " ", i + 1);
                    paoRep.Write("合計", string.Format("{0:#,0}", targetRow.Cells[14].Value) != "" ? string.Format("{0:#,0}", targetRow.Cells[14].Value) : " ", i + 1);




                    paoRep.z_Objects.SetObject("支払先名", i + 1);
                    lenB = Encoding.Default.GetBytes(targetRow.Cells["支払先名"].Value.ToString()).Length;
                    if (24 < lenB)
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 6;
                    }
                    else
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 9;
                    }


                    paoRep.z_Objects.SetObject("合計", i + 1);
                    lenB = Encoding.Default.GetBytes(targetRow.Cells[14].Value.ToString()).Length;
                    if (9 < lenB)
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 6;
                    }
                    else
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 9;
                    }

                    CurRow++;


                }

                page++;

                paoRep.PageEnd();

            }



            paoRep.Output();
        }

        private void コマンド支払通知_Click(object sender, EventArgs e)
        {
            string param = $" -user:{CommonConstants.LoginUserName}" +
                           $" -sv:{CommonConstants.ServerInstanceName.Replace(" ", "_")}" +
                           $" -pv:payment,{dtm集計年月.ToString().Replace(" ", "_")}" +
                           $",{PayeeCode.Replace(" ", "_")}";
            FunctionClass.GetShell(param);
        }

        private void コピーボタン_Click(object sender, EventArgs e)
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
        private void コマンド入出力_Click(object sender, EventArgs e)
        {

            DialogResult result = MessageBox.Show("選択セルの内容をクリップボードへコピーします。\nよろしいですか？", "入出力コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return;
            }

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
        private void コマンド支払_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("月間一覧上の金額は誤差が発生している可能性があります。\n続行しますか？", "支払コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (result == DialogResult.No)
            {
                return;
            }

            F_支払 targetform = new F_支払();
            targetform.MdiParent = this.MdiParent;
            targetform.FormClosed += (s, args) => { this.Enabled = true; };
            this.Enabled = false;

            targetform.Show();

        }


        private void 集計年月_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dtm集計年月 = Convert.ToDateTime(集計年月.SelectedValue);

                // リストを更新する
                if (DoUpdate())
                {
                    if (dataGridView1.RowCount > 0)
                    {
                        コマンド支払先.Enabled = true;
                        コマンド明細参照.Enabled = true;
                        コマンド支払.Enabled = true;
                        // コマンド支払通知.Enabled = true;
                        コマンド印刷.Enabled = true;
                        コマンド入出力.Enabled = true;
                        コマンド出力.Enabled = true;
                        コピーボタン.Enabled = true;
                    }
                    else
                    {
                        コマンド支払.Enabled = false;
                        コマンド支払先.Enabled = false;
                        // コマンド支払通知.Enabled = false;
                        コマンド印刷.Enabled = false;
                        コマンド入出力.Enabled = false;
                        コマンド出力.Enabled = false;
                        コピーボタン.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("エラーが発生しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    コマンド支払先.Enabled = false;
                    コマンド明細参照.Enabled = false;
                    コマンド支払.Enabled = false;
                    // コマンド支払通知.Enabled = false;
                    コマンド印刷.Enabled = false;
                    コマンド入出力.Enabled = false;
                    コマンド出力.Enabled = false;
                    コピーボタン.Enabled = false;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"集計年月_AfterUpdate エラー: {ex.Message}");
            }
        }

        private void F_支払一覧_月間_FormClosing(object sender, FormClosingEventArgs e)
        {
            string LoginUserCode = CommonConstants.LoginUserCode;
            LocalSetting test = new LocalSetting();
            test.SavePlace(LoginUserCode, this);
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
    }
}