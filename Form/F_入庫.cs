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
                        if (!ErrCheck())
                        {
                            return;
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
                            if (!ErrCheck())
                                goto Bye_コマンド新規_Click;

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

        private bool ErrCheck()
        {
            //入力確認    
            if (!FunctionClass.IsError(this.入庫コード)) return false;
            return true;
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















    }
}
