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
using GrapeCity.Win.BarCode.ValueType;
using System.Runtime.InteropServices;
using System.Threading.Channels;

namespace u_net
{
    public partial class F_社員 : Form
    {
        private Control? previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private bool setCombo = false;

        private const string BASE_CAPTION = "社員";
        // private MSHierarchicalFlexGridLib.MSHFlexGrid objGrid;  // 未使用
        //private MSHFlexGridLib.MSHFlexGrid objGrid;  // 使用する場合は適切な型に修正
        private object varOpenArgs;  // VariantはC#ではdynamicかobjectを使用
        private string strCaption;
        private int intWindowHeight;
        private int intWindowWidth;
        private int intKeyCode;

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
                return string.IsNullOrEmpty(社員コード.Text) ? "" : 社員コード.Text;
            }
        }

        public bool IsDeleted
        {
            get
            {
                bool isEmptyOrDbNull = string.IsNullOrEmpty(this.削除日時.Text) || Convert.IsDBNull(this.削除日時.Text);
                return !isEmptyOrDbNull;
            }
        }

        public bool IsDecided
        {
            get
            {
                bool isEmptyOrDbNull = string.IsNullOrEmpty(this.確定日時.Text) || Convert.IsDBNull(this.確定日時.Text);
                return !isEmptyOrDbNull;
            }
        }

        public F_社員()
        {
            this.Text = "社員";       // ウィンドウタイトルを設定
            this.MaximizeBox = false;  // 最大化ボタンを無効化
            this.MinimizeBox = false; //最小化ボタンを無効化

            InitializeComponent();
        }

        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        public void CommonConnect()
        {
            CommonConnection connectionInfo = new CommonConnection();
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

            foreach (Control control in Controls)
            {
                control.PreviewKeyDown += OriginalClass.ValidateCheck;
            }

            //実行中フォーム起動
            //string LoginUserCode = "000";//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            //LocalSetting localSetting = new LocalSetting();
            //localSetting.LoadPlace(LoginUserCode, this);

            //コンボボックスの設定
            OriginalClass ofn = new OriginalClass();
            ofn.SetComboBox(勤務地コード, "SELECT * FROM M営業所");

            this.社員分類.DataSource = new KeyValuePair<int, String>[] {
                new KeyValuePair<int, String>(1, "パート"),
                new KeyValuePair<int, String>(0, "正社員"),
            };
            this.社員分類.DisplayMember = "Value";
            this.社員分類.ValueMember = "Key";

            try
            {
                this.SuspendLayout();

                //object varOpenArgs = this.OpenArgs;

                int intWindowHeight = this.Height;
                int intWindowWidth = this.Width;


                if (string.IsNullOrEmpty(args))
                {
                    // 新規モードへ
                    if (!GoNewMode())
                    {
                        throw new Exception("初期化に失敗しました。");
                    }
                }
                else
                {
                    // 読込モードへ
                    if (!GoModifyMode())
                    {
                        throw new Exception("初期化に失敗しました。");
                    }
                    if (!string.IsNullOrEmpty(args))
                    {
                        this.社員コード.Text = args;
                        UpdatedControl(this.社員コード);
                    }
                }

                // 成功時の処理
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("初期化に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            finally
            {
                this.ResumeLayout();
                fn.WaitForm.Close();
            }
        }

        private bool GoNewMode()
        {
            try
            {
                // 各コントロール値を初期化
                VariableSet.SetControls(this);

                CommonConnect();

                string code = FunctionClass.GetNewCode(cn, CommonConstants.CH_EMPLOYEE);
                this.社員コード.Text = code.Substring(code.Length - 3);

                // 編集による変更がない状態へ遷移する
                ChangedData(false);

                // ヘッダ部動作制御
                //FunctionClass.LockData(this, false);
                this.氏名.Focus();
                this.社員コード.Enabled = false;
                this.コマンド新規.Enabled = false;
                this.コマンド読込.Enabled = true;
                this.コマンド複写.Enabled = false;
                this.コマンド削除.Enabled = false;
                this.コマンドメール.Enabled = false;
                this.コマンド承認.Enabled = false;
                this.コマンド確定.Enabled = false;
                this.コマンド登録.Enabled = false;

                return true;
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_GoNewMode - " + ex.Message);
                return false;
            }
        }

        public void ChangedData(bool isChanged)
        {
            try
            {
                if (isChanged)
                {
                    this.Text = BASE_CAPTION + "*";
                }
                else
                {
                    this.Text = BASE_CAPTION;
                }

                // キー情報を表示するコントロールを制御
                // コードにフォーカスがある状態でサブフォームから呼び出されたときの対処
                if (this.ActiveControl == this.社員コード)
                {
                    this.氏名.Focus();
                }
                this.社員コード.Enabled = !isChanged;
                this.コマンド複写.Enabled = !isChanged;
                this.コマンド削除.Enabled = !isChanged;
                this.コマンドメール.Enabled = !isChanged;
                if (IsChanged)
                {
                    コマンド確定.Enabled = true;
                }
                this.コマンド登録.Enabled = isChanged;
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_ChangedData - " + ex.Message);
                // エラー処理が必要に応じて実装
            }
        }

        private bool GoModifyMode()
        {
            try
            {
                bool result = false;

                // 各コントロールの値をクリア
                VariableSet.SetControls(this);

                // 編集による変更がない状態へ遷移
                //ChangedData(false);

                this.社員コード.Enabled = true;
                this.社員コード.Focus();
                // 仕入先コードコントロールが使用可能になってから LockData を呼び出す
                FunctionClass.LockData(this, true, "社員コード");
                this.コマンド新規.Enabled = true;
                this.コマンド読込.Enabled = false;
                this.コマンド複写.Enabled = false;
                this.コマンド確定.Enabled = false;
                this.コマンド登録.Enabled = false;
                this.Text = BASE_CAPTION;
                //if (!string.IsNullOrEmpty(this.削除日時.Text))
                //{
                //this.削除.Text = "■";
                //}

                result = true;
                return result;
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_GoModifyMode - " + ex.Message);
                return false;
            }
        }

        private void Form_Unload(object sender, FormClosingEventArgs e)
        {
            //string LoginUserCode = "000";//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            //LocalSetting test = new LocalSetting();
            //test.SavePlace(LoginUserCode, this);

            try
            {
                // 変更された場合
                if (IsChanged)
                {
                    var intRes = MessageBox.Show("変更内容を登録しますか？", BASE_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    switch (intRes)
                    {
                        case DialogResult.Yes:
                            if (SaveData())
                            {
                                // 保存に成功した場合
                                return;
                            }
                            else
                            {
                                if (MessageBox.Show("登録できませんでした。" + Environment.NewLine +
                                    "強制終了しますか？" + Environment.NewLine +
                                    "[はい]を選択した場合、社員コードは破棄されます。" + Environment.NewLine +
                                    "[いいえ]を選択した場合、終了しません。", BASE_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                                {
                                    e.Cancel = false;
                                }
                                else
                                {
                                    e.Cancel = true;
                                }
                                return;
                            }
                        case DialogResult.No:
                            // 新規モードのときはコードを戻す
                            break;
                        case DialogResult.Cancel:
                            e.Cancel = true;
                            return;
                    }
                }

                // 新規モードのときに登録しない場合は内部の更新データを元に戻す
                if (IsNewData)
                {
                    if (!string.IsNullOrEmpty(CurrentCode))
                    {

                        CommonConnect();

                        // 初版データのときのみ採番された番号を戻す
                        if (!FunctionClass.ReturnNewCode(cn, CommonConstants.CH_EMPLOYEE, CurrentCode))
                        {
                            MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine + Environment.NewLine +
                                "社員コード　：　" + CurrentCode, BASE_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_FormClosing - " + ex.Message);
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool SaveData()
        {
            Connect();
            SqlTransaction transaction = cn.BeginTransaction();
            {
                try
                {
                    DateTime now = DateTime.Now;
                    string strwhere = " 社員コード='" + this.社員コード.Text;

                    if (!DataUpdater.UpdateOrInsertDataFrom(this, cn, "M社員", strwhere, "社員コード", transaction))
                    {
                        //transaction.Rollback(); 関数内でロールバック入れた
                        return false;
                    }

                    // トランザクションをコミット
                    transaction.Commit();

                    //社員コード.Enabled = true;

                    // 新規モードのときは修正モードへ移行する
                    if (IsNewData)
                    {
                        コマンド新規.Enabled = true;
                        コマンド読込.Enabled = false;
                        コマンド複写.Enabled = true;
                        コマンド削除.Enabled = true;
                        コマンド承認.Enabled = false;
                    }
                    return true;

                }
                catch (Exception ex)
                {
                    // トランザクション内でエラーが発生した場合、ロールバックを実行
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }

                    コマンド登録.Enabled = true;
                    // エラーメッセージを表示またはログに記録
                    MessageBox.Show("データの保存中にエラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        private void コマンド新規_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.DoubleBuffered = true;

                if (this.ActiveControl == this.コマンド新規)
                {
                    this.コマンド新規.Focus();
                }

                // 変更がある
                if (this.IsChanged)
                {
                    var intRes = MessageBox.Show("変更内容を登録しますか？", "新規コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    switch (intRes)
                    {
                        case DialogResult.Yes:
                            //if (!ErrCheck()) return;
                            // 登録処理
                            if (!SaveData())
                            {
                                MessageBox.Show("エラーのため登録できません。", "新規コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                goto Bye_コマンド新規_Click;
                            }
                            break;
                        case DialogResult.Cancel:
                            goto Bye_コマンド新規_Click;
                    }
                }

                // 新規モードへ移行
                if (!GoNewMode())
                {
                    MessageBox.Show("エラーのため新規モードへ移行できません。", "新規コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    goto Bye_コマンド新規_Click;
                }
            }
            finally
            {
                this.DoubleBuffered = false;
                Cursor.Current = Cursors.Default;
            }

        Bye_コマンド新規_Click:
            return;
        }

        private void コマンド読込_Click(object sender, EventArgs e)
        {
            try
            {
                this.DoubleBuffered = true;

                CommonConnect();

                if (!this.IsChanged)
                {
                    // 新規モードで且つコードが取得済みのときはコードを戻す
                    if (this.IsNewData && !string.IsNullOrEmpty(this.CurrentCode))
                    {
                        // 採番された番号を戻す
                        if (!FunctionClass.ReturnNewCode(cn, CommonConstants.CH_EMPLOYEE, this.CurrentCode))
                        {
                            MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine +
                                "社員コード　：　" + this.CurrentCode, "読込コマンド", MessageBoxButtons.OK);
                        }
                    }
                    // 読込モードへ移行する
                    if (!GoModifyMode())
                    {
                        goto Err_コマンド読込_Click;
                    }
                    goto Bye_コマンド読込_Click;
                }

                // 変更されているときは登録確認を行う
                var intRes = MessageBox.Show("変更内容を登録しますか？", "読込コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (intRes)
                {
                    case DialogResult.Yes:
                        //if (!ErrCheck()) return;

                        // 登録処理
                        if (!SaveData())
                        {
                            MessageBox.Show("エラーのため登録できません。", "読込コマンド", MessageBoxButtons.OK);
                            return;
                        }
                        break;
                    case DialogResult.No:
                        // 新規モードで且つコードが取得済みのときはコードを戻す
                        if (this.IsNewData && !string.IsNullOrEmpty(this.CurrentCode))
                        {
                            // 採番された番号を戻す
                            if (!FunctionClass.ReturnNewCode(cn, CommonConstants.CH_EMPLOYEE, this.CurrentCode))
                            {
                                MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine +
                                    "社員コード　：　" + this.CurrentCode, "読込コマンド", MessageBoxButtons.OK);
                            }
                        }
                        break;
                    case DialogResult.Cancel:
                        return;
                }

                // 読込モードへ移行する
                if (!GoModifyMode())
                {
                    goto Err_コマンド読込_Click;
                }
            }
            finally
            {
                //this.Painting = true;
            }

        Bye_コマンド読込_Click:
            return;

        Err_コマンド読込_Click:
            //Debug.Print(this.Name + "_コマンド読込_Click - " + Err.Number + " : " + Err.Description);
            MessageBox.Show("エラーが発生しました。" + Environment.NewLine +
                "[ " + BASE_CAPTION + " ]を終了します。", "読込コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //this.Painting = true;
            this.Close();
        }

        private void コマンドメール_Click(object sender, EventArgs e)
        {
            if (電子メールアドレス.Text != null)
            {
                MessageBox.Show("電子メールアドレスを入力してください。", "メールコマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                電子メールアドレス.Focus();
            }

            //メールを作成
        }

        private void コマンド複写_Click(object sender, EventArgs e)
        {
            try
            {
                if (CopyData(FunctionClass.GetNewCode(cn, CommonConstants.CH_EMPLOYEE).Substring(3)))
                {
                    // 変更があった場合の処理
                    ChangedData(true);

                    // ヘッダ部制御
                    FunctionClass.LockData(this, false);

                    this.氏名.Focus();

                    // ボタンの有効無効の変更
                    this.コマンド新規.Enabled = false;
                    this.コマンド読込.Enabled = true;
                }

                CommonConnect();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{this.Name}_コマンド複写_Click - {ex.GetType().Name} : {ex.Message}");
                MessageBox.Show($"{ex.GetType().Name} : {ex.Message}", "複写コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CopyData(string codeString, int editionNumber = -1)
        {
            try
            {
                // キー情報を設定
                社員コード.Text = codeString;
                if (editionNumber != -1)
                {
                    //Revision.Text = editionNumber.ToString();
                }
                // 初期値を設定
                作成日時.Text = null;
                作成者コード.Text = null;
                作成者名.Text = null;
                更新日時.Text = null;
                更新者コード.Text = null;
                更新者名.Text = null;
                確定日時.Text = null;
                確定者コード.Text = null;
                //承認者日時.Text = null;
                //承認者コード.Text = null;
                //承認者名.Text = null;
                削除日時.Text = null;
                削除者コード.Text = null;

                return true;
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_CopyData - " + ex.Message);
                return false;
            }
        }

        private async void コマンド削除_Click(object sender, EventArgs e)
        {
            try
            {
                string strCode;         // 削除するデータのコード
                //int intEdition;         // 削除するデータの版数
                string strHeadCode;
                string strMsg;
                string strMsgPlus;

                strCode = this.CurrentCode;
                //intEdition = this.CurrentRevision;
                FunctionClass fn = new FunctionClass();

                DialogResult intRes;

                if (ActiveControl == this.コマンド削除)
                {
                    GetNextControl(コマンド削除, false).Focus();
                }


                //if (IsDeleted)
                //{
                // 削除済みのため復元処理
                strMsg = $"社員コード　：　{CurrentCode}{Environment.NewLine}{Environment.NewLine}" +
                          $"この社員データは削除されています。復元しますか？{Environment.NewLine}" +
                    $"削除/復元するには[OK]を選択してください。{Environment.NewLine}{Environment.NewLine}" +
                    $"※削除後も参照することができます。";

                intRes = MessageBox.Show(strMsg, "削除コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                if (intRes == DialogResult.Cancel)
                {
                    goto Bye_コマンド削除_Click;
                }
                else
                {
                    //ログイン認証フォームができたら
                    //using (var authForm = new F_認証())
                    //{
                    //authForm.ShowDialog();
                    //while (strCertificateCode == "")
                    //{

                    //if (Form.ActiveForm == null || Form.ActiveForm.Name != "認証")
                    //{
                    //    MessageBox.Show("復元操作は取り消されました。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //    return;
                    //}
                    //Application.DoEvents();

                    //}
                    Connect();
                    fn.DoWait("削除しています...");
                    if (intRes == DialogResult.Yes || intRes == DialogResult.OK)
                    {
                        if (SetDeleted(cn, strCode, -1, DateTime.Now, CommonConstants.LoginUserCode))
                            goto Err_コマンド削除_Click;
                    }
                    else
                        goto Err_コマンド削除_Click;


                    //if (SetDeleted(cn, strCode, intEdition, DateTime.Now, CommonConstants.LoginUserCode))
                    //{
                    //    MessageBox.Show("復元しました。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                    //else
                    //{
                    //    MessageBox.Show("エラーが発生しました。復元できませんでした。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //}
                    //}
                }
            //}
            //else
            //{
            //    // 削除処理

            //    strMsg = $"仕入先コード　：　{CurrentCode}{Environment.NewLine}{Environment.NewLine}" +
            //"このデータを削除しますか？{Environment.NewLine}削除後、復元することができます。";

            //    if (MessageBox.Show(strMsg, "削除コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //    {
            //        //using (var authenticationForm = new F_認証())
            //        //{
            //        //  authenticationForm.ShowDialog();

            //        //while (strCertificateCode == "")
            //        //{
            //        if (Form.ActiveForm == null || Form.ActiveForm.Name != "認証")
            //        {
            //            MessageBox.Show("削除操作は取り消されました。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //            return;
            //        }

            //        Application.DoEvents();
            //        // }

            //        Connect();
            //        if (SetDeleted(cn, strCode, intEdition, DateTime.Now, CommonConstants.LoginUserCode))
            //        {
            //            MessageBox.Show("削除しました。{Environment.NewLine}復元するには再度削除コマンドを実行してください。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //        else
            //        {
            //            MessageBox.Show("エラーが発生しました。{Environment.NewLine}削除できませんでした。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //        }
            //        // }
            //    }

            //}

            Bye_コマンド削除_Click:

                return;

            Err_コマンド削除_Click:

                return;

            }
            catch (Exception ex)
            {
                MessageBox.Show(this.Name + "_コマンド削除_Click - " + ex.Message);
            }
        }


        private bool SetDeleted(SqlConnection cn, string codeString, int editionNumber, DateTime deleteTime, string deleteUser)
        {
            try
            {
                string strKey;
                string strUpdate;
                string employeename;
                employeename = FunctionClass.EmployeeName(cn, deleteUser);

                bool isDeleted = false;

                if (editionNumber == -1)
                {
                    strKey = "社員コード=@CodeString";
                }
                else
                {
                    strKey = "社員コード=@CodeString AND 社員版数=@EditionNumber";
                }

                if (this.IsDeleted)
                {
                    strUpdate = "削除日時=NULL,削除者コード=NULL";
                }
                else
                {

                    strUpdate = "削除日時=@DeleteTime,削除者コード=@DeleteUser";
                }

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;
                    cmd.Transaction = cn.BeginTransaction();

                    cmd.Parameters.AddWithValue("@CodeString", codeString);
                    cmd.Parameters.AddWithValue("@EditionNumber", editionNumber);
                    cmd.Parameters.AddWithValue("@DeleteTime", deleteTime);
                    cmd.Parameters.AddWithValue("@DeleteUser", deleteUser);
                    cmd.Parameters.AddWithValue("@employeename", employeename);

                    string sql = "UPDATE M社員 SET " + strUpdate +
                                 ",更新日時=@DeleteTime,更新者コード=@DeleteUser WHERE " + strKey;



                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();


                    cmd.Transaction.Commit(); // トランザクション完了

                    // GUI更新
                    if (this.IsDeleted)
                    {
                        this.削除日時.Text = null;
                        this.削除者コード.Text = null;
                        this.削除.Text = null;
                    }
                    else
                    {
                        this.削除日時.Text = deleteTime.ToString();
                        this.削除者コード.Text = deleteUser;
                        this.削除.Text = "■";
                    }

                    isDeleted = false;
                }


                return isDeleted;
            }
            catch (Exception ex)
            {
                cmd.Transaction.Rollback();
                return true;
            }
        }

        private void コマンド承認_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("現在開発中です。", "承認コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド確定_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("現在開発中です。", "確定コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void コマンド登録_Click(object sender, EventArgs e)
        {

            this.DoubleBuffered = true;

            if (ActiveControl == コマンド登録)
            {
                GetNextControl(コマンド登録, false).Focus();
            }

            // 登録時におけるエラーチェック
            //if (!ErrCheck())
            //{
            //    goto Bye_コマンド登録_Click;
            //}

            FunctionClass fn = new FunctionClass();
            fn.DoWait("登録しています...");

            if (SaveData())
            {
                // 登録成功
                ChangedData(false);

                if (IsNewData)
                {
                    コマンド新規.Enabled = true;
                    コマンド読込.Enabled = false;
                    //コマンド複写.Enabled = true;
                    //コマンド削除.Enabled = true;
                }
                コマンド承認.Enabled = this.IsDecided;
                コマンド確定.Enabled = true;

                fn.WaitForm.Close();
                //MessageBox.Show("登録を完了しました", "登録コマンド", MessageBoxButtons.OK);
            }
            else
            {
                fn.WaitForm.Close();
                MessageBox.Show("登録できませんでした。", "登録コマンド", MessageBoxButtons.OK);
            }
        }

        private void コマンド終了_Click(object sender, EventArgs e)
        {
            Close(); // フォームを閉じる
        }

        private void UpdatedControl(Control controlObject)
        {
            try
            {
                string strName = controlObject.Name;
                //object varValue = controlObject.Text;
                string strSQL;
                string strKey;


                switch (strName)
                {
                    case "社員コード":
                        //FunctionClass fn = new FunctionClass();
                        //fn.DoWait("読み込んでいます...");

                        LoadData(this.CurrentCode);
                        //LoadHeader(this, this.CurrentCode);
                        //if (振込手数料負担コード.SelectedValue != null && 振込手数料負担コード.SelectedValue.ToString() == "3")
                        //{
                        //    振込手数料上限金額.Enabled = true;
                        //}
                        //else
                        //{
                        //    振込手数料上限金額.Enabled = false;
                        //}

                        //this.コマンド複写.Enabled = true;
                        //this.コマンド削除.Enabled = true;
                        //fn.WaitForm.Close();
                        break;

                    case "氏名":
                        break;
                    case "顧客名":
                        break;
                    case "顧客担当者名":
                        break;
                    case "電話番号":
                        break; ;
                    case "ファックス番号":
                        break;

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.Name + "_UpdatedControl - " + ex.Message);
            }
        }

        private void LoadData(string codeString, int editionNumber = -1)
        {
            // ヘッダ部の表示
            LoadHeader(this, codeString, editionNumber);

            FunctionClass.LockData(this, this.IsDeleted, "社員コード");
            this.コマンド複写.Enabled = true;
            this.コマンド削除.Enabled = true;
            this.コマンドメール.Enabled = !this.IsDeleted;
            this.コマンド確定.Enabled = !this.IsDeleted;
        }

        public bool LoadHeader(Form formObject, string codeString, int editionNumber = -1)
        {
            bool loadHeader = false;

            Connect();

            string strSQL;

            if (editionNumber == -1)
            {
                strSQL = "SELECT * FROM V社員ヘッダ WHERE 社員コード='" + codeString + "'";
            }
            else
            {
                strSQL = "SELECT * FROM V社員ヘッダ WHERE " +
                    "社員コード='" + codeString + "' AND 社員版数=" + editionNumber;
            }

            VariableSet.SetTable2Form(this, strSQL, cn);
            loadHeader = true;

            return loadHeader;
        }

        private bool IsError(Control controlObject, ref int Cancel)
        {
            try
            {
                int intKeyCode = 0;  // intKeyCodeの宣言が不足しているため、適切な値に置き換えてください。

                // キーコードによるチェック
                switch (intKeyCode)
                {
                    case (int)Keys.F1:
                    case (int)Keys.F2:
                    case (int)Keys.F3:
                    case (int)Keys.F4:
                    case (int)Keys.F5:
                    case (int)Keys.F6:
                    case (int)Keys.F7:
                    case (int)Keys.F8:
                    case (int)Keys.F9:
                    case (int)Keys.F10:
                    case (int)Keys.F11:
                    case (int)Keys.F12:
                        return false;  // キーコードが該当する場合はエラーチェックしない
                }

                string strName = controlObject.Name;
                object varValue = controlObject.Text;  // Valueプロパティがない場合はTextプロパティを使用

                switch (strName)
                {
                    case "社員コード":
                        if (varValue == null || string.IsNullOrWhiteSpace(varValue.ToString()))
                        {
                            MessageBox.Show("データの識別情報がありません。\nシステムエラーです。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return true;
                        }
                        break;
                    case "氏名":
                    case "ふりがな":
                    case "パート":
                    case "部":
                        if (varValue == null || string.IsNullOrWhiteSpace(varValue.ToString()))
                        {
                            MessageBox.Show($"[ {strName} ]を入力してください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return true;
                        }
                        break;
                    case "件名":
                    case "顧客名":
                    case "顧客担当者名":
                    case "納期":
                    case "納入場所":
                    case "支払条件":
                    case "有効期間":
                        if (varValue == null || string.IsNullOrWhiteSpace(varValue.ToString()))
                        {
                            MessageBox.Show($"{strName} を入力してください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return true;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                // エラーログの出力など、適切なエラーハンドリングを行う
                Debug.Print($"{controlObject.Name}_IsError - {ex.Message}");
            }

            return false;
        }

        // 日付選択フォームへの参照を保持するための変数
        private F_カレンダー dateSelectionForm;

        private void 入社年月日選択ボタン_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();
            if (dateSelectionForm.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                入社年月日.Text = selectedDate;
            }
        }

        private void 退社年月日選択ボタン_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();
            if (dateSelectionForm.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                退社.Text = selectedDate;
            }
        }

        private void 氏名_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■全角25文字まで入力できます。";
        }

        private void 氏名_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void チーム名_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■全角15文字まで入力できます。";
        }

        private void チーム名_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 氏名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(this.ActiveControl, 50);
            ChangedData(true);
        }

        private void ふりがな_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(this.ActiveControl, 50);
            ChangedData(true);
        }

        private void 役職名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(this.ActiveControl, 50);
            ChangedData(true);
        }

        private void 表示名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(this.ActiveControl, 50);
            ChangedData(true);
        }

        private void 頭文字_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(this.ActiveControl, 50);
            ChangedData(true);
        }

        private void 社員分類_TextUpdate(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 勤務地コード_TextUpdate(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 部_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(this.ActiveControl, 50);
            ChangedData(true);
        }

        private void チーム名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(this.ActiveControl, 30);
            ChangedData(true);
        }

        private void 電子メールアドレス_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(this.ActiveControl, 50);
            ChangedData(true);
        }

        private void 入社年月日_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 退社_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 担当地区コード_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 承認順序_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void textBox10_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(this.ActiveControl, 50);
            ChangedData(true);
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(this.ActiveControl, 50);
            ChangedData(true);
        }

        private void 生年月日_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(this.ActiveControl, 50);
            ChangedData(true);
        }

        private void 社員コード_Validated(object sender, EventArgs e)
        {
            //IsError(ActiveControl, Cancel);
        }

        private void 社員コード_Validating(object sender, CancelEventArgs e)
        {
            UpdatedControl(ActiveControl);
        }

        private void 社員コード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(ActiveControl, 3);
        }

        private void 勤務地コード_Validating(object sender, CancelEventArgs e)
        {

        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;
                case Keys.F1:
                    if (コマンド新規.Enabled)
                    {
                        コマンド新規.Focus();
                        コマンド新規_Click(sender, e);
                    }
                    break;
                case Keys.F2:
                    if (コマンド読込.Enabled)
                    {
                        コマンド読込.Focus();
                        コマンド読込_Click(sender, e);
                    }
                    break;
                case Keys.F3:
                    if (コマンド複写.Enabled) コマンド複写_Click(sender, e);
                    break;
                case Keys.F4:
                    if (コマンド削除.Enabled) コマンド削除_Click(sender, e);
                    break;
                case Keys.F5:
                    if (コマンドメール.Enabled)
                    {
                        コマンドメール.Focus();
                        コマンドメール_Click(sender, e);
                    }
                    //if (コマンドメーカー.Enabled) コマンドメーカー_Click(sender, e);
                    break;
                case Keys.F6:
                    //if (コマンドメール.Enabled)
                    //{
                    //    コマンドメール.Focus();
                    //    コマンドメール_Click(sender, e);
                    //}
                    break;
                case Keys.F8:
                    //if (コマンド印刷.Enabled) コマンド印刷_Click(sender, e);
                    break;
                case Keys.F9:
                    if (コマンド承認.Enabled) コマンド承認_Click(sender, e);
                    break;
                case Keys.F10:
                    if (コマンド確定.Enabled) コマンド確定_Click(sender, e);
                    break;
                case Keys.F11:
                    if (コマンド登録.Enabled) コマンド登録_Click(sender, e);
                    break;
                case Keys.F12:
                    if (コマンド終了.Enabled) コマンド終了_Click(sender, e);
                    break;

            }
        }
    }
}