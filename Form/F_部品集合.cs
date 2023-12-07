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
    public partial class F_部品集合 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "部品集合";
        private int selected_frame = 0;

        public F_部品集合()
        {
            this.Text = "部品集合";       // ウィンドウタイトルを設定
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


                if (string.IsNullOrEmpty(args))
                {
                    コマンド新規_Click(sender, e);
                }
                else
                {
                    //コマンド読込_Click(sender, e);
                    //if (!string.IsNullOrEmpty(args))
                    //{
                    //    this.部品コード.Text = args;
                    //    UpdatedControl(部品コード);
                    //}
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

        private void Form_Unload(object sender, FormClosingEventArgs e)
        {
            string LoginUserCode = CommonConstants.LoginUserCode;//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            LocalSetting test = new LocalSetting();
            test.SavePlace(LoginUserCode, this);

            try
            {

                Connect();

                // データへの変更がないときの処理
                //if (!IsChanged)
                //{
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
                    //return;
                //}

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

        //public bool IsChanged
        //{
        //    get
        //    {
        //        return コマンド登録.Enabled;
        //    }
        //}

        //public bool IsNewData
        //{
        //    get
        //    {
        //        return !コマンド新規.Enabled;
        //    }
        //}

        //public string CurrentCode
        //{
        //    get
        //    {
        //        return string.IsNullOrEmpty(部品コード.Text) ? "" : 部品コード.Text;
        //    }
        //}

        //public int CurrentEdition
        //{
        //    get
        //    {
        //        return string.IsNullOrEmpty(版数.Text) ? 0 : int.Parse(版数.Text);
        //    }
        //}

        //public bool IsIncluded
        //{
        //    get
        //    {
        //        return !string.IsNullOrEmpty(部品集合コード.Text);
        //    }
        //}

        private bool SaveData()
        {

            Connect();
            SqlTransaction transaction = cn.BeginTransaction();
            {
                try
                {

                    //DateTime dteNow = DateTime.Now;
                    //Control objControl1 = null;
                    //Control objControl2 = null;
                    //Control objControl3 = null;
                    //Control objControl4 = null;
                    //Control objControl5 = null;
                    //Control objControl6 = null;
                    //object varSaved1 = null;
                    //object varSaved2 = null;
                    //object varSaved3 = null;
                    //object varSaved4 = null;
                    //object varSaved5 = null;
                    //object varSaved6 = null;

                    //if (IsNewData)
                    //{
                        //objControl1 = this.作成日時;
                        //objControl2 = this.作成者コード;
                        //objControl3 = this.CreatorName;
                        //varSaved1 = objControl1.Text;
                        //varSaved2 = objControl2.Text;
                        //varSaved3 = objControl3.Text;
                        //objControl1.Text = dteNow.ToString(); // ここでDateTimeをstringに変換して設定
                        //objControl2.Text = CommonConstants.LoginUserCode;
                        //objControl3.Text = CommonConstants.LoginUserFullName;
                    //}

                    //objControl4 = this.更新日時;
                    //objControl5 = this.更新者コード;
                    //objControl6 = this.UpdaterName;

                    //varSaved4 = objControl4.Text;
                    //varSaved5 = objControl5.Text;
                    //varSaved6 = objControl6.Text;

                    //objControl4.Text = dteNow.ToString(); // ここでDateTimeをstringに変換して設定
                    //objControl5.Text = CommonConstants.LoginUserCode;
                    //objControl6.Text = CommonConstants.LoginUserFullName;


                    //string strwhere = " 部品コード='" + this.部品コード.Text + "'";

                    //if (!DataUpdater.UpdateOrInsertDataFrom(this, cn, "M部品", strwhere, "部品コード", transaction))
                    //{


                    //    if (IsNewData)
                    //    {
                    //        objControl1.Text = varSaved1.ToString();
                    //        objControl2.Text = varSaved2.ToString();
                    //        objControl3.Text = varSaved3.ToString();

                    //    }

                    //    objControl4.Text = varSaved4.ToString();
                    //    objControl5.Text = varSaved5.ToString();
                    //    objControl6.Text = varSaved6.ToString();

                    //    return false;
                    //}

                    // トランザクションをコミット
                    transaction.Commit();


                    //部品コード.Enabled = true;

                    // 新規モードのときは修正モードへ移行する
                    //if (IsNewData)
                    //{
                    //    コマンド新規.Enabled = true;
                    //}

                    //コマンド複写.Enabled = true;
                    //コマンド削除.Enabled = true;
                    //コマンド登録.Enabled = false;

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in SaveData: " + ex.Message);
                    return false;
                }
            }
        }

        private void コマンド登録_Click(object sender, EventArgs e)
        {

            //if (this.ActiveControl == this.コマンド登録)
            //{
            //    GetNextControl(コマンド登録, false).Focus();
            //}

            // エラーチェック
            if (!ErrCheck())
            {
                goto Bye_コマンド登録_Click;
            }

            FunctionClass fn = new FunctionClass();
            fn.DoWait("登録しています...");


            // 登録処理
            if (SaveData())
            {
                // 登録に成功した
                ChangedData(false); // データ変更取り消し

                //if (this.IsNewData)
                //{
                //    // 新規モードのとき
                //    this.コマンド新規.Enabled = true;
                //    this.コマンド読込.Enabled = false;
                //    this.コマンド廃止.Enabled = true;
                //}
                // UpdatePurGridの呼び出し
                // Call UpdatePurGrid();
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

        Bye_コマンド登録_Click:
            return;

        }

        private bool ErrCheck()
        {
            //入力確認    
            //if (!FunctionClass.IsError(this.部品コード)) return false;
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

            // コードにフォーカスがある状態でサブフォームから呼び出されたときの対処
            //if (this.ActiveControl == this.部品コード)
            //{
            //    this.品名.Focus();
            //}

            //this.部品コード.Enabled = !isChanged;
            //this.改版ボタン.Enabled = !isChanged;
            //// this.コマンド複写.Enabled = !isChanged; // コマンド複写についての情報が提供されていないためコメントアウト
            //this.コマンド削除.Enabled = !isChanged;
            //this.コマンド登録.Enabled = isChanged;

            // RoHSの状態表示を更新する
            ShowRohsStatus();
        }

        private void コマンド新規_Click(object sender, EventArgs e)
        {
            try
            {
                Connect();

                Cursor.Current = Cursors.WaitCursor;
                this.DoubleBuffered = true;

                //if (this.ActiveControl == this.コマンド新規)
                //{
                //    this.コマンド新規.Focus();
                //}

                // 変更がある
                //if (this.IsChanged)
                //{
                //    var intRes = MessageBox.Show("変更内容を登録しますか？", "新規コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                //    switch (intRes)
                //    {
                //        case DialogResult.Yes:
                //            // 登録処理
                //            if (!SaveData())
                //            {
                //                MessageBox.Show("エラーのため登録できません。", "新規コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //                goto Bye_コマンド新規_Click;
                //            }
                //            break;
                //        case DialogResult.Cancel:
                //            goto Bye_コマンド新規_Click;
                //    }
                //}

                VariableSet.SetControls(this);
                //DispGrid(CurrentCode);

                //string code = FunctionClass.採番(cn, "PAR");
                //部品コード.Text = code.Substring(Math.Max(0, code.Length - 8));
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
                //廃止.Checked = false;


                ChangedData(false);

                //InventoryAmount.Text = 0.ToString();
                //ShowRohsStatus();
                //品名.Focus();
                //部品コード.Enabled = false;
                //改版ボタン.Enabled = false;
                //コマンド新規.Enabled = false;
                //コマンド読込.Enabled = true;
                //コマンド削除.Enabled = false;
                //コマンド廃止.Enabled = false;
                //コマンドツール.Enabled = false;
                //コマンド登録.Enabled = false;



            }
            finally
            {
                
                this.DoubleBuffered = false;
                Cursor.Current = Cursors.Default;
            }

        Bye_コマンド新規_Click:
            return;
        }

        private void ShowRohsStatus()
        {
    //        if (this.Rohs2ProvisionalRegisteredStatusCode.Checked)
    //        {
    //            this.RohsStatusCode.SelectedValue = 5;
    //            // this.RohsStatusName = "仮RoHS2";
    //        }
    //        else
    //        {
    //            if ((Rohs2ChemSherpaStatusCode.SelectedValue != null && (int)Rohs2ChemSherpaStatusCode.SelectedValue == 2) ||
    //(Rohs2JampAisStatusCode.SelectedValue != null && (int)Rohs2JampAisStatusCode.SelectedValue == 2) ||
    //(Rohs2NonInclusionCertificationStatusCode.SelectedValue != null && (int)Rohs2NonInclusionCertificationStatusCode.SelectedValue == 1) ||
    //(Rohs2DocumentStatusCode.SelectedValue != null && (int)Rohs2DocumentStatusCode.SelectedValue == 2))
    //            {
    //                this.RohsStatusCode.SelectedValue = 2;
    //                // this.RohsStatusName = "RoHS2";
    //            }
    //            else if ((Rohs1ChemSherpaStatusCode.SelectedValue != null && (int)Rohs1ChemSherpaStatusCode.SelectedValue == 2) ||
    //     (JampAis.SelectedValue != null && (int)JampAis.SelectedValue == 2) ||
    //     (非含有証明書.SelectedValue != null && (byte)非含有証明書.SelectedValue == 1) ||
    //     (RoHS資料.SelectedValue != null && (Int16)RoHS資料.SelectedValue == 2))
    //            {
    //                this.RohsStatusCode.SelectedValue = 6;
    //                // this.RohsStatusName = "RoHS2非対応";
    //            }
    //            else
    //            {
    //                this.RohsStatusCode.SelectedValue = 3;
    //                // this.RohsStatusName = "RoHS1非対応";
    //            }
    //        }
        }

        private void コマンド読込_Click(object sender, EventArgs e)
        {
            if (!AskSave()) { return; }


            // strOpenArgsがどのように設定されているかに依存します。
            // もしstrOpenArgsに関連する処理が必要な場合はここに追加してください。

            // 各コントロールの値をクリア
            VariableSet.SetControls(this);

            // コントロールを操作
            //部品コード.Enabled = true;
            //部品コード.Focus();
            //改版ボタン.Enabled = false;
            //コマンド新規.Enabled = true;
            //コマンド読込.Enabled = false;
            //コマンド廃止.Enabled = false;
            //コマンド登録.Enabled = false;
        }

        private bool AskSave()
        {
            try
            {
                Connect();

                DialogResult response;
                //bool isNewData = IsNewData; // 仮定されたIsNewDataプロパティの取得
                //string currentCode = CurrentCode; // 仮定されたCurrentCodeプロパティの取得

                //if (コマンド登録.Enabled)
                //{
                //    response = MessageBox.Show("変更内容を登録しますか？", "質問", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                //    switch (response)
                //    {
                //        case DialogResult.Yes:
                //            if (SaveData())
                //            {
                //                // 保存が成功した場合の処理
                //                return true;
                //            }
                //            else
                //            {
                //                return false;
                //            }
                //        case DialogResult.No:
                //            break;
                //        case DialogResult.Cancel:
                //            return false;
                //    }
                //}

                // 登録しない場合
                //if (isNewData)
                //{
                //    if (!string.IsNullOrEmpty(currentCode))
                //    {
                //        // 部品コードが採番された場合、番号を戻す処理
                //        if (FunctionClass.Recycle(cn, "PAR" + currentCode))
                //        {
                //            MessageBox.Show("部品コードは破棄されました。" + Environment.NewLine + Environment.NewLine +
                //                            "部品コード： " + currentCode, "通知", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        }
                //        else
                //        {
                //            MessageBox.Show("部品コードを戻す際にエラーが発生しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                //        }
                //    }
                //}

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in AskSave: " + ex.Message);
                return false;
            }
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
                //if (this.ActiveControl == this.改版ボタン)
                //{
                //    GetNextControl(改版ボタン, false).Focus();
                //}


                MessageBox.Show("部品の改版機能は未完成です。\n履歴に登録される情報は完全ではありません。", "改版", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                if (MessageBox.Show("改版しますか？\n\n・旧版データは履歴コマンドから参照できます。\n・最新版の部品データが有効になります。\n・この操作を元に戻すことはできません。",
                                    "改版", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }


                FunctionClass fn = new FunctionClass();
                fn.DoWait("改版しています...");

                CommonConnect();

                if (SaveData())
                {
                    //if (AddHistory(cn, this.CurrentCode, this.CurrentEdition))
                    //{
                    //    //this.部品コード.Requery;
                    //    // ■ なぜかRequeryしてもColumn(1)がNULLとなるので、版数を+1する
                    //    this.版数.Text = (Convert.ToInt32(this.CurrentEdition) + 1).ToString();
                    //    this.コマンドツール.Enabled = true;

                    //    fn.WaitForm.Close();
                    //}
                    //else
                    //{
                    //    fn.WaitForm.Close();
                    //    MessageBox.Show("改版できませんでした。", "改版", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    //}
                }
                else
                {
                    fn.WaitForm.Close();
                    MessageBox.Show("改版できませんでした。", "改版", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_改版ボタン_Click - " + ex.Message);
                MessageBox.Show("エラーが発生しました。", BASE_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void コマンド改版_Click(object sender, EventArgs e)
        {

        }

        private void コマンド部品_Click(object sender, EventArgs e)
        {

        }

        private void コマンドユニット管理_Click(object sender, EventArgs e)
        {

        }

        private F_検索 SearchForm;

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

                //OpenForm("認証", CommonConstants.USER_CODE_TECH);

                //while (string.IsNullOrEmpty(certificateCode))
                //{
                //    if (SysCmd(acSysCmdGetObjectState, acForm, "認証") == 0)
                //    {
                //        MessageBox.Show("削除はキャンセルされました。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //        return;
                //    }

                //    Application.DoEvents();
                //}

                // 部品情報削除
                FunctionClass fn = new FunctionClass();
                fn.DoWait("削除しています...");

                Connect();

                // 削除に成功すれば新規モードへ移行する
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

                // 以下、初期値の設定
                //string code = FunctionClass.採番(cn, "PAR");
                //部品コード.Text = code.Substring(Math.Max(0, code.Length - 8));
                //this.版数.Text = 1.ToString();
                //this.InventoryAmount.Text = 0.ToString();
                //this.作成日時.Text = null;
                //this.作成者コード.Text = null;
                //this.CreatorName.Text = null;
                //this.更新日時.Text = null;
                //this.更新者コード.Text = null;
                //this.UpdaterName.Text = null;

                // インターフェース更新
                //this.コマンド新規.Enabled = false;
                //this.コマンド読込.Enabled = true;

                // 使用先の表示
                //DispGrid(this.CurrentCode);

                // RoHS対応状態の表示を更新する
                ShowRohsStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "複写コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }

        private void コマンド編集_Click(object sender, EventArgs e)
        {

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
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;

                case Keys.F1:
                    //if (コマンド新規.Enabled)
                    //{
                    //    コマンド新規.Focus();
                    //    コマンド新規_Click(sender, e);
                    //}
                    break;
                case Keys.F2:
                    //if (コマンド読込.Enabled)
                    //{
                    //    コマンド読込.Focus();
                    //    コマンド読込_Click(sender, e);
                    //}
                    break;
                case Keys.F3:
                    //if (コマンド複写.Enabled) コマンド複写_Click(sender, e);
                    break;
                case Keys.F4:
                    //if (コマンド削除.Enabled) コマンド削除_Click(sender, e);
                    break;
                case Keys.F5:
                    //if (コマンドユニット.Enabled) コマンドユニット_Click(sender, e);
                    break;
                case Keys.F6:
                    //if (コマンドユニット表.Enabled) コマンドユニット表_Click(sender, e);
                    break;
                case Keys.F7:
                    //if (コマンド廃止.Enabled) コマンド廃止_Click(sender, e);
                    break;
                case Keys.F8:
                    //if (コマンドツール.Enabled) コマンドツール_Click(sender, e);
                    break;
                case Keys.F9:
                    //if (コマンド承認.Enabled) コマンド承認_Click(sender, e);
                    break;
                case Keys.F10:
                    //if (コマンド確定.Enabled) コマンド確定_Click(sender, e);
                    break;
                case Keys.F11:
                    if (コマンド登録.Enabled) コマンド登録_Click(sender, e);
                    break;
                case Keys.F12:
                    //if (コマンド終了.Enabled) コマンド終了_Click(sender, e);
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


    }
}
