using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data.OleDb;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using Microsoft.Win32;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using u_net.Public;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using System.Net.NetworkInformation;

namespace u_net
{



    public partial class F_メーカー : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "メーカー";






        public F_メーカー()
        {
            this.Text = "メーカー";       // ウィンドウタイトルを設定
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

        //SqlConnection cn = new SqlConnection();
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

            //実行中フォーム起動
            string LoginUserCode = CommonConstants.LoginUserCode;
            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(LoginUserCode, this);

            try
            {
                this.SuspendLayout();
                //DoWait("しばらくお待ちください...");

                //IntPtr hIcon = LoadIconFromPath(CurrentProject.Path + "\\card.ico");
                //SendMessage(this.Handle, WM_SETICON, new IntPtr(1), hIcon);

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
                    // 修正モードへ
                    if (!GoModifyMode())
                    {
                        throw new Exception("初期化に失敗しました。");
                    }
                    if (!string.IsNullOrEmpty(args))
                    {
                        this.メーカーコード.Text = args;
                        UpdatedControl(メーカーコード);
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

                string code = FunctionClass.GetNewCode(cn, CommonConstants.CH_MAKER);
                this.メーカーコード.Text = code.Substring(code.Length - 8);
                // this.メーカーコード.Text = 採番(objConnection, CH_MAKER).Substring(採番(objConnection, CH_MAKER).Length - 8);
                this.Revision.Text = 1.ToString();

                // 編集による変更がない状態へ遷移する
                ChangedData(false);

                // ヘッダ部動作制御
                FunctionClass.LockData(this, false);
                this.メーカー名.Focus();
                this.メーカーコード.Enabled = false;
                this.コマンド新規.Enabled = false;
                this.コマンド読込.Enabled = true;
                this.コマンド複写.Enabled = false;
                this.コマンド削除.Enabled = false;
                this.コマンドメール.Enabled = false;
                // this.コマンド承認.Enabled = false;
                // this.コマンド確定.Enabled = false;
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
                if (this.ActiveControl == this.メーカーコード)
                {
                    this.メーカー名.Focus();
                }
                this.メーカーコード.Enabled = !isChanged;
                this.コマンド複写.Enabled = !isChanged;
                this.コマンド削除.Enabled = !isChanged;
                this.コマンドメール.Enabled = !isChanged;
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
                ChangedData(false);

                this.メーカーコード.Enabled = true;
                this.メーカーコード.Focus();
                // メーカーコードコントロールが使用可能になってから LockData を呼び出す
                FunctionClass.LockData(this, true, "メーカーコード");
                this.コマンド新規.Enabled = true;
                this.コマンド読込.Enabled = false;
                this.コマンド複写.Enabled = false;
                // this.コマンド確定.Enabled = false;
                this.コマンド登録.Enabled = false;
                if (!string.IsNullOrEmpty(this.削除日時.Text))
                {
                    this.削除.Text = "■";
                }



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

            string LoginUserCode = CommonConstants.LoginUserCode;
            LocalSetting test = new LocalSetting();
            test.SavePlace(LoginUserCode, this);


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
                                    "[はい]を選択した場合、メーカーコードは破棄されます。" + Environment.NewLine +
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
                    if (!string.IsNullOrEmpty(CurrentCode) && CurrentRevision == 1)
                    {

                        CommonConnect();

                        // 初版データのときのみ採番された番号を戻す
                        if (!FunctionClass.ReturnNewCode(cn, CommonConstants.CH_MAKER, CurrentCode))
                        {
                            MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine +
                                "メーカーコード　：　" + CurrentCode, BASE_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                return string.IsNullOrEmpty(メーカーコード.Text) ? "" : メーカーコード.Text;
            }
        }

        public int CurrentRevision
        {
            get
            {
                return string.IsNullOrEmpty(Revision.Text) ? 0 : int.Parse(Revision.Text);
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


        private bool SaveData()
        {


            Connect();
            SqlTransaction transaction = cn.BeginTransaction();
            {


                try
                {


                    DateTime now = DateTime.Now;
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

                    bool isNewData = IsNewData;

                    if (isNewData)
                    {
                        objControl1 = 作成日時;
                        objControl2 = 作成者コード;
                        objControl3 = 作成者名;
                        varSaved1 = objControl1.Text;
                        varSaved2 = objControl2.Text;
                        varSaved3 = objControl3.Text;
                        varSaved7 = ActiveDate.Text;
                        objControl1.Text = now.ToString();
                        objControl2.Text = CommonConstants.LoginUserCode;
                        objControl3.Text = CommonConstants.LoginUserFullName;
                        ActiveDate.Text = now.ToString();
                    }

                    objControl4 = 更新日時;
                    objControl5 = 更新者コード;
                    objControl6 = 更新者名;

                    // 登録前の状態を退避しておく
                    varSaved4 = objControl4.Text;
                    varSaved5 = objControl5.Text;
                    varSaved6 = objControl6.Text;

                    // 値の設定
                    objControl4.Text = now.ToString();
                    objControl5.Text = CommonConstants.LoginUserCode;
                    objControl6.Text = CommonConstants.LoginUserFullName;



                    string strwhere = " メーカーコード='" + this.メーカーコード.Text + "' and Revision=" + this.Revision.Text;

                    if (!DataUpdater.UpdateOrInsertDataFrom(this, cn, "Mメーカー", strwhere, "メーカーコード", transaction, "Revision"))
                    {
                        //transaction.Rollback(); 関数内でロールバック入れた


                        if (isNewData)
                        {
                            objControl1.Text = varSaved1.ToString();
                            objControl2.Text = varSaved2.ToString();
                            objControl3.Text = varSaved3.ToString();
                            ActiveDate.Text = varSaved7.ToString();

                        }

                        objControl4.Text = varSaved4.ToString();
                        objControl5.Text = varSaved5.ToString();
                        objControl6.Text = varSaved6.ToString();






                        return false;
                    }



                    // トランザクションをコミット
                    transaction.Commit();




                    メーカーコード.Enabled = true;

                    // 新規モードのときは修正モードへ移行する
                    if (IsNewData)
                    {
                        コマンド新規.Enabled = true;
                    }

                    コマンド複写.Enabled = true;
                    コマンド削除.Enabled = true;
                    コマンド登録.Enabled = false;

                    return true;

                }
                catch (Exception ex)
                {
                    // トランザクション内でエラーが発生した場合、ロールバックを実行
                    //if (transaction != null)
                    //{
                    //    transaction.Rollback();
                    //}

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

                //this.Painting = false;

                CommonConnect();

                if (!this.IsChanged)
                {
                    // 新規モードで且つコードが取得済みのときはコードを戻す
                    if (this.IsNewData && !string.IsNullOrEmpty(this.CurrentCode))
                    {



                        // 採番された番号を戻す
                        if (!FunctionClass.ReturnNewCode(cn, CommonConstants.CH_MAKER, this.CurrentCode))
                        {
                            MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine +
                                "メーカーコード　：　" + this.CurrentCode, "読込コマンド", MessageBoxButtons.OK);
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
                        if (!ErrCheck()) return;

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
                            if (!FunctionClass.ReturnNewCode(cn, CommonConstants.CH_MAKER, this.CurrentCode))
                            {
                                MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine +
                                    "メーカーコード　：　" + this.CurrentCode, "読込コマンド", MessageBoxButtons.OK);
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




        private bool ErrCheck()
        {
            //入力確認    
            if (!FunctionClass.IsError(this.メーカーコード)) return false;
            if (!FunctionClass.IsError(this.メーカー名)) return false;
            if (!FunctionClass.IsError(this.メーカー名フリガナ)) return false;
            if (!FunctionClass.IsError(this.メーカー省略名)) return false;
            return true;
        }


        private void コマンド複写_Click(object sender, EventArgs e)
        {
            try
            {
                if (ActiveControl == コマンド複写)
                {
                    GetNextControl(コマンド複写, false).Focus();
                }

                this.DoubleBuffered = true;
                //this.Painting = false;

                CommonConnect();

                string newCode = FunctionClass.GetNewCode(cn, CommonConstants.CH_MAKER);
                if (CopyData(newCode.Substring(newCode.Length - 8), 1))
                {
                    ChangedData(true);
                    FunctionClass.LockData(this, false);

                    this.メーカー名.Focus();
                    this.コマンド新規.Enabled = false;
                    this.コマンド読込.Enabled = true;
                }
            }
            finally
            {
                //this.Painting = true;
            }
        }


        private bool CopyData(string codeString, int editionNumber = -1)
        {
            try
            {
                // キー情報を設定
                メーカーコード.Text = codeString;
                if (editionNumber != -1)
                {
                    Revision.Text = editionNumber.ToString();
                }

                // 初期値を設定
                作成日時.Text = null;
                作成者コード.Text = null;
                作成者名.Text = null;
                更新日時.Text = null;
                更新者コード.Text = null;
                更新者名.Text = null;
                // 他の値も同様に設定

                return true;
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_CopyData - " + ex.Message);
                return false;
            }
        }



        private void コマンド削除_Click(object sender, EventArgs e)
        {
            try
            {
                string strCode;         // 削除するデータのコード
                int intEdition;         // 削除するデータの版数
                string strMsg;

                strCode = this.CurrentCode;
                intEdition = this.CurrentRevision;
                FunctionClass fn = new FunctionClass();

                DialogResult intRes;


                if (ActiveControl == コマンド削除)
                {
                    GetNextControl(コマンド削除, false).Focus();
                }

                if (intEdition == 1)
                {
                    // 初版の場合
                    strMsg = "メーカーコード　：　" + strCode + "\r\n\r\n" +
                        "このメーカーデータを削除/復元します。\r\n" +
                        "削除/復元するには[はい]を選択してください。\r\n\r\n" +
                        "※削除後も参照することができます。";
                    intRes = MessageBox.Show(strMsg, "削除コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                }
                else
                {
                    // 2版以上の場合
                    strMsg = "このバージョンのU-netでは操作できません。\r\n" +
                        "最新バージョンのU-netで操作してください";
                    intRes = MessageBox.Show(strMsg, "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (intRes == DialogResult.Cancel)
                {
                    goto Bye_コマンド削除_Click;
                }
                else
                {


                    // 削除処理

                    Connect();


                    if (intRes == DialogResult.Yes)
                    {

                        fn.DoWait("削除しています...");

                        // 応答がYesのとき
                        if (SetDeleted(cn, strCode, intEdition, DateTime.Now, CommonConstants.LoginUserCode))
                        {
                            fn.WaitForm.Close();
                            goto Err_コマンド削除_Click;
                        }

                        fn.WaitForm.Close();
                    }
                    else if (intRes == DialogResult.OK)
                    {

                    }
                    else
                    {
                        goto Bye_コマンド削除_Click;
                    }
                }

            Bye_コマンド削除_Click:
                //DoCmd.Close(AcObjectType.acForm, "実行中", AcCloseSave.acSavePrompt);

                return;

            Err_コマンド削除_Click:
                MessageBox.Show("エラーが発生しました。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
                goto Bye_コマンド削除_Click;
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_コマンド削除_Click - " + ex.Message);
            }
        }


        private bool SetDeleted(SqlConnection cn, string codeString, int editionNumber, DateTime deleteTime, string deleteUser)
        {
            try
            {
                string strKey;
                string strUpdate;

                bool isDeleted = false;

                if (editionNumber == -1)
                {
                    strKey = "メーカーコード=@CodeString";
                }
                else
                {
                    strKey = "メーカーコード=@CodeString AND Revision=@EditionNumber";
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

                    string sql = "UPDATE Mメーカー SET " + strUpdate +
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
                // エラーハンドリングを実装
                return true;
            }
        }





        private void コマンド仕入先_Click(object sender, EventArgs e)
        {
            F_仕入先 targetform = new F_仕入先();

            targetform.ShowDialog();
        }

        private void コマンドメール_Click(object sender, EventArgs e)
        {
            string toEmail = Convert.ToString(担当者メールアドレス.Text);

            if (string.IsNullOrEmpty(toEmail))
            {
                MessageBox.Show("メールアドレスを入力してください。", "メールコマンド", MessageBoxButtons.OK);
                担当者メールアドレス.Focus();
                return;
            }

            // デフォルトのメールクライアントを起動して新しいメールを作成
            try
            {
                string mailtoLink = "mailto:" + toEmail;
                System.Diagnostics.Process.Start(new ProcessStartInfo(mailtoLink) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("メールを起動できませんでした。\nエラー: " + ex.Message, "メールコマンド", MessageBoxButtons.OK);
            }
        }


        private static string GetDefaultMailerExePath()
        {
            return _GetDefaultExePath(@"mailto\shell\open\command");
        }

        private static string _GetDefaultExePath(string keyPath)
        {
            string path = "";

            // レジストリ・キーを開く
            // 「HKEY_CLASSES_ROOT\xxxxx\shell\open\command」
            RegistryKey rKey = Registry.ClassesRoot.OpenSubKey(keyPath);
            if (rKey != null)
            {
                // レジストリの値を取得する
                string command = (string)rKey.GetValue(String.Empty);
                if (command == null)
                {
                    return path;
                }

                // 前後の余白を削る
                command = command.Trim();
                if (command.Length == 0)
                {
                    return path;
                }

                // 「"」で始まる長いパス形式かどうかで処理を分ける
                if (command[0] == '"')
                {
                    // 「"～"」間の文字列を抽出
                    int endIndex = command.IndexOf('"', 1);
                    if (endIndex != -1)
                    {
                        // 抽出開始を「1」ずらす分、長さも「1」引く
                        path = command.Substring(1, endIndex - 1);
                    }
                }
                else
                {
                    // 「（先頭）～（スペース）」間の文字列を抽出
                    int endIndex = command.IndexOf(' ');
                    if (endIndex != -1)
                    {
                        path = command.Substring(0, endIndex);
                    }
                    else
                    {
                        path = command;
                    }
                }
            }

        return path;
        }


        private void コマンド印刷_Click(object sender, EventArgs e)
        {
            // デスクトップフォルダのパスを取得
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            // 画面のキャプチャをデスクトップに保存
            string screenshotFileName = "screenshot.png";
            string screenshotFilePath = Path.Combine(desktopPath, screenshotFileName);
            OriginalClass.CaptureActiveForm(screenshotFilePath);

            // 印刷ダイアログを表示
            OriginalClass.PrintScreen(screenshotFilePath);
        }



        private void コマンド承認_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。", "承認コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド確定_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。", "確定コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド登録_Click(object sender, EventArgs e)
        {
            //try
            //{
            this.DoubleBuffered = true;

            //this.Painting = false;

            if (ActiveControl == コマンド登録)
            {
                GetNextControl(コマンド登録, false).Focus();
            }

            // 登録時におけるエラーチェック
            if (!ErrCheck())
            {
                goto Bye_コマンド登録_Click;
            }

            FunctionClass fn = new FunctionClass();
            fn.DoWait("登録しています...");

            if (SaveData())
            {
                // 登録成功
                ChangedData(false);

                if (IsNewData)
                {
                    // 新規モードの場合、版数一覧を更新し、ボタンの状態を変更
                    // Me.メーカー版数.Requery(); // データを再読み込む処理が必要
                    コマンド新規.Enabled = true;
                    コマンド読込.Enabled = false;
                }

                // その他の処理を追加
                // Me.コマンド承認.Enabled = Me.IsDecided;
                // Me.コマンド確定.Enabled = true;
                fn.WaitForm.Close();
                MessageBox.Show("登録を完了しました", "登録コマンド", MessageBoxButtons.OK);
            }
            else
            {
                fn.WaitForm.Close();
                MessageBox.Show("登録できませんでした。", "登録コマンド", MessageBoxButtons.OK);
            }


        //}
        //finally
        //{
        //Close();
        //this.Painting = true;
        //}

        Bye_コマンド登録_Click:
            return;
        }

        private void コマンド終了_Click(object sender, EventArgs e)
        {
            // エラーハンドリングはC#では別の方法を使用するため、ここでは省略
            // On Error Resume Next の代替方法はC#にはありません

            Close(); // フォームを閉じる
        }



        private void Form_KeyDown(object sender, KeyEventArgs e)
        {

            bool intShiftDown = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;

            if (intShiftDown)
            {
                Debug.Print(Name + " - Shiftキーが押されました");
            }

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
                    if (コマンド仕入先.Enabled) コマンド仕入先_Click(sender, e);
                    break;
                case Keys.F6:
                    if (コマンドメール.Enabled)
                    {
                        コマンドメール.Focus();
                        コマンドメール_Click(sender, e);
                    }
                    break;
                case Keys.F8:
                    if (コマンド印刷.Enabled) コマンド印刷_Click(sender, e);
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




        private void UpdatedControl(Control controlObject)
        {
            try
            {
                string strName = controlObject.Name;
                object varValue = controlObject.Text; // Assuming that the Value property is equivalent to the Text property in your case

                switch (strName)
                {
                    case "メーカーコード":
                        LoadData(this.CurrentCode);
                        ChangedData(false);
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_UpdatedControl - " + ex.Message);
            }
        }


        private void LoadData(string codeString, int editionNumber = -1)
        {
            // ヘッダ部の表示
            LoadHeader(this, codeString, editionNumber);

            // 動作を制御する
            // Me は Form クラスのインスタンスとして使用されることを仮定しています
            // フォームのクラス名で適切な型に置き換えてください
            FunctionClass.LockData(this, this.IsDeleted, "メーカーコード");
            this.コマンド複写.Enabled = true;
            this.コマンド削除.Enabled = !this.IsDeleted;
            this.コマンドメール.Enabled = !this.IsDeleted;
        }


        public bool LoadHeader(Form formObject, string codeString, int editionNumber = -1)
        {
            bool loadHeader = false;

            Connect();


            string strSQL;
            if (editionNumber == -1)
            {
                strSQL = "SELECT * FROM Vメーカーヘッダ WHERE メーカーコード='" + codeString + "'";
            }
            else
            {
                strSQL = "SELECT * FROM Vメーカーヘッダ WHERE メーカーコード='" + codeString + "' AND Revision= " + editionNumber;
            }

            //using (SqlCommand command = new SqlCommand(strSQL, cn))
            //{
            //    command.Parameters.AddWithValue("@codeString", codeString);
            //    if (editionNumber != -1)
            //    {
            //        command.Parameters.AddWithValue("@editionNumber", editionNumber);
            //    }

            //    using (SqlDataReader reader = command.ExecuteReader())
            //    {
            //        if (reader.Read())
            //        {
            //ここ修正した。
            VariableSet.SetTable2Form(this, strSQL, cn);
            loadHeader = true;
            //        }
            //    }
            //}


            return loadHeader;
        }


        private void ウェブアドレス_Click(object sender, EventArgs e)
        {
            string inputText = ウェブアドレス.Text;

            // 入力が有効な URL の形式であるかを確認
            if (OriginalClass.IsValidUrl(inputText))
            {
                OriginalClass.OpenUrl(inputText);

            }
        }





        private async void 郵便番号_Validated(object sender, EventArgs e)
        {
            // 郵便番号のテキストボックスの内容を取得
            string zipCode = 郵便番号.Text;

            // 郵便番号が正しい形式かどうかを確認
            if (OriginalClass.IsValidZipCode(zipCode) && string.IsNullOrEmpty(住所1.Text))
            {
                // 郵便番号APIを使用して住所情報を取得
                string address = await OriginalClass.GetAddressFromZipCode(zipCode);
                住所1.Text = address;
            }
            else
            {
                // 郵便番号が正しい形式でない場合、エラーメッセージなどを表示
                住所1.Text = null;
            }


            UpdatedControl((Control)sender);

        }






        private void ウェブアドレス_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 100);
            ChangedData(true);
        }

        private void メーカーコード_Validated(object sender, EventArgs e)
        {
            UpdatedControl((Control)sender);
        }

        private void メーカーコード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (!メーカーコード.Modified) return;

            FunctionClass.IsError((Control)sender);
        }

        private void メーカーコード_KeyDown(object sender, KeyEventArgs e)
        {
            // 入力された値がエラー値の場合、textプロパティが設定できなくなるときの対処
            if (e.KeyCode == Keys.Return) // Enter キーが押されたとき
            {
                string strCode = メーカーコード.Text;
                if (string.IsNullOrEmpty(strCode)) return;

                strCode = strCode.PadLeft(8, '0'); // ゼロで桁を埋める例
                if (strCode != メーカーコード.Text)
                {
                    メーカーコード.Text = strCode;
                }
            }
        }



        private void メーカー省略名_Validated(object sender, EventArgs e)
        {
            UpdatedControl((Control)sender);
        }


        private void メーカー省略名_TextChanged(object sender, EventArgs e)
        {

            FunctionClass.LimitText(((TextBox)sender), 10);
            ChangedData(true);
        }



        private void メーカー名_Validated(object sender, EventArgs e)
        {
            UpdatedControl((Control)sender);
        }


        private void メーカー名_TextChanged(object sender, EventArgs e)
        {


            FunctionClass.LimitText(((TextBox)sender), 60);
            ChangedData(true);
        }



        private void メーカー名フリガナ_Validated(object sender, EventArgs e)
        {
            UpdatedControl((Control)sender);
        }


        private void メーカー名フリガナ_TextChanged(object sender, EventArgs e)
        {

            FunctionClass.LimitText(((TextBox)sender), 120);
            ChangedData(true);
        }



        private void 仕入先1_Validated(object sender, EventArgs e)
        {
            UpdatedControl((Control)sender);
        }


        private void 仕入先1_TextChanged(object sender, EventArgs e)
        {

            FunctionClass.LimitText(((TextBox)sender), 60);
            ChangedData(true);
        }


        private void 仕入先2_Validated(object sender, EventArgs e)
        {
            UpdatedControl((Control)sender);
        }


        private void 仕入先2_TextChanged(object sender, EventArgs e)
        {

            FunctionClass.LimitText(((TextBox)sender), 60);
            ChangedData(true);
        }


        private void 仕入先3_Validated(object sender, EventArgs e)
        {
            UpdatedControl((Control)sender);
        }


        private void 仕入先3_TextChanged(object sender, EventArgs e)
        {

            FunctionClass.LimitText(((TextBox)sender), 60);
            ChangedData(true);
        }



        private void 住所1_Validated(object sender, EventArgs e)
        {
            UpdatedControl((Control)sender);
        }


        private void 住所1_TextChanged(object sender, EventArgs e)
        {

            FunctionClass.LimitText(((TextBox)sender), 50);
            ChangedData(true);
        }


        private void 住所2_Validated(object sender, EventArgs e)
        {
            UpdatedControl((Control)sender);
        }


        private void 住所2_TextChanged(object sender, EventArgs e)
        {

            FunctionClass.LimitText(((TextBox)sender), 50);
            ChangedData(true);
        }




        private void 担当者メールアドレス_TextChanged(object sender, EventArgs e)
        {

            FunctionClass.LimitText(((TextBox)sender), 100);
            ChangedData(true);
        }

        private void 担当者名_Validated(object sender, EventArgs e)
        {
            UpdatedControl((Control)sender);
        }


        private void 担当者名_TextChanged(object sender, EventArgs e)
        {

            FunctionClass.LimitText(((TextBox)sender), 50);
            ChangedData(true);
        }



        private void 電話番号1_Validated(object sender, EventArgs e)
        {
            UpdatedControl((Control)sender);
        }


        private void 電話番号1_TextChanged(object sender, EventArgs e)
        {

            FunctionClass.LimitText(((TextBox)sender), 5);
            ChangedData(true);
        }


        private void 電話番号2_Validated(object sender, EventArgs e)
        {
            UpdatedControl((Control)sender);
        }


        private void 電話番号2_TextChanged(object sender, EventArgs e)
        {

            FunctionClass.LimitText(((TextBox)sender), 5);
            ChangedData(true);
        }



        private void 電話番号3_Validated(object sender, EventArgs e)
        {
            UpdatedControl((Control)sender);
        }


        private void 電話番号3_TextChanged(object sender, EventArgs e)
        {

            FunctionClass.LimitText(((TextBox)sender), 5);
            ChangedData(true);
        }

        private void FAX番号1_Validated(object sender, EventArgs e)
        {
            UpdatedControl((Control)sender);
        }


        private void FAX番号1_TextChanged(object sender, EventArgs e)
        {

            FunctionClass.LimitText(((TextBox)sender), 5);
            ChangedData(true);
        }


        private void FAX番号2_Validated(object sender, EventArgs e)
        {
            UpdatedControl((Control)sender);
        }


        private void FAX番号2_TextChanged(object sender, EventArgs e)
        {

            FunctionClass.LimitText(((TextBox)sender), 5);
            ChangedData(true);
        }



        private void FAX番号3_Validated(object sender, EventArgs e)
        {
            UpdatedControl((Control)sender);
        }


        private void FAX番号3_TextChanged(object sender, EventArgs e)
        {

            FunctionClass.LimitText(((TextBox)sender), 5);
            ChangedData(true);
        }

        private void 備考_TextChanged(object sender, EventArgs e)
        {

            FunctionClass.LimitText(((TextBox)sender), 2000);
            ChangedData(true);
        }


        private void 郵便番号_TextChanged(object sender, EventArgs e)
        {

            FunctionClass.LimitText(((TextBox)sender), 7);
            ChangedData(true);
        }

        private void メーカー名_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■全角30文字まで入力できます。";
        }

        private void メーカー名_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void メーカー名フリガナ_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■全角60文字まで入力できます。";
        }

        private void メーカー名フリガナ_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void メーカー省略名_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■メーカーの省略名を入力します。　■全角5文字まで入力できます。";
        }

        private void メーカー省略名_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 住所1_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■全角25文字まで入力できます。";
        }

        private void 住所1_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 住所2_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■全角25文字まで入力できます。";
        }

        private void 住所2_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 担当者名_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■全角25文字まで入力できます。";
        }

        private void 担当者名_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 担当者メールアドレス_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■半角100文字まで入力できます。";
        }

        private void 担当者メールアドレス_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void ウェブアドレス_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■半角100文字まで入力できます。";
        }

        private void ウェブアドレス_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 仕入先1_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■全角30文字まで入力できます。";
        }

        private void 仕入先1_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 仕入先2_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■全角30文字まで入力できます。";
        }

        private void 仕入先2_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 仕入先3_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■全角30文字まで入力できます。";
        }

        private void 仕入先3_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 備考_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■全角100文字まで入力できます。";
        }

        private void 備考_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }
    }


}
