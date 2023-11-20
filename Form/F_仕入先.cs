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

namespace u_net
{
    public partial class F_仕入先 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "仕入先";
        private bool setCombo = true;

        public bool IsInvalid
        {
            get
            {
                return 無効日時.Text != null;
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
                return string.IsNullOrEmpty(仕入先コード.Text) ? "" : 仕入先コード.Text;
            }
        }

        public int CurrentRevision
        {
            get
            {
                return string.IsNullOrEmpty(Revision.Text) ? 0 : int.Parse(Revision.Text);
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

        public F_仕入先()
        {
            this.Text = "仕入先";       // ウィンドウタイトルを設定
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
            //string LoginUserCode = "000";//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(CommonConstants.LoginUserCode, this);

            //コンボボックスの設定
            OriginalClass ofn = new OriginalClass();
            ofn.SetComboBox(仕入先コード, "SELECT 仕入先名 as Disply,仕入先コード as Value FROM M仕入先 ORDER BY 仕入先コード DESC");
            ofn.SetComboBox(Revision, "SELECT Revision as Disply , Revision as Value FROM M仕入先 ORDER BY Revision DESC");

            this.支払先専用.DataSource = new KeyValuePair<int, String>[] {
                new KeyValuePair<int, String>(1, "専用とする"),
                new KeyValuePair<int, String>(0, "専用としない"),
            };
            this.支払先専用.DisplayMember = "Value";
            this.支払先専用.ValueMember = "Key";

            this.CloseDay.DataSource = new KeyValuePair<int, String>[] {
                new KeyValuePair<int, String>(5, "5日"),
                new KeyValuePair<int, String>(10, "10日"),
                new KeyValuePair<int, String>(15, "15日"),
                new KeyValuePair<int, String>(20, "20日"),
                new KeyValuePair<int, String>(25, "25日"),
                new KeyValuePair<int, String>(0, "末日"),
            };
            this.CloseDay.DisplayMember = "Value";
            this.CloseDay.ValueMember = "Key";

            this.評価ランク.DataSource = new KeyValuePair<String, String>[] {
                new KeyValuePair<String, String>("A ", "A "),
                new KeyValuePair<String, String>("B1", "B1"),
                new KeyValuePair<String, String>("B2", "B2"),
                new KeyValuePair<String, String>("B3", "B3"),
                new KeyValuePair<String, String>("C ", "C "),
            };
            this.評価ランク.DisplayMember = "Value";
            this.評価ランク.ValueMember = "Key";

            this.振込先金融機関分類コード.DataSource = new KeyValuePair<int, String>[] {
                new KeyValuePair<int, String>(1, "銀行"),
                new KeyValuePair<int, String>(2, "信用金庫"),
                new KeyValuePair<int, String>(3, "信用組合"),
                new KeyValuePair<int, String>(4, "農協"),
            };
            this.振込先金融機関分類コード.DisplayMember = "Value";
            this.振込先金融機関分類コード.ValueMember = "Key";

            this.振込先金融機関店分類コード.DataSource = new KeyValuePair<int, String>[] {
                new KeyValuePair<int, String>(1, "本店"),
                new KeyValuePair<int, String>(2, "支店"),
            };
            this.振込先金融機関店分類コード.DisplayMember = "Value";
            this.振込先金融機関店分類コード.ValueMember = "Key";

            this.振込先口座区分コード.DataSource = new KeyValuePair<int, String>[] {
                new KeyValuePair<int, String>(1, "普通"),
                new KeyValuePair<int, String>(2, "当座"),
            };
            this.振込先口座区分コード.DisplayMember = "Value";
            this.振込先口座区分コード.ValueMember = "Key";

            this.振込手数料負担コード.DataSource = new KeyValuePair<int, String>[] {
                new KeyValuePair<int, String>(1, "支払先"),
                new KeyValuePair<int, String>(2, "自社"),
                new KeyValuePair<int, String>(3, "自社（条件付）"),

            };
            this.振込手数料負担コード.DisplayMember = "Value";
            this.振込手数料負担コード.ValueMember = "Key";

            this.相殺有無.DataSource = new KeyValuePair<int, String>[] {
                new KeyValuePair<int, String>(1, "有り"),
                new KeyValuePair<int, String>(2, "無し"),
            };
            this.相殺有無.DisplayMember = "Value";
            this.相殺有無.ValueMember = "Key";

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
                    if (!string.IsNullOrEmpty(args))
                    {
                        //this.仕入先コード.SelectedValue = args;
                        this.仕入先コード.Text = args;
                        UpdatedControl(this.仕入先コード);
                        ChangedData(false);

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
                setCombo = false;
                this.ResumeLayout();
                fn.WaitForm.Close();
            }
        }

        private bool GoNewMode()
        {
            try
            {
                // 各コントロール値を初期化
                VariableSet.SetControls(this);

                CommonConnect();

                string code = FunctionClass.GetNewCode(cn, CommonConstants.CH_SUPPLIER);
                this.仕入先コード.Text = code.Substring(code.Length - 8);
                // this.仕入先コード.Text = 採番(objConnection, CH_MAKER).Substring(採番(objConnection, CH_MAKER).Length - 8);
                this.Revision.Text = "1";
                this.支払先専用.SelectedValue = 0;
                this.CloseDay.SelectedValue = 20;

                // 編集による変更がない状態へ遷移する
                ChangedData(false);

                // ヘッダ部動作制御
                //FunctionClass.LockData(this, false);
                this.仕入先名.Focus();
                this.仕入先コード.Enabled = false;
                this.コマンド新規.Enabled = false;
                this.コマンド修正.Enabled = true;
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
                if (this.ActiveControl == this.仕入先コード)
                {
                    this.仕入先名.Focus();
                }
                this.仕入先コード.Enabled = !isChanged;
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
                VariableSet.SetControls(this);

                // 編集による変更がない状態へ遷移
                //ChangedData(false);

                this.仕入先コード.Enabled = true;
                this.仕入先コード.Focus();
                // 仕入先コードコントロールが使用可能になってから LockData を呼び出す
                //FunctionClass.LockData(this, true, "仕入先コード");
                this.コマンド新規.Enabled = true;
                this.コマンド修正.Enabled = false;
                this.コマンド複写.Enabled = false;
                this.コマンド登録.Enabled = false;
                this.Text = BASE_CAPTION;
                //if (!string.IsNullOrEmpty(this.削除日時.Text))
                //{
                //    this.削除.Text = "■";
                //}

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

            this.DoubleBuffered = true;

            if (ActiveControl == コマンド登録)
            {
                GetNextControl(コマンド登録, false).Focus();
            }

            // 登録時におけるエラーチェック
            if (!ErrCheck())
            {
                goto Bye_コマンド登録_Click;
            }

            FunctionClass fn = new FunctionClass();
            fn.DoWait("登録しています...");

            if (SaveData())
            {
                // 登録成功
                ChangedData(false);

                if (IsNewData)
                {
                    コマンド新規.Enabled = true;
                    コマンド修正.Enabled = false;
                    コマンド複写.Enabled = true;
                    コマンド削除.Enabled = true;
                    コマンド承認.Enabled = true;

                }

                fn.WaitForm.Close();
                MessageBox.Show("登録を完了しました", "登録コマンド", MessageBoxButtons.OK);
            }
            else
            {
                fn.WaitForm.Close();
                MessageBox.Show("登録できませんでした。", "登録コマンド", MessageBoxButtons.OK);
            }

        Bye_コマンド登録_Click:
            return;
        }

        private bool SaveData()
        {
            object varSaved1 = null;
            object varSaved2 = null;
            object varSaved3 = null;
            object varSaved4 = null;
            object varSaved5 = null;
            object varSaved6 = null;
            object varSaved7 = null;

            Connect();
            DateTime dtmNow = FunctionClass.GetServerDate(cn);

            if (IsNewData)
            {
                varSaved1 = 作成日時.Text;
                varSaved2 = 作成者コード.Text;
                varSaved3 = 作成者名.Text;
                varSaved7 = ActiveDate.Text;
                作成日時.Text = dtmNow.ToString();
                作成者コード.Text = CommonConstants.LoginUserCode;
                作成者名.Text = CommonConstants.LoginUserFullName;
                ActiveDate.Text = dtmNow.ToString();
            }
            varSaved4 = 更新日時.Text;
            varSaved5 = 更新者コード.Text;
            varSaved6 = 更新者名.Text;
            更新日時.Text = dtmNow.ToString();
            更新者コード.Text = CommonConstants.LoginUserCode;
            更新者名.Text = CommonConstants.LoginUserFullName;

            
            using (SqlTransaction transaction = cn.BeginTransaction())
            {
                try
                {
                    string strwhere = " 仕入先コード='" + this.仕入先コード.Text + "' and Revision=" + this.Revision.Text;

                    if (!DataUpdater.UpdateOrInsertDataFrom(this, cn, "M仕入先", strwhere, "仕入先コード", transaction))
                    {
                        //保存できなかった時の処理
                        if (IsNewData)
                        {
                            作成日時.Text = varSaved1.ToString();
                            作成者コード.Text = varSaved2.ToString();
                            作成者名.Text = varSaved3.ToString();
                            ActiveDate.Text = varSaved7.ToString();
                            transaction.Rollback();
                        }

                        更新日時.Text = varSaved4.ToString();
                        更新者コード.Text = varSaved5.ToString();
                        更新者名.Text = varSaved6.ToString();
                        return false;
                    }

                    // トランザクションをコミット
                    transaction.Commit();

                    //仕入先コード.Enabled = true;

                    // 新規モードのときは修正モードへ移行する
                    if (IsNewData)
                    {
                        コマンド新規.Enabled = true;
                        コマンド修正.Enabled = false;
                        コマンド複写.Enabled = true;
                        コマンド削除.Enabled = true;
                        コマンド承認.Enabled = false;
                    }
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
                            if (!ErrCheck()) return;
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



        private void コマンド修正_Click(object sender, EventArgs e)
        {
            try
            {
                this.DoubleBuffered = true;

                CommonConnect();

                if (!this.IsChanged)
                {
                    // 新規モードで且つコードが取得済みのときはコードを戻す
                    if (this.IsNewData && !string.IsNullOrEmpty(this.CurrentCode))
                    {
                        // 採番された番号を戻す
                        if (!FunctionClass.ReturnNewCode(cn, CommonConstants.CH_SUPPLIER, this.CurrentCode))
                        {
                            MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine +
                                "仕入先コード　：　" + this.CurrentCode, "修正コマンド", MessageBoxButtons.OK);
                        }
                    }
                    // 読込モードへ移行する
                    if (!GoModifyMode())
                    {
                        goto Err_コマンド修正_Click;
                    }
                    goto Bye_コマンド修正_Click;
                }

                // 変更されているときは登録確認を行う
                var intRes = MessageBox.Show("変更内容を登録しますか？", "修正コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (intRes)
                {
                    case DialogResult.Yes:
                        if (!ErrCheck()) return;

                        // 登録処理
                        if (!SaveData())
                        {
                            MessageBox.Show("エラーのため登録できません。", "修正コマンド", MessageBoxButtons.OK);
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
                                    "仕入先コード　：　" + this.CurrentCode, "修正コマンド", MessageBoxButtons.OK);
                            }
                        }
                        break;
                    case DialogResult.Cancel:
                        return;
                }

                // 読込モードへ移行する
                if (!GoModifyMode())
                {
                    goto Err_コマンド修正_Click;
                }
            }
            finally
            {
                //this.Painting = true;
            }

        Bye_コマンド修正_Click:
            return;

        Err_コマンド修正_Click:
            //Debug.Print(this.Name + "_コマンド読込_Click - " + Err.Number + " : " + Err.Description);
            MessageBox.Show("エラーが発生しました。" + Environment.NewLine +
                "[ " + BASE_CAPTION + " ]を終了します。", "読込コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //this.Painting = true;
            this.Close();
        }


        private bool ErrCheck()
        {
            //入力確認    

            if (振込手数料負担コード.SelectedValue != null && 振込手数料負担コード.SelectedValue.ToString() == "3")

            {
                if (!FunctionClass.IsError(this.振込手数料上限金額)) return false;
            }

            if (支払先専用.SelectedValue != null && 支払先専用.SelectedValue.ToString() == "1")
            {
                if (!FunctionClass.IsError(this.支払先専用)) return false;
                if (!FunctionClass.IsError(this.仕入先名)) return false;
                if (!FunctionClass.IsError(this.仕入先名フリガナ)) return false;
            }
            else
            {
                if (!FunctionClass.IsError(this.窓口郵便番号)) return false;
                if (!FunctionClass.IsError(this.仕入先名)) return false;
                if (!FunctionClass.IsError(this.仕入先名フリガナ)) return false;
                if (!FunctionClass.IsError(this.窓口住所1)) return false;
                if (!FunctionClass.IsError(this.窓口電話番号1)) return false;
                if (!FunctionClass.IsError(this.窓口電話番号2)) return false;
                if (!FunctionClass.IsError(this.窓口電話番号3)) return false;
                if (!FunctionClass.IsError(this.窓口ファックス番号1)) return false;
                if (!FunctionClass.IsError(this.窓口ファックス番号2)) return false;
                if (!FunctionClass.IsError(this.窓口ファックス番号3)) return false;
                if (!FunctionClass.IsError(this.支払先専用)) return false;
                if (!FunctionClass.IsError(this.評価ランク)) return false;
                if (!FunctionClass.IsError(this.担当者名)) return false;
            }

            return true;
        }

        private void コマンド複写_Click(object sender, EventArgs e)
        {
            try
            {
                仕入先名.Focus();
                コマンド複写.Enabled = false;

                CommonConnect();
                // 初期値設定

                仕入先コード.Text = FunctionClass.GetNewCode(cn, CommonConstants.CH_SUPPLIER).Substring(8);
                Revision.Text = "1";
                作成日時.Text = null;
                作成者コード.Text = null;
                作成者名.Text = null;
                更新日時.Text = null;
                更新者コード.Text = null;
                更新者名.Text = null;

                // インターフェース更新
                仕入先コード.Enabled = false;
                コマンド新規.Enabled = false;
                コマンド修正.Enabled = false;
                //コマンド複写.Enabled = false;
                コマンド削除.Enabled = false;
                コマンド登録.Enabled = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{this.Name}_コマンド複写_Click - {ex.GetType().Name} : {ex.Message}");
                MessageBox.Show($"{ex.GetType().Name} : {ex.Message}", "複写コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CopyData(string codeString, int editionNumber = -1)
        {
            try
            {
                // キー情報を設定
                仕入先コード.Text = codeString;
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

        private void データ複写ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                // With ブロック内ではコントロールに対して直接アクセス可能
                this.窓口郵便番号.Text = this.郵便番号.Text;
                this.窓口住所1.Text = this.住所1.Text;
                this.窓口住所2.Text = this.住所2.Text;
                this.窓口電話番号1.Text = this.電話番号1.Text;
                this.窓口電話番号2.Text = this.電話番号2.Text;
                this.窓口電話番号3.Text = this.電話番号3.Text;
                this.窓口ファックス番号1.Text = this.ファックス番号1.Text;
                this.窓口ファックス番号2.Text = this.ファックス番号2.Text;
                this.窓口ファックス番号3.Text = this.ファックス番号3.Text;
                this.窓口メールアドレス.Text = this.メールアドレス.Text;

                ChangedData(true);

            }
            catch (Exception ex)
            {
                MessageBox.Show($"データ複写ボタン_Click - {ex.GetType().Name} : {ex.Message}");
            }
        }

        private void コマンド削除_Click(object sender, EventArgs e)
        {
            try
            {
                string strCode;         // 削除するデータのコード
                int intEdition;         // 削除するデータの版数
                string strMsg;

                strCode = this.CurrentCode;
                intEdition = this.CurrentRevision;
                FunctionClass fn = new FunctionClass();

                DialogResult intRes;

                if (IsDeleted)
                {
                    // 削除済みのため復元処理
                    strMsg = $"仕入先コード　：　{CurrentCode}\n\n" +
                              "このデータは削除されています。復元しますか？";

                    intRes = MessageBox.Show(strMsg, "削除コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    if (intRes == DialogResult.Cancel)
                    {
                        goto Bye_コマンド削除_Click;
                    }
                    else
                    {

                        var authForm = new F_認証();
                        authForm.ShowDialog();
                        if (string.IsNullOrEmpty(CommonConstants.LoginUserCode))
                        {

                            //while (strCertificateCode == "")
                            //{

                            //if (Form.ActiveForm == null || Form.ActiveForm.Name != "認証")
                            //{
                            MessageBox.Show("復元操作は取り消されました。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        //Application.DoEvents();
                        //trueは削除
                        if (SetDeleted(strCode, true))
                        {
                            MessageBox.Show("復元しました。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("エラーが発生しました。復元できませんでした。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                }
                else
                {
                    // 削除処理

                    strMsg = $"仕入先コード　：　{CurrentCode}\n\n" +
                "このデータを削除しますか？\n削除後、復元することができます。";

                    if (MessageBox.Show(strMsg, "削除コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        var authForm = new F_認証();
                        authForm.ShowDialog();
                        if (string.IsNullOrEmpty(CommonConstants.LoginUserCode))
                        {
                            MessageBox.Show("削除操作は取り消されました。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }

                        //Application.DoEvents();                        

                        //falseは復元
                        if (SetDeleted(strCode, false))
                        {
                            MessageBox.Show("削除しました。\n復元するには再度削除コマンドを実行してください。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("エラーが発生しました。\n削除できませんでした。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        // }
                    }

                }
            Bye_コマンド削除_Click:

                return;

            }
            catch (Exception ex)
            {
                MessageBox.Show(this.Name + "_コマンド削除_Click - " + ex.Message);
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

            varSaved1 = 無効日時.Text;
            varSaved2 = 無効者コード.Text;
            varSaved3 = 無効者名.Text;
            if (DoRestore)
            {
                無効日時.Text = null;
                無効者コード.Text = null;
                無効者名.Text = null;
            }
            else
            {
                無効日時.Text = dtmNow.ToString();
                無効者コード.Text = CommonConstants.LoginUserCode;
                無効者名.Text = employeename;
            }

            Connect();
            using (SqlTransaction transaction = cn.BeginTransaction())
            {
                try
                {
                    string strwhere = " 仕入先コード='" + this.仕入先コード.Text + "' and Revision=" + this.Revision.Text;

                    if (!DataUpdater.UpdateOrInsertDataFrom(this, cn, "M仕入先", strwhere, "仕入先コード", transaction))
                    {
                        無効日時.Text = varSaved1.ToString();
                        無効者コード.Text = varSaved2.ToString();
                        無効者名.Text = varSaved3.ToString();
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

        private void コマンドメーカー_Click(object sender, EventArgs e)
        {

            if (ActiveControl == コマンドメーカー)
            {
                GetNextControl(コマンドメーカー, false).Focus();
            }

            Form form = new F_メーカー管理();
            form.Show();
        }


        private void コマンド印刷_Click(object sender, EventArgs e)
        {
            // デスクトップフォルダのパスを取得
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            // 画面のキャプチャをデスクトップに保存
            string screenshotFileName = "screenshot.png";
            string screenshotFilePath = Path.Combine(desktopPath, screenshotFileName);
            OriginalClass.CaptureActiveForm(screenshotFilePath);

            // 印刷ダイアログを表示
            OriginalClass.PrintScreen(screenshotFilePath);
        }


        private void コマンド承認_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。", "承認コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド確定_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。", "確定コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private void コマンド終了_Click(object sender, EventArgs e)
        {
            Close(); // フォームを閉じる
        }

        private void Form_Unload(object sender, FormClosingEventArgs e)
        {
            //string LoginUserCode = "000";//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            LocalSetting test = new LocalSetting();
            test.SavePlace(CommonConstants.LoginUserCode, this);

            try
            {
                // 変更された場合
                if (IsChanged)
                {
                    var intRes = MessageBox.Show("変更内容を登録しますか？", BASE_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    switch (intRes)
                    {
                        case DialogResult.Yes:
                            if (SaveData())
                            {
                                // 保存に成功した場合
                                return;
                            }
                            else
                            {
                                if (MessageBox.Show("登録できませんでした。" + Environment.NewLine +
                                    "強制終了しますか？" + Environment.NewLine +
                                    "[はい]を選択した場合、仕入先コードは破棄されます。" + Environment.NewLine +
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
                                "仕入先コード　：　" + CurrentCode, BASE_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    if (コマンドメーカー.Enabled) コマンドメーカー_Click(sender, e);
                    break;
                case Keys.F6:
                    //if (コマンドメール.Enabled)
                    //{
                    //    コマンドメール.Focus();
                    //    コマンドメール_Click(sender, e);
                    //}
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

        private void UpdatedControl(Control controlObject)
        {
            try
            {
                string strName = controlObject.Name;
                object varValue = controlObject.Text;

                switch (strName)
                {
                    case "仕入先コード":
                        FunctionClass fn = new FunctionClass();
                        fn.DoWait("読み込んでいます...");


                        LoadHeader(this, this.仕入先コード.Text);

                        if (振込手数料負担コード.SelectedValue != null && 振込手数料負担コード.SelectedValue.ToString() == "3")

                        {
                            振込手数料上限金額.Enabled = true;
                        }
                        else
                        {
                            振込手数料上限金額.Enabled = false;
                        }

                        this.コマンド複写.Enabled = true;
                        this.コマンド削除.Enabled = true;
                        fn.WaitForm.Close();
                        break;

                    case "振込手数料負担コード":


                        if (controlObject is ComboBox comboBox)
                        {
                            if (comboBox.SelectedValue is int selectedValue && selectedValue == 3)
                            {
                                if (string.IsNullOrEmpty(振込手数料上限金額.Text))
                                {
                                    振込手数料上限金額.Text = "10000";
                                }
                                振込手数料上限金額.Enabled = true;
                                振込手数料上限金額.Focus();
                            }
                            else
                            {
                                支払先専用.Focus();
                                振込手数料上限金額.Enabled = false;
                            }
                        }
                        break;

                }
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_UpdatedControl - " + ex.Message);
            }
        }


        private void LoadData(string codeString, int editionNumber = -1)
        {
            // ヘッダ部の表示
            LoadHeader(this, codeString, editionNumber);

            FunctionClass.LockData(this, this.IsDeleted, "仕入先コード");
            this.コマンド複写.Enabled = true;
            this.コマンド削除.Enabled = !this.IsDeleted;
            this.コマンドメール.Enabled = !this.IsDeleted;
        }


        public bool LoadHeader(Form formObject, string codeString, int editionNumber = -1)
        {
            bool loadHeader = false;

            Connect();

            string strSQL;
            strSQL = "SELECT * FROM V仕入先ヘッダ WHERE 仕入先コード='" + codeString + "'";

            VariableSet.SetTable2Form(this, strSQL, cn);
            loadHeader = true;

            return loadHeader;
        }

        private void ウェブアドレス_Click(object sender, EventArgs e)
        {
            string inputText = WWWアドレス.Text;

            // 入力が有効な URL の形式であるかを確認
            if (OriginalClass.IsValidUrl(inputText))
            {
                OriginalClass.OpenUrl(inputText);

            }
        }

        private async void 郵便番号_Validated(object sender, EventArgs e)
        {
            // 郵便番号のテキストボックスの内容を取得
            string zipCode = 郵便番号.Text;

            // 郵便番号が正しい形式かどうかを確認
            if (OriginalClass.IsValidZipCode(zipCode) && string.IsNullOrEmpty(住所1.Text))
            {
                // 郵便番号APIを使用して住所情報を取得
                string address = await OriginalClass.GetAddressFromZipCode(zipCode);
                住所1.Text = address;
            }
            else
            {
                // 郵便番号が正しい形式でない場合、エラーメッセージなどを表示
                住所1.Text = null;
            }

            //UpdatedControl((Control)sender);
        }

        private void ウェブアドレス_TextChanged(object sender, EventArgs e)
        {
            if (!FunctionClass.LimitText(this.ActiveControl, 100)) return;
            ChangedData(true);
        }


        private void 仕入先コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            UpdatedControl((Control)sender);
        }
        private void 仕入先コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            FunctionClass.IsError((Control)sender);
        }

        private void 仕入先コード_KeyDown(object sender, KeyEventArgs e)
        {
            // 入力された値がエラー値の場合、textプロパティが設定できなくなるときの対処
            if (e.KeyCode == Keys.Return) // Enter キーが押されたとき
            {
                string strCode = 仕入先コード.Text;
                if (string.IsNullOrEmpty(strCode)) return;

                strCode = strCode.PadLeft(8, '0'); // ゼロで桁を埋める例
                if (strCode != 仕入先コード.Text)
                {
                    仕入先コード.Text = strCode;
                }
            }
        }

        private void 仕入先名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 40);
            ChangedData(true);
        }


        private void 仕入先名フリガナ_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 100);
            ChangedData(true);
        }

        private void 住所1_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 50);
            ChangedData(true);
        }


        private void 住所2_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 50);
            ChangedData(true);
        }


        private void 担当者メールアドレス_TextChanged(object sender, EventArgs e)
        {
            //FunctionClass.LimitText(((TextBox)sender), 100);
            ChangedData(true);
        }

        private void 担当者名_Validated(object sender, EventArgs e)
        {
            UpdatedControl((Control)sender);
        }


        private void 担当者名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 20);
            ChangedData(true);
        }



        private void 電話番号1_Validated(object sender, EventArgs e)
        {
            UpdatedControl((Control)sender);
        }


        private void 電話番号1_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 4);
            ChangedData(true);
        }


        private void 電話番号2_Validated(object sender, EventArgs e)
        {
            UpdatedControl((Control)sender);
        }


        private void 電話番号2_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 4);
            ChangedData(true);
        }



        private void 電話番号3_Validated(object sender, EventArgs e)
        {
            UpdatedControl((Control)sender);
        }


        private void 電話番号3_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 4);
            ChangedData(true);
        }

        private void FAX番号1_Validated(object sender, EventArgs e)
        {
            UpdatedControl((Control)sender);
        }


        private void FAX番号1_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 4);
            ChangedData(true);
        }


        private void FAX番号2_Validated(object sender, EventArgs e)
        {
            UpdatedControl((Control)sender);
        }


        private void FAX番号2_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 4);
            ChangedData(true);
        }



        private void FAX番号3_Validated(object sender, EventArgs e)
        {
            UpdatedControl((Control)sender);
        }


        private void FAX番号3_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 4);
            ChangedData(true);
        }

        private void 備考_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 4000);
            ChangedData(true);
        }


        private void 郵便番号_TextChanged(object sender, EventArgs e)
        {
            //FunctionClass.LimitText(((TextBox)sender), 7);
            ChangedData(true);
        }


        private void 仕入先名フリガナ_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■半角カタカナは入力しないでください。　■「カブシキガイシャ」等は省略します。";
        }

        private void 仕入先名フリガナ_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 住所1_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■建物名以外の住所を入力します。　■全角２５文字まで入力できます。";
        }

        private void 住所1_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 住所2_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■建物名を入力します。　■全角２５文字まで入力できます。";
        }

        private void 住所2_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 担当者名_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■仕入先の主な担当者名を入力します。　■全角１０文字まで入力できます。";
        }

        private void 担当者名_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 担当者メールアドレス_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■電子メールアドレスを入力します。通常は半角文字で入力してください。";
        }

        private void 担当者メールアドレス_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void ウェブアドレス_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■ウェブアドレスを入力します。通常は半角文字で入力します。";
        }

        private void ウェブアドレス_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 備考_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■全角4000文字まで入力できます。";
        }

        private void 備考_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 仕入先コード_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■仕入先コードを入力します。　■半角８文字まで入力できます。";
        }

        private void 仕入先コード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 8);
        }

        private void 評価ランク_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■部品の受入評価ランクを選択します。　■[space] キーでドロップダウンリスト表示。";
        }

        private void 評価ランク_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 窓口郵便番号_TextChanged(object sender, EventArgs e)
        {
            //FunctionClass.LimitText(((TextBox)sender), 7);
            ChangedData(true);
        }

        private async void 窓口郵便番号_Validated(object sender, EventArgs e)
        {
            string zipCode = 窓口郵便番号.Text;

            if (OriginalClass.IsValidZipCode(zipCode) && string.IsNullOrEmpty(窓口住所1.Text))
            {
                string address = await OriginalClass.GetAddressFromZipCode(zipCode);
                窓口住所1.Text = address;
            }
            else
            {
                窓口住所1.Text = null;
            }

        }

        private void 窓口住所1_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■建物名以外の住所を入力します。　■全角２５文字まで入力できます。";
        }

        private void 窓口住所1_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 50);
            ChangedData(true);
        }

        private void 窓口住所2_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■建物名を入力します。　■全角２５文字まで入力できます。";
        }

        private void 窓口住所2_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 50);
            ChangedData(true);
        }

        private void 窓口電話番号1_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 4);
            ChangedData(true);
        }

        private void 窓口電話番号2_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 4);
            ChangedData(true);
        }

        private void 窓口電話番号3_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 4);
            ChangedData(true);
        }

        private void 窓口ファックス番号1_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 4);
            ChangedData(true);
        }

        private void 窓口ファックス番号2_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 4);
            ChangedData(true);
        }

        private void 窓口ファックス番号3_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 4);
            ChangedData(true);
        }

        private void 窓口メールアドレス_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■担当者の電子メールアドレスを入力します。通常は半角文字で入力します。";
        }

        private void 窓口メールアドレス_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 担当者名2_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■仕入先の主な担当者名を入力します。　■全角１０文字まで入力できます。";
        }

        private void 担当者名2_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 20);
            ChangedData(true);
        }

        private void 担当者名3_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 20);
            ChangedData(true);
        }

        private void 担当者名3_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■仕入先の主な担当者名を入力します。　■全角１０文字まで入力できます。";
        }

        private void Contact1PhoneNumber_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 20);
            ChangedData(true);
        }

        private void Contact1PhoneNumber2_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 20);
            ChangedData(true);
        }

        private void Contact1PhoneNumber3_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 20);
            ChangedData(true);
        }

        private void Contact1MailAddress_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 50);
            ChangedData(true);
        }

        private void Contact2MailAddress_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 50);
            ChangedData(true);
        }

        private void Contact3MailAddress_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 50);
            ChangedData(true);
        }

        private void 支払先専用_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■[支払先] マスタとしてのみ使用するかどうかを指定します。";
        }

        private void 支払先専用_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void CloseDay_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 振込先金融機関名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 50);
            ChangedData(true);
        }

        private void 振込先金融機関分類コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 振込先金融機関コード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 10);
            ChangedData(true);
        }

        private void 振込先金融機関店分類コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 振込先金融機関支店名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 50);
            ChangedData(true);
        }

        private void 振込先金融機関支店コード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 10);
            ChangedData(true);
        }

        private void 振込先口座区分コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 振込先口座番号_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 20);
            ChangedData(true);
        }

        private void 振込先口座名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((TextBox)sender), 50);
            ChangedData(true);
        }

        private void 振込手数料負担コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangedData(true);
            UpdatedControl((Control)sender);
        }

        private void 振込手数料上限金額_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■自社が負担する振込手数料の上限金額を入力します。支払金額が上限金額未満のとき、自社負担となります。";
        }

        private void 振込手数料上限金額_TextChanged(object sender, EventArgs e)
        {
            //少数以下を非表示
            if (decimal.TryParse(振込手数料上限金額.Text, out decimal value))
            {
                振込手数料上限金額.Text = value.ToString("N0");
            }
            ChangedData(true);
        }

        private void 手形発送先郵便番号_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■手形の発送先郵便番号を入力します。　■「-」は省略します。";
        }

        private async void 手形発送先郵便番号_Validated(object sender, EventArgs e)
        {
            string zipCode = 手形発送先郵便番号.Text;

            if (OriginalClass.IsValidZipCode(zipCode))
            {
                string address = await OriginalClass.GetAddressFromZipCode(zipCode);
                手形発送先住所.Text = address;
            }
            else
            {
                手形発送先住所.Text = null;
            }
        }

        private void 手形発送先住所_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■手形の発送先住所を入力します。　■全角２５文字まで入力できます。";
        }

        private void 手形発送先住所_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 手形発送先建物名_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■手形の発送先住所（建物名）を入力します。　■全角２５文字まで入力できます。";
        }

        private void 手形発送先建物名_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 手形発送先部署_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 手形発送先電話番号_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "■手形の発送先電話番号を入力します。　■「-」は省略できません。";
        }

        private void 相殺有無_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void label43_Click(object sender, EventArgs e)
        {

        }
    }


}
