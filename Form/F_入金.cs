﻿using System;
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
using System.Data.Common;
//using DocumentFormat.OpenXml.Wordprocessing;

namespace u_net
{
    public partial class F_入金 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "入金";
        private int selected_frame = 0;

        public F_入金()
        {
            this.Text = "入金";       // ウィンドウタイトルを設定
            this.MaximizeBox = false;  // 最大化ボタンを無効化
            this.MinimizeBox = false; //最小化ボタンを無効化

            InitializeComponent();

        }
        public string CurrentCode
        {
            get
            {
                return string.IsNullOrEmpty(入金コード.Text) ? "" : 入金コード.Text;
            }
        }

        public bool IsChanged
        {
            get
            {
                return コマンド登録.Enabled;
            }
        }

        public bool IsDemanded
        {
            get
            {
                return !string.IsNullOrEmpty(請求コード.Text) ? true : false;
            }
        }

        public bool IsNewData
        {
            get
            {
                return !コマンド新規.Enabled;
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
            string LoginUserCode = CommonConstants.LoginUserCode;
            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(LoginUserCode, this);

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
                    else
                    {
                        if (SetRelay())
                        {
                            if (fn.WaitForm != null) fn.WaitForm.Close();
                        }
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

                        入金コード.Text = args;

                        UpdatedControl(入金コード);
                        // 編集による変更がない状態へ遷移
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
                this.ResumeLayout();
                if (fn.WaitForm != null) fn.WaitForm.Close();
                this.Focus();
            }
        }


        private bool GoNewMode()
        {
            try
            {
                // 各コントロール値を初期化
                VariableSet.SetControls(this);

                Connect();

                入金コード.Text = FunctionClass.採番(cn, "B").ToString();
                入金日.Text = DateTime.Now.ToString("yyyy/MM/dd");

                // 明細部の初期化
                string strSQL = "SELECT * FROM T入金明細 WHERE 入金コード='" + CurrentCode + "' ORDER BY 明細番号";
                if (!LoadDetails(strSQL, 入金明細1.Detail)) return false;

                // ヘッダ部動作制御
                FunctionClass.LockData(this, false);
                入金日.Focus();
                入金コード.Enabled = false;

                コマンド新規.Enabled = false;
                コマンド修正.Enabled = true;
                コマンド複写.Enabled = false;
                コマンド削除.Enabled = false;
                コマンド承認.Enabled = false;
                コマンド確定.Enabled = false;
                コマンド登録.Enabled = false;

                // 明細部動作制御
                入金明細1.Detail.AllowUserToDeleteRows = true;
                入金明細1.Detail.ReadOnly = false;
                入金明細1.Detail.AllowUserToAddRows = true;

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

                // 明細部の初期化
                string strSQL = "SELECT * FROM T入金明細 WHERE 入金コード='" + CurrentCode + "' ORDER BY 明細番号";
                VariableSet.SetTable2Details(入金明細1.Detail, strSQL, cn);

                this.入金コード.Enabled = true;
                this.入金コード.Focus();

                FunctionClass.LockData(this, true, "入金コード");
                this.コマンド新規.Enabled = true;
                this.コマンド修正.Enabled = false;
                this.コマンド複写.Enabled = false;
                this.コマンド登録.Enabled = false;

                result = true;
                return result;
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.Name + "_GoModifyMode - " + ex.Message);
                return false;
            }
        }
        private bool SetRelay()
        {
            try
            {
                F_売掛一覧? fm = Application.OpenForms.OfType<F_売掛一覧>().FirstOrDefault();
                // 売掛一覧が開いている場合はリレー入力する
                if (fm != null)
                {
                    // 一覧のデータ件数が1件以上表示されているときのみリレー入力する
                    if (int.Parse(fm.表示件数.Text) > 0)
                    {
                        this.入金月.Text = fm.SalesMonth.ToString();
                        this.顧客コード.Focus();
                        this.顧客コード.Text = fm.CustomerCode;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("SetRelay - " + ex.Message);
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

        private void UpdatedControl(Control controlObject)
        {
            FunctionClass fn = new FunctionClass();
            try
            {
                string strSQL;

                switch (controlObject.Name)
                {
                    case "入金コード":
                        Connect();

                        fn.DoWait("読み込んでいます...");

                        // ヘッダ部の表示
                        if (!LoadHeader()) throw new Exception("初期化に失敗しました。");

                        // 明細部の表示
                        strSQL = $"SELECT * FROM T入金明細 WHERE 入金コード= '{this.CurrentCode}' ORDER BY 明細番号";

                        //明細表示
                        if (!VariableSet.SetTable2Details(入金明細1.Detail, strSQL, cn))
                            throw new Exception("初期化に失敗しました。");

                        // 動作を制御する
                        FunctionClass.LockData(this, IsDemanded, "入金コード");

                        入金明細1.Detail.AllowUserToAddRows = !IsDemanded;
                        入金明細1.Detail.AllowUserToDeleteRows = !this.IsDemanded;
                        入金明細1.Detail.ReadOnly = this.IsDemanded;
                        this.コマンド複写.Enabled = true;
                        this.コマンド削除.Enabled = !this.IsDemanded;

                        ChangedData(false);
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

        public bool LoadHeader()
        {
            try
            {
                Connect();
                string strSQL;
                strSQL = "SELECT * FROM V入金ヘッダ WHERE 入金コード='" + CurrentCode + "'";

                if (!VariableSet.SetTable2Form(this, strSQL, cn)) return false;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("読み込み時エラーです" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        private bool SaveData(bool NewData, string SaveCode)
        {

            {
                try
                {
                    Connect();
                    DateTime dteNow = FunctionClass.GetServerDate(cn);
                    cn.Close();
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

                    Connect();
                    if (RegTrans(SaveCode, cn))
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

        private bool RegTrans(string codeString, SqlConnection cn)
        {
            try
            {
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
                string strwhere = " 入金コード='" + codeString + "'";

                if (!DataUpdater.UpdateOrInsertDataFrom(this, cn, "T入金", strwhere, "入金コード", transaction))
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
                string strwhere = $"入金コード= '{codeString}'";
                //明細部の登録
                if (!DataUpdater.UpdateOrInsertDetails(this.入金明細1.Detail, cn, "T入金明細", strwhere, "入金コード", transaction))
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

        private void Form_Unload(object sender, FormClosingEventArgs e)
        {
            string LoginUserCode = CommonConstants.LoginUserCode;
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
            // エラーチェック
            if (!IsErrorData("入金コード"))
            {
                goto Bye_コマンド登録_Click;
            }

            FunctionClass fn = new FunctionClass();
            fn.DoWait("登録しています...");

            // 登録処理
            if (SaveData(IsNewData, CurrentCode))
            {
                // 登録に成功した
                ChangedData(false); // データ変更取り消し

                if (this.IsNewData)
                {
                    // 新規モードのとき
                    this.コマンド新規.Enabled = true;
                    this.コマンド修正.Enabled = false;
                }

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
                    varValue = controlObject.Text;
                }

                switch (controlObject.Name)
                {
                    case "入金日":
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

                    case "顧客コード":
                        if (varValue == null || string.IsNullOrEmpty(varValue.ToString()))
                        {
                            MessageBox.Show(controlObject.Name + "を入力してください。", "警告",
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }

                        string str1 = GetCustomerName(cn, varValue.ToString());

                        if (string.IsNullOrEmpty(str1))
                        {
                            MessageBox.Show("指定された顧客データはありません。", controlObject.Name,
                                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        else
                        {
                            顧客名.Text = str1;
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


        /// <summary>
        /// 顧客コードから顧客名を得る（入金用）
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="customerCode"></param>
        /// <returns></returns>
        public static string GetCustomerName(SqlConnection connection, string customerCode)
        {
            // 顧客名を格納する変数
            string customerName = "";

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;

                    // SQLクエリを構築
                    string query = "SELECT * FROM M顧客 WHERE 顧客コード = @CustomerCode AND 取引開始日 IS NOT NULL ";
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@CustomerCode", customerCode);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // レコードが存在する場合、顧客名を取得
                            string customerName1 = reader["顧客名"].ToString();
                            string customerName2 = reader["顧客名2"].ToString();

                            if (!string.IsNullOrEmpty(customerName2))
                            {
                                // 顧客名2が存在する場合、顧客名1と結合
                                customerName = customerName1 + " " + customerName2;
                            }
                            else
                            {
                                // 顧客名2が存在しない場合、顧客名1のみを使用
                                customerName = customerName1;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                // エラーハンドリングを行うか、エラーログを記録するなどの処理をここに追加できます
                Console.WriteLine("GetCustomerName - " + ex.Message);
            }

            return customerName;
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
            if (this.ActiveControl == this.入金コード)
            {
                this.入金日.Focus();
            }

            this.入金コード.Enabled = !isChanged;
            this.コマンド複写.Enabled = !isChanged;
            this.コマンド削除.Enabled = !isChanged;
            this.コマンド顧客.Enabled = !isChanged;
            this.コマンド登録.Enabled = isChanged;

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



        private void コマンド読込_Click(object sender, EventArgs e)
        {
            // if (!AskSave()) { return; }


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


        private void コマンド承認_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。", "承認コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド確定_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。", "確定コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド部品表_Click(object sender, EventArgs e)
        {
            try
            {

                //if (this.ActiveControl == this.コマンドユニット表)
                //{
                //    GetNextControl(コマンドユニット表, false).Focus();
                //}

                //string strCode = this.メーカーコード.Text;
                //if (string.IsNullOrEmpty(strCode))
                //{
                //    MessageBox.Show("メーカーコードを入力してください。", BASE_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    this.メーカーコード.Focus();
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

        private void コマンド部品定数表_Click(object sender, EventArgs e)
        {

        }

        private void コマンドツール_Click(object sender, EventArgs e)
        {

        }

        private F_検索 SearchForm;

        private void コマンド削除_Click(object sender, EventArgs e)
        {
            try
            {
                // 削除の確認
                DialogResult result = MessageBox.Show(
                    "入金コード　：　" + CurrentCode + "\n\n" +
                    "この入金データを削除します。\n" +
                    "削除後元に戻すことはできません。\n\n" +
                    "削除しますか？",
                    "削除コマンド",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No) return;
                Connect();
                // 削除処理
                if (DeleteData(cn, CurrentCode))
                {
                    MessageBox.Show("削除しました。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    // 新規モードへ移行する
                    if (!GoNewMode())
                    {
                        MessageBox.Show($"エラーのため新規モードへ移行できません。\n[{this.Name}]を終了します。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.Close();
                    }
                }
                else
                {
                    MessageBox.Show("削除できませんでした。\n他のユーザーにより請求処理されている可能性があります。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"エラーが発生しました。\n\n{ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        public bool DeleteData(SqlConnection connectionObject, string codeString)
        {
            SqlTransaction transaction = connectionObject.BeginTransaction();  // トランザクション開始
            try
            {
                string strKey;
                string strSQL1;
                string strSQL2;

                strKey = $"入金コード='{codeString}' AND 請求コード IS NULL";
                strSQL1 = $"SELECT * FROM T入金 WHERE {strKey}";

                SqlCommand cmd = new SqlCommand(strSQL1, cn);
                
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            //EOFの処理
                            return false;
                        }
                        else
                        {
                            // レコードが存在する場合の処理
                            strKey = $"入金コード='{codeString}'";
                            strSQL1 = $"DELETE T入金 WHERE {strKey}";
                            strSQL2 = $"DELETE T入金明細 WHERE {strKey}";

                            using (SqlCommand command = new SqlCommand(strSQL1, connectionObject, transaction))
                            {
                                command.ExecuteNonQuery();
                            }

                            using (SqlCommand command = new SqlCommand(strSQL2, connectionObject, transaction))
                            {
                                command.ExecuteNonQuery();
                            }
                        }
                    }              

                // トランザクション完了
                transaction.Commit();
                return true;
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine("_DeleteData - " + ex.Message);
                MessageBox.Show("エラーが発生しました。" + Environment.NewLine + Environment.NewLine +
                                ex.Message, "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
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

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "複写コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
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
                    if (コマンド顧客.Enabled) コマンド顧客_Click(sender, e);
                    break;
                case Keys.F6:
                    if (コマンド領収書.Enabled) コマンド領収書_Click(sender, e);
                    break;
                case Keys.F7:
                    //if (コマンド廃止.Enabled) コマンド廃止_Click(sender, e);
                    break;
                case Keys.F8:
                    //if (コマンドツール.Enabled) コマンドツール_Click(sender, e);
                    break;
                case Keys.F9:
                    // if (コマンド承認.Enabled) コマンド承認_Click(sender, e);
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

        private void 入金日_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■日付をyyyy/mm/ddの形式で入力します。　■[space]キーでカレンダーを表示します。";
        }

        private void 入金日_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 顧客コード_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■入金した顧客の顧客コードを入力します。　■[space]キーで検索ウィンドウを表示します。";
        }

        private void 顧客コード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 売掛年月_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■入金対象となる売掛年月を入力します。　■年月をyyyy/mm形式で入力します。";
        }

        private void 売掛年月_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 領収証但書_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■領収書の但し書欄へ表示する内容を入力します。　■全角20文字まで入力できます。";
        }

        private void 領収証但書_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 送付状摘要_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■領収書を送付する送付状の摘要内容を入力します。";
        }

        private void 送付状摘要_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void コマンド顧客_Click(object sender, EventArgs e)
        {
            string param;
            param = $" -sv:{CommonConstants.ServerInstanceName} -open:customer";
            FunctionClass.GetShell(param);
        }

        private void コマンド領収書_Click(object sender, EventArgs e)
        {
            MessageBox.Show("本機能は使用できません。", "領収書プレビュー", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }
}
