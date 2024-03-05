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
    public partial class F_支払明細参照 : MidForm
    {
        public DateTime dtm集計年月 { get; set; }

        public string str支払区分コード { get; set; }

        public string str支払先コード { get; set; }
        public string OrderCode
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

        public int OrderEdition
        {
            get
            {
                if (dataGridView1.CurrentRow.Index != dataGridView1.RowCount - 1)
                {
                    return Int32.Parse(dataGridView1.CurrentRow.Cells[1].Value?.ToString());
                }
                else
                {
                    return -1;
                }
            }
        }

        int intWindowHeight = 0;
        int intWindowWidth = 0;

        private Control? previousControl;
        private SqlConnection? cn;
        public F_支払明細参照()
        {
            InitializeComponent();
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


            }

            OriginalClass ofn = new OriginalClass();
            ofn.SetComboBox(支払区分コード, "SELECT 買掛区分コード as Value, 買掛区分名 as Display, 番号 FROM M買掛区分 UNION SELECT '', '（全て）', 0 AS 番号 FROM M買掛区分 ORDER BY 番号");
            支払区分コード.SelectedIndex = -1;


            bool blnNotLoaded1 = false;
            bool blnNotLoaded2 = false;

            if (!Application.OpenForms.OfType<Form>().Any(f => f.Name == "F_支払一覧_年間"))
            {
                blnNotLoaded1 = true;
            }
            else if (!Application.OpenForms.OfType<Form>().Any(f => f.Name == "F_支払一覧_月間"))
            {
                blnNotLoaded2 = true;
            }

            // フォームの読み込み状況によって動作を切り替える
            
            if (!blnNotLoaded1 && blnNotLoaded2)
            {
                F_支払一覧_年間? frmCall1 = Application.OpenForms.OfType<F_支払一覧_年間>().FirstOrDefault();
                // 呼び出し元フォームから抽出条件値を取得する
                if (frmCall1 != null)
                {
                    dtm集計年月 = frmCall1.PayMonth;
                    str支払区分コード = frmCall1.groupCode;
                    str支払先コード = frmCall1.PayeeCode;
                    集計年月.SelectedValue = $"{frmCall1.PayMonth.Year}/{frmCall1.PayMonth.Month:D2}";
                    支払先コード.Text = str支払先コード;
                    支払先名.Text = frmCall1.PayeeName;
                }
            }
            else if (blnNotLoaded1 && !blnNotLoaded2)
            {
                F_支払一覧_月間? frmCall2 = Application.OpenForms.OfType<F_支払一覧_月間>().FirstOrDefault();
                // 呼び出し元フォームから抽出条件値を取得する
                if (frmCall2 != null)
                {
                    dtm集計年月 = frmCall2.dtm集計年月;
                    str支払区分コード = "";
                    str支払先コード = frmCall2.PayeeCode;
                    集計年月.SelectedValue = $"{frmCall2.dtm集計年月.Year}/{frmCall2.dtm集計年月.Month:D2}";
                    支払先コード.Text = str支払先コード;
                    支払先名.Text = frmCall2.PayeeName;
                }
            }
            else
            {
                // 支払一覧フォームが起動していないときは閉じる
                MessageBox.Show("[支払一覧]画面が起動していない状態では実行できません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                Close();
            }

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

            if (DoUpdate())
            {
                if (dataGridView1.RowCount > 0)
                {
                    コマンド発注参照.Enabled = true;
                    コマンド支払先参照.Enabled = true;
                    コマンド印刷.Enabled = true;
                    コマンド入出力.Enabled = true;
                }
                else
                {
                    コマンド発注参照.Enabled = false;
                    コマンド支払先参照.Enabled = false;
                    コマンド印刷.Enabled = false;
                    コマンド入出力.Enabled = false;
                }
            }
            else
            {
                MessageBox.Show("エラーが発生しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                コマンド発注参照.Enabled = false;
                コマンド支払先参照.Enabled = false;
                コマンド印刷.Enabled = false;
                コマンド入出力.Enabled = false;
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

        private void F_製品管理_FormClosing(object sender, FormClosingEventArgs e)
        {
            string LoginUserCode = CommonConstants.LoginUserCode;
            LocalSetting test = new LocalSetting();
            test.SavePlace(LoginUserCode, this);
        }

        public bool DoUpdate()
        {
            if (string.IsNullOrEmpty(集計年月.Text)) return true;
            if (string.IsNullOrEmpty(支払先コード.Text)) return true;

            FunctionClass fn = new FunctionClass();
            fn.DoWait("集計しています...");

            bool result = true;
            try
            {
                SetGrid(dtm集計年月, str支払先コード, str支払区分コード);
                //SetTotal(dataGridView1);

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


        private bool SetGrid(DateTime TargetMonth, string PayeeCode, string groupCode = null)
        {
            bool success = false;

            Connect();

            try
            {

                FunctionClass fn = new FunctionClass();


                using (SqlCommand command = new SqlCommand("SP支払明細参照", cn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PayMonth", TargetMonth);
                    command.Parameters.AddWithValue("@PayeeCode", PayeeCode);
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
                    dataGridView1.Columns[0].Width = 1400 / twipperdot;
                    dataGridView1.Columns[1].Width = 450 / twipperdot;
                    dataGridView1.Columns[2].Width = 450 / twipperdot;
                    dataGridView1.Columns[3].Width = 1100 / twipperdot;
                    dataGridView1.Columns[4].Width = 3600 / twipperdot;
                    dataGridView1.Columns[5].Width = 3600 / twipperdot;

                    dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200);
                    dataGridView1.Columns[1].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200);
                    dataGridView1.Columns[2].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200);

                    for (int col = 6; col <= 7; col++)
                    {
                        dataGridView1.Columns[col].Width = 1400 / twipperdot;
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

        private static void SetTotal(DataGridView dataGridView)
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
                for (int col = 6; col <= 7; col++)
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


        private void SetPayeeInfo(string payeeCode)
        {

            // SQLクエリを構築
            string query = "SELECT * FROM M仕入先 WHERE 仕入先コード = @PayeeCode AND 無効日時 IS NULL";

            try
            {
                Connect();

                using (SqlCommand cmd = new SqlCommand(query, cn))
                {
                    // パラメータを追加
                    cmd.Parameters.AddWithValue("@PayeeCode", payeeCode);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // データが存在する場合
                            string payeeName = $"{reader["仕入先名"]} {reader["仕入先名2"]}";
                            支払先名.Text = payeeName;

                        }
                        else
                        {
                            // データが存在しない場合
                            支払先名.Text = null;

                        }
                    }
                }

            }
            catch (Exception ex)
            {

                支払先名.Text = null;

            }
        }











        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            //SetTotal(dataGridView1);

            
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


            F_発注 targetform = new F_発注();
            targetform.args = OrderCode + "," + OrderEdition;
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
                        //datagidviewの並び替えが行われるため
                        e.Handled = true;
                        break;
                    case Keys.F4:
                        if (this.コマンド更新.Enabled) コマンド更新_Click(null, null);
                        break;
                    case Keys.F5:
                        if (this.コマンド発注参照.Enabled) コマンド発注参照_Click(null, null);
                        break;
                    case Keys.F6:
                        if (this.コマンド支払先参照.Enabled) コマンド支払先参照_Click(null, null);
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
                    case Keys.Return:
                        if (this.ActiveControl == this.dataGridView1)
                        {
                            if (dataGridView1.SelectedRows.Count > 0)
                            {

                                F_発注 targetform = new F_発注();
                                targetform.args = OrderCode + "," + OrderEdition;
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






        private void コマンド抽出_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。", "抽出コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
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


        private void コマンド印刷_Click(object sender, EventArgs e)
        {
            MessageBox.Show("このコマンドは定義されていないため、使用できません。", "印刷コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
        private void コマンド支払先参照_Click(object sender, EventArgs e)
        {
            F_仕入先 targetform = new F_仕入先();
            targetform.args = str支払先コード;
            targetform.ShowDialog();
        }





        private void コマンド発注参照_Click(object sender, EventArgs e)
        {
            F_発注 targetform = new F_発注();
            targetform.args = OrderCode + "," + OrderEdition;
            targetform.ShowDialog();
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
                        コマンド発注参照.Enabled = true;
                        コマンド支払先参照.Enabled = true;
                        コマンド印刷.Enabled = true;
                        コマンド入出力.Enabled = true;
                    }
                    else
                    {
                        コマンド発注参照.Enabled = false;
                        コマンド支払先参照.Enabled = false;
                        コマンド印刷.Enabled = false;
                        コマンド入出力.Enabled = false;
                    }
                }
                else
                {
                    MessageBox.Show("エラーが発生しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    コマンド発注参照.Enabled = false;
                    コマンド支払先参照.Enabled = false;
                    コマンド印刷.Enabled = false;
                    コマンド入出力.Enabled = false;
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine($"コマンド更新_Click エラー: {ex.Message}");
            }
        }


        private void 集計年月_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                dtm集計年月 = Convert.ToDateTime(集計年月.SelectedValue);

                if (string.IsNullOrEmpty(str支払先コード)) return;

                // リストを更新する
                if (DoUpdate())
                {
                    if (dataGridView1.RowCount > 0)
                    {
                        コマンド発注参照.Enabled = true;
                        コマンド支払先参照.Enabled = true;
                        コマンド印刷.Enabled = true;
                        コマンド入出力.Enabled = true;
                    }
                    else
                    {
                        コマンド発注参照.Enabled = false;
                        コマンド支払先参照.Enabled = false;
                        コマンド印刷.Enabled = false;
                        コマンド入出力.Enabled = false;
                    }
                }
                else
                {
                    //MessageBox.Show("エラーが発生しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    コマンド発注参照.Enabled = false;
                    コマンド支払先参照.Enabled = false;
                    コマンド印刷.Enabled = false;
                    コマンド入出力.Enabled = false;
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
                if (dtm集計年月 == DateTime.MinValue)
                {
                    return;
                }


                // リストを更新する
                if (DoUpdate())
                {
                    if (dataGridView1.RowCount > 0)
                    {
                        コマンド発注参照.Enabled = true;
                        コマンド支払先参照.Enabled = true;
                        コマンド印刷.Enabled = true;
                        コマンド入出力.Enabled = true;
                    }
                    else
                    {
                        コマンド発注参照.Enabled = false;
                        コマンド支払先参照.Enabled = false;
                        コマンド印刷.Enabled = false;
                        コマンド入出力.Enabled = false;
                    }
                }
                else
                {
                    //MessageBox.Show("エラーが発生しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    コマンド発注参照.Enabled = false;
                    コマンド支払先参照.Enabled = false;
                    コマンド印刷.Enabled = false;
                    コマンド入出力.Enabled = false;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"エラーが発生しました。\n{ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void 支払先コード_Validated(object sender, EventArgs e)
        {
            try
            {
                str支払先コード = 支払先コード.Text;
                SetPayeeInfo(str支払先コード);

                // 集計年度が指定されていないときは何もしない
                if (dtm集計年月 == DateTime.MinValue)
                {
                    return;
                }


                // リストを更新する
                if (DoUpdate())
                {
                    if (dataGridView1.RowCount > 0)
                    {
                        コマンド発注参照.Enabled = true;
                        コマンド支払先参照.Enabled = true;
                        コマンド印刷.Enabled = true;
                        コマンド入出力.Enabled = true;
                    }
                    else
                    {
                        コマンド発注参照.Enabled = false;
                        コマンド支払先参照.Enabled = false;
                        コマンド印刷.Enabled = false;
                        コマンド入出力.Enabled = false;
                    }
                }
                else
                {
                    //MessageBox.Show("エラーが発生しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    コマンド発注参照.Enabled = false;
                    コマンド支払先参照.Enabled = false;
                    コマンド印刷.Enabled = false;
                    コマンド入出力.Enabled = false;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show($"エラーが発生しました。\n{ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private F_検索 SearchForm;
        private void 支払先検索ボタン_Click(object sender, EventArgs e)
        {
            SearchForm = new F_検索();
            SearchForm.FilterName = "支払先名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                string SelectedCode = SearchForm.SelectedCode;

                支払先コード.Text = SelectedCode;
                支払先コード_Validated(sender, e);

            }
        }

        private void 支払先参照ボタン_Click(object sender, EventArgs e)
        {
            F_仕入先 targetform = new F_仕入先();
            targetform.args = 支払先コード.Text;
            targetform.ShowDialog();
        }


        private void 支払先コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                TextBox textBox = (TextBox)sender;
                string formattedCode = textBox.Text.Trim().PadLeft(8, '0');

                if (formattedCode != textBox.Text || string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.Text = formattedCode;
                    支払先コード_Validated(sender, e);
                }
            }
        }

        private void 支払先コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                支払先検索ボタン_Click(sender, e);
                e.Handled = true; // イベントの処理が完了したことを示す
            }
        }

        private void 支払先コード_DoubleClick(object sender, EventArgs e)
        {
            支払先検索ボタン_Click(sender, e);
        }

        private void 支払先参照ボタン_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■支払先データを参照します。";
        }

        private void 支払先参照ボタン_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        
    }
}