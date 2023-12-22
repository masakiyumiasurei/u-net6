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
using System.Text;
using Pao.Reports;
using GrapeCity.Win.MultiRow;

namespace u_net
{
    public partial class F_製品 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "製品";
        private int selected_frame = 0;
        public bool IsDirty = false;

        public F_製品()
        {
            this.Text = "製品";       // ウィンドウタイトルを設定
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

        private string Right(string value, int length)
        {
            if (value.Length <= length)
                return value;
            else
                return value.Substring(value.Length - length, length);
        }


        public bool BeDetails
        {
            get
            {
                return 0 < 製品明細1.Detail.RowCount;
            }
        }
        public string CurrentCode
        {
            get
            {
                return Nz(製品コード.Text);
            }
        }

        public int CurrentEdition
        {
            get
            {
                return string.IsNullOrEmpty(製品版数.Text) ? 0 : Int32.Parse(製品版数.Text);
            }
        }

 


        public bool IsNewData
        {
            get
            {
                return !コマンド新規.Enabled;
            }
        }

        public bool IsRevising
        {
            get
            {
                return !コマンド新規.Enabled;
            }
        }


        public bool IsAbolished
        {
            get
            {
                return !IsNull(廃止.Text);
            }
        }

        public bool IsApproved
        {
            get
            {
                return !IsNull(承認日時.Text);
            }
        }

        public bool IsDecided
        {
            get
            {
                return !IsNull(確定日時.Text);
            }
        }

        public bool IsDeleted
        {
            get
            {
                return !IsNull(無効日時.Text);
            }
        }

        private bool IsLatestEdition
        {
            get
            {
                int productVersion = string.IsNullOrEmpty(製品版数.Text?.ToString()) ? 0 : Int32.Parse(製品版数.Text);
                int maxVersion = string.IsNullOrEmpty(((DataRowView)製品コード.SelectedItem)?.Row.Field<Int16>("Display3").ToString()) ? 0 : Int32.Parse(((DataRowView)製品コード.SelectedItem)?.Row.Field<Int16>("Display3").ToString());
                return productVersion == maxVersion;
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
            return value == null || Convert.IsDBNull(value) || string.IsNullOrEmpty((string?)value);
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
            string LoginUserCode = CommonConstants.LoginUserCode;//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(LoginUserCode, this);

            MyApi myapi = new MyApi();
            int xSize, ySize, intpixel, twipperdot;

            //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
            intpixel = myapi.GetLogPixel();
            twipperdot = myapi.GetTwipPerDot(intpixel);


            OriginalClass ofn = new OriginalClass();
            ofn.SetComboBox(製品コード, "SELECT A.製品コード as Value, A.製品コード as Display, A.最新版数 as Display3, { fn REPLACE(STR(CONVERT(bit, M製品.無効日時), 1, 0), '1', '×') } AS Display2 FROM M製品 INNER JOIN (SELECT 製品コード, MAX(製品版数) AS 最新版数 FROM M製品 GROUP BY 製品コード) A ON M製品.製品コード = A.製品コード AND M製品.製品版数 = A.最新版数 ORDER BY A.製品コード DESC");
            製品コード.DrawMode = DrawMode.OwnerDrawFixed;


            ofn.SetComboBox(SeriesCode, "SELECT シリーズコード as Value,シリーズコード as Display,シリーズ名 as Display2 FROM Mシリーズ WHERE 無効日時 IS NULL ORDER BY シリーズ名");
            SeriesCode.DrawMode = DrawMode.OwnerDrawFixed;


            try
            {
                this.SuspendLayout();

                int intWindowHeight = this.Height;
                int intWindowWidth = this.Width;


                if (string.IsNullOrEmpty(args)) // 新規
                {
                    if (!GoNewMode())
                    {
                        MessageBox.Show("初期化に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }
                else // 読込se
                {
                    if (!GoModifyMode())
                    {
                        MessageBox.Show("初期化に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }

                    this.製品版数.Text = Convert.ToInt32(args.Substring(0, args.IndexOf(","))).ToString();
                    this.製品コード.Focus();
                    this.製品コード.Text = args.Substring(0, args.IndexOf(","));
                }
                args = null;




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




                // ヘッダ部の初期化
                VariableSet.SetControls(this);


                this.製品コード.Text = Right(FunctionClass.採番(cn, "PRO"),8);
                this.製品版数.Text = 1.ToString();
                this.指導書変更.Checked = false;

                // 明細部の初期化
                strSQL = "SELECT * FROM V製品明細 WHERE 製品コード='" +
                             this.CurrentCode + "' AND 製品版数=" + this.CurrentEdition +
                             " ORDER BY 明細番号";
                VariableSet.SetTable2Details(製品明細1.Detail, strSQL, cn);

                // ヘッダ部動作制御
                FunctionClass.LockData(this, false);

                this.品名.Focus();
                this.製品コード.Enabled = false;
                this.製品版数.Enabled = false;
                this.改版ボタン.Enabled = false;
                this.コマンド新規.Enabled = false;
                this.コマンド読込.Enabled = true;
                this.コマンド複写.Enabled = false;
                this.コマンド削除.Enabled = false;
                this.コマンドユニット表.Enabled = false;
                this.コマンド廃止.Enabled = false;
                this.コマンドツール.Enabled = false;
                this.コマンド承認.Enabled = false;
                this.コマンド確定.Enabled = false;
                this.コマンド登録.Enabled = false;

                // 明細部動作制御
                製品明細1.Detail.AllowUserToAddRows = true;
                製品明細1.Detail.AllowUserToDeleteRows = true;
                製品明細1.Detail.ReadOnly = false;

                success = true;
                return success;
            }
            catch (Exception ex)
            {
                Console.WriteLine(this.Name + "_GoNewMode - " + ex.Message);
                return false;
            }
        }

        private bool GoModifyMode()
        {
            try
            {
                bool success = false;
                string strSQL = "";

                // 各コントロール値をクリア
                VariableSet.SetControls(this);

                製品版数.DataSource = null;
                指導書変更.Checked = false;

                // 明細部の初期化
                strSQL = "SELECT * FROM V製品明細 WHERE 製品コード='" +
                             this.CurrentCode + "' AND 製品版数=" + this.CurrentEdition +
                             " ORDER BY 明細番号";
                VariableSet.SetTable2Details(製品明細1.Detail, strSQL, cn);

                ChangedData(false);

                this.製品コード.Focus();

                FunctionClass.LockData(this, true,"製品コード");


                // ボタンの状態を設定
                this.コマンド新規.Enabled = true;
                this.コマンド読込.Enabled = false;


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

            try
            {

                Connect();

                // データへの変更がないときの処理
                if (!IsDirty)
                {
                    // 新規モードで且つコードが取得済みのときはコードを戻す
                    if (IsNewData && !string.IsNullOrEmpty(CurrentCode) && CurrentEdition == 1)
                    {
                        // 採番された番号を戻す
                        if (!FunctionClass.ReturnCode(cn, "PRO" + CurrentCode))
                        {
                            MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine + Environment.NewLine +
                                            "製品コード　：　" + CurrentCode, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    return;
                }

                // 修正されているときは登録確認を行う
                var intRes = MessageBox.Show("変更内容を登録しますか？", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (intRes)
                {
                    case DialogResult.Yes:
                        // エラーチェック
                        if (!ErrCheck())
                        {
                            return;
                        }
                        // 登録処理
                        if (!SaveData())
                        {
                            if (MessageBox.Show("エラーのため登録できませんでした。" + Environment.NewLine +
                                                "強制終了しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                return;
                            }
                        }
                        break;
                    case DialogResult.No:
                        //新規コードを取得していたときはコードを戻す
                        if (IsNewData && !string.IsNullOrEmpty(CurrentCode) && CurrentEdition == 1)
                        {
                            if (!FunctionClass.ReturnCode(cn, "PRO" + CurrentCode))
                            {
                                MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine +
                                                "製品コード　：　" + CurrentCode, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                        break;
                    case DialogResult.Cancel:
                        return;
                }


            }
            catch (Exception ex)
            {
                Debug.Print(Name + "_Unload - " + ex.Message);
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Form unitSelectionForm = Application.OpenForms["F_ユニット選択"];

                if (unitSelectionForm != null)
                {
                    // フォームが開いている場合は閉じる
                    unitSelectionForm.Close();
                }
            }
        }


        
 
        private bool ErrCheck()
        {
            //入力確認    
            if (!FunctionClass.IsError(this.品名)) return false;
            if (!FunctionClass.IsError(this.シリーズ名)) return false;
            if (!FunctionClass.IsError(this.識別コード)) return false;

            if((IsDecided && 製品明細1.Detail.RowCount < 1) || (!IsDecided && 製品明細1.Detail.RowCount <= 1))
            {
                MessageBox.Show("１つ以上の明細が必要です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            return true;
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

            if (ActiveControl == 製品コード)
            {
                品名.Focus();
            }

            製品コード.Enabled = !isChanged;

            if (ActiveControl == 製品版数)
            {
                品名.Focus();
            }

            IsDirty = isChanged;

            製品版数.Enabled = !isChanged;
            コマンド複写.Enabled = !isChanged;
            コマンド削除.Enabled = !isChanged;
            コマンドユニット表.Enabled = !isChanged;
            コマンド廃止.Enabled = !isChanged;
            コマンドツール.Enabled = !isChanged;

            if (isChanged && !IsApproved)
            {
                コマンド承認.Enabled = false;
                コマンド確定.Enabled = true;
            }

            コマンド登録.Enabled = isChanged;
        }
       



    
        private bool IsError(Control controlObject)
        {
            try
            {
                object varValue = controlObject.Text;
                string controlName = controlObject.Name;

                switch (controlName)
                {
                    case "品名":
                    case "シリーズ名":
                    case "識別コード":
                        if (string.IsNullOrEmpty(varValue.ToString()))
                        {
                            MessageBox.Show(controlName + "を入力してください.", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;
                 
                    default:
                        // 他のコントロールに対するエラーチェックロジックを追加してください。
                        break;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // エラーハンドリングが必要に応じて行われるべきです
                return true;
            }
        }

        private void UpdatedControl(Control controlObject)
        {
            FunctionClass fn = new FunctionClass();

            try
            {
                string strSQL;
                
                
                Connect();

                switch (controlObject.Name)
                {
                    case "製品コード":
                        fn.DoWait("読み込んでいます...");


                        // 版数のソース更新
                        UpdateEditionList(controlObject.Text);

                        // OpenArgsが設定されていなければ版数を最新版とする
                        // 開いてからコードを変えて読み込むときはOpenArgsはnullに
                        // 設定されているため、最新版となる
                        if (string.IsNullOrEmpty(args))
                        {
                            this.製品版数.Text = ((DataRowView)製品コード.SelectedItem)?.Row.Field<String>("Display3")?.ToString();
                        }

                        // ヘッダ部の表示
                        LoadHeader(this, CurrentCode, CurrentEdition);

                        // 明細部の表示
                        strSQL = "SELECT * FROM V製品明細 WHERE 製品コード='" +
                                 this.CurrentCode + "' AND 製品版数=" + this.CurrentEdition +
                                 " ORDER BY 明細番号";
                        if (!VariableSet.SetTable2Details(製品明細1.Detail, strSQL, cn))
                        {
                            fn.WaitForm.Close();
                            return;
                        }

                        // RoHS対応状況を表示する
                        this.RoHS対応.Text = GetRohsStatus();

                        // 製品材料費を表示する 
                        //明細処理
                        //SubForm.SetTotalAmount();

                        // 状態の表示
                        SetEditionStatus();

                        


                        // 動作を制御する
                        FunctionClass.LockData(this, this.IsDecided || this.IsDeleted, "製品コード");
                        this.製品版数.Enabled = true; // 版数を編集可能にする
                        this.改版ボタン.Enabled = this.IsLatestEdition && this.IsApproved;
                        製品明細1.Detail.AllowUserToAddRows = !this.IsDecided;
                        製品明細1.Detail.AllowUserToDeleteRows = !this.IsDecided;
                        製品明細1.Detail.ReadOnly = this.IsDecided;

                        ChangedData(false);

                        this.コマンド複写.Enabled = !this.IsDirty;
                        this.コマンド削除.Enabled = this.IsLatestEdition;
                        this.コマンドユニット表.Enabled = !this.IsDirty;
                        this.コマンド承認.Enabled = this.IsDecided && !this.IsApproved;
                        this.コマンド確定.Enabled = !this.IsApproved;

                        fn.WaitForm.Close();

                        break;

                    case "製品版数":
                        fn.DoWait("読み込んでいます...");

                        // ヘッダ部の表示
                        LoadHeader(this, CurrentCode, CurrentEdition);

                        // 明細部の表示
                        strSQL = "SELECT * FROM V製品明細 WHERE 製品コード='" +
                                 this.CurrentCode + "' AND 製品版数=" + this.CurrentEdition +
                                 " ORDER BY 明細番号";
                        if (!VariableSet.SetTable2Details(製品明細1.Detail, strSQL, cn))
                        {
                            fn.WaitForm.Close();
                            return;
                        }


                        // RoHS対応状況を表示する
                        this.RoHS対応.Text = GetRohsStatus();

                        // 製品材料費を表示する
                        //明細処理
                        //SubForm.SetTotalAmount();

                        // 状態の表示
                        SetEditionStatus();

                        

                        // 動作を制御する
                        FunctionClass.LockData(this, this.IsDecided || this.IsDeleted, "製品コード", "製品版数");
                        this.改版ボタン.Enabled = this.IsLatestEdition && this.IsApproved;
                        製品明細1.Detail.AllowUserToAddRows = !this.IsDecided;
                        製品明細1.Detail.AllowUserToDeleteRows = !this.IsDecided;
                        製品明細1.Detail.ReadOnly = this.IsDecided;

                        ChangedData(false);

                        this.コマンド複写.Enabled = !this.IsDirty;
                        this.コマンド削除.Enabled = this.IsLatestEdition;
                        this.コマンドユニット表.Enabled = !this.IsDirty;
                        this.コマンド承認.Enabled = this.IsDecided && !this.IsApproved;
                        this.コマンド確定.Enabled = !this.IsApproved;

                        fn.WaitForm.Close();

                        break;
                }


                //テスト用
                //製品明細1.Detail.AllowUserToAddRows = true;
                //製品明細1.Detail.AllowUserToDeleteRows = true;
                //製品明細1.Detail.ReadOnly = false;


            }
            catch (Exception ex)
            {
                // 例外処理
                Debug.Print(this.Name + "_UpdatedControl - " + ex.Message);
                fn.WaitForm.Close();
            }
        }


        private void UpdateEditionList(string codeString)
        {

            OriginalClass ofn = new OriginalClass();


            ofn.SetComboBox(製品版数, "SELECT 製品版数 AS Value, 製品版数 AS Display, " +
                    "{ fn REPLACE(STR(CONVERT(bit, 承認日時), 1, 0), '1', '■') } AS Display2 " +
                    "FROM M製品 " +
                    "WHERE (製品コード = '" + codeString + "') " +
                    "ORDER BY 製品版数 DESC");
            製品版数.DrawMode = DrawMode.OwnerDrawFixed;

        }

        private string GetRohsStatus()
        {
            try
            {
                int stPriority = 100; // RoHSの対応状況を優先的に選定するための番号

                if ((IsDecided && 製品明細1.Detail.RowCount < 1) || (!IsDecided && 製品明細1.Detail.RowCount <= 1))
                {
                    stPriority = 1;
                    return "×";
                }

                for (int i = 0; i < 製品明細1.Detail.RowCount; i++)
                {
                    if (製品明細1.Detail.Rows[i].IsNewRow) continue;

                    if (!Convert.ToBoolean(製品明細1.Detail.Rows[i].Cells["削除対象"].Value))
                    {
                        int priority = Convert.ToInt32(製品明細1.Detail.Rows[i].Cells["RohsStatusPriority"].Value);
                        if (priority < stPriority)
                        {
                            stPriority = priority;
                            return 製品明細1.Detail.Rows[i].Cells["RohsStatusSign"].Value.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // 例外処理
                Console.WriteLine("GetRohsStatus Error: " + ex.Message);
            }

            return ""; // エラー時は空文字列を返す（またはエラー処理に応じて適切な値を返す）
        }



        public void SetEditionStatus()
        {
            // 状態を初期化
            状態.Text = null;

            if (IsApproved)
            {
                if (CurrentEdition == Convert.ToInt32(製品版数.Text))
                {
                    状態.Text = "最新版";
                    状態.ForeColor = Color.Red;
                }
                else
                {
                    状態.ForeColor = Color.Black;

                    if (製品版数.GetItemText(製品版数.Items[0]) == "")
                    {
                        状態.Text = "改版中";
                    }
                    else
                    {
                        状態.Text = "旧版";
                    }
                }
            }
        }

        private bool LoadHeader(Form formObject, string codeString,int editionNumber)
        {
            try
            {
                Connect();

                string strSQL;

                strSQL = "SELECT * FROM V製品ヘッダ WHERE 製品コード ='" + codeString + "' and 製品版数 = " + editionNumber;

    
                if (string.IsNullOrEmpty(確定日時.Text))
                {
                    確定表示.SendToBack();
                }
                else
                {
                    確定表示.BringToFront();
                }
 
                if (string.IsNullOrEmpty(承認日時.Text))
                {
                    承認表示.SendToBack();
                }
                else
                {
                    承認表示.BringToFront();
                }

                if (SupersededDate == null || string.IsNullOrEmpty(SupersededDate.Text))
                {
                    廃止表示.SendToBack();
                }
                else
                {
                    廃止表示.BringToFront();
                }


                VariableSet.SetTable2Form(this, strSQL, cn);

                if (状態.Text == "最新版")
                {
                    状態.ForeColor = Color.Red;
                }
                else
                {
                    状態.ForeColor = Color.Black;
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

        private bool SaveData()
        {

            Connect();
            
            {
                try
                {
                    Connect();

                    DateTime dtmNow = FunctionClass.GetServerDate(cn);

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
                        objControl1 = 作成日時;
                        objControl2 = 作成者コード;
                        objControl3 = 作成者名;

                        varSaved1 = objControl1.Text;
                        varSaved2 = objControl2.Text;
                        varSaved3 = objControl3.Text;

                        objControl1.Text = dtmNow.ToString();
                        objControl2.Text = CommonConstants.LoginUserCode;
                        objControl3.Text = CommonConstants.LoginUserFullName;
                    }

                    objControl4 = 更新日時;
                    objControl5 = 更新者コード;
                    objControl6 = 更新者名;

                    varSaved4 = objControl4.Text;
                    varSaved5 = objControl5.Text;
                    varSaved6 = objControl6.Text;

                    objControl4.Text = dtmNow.ToString();
                    objControl5.Text = CommonConstants.LoginUserCode;
                    objControl6.Text = CommonConstants.LoginUserFullName;

                    
                    // 登録処理
                    if (RegTrans(CurrentCode,CurrentEdition,false))
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
                    }
                }
                catch (Exception ex)
                {
                    // エラーハンドリングが必要な場合には追加してください
                    Console.WriteLine($"Error in SaveData: {ex.Message}");
                }

                return false;
            }
        }

        private bool RegTrans(string codeString,int editionNumber,bool updatePreEdition)
        {
            Connect();
            SqlTransaction transaction = cn.BeginTransaction();
            {

                try
                {

                    string strwhere = "製品コード='" + codeString + "' and 製品版数 =" + editionNumber;
                    // ヘッダ部の登録
                    if (!DataUpdater.UpdateOrInsertDataFrom(this, cn, "M製品", strwhere, "製品コード", transaction,"製品版数"))
                    {
                        transaction.Rollback();  // 変更をキャンセル
                        return false;
                    }

                    // 明細部の登録
                    if (!DataUpdater.UpdateOrInsertDetails(this.製品明細1.Detail, cn, "M製品明細", strwhere, "製品コード", transaction))
                    {
                        transaction.Rollback();  // 変更をキャンセル
                        return false;
                    }

                    // 前版データの更新処理
                    if (updatePreEdition)
                    {
                        string sql = "";
                        if (IsApproved) // 改版データを承認した場合
                        {
                            sql = $"UPDATE M製品 SET 無効日時=GETDATE(), 無効者コード='{承認者コード.Text}' WHERE 製品コード='{codeString}' AND 製品版数={editionNumber - 1}";
                        }
                        else // 改版データを承認しない場合
                        {
                            sql = $"UPDATE M製品 SET 無効日時=NULL, 無効者コード=NULL WHERE 製品コード='{codeString}' AND 製品版数={editionNumber - 1}";
                        }

                        using (SqlCommand cmd = new SqlCommand(sql, cn, transaction))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }

                    // 承認登録時の処理
                    if (IsApproved)
                    {
                        // 対象製品の型式マスタを更新する
                        if (!UpdateModelMaster(CurrentCode, CurrentEdition,cn,transaction))
                        {
                            transaction.Rollback(); // 変更をキャンセル
                            return false;
                        }
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

        private bool UpdateModelMaster(string productCode, int productEdition, SqlConnection cn, SqlTransaction transaction)
        {
            var result = false;
            
            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;
                    cmd.Transaction = transaction;

                    // 旧データを削除する
                    string strSQLDelete = "DELETE FROM M製品型式 WHERE 製品コード = @ProductCode AND 製品版数 = @ProductEdition";
                    cmd.CommandText = strSQLDelete;
                    cmd.Parameters.AddWithValue("@ProductCode", productCode);
                    cmd.Parameters.AddWithValue("@ProductEdition", productEdition);
                    cmd.ExecuteNonQuery();

                    // 型式一覧を取得する
                    //string strSQLSelect = "SELECT 型式名 FROM 製品明細 WHERE 削除対象 = FALSE ORDER BY 型式名 DESC";
                    //cmd.CommandText = strSQLSelect;
                    //using (SqlDataReader reader = cmd.ExecuteReader())
                    //{

                    if (製品明細1.Detail.RowCount < 1)
                    {
                        Console.WriteLine("製品マスタの明細データがありません。不正なデータです。");
                        return result;
                    }

                    // 製品型式マスタに登録
                    string strSQLInsert = "INSERT INTO M製品型式 (製品コード, 製品版数, 明細番号, 型式名) VALUES (@ProductCode, @ProductEdition, @DetailNumber, @ModelName)";
                    cmd.CommandText = strSQLInsert;
                    int detailNumber = 0;
                    string modelName = "";


                    for (int i = 0; i < 製品明細1.Detail.RowCount; i++)
                    {
                        if (製品明細1.Detail.Rows[i].IsNewRow == true)
                        {
                            //新規行の場合は、処理をスキップ
                            continue;
                        }

                        if(string.IsNullOrEmpty(製品明細1.Detail.Rows[i].Cells["削除対象"].Value?.ToString()))
                        {
                            製品明細1.Detail.Rows[i].Cells["削除対象"].Value = false;
                        }

                        if((bool)製品明細1.Detail.Rows[i].Cells["削除対象"].Value)
                        {
                            continue;
                        }
                        
                        string currentModelName = 製品明細1.Detail.Rows[i].Cells["型式名"].Value.ToString();


                        if (currentModelName != modelName)
                        {
                            detailNumber++;
                            modelName = currentModelName;

                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@ProductCode", productCode);
                            cmd.Parameters.AddWithValue("@ProductEdition", productEdition);
                            cmd.Parameters.AddWithValue("@DetailNumber", detailNumber);
                            cmd.Parameters.AddWithValue("@ModelName", modelName);

                            cmd.ExecuteNonQuery();
                        }

                    }

              

                    result = true; // 成功
                }
            }
            catch (Exception ex)
            {
                // エラーハンドリング
                Console.WriteLine($"UpdateModelMaster - {ex.Message}");
            }

            return result;
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
                    if (コマンド読込.Enabled) コマンド読込_Click(sender, e);
                    break;
                case Keys.F3:
                    if (コマンド複写.Enabled) コマンド複写_Click(sender, e);
                    break;
                case Keys.F4:
                    if (コマンド削除.Enabled) コマンド削除_Click(sender, e);
                    break;
                case Keys.F5:
                    if (コマンドユニット.Enabled) コマンドユニット_Click(sender, e);
                    break;
                case Keys.F6:
                    if (コマンドユニット表.Enabled) コマンドユニット表_Click(sender, e);
                    break;
                case Keys.F7:
                    if (コマンド廃止.Enabled) コマンド廃止_Click(sender, e);
                    break;
                case Keys.F8:
                    if (コマンドツール.Enabled) コマンドツール_Click(sender, e);
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

        private void 改版ボタン_Click(object sender, EventArgs e)
        {

            FunctionClass fn = new FunctionClass();
            try
            {
                bool isErrorOccurred = false;

                // 製品が廃止されているときは続行意思を確認する
                if (!string.IsNullOrEmpty(SupersededDate.Text))
                {
                    if (MessageBox.Show("この製品は廃止されています。" + Environment.NewLine +
                                        "続行しますか？", "改版", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }


                
                fn.DoWait("改版しています...");

                CommonConnect();

                if (CopyData(this.CurrentCode, this.CurrentEdition + 1) && ClearHistory())
                {
                    // データ変更とする
                    ChangedData(true);
                    // ヘッダ部制御
                    FunctionClass.LockData(this, false);
                    this.品名.Focus();
                    this.改版ボタン.Enabled = false;
                    this.コマンド新規.Enabled = false;
                    this.コマンド読込.Enabled = true;
                    this.コマンド承認.Enabled = false;
                    // 明細部制御
                    製品明細1.Detail.AllowUserToAddRows = true;
                    製品明細1.Detail.AllowUserToDeleteRows = true;
                    製品明細1.Detail.ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_改版ボタン_Click - " + ex.Message);
                MessageBox.Show("エラーが発生しました。", BASE_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            finally
            {
                fn.WaitForm.Close();
            }
        }

        private bool CopyData(string codeString, int editionNumber)
        {
            try
            {


                for (int i = 0; i < 製品明細1.Detail.RowCount; i++)
                {
                    if (製品明細1.Detail.Rows[i].IsNewRow == true)
                    {
                        //新規行の場合は、処理をスキップ
                        continue;
                    }

                    製品明細1.Detail.Rows[i].Cells["製品コード"].Value = codeString;
                    製品明細1.Detail.Rows[i].Cells["製品版数"].Value = editionNumber;


                }


                // 表示情報の更新
                this.製品コード.Text = codeString;
                this.製品版数.Text = editionNumber.ToString();
                this.指導書変更.Checked = false;
                this.状態.Text = null;
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
                this.承認者名.Text = null;
                this.廃止.Text = null;
                this.SupersededDate = null;

                return true;
            }
            catch (Exception ex)
            {
                // エラーハンドリング
                Debug.Print($"{this.Name}_CopyData - {ex.Message}");
                return false;
            }
        }
        private bool ClearHistory()
        {
            try
            {

                if (製品明細1.Detail.RowCount == 0)
                {
                    return true;
                }

                int lngi = 1; // 明細番号の初期化

                for (int i = 0; i < 製品明細1.Detail.RowCount; i++)
                {
                    if (製品明細1.Detail.Rows[i].IsNewRow == true)
                    {
                        //新規行の場合は、処理をスキップ
                        continue;
                    }

                    if(string.IsNullOrEmpty(製品明細1.Detail.Rows[i].Cells["削除対象"].Value?.ToString()))
                        {
                            製品明細1.Detail.Rows[i].Cells["削除対象"].Value = false;
                        }

                    if (Convert.ToBoolean(製品明細1.Detail.Rows[i].Cells["削除対象"].Value))
                    {
                        製品明細1.Detail.Rows.RemoveAt(i);
                    }
                    else
                    {
                        製品明細1.Detail.Rows[i].Cells["明細番号"].Value = lngi;
                        製品明細1.Detail.Rows[i].Cells["変更操作コード"].Value = null;
                        製品明細1.Detail.Rows[i].Cells["変更内容"].Value = null;
                        lngi++;
                    }
                }


                return true;
            }
            catch (Exception ex)
            {
                // エラーハンドリング
                Debug.Print($"{this.Name}_ClearHistory - {ex.Message}");
                return false;
            }
        }
        private void 変更ボタン_Click(object sender, EventArgs e)
        {
            FunctionClass.LockData(this, false);
        }

        private void コマンド読込_Click(object sender, EventArgs e)
        {
            try
            {

                Connect();

                this.SuspendLayout();

                if (!this.IsDirty)
                {
                    // 新規モードで且つコードが取得済みのときはコードを戻す
                    if (this.IsNewData && this.CurrentCode != "" && this.CurrentEdition == 1)
                    {
                        // 採番された番号を戻す
                        if (!FunctionClass.ReturnCode(cn, "PRO" + this.CurrentCode))
                        {
                            MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine + Environment.NewLine +
                                            "製品コード　：　" + this.CurrentCode, "修正コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }

                    // 修正モードへ移行する
                    if (!GoModifyMode())
                    {
                        if (MessageBox.Show("エラーが発生しました。" + Environment.NewLine +
                                    "管理者に連絡してください。" + Environment.NewLine + Environment.NewLine +
                                    "強制終了しますか？", "修正コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                        {
                            this.ResumeLayout();
                            this.Close();
                        }
                        else
                        {
                            this.ResumeLayout();
                            return;
                        }
                    }

                    goto Bye_コマンド読込_Click;
                }

                // 修正されているときは登録確認を行う
                DialogResult result = MessageBox.Show("変更内容を登録しますか？", "修正コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                switch (result)
                {
                    case DialogResult.Yes:
                        // エラーチェック
                        if (!ErrCheck())
                        {
                            return;
                        }

                        // 登録処理
                        if (!SaveData())
                        {
                            MessageBox.Show("エラーのため登録できません。", "修正コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        break;

                    case DialogResult.No:
                        // 新規モードで且つコードが取得済みのときはコードを戻す
                        if (this.IsNewData && this.CurrentCode != "" && this.CurrentEdition == 1)
                        {
                            // 採番された番号を戻す
                            if (!FunctionClass.ReturnCode(cn, "PRO" + this.CurrentCode))
                            {
                                MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine + Environment.NewLine +
                                                "製品コード　：　" + this.CurrentCode, "修正コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                        break;

                    case DialogResult.Cancel:
                        return;
                }

                // 修正モードへ移行する
                if (!GoModifyMode())
                {
                    if (MessageBox.Show("エラーが発生しました。" + Environment.NewLine +
                                    "管理者に連絡してください。" + Environment.NewLine + Environment.NewLine +
                                    "強制終了しますか？", "修正コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    {
                        this.ResumeLayout();
                        this.Close();
                    }
                    else
                    {
                        this.ResumeLayout();
                        return;
                    }
                }

            Bye_コマンド読込_Click:
                this.ResumeLayout();
                return;
            }
            catch (Exception ex)
            {
                // エラーハンドリング
                Debug.Print(this.Name + "_コマンド読込_Click - " + ex.Message);

                if (MessageBox.Show("エラーが発生しました。" + Environment.NewLine +
                                    "管理者に連絡してください。" + Environment.NewLine + Environment.NewLine +
                                    "強制終了しますか？", "修正コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                {
                    this.ResumeLayout();
                    this.Close();
                }
                else
                {
                    this.ResumeLayout();
                    return;
                }
            }
        }

        private void コマンド廃止_Click(object sender, EventArgs e)
        {
            try
            {

                Connect();

                if (!this.IsApproved || (this.CurrentEdition != Convert.ToInt32(this.製品版数.GetItemText(this.製品版数.Items[0]))))
                {
                    MessageBox.Show("承認されている最新版でなければ廃止できません。", "廃止", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                // 認証処理
                string strHeadCode = CommonConstants.USER_CODE_TECH; // 承認者を指定する

                // ログオンユーザーが指定ユーザーなら認証者コードにユーザーコードを設定する
                if (CommonConstants.LoginUserCode != strHeadCode)
                {
                    using (var authenticationForm = new F_認証())
                    {
                        authenticationForm.args = strHeadCode;
                        authenticationForm.ShowDialog();

                        if (string.IsNullOrEmpty(CommonConstants.strCertificateCode))
                        {
                            MessageBox.Show("認証できません。" + Environment.NewLine + "承認できません。", "承認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }


                FunctionClass fn = new FunctionClass();
                fn.DoWait("廃止しています...");

                // 値を退避させる
                var var1 = this.SupersededDate.Text;
                var var2 = this.廃止.Text;

                // 値をセットする
                if (this.IsAbolished)
                {
                    this.SupersededDate.Text = null;
                    this.廃止.Text = null;
                }
                else
                {
                    this.SupersededDate.Text = FunctionClass.GetServerDate(cn).ToString("yyyy/MM/dd");
                    this.廃止.Text = "-1";
                }

                if (SaveData())
                {

                    // 版数のソース更新
                    UpdateEditionList(this.CurrentCode);
                    ChangedData(false);
                }
                else
                {
                    this.SupersededDate.Text = var1;
                    this.廃止.Text = var2;
                    MessageBox.Show("登録できませんでした。", "製品", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                fn.WaitForm.Close();

            }
            catch (Exception ex)
            {
                // エラーハンドリング
                Debug.Print($"{this.Name}_コマンド廃止_Click - {ex.Message}");
            }
        }

        private void コマンド確定_Click(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();
            Connect();

            try
            {
                object varSaved1 = null;  // 確定日時保存用（エラー発生時の対策）
                object varSaved2 = null;  // 確定者コード保存用（エラー発生時の対策）


                fn.DoWait("確定しています...");

                // エラーチェック
                if (!ErrCheck())
                {
                    return;
                };

                
                

                // 登録前の確定日を保存しておく
                varSaved1 = 確定日時.Text;
                varSaved2 = 確定者コード.Text;

                // 確定情報を設定する
                if (IsDecided)
                {
                    確定日時.Text = null;
                    確定者コード.Text = null;
                }
                else
                {
                    確定日時.Text = FunctionClass.GetServerDate(cn).ToString("yyyy/MM/dd");
                    確定者コード.Text = CommonConstants.LoginUserCode;
                }

                // 登録する
                if (SaveData())
                {

                    // 版数のソース更新
                    UpdateEditionList(CurrentCode);

                    
                    FunctionClass.LockData(this, IsDecided || IsDeleted, "製品コード", "製品版数");

                    // RoHS対応状況を表示する
                    RoHS対応.Text = GetRohsStatus();

                    

                    // 新規モードのときは修正モードへ移行する
                    if (IsNewData)
                    {
                        コマンド新規.Enabled = true;
                        コマンド読込.Enabled = false;
                    }

                    コマンド承認.Enabled = IsDecided;

                    製品明細1.Detail.AllowUserToAddRows = !IsDecided;
                    製品明細1.Detail.AllowUserToDeleteRows = !IsDecided;
                    製品明細1.Detail.ReadOnly = IsDecided;

                    ChangedData(false);
                }
                else
                {
                    確定日時.Text = varSaved1.ToString();
                    確定者コード.Text = varSaved2.ToString();
                    MessageBox.Show("登録できませんでした。", "確定コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"{Name}_コマンド確定_Click - {ex.GetType().Name}: {ex.Message}");
                MessageBox.Show("エラーが発生したため、確定できませんでした。", "確定コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                fn.WaitForm.Close();
            }
        }

        private void コマンド承認_Click(object sender, EventArgs e)
        {

            FunctionClass fn = new FunctionClass();
            Connect();

            try
            {
                string var1 = null;
                string var2 = null;
                string var3 = null;


                // 承認を取り消す場合は確認する
                if (IsApproved)
                {
                    if (MessageBox.Show("承認を取り消します。\nこの版数の製品が既に購買されている場合、\n購買データに障害が発生する可能性があります。\n\n続行しますか？", "承認コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;
                }

                // 認証処理
                string strHeadCode = CommonConstants.USER_CODE_TECH; // 承認者を指定する

                // ログオンユーザーが指定ユーザーなら認証者コードにユーザーコードを設定する
                if (CommonConstants.LoginUserCode != strHeadCode)   
                {
                    using (var authenticationForm = new F_認証())
                    {
                        authenticationForm.args = strHeadCode;
                        authenticationForm.ShowDialog();

                        if (string.IsNullOrEmpty(CommonConstants.strCertificateCode))
                        {
                            MessageBox.Show("認証できません。" + Environment.NewLine + "承認できません。", "承認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }


                fn.DoWait("承認しています...");

                // 値を退避させる
                var1 = 承認日時.Text;
                var2 = 承認者コード.Text;
                var3 = 承認者名.Text;

                // 値をセットする
                if (IsApproved)
                {
                    承認日時.Text = null;
                    承認者コード.Text = null;
                    承認者名.Text = null;
                }
                else
                {
                    承認日時.Text = FunctionClass.GetServerDate(cn).ToString();
                    承認者コード.Text = CommonConstants.strCertificateCode;
                    承認者名.Text = FunctionClass.GetUserFullName(cn, CommonConstants.strCertificateCode);
                }
         

                // サーバーへ登録する
                if (RegTrans(CurrentCode, CurrentEdition, 1 < CurrentEdition))
                {
                    // 版数のソース更新
                    UpdateEditionList(CurrentCode);

                    // 承認されたときは製品型式マスタを更新する
                    //RegTrans内で行っているのでコメントアウト
                    //if (IsApproved)
                    //{
                    //    if (!UpdateModelMaster(CurrentCode, CurrentEdition, cn,transaction))
                    //        fn.WaitForm.Close();
                    //    return;
                    //}

                    SetEditionStatus(); // 状態の表示
                    ChangedData(false);
                    // 新規モードで承認することはできない仕様とする。
                    コマンド確定.Enabled = !IsApproved;
                    改版ボタン.Enabled = IsApproved;
                }
                else
                {
                    承認日時.Text = var1;
                    承認者コード.Text = var2;
                    承認者名.Text = var3;
                    MessageBox.Show("登録できませんでした。", "承認コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }


                fn.WaitForm.Close();
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_コマンド承認_Click - " + ex.Message);
            }

        }


        private void コマンド複写_Click(object sender, EventArgs e)
        {

            FunctionClass fn = new FunctionClass();
            Connect();

            try
            {

                fn.DoWait("複写しています...");


                // 複写に成功すればインターフェースを更新する
                if (CopyData(Right(FunctionClass.採番(cn, "PRO"), 8), 1) && ClearHistory())
                {
                    // データ変更とする
                    ChangedData(true);
                    // ヘッダ部制御
                    FunctionClass.LockData(this, false);

                    // ■ 値集合ソースをクリアする必要はないのか？

                    品名.Focus();
                    改版ボタン.Enabled = false;
                    コマンド新規.Enabled = false;
                    コマンド読込.Enabled = true;
                    コマンド承認.Enabled = false;

                    // 明細部制御
                    製品明細1.Detail.AllowUserToAddRows = true;
                    製品明細1.Detail.AllowUserToDeleteRows = true;
                    製品明細1.Detail.ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                // エラーメッセージボックスを表示
                MessageBox.Show($"{ex.GetType().Name}: {ex.Message}", "複写コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                fn.WaitForm.Close();
            }

        }


        private void コマンド登録_Click(object sender, EventArgs e)
        {

            FunctionClass fn = new FunctionClass();
            Connect();

            try
            {

                fn.DoWait("登録しています...");

                if (SaveData())
                {

                    // 版数のソース更新
                    UpdateEditionList(CurrentCode);
                    // 製品版数.Requery();

                    ChangedData(false);

                    // RoHS対応状況を表示する
                    RoHS対応.Text = GetRohsStatus();


                    // 新規モードのときは修正モードへ移行する
                    if (IsNewData)
                    {
                        コマンド新規.Enabled = true;
                        コマンド読込.Enabled = false;

                        OriginalClass ofn = new OriginalClass();
                        ofn.SetComboBox(製品コード, "SELECT A.製品コード as Value, A.製品コード as Display, A.最新版数 as Display3, { fn REPLACE(STR(CONVERT(bit, M製品.無効日時), 1, 0), '1', '×') } AS Display2 FROM M製品 INNER JOIN (SELECT 製品コード, MAX(製品版数) AS 最新版数 FROM M製品 GROUP BY 製品コード) A ON M製品.製品コード = A.製品コード AND M製品.製品版数 = A.最新版数 ORDER BY A.製品コード DESC");
                    }

                    if (!IsApproved)
                    {
                        コマンド確定.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("登録できませんでした。", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                // エラーメッセージボックスを表示
                MessageBox.Show($"{ex.GetType().Name}: {ex.Message}", "登録コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                fn.WaitForm.Close();
            }

        }



        private void コマンド削除_Click(object sender, EventArgs e)
        {
            Connect();

            try
            {
                // エラーハンドリングはC#のtry-catch構文を使用する
                string strHeadCode;       // 部長の社員コード
                object varSaved1 = null;  // 無効日時保存用
                object varSaved2 = null;  // 無効者コード保存用

                if (IsApproved)
                {
                    // 承認されているときは版数に関係なくデータを無効化する
                    DialogResult intRes = MessageBox.Show(
                        $"製品コード　：　{CurrentCode}{Environment.NewLine}" +
                        $"版数　：　{CurrentEdition}{Environment.NewLine}{Environment.NewLine}" +
                        $"この承認済み製品データを削除します。{Environment.NewLine}" +
                        $"削除後参照のみ可能となります。{Environment.NewLine}" +
                        $"承認済みデータを削除すると運用上問題が生じることがあります。{Environment.NewLine}{Environment.NewLine}" +
                        $"削除しますか？",
                        "削除コマンド",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (intRes == DialogResult.No)
                        return;

                    // 認証処理
                    strHeadCode = CommonConstants.USER_CODE_TECH; // 承認者を指定する

                    // ログオンユーザーが指定ユーザーなら認証者コードにユーザーコードを設定する
                    if (CommonConstants.LoginUserCode != strHeadCode)
                    {
                        using (var authenticationForm = new F_認証())
                        {
                            authenticationForm.args = strHeadCode;
                            authenticationForm.ShowDialog();

                            if (string.IsNullOrEmpty(CommonConstants.strCertificateCode))
                            {
                                MessageBox.Show("認証に失敗しました。" + Environment.NewLine + "実行できません。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }
                    }

                    // 削除情報設定
                    varSaved1 = 無効日時.Text;
                    varSaved2 = 無効者コード.Text;

                    if (IsDeleted)
                    {
                        無効日時.Text = null;
                        無効者コード.Text = null;
                    }
                    else
                    {
                        // ここにGetServerDateのC#版の処理を追加
                        無効日時.Text = FunctionClass.GetServerDate(cn).ToString();
                        無効者コード.Text = CommonConstants.strCertificateCode;
                    }
              

                    // 表示データを登録する
                    if (RegTrans(CurrentCode, CurrentEdition,false))
                    {
                        if (IsDeleted)
                            MessageBox.Show("削除されました。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        else
                            MessageBox.Show("削除は取り消されました。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // 登録失敗
                        無効日時.Text = varSaved1.ToString();
                        無効者コード.Text = varSaved2.ToString();
                        MessageBox.Show("エラー発生により処理は取り消されました。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    // 承認されていないときは版数により処理が異なる
                    // 対象データが確定されているときは意思を確認する
                    if (IsDecided)
                    {
                        if (MessageBox.Show(
                            "この製品データは確定されています。" + Environment.NewLine +
                            "通常、確定済みのデータを削除することはありません。" + Environment.NewLine + Environment.NewLine +
                            "続行しますか？",
                            "削除コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                    }

                    // 他のユーザーによって承認されているかどうか確認する
                    if (IsApprovedS(cn, CurrentCode, CurrentEdition))
                    {
                        MessageBox.Show("このデータは他のユーザーにより承認されたため、削除できません。",
                            "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    if (CurrentEdition == 1)
                    {
                        // 指定版数データを完全削除する
                        if (MessageBox.Show(
                            $"製品コード　：　{CurrentCode}{Environment.NewLine}" +
                            $"版数　：　{CurrentEdition}{Environment.NewLine}{Environment.NewLine}" +
                            $"この製品データを削除します。{Environment.NewLine}" +
                            $"削除後参照することはできません。{Environment.NewLine}" +
                            $"また、この処理を取り消すことはできません。{Environment.NewLine}{Environment.NewLine}" +
                            $"削除しますか？",
                            "削除コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }

                        if (DeleteData(cn, CurrentCode, CurrentEdition))
                        {
                            MessageBox.Show("削除しました。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            // 新規モードへ移行する
                            if (!GoNewMode())
                            {
                                MessageBox.Show($"エラーのため新規モードへ移行できません。{Environment.NewLine}" +
                                                $"[{Name}]画面を終了します。",
                                    "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                Close();
                            }

                        }
                        else
                        {
                            MessageBox.Show("エラー発生により処理は取り消されました。",
                                "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    else
                    {
                        // 指定版数データを完全削除し、前版を有効にする
                        if (MessageBox.Show(
                            $"製品コード　：　{CurrentCode}{Environment.NewLine}" +
                            $"版数　：　{CurrentEdition}{Environment.NewLine}{Environment.NewLine}" +
                            $"この製品データを削除し、前版に戻します。{Environment.NewLine}" +
                            $"この処理を取り消すことはできません。{Environment.NewLine}{Environment.NewLine}" +
                            $"削除しますか？",
                            "削除コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }

                        if (UndoRevise(cn, CurrentCode, CurrentEdition))
                        {
                            MessageBox.Show("改版を取り消しました。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            

                            // 前版を表示する
                            製品版数.Focus();
                            製品版数.Text = (CurrentEdition - 1).ToString();

                            // 版数のソース更新
                            UpdateEditionList(this.CurrentCode);
                        }
                        else
                        {
                            MessageBox.Show("エラー発生により処理は取り消されました。",
                                "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool IsApprovedS(SqlConnection connection, string codeString, int editionNumber)
        {
            bool isApproved;

            using (SqlCommand cmd = new SqlCommand())
            {
                // SQLコマンドの構築
                string strKey = $"製品コード = '{codeString}' AND 製品版数 = {editionNumber} AND 承認日時 IS NULL";
                string strSQL = $"SELECT COUNT(*) FROM M製品 WHERE {strKey}";

                // SQLコマンドを設定
                cmd.CommandText = strSQL;
                cmd.Connection = connection;

                // サーバーへの問い合わせ実行

                int count = (int)cmd.ExecuteScalar();

                // 結果の判定
                isApproved = count == 0;
            }

            return isApproved;
        }

        private bool DeleteData(SqlConnection connection, string codeString, int editionNumber)
        {
            bool success = false;
            SqlTransaction transaction = null;

            try
            {

                // 他のユーザーによって承認されているかどうか確認する
                string strKey = $"製品コード = '{codeString}' AND 製品版数 = {editionNumber} AND 承認日時 IS NULL";
                string strSQL1 = $"SELECT COUNT(*) FROM M製品 WHERE {strKey}";

                using (SqlCommand cmdCheck = new SqlCommand(strSQL1, connection))
                {
                    int count = (int)cmdCheck.ExecuteScalar();

                    if (count > 0)
                    {
                        strKey = $"製品コード = '{codeString}' AND 製品版数 = {editionNumber} ";

                        // 承認されていない場合にのみ削除処理を実行
                        string strSQL2 = $"DELETE FROM M製品 WHERE {strKey}";
                        string strSQL3 = $"DELETE FROM M製品明細 WHERE {strKey}";

                        using (SqlCommand cmdDelete1 = new SqlCommand(strSQL2, connection))
                        using (SqlCommand cmdDelete2 = new SqlCommand(strSQL3, connection))
                        {
                            transaction = connection.BeginTransaction();

                            cmdDelete1.Transaction = transaction;
                            cmdDelete2.Transaction = transaction;

                            // 削除処理実行
                            cmdDelete1.ExecuteNonQuery();
                            cmdDelete2.ExecuteNonQuery();

                            // トランザクションのコミット
                            transaction.Commit();

                            success = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                if (transaction != null)
                {
                    // トランザクションのロールバック
                    transaction.Rollback();
                }

                Console.WriteLine($"DeleteData - Error: {ex.Message}");
            }

            return success;
        }

        private bool UndoRevise(SqlConnection connection, string codeString, int editionNumber)
        {
            bool success = false;
            SqlTransaction transaction = null;

            try
            {

                // 対象版数のデータを削除
                string strKey1 = $"製品コード = '{codeString}' AND 製品版数 = {editionNumber}";
                string strSQL1 = $"DELETE FROM M製品明細 WHERE {strKey1}";
                string strSQL2 = $"DELETE FROM M製品 WHERE {strKey1}";

                // 前版に戻すための更新クエリ
                string strKey2 = $"製品コード = '{codeString}' AND 製品版数 = {editionNumber - 1}";
                string strSQL3 = $"UPDATE M製品 SET 無効日時 = NULL, 無効者コード = NULL WHERE {strKey2}";

                using (SqlCommand cmdDelete1 = new SqlCommand(strSQL1, connection))
                using (SqlCommand cmdDelete2 = new SqlCommand(strSQL2, connection))
                using (SqlCommand cmdUpdate = new SqlCommand(strSQL3, connection))
                {
                    transaction = connection.BeginTransaction();

                    cmdDelete1.Transaction = transaction;
                    cmdDelete2.Transaction = transaction;
                    cmdUpdate.Transaction = transaction;

                    // 削除処理と更新処理を実行
                    cmdDelete1.ExecuteNonQuery();
                    cmdDelete2.ExecuteNonQuery();
                    cmdUpdate.ExecuteNonQuery();

                    // トランザクションのコミット
                    transaction.Commit();

                    success = true;
                }
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                if (transaction != null)
                {
                    // トランザクションのロールバック
                    transaction.Rollback();
                }

                Console.WriteLine($"UndoRevise - Error: {ex.Message}");
            }


            return success;
        }


        private void コマンドツール_Click(object sender, EventArgs e)
        {
            F_製品_ツール targetform = new F_製品_ツール();

            targetform.args = CurrentCode;
            targetform.ShowDialog();
        }
        //未着手
        private void コマンドユニット_Click(object sender, EventArgs e)
        {
            try
            {
                if (selected_frame == 1)
                {
                    //string code = OriginalClass.Nz(仕入先1コード.Text, null);
                    //if (string.IsNullOrEmpty(code))
                    //{
                    //    MessageBox.Show("仕入先1を入力してください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //    仕入先1コード.Focus();
                    //}
                    //else
                    //{
                    //    F_仕入先 targetform = new F_仕入先();

                    //    targetform.args = code;
                    //    targetform.ShowDialog();
                    //}
                }
                else if (selected_frame == 2)
                {
                    //string code = OriginalClass.Nz(仕入先2コード.Text, null);
                    //if (string.IsNullOrEmpty(code))
                    //{
                    //    MessageBox.Show("仕入先2を入力してください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //    仕入先2コード.Focus();
                    //}
                    //else
                    //{
                    //    F_仕入先 targetform = new F_仕入先();

                    //    targetform.args = code;
                    //    targetform.ShowDialog();
                    //}
                }
                else if (selected_frame == 3)
                {
                    //string code = OriginalClass.Nz(仕入先3コード.Text, null);
                    //if (string.IsNullOrEmpty(code))
                    //{
                    //    MessageBox.Show("仕入先3を入力してください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //    仕入先3コード.Focus();
                    //}
                    //else
                    //{
                    //    F_仕入先 targetform = new F_仕入先();

                    //    targetform.args = code;
                    //    targetform.ShowDialog();
                    //}
                }
                else
                {
                    MessageBox.Show("参照する仕入先を選択してください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in コマンドユニット_Click: " + ex.Message);
            }
        }

        private void コマンドユニット表_Click(object sender, EventArgs e)
        {
            IReport paoRep = ReportCreator.GetPreview();

            paoRep.LoadDefFile("../../../Reports/ユニット表.prepd");

            Connect();

            DataRowCollection Vユニット表;

            string sqlQuery = "SELECT * FROM Vユニット表 where 製品コード='" + CurrentCode + "' and 製品版数=" + CurrentEdition + " ORDER BY 明細番号";

            using (SqlCommand command = new SqlCommand(sqlQuery, cn))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataSet dataSet = new DataSet();

                    adapter.Fill(dataSet);

                    Vユニット表 = dataSet.Tables[0].Rows;

                }
            }

            //最大行数
            int maxRow = 49;
            //現在の行
            int CurRow = 0;
            //行数
            int RowCount = maxRow;
            if (Vユニット表.Count > 0)
            {
                RowCount = Vユニット表.Count;
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
                paoRep.Write("製品コード",製品コード.Text != "" ? 製品コード.Text : " ");
                paoRep.Write("製品版数", 製品版数.Text != "" ? 製品版数.Text : " ");
                paoRep.Write("製品名", 品名.Text != "" ? 品名.Text : " ");
                paoRep.Write("シリーズ名", シリーズ名.Text != "" ? シリーズ名.Text : " ");

                if(製品明細1.Detail.SortOrder != 0)
                {
                    paoRep.Write("確認表示","（確認用）");
                }
                else
                {
                    paoRep.Write("確認表示", "");
                }

                if (string.IsNullOrEmpty(識別コード.Text))
                {
                    paoRep.Write("指導所番号", " ");
                }
                else
                {
                    string 指導書番号 = $"{識別コード.Text}-04{page:D2}";
                    paoRep.Write("指導書番号", 指導書番号);
                }

                //フッダー
                paoRep.Write("出力日時", "出力日時：" + now.ToString("yyyy/MM/dd HH:mm:ss"));
                paoRep.Write("ページ", ("ページ： " + page + "/" + maxPage).ToString());

                //明細
                for (var i = 0; i < maxRow; i++)
                {
                    if (CurRow >= Vユニット表.Count) break;

                    DataRow targetRow = Vユニット表[CurRow];

                    paoRep.Write("明細番号", targetRow["明細番号"].ToString() != "" ? targetRow["明細番号"].ToString() : " ", i + 1);
                    paoRep.Write("型式名", targetRow["型式名"].ToString() != "" ? targetRow["型式名"].ToString() : " ", i + 1);
                    paoRep.Write("ユニットコード", targetRow["ユニットコード"].ToString() != "" ? targetRow["ユニットコード"].ToString() : " ", i + 1);
                    paoRep.Write("ユニット版数", targetRow["ユニット版数"].ToString() != "" ? targetRow["ユニット版数"].ToString() : " ", i + 1);
                    paoRep.Write("品名", targetRow["品名"].ToString() != "" ? targetRow["品名"].ToString() : " ", i + 1);
                    paoRep.Write("型番", targetRow["型番"].ToString() != "" ? targetRow["型番"].ToString() : " ", i + 1);
                    paoRep.Write("ユニット指導書番号", targetRow["ユニット指導書番号"].ToString() != "" ? targetRow["ユニット指導書番号"].ToString() : " ", i + 1);


                    paoRep.z_Objects.SetObject("型式名", i + 1);
                    lenB = Encoding.Default.GetBytes(targetRow["型式名"].ToString()).Length;
                    if (10 < lenB)
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 6;
                    }
                    else
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 9;
                    }

                    paoRep.z_Objects.SetObject("品名", i + 1);
                    lenB = Encoding.Default.GetBytes(targetRow["品名"].ToString()).Length;
                    if (28 < lenB)
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 6;
                    }
                    else
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 9;
                    }

                    paoRep.z_Objects.SetObject("型番", i + 1);
                    lenB = Encoding.Default.GetBytes(targetRow["型番"].ToString()).Length;
                    if (26 < lenB)
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 7;
                    }
                    else
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 9;
                    }

                    paoRep.z_Objects.SetObject("ユニット指導書番号", i + 1);
                    lenB = Encoding.Default.GetBytes(targetRow["ユニット指導書番号"].ToString()).Length;
                    if (26 < lenB)
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 6;
                    }
                    else
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 8;
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

        private bool InvalidateData(string codeString, int editionNumber)
        {
            try
            {
                Connect();

                // 既に無効日時が設定されているときは、無効にすることはできない。
                if (無効日時.Text != null)
                    return false;

                無効日時.Text = FunctionClass.GetServerDate(cn).ToString();


        
                if (RegTrans(codeString, editionNumber,false))
                {
                    return true;
                }
                else
                {
                    無効日時.Text = null;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_InvalidateData - " + ex.Message);
                return false; // 例外発生時は適切なエラー処理を行ってください
            }
        }

        private bool AskSave(int response)
        {
            try
            {
                response = 0;

                Connect();

                DialogResult result = MessageBox.Show("変更内容を登録しますか？", "質問", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                switch (result)
                {
                    case DialogResult.Yes:
                        // エラーチェック
                        if (!ErrCheck())
                            return false;

                        // 登録処理
                        if (!SaveData())
                        {
                            MessageBox.Show("エラーのため登録できません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return false;
                        }
                        break;
                    case DialogResult.No:
                        // 新規モードでかつコードが取得済みのときはコードを戻す
                        if (IsNewData && !string.IsNullOrEmpty(CurrentCode) && CurrentEdition == 1)
                        {
                            // 採番された番号を戻す
                            if (!FunctionClass.ReturnCode(cn, "PRO" + CurrentCode))
                            {
                                MessageBox.Show("エラーのためコードは破棄されました。\n\n製品コード　：　" + CurrentCode, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return false;
                            }
                        }
                        break;
                    case DialogResult.Cancel:
                        return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.Print("AskSave - " + ex.Message);
                return false;
            }
        }

        private bool SavedRevisedEdition(string codeString, int editionNumber)
        {
            Connect();

            try
            {
                // 改版データ登録時の処理

                string strKey = $"製品コード='{codeString}' AND 製品版数={editionNumber - 1} AND 無効日時 IS NULL";
                string strSQL = $"SELECT COUNT(*) FROM M製品 WHERE {strKey}";

                using (SqlCommand command = new SqlCommand(strSQL, cn))
                {
                    int rowCount = Convert.ToInt32(command.ExecuteScalar());

                    strKey = $"製品コード='{codeString}' AND 製品版数={editionNumber - 1}";

                    // 承認されたとき
                    if (IsApproved)
                    {
                        if (rowCount > 0)
                        {
                            cn.Open();
                            using (SqlCommand updateCommand = new SqlCommand($"UPDATE M製品 SET 無効日時=GETDATE() WHERE {strKey}", cn))
                            {
                                updateCommand.ExecuteNonQuery();
                            }
                        }
                    }
                    else
                    {
                        // 承認が取り消されたとき
                        if (rowCount == 0)
                        {
                            cn.Open();
                            using (SqlCommand updateCommand = new SqlCommand($"UPDATE M製品 SET 無効日時=NULL WHERE {strKey}", cn))
                            {
                                updateCommand.ExecuteNonQuery();
                            }
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.Print($"{this.Name}_SavedRevisedEdition - {ex.GetType().Name} : {ex.Message}");
                return false;
            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }
        }

        // 各コントロールの処理


        private void 製品コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {

                string strCode = 製品コード.Text.ToString();
                string formattedCode = strCode.Trim().PadLeft(8, '0');

                if (formattedCode != strCode || string.IsNullOrEmpty(strCode))
                {
                    製品コード.Text = formattedCode;
                }

            }
        }
        private void 製品コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            製品版数.Text = ((DataRowView)製品コード.SelectedItem)?.Row.Field<Int16>("Display3").ToString();
            UpdatedControl(sender as Control);
        }

        private void 製品コード_TextChanged(object sender, EventArgs e)
        {
            if (製品コード.SelectedValue == null)
            {
                製品版数.Text = null;
            }
            FunctionClass.LimitText(sender as Control, 8);
        }

        private void 製品コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 100, 30 }, new string[] { "Display", "Display2" });
            製品コード.Invalidate();
            製品コード.DroppedDown = true;
        }

        private void SeriesCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            シリーズ名.Text = ((DataRowView)SeriesCode.SelectedItem)?.Row.Field<String>("Display2")?.ToString();

            UpdatedControl(sender as Control);
        }

        private void SeriesCode_TextChanged(object sender, EventArgs e)
        {
            if (SeriesCode.SelectedValue == null)
            {
                シリーズ名.Text = null;
            }

            FunctionClass.LimitText(sender as Control, 8);

            ChangedData(true);
        }

        private void SeriesCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {

                string strCode = SeriesCode.ToString();
                string formattedCode = strCode.Trim().PadLeft(8, '0');

                if (formattedCode != strCode || string.IsNullOrEmpty(strCode))
                {
                    SeriesCode.Text = formattedCode;
                }
                
            }
        }

        private void SeriesCode_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 100, 100 }, new string[] { "Display", "Display2" });
            SeriesCode.Invalidate();
            SeriesCode.DroppedDown = true;
        }

        private void 製品版数_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 30, 30 }, new string[] { "Display", "Display2" });
            製品版数.Invalidate();
            製品版数.DroppedDown = true;
        }

        private void 製品版数_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 製品版数_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 5);
        }

        private void シリーズ名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 30);
            ChangedData(true);
        }

        private void シリーズ名_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (シリーズ名.Modified == false) return;

            if (IsError(sender as Control) == true) e.Cancel = true;


        }

        private void 識別コード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 20);
            ChangedData(true);
        }

        private void 識別コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (識別コード.Modified == false) return;

            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 廃止_Validated(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 備考_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 400);
            ChangedData(true);
        }

        private void 品名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 60);
            ChangedData(true);
        }

        private void 品名_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

            if (品名.Modified == false)
                return;
            if (IsError(sender as Control) == true) e.Cancel = true;

        }

        private void 確定日時_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(確定日時.Text))
            {
                確定表示.SendToBack();
            }
            else
            {
                確定表示.BringToFront();
            }
        }

        private void 承認日時_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(承認日時.Text))
            {
                承認表示.SendToBack();
            }
            else
            {
                承認表示.BringToFront();
            }
        }


        private void SupersededDate_TextChanged(object sender, EventArgs e)
        {
            if (SupersededDate == null || string.IsNullOrEmpty(SupersededDate.Text))
            {
                廃止表示.SendToBack();
            }
            else
            {
                廃止表示.BringToFront();
            }
        }

        private void 製品コード_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■読み込む製品データのコードを入力します。　■半角８文字まで入力でき、上位０は省略可能です。";
        }

        private void 製品コード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 製品版数_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■参照する受注データの版数を選択します。　■入力欄から改版することはできません。";
        }

        private void 製品版数_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 品名_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■カタログ等に記載される製品の品名を入力します。　■全角３０文字まで入力できます。";
        }

        private void 品名_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void シリーズ名_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■製品のシリーズ名を入力します。シリーズ名は機種名とも呼ばれます。　■半角３０文字まで入力できます。";
        }

        private void シリーズ名_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 識別コード_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■指導書No.の一部を入力します。　■半角20文字まで入力できます。";
        }

        private void 識別コード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 備考_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "全角２００文字まで入力できます。";
        }

        private void 備考_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }
    }
}
