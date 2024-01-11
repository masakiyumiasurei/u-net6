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
using Microsoft.Data.SqlClient;
using u_net.Public;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace u_net
{
    public partial class F_振込一覧 : MidForm
    {

        public DateTime dtm支払年月 { get; set; }

        public string str支払先コード { get; set; }

        int intWindowHeight = 0;
        int intWindowWidth = 0;

        private Control? previousControl;
        private SqlConnection? cn;
        public F_振込一覧()
        {
            InitializeComponent();
        }


        public string CurrentCode
        {
            get
            {
                if (dataGridView1.CurrentRow.Index != dataGridView1.RowCount - 1)
                {
                    return dataGridView1.CurrentRow.Cells[2].Value?.ToString();
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

            using (SqlCommand cmd = new SqlCommand("SP支払年月", cn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();

                // レコードセットを設定
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);

                支払年月.DisplayMember = "年月";
                支払年月.ValueMember = "支払年月";
                支払年月.DataSource = dataTable;

                支払年月.DrawMode = DrawMode.OwnerDrawFixed;
            }



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
            if (string.IsNullOrEmpty(支払年月.Text)) return false;

            FunctionClass fn = new FunctionClass();
            fn.DoWait("集計しています...");

            bool result = true;
            try
            {
                SetGrid(dtm支払年月);
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

        private bool SetGrid(DateTime PayDay, string SalesmanCode = null)
        {
            bool success = false;

            Connect();

            try
            {

                FunctionClass fn = new FunctionClass();


                string query = "SELECT * FROM BankTransferList(@PayMonth, NULL, NULL, NULL) ORDER BY 支払先名フリガナ";

                using (SqlCommand command = new SqlCommand(query, cn))
                {
                    command.CommandType = CommandType.Text;
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
                    dataGridView1.Columns[0].Visible = false;
                    dataGridView1.Columns[1].Visible = false;
                    dataGridView1.Columns[2].Width = 1100 / twipperdot;
                    dataGridView1.Columns[3].Width = 3800 / twipperdot;
                    dataGridView1.Columns[4].Width = 300 / twipperdot;
                    dataGridView1.Columns[5].Visible = false;

                    for (int col = 6; col <= 7; col++)
                    {
                        dataGridView1.Columns[col].Width = 1400 / twipperdot;
                        dataGridView1.Columns[col].DefaultCellStyle.Format = "#,###,###,##0";
                        dataGridView1.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }

                    dataGridView1.Columns[8].Width = 300 / twipperdot;

                    for (int col = 9; col <= 15; col++)
                    {
                        dataGridView1.Columns[col].Width = 1400 / twipperdot;
                        dataGridView1.Columns[col].DefaultCellStyle.Format = "#,###,###,##0";
                        dataGridView1.Columns[col].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                    }

                    dataGridView1.Columns[16].Visible = false;
                    dataGridView1.Columns[17].Visible = false;
                    dataGridView1.Columns[18].Visible = false;

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
                dataGridView.Rows[rowCount].Cells[2].Value = "(合計)";

                // 列ごとの合計金額を計算し、表示する
                for (int col = 6; col <= 15; col++)
                {
                    if (col == 8) continue;

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

                if (dataGridView.RowCount > 0)
                {
                    dataGridView.Rows[0].Selected = true;
                    dataGridView.FirstDisplayedScrollingRowIndex = 0;
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
            if (dataGridView1.CurrentRow.Index == dataGridView1.RowCount - 1) return;

            switch (dataGridView1.CurrentCell.ColumnIndex)
            {
                case 12:
                    str支払先コード = CurrentCode;
                    F_手形 targetform = new F_手形();
                    targetform.ShowDialog();
                    break;

                case 13:
                    str支払先コード = CurrentCode;
                    F_相殺 targetform2 = new F_相殺();
                    targetform2.ShowDialog();
                    break;

                case 14:
                    str支払先コード = CurrentCode;
                    F_TransferList_Others targetform3 = new F_TransferList_Others();
                    targetform3.ShowDialog();
                    break;

                case 15:
                    str支払先コード = CurrentCode;
                    F_振込繰越 targetform4 = new F_振込繰越();
                    targetform4.ShowDialog();
                    break;
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
                    case Keys.F1:
                        if (this.コマンド抽出.Enabled) コマンド抽出_Click(null, null);
                        break;
                    case Keys.F2:
                        if (this.コマンド検索.Enabled) コマンド検索_Click(null, null);
                        break;
                    case Keys.F3:
                        if (this.コマンド初期化.Enabled) コマンド初期化_Click(null, null);
                        e.Handled = true;
                        break;
                    case Keys.F4:
                        if (this.コマンド更新.Enabled) コマンド更新_Click(null, null);
                        break;
                    case Keys.F5:
                        if (this.コマンド支払先.Enabled) コマンド支払先_Click(null, null);
                        break;
                    case Keys.F6:
                        if (this.コマンド締切.Enabled) コマンド締切_Click(null, null);
                        break;

                    case Keys.F9:
                        if (this.コマンド印刷.Enabled) コマンド印刷_Click(null, null);
                        break;
                    case Keys.F10:
                        if (this.コマンド保守.Enabled) コマンド保守_Click(null, null);
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

                    コマンド支払先.Enabled = true;
                    コマンド締切.Enabled = true;
                    コマンド支払通知.Enabled = true;
                    コマンド印刷.Enabled = true;
                    コマンド入出力.Enabled = true;

                }
                else
                {
                    MessageBox.Show("エラーが発生しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    コマンド支払通知.Enabled = false;
                    コマンド支払先.Enabled = false;
                    コマンド締切.Enabled = false;
                    コマンド印刷.Enabled = false;
                    コマンド入出力.Enabled = false;
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
            targetform.args = CurrentCode;
            targetform.ShowDialog();
        }

        private void コマンド印刷_Click(object sender, EventArgs e)
        {
            string param = $" -user:{CommonConstants.LoginUserName}" +
                           $" -sv:{CommonConstants.ServerInstanceName.Replace(" ", "_")}" +
                           $" -pv:transfer,{dtm支払年月.ToString().Replace(" ", "_")}";
            FunctionClass.GetShell(param);
        }

        private void コマンド保守_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。", "保守コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
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



        private void コマンド支払通知_Click_1(object sender, EventArgs e)
        {
            string param = $" -user:{CommonConstants.LoginUserName}" +
                           $" -sv:{CommonConstants.ServerInstanceName.Replace(" ", "_")}" +
                           $" -pv:payment,{dtm支払年月.ToString().Replace(" ", "_")}" +
                           $",{CurrentCode.Replace(" ", "_")}";
            FunctionClass.GetShell(param);
        }



        private void コマンド締切_Click(object sender, EventArgs e)
        {
            try
            {
                FunctionClass fn = new FunctionClass();


                if (string.IsNullOrEmpty(締切.Text))
                {
                    if (MessageBox.Show($"{dtm支払年月.ToString("yyyy/MM/dd")}の支払を締め切ります。{Environment.NewLine}{Environment.NewLine}よろしいですか？", "締切コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }

                    if (ClosePurchase(dtm支払年月, DateTime.Now, CommonConstants.LoginUserCode, CommonConstants.LoginUserFullName))
                    {

                        int idx = 支払年月.SelectedIndex;

                        Connect();

                        using (SqlCommand cmd = new SqlCommand("SP支払年月", cn))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            SqlDataReader reader = cmd.ExecuteReader();

                            // レコードセットを設定
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);

                            支払年月.DisplayMember = "年月";
                            支払年月.ValueMember = "支払年月";
                            支払年月.DataSource = dataTable;

                            支払年月.DrawMode = DrawMode.OwnerDrawFixed;
                        }

                        支払年月.SelectedIndex = idx;

                        //締切.Text = ((DataRowView)支払年月.SelectedItem)?.Row.Field<String>("締切")?.ToString();

                        MessageBox.Show($"{dtm支払年月.ToString("yyyy/MM/dd")}の支払を締め切りました。", "締切コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("エラーが発生したため、処理は取り消されました。", "締切コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    if (MessageBox.Show($"{dtm支払年月.ToString("yyyy/MM/dd")}の支払は既に締め切られています。{Environment.NewLine}解除しますか？{Environment.NewLine}{Environment.NewLine}" +
                                       $"＜注意＞{Environment.NewLine}" +
                                       $"・解除する月が最後の支払月かどうか確認してください。{Environment.NewLine}" +
                                       $"・最後の支払月以前の支払月を解除した場合、以降の動作は保証できません。{Environment.NewLine}" +
                                       $"・解除後元に戻すことはできません。", "締切コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }

                    if (CancelClose(dtm支払年月))
                    {

                        int idx = 支払年月.SelectedIndex;

                        Connect();

                        using (SqlCommand cmd = new SqlCommand("SP支払年月", cn))
                        {
                            cmd.CommandType = System.Data.CommandType.StoredProcedure;

                            SqlDataReader reader = cmd.ExecuteReader();

                            // レコードセットを設定
                            DataTable dataTable = new DataTable();
                            dataTable.Load(reader);

                            支払年月.DisplayMember = "年月";
                            支払年月.ValueMember = "支払年月";
                            支払年月.DataSource = dataTable;

                            支払年月.DrawMode = DrawMode.OwnerDrawFixed;
                        }

                        支払年月.SelectedIndex = idx;

                        //締切.Text = ((DataRowView)支払年月.SelectedItem)?.Row.Field<String>("締切")?.ToString();

                        MessageBox.Show($"{dtm支払年月.ToString("yyyy/MM/dd")}の支払締めを解除しました。", "締切コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("エラーが発生したため、処理は取り消されました。", "締切コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private bool ClosePurchase(DateTime closeMonth, DateTime closeDate, string operateUserCode, string operateUserFullName)
        {
            try
            {
                Connect();

                using (SqlCommand objCommand = new SqlCommand("usp_ClosePurchase", cn))
                {
                    objCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@CloseMonth", closeMonth);
                    objCommand.Parameters.AddWithValue("@CloseDate", closeDate);
                    objCommand.Parameters.AddWithValue("@OperateDate", DateTime.Now);
                    objCommand.Parameters.AddWithValue("@OperateUserCode", operateUserCode);
                    objCommand.Parameters.AddWithValue("@OperateUserFullName", operateUserFullName);

                    objCommand.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        private bool CancelClose(DateTime closeMonth)
        {
            try
            {
                Connect();

                using (SqlCommand objCommand = new SqlCommand("usp_CancelClosePurchase", cn))
                {
                    objCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    objCommand.Parameters.AddWithValue("@CloseMonth", closeMonth);

                    objCommand.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }



        private void 支払年月_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dtm支払年月 = Convert.ToDateTime(支払年月.SelectedValue);
                締切.Text = ((DataRowView)支払年月.SelectedItem)?.Row.Field<String>("締切")?.ToString();

                // リストを更新する
                if (DoUpdate())
                {

                    コマンド支払先.Enabled = true;
                    コマンド締切.Enabled = true;
                    コマンド支払通知.Enabled = true;
                    コマンド印刷.Enabled = true;
                    コマンド入出力.Enabled = true;

                }
                else
                {
                    MessageBox.Show("エラーが発生しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    コマンド支払先.Enabled = false;
                    コマンド締切.Enabled = false;
                    コマンド支払通知.Enabled = false;
                    コマンド印刷.Enabled = false;
                    コマンド入出力.Enabled = false;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"支払年月_AfterUpdate エラー: {ex.Message}");
            }
        }

        private void 支払年月_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 100, 50 }, new string[] { "年月", "締切" });
            支払年月.Invalidate();
            支払年月.DroppedDown = true;
        }

        private void 支払年月_TextChanged(object sender, EventArgs e)
        {
            if (支払年月.SelectedValue == null)
            {
                締切.Text = null;
            }
        }

        private void F_振込一覧_FormClosing(object sender, FormClosingEventArgs e)
        {
            string LoginUserCode = CommonConstants.LoginUserCode;
            LocalSetting test = new LocalSetting();
            test.SavePlace(LoginUserCode, this);
        }
    }
}