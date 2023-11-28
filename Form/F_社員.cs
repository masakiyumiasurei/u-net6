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
using System.Linq.Expressions;
using System.Text.RegularExpressions;

namespace u_net
{
    public partial class F_社員 : Form
    {
        private Control previousControl;
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
        private bool noUpd = false; //setcontrolメソッドなどで主キーコードが空に更新されたタイミングで画面更新処理を行わない様にする


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

            this.tabControl1.TabPages.Remove(this.ページ79);

            //実行中フォーム起動
            string LoginUserCode = "000";//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(LoginUserCode, this);

            //コンボボックスの設定
            OriginalClass ofn = new OriginalClass();
            //ofn.SetComboBox(勤務地コード, "SELECT * FROM M営業所");
            ofn.SetComboBox(営業所コード, "SELECT 営業所コード as Display, 営業所名 as Display2, 営業所コード as Value FROM M営業所");
            営業所コード.DrawMode = DrawMode.OwnerDrawFixed;
            //ofn.SetComboBox(分類コード, "SELECT 分類記号 as Display,対象部品名 as Display2,分類コード as Value FROM M部品分類");
            //分類コード.DrawMode = DrawMode.OwnerDrawFixed;

            setCombo = false;

            this.パート.DataSource = new KeyValuePair<byte, String>[] {
                new KeyValuePair<byte, String>(0, "正社員"),
                new KeyValuePair<byte, String>(1, "パート"),
                };
            this.パート.DisplayMember = "Value";
            this.パート.ValueMember = "Key";

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
                this.コマンド確定.Enabled = true;
                this.コマンド登録.Enabled = true;

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
                コマンド登録.Enabled = isChanged;
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
                //noUpd = true;
                //VariableSet.SetControls(this);
                //this.社員コード.Enabled = true;
                //this.社員コード.Focus();
                //this.コマンド新規.Enabled = true;
                //this.コマンド読込.Enabled = false;
                //this.コマンド複写.Enabled = false;
                //this.コマンド確定.Enabled = false;
                //this.コマンド登録.Enabled = false;
                //noUpd = false;
                //return true;
                bool result = false;

                // 各コントロールの値をクリア
                VariableSet.SetControls(this);

                // 編集による変更がない状態へ遷移
                ChangedData(false);

                this.社員コード.Enabled = true;
                this.社員コード.Focus();
                // メーカーコードコントロールが使用可能になってから LockData を呼び出す
                FunctionClass.LockData(this, true, "社員コード");
                this.コマンド新規.Enabled = true;
                this.コマンド読込.Enabled = false;
                this.コマンド複写.Enabled = false;
                // this.コマンド確定.Enabled = false;
                this.コマンド登録.Enabled = false;
                if (!string.IsNullOrEmpty(this.削除日時.Text))
                {
                    this.削除.Text = "■";
                }
                if (!string.IsNullOrEmpty(this.確定日時.Text))
                {
                    this.確定.Text = "■";
                }




                result = true;
                return result;

            }
            catch (Exception ex)
            {
                noUpd = false;
                MessageBox.Show("GoModifyMode - " + ex.HResult + " : " + ex.Message);
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
            //SqlTransaction transaction = cn.BeginTransaction();
            SqlTransaction transaction;
            Control objControl1 = null;
            Control objControl2 = null;
            Control objControl3 = null;
            Control objControl4 = null;
            Control objControl5 = null;
            Control objControl6 = null;
            object varSaved1 = null;//作成日保存用（エラー発生時の対策）
            object varSaved2 = null;//作成者コード保存用（エラー発生時の対策）
            object varSaved3 = null;//作成者名保存用（エラー発生時の対策）
            object varSaved4 = null;//更新日保存用（エラー発生時の対策）
            object varSaved5 = null;//更新者コード保存用（エラー発生時の対策）
            object varSaved6 = null;//更新者名保存用（エラー発生時の対策）

            try
            {
                Connect();

                //DateTime now = DateTime.Now;
                DateTime dtmNow = FunctionClass.GetServerDate(cn);

                if (IsNewData)
                {
                    objControl1 = 作成日時;
                    objControl2 = 作成者コード;
                    objControl3 = 作成者名;

                    varSaved1 = objControl1.Text;
                    varSaved2 = objControl2.Text;
                    varSaved3 = objControl3.Text;

                    //objControl1.Text = dtmNow.ToString();
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
                //objControl4.Text = dtmNow.ToString();
                objControl5.Text = CommonConstants.LoginUserCode;
                objControl6.Text = CommonConstants.LoginUserFullName;

                // Mシリーズデータを保存
                string strwhere = " 社員コード='" + this.社員コード.Text + "'";
                transaction = cn.BeginTransaction();



                if (!DataUpdater.UpdateOrInsertDataFrom(this, cn, "M社員", strwhere, "社員コード", transaction))
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

                    return false;
                }

                //string sql = "DELETE FROM M社員 WHERE " + strwhere;
                //SqlCommand command = new SqlCommand(sql, cn, transaction);
                //command.ExecuteNonQuery();

                // トランザクションをコミット
                transaction.Commit();

                MessageBox.Show("登録を完了しました");

                社員コード.Enabled = true;

                // 新規モードのときは修正モードへ移行する
                if (IsNewData)
                {
                    コマンド新規.Enabled = true;
                    コマンド読込.Enabled = false;
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
                Cursor.Current = Cursors.WaitCursor;
                this.DoubleBuffered = true;

                if (this.ActiveControl == this.コマンド新規)
                {
                    this.コマンド新規.Focus();
                }

                //if (this.ActiveControl == this.コマンド新規)
                //{
                //    if (previousControl != null)
                //    {
                //        previousControl.Focus();
                //    }
                //}

                //// 変更があるときは登録確認を行う
                //if (this.コマンド登録.Enabled)
                //{
                //    var Res = MessageBox.Show("変更内容を登録しますか？", "新規コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                //    switch (Res)
                //    {
                //        case DialogResult.Yes:

                //            if (!ErrCheck(社員コード)) return;
                //            // 登録処理
                //            if (!SaveData())
                //            {
                //                MessageBox.Show("登録できませんでした。", "新規コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //                return;
                //            }

                //            break;
                //        case DialogResult.Cancel:
                //            return;
                //    }
                //}

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
                Cursor.Current = Cursors.WaitCursor;
                this.DoubleBuffered = true;

                if (this.ActiveControl == this.コマンド読込)
                {
                    this.コマンド読込.Focus();
                }

                CommonConnect();

                if (!this.IsChanged)
                {
                    // 新規モードで且つコードが取得済みのときはコードを戻す
                    if (this.IsNewData && !string.IsNullOrEmpty(this.CurrentCode))
                    {
                        // 採番された番号を戻す
                        if (!FunctionClass.ReturnNewCode(cn, CommonConstants.CH_EMPLOYEE, this.CurrentCode))
                        {
                            MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine + Environment.NewLine +
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
                        if (!ErrCheck(社員コード)) return;

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
            コマンド新規.Enabled = true;
            コマンド終了.Enabled = true;
            コマンド削除.Enabled = false;
            コマンドメール.Enabled = false;
            コマンド確定.Enabled = false;
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
                Cursor.Current = Cursors.WaitCursor;
                this.DoubleBuffered = true;

                if (this.ActiveControl == this.コマンド複写)
                {
                    this.コマンド複写.Focus();
                }

                CommonConnect();

                string newCode = FunctionClass.GetNewCode(cn, CommonConstants.CH_MAKER);
                if (CopyData(newCode.Substring(newCode.Length - 3), 1))
                //if (CopyData(FunctionClass.GetNewCode(cn, CommonConstants.CH_EMPLOYEE).Substring(3)))
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

                //CommonConnect();
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
                          $"この社員データは削除/復元します。{Environment.NewLine}" +
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
                if (DeleteData(CurrentCode))
                {
                    削除.Text = "■";
                    MessageBox.Show("削除しました。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.SuspendLayout();

                    // 新規モードへ移行
                    if (!GoNewMode())
                    {
                        MessageBox.Show($"エラーのため新規モードへ移行できません。\n[{Name}]を終了します。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.ResumeLayout();
                        Close();
                    }

                    this.ResumeLayout();
                }
                else
                {
                    MessageBox.Show("削除できませんでした。他のユーザーにより削除されている可能性があります。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                fn.WaitForm.Close();

                return;

            Err_コマンド削除_Click:

                return;

            }
            catch (Exception ex)
            {
                MessageBox.Show(this.Name + "_コマンド削除_Click - " + ex.Message);
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
                // データベース操作のためのSQL文
                string selectSql = "SELECT * FROM M社員 WHERE 社員コード = " + codeParam + " AND 削除日時 IS NULL";
                string updateSql = "UPDATE M社員 SET 削除日時 = GETDATE(), 削除者コード = @UserCode WHERE 社員コード = " + codeParam + " AND 削除日時 IS NULL";

                using (SqlCommand selectCommand = new SqlCommand(selectSql, cn, transaction))
                {
                    selectCommand.Parameters.Add(new SqlParameter(codeParam, codeString));
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            // 商品が見つかった場合、削除処理を実行
                            reader.Close(); // 同じ接続なのでデータリーダーを閉じておく

                            using (SqlCommand updateCommand = new SqlCommand(updateSql, cn, transaction))
                            {
                                updateCommand.Parameters.Add(new SqlParameter(codeParam, codeString));
                                updateCommand.Parameters.Add(new SqlParameter("@UserCode", CommonConstants.LoginUserCode)); // LoginUserCode に適切な値を設定

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

        private bool ErrCheck(Control control)
        {
            //入力確認
            switch (control.Name)
            {
                case "氏名":
                    if (!FunctionClass.IsError(control)) return false;
                    break;
                case "社員コード":
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
            }
            return true;
        }

        private void コマンド承認_Click(object sender, EventArgs e)
        {
            //MessageBox.Show("現在開発中です。", "承認コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド確定_Click(object sender, EventArgs e)
        {
            try
            {
                object varSaved1 = null; // Use object for nullable values
                object varSaved2 = null;

                ////PreviousControlがオプショングループのときにAccessの応答がなくなる
                //if (this.ActiveControl == this.コマンド確定)
                //    previousControl.Focus();
                if (this.ActiveControl == this.コマンド確定)
                {
                    this.コマンド確定.Focus();
                }


                if (!IsDecided)
                {
                    if (IsErrorData("社員コード"))
                        goto Bye_コマンド確定_Click;
                }

                varSaved1 = 確定日時.Text;
                varSaved2 = 確定者コード.Text;

                if (IsDecided)
                {
                    //確定日時.Text = null;
                    //確定者コード.Text = null;
                    //削除日時.Text = null;
                    //退社.Text = null;
                    //生年月日.Text = null;
                    //作成日時.Text = null;
                    //更新日時.Text = null;
                    //入社年月日.Text = null;
                }
                else
                {
                    //確定日時.Text = null;
                    確定日時.Text = DateTime.Now.ToString();
                    確定者コード.Text = CommonConstants.LoginUserCode;
                    更新日時.Text = DateTime.Now.ToString();
                    作成日時.Text = DateTime.Now.ToString();


                    //削除日時.Text=null;
                    //退社.Text = null;
                    //生年月日.Text = null;
                    //作成日時.Text = null;
                    //更新日時.Text = null;
                    //入社年月日.Text = null;

                }

                FunctionClass fn = new FunctionClass();
                fn.DoWait("登録しています...");

                if (SaveData())
                {
                    ChangedData(false);

                    if (IsNewData)
                    {
                        コマンド新規.Enabled = true;
                        コマンド読込.Enabled = false;
                    }
                }
                else
                {
                    確定日時.Text = varSaved1.ToString();
                    確定者コード.Text = varSaved2.ToString();
                    MessageBox.Show("確定できませんでした。", "確定コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                FunctionClass.LockData(this, IsDecided || IsDeleted, "社員コード");
                fn.WaitForm.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーが発生しました。" + Environment.NewLine + "確定コマンドは取り消されました。", "確定コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
                goto Bye_コマンド確定_Click;
            }

        Bye_コマンド確定_Click:

            return;
        }

        private bool IsErrorData(string ExFieldName1, string ExFieldName2 = "")
        {
            //カレントデータのエラーをチェックする
            //ExFieldName - 除外するフィールド名
            //戻り値
            //エラーあり - True
            //エラーなし - False

            bool isErrorData = false;

            foreach (Control objControl in Controls)
            {
                if ((objControl is TextBox || objControl is ComboBox) && objControl.Visible)
                {
                    if (objControl.Name != ExFieldName1 && objControl.Name != ExFieldName2)
                    {
                        if (FunctionClass.IsError(objControl))
                        {
                            isErrorData = true;
                            objControl.Focus();
                            return isErrorData;
                        }
                    }
                }
            }
            return isErrorData;
        }

        private void コマンド登録_Click(object sender, EventArgs e)
        {
            ////保存確認
            //if (MessageBox.Show("変更内容を保存しますか？", "保存確認",
            //    MessageBoxButtons.OKCancel,
            //    MessageBoxIcon.Question) == DialogResult.OK)
            //{
            //    //if (!ErrCheck("")) return;

            //    if (!SaveData()) return;
            //}

            this.DoubleBuffered = true;

            if (ActiveControl == コマンド登録)
            {
                GetNextControl(コマンド登録, false).Focus();
            }

            //// 登録時におけるエラーチェック
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
                object varValue = controlObject.Text;
                //string strSQL;
                //string strKey;


                switch (strName)
                {
                    case "社員コード":
                        //FunctionClass fn = new FunctionClass();
                        //fn.DoWait("読み込んでいます...");

                        LoadData(this.CurrentCode);
                        ChangedData(false);
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
            //this.コマンド削除.Enabled = true;
            this.コマンド削除.Enabled = !this.IsDeleted;
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

        //private bool ErrCheck(Control argscontrol, string? tname = null)
        //{
        //    ////入力確認    
        //    //if (!FunctionClass.IsError(this.社員コード)) return false;
        //    //if (!FunctionClass.IsError(this.氏名)) return false;
        //    //if (!FunctionClass.IsError(this.ふりがな)) return false;
        //    //if (!FunctionClass.IsError(this.部)) return false;
        //    foreach (Control control in argscontrol.Controls)
        //        //入力確認
        //        if (string.IsNullOrEmpty(tname) || tname == control.Name)
        //        {
        //            switch (control.Name)
        //            {
        //                case "氏名":
        //                    if (!FunctionClass.IsError(control)) return false;
        //                    break;
        //                case "社員コード":
        //                    if (!FunctionClass.IsError(control)) return false;
        //                    if (OriginalClass.IsNumeric(control))
        //                    {
        //                        MessageBox.Show("数字を入力してください。: ", "数値判定エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);

        //                        return false;
        //                    }
        //                    double result;
        //                    double.TryParse(control.Text, out result);
        //                    if (result < 0)
        //                    {
        //                        MessageBox.Show("0 以上の値を入力してください。", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //                        return false;
        //                    }
        //                    break;
        //                case "ふりがな":
        //                    if (!FunctionClass.IsError(control)) return false;
        //                    break;
        //                case "部":
        //                    if (!FunctionClass.IsError(control)) return false;
        //                    break;
        //            }
        //        }
        //    return true;
        //}

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
            FunctionClass.LimitText(氏名, 50);
            ChangedData(true);
        }

        private void ふりがな_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(ふりがな, 50);
            ChangedData(true);
        }

        private void 役職名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(役職名, 50);
            ChangedData(true);
        }

        private void 表示名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(表示名, 50);
            ChangedData(true);
        }

        private void 頭文字_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(頭文字, 50);
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
            FunctionClass.LimitText(部, 50);
            ChangedData(true);
        }

        private void チーム名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(チーム名, 30);
            ChangedData(true);
        }

        private void 電子メールアドレス_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(電子メールアドレス, 50);
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
            FunctionClass.LimitText(textBox10, 50);
            ChangedData(true);
        }

        private void textBox9_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(textBox9, 50);
            ChangedData(true);
        }

        private void 生年月日_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(生年月日, 50);
            ChangedData(true);
        }

        private void 社員コード_Validating(object sender, CancelEventArgs e)
        {
            //UpdatedControl(ActiveControl);
            FunctionClass.IsError((Control)sender);
        }

        private void 社員コード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(社員コード, 3);
        }

        private void 社員コード_KeyDown(object sender, KeyEventArgs e)
        {
            //// 入力された値がエラー値の場合、textプロパティが設定できなくなるときの対処
            //if (e.KeyCode == Keys.Return) // Enter キーが押されたとき
            //{
            //    string strCode = 社員コード.Text;
            //    if (string.IsNullOrEmpty(strCode)) return;

            //    strCode = strCode.PadLeft(3, '0'); // ゼロで桁を埋める例
            //    if (strCode != 社員コード.Text)
            //    {
            //        社員コード.Text = strCode;
            //    }
            //}
        }

        private void 勤務地コード_Validating(object sender, CancelEventArgs e)
        {
            if (FunctionClass.IsError(sender as Control) == true) e.Cancel = true;
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;

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

        private void 社員コード_Validated(object sender, EventArgs e)
        {
            //FunctionClass.IsError(ActiveControl);
            UpdatedControl((Control)sender);
        }

        private void 勤務地コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            勤務地名.Text = ((DataRowView)営業所コード.SelectedItem)?.Row.Field<String>("Display2")?.ToString();
            ChangedData(true);
        }

        private void 勤務地コード_TextChanged(object sender, EventArgs e)
        {
            if (営業所コード.SelectedValue == null)
                勤務地名.Text = null;
        }

        private void 勤務地コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 50, 150 }, new string[] { "Display", "Display2" });
            営業所コード.Invalidate();
            営業所コード.DroppedDown = true;

        }

        private void 営業所コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                ComboBox comboBox = sender as ComboBox;
                if (comboBox != null)
                {
                    comboBox.DroppedDown = true;
                }
                e.KeyChar = (char)0;
            }
        }
    }
}