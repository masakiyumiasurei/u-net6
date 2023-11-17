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

            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");

            OriginalClass ofn = new OriginalClass();
            ofn.SetComboBox(シリーズコード, "SELECT シリーズコード as Display,シリーズコード as Value FROM Mシリーズ ORDER BY シリーズコード DESC");

            setCombo = false;


            int intWindowHeight = this.Height;
            int intWindowWidth = this.Width;
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

                string LoginUserCode = "000";//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
                LocalSetting localSetting = new LocalSetting();
                localSetting.LoadPlace(LoginUserCode, this);

            }
            catch (Exception ex)
            {
                ChangedData(false);
                MessageBox.Show("初期化に失敗しました。\n" + ex.Message, "エラー");
            }
            finally
            {
                this.ResumeLayout();
            }
        }

        private bool GoNewMode()
        {
            try
            {
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
                MessageBox.Show("初期化に失敗しました。\n" + ex.Message, "エラー");
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

        private bool ErrCheck(Control control)
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
        private bool ErrCheck(Control argscontrol, string? tname=null)
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
                            if (OriginalClass.IsNumeric(control))
                            {
                                MessageBox.Show("数字を入力してください。: ", "数値判定エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);

                                return false;
                            }
                            double result;
                            double.TryParse(control.Text, out result);
                            if (result < 0)
                            {
                                MessageBox.Show("0 以上の値を入力してください。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                            break;
                        case "補正値":
                            if (!FunctionClass.IsError(control))
                            {
                                MessageBox.Show("在庫数量を補正しないときは 0 を入力してください。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                            if (OriginalClass.IsNumeric(control))
                            {
                                MessageBox.Show("数字を入力してください。: ", "数値判定エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                return false;
                            }
                            break;
                    }
                }
            return true;
        }

        private void コマンド登録_Click(object sender, EventArgs e)
        {
            //保存確認
            if (MessageBox.Show("変更内容を保存しますか？", "保存確認",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) == DialogResult.OK)
            {
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
            }
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
                setCombo = true;
                OriginalClass ofn = new OriginalClass();
                ofn.SetComboBox(シリーズコード, "SELECT シリーズコード as Display,シリーズコード as Value FROM Mシリーズ ORDER BY シリーズコード DESC");
                setCombo = false;
            
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
            MessageBox.Show("現在開発中です。。", "コマンド複写", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                string LoginUserCode = "000";//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
                LocalSetting test = new LocalSetting();
                test.SavePlace(LoginUserCode, this);

                //実行中フォーム起動

            }
            catch (Exception ex)
            {
                // エラー処理
                Console.WriteLine("Form_FormClosing error: " + ex.Message);
                MessageBox.Show("終了時にエラーが発生しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                this.Text = this.Name + "*";
            }
            else
            {
                this.Text = this.Name;
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

            if (!FunctionClass.LimitText((Control)sender, 8)) return;
            UpdatedControl();
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
                                 stock = Convert.ToInt32(reader["在庫数量"]);
                                //ストアドの結果では在庫数量は7番目のインデックスになるのだがVBAのコードではrs1.Fields(5).Valueとなっていた
                                //一応ソースに合わせておくが、確認次第上にするかも
                                //stock = Convert.ToInt32(reader[5]);
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

        private void シリーズコード_KeyPress(object sender, KeyPressEventArgs e)
        {
            int keyAscii = e.KeyChar; // キーのASCIIコードを取得（仮の値）

            keyAscii = FunctionClass.ChangeBig(keyAscii);

            switch (keyAscii)
            {
                case ' ': // スペース
                case 12288: // 全角スペース
                    if (ActiveControl is ComboBox comboBox)
                    {
                        comboBox.DroppedDown = true; // ドロップダウンメニューを表示
                    }
                    e.Handled = true; // キー入力を無効にする
                    break;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void シリーズ名_TextChanged(object sender, EventArgs e)
        {
            if (!FunctionClass.LimitText((Control)sender, 40)) return;
            ChangedData(true);
        }

        private void シリーズ名_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■半角４０文字まで入力できます。";
        }

        private void シリーズコード_TextChanged(object sender, EventArgs e)
        {
            if (this.ActiveControl == null) return;
            string enteredText = シリーズコード.Text;
            if (string.IsNullOrEmpty(enteredText)) return;


            if (!OriginalClass.ComboBoxContainsValue(シリーズコード, enteredText))
            {
                MessageBox.Show("シリーズを選択してください。" + Environment.NewLine + "シリーズは事前に登録されている必要があります。",
                    this.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                シリーズコード.Text = ""; // テキストボックスをクリア
                シリーズコード.SelectedValue = DBNull.Value;
            }

            if (!FunctionClass.LimitText((Control)sender, 8)) return;
            ChangedData(true);
        }

        private void シリーズコード_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■この欄を入力すると自動的に在庫管理対象となります。　■半角２０文字まで入力できます。　■[space]キーでドロップダウンリストを表示します。";
        }


        private void 商品分類コード_TextChanged(object sender, EventArgs e)
        {
            if (!FunctionClass.LimitText((Control)sender, 2)) return;
            ChangedData(true);
        }

        private void 掛率有効_CheckedChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 売上区分コード_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■この商品の売上区分を選択します。　■この入力値は受注入力時の初期値になります。";
        }

        private void FlowCategoryCode_TextChanged(object sender, EventArgs e)
        {
            if (!FunctionClass.LimitText((Control)sender, 3)) return;
            ChangedData(true);
        }

        private void 数量単位コード_TextChanged(object sender, EventArgs e)
        {
            if (!FunctionClass.LimitText((Control)sender, 2)) return;
            ChangedData(true);
        }


        private void Discontinued_Enter(object sender, EventArgs e)
        {
            //this.toolStripStatusLabel2.Text = "■この商品を出荷する際、顧客シリアルが必要な時に指定します。";
        }

        private void Discontinued_CheckedChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 備考_TextChanged(object sender, EventArgs e)
        {
            if (!FunctionClass.LimitText((Control)sender, 200)) return;
            ChangedData(true);
        }               

        private void IsUnit_CheckedChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 補正値増加ボタン_Click(object sender, EventArgs e)
        {
            補正値.Focus();
            補正値.Text = (long.Parse(this.補正値.Text) + 1).ToString();
        }

        private void UpdateAndRefreshGridView(DataTable dataTable, string fieldName, long startValue = 1, long offset = 1)
        {
            long lngi = startValue;

            //DataRow[] dr = dataTable.Select("", "明細番号 desc");

            //foreach (DataRow row in dr)
            //{
            //    row[fieldName] = lngi.ToString();
            //    lngi += offset;
            //}

            DataView dv = new DataView(dataTable);
            dv.Sort = "明細番号 DESC";

            foreach (DataRowView drv in dv)
            {
                DataRow row = drv.Row;
                row[fieldName] = lngi.ToString();
                lngi += offset;
            }

            BindingSource bindingSource = (BindingSource)dataGridView1.DataSource;
            bindingSource.DataSource = dv;
            bindingSource.ResetBindings(false);
        }


    }
}


//public class DataGridViewEx : DataGridView
//{
//    [System.Security.Permissions.UIPermission(
//        System.Security.Permissions.SecurityAction.Demand,
//        Window = System.Security.Permissions.UIPermissionWindow.AllWindows)]
//    protected override bool ProcessDialogKey(Keys keyData)
//    {
//        //Enterキーが押された時は、Tabキーが押されたようにする
//        if ((keyData & Keys.KeyCode) == Keys.Enter)
//        {
//            return this.ProcessTabKey(keyData);
//        }
//        // 既定の処理を行う
//        return base.ProcessDialogKey(keyData);
//    }

//    [System.Security.Permissions.SecurityPermission(
//        System.Security.Permissions.SecurityAction.Demand,
//        Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
//    protected override bool ProcessDataGridViewKey(KeyEventArgs e)
//    {
//        //Enterキーが押された時は、Tabキーが押されたようにする
//        if (e.KeyCode == Keys.Enter)
//        {
//            return this.ProcessTabKey(e.KeyCode);
//        }
//        return base.ProcessDataGridViewKey(e);
//    }
//}

//private void b_内容検索_KeyDown(object sender, KeyEventArgs e)
//{//内容検索の文字列を含む内容の行を選択する
//    if (e.KeyCode == Keys.Enter)
//    {
//        this.dataGridView1.ClearSelection();

//        DataGridView dgv = this.dataGridView1;
//        System.Collections.IList list = dgv.Rows;
//        for (int i = 0; i < list.Count; i++)
//        {
//            //nullを比較するとエラーになるので先に省く
//            if (dataGridView1["内容", i].FormattedValue.ToString() != null)
//            {
//                //ボックスの文字列を比較
//                if ((dataGridView1["内容", i].FormattedValue.ToString().Contains(this.b_内容検索.Text)) && (true))
//                {
//                    //ボックスを選択
//                    this.dataGridView1["内容", i].Selected = true;
//                }
//            }
//        }
//    }
//}

//private void b_相手検索_KeyDown(object sender, KeyEventArgs e)
//{//相手検索の文字列を含む相手の行を選択する

//    if (e.KeyCode == Keys.Enter)
//    {
//        this.dataGridView1.ClearSelection();

//        DataGridView dgv = this.dataGridView1;
//        System.Collections.IList list = dgv.Rows;
//        for (int i = 0; i < list.Count; i++)
//        {
//            //nullを比較するとエラーになるので先に省く
//            if (dataGridView1["交渉相手", i].FormattedValue.ToString() != null)
//            {
//                //ボックスの文字列を比較
//                if ((dataGridView1["交渉相手", i].FormattedValue.ToString().Contains(this.b_相手検索.Text)) && (true))
//                {
//                    //ボックスを選択
//                    this.dataGridView1["交渉相手", i].Selected = true;
//                }
//            }
//        }
//    }
//}


//private void Form1_Load(object sender, EventArgs e)
//{
//              //上部の設定
//    Connect();
//    cmd = cn.CreateCommand();

//    cmd.CommandText = "select 顧客コード from T_顧客 where id=" + kokyaku_id;

//    SqlDataReader dr = cmd.ExecuteReader();

//    if (dr.HasRows)
//    {
//        dr.Read();
//        顧客コード.Text = dr["顧客コード"].ToString();
//        kokyaku_cd = dr["顧客コード"].ToString();                
//        dr.Close();
//    }

//    cmd.CommandText = "select isnull(sum(滞納額),0) as 滞納額合計,isnull(sum(変動水道代),0) as 水道代合計 from T_滞納 " +
//        "where 顧客コード='" + kokyaku_cd + "'";
//    dr = cmd.ExecuteReader();
//    if (dr.HasRows)
//    {
//        dr.Read();
//        滞納額合計.Text = dr["滞納額合計"].ToString();
//        水道代合計.Text = dr["水道代合計"].ToString();
//        dr.Close();
//    }

//    cmd.CommandText = "select isnull(sum(入金額),0) as 入金額合計 from T_滞納入金 " +
//       "where 顧客コード='" + kokyaku_cd + "'";

//    dr = cmd.ExecuteReader();
//    if (dr.HasRows)
//    {
//        dr.Read();
//        入金額合計.Text = dr["入金額合計"].ToString();
//        dr.Close();
//    }
//    int zankin;
//    zankin = Convert.ToInt32(滞納額合計.Text) + Convert.ToInt32(水道代合計.Text) - Convert.ToInt32(入金額合計.Text);
//    滞納残金.Text = zankin.ToString();


//    cmd.CommandText = "SELECT * FROM " +            
//    "(SELECT IIf(IsNull(T_契約緊急連絡先.id, 0) > 0, T_契約緊急連絡先.名称, T_契約保証人.氏名) as 関係人氏名," +
//    "IIf(IsNull(T_契約緊急連絡先.id, 0) > 0, T_契約緊急連絡先.カナ, T_契約保証人.カナ) as 関係人カナ," +
//    "IIf(IsNull(T_契約緊急連絡先.id, 0) > 0, T_契約緊急連絡先.TEL, T_契約保証人.TEL) as 関係人TEL," +
//    "IIf(IsNull(T_契約緊急連絡先.id, 0) > 0, T_契約緊急連絡先.TEL携帯, T_契約保証人.TEL携帯) as 関係人TEL携帯," +
//    "IIf(IsNull(T_契約緊急連絡先.id, 0) > 0, T_契約緊急連絡先.続柄, T_契約保証人.続柄) as 関係人続柄," +
//    "IIf(IsNull(T_契約緊急連絡先.id, 0) > 0, T_契約緊急連絡先.生年月日, T_契約保証人.生年月日) as 関係人生年月日," +
//    "IIf(IsNull(T_契約緊急連絡先.id, 0) > 0, T_契約緊急連絡先.住所, T_契約保証人.住所1) as 関係人住所1," +
//    "IIf(IsNull(T_契約緊急連絡先.id, 0) > 0, T_契約緊急連絡先.住所2, T_契約保証人.住所2) as 関係人住所2," +
//    "IIf(IsNull(T_契約緊急連絡先.id, 0) > 0, '緊急連絡先', '契約保証人') as 関係人種別, " +
//    "T_顧客.生年月日,T_顧客.性別 " +
//    "FROM T_顧客 left join T_契約保証人 ON T_顧客.顧客コード = T_契約保証人.顧客コード " +
//    "and T_契約保証人.id in (SELECT MIN(id) FROM T_契約保証人 group by 顧客コード) " +
//    "left join T_契約緊急連絡先 ON T_顧客.顧客コード = T_契約緊急連絡先.顧客コード " +
//    "and T_契約緊急連絡先.id in (SELECT MIN(id) FROM T_契約緊急連絡先 group by 顧客コード) " +
//    "where T_顧客.ID = " + kokyaku_id + ") as T_kokyaku  "; 

//    dr = cmd.ExecuteReader();
//    if (dr.HasRows)
//    {
//        dr.Read();
//        関係人氏名.Text = dr["関係人氏名"].ToString();
//        関係人カナ.Text = dr["関係人カナ"].ToString();
//        関係人TEL.Text = dr["関係人TEL"].ToString();
//        関係人TEL携帯.Text = dr["関係人TEL携帯"].ToString();
//        関係人続柄.Text = dr["関係人続柄"].ToString();                
//        関係人種別.Text = dr["関係人種別"].ToString();
//        関係人氏名.Text = dr["関係人氏名"].ToString();

//        if (dr["性別"] != DBNull.Value)
//        {
//            int genderCode = Convert.ToInt32(dr["性別"]);

//            if (genderCode == 1)
//            {
//                性別.Text = "男";
//            }
//            else if (genderCode == 2)
//            {
//                性別.Text = "女";
//            }
//        }
//        if (dr["生年月日"] != DBNull.Value)
//        {
//            DateTime dateOfBirth = (DateTime)dr["生年月日"];
//            Age.Text = Myage.CalculateAge(dateOfBirth).ToString();
//        }

//        //生年月日がnullでも空文字でもない場合
//        if (!dr.IsDBNull(dr.GetOrdinal("関係人生年月日")) && dr.GetDateTime(dr.GetOrdinal("関係人生年月日")) != DateTime.MinValue)
//        {
//            関係人年齢.Text = (GetAge((DateTime)dr["関係人生年月日"]).ToString());
//            関係人生年月日.Text = ((DateTime)dr["関係人生年月日"]).Date.ToString("yyyy/MM/dd");
//        }

//        //$""内で{}で囲んだ部分は式として解釈され、if-else文と同じように動作する
//        関係人住所.Text = $"{(dr.IsDBNull(dr.GetOrdinal("関係人住所1")) ? "" : dr["関係人住所1"].ToString())}" +
//        $"{(dr.IsDBNull(dr.GetOrdinal("関係人住所2")) ? "" : dr["関係人住所2"].ToString())}";
//    dr.Close();
//    }

//    cn.Close();

//    //this.v_顧客TableAdapter.Fill(this.rentDataSet.V_顧客, kokyaku_id);
//    //this.koushoTableAdapter.Fill(this.rentDataSet.kousho, kokyaku_id);
//    //this.t_約定内容TableAdapter.Fill(this.rentDataSet.T_約定内容);
//    //this.t_滞納TableAdapter.Fill(this.rentDataSet.T_滞納, kokyaku_cd);
//    //this.t_CODETableAdapter.Fill(this.rentDataSet.T_CODE);

//    DataGridView dgv = this.dataGridView1;
//    string col="";

//    for (int j = 0; j < 3; j++)
//    {
//        switch (j)
//        {
//            case 0:
//                col = "約定内容";
//                break;
//            case 1:
//                col = "区分";
//                break;
//            case 2:
//                col = "交渉相手";
//                break;
//        }

//        DataGridViewComboBoxColumn cbc = (DataGridViewComboBoxColumn)dgv.Columns[col];
//        System.Collections.IList list = dgv.Rows;

//        for (int i = 0; i < list.Count; i++)//プルダウンのソースである約定内容カラム　の内容をプルダウンにセットする
//        {
//            DataGridViewRow datarow = (DataGridViewRow)list[i];
//            //nullを比較するとエラーになるので先に省く
//            if (datarow.Cells[col].Value != null)
//            {
//                //コンボボックスのItemsに無く、かつ""でないものを判別
//                if ((!cbc.Items.Contains(datarow.Cells[col].Value)) && (datarow.Cells[col].Value.ToString() != ""))
//                {
//                    //コンボボックスの項目に追加する
//                    cbc.Items.Add(datarow.Cells[col].Value);
//                }
//            }
//        }
//        if (j == 0)
//        {
//            //foreach (DataRow DTdr in this.rentDataSet.T_約定内容.Rows)//DataSetT_約定内容　の内容をプルダウンにセットする
//            //{
//            //    //nullを比較するとエラーになるので先に省く
//            //    if (DTdr["約定内容"] != null)
//            //    {
//            //        //コンボボックスのItemsに無く、かつ""でないものを判別
//            //        if ((!cbc.Items.Contains(DTdr["約定内容"])) && (DTdr["約定内容"].ToString() != ""))
//            //        {
//            //            //コンボボックスの項目に追加する
//            //            cbc.Items.Add(DTdr["約定内容"]);
//            //        }
//            //    }
//            //}
//        }
//        //リストの数
//        new_cnt = list.Count-1;
//    }                        
//}


//    DataGridView dgv = this.dataGridView1;


//    Connect();
//    string sql;
//    sql = "update T_顧客 set 特記事項 = N'" + 備考.Text + "' where id =" + kokyaku_id;
//    SqlCommand cmd = new SqlCommand(sql, cn);

//    var transaction = cn.BeginTransaction();
//    cmd.Transaction = transaction;

//    try
//    {
//       cmd.ExecuteNonQuery();

//        //確定されてない時　何故か1回endedit　を行うと2回目はendeditでもセルが確定されない。
//        //入力セルを移動すると確定される
//        //2回目はvalueが空白（DBNULL）になっている この条件でfalseにする

//        if (dataGridView1.CurrentRow != null)
//            if (dataGridView1.CurrentCell.EditedFormattedValue.ToString() == dataGridView1.CurrentCell.Value.ToString())
//            {
//                this.Validate();
//                //this.koushoBindingSource.EndEdit();
//                //this.koushoTableAdapter.Update(this.rentDataSet);
//                MessageBox.Show("変更を保存しました");
//                transaction.Commit();
//                cn.Close();
//            }
//            else
//            {
//                MessageBox.Show(dataGridView1.CurrentCell.OwningColumn.HeaderText + "項目の「" + dataGridView1.CurrentCell.EditedFormattedValue.ToString() +
// "」は確定されてません。\r\n そのセルを確定してください。違うセルを選択すれば確定されます。");
//                transaction.Commit();
//                cn.Close();
//                return;
//            }
//        else
//        {
//            MessageBox.Show("変更を保存しました");
//            transaction.Commit();
//            cn.Close();
//            return;
//        }
//    }
//    catch (Exception err)
//    {
//        MessageBox.Show("保存できませんでした:" + err.Message);
//        transaction.Rollback();
//        cn.Close();
//        return;
//    }
//    //約定額に登録のあるレコードがあればメッセージ
//    foreach (DataGridViewRow row in dgv.Rows)
//    {
//        //最終行はインスタンスがないため null チェック
//        if (dgv["約定額", row.Index].Value != null)
//        {
//            int tmpint = DBNull.Value.Equals(dgv["約定額", row.Index].Value) ? 0 : (int)(dgv["約定額", row.Index].Value);

//            if (tmpint > 0)
//            {
//                MessageBox.Show("約定の登録があります。約定画面で確認してください");
//                break;
//            }
//        }
//    }
//}
