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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Data.Common;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using Microsoft.Identity.Client.NativeInterop;
using System.Runtime.ConstrainedExecution;

namespace u_net
{
    public partial class F_支払 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "支払";
        private int selected_frame = 0;
        int intWindowHeight;
        int intWindowWidth;
        public bool IsDirty = false;
        private bool setCombo = true;

        public F_支払()
        {
            this.Text = "支払";       // ウィンドウタイトルを設定
            this.MaximizeBox = false;  // 最大化ボタンを無効化
            this.MinimizeBox = false; //最小化ボタンを無効化

            InitializeComponent();

        }

        public string CurrentCode
        {
            get
            {
                return string.IsNullOrEmpty(支払コード.Text) ? "" : 支払コード.Text;
            }
        }

        public bool IsApproved
        {
            get
            {
                return !string.IsNullOrEmpty(承認日時.Text);

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

        public string SP支払年月入力
        {
            get
            {
                return "SELECT A.CloseMonth as Display,A.CloseMonth as Value FROM (SELECT STR({ fn YEAR(DATEADD(month,-12,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-12,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,-11,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-11,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,-10,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-10,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,-9,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-9,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,-8,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-8,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,-7,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-7,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,-6,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-6,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,-5,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-5,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,-4,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-4,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,-3,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-3,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,-2,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-2,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,-1,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-1,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(GETDATE()) }, 4, 0) + '/' + STR({ fn MONTH(GETDATE()) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,1,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,1,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,2,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,2,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,3,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,3,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,4,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,4,GETDATE())) }, 2, 0) AS CloseMonth " +
                    ") AS A LEFT OUTER JOIN(SELECT STR( YEAR(CloseMonth) ,4 ,0 ) + '/' + STR( MONTH(CloseMonth) ,2 ,0 ) AS CloseMonth " +
                    "FROM T振込繰越残高 GROUP BY CloseMonth) AS B ON A.CloseMonth = B.CloseMonth WHERE B.CloseMonth IS NULL ORDER BY A.CloseMonth";
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

        private void Form_Resize(object sender, EventArgs e)
        {
            try
            {
                this.支払明細1.Height += (this.Height - intWindowHeight);
                this.支払明細1.Width += (this.Width - intWindowWidth);
                intWindowHeight = this.Height;
                intWindowWidth = this.Width;

            }
            catch (Exception ex)
            {
                Debug.Print($"{nameof(Form_Resize)} - {ex.Message}");
            }
        }
        private void SetPayeeInfo(string payeeCode)
        {
            Connect();
            string strSQL = $"SELECT * FROM M仕入先 WHERE 仕入先コード = '{payeeCode}' AND 無効日時 IS NULL";

            using (SqlCommand command = new SqlCommand(strSQL, cn))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            this.支払先名.Text = $"{reader["仕入先名"]} {reader["仕入先名2"]}";
                            this.支払先担当者名.Text = reader["担当者名"].ToString();
                        }
                    }
                    else
                    {
                        this.支払先名.Text = null;
                        this.支払先担当者名.Text = null;
                    }
                }
            }
        }
        private bool SetRelay()
        {
            bool result = false;

            try
            {
                // 支払一覧（年間）フォームの状態をチェック
                if (Application.OpenForms["F_支払一覧_年間"] != null)
                {
                    //フォームが完成したらコメント外す
                    //F_支払一覧_年間 objForm1 = (F_支払一覧_年間)Application.OpenForms["F_支払一覧_年間"];

                    //if (objForm1.DataCount > 0)
                    //{
                    //    this.集計年月.Text = $"{objForm1.PayMonth.Year}/{objForm1.PayMonth.Month:D2}";
                    //    this.支払先コード.Text = objForm1.PayeeCode;
                    //    SetPayeeInfo(objForm1.PayeeCode);
                    //}
                }
                // 支払一覧（月間）フォームの状態をチェック
                else if (Application.OpenForms["F_支払一覧_月間"] != null)
                {
                          F_支払一覧_月間 objForm2 = (F_支払一覧_月間)Application.OpenForms["F_支払一覧_月間"];

                    //if (objForm2.DataCount > 0)
                    //{
                    //    this.集計年月.Text = $"{objForm2.dtm集計年月.Year}/{objForm2.dtm集計年月.Month:D2}";
                    //    this.支払先コード.Text = objForm2.PayeeCode;
                    //    SetPayeeInfo(objForm2.PayeeCode);
                    //}
                }

                result = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{this.GetType().Name}_SetRelay - {ex.Message}");
            }

            return result;
        }
        private bool IsErrorData(string? exFieldName1 = null, string? exFieldName2 = null)
        {
            try
            {
                foreach (Control control in Controls)
                {
                    if ((control is System.Windows.Forms.TextBox || control is System.Windows.Forms.ComboBox) && control.Visible)
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


                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{GetType().Name}_IsErrorData - {ex.GetType().Name} : {ex.Message}");
                return true;
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
                    case "集計年月":
                    case "支払年月":
                        if (varValue == null || string.IsNullOrEmpty(varValue.ToString()))
                        {
                            MessageBox.Show(controlObject.Name + "を入力してください。", "警告",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;

                        }
                        if (!DateTime.TryParse(varValue.ToString(), out DateTime dateValue))
                        {
                            MessageBox.Show("日付を入力してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;

                    case "支払先コード":
                    case "振込指定":
                        if (varValue == null || string.IsNullOrEmpty(varValue.ToString()))
                        {
                            MessageBox.Show(controlObject.Name + "を入力してください。", "警告",
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

        public void 支払コードcmb()
        {
            setCombo = true;
            //
            string str = CurrentCode;
            OriginalClass ofn = new OriginalClass();
            ofn.SetComboBox(支払コード, "SELECT T支払.支払コード as Display,T支払.支払コード  as Value FROM T支払 INNER JOIN T支払明細" +
                " ON T支払.支払コード = T支払明細.支払コード GROUP BY T支払.支払コード ORDER BY T支払.支払コード DESC");
            支払コード.Text = str;
            setCombo = false;
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

            支払コードcmb();
            setCombo = true; //コンボのセットでsetcomboをfalseにしているので
            OriginalClass ofn = new OriginalClass();

            string cmbsql = "select 集計年月 AS Display,集計年月 AS Value from(" +
                "SELECT STR({ fn YEAR(DATEADD(month,-1,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-1,GETDATE())) }, 2, 0) AS 集計年月 " +
                "UNION ALL SELECT STR({ fn YEAR(GETDATE()) }, 4, 0) + '/' + STR({ fn MONTH(GETDATE()) }, 2, 0) AS 集計年月 " +
                "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,1,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,1,GETDATE())) }, 2, 0) AS 集計年月 " +
                "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,2,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,2,GETDATE())) }, 2, 0) AS 集計年月) as T";
            ofn.SetComboBox(集計年月, cmbsql);

            ofn.SetComboBox(支払年月, SP支払年月入力);


            this.振込指定.DataSource = new KeyValuePair<int, String>[] {
               new KeyValuePair<int, String>(1, "振り込む"),
                new KeyValuePair<int, String>(2, "振り込まない"),
            };
            this.振込指定.DisplayMember = "Value";
            this.振込指定.ValueMember = "Key";

            MyApi myapi = new MyApi();
            int xSize, ySize, intpixel, twipperdot;

            //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
            intpixel = myapi.GetLogPixel();
            twipperdot = myapi.GetTwipPerDot(intpixel);

            try
            {
                this.SuspendLayout();

                intWindowHeight = this.Height;
                intWindowWidth = this.Width;


                if (string.IsNullOrEmpty(args))
                {
                    if (GoNewMode())
                    {
                        SetRelay();
                    }
                    else
                    {
                        throw new Exception("初期化に失敗しました。");
                    }
                }
                else
                {
                    if (!GoModifyMode())
                    {
                        throw new Exception("初期化に失敗しました。");
                    }

                    this.支払コード.Text = args;
                    支払コード.Focus();

                    UpdatedControl(this.支払コード);


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
                setCombo = false;
                this.ResumeLayout();
                if (fn.WaitForm != null) fn.WaitForm.Close();
            }
        }

        public bool LoadHeader()
        {
            try
            {
                Connect();
                string strSQL;
                strSQL = "SELECT * FROM V支払読込 WHERE 支払コード='" + CurrentCode + "'";

                if (!VariableSet.SetTable2Form(this, strSQL, cn, "集計年月", "支払年月")) return false;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("読み込み時エラーです" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }
        private bool GoModifyMode()
        {
            try
            {
                setCombo = true;
                Connect();
                bool success = false;
                string strSQL = "";

                // 各コントロール値をクリア
                VariableSet.SetControls(this);

                // 明細部の初期化
                strSQL = "SELECT * FROM T支払明細 WHERE 支払コード='" + this.CurrentCode +
                    "'  ORDER BY 明細番号";
                VariableSet.SetTable2Details(支払明細1.Detail, strSQL, cn);

                ChangedData(false);
                //this.支払コード.Enabled = true;
                this.支払コード.Focus();

                LockData(this, true, "支払コード");

                this.コマンド新規.Enabled = true;
                this.コマンド修正.Enabled = false;
                setCombo = false;
                success = true;
                return success;
            }
            catch (Exception ex)
            {
                Console.WriteLine(this.Name + "_GoModifyMode - " + ex.Message);
                return false;
            }
        }

        private bool GoNewMode()
        {
            try
            {
                setCombo = true;
                // 各コントロール値を初期化
                VariableSet.SetControls(this);
                Connect();

                支払コード.Text = FunctionClass.採番(cn, "PAY").ToString();
                AdjustPayable.Checked = false;
                // 明細部の初期化
                string strSQL = "SELECT * FROM T支払明細 WHERE 支払コード='" + this.CurrentCode +
                    "'  ORDER BY 明細番号";
                VariableSet.SetTable2Details(支払明細1.Detail, strSQL, cn);

                // ヘッダ部動作制御
                FunctionClass.LockData(this, false);
                支払先コード.Focus();
                支払コード.Enabled = false;

                コマンド新規.Enabled = false;
                コマンド修正.Enabled = true;
                コマンド複写.Enabled = false;
                コマンド削除.Enabled = false;
                コマンド登録.Enabled = false;

                // 明細部動作制御
                支払明細1.Detail.AllowUserToDeleteRows = true;
                支払明細1.Detail.ReadOnly = false;
                支払明細1.Detail.AllowUserToAddRows = true;
                setCombo = false;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{this.Name}_GoNewMode - {ex.GetType().Name} : {ex.Message}");
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
                Connect();

                // 変更されていないとき
                if (!IsDirty)
                {
                    // 新規モードで且つコードが取得済みのときはコードを戻す
                    if (this.IsNewData && !string.IsNullOrEmpty(this.CurrentCode))
                    {
                        // 採番された番号を戻す
                        if (!ReturnCode(cn, this.CurrentCode))
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

                        if (IsErrorData("支払コード", "支払版数"))
                        {
                            e.Cancel = true;
                            return;
                        }

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
                        if (this.IsNewData && !string.IsNullOrEmpty(this.CurrentCode))
                        {
                            // 採番された番号を戻す
                            if (!ReturnCode(cn, this.CurrentCode))
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

            }
            catch (Exception ex)
            {
                Debug.Print(Name + "_Unload - " + ex.Message);
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private string MonthAdd(long number, string targetMonth)
        {
            DateTime dtmNew = DateTime.ParseExact(targetMonth + "/1", "yyyy/MM/d", System.Globalization.CultureInfo.InvariantCulture);

            dtmNew = dtmNew.AddMonths((int)number);

            return dtmNew.ToString("yyyy/MM");
        }

        private void コマンド登録_Click(object sender, EventArgs e)
        {

            if (this.ActiveControl == this.コマンド登録)
            {
                GetNextControl(コマンド登録, false).Focus();
            }

            // エラーチェック
            if (IsErrorData("支払コード")) return;


            FunctionClass fn = new FunctionClass();
            fn.DoWait("登録しています...");


            // 登録処理
            if (SaveData())
            {
                支払コードcmb();

                // 登録に成功した
                ChangedData(false);

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

        private bool ErrCheck()
        {
            //入力確認    
            //if (!FunctionClass.IsError(this.部品コード)) return false;
            //if (!FunctionClass.IsError(this.版数)) return false;
            return true;
        }

        public void ChangedData(bool isChanged)
        {
            if (ActiveControl == null) return;

            IsDirty = isChanged;

            if (isChanged)
            {
                this.Text = BASE_CAPTION + "*";
            }
            else
            {
                this.Text = BASE_CAPTION;
            }

            // コードにフォーカスがある状態でサブフォームから呼び出されたときの対処
            if (this.ActiveControl == this.支払明細1)
            {
                this.集計年月.Focus();
            }
            if (this.ActiveControl == this.支払コード)
            {
                this.集計年月.Focus();
            }
            this.支払コード.Enabled = !isChanged;

            this.コマンド複写.Enabled = !isChanged; 
            this.コマンド削除.Enabled = !isChanged;
            this.コマンド登録.Enabled = isChanged;

            if (isChanged)
            {
                コマンド承認.Enabled = false;
                コマンド確定.Enabled = true;
            }
        }

        private bool CopyData(string codeString)
        {
            try
            {
                // 明細部の初期設定
                DataTable dataTable = (DataTable)支払明細1.Detail.DataSource;
                if (dataTable != null)
                {
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        // 行の状態が Deleted の時は次の行へ
                        if (dataTable.Rows[i].RowState == DataRowState.Deleted)
                        {
                            continue;
                        }
                        dataTable.Rows[i]["支払コード"] = codeString;

                    }
                    支払明細1.Detail.DataSource = dataTable; // 更新した DataTable を再セット
                }


                支払コード.Text = codeString;

                削除.Text = null;
                作成日時.Text = null;
                作成者コード.Text = null;
                作成者名.Text = null;
                更新日時.Text = string.Empty;
                更新者コード.Text = string.Empty;
                更新者名.Text = null;

                チェック();

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーが発生しました。", "改版", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
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

                // 変更がある
                if (IsDirty)
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

        Bye_コマンド新規_Click:
            return;
        }

        private bool SaveData()
        {

            Connect();

            {
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

                    if (IsNewData)
                    {
                        objControl1 = this.作成日時;
                        objControl2 = this.作成者コード;
                        objControl3 = this.作成者名;
                        varSaved1 = objControl1.Text;
                        varSaved2 = objControl2.Text;
                        varSaved3 = objControl3.Text;
                        objControl1.Text = dteNow.ToString(); // ここでDateTimeをstringに変換して設定
                        objControl2.Text = CommonConstants.LoginUserCode;
                        objControl3.Text = CommonConstants.LoginUserFullName;
                    }

                    objControl4 = this.更新日時;
                    objControl5 = this.更新者コード;
                    objControl6 = this.更新者名;

                    varSaved4 = objControl4.Text;
                    varSaved5 = objControl5.Text;
                    varSaved6 = objControl6.Text;

                    objControl4.Text = dteNow.ToString(); // ここでDateTimeをstringに変換して設定
                    objControl5.Text = CommonConstants.LoginUserCode;
                    objControl6.Text = CommonConstants.LoginUserFullName;


                    if (RegTrans(CurrentCode, cn))
                    {
                        return true;
                    }
                    else
                    {
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

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in SaveData: " + ex.Message);
                    return false;
                }
            }
        }
        private bool RegTrans(string codeString, SqlConnection cn, bool UpdatePreEdition = false)
        {
            try
            {
                string strKey = "";

                SqlTransaction transaction = cn.BeginTransaction();

                // ヘッダ部の登録
                if (!SaveHeader(codeString, cn, transaction))
                {
                    transaction.Rollback(); // 変更をキャンセル
                    return false;
                }

                // 明細部の登録                    

                if (!SaveDetails(codeString, cn, transaction))
                {
                    transaction.Rollback(); // 変更をキャンセル
                    return false;
                }

                transaction.Commit(); // トランザクション完了

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
        private bool SaveHeader(string codeString, SqlConnection cn, SqlTransaction transaction)
        {
            try
            {
                string strwhere = " 支払コード='" + codeString + "'";


                if (!DataUpdater.UpdateOrInsertDataFrom(this, cn, "T支払", strwhere, "支払コード", transaction, "集計年月", "支払年月"))
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
        public bool SaveDetails(string codeString, SqlConnection cn, SqlTransaction transaction)
        {
            try
            {
                string strwhere = $"支払コード= '{codeString}'";
                //明細部の登録
                if (!DataUpdater.UpdateOrInsertDetails(this.支払明細1.Detail, cn, "T支払明細", strwhere, "支払コード", transaction))
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


        private void UpdatedControl(Control controlObject)
        {
            FunctionClass fn = new FunctionClass();
            try
            {
                string strSQL;

                switch (controlObject.Name)
                {
                    case "支払コード":
                        Connect();

                        fn.DoWait("読み込んでいます...");

                        // ヘッダ部の表示
                        if (!LoadHeader()) throw new Exception("初期化に失敗しました。");

                        // 明細部の表示
                        strSQL = $"SELECT * FROM T支払明細 WHERE 支払コード= '{this.CurrentCode}' ORDER BY 明細番号";

                        //明細表示
                        if (!VariableSet.SetTable2Details(支払明細1.Detail, strSQL, cn))
                            throw new Exception("初期化に失敗しました。");

                        // 動作を制御する
                        LockData(this, IsDecided || IsDeleted, "支払コード");

                        支払明細1.Detail.AllowUserToAddRows = !this.IsDecided && !this.IsDeleted;
                        支払明細1.Detail.AllowUserToDeleteRows = !this.IsDecided && !this.IsDeleted;
                        支払明細1.Detail.ReadOnly = this.IsDecided || this.IsDeleted;
                        this.コマンド複写.Enabled = !this.IsDirty;
                        this.コマンド削除.Enabled = !this.IsNewData;

                        this.コマンド承認.Enabled = this.IsDecided && (!this.IsDeleted);
                        this.コマンド確定.Enabled = (!this.IsApproved) && (!this.IsDeleted);
                        ChangedData(false);
                        チェック();
                        break;

                    case "支払先コード":
                        SetPayeeInfo(支払先コード.Text);
                        break;
                    case "集計年月":
                        setCombo = true;
                        if (controlObject.Text != null)
                        {
                            DateTime dateValue;
                            if (DateTime.TryParse(controlObject.Text, out dateValue))

                                this.集計年月.Text = $"{dateValue.Year}/{dateValue.Month:D2}";
                        }

                        if (this.支払年月.Text == null)
                        {
                            this.支払年月.Text = MonthAdd(1, controlObject.Text.ToString());
                        }
                        setCombo = false;
                        break;
                    case "支払年月":
                        if (controlObject.Text != null)
                        {
                            DateTime dateValue;
                            if (DateTime.TryParse(controlObject.Text, out dateValue))

                                this.支払年月.Text = $"{dateValue.Year}/{dateValue.Month:D2}";
                        }

                        if (this.集計年月.Text == null)
                        {
                            this.集計年月.Text = MonthAdd(1, controlObject.Text.ToString());
                        }

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

        private void コマンド承認_Click(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();
            try
            {
                object var1;
                object var2;
                object var3;
                string strAppCode1 = ""; // 承認者1のユーザーコード
                string strAppCode2 = "";     // 承認者2のユーザーコード
                string strAppCode3 = "";
                string strCerCode = "";

                // 認証処理
                strAppCode1 = USER_CODE_MANAGE;      // 承認者1を指定する（管理部長）
                strAppCode2 = USER_CODE_GA;          // 承認者2を指定する（業務チーム）

                F_認証 fm = new F_認証();

                fm.ShowDialog();
                if (string.IsNullOrEmpty(strCertificateCode))
                {
                    MessageBox.Show("認証に失敗しました。" + Environment.NewLine + "承認できません。。", "承認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Application.DoEvents();


                strCerCode = strCertificateCode;

                if (!(strCerCode == strAppCode1 || strCerCode == strAppCode2))
                {
                    MessageBox.Show("指定されたユーザーには承認を実行する権限がありません。\n処理は取り消されました。", "承認コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                fn.DoWait("承認しています...");

                var1 = this.承認日時.Text;
                var2 = this.承認者コード.Text;
                var3 = this.承認者名.Text;

                Connect();

                if (IsApproved)
                {
                    this.承認日時.Text = null;
                    this.承認者コード.Text = null;
                    this.承認者名.Text = null;
                }
                else
                {
                    this.承認日時.Text = GetServerDate(cn).ToString();
                    this.承認者コード.Text = strCerCode;
                    this.承認者名.Text = EmployeeName(cn, strCerCode);
                }

                if (RegTrans(this.CurrentCode, cn))
                {
                    ChangedData(false);
                    this.コマンド確定.Enabled = !this.IsApproved;
                }
                else
                {
                    this.承認日時.Text = var1.ToString();
                    this.承認者コード.Text = var2.ToString();
                    this.承認者名.Text = var3.ToString();
                    // ロールバック処理をこちらに追加してください。
                    MessageBox.Show("登録できませんでした。", "承認コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                チェック();
            }
            catch (Exception ex)
            {
                Debug.Print($"{this.Name}_コマンド承認_Click - {ex.Message}");
            }
            finally
            {
                if (fn.WaitForm != null) fn.WaitForm.Close();
            }
        }

        private void コマンド確定_Click(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();
            try
            {
                // エラーチェック
                if (IsErrorData("支払コード")) return;

                fn.DoWait("確定しています...");

                // 登録前の確定日を保存しておく
                object varSaved1 = this.確定日時.Text;
                object varSaved2 = this.確定者コード.Text;

                Connect();
                // 確定情報を設定する
                if (IsDecided)
                {
                    this.確定日時.Text = null;
                    this.確定者コード.Text = null;
                }
                else
                {
                    this.確定日時.Text = GetServerDate(cn).ToString();
                    this.確定者コード.Text = LoginUserCode;
                }

                // 登録する
                if (RegTrans(this.CurrentCode, cn))
                {
                    支払コードcmb();

                    LockData(this, IsDecided, "支払コード");

                    if (IsNewData)
                    {
                        this.コマンド新規.Enabled = true;
                        this.コマンド修正.Enabled = false;
                    }

                    this.コマンド承認.Enabled = IsDecided;
                    支払明細1.Detail.AllowUserToAddRows = !this.IsDecided;
                    支払明細1.Detail.AllowUserToDeleteRows = !this.IsDecided;
                    支払明細1.Detail.ReadOnly = this.IsDecided;
                    ChangedData(false);
                    チェック();
                }
                else
                {
                    this.確定日時.Text = varSaved1.ToString();
                    this.確定者コード.Text = varSaved2.ToString();
                    MessageBox.Show("登録できませんでした。", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"{this.Name}_コマンド確定_Click - {ex.Message}");
            }
            finally
            {
                if (fn.WaitForm != null) fn.WaitForm.Close();
            }
        }


        private F_検索 SearchForm;

        private void コマンド削除_Click(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();
            try
            {
                object varSaved1 = null; // 無効日時保存用
                object varSaved2 = null; // 無効者コード保存用
                string strDelUserCode; // 削除可能ユーザーのユーザーコード
                DialogResult intRes;


                intRes = MessageBox.Show("支払コード　：　" + this.CurrentCode + "\n\nこの支払データを削除します。" +
                    "\n削除後、再度削除コマンドを実行することにより、元に戻すことができます。\n\n削除しますか？", "削除コマンド",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (intRes == DialogResult.No) return;

                if (this.IsDeleted)
                {
                    strDelUserCode = this.無効者コード.Text.ToString();
                }
                else if (this.IsApproved)
                {
                    strDelUserCode = this.承認者コード.Text.ToString();
                }
                else if (this.IsDecided)
                {
                    strDelUserCode = this.確定者コード.Text.ToString();
                }
                else
                {
                    strDelUserCode = this.更新者コード.Text.ToString();
                }

                if (LoginUserCode != strDelUserCode)
                {
                    F_認証 fm = new F_認証();
                    fm.args = strDelUserCode;
                    fm.ShowDialog();
                    if (string.IsNullOrEmpty(strCertificateCode))
                    {
                        MessageBox.Show("承認に失敗しました。" + Environment.NewLine + "承認できません。", "承認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                }

                fn.DoWait("削除しています...");

                varSaved1 = this.無効日時.Text;
                varSaved2 = this.無効者コード.Text;
                Connect();

                if (this.IsDeleted)
                {
                    this.無効日時.Text = null;
                    this.無効者コード.Text = null;
                }
                else
                {

                    this.無効日時.Text = GetServerDate(cn).ToString();
                    this.無効者コード.Text = strDelUserCode;
                }

                if (RegTrans(this.CurrentCode, cn))
                {
                    支払コードcmb();

                    LockData(this, this.IsDecided || this.IsDeleted, "支払コード");
                    支払明細1.Detail.AllowUserToAddRows = !this.IsDecided && !IsDeleted;
                    支払明細1.Detail.AllowUserToDeleteRows = !this.IsDecided && !IsDeleted;
                    支払明細1.Detail.ReadOnly = this.IsDecided || IsDeleted;

                    チェック();
                }
                else
                {
                    this.無効日時.Text = varSaved1.ToString();
                    this.無効者コード.Text = varSaved2.ToString();
                    MessageBox.Show("登録できませんでした。", "支払", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(this.Name + "_コマンド削除_Click - " + ex.Message);
            }
            finally
            {
                if (fn.WaitForm != null) fn.WaitForm.Close();
            }
        }

        private void コマンド複写_Click(object sender, EventArgs e)
        {
            try
            {
                ChangedData(true);

                Connect();

                string code = FunctionClass.採番(cn, "PAY");
                if (CopyData(code))
                {
                    ChangedData(true);
                    FunctionClass.LockData(this, false);
                    this.集計年月.Focus();

                    this.コマンド新規.Enabled = false;
                    this.コマンド修正.Enabled = true;

                    // 明細部制御
                    支払明細1.Detail.AllowUserToAddRows = true;
                    支払明細1.Detail.AllowUserToDeleteRows = true;
                    支払明細1.Detail.ReadOnly = false; //readonlyなのでaccessと真偽が逆になる   
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
        private void コマンド修正_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult intRes;

                if (!IsDirty)
                {
                    if (this.IsNewData && !string.IsNullOrEmpty(this.CurrentCode))
                    {
                        if (!ReturnCode(cn, this.CurrentCode))
                        {
                            MessageBox.Show($"エラーのためコードは破棄されました。\n\n支払コード　：　{this.CurrentCode}",
                                "修正コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }

                    if (!GoModifyMode()) throw new Exception();
                    goto Bye_コマンド修正_Click;
                }

                intRes = MessageBox.Show("変更内容を登録しますか？", "修正コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                switch (intRes)
                {
                    case DialogResult.Yes:
                        if (IsErrorData("製品コード", "")) return;
                        if (!SaveData())
                        {
                            MessageBox.Show("エラーのため登録できません。", "修正コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        break;

                    case DialogResult.No:
                        if (this.IsNewData && !string.IsNullOrEmpty(this.CurrentCode))
                        {
                            if (!ReturnCode(cn, this.CurrentCode))
                            {
                                MessageBox.Show($"エラーのためコードは破棄されました。\n\n支払コード　：　{this.CurrentCode}",
                                    "修正コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                        break;

                    case DialogResult.Cancel:
                        return;
                }

                if (!GoModifyMode()) throw new Exception();

            }
            catch (Exception ex)
            {
                Debug.Print($"{this.Name}_コマンド修正_Click - {ex.Message}");
                if (MessageBox.Show("エラーが発生しました。\n管理者に連絡してください。\n\n強制終了しますか？",
                    "修正コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    this.Close();
                }
                else
                {
                    goto Bye_コマンド修正_Click;
                }
            }

        Bye_コマンド修正_Click:
            return;

        }
        private void コマンド終了_Click(object sender, EventArgs e)
        {
            Close(); // フォームを閉じる
        }
        //private void UpdatePurGrid()
        //{
        //■現在未使用
        //登録後の処理として、購買フォームが開いている場合はグリッドを更新する
        //    try
        //    {
        //        // 購買フォームが開いているかを確認
        //        if (Application.OpenForms["購買"] == null)
        //        {
        //            return;
        //        }

        //        // 購買フォームのグリッドを取得
        //        Form frmPurchase = Application.OpenForms["購買"];
        //        MSHierarchicalFlexGridLib.MSHFlexGrid obj1 = (MSHierarchicalFlexGridLib.MSHFlexGrid)frmPurchase.Controls["objGrid"];

        //        if (obj1 != null)
        //        {
        //            int currentRow = obj1.row;
        //            string supplierCode = OriginalClass.Nz(仕入先1コード.Text,null);
        //            string supplierName = OriginalClass.Nz(Supplier1Name.Text, null);
        //            string productName = OriginalClass.Nz(品名.Text, null);
        //            string modelNumber = OriginalClass.Nz(型番.Text, null);
        //            double unitPrice = OriginalClass.Nz(仕入先1単価.Text, null);

        //            if (obj1.TextMatrix[currentRow, 5] != supplierCode)
        //            {
        //                obj1.TextMatrix[currentRow, 5] = supplierCode;
        //                obj1.TextMatrix[currentRow, 6] = supplierName;
        //                frmPurchase.GetType().GetMethod("ChangedControl").Invoke(frmPurchase, new object[] { true });
        //            }

        //            obj1.TextMatrix[currentRow, 8] = productName;
        //            obj1.TextMatrix[currentRow, 9] = modelNumber;
        //            obj1.TextMatrix[currentRow, 13] = unitPrice.ToString("###,###,##0.00");

        //            frmPurchase.GetType().GetMethod("CalcAmount").Invoke(frmPurchase, new object[] { currentRow });
        //            frmPurchase.GetType().GetProperty("bleGridUpdated").SetValue(frmPurchase, true, null);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.Print(Name + "_UpdatedPurGrid - " + ex.Message);
        //    }
        //}

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
                    // 複数行入力可能な項目はEnterでフォーカス移動させない
                    switch (this.ActiveControl.Name)
                    {
                        case "備考":
                            //case "メモ":
                            return;
                    }
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
                //case Keys.F5:
                //    if (コマンドユニット.Enabled) コマンドユニット_Click(sender, e);
                //    break;
                //case Keys.F6:
                //    if (コマンドユニット表.Enabled) コマンドユニット表_Click(sender, e);
                //    break;
                //case Keys.F7:
                //    if (コマンド廃止.Enabled) コマンド廃止_Click(sender, e);
                //    break;
                //case Keys.F8:
                //    if (コマンドツール.Enabled) コマンドツール_Click(sender, e);
                //    break;
                //case Keys.F9:
                //    if (コマンド承認.Enabled) コマンド承認_Click(sender, e);
                //    break;
                //case Keys.F10:
                //    if (コマンド確定.Enabled) コマンド確定_Click(sender, e);
                //    break;
                case Keys.F11:
                    if (コマンド登録.Enabled) コマンド登録_Click(sender, e);
                    break;
                case Keys.F12:
                    if (コマンド終了.Enabled) コマンド終了_Click(sender, e);
                    break;
            }
        }

        private void 集計年月_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■支払一覧に反映されるされる集計月を入力します。";
        }

        private void 集計年月_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 支払年月_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■振込一覧に反映される支払月を入力します。";
        }

        private void 支払年月_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 振込指定_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■振込一覧に反映される支払月を入力します。";
        }

        private void 振込指定_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 備考_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角１００文字まで入力できます。";
        }

        private void 備考_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 支払コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            UpdatedControl(支払コード);
        }


        private void 支払先コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (this.支払先コード.Modified == false) return;
            if (IsError(sender as Control, false) == true) e.Cancel = true;

        }

        private void 支払先コード_Validated(object sender, EventArgs e)
        {
            if (this.支払先コード.Modified == false) return;
            UpdatedControl(支払先コード);
        }

        private void 支払先コード_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 支払先検索ボタン_Click(object sender, EventArgs e)
        {
            F_検索 SearchForm = new F_検索();
            SearchForm.FilterName = "支払先名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                string SelectedCode = SearchForm.SelectedCode;
                支払先コード.Text = SelectedCode;
                UpdatedControl(支払先コード);
            }
        }

        private void 支払先コード_KeyDown(object sender, KeyEventArgs e)
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
                }
            }
        }

        private void 支払先コード_DoubleClick(object sender, EventArgs e)
        {
            支払先検索ボタン_Click(sender, e);
        }
        private void 支払先コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Space:
                    e.Handled = true;
                    支払先検索ボタン_Click(sender, e);
                    break;
            }
        }

        private void 支払先参照ボタン_Click(object sender, EventArgs e)
        {
            F_仕入先 fm = new F_仕入先();
            fm.args = 支払先コード.Text;
            fm.ShowDialog();
        }

        private void 支払年月_Validated(object sender, EventArgs e)
        {
            UpdatedControl(支払年月);
        }

        private void 支払年月_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control, false) == true) e.Cancel = true;
        }

        private void 支払年月_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            FunctionClass.LimitText(支払年月, 7);
            ChangedData(true);
        }

        private void 集計年月_Validated(object sender, EventArgs e)
        {
            UpdatedControl(集計年月);
        }

        private void 集計年月_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control, false) == true) e.Cancel = true;
        }

        private void 集計年月_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            FunctionClass.LimitText(集計年月, 7);
            ChangedData(true);
        }

        private void 振込指定_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control, false) == true) e.Cancel = true;
        }

        private void 振込指定_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            ChangedData(true);
        }


        private void 備考_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            FunctionClass.LimitText(集計年月, 200);
            ChangedData(true);
        }

        private void AdjustPayable_CheckedChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            ChangedData(true);
        }
    }
}
