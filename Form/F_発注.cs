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

namespace u_net
{
    public partial class F_発注 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "発注";
        private int selected_frame = 0;

        public F_発注(string? openargs)
        {
            this.Text = "発注";       // ウィンドウタイトルを設定
            this.MaximizeBox = false;  // 最大化ボタンを無効化
            this.MinimizeBox = false; //最小化ボタンを無効化
            args = string.IsNullOrEmpty(openargs) ? "" : openargs;

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
            ofn.SetComboBox(発注版数, "SELECT 発注コード as Display ,発注コード as Value FROM V発注_最新版 ORDER BY 発注コード DESC");

            try
            {
                this.SuspendLayout();

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

                //下のLoadDetailsとLockDataはUpdateControlで実行されるので不要か
                // 明細部の初期化
                //string strSQL = "SELECT * FROM V発注明細 WHERE 発注コード='" + CurrentCode + "' ORDER BY 明細番号";
                //LoadDetails(strSQL, SubForm, SubDatabase, "発注明細");

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
                //SubForm.AllowAdditions = true;
                //SubForm.AllowDeletions = true;
                //SubForm.AllowEdits = true;

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

                // 編集による変更がない状態へ遷移
                //ChangedData(false);


                this.発注コード.Enabled = true;
                this.発注コード.Focus();

                FunctionClass.LockData(this, true, "発注コード", "発注版数");
                this.コマンド新規.Enabled = true;
                this.コマンド読込.Enabled = false;
                this.コマンド複写.Enabled = false;
                this.コマンド確定.Enabled = false;
                this.コマンド登録.Enabled = false;
                //this.Text = BASE_CAPTION;                

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

            string LoginUserCode = CommonConstants.LoginUserCode;//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            LocalSetting test = new LocalSetting();
            test.SavePlace(LoginUserCode, this);

            try
            {

                Connect();

                // データへの変更がないときの処理
                if (!IsChanged)
                {
                    // 新規モードで且つコードが取得済みのときはコードを戻す
                    //if (IsNewData && !string.IsNullOrEmpty(CurrentCode) && CurrentEdition == 1)
                    //{
                    //    // 採番された番号を戻す
                    //    if (!FunctionClass.ReturnCode(cn, "PAR" + CurrentCode))
                    //    {
                    //        MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine + Environment.NewLine +
                    //                        "部品コード　：　" + CurrentCode, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //    }
                    //}
                    return;
                }

                // 修正されているときは登録確認を行う
                var intRes = MessageBox.Show("変更内容を登録しますか？", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (intRes)
                {
                    case DialogResult.Yes:
                        //// エラーチェック
                        //if (!ErrCheck())
                        //{
                        //    return;
                        //}
                        //// 登録処理
                        //if (!SaveData())
                        //{
                        //    if (MessageBox.Show("エラーのため登録できませんでした。" + Environment.NewLine +
                        //                        "強制終了しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        //    {
                        //        return;
                        //    }
                        //}
                        break;
                    case DialogResult.No:
                        // 新規コードを取得していたときはコードを戻す
                        //if (IsNewData && !string.IsNullOrEmpty(CurrentCode) && CurrentEdition == 1)
                        //{
                        //    if (!FunctionClass.ReturnCode(cn, "PAR" + CurrentCode))
                        //    {
                        //        MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine +
                        //                        "部品コード　：　" + CurrentCode, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //    }
                        //}
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
        }


        private void コマンド登録_Click(object sender, EventArgs e)
        {

            if (this.ActiveControl == this.コマンド登録)
            {
                GetNextControl(コマンド登録, false).Focus();
            }

            // エラーチェック
            if (!ErrCheck())
            {
                goto Bye_コマンド登録_Click;
            }

            FunctionClass fn = new FunctionClass();
            fn.DoWait("登録しています...");


        // 登録処理
        //if (SaveData())
        //{
        //    // 登録に成功した
        //    ChangedData(false); // データ変更取り消し

        //    if (this.IsNewData)
        //    {
        //        // 新規モードのとき
        //        this.コマンド新規.Enabled = true;
        //        this.コマンド読込.Enabled = false;
        //        this.コマンド部品.Enabled = true;
        //    }
        //    // UpdatePurGridの呼び出し
        //    // Call UpdatePurGrid();
        //    fn.WaitForm.Close();
        //    MessageBox.Show("登録を完了しました", "登録コマンド", MessageBoxButtons.OK);
        //}
        //else
        //{
        //    // 登録に失敗したとき
        //fn.WaitForm.Close();
        //this.コマンド登録.Enabled = true;
        //MessageBox.Show("登録できませんでした。", "登録コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //}

        Bye_コマンド登録_Click:
            return;

        }

        private bool SaveData()
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


            using (SqlTransaction transaction = cn.BeginTransaction())
            {
                try
                {
                    string strwhere = " 発注コード='" + CurrentCode + "' and 発注版数=" + CurrentEdition;

                    if (!DataUpdater.UpdateOrInsertDataFrom(this, cn, "T発注", strwhere, "発注コード", transaction))
                    {
                        //保存できなかった時の処理
                        headerErr = true;
                        throw new Exception();
                    }
                    //明細部の登録




                    // トランザクションをコミット
                    transaction.Commit();

                    //仕入先コード.Enabled = true;


                    return true;

                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    コマンド登録.Enabled = true;
                    MessageBox.Show("データの保存中にエラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    return false;
                }
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

            // キー情報を表示するコントロールを制御する
            // コードにフォーカスがある状態でサブフォームから呼び出されたときの対処

            if (ActiveControl == 発注コード) 発注日.Focus();
            発注コード.Enabled = !IsChanged;
            if (ActiveControl == 発注版数) 発注日.Focus();
            発注版数.Enabled = !IsChanged;

            コマンド複写.Enabled = !IsChanged;
            コマンド削除.Enabled = !IsChanged;
            コマンド発注書.Enabled = !IsChanged;
            if (IsChanged) コマンド送信.Enabled = false;
            if (IsChanged) コマンド確定.Enabled = true;
            コマンド登録.Enabled = IsChanged;
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
                if (this.IsChanged)
                {
                    var intRes = MessageBox.Show("変更内容を登録しますか？", "新規コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    switch (intRes)
                    {
                        case DialogResult.Yes:
                            // 登録処理
                            //if (!SaveData())
                            //{
                            //    MessageBox.Show("エラーのため登録できません。", "新規コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //    goto Bye_コマンド新規_Click;
                            //}
                            break;
                        case DialogResult.Cancel:
                            goto Bye_コマンド新規_Click;
                    }
                }

                VariableSet.SetControls(this);
                //DispGrid(CurrentCode);

                //string code = FunctionClass.採番(cn, "PAR");
                //発注コード.Text = code.Substring(Math.Max(0, code.Length - 8));
                //版数.Text = 1.ToString();
                //入数.Text = 1.ToString();
                //単位数量.Text = 1.ToString();
                //ロス率.Text = 0f.ToString();
                //Rohs1ChemSherpaStatusCode.SelectedValue = 1;
                //JampAis.SelectedValue = 1;
                //非含有証明書.SelectedValue = (byte)3;
                //RoHS資料.SelectedValue = (Int16)1;
                //Rohs2ChemSherpaStatusCode.SelectedValue = 1;
                //Rohs2JampAisStatusCode.SelectedValue = 1;
                //Rohs2NonInclusionCertificationStatusCode.SelectedValue = 3;
                //Rohs2DocumentStatusCode.SelectedValue = 1;
                //Rohs2ProvisionalRegisteredStatusCode.Checked = true;
                //在庫管理.Checked = false;


                ChangedData(false);

                //InventoryAmount.Text = 0.ToString();
                //ShowRohsStatus();
                //品名.Focus();
                発注コード.Enabled = false;
                改版ボタン.Enabled = false;
                コマンド新規.Enabled = false;
                コマンド読込.Enabled = true;
                コマンド削除.Enabled = false;
                コマンド部品.Enabled = false;
                コマンド購買.Enabled = false;
                コマンド登録.Enabled = false;



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
            // if (!AskSave()) { return; }


            // strOpenArgsがどのように設定されているかに依存します。
            // もしstrOpenArgsに関連する処理が必要な場合はここに追加してください。

            // 各コントロールの値をクリア
            VariableSet.SetControls(this);

            // コントロールを操作
            発注コード.Enabled = true;
            発注コード.Focus();
            改版ボタン.Enabled = false;
            コマンド新規.Enabled = true;
            コマンド読込.Enabled = false;
            コマンド部品.Enabled = false;
            コマンド登録.Enabled = false;
        }




        private void コマンド承認_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。", "承認コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド確定_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。", "確定コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void 改版ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ActiveControl == this.改版ボタン)
                {
                    GetNextControl(改版ボタン, false).Focus();
                }


                MessageBox.Show("部品の改版機能は未完成です。\n履歴に登録される情報は完全ではありません。", "改版", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                if (MessageBox.Show("改版しますか？\n\n・旧版データは履歴コマンドから参照できます。\n・最新版の部品データが有効になります。\n・この操作を元に戻すことはできません。",
                                    "改版", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }


                FunctionClass fn = new FunctionClass();
                fn.DoWait("改版しています...");

                CommonConnect();

                //if (SaveData())
                //{
                //    if (AddHistory(cn, this.CurrentCode, this.CurrentEdition))
                //    {
                //        //this.部品コード.Requery;
                //        // ■ なぜかRequeryしてもColumn(1)がNULLとなるので、版数を+1する
                //        this.版数.Text = (Convert.ToInt32(this.CurrentEdition) + 1).ToString();
                //        this.コマンド購買.Enabled = true;

                //        fn.WaitForm.Close();
                //    }
                //    else
                //    {
                //        fn.WaitForm.Close();
                //        MessageBox.Show("改版できませんでした。", "改版", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    }
                //}
                //else
                //{
                //    fn.WaitForm.Close();
                //    MessageBox.Show("改版できませんでした。", "改版", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //}
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_改版ボタン_Click - " + ex.Message);
                MessageBox.Show("エラーが発生しました。", BASE_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void コマンド送信_Click(object sender, EventArgs e)
        {
            try
            {

                if (this.ActiveControl == this.コマンド送信)
                {
                    GetNextControl(コマンド送信, false).Focus();
                }

                //string strCode = this.メーカーコード.Text;
                //if (string.IsNullOrEmpty(strCode))
                //{
                //    MessageBox.Show("メーカーコードを入力してください。", BASE_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    //this.メーカーコード.Focus();
                //}
                //else
                //{
                //    F_メーカー targetform = new F_メーカー();

                //    targetform.args = strCode;
                //    targetform.ShowDialog();
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in コマンドメーカー_Click: " + ex.Message);
            }
        }

        private void コマンド購買_Click(object sender, EventArgs e)
        {
            try
            {
                //F_部品履歴 targetform = new F_部品履歴();

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
            //SearchForm = new F_検索();
            //SearchForm.FilterName = "メーカー名フリガナ";
            //if (SearchForm.ShowDialog() == DialogResult.OK)
            //{
            //    string SelectedCode = SearchForm.SelectedCode;

            //    メーカーコード.Text = SelectedCode;
            //    string str1 = FunctionClass.GetMakerName(cn, SelectedCode);
            //    string str2 = FunctionClass.GetMakerShortName(cn, SelectedCode);
            //    MakerName.Text = str1;
            //    購買コード.Text = str2;

            //}
        }

        private void コマンド削除_Click(object sender, EventArgs e)
        {
            try
            {
                //if (ActiveControl == this.コマンド削除)
                //{
                //    GetNextControl(コマンド削除, false).Focus();
                //}

                //if (IsIncluded)
                //{
                //    MessageBox.Show("この部品は部品集合に構成されているため、削除できません。\n削除するには対象となる部品集合から構成を解除する必要があります。",
                //        "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    return;
                //}

                //string strMsg = "部品コード　：　" + this.CurrentCode + "\n\nこのデータを削除しますか？\n削除後、管理者により復元することができます。";

                //if (MessageBox.Show(strMsg, "削除コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                //    return;

                ////OpenForm("認証", CommonConstants.USER_CODE_TECH);

                ////while (string.IsNullOrEmpty(certificateCode))
                ////{
                ////    if (SysCmd(acSysCmdGetObjectState, acForm, "認証") == 0)
                ////    {
                ////        MessageBox.Show("削除はキャンセルされました。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ////        return;
                ////    }

                ////    Application.DoEvents();
                ////}

                //// 部品情報削除
                //FunctionClass fn = new FunctionClass();
                //fn.DoWait("削除しています...");

                //Connect();

                //// 削除に成功すれば新規モードへ移行する
                //if (DeleteData(cn, CurrentCode, CurrentEdition))
                //{
                //    fn.WaitForm.Close();
                //    MessageBox.Show("削除しました。\n部品コード　：　" + this.CurrentCode, "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //    コマンド新規_Click(sender, e);
                //}
                //else
                //{
                //    fn.WaitForm.Close();
                //    MessageBox.Show("削除できませんでした。\n部品コード　：　" + this.CurrentCode, "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in コマンド削除_Click: " + ex.Message);
            }
        }

        private void コマンド複写_Click(object sender, EventArgs e)
        {
            try
            {

                //this.品名.Focus();
                ChangedData(true);

                Connect();

                string code = FunctionClass.採番(cn, CH_ORDER);
                //発注コード.Text = code.Substring(Math.Max(0, code.Length - 8));
                //this.版数.Text = 1.ToString();
                //this.InventoryAmount.Text = 0.ToString();
                //this.作成日時.Text = null;
                //this.作成者コード.Text = null;
                //this.CreatorName.Text = null;
                //this.更新日時.Text = null;
                //this.更新者コード.Text = null;
                //this.UpdaterName.Text = null;

                // インターフェース更新
                this.コマンド新規.Enabled = false;
                this.コマンド読込.Enabled = true;

                // 使用先の表示
                //DispGrid(this.CurrentCode);

                // RoHS対応状態の表示を更新する
                //ShowRohsStatus();
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

        }

        private void コマンド発注書_Click(object sender, EventArgs e)
        {
            try
            {
                //if (selected_frame == 1)
                //{
                //    string code = OriginalClass.Nz(仕入先1コード.Text, null);
                //    if (string.IsNullOrEmpty(code))
                //    {
                //        MessageBox.Show("仕入先1を入力してください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //        仕入先1コード.Focus();
                //    }
                //    else
                //    {
                //        F_仕入先 targetform = new F_仕入先();

                //        targetform.args = code;
                //        targetform.ShowDialog();
                //    }
                //}
                //else if (selected_frame == 2)
                //{
                //    string code = OriginalClass.Nz(仕入先2コード.Text, null);
                //    if (string.IsNullOrEmpty(code))
                //    {
                //        MessageBox.Show("仕入先2を入力してください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //        仕入先2コード.Focus();
                //    }
                //    else
                //    {
                //        F_仕入先 targetform = new F_仕入先();

                //        targetform.args = code;
                //        targetform.ShowDialog();
                //    }
                //}
                //else if (selected_frame == 3)
                //{
                //    string code = OriginalClass.Nz(仕入先3コード.Text, null);
                //    if (string.IsNullOrEmpty(code))
                //    {
                //        MessageBox.Show("仕入先3を入力してください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //        仕入先3コード.Focus();
                //    }
                //    else
                //    {
                //        F_仕入先 targetform = new F_仕入先();

                //        targetform.args = code;
                //        targetform.ShowDialog();
                //    }
                //}
                //else
                //{
                //    MessageBox.Show("参照する仕入先を選択してください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in コマンド仕入先_Click: " + ex.Message);
            }
        }

        private void コマンド終了_Click(object sender, EventArgs e)
        {
            Close(); // フォームを閉じる
        }

        private void 仕入先選択ボタン_Click(object sender, EventArgs e)
        {

        }

        private void テストコマンド_Click(object sender, EventArgs e)
        {

        }

        public bool LoadHeader()
        {
            try
            {
                Connect();

                string strSQL;

                strSQL = "SELECT * FROM V発注ヘッダ WHERE 発注コード='" + CurrentCode + "' AND 発注版数= " + CurrentEdition;

                if (!VariableSet.SetTable2Form(this, strSQL, cn)) return false;

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
                object varParm = null; // varParm の型が VBA の Variant に相当するものになる

                switch (controlName)
                {
                    case "発注コード":
                        SetEditions(発注コード.Text);

                        if (args == "")
                        {
                            Connect();
                            string sql = "SELECT 最新版数 FROM V発注_最新版 where 発注コード='" + CurrentCode + "'";
                            発注版数.Text = OriginalClass.GetScalar<string>(cn, sql);
                            cn.Close();
                        }

                        if (!LoadHeader()) return;


                        string strSQL = "SELECT * FROM V発注明細 WHERE 発注コード='" + CurrentCode +
                                         "' AND 発注版数=" + CurrentEdition + " ORDER BY 明細番号";
                        //明細表示
                        if (!LoadDetails(strSQL, this.発注明細1.Detail)) return;

                        FunctionClass.LockData(this, IsDecided || IsDeleted, "発注コード", "発注版数");

                        発注版数.Enabled = true;

                        if (IsLastEdition && IsApproved && !IsDeleted)
                        {
                            改版ボタン.Enabled = (!IsReceived) || PurchasingUserLogin;
                        }
                        else
                        {
                            改版ボタン.Enabled = false;
                        }
                        //編集モードの確認　大馬さんに確認する
                        //SubForm.AllowAdditions = !IsDecided && !IsDeleted;
                        //SubForm.AllowDeletions = !IsDecided && !IsDeleted;
                        //SubForm.AllowEdits = !IsDecided && !IsDeleted;
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

                        if (!LoadDetails(strSQL, this.発注明細1.Detail)) return;

                        FunctionClass.LockData(this, IsDecided || IsDeleted, "発注コード", "発注版数");

                        if (IsLastEdition && IsApproved && !IsDeleted)
                        {
                            改版ボタン.Enabled = (!IsReceived) || PurchasingUserLogin;
                        }
                        else
                        {
                            改版ボタン.Enabled = false;
                        }

                        //SubForm.AllowAdditions = !IsDecided && !IsDeleted;
                        //SubForm.AllowDeletions = !IsDecided && !IsDeleted;
                        //SubForm.AllowEdits = !IsDecided && !IsDeleted;
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
                        //   発注者名.Text = 発注者コード.Column(1);
                        break;

                    case "仕入先コード":
                        //  SetSupplier(varParm.ToString());
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
            //UpdatedControl(sender as Control);
        }

        private void 発注コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 発注コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            //FunctionClass.LimitText(sender as Control, 8);
        }

        private void 発注コード_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Return)
            //{
            //    ComboBox comboBox = sender as ComboBox;
            //    if (comboBox != null)
            //    {
            //        string strCode = comboBox.Text.Trim();
            //        if (!string.IsNullOrEmpty(strCode))
            //        {
            //            strCode = strCode.PadLeft(8, '0');
            //            if (strCode != comboBox.Text)
            //            {
            //                comboBox.Text = strCode;
            //                部品コード_Validated(sender, e);
            //            }
            //        }
            //    }
            //}
        }

        private void 発注コード_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■発注コードを入力します。";
        }

        private void 発注コード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 発注日_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■[space]キーでカレンダーを参照できます。　■未来の日付は入力できません。";
        }

        private void 発注日_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 発注日_KeyDown(object sender, KeyEventArgs e)
        {

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

        private void 在庫管理_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■在庫管理を行う場合はチェックを入れます。";
        }

        private void 在庫管理_Leave(object sender, EventArgs e)
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

    }
}
