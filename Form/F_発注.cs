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
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Drawing.Imaging;
using System.Drawing.Printing;
using Microsoft.IdentityModel.Tokens;
using System.Data.Common;
using static u_net.CommonConstants;
using Microsoft.Identity.Client.NativeInterop;
using GrapeCity.Win.MultiRow;
using System.Threading.Channels;
using Pao.Reports;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Transactions;
using MultiRowDesigner;
using System.Runtime.InteropServices;
using System.Text;
using System.Diagnostics.Eventing.Reader;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace u_net
{
    public partial class F_発注 : Form
    {
        //使用するか
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        // WM_UNDOメッセージの定義
        private const int WM_UNDO = 0x0304;


        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "発注";
        private int selected_frame = 0;
        public bool blnNewParts = true;
        public int intWindowHeight = 0;
        public int intWindowWidth = 0;
        public int buttonCnt = 0; //何故か明細のボタンクリックで2回買掛区分編集フォームが開くため
        public F_発注()
        {
            this.Text = "発注";       // ウィンドウタイトルを設定
            this.MaximizeBox = false;  // 最大化ボタンを無効化
            this.MinimizeBox = false; //最小化ボタンを無効化
                                      //  args = string.IsNullOrEmpty(openargs) ? "" : openargs;

            InitializeComponent();

        }
        public string CurrentCode
        {
            get
            {
                return string.IsNullOrEmpty(発注コード.Text) ? "" : 発注コード.Text;
            }
        }
        public int CurrentEdition
        {
            get
            {
                int result;
                return int.TryParse(発注版数.Text, out result) ? result : 0;
            }
        }

        public bool InvManageOn
        {
            get
            {
                return 在庫管理.Checked;
            }
        }
        public bool IsApproved
        {
            get
            {
                return !string.IsNullOrEmpty(承認日時.Text) && !string.IsNullOrEmpty(承認者コード.Text) ? true :
                    false;

            }
        }
        public bool IsChanged
        {
            get
            {
                return コマンド登録.Enabled;
            }
        }

        public string IsCompleted
        {
            get
            {
                return string.IsNullOrEmpty(入庫状況.Text) ? "0" : 入庫状況.Text;
            }
        }

        public bool IsDecided
        {
            get
            {
                return !string.IsNullOrEmpty(確定日時.Text) ? true : false;
            }
        }

        public bool IsDeleted
        {
            get
            {
                return !string.IsNullOrEmpty(削除.Text) ? true : false;
            }
        }

        public bool IsLastEdition
        {
            get
            {
                int lastEdition = GetLastEdition(cn, CurrentCode);
                return CurrentEdition == lastEdition;
            }
        }
        public bool IsLotOrder
        {
            get
            {
                return !string.IsNullOrEmpty(購買コード.Text) ? true : false;
            }
        }

        public bool IsNewData
        {
            get
            {
                return !コマンド新規.Enabled;
            }
        }

        private bool IsReceived
        {
            get
            {
                return !string.IsNullOrEmpty(OriginalClass.Nz(入庫状況表示.Text, ""));
            }
        }

        private bool PurchasingUserLogin
        {
            //ログオンユーザーが購買担当かどうかを取得します
            get
            {
                return LoginUserCode == "080" || LoginUserCode == "006";
            }
        }

        private bool IsOrderByOn
        {
            get
            {
                return 発注明細1.sortFlg;
            }
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
            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(CommonConstants.LoginUserCode, this);

            //MyApi myapi = new MyApi();
            //int xSize, ySize, intpixel, twipperdot;

            //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
            //intpixel = myapi.GetLogPixel();
            //twipperdot = myapi.GetTwipPerDot(intpixel);

            OriginalClass ofn = new OriginalClass();
            ofn.SetComboBox(発注コード, "SELECT 発注コード as Display ,発注コード as Value FROM V発注_最新版 ORDER BY 発注コード DESC");

            string sql = "SELECT 氏名 as Display ,社員コード as Value FROM M社員 WHERE (退社 IS NULL) AND (削除日時 IS NULL) ORDER BY ふりがな";
            ofn.SetComboBox(発注者コード, sql);

            try
            {
                this.SuspendLayout();

                intWindowHeight = this.Height;
                intWindowWidth = this.Width;


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
                        //引数をカンマで分けてそれぞれの項目に設定
                        int indexOfComma = args.IndexOf(",");
                        string editionString = args.Substring(indexOfComma + 1).Trim();
                        int edition;
                        if (int.TryParse(editionString, out edition))
                        {
                            発注版数.Text = edition.ToString();
                        }

                        string codeString = args.Substring(0, indexOfComma).Trim();
                        発注コード.Text = codeString;

                        UpdatedControl("発注コード");
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
                this.Focus();
            }
        }

        private void SetEditions(string code)
        {
            OriginalClass ofn = new OriginalClass();
            ofn.SetComboBox(発注版数, "SELECT 発注版数 as Display,発注版数 as Value FROM T発注 where 発注コード='" + code + "' ORDER BY 発注版数 DESC");
        }

        private bool GoNewMode()
        {
            try
            {
                // 各コントロール値を初期化
                VariableSet.SetControls(this);

                Connect();

                発注コード.Text = FunctionClass.採番(cn, CH_ORDER).ToString();
                SetEditions(発注コード.Text);
                発注版数.Text = "1";
                発注日.Text = DateTime.Now.ToString("yyyy/MM/dd");
                発注者コード.Text = LoginUserCode;
                在庫管理.Checked = false;
                NoCredit.Checked = false;
                UpdatedControl("発注者コード");

                // 明細部の初期化
                string strSQL = "SELECT * FROM V発注明細 WHERE 発注コード='" + CurrentCode + "' ORDER BY 明細番号";
                if (!LoadDetails(strSQL, 発注明細1.Detail)) return false;

                // ヘッダ部動作制御
                FunctionClass.LockData(this, false);
                発注日.Focus();
                発注コード.Enabled = false;
                発注版数.Enabled = false;
                改版ボタン.Enabled = false;
                コマンド新規.Enabled = false;
                コマンド読込.Enabled = true;
                コマンド複写.Enabled = false;
                コマンド削除.Enabled = false;
                コマンド発注書.Enabled = false;
                コマンド送信.Enabled = false;
                コマンド購買.Enabled = false;
                コマンド承認.Enabled = false;
                コマンド確定.Enabled = false;
                コマンド登録.Enabled = false;

                // 明細部動作制御
                発注明細1.Detail.AllowUserToDeleteRows = true;
                発注明細1.Detail.ReadOnly = false;
                発注明細1.Detail.AllowUserToAddRows = true;


                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{this.Name}_GoNewMode - {ex.GetType().Name} : {ex.Message}");
                return false;
            }
        }

        private bool GoModifyMode()
        {
            try
            {
                bool result = false;

                // 各コントロールの値をクリア
                VariableSet.SetControls(this);

                // 明細部の初期化
                string strSQL = "SELECT * FROM V発注明細 WHERE 発注コード='" + CurrentCode +
                                         "' AND 発注版数=" + CurrentEdition + " ORDER BY 明細番号";
                VariableSet.SetTable2Details(発注明細1.Detail, strSQL, cn);

                this.発注コード.Enabled = true;
                this.発注コード.Focus();

                FunctionClass.LockData(this, true, "発注コード", "発注版数");
                this.コマンド新規.Enabled = true;
                this.コマンド読込.Enabled = false;
                this.コマンド複写.Enabled = false;
                this.コマンド確定.Enabled = false;
                this.コマンド登録.Enabled = false;

                // 編集による変更がない状態へ遷移
                ChangedData(false);

                result = true;
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.Name + "_GoModifyMode - " + ex.Message);
                return false;
            }
        }



        private void コマンド登録_Click(object sender, EventArgs e)
        {

            if (this.ActiveControl == this.コマンド登録)
            {
                GetNextControl(コマンド登録, false).Focus();
            }

            //明細部のエラーチェック
            if (IsErrorDetails()) return;

            // 並び替えチェック

            if (IsOrderByOn)
            {
                if (MessageBox.Show("明細行が並べ替えられています。\n並べ替えを解除して登録しますか？", "登録コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    発注明細1.CancelOrderBy();
                }
                else
                {
                    goto Bye_コマンド登録_Click;
                }
            }

            FunctionClass fn = new FunctionClass();
            fn.DoWait("登録しています...");

            // 登録処理
            if (SaveData(CurrentCode, CurrentEdition))
            {
                // 登録に成功した
                ChangedData(false); // データ変更取り消し

                if (this.IsNewData)
                {
                    // 新規モードのとき
                    this.コマンド新規.Enabled = true;
                    this.コマンド読込.Enabled = false;
                    //発注版数に追加したレコードを入れ直す
                    OriginalClass ofn = new OriginalClass();
                    ofn.SetComboBox(発注版数, "SELECT 発注コード as Display ,発注コード as Value FROM V発注_最新版 ORDER BY 発注コード DESC");
                }
                this.コマンド承認.Enabled = this.IsDecided;
                this.コマンド確定.Enabled = true;

                fn.WaitForm.Close();
                MessageBox.Show("登録を完了しました", "登録コマンド", MessageBoxButtons.OK);
            }
            else
            {   // 登録に失敗したとき
                fn.WaitForm.Close();
                this.コマンド登録.Enabled = true;
                MessageBox.Show("登録できませんでした。", "登録コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        Bye_コマンド登録_Click:
            return;

        }

        private bool SaveData(string SaveCode, int SaveEdition = 0)
        {
            object varSaved1 = null;
            object varSaved2 = null;
            object varSaved3 = null;

            Control objControl1 = null;
            Control objControl2 = null;
            Control objControl3 = null;
            bool headerErr = false;

            Connect();
            DateTime dtmNow = FunctionClass.GetServerDate(cn);

            objControl1 = 登録日時;
            objControl2 = 登録者コード;
            objControl3 = 登録者名;
            varSaved1 = objControl1.Text;
            varSaved2 = objControl2.Text;
            varSaved3 = objControl3.Text;
            objControl1.Text = dtmNow.ToString();
            objControl2.Text = CommonConstants.LoginUserCode;
            objControl3.Text = CommonConstants.LoginUserFullName;

            if (RegTrans(SaveCode, SaveEdition, cn))
            {
                return true;
            }
            else
            {
                objControl1.Text = varSaved1.ToString();
                objControl2.Text = varSaved2.ToString();
                objControl3.Text = varSaved3.ToString();
                return false;
            }
        }

        public bool RegTrans(string codeString, int editionNumber, SqlConnection cn)
        {
            bool success = false;

            try
            {
                // トランザクション開始
                SqlTransaction transaction = cn.BeginTransaction();

                try
                {
                    // ヘッダ部の登録
                    if (!SaveHeader(codeString, editionNumber, cn, transaction))
                    {
                        transaction.Rollback();  // 変更をキャンセル
                        return false;
                    }

                    // 明細部の登録

                    if (!SaveDetails(codeString, editionNumber, cn, transaction))
                    {
                        transaction.Rollback();  // 変更をキャンセル
                        return false;
                    }

                    // トランザクション完了
                    transaction.Commit();
                    success = true;
                }
                catch (Exception ex)
                {
                    // トランザクション中にエラーが発生した場合はロールバック
                    transaction.Rollback();
                    Console.WriteLine($"RegTrans-: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"RegTrans-: {ex.Message}");
            }
            finally
            {
                if (cn.State == System.Data.ConnectionState.Open)
                {
                    cn.Close();  // 接続を閉じる
                }
            }

            return success;
        }

        private bool SaveHeader(string codeString, int editionNumber, SqlConnection cn, SqlTransaction transaction)
        {

            try
            {
                string strwhere = " 発注コード='" + codeString + "' and 発注版数=" + editionNumber;

                if (editionNumber > 1)
                {
                    //改版登録のときは旧版を無効にする
                    string strSQL = $"UPDATE T発注 SET 無効日時=getdate(), 無効者コード='{LoginUserCode}' " +
                        $"WHERE 発注コード='{codeString}' AND 発注版数={editionNumber - 1}";

                    SqlCommand command = new SqlCommand(strSQL, cn, transaction);
                    command.ExecuteNonQuery();

                }

                if (!DataUpdater.UpdateOrInsertDataFrom(this, cn, "T発注", strwhere, "発注コード", transaction, "発注版数"))
                {
                    //保存できなかった時の処理 catchで対応する
                    throw new Exception();
                }

                return true;

            }
            catch (Exception ex)
            {
                コマンド登録.Enabled = true;
                MessageBox.Show("データの保存中にエラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        public bool SaveDetails(string codeString, int editionNumber, SqlConnection cn, SqlTransaction transaction)
        {
            try
            {
                string strwhere = $"発注コード= '{codeString}' AND 発注版数= {editionNumber}";
                //明細部の登録
                if (!DataUpdater.UpdateOrInsertDetails(this.発注明細1.Detail, cn, "T発注明細", strwhere, "発注コード", transaction))
                {
                    //保存できなかった時の処理                    
                    throw new Exception();
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("データの保存中にエラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }

        public int GetLastEdition(SqlConnection connectionObject, string code)
        {
            //最終版数を返す

            int lastEdition = 0;

            try
            {
                using (SqlCommand command = new SqlCommand("SELECT 最新版数 FROM V発注_最新版 WHERE 発注コード=@Code", connectionObject))
                {
                    command.Parameters.AddWithValue("@Code", code);

                    connectionObject.Open();
                    object result = command.ExecuteScalar();

                    if (result != null && result != DBNull.Value)
                    {
                        lastEdition = Convert.ToInt32(result);
                    }
                    else
                    {
                        lastEdition = -1;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in GetLastEdition: " + ex.Message);
                // Handle the exception as needed.
            }
            finally
            {
                connectionObject.Close();
            }

            return lastEdition;
        }

        private bool ErrCheck()
        {
            //入力確認    
            //if (!FunctionClass.IsError(this.発注コード)) return false;
            //if (!FunctionClass.IsError(this.版数)) return false;
            return true;
        }

        private bool IsError(Control controlObject, bool Cancel)
        {
            try
            {
                bool isError = false;

                object varValue = controlObject.Text;

                switch (controlObject.Name)
                {
                    case "発注コード":
                    case "発注版数":
                        if (varValue == null || varValue == DBNull.Value)
                        {
                            MessageBox.Show("コード情報がありません。" + Environment.NewLine + "システムエラーです。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        break;

                    case "発注日":
                        if (varValue == null || varValue == DBNull.Value)
                        {
                            MessageBox.Show(controlObject.Name + " を入力してください。", "入力", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            goto Exit_IsError;
                        }

                        if (!DateTime.TryParse(varValue.ToString(), out _))
                        {
                            MessageBox.Show("日付を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }

                        if (DateTime.Now < Convert.ToDateTime(varValue))
                        {
                            MessageBox.Show("未来の日付は入力できません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        break;

                    case "発注者コード":
                        if (varValue == null || varValue == DBNull.Value)
                        {
                            MessageBox.Show("発注者名を選択してください。", "発注者名", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            goto Exit_IsError;
                        }
                        break;

                    case "仕入先コード":
                        Connect();
                        string str1 = FunctionClass.GetSupplierName(cn, (varValue == null || varValue == DBNull.Value) ? null : varValue.ToString());

                        if (string.IsNullOrEmpty(str1))
                        {
                            MessageBox.Show("指定された仕入先データはありません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        else
                        {
                            this.仕入先名.Text = str1;
                        }
                        break;

                    case "仕入先担当者名":
                        if (varValue == null || varValue == DBNull.Value)
                        {
                            MessageBox.Show(controlObject.Name + " を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        break;

                    case "在庫管理":
                        //アクセスでは　Me.SubForm.Form.RecordCount　となっており、オブジェクトエラーになっている！RecordCountは使えない
                        if (Cancel == false && this.発注明細1.Detail.RowCount > 0)
                        {
                            MessageBox.Show("入力済みの発注部品に対して在庫管理の有無を変更することはできません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        break;
                }

                return false;

            Exit_IsError:
                // エラー発生後処理
                isError = true;
                if (Cancel) return true; // Cancel が true の場合は処理しない

                Cancel = true;

                //me.RecordSourceがどこのレコードソースか不明のため

                //if (string.IsNullOrEmpty(this.RecordSource))
                //{
                //    // RecordSourceが空の場合はWM_UNDOメッセージを送信
                //    SendMessage(this.Handle, WM_UNDO, IntPtr.Zero, IntPtr.Zero);
                //}
                //else
                //{
                // RecordSourceが空でない場合はActiveControlのUndoメソッドを呼び出す
                if (controlObject is TextBox textBox)
                {
                    textBox.Undo();
                }

                //}

                return isError;
            }
            catch (Exception ex)
            {
                // 例外処理
                Console.WriteLine(ex.Message);
                return true;
            }
        }


        private bool IsErrorData(string exFieldName1, string exFieldName2 = null)
        {
            try
            {
                foreach (Control control in Controls)
                {
                    if ((control is TextBox || control is ComboBox) && control.Visible)
                    {
                        if (control.Name != exFieldName1 && control.Name != exFieldName2)
                        {
                            if (IsError(control, true))
                            {
                                control.Focus();
                                return true;
                            }
                        }
                    }
                }
                // 明細行数のチェック
                if (IsErrorDetails())
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{GetType().Name}_IsErrorData - {ex.GetType().Name} : {ex.Message}");
                return true;
            }
        }

        private bool IsErrorDetails()
        {
            try
            {
                int count = 発注明細1.Detail.RowCount;

                if (count == 0)
                {
                    MessageBox.Show("明細行を1行以上入力してください。", "発注明細", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    発注明細1.Detail.Focus();
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{GetType().Name}_IsErrorDetails - {ex.GetType().Name} : {ex.Message}");
                return true;
            }
        }

        public void ChangedData(bool isChanged)
        {
            if (ActiveControl == null) return;

            if (isChanged)
            {
                this.Text = BASE_CAPTION + "*";
            }
            else
            {
                this.Text = BASE_CAPTION;
            }

            // キー情報を表示するコントロールを制御する
            // コードにフォーカスがある状態でサブフォームから呼び出されたときの対処

            if (ActiveControl == 発注コード) 発注日.Focus();
            発注コード.Enabled = !isChanged;
            if (ActiveControl == 発注版数) 発注日.Focus();
            発注版数.Enabled = !isChanged;

            コマンド複写.Enabled = !isChanged;
            コマンド削除.Enabled = !isChanged;
            コマンド発注書.Enabled = !isChanged;
            if (isChanged) コマンド送信.Enabled = false;
            if (isChanged) コマンド確定.Enabled = true;
            コマンド登録.Enabled = isChanged;
        }

        private void コマンド新規_Click(object sender, EventArgs e)
        {
            try
            {
                //Connect();
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
                            if (!SaveData(CurrentCode, CurrentEdition))
                            {
                                MessageBox.Show("エラーのため登録できません。", "新規コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            break;
                        case DialogResult.Cancel:
                            return;
                    }
                }

                if (!GoNewMode())
                {
                    MessageBox.Show("エラーのため新規モードへ移行できません。", "新規コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }


            }
            finally
            {
                this.DoubleBuffered = false;
                Cursor.Current = Cursors.Default;
            }
        }

        private void コマンド読込_Click(object sender, EventArgs e)
        {
            try
            {
                if (!this.IsChanged)
                {
                    // 新規モードで且つコードが取得済みのときはコードを戻す
                    if (this.IsNewData && !string.IsNullOrEmpty(this.CurrentCode))
                    {
                        // 採番された番号を戻す
                        if (!FunctionClass.ReturnCode(cn, this.CurrentCode))
                        {
                            MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine +
                                            "発注コード　：　" + this.CurrentCode, "修正コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    // 修正モードへ移行する
                    if (!GoModifyMode()) goto Err_コマンド読込_Click;
                    goto Bye_コマンド読込_Click;
                }

                // 修正されているときは登録確認を行う
                DialogResult result = MessageBox.Show("変更内容を登録しますか？", "修正コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (result)
                {
                    case DialogResult.Yes:
                        // エラーチェック
                        //bool Cancel = true;
                        if (IsError(this.発注コード, true)) return;

                        // 登録処理   accessのソースでは何故かこのような条件式になっていた　引数がおかしいので、必ずエラーが起こる仕様　という事でエラーメッセージを返す
                        //if (!SaveData(IsNewData, CurrentCode))

                        {
                            MessageBox.Show("エラーのため登録できません。", "修正コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        break;
                    case DialogResult.No:
                        // 新規モードで且つコードが取得済みのときはコードを戻す
                        if (this.IsNewData && this.CurrentCode != "")
                        {
                            // 採番された番号を戻す
                            if (!FunctionClass.ReturnCode(cn, this.CurrentCode))
                            {
                                MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine +
                                                "発注コード　：　" + this.CurrentCode, "修正コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                        break;
                    case DialogResult.Cancel:
                        return;
                }

                // 修正モードへ移行する
                if (!GoModifyMode()) goto Err_コマンド読込_Click;

                Bye_コマンド読込_Click:

                return;

            Err_コマンド読込_Click:
                // エラー処理                
                MessageBox.Show("エラーが発生しました。" + Environment.NewLine +
                                "[" + this.Name + "]を終了します。", "修正コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);

                this.Close();
            }
            catch (Exception ex)
            {
                // 例外処理
                MessageBox.Show(ex.Message);
            }
        }


        private void コマンド承認_Click(object sender, EventArgs e)
        {
            try
            {
                string strHeadCode;
                object varSaved1;
                object varSaved2;
                object varSaved3;

                if (this.ActiveControl == this.コマンド承認)
                    コマンド承認.Focus();

                Connect();
                strHeadCode = FunctionClass.GetHeadCode(cn, this.発注者コード.Text);

                //■発注者の長が見つからないときは、技術部員の発注と捉える
                if (strHeadCode == "000")
                {
                    // ログオンユーザーが承認可能ユーザーなら認証済みユーザーとする
                    strHeadCode = USER_CODE_TECH;
                }
                else
                {
                    //ログオンユーザーが承認可能ユーザーでないなら、営業部承認者で認証を要求する

                    // 認証フォームを開く
                    // 認証が成功すると strCertificateCode が設定される
                    F_認証 fm = new F_認証();
                    fm.args = strHeadCode;
                    fm.ShowDialog();
                    if (string.IsNullOrEmpty(strCertificateCode))
                    {
                        MessageBox.Show("承認に失敗しました。" + Environment.NewLine + "承認できません。", "承認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        goto Bye_コマンド承認_Click;
                    }

                    Application.DoEvents();
                }

                // 承認処理
                varSaved1 = this.承認日時.Text;
                varSaved2 = this.承認者コード.Text;
                varSaved3 = this.承認者名.Text;

                if (this.IsApproved)
                {
                    this.承認日時.Text = null;
                    this.承認者コード.Text = null;
                    this.承認者名.Text = null;
                }
                else
                {
                    DateTime dtmNow = FunctionClass.GetServerDate(cn);
                    this.承認日時.Text = dtmNow.ToString();
                    this.承認者コード.Text = strCertificateCode;
                    this.承認者名.Text = FunctionClass.GetUserFullName(cn, strCertificateCode);
                }

                if (RegTrans(this.CurrentCode, this.CurrentEdition, cn))
                {
                    blnNewParts = false;
                }
                else
                {

                    this.承認日時.Text = varSaved1.ToString();
                    this.承認者コード.Text = varSaved2.ToString();
                    this.承認者名.Text = varSaved3.ToString();
                    MessageBox.Show("承認できませんでした。", "承認コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                this.改版ボタン.Enabled = this.IsApproved && ((!this.IsReceived) || PurchasingUserLogin);
                this.コマンド送信.Enabled = this.IsApproved;
                this.コマンド確定.Enabled = !this.IsApproved;

            Bye_コマンド承認_Click:

                return;
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_コマンド承認_Click - " + ex.HResult + " : " + ex.Message);
                MessageBox.Show("エラーが発生しました。" + Environment.NewLine + "操作は取り消されました。", "承認コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }


        private void コマンド確定_Click(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();
            try
            {
                object varSaved = null; // 確定日保存用（エラー発生時の対策）

                if (this.NoCredit.Checked == true && !this.IsDecided)
                {
                    DialogResult result = MessageBox.Show("振込処理は行われません。 本当によろしいですか？",
                        "確定コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        // Yesの場合の処理
                        //access に何も記載がなかった
                    }
                    else
                    {
                        goto Bye_コマンド確定_Click;
                    }
                }

                // 登録時におけるエラーチェック

                if (!this.IsDecided && this.IsErrorData("発注コード", "発注版数"))
                {
                    goto Bye_コマンド確定_Click;
                }

                // 明細行が並べ替えられているときはその旨を知らせる
                if (IsOrderByOn)
                {
                    if (MessageBox.Show("明細行が並べ替えられています。 並べ替えを解除して登録しますか？",
                        "確定コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        発注明細1.CancelOrderBy();
                    }
                    else
                    {
                        goto Bye_コマンド確定_Click;
                    }
                }

                // 登録前の確定日を保存しておく
                varSaved = this.確定日時.Text;
                Connect();
                // 確定日を設定する
                if (this.IsDecided)
                {
                    this.確定日時.Text = null;
                }
                else
                {
                    this.確定日時.Text = FunctionClass.GetServerDate(cn).ToString();
                }

                fn.DoWait("登録しています...");

                // 表示データを登録する
                if (SaveData(this.CurrentCode, this.CurrentEdition))
                {

                    blnNewParts = false;

                    // 新規モードのときは修正モードへ移行する
                    if (this.IsNewData)
                    {
                        this.コマンド新規.Enabled = true;
                        this.コマンド読込.Enabled = false;
                    }

                    this.コマンド承認.Enabled = this.IsDecided;
                }
                else
                {
                    this.確定日時.Text = varSaved?.ToString();
                    MessageBox.Show("確定できませんでした。", "確定コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                // 確定状態によって動作を制御する

                FunctionClass.LockData(this, this.IsDecided || this.IsDeleted, "発注コード", "発注版数");
                発注明細1.Detail.AllowUserToDeleteRows = !this.IsDecided;
                発注明細1.Detail.ReadOnly = this.IsDecided;
                発注明細1.Detail.AllowUserToAddRows = !this.IsDecided;
                ChangedData(false);
                チェック();

            Bye_コマンド確定_Click:
                fn.WaitForm.Close();
                return;
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_コマンド確定_Click - " + ex.Message);
                MessageBox.Show("エラーが発生しました。確定コマンドは取り消されました。", "確定コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
                fn.WaitForm.Close();
                return;
            }
        }


        private void 改版ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ActiveControl == this.改版ボタン)
                {
                    GetNextControl(改版ボタン, false).Focus();
                }


                if (IsLotOrder)
                {
                    DialogResult result = MessageBox.Show("この発注データは購買処理により作成されています。\n"
                        + "本バージョンには改版後の在庫計算に問題があります。\n"
                        + "在庫に関係しない箇所の変更であれば問題ありません。\n\n"
                        + "改版しますか？", "改版", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.No)
                    {
                        return;
                    }
                }

                if (CopyData(CurrentCode, CurrentEdition + 1))
                {
                    ChangedData(true);

                    // ヘッダ部制御
                    FunctionClass.LockData(this, false);
                    発注日.Focus();
                    改版ボタン.Enabled = false;
                    コマンド新規.Enabled = false;
                    コマンド読込.Enabled = true;
                    コマンド承認.Enabled = false;

                    // 明細部制御
                    発注明細1.Detail.AllowUserToAddRows = true;
                    発注明細1.Detail.AllowUserToDeleteRows = true;
                    発注明細1.Detail.ReadOnly = false; //readonlyなのでaccessと真偽が逆になる   
                }
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_ChangeVersionButton_Click - " + ex.Message);
                MessageBox.Show("エラーが発生しました。", "改版", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private bool CopyData(string codeString, int editionNumber)
        {
            try
            {
                // 明細部の初期設定
                DataTable dataTable = (DataTable)発注明細1.Detail.DataSource;
                if (dataTable != null)
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        // 行の状態が Deleted の時は次の行へ
                        if (dataTable.Rows[i].RowState == DataRowState.Deleted)
                        {
                            continue;
                        }
                        dataTable.Rows[i]["発注コード"] = codeString;
                        dataTable.Rows[i]["発注版数"] = editionNumber;
                        dataTable.Rows[i]["回答納期"] = DBNull.Value;
                    }
                    発注明細1.Detail.DataSource = dataTable; // 更新した DataTable を再セット
                }



                発注コード.Text = codeString;
                SetEditions(codeString);
                発注版数.Text = editionNumber.ToString();
                発注日.Text = DateTime.Now.ToString();

                //'複写のときのみ購買情報を削除する
                //→改版時は購買情報を残すことにする

                if (editionNumber == 1)
                {
                    在庫管理.Checked = false;
                    NoCredit.Checked = false;
                    購買コード.Text = null;
                    シリーズ名.Text = null;
                    ロット番号.Text = null;
                }

                登録日時.Text = null;
                登録者コード.Text = null;
                登録者名.Text = null;
                確定日時.Text = null;
                送信.Text = string.Empty;
                入庫状況.Text = string.Empty;
                承認日時.Text = null;
                承認者コード.Text = null;
                承認者名.Text = null;
                無効日時.Text = null;
                入庫状況.Text = null;

                チェック();


                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーが発生しました。", "改版", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void コマンド送信_Click(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();
            try
            {
                if (ActiveControl == コマンド送信)
                    コマンド送信.Focus();

                string strFaxNumber = 仕入先ファックス番号.Text;

                DialogResult intResult = MessageBox.Show($"ファックスを送信します。{Environment.NewLine}{Environment.NewLine}" +
                    $"送信先FAX番号：　{strFaxNumber}{Environment.NewLine}{Environment.NewLine}" +
                    $"発注単価を印字しますか？{Environment.NewLine}{Environment.NewLine}" +
                    $"[はい] - 印字された発注書を送信します{Environment.NewLine}" +
                    $"[いいえ] - 印字されない発注書を送信します{Environment.NewLine}" +
                    $"[キャンセル] - 操作を取り消します",
                    "送信コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                switch (intResult)
                {
                    case DialogResult.Yes:
                        bleShowPrice = true;
                        break;
                    case DialogResult.No:
                        bleShowPrice = false;
                        break;
                    case DialogResult.Cancel:
                        return;
                }

                fn.DoWait("発注書を送信しています...");
                Application.DoEvents();
                Connect();

                string server = cn.ConnectionString.Contains(",1436") || cn.ConnectionString.Contains("\\unet_secondary")
                    ? "secondary"
                    : "primary";

                string param = $" -sv:{server.Replace(" ", "_")} -sf:porder,{strFaxNumber.TrimEnd().Replace(" ", "_")}," +
                    $"{CurrentCode.TrimEnd().Replace(" ", "_")}," +
                    $"{CurrentEdition.ToString().TrimEnd().Replace(" ", "_")}," +
                    $"{仕入先名.Text.TrimEnd().Replace(" ", "_")}," +
                    $"{仕入先電話番号.Text.TrimEnd().Replace(" ", "_")}";
                param = $" -user:{LoginUserName}{param}";

                FunctionClass.GetShell(param);

                MessageBox.Show("発注書を送信しました。{Environment.NewLine}[ファックス管理] で送信状況を確認できます。",
                    "送信コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Debug.Print($"{Name}_コマンド送信_Click - {ex.GetType().Name}: {ex.Message}");
                MessageBox.Show("発注書の送信中にエラーが発生しました。", "送信コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                fn.WaitForm.Close();
                Application.DoEvents();
            }
        }

        private void コマンド購買_Click(object sender, EventArgs e)
        {
            try
            {
                //F_購買 targetform = new F_購買();

                //targetform.args = CurrentCode;
                //targetform.args2 = CurrentEdition;
                //targetform.ShowDialog();

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in コマンド履歴_Click: " + ex.Message);
            }
        }


        private F_検索 SearchForm;

        private void 発注日選択ボタン_Click(object sender, EventArgs e)
        {
            F_カレンダー dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(発注日.Text))
            {
                dateSelectionForm.args = 発注日.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                発注日.Text = selectedDate;
            }
        }

        private void コマンド削除_Click(object sender, EventArgs e)
        {
            try
            {
                int intRes; // 削除処理の切り分けに使用（全版OR旧版のみ）
                string str1; // 認証ユーザーコード
                string strHeadCode;
                string strMsg;
                string strMsgPlus;
                FunctionClass fn = new FunctionClass();

                if (this.ActiveControl == this.コマンド削除)
                {
                    this.コマンド削除.Focus();
                }

                // 購買により生成された発注データは削除できない
                if (this.IsLotOrder)
                {
                    if (MessageBox.Show("この発注データは購買処理によって作成されたものです。" + Environment.NewLine +
                                        "この発注データを削除しても購買データ内の発注データは削除されません。" + Environment.NewLine +
                                        "作成元の購買データを削除すると、対象となる発注データは全て削除されます。" + Environment.NewLine + Environment.NewLine +
                                        "続行しますか？", "削除コマンド", MessageBoxButtons.YesNo) == DialogResult.No)
                    {
                        return;
                    }
                }

                // 確認メッセージを表示し、削除処理（全版OR旧版のみ）を決定する
                strMsg = "発注コード　：　" + this.CurrentCode + Environment.NewLine + Environment.NewLine +
                         "旧版の発注データも削除しますか？" + Environment.NewLine + Environment.NewLine +
                         "[はい] - 全版数の発注データを削除します" + Environment.NewLine +
                         "[いいえ] - （第 " + this.CurrentEdition + " 版）のみを削除します" + Environment.NewLine +
                         "[キャンセル] - 操作を取り消します" + Environment.NewLine;

                if (this.IsCompleted == "■")
                {
                    strMsgPlus = "（注意）" + Environment.NewLine + "この発注に対する入庫データは破棄されます。" + Environment.NewLine + Environment.NewLine;
                }
                else if (this.IsCompleted == "□")
                {
                    strMsgPlus = "（注意）" + Environment.NewLine + "この発注に対する入庫データは破棄されます。" + Environment.NewLine + Environment.NewLine;
                }
                else
                {
                    strMsgPlus = "";
                }

                intRes = (int)MessageBox.Show(strMsgPlus + strMsg, "削除コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (intRes == (int)DialogResult.Cancel)
                {
                    return;
                }

                // 認証ユーザーを設定する
                if (this.IsApproved)
                {
                    str1 = this.承認者コード.Text;
                }
                else
                {
                    str1 = this.発注者コード.Text;
                }

                // ログインユーザーが認証ユーザーでなければ認証を要求する
                if (LoginUserCode != str1)
                {
                    // 認証フォームを開く
                    // 認証が成功すると strCertificateCode が設定される
                    F_認証 fm = new F_認証();
                    fm.args = str1;
                    fm.ShowDialog();


                    if (string.IsNullOrEmpty(strCertificateCode))
                    {
                        MessageBox.Show("認証できません。" + Environment.NewLine + "操作は取り消されました。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        goto Bye_コマンド削除_Click;
                    }
                }

                // 待機メッセージを表示

                fn.DoWait("登録しています...");

                Connect();
                // 削除に成功すれば新規モードへ移行する
                if (DeleteData(cn, this.CurrentCode, this.CurrentEdition, FunctionClass.GetServerDate(cn), LoginUserCode, intRes == (int)DialogResult.No))
                {
                    コマンド新規_Click(sender, e);
                }
                else
                {
                    MessageBox.Show("削除できませんでした。" + Environment.NewLine + Environment.NewLine +
                                    "発注コード　：　" + this.CurrentCode, "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            Bye_コマンド削除_Click:
                fn.WaitForm.Close();

                return;

            }
            catch (Exception ex)
            {
                Console.WriteLine("_コマンド削除_Click -" + ex.Message);
            }
        }

        public bool DeleteData(SqlConnection connectionObject, string codeString, int editionNumber, DateTime deleteTime, string deleteUser, bool undoEdition)
        {
            SqlTransaction transaction = connectionObject.BeginTransaction();  // トランザクション開始
            try
            {
                string strKey;
                string strSQL1;
                string strSQL2;


                if (undoEdition)
                {
                    // 指定版を完全に削除する
                    strKey = "発注コード='" + codeString + "' AND 発注版数=" + editionNumber;

                    using (SqlCommand command = new SqlCommand("DELETE FROM T発注 WHERE " + strKey, connectionObject, transaction))
                    {
                        command.ExecuteNonQuery();
                    }

                    using (SqlCommand command = new SqlCommand("DELETE FROM T発注明細 WHERE " + strKey, connectionObject, transaction))
                    {
                        command.ExecuteNonQuery();
                    }

                    // 旧版を有効化する
                    strKey = "発注コード='" + codeString + "' AND 発注版数=" + (editionNumber - 1);
                    using (SqlCommand command = new SqlCommand("UPDATE T発注 SET 無効日時=NULL,無効者コード=NULL WHERE " + strKey, connectionObject, transaction))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                else
                {
                    strKey = "発注コード='" + codeString + "' AND 発注版数=" + editionNumber;
                    using (SqlCommand command = new SqlCommand("UPDATE T発注 SET 無効日時=@DeleteTime,無効者コード=@DeleteUser WHERE " + strKey, connectionObject, transaction))
                    {
                        command.Parameters.AddWithValue("@DeleteTime", deleteTime);
                        command.Parameters.AddWithValue("@DeleteUser", deleteUser);
                        command.ExecuteNonQuery();
                    }
                }

                // トランザクション完了
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine("_DeleteData - " + ex.Message);
                MessageBox.Show("エラーが発生しました。" + Environment.NewLine + Environment.NewLine +
                                ex.Message, "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void コマンド複写_Click(object sender, EventArgs e)
        {
            try
            {
                Connect();

                string code = FunctionClass.採番(cn, CH_ORDER);

                if (CopyData(code, 1))
                {
                    ChangedData(true);
                    FunctionClass.LockData(this, false);
                    this.発注日.Focus();

                    this.改版ボタン.Enabled = false;
                    this.コマンド新規.Enabled = false;
                    this.コマンド読込.Enabled = true;
                    this.コマンド承認.Enabled = false;

                    // 明細部制御
                    発注明細1.Detail.AllowUserToAddRows = true;
                    発注明細1.Detail.AllowUserToDeleteRows = true;
                    発注明細1.Detail.ReadOnly = false; //readonlyなのでaccessと真偽が逆になる   
                }
                else
                {
                    throw new InvalidOperationException("CopyData時:エラー"); ;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "複写コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }

        private void コマンド部品_Click(object sender, EventArgs e)
        {
            F_部品 fm = new F_部品();
            fm.args = 発注明細1.CurrentPartsCode;          //発注明細のカレントレコードを渡す
            if (fm.args == "")
            {
                MessageBox.Show("明細を選択してください。", "コマンド部品", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            fm.ShowDialog();
        }

        private void コマンド発注書_Click(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();
            try
            {
                DialogResult intRes;

                if (ActiveControl == コマンド発注書)
                {
                    コマンド発注書.Focus();
                }

                // 発注単価を出力するかどうか要求する
                intRes = MessageBox.Show("発注単価を表示しますか？", "発注書コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (intRes)
                {
                    case DialogResult.Yes:
                        bleShowPrice = true;
                        break;
                    case DialogResult.No:
                        bleShowPrice = false;
                        break;
                    default:
                        return;
                }

                fn.DoWait("発注書を作成しています...");

            //注意　レポート出力テストのため外部システムの実行をコメント

            //string param = $" -sv:{ServerInstanceName.Replace(" ", "_")} -pv:porder,{CurrentCode.TrimEnd().Replace(" ", "_")}," +
            //    $"{CurrentEdition.ToString().Replace(" ", "_")}";
            //param = $" -user:{LoginUserName}{param}";
            //FunctionClass.GetShell(param);

            Bye_コマンド発注書_Click:

                fn.WaitForm.Close();

                発注書印刷();

                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーが発生しました。", "発注書コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void 発注書印刷()
        {

            IReport paoRep = ReportCreator.GetPreview();

            paoRep.LoadDefFile("../../../Reports/発注書.prepd");

            Connect();

            DataRowCollection V発注書;

            string sqlQuery = "SELECT * FROM V発注書   where 発注コード='" + CurrentCode +
                "' and 発注版数=" + CurrentEdition + " ORDER BY 発注コード, 明細番号";

            using (SqlCommand command = new SqlCommand(sqlQuery, cn))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataSet dataSet = new DataSet();

                    adapter.Fill(dataSet);

                    V発注書 = dataSet.Tables[0].Rows;
                }
            }

            //最大行数
            int maxRow = 17;
            //現在の行
            int CurRow = 0;
            //行数
            int RowCount = maxRow;

            if (V発注書.Count > 0)
            {
                RowCount = V発注書.Count;
            }

            int page = 1;
            double maxPage = Math.Ceiling((double)RowCount / maxRow);

            DateTime now = DateTime.Now;

            int lenB;

            //描画すべき行がある限りページを増やす
            while (RowCount > 0)
            {
                RowCount -= maxRow;

                paoRep.PageStart();

                //ヘッダー
                paoRep.Write("仕入先名", 仕入先名.Text != "" ? 仕入先名.Text : " ");
                paoRep.Write("発注コード", 発注コード.Text != "" ? 発注コード.Text : " ");
                paoRep.Write("発注日", 発注日.Text != "" ? 発注日.Text : " ");
                paoRep.Write("仕入先コード", 仕入先コード.Text != "" ? 仕入先コード.Text : " ");
                paoRep.Write("発注日", 発注日.Text != "" ? 発注日.Text : " ");
                paoRep.Write("ファックス番号", 仕入先ファックス番号.Text != "" ? 仕入先ファックス番号.Text : " ");
                paoRep.Write("担当者名", 仕入先担当者名.Text != "" ? 仕入先担当者名.Text : " ");
                paoRep.Write("購買コード", 購買コード.Text != "" ? 購買コード.Text : " ");
                paoRep.Write("シリーズ名", シリーズ名.Text != "" ? シリーズ名.Text : " ");
                paoRep.Write("ロット番号", ロット番号.Text != "" ? ロット番号.Text : " ");
                paoRep.Write("無効日時", 無効日時.Text != "" ? 無効日時.Text : " ");
                paoRep.Write("承認者名", 承認者名.Text != "" ? 承認者名.Text : " ");
                paoRep.Write("発注者名", 発注者名.Text != "" ? 発注者名.Text : " ");
                paoRep.Write("無効日時表示", 無効日時.Text != "" ? "＜この注文書は無効です。＞" : " ");
                paoRep.Write("発注版数表示", int.TryParse(発注版数.Text, out int version) && version > 1 ?
                    "（第 " + 発注版数.Text + " 版）" : " ");

                //隠しボックスだが不要　とりあえず置いておく
                paoRep.Write("発注版数", " ");

                //フッダー
                paoRep.Write("摘要", 摘要.Text != "" ? 摘要.Text : " ");
                paoRep.Write("Now", now.ToString("yyyy年M月d日"));
                paoRep.Write("ページ番号", (page + "/" + maxPage + " ページ").ToString());

                //明細
                for (var i = 0; i < maxRow; i++)
                {
                    if (CurRow >= V発注書.Count) break;

                    DataRow targetRow = V発注書[CurRow];

                    paoRep.Write("明細番号", (CurRow + 1).ToString(), i + 1);  //連番にしたい時はこちら。明細番号は歯抜けがあるので
                    //paoRep.Write("明細番号", targetRow["明細番号"].ToString() != "" ? targetRow["明細番号"].ToString() : " ", i + 1);
                    paoRep.Write("部品コード", targetRow["部品コード"].ToString() != "" ? targetRow["部品コード"].ToString() : " ", i + 1);
                    paoRep.Write("品名", targetRow["品名"].ToString() != "" ? targetRow["品名"].ToString() : " ", i + 1);
                    paoRep.Write("型番", targetRow["型番"].ToString() != "" ? targetRow["型番"].ToString() : " ", i + 1);
                    paoRep.Write("RoHS", targetRow["RoHS"].ToString() != "" ? targetRow["RoHS"].ToString() : " ", i + 1);
                    paoRep.Write("メーカー名", targetRow["メーカー名"].ToString() != "" ? targetRow["メーカー名"].ToString() : " ", i + 1);
                    paoRep.Write("発注数量", targetRow["発注数量"].ToString() != "" ? targetRow["発注数量"].ToString() : " ", i + 1);

                    decimal 発注単価 = 0;
                    string 表示単価 = "";
                    if (decimal.TryParse(targetRow["発注単価"].ToString(), out 発注単価))
                    {
                        表示単価 = 発注単価.ToString("N2");
                    }

                    paoRep.Write("発注単価", 表示単価, i + 1);

                    paoRep.Write("発注納期", targetRow["発注納期"].ToString() != "" ? ((DateTime)targetRow["発注納期"]).ToString("yyyy/M/d") : " ", i + 1);
                    paoRep.Write("回答納期", targetRow["回答納期"].ToString() != "" ? ((DateTime)targetRow["回答納期"]).ToString("yyyy/M/d") : " ", i + 1);


                    paoRep.z_Objects.SetObject("品名", i + 1);
                    lenB = Encoding.Default.GetBytes(targetRow["品名"].ToString()).Length;
                    if (lenB <= 40)
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 10;
                    }
                    else
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 8;
                    }


                    paoRep.z_Objects.SetObject("型番", i + 1);
                    lenB = Encoding.Default.GetBytes(targetRow["型番"].ToString()).Length;
                    if (lenB <= 40)
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 10;
                    }
                    else
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 8;
                    }


                    paoRep.z_Objects.SetObject("メーカー名", i + 1);
                    lenB = Encoding.Default.GetBytes(targetRow["メーカー名"].ToString()).Length;
                    if (lenB <= 28)
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 10;
                    }
                    else
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 7;
                    }

                    CurRow++;

                }

                page++;

                paoRep.PageEnd();

            }
            paoRep.Output();

        }


        private void コマンド終了_Click(object sender, EventArgs e)
        {
            Close(); // フォームを閉じる
        }

        private void Form_Unload(object sender, FormClosingEventArgs e)
        {
            LocalSetting ls = new LocalSetting();
            ls.SavePlace(LoginUserCode, this);

            try
            {
                Connect();

                // データへの変更されたときの処理
                if (IsChanged)
                {
                    var intRes = MessageBox.Show("変更内容を登録しますか？", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    switch (intRes)
                    {
                        case DialogResult.Yes:
                            //// 明細行数のみ確認する。エラー検出は確定時に行う
                            if (IsErrorDetails())
                            {
                                e.Cancel = true;
                                return;
                            }
                            //// 明細行が並べ替えられているときはその旨を知らせる
                            if (IsOrderByOn)
                            {
                                if (MessageBox.Show("明細行が並べ替えられています。" + Environment.NewLine +
                                                    "並べ替えを解除して登録しますか？", "登録コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                {
                                    発注明細1.CancelOrderBy();

                                }
                            }
                            if (SaveData(CurrentCode, CurrentEdition))
                            {
                                blnNewParts = false;
                                return;
                            }
                            else
                            {
                                if (MessageBox.Show("登録できませんでした。" + Environment.NewLine + Environment.NewLine +
                                 "強制終了しますか？", "エラー", MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Exclamation) == DialogResult.Yes)
                                {
                                    return;
                                }
                                else
                                {
                                    e.Cancel = true;
                                    return;
                                }
                            }

                            break;
                        case DialogResult.No:

                            //accessではメッセージボックスがNOの処理は何も書いてなかった

                            break;
                        case DialogResult.Cancel:
                            e.Cancel = true;
                            return;
                    }
                    // 新規モードで且つコードが取得済みのときはコードを戻す
                    if (IsNewData && !string.IsNullOrEmpty(CurrentCode) && CurrentEdition == 1)
                    {
                        // 採番された番号を戻す
                        if (!FunctionClass.Recycle(cn, CurrentCode))
                        {
                            MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine + Environment.NewLine +
                                            "発注コード　：　" + CurrentCode, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    return;
                }

                Form targetForm = Application.OpenForms["F_部品選択"];
                if (targetForm != null)
                {
                    targetForm.Close();
                }

                Form targetForm2 = Application.OpenForms["F_発注履歴"];
                if (targetForm2 != null)
                {
                    targetForm2.Close();
                }

            }
            catch (Exception ex)
            {
                Debug.Print(Name + "_Unload - " + ex.Message);
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 仕入先選択ボタン_Click(object sender, EventArgs e)
        {
            SearchForm = new F_検索();
            SearchForm.FilterName = "仕入先名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                string SelectedCode = SearchForm.SelectedCode;

                仕入先コード.Text = SelectedCode;
                UpdatedControl("仕入先コード");
            }
        }

        private void テストコマンド_Click(object sender, EventArgs e)
        {

        }

        public void チェック()
        {
            if (string.IsNullOrEmpty(確定日時.Text))
            {
                確定.Text = "";
            }
            else
            {
                確定.Text = "■";
            }

            if (string.IsNullOrEmpty(承認日時.Text))
            {
                承認.Text = "";
            }
            else
            {
                承認.Text = "■";
            }

            if (IsCompleted == "2")
            {
                入庫状況表示.Text = "■";
            }
            else if (IsCompleted == "1")
            {
                入庫状況表示.Text = "□";
            }
            else
            {
                入庫状況表示.Text = "";
            }

            if (送信.Text == "4")
            {
                送信.Text = "■";
            }
            else
            {
                送信.Text = "";
            }

            if (string.IsNullOrEmpty(無効日時.Text))
            {
                削除.Text = "";
            }
            else
            {
                削除.Text = "■";
            }
        }
        public bool LoadHeader()
        {
            try
            {
                Connect();

                string strSQL;

                strSQL = "SELECT * FROM V発注ヘッダ WHERE 発注コード='" + CurrentCode + "' AND 発注版数= " + CurrentEdition;

                if (!VariableSet.SetTable2Form(this, strSQL, cn)) return false;

                チェック();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("読み込み時エラーです" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        public bool LoadDetails(string strSQL, GcMultiRow multiRow)
        {
            try
            {
                Connect();

                using (SqlCommand command = new SqlCommand(strSQL, cn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        multiRow.DataSource = dataTable;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("読み込み時エラーです" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        private void UpdatedControl(string controlName)
        {
            try
            {

                switch (controlName)
                {
                    case "発注コード":
                        SetEditions(発注コード.Text);

                        if (args == "")
                        {
                            //argsが設定されていなければ版数を最新版とする
                            Connect();
                            string mysql = "SELECT 最新版数 FROM V発注_最新版 where 発注コード='" + CurrentCode + "'";
                            発注版数.Text = OriginalClass.GetScalar<string>(cn, mysql);
                            cn.Close();
                        }

                        if (!LoadHeader()) return;


                        string strSQL = "SELECT * FROM V発注明細 WHERE 発注コード='" + CurrentCode +
                                         "' AND 発注版数=" + CurrentEdition + " ORDER BY 明細番号";
                        //明細表示
                        if (!LoadDetails(strSQL, this.発注明細1.Detail)) return;

                        FunctionClass.LockData(this, IsDecided || IsDeleted, "発注コード", "発注版数");

                        ChangedData(false);
                        発注版数.Enabled = true;

                        if (IsLastEdition && IsApproved && !IsDeleted)
                        {
                            改版ボタン.Enabled = (!IsReceived) || PurchasingUserLogin;
                        }
                        else
                        {
                            改版ボタン.Enabled = false;
                        }

                        //編集モード
                        発注明細1.Detail.AllowUserToAddRows = (!IsDecided && !IsDeleted);
                        発注明細1.Detail.AllowUserToDeleteRows = (!IsDecided && !IsDeleted);
                        発注明細1.Detail.ReadOnly = (IsDecided || IsDeleted); //readonlyなのでaccessと真偽が逆になる                                                                       

                        コマンド複写.Enabled = true;
                        コマンド削除.Enabled = IsLastEdition && !IsDeleted;
                        コマンド発注書.Enabled = true;
                        コマンド送信.Enabled = IsApproved;
                        コマンド部品.Enabled = true;
                        コマンド購買.Enabled = IsLotOrder;
                        コマンド承認.Enabled = IsDecided && !IsApproved && !IsDeleted;
                        コマンド確定.Enabled = !IsApproved && !IsDeleted;

                        break;

                    case "発注版数":
                        if (!LoadHeader()) return;
                        strSQL = "SELECT * FROM V発注明細 WHERE 発注コード='" + CurrentCode +
                                 "' AND 発注版数=" + CurrentEdition + " ORDER BY 明細番号";
                        Connect();
                        if (!VariableSet.SetTable2Details(this.発注明細1.Detail, strSQL, cn)) return;

                        FunctionClass.LockData(this, IsDecided || IsDeleted, "発注コード", "発注版数");
                        ChangedData(false);

                        if (IsLastEdition && IsApproved && !IsDeleted)
                        {
                            改版ボタン.Enabled = (!IsReceived) || PurchasingUserLogin;
                        }
                        else
                        {
                            改版ボタン.Enabled = false;
                        }

                        発注明細1.Detail.AllowUserToAddRows = (!IsDecided && !IsDeleted);
                        発注明細1.Detail.AllowUserToDeleteRows = (!IsDecided && !IsDeleted);
                        発注明細1.Detail.ReadOnly = (IsDecided || IsDeleted);

                        コマンド複写.Enabled = true;
                        コマンド削除.Enabled = IsLastEdition && !IsDeleted;
                        コマンド発注書.Enabled = true;
                        コマンド送信.Enabled = IsApproved;
                        コマンド部品.Enabled = true;
                        コマンド購買.Enabled = !string.IsNullOrEmpty(購買コード.Text);
                        コマンド承認.Enabled = IsDecided && !IsApproved && !IsDeleted;
                        コマンド確定.Enabled = !IsApproved && !IsDeleted;

                        break;

                    case "発注日":
                        break;

                    case "発注者コード":
                        Connect();
                        string sql = $"SELECT 氏名  FROM M社員 WHERE 社員コード= {発注者コード.Text}";
                        発注者名.Text = OriginalClass.GetScalar<string>(cn, sql);
                        cn.Close();

                        break;

                    case "仕入先コード":
                        SetSupplier(仕入先コード.Text);
                        break;

                    case "在庫管理":
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_UpdatedControl - " + ex.GetType().ToString() + " : " + ex.Message);
            }
        }


        private void SetSupplier(string supplierCode)
        {
            try
            {
                Connect();

                string sql = "SELECT 仕入先名, 担当者名 + ISNULL('・' + 担当者名2, '') + ISNULL('・' + 担当者名3, '') AS 仕入先担当者名, " +
                             "REPLACE(窓口電話番号1 + '-' + 窓口電話番号2 + '-' + 窓口電話番号3, ' ', '') AS 仕入先電話番号, " +
                             "REPLACE(窓口ファックス番号1 + '-' + 窓口ファックス番号2 + '-' + 窓口ファックス番号3, ' ', '') AS 仕入先ファックス番号 " +
                             "FROM M仕入先 WHERE (仕入先コード = @SupplierCode)";

                using (SqlCommand command = new SqlCommand(sql, cn))
                {
                    command.Parameters.AddWithValue("@SupplierCode", supplierCode);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // フォームのコントロールに値を設定
                            仕入先名.Text = reader["仕入先名"].ToString();
                            仕入先担当者名.Text = reader["仕入先担当者名"].ToString();
                            仕入先電話番号.Text = reader["仕入先電話番号"].ToString();
                            仕入先ファックス番号.Text = reader["仕入先ファックス番号"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SetSupplier - {ex.GetType().Name} : {ex.Message}");
            }
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            try
            {
                this.発注明細1.Height += (this.Height - intWindowHeight);
                this.発注明細1.Width += (this.Width - intWindowWidth);
                intWindowHeight = this.Height;
                intWindowWidth = this.Width;

            }
            catch (Exception ex)
            {
                Debug.Print($"{nameof(Form_Resize)} - {ex.Message}");
            }
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
                    if (コマンド発注書.Enabled) コマンド発注書_Click(sender, e);
                    break;
                case Keys.F6:
                    if (コマンド発注書.Enabled) コマンド部品_Click(sender, e);
                    break;
                case Keys.F7:
                    if (コマンド発注書.Enabled) コマンド購買_Click(sender, e);
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

        private void 発注コード_Validated(object sender, EventArgs e)
        {
            UpdatedControl("発注コード");
        }

        private void 発注コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {           
            if (IsError(sender as Control, false) == true) e.Cancel = true;
        }
        private void 発注コード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 11);
        }

        private void 発注コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            //FunctionClass.LimitText(sender as Control, 8);
        }

        private void 発注コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                ComboBox comboBox = sender as ComboBox;
                if (comboBox != null)
                {
                    string strCode = this.発注コード.Text;
                    if (string.IsNullOrEmpty(strCode))
                        return;

                    strCode = FunctionClass.FormatCode(CH_ORDER, strCode);

                    if (strCode != this.発注コード.Text)
                    {
                        this.発注コード.Text = strCode;
                    }
                }
            }
        }

        private void 発注コード_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■発注コードを入力します。";
        }

        private void 発注コード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }



        private void 仕入先コード_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■仕入先コードを入力します。　■コードは８桁で先頭の 0 は省略できます。";
        }

        private void 仕入先コード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 仕入先担当者名_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■仕入先の担当者名を入力します。　■全角１０文字まで入力できます。";
        }

        private void 仕入先担当者名_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 発注版数_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■発注データの版数を入力します。　■通常、旧版を参照するときに入力します。";
        }

        private void 発注版数_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 摘要_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■発注書の摘要欄に表示する文章を入力します。";
        }

        private void 摘要_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 備考_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■業務連絡事項を入力します。発注書には反映されません。";
        }

        private void 備考_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }



        private void NoCredit_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■振込を行う必要が無いときのみチェックを入れてください。";
        }

        private void NoCredit_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 発注者コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 50, 500 }, new string[] { "Display", "Display2" });
            発注者コード.Invalidate();
            発注者コード.DroppedDown = true;
        }

        private void 備考_DoubleClick(object sender, EventArgs e)
        {
            //エラーとなっている
        }

        private void 在庫管理_CheckedChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        bool isEntering = true;
        bool originalValue = false;
        private void 在庫管理_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■在庫管理を行う場合はチェックを入れます。";
            // Enter イベント発生時に現在の値を保存
            isEntering = true;
            originalValue = 在庫管理.Checked;
        }

        private void 在庫管理_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }
        private void 在庫管理_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control, false) == true)
            {
                if (isEntering)
                {
                    // 変更前の値と現在の値を比較し、エラーがある場合は元に戻す
                    if (在庫管理.Checked != originalValue)
                    {
                        在庫管理.Checked = originalValue;
                        //e.Cancel = true; // 逃げれなくなるのでコメント
                    }

                    // Enter イベントの処理が完了したのでフラグをリセット
                    isEntering = false;
                }
                else
                {
                    // 通常の Validating イベントの処理をここに記述
                }
            }
        }

        private void 仕入先コード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 8);
            ChangedData(true);
        }

        private void 仕入先コード_Validated(object sender, EventArgs e)
        {
            UpdatedControl("仕入先コード");
        }

        private void 仕入先コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
            if (IsError(sender as Control, false) == true) e.Cancel = true;
        }

        private void 仕入先コード_DoubleClick(object sender, EventArgs e)
        {
            this.仕入先選択ボタン_Click(sender, e);
        }

        private void 仕入先コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Control control = (Control)sender;
                string strCode = control.Text.Trim();

                if (string.IsNullOrEmpty(strCode))
                {
                    return;
                }

                strCode = strCode.PadLeft(8, '0');

                if (strCode != control.Text)
                {
                    control.Text = strCode;
                    UpdatedControl("仕入先コード");
                }
            }
        }

        private void 仕入先コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Space:
                    仕入先選択ボタン_Click(sender, e);
                    break;
            }
        }

        private void 仕入先担当者名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 64);
            ChangedData(true);
        }

        private void 仕入先担当者名_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control, false) == true) e.Cancel = true;
        }

        private void 摘要_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 2000);
            ChangedData(true);
        }

        private void 摘要_Validated(object sender, EventArgs e)
        {
            UpdatedControl("摘要");
        }

        private void 摘要_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control, false) == true) e.Cancel = true;
        }

        private void 発注者コード_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 発注者コード_Validated(object sender, EventArgs e)
        {
            UpdatedControl("発注者コード");
        }

        private void 発注者コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control, false) == true) e.Cancel = true;
        }

        private void 発注者コード_Enter(object sender, EventArgs e)
        {
            //何のための処理か？？
            int listCount = 発注者コード.Items.Count;
        }

        string tmpdate = "";
        private void 発注日_Enter(object sender, EventArgs e)
        {
            tmpdate = 発注日.Text;
            toolStripStatusLabel1.Text = "■[space]キーでカレンダーを参照できます。　■未来の日付は入力できません。";
        }

        private void 発注日_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 発注日_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Space:
                    e.Handled = true;
                    発注日選択ボタン_Click(sender, e);
                    break;
            }

        }

        private void 発注日_Validated(object sender, EventArgs e)
        {
            UpdatedControl("発注日");
        }

        private void 発注日_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (発注日.Text != tmpdate)
            {
                if (IsError(sender as Control, false) == true)
                {                
                    発注日.Text = tmpdate;
                    e.Cancel = true; // 逃がさない
                }
            }
        }

        private void 発注日_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 10);
            ChangedData(true);
        }



        private void 発注版数_Validated(object sender, EventArgs e)
        {
            UpdatedControl("発注版数");
        }

        private void 発注版数_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control, false) == true) e.Cancel = true;
        }

        private void 備考_Validated(object sender, EventArgs e)
        {
            UpdatedControl("備考");
        }

        private void 備考_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control, false) == true) e.Cancel = true;
        }

        private void 備考_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 2000);
            ChangedData(true);
        }

        private void NoCredit_Validated(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void NoCredit_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control, false) == true) e.Cancel = true;
        }
    }
}

