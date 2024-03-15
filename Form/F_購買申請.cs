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
using static System.ComponentModel.Design.ObjectSelectorEditor;
using Microsoft.EntityFrameworkCore.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Transactions;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Data.Common;
using Microsoft.Identity.Client;
using Microsoft.IdentityModel.Tokens;

namespace u_net
{
    public partial class F_購買申請 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "購買申請";
        private bool setCombo = true;
        private bool Terminate;//強制終了用フラグ
        private bool setProduct = false;
        int intWindowHeight;
        int intWindowWidth;
        private bool loaded = true;


        const int WM_UNDO = 0x0304; // Windows message for undo

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, int Msg, IntPtr wParam, IntPtr lParam);

        public bool IsApproved
        {
            get
            {
                return !string.IsNullOrEmpty(確認者コード3.Text);

            }
        }

        public bool IsScheduled
        {
            get
            {
                return !string.IsNullOrEmpty(this.Scheduled.Text);
            }
        }

        public bool IsCompleted
        {
            get
            {
                return !string.IsNullOrEmpty(this.完了.Text);
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

        public bool IsSeriesOn
        {
            get
            {
                return シリーズ名.Text != null;
            }
        }

        public string CurrentCode
        {
            get
            {
                return string.IsNullOrEmpty(購買申請コード.Text) ? "" : 購買申請コード.Text;
            }
        }

        public int CurrentEdition
        {
            get
            {
                return string.IsNullOrEmpty(購買申請版数.Text) ? 0 : int.Parse(購買申請版数.Text);
            }
        }

        public bool IsDeleted
        {
            get
            {
                bool isEmptyOrDbNull = string.IsNullOrEmpty(this.無効日時.Text) || Convert.IsDBNull(this.無効日時.Text);
                return !isEmptyOrDbNull;
            }
        }

        public bool IsDecided
        {
            get
            {
                return !string.IsNullOrEmpty(確認者コード3.Text);
            }
        }

        public bool IsEnd
        {
            get
            {
                return !string.IsNullOrEmpty(終了入力.Text);
            }
        }

        public F_購買申請()
        {
            this.Text = "購買申請";       // ウィンドウタイトルを設定
            this.MaximizeBox = false;  // 最大化ボタンを無効化
            this.MinimizeBox = false; //最小化ボタンを無効化

            InitializeComponent();

            申請者コード.DropDownWidth = 204;
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
            foreach (Control control in Controls)
            {
                control.PreviewKeyDown += OriginalClass.ValidateCheck;
            }

            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");


            //実行中フォーム起動
            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(CommonConstants.LoginUserCode, this);

            //コンボボックスの設定
            OriginalClass ofn = new OriginalClass();
            ofn.SetComboBox(購買申請コード, "SELECT 購買申請コード as Display,購買申請コード as Value FROM T購買申請 ORDER BY 購買申請コード DESC");

            //購買申請コードを登録してから、updatcontrolで行う
            //ofn.SetComboBox(購買申請版数, "SELECT 購買申請版数 as Display , 購買申請版数 as Value FROM T購買申請 ORDER BY 購買申請版数 DESC");

            ofn.SetComboBox(商品コード, "SELECT M商品.商品コード  as Display, M商品.商品名  as Display2, Mシリーズ.シリーズ名  as Display3," +
                " - CONVERT (int, CONVERT (bit, ISNULL(M商品.シリーズコード, 0)))  as Display4," +
                " 商品コード as Value FROM M商品 " +
                "LEFT OUTER JOIN Mシリーズ ON M商品.シリーズコード = Mシリーズ.シリーズコード " +
                "ORDER BY M商品.商品名");
            商品コード.DrawMode = DrawMode.OwnerDrawFixed;
            商品コード.DropDownWidth = 700;

            ofn.SetComboBox(申請者コード, "SELECT [社員コード] as Display, 氏名 as Display2 ,社員コード as Value FROM M社員 WHERE (退社 IS NULL) AND (削除日時 IS NULL) AND (ふりがな <> N'ん') ORDER BY ふりがな");
            申請者コード.DrawMode = DrawMode.OwnerDrawFixed;
            申請者コード.DropDownWidth = 350;

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
                    // 修正モードへ
                    if (!GoModifyMode())
                    {
                        throw new Exception("初期化に失敗しました。");
                    }

                    //引数をカンマで分けてそれぞれの項目に設定
                    int indexOfComma = args.IndexOf(",");
                    string editionString = args.Substring(indexOfComma + 1).Trim();
                    int edition;
                    if (int.TryParse(editionString, out edition))
                    {
                        購買申請版数.Text = edition.ToString();
                    }

                    string codeString = args.Substring(0, indexOfComma).Trim();
                    購買申請コード.Text = codeString;

                    UpdatedControl(購買申請コード);
                }
                fn.WaitForm.Close();

                loaded = false;

                if (Terminate)
                    MessageBox.Show("強制終了指示", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                setCombo = false;
                this.ResumeLayout();
                fn.WaitForm.Close();
            }
        }
        private void LockCtl()
        {
            this.申請者名.ReadOnly = true;
            this.シリーズ名.ReadOnly = true;
            this.登録日時.ReadOnly = true;
            this.登録者コード.ReadOnly = true;
            this.登録者名.ReadOnly = true;
            this.確認_営業部.ReadOnly = true;
            this.Scheduled.ReadOnly = true;
            this.完了.ReadOnly = true;
            this.削除.ReadOnly = true;
            this.確認_製造部.ReadOnly = true;
            this.終了入力.ReadOnly = true;
        }

        private bool GoNewMode()
        {
            try
            {
                bool result = false;

                // 各コントロール値を初期化
                VariableSet.SetControls(this);

                Connect();

                this.購買申請コード.Text = FunctionClass.採番(cn, "PUR");
                this.購買申請版数.SelectedValue = 1;
                this.購買申請版数.Text = "1";

                // データを初期化する
                //this.申請日.Text = DateTime.Now.Date.ToString();
                this.申請日.Text = FunctionClass.GetServerDate(cn).ToString("yyyy/MM/dd");
                this.申請者コード.Text = CommonConstants.LoginUserCode;
                this.申請者名.Text = ((DataRowView)申請者コード.SelectedItem)?.Row.Field<String>("Display2")?.ToString();
                this.確認日時3.Text = null;
                this.確認者コード3.Text = null;
                this.確認日時1.Text = null;
                this.確認者コード1.Text = null;
                this.終了日時.Text = null;
                this.終了者コード.Text = null;
                this.登録日時.Text = null;
                this.登録者コード.Text = null;
                this.登録者名.Text = null;
                this.無効日時.Text = null;
                this.無効者コード.Text = null;
                this.ItemRevision.Text = "1";

                // 編集による変更がない状態へ遷移する
                ChangedData(false);

                //インターフェースを制御する
                FunctionClass.LockData(this, false);
                // 個別にReadOnlyを設定
                LockCtl();
                this.申請者コード.Focus();
                this.購買申請コード.Enabled = false;
                this.購買申請版数.Enabled = false;
                this.コマンド新規.Enabled = false;
                this.コマンド読込.Enabled = true;
                this.コマンド複写.Enabled = false;
                this.コマンド削除.Enabled = false;
                this.コマンド改版.Enabled = false;
                this.コマンド承認.Enabled = false;
                this.コマンド登録.Enabled = false;
                this.確認_製造部ボタン.Enabled = false;
                this.終了ボタン.Enabled = false;

                return true;
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_GoNewMode - " + ex.Message);
                return false;
            }
        }

        private bool IsLimit_N(object value, int maxLength, int decimalPlaces, string controlName)
        {
            try
            {
                if (value == null || value.Equals(DBNull.Value))
                    return false;

                string stringValue = value.ToString();
                int integerPartLength = stringValue.IndexOf('.') == -1 ? stringValue.Length : stringValue.IndexOf('.');

                if (integerPartLength > maxLength || stringValue.Length - integerPartLength - 1 > decimalPlaces)
                {
                    string errorMessage = $"入力された値が制限を超えています。{Environment.NewLine}制限: {maxLength}桁（整数部）、{decimalPlaces}桁（小数部）{Environment.NewLine}{controlName}";
                    MessageBox.Show(errorMessage, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private bool IsError(Control controlObject, bool cancel)
        {
            bool result = false;

            try
            {
                object varValue = controlObject.Text; // Assuming Text property is used for the value

                switch (controlObject.Name)
                {
                    case "購買申請コード":
                        // Implement CheckPurchase logic here if needed
                        break;
                    case "申請者コード":
                        if (string.IsNullOrEmpty(varValue.ToString()) || varValue.Equals(DBNull.Value))
                        {
                            MessageBox.Show("申請者を選択してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        break;
                    case "申請日":
                        if (string.IsNullOrEmpty(varValue.ToString()) || varValue.Equals(DBNull.Value))
                        {
                            MessageBox.Show("申請日を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        if (!DateTime.TryParse(varValue.ToString(), out DateTime dateValue))
                        {
                            MessageBox.Show("日付を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        if (dateValue > DateTime.Now)
                        {
                            MessageBox.Show("未来日付は入力できません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        break;
                    case "ロット番号1":
                    case "ロット番号2":
                        if (string.IsNullOrEmpty(varValue.ToString()) || varValue.Equals(DBNull.Value))
                            goto Bye_IsError;
                        if (!FunctionClass.IsLimit_N(varValue, 7, 2, controlObject.Name))
                            goto Exit_IsError;
                        if (decimal.TryParse(varValue.ToString(), out decimal dec))
                        {
                            if (dec < 0)
                            {
                                string strMsg = "正数値を入力してください。" + Environment.NewLine + Environment.NewLine + controlObject.Name;
                                MessageBox.Show(strMsg, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                goto Exit_IsError;
                            }
                        }
                        else
                        {
                            string strMsg = "数値を入力してください。" + Environment.NewLine + Environment.NewLine + controlObject.Name;
                            MessageBox.Show(strMsg, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        break;
                    case "数量":
                        if (!int.TryParse(varValue.ToString(), out _))
                        {
                            string strMsg = "数値を入力してください。" + Environment.NewLine + Environment.NewLine + controlObject.Name;
                            MessageBox.Show(strMsg, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }

                        if (string.IsNullOrEmpty(varValue.ToString()) || varValue.Equals(DBNull.Value))
                        {
                            MessageBox.Show(controlObject.Name + " を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }

                        if (!FunctionClass.IsLimit_N(varValue, 14, 0, controlObject.Name))
                            goto Exit_IsError;



                        break;
                    case "材料単価":
                        if (!decimal.TryParse(varValue.ToString(), out _))
                        {
                            string strMsg = "数値を入力してください。" + Environment.NewLine + Environment.NewLine + controlObject.Name;
                            MessageBox.Show(strMsg, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }

                        if (string.IsNullOrEmpty(varValue.ToString()) || varValue.Equals(DBNull.Value))
                        {
                            MessageBox.Show(controlObject.Name + " を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }

                        if (!FunctionClass.IsLimit_N(varValue, 14, 2, controlObject.Name))
                            goto Exit_IsError;


                        break;
                    case "購買納期":
                        if (string.IsNullOrEmpty(varValue.ToString()) || varValue.Equals(DBNull.Value))
                            goto Bye_IsError;
                        if (!DateTime.TryParse(varValue.ToString(), out DateTime purchaseDate))
                        {
                            MessageBox.Show("日付を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }

                        // 入力時はチェックせず、承認時にチェックする内容
                        if (cancel)
                        {
                            if (!string.IsNullOrEmpty(申請日.Text) && purchaseDate < DateTime.Parse(申請日.Text))
                            {
                                MessageBox.Show("申請日以前の日付は入力できません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                goto Exit_IsError;
                            }
                            if (!string.IsNullOrEmpty(出荷予定日.Text) && DateTime.Parse(出荷予定日.Text) < purchaseDate)
                            {
                                MessageBox.Show("出荷予定日以降の日付は入力できません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                goto Exit_IsError;
                            }
                        }
                        break;
                    case "出荷予定日":
                        if (string.IsNullOrEmpty(varValue.ToString()) || varValue.Equals(DBNull.Value))
                        {
                            MessageBox.Show(controlObject.Name + " を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        if (!DateTime.TryParse(varValue.ToString(), out DateTime shipmentDate))
                        {
                            MessageBox.Show("日付を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }

                        // 入力時はチェックせず、承認時にチェックする内容
                        if (cancel)
                        {
                            if (!string.IsNullOrEmpty(申請日.Text) && shipmentDate < DateTime.Parse(申請日.Text))
                            {
                                MessageBox.Show("申請日以前の日付は入力できません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                goto Exit_IsError;
                            }
                            if (!string.IsNullOrEmpty(購買納期.Text) && shipmentDate < DateTime.Parse(購買納期.Text))
                            {
                                MessageBox.Show("購買納期以前の日付は入力できません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                goto Exit_IsError;
                            }
                        }
                        break;
                    default:
                        break;
                }

            Bye_IsError:
                return false;

            Exit_IsError:
                result = true;
                if (cancel)
                    //return false;
                    return result;

                cancel = true;

                SendMessage(controlObject.Handle, WM_UNDO, IntPtr.Zero, IntPtr.Zero);

                return true;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{this.Name}_IsError - {ex.GetType().Name} : {ex.Message}");
                return true;
            }
        }

        private bool IsErrorData(string exFieldName1, string exFieldName2 = null)
        {
            foreach (Control control in this.Controls)
            {
                if ((control is TextBox || control is ComboBox || control is CheckBox) && control.Visible)
                {
                    if (control.Name != exFieldName1 && control.Name != exFieldName2)
                    {
                        if (IsError(control, true))
                        {
                            // JumpFocus is not directly translatable, you might need to implement a similar logic
                            control.Focus();
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        public void ChangedData(bool isChanged)
        {
            if (loaded == true) return;

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
                if (this.ActiveControl == this.購買申請コード)
                {
                    this.申請者コード.Focus();
                }
                this.購買申請コード.Enabled = !isChanged;
                if (this.ActiveControl == this.購買申請版数)
                {
                    this.申請者コード.Focus();
                }
                this.購買申請版数.Enabled = !isChanged;
                this.コマンド複写.Enabled = !isChanged;
                this.コマンド削除.Enabled = !isChanged;
                this.コマンド承認.Enabled = !isChanged;
                this.コマンド登録.Enabled = isChanged;
                this.確認_製造部ボタン.Enabled = !isChanged;
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

                this.購買申請コード.Enabled = true;
                this.購買申請コード.Focus();
                // 購買申請コードコントロールが使用可能になってから LockData を呼び出す
                FunctionClass.LockData(this, true, "購買申請コード", "購買申請版数");
                // 個別にReadOnlyを設定
                LockCtl();
                this.コマンド新規.Enabled = true;
                this.コマンド読込.Enabled = false;
                this.コマンド複写.Enabled = true;
                this.コマンド削除.Enabled = true;
                this.コマンド商品.Enabled = true;
                this.コマンド承認.Enabled = true;
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

        private void コマンド登録_Click(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();

            try
            {
                this.DoubleBuffered = true;

                fn.DoWait("登録しています...");

                if (ActiveControl == コマンド登録)
                {
                    GetNextControl(コマンド登録, false).Focus();
                }

                if (SaveData(CurrentCode, CurrentEdition))
                {
                    // 登録成功
                    ChangedData(false);
                    購買申請コード.Enabled = true;

                    //新規モードのときは修正モードへ移行する
                    if (IsNewData)
                    {
                        if (!string.IsNullOrEmpty(this.購買申請コード.DataSource.ToString()))
                        {
                            // Assuming 購買申請コード is a DataGridView
                            ((DataTable)this.購買申請コード.DataSource).AcceptChanges(); // Refresh the data

                            // If it's a different type of control or data structure, adjust accordingly.
                        }

                        コマンド新規.Enabled = true;
                        コマンド読込.Enabled = false;
                    }
                    確認_製造部ボタン.Enabled = true;

                    //版数のソース更新 改版時のため用
                    OriginalClass ofn = new OriginalClass();
                    setCombo = true;
                    ofn.SetComboBox(購買申請版数, " SELECT 購買申請版数 as Display,購買申請版数 as Value " +
                            "FROM T購買申請 " +
                            "WHERE (購買申請コード = '" + CurrentCode + "') " +
                            "ORDER BY 購買申請版数 DESC");
                    購買申請版数.SelectedIndex = 0;
                    setCombo = false;
                }
                else
                {
                    // 登録失敗
                    コマンド登録.Enabled = true;
                    MessageBox.Show("登録できませんでした。", "登録コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"エラーが発生しました。{Environment.NewLine}登録できません。{Environment.NewLine}{ex.Message}", "登録コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                fn.WaitForm.Close();
            }
        }


        private bool SaveData(string code, int edition)
        {
            try
            {
                Control objControl1 = null;
                Control objControl2 = null;
                Control objControl3 = null;
                object varSaved1 = null;
                object varSaved2 = null;
                object varSaved3 = null;
                bool result = false;

                Connect();
                DateTime dtmNow = FunctionClass.GetServerDate(cn);

                objControl1 = this.登録日時;
                objControl2 = this.登録者コード;
                objControl3 = this.登録者名;
                //登録前の値を退避しておく
                varSaved1 = objControl1.Text;
                varSaved2 = objControl2.Text;
                varSaved3 = objControl3.Text;
                //登録情報設定
                objControl1.Text = dtmNow.ToString();
                objControl2.Text = CommonConstants.LoginUserCode;
                objControl3.Text = CommonConstants.LoginUserFullName;

                //登録処理
                if (RegTrans(code, edition))
                {
                    result = true;
                }
                else
                {
                    objControl1.Text = varSaved1.ToString();
                    objControl2.Text = varSaved2.ToString();
                    objControl3.Text = varSaved3.ToString();
                }
                return result;
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_SaveData - " + ex.Message);
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

                // 変更があるときは登録確認を行う
                if (this.IsChanged)
                {
                    var intRes = MessageBox.Show("変更内容を登録しますか？", "新規コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    switch (intRes)
                    {
                        case DialogResult.Yes:
                            //if (!ErrCheck()) return;
                            // 登録処理
                            if (!SaveData(this.CurrentCode, this.CurrentEdition))
                            {
                                MessageBox.Show("登録できませんでした。", "新規コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            FunctionClass fn = new FunctionClass();

            try
            {
                DialogResult intRes;

                if (ActiveControl == this.コマンド読込)
                {
                    GetNextControl(コマンド読込, false).Focus();
                }

                if (IsChanged)
                {
                    // データに変更があった場合の処理
                    intRes = MessageBox.Show("変更内容を登録しますか？", "購買申請", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    switch (intRes)
                    {
                        case DialogResult.Yes:
                            // 登録処理
                            fn.DoWait("登録しています...");
                            if (!SaveData(CurrentCode, CurrentEdition))
                            {
                                MessageBox.Show("エラーのため登録できませんでした。", "修正コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            break;

                        case DialogResult.No:
                            // 新規モードのときに登録しない場合はコードを戻す
                            if (IsNewData && !string.IsNullOrEmpty(CurrentCode) && CurrentEdition == 1)
                            {
                                if (!FunctionClass.ReturnCode(cn, CurrentCode))
                                {
                                    MessageBox.Show($"エラーのためコードは破棄されました。{Environment.NewLine}購買申請コード　：　{CurrentCode}",
                                        "購買申請", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    if (IsNewData && !string.IsNullOrEmpty(CurrentCode) && CurrentEdition == 1)
                    {
                        if (!FunctionClass.ReturnCode(cn, CurrentCode))
                        {
                            MessageBox.Show($"エラーのためコードは破棄されました。{Environment.NewLine}購買申請コード　：　{CurrentCode}",
                                "購買申請", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }

                // 修正モードへ移行する
                if (!GoModifyMode()) return;
            }
            catch (Exception ex)
            {
                Debug.Print($"{this.Name}_コマンド読込_Click - {ex.GetType().Name} : {ex.Message}");

                if (MessageBox.Show($"エラーが発生しました。{Environment.NewLine}管理者に連絡してください。{Environment.NewLine}強制終了しますか？",
                    "読込コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    fn.WaitForm.Close();
                }
            }
        }


        private bool ErrCheck()
        {
            //入力確認    
            return true;
        }

        private void コマンド複写_Click(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();

            if (this.ActiveControl == this.コマンド複写)
                this.コマンド複写.Focus();

            fn.DoWait("複写しています...");

            Connect();

            // 複写に成功すればインターフェースを更新する
            if (CopyData(FunctionClass.採番(cn, "PUR"), 1))
            {
                // 申請日を設定（■専務の要望により、申請日の初期値は改版時と異なる）
                //this.申請日.Text = DateTime.Today.ToString();
                this.申請日.Text = FunctionClass.GetServerDate(cn).ToString("yyyy/MM/dd");

                // ヘッダ部制御
                FunctionClass.LockData(this, false);

                // 個別にReadOnlyを設定
                LockCtl();

                // データが変更されたことにする
                ChangedData(true);

                // コントロールの有効/無効を設定
                this.申請者コード.Focus();
                this.購買申請コード.Enabled = false;
                this.コマンド新規.Enabled = false;
                this.コマンド読込.Enabled = true;
                this.コマンド改版.Enabled = false;
                this.コマンド承認.Enabled = false;
                this.コマンド確定.Enabled = false;
                this.確認_製造部ボタン.Enabled = false;
                this.終了ボタン.Enabled = false;
            }
            else
            {
                Debug.Print($"{this.Name}_コマンド複写_Click - {GetType().Name}");

                MessageBox.Show("エラーが発生しました。" + Environment.NewLine + "複写できません。",
                    "複写コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
                goto Bye_コマンド複写_Click;
            }

        Bye_コマンド複写_Click:
            fn.WaitForm.Close();
            return;
        }

        private bool CopyData(string codeString, int editionNumber)
        {
            try
            {
                // キー情報を設定
                購買申請コード.Text = codeString;
                購買申請版数.Text = editionNumber.ToString();

                // 初期値を設定
                申請者コード.Text = CommonConstants.LoginUserCode;
                確認日時3.Text = null;
                確認者コード3.Text = null;
                確認日時1.Text = null;
                確認者コード1.Text = null;
                MountChipLotCreated.Text = "0";
                完了.Text = null;
                終了日時.Text = null;
                終了者コード.Text = null;
                登録日時.Text = null;
                登録者コード.Text = null;
                登録者名.Text = null;
                無効日時.Text = null;
                無効者コード.Text = null;
                // 他の値も同様に設定

                return true;
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_CopyData - " + ex.Message);
                return false;
            }
        }

        private bool DeleteData(SqlConnection targetConnection, string code, int edition)
        {
            bool success = false;
            SqlTransaction transaction = null;

            try
            {
                targetConnection.Open();
                transaction = targetConnection.BeginTransaction();

                string strKey;
                string strSQL;

                strKey = $"購買申請コード='{code}' AND 購買申請版数={edition}";
                strSQL = $"UPDATE T購買申請 SET 無効日時=GETDATE(),無効者コード='{CommonConstants.LoginUserCode}' WHERE {strKey}";

                using (SqlCommand command = new SqlCommand(strSQL, targetConnection, transaction))
                {
                    command.ExecuteNonQuery();
                }

                transaction.Commit();
                success = true;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }

                Console.WriteLine($"Error in DeleteData: {ex.Message}");
            }
            finally
            {
                if (targetConnection.State == System.Data.ConnectionState.Open)
                {
                    targetConnection.Close();
                }
            }

            return success;
        }

        private string GetSeriesCode(string productCode)
        {
            string seriesCode = "";

            Connect();

            try
            {
                string strSQL = $"SELECT シリーズコード FROM M商品 WHERE 商品コード='{productCode}'";

                using (SqlCommand command = new SqlCommand(strSQL, cn))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            seriesCode = reader["シリーズコード"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetSeriesCode: {ex.Message}");
            }
            finally
            {
                if (cn.State == System.Data.ConnectionState.Open)
                {
                    cn.Close();
                }
            }

            return seriesCode;
        }

        private bool RecalcStock(string seriesCode, DateTime beginDate)
        {
            bool success = false;
            Connect();
            SqlCommand objCommand = new SqlCommand("SPシリーズ在庫再計算", cn);
            objCommand.CommandType = CommandType.StoredProcedure;

            try
            {
                objCommand.Parameters.AddWithValue("@SeriesCode", seriesCode);
                objCommand.Parameters.AddWithValue("@BeginDate", beginDate);

                objCommand.ExecuteNonQuery();
                success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in RecalcStock: {ex.Message}");
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }

                objCommand.Dispose();
            }

            return success;
        }

        private bool RegTrans(string code, int edition)
        {
            bool success = false;
            SqlTransaction transaction = null;

            Connect();

            try
            {
                transaction = cn.BeginTransaction();

                // Header registration
                if (!SaveHeader(this, code, edition, transaction))
                {
                    transaction.Rollback();
                    goto Bye_RegTrans;
                }

                // Detail registration
                // Uncomment and modify the following lines if needed
                //string strKey = $"購買申請コード='{code}' AND 購買申請版数={edition}";
                //if (!SaveDetails(SubForm, "T購買申請明細", strKey, objConnection, transaction))
                //{
                //    transaction.Rollback();
                //    goto Bye_RegTrans;
                //}

                // Series stock recalculation
                // Uncomment and modify the following lines if needed
                //if (DoRecalc)
                //{
                //    if (!RecalcStock(GetSeriesCode(Me.商品コード.Value), GetServerDate(objConnection), objConnection, transaction))
                //    {
                //        transaction.Rollback();
                //        goto Bye_RegTrans;
                //    }
                //}

                // Transaction completed
                transaction.Commit();
                success = true;
            }
            catch (Exception ex)
            {
                if (transaction != null)
                {
                    transaction.Rollback();
                }

                Console.WriteLine($"Error in RegTrans: {ex.Message}");
            }
            finally
            {
                if (cn.State == System.Data.ConnectionState.Open)
                {
                    cn.Close();
                }
            }

        Bye_RegTrans:
            return success;
        }


        private bool SaveHeader(Form inputForm, string code, int edition, SqlTransaction transaction)
        {
            try
            {
                //ItemRevisionは既定値で1を入れているため
                if (string.IsNullOrEmpty(ItemRevision.Text))
                    ItemRevision.Text = "1";

                string strKey = $"購買申請コード='{code}' AND 購買申請版数={edition}";

                DataUpdater.UpdateOrInsertDataFrom(this, cn, "T購買申請", strKey, "購買申請コード", transaction, "購買申請版数");
            }
            catch (Exception ex)
            {
                Debug.Print("SaveHeader - " + ex.Message);
                return false;
            }

            return true;
        }

        private void コマンド削除_Click(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();

            try
            {
                object varSaved1 = null; // 無効日時保存用（エラー発生時の対策）
                object varSaved2 = null; // 無効者コード保存用（エラー発生時の対策）
                string strMsg;
                DialogResult intRes;

                Connect();

                // 削除確認
                if (IsDeleted)
                {
                    strMsg =
                        "購買申請コード　：　" + CurrentCode + Environment.NewLine + Environment.NewLine +
                        "この購買申請データを復元しますか？";
                }
                else
                {
                    strMsg =
                        "購買申請コード　：　" + CurrentCode + Environment.NewLine + Environment.NewLine +
                        "この購買申請データを削除しますか？" + Environment.NewLine +
                        "削除後、再度削除コマンドを実行することで復元することができます。";
                }
                intRes = MessageBox.Show(strMsg, "削除コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (intRes == DialogResult.No) return;

                // シリーズ在庫締め処理後、削除すると在庫が狂うことを警告する
                if (!IsDeleted && IsCompleted)
                {
                    if (MessageBox.Show("シリーズ在庫の締め処理が完了しています｡ " + Environment.NewLine +
                                        "削除すると関係するシリーズの在庫数が狂います。" + Environment.NewLine + Environment.NewLine +
                                        "続行しますか？", "削除コマンド", MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }

                // 削除処理開始
                if (IsDeleted)
                {
                    fn.DoWait("復元しています...");
                }
                else
                {
                    fn.DoWait("削除しています...");
                }

                // 登録前の削除情報を保存しておく
                varSaved1 = 無効日時.Text;
                varSaved2 = 無効者コード.Text;

                // 削除情報を設定する
                if (IsDeleted)
                {
                    無効日時.Text = null;
                    無効者コード.Text = null;
                }
                else
                {
                    無効日時.Text = FunctionClass.GetServerDate(cn).ToString();
                    無効者コード.Text = CommonConstants.LoginUserCode;
                }

                if (RegTrans(CurrentCode, CurrentEdition))
                {
                    確認_製造部ボタン.Enabled = (!IsCompleted) && (!this.IsEnd) && (!IsDeleted);
                    終了ボタン.Enabled = IsApproved && (!IsDeleted);

                    // 削除されたときは新規データへ移行
                    if (IsDeleted)
                    {
                        if (!GoNewMode())
                        {
                            MessageBox.Show("エラーのため新規モードへ移行できません。" + Environment.NewLine +
                                            "[" + this.Text + "]を終了します。", "削除コマンド",
                                            MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            this.Close();
                        }
                    }
                }
                else
                {
                    // 登録失敗
                    無効日時.Text = varSaved1.ToString();
                    無効者コード.Text = varSaved2.ToString();
                    if (IsDeleted)
                    {
                        MessageBox.Show("復元できませんでした。", "削除コマンド",
                                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    else
                    {
                        MessageBox.Show("削除できませんでした。", "削除コマンド",
                                        MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print(Name + "_コマンド削除_Click - " + ex.GetType().Name + " : " + ex.Message);
                MessageBox.Show("エラーが発生しました。" + Environment.NewLine +
                                "システム管理者へ連絡してください。", "削除コマンド",
                                MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                申請日.Focus();

                // マウントラインで生産計画されているときは改版できないことにする
                // ■■■シリーズ在庫締め処理後でも改版したいときは、このブロックをコメント化する
                if (IsScheduled)
                {
                    MessageBox.Show("この購買申請は生産計画されているため、改版できません｡ " +
                                    "改版するには生産ラインの管理者へ問い合わせてください。",
                                    "改版コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // 確認
                if (MessageBox.Show("改版後、旧版に戻すことはできません。" + Environment.NewLine +
                                    "改版しますか？", "改版コマンド", MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }

                // シリーズ在庫締め処理後、改版すると在庫が狂うことを警告する
                if (IsCompleted)
                {
                    if (MessageBox.Show("シリーズ在庫の締め処理が完了しています｡ " + Environment.NewLine +
                                        "改版すると関係するシリーズの在庫数が狂います。" + Environment.NewLine +
                                        "続行しますか？", "改版コマンド", MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }

                // 承認のチェック■を初期化
                this.確認_営業部.Text = null;
                this.確認_製造部.Text = null;
                this.終了入力.Text = null;

                // 複写に成功すればインターフェースを更新する
                if (CopyData(CurrentCode, (int)CurrentEdition + 1))
                {

                    // 変更された
                    ChangedData(true);
                    // ヘッダ部制御
                    FunctionClass.LockData(this, false);
                    // 個別にReadOnlyを設定
                    LockCtl();
                    申請日.Focus();
                    購買申請コード.Enabled = false;
                    購買申請版数.Enabled = false;

                    コマンド新規.Enabled = false;
                    コマンド読込.Enabled = true;
                    コマンド改版.Enabled = false;
                }

            }
            catch (Exception ex)
            {
                Debug.Print($"{this.Name}_コマンド改版_Click - {ex.GetType().Name} : {ex.Message}");
                MessageBox.Show("エラーが発生しました。", "改版コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private bool SetDeleted(string codeString, bool DoRestore)
        {

            object varSaved1 = null;
            object varSaved2 = null;
            object varSaved3 = null;
            DateTime dtmNow = FunctionClass.GetServerDate(cn);
            string strKey;
            string strUpdate;
            string employeename;
            employeename = FunctionClass.EmployeeName(cn, CommonConstants.LoginUserCode);

            bool isDeleted = false;

            varSaved1 = 完了.Text;
            varSaved3 = 削除.Text;
            if (DoRestore)
            {
                完了.Text = null;
                削除.Text = null;
            }
            else
            {
                完了.Text = dtmNow.ToString();
                削除.Text = employeename;
            }

            Connect();
            using (SqlTransaction transaction = cn.BeginTransaction())
            {
                try
                {
                    string strwhere = " 仕入先コード='" + this.購買申請コード.Text + "' and Revision=" + this.購買申請版数.Text;

                    if (!DataUpdater.UpdateOrInsertDataFrom(this, cn, "M仕入先", strwhere, "仕入先コード", transaction))
                    {
                        完了.Text = varSaved1.ToString();
                        削除.Text = varSaved3.ToString();
                        transaction.Rollback();
                        return false;
                    }


                    transaction.Commit(); // トランザクション完了   

                    return true;
                }
                catch (Exception ex)
                {
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }
                    return false;
                }
            }
        }



        private void コマンド承認_Click(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();

            try
            {
                object var1 = null;
                object var2 = null;
                bool blnApproved;//確認前、承認状態であったか
                // 確認_営業部用
                object var3 = null;


                // 登録データのエラーチェック
                if (IsErrorData("購買申請コード", "購買申請版数"))
                    return;

                // シリーズ在庫締め処理後、承認を取り消すと在庫が狂うことを警告する
                if (IsApproved && IsCompleted)
                {
                    DialogResult result = MessageBox.Show("シリーズ在庫の締め処理が完了しています。" + Environment.NewLine +
                                                          "承認を取り消すと関係するシリーズの在庫数が狂います。" + Environment.NewLine + Environment.NewLine +
                                                          "続行しますか？", "承認コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.No)
                        return;
                }

                // 認証する

                using (var targetform = new F_認証())
                {
                    targetform.args = "007";
                    targetform.MdiParent = this.MdiParent;
                    targetform.FormClosed += (s, args) => { this.Enabled = true; };
                    this.Enabled = false;

                    targetform.Show();


                    if (string.IsNullOrEmpty(CommonConstants.strCertificateCode))
                    {
                        MessageBox.Show("認証に失敗しました。" + Environment.NewLine + "承認はできません。", "承認コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                fn.DoWait("しばらくお待ちください...");

                var1 = 確認日時3.Text;
                var2 = 確認者コード3.Text;
                var3 = 確認_営業部.Text;
                blnApproved = IsApproved;

                Connect();

                // 承認情報を設定する
                if (IsApproved)
                {
                    確認日時3.Text = null;
                    確認者コード3.Text = null;
                    確認_営業部.Text = null;
                }
                else
                {
                    確認日時3.Text = FunctionClass.GetServerDate(cn).ToString();
                    確認者コード3.Text = CommonConstants.LoginUserCode;
                    確認_営業部.Text = "■";
                }

                // 表示内容で登録する
                // このとき、シリーズ対応かつ承認状態に変化があれば在庫の再計算を実行する
                // if (RegTrans(CurrentCode, IsSeriesOn && (blnApproved != IsApproved)))
                if (RegTrans(CurrentCode, CurrentEdition))
                {
                    FunctionClass.LockData(this, IsDecided);
                    // 個別にReadOnlyを設定
                    LockCtl();
                    コマンド改版.Enabled = IsApproved;
                    終了ボタン.Enabled = IsApproved && (!IsDeleted);
                }
                else
                {
                    確認日時3.Text = var1.ToString();
                    確認者コード3.Text = var2.ToString();
                    確認_営業部.Text = var3.ToString();
                    MessageBox.Show("登録できませんでした。" + Environment.NewLine +
                                    "操作は取り消されました。", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Debug.Print(Name + "_コマンド承認_Click - " + ex.GetType().ToString() + " : " + ex.Message);
                MessageBox.Show("エラーが発生しました。", "承認コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (fn.WaitForm != null)
                    fn.WaitForm.Close();
            }

        Bye_コマンド承認_Click:
            return;
        }

        private void コマンド確定_Click(object sender, EventArgs e)
        {
            MessageBox.Show("このコマンドは使用できません。", "確定コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド終了_Click(object sender, EventArgs e)
        {
            Close(); // フォームを閉じる
        }

        private void Form_Unload(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (IsChanged)
                {
                    // データに変更があった場合の処理
                    DialogResult result = MessageBox.Show("変更内容を登録しますか？", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    switch (result)
                    {
                        case DialogResult.Yes:
                            // エラーチェック
                            // if (IsErrorData("購買申請コード"))
                            // {
                            //     e.Cancel = true;
                            //     return;
                            // }

                            // 登録処理
                            FunctionClass fn = new FunctionClass();
                            fn.DoWait("登録しています...");

                            if (!SaveData(CurrentCode, CurrentEdition))
                            {
                                if (MessageBox.Show("エラーのため登録できませんでした。" + Environment.NewLine +
                                                    "強制終了しますか？", "エラー", MessageBoxButtons.YesNo,
                                                    MessageBoxIcon.Question) == DialogResult.No)
                                {
                                    e.Cancel = true;
                                    return;
                                }
                            }
                            break;

                        case DialogResult.No:
                            // 新規モードのときに登録しない場合はコードを戻す
                            if (IsNewData && CurrentCode != "" && CurrentEdition == 1)
                            {
                                if (!FunctionClass.ReturnCode(cn, CurrentCode))
                                {
                                    MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine +
                                                    "購買申請コード　：　" + CurrentCode, "警告", MessageBoxButtons.OK,
                                                    MessageBoxIcon.Exclamation);
                                }
                            }
                            break;

                        case DialogResult.Cancel:
                            e.Cancel = true;
                            break;
                    }
                }
                else
                {
                    // 新規モードのときに変更がない場合はコードを戻す
                    if (IsNewData && CurrentCode != "" && CurrentEdition == 1)
                    {
                        if (!FunctionClass.ReturnCode(cn, CurrentCode))
                        {
                            MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine +
                                            "購買申請コード　：　" + CurrentCode, "警告", MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation);
                        }
                    }
                }

                // フォームの情報を保存
                LocalSetting ls = new LocalSetting();
                ls.SavePlace(CommonConstants.LoginUserCode, this);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.GetType().Name} : {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void コマンド商品_Click(object sender, EventArgs e)
        {
            try
            {
                F_商品 targetform = new F_商品();
                if (this.ActiveControl != null && this.ActiveControl.Parent != null)
                {
                    int previousIndex = this.ActiveControl.Parent.Controls.GetChildIndex(this.ActiveControl) - 1;
                    if (previousIndex >= 0)
                    {
                        this.ActiveControl.Parent.Controls[previousIndex].Focus();
                    }
                }
                targetform.MdiParent = this.MdiParent;
                targetform.FormClosed += (s, args) => { this.Enabled = true; };
                this.Enabled = false;

                targetform.Show();

            }
            catch (Exception ex)
            {
                // Handle exceptions here
                Debug.WriteLine($"Error in コマンド商品_Click: {ex.Message}");
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
                    // 備考でEnter押下時、フォーカス移動しないで改行する

                    switch (ActiveControl.Name)
                    {
                        case "備考":
                        case "購買申請コード":


                            return;
                    }

                    SelectNextControl(ActiveControl, true, true, true, true);
                    e.Handled = true;
                    e.SuppressKeyPress = true;

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

                case Keys.F6:
                    if (コマンド商品.Enabled) コマンド商品_Click(sender, e);
                    break;

                case Keys.F9:
                    if (コマンド承認.Enabled) コマンド承認_Click(sender, e);
                    break;

                case Keys.F10:
                    if (コマンド確定.Enabled) コマンド確定_Click(sender, e);
                    break;

                case Keys.F11:
                    if (コマンド登録.Enabled)
                    {
                        コマンド登録.Focus();
                        コマンド登録_Click(sender, e);
                    }
                    break;

                case Keys.F12:
                    if (コマンド終了.Enabled)
                    {
                        コマンド終了.Focus();
                        コマンド終了_Click(sender, e);
                    }
                    break;
            }
        }

        private DataTable Get購買申請版数Data(string 購買申請コード)
        {
            DataTable 購買申請版数DataTable = new DataTable();

            try
            {
                Connect();
                string sqlQuery = $"SELECT 購買申請版数 as Display,購買申請版数 as Value FROM T購買申請 WHERE 購買申請コード = '{購買申請コード}' ORDER BY 購買申請版数 DESC";

                using (SqlCommand command = new SqlCommand(sqlQuery, cn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        adapter.Fill(購買申請版数DataTable);
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.Print($"Error in Get購買申請版数Data: - {ex.Message}");
                throw; // Rethrow the exception
            }

            return 購買申請版数DataTable;
        }

        private void UpdatedControl(Control controlObject)
        {
            try
            {
                string strSQL;
                string strName = controlObject.Name;
                object varValue = controlObject.Text;

                switch (strName)
                {
                    case "購買申請コード":
                        //版数のソース更新
                        OriginalClass ofn = new OriginalClass();

                        ofn.SetComboBox(購買申請版数, " SELECT 購買申請版数 as Display,購買申請版数 as Value " +
                                "FROM T購買申請 " +
                                "WHERE (購買申請コード = '" + CurrentCode + "') " +
                                "ORDER BY 購買申請版数 DESC");


                        //同一コードのデータ表示
                        strSQL = $"SELECT * FROM V購買申請 " +
                                 $"WHERE 購買申請コード='{this.CurrentCode}' AND 購買申請版数={this.CurrentEdition}";
                        LoadHeader(this, strSQL);

                        // インターフェースを制御する
                        FunctionClass.LockData(this, IsDecided || IsDeleted, "購買申請コード", "購買申請版数");
                        // 個別にReadOnlyを設定
                        LockCtl();


                        ChangedData(false);

                        コマンド複写.Enabled = true;
                        コマンド削除.Enabled = !IsCompleted;
                        コマンド改版.Enabled = IsApproved && !IsCompleted && !IsEnd && !IsDeleted;
                        コマンド承認.Enabled = !IsApproved && !IsDeleted;
                        確認_製造部ボタン.Enabled = !IsCompleted && !this.IsEnd && !IsDeleted;
                        終了ボタン.Enabled = IsApproved && !IsDeleted;
                        break;

                    case "購買申請版数":
                        //同一コードのデータ表示
                        strSQL = $"SELECT * FROM V購買申請 " +
                                 $"WHERE 購買申請コード='{CurrentCode}' AND 購買申請版数={CurrentEdition}";
                        LoadHeader(this, strSQL);

                        // インターフェースを制御する
                        FunctionClass.LockData(this, IsDecided || IsDeleted, "購買申請コード", "購買申請版数");
                        // 個別にReadOnlyを設定
                        LockCtl();

                        ChangedData(false);

                        コマンド複写.Enabled = true;
                        コマンド削除.Enabled = !IsCompleted;
                        コマンド改版.Enabled = IsApproved && !IsCompleted && !IsEnd && !IsDeleted;
                        コマンド承認.Enabled = !IsApproved && !IsDeleted;
                        確認_製造部ボタン.Enabled = !IsCompleted && !this.IsEnd && !IsDeleted;
                        終了ボタン.Enabled = IsApproved && !IsDeleted;
                        break;

                    case "申請者コード":
                        申請者名.Text = ((DataRowView)申請者コード.SelectedItem)?.Row.Field<String>("Display2")?.ToString();
                        break;

                    case "商品コード":
                        setProduct = true;
                        if (controlObject.Text != null)
                        {
                            商品名.Text = ((DataRowView)商品コード.SelectedItem)?.Row.Field<String>("Display2")?.ToString();
                        }
                        シリーズ名.Text = ((DataRowView)商品コード.SelectedItem)?.Row.Field<String>("Display3")?.ToString();
                        break;

                    case "商品名":
                        if (setProduct)
                        {
                            setProduct = false;
                        }
                        else
                        {
                            商品コード.Text = null;
                            シリーズ名.Text = null;
                        }

                        break;

                    case "IsManufacturing":
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_UpdatedControl - " + ex.Message);
            }
        }

        private bool LoadHeader(Form targetForm, string sourceSQL)
        {
            try
            {
                bool result = false;

                Connect();

                VariableSet.SetTable2Form(this, sourceSQL, cn);

                if (!string.IsNullOrEmpty(申請日.Text))
                {
                    if (DateTime.TryParse(this.申請日.Text, out DateTime tempDate))
                    {
                        申請日.Text = tempDate.ToString("yyyy/MM/dd");
                    }
                }

                if (!string.IsNullOrEmpty(材料単価.Text))
                {
                    decimal val = decimal.Parse(材料単価.Text);
                    材料単価.Text = val.ToString("N0");
                }

                if (!string.IsNullOrEmpty(確認者コード3.Text))
                {
                    確認_営業部.Text = "■";
                }

                if (!string.IsNullOrEmpty(MountChipLotCreated.Text) && MountChipLotCreated.Text != "0")
                {
                    Scheduled.Text = "■";
                }

                if (!string.IsNullOrEmpty(無効日時.Text))
                {
                    削除.Text = "■";
                }

                if (!string.IsNullOrEmpty(確認者コード1.Text))
                {
                    確認_製造部.Text = "■";
                }

                if (!string.IsNullOrEmpty(終了日時.Text))
                {
                    終了入力.Text = "■";
                }


                result = true;

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine("LoadHeader - " + ex.Message);
                throw; // You may want to handle or log the exception based on your application's requirements.
            }
        }

        private async void ロット番号1_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void ロット番号1_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 8);
            ChangedData(true);
        }

        private async void ロット番号2_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void ロット番号2_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 8);
            ChangedData(true);
        }

        private void 確認_製造部ボタン_Click(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();

            try
            {
                object var1 = null;
                object var2 = null;
                // 確認製造部用
                object var3 = null;
                bool blnApproved; // 確認前、承認状態であったか

                if (!IsApproved)
                {
                    MessageBox.Show("承認されていないため、確認処理を完了できません。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // 登録データのエラーチェック
                if (IsErrorData("購買申請コード", "購買申請版数"))
                    return;

                // 確認状態を取り消す時は事前にユーザーに確認する
                //if (this.確認者コード1.Text != null && !Convert.IsDBNull(this.確認者コード1.Text))
                if (!string.IsNullOrEmpty(this.確認者コード1.Text) && !Convert.IsDBNull(this.確認者コード1.Text))
                {
                    if (MessageBox.Show("製造部確認を取り消しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;
                }

                fn.DoWait("しばらくお待ちください...");

                var1 = this.確認日時1.Text;
                var2 = this.確認者コード1.Text;
                var3 = this.確認_製造部.Text;
                blnApproved = this.IsApproved;

                Connect();

                // 確認情報を設定する
                // if (Convert.IsDBNull(this.確認者コード1.Text))
                if (string.IsNullOrEmpty(this.確認者コード1.Text))
                {
                    this.確認日時1.Text = FunctionClass.GetServerDate(cn).ToString();
                    this.確認者コード1.Text = CommonConstants.LoginUserCode;
                    // 確認_製造部にチェック■を入れる
                    this.確認_製造部.Text = "■";
                }
                else
                {
                    this.確認日時1.Text = null;
                    this.確認者コード1.Text = null;
                    // 確認_製造部のチェックを外す
                    this.確認_製造部.Text = null;
                }

                // 表示内容で登録する
                // このとき、シリーズ対応かつ承認状態に変化があれば在庫の再計算を実行する
                if (RegTrans(this.CurrentCode, this.CurrentEdition))
                {
                    FunctionClass.LockData(this, this.IsDecided);
                    // 個別にReadOnlyを設定
                    LockCtl();
                    this.終了ボタン.Enabled = this.IsApproved && (!this.IsDeleted);
                }
                else
                {
                    this.確認日時1.Text = var1.ToString();
                    this.確認者コード1.Text = var2.ToString();
                    this.確認_製造部.Text = var3.ToString();
                    MessageBox.Show("登録できませんでした。" + Environment.NewLine + "操作は取り消されました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_確認_製造部ボタン_Click - " + ex.HResult + " : " + ex.Message);
                MessageBox.Show("エラーが発生しました。", "確認", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (fn.WaitForm != null)
                    fn.WaitForm.Close();
            }
        }

        private void 購買申請コード_Validated(object sender, EventArgs e)
        {

        }

        private void 購買申請コード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((ComboBox)sender), 11);
        }

        private void 購買申請コード_Enter(object sender, EventArgs e)
        {
            ////// ・Access
            //////If Me.購買申請コード.RowSource = "" Then
            //////    Me.購買申請コード.RowSource  = "SELECT 購買申請コード FROM T購買申請 ORDER BY 購買申請コード DESC"
            //////End If
        }

        private void 購買申請コード_KeyDown(object sender, KeyEventArgs e)
        {
            // 入力された値がエラー値の場合、textプロパティが設定できなくなるときの対処
            if (e.KeyCode == Keys.Return) // Enter キーが押されたとき
            {
                string strCode = 購買申請コード.Text;
                if (string.IsNullOrEmpty(strCode)) return;

                //strCode = strCode.PadLeft(8, '0'); // ゼロで桁を埋める例
                strCode = FunctionClass.FormatCode("PUR", strCode);
                if (strCode != 購買申請コード.Text)
                {
                    購買申請コード.Text = strCode;
                    SelectNextControl(ActiveControl, true, true, true, true);
                }
            }
        }

        private void 購買申請版数_Validated(object sender, EventArgs e)
        {

        }

        private void 購買納期_Validated(object sender, EventArgs e)
        {
          //  UpdatedControl(sender as Control);
        }

        private void 購買納期_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 10);
            ChangedData(true);
        }

        // 日付選択フォームへの参照を保持するための変数
        private F_カレンダー dateSelectionForm1 = new F_カレンダー();
        private F_カレンダー dateSelectionForm2 = new F_カレンダー();
        private F_カレンダー dateSelectionForm3 = new F_カレンダー();

        private void 購買納期選択ボタン_Click(object sender, EventArgs e)
        {


            if (!string.IsNullOrEmpty(購買納期.Text))
            {
                dateSelectionForm2.args = 購買納期.Text;
            }

            if (dateSelectionForm2.ShowDialog() == DialogResult.OK && !購買納期.ReadOnly && 購買納期.Enabled)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm2.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                購買納期.Text = selectedDate;
                購買納期.Focus();
            }
        }

        private void 材料単価_Validated(object sender, EventArgs e)
        {
           // UpdatedControl(sender as Control);
        }

        private void 材料単価_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 終了ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                object var1 = this.終了日時.Text;
                object var2 = this.終了者コード.Text;
                // 終了入力用
                object var3 = this.終了入力.Text;

                Connect();

                // 終了状態を取り消す時は事前にユーザーに確認する
                if (this.IsEnd)
                {
                    DialogResult result = MessageBox.Show("終了状態を取り消しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.No)
                        return;
                }

                // 終了情報を設定する
                if (this.IsEnd)
                {
                    this.終了日時.Text = null;
                    this.終了者コード.Text = null;
                    this.終了入力.Text = null;
                }
                else
                {
                    // 適切な GetServerDate メソッドと LoginUserCode メソッドを呼び出してください
                    this.終了日時.Text = FunctionClass.GetServerDate(cn).ToString(); // 適切なメソッドを呼び出す
                    this.終了者コード.Text = CommonConstants.LoginUserCode; // 適切なメソッドを呼び出す
                    this.終了入力.Text = "■";
                }

                // 適切な RegTrans メソッドを呼び出してください
                if (RegTrans(this.CurrentCode, this.CurrentEdition))
                {
                    this.コマンド改版.Enabled = this.IsApproved && (!this.IsCompleted) && (!this.IsEnd) && (!this.IsDeleted);
                    this.確認_製造部ボタン.Enabled = (!this.IsCompleted) && (!this.IsEnd) && (!this.IsDeleted);
                }
                else
                {
                    this.終了日時.Text = var1.ToString();
                    this.終了者コード.Text = var2.ToString();
                    this.終了入力.Text = var3.ToString();
                    MessageBox.Show("登録できませんでした。\n操作は取り消されました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 商品コード_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■入力は任意ですが、在庫管理を有効にするときは必ずシリーズが対応している商品を選択してください。　■商品名を任意に入力するときは空欄にしてください。";
        }

        private void 商品コード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 商品名_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■顧客用の名称を入力します。　■半角３０文字まで入力できます。";
        }

        private void 商品名_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 購買納期_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■購買する部品の納期を入力します。";
        }

        private void 購買納期_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 出荷予定日_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■製品の出荷予定日を入力します。";
        }

        private void 出荷予定日_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 備考_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■全角2000文字まで入力できます。";
        }

        private void 備考_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 購買申請コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (tmpshinseisha == 購買申請コード.Text) return;
            if (IsError(sender as Control, e.Cancel))
            {
                e.Cancel = true;
                購買申請コード.Text = tmpshinseisha;
            }
        }

        private void 購買申請版数_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //TextBox textBox = (TextBox)sender;

            //if (textBox.Modified == false) return;

           // if (IsError(sender as Control, false) == true) e.Cancel = true;
        }

        private void 申請日_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;
           // if (tmpshinsei == 申請日.Text) return;
            if (IsError(textBox, false) == true) e.Cancel = true;
        }

        private void 出荷予定日_Validated(object sender, EventArgs e)
        {
           // UpdatedControl(sender as Control);
        }

        private void 出荷予定日_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox, false) == true) e.Cancel = true;
        }

        private void 出荷予定日_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 10);
            ChangedData(true);
        }

        private void 出荷予定日_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            出荷予定日選択ボタン_Click(sender, e);
        }

        private void 出荷予定日選択ボタン_Click(object sender, EventArgs e)
        {


            if (!string.IsNullOrEmpty(出荷予定日.Text))
            {
                dateSelectionForm3.args = 出荷予定日.Text;
            }

            if (dateSelectionForm3.ShowDialog() == DialogResult.OK && !出荷予定日.ReadOnly && 出荷予定日.Enabled)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm3.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                出荷予定日.Text = selectedDate;
                出荷予定日.Focus();
            }
        }

        private void 商品コード_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 商品コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (IsError(sender as Control, e.Cancel)) e.Cancel = true;
        }

        private void 商品コード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((ComboBox)sender), 8);
            ChangedData(true);
        }

        private void 商品コード_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyCode == Keys.Return)
            {
                e.Handled = true;
                string strCode = 商品コード.Text.ToString();
                string formattedCode = strCode.Trim().PadLeft(8, '0');

                if (formattedCode != strCode || string.IsNullOrEmpty(strCode))
                {
                    商品コード.Text = formattedCode;
                }

            }
        }

        private void 商品名_Validated(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Modified == false) return;
            UpdatedControl(sender as Control);
        }

        private void 商品名_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Modified == false) return;

            if (IsError(textBox, false) == true) e.Cancel = true;
        }

        private void 商品名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 30);
            ChangedData(true);
        }

        private void 申請者コード_Validated(object sender, EventArgs e)
        {
            //UpdatedControl(sender as Control);
        }

        private void 申請者コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control, e.Cancel)) e.Cancel = true;
        }

        private void 申請者コード_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 申請者コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                ((ComboBox)sender).AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                ((ComboBox)sender).AutoCompleteSource = AutoCompleteSource.ListItems;
                e.Handled = true;
            }
        }

        private void 出荷予定日_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true;
                出荷予定日選択ボタン_Click(sender, e);
            }
        }

        private void 申請日_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 申請日_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 10);
            ChangedData(true);
        }

        private void 申請日_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            申請日選択ボタン_Click(sender, e);
        }

        private void 申請日_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true;
                申請日選択ボタン_Click(sender, e);
            }
        }

        private void 申請日選択ボタン_Click(object sender, EventArgs e)
        {


            if (!string.IsNullOrEmpty(申請日.Text))
            {
                dateSelectionForm1.args = 申請日.Text;
            }

            if (dateSelectionForm1.ShowDialog() == DialogResult.OK && !申請日.ReadOnly && 申請日.Enabled)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm1.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                申請日.Text = selectedDate;
                申請日.Focus();
            }
        }

        private void 数量_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 数量_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox, false) == true) e.Cancel = true;
        }

        private void 数量_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 11);
            ChangedData(true);
        }

        private void 備考_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 備考_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            //if (textBox.Modified == false) return;

            if (IsError(textBox, false) == true) e.Cancel = true;
        }

        private void 備考_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 4000);
            ChangedData(true);
        }

        private void ロット番号1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;
            if (IsError(textBox, false) == true) e.Cancel = true;
            if (string.IsNullOrEmpty(textBox.Text)) return;

            decimal value = decimal.Parse(textBox.Text);
            ロット番号1.Text = value.ToString("F2");
        }

        private void ロット番号2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;           
            if (IsError(textBox, false) == true) e.Cancel = true;
            if (string.IsNullOrEmpty(textBox.Text)) return;

            decimal value = decimal.Parse(textBox.Text);
            ロット番号2.Text = value.ToString("F2");
        }

        private void 材料単価_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox, false) == true) e.Cancel = true;
        }

        private void 購買納期_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            購買納期選択ボタン_Click(sender, e);
        }

        private void 購買納期_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                e.Handled = true;
                // 購買納期選択ボタンのクリックイベントを呼び出す
                購買納期選択ボタン_Click(sender, e);
            }
        }

        private void 購買納期_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox, false) == true) e.Cancel = true;
        }

        private void 申請者コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 34, 170 }, new string[] { "Display", "Display2" });
            申請者コード.Invalidate();
            申請者コード.DroppedDown = true;
        }

        private void 商品コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 100, 300, 300, 0 }, new string[] { "Display", "Display2", "Display3", "Display4" });
            商品コード.Invalidate();
            商品コード.DroppedDown = true;
        }

        private void 申請者コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            申請者名.Text = ((DataRowView)申請者コード.SelectedItem)?.Row.Field<String>("Display2")?.ToString();
            ChangedData(true);
        }

        private void 商品コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            商品名.Text = ((DataRowView)商品コード.SelectedItem)?.Row.Field<String>("Display2")?.ToString();
            シリーズ名.Text = ((DataRowView)商品コード.SelectedItem)?.Row.Field<String>("Display3")?.ToString();
            ChangedData(true);
        }

        private void 購買申請コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            try
            {
                if (購買申請コード.SelectedIndex == 0) return;

                UpdatedControl(購買申請コード);
            }
            catch (Exception ex)
            {
                Debug.Print($"{this.Name}_購買申請コード_AfterUpdate - {ex.HResult} : {ex.Message}");
                MessageBox.Show("コード設定時にエラーが発生しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                // 強制終了フラグを設定する
                Terminate = true;
            }
        }

        private void 購買申請版数_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            UpdatedControl(購買申請版数);
        }

        

        private void F_購買申請_Shown(object sender, EventArgs e)
        {
            if (購買申請コード.Enabled == false)
            {
                申請者コード.Focus();
            }
        }

        string tmpshinsei = "";
        private void 申請日_Enter(object sender, EventArgs e)
        {
            tmpshinsei = 申請日.Text;
        }

        string tmpshinseisha="";
        private void 申請者コード_Enter(object sender, EventArgs e)
        {
            tmpshinseisha = 申請者コード.Text;
        }
    }
}
