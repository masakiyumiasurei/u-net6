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
            ofn.SetComboBox(発注コード, " SELECT 発注コード as Display,発注版数 as Display2, Format(発注日,'yyyy/MM/dd') as Display3, 仕入先名 as Display4, 仕入先担当者名 as Display5, 発注コード as Value FROM V入庫_発注コード選択 ORDER BY 発注コード");
            発注コード.DrawMode = DrawMode.OwnerDrawFixed;





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
                    if (GoNewMode())
                    {
                        
                    }
                    else
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
                //LoadDetails(strSQL, SubForm, SubDatabase, "入庫明細");

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
            this.TaxRate.Text = FunctionClass.GetTaxRate(cn,date1).ToString("0.##");

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

                // 各コントロール値をクリア
                VariableSet.SetControls(this);

                // 入庫明細の削除
                //SubDatabase.Execute("DELETE FROM 入庫明細");

                // 入庫明細の再読み込み
                //this.SubForm.Form.Requery();

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

                    if (!DataUpdater.UpdateOrInsertDataFrom(this, cn, "T入庫", strwhere, "入庫コード", transaction))
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
                        if (FunctionClass.IsClosedPayment(cn,dateValue2)){
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




        private void ChangedData(bool isChanged)
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
                        //strSQL = "SELECT * FROM V入庫明細 " +
                        //    $"WHERE 入庫コード='{this.CurrentCode}' " +
                        //    "ORDER BY 明細番号";
                        //LoadDetails(strSQL, SubForm, SubDatabase, "入庫明細");

                        // 動作制御
                        FunctionClass.LockData(this, this.IsDecided || this.IsDeleted || this.IsCompleted, "入庫コード");
                        this.コマンド複写.Enabled = true;
                        this.コマンド削除.Enabled = !(this.IsDeleted || this.IsCompleted);
                        //SubForm.AllowAdditions = !(this.IsDeleted || this.IsCompleted);
                        //SubForm.AllowDeletions = !(this.IsDeleted || this.IsCompleted);
                        //SubForm.AllowEdits = !(this.IsDeleted || this.IsCompleted);
                        break;
                    case "入庫日":
                        // 集計年月と支払年月を入力する
                        if (!string.IsNullOrEmpty(controlObject.Text) && !string.IsNullOrEmpty(this.SupplierCloseDay.Text))
                        {
                            dat1 = Convert.ToDateTime(controlObject.Text);
                            int SupplierCloseDayInt;
                            int.TryParse(SupplierCloseDay.Text, out SupplierCloseDayInt);
                            this.集計年月.Text = FunctionClass.GetAddupMonth(cn,dat1, SupplierCloseDayInt);
                            dat1 = FunctionClass.GetPayDay(cn,DateTime.Parse(集計年月.Text));
                            this.支払年月.Text = $"{dat1.Year}/{dat1.Month.ToString("D2")}";
                        }

                        // 適用する消費税率を入力する
                        this.TaxRate.Text = FunctionClass.GetTaxRate(cn,DateTime.Parse(controlObject.Text)).ToString();
                        break;
                    case "発注コード":
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
                        //if (!SetDetails(this.CurrentCode, controlObject.Text, this.発注版数.Text))
                        //{
                        //    MessageBox.Show("発注データの呼び出しに失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //}

                        fn.WaitForm.Close();
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

        }

        private void コマンド複写_Click(object sender, EventArgs e)
        {

        }

        private void コマンド削除_Click(object sender, EventArgs e)
        {

        }

        private void コマンド発注_Click(object sender, EventArgs e)
        {

        }

        private void コマンド仕入先_Click(object sender, EventArgs e)
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
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 140,20,100,300,200 }, new string[] { "Display", "Display2","Display3","Display4","Display5" });
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
                    string strCode = ActiveControl.Text;
                    if (strCode == "") return;
                    strCode = FunctionClass.FormatCode("ORD", strCode);
                    if (strCode != (ActiveControl as dynamic).Value)
                    {
                        // Text プロパティで値を設定後、キーコードはリセットしておく
                        ActiveControl.Text = strCode;
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
    }
}
