using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using u_net.Public;


namespace u_net
{
    public partial class F_メーカー : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
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
            try
            {
                this.SuspendLayout();
                //DoWait("しばらくお待ちください...");

                //IntPtr hIcon = LoadIconFromPath(CurrentProject.Path + "\\card.ico");
                //SendMessage(this.Handle, WM_SETICON, new IntPtr(1), hIcon);

                //object varOpenArgs = this.OpenArgs;

                int intWindowHeight = this.Height;
                int intWindowWidth = this.Width;

                string code = null;

                if (string.IsNullOrEmpty(code))
                {
                    // 新規モードへ
                    //if (!GoNewMode())
                    //{
                    //    throw new Exception("初期化に失敗しました。");
                    //}
                }
                else
                {
                    // 修正モードへ
                    if (!GoModifyMode())
                    {
                        throw new Exception("初期化に失敗しました。");
                    }
                    //this.メーカーコード.Text = varOpenArgs.ToString();
                    // コードを設定したことでイベント発生
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
            }
        }


        private bool GoNewMode()
        {
            try
            {
                // 各コントロール値を初期化
                //SetControls(this, null);

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
                //SetControls(this, null);

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
            try
            {

                // 変更された場合
                if (IsChanged)
                {
                    var intRes = MessageBox.Show("変更内容を登録しますか？", BASE_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    switch (intRes)
                    {
                        case DialogResult.Yes:
                            if (SaveData(CurrentCode, CurrentRevision))
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

        private bool SaveData(string SaveCode, int SaveEdition = -1)
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
                    //objControl2.Text = LoginUserCode;
                    //objControl3.Text = LoginUserFullName;
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
                //objControl5.Text = LoginUserCode;
                //objControl6.Text = LoginUserFullName;

                // 登録処理
                if (RegTrans(SaveCode, SaveEdition))
                {
                    // 登録成功
                    return true;
                }
                else
                {
                    // 登録失敗
                    if (isNewData)
                    {
                        objControl1.Text = (string)varSaved1;
                        objControl2.Text = (string)varSaved2;
                        objControl3.Text = (string)varSaved3;
                        ActiveDate.Text = (string)varSaved7;
                    }

                    objControl4.Text = (string)varSaved4;
                    objControl5.Text = (string)varSaved5;
                    objControl6.Text = (string)varSaved6;
                }

                return false;
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_SaveData - " + ex.Message);
                return false;
            }
        }

        public bool RegTrans(string codeString, int editionNumber = -1)
        {
            try
            {
                bool success = false;
                string strKey = "";

                Connect();

                using (SqlTransaction trans = cn.BeginTransaction())
                {
                    try
                    {
                        // ヘッダ部の登録
                        if (SaveHeader(this, codeString, editionNumber))
                        {
                            trans.Commit(); // トランザクション完了
                            success = true;
                        }
                        else
                        {
                            trans.Rollback(); // 変更をキャンセル
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback(); // 変更をキャンセル
                        throw ex;
                    }
                }

                return success;
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close(); // データベース接続を閉じる
                }

                Debug.Print(this.Name + "_RegTrans - " + ex.Message);
                return false;
            }
        }

        public bool SaveHeader(Form formObject, string codeString, int editionNumber = -1)
        {
            try
            {
                bool success = false;

                Connect();

                using (SqlTransaction trans = cn.BeginTransaction())
                {
                    try
                    {
                        string strKey = (editionNumber == -1) ? "メーカーコード = @CodeString" : "メーカーコード = @CodeString AND Revision = @EditionNumber";
                        string strSQL = "SELECT * FROM Mメーカー WHERE " + strKey;

                        using (SqlCommand cmd = new SqlCommand(strSQL, cn, trans))
                        {
                            cmd.Parameters.AddWithValue("@CodeString", codeString);
                            if (editionNumber != -1)
                            {
                                cmd.Parameters.AddWithValue("@EditionNumber", editionNumber);
                            }

                            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            {
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);

                                if (dataTable.Rows.Count == 0)
                                {
                                    // 新しいレコードを追加
                                    DataRow newRow = dataTable.NewRow();
                                    FunctionClass.SetForm2Table(formObject, newRow, "", "");
                                    dataTable.Rows.Add(newRow);
                                }
                                else
                                {
                                    // 既存のレコードを更新
                                    FunctionClass.SetForm2Table(formObject, dataTable.Rows[0], "メーカーコード", "Revision");
                                }

                                // データベースに変更を保存
                                using (SqlCommandBuilder builder = new SqlCommandBuilder(adapter))
                                {
                                    adapter.Update(dataTable);
                                }
                            }
                        }

                        trans.Commit(); // トランザクション完了
                        success = true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback(); // 変更をキャンセル
                        throw ex;
                    }
                }

                return success;
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close(); // データベース接続を閉じる
                }

                Debug.Print(this.Name + "_SaveHeader - " + ex.Message);
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

                // 変更がある
                if (this.IsChanged)
                {
                    var intRes = MessageBox.Show("変更内容を登録しますか？", "新規コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    switch (intRes)
                    {
                        case DialogResult.Yes:
                            // 登録処理
                            if (!SaveData(this.CurrentCode, this.CurrentRevision))
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
                        // エラーチェック
                        if (IsErrorData("メーカーコード"))
                        {
                            return;
                        }

                        // 登録処理
                        if (!SaveData(this.CurrentCode, this.CurrentRevision))
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


        private bool IsErrorData(string ExFieldName1, string ExFieldName2 = "")
        {
            try
            {
                bool isErrorData = false;

                // ヘッダ部のチェック
                foreach (Control objControl in this.Controls)
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
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_IsErrorData - " + ex.Message);
                return true;
            }
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
                string strCode = CurrentCode; // 仮のCurrentCodeの取得
                int intEdition = CurrentRevision; // 仮のCurrentRevisionの取得
                string strMsg;

                this.DoubleBuffered = true;
                //this.Painting = false;

                DialogResult intRes;

                if (ActiveControl == コマンド削除)
                {
                    GetNextControl(コマンド削除, false).Focus();
                }

                if (intEdition == 1)
                {
                    strMsg = "メーカーコード　：　" + strCode + Environment.NewLine +
                             "このメーカーデータを削除/復元します。" + Environment.NewLine +
                             "削除/復元するには[はい]を選択してください。" + Environment.NewLine + Environment.NewLine +
                             "※削除後も参照することができます。";

                    intRes = MessageBox.Show(strMsg, "削除コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                }
                else
                {
                    strMsg = "このバージョンのU-netでは操作できません。" + Environment.NewLine +
                             "最新バージョンのU-netで操作してください";

                    intRes = MessageBox.Show(strMsg, "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 2版以上の場合の処理はコメントアウトしました
                    // strMsg の内容に合わせて適切な処理を実装してください
                }

                if (intRes == DialogResult.Cancel)
                {
                    goto Bye_コマンド削除_Click;
                }
                else
                {
                    // 認証処理や削除処理を実装する必要があります
                    // また、MessageBox などのメッセージボックスを適切に置き換えてください
                }
            }
            finally
            {
                //this.Painting = true;
            }

        Bye_コマンド削除_Click:
            Close();
        }

        private void コマンド仕入先_Click(object sender, EventArgs e)
        {
            // エラーハンドリングはC#では別の方法を使用するため、ここでは省略
            // On Error Resume Next の代替方法はC#にはありません

            if (ActiveControl == コマンド仕入先)
            {
                GetNextControl(コマンド仕入先, false).Focus();
            }

            // 仕入先フォームを開く
            //Form 仕入先Form = new 仕入先(); // 仕入先フォームの名前に合わせて変更してください
            //仕入先Form.Show();
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
                System.Diagnostics.Process.Start(mailtoLink);
            }
            catch (Exception ex)
            {
                MessageBox.Show("メールを起動できませんでした。\nエラー: " + ex.Message, "メールコマンド", MessageBoxButtons.OK);
            }
        }

        private void コマンド印刷_Click(object sender, EventArgs e)
        {

        }

        private void コマンド承認_Click(object sender, EventArgs e)
        {

        }

        private void コマンド確定_Click(object sender, EventArgs e)
        {

        }

        private void コマンド登録_Click(object sender, EventArgs e)
        {
            try
            {
                this.DoubleBuffered = true;

                //this.Painting = false;

                if (ActiveControl == コマンド登録)
                {
                    GetNextControl(コマンド登録, false).Focus();
                }

                // 登録時におけるエラーチェック
                if (IsErrorData("メーカーコード"))
                {
                    goto Bye_コマンド登録_Click;
                }

                //DoWait("登録しています...");

                if (SaveData(CurrentCode, CurrentRevision))
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
                }
                else
                {
                    MessageBox.Show("登録できませんでした。", "登録コマンド", MessageBoxButtons.OK);
                }
            }
            finally
            {
                Close();
                //this.Painting = true;
            }

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
                case Keys.F1:
                    if (コマンド新規.Enabled) コマンド新規_Click(sender, e);
                    break;
                case Keys.F2:
                    if (コマンド読込.Enabled) コマンド読込_Click(sender, e);
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
                    if (コマンドメール.Enabled) コマンドメール_Click(sender, e);
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







        //private void F_メーカー_KeyDown(object sender, KeyEventArgs e)
        //{
        //    switch (e.KeyCode)
        //    {
        //        case Keys.Space: //コンボボックスならドロップダウン
        //            {
        //                Control activeControl = this.ActiveControl;
        //                if (activeControl is System.Windows.Forms.ComboBox)
        //                {
        //                    System.Windows.Forms.ComboBox activeComboBox = (System.Windows.Forms.ComboBox)activeControl;
        //                    activeComboBox.DroppedDown = true;
        //                }
        //            }
        //            break;
        //        case Keys.F1:
        //            if (コマンド新規.Enabled)
        //            {
        //                コマンド新規.Focus();
        //                コマンド新規_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
        //            }
        //            break;
        //        case Keys.F2:
        //            if (コマンド読込.Enabled)
        //            {
        //                コマンド読込.Focus();
        //                コマンド修正_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
        //            }
        //            break;
        //        case Keys.F3:
        //            if (コマンド複写.Enabled)
        //            {
        //                コマンド複写_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
        //            }
        //            break;
        //        case Keys.F4:
        //            if (コマンド削除.Enabled)
        //            {
        //                コマンド削除_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
        //            }
        //            break;
        //        case Keys.F5:
        //            if (コマンド仕入先.Enabled)
        //            {
        //                コマンドシリーズ_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
        //            }
        //            break;
        //        case Keys.F9:
        //            if (コマンド承認.Enabled)
        //            {
        //                コマンド承認_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
        //            }
        //            break;
        //        case Keys.F10:
        //            if (コマンド確定.Enabled)
        //            {
        //                コマンド確定_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
        //            }
        //            break;
        //        case Keys.F11:
        //            if (コマンド登録.Enabled)
        //            {
        //                コマンド登録.Focus();
        //                コマンド登録_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
        //            }
        //            break;
        //        case Keys.F12:
        //            if (コマンド終了.Enabled)
        //            {
        //                コマンド終了_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
        //            }
        //            break;
        //    }
        //}


    }


}
