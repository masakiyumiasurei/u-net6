using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data.OleDb;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using u_net.Public;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data.Common;
using GrapeCity.Win.MultiRow;
using System.ComponentModel;
using System.DirectoryServices;
using Microsoft.EntityFrameworkCore.Metadata;
using System.Reflection.Metadata.Ecma335;


namespace u_net
{
    public partial class F_シリーズ : Form
    {
        private Control? previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private bool setCombo = true;
        private const string STR_CODEHEADER = "SRS";
        private bool noUpd = false; //setcontrolメソッドなどで主キーコードが空に更新されたタイミングで画面更新処理を行わない様にする
        private int int在庫補正数量 = 0;
        private string BASE_CAPTION = "シリーズ";
        int intWindowHeight;
        int intWindowWidth;

        public bool IsChanged
        {
            get
            {
                return コマンド登録.Enabled;
            }
        }

        public bool IsNewData
        {
            get
            {
                return !コマンド新規.Enabled;
            }
        }

        public string CurrentCode
        {
            get
            {
                return string.IsNullOrEmpty(シリーズコード.Text) ? "" : シリーズコード.Text;
            }
        }

        public F_シリーズ()
        {
            this.Text = "シリーズ";       // ウィンドウタイトルを設定
            this.MaximizeBox = false;  // 最大化ボタンを無効化

            InitializeComponent();
        }
        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        SqlCommand cmd = new SqlCommand();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter();

        private void Form_Load(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                control.PreviewKeyDown += OriginalClass.ValidateCheck;
            }

            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");

            OriginalClass ofn = new OriginalClass();
            ofn.SetComboBox(シリーズコード, "SELECT シリーズコード as Display,シリーズコード as Value FROM Mシリーズ WHERE(無効日時 IS NULL) ORDER BY シリーズコード DESC");

            setCombo = false;


            intWindowHeight = this.Height;
            intWindowWidth = this.Width;
            previousControl = null;

            // DataGridViewの設定
            dataGridView1.AllowUserToResizeColumns = true;
            dataGridView1.Font = new Font("MS ゴシック", 10);
            dataGridView1.DefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 210, 255);
            dataGridView1.DefaultCellStyle.SelectionForeColor = Color.Black;
            dataGridView1.GridColor = Color.FromArgb(230, 230, 230);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new Font("MS ゴシック", 9);
            dataGridView1.DefaultCellStyle.Font = new Font("MS ゴシック", 10);
            dataGridView1.DefaultCellStyle.ForeColor = Color.Black;

            System.Type dgvtype = typeof(DataGridView);
            System.Reflection.PropertyInfo dgvPropertyInfo = dgvtype.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            dgvPropertyInfo.SetValue(dataGridView1, true, null);

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;


            try
            {
                this.SuspendLayout();

                if (string.IsNullOrEmpty(args))
                {
                    if (!GoNewMode())
                    {
                        return;
                    }
                }
                else
                {
                    if (!GoModifyMode())
                    {
                        return;
                    }
                    if (!string.IsNullOrEmpty(args))
                    {
                        ChangedData(false);
                        this.シリーズコード.Text = args;
                    }
                }
                //setcontrolでクリアするので、ここに記述
                if (!SetGrid()) return;
                fn.WaitForm.Close();

                LocalSetting localSetting = new LocalSetting();
                localSetting.SavePlace(CommonConstants.LoginUserCode, this);

            }
            catch (Exception ex)
            {
                ChangedData(false);
                MessageBox.Show("初期化に失敗しました。\n" + ex.Message, "エラー");
            }
            finally
            {
                ChangedData(false);
                this.ResumeLayout();
            }
        }

        private bool GoNewMode()
        {
            try
            {
                noUpd = true;
                // 各コントロール値を初期化
                VariableSet.SetControls(this);
                string strSQL;
                Connect();

                // ヘッダ部を制御
                string original = FunctionClass.採番(cn, STR_CODEHEADER);
                シリーズコード.Text = original.Substring(original.Length - 8);
                在庫下限数量.Text = "0";
                在庫補正数量.Text = "0";
                補正値.Text = "0";

                FunctionClass.LockData(this, false);
                シリーズ名.Focus();
                シリーズコード.Enabled = false;
                コマンド新規.Enabled = false;
                コマンド修正.Enabled = true;
                コマンド複写.Enabled = false;
                コマンド削除.Enabled = false;
                コマンド商品参照.Enabled = false;
                //   コマンド承認.Enabled = false;
                //   コマンド確定.Enabled = false;
                コマンド登録.Enabled = false;
                noUpd = false;
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GoNewMode - " + ex.Message);
                noUpd = false;
                return false;
            }
        }

        private bool GoModifyMode()
        {
            try
            {
                noUpd = true;
                VariableSet.SetControls(this);
                FunctionClass.LockData(this, true, "シリーズコード");
                this.シリーズコード.Enabled = true;
                this.シリーズコード.Focus();
                this.コマンド新規.Enabled = true;
                this.コマンド修正.Enabled = false;
                this.コマンド複写.Enabled = false;
                this.コマンド登録.Enabled = false;
                noUpd = false;
                return true;
            }
            catch (Exception ex)
            {
                noUpd = false;
                MessageBox.Show("GoModifyMode - " + ex.HResult + " : " + ex.Message);
                return false;
            }
        }

        private bool SetGrid()
        {
            try
            {
                string query = "SELECT * FROM Vシリーズ管理 WHERE 廃止 IS NULL ORDER BY シリーズコード DESC ";
                Connect();
                DataGridUtils.SetDataGridView(cn, query, this.dataGridView1);

                int cnt = 0;

                if (dataGridView1.DataSource != null)
                {
                    cnt = ((DataTable)dataGridView1.DataSource).Rows.Count;
                    表示件数.Text = cnt.ToString();

                    if (cnt == 0)
                    {
                        // データがない場合はダミーデータを追加
                        dataGridView1.Rows.Add();
                    }
                }

                MyApi myapi = new MyApi();
                int xSize, ySize, intpixel, twipperdot;

                intpixel = myapi.GetLogPixel();
                twipperdot = myapi.GetTwipPerDot(intpixel);

                dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                dataGridView1.ColumnHeadersHeight = 25;

                //0列目はaccessでは行ヘッダのため、ずらす
                dataGridView1.Columns[0].Width = 1200 / twipperdot; //1150
                dataGridView1.Columns[1].Width = 2500 / twipperdot;
                dataGridView1.Columns[2].Width = 1200 / twipperdot;
                dataGridView1.Columns[3].Width = 1200 / twipperdot;
                dataGridView1.Columns[4].Width = 2300 / twipperdot;
                dataGridView1.Columns[5].Width = 1700 / twipperdot;
                dataGridView1.Columns[6].Width = 400 / twipperdot;

                return true;
            }
            catch (Exception ex)
            {
                ChangedData(false);
                MessageBox.Show("グリッドの初期化に失敗しました。\n" + ex.Message, "エラー");
                return false;
            }
        }
        private void DataGridView1_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            //行番号を表示する
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
                TextRenderer.DrawText(e.Graphics, (e.RowIndex + 1).ToString(), e.CellStyle.Font, indexRect,
                    e.CellStyle.ForeColor, TextFormatFlags.Right | TextFormatFlags.VerticalCenter);

                //描画が完了したことを知らせる
                e.Handled = true;
                dataGridView1.ResumeLayout();
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0) // ヘッダー行でない場合
            {
                try
                {
                    GoModifyMode();
                    シリーズコード.SelectedValue = dataGridView1.Rows[e.RowIndex].Cells[0].Value.ToString(); // 1列目のデータを取得
                    シリーズ名.Focus();
                    //GoModifyModeで表示件数がクリアされるため
                    int cnt = ((DataTable)dataGridView1.DataSource).Rows.Count;
                    表示件数.Text = cnt.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
        private void Cleargrid(DataGridView dataGridView)
        {
            dataGridView.ClearSelection();

            if (dataGridView.Rows.Count > 0)
            {
                dataGridView.Rows[0].Selected = true;
                dataGridView.FirstDisplayedScrollingRowIndex = 0; // 先頭行を表示
            }
        }
        private bool ErrCheck(Control argscontrol, string? tname = null)
        {            
            foreach (Control control in argscontrol.Controls)
                //入力確認
                if (string.IsNullOrEmpty(tname) || tname == control.Name)
                {
                    switch (control.Name)
                    {
                        case "シリーズ名":
                            if (!FunctionClass.IsError(control)) return false;
                            break;
                        case "在庫下限数量":
                            if (!FunctionClass.IsError(control)) return false;
                            if (!OriginalClass.IsNumeric(在庫下限数量.Text))
                            {
                                MessageBox.Show("数字を入力してください。: ", "数値判定エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                return true;
                            }
                            double result;
                            double.TryParse(在庫下限数量.Text, out result);
                            if (result < 0)
                            {
                                MessageBox.Show("0 以上の値を入力してください。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return true;
                            }
                            break;
                        case "補正値":
                            if (!FunctionClass.IsError(control))
                            {
                                MessageBox.Show("在庫数量を補正しないときは 0 を入力してください。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return true;
                            }
                            if (!OriginalClass.IsNumeric(補正値.Text))
                            {
                                MessageBox.Show("数字を入力してください。: ", "数値判定エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return true;
                            }
                            break;
                    }
                }
            return false;
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
        private void コマンド登録_Click(object sender, EventArgs e)
        {
            //保存確認
            //if (MessageBox.Show("変更内容を保存しますか？", "保存確認",
            //    MessageBoxButtons.OKCancel,
            //    MessageBoxIcon.Question) == DialogResult.OK)
            //{
            if (!ErrCheck(this)) return;

            if (!SaveData()) return;

            シリーズコード.Enabled = true;
            ChangedData(false);

            if (IsNewData)
            {
                コマンド新規.Enabled = true;
                コマンド修正.Enabled = false;
            }
            if (!SetGrid()) return;
            在庫数量.Text = GetStock(CurrentCode, DateTime.Now, cn).ToString();
            Cleargrid(dataGridView1);
            //}
        }

        private bool SaveData()
        {
            //SqlTransaction transaction = cn.BeginTransaction();
            SqlTransaction transaction;
            Control objControl1 = null;
            Control objControl2 = null;
            Control objControl3 = null;
            Control objControl4 = null;
            Control objControl5 = null;
            Control objControl6 = null;
            object varSaved1 = null;
            object varSaved2 = null;
            object varSaved3 = null;
            object varSaved4 = null;
            object varSaved5 = null;
            object varSaved6 = null;
            object varSaved7 = null;

            try
            {
                Connect();
                varSaved7 = 在庫補正数量.Text;
                在庫補正数量.Text = (Convert.ToInt64(this.在庫補正数量.Text) + Convert.ToInt64(補正値.Text)).ToString();

                DateTime dtmNow = FunctionClass.GetServerDate(cn);
                if (IsNewData)
                {
                    objControl1 = 作成日時;
                    objControl2 = 作成者コード;
                    objControl3 = 作成者名;

                    varSaved1 = objControl1.Text;
                    varSaved2 = objControl2.Text;
                    varSaved3 = objControl3.Text;

                    objControl1.Text = dtmNow.ToString();
                    objControl2.Text = CommonConstants.LoginUserCode;
                    objControl3.Text = CommonConstants.LoginUserFullName;
                }

                objControl4 = 更新日時;
                objControl5 = 更新者コード;
                objControl6 = 更新者名;

                // 登録前の状態を退避しておく
                varSaved4 = objControl4.Text;
                varSaved5 = objControl5.Text;
                varSaved6 = objControl6.Text;

                // 値の設定
                objControl4.Text = dtmNow.ToString();
                objControl5.Text = CommonConstants.LoginUserCode;
                objControl6.Text = CommonConstants.LoginUserFullName;

                // Mシリーズデータを保存
                string strwhere = " シリーズコード='" + this.シリーズコード.Text + "'";
                transaction = cn.BeginTransaction();

                if (!DataUpdater.UpdateOrInsertDataFrom(this, cn, "Mシリーズ", strwhere, "シリーズコード", transaction))
                {
                    //保存できなかった時の処理
                    if (IsNewData)
                    {
                        objControl1.Text = varSaved1.ToString();
                        objControl2.Text = varSaved2.ToString();
                        objControl3.Text = varSaved3.ToString();
                    }

                    objControl4.Text = varSaved4.ToString();
                    objControl5.Text = varSaved5.ToString();
                    objControl6.Text = varSaved6.ToString();
                    在庫補正数量.Text = varSaved7.ToString();
                    return false;
                }

                補正値.Text = "0";

                transaction.Commit();

                //コンボボックスのソースを変更する必要がある
                object code = シリーズコード.SelectedValue;

                setCombo = true;
                OriginalClass ofn = new OriginalClass();
                ofn.SetComboBox(シリーズコード, "SELECT シリーズコード as Display,シリーズコード as Value FROM Mシリーズ WHERE(無効日時 IS NULL) ORDER BY シリーズコード DESC");
                setCombo = false;

                シリーズコード.SelectedValue = code;

                MessageBox.Show("登録を完了しました");

                シリーズコード.Enabled = true;

                // 新規モードのときは修正モードへ移行する
                if (IsNewData)
                {
                    コマンド新規.Enabled = true;
                    コマンド修正.Enabled = false;
                }

                コマンド複写.Enabled = true;
                コマンド削除.Enabled = true;
                コマンド登録.Enabled = false;

                return true;

            }
            catch (Exception ex)
            {

                コマンド登録.Enabled = true;
                // エラーメッセージを表示またはログに記録
                MessageBox.Show("データの保存中にエラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void コマンド新規_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ActiveControl == this.コマンド新規)
                {
                    if (previousControl != null)
                    {
                        previousControl.Focus();
                    }
                }

                // 変更があるときは登録確認を行う
                if (this.コマンド登録.Enabled)
                {
                    var Res = MessageBox.Show("変更内容を登録しますか？", "新規コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    switch (Res)
                    {
                        case DialogResult.Yes:

                            if (!ErrCheck(シリーズコード)) return;
                            // 登録処理
                            if (!SaveData())
                            {
                                MessageBox.Show("登録できませんでした。", "新規コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }

                            break;
                        case DialogResult.Cancel:
                            return;
                    }
                }
                // 新規モードへ移行
                if (!GoNewMode())
                {
                    goto Err_コマンド新規_Click;
                }

                return;

            Err_コマンド新規_Click:
                MessageBox.Show("エラーのため新規モードへ移行できません。", "新規コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            catch (Exception ex)
            {
                // 例外処理
                Debug.Print(this.Name + "_コマンド新規 - " + ex.Message);
                MessageBox.Show("エラーのため新規モードへ移行できません。", "新規コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void コマンド修正_Click(object sender, EventArgs e)
        {
            try
            {
                // データに変更があった場合の処理
                if (IsChanged)
                {
                    var res = MessageBox.Show("変更内容を登録しますか？", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    switch (res)
                    {
                        case DialogResult.Yes:

                            if (!ErrCheck(this)) return;
                            this.DoubleBuffered = false;
                            FunctionClass fn = new FunctionClass();
                            fn.DoWait("しばらくお待ちください...");

                            if (!SaveData())
                            {
                                MessageBox.Show("エラーのため登録できませんでした。", "修正コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            this.DoubleBuffered = true;
                            fn.WaitForm.Close();
                            break;

                        case DialogResult.No:
                            // 新規モードのときに登録しない場合はコードを戻す
                            if (!IsNewData && !string.IsNullOrEmpty(CurrentCode))
                            {
                                Connect();
                                if (!FunctionClass.ReturnCode(cn, STR_CODEHEADER + CurrentCode))
                                {
                                    MessageBox.Show("エラーのためコードは破棄されました。\n\nシリーズコード： " + CurrentCode, CurrentCode, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                            }
                            break;

                        case DialogResult.Cancel:
                            return;
                    }
                }
                else
                {
                    // 新規モードのときに変更がない場合はコードを戻す
                    if (IsNewData && !string.IsNullOrEmpty(CurrentCode))
                    {
                        if (!FunctionClass.ReturnCode(cn, STR_CODEHEADER + CurrentCode))
                        {
                            MessageBox.Show("エラーのためコードは破棄されました。\n\nシリーズコード： " + CurrentCode, CurrentCode, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }

                // 修正モードへ移行する
                if (!GoModifyMode())
                {
                    // 移行に失敗した場合の処理
                    Debug.Print(this.Name + "_コマンド修正_Click - Error");
                    if (MessageBox.Show("エラーが発生しました。\n\n管理者に連絡してください。\n\n強制終了しますか？", "修正コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    {
                        this.Close();
                    }

                }

                //GoModifyModeで表示件数がクリアされるため
                int cnt = ((DataTable)dataGridView1.DataSource).Rows.Count;
                表示件数.Text = cnt.ToString();

            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_コマンド修正_Click - " + ex.Message);
                MessageBox.Show("エラーが発生しました。\n\n管理者に連絡してください。\n\n強制終了しますか？", "修正コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void コマンド複写_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。", "コマンド複写", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool CopyData(string codeString)
        {
            try
            {
                // DataGridView内の各行にアクセス
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // 行が新しい行を示す場合など、データ行でない場合は無視
                    if (!row.IsNewRow)
                    {
                        // シリーズコードカラムのセルを取得
                        DataGridViewCell productCodeCell = row.Cells["dgvシリーズコード"]; // カラム名に応じて変更

                        if (productCodeCell != null)
                        {
                            // シリーズコードカラムのセルの値を新しいシリーズコードに変更
                            productCodeCell.Value = codeString;
                        }
                    }
                }
                // コントロールのフィールドを初期化
                シリーズコード.Text = codeString;
                作成日時.Text = null;
                作成者コード.Text = null;
                作成者名.Text = null;
                更新日時.Text = null;
                更新者コード.Text = null;
                更新者名.Text = null;
                削除.Text = null;



                return true;
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                MessageBox.Show("_CopyData - " + ex.Message);
                return false;
            }
        }

        private void コマンド削除_Click(object sender, EventArgs e)
        {
            if (ActiveControl == コマンド削除)
            {
                if (previousControl != null)
                {
                    previousControl.Focus();
                }
            }

            if (IsRegisteredProduct(CurrentCode))
            {
                MessageBox.Show("このシリーズは商品として登録されているため、削除することはできません。" +
                    "\n削除する前に、このシリーズに関連する商品を全て削除してください。", "削除コマンド",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            DialogResult result = MessageBox.Show($"シリーズコード：{CurrentCode}\n\nこのシリーズデータを削除します。\n削除後元に戻すことはできません。\n\n削除しますか？", "削除コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
                return;

            // データベースからデータを削除
            if (DeleteData(CurrentCode))
            {
                MessageBox.Show("削除しました。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.SuspendLayout();
                // 新規モードへ移行
                if (!GoNewMode())
                {
                    MessageBox.Show($"エラーのため新規モードへ移行できません。\n[{Name}]を終了します。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.ResumeLayout();
                    Close();
                }
                SetGrid();
                Cleargrid(dataGridView1);
                this.ResumeLayout();
            }
            else
            {
                MessageBox.Show("削除できませんでした。他のユーザーにより削除されている可能性があります。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        //シリーズコードがあるかどうかの判定　あればtrueを返す
        private bool IsRegisteredProduct(string seriesCode)
        {
            bool isRegisteredProduct = false;
            try
            {
                Connect();
                string strSQL = "SELECT DISTINCT シリーズコード FROM M商品 WHERE (シリーズコード IS NOT NULL) " +
                                "AND シリーズコード=@SeriesCode AND 無効日時 IS NULL";
                using (SqlCommand command = new SqlCommand(strSQL, cn))
                {
                    command.Parameters.AddWithValue("@SeriesCode", seriesCode);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isRegisteredProduct = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("IsRegisteredProduct - " + ex.Message);
            }

            return isRegisteredProduct;
        }
        private bool DeleteData(string codeString)
        {
            bool success = false;

            // SQL文で使用するパラメータ名
            string codeParam = "@Code";

            // トランザクションを開始
            Connect();
            SqlTransaction transaction = cn.BeginTransaction();

            try
            {
                //ログインユーザについてはどうするか検討
                string LoginUserCode = "tes";
                // データベース操作のためのSQL文
                string selectSql = "SELECT * FROM Mシリーズ WHERE シリーズコード = " + codeParam + " AND 無効日時 IS NULL";
                string updateSql = "UPDATE Mシリーズ SET 無効日時 = GETDATE(), 無効者コード = @UserCode WHERE シリーズコード = " + codeParam + " AND 無効日時 IS NULL";

                using (SqlCommand selectCommand = new SqlCommand(selectSql, cn, transaction))
                {
                    selectCommand.Parameters.Add(new SqlParameter(codeParam, codeString));
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            // シリーズが見つかった場合、削除処理を実行
                            reader.Close(); // 同じ接続なのでデータリーダーを閉じておく

                            using (SqlCommand updateCommand = new SqlCommand(updateSql, cn, transaction))
                            {
                                updateCommand.Parameters.Add(new SqlParameter(codeParam, codeString));
                                updateCommand.Parameters.Add(new SqlParameter("@UserCode", LoginUserCode)); // LoginUserCode に適切な値を設定

                                int rowsAffected = updateCommand.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    // 更新が成功した場合、トランザクションをコミット
                                    transaction.Commit();
                                    success = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // エラー処理
                Console.WriteLine("DeleteData - " + ex.Message);

                try
                {
                    // エラーが発生した場合、トランザクションをロールバック
                    transaction.Rollback();
                }
                catch (Exception rollbackEx)
                {
                    Console.WriteLine("Rollback Error - " + rollbackEx.Message);
                }
            }
            finally
            {
                cn.Close();
            }

            return success;
        }

        private void コマンド承認_Click(object sender, EventArgs e)
        {
            if (ActiveControl == コマンド承認)
            {
                if (previousControl != null)
                {
                    previousControl.Focus();
                }
            }
            MessageBox.Show("このコマンドは使用できません。", "承認コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド終了_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //フォームを閉じる時のロールバック等の処理
        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (IsChanged)
                {
                    // データに変更がある場合の処理
                    DialogResult result = MessageBox.Show("変更内容を登録しますか？", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    switch (result)
                    {
                        case DialogResult.Yes:
                            // エラーチェック
                            //if (!ErrCheck()) return;


                            // 登録処理
                            //DoWait("登録しています...");
                            this.SuspendLayout();

                            if (!SaveData())
                            {
                                if (MessageBox.Show("エラーのため登録できませんでした。" + Environment.NewLine +
                                    "強制終了しますか？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                {
                                    e.Cancel = true;
                                    return;
                                }
                            }
                            break;

                        case DialogResult.No:
                            // 新規モードのときに登録しない場合はコードを戻す
                            if (IsNewData && !string.IsNullOrEmpty(CurrentCode))
                            {
                                Connect();
                                if (!FunctionClass.ReturnCode(cn, STR_CODEHEADER + CurrentCode))
                                {
                                    MessageBox.Show("エラーのためコードは破棄されました。\n\nシリーズコード： " + CurrentCode, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                            }
                            break;

                        case DialogResult.Cancel:
                            e.Cancel = true;
                            return;
                    }
                }
                else
                {
                    // 新規モードのときに変更がない場合はコードを戻す
                    if (IsNewData && !string.IsNullOrEmpty(CurrentCode))
                    {
                        Connect();
                        if (!FunctionClass.ReturnCode(cn, STR_CODEHEADER + CurrentCode))
                        {
                            MessageBox.Show("エラーのためコードは破棄されました。\n\nシリーズコード： " + CurrentCode, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }

                LocalSetting localSetting = new LocalSetting();
                localSetting.SavePlace(CommonConstants.LoginUserCode, this);

                //実行中フォーム起動

            }
            catch (Exception ex)
            {
                // エラー処理
                Console.WriteLine("Form_FormClosing error: " + ex.Message);
                MessageBox.Show("終了時にエラーが発生しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void コマンド商品参照_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。", "商品参照コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        private void コマンド確定_Click(object sender, EventArgs e)
        {

        }

        // コントロールがフォーカスを受け取ったとき、前回のフォーカスを記憶
        private void Control_GotFocus(object sender, EventArgs e)
        {
            previousControl = sender as Control;
        }


        public void ChangedData(bool dataChanged)
        {
            if (dataChanged)
            {
                this.Text = this.BASE_CAPTION + "*";
            }
            else
            {
                this.Text = this.BASE_CAPTION;
            }

            if (this.ActiveControl == this.シリーズコード)
            {
                this.シリーズ名.Focus();
            }

            this.シリーズコード.Enabled = !dataChanged;
            this.コマンド複写.Enabled = !dataChanged;
            this.コマンド削除.Enabled = !dataChanged;
            this.コマンド登録.Enabled = dataChanged;
        }

        private void F_シリーズ_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space: //コンボボックスならドロップダウン
                    {
                        Control activeControl = this.ActiveControl;
                        if (activeControl is System.Windows.Forms.ComboBox)
                        {
                            ComboBox activeComboBox = (ComboBox)activeControl;
                            activeComboBox.DroppedDown = true;
                        }
                    }
                    break;
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;
                case Keys.F1:
                    if (コマンド新規.Enabled)
                    {
                        コマンド新規.Focus();
                        コマンド新規_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F2:
                    if (コマンド修正.Enabled)
                    {
                        コマンド修正.Focus();
                        コマンド修正_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F3:
                    if (コマンド複写.Enabled)
                    {
                        コマンド複写_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F4:
                    if (コマンド削除.Enabled)
                    {
                        コマンド削除_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F5:
                    if (コマンド商品参照.Enabled)
                    {
                        コマンド商品参照_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F9:
                    if (コマンド承認.Enabled)
                    {
                        コマンド承認_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F10:
                    if (コマンド確定.Enabled)
                    {
                        コマンド確定_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F11:
                    if (コマンド登録.Enabled)
                    {
                        コマンド登録.Focus();
                        コマンド登録_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F12:
                    if (コマンド終了.Enabled)
                    {
                        コマンド終了_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
            }
        }


        private void シリーズコード_SelectedIndexChanged(object sender, EventArgs e)
        {//シリーズコードのコンボボックスのソースセット時には処理を行わない様にするため
            if (setCombo) return;
            if (noUpd) return;
            if (!FunctionClass.LimitText((Control)sender, 8)) return;
            this.SuspendLayout();
            UpdatedControl();
            this.ResumeLayout();
        }

        private void UpdatedControl()
        {
            //シリーズコードの更新後処理でレコードの値を表示する
            this.コマンド複写.Enabled = true;
            this.コマンド削除.Enabled = true;
            try
            {
                //ヘッダ部の表示
                string strSQL = "SELECT * FROM Vシリーズヘッダ WHERE シリーズコード='" + CurrentCode + "'";

                Connect();
                if (!VariableSet.SetTable2Form(this, strSQL, cn)) return;
                在庫数量.Text = GetStock(CurrentCode, DateTime.Now, cn).ToString();
                補正値.Text = "0";

                //ヘッダの制御
                FunctionClass.LockData(this, false, "シリーズコード");
                コマンド複写.Enabled = true;
                コマンド削除.Enabled = true;
                コマンド商品参照.Enabled = true;
                cn.Close();

                ChangedData(false);

            }
            catch (Exception ex)
            {
                MessageBox.Show("正しく読み込みが出来ませんでした" + ex.Message);
                cn.Close();

            }
        }

        private static int GetStock(string seriesCode, DateTime targetDate, SqlConnection cn)
        {
            int stock = 0;
            string strDate = targetDate.ToString("yyyy/MM/dd"); // 対象フィールドが文字列型であるため

            try
            {
                using (SqlCommand command = new SqlCommand("SPシリーズ在庫参照", cn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@TargetDate", targetDate);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            // 条件に合致するレコードを抽出
                            if (string.Equals(seriesCode, reader["シリーズコード"].ToString()) &&
                                DateTime.Equals(strDate, reader["確認日"].ToString()))
                            {
                                //stock = Convert.ToInt32(reader["在庫数量"]);
                                //ストアドの結果では在庫数量は7番目のインデックスになるのだがVBAのコードではrs1.Fields(5).Valueとなっていた
                                //一応ソースに合わせておくが、確認次第上に変更かも
                                stock = Convert.ToInt32(reader[5]);
                                break; // 一致するレコードが見つかったらループを終了
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");

            }
            return stock;
        }

        private void シリーズコード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(ActiveControl.Text))
                    return;

                string strCode = ActiveControl.Text.PadLeft(8, '0');

                if (strCode != (ActiveControl.Text ?? ""))
                {
                    ActiveControl.Text = strCode;
                }
            }
        }



        private void シリーズ名_TextChanged(object sender, EventArgs e)
        {
            if (!FunctionClass.LimitText((Control)sender, 20)) return;
            ChangedData(true);
        }
        private void Discontinued_CheckedChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 補正値増加ボタン_Click(object sender, EventArgs e)
        {
            if (!補正値.Enabled || 補正値.ReadOnly) return;
            補正値.Focus();
            補正値.Text = (long.Parse(this.補正値.Text) + 1).ToString();
        }

        private void 補正値減少ボタン_Click(object sender, EventArgs e)
        {
            if (!補正値.Enabled || 補正値.ReadOnly) return;
            補正値.Focus();
            補正値.Text = (long.Parse(this.補正値.Text) - 1).ToString();
        }

        private void 在庫下限数量_TextChanged(object sender, EventArgs e)
        {
            if (!FunctionClass.LimitText((Control)sender, 10)) return;
            ChangedData(true);
        }

        private void 補正値_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■在庫数量を修正するための補正数量を入力します。マイナス値も入力できます。";
        }

        private void 補正値_Validating(object sender, CancelEventArgs e)
        {
            if (補正値.Modified == false) return;

            if (ErrCheck(this, "補正値")) e.Cancel=true;
        }

        private void 補正値_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 在庫下限数量_Validating(object sender, CancelEventArgs e)
        {
            if (在庫下限数量.Modified == false) return;

           if( ErrCheck(this, "在庫下限数量")) e.Cancel=true;
        }
    }
}


