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
using static u_net.CommonConstants;
using static u_net.Public.FunctionClass;
using Microsoft.Identity.Client.NativeInterop;
using System.Data.Common;
using MultiRowDesigner;
using GrapeCity.Win.MultiRow;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using ComboBox = System.Windows.Forms.ComboBox;
//using DocumentFormat.OpenXml.Presentation;

namespace u_net
{
    public partial class F_部品集合 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "部品集合";
        private int selected_frame = 0;
        public bool IsDirty = false;
        private int intWindowHeight = 0;
        private int intWindowWidth = 0;
        private bool blnEmergency = false;
        private bool combosetFlg = true;
        public F_部品集合()
        {
            this.Text = "部品集合";       // ウィンドウタイトルを設定
            this.MaximizeBox = false;  // 最大化ボタンを無効化
            this.MinimizeBox = false; //最小化ボタンを無効化

            InitializeComponent();
            分類コード.DropDownWidth = 550;
        }

        public string CurrentCode
        {
            get
            {
                return string.IsNullOrEmpty(部品集合コード.Text) ? "" : 部品集合コード.Text;
            }
        }
        public int CurrentEdition
        {
            get
            {
                int result;
                return int.TryParse(部品集合版数.Text, out result) ? result : 0;
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
        public bool IsDecided
        {  //確定日時がnullじゃなかったらture
            get
            {
                return !string.IsNullOrEmpty(確定日時.Text) ? true : false;
            }
        }
        public bool IsDeleted
        {
            get
            {
                return !string.IsNullOrEmpty(無効日時.Text) ? true : false;
            }
        }
        public bool IsNewData
        {
            get
            {
                return !コマンド新規.Enabled;
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

            if (string.IsNullOrEmpty(無効日時.Text))
            {
                削除.Text = "";
            }
            else
            {
                削除.Text = "■";
            }
        }
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

            OriginalClass ofn = new OriginalClass();
            ofn.SetComboBox(分類コード, "SELECT 分類記号 as Display,対象部品名 as Display2 ,分類コード as Value FROM M部品分類 " +
                " ORDER BY 分類記号");
            分類コード.DrawMode = DrawMode.OwnerDrawFixed;

            //MyApi myapi = new MyApi();
            //int xSize, ySize, intpixel, twipperdot;

            ////1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
            //intpixel = myapi.GetLogPixel();
            //twipperdot = myapi.GetTwipPerDot(intpixel);

            try
            {
                this.SuspendLayout();

                intWindowHeight = this.Height;
                intWindowWidth = this.Width;


                if (string.IsNullOrEmpty(args))
                {
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

                    // ','を区切り文字としてvarOpenArgsを分割
                    string[] argsArray = args.Split(',');

                    // 部品集合版数を整数に変換して部品集合版数のTextBoxに設定
                    this.部品集合版数.Text = argsArray.Length > 1 ? argsArray[1].Trim() : "";

                    // 部品集合コードを部品集合コードのTextBoxに設定
                    this.部品集合コード.Text = argsArray.Length > 0 ? argsArray[0].Trim() : "";

                    UpdatedControl(this.部品集合コード);
                    チェック();
                    combosetFlg = false;
                    this.コマンド複写.Enabled = true;
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
                ChangedData(false);
                this.ResumeLayout();
                if (fn.WaitForm != null) fn.WaitForm.Close();
            }
        }

        private bool GoNewMode()
        {
            try
            {
                // 各コントロール値を初期化
                VariableSet.SetControls(this);

                Connect();

                string result = FunctionClass.採番(cn, CH_ORDER).ToString();
                部品集合コード.Text = result.Substring(result.Length - 8);
                部品集合版数.Text = "1";

                // 明細部の初期化
                string strSQL = "SELECT * FROM V部品集合明細 WHERE 部品集合コード='" + CurrentCode + "' ORDER BY 明細番号";
                VariableSet.SetTable2Details(部品集合明細1.Detail, strSQL, cn);

                // ヘッダ部動作制御
                FunctionClass.LockData(this, false);
                集合名.Focus();
                部品集合コード.Enabled = false;
                部品集合版数.Enabled = false;

                コマンド新規.Enabled = false;
                コマンド修正.Enabled = true;
                コマンド複写.Enabled = false;
                コマンド削除.Enabled = false;
                コマンド編集.Enabled = false;
                コマンド改版.Enabled = false;

                コマンド承認.Enabled = false;
                コマンド確定.Enabled = false;
                コマンド登録.Enabled = false;

                // 明細部動作制御
                部品集合明細1.Detail.AllowUserToDeleteRows = true;
                部品集合明細1.Detail.ReadOnly = false;
                部品集合明細1.Detail.AllowUserToAddRows = true;
                部品集合明細1.Detail.AllowRowMove = true;

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
                Connect();
                bool success = false;
                string strSQL = "";

                // 各コントロール値をクリア
                VariableSet.SetControls(this);

                // 明細部の初期化
                strSQL = "SELECT * FROM V部品集合明細 WHERE 部品集合コード='" + this.CurrentCode +
                    "' AND 部品集合版数= " + CurrentEdition + " ORDER BY 明細番号";
                VariableSet.SetTable2Details(部品集合明細1.Detail, strSQL, cn);

                this.部品集合コード.Enabled = true;
                this.部品集合コード.Focus();

                LockData(this, true, "部品集合コード", "部品集合版数");

                this.コマンド新規.Enabled = true;
                this.コマンド修正.Enabled = false;
                this.コマンド複写.Enabled = false;
                this.コマンド確定.Enabled = false;
                this.コマンド登録.Enabled = false;

                success = true;
                return success;
            }
            catch (Exception ex)
            {
                Console.WriteLine(this.Name + "_GoModifyMode - " + ex.Message);
                return false;
            }
        }

        private void UpdateEditionList()
        {
            OriginalClass ofn = new OriginalClass();

            //日付はbitにコンバードできないのでエラーになる
            string strSQL = "SELECT 部品集合版数 AS Value, 部品集合版数 AS Display, " +
                    "{ fn REPLACE(STR(CONVERT(bit, 承認日時), 1, 0), '1', '■') } AS Display2 FROM M部品集合 " +
                    $"WHERE 部品集合コード = N'{CurrentCode}' ORDER BY 部品集合版数 DESC";
            ofn.SetComboBox(部品集合版数, strSQL);
            部品集合版数.DrawMode = DrawMode.OwnerDrawFixed;
            部品集合版数.DropDownWidth = 100;
        }

        private void UpdatedControl(Control controlObject)
        {
            FunctionClass fn = new FunctionClass();
            try
            {
                string strSQL;

                switch (controlObject.Name)
                {
                    case "部品集合コード":
                        Connect();
                        combosetFlg = true;
                        fn.DoWait("読み込んでいます...");

                        // 版数のソース更新
                        UpdateEditionList();

                        // OpenArgsが設定されていなければ版数を最新版とする
                        // 開いてからコードを変えて読み込むときはOpenArgsはnullに
                        // 設定されているため、最新版となる
                        if (string.IsNullOrEmpty(args))
                        {
                            Connect();
                            string mysql = "SELECT max(部品集合版数) FROM M部品集合 where 発注コード='" + CurrentCode + "'";
                            部品集合版数.Text = OriginalClass.GetScalar<string>(cn, mysql);
                            cn.Close();
                        }

                        // ヘッダ部の表示
                        if (!LoadHeader()) throw new Exception("初期化に失敗しました。");

                        // 明細部の表示
                        strSQL = $"SELECT *, case WHEN 廃止 <> 0 THEN '■' ELSE NULL END AS 廃止表示, " +
                            $" case WHEN 購買対象 <> 0 THEN '■' else null end as 購買対象表示 " +
                            $" FROM V部品集合明細 WHERE 部品集合コード='{this.CurrentCode}' AND " +
                            $"部品集合版数={this.CurrentEdition} ORDER BY 明細番号";

                        //明細表示
                        if (!VariableSet.SetTable2Details(部品集合明細1.Detail, strSQL, cn))
                            throw new Exception("初期化に失敗しました。");

                        // 動作を制御する
                        LockData(this, this.IsDecided, "部品集合コード");
                        this.部品集合版数.Enabled = true;
                 

                        部品集合明細1.Detail.AllowUserToAddRows = !this.IsDecided;
                        部品集合明細1.Detail.AllowUserToDeleteRows = !this.IsDecided;
                        部品集合明細1.Detail.ReadOnly = this.IsDecided;
                        部品集合明細1.Detail.AllowRowMove = !this.IsDecided;

                        ChangedData(false);

                        this.コマンド複写.Enabled = !this.IsDirty;
                        this.コマンド削除.Enabled = true;
                        this.コマンド改版.Enabled = this.IsApproved && (!this.IsDeleted);
                        this.コマンド承認.Enabled = this.IsDecided && (!this.IsDeleted);
                        this.コマンド確定.Enabled = (!this.IsApproved) && (!this.IsDeleted);
                        break;

                    case "部品集合版数":
                        fn.DoWait("読み込んでいます...");

                        // ヘッダ部の表示
                        if (!LoadHeader()) throw new Exception("初期化に失敗しました。");

                        // 明細部の表示
                        strSQL = $"SELECT * FROM V部品集合明細 WHERE 部品集合コード='{this.CurrentCode}' AND 部品集合版数={this.CurrentEdition} ORDER BY 明細番号";
                        if (!VariableSet.SetTable2Details(部品集合明細1.Detail, strSQL, cn))
                            throw new Exception("初期化に失敗しました。");

                        // 動作を制御する
                        LockData(this, this.IsDecided, "部品集合コード", "部品集合版数");
                        部品集合明細1.Detail.AllowUserToAddRows = !this.IsDecided;
                        部品集合明細1.Detail.AllowUserToDeleteRows = !this.IsDecided;
                        部品集合明細1.Detail.ReadOnly = this.IsDecided;
                        部品集合明細1.Detail.AllowRowMove = !this.IsDecided;

                        ChangedData(false);

                        this.コマンド複写.Enabled = !this.IsDirty;
                        this.コマンド削除.Enabled = true;
                        this.コマンド改版.Enabled = this.IsApproved && (!this.IsDeleted);
                        this.コマンド承認.Enabled = this.IsDecided && (!this.IsDeleted);
                        this.コマンド確定.Enabled = (!this.IsApproved) && (!this.IsDeleted);
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_UpdatedControl - " + ex.Message);
            }
            finally
            {
                if (fn.WaitForm != null) fn.WaitForm.Close();
            }
        }

        public bool LoadHeader()
        {
            try
            {
                Connect();
                string strSQL;
                strSQL = "SELECT * FROM V部品集合ヘッダ WHERE 部品集合コード='" + CurrentCode + "' AND 部品集合版数= " + CurrentEdition;

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

        private bool IsError(Control controlObject, bool cancel)
        {
            try
            {
                object varValue;

                // 対象コントロールがアクティブコントロールのときはTextプロパティをチェックする
                // そうでないときはValueプロパティをチェックする
                if (controlObject == ActiveControl)
                {
                    FunctionClass fn = new FunctionClass();
                    varValue = fn.Zn(controlObject.Text, "");
                }
                else
                {
                    varValue = controlObject.Text; // Value プロパティについては、対象コントロールの型によって適切なプロパティを使うべきです
                }

                switch (controlObject.Name)
                {
                    case "集合名":
                        if (varValue == null || string.IsNullOrEmpty(varValue.ToString()))
                        {
                            MessageBox.Show(controlObject.Name + "を入力してください。", "警告",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;

                    case "分類コード":
                        if (varValue == null || string.IsNullOrEmpty(varValue.ToString()))
                        {
                            MessageBox.Show("集合分類を選択してください。", "警告",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{controlObject.Name}_IsError - {ex.GetType().Name}: {ex.Message}");
                return true;
            }
        }

        private bool IsErrorData(string? exFieldName1 = null, string? exFieldName2 = null)
        {
            try
            {
                foreach (Control control in Controls)
                {
                    if ((control is System.Windows.Forms.TextBox || control is ComboBox) && control.Visible)
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
                int count = 部品集合明細1.Detail.RowCount;

                if (count == 0)
                {
                    MessageBox.Show("明細行を1行以上入力してください。", "部品集合明細", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    部品集合明細1.Detail.Focus();
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
        private void Form_Unload(object sender, FormClosingEventArgs e)
        {
            // 緊急事態のときは強制終了する
            if (blnEmergency) return;
            string LoginUserCode = CommonConstants.LoginUserCode;
            LocalSetting test = new LocalSetting();
            test.SavePlace(LoginUserCode, this);

            try
            {
                Connect();

                // 変更されていないとき
                if (!IsDirty)
                {
                    // 新規モードで且つコードが取得済みのときはコードを戻す
                    if (this.IsNewData && !string.IsNullOrEmpty(this.CurrentCode) && this.CurrentEdition == 1)
                    {
                        // 採番された番号を戻す
                        if (!ReturnCode(cn, "PTG" + this.CurrentCode))
                        {
                            MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine +
                                            "部品集合コード　：　" + this.CurrentCode, "警告",
                                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    return;
                }

                // 修正されているときは登録確認を行う
                DialogResult result = MessageBox.Show("変更内容を登録しますか？", "確認",
                    MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (result)
                {
                    case DialogResult.Yes:

                        if (!SaveData())
                        {
                            if (MessageBox.Show("エラーのため登録できませんでした。" + Environment.NewLine +
                                                "強制終了しますか？", "エラー", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                e.Cancel = true;
                                return;
                            }
                        }
                        break;

                    case DialogResult.No:
                        // 新規モードで且つコードが取得済みのときはコードを戻す
                        if (this.IsNewData && !string.IsNullOrEmpty(this.CurrentCode) && this.CurrentEdition == 1)
                        {
                            // 採番された番号を戻す
                            if (!ReturnCode(cn, "PTG" + this.CurrentCode))
                            {
                                MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine +
                                                "部品集合コード　：　" + this.CurrentCode, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                        break;

                    case DialogResult.Cancel:
                        e.Cancel = true;
                        break;
                }

                // 部品選択フォームが開かれていれば閉じる
                F_部品選択 form = (F_部品選択)Application.OpenForms["F_部品選択"];
                if (form != null)
                {
                    form.Close();
                }

            }
            catch (Exception ex)
            {
                Debug.Print(Name + "_Unload - " + ex.Message);
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private string GetHeadUserCode(string userCode)
        {
            //指定されたユーザー（コード）が所属する部の長のユーザーコードを得る
            string headUserCode = "";
            Connect();

            string strSQL = "SELECT 社員コード FROM M社員 WHERE " +
                            "ユーザグループ１=(" +
                            $"SELECT ユーザグループ１ FROM M社員 WHERE 社員コード='{userCode}') " +
                            "AND (" +
                            "ユーザグループ２ = 'Director' " +
                            "OR ユーザグループ２ = 'Boarder' " +
                            "OR ユーザグループ２ = 'President')";

            using (SqlCommand command = new SqlCommand(strSQL, cn))
            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    headUserCode = reader["社員コード"].ToString();
                }
            }

            return headUserCode;
        }

        private bool SaveData()
        {

            Connect();

            try
            {
                DateTime dteNow = DateTime.Now;
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
                DateTime dtmNow = FunctionClass.GetServerDate(cn);

                if (IsNewData)
                {
                    objControl1 = this.作成日時;
                    objControl2 = this.作成者コード;
                    objControl3 = this.作成者名;
                    varSaved1 = objControl1.Text;
                    varSaved2 = objControl2.Text;
                    varSaved3 = objControl3.Text;
                    objControl1.Text = dtmNow.ToString();
                    objControl2.Text = CommonConstants.LoginUserCode;
                    objControl3.Text = CommonConstants.LoginUserFullName;
                }

                objControl4 = this.更新日時;
                objControl5 = this.更新者コード;
                objControl6 = this.更新者名;

                varSaved4 = objControl4.Text;
                varSaved5 = objControl5.Text;
                varSaved6 = objControl6.Text;

                objControl4.Text = dtmNow.ToString();
                objControl5.Text = CommonConstants.LoginUserCode;
                objControl6.Text = CommonConstants.LoginUserFullName;

                if (RegTrans(CurrentCode, CurrentEdition, cn))
                {
                    return true;
                }
                else
                {

                    objControl1.Text = varSaved1?.ToString();
                    objControl2.Text = varSaved2?.ToString();
                    objControl3.Text = varSaved3?.ToString();
                    return false;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in SaveData: " + ex.Message);
                return false;
            }

        }

        private bool RegTrans(string codeString, int editionNumber, SqlConnection cn, bool UpdatePreEdition = false)
        {
            try
            {
                string strKey = "";

                SqlTransaction transaction = cn.BeginTransaction();

                // ヘッダ部の登録
                if (!SaveHeader(codeString, editionNumber, cn, transaction))
                {
                    transaction.Rollback(); // 変更をキャンセル
                    return false;
                }

                // 明細部の登録                    

                if (!SaveDetails(codeString, editionNumber, cn, transaction))
                {
                    transaction.Rollback(); // 変更をキャンセル
                    return false;
                }

                // 前版データの更新処理
                if (UpdatePreEdition)
                {
                    string sql;
                    strKey = $"部品集合コード='{codeString}' AND 部品集合版数={editionNumber - 1}";
                    if (IsApproved)
                    {
                        sql = $"UPDATE M部品集合 SET 無効日時=GETDATE(),無効者コード='{this.承認者コード.Text}' WHERE {strKey}";
                    }
                    else
                    {
                        sql = $"UPDATE M部品集合 SET 無効日時=NULL,無効者コード=NULL WHERE {strKey}";
                    }

                    SqlCommand cmd = new SqlCommand(sql, cn,transaction);
                    cmd.ExecuteNonQuery();
                }

                transaction.Commit(); // トランザクション完了
                if (!UpdateManager())
                {
                    Console.WriteLine("_RegTrans - 管理リストは更新できません。");
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{nameof(RegTrans)} - {ex.GetType().Name}: {ex.Message}");
                return false;
            }
            finally
            {
                cn.Close();
            }
        }

        public bool UpdateManager()
        {
            bool result = false;

            try
            {
                // 対象のフォームが読み込まれているかチェック
                if (Application.OpenForms["F_部品集合管理"] == null)
                {
                    return result;
                }

                // 対象のフォームのインスタンスを取得
                F_部品集合管理 frmManager = (F_部品集合管理)Application.OpenForms["F_部品集合管理"];

                // UpdateOnプロパティをtrueに設定
                frmManager.UpdateOn = true;
                frmManager.DoUpdate();
                result = true;
            }
            catch (Exception ex)
            {
                // エラーログ出力（この例ではコンソールに出力）
                Console.WriteLine($"{this.GetType().Name}_UpdateManager - {ex.GetType().Name} : {ex.Message}");
            }

            return result;
        }

        private bool SaveHeader(string codeString, int editionNumber, SqlConnection cn, SqlTransaction transaction)
        {
            try
            {
                string strwhere = " 部品集合コード='" + codeString + "' and 部品集合版数=" + editionNumber;


                if (!DataUpdater.UpdateOrInsertDataFrom(this, cn, "M部品集合", strwhere, "部品集合コード", transaction, "部品集合版数"))
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
                string strwhere = $"部品集合コード= {codeString} AND 部品集合版数= {editionNumber}";
                //明細部の登録
                if (!DataUpdater.UpdateOrInsertDetails(this.部品集合明細1.Detail, cn, "M部品集合明細", strwhere, "部品集合コード", transaction))
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

        private void コマンド登録_Click(object sender, EventArgs e)
        {
            // エラーチェック accessにはなかったが
            if (IsErrorData())
            {
                MessageBox.Show("登録できませんでした。", "登録コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            FunctionClass fn = new FunctionClass();
            fn.DoWait("登録しています...");


            // 登録処理
            if (SaveData())
            {
                // 登録に成功した
                ChangedData(false); // データ変更取り消し

                if (this.IsNewData)
                {
                    // 新規モードのとき
                    this.コマンド新規.Enabled = true;
                    this.コマンド修正.Enabled = false;
                }
                this.コマンド確定.Enabled = true;
                fn.WaitForm.Close();
                MessageBox.Show("登録を完了しました", "登録コマンド", MessageBoxButtons.OK);
            }
            else
            {
                // 登録に失敗したとき
                fn.WaitForm.Close();
                //this.コマンド登録.Enabled = true;

                MessageBox.Show("登録できませんでした。", "登録コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return;
        }


        private void Form_Resize(object sender, EventArgs e)
        {
            try
            {
                this.部品集合明細1.Height += (this.Height - intWindowHeight);
                this.部品集合明細1.Width += (this.Width - intWindowWidth);
                intWindowHeight = this.Height;
                intWindowWidth = this.Width;

            }
            catch (Exception ex)
            {
                Debug.Print($"{nameof(Form_Resize)} - {ex.Message}");
            }
        }
        private bool ErrCheck()
        {
            //入力確認    
            //if (!FunctionClass.IsError(this.部品コード)) return false;
            //if (!FunctionClass.IsError(this.版数)) return false;
            return true;
        }

        public void ChangedData(bool isChanged)
        {
            try
            {
                IsDirty = isChanged;

                if (ActiveControl == null) return;

                if (isChanged)
                {
                    this.Text = BASE_CAPTION + "*";
                }
                else
                {
                    this.Text = BASE_CAPTION;
                }

                if (this.ActiveControl == this.部品集合明細1) this.集合名.Focus();
                if (this.ActiveControl == this.部品集合コード) this.集合名.Focus();
                this.部品集合コード.Enabled = !isChanged;
                if (this.ActiveControl == this.部品集合版数) this.集合名.Focus();
                this.部品集合版数.Enabled = !isChanged;
                this.コマンド複写.Enabled = !isChanged;
                this.コマンド削除.Enabled = !isChanged;

                if (isChanged)
                {
                    this.コマンド承認.Enabled = false;
                    this.コマンド確定.Enabled = true;
                }
                this.コマンド登録.Enabled = isChanged;
            }
            catch (Exception ex)
            {
                Console.WriteLine("部品集合_ChangedData: " + ex.Message);
            }
        }

        private void コマンド新規_Click(object sender, EventArgs e)
        {
            try
            {
                Connect();

                Cursor.Current = Cursors.WaitCursor;
                this.DoubleBuffered = true;

                if (this.ActiveControl == this.コマンド新規)
                {
                    this.コマンド新規.Focus();
                }

                //  変更がある
                if (this.IsChanged)
                {
                    DialogResult intRes = MessageBox.Show("変更内容を登録しますか？", "新規コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
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

                if (!GoNewMode())
                {
                    MessageBox.Show("エラーのため新規モードへ移行できませんでした。", this.Name,
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    blnEmergency = true;
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{this.Name}_コマンド新規_Click - {ex.GetType().Name} : {ex.Message}");
                MessageBox.Show($"予期しないエラーが発生しました。: {ex.Message}", "新規コマンド",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {

                this.DoubleBuffered = false;
                Cursor.Current = Cursors.Default;
            }

        Bye_コマンド新規_Click:
            return;
        }


        private void コマンド承認_Click(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();
            try
            {
                string strAppUserCode;      // 承認ユーザーコード
                string strHeadCode;         // 部長のユーザーコード
                object varSaved1;
                object varSaved2;

                // 承認を取り消す場合は確認する
                if (IsApproved)
                {
                    if (MessageBox.Show("承認を取り消します。" + Environment.NewLine + Environment.NewLine +
                                        "承認を取り消すと購買設定が初期化されます。" + Environment.NewLine +
                                        "承認の取り消しは問題ないと判断できる場合のみ実行してください。" + Environment.NewLine +
                                        "通常、承認後の修正は改版したデータを修正します。", "承認コマンド",
                                        MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        goto Bye_コマンド承認_Click;
                    }
                }

                // ログインユーザーが発信者の長でないときは認証する
                strHeadCode = GetHeadUserCode(更新者コード.Text);
                if (LoginUserCode == strHeadCode)
                {
                    strAppUserCode = LoginUserCode;
                }
                else
                {
                    // 認証する
                    F_認証 fm = new F_認証();
                    fm.args = strHeadCode;
                    fm.ShowDialog();

                    // 認証フォームが閉じていれば、認証不成立となる
                    if (string.IsNullOrEmpty(strCertificateCode))
                    {
                        MessageBox.Show("認証できませんでした。" + Environment.NewLine +
                                        "承認はできません。", "承認コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        goto Bye_コマンド承認_Click;
                    }

                    strAppUserCode = strCertificateCode;
                }

                // 承認状況に応じて購買対象情報を更新する
                if (IsApproved)
                {
                    if (!InitBuyParts())
                    {
                        MessageBox.Show("購買対象設定を初期化できませんでした。", "承認コマンド",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        goto Bye_コマンド承認_Click;
                    }
                }
                else
                {
                    if (!SetDefaultDetails())
                    {
                        MessageBox.Show("初期購買対象部品を設定できませんでした。", "承認コマンド",
                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        goto Bye_コマンド承認_Click;
                    }
                }

                fn.DoWait("承認しています...");
                // 承認情報を設定する前に現在の情報を保存しておく
                varSaved1 = 承認日時.Text;
                varSaved2 = 承認者コード.Text;

                Connect();
                // 承認情報を設定する
                if (IsApproved)
                {
                    承認日時.Text = null;
                    承認者コード.Text = null;
                }
                else
                {
                    承認日時.Text = GetServerDate(cn).ToString();
                    承認者コード.Text = strAppUserCode;
                }

                // 表示されている内容で登録する

                if (RegTrans(CurrentCode, CurrentEdition, cn, CurrentEdition > 1))
                {
                    //部品集合版数の更新
                    UpdateEditionList();
                    コマンド改版.Enabled = IsApproved;
                    コマンド確定.Enabled = !IsApproved;
                    チェック();
                }
                else
                {
                    承認日時.Text = varSaved1.ToString();
                    承認者コード.Text = varSaved2.ToString();
                    MessageBox.Show("登録できませんでした。", "承認コマンド",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            Bye_コマンド承認_Click:
                if (fn.WaitForm != null) fn.WaitForm.Close();
                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{Name}_コマンド承認_Click - {ex.GetType().Name}: {ex.Message}");

                // エラーが発生した場合の処理
                MessageBox.Show("エラーが発生しました。" + Environment.NewLine +
                                ex.Message, "承認コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (fn.WaitForm != null) fn.WaitForm.Close();
            }
        }

        private bool SetDefaultDetails()
        {
            //明細の変更　DB接続は不要　後で修正する
            try
            {
                DataTable dataTable = (DataTable)部品集合明細1.Detail.DataSource;
                if (dataTable != null)
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        // 行の状態が Deleted の時は次の行へ
                        if (dataTable.Rows[i].RowState == DataRowState.Deleted)
                        {
                            continue;
                        }
                        if (dataTable.Rows[i]["明細番号"] != DBNull.Value)
                        {
                            if (Convert.ToInt32(dataTable.Rows[i]["明細番号"]) == 1)
                            {
                                dataTable.Rows[i]["購買対象"] = -1;
                            }
                        }
                    }
                    部品集合明細1.Detail.DataSource = dataTable; // 更新した DataTable を再セット
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.Print($"{nameof(SetDefaultDetails)} - {ex.Message}");
                return false;
            }
        }

        private bool InitBuyParts()
        {
            try
            {
                DataTable dataTable = (DataTable)部品集合明細1.Detail.DataSource;
                if (dataTable != null)
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        // 行の状態が Deleted の時は次の行へ
                        if (dataTable.Rows[i].RowState == DataRowState.Deleted)
                        {
                            continue;
                        }

                        dataTable.Rows[i]["購買対象"] = 0;

                    }

                    部品集合明細1.Detail.DataSource = dataTable; // 更新した DataTable を再セット
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in InitBuyParts: {ex.Message}");
                return false;
            }
        }


        private void コマンド確定_Click(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();
            try
            {
                if (!IsDecided)
                {
                    if (IsErrorData("部品集合コード", "部品集合版数"))
                        return;
                }

                fn.DoWait("登録しています...");

                // 登録前の確定日を保存しておく
                object varSaved = 確定日時.Text;

                Connect();
                // 確定日を設定する
                確定日時.Text = IsDecided ? null : GetServerDate(cn).ToString();

                // 表示データを登録する
                if (SaveData())
                {
                    //部品集合版数の更新
                    UpdateEditionList();

                    // 新規モードのときは修正モードへ移行する
                    if (IsNewData)
                    {
                        コマンド新規.Enabled = true;
                        コマンド修正.Enabled = false;
                    }

                    コマンド承認.Enabled = IsDecided;
                }
                else
                {
                    確定日時.Text = varSaved.ToString();
                    MessageBox.Show("登録できませんでした。", "確定コマンド",
                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                // 確定状態によって動作を制御する
                LockData(this, IsDecided, "部品集合コード", "部品集合版数");
                部品集合明細1.Detail.AllowUserToAddRows = !this.IsDecided;
                部品集合明細1.Detail.AllowUserToDeleteRows = !this.IsDecided;
                部品集合明細1.Detail.ReadOnly = this.IsDecided;
                部品集合明細1.Detail.AllowRowMove = !this.IsDecided;

                ChangedData(false);
                チェック();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{Name}_コマンド確定_Click - {ex.GetType().Name}: {ex.Message}");
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (fn.WaitForm != null) fn.WaitForm.Close();

            }
        }


        private void コマンド改版_Click(object sender, EventArgs e)
        {
            try
            {
                if (ActiveControl == コマンド改版)
                    コマンド改版.Focus();

                // 複写に成功すればインターフェースを更新する
                if (CopyData(CurrentCode, CurrentEdition + 1))
                {
                    // 変更された
                    ChangedData(true);

                    // ヘッダ部制御
                    LockData(this, false);
                    集合名.Focus();

                    コマンド新規.Enabled = false;
                    コマンド修正.Enabled = true;
                    コマンド編集.Enabled = false;
                    コマンド改版.Enabled = false;
                    コマンド承認.Enabled = false;
                    this.部品集合版数.Enabled = false;

                    // 明細部制御
                    部品集合明細1.Detail.AllowUserToAddRows = true;
                    部品集合明細1.Detail.AllowUserToDeleteRows = true;
                    部品集合明細1.Detail.ReadOnly = false;
                    部品集合明細1.Detail.AllowRowMove = true;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{Name}_コマンド改版_Click - {ex.GetType().Name}: {ex.Message}");
                MessageBox.Show("エラーが発生しました。", "改版コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void コマンド部品_Click(object sender, EventArgs e)
        {
            //部品集合明細 subform = Application.OpenForms.OfType<部品集合明細>().FirstOrDefault();
            F_部品 fm = new F_部品();

            fm.args = 部品集合明細1.PartsCode;
            fm.ShowDialog();
        }

        private void コマンドユニット管理_Click(object sender, EventArgs e)
        {
            F_ユニット管理 fm = new F_ユニット管理();
            fm.ShowDialog();
        }

        private F_検索 SearchForm;

        private void コマンド削除_Click(object sender, EventArgs e)
        {
            try
            {
                object varSaved1 = null; // 無効日時保存用
                object varSaved2 = null; // 無効者コード保存用
                DialogResult intRes;

                if (IsDeleted)
                {
                    intRes = MessageBox.Show($"部品集合：{CurrentCode}（第 {CurrentEdition} 版）{Environment.NewLine}{Environment.NewLine}" +
                        $"この部品集合データは削除されています。{Environment.NewLine}{Environment.NewLine}" +
                        "復元しますか？", "削除コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (intRes == DialogResult.No)
                        return;
                }
                else
                {
                    if (!IsApproved && CurrentEdition > 1)
                    {
                        intRes = MessageBox.Show($"部品集合：{CurrentCode}（第 {CurrentEdition} 版）{Environment.NewLine}{Environment.NewLine}" +
                            $"この部品集合データは改版中です。{Environment.NewLine}" +
                            $"前版に戻しますか？{Environment.NewLine}{Environment.NewLine}" +
                            $"[はい]を選択した場合、第 {CurrentEdition} 版のデータは完全に削除され、復元することはできません。{Environment.NewLine}" +
                            $"[いいえ]を選択した場合、全ての版に対して削除され、再度削除コマンドを実行することにより復元することができます。{Environment.NewLine}",
                            "削除コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                        if (intRes == DialogResult.Cancel)
                            return;

                        if (intRes == DialogResult.Yes)
                        {
                            // 完全削除（前版に戻す）
                            if (DeleteData(CurrentCode, CurrentEdition))
                            {
                                MessageBox.Show("削除しました。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);

                                // 削除後、前版データを表示する

                                部品集合版数.Focus();
                                部品集合版数.Text = (CurrentEdition - 1).ToString();
                            }
                            else
                            {
                                MessageBox.Show("削除できませんでした。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                        }
                    }
                    else
                    {
                        intRes = MessageBox.Show($"部品集合：{CurrentCode}（第 {CurrentEdition} 版）{Environment.NewLine}{Environment.NewLine}" +
                            $"この部品集合データを削除します。{Environment.NewLine}" +
                            $"削除後、再度削除コマンドを実行することにより復元することができます。{Environment.NewLine}{Environment.NewLine}" +
                            "削除しますか？", "削除コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (intRes == DialogResult.No)
                            return;
                    }
                }

                // 削除情報設定
                varSaved1 = 無効日時.Text;
                varSaved2 = 無効者コード.Text;

                Connect();

                if (IsDeleted)
                {
                    無効日時.Text = null;
                    無効者コード.Text = null;
                }
                else
                {

                    無効日時.Text = GetServerDate(cn).ToString();
                    無効者コード.Text = strCertificateCode;

                }

                // 表示データを登録する
                if (RegTrans(CurrentCode, CurrentEdition, cn))
                {
                    // 登録成功（修正モードで呼び出した状態にならないといけない）
                    // 発注コード.Enabled = true;
                    // コマンド複写.Enabled = true;
                    // コマンド削除.Enabled = true;
                    // コマンド承認.Enabled = IsDecided;
                    // コマンド登録.Enabled = false;
                }
                else
                {
                    // 登録失敗
                    無効日時.Text = varSaved1.ToString();
                    無効者コード.Text = varSaved2.ToString();
                    MessageBox.Show("削除できませんでした。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                チェック();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{Name}_コマンド削除_Click - {ex.GetType().Name}: {ex.Message}");
            }
        }


        private bool DeleteData(string codeString, int editionNumber)
        {
            try
            {
                string strKey = $"部品集合コード='{codeString}' AND 部品集合版数={editionNumber}";

                Connect();
                using (SqlTransaction transaction = cn.BeginTransaction())
                {
                    try
                    {
                        // 部品集合データを削除
                        using (SqlCommand command1 = new SqlCommand($"DELETE FROM M部品集合 WHERE {strKey}", cn, transaction))
                        {
                            command1.ExecuteNonQuery();
                        }
                        // 部品集合明細データを削除
                        using (SqlCommand command2 = new SqlCommand($"DELETE FROM M部品集合明細 WHERE {strKey}", cn, transaction))
                        {
                            command2.ExecuteNonQuery();
                        }

                        transaction.Commit(); // トランザクションのコミット
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback(); // トランザクションのロールバック
                        Debug.WriteLine($"Error in DeleteData: {ex.Message}");
                        MessageBox.Show("エラーが発生しました。" + Environment.NewLine + Environment.NewLine +
                                ex.Message, "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in DeleteData: {ex.Message}");
                return false;
            }
        }


        private bool CopyData(string codeString, int editionNumber)
        {
            try
            {
                //Connect();
                // 明細部のキー情報を設定する（同時に購買対象設定を初期化する）

                // 明細部の初期設定
                DataTable dataTable = (DataTable)部品集合明細1.Detail.DataSource;
                if (dataTable != null)
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        // 行の状態が Deleted の時は次の行へ
                        if (dataTable.Rows[i].RowState == DataRowState.Deleted)
                        {
                            continue;
                        }
                        dataTable.Rows[i]["部品集合コード"] = codeString;
                        dataTable.Rows[i]["部品集合版数"] = editionNumber;
                        dataTable.Rows[i]["購買対象"] = 0;
                    }
                    部品集合明細1.Detail.DataSource = dataTable; // 更新した DataTable を再セット
                }


                // キー情報を設定する
                this.部品集合コード.Text = codeString;
                this.部品集合版数.Text = editionNumber.ToString();

                // 初期値を設定する
                this.作成日時.Text = null;
                this.作成者コード.Text = null;
                this.作成者名.Text = null;
                this.更新日時.Text = null;
                this.更新者コード.Text = null;
                this.更新者名.Text = null;
                this.確定日時.Text = null;
                this.確定者コード.Text = null;
                this.承認日時.Text = null;
                this.承認者コード.Text = null;
                this.無効日時.Text = null;
                this.無効者コード.Text = null;

                チェック();

                return true;
            }
            catch (Exception ex)
            {
                // エラーハンドリング: 例外が発生した場合にエラーをログに記録し、適切に処理する
                Debug.WriteLine($"{this.Name}_CopyData - {ex.Message}");
                return false;
            }
        }

        private void コマンド編集_Click(object sender, EventArgs e)
        {
            MessageBox.Show("本バージョンではこのコマンドはサポートされていません。", "編集コマンド",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド終了_Click(object sender, EventArgs e)
        {
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
                    switch (this.ActiveControl.Name)
                    {
                        case "備考":
                            return;
                    }
                    SelectNextControl(ActiveControl, true, true, true, true);
                    //e.Handled = true;
                    //e.SuppressKeyPress = true;
                    break;
                case Keys.Space: //コンボボックスならドロップダウン
                    {
                        Control activeControl = this.ActiveControl;
                        if (activeControl is System.Windows.Forms.ComboBox)
                        {
                            e.Handled = true;
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
                    if (コマンド修正.Enabled)
                    {
                        コマンド修正.Focus();
                        コマンド修正_Click(sender, e);
                    }
                    break;
                case Keys.F3:
                    if (コマンド複写.Enabled) コマンド複写_Click(sender, e);
                    break;
                case Keys.F4:
                    if (コマンド削除.Enabled) コマンド削除_Click(sender, e);
                    break;
                case Keys.F5:
                    if (コマンド編集.Enabled) コマンド編集_Click(sender, e);
                    break;
                case Keys.F6:
                    if (コマンド改版.Enabled) コマンド改版_Click(sender, e);
                    break;
                case Keys.F7:
                    if (コマンド部品.Enabled) コマンド部品_Click(sender, e);
                    break;
                case Keys.F8:
                    if (コマンドユニット管理.Enabled) コマンドユニット管理_Click(sender, e);
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

        private void 集合名_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角25文字まで入力できます。";
        }

        private void 集合名_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 分類コード_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■[space]キーでドロップダウンリストを表示します。";
        }

        private void 分類コード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 備考_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角250文字まで入力できます。";
        }

        private void 備考_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void コマンド修正_Click(object sender, EventArgs e)
        {
            try
            {
                Connect();
                DialogResult intRes;

                // 変更されていないとき
                if (!IsDirty)
                {
                    // 新規モードで且つコードが取得済みのときはコードを戻す
                    if (IsNewData && CurrentCode != "" && CurrentEdition == 1)
                    {
                        // 採番された番号を戻す
                        if (!ReturnCode(cn, "PTG" + CurrentCode))
                        {
                            MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine +
                                            "部品集合コード　：　" + CurrentCode, "修正コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }

                    // 修正モードへ移行する
                    if (!GoModifyMode())
                    {
                        intRes = MessageBox.Show("エラーが発生しました。" + Environment.NewLine +
                                            "強制終了しますか？　" + CurrentCode, "修正コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                        switch (intRes)
                        {
                            case DialogResult.Yes:
                                this.Close();
                                break;
                            case DialogResult.No:
                            case DialogResult.Cancel:
                                return;
                        }
                    }
                    else
                    {
                        goto Bye_コマンド修正_Click;
                    }
                }

                // 変更されているときは登録確認を行う
                intRes = MessageBox.Show("変更内容を登録しますか？", "修正コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (intRes)
                {
                    case DialogResult.Yes:

                        // 登録処理
                        if (!SaveData())
                        {
                            MessageBox.Show("エラーのため登録できません。", "修正コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Bye_コマンド修正_Click;
                        }
                        break;

                    case DialogResult.No:
                        // 新規モードで且つコードが取得済みのときはコードを戻す
                        if (IsNewData && CurrentCode != "" && CurrentEdition == 1)
                        {
                            // 採番された番号を戻す
                            if (!ReturnCode(cn, "PTG" + CurrentCode))
                            {
                                MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine +
                                                "部品集合コード　：　" + CurrentCode, "修正コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                        break;

                    case DialogResult.Cancel:
                        return;
                }

                // 修正モードへ移行する
                if (!GoModifyMode())
                    goto Bye_コマンド修正_Click;

                Bye_コマンド修正_Click:

                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{Name}_コマンド修正_Click - {ex.GetType().Name}: {ex.Message}");

                // エラーが発生した場合の処理
                if (MessageBox.Show("エラーが発生しました。" + Environment.NewLine +
                                    "管理者に連絡してください。" + Environment.NewLine + Environment.NewLine +
                                    "強制終了しますか？", "修正コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    Close();
                }
            }
        }

        private void コマンド複写_Click(object sender, EventArgs e)
        {
            try
            {
                combosetFlg = true;

                if (ActiveControl == コマンド複写)
                    コマンド複写.Focus();
                Connect();

                // 複写に成功すればインターフェースを更新する
                string original = 採番(cn, "PTG");
                string newCode = original.Substring(original.Length - 8);

                if (CopyData(newCode, 1))
                {
                    // 変更された
                    ChangedData(true);

                    // ヘッダ部制御
                    LockData(this, false);
                    集合名.Focus();
                    コマンド新規.Enabled = false;
                    コマンド修正.Enabled = true;
                    コマンド編集.Enabled = false;
                    コマンド改版.Enabled = false;
                    コマンド承認.Enabled = false;
                    部品集合版数.Enabled = false;

                    // 明細部制御
                    // 明細部動作制御
                    部品集合明細1.Detail.AllowUserToDeleteRows = true;
                    部品集合明細1.Detail.ReadOnly = false;
                    部品集合明細1.Detail.AllowUserToAddRows = true;
                    部品集合明細1.Detail.AllowRowMove = true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{Name}_コマンド複写_Click - {ex.GetType().Name}: {ex.Message}");

                // エラーが発生した場合の処理
                MessageBox.Show("エラーが発生しました。", "複写コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }

        private void 集合名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((System.Windows.Forms.TextBox)sender), 50);
            ChangedData(true);
        }

        private void 備考_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText((System.Windows.Forms.TextBox)sender, 500);
            ChangedData(true);
        }

        private void 部品集合コード_Validated(object sender, EventArgs e)
        {
            if (部品集合コード.Modified == false) return;
            UpdatedControl(this.部品集合コード);
        }

        private void 部品集合版数_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (combosetFlg) return;
            UpdatedControl(this.部品集合版数);
        }

        private void 分類コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            //OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 50 }, new string[] { "Display" });
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 50, 500 }, new string[] { "Display", "Display2" });

            分類コード.Invalidate();
            // 分類コード.DroppedDown = true;
        }

        private void 分類コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            Connect();
            string sql = $"select 対象部品名 from M部品分類 WHERE 分類記号= '{分類コード.Text}'";
            集合分類.Text = OriginalClass.GetScalar<string>(cn, sql);
            cn.Close();
            ChangedData(true);
        }

        private void 部品集合版数_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 50, 50 }, new string[] { "Display", "Display2" });
            部品集合版数.Invalidate();
            部品集合版数.DroppedDown = true;
        }

        private void 分類コード_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }
    }
}
