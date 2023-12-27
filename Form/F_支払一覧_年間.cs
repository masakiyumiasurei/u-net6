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
    public partial class F_支払一覧_年間 : MidForm
    {
        public string str集計年度;
        public string str支払区分コード;

        int intWindowHeight = 0;
        int intWindowWidth = 0;

        private Control? previousControl;
        private SqlConnection? cn;
        public F_支払一覧_年間()
        {
            InitializeComponent();
        }

        public string PayeeCode
        {
            get
            {
                return dataGridView1.CurrentRow.Cells[0].Value?.ToString();
            }
        }

        public string PayeeName
        {
            get
            {
                return dataGridView1.CurrentRow.Cells[1].Value?.ToString();
            }
        }

        public DateTime PayMonth
        {
            get
            {
                int int1 = (dataGridView1.CurrentCell.ColumnIndex % 12) + 2;  // 月の調整
                int int2 = dataGridView1.CurrentCell.ColumnIndex / 11;        // 年の調整

                int selectedYear = Convert.ToInt32(str集計年度) + int2;
                int selectedMonth = int1;

                return new DateTime(selectedYear, selectedMonth, 1);
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

            using (SqlCommand cmd = new SqlCommand("SP支払年度", cn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();

                // レコードセットを設定
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);

                集計年度.DisplayMember = "支払年度";
                集計年度.ValueMember = "支払年度";
                集計年度.DataSource = dataTable;


            }

            OriginalClass ofn = new OriginalClass();
            ofn.SetComboBox(支払区分コード, "SELECT 買掛区分コード as Value, 買掛区分名 as Display, 番号 FROM M買掛区分 UNION SELECT '', '（全て）', 0 AS 番号 FROM M買掛区分 ORDER BY 番号");
            支払区分コード.SelectedIndex = -1;
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
        public bool DoUpdate()
        {
            if (string.IsNullOrEmpty(集計年度.Text)) return false;

            FunctionClass fn = new FunctionClass();
            fn.DoWait("集計しています...");

            bool result = true;
            try
            {
                SetGrid(str集計年度, str支払区分コード);
                AddTotalRow(dataGridView1);

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

        private bool SetGrid(string yearString, string groupCode = null)
        {
            bool success = false;

            Connect();

            try
            {

                FunctionClass fn = new FunctionClass();


                using (SqlCommand command = new SqlCommand("SP支払一覧_年間", cn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayYear", yearString);
                    command.Parameters.AddWithValue("@GroupCode", fn.Zn(groupCode));

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
            if (e.RowIndex == -1 && e.ColumnIndex > 0)
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


            F_支払 targetform = new F_支払();
            targetform.ShowDialog();
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
                        コピーボタン.Enabled = true;
                    }
                    else
                    {
                        コマンド支払.Enabled = false;
                        コマンド支払先.Enabled = false;
                        // コマンド支払通知.Enabled = false;
                        コマンド印刷.Enabled = false;
                        コマンド入出力.Enabled = false;
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
            targetform.ShowDialog();
        }

        private void コマンド明細参照_Click(object sender, EventArgs e)
        {
            F_支払明細参照 targetform = new F_支払明細参照();
            targetform.ShowDialog();
        }


        private void コマンド印刷_Click(object sender, EventArgs e)
        {

        }

        private void コマンド保守_Click(object sender, EventArgs e)
        {

        }

        private void コマンド支払通知_Click(object sender, EventArgs e)
        {
            string param = $" -user:{CommonConstants.LoginUserName}" +
                           $" -sv:{CommonConstants.ServerInstanceName.Replace(" ", "_")}" +
                           $" -pv:payment,{PayMonth.ToString().Replace(" ", "_")}" +
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


            F_支払 targetform = new F_支払();
            targetform.ShowDialog();
        }


        private void 集計年度_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                str集計年度 = 集計年度.Text;

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
                        コピーボタン.Enabled = true;
                    }
                    else
                    {
                        コマンド支払.Enabled = false;
                        コマンド支払先.Enabled = false;
                        // コマンド支払通知.Enabled = false;
                        コマンド印刷.Enabled = false;
                        コマンド入出力.Enabled = false;
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
                    コピーボタン.Enabled = false;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"集計年月_AfterUpdate エラー: {ex.Message}");
            }
        }

        private void 支払区分コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                str支払区分コード = string.IsNullOrEmpty(支払区分コード.SelectedValue?.ToString()) ? null : 支払区分コード.SelectedValue?.ToString();

                // 集計年度が指定されていないときは何もしない
                if (string.IsNullOrEmpty(str集計年度))
                {
                    return;
                }


                // リストを更新する
                if (DoUpdate())
                {
                    if (dataGridView1.RowCount > 0)
                    {
                        コマンド支払.Enabled = true;
                        コマンド明細参照.Enabled = true;
                        コマンド支払先.Enabled = true;
                        // コマンド支払通知.Enabled = true;
                        コマンド印刷.Enabled = true;
                        コマンド入出力.Enabled = true;
                        コピーボタン.Enabled = true;
                    }
                    else
                    {
                        コマンド支払.Enabled = false;
                        コマンド明細参照.Enabled = false;
                        コマンド支払先.Enabled = false;
                        // コマンド支払通知.Enabled = false;
                        コマンド印刷.Enabled = false;
                        コマンド入出力.Enabled = false;
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
                    コピーボタン.Enabled = false;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"エラーが発生しました。\n{ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}