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
using Pao.Reports;
using GrapeCity.Win.MultiRow;
using System.Text;

namespace u_net
{
    public partial class F_ユニット : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "ユニット";
        private int selected_frame = 0;
        public bool IsDirty = false;
        int intWindowHeight ;
        int intWindowWidth ;
        bool copyflg = false;
        bool saveflg = false;

        public F_ユニット()
        {
            this.Text = "ユニット";       // ウィンドウタイトルを設定
            this.MaximizeBox = false;  // 最大化ボタンを無効化
            this.MinimizeBox = false; //最小化ボタンを無効化

            InitializeComponent();

            this.ユニットコード.DropDownWidth = 110;
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
                return 0 < ユニット明細1.Detail.RowCount;
            }
        }

        public string CurrentCode
        {
            get
            {
                return Nz(ユニットコード.Text);
            }
        }

        public int CurrentEdition
        {
            get
            {
                return string.IsNullOrEmpty(ユニット版数.Text) ? 0 : Int32.Parse(ユニット版数.Text);
            }
        }

        public string CurrentPartsCode
        {
            get
            {
                if (ユニット明細1?.Detail?.CurrentRow != null)
                {
                    // "部品コード"のセルが存在するか確認
                    var cell = ユニット明細1.Detail.CurrentRow.Cells["部品コード"];
                    if (cell != null)
                    {
                        // セルのValueがnullでないか確認
                        if (cell.Value != null)
                        {
                            return cell.Value.ToString();
                        }
                    }
                }
                // 上記のいずれかがnullの場合は、適切なデフォルト値やnullを返す
                return null;

            }
        }

        public bool IsNewData
        {
            get
            {
                return !コマンド新規.Enabled;
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
                int productVersion = string.IsNullOrEmpty(ユニット版数.Text?.ToString()) ? 0 : Int32.Parse(ユニット版数.Text);
                int maxVersion = string.IsNullOrEmpty(((DataRowView)ユニットコード.SelectedItem)?.Row.Field<Int16>("Display3").ToString()) ? 0 : Int32.Parse(((DataRowView)ユニットコード.SelectedItem)?.Row.Field<Int16>("Display3").ToString());
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
            ofn.SetComboBox(ユニットコード, "SELECT A.ユニットコード as Value, A.ユニットコード as Display , A.最新版数 as Display3, { fn REPLACE(STR(CONVERT(bit, Mユニット.無効日時), 1, 0), '1', '×') } AS Display2 FROM Mユニット INNER JOIN (SELECT ユニットコード, MAX(ユニット版数) AS 最新版数 FROM Mユニット GROUP BY ユニットコード) A ON Mユニット.ユニットコード = A.ユニットコード AND Mユニット.ユニット版数 = A.最新版数 ORDER BY A.ユニットコード DESC");
            ユニットコード.DrawMode = DrawMode.OwnerDrawFixed;


            try
            {
                this.SuspendLayout();

                intWindowHeight = this.Height;
                intWindowWidth = this.Width;


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

                    this.ユニット版数.Text = Convert.ToInt32(args.Substring(0, args.IndexOf(","))).ToString();
                    this.ユニットコード.Focus();
                    this.ユニットコード.Text = args.Substring(0, args.IndexOf(","));
                }
                args = null;




                // 成功時の処理
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("指定ユニットを開くことはできません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            finally
            {
                this.ResumeLayout();

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


                this.ユニットコード.Text = Right(FunctionClass.採番(cn, "UNI"), 8);
                this.ユニット版数.Text = 1.ToString();


                // 明細部の初期化
                strSQL = "SELECT * FROM Vユニット明細 WHERE ユニットコード='" +
                             this.CurrentCode + "' AND ユニット版数=" + this.CurrentEdition +
                             " ORDER BY 明細番号";
                VariableSet.SetTable2Details(ユニット明細1.Detail, strSQL, cn);

                // ヘッダ部動作制御
                FunctionClass.LockData(this, false);

                this.品名.Focus();
                this.ユニットコード.Enabled = false;
                this.ユニット版数.Enabled = false;
                this.改版ボタン.Enabled = false;
                this.コマンド新規.Enabled = false;
                this.コマンド読込.Enabled = true;
                this.コマンド複写.Enabled = false;
                this.コマンド削除.Enabled = false;
                this.コマンド部品表.Enabled = false;
                this.コマンド部品定数表.Enabled = false;
                //this.コマンドツール.Enabled = false;
                this.コマンド承認.Enabled = false;
                this.コマンド確定.Enabled = false;
                this.コマンド登録.Enabled = false;

                // 明細部動作制御
                ユニット明細1.Detail.AllowUserToAddRows = true;
                ユニット明細1.Detail.AllowUserToDeleteRows = true;
                ユニット明細1.Detail.ReadOnly = false;

                success = true;
                return success;
            }
            catch (Exception ex)
            {
                Console.WriteLine(this.Name + "_GoNewMode - " + ex.Message);
                return false;
            }
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            try
            {
                this.ユニット明細1.Detail.Height += (this.Height - intWindowHeight);
                this.ユニット明細1.Detail.Width += (this.Width - intWindowWidth);
                intWindowHeight = this.Height;
                intWindowWidth = this.Width;

            }
            catch (Exception ex)
            {
                Debug.Print($"{nameof(Form_Resize)} - {ex.Message}");
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

                ユニット版数.DataSource = null;


                // 明細部の初期化
                strSQL = "SELECT * FROM Vユニット明細 WHERE ユニットコード='" +
                             this.CurrentCode + "' AND ユニット版数=" + this.CurrentEdition +
                             " ORDER BY 明細番号";
                VariableSet.SetTable2Details(ユニット明細1.Detail, strSQL, cn);

                ChangedData(false);

                this.ユニットコード.Focus();

                FunctionClass.LockData(this, true, "ユニットコード");


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
                        if (!FunctionClass.ReturnCode(cn, "UNI" + CurrentCode))
                        {
                            MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine + Environment.NewLine +
                                            "ユニットコード　：　" + CurrentCode, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                            if (!FunctionClass.ReturnCode(cn, "UNI" + CurrentCode))
                            {
                                MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine +
                                                "ユニットコード　：　" + CurrentCode, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                Form unitSelectionForm = Application.OpenForms["F_部品選択"];

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
            if (!FunctionClass.IsError(this.ユニットコード)) return false;
            if (!FunctionClass.IsError(this.ユニット版数)) return false;

            if ((IsDecided && ユニット明細1.Detail.RowCount < 1) || (!IsDecided && ユニット明細1.Detail.RowCount <= 1))
            {
                MessageBox.Show("１つ以上の明細が必要です。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (!FunctionClass.IsError(this.品名)) return false;
            if (!FunctionClass.IsError(this.型番)) return false;
            if (!FunctionClass.IsError(this.識別コード)) return false;

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

            if (ActiveControl == ユニットコード)
            {
                品名.Focus();
            }

            ユニットコード.Enabled = !isChanged;

            if (ActiveControl == ユニット版数)
            {
                品名.Focus();
            }

            IsDirty = isChanged;

            ユニット版数.Enabled = !isChanged;
            コマンド複写.Enabled = !isChanged;
            コマンド削除.Enabled = !isChanged;
            コマンド部品表.Enabled = !isChanged;
            コマンド部品定数表.Enabled = !isChanged;


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
                    case "型番":
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

            //ユニットコードコンボボックスの先頭行が読み込まれないようにするため
            if (copyflg) return;
            if (saveflg) return;

            try
            {
                string strSQL;


                Connect();


                switch (controlObject.Name)
                {
                    case "ユニットコード":
                        fn.DoWait("読み込んでいます...");


                        // 版数のソース更新
                        UpdateEditionList(controlObject.Text);

                        // OpenArgsが設定されていなければ版数を最新版とする
                        // 開いてからコードを変えて読み込むときはOpenArgsはnullに
                        // 設定されているため、最新版となる
                        if (string.IsNullOrEmpty(args))
                        {
                            this.ユニット版数.Text = ((DataRowView)ユニットコード.SelectedItem)?.Row.Field<String>("Display3")?.ToString();
                        }

                        // ヘッダ部の表示
                        LoadHeader(this, CurrentCode, CurrentEdition);

                        // 明細部の表示
                        strSQL = "SELECT *,IIf(置換不可 = -1,'■','') as 置換不可表示,(単価/入数) as ユニット材料費 FROM Vユニット明細 WHERE ユニットコード='" +
                                 this.CurrentCode + "' AND ユニット版数=" + this.CurrentEdition +
                                 " ORDER BY 明細番号";
                        if (!VariableSet.SetTable2Details(ユニット明細1.Detail, strSQL, cn))
                        {
                            fn.WaitForm.Close();
                            return;
                        }

                        // RoHS対応状況を表示する
                        this.RoHS対応表示.Text = GetRohsStatus();

                        // 製品材料費を表示する 
                        //明細処理
                        //SubForm.SetTotalAmount();



                        // 動作を制御する
                        FunctionClass.LockData(this, this.IsDecided || this.IsDeleted, "ユニットコード");
                        this.ユニット版数.Enabled = true; // 版数を編集可能にする
                        this.改版ボタン.Enabled = this.IsLatestEdition && this.IsApproved && !IsDeleted;
                        ユニット明細1.Detail.AllowUserToAddRows = !this.IsDecided;
                        ユニット明細1.Detail.AllowUserToDeleteRows = !this.IsDecided;
                        ユニット明細1.Detail.ReadOnly = this.IsDecided;

                        ChangedData(false);

                        this.コマンド複写.Enabled = !this.IsDirty;
                        this.コマンド削除.Enabled = this.IsLatestEdition;
                        this.コマンド部品表.Enabled = !this.IsDirty;
                        this.コマンド承認.Enabled = this.IsDecided && !this.IsApproved;
                        this.コマンド確定.Enabled = !this.IsApproved;

                        fn.WaitForm.Close();

                        break;

                    case "ユニット版数":
                        fn.DoWait("読み込んでいます...");

                        // ヘッダ部の表示
                        LoadHeader(this, CurrentCode, CurrentEdition);

                        // 明細部の表示
                        strSQL = "SELECT *,IIf(置換不可 = -1,'■','') as 置換不可表示,(単価/入数) as ユニット材料費 FROM Vユニット明細 WHERE ユニットコード='" +
                                 this.CurrentCode + "' AND ユニット版数=" + this.CurrentEdition +
                                 " ORDER BY 明細番号";
                        if (!VariableSet.SetTable2Details(ユニット明細1.Detail, strSQL, cn))
                        {
                            fn.WaitForm.Close();
                            return;
                        }


                        // RoHS対応状況を表示する
                        this.RoHS対応表示.Text = GetRohsStatus();




                        // 動作を制御する
                        FunctionClass.LockData(this, this.IsDecided || this.IsDeleted, "ユニットコード", "ユニット版数");
                        this.改版ボタン.Enabled = this.IsLatestEdition && this.IsApproved && !IsDeleted;
                        ユニット明細1.Detail.AllowUserToAddRows = !this.IsDecided;
                        ユニット明細1.Detail.AllowUserToDeleteRows = !this.IsDecided;
                        ユニット明細1.Detail.ReadOnly = this.IsDecided;

                        ChangedData(false);

                        this.コマンド複写.Enabled = !this.IsDirty;
                        this.コマンド削除.Enabled = this.IsLatestEdition;
                        this.コマンド部品表.Enabled = !this.IsDirty;
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
                if (fn.WaitForm == null) return;
                fn.WaitForm.Close();
            }
        }

        private void UpdateEditionList(string codeString)
        {

            OriginalClass ofn = new OriginalClass();


            ofn.SetComboBox(ユニット版数, "SELECT ユニット版数 AS Value, ユニット版数 AS Display, " +
                    "{ fn REPLACE(STR(CONVERT(bit, 承認日時), 1, 0), '1', '■') } AS Display2 " +
                    "FROM Mユニット " +
                    "WHERE (ユニットコード = '" + codeString + "') " +
                    "ORDER BY ユニット版数 DESC");
            ユニット版数.DrawMode = DrawMode.OwnerDrawFixed;

        }

        private string GetRohsStatus()
        {
            try
            {
                int stPriority = 100; // RoHSの対応状況を優先的に選定するための番号

                if ((IsDecided && ユニット明細1.Detail.RowCount < 1) || (!IsDecided && ユニット明細1.Detail.RowCount <= 1))
                {
                    stPriority = 1;
                    return "×";
                }

                for (int i = 0; i < ユニット明細1.Detail.RowCount; i++)
                {
                    if (ユニット明細1.Detail.Rows[i].IsNewRow) continue;

                    if (!Convert.ToBoolean(ユニット明細1.Detail.Rows[i].Cells["削除対象"].Value))
                    {
                        int priority = Convert.ToInt32(ユニット明細1.Detail.Rows[i].Cells["RohsStatusPriority"].Value);
                        if (priority < stPriority)
                        {
                            stPriority = priority;
                            return ユニット明細1.Detail.Rows[i].Cells["RohsStatusSign"].Value.ToString();
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

        private bool LoadHeader(Form formObject, string codeString, int editionNumber)
        {
            try
            {
                Connect();

                string strSQL;

                strSQL = "SELECT * FROM Vユニットヘッダ WHERE ユニットコード ='" + codeString + "' and ユニット版数 = " + editionNumber;


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


                if (string.IsNullOrEmpty(無効日時.Text))
                {
                    削除表示.SendToBack();
                }
                else
                {
                    削除表示.BringToFront();
                }


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
                    if (RegTrans(CurrentCode, CurrentEdition, false))
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

        private bool RegTrans(string codeString, int editionNumber, bool updatePreEdition)
        {
            Connect();
            SqlTransaction transaction = cn.BeginTransaction();
            {

                try
                {

                    string strwhere = "ユニットコード='" + codeString + "' and ユニット版数 =" + editionNumber;
                    // ヘッダ部の登録
                    if (!DataUpdater.UpdateOrInsertDataFrom(this, cn, "Mユニット", strwhere, "ユニットコード", transaction, "ユニット版数"))
                    {
                        transaction.Rollback();  // 変更をキャンセル
                        return false;
                    }

                    // 明細部の登録
                    if (!DataUpdater.UpdateOrInsertDetails(this.ユニット明細1.Detail, cn, "Mユニット明細", strwhere, "ユニットコード", transaction))
                    {
                        transaction.Rollback();  // 変更をキャンセル
                        return false;
                    }

                    // 前版データの更新処理
                    if (updatePreEdition)
                    {
                        string sql = "";
                        strwhere = "ユニットコード='" + codeString + "' and ユニット版数 =" + (editionNumber - 1);

                        if (IsApproved) // 改版データを承認した場合
                        {
                            sql = $"UPDATE Mユニット SET 無効日時=GETDATE(), 無効者コード='{承認者コード.Text}' WHERE " + strwhere;
                        }
                        else // 改版データを承認しない場合
                        {
                            sql = $"UPDATE Mユニット SET 無効日時=NULL, 無効者コード=NULL WHERE " + strwhere;
                        }

                        using (SqlCommand cmd = new SqlCommand(sql, cn, transaction))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }




                    string rohsStatus = GetRohsStatus() == "" ? "NULL" : "'1'";
                    string nonCor = IsBasedNonCor() == 9 ? "NULL" : "'" + IsBasedNonCor().ToString() + "'";

                    string sql2 = "";

                    sql2 = $"UPDATE Mユニット SET RoHS対応 = {rohsStatus}, 非含有証明書 = {nonCor} WHERE " + strwhere;

                    using (SqlCommand cmd = new SqlCommand(sql2, cn, transaction))
                    {
                        cmd.ExecuteNonQuery();
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

        private int IsBasedNonCor()
        {
            try
            {


                if ((IsDecided && ユニット明細1.Detail.RowCount < 1) || (!IsDecided && ユニット明細1.Detail.RowCount <= 1))
                {
                    return 0;
                }

                int result = 0;

                foreach (Row row in ユニット明細1.Detail.Rows)
                {

                    if (row.IsNewRow) continue;

                    // ユニットの場合、製品と違いRoHSフィールドの値がNULLであることはあり得ないが、
                    // 万が一NULL値を取得してしまった場合に備え条件を含める
                    if (row.Cells["非含有証明書"].Value is DBNull)
                    {
                        result = 9;
                        break;
                    }
                    else if (row.Cells["非含有証明書"].Value.ToString() == "？")
                    {
                        result = 3;
                    }
                    else if (row.Cells["非含有証明書"].Value.ToString() == "△" && result < 3)
                    {
                        result = 2;
                    }
                    else if (row.Cells["非含有証明書"].Value.ToString() == "○" && result < 2)
                    {
                        result = 1;
                    }
                }

                return result;

            }
            catch (Exception ex)
            {
                Debug.Print($"{GetType().Name}_IsBasedNonCor - {ex.Message}");
                return 0;
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
                    if (コマンド読込.Enabled) コマンド読込_Click(sender, e);
                    break;
                case Keys.F3:
                    if (コマンド複写.Enabled) コマンド複写_Click(sender, e);
                    break;
                case Keys.F4:
                    if (コマンド削除.Enabled) コマンド削除_Click(sender, e);
                    break;
                case Keys.F5:
                    if (コマンド部品.Enabled) コマンド部品_Click(sender, e);
                    break;
                case Keys.F6:
                    if (コマンド部品表.Enabled) コマンド部品表_Click(sender, e);
                    break;
                case Keys.F7:
                    if (コマンド部品定数表.Enabled) コマンド部品定数表_Click(sender, e);
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

                fn.DoWait("改版しています...");


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
                    ユニット明細1.Detail.AllowUserToAddRows = true;
                    ユニット明細1.Detail.AllowUserToDeleteRows = true;
                    ユニット明細1.Detail.ReadOnly = false;
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


                for (int i = 0; i < ユニット明細1.Detail.RowCount; i++)
                {
                    if (ユニット明細1.Detail.Rows[i].IsNewRow == true)
                    {
                        //新規行の場合は、処理をスキップ
                        continue;
                    }

                    ユニット明細1.Detail.Rows[i].Cells["ユニットコード"].Value = codeString;
                    ユニット明細1.Detail.Rows[i].Cells["ユニット版数"].Value = editionNumber;


                }

                copyflg = true;

                // 表示情報の更新
                this.ユニットコード.Text = codeString;
                this.ユニット版数.Text = editionNumber.ToString();

                copyflg = false;

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

                if (ユニット明細1.Detail.RowCount == 0)
                {
                    return true;
                }

                int lngi = 1; // 明細番号の初期化

                for (int i = 0; i < ユニット明細1.Detail.RowCount; i++)
                {
                    if (ユニット明細1.Detail.Rows[i].IsNewRow == true)
                    {
                        //新規行の場合は、処理をスキップ
                        continue;
                    }

                    if (string.IsNullOrEmpty(ユニット明細1.Detail.Rows[i].Cells["削除対象"].Value?.ToString()))
                    {
                        ユニット明細1.Detail.Rows[i].Cells["削除対象"].Value = false;
                    }

                    if (Convert.ToBoolean(ユニット明細1.Detail.Rows[i].Cells["削除対象"].Value))
                    {
                        ユニット明細1.Detail.Rows.RemoveAt(i);
                    }
                    else
                    {
                        ユニット明細1.Detail.Rows[i].Cells["明細番号"].Value = lngi;
                        ユニット明細1.Detail.Rows[i].Cells["変更操作コード"].Value = null;
                        ユニット明細1.Detail.Rows[i].Cells["変更内容"].Value = null;
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
                        if (!FunctionClass.ReturnCode(cn, "UNI" + this.CurrentCode))
                        {
                            MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine + Environment.NewLine +
                                            "ユニットコード　：　" + this.CurrentCode, "修正コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                            if (!FunctionClass.ReturnCode(cn, "UNI" + this.CurrentCode))
                            {
                                MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine + Environment.NewLine +
                                                "ユニットコード　：　" + this.CurrentCode, "修正コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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


                    FunctionClass.LockData(this, IsDecided || IsDeleted, "ユニットコード", "ユニット版数");

                    // RoHS対応状況を表示する
                    RoHS対応表示.Text = GetRohsStatus();



                    // 新規モードのときは修正モードへ移行する
                    if (IsNewData)
                    {
                        コマンド新規.Enabled = true;
                        コマンド読込.Enabled = false;
                    }

                    コマンド承認.Enabled = IsDecided;

                    ユニット明細1.Detail.AllowUserToAddRows = !IsDecided;
                    ユニット明細1.Detail.AllowUserToDeleteRows = !IsDecided;
                    ユニット明細1.Detail.ReadOnly = IsDecided;

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




                // 認証処理
                string strHeadCode = CommonConstants.USER_CODE_TECH; // 承認者を指定する

                // ログオンユーザーが指定ユーザーなら認証者コードにユーザーコードを設定する
                if (CommonConstants.LoginUserCode != strHeadCode)
                {
                    using (var authenticationForm = new F_認証())
                    {
                        authenticationForm.args = strHeadCode;

                        //authenticationForm.MdiParent = this.MdiParent;
                        //authenticationForm.FormClosed += (s, args) => { this.Enabled = true; };
                        //this.Enabled = false;

                        authenticationForm.ShowDialog();

                        //authenticationForm.ShowDialog();

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
                if (CopyData(Right(FunctionClass.採番(cn, "UNI"), 8), 1) && ClearHistory())
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
                    ユニット明細1.Detail.AllowUserToAddRows = true;
                    ユニット明細1.Detail.AllowUserToDeleteRows = true;
                    ユニット明細1.Detail.ReadOnly = false;
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


                    var unitCode = ユニットコード.Text;

                    // 版数のソース更新
                    UpdateEditionList(CurrentCode);
                    // 製品版数.Requery();

                    ChangedData(false);

                    // RoHS対応状況を表示する
                    RoHS対応表示.Text = GetRohsStatus();


                    // 新規モードのときは修正モードへ移行する
                    if (IsNewData)
                    {
                        コマンド新規.Enabled = true;
                        コマンド読込.Enabled = false;

                        saveflg = true;

                        OriginalClass ofn = new OriginalClass();
                        ofn.SetComboBox(ユニットコード, "SELECT A.ユニットコード as Value, A.ユニットコード as Display , A.最新版数 as Display3, { fn REPLACE(STR(CONVERT(bit, Mユニット.無効日時), 1, 0), '1', '×') } AS Display2 FROM Mユニット INNER JOIN (SELECT ユニットコード, MAX(ユニット版数) AS 最新版数 FROM Mユニット GROUP BY ユニットコード) A ON Mユニット.ユニットコード = A.ユニットコード AND Mユニット.ユニット版数 = A.最新版数 ORDER BY A.ユニットコード DESC");

                        saveflg = false;

                        ユニットコード.SelectedValue = unitCode;
                        ユニットコード_SelectedIndexChanged(sender, e);

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
                        $"ユニットコード　：　{CurrentCode}{Environment.NewLine}" +
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
                            //authenticationForm.MdiParent = this.MdiParent;
                            //authenticationForm.FormClosed += (s, args) => { this.Enabled = true; };
                            //this.Enabled = false;

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
                    if (RegTrans(CurrentCode, CurrentEdition, false))
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
                            $"ユニットコード　：　{CurrentCode}{Environment.NewLine}" +
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
                            $"ユニットコード　：　{CurrentCode}{Environment.NewLine}" +
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
                            ユニット版数.Focus();
                            ユニット版数.Text = (CurrentEdition - 1).ToString();

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

        private void コマンドツール_Click(object sender, EventArgs e)
        {
            F_ユニット_ツール targetform = new F_ユニット_ツール();

            targetform.args = CurrentCode;

            targetform.MdiParent = this.MdiParent;
            targetform.FormClosed += (s, args) => { this.Enabled = true; };
            this.Enabled = false;

            targetform.Show();
        }

        private void コマンド部品_Click(object sender, EventArgs e)
        {
            F_部品 targetform = new F_部品();

            targetform.args = CurrentPartsCode;
            targetform.MdiParent = this.MdiParent;
            targetform.FormClosed += (s, args) => { this.Enabled = true; };
            this.Enabled = false;

            targetform.Show();
        }

        private void コマンド部品表_Click(object sender, EventArgs e)
        {
            IReport paoRep = ReportCreator.GetPreview();

            paoRep.LoadDefFile("../../../Reports/部品表.prepd");

            Connect();

            F_ユニット? f_ユニット = Application.OpenForms.OfType<F_ユニット>().FirstOrDefault();

            DataRowCollection V部品表;

            string sqlQuery = "SELECT * FROM V部品表 where ユニットコード='" + CurrentCode + "' and ユニット版数=" + CurrentEdition + " ORDER BY 明細番号";

            using (SqlCommand command = new SqlCommand(sqlQuery, cn))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataSet dataSet = new DataSet();

                    adapter.Fill(dataSet);

                    V部品表 = dataSet.Tables[0].Rows;

                }
            }

            //最大行数
            int maxRow = 49;
            //現在の行
            int CurRow = 0;
            //行数
            int RowCount = maxRow;
            if (V部品表.Count > 0)
            {
                RowCount = V部品表.Count;
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
                paoRep.Write("ユニットコード", V部品表[0]["ユニットコード"].ToString() != "" ? V部品表[0]["ユニットコード"].ToString() : " ");
                paoRep.Write("ユニット版数", V部品表[0]["ユニット版数"].ToString() != "" ? V部品表[0]["ユニット版数"].ToString() : " ");
                paoRep.Write("ユニット品名", V部品表[0]["ユニット品名"].ToString() != "" ? V部品表[0]["ユニット品名"].ToString() : " ");
                paoRep.Write("ユニット型番", V部品表[0]["ユニット型番"].ToString() != "" ? V部品表[0]["ユニット型番"].ToString() : " ");

                paoRep.Write("承認日時", V部品表[0]["承認日時"].ToString() != "" ? V部品表[0]["承認日時"].ToString() : " ");

                if (!string.IsNullOrEmpty(V部品表[0]["無効日時"].ToString()))
                {
                    paoRep.Write("コメント", "（削除済み）");
                }
                else if (f_ユニット.ユニット明細1.Detail.SortOrder != 0)
                {
                    paoRep.Write("コメント", "（確認用）");
                }
                else
                {
                    paoRep.Write("コメント", "");
                }

                if (string.IsNullOrEmpty(V部品表[0]["識別コード"].ToString()))
                {
                    paoRep.Write("ページコード", " ");
                }
                else
                {
                    string ページコード = $"{V部品表[0]["識別コード"].ToString()}-04{page:D2}";
                    paoRep.Write("ページコード", ページコード);
                }

                //フッダー
                paoRep.Write("出力日時", "出力日時：" + now.ToString("yyyy/MM/dd HH:mm:ss"));
                paoRep.Write("ページ", ("ページ： " + page + "/" + maxPage).ToString());

                //明細
                for (var i = 0; i < maxRow; i++)
                {
                    if (CurRow >= V部品表.Count) break;

                    DataRow targetRow = V部品表[CurRow];

                    paoRep.Write("明細番号", targetRow["明細番号"].ToString() != "" ? targetRow["明細番号"].ToString() : " ", i + 1);
                    paoRep.Write("構成番号", targetRow["構成番号"].ToString() != "" ? targetRow["構成番号"].ToString() : " ", i + 1);
                    paoRep.Write("形状", targetRow["形状"].ToString() != "" ? targetRow["形状"].ToString() : " ", i + 1);
                    paoRep.Write("部品コード", targetRow["部品コード"].ToString() != "" ? targetRow["部品コード"].ToString() : " ", i + 1);
                    paoRep.Write("品名", targetRow["品名"].ToString() != "" ? targetRow["品名"].ToString() : " ", i + 1);
                    paoRep.Write("型番", targetRow["型番"].ToString() != "" ? targetRow["型番"].ToString() : " ", i + 1);
                    paoRep.Write("メーカー名", targetRow["メーカー名"].ToString() != "" ? targetRow["メーカー名"].ToString() : " ", i + 1);
                    paoRep.Write("変更", targetRow["変更"].ToString() != "" ? targetRow["変更"].ToString() : " ", i + 1);

                    if (targetRow["削除対象"].ToString() == "1")
                    {
                        paoRep.Write("削除対象", "------------------------------------------------------------------------------------------------", i + 1);
                    }
                    else
                    {
                        paoRep.Write("削除対象", "", i + 1);
                    }

                    CurRow++;


                }

                page++;

                paoRep.PageEnd();



            }


            paoRep.Output();
        }

        private void コマンド部品定数表_Click(object sender, EventArgs e)
        {
            var intRes = MessageBox.Show("部品単価を表示しますか？", "部品定数表コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
            bool bleShowPrice;

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




            IReport paoRep = ReportCreator.GetPreview();

            paoRep.LoadDefFile("../../../Reports/部品定数表.prepd");

            Connect();

            F_ユニット? f_ユニット = Application.OpenForms.OfType<F_ユニット>().FirstOrDefault();

            DataRowCollection V部品定数表;

            string sqlQuery = "SELECT * FROM V部品定数表 where ユニットコード='" + CurrentCode + "' and ユニット版数=" + CurrentEdition + " ORDER BY 品名,型番";

            using (SqlCommand command = new SqlCommand(sqlQuery, cn))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataSet dataSet = new DataSet();

                    adapter.Fill(dataSet);

                    V部品定数表 = dataSet.Tables[0].Rows;

                }
            }

            //最大行数
            int maxRow = 26;
            //現在の行
            int CurRow = 0;
            //行数
            int RowCount = maxRow;
            if (V部品定数表.Count > 0)
            {
                RowCount = V部品定数表.Count;
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
                paoRep.Write("シリーズ名", " ");
                paoRep.Write("ロット数量", " ");
                paoRep.Write("ロット番号", " ");
                paoRep.Write("売上区分", " ");
                paoRep.Write("発注納期", " ");
                paoRep.Write("発注日", " ");

                paoRep.Write("ユニットコード", V部品定数表[0]["ユニットコード"].ToString() != "" ? V部品定数表[0]["ユニットコード"].ToString() : " ");
                paoRep.Write("ユニット版数", V部品定数表[0]["ユニット版数"].ToString() != "" ? V部品定数表[0]["ユニット版数"].ToString() : " ");
                paoRep.Write("ユニット品名", V部品定数表[0]["ユニット品名"].ToString() != "" ? V部品定数表[0]["ユニット品名"].ToString() : " ");
                paoRep.Write("ユニット型番", V部品定数表[0]["ユニット型番"].ToString() != "" ? V部品定数表[0]["ユニット型番"].ToString() : " ");

                paoRep.Write("承認日時", V部品定数表[0]["承認日時"].ToString() != "" ? V部品定数表[0]["承認日時"].ToString() : " ");



                //フッダー
                paoRep.Write("出力日時", "出力日時：" + now.ToString("yyyy/MM/dd HH:mm:ss"));
                paoRep.Write("ページ", ("ページ： " + page + "/" + maxPage).ToString());

                //明細
                for (var i = 0; i < maxRow; i++)
                {
                    if (CurRow >= V部品定数表.Count) break;

                    DataRow targetRow = V部品定数表[CurRow];

                    paoRep.Write("明細番号", (CurRow + 1).ToString(), i + 1);
                    paoRep.Write("部品置換", targetRow["部品置換"].ToString() != "" ? targetRow["部品置換"].ToString() : " ", i + 1);
                    paoRep.Write("形状", targetRow["形状"].ToString() != "" ? targetRow["形状"].ToString() : " ", i + 1);
                    paoRep.Write("部品コード", targetRow["部品コード"].ToString() != "" ? targetRow["部品コード"].ToString() : " ", i + 1);
                    paoRep.Write("品名", targetRow["品名"].ToString() != "" ? targetRow["品名"].ToString() : " ", i + 1);
                    paoRep.Write("型番", targetRow["型番"].ToString() != "" ? targetRow["型番"].ToString() : " ", i + 1);
                    paoRep.Write("メーカー名", targetRow["メーカー名"].ToString() != "" ? targetRow["メーカー名"].ToString() : " ", i + 1);
                    paoRep.Write("ShelfNumber", targetRow["ShelfNumber"].ToString() != "" ? targetRow["ShelfNumber"].ToString() : " ", i + 1);
                    paoRep.Write("仕入先名", targetRow["仕入先名"].ToString() != "" ? targetRow["仕入先名"].ToString() : " ", i + 1);
                    paoRep.Write("定数", targetRow["定数"].ToString() != "" ? targetRow["定数"].ToString() : " ", i + 1);
                    if (bleShowPrice)
                    {
                        paoRep.Write("部品単価", targetRow["部品単価"].ToString() != "" ? targetRow["部品単価"].ToString() : " ", i + 1);
                    }
                    else
                    {
                        paoRep.Write("部品単価", " ", i + 1);
                    }

                    paoRep.Write("発注コード", " ", i + 1);
                    paoRep.Write("発注数量", " ", i + 1);
                    paoRep.Write("出庫数量１", "／", i + 1);
                    paoRep.Write("確認１", " ", i + 1);
                    paoRep.Write("出庫数量２", "／", i + 1);
                    paoRep.Write("確認２", " ", i + 1);



                    paoRep.z_Objects.SetObject("品名", i + 1);
                    lenB = Encoding.Default.GetBytes(targetRow["品名"].ToString()).Length;
                    if (26 < lenB)
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 6;
                    }
                    else if (20 < lenB)
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 8;
                    }
                    else
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 10;
                    }

                    paoRep.z_Objects.SetObject("型番", i + 1);
                    lenB = Encoding.Default.GetBytes(targetRow["型番"].ToString()).Length;
                    if (40 < lenB)
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 6;
                    }
                    else if (26 < lenB)
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 8;
                    }
                    else
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 10;
                    }

                    paoRep.z_Objects.SetObject("仕入先名", i + 1);
                    lenB = Encoding.Default.GetBytes(targetRow["仕入先名"].ToString()).Length;
                    if (16 < lenB)
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 5;
                    }
                    else if (10 < lenB)
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 7;
                    }
                    else
                    {
                        paoRep.z_Objects.z_Text.z_FontAttr.Size = 10;
                    }


                    CurRow++;


                }

                page++;

                paoRep.PageEnd();



            }


            paoRep.Output();
        }

        private bool IsApprovedS(SqlConnection connection, string codeString, int editionNumber)
        {
            bool isApproved;

            using (SqlCommand cmd = new SqlCommand())
            {
                // SQLコマンドの構築
                string strKey = $"ユニットコード = '{codeString}' AND ユニット版数 = {editionNumber} AND 承認日時 IS NULL";
                string strSQL = $"SELECT COUNT(*) FROM Mユニット WHERE {strKey}";

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
                string strKey = $"ユニットコード = '{codeString}' AND ユニット版数 = {editionNumber} AND 承認日時 IS NULL";
                string strSQL1 = $"SELECT COUNT(*) FROM Mユニット WHERE {strKey}";

                using (SqlCommand cmdCheck = new SqlCommand(strSQL1, connection))
                {
                    int count = (int)cmdCheck.ExecuteScalar();

                    if (count > 0)
                    {
                        strKey = $"ユニットコード = '{codeString}' AND ユニット版数 = {editionNumber} ";

                        // 承認されていない場合にのみ削除処理を実行
                        string strSQL2 = $"DELETE FROM Mユニット WHERE {strKey}";
                        string strSQL3 = $"DELETE FROM Mユニット明細 WHERE {strKey}";

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
                string strKey1 = $"ユニットコード = '{codeString}' AND ユニット版数 = {editionNumber}";
                string strSQL1 = $"DELETE FROM Mユニット明細 WHERE {strKey1}";
                string strSQL2 = $"DELETE FROM Mユニット WHERE {strKey1}";

                // 前版に戻すための更新クエリ
                string strKey2 = $"ユニットコード = '{codeString}' AND ユニット版数 = {editionNumber - 1}";
                string strSQL3 = $"UPDATE Mユニット SET 無効日時 = NULL, 無効者コード = NULL WHERE {strKey2}";

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

        private void コマンド終了_Click(object sender, EventArgs e)
        {
            Close(); // フォームを閉じる
        }

        public long ChangeParts(string source, string destination, string name, string model, string maker,
            long price, string form, long pieces, string roHS, string ncc, string abolition, bool changeLog,
            string operation, string note)
        {
            try
            {
                long recordsAffected = 0;
                

                foreach (Row row in ユニット明細1.Detail.Rows)
                {
                    if (row.Cells["部品コード"].Value.ToString() == source)
                    {
                        row.Cells["部品コード"].Value = destination;
                        row.Cells["品名"].Value = name;
                        row.Cells["型番"].Value = model;
                        row.Cells["メーカー名"].Value = maker;
                        row.Cells["単価"].Value = price;
                        row.Cells["形状名"].Value = form;
                        row.Cells["入数"].Value = pieces;
                        row.Cells["RohsStatusSign"].Value = roHS;
                        row.Cells["非含有証明書"].Value = ncc;
                        row.Cells["廃止"].Value = abolition;
                        if (changeLog)
                        {
                            if (operation == "")
                            {
                                row.Cells["変更操作コード"].Value = DBNull.Value;
                            }
                            else
                            {
                                row.Cells["変更操作コード"].Value = operation;
                            }

                            if (note == "")
                            {
                                row.Cells["変更内容"].Value = DBNull.Value;
                            }
                            else
                            {
                                row.Cells["変更内容"].Value = note;
                            }
                        }

                        row.Cells["ユニット材料費"].Value = price / pieces;

                        //追加件数
                        recordsAffected++;
                    }
                }

                return recordsAffected;
            }
            catch (Exception ex)
            {
                // 例外処理の方法によって、エラーメッセージの表示やログへの書き込みなどを適切に行う必要があります。
                Debug.WriteLine($"ChangeParts - {ex.Message}");
                return -1;
            }
        }

        //各コントロールの処理
        private void ユニットコード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {

                string strCode = ユニットコード.Text.ToString();
                string formattedCode = strCode.Trim().PadLeft(8, '0');

                if (formattedCode != strCode || string.IsNullOrEmpty(strCode))
                {
                    ユニットコード.Text = formattedCode;
                }

            }
        }

        private void ユニットコード_SelectedIndexChanged(object sender, EventArgs e)
        {
            ユニット版数.Text = ((DataRowView)ユニットコード.SelectedItem)?.Row.Field<Int16>("Display3").ToString();
            UpdatedControl(ユニットコード);
        }

        private void ユニットコード_TextChanged(object sender, EventArgs e)
        {
            if (ユニットコード.SelectedValue == null)
            {
                ユニット版数.Text = null;
            }
            FunctionClass.LimitText(sender as Control, 8);
        }

        private void ユニットコード_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 75, 16 }, new string[] { "Display", "Display2" });
            ユニットコード.Invalidate();
            ユニットコード.DroppedDown = true;
        }

        private void ユニット版数_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 30, 30 }, new string[] { "Display", "Display2" });
            ユニット版数.Invalidate();
            ユニット版数.DroppedDown = true;
        }

        private void ユニット版数_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void ユニット版数_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 5);
        }

        private void 型番_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 30);
            ChangedData(true);
        }

        private void 識別コード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 20);
            ChangedData(true);
        }

        private void 品名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 60);
            ChangedData(true);
        }

        private void 廃止_CheckedChanged(object sender, EventArgs e)
        {
            ChangedData(true);
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

        private void 無効日時_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(無効日時.Text))
            {
                削除表示.SendToBack();
            }
            else
            {
                削除表示.BringToFront();
            }
        }

        private void ユニットコード_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■読み込むユニットデータのコードを入力します。　■半角８文字まで入力でき、上位０は省略可能です。";
        }

        private void ユニットコード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 品名_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角２５文字まで入力できます。";
        }

        private void 品名_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 型番_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■半角３０文字まで入力できます。";
        }

        private void 型番_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 識別コード_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■指導書No.の一部を入力します。　■７文字まで入力できます。";
        }

        private void 識別コード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void RoHS対応_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■本ユニットがRoHSに対応しているかどうかを示します。";
        }

        private void RoHS対応_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void コマンド部品_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■カーソルがある明細行の登録部品を参照します。";
        }

        private void コマンド部品_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }
    }
}
