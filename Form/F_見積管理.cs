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

        public string CurrentCode
        {
            // 現在選択されているデータのコードを取得する
            get
            {

                if (dataGridView1.SelectedRows.Count > 0)
                {
                    return dataGridView1.SelectedRows[0].Cells[0].Value?.ToString();
                }
                else
                {
                    // エラーが発生した場合の処理を記述
                    return "";
                }
            }
        }
        public string? CurrentEdition
        {
            // 現在選択されているデータの版数を取得する
            get
            {
                if (dataGridView1.SelectedRows.Count > 0)
                {
                    return dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                }
                else
                {
                    return "";
                }
            }
        }
        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        //現在選択されているデータのコードを取得する        

        public long DataCount()
        {
            return dataGridView1.RowCount; ;
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
                        if (ActiveControl == this.dataGridView1)
                        {
                            F_見積 form = new F_見積();
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

            string strSQL = "";
            string strFilter = "";
            string strSource;
            string[] arr1;
            //object var1;
            string str1;
            string tmpstr = "";
            bool result = false;

            try
            {
                //見積コード指定
                if (!string.IsNullOrEmpty(this.str見積コード開始))
                {
                    strFilter = FunctionClass.WhereString(strFilter, $"(見積コード BETWEEN '{this.str見積コード開始}' AND '{this.str見積コード終了}')");
                }

                // 見積日指定
                if (this.dtm見積日開始 != DateTime.MinValue && this.dtm見積日終了 != DateTime.MinValue)
                {
                    tmpstr = $"見積日 BETWEEN '{this.dtm見積日開始}' AND '{this.dtm見積日終了}'";
                    strFilter = FunctionClass.WhereString(strFilter, tmpstr);
                }

                // 担当者名指定
                if (!string.IsNullOrEmpty(this.str担当者名))
                {
                    tmpstr = $"担当者名 LIKE '%{this.str担当者名}%'";
                    strFilter = FunctionClass.WhereString(strFilter, tmpstr);
                }

                // 顧客名指定
                if (!string.IsNullOrEmpty(this.str顧客名))
                {
                    arr1 = this.str顧客名.Split(' ');

                    foreach (var var1 in arr1)
                    {
                        str1 = var1.ToString();
                        if (!string.IsNullOrEmpty(str1))
                        {
                            strFilter = FunctionClass.WhereString(strFilter, $"顧客名 LIKE '%{str1}%'");
                        }
                    }
                }


                // 件名指定（マルチワード対応）
                if (!string.IsNullOrEmpty(this.str件名))
                {
                    arr1 = this.str件名.Split();

                    foreach (var var1 in arr1)
                    {
                        str1 = var1.ToString();
                        if (!string.IsNullOrEmpty(str1))
                        {
                            strFilter = FunctionClass.WhereString(strFilter, $"件名 LIKE '%{str1}%'");
                        }
                    }
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

                Connect();
                DataGridUtils.SetDataGridView(cn, strSQL, this.dataGridView1);

                MyApi myapi = new MyApi();
                int xSize, ySize, intpixel, twipperdot;

                ////1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
                intpixel = myapi.GetLogPixel();
                twipperdot = myapi.GetTwipPerDot(intpixel);

                intWindowHeight = this.Height;
                intWindowWidth = this.Width;

                dataGridView1.Columns[0].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200); // 薄い黄色
                dataGridView1.Columns[1].DefaultCellStyle.BackColor = Color.FromArgb(255, 255, 200); // 薄い黄色

                dataGridView1.Columns[0].Width = 1500 / twipperdot; //1150
                dataGridView1.Columns[1].Width = 300 / twipperdot;
                dataGridView1.Columns[2].Width = 1300 / twipperdot;
                dataGridView1.Columns[3].Width = 2500 / twipperdot;
                dataGridView1.Columns[4].Width = 4400 / twipperdot;
                dataGridView1.Columns[5].Width = 4400 / twipperdot;
                dataGridView1.Columns[6].Width = 1500 / twipperdot;
                dataGridView1.Columns[7].Width = 400 / twipperdot;//1300
                dataGridView1.Columns[8].Width = 400 / twipperdot;
                dataGridView1.Columns[9].Width = 400 / twipperdot;
                dataGridView1.Columns[10].Width = 400 / twipperdot;

                if (dataGridView1.Rows.Count <= 0)
                {
                    estEXT = true;
                }
                else
                {
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
            this.dtm見積日開始 = DateTime.Now.AddDays(-7).Date;
            this.dtm見積日終了 = DateTime.Now.Date;
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

            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(CommonConstants.LoginUserCode, this);

            MyApi myapi = new MyApi();
            int xSize, ySize, intpixel, twipperdot;

            //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
            intpixel = myapi.GetLogPixel();
            twipperdot = myapi.GetTwipPerDot(intpixel);

            intWindowHeight = this.Height;
            intWindowWidth = this.Width;

            gridobject = this.dataGridView1;

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

            InitializeFilter();

            if (!DoUpdate())
            {
                MessageBox.Show($"初期化に失敗しました。[{Name}]を終了します。", "初期処理", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
                return;
            }

            fn.WaitForm.Close();
            Cleargrid(dataGridView1);
        }

        private void Form_Resize(object sender, EventArgs e)
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

        public bool DoUpdate()
        {
            bool result = false;

            try
            {
                gridobject.SuspendLayout();

                if (GridSet())
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
                F_見積 form = new F_見積();
                form.varOpenArgs = $"{CurrentCode},{CurrentEdition}";
                form.ShowDialog();
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
            //dataGridView.ClearSelection();

            //if (dataGridView.Rows.Count > 0)
            //{
            //    dataGridView.Rows[0].Selected = true;
            //    dataGridView.FirstDisplayedScrollingRowIndex = 0; // 先頭行を表示
            //}
        }

        private void コマンド終了_Click(object sender, EventArgs e)
        {
            dataGridView1.Focus();
            this.Close();
        }

        private void コマンド抽出_Click(object sender, EventArgs e)
        {
            try
            {
                this.dataGridView1.Focus();
                objParent = this;

                // 見積管理_抽出フォームを開く
                F_見積管理_抽出 form = new F_見積管理_抽出();
                form.ShowDialog();
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
                this.dataGridView1.Focus();

                FunctionClass fn = new FunctionClass();
                fn.DoWait("初期化しています...");
                InitializeFilter();

                // DoUpdate メソッドが更新できない場合はメッセージを表示
                if (!DoUpdate())
                {
                    MessageBox.Show("エラーが発生しました。", "初期化コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                // 実行中フォームを閉じる
                fn.WaitForm.Close();
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
                this.dataGridView1.Focus();

                FunctionClass fn = new FunctionClass();
                fn.DoWait("初期化しています...");
                //InitializeFilter();
                SetFilter1();
                // DoUpdate メソッドが更新できない場合はメッセージを表示
                if (!DoUpdate())
                {
                    MessageBox.Show("エラーが発生しました。", "初期化コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                fn.WaitForm.Close();
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
            FunctionClass fn = new FunctionClass();
            try
            {
                this.SuspendLayout();
                this.dataGridView1.Focus();

                // 更新中の待機画面を表示

                fn.DoWait("更新しています...");

                // DoUpdate メソッドが更新できない場合はメッセージを表示
                if (!DoUpdate())
                {
                    MessageBox.Show("更新できませんでした。", "更新コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                // 実行中フォームを閉じる
                fn.WaitForm.Close();
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                Console.WriteLine("Error: " + ex.Message);
                fn.WaitForm.Close();
            }
            finally
            {
                this.Invalidate();
            }
        }

        private void コマンド検索_Click(object sender, EventArgs e)
        {
            dataGridView1.Focus();

            F_検索コード form = new F_検索コード(this, CommonConstants.CH_ESTIMATE);
            form.ShowDialog();
        }



        private bool ascending = true;

        private void コマンド見積_Click(object sender, EventArgs e)
        {
            dataGridView1.Focus();
            F_見積 form = new F_見積();
            form.varOpenArgs = $"{CurrentCode},{CurrentEdition}";
            form.ShowDialog();
        }

        private void コマンド見積書_Click(object sender, EventArgs e)
        {
            Connection connection = new Connection();
            string servername = "";
            string server = "";
            string[] parts = connection.Getconnect().Split(';');
            foreach (var part in parts)
            {
                if (part.StartsWith("Data Source="))
                {
                    servername = part.Replace("Data Source=", "");
                }
            }

            if (servername.Contains(",1436") || servername.Contains("\\unet_secondary"))
            {
                server = "secondary";
            }
            else
            {
                server = "primary";
            }

            string param = string.Format(" -sv:{0} -pv:estimate,{1},{2}",
                server.Replace(" ", "_"), CurrentCode.Trim().Replace(" ", "_"), CurrentEdition.Replace(" ", "_"));
            param = " -user:" + CommonConstants.LoginUserName + param;

            FunctionClass.GetShell(param);

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
                this.dataGridView1.Focus();

                Connect();

                // 現在のコードを取得
                string strDocumentCode = CurrentCode;
                if (string.IsNullOrEmpty(strDocumentCode)) return;
                // 本データがグループに登録済みかどうかを判断する
                switch (FunctionClass.DetectGroupMember(cn, strDocumentCode))
                {
                    case 0:
                        // グループに登録済みでない場合
                        
                        F_グループ form = new F_グループ();
                        form.args = strDocumentCode;
                        form.ShowDialog();
                        break;
                    case 1:
                        // グループに登録済みの場合
                        F_リンク form2 = new F_リンク();
                        form2.args = strDocumentCode;
                        form2.ShowDialog();
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

        private void 一覧_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && dataGridView1.HitTest(e.X, e.Y).RowIndex == -1)
            {
                try
                {
                    dataGridView1.Sort(dataGridView1.Columns[dataGridView1.HitTest(e.X, e.Y).ColumnIndex], ListSortDirection.Ascending);
                }
                catch (Exception ex)
                {
                    // エラーが発生した場合の処理
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            FunctionKeyDown(sender, e);

        }
    }
}