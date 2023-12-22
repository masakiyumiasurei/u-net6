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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace u_net
{
    public partial class F_見積管理 : MidForm
    {
        public string str見積コード開始;
        public string str見積コード終了;
        public DateTime dtm見積日開始;
        public DateTime dtm見積日終了;
        public string str担当者名;
        public string str顧客コード;
        public string str顧客名;
        public string str件名;
        public long lng確定指定;
        public long lng承認指定;
        public long lng削除指定;
        public string strSearchCode;
        public bool estEXT;

        private int intSelectionMode;
        private int intWindowHeight;
        private int intWindowWidth;
        private int intWindowHeightMax;
        private int intWindowWidthMax;
        private int intKeyCode;
        private int intButton;
        private DataGridView gridobject;
        public object objParent;

        private Control? previousControl;
        private SqlConnection? cn;

        public F_見積管理()
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
        public string CurrentCode()
        {
            if (gridobject.CurrentRow != null && gridobject.Rows.Count > 0)
            {
                return gridobject.Rows[gridobject.CurrentRow.Index].Cells[0].Value?.ToString() ?? string.Empty;
            }

            return string.Empty;
        }

        //現在選択されているデータの版数を取得する
        public string CurrentEdition()
        {
            if (gridobject.CurrentRow != null && gridobject.Rows.Count > 0)
            {
                return gridobject.Rows[gridobject.CurrentRow.Index].Cells[1].Value?.ToString() ?? string.Empty;
            }

            return string.Empty;
        }

        public long DataCount()
        {
            //return gridobject.Rows - gridobject.FixedRows;
            return 1;
        }

        public bool IsApproved()
        {
            object value = gridobject.Rows[gridobject.CurrentRow.Index].Cells[8].Value;

            string valueAsString = (value ?? "").ToString();

            return !string.IsNullOrEmpty(valueAsString);
        }

        public bool IsCompleted()
        {
            object value = gridobject.Rows[gridobject.CurrentRow.Index].Cells[9].Value;

            string valueAsString = (value ?? "").ToString();

            return !string.IsNullOrEmpty(valueAsString);
        }

        public bool IsDeleted()
        {
            object value = gridobject.Rows[gridobject.CurrentRow.Index].Cells[10].Value;

            string valueAsString = (value ?? "").ToString();

            return !string.IsNullOrEmpty(valueAsString);
        }

        private void FunctionKeyDown(object sender, KeyEventArgs e, int Shift)
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
                        if (コマンド見積.Enabled)
                        {
                            コマンド見積.Focus();
                            コマンド見積_Click(sender, e);
                        }
                        break;
                    case Keys.F6:
                        if (コマンド見積書.Enabled)
                        {
                            コマンド見積書.Focus();
                            コマンド見積書_Click(sender, e);
                        }
                        break;
                    case Keys.F7:
                        break;
                    case Keys.F8:
                        break;
                    case Keys.F9:
                        if (コマンド更新.Enabled)
                        {
                            コマンド更新.Focus();
                            コマンド更新_Click(sender, e);
                        }
                        break;
                    case Keys.F10:
                        if (コマンド印刷.Enabled)
                        {
                            コマンド印刷.Focus();
                            コマンド印刷_Click(sender, e);
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
                        if (ActiveControl == this.一覧)
                        {
                            F_見積 form = new F_見積();
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

        private bool GridDrawn()
        {
            try
            {
                int lngi;

                bool result = false;

                if (gridobject.EnableHeadersVisualStyles)
                {
                    var dgvType = gridobject.GetType();
                    var pi = dgvType.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                    pi.SetValue(gridobject, true, null);
                }

                gridobject.SuspendLayout();

                if (gridobject.RowCount <= gridobject.ColumnCount)
                {
                    result = true;
                    goto Bye_GridDrawn;
                }

                lngi = 1;
                while (lngi <= gridobject.RowCount - gridobject.ColumnCount)
                {
                    gridobject.Rows[lngi - 1].Cells[0].Value = lngi;

                    gridobject.Rows[lngi - 1].Cells[1].Style.BackColor = Color.FromArgb(250, 250, 150);
                    gridobject.Rows[lngi - 1].Cells[2].Style.BackColor = Color.FromArgb(250, 250, 150);

                    lngi++;
                }

                gridobject.CurrentCell = gridobject.Rows[gridobject.ColumnCount].Cells[gridobject.ColumnCount];

                gridobject.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                gridobject.ClearSelection();

                result = true;

            Bye_GridDrawn:
                return result;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GridDrawn - " + ex.Message);
                return false;
            }
        }

        private bool GridSet()
        {
            Connect();

            string strSQL = null;
            string strFilter = null;
            string strSource;
            string[] arr1;
            object var1;
            string str1;

            bool result = false;
            DataTable dt = new DataTable();

            try
            {
                //見積コード指定
                if (!string.IsNullOrEmpty(this.str見積コード開始))
                {
                    string filter = $"(見積コード BETWEEN '{this.str見積コード開始}' AND '{this.str見積コード終了}')";
                }

                // 見積日指定
                if (Convert.ToDouble(this.dtm見積日開始) != 0 && Convert.ToDouble(this.dtm見積日終了) != 0)
                {
                    string dateFilter = $"見積日 BETWEEN '{this.dtm見積日開始}' AND '{this.dtm見積日終了}'";
                    FunctionClass.WhereString(strFilter, dateFilter);
                }

                // 担当者名指定
                if (!string.IsNullOrEmpty(this.str担当者名))
                {
                    string nameFilter = $"担当者名 LIKE '%{this.str担当者名}%'";
                    FunctionClass.WhereString(strFilter, nameFilter);
                }

                // 顧客コード指定
                if (!string.IsNullOrEmpty(this.str顧客コード))
                {
                    string customerFilter = $"顧客コード = '{this.str顧客コード}'";
                    FunctionClass.WhereString(strFilter, customerFilter);
                }
                else
                {
                    // 顧客名指定
                    if (!string.IsNullOrEmpty(this.str顧客名))
                    {
                        strSource = this.str顧客名;
                        arr1 = strSource.Split(' ');

                        //foreach (var var1 in arr1)
                        //{
                        //    str1 = var1.ToString();
                        //    if (!string.IsNullOrEmpty(str1))
                        //    {
                        //        string nameFilter = $"顧客名 LIKE '%{str1}%'";
                        //        FunctionClass.WhereString(strFilter, nameFilter);
                        //    }
                        //}
                    }
                }

                // 件名指定（マルチワード対応）
                if (!string.IsNullOrEmpty(this.str件名))
                {
                    strSource = this.str件名;
                    arr1 = strSource.Split();

                    //foreach (var var1 in arr1)
                    //{
                    //    str1 = var1.ToString();
                    //    if (!string.IsNullOrEmpty(str1))
                    //    {
                    //        string subjectFilter = $"件名 LIKE '%{str1}%'";
                    //        FunctionClass.WhereString(strFilter, subjectFilter);
                    //    }
                    //}
                }

                // 確定指定
                switch (lng確定指定)
                {
                    case 1:
                        strFilter = FunctionClass.WhereString(strFilter, "確定 IS NULL");
                        break;
                    case 2:
                        strFilter = FunctionClass.WhereString(strFilter, "確定 IS NOT NULL");
                        break;
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

                if (string.IsNullOrEmpty(strFilter))
                {
                    strSQL = "SELECT * FROM V見積管理 ORDER BY 見積コード DESC";
                }
                else
                {
                    strSQL = $"SELECT * FROM V見積管理 WHERE {strFilter} ORDER BY 見積コード DESC";
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
                    estEXT = true;
                    gridobject.DataSource = null;
                }
                else
                {
                    gridobject.DataSource = dt;
                    estEXT = false;
                }
                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{GetType().Name}_GridSet - {ex.GetType().Name} : {ex.Message}");
            }
            finally
            {
                this.表示件数.Text = this.DataCount().ToString();
            }

            return result;
        }


        public override void SearchCode(string codeString)
        {
            strSearchCode = codeString;

            str見積コード開始 = strSearchCode;
            str見積コード終了 = strSearchCode;
            dtm見積日開始 = DateTime.MinValue;
            dtm見積日終了 = DateTime.MinValue;

            if (!DoUpdate())
            {
                MessageBox.Show("エラーが発生しました。");
            }
        }

        private void SetFilter1()
        {
            str見積コード開始 = "";
            str見積コード終了 = "";
            dtm見積日開始 = DateTime.MinValue;
            dtm見積日終了 = DateTime.MinValue;
            str担当者名 = "";
            str顧客名 = "";
            lng確定指定 = 0;
            lng承認指定 = 0;
            lng削除指定 = 1;
        }

        private void InitializeFilter()
        {
            this.str見積コード開始 = "";
            this.str見積コード終了 = "";
            this.dtm見積日開始 = DateTime.Now.AddDays(-7);
            this.dtm見積日終了 = DateTime.Now;
            this.str担当者名 = "";
            this.str顧客名 = "";
            this.lng確定指定 = 0;
            this.lng承認指定 = 0;
            this.lng削除指定 = 1;
        }

        private void Form_Load(object sender, EventArgs e)
        {
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

            gridobject = this.一覧;

            // DataGridViewの設定
            gridobject.AllowUserToResizeColumns = true;
            gridobject.Font = new Font("BIZ UDPゴシック", 10);
            gridobject.DefaultCellStyle = new DataGridViewCellStyle
            {
                BackColor = Color.FromArgb(210, 210, 255),
                ForeColor = Color.Black,
                SelectionBackColor = Color.FromArgb(210, 210, 255),
                SelectionForeColor = Color.Black
            };
            gridobject.GridColor = Color.FromArgb(230, 230, 230);
            gridobject.CellBorderStyle = DataGridViewCellBorderStyle.Single;
            gridobject.BackgroundColor = Color.FloralWhite;
            gridobject.MultiSelect = false;
            gridobject.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridobject.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            gridobject.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            gridobject.RowHeadersVisible = false;
            gridobject.RowTemplate.Height = 10 + (int)gridobject.Font.Size;
            gridobject.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            gridobject.ScrollBars = ScrollBars.Both;
            gridobject.DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            gridobject.RowsDefaultCellStyle.Font = new Font("BIZ UDPゴシック", 10);
            gridobject.RowTemplate.Height = (int)(gridobject.RowTemplate.Height + 10 * gridobject.Font.Size);

            gridobject.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            int intSelectionMode = (int)gridobject.SelectionMode;
            gridobject.ScrollBars = ScrollBars.Both;

            // Update the list
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

            InitializeFilter();
            DoUpdate();
            fn.WaitForm.Close();
            Cleargrid(一覧);
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            try
            {
                一覧.Height += this.Height - intWindowHeight;
                intWindowHeight = this.Height; // Save the new height

                一覧.Width += this.Width - intWindowWidth;
                intWindowWidth = this.Width; // Save the new width
            }
            catch (Exception ex)
            {
                Debug.Print($"{Name}_Form_Resize - {ex.HResult} : {ex.Message}");
            }
        }

        public bool DoUpdate()
        {
            bool result = false;

            try
            {
                gridobject.SuspendLayout();

                if (GridSet() && GridDrawn())
                {
                    return true;
                }
                else
                {
                    Debug.WriteLine($"{nameof(F_見積管理)}_DoUpdate - An error occurred during update.");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{nameof(F_見積管理)}_DoUpdate - {ex.GetType().Name}: {ex.Message}");
                return false;
            }
            finally
            {
                gridobject.ResumeLayout();
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

        //ダブルクリックで仕入先フォームを開く　仕入先コードを渡す
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            //if (e.Button != MouseButtons.Left) return; // 左ボタンのダブルクリック以外は無視

            if (e.RowIndex >= 0) // ヘッダー行でない場合
            {
                //string selectedData = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(); // 1列目のデータを取得

                F_仕入先 targetform = new F_仕入先();

                //targetform.args = selectedData;
                targetform.ShowDialog();
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // ヘッダー行でない場合
            {
                //dataGridView1.ClearSelection();
                //dataGridView1.Rows[e.RowIndex].Selected = true;
            }
        }

        private bool sorting;

        private void dataGridView1_Sorted(object sender, EventArgs e)
        {
            if (!sorting)
            {
                sorting = true;

                // DataGridViewのソートが完了したら、先頭行を選択する
                //if (dataGridView1.Rows.Count > 0)
                //{
                //    Cleargrid(dataGridView1);
                //}

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
            一覧.Focus();
            this.Close();
        }

        private void コマンド抽出_Click(object sender, EventArgs e)
        {
            try
            {
                this.一覧.Focus();
                objParent = this;

                // 見積管理_抽出フォームを開く
                F_見積管理_抽出 form = new F_見積管理_抽出();
                form.Show();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void コマンド入出力_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。", "入出力コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド初期化_Click(object sender, EventArgs e)
        {
            try
            {
                this.SuspendLayout();
                this.一覧.Focus();

                FunctionClass fn = new FunctionClass();
                fn.DoWait("初期化しています...");
                InitializeFilter();

                // DoUpdate メソッドが更新できない場合はメッセージを表示
                if (!DoUpdate())
                {
                    MessageBox.Show("エラーが発生しました。", "初期化コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                // 実行中フォームを閉じる
                this.Close();
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                this.Invalidate();
            }
        }

        private void コマンド全表示_Click(object sender, EventArgs e)
        {
            try
            {
                this.SuspendLayout();
                this.一覧.Focus();

                FunctionClass fn = new FunctionClass();
                fn.DoWait("初期化しています...");
                InitializeFilter();

                // DoUpdate メソッドが更新できない場合はメッセージを表示
                if (!DoUpdate())
                {
                    MessageBox.Show("エラーが発生しました。", "初期化コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                // 実行中フォームを閉じる
                this.Close();
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                this.Invalidate();
            }
        }

        private void コマンド更新_Click(object sender, EventArgs e)
        {
            try
            {
                this.SuspendLayout();
                this.一覧.Focus();

                // 更新中の待機画面を表示
                FunctionClass fn = new FunctionClass();
                fn.DoWait("更新しています...");

                // DoUpdate メソッドが更新できない場合はメッセージを表示
                if (!DoUpdate())
                {
                    MessageBox.Show("更新できませんでした。", "更新コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                // 実行中フォームを閉じる
                this.Close();
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                this.Invalidate();
            }
        }

        private void コマンド検索_Click(object sender, EventArgs e)
        {
            一覧.Focus();
            objParent = this;
            //F_検索コード form = new F_検索コード();
            //form.ShowDialog();
        }

        private void Form_KeyDown(object sender, KeyEventArgs e, int Shift)
        {
            FunctionKeyDown(sender, e, Shift);
        }

        private bool ascending = true;

        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //if (dataGridView1.Columns[e.ColumnIndex].Name == "仕入先名")
            //{
            //    if (ascending)
            //    {
            //        dataGridView1.Sort(dataGridView1.Columns["仕入先名フリガナ"], System.ComponentModel.ListSortDirection.Ascending);
            //    }
            //    else
            //    {
            //        dataGridView1.Sort(dataGridView1.Columns["仕入先名フリガナ"], System.ComponentModel.ListSortDirection.Descending);
            //    }
            //    ascending = !ascending;
            //}
        }

        private void コマンド見積_Click(object sender, EventArgs e)
        {
            一覧.Focus();
            F_見積 form = new F_見積();
            form.ShowDialog();
        }

        private void コマンド見積書_Click(object sender, EventArgs e)
        {
            try
            {
                一覧.Focus();

                Connect();

                // 現在の接続先サーバーによって server を設定
                string server = "";
                if (cn.ConnectionString.Contains(",1436") ||
                    cn.ConnectionString.Contains("\\unet_secondary"))
                {
                    server = "secondary";
                }
                else
                {
                    server = "primary";
                }

                // コマンドライン引数の作成
                string param = $" -sv:{server.Replace(" ", "_")} -pv:estimate,{this.CurrentCode().TrimEnd().Replace(" ", "_")},{this.CurrentEdition().Replace(" ", "_")}";
                param = $" -user:{CommonConstants.LoginUserName}{param}";

                // プロセスを起動
                using (Process process = new Process())
                {
                    process.StartInfo.FileName = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles), "Uinics", "Uinics U-net 3 Client", "unetc.exe");
                    process.StartInfo.Arguments = param;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;

                    process.Start();
                    process.WaitForExit();
                }
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                Console.WriteLine("Error: " + ex.Message);
                MessageBox.Show("エラーのため実行できません。", "見積書コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void コマンド印刷_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。", "印刷コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form_Unload(object sender, FormClosingEventArgs e)
        {
            LocalSetting localSettingInstance = new LocalSetting();
            localSettingInstance.SavePlace(CommonConstants.LoginUserCode, this);
        }

        private void リンクボタン_Click(object sender, EventArgs e)
        {
            try
            {
                // エラーを無視して続行
                this.一覧.Focus();

                Connect();

                // 現在のコードを取得
                string strDocumentCode = this.CurrentCode();

                // 本データがグループに登録済みかどうかを判断する
                switch (FunctionClass.DetectGroupMember(cn, strDocumentCode))
                {
                    case 0:
                        // グループに登録済みでない場合
                        //F_グループ form = new F_グループ(strDocumentCode);
                        //form.ShowDialog();
                        break;
                    case 1:
                        // グループに登録済みの場合
                        //F_リンク form1 = new F_リンク(strDocumentCode);
                        //form1.ShowDialog();
                        break;
                    case -1:
                        // エラーのため実行できない場合
                        MessageBox.Show("エラーのため実行できません。", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        break;
                }
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void 一覧_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                // エラーを無視して続行
                //if ((intButton & 1) <= 0 || gridobject.MouseRow == 0)
                //{
                //    // 保存マウスボタン初期化
                //    intButton = 1;
                //    return;
                //}

                // "見積" フォームを開く
                //OpenForm("見積", $"{this.CurrentCode},{this.CurrentEdition}");
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void 一覧_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            gridobject.Refresh();
        }

        private void 一覧_CellLeave(object sender, DataGridViewCellEventArgs e)
        {
            gridobject.Refresh();
        }

        private void 一覧_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                // マウスボタン確保
                //intButton = e.Button;

                // 並べ替え前の表示
                if (一覧.HitTest(e.X, e.Y).RowIndex == -1)
                {
                    // ヘッダーがクリックされた場合
                    一覧.SelectionMode = DataGridViewSelectionMode.ColumnHeaderSelect;
                    一覧.Columns[一覧.HitTest(e.X, e.Y).ColumnIndex].Selected = true;
                }
                else
                {
                    // セルがクリックされた場合
                    一覧.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    一覧.Rows[一覧.HitTest(e.X, e.Y).RowIndex].Selected = true;
                }

                一覧.Refresh();
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void 一覧_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && 一覧.HitTest(e.X, e.Y).RowIndex == -1)
            {
                try
                {
                    一覧.Sort(一覧.Columns[一覧.HitTest(e.X, e.Y).ColumnIndex], ListSortDirection.Ascending);
                }
                catch (Exception ex)
                {
                    // エラーが発生した場合の処理
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
    }
}