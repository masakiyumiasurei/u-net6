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
using GrapeCity.Win.MultiRow;

namespace u_net
{



    public partial class F_入庫 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "入庫";
        private int selected_frame = 0;





        public F_入庫()
        {
            this.Text = "入庫";       // ウィンドウタイトルを設定
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

        public string SupplierCode
        {
            get
            {
                return Nz(仕入先コード.Text);
            }
        }

        // 現在の入庫コードを取得するプロパティ
        public string CurrentCode
        {
            get
            {
                return Nz(入庫コード.Text);
            }
        }

        // 現在のデータを登録したユーザーコードを取得するプロパティ
        public string InputUserCode
        {
            get
            {
                return 登録者コード.Text;
            }
        }

        // 現在のデータが編集されているかどうかを取得するプロパティ
        public bool IsChanged
        {
            get
            {
                return コマンド登録.Enabled;
            }
        }

        // 現在のデータが棚卸処理されているかどうかを取得するプロパティ
        public bool IsCompleted
        {
            get
            {
                return !IsNull(棚卸コード.Text);
            }
        }

        // 現在のデータが確定されているかどうかを取得するプロパティ
        public bool IsDecided
        {
            get
            {
                return !IsNull(確定日時.Text);
            }
        }

        // 現在のデータが削除されているかどうかを取得するプロパティ
        public bool IsDeleted
        {
            get
            {
                return !IsNull(無効日時.Text);
            }
        }

        // 現在のデータが新規データかどうかを取得するプロパティ
        public bool IsNewData
        {
            get
            {
                return !コマンド新規.Enabled;
            }
        }

        // Nz関数の代用
        private string Nz(object value)
        {
            return value == null ? "" : value.ToString();
        }

        // IsNull関数の代用
        private bool IsNull(object value)
        {
            return value == null || Convert.IsDBNull(value);
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
            string LoginUserCode = CommonConstants.LoginUserCode;//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(LoginUserCode, this);

            MyApi myapi = new MyApi();
            int xSize, ySize, intpixel, twipperdot;

            //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
            intpixel = myapi.GetLogPixel();
            twipperdot = myapi.GetTwipPerDot(intpixel);




            OriginalClass ofn = new OriginalClass();

            //入庫明細用
            ofn.SetComboBox(買掛区分コード設定, "SELECT 買掛区分 as Display,買掛区分コード as Display2, 買掛明細コード as Display3 , 買掛区分コード as Value FROM V買掛区分");
            買掛区分コード設定.DrawMode = DrawMode.OwnerDrawFixed;

            ofn.SetComboBox(入庫コード, " SELECT 入庫コード as Display, 入庫コード as Value FROM T入庫 WHERE(発注コード IS NOT NULL) ORDER BY 入庫コード DESC");

            ofn.SetComboBox(入庫者コード, " SELECT 社員コード AS Display, 氏名 AS Display2, 社員コード as Value FROM M社員 WHERE(退社 IS NULL) AND(部 <> N'社長') AND(ふりがな <> N'ん') ORDER BY ふりがな");
            入庫者コード.DrawMode = DrawMode.OwnerDrawFixed;


            ofn.SetComboBox(発注コード, " SELECT 発注コード as Display,発注版数 as Display2, Format(発注日,'yyyy/MM/dd') as Display3, 仕入先名 as Display4, 仕入先担当者名 as Display5, 発注コード as Value FROM V入庫_発注コード選択 ORDER BY 発注コード");
            発注コード.DrawMode = DrawMode.OwnerDrawFixed;

            ofn.SetComboBox(集計年月, " SELECT 集計年月 as Display, 集計年月 as Value FROM V集計年月");

            Connect();

            using (SqlCommand cmd = new SqlCommand("SP支払年月入力", cn))
            {
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                SqlDataReader reader = cmd.ExecuteReader();

                // レコードセットを設定
                DataTable dataTable = new DataTable();
                dataTable.Load(reader);


                支払年月.DisplayMember = "支払年月";
                支払年月.ValueMember = "支払年月";
                支払年月.DataSource = dataTable;



            }



            try
            {
                this.SuspendLayout();

                int intWindowHeight = this.Height;
                int intWindowWidth = this.Width;


                Connect();

                if (string.IsNullOrEmpty(args))
                {
                    if (FunctionClass.IsInventory(cn))
                    {
                        MessageBox.Show("現在棚卸中です。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    // 新規モードへ移行
                    if (!GoNewMode())
                    {

                        MessageBox.Show("入庫は使用できません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (!SetRelay())
                    {
                        MessageBox.Show("入庫は使用できません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
                else
                {
                    if (!GoModifyMode())
                    {
                        MessageBox.Show("入庫は使用できません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                    入庫コード.Text = args;
                    UpdatedControl(入庫コード);
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



        private bool SetRelay()
        {
            bool result = false;

            F_発注管理? f_発注管理 = Application.OpenForms.OfType<F_発注管理>().FirstOrDefault();

            if (f_発注管理 != null)
            {
                発注コード.Text = f_発注管理.CurrentCode;
            }


            return true;
        }
        public bool GoNewMode()
        {
            try
            {
                bool success = false;
                string strSQL = "";

                Connect();


                // 明細部の入庫数量をアンロック
                //SubForm.入庫数量.Locked = false;

                // ヘッダ部の初期化
                VariableSet.SetControls(this);
                this.入庫コード.Text = FunctionClass.採番(cn, "STR");
                this.入庫日.Text = DateTime.Now.ToString("yyyy/MM/dd");
                this.入庫者コード.Text = CommonConstants.LoginUserCode;
                this.入庫者名.Text = FunctionClass.EmployeeName(cn, CommonConstants.LoginUserCode);
                DateTime dat1 = DateTime.Parse(入庫日.Text);
                SetDefaultValue();

                // 明細部の初期化
                strSQL = "SELECT * FROM T入庫明細 WHERE 入庫コード='" + this.CurrentCode + "' ORDER BY 明細番号";
                VariableSet.SetTable2Details(入庫明細1.Detail, strSQL, cn);

                // ヘッダ部動作制御
                FunctionClass.LockData(this, false);
                this.発注コード.Focus();
                this.入庫コード.Enabled = false;
                this.コマンド新規.Enabled = false;
                this.コマンド修正.Enabled = true;
                this.コマンド複写.Enabled = false;
                this.コマンド削除.Enabled = false;
                this.コマンド承認.Enabled = false;
                this.コマンド確定.Enabled = false;
                this.コマンド登録.Enabled = false;

                // 明細部動作制御
                //SubForm.AllowAdditions = true;
                //SubForm.AllowDeletions = true;
                //SubForm.AllowEdits = true;

                success = true;
                return success;
            }
            catch (Exception ex)
            {
                Console.WriteLine(this.Name + "_GoNewMode - " + ex.Message);
                return false;
            }
        }


        private void SetDefaultValue()
        {
            Connect();

            // 入庫日が設定されていない場合は何もしない
            if (IsNull(this.入庫日.Text))
            {
                this.集計年月.Text = null;
                this.支払年月.Text = null;
                return;
            }

            // 入庫日に基づき集計年月と支払年月を設定する
            DateTime date1 = Convert.ToDateTime(this.入庫日.Text);

            // 適用する消費税率を入力する（■入庫日＝検収日としている）
            this.TaxRate.Text = FunctionClass.GetTaxRate(cn, date1).ToString("0.##");

            // GetAddupMonth および GetPayDay 関数の実装が提供されていないため、コメントアウトしています
            /*
            string strDefaultAddup = GetAddupMonth(date1);
            date1 = GetPayDay(date1);
            string strDefaultPay = $"{date1.Year}/{date1.Month:D2}";
            this.集計年月.Text = strDefaultAddup;
            this.支払年月.Text = strDefaultPay;
            */
        }

        private bool GoModifyMode()
        {
            try
            {
                bool success = false;
                string strSQL = "";

                // 各コントロール値をクリア
                VariableSet.SetControls(this);

                // 明細部の初期化
                strSQL = "SELECT * FROM T入庫明細 WHERE 入庫コード='" + this.CurrentCode + "' ORDER BY 明細番号";
                VariableSet.SetTable2Details(入庫明細1.Detail, strSQL, cn);

                // rsWork.Requery がコメントアウトされているため、必要に応じて適切な実装を行う

                // 入庫コードが使用可能になるまで待つ
                this.入庫コード.Enabled = true;
                this.入庫コード.Focus();

                // 入庫コードコントロールが使用可能になってから LockData を呼び出す
                FunctionClass.LockData(this, true, "入庫コード");

                // ボタンの状態を設定
                this.コマンド新規.Enabled = true;
                this.コマンド修正.Enabled = false;
                this.コマンド複写.Enabled = false;
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

        private void Form_Unload(object sender, FormClosingEventArgs e)
        {

            string LoginUserCode = CommonConstants.LoginUserCode;//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            LocalSetting test = new LocalSetting();
            test.SavePlace(LoginUserCode, this);

            Connect();

            try
            {
                DialogResult intRes;

                if (!IsChanged)
                {
                    // 新規モードでかつコードが取得済みのときはコードを戻す
                    if (IsNewData && CurrentCode != "")
                    {
                        // 採番された番号を戻す
                        if (!FunctionClass.ReturnCode(cn, CurrentCode))
                        {
                            MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine + Environment.NewLine +
                                "入庫コード　：　" + CurrentCode, "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    goto Bye_Form_Unload;
                }

                // 修正されているときは登録確認を行う
                intRes = MessageBox.Show("変更内容を登録しますか？", this.Name, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (intRes)
                {
                    case DialogResult.Yes:
                        // エラーチェック
                        foreach (Control control in Controls)
                        {
                            if (control is TextBox || control is ComboBox || control is CheckBox)
                            {
                                if (IsError(control))
                                {
                                    return;
                                }
                            }
                        }

                        // 登録処理
                        if (!SaveData())
                        {
                            if (MessageBox.Show("エラーのため登録できませんでした。" + Environment.NewLine +
                                "強制終了しますか？", "Question", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                e.Cancel = true;
                                return;
                            }
                        }
                        break;
                    case DialogResult.No:
                        // 新規モードでかつコードが取得済みのときはコードを戻す
                        if (IsNewData && CurrentCode != "")
                        {
                            // 採番された番号を戻す
                            if (!FunctionClass.ReturnCode(cn, CurrentCode))
                            {
                                MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine + Environment.NewLine +
                                    "入庫コード　：　" + CurrentCode, "Information", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                        break;
                    case DialogResult.Cancel:
                        e.Cancel = true;
                        return;
                }

            Bye_Form_Unload:
                // 部品選択フォームが開かれていれば閉じる
                foreach (Form openForm in Application.OpenForms)
                {
                    if (openForm.Name == "F_部品選択")
                    {
                        openForm.Close();
                        break;
                    }
                }

                return;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{this.Name}_Form_Unload - {ex.Message}");
                MessageBox.Show("予期しないエラーが発生しました。" + Environment.NewLine +
                    "強制終了します。" + Environment.NewLine + Environment.NewLine +
                    $"{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);


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
                case Keys.F1:
                    if (コマンド新規.Enabled) コマンド新規_Click(sender, e);
                    break;

                case Keys.F2:
                    if (コマンド修正.Enabled) コマンド修正_Click(sender, e);
                    break;

                case Keys.F3:
                    if (コマンド複写.Enabled) コマンド複写_Click(sender, e);
                    break;

                case Keys.F4:
                    if (コマンド削除.Enabled) コマンド削除_Click(sender, e);
                    break;

                case Keys.F5:
                    if (コマンド発注.Enabled) コマンド発注_Click(sender, e);
                    break;

                case Keys.F6:
                    if (コマンド仕入先.Enabled) コマンド仕入先_Click(sender, e);
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



        private void コマンド新規_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult intRes;

                Cursor.Current = Cursors.WaitCursor;

                Connect();

                // 変更があるときは登録確認を行う
                if (IsChanged)
                {
                    intRes = MessageBox.Show("変更内容を登録しますか？", "新規コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    switch (intRes)
                    {
                        case DialogResult.Yes:
                            // エラーチェック
                            foreach (Control control in Controls)
                            {
                                if (control is TextBox || control is ComboBox || control is CheckBox)
                                {
                                    if (IsError(control))
                                    {
                                        goto Bye_コマンド新規_Click;
                                    }
                                }
                            }

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

                // 棚卸中かどうかを確認する
                if (FunctionClass.IsInventory(cn))
                {
                    MessageBox.Show("現在棚卸中です。", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    goto Bye_コマンド新規_Click;
                }

                // 新規モードへ移行する
                if (!GoNewMode())
                    goto Err_コマンド新規_Click;

                Bye_コマンド新規_Click:
                Cursor.Current = Cursors.Default;
                return;

            Err_コマンド新規_Click:
                MessageBox.Show("エラーのため新規モードへ移行できません。", "新規コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{this.Name}_コマンド新規_Click - {ex.Message}");
                MessageBox.Show("エラーのため新規モードへ移行できません。", "新規コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        private bool SaveData()
        {

            Connect();
            SqlTransaction transaction = cn.BeginTransaction();
            {
                try
                {

                    DateTime dteNow = DateTime.Now;
                    Control objControl1 = null;
                    Control objControl2 = null;
                    Control objControl3 = null;
                    object varSaved1 = null;
                    object varSaved2 = null;
                    object varSaved3 = null;


                    if (IsNewData)
                    {
                        objControl1 = this.登録日時;
                        objControl2 = this.登録者コード;
                        objControl3 = this.登録者名;
                        varSaved1 = objControl1.Text;
                        varSaved2 = objControl2.Text;
                        varSaved3 = objControl3.Text;
                        objControl1.Text = dteNow.ToString(); // ここでDateTimeをstringに変換して設定
                        objControl2.Text = CommonConstants.LoginUserCode;
                        objControl3.Text = CommonConstants.LoginUserFullName;
                    }




                    string strwhere = " 入庫コード='" + this.入庫コード.Text + "'";

                    if (!RegTrans(CurrentCode))
                    {


                        if (IsNewData)
                        {
                            objControl1.Text = varSaved1.ToString();
                            objControl2.Text = varSaved2.ToString();
                            objControl3.Text = varSaved3.ToString();

                        }

                        return false;
                    }

                    // トランザクションをコミット
                    transaction.Commit();


                    入庫コード.Enabled = true;

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
                    Console.WriteLine("Error in SaveData: " + ex.Message);
                    return false;
                }
            }
        }


        private bool RegTrans(string codeString)
        {
            Connect();
            SqlTransaction transaction = cn.BeginTransaction();
            {

                try
                {

                    string strwhere = "入庫コード='" + this.入庫コード.Text + "'";
                    // ヘッダ部の登録
                    if (!DataUpdater.UpdateOrInsertDataFrom(this, cn, "T入庫", strwhere, "入庫コード", transaction))
                    {
                        transaction.Rollback();  // 変更をキャンセル
                        return false;
                    }

                    // 明細部の登録
                    if (!DataUpdater.UpdateOrInsertDetails(this.入庫明細1.Detail, cn, "T入庫明細", strwhere, "入庫コード", transaction))
                    {
                        transaction.Rollback();  // 変更をキャンセル
                        return false;
                    }

                    // 入庫データ登録後に部品の在庫を更新する
                    // ここでいう在庫とはシステムが管理している実在庫のことである
                    if (!UpdateStock())
                    {
                        transaction.Rollback();  // 変更をキャンセル
                        return false;
                    }

                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    Debug.Print($"{Name}_RegTrans - {ex.GetType().ToString()} : {ex.Message}");
                    transaction.Rollback();
                    return false;

                }
            }
        }

        private bool UpdateStock()
        {
            try
            {
                Connect();
                using (SqlCommand cmd = new SqlCommand("SP部品在庫数量更新", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.Print($"{Name}_UpdateStock - {ex.GetType().ToString()} : {ex.Message}");
                return false;
            }
        }


        private bool IsError(Control controlObject)
        {
            try
            {

                Connect();

                object varValue = controlObject.Text; // Valueプロパティの代わりにTextプロパティを使用
                switch (controlObject.Name)
                {
                    case "入庫日":
                        if (string.IsNullOrEmpty(varValue?.ToString()))
                        {
                            MessageBox.Show(controlObject.Name + " を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        if (!DateTime.TryParse(varValue.ToString(), out DateTime inputDate))
                        {
                            MessageBox.Show("日付を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        if (DateTime.Now < inputDate)
                        {
                            MessageBox.Show("未来の日付は入力できません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        break;
                    case "入庫者コード":
                        if (string.IsNullOrEmpty(varValue?.ToString()))
                        {
                            MessageBox.Show(controlObject.Name + " を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        break;
                    case "発注コード":
                        if (IsNewData)
                        {
                            if (string.IsNullOrEmpty((発注コード.SelectedItem as DataRowView)?.Row["Display2"]?.ToString() ?? null))
                            {
                                MessageBox.Show("指定された発注データは登録されていないか、既に完了しています。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                goto Exit_IsError;
                            }
                        }
                        break;
                    case "集計年月":
                        if (string.IsNullOrEmpty(varValue?.ToString()))
                        {
                            MessageBox.Show("[" + controlObject.Name + "]を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        if (!DateTime.TryParse(varValue.ToString(), out DateTime dateValue1))
                        {
                            MessageBox.Show("日付を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        break;
                    case "支払年月":
                        if (string.IsNullOrEmpty(varValue?.ToString()))
                        {
                            MessageBox.Show("[" + controlObject.Name + "]を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        if (!DateTime.TryParse(varValue.ToString(), out DateTime dateValue2))
                        {
                            MessageBox.Show("日付を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        if (FunctionClass.IsClosedPayment(cn, dateValue2))
                        {
                            MessageBox.Show("指定された支払月は締め切られているため、入力できません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        break;

                }

                return false;

            Exit_IsError:
                return true;

            }
            catch (Exception ex)
            {
                Debug.Print("IsError - " + ex.Message);
                return true;
            }
        }




        internal void ChangedData(bool isChanged)
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

            // コードにフォーカスがある状態でサブフォームから呼び出されたときの対処
            if (this.ActiveControl == this.入庫コード)
            {
                this.入庫日.Focus();
            }

            this.入庫コード.Enabled = !isChanged;
            this.コマンド複写.Enabled = !isChanged;
            this.コマンド削除.Enabled = !isChanged;
            this.コマンド登録.Enabled = isChanged;


        }


        public void UpdatedControl(Control controlObject)
        {
            try
            {

                var varValue = controlObject.Text;
                string strSQL;
                DateTime dat1;
                Connect();

                switch (controlObject.Name)
                {
                    case "入庫コード":
                        // ヘッダ部の表示
                        LoadHeader(this, this.CurrentCode);

                        // 明細部の表示
                        strSQL = "SELECT *,IIf(発注残数量=0,'■','') as 全入庫 FROM V入庫明細 " +
                            $"WHERE 入庫コード='{this.CurrentCode}' " +
                            "ORDER BY 明細番号";
                        VariableSet.SetTable2Details(入庫明細1.Detail, strSQL, cn);

                        ChangedData(false);

                        // 動作制御
                        FunctionClass.LockData(this, this.IsDecided || this.IsDeleted || this.IsCompleted, "入庫コード");
                        this.コマンド複写.Enabled = true;
                        this.コマンド削除.Enabled = !(this.IsDeleted || this.IsCompleted);
                        入庫明細1.Detail.AllowUserToAddRows = !(this.IsDeleted || this.IsCompleted);
                        入庫明細1.Detail.AllowUserToDeleteRows = !(this.IsDeleted || this.IsCompleted);
                        入庫明細1.Detail.ReadOnly = (this.IsDeleted || this.IsCompleted);
                        break;
                    case "入庫日":
                        // 集計年月と支払年月を入力する
                        if (!string.IsNullOrEmpty(controlObject.Text) && !string.IsNullOrEmpty(this.SupplierCloseDay.Text))
                        {
                            dat1 = Convert.ToDateTime(controlObject.Text);
                            int SupplierCloseDayInt;
                            int.TryParse(SupplierCloseDay.Text, out SupplierCloseDayInt);
                            this.集計年月.Text = FunctionClass.GetAddupMonth(cn, dat1, SupplierCloseDayInt);
                            dat1 = FunctionClass.GetPayDay(cn, DateTime.Parse(集計年月.Text));
                            this.支払年月.Text = $"{dat1.Year}/{dat1.Month.ToString("D2")}";
                        }

                        // 適用する消費税率を入力する
                        this.TaxRate.Text = FunctionClass.GetTaxRate(cn, DateTime.Parse(controlObject.Text)).ToString();
                        break;
                    case "発注コード":
                        if (発注コード.SelectedIndex == -1) return;

                        FunctionClass fn = new FunctionClass();
                        fn.DoWait("発注データ読み込み中...");


                        this.発注版数.Text = (発注コード.SelectedItem as DataRowView)?.Row["Display2"]?.ToString() ?? null;

                        // 集計年月と支払年月を入力する
                        if (!string.IsNullOrEmpty(this.入庫日.Text) && !string.IsNullOrEmpty(this.SupplierCloseDay.Text))
                        {
                            dat1 = Convert.ToDateTime(this.入庫日.Text);
                            int SupplierCloseDayInt;
                            int.TryParse(SupplierCloseDay.Text, out SupplierCloseDayInt);
                            this.集計年月.Text = FunctionClass.GetAddupMonth(cn, dat1, SupplierCloseDayInt);
                            dat1 = FunctionClass.GetPayDay(cn, DateTime.Parse(集計年月.Text));
                            this.支払年月.Text = $"{dat1.Year}/{dat1.Month.ToString("D2")}";
                        }

                        // 発注データからリレー入力する
                        strSQL = $"SELECT '{CurrentCode}' AS 入庫コード,発注明細番号 as 明細番号, *,IIf(発注残数量=0,'■','') as 全入庫 FROM V入庫明細_発注 " +
                               $"WHERE 発注コード='{発注コード.Text}' AND 発注版数={発注版数.Text} ORDER BY 発注明細番号";
                        if (!VariableSet.SetTable2Details(入庫明細1.Detail, strSQL, cn))
                        {
                            MessageBox.Show("発注データの呼び出しに失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }

                        fn.WaitForm.Close();

                        ChangedData(false);
                        break;
                    case "集計年月":
                        // 入力文字が削除されたとき以外は書式を整える
                        if (!string.IsNullOrEmpty(varValue))
                        {
                            this.集計年月.Text = $"{Convert.ToDateTime(varValue).Year}/{Convert.ToDateTime(varValue).Month.ToString("D2")}";
                            dat1 = FunctionClass.GetPayDay(cn, DateTime.Parse(集計年月.Text));
                            this.支払年月.Text = $"{dat1.Year}/{dat1.Month.ToString("D2")}";
                        }
                        break;
                    case "支払年月":
                        // 入力文字が削除されたとき以外は書式を整える
                        if (!string.IsNullOrEmpty(varValue))
                        {
                            this.支払年月.Text = $"{Convert.ToDateTime(varValue).Year}/{Convert.ToDateTime(varValue).Month.ToString("D2")}";
                        }
                        if (string.IsNullOrEmpty(this.集計年月.Text))
                        {
                            this.集計年月.Text = MonthAdd(-1, varValue);
                        }
                        break;
                }

                //テスト用
                //入庫明細1.Detail.ReadOnly = false;


            }
            catch (Exception ex)
            {
                Debug.Print($"{this.Name}_UpdatedControl - {ex.Message}");
            }
            finally
            {


            }
        }


        private bool LoadHeader(Form formObject, string codeString)
        {
            try
            {
                Connect();

                string strSQL;

                strSQL = "SELECT * FROM V入庫ヘッダ WHERE 入庫コード ='" + codeString + "'";



                VariableSet.SetTable2Form(this, strSQL, cn);

                if (!string.IsNullOrEmpty(入庫日.Text))
                {
                    DateTime tempDate = DateTime.Parse(入庫日.Text);
                    入庫日.Text = tempDate.ToString("yyyy/MM/dd");

                }

                return true;



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // エラーハンドリングが必要に応じて行われるべきです
                return false;
            }
        }




        public string MonthAdd(long number, string TargetMonth)
        {
            DateTime dtmNew = DateTime.Parse(TargetMonth + "/1");
            dtmNew = dtmNew.AddMonths((int)number);

            return dtmNew.ToString("yyyy/MM");
        }

        private void コマンド修正_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.SuspendLayout();

                Connect();

                if (!IsChanged)
                {
                    if (IsNewData && !string.IsNullOrEmpty(CurrentCode))
                    {
                        if (!FunctionClass.ReturnCode(cn, CurrentCode))
                        {
                            MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine +
                                            "入庫コード　：　" + CurrentCode,
                                            "修正コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }

                    if (!GoModifyMode())
                    {
                        MessageBox.Show("エラーのため修正モードへ移行できません。", "修正コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    return;
                }

                var result = MessageBox.Show("変更内容を登録しますか？", "修正コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                switch (result)
                {
                    case DialogResult.Yes:
                        // エラーチェック
                        foreach (Control control in Controls)
                        {
                            if (control is TextBox || control is ComboBox || control is CheckBox)
                            {
                                if (IsError(control))
                                {
                                    return;
                                }
                            }
                        }

                        if (!SaveData())
                        {
                            MessageBox.Show("エラーのため登録できません。", "修正コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        break;

                    case DialogResult.No:
                        if (IsNewData && !string.IsNullOrEmpty(CurrentCode))
                        {
                            if (!FunctionClass.ReturnCode(cn, CurrentCode))
                            {
                                MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine +
                                                "入庫コード　：　" + CurrentCode,
                                                "修正コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                        break;

                    case DialogResult.Cancel:
                        return;
                }

                if (!GoModifyMode())
                {
                    MessageBox.Show("エラーのため修正モードへ移行できません。", "修正コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーのため修正モードへ移行できません。", "修正コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            finally
            {
                this.ResumeLayout();
                Cursor.Current = Cursors.Default;
            }
        }

        private void コマンド複写_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。", "複写コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド削除_Click(object sender, EventArgs e)
        {

            Connect();

            try
            {
                string strMsg;

                // 削除するかどうか判断を仰ぐ
                strMsg = $"入庫コード　：　{CurrentCode}{Environment.NewLine}{Environment.NewLine}このデータを削除しますか？";
                if (MessageBox.Show(strMsg, "削除コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                // 削除済みかどうかを調べ、削除済みであれば何もしない
                if (DataIsDeleted(CurrentCode))
                {
                    MessageBox.Show("現在のデータは既に削除されています。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    コマンド削除.Enabled = false;
                    return;
                }

                // ログインユーザーが表示データの登録ユーザーでなければ認証する
                if (CommonConstants.LoginUserCode != InputUserCode)
                {
                    using (var authenticationForm = new F_認証())
                    {
                        authenticationForm.args = InputUserCode;
                        authenticationForm.ShowDialog();

                        if (string.IsNullOrEmpty(CommonConstants.strCertificateCode))
                        {
                            MessageBox.Show("認証できません。" + Environment.NewLine + "削除はキャンセルされました。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }

                // 削除処理
                if (DeleteData(cn, CurrentCode))
                {
                    MessageBox.Show("削除しました。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 棚卸中かどうかを確認する
                    if (FunctionClass.IsInventory(cn))
                    {
                        Close();
                    }
                    else
                    {
                        // 棚卸中でなければ新規モードへ移行する

                        if (!GoNewMode())
                        {
                            MessageBox.Show($"エラーのため新規モードへ移行できません。{Environment.NewLine}[{Name}]を終了します。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            Close();
                        }

                    }
                }
                else
                {
                    MessageBox.Show($"削除できませんでした。{Environment.NewLine}{Environment.NewLine}入庫コード　：　{CurrentCode}", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"{Name}_コマンド削除_Click - {ex.GetType().ToString()} : {ex.Message}");
            }
        }


        private bool DataIsDeleted(string codeString)
        {
            try
            {

                Connect();

                string strKey = $"入庫コード='{codeString}' AND 無効日時 IS NOT NULL";
                string strSQL = $"SELECT * FROM T入庫 WHERE {strKey}";

                using (var cmd = new SqlCommand(strSQL, cn))
                using (var reader = cmd.ExecuteReader())
                {
                    return reader.HasRows;
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"{Name}_DataIsDeleted - {ex.GetType().ToString()} : {ex.Message}");
                return false;
            }
        }

        private bool DeleteData(SqlConnection connectionObject, string codeString)
        {
            try
            {
                string strKey = $"入庫コード='{codeString}'";

                using (var cmd = new SqlCommand($"UPDATE T入庫 SET 無効日時=GETDATE() WHERE {strKey}", connectionObject))
                {
                    connectionObject.Open();
                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.Print($"{Name}_DeleteData - {ex.GetType().ToString()} : {ex.Message}");

                if (connectionObject.State == ConnectionState.Open)
                {
                    connectionObject.Close();
                }

                return false;
            }
            finally
            {
                if (connectionObject.State == ConnectionState.Open)
                {
                    connectionObject.Close();
                }
            }
        }

        private void コマンド発注_Click(object sender, EventArgs e)
        {
            if (IsNull(発注コード.Text))
            {
                F_発注 targetform = new F_発注();
                targetform.ShowDialog();
            }
            else
            {
                F_発注 targetform = new F_発注();
                targetform.args = $"{発注コード.Text},{発注版数.Text}";
                targetform.ShowDialog();
            }
        }

        private void コマンド仕入先_Click(object sender, EventArgs e)
        {
            F_仕入先 targetform = new F_仕入先();
            targetform.args = SupplierCode;
            targetform.ShowDialog();
        }

        private void コマンド承認_Click(object sender, EventArgs e)
        {
            IsErrorDetails();
        }



        private void IsErrorDetails()
        {
            // 仮にGridViewが入庫明細1という名前であると仮定
            GcMultiRow multiRow = 入庫明細1.Detail; // yourDataGridViewControlは実際のコントロールに置き換える

            int emptyCount = 0;

            for (int i = 0; i < multiRow.RowCount; i++)
            {
                if (multiRow.Rows[i].IsNewRow == true)
                {
                    //新規行の場合は、処理をスキップ
                    continue;
                }
                // 買掛区分の列が0番目であると仮定
                if (string.IsNullOrEmpty(multiRow.Rows[i].Cells["買掛区分"].DisplayText))
                {
                    emptyCount++;
                }


            }

            MessageBox.Show($"買掛区分が空のレコード数: {emptyCount}");
        }

        private void コマンド確定_Click(object sender, EventArgs e)
        {

            FunctionClass fn = new FunctionClass();
            fn.DoWait("確定しています...");

            Connect();

            try
            {
                object var1 = null;
                object var2 = null;


                // 未確定の場合エラーチェックを行う
                if (!IsDecided)
                {
                    // エラーチェック
                    foreach (Control control in Controls)
                    {
                        if (control is TextBox || control is ComboBox || control is CheckBox)
                        {
                            if (IsError(control))
                            {
                                return;
                            }
                        }
                    }
                }



                // 新規データ時の処理
                if (IsNewData)
                {
                    登録日時.Text = FunctionClass.GetServerDate(cn).ToString();
                    登録者コード.Text = CommonConstants.LoginUserCode;
                    登録者名.Text = CommonConstants.LoginUserFullName;
                }

                // 値を退避させる
                var1 = 確定日時.Text;
                var2 = 確定者コード.Text;

                // 値をセットする
                if (IsDecided)
                {
                    確定日時.Text = null;
                    確定者コード.Text = null;
                }
                else
                {
                    確定日時.Text = FunctionClass.GetServerDate(cn).ToString();
                    確定者コード.Text = CommonConstants.LoginUserCode; ;
                }

                // サーバーへ登録する
                if (RegTrans(CurrentCode))
                {
                    ChangedData(false);
                    // 新規モードのときは修正モードへ移行する
                    if (IsNewData)
                    {
                        コマンド新規.Enabled = true;
                        コマンド修正.Enabled = false;
                    }
                }
                else
                {
                    // 新規モードのときは登録情報を戻す
                    if (IsNewData)
                    {
                        登録日時.Text = null;
                        登録者コード.Text = null;
                        登録者名.Text = null;
                    }
                    確定日時.Text = var1.ToString();
                    確定者コード.Text = var2.ToString();
                    MessageBox.Show("登録できませんでした。", "確定コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                // 確定状態では入庫数量変更不可にする
                //SubForm.入庫数量.Locked = 確定日時.Text != null;
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_コマンド確定_Click - " + ex.GetType().ToString() + " : " + ex.Message);
            }
            finally
            {
                fn.WaitForm.Close();
            }
        }

        private void コマンド登録_Click(object sender, EventArgs e)
        {

            FunctionClass fn = new FunctionClass();
            fn.DoWait("登録しています...");

            try
            {


                this.SuspendLayout();

                if (SaveData())
                {
                    ChangedData(false);

                    // 新規モードのときは修正モードへ移行する
                    if (IsNewData)
                    {
                        this.コマンド新規.Enabled = true;
                        this.コマンド修正.Enabled = false;


                        OriginalClass ofn = new OriginalClass();
                        ofn.SetComboBox(入庫コード, " SELECT 入庫コード as Display, 入庫コード as Value FROM T入庫 WHERE(発注コード IS NOT NULL) ORDER BY 入庫コード DESC");

                    }
                }
                else
                {
                    MessageBox.Show("登録できませんでした。", "登録コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーが発生しました。", "登録コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                fn.WaitForm.Close();
                this.ResumeLayout();

            }
        }

        private void コマンド終了_Click(object sender, EventArgs e)
        {


            Close(); // フォームを閉じる
        }

        private void 発注コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            発注版数.Text = (発注コード.SelectedItem as DataRowView)?.Row["Display2"]?.ToString() ?? null;

            UpdatedControl(sender as Control);

        }


        private void 発注コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 140, 20, 100, 300, 200 }, new string[] { "Display", "Display2", "Display3", "Display4", "Display5" });
            発注コード.Invalidate();
            発注コード.DroppedDown = true;
        }

        private void 発注コード_TextChanged(object sender, EventArgs e)
        {

            if (発注コード.SelectedValue == null)
            {
                発注版数.Text = null;
            }
            ChangedData(true);
        }

        private void 発注コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 発注コード_KeyPress(object sender, KeyPressEventArgs e)
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

        private void 発注コード_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Return)
                {
                    string strCode = 発注コード.Text;
                    if (strCode == "") return;
                    strCode = FunctionClass.FormatCode("ORD", strCode);
                    if (strCode != 発注コード.Text)
                    {
                        // Text プロパティで値を設定後、キーコードはリセットしておく
                        発注コード.Text = strCode;
                        e.SuppressKeyPress = true;
                    }
                }
            }
            catch (Exception ex)
            {
                // エラーハンドリング（エラーが発生した場合の処理）
                Debug.Print($"{nameof(発注コード_KeyDown)} - {ex.Message}");
            }
        }

        private void 入庫者コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            入庫者名.Text = (入庫者コード.SelectedItem as DataRowView)?.Row["Display2"]?.ToString() ?? null;

            UpdatedControl(sender as Control);
        }

        private void 入庫者コード_TextChanged(object sender, EventArgs e)
        {
            if (入庫者コード.SelectedValue == null)
            {
                入庫者名.Text = null;
            }
            ChangedData(true);
        }

        private void 入庫者コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 50, 150 }, new string[] { "Display", "Display2" });
            入庫者コード.Invalidate();
            入庫者コード.DroppedDown = true;
        }

        private void 入庫者コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 入庫コード_KeyDown(object sender, KeyEventArgs e)
        {
            string strCode;

            switch (e.KeyCode)
            {
                case Keys.Return:
                    strCode = 入庫コード.Text;
                    if (string.IsNullOrEmpty(strCode))
                        return;

                    strCode = FunctionClass.FormatCode("STR", strCode);
                    if (strCode != Nz(入庫コード.Text))
                    {
                        入庫コード.Text = strCode;
                    }
                    break;
            }
        }

        private void 入庫コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ') // スペースキーが押されたかを確認
            {
                if (sender is ComboBox comboBox)
                {
                    comboBox.DroppedDown = true; // コンボボックスのドロップダウンを開く
                    e.Handled = true; // イベントの処理が完了したことを示す
                }
            }
        }

        private void 入庫コード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 11);

        }

        private void 入庫コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 入庫コード_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 集計年月_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 集計年月_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 集計年月_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 集計年月_Enter(object sender, EventArgs e)
        {
            MessageBox.Show("通常、支払年月を変更することはありません。" + Environment.NewLine +
                    "変更するときは、適切であると判断できる値を入力してください。",
                    "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        }

        private void 支払年月_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 支払年月_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 支払年月_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 支払年月_Enter(object sender, EventArgs e)
        {
            MessageBox.Show("通常、支払年月を変更することはありません。" + Environment.NewLine +
                    "変更するときは、適切であると判断できる値を入力してください。",
                    "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void 摘要_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 200);
            ChangedData(true);
        }

        private void 摘要_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 摘要_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 入庫日_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 入庫日_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 入庫日_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 入庫日_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                入庫日選択ボタン_Click(sender, e);
                e.Handled = true; // イベントの処理が完了したことを示す
            }
        }

        private F_カレンダー dateSelectionForm;

        private void 入庫日選択ボタン_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(入庫日.Text))
            {
                dateSelectionForm.args = 入庫日.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK && !入庫日.ReadOnly && 入庫日.Enabled)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                入庫日.Text = selectedDate;
            }
        }

        private void 発注コード_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■発注書の注文番号（発注コード）を入力します。　■先頭のキーワード及び 0 は省略できます。　■入庫登録された発注データは表示されません。";
        }

        private void 発注コード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 買掛区分コード設定_SelectedIndexChanged(object sender, EventArgs e)
        {
            for (int i = 0; i < 入庫明細1.Detail.RowCount; i++)
            {
                if (入庫明細1.Detail.Rows[i].IsNewRow == true)
                {
                    //新規行の場合は、処理をスキップ
                    continue;
                }
                入庫明細1.Detail.Rows[i].Cells["買掛区分"].Value = ((DataRowView)買掛区分コード設定.SelectedItem)?.Row.Field<String>("Display")?.ToString();
                入庫明細1.Detail.Rows[i].Cells["買掛区分コード"].Value = ((DataRowView)買掛区分コード設定.SelectedItem)?.Row.Field<String>("Display2")?.ToString();
                入庫明細1.Detail.Rows[i].Cells["買掛明細コード"].Value = ((DataRowView)買掛区分コード設定.SelectedItem)?.Row.Field<Int16?>("Display3")?.ToString();


            }
        }

        private void 買掛区分コード設定_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 200, 0, 0 }, new string[] { "Display", "Display2", "Display3" });
            買掛区分コード設定.Invalidate();
            買掛区分コード設定.DroppedDown = true;
        }

        private void 買掛区分コード設定_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■現在表示されている明細行全ての買掛区分欄に入力値を設定し、更新します。既存値は破棄されます。";
        }

        private void 買掛区分コード設定_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }
    }
}
