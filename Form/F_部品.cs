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

namespace u_net
{



    public partial class F_部品 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "部品";

        



        public F_部品()
        {
            this.Text = "部品";       // ウィンドウタイトルを設定
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

            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");

            //実行中フォーム起動
            string LoginUserCode = "000";//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(LoginUserCode, this);

            MyApi myapi = new MyApi();
            int xSize, ySize, intpixel, twipperdot;

            //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
            intpixel = myapi.GetLogPixel();
            twipperdot = myapi.GetTwipPerDot(intpixel);

            // DataGridViewの設定
            部品使用先.AllowUserToResizeColumns = true;
            部品使用先.Font = new Font("MS ゴシック", 10);
            部品使用先.DefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 210, 255);
            部品使用先.DefaultCellStyle.SelectionForeColor = Color.Black;
            部品使用先.GridColor = Color.FromArgb(230, 230, 230);
            部品使用先.ColumnHeadersDefaultCellStyle.Font = new Font("MS ゴシック", 9);
            部品使用先.DefaultCellStyle.Font = new Font("MS ゴシック", 10);
            部品使用先.DefaultCellStyle.ForeColor = Color.Black;


            this.m部品分類TableAdapter.Fill(this.newDataSet.M部品分類);
            this.m部品形状TableAdapter.Fill(this.newDataSet.M部品形状);
            this.rohsStatusCodeTableAdapter.Fill(this.newDataSet.RohsStatusCode);


            this.JampAis.DataSource = new KeyValuePair<int, String>[] {
                new KeyValuePair<int, String>(2, "有り"),
                new KeyValuePair<int, String>(1, "無し"),
            };
            this.JampAis.DisplayMember = "Value";
            this.JampAis.ValueMember = "Key";

            this.非含有証明書.DataSource = new KeyValuePair<int, String>[] {
                new KeyValuePair<int, String>(1, "返却済み"),
                new KeyValuePair<int, String>(2, "未返却"),
                new KeyValuePair<int, String>(3, "未提出"),
            };
            this.非含有証明書.DisplayMember = "Value";
            this.非含有証明書.ValueMember = "Key";

            this.RoHS資料.DataSource = new KeyValuePair<int, String>[] {
                new KeyValuePair<int, String>(2, "有り"),
                new KeyValuePair<int, String>(1, "無し"),
            };
            this.RoHS資料.DisplayMember = "Value";
            this.RoHS資料.ValueMember = "Key";

            this.Rohs1ChemSherpaStatusCode.DataSource = new KeyValuePair<int, String>[] {
                new KeyValuePair<int, String>(2, "有り"),
                new KeyValuePair<int, String>(1, "無し"),
            };
            this.Rohs1ChemSherpaStatusCode.DisplayMember = "Value";
            this.Rohs1ChemSherpaStatusCode.ValueMember = "Key";

            this.Rohs2JampAisStatusCode.DataSource = new KeyValuePair<int, String>[] {
                new KeyValuePair<int, String>(2, "有り"),
                new KeyValuePair<int, String>(1, "無し"),
            };
            this.Rohs2JampAisStatusCode.DisplayMember = "Value";
            this.Rohs2JampAisStatusCode.ValueMember = "Key";

            this.Rohs2NonInclusionCertificationStatusCode.DataSource = new KeyValuePair<int, String>[] {
                new KeyValuePair<int, String>(1, "返却済み"),
                new KeyValuePair<int, String>(2, "未返却"),
                new KeyValuePair<int, String>(3, "未提出"),
            };
            this.Rohs2NonInclusionCertificationStatusCode.DisplayMember = "Value";
            this.Rohs2NonInclusionCertificationStatusCode.ValueMember = "Key";

            this.Rohs2DocumentStatusCode.DataSource = new KeyValuePair<int, String>[] {
                new KeyValuePair<int, String>(2, "有り"),
                new KeyValuePair<int, String>(1, "無し"),
            };
            this.Rohs2DocumentStatusCode.DisplayMember = "Value";
            this.Rohs2DocumentStatusCode.ValueMember = "Key";

            this.Rohs2ChemSherpaStatusCode.DataSource = new KeyValuePair<int, String>[] {
                new KeyValuePair<int, String>(2, "有り"),
                new KeyValuePair<int, String>(1, "無し"),
            };
            this.Rohs2ChemSherpaStatusCode.DisplayMember = "Value";
            this.Rohs2ChemSherpaStatusCode.ValueMember = "Key";

            this.CalcInventoryCode.DataSource = new KeyValuePair<string, String>[] {
                new KeyValuePair<string, String>("01", "する"),
                new KeyValuePair<string, String>("02", "しない"),
            };
            this.CalcInventoryCode.DisplayMember = "Value";
            this.CalcInventoryCode.ValueMember = "Key";

            this.受入検査ランク.DataSource = new String[] {
                new String("A"),
                new String("B1"),
                new String("B2"),
                new String("C"),
                new String("D"),
            };





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
                    コマンド読込_Click(sender,e);
                    if (!string.IsNullOrEmpty(args))
                    {
                        this.部品コード.Text = args;
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


        private bool GoNewMode()
        {
            try
            {
                // 各コントロール値を初期化
                VariableSet.SetControls(this);

                CommonConnect();

                string code = FunctionClass.GetNewCode(cn, CommonConstants.CH_MAKER);
                this.メーカーコード.Text = code.Substring(code.Length - 8);
                // this.メーカーコード.Text = 採番(objConnection, CH_MAKER).Substring(採番(objConnection, CH_MAKER).Length - 8);
                this.版数.Text = 1.ToString();

                // 編集による変更がない状態へ遷移する
                ChangedData(false);

                // ヘッダ部動作制御
                FunctionClass.LockData(this, false);
                this.品名.Focus();
                this.メーカーコード.Enabled = false;
                this.コマンド新規.Enabled = false;
                this.コマンド読込.Enabled = true;
                this.コマンド複写.Enabled = false;
                this.コマンド削除.Enabled = false;
                this.コマンドメーカー.Enabled = false;
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
                if (this.ActiveControl == this.メーカーコード)
                {
                    this.品名.Focus();
                }
                this.メーカーコード.Enabled = !isChanged;
                this.コマンド複写.Enabled = !isChanged;
                this.コマンド削除.Enabled = !isChanged;
                this.コマンドメーカー.Enabled = !isChanged;
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
                ChangedData(false);

                this.メーカーコード.Enabled = true;
                this.メーカーコード.Focus();
                // メーカーコードコントロールが使用可能になってから LockData を呼び出す
                FunctionClass.LockData(this, true, "メーカーコード");
                this.コマンド新規.Enabled = true;
                this.コマンド読込.Enabled = false;
                this.コマンド複写.Enabled = false;
                // this.コマンド確定.Enabled = false;
                this.コマンド登録.Enabled = false;
                if (!string.IsNullOrEmpty(this.過不足数量.Text))
                {
                    this.削除.Text = "■";
                }

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

            string LoginUserCode = "000";//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            LocalSetting test = new LocalSetting();
            test.SavePlace(LoginUserCode, this);


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
                                    "[はい]を選択した場合、メーカーコードは破棄されます。" + Environment.NewLine +
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
                                "メーカーコード　：　" + CurrentCode, BASE_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                return string.IsNullOrEmpty(メーカーコード.Text) ? "" : メーカーコード.Text;
            }
        }

        public int CurrentRevision
        {
            get
            {
                return string.IsNullOrEmpty(版数.Text) ? 0 : int.Parse(版数.Text);
            }
        }


        public bool IsDeleted
        {
            get
            {
                bool isEmptyOrDbNull = string.IsNullOrEmpty(this.過不足数量.Text) || Convert.IsDBNull(this.過不足数量.Text);

                return !isEmptyOrDbNull;
            }
        }


        private bool SaveData()
        {


            Connect();
            SqlTransaction transaction = cn.BeginTransaction();
            {
                

                try
                {


                    DateTime now = DateTime.Now;
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
                    object varSaved7 = null;

                    bool isNewData = IsNewData;

                    if (isNewData)
                    {
                        objControl1 = 作成日時;
                        objControl2 = 作成者コード;
                        objControl3 = 作成者名;
                        varSaved1 = objControl1.Text;
                        varSaved2 = objControl2.Text;
                        varSaved3 = objControl3.Text;
                        objControl1.Text = now.ToString();
                        objControl2.Text = CommonConstants.LoginUserCode;
                        objControl3.Text = CommonConstants.LoginUserFullName;
                    }

                    objControl4 = 更新日時;
                    objControl5 = 更新者コード;
                    objControl6 = 更新者名;

                    // 登録前の状態を退避しておく
                    varSaved4 = objControl4.Text;
                    varSaved5 = objControl5.Text;
                    varSaved6 = objControl6.Text;

                    // 値の設定
                    objControl4.Text = now.ToString();
                    objControl5.Text = CommonConstants.LoginUserCode;
                    objControl6.Text = CommonConstants.LoginUserFullName;



                    string strwhere = " メーカーコード='" + this.メーカーコード.Text + "' and Revision=" + this.版数.Text;

                    if (!DataUpdater.UpdateOrInsertDataFrom(this, cn, "Mメーカー", strwhere, "メーカーコード", transaction))
                    {
                        //transaction.Rollback(); 関数内でロールバック入れた
                        return false;
                    }



                    // トランザクションをコミット
                    transaction.Commit();


                   

                    メーカーコード.Enabled = true;

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
                    // トランザクション内でエラーが発生した場合、ロールバックを実行
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }

                    コマンド登録.Enabled = true;
                    // エラーメッセージを表示またはログに記録
                    MessageBox.Show("データの保存中にエラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                
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
                if (this.IsChanged)
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

                VariableSet.SetControls(this);
                //DispGrid(CurrentCode);

                //string generatedPartCode = Right(FunctionClass.採番(cn, "PAR"), 8);
                //部品コード.Text = generatedPartCode;
                版数.Text = 1.ToString();
                入数.Text = 1.ToString();
                単位数量.Text = 1.ToString();
                ロス率.Text = 0f.ToString();
                Rohs1ChemSherpaStatusCode.SelectedValue = 1;
                JampAis.SelectedValue = 1;
                非含有証明書.SelectedValue = 3;
                RoHS資料.SelectedValue = 1;
                Rohs2ChemSherpaStatusCode.SelectedValue = 1;
                Rohs2JampAisStatusCode.SelectedValue = 1;
                Rohs2NonInclusionCertificationStatusCode.SelectedValue = 3;
                Rohs2DocumentStatusCode.SelectedValue = 1;
                Rohs2ProvisionalRegisteredStatusCode.Checked = true;
                廃止.Checked = false;

                InventoryAmount.Text = 0.ToString();
                //ShowRohsStatus();
                //AllowEdits = true;
                品名.Focus();
                部品コード.Enabled = false;
                改版ボタン.Enabled = false;
                コマンド新規.Enabled = false;
                コマンド読込.Enabled = true;
                コマンド削除.Enabled = false;
                コマンド入出庫.Enabled = false;
                コマンド履歴.Enabled = false;
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
            try
            {
                this.DoubleBuffered = true;

                //this.Painting = false;

                CommonConnect();

                if (!this.IsChanged)
                {
                    // 新規モードで且つコードが取得済みのときはコードを戻す
                    if (this.IsNewData && !string.IsNullOrEmpty(this.CurrentCode))
                    {



                        // 採番された番号を戻す
                        if (!FunctionClass.ReturnNewCode(cn, CommonConstants.CH_MAKER, this.CurrentCode))
                        {
                            MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine +
                                "メーカーコード　：　" + this.CurrentCode, "読込コマンド", MessageBoxButtons.OK);
                        }
                    }

                    // 読込モードへ移行する
                    if (!GoModifyMode())
                    {
                        goto Err_コマンド読込_Click;
                    }

                    goto Bye_コマンド読込_Click;
                }

                // 変更されているときは登録確認を行う
                var intRes = MessageBox.Show("変更内容を登録しますか？", "読込コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (intRes)
                {
                    case DialogResult.Yes:
                        if (!ErrCheck()) return;

                        // 登録処理
                        if (!SaveData())
                        {
                            MessageBox.Show("エラーのため登録できません。", "読込コマンド", MessageBoxButtons.OK);
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
                                    "メーカーコード　：　" + this.CurrentCode, "読込コマンド", MessageBoxButtons.OK);
                            }
                        }
                        break;
                    case DialogResult.Cancel:
                        return;
                }

                // 読込モードへ移行する
                if (!GoModifyMode())
                {
                    goto Err_コマンド読込_Click;
                }
            }
            finally
            {
                //this.Painting = true;
            }

        Bye_コマンド読込_Click:
            return;

        Err_コマンド読込_Click:
            //Debug.Print(this.Name + "_コマンド読込_Click - " + Err.Number + " : " + Err.Description);
            MessageBox.Show("エラーが発生しました。" + Environment.NewLine +
                "[ " + BASE_CAPTION + " ]を終了します。", "読込コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //this.Painting = true;
            this.Close();
        }


        //private bool IsErrorData(string ExFieldName1, string ExFieldName2 = "")
        //{
        //    try
        //    {
        //        bool isErrorData = false;

        //        // ヘッダ部のチェック
        //        foreach (Control objControl in this.Controls)
        //        {
        //            if ((objControl is TextBox || objControl is ComboBox) && objControl.Visible)
        //            {
        //                if (objControl.Name != ExFieldName1 && objControl.Name != ExFieldName2)
        //                {
        //                    if (!FunctionClass.IsError(objControl))
        //                    { 
        //                        isErrorData = true;
        //                        objControl.Focus();
        //                        return isErrorData;
        //                    }
        //                }
        //            }
        //        }

        //        return isErrorData;
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.Print(this.Name + "_IsErrorData - " + ex.Message);
        //        return true;
        //    }
        //}


        private bool ErrCheck()
        {
            //入力確認    
            if (!FunctionClass.IsError(this.メーカーコード)) return false;
            if (!FunctionClass.IsError(this.品名)) return false;
            if (!FunctionClass.IsError(this.型番)) return false;
            if (!FunctionClass.IsError(this.メーカーコード)) return false;
            return true;
        }


        private void コマンド複写_Click(object sender, EventArgs e)
        {
            try
            {
                if (ActiveControl == コマンド複写)
                {
                    GetNextControl(コマンド複写, false).Focus();
                }

                this.DoubleBuffered = true;
                //this.Painting = false;

                CommonConnect();

                string newCode = FunctionClass.GetNewCode(cn, CommonConstants.CH_MAKER);
                if (CopyData(newCode.Substring(newCode.Length - 8), 1))
                {
                    ChangedData(true);
                    FunctionClass.LockData(this, false);

                    this.品名.Focus();
                    this.コマンド新規.Enabled = false;
                    this.コマンド読込.Enabled = true;
                }
            }
            finally
            {
                //this.Painting = true;
            }
        }


        private bool CopyData(string codeString, int editionNumber = -1)
        {
            try
            {
                // キー情報を設定
                メーカーコード.Text = codeString;
                if (editionNumber != -1)
                {
                    版数.Text = editionNumber.ToString();
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


                if (ActiveControl == コマンド削除)
                {
                    GetNextControl(コマンド削除, false).Focus();
                }

                if (intEdition == 1)
                {
                    // 初版の場合
                    strMsg = "メーカーコード　：　" + strCode + "\r\n\r\n" +
                        "このメーカーデータを削除/復元します。\r\n" +
                        "削除/復元するには[はい]を選択してください。\r\n\r\n" +
                        "※削除後も参照することができます。";
                    intRes = MessageBox.Show(strMsg, "削除コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                }
                else
                {
                    // 2版以上の場合
                    strMsg = "このバージョンのU-netでは操作できません。\r\n" +
                        "最新バージョンのU-netで操作してください";
                    intRes = MessageBox.Show(strMsg, "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (intRes == DialogResult.Cancel)
                {
                    goto Bye_コマンド削除_Click;
                }
                else
                {


                    // 削除処理

                    Connect();


                    if (intRes == DialogResult.Yes)
                    {
                        
                        fn.DoWait("削除しています...");

                        // 応答がYesのとき
                        if (SetDeleted(cn, strCode, intEdition, DateTime.Now, CommonConstants.LoginUserCode))
                        {
                            fn.WaitForm.Close();
                            goto Err_コマンド削除_Click;
                        }

                        fn.WaitForm.Close();
                    }
                    else if (intRes == DialogResult.OK)
                    {

                    }
                    else
                    {
                        goto Bye_コマンド削除_Click;
                    }
                }

            Bye_コマンド削除_Click:
                //DoCmd.Close(AcObjectType.acForm, "実行中", AcCloseSave.acSavePrompt);
                
                return;

            Err_コマンド削除_Click:
                MessageBox.Show("エラーが発生しました。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
                goto Bye_コマンド削除_Click;
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_コマンド削除_Click - " + ex.Message);
            }
        }


        private bool SetDeleted(SqlConnection cn, string codeString, int editionNumber, DateTime deleteTime, string deleteUser)
        {
            try
            {
                string strKey;
                string strUpdate;

                bool isDeleted = false;

                if (editionNumber == -1)
                {
                    strKey = "メーカーコード=@CodeString";
                }
                else
                {
                    strKey = "メーカーコード=@CodeString AND Revision=@EditionNumber";
                }

                if (this.IsDeleted)
                {

                    strUpdate = "削除日時=NULL,削除者コード=NULL";
                }
                else
                {
                    strUpdate = "削除日時=@DeleteTime,削除者コード=@DeleteUser";
                }

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;
                    cmd.Transaction = cn.BeginTransaction();

                    cmd.Parameters.AddWithValue("@CodeString", codeString);
                    cmd.Parameters.AddWithValue("@EditionNumber", editionNumber);
                    cmd.Parameters.AddWithValue("@DeleteTime", deleteTime);
                    cmd.Parameters.AddWithValue("@DeleteUser", deleteUser);

                    string sql = "UPDATE Mメーカー SET " + strUpdate +
                                 ",更新日時=@DeleteTime,更新者コード=@DeleteUser WHERE " + strKey;

                    cmd.CommandText = sql;
                    cmd.ExecuteNonQuery();


                    cmd.Transaction.Commit(); // トランザクション完了

                    // GUI更新
                    if (this.IsDeleted)
                    {
                        this.過不足数量.Text = null;
                        this.削除.Text = null;
                    }
                    else
                    {
                        this.過不足数量.Text = deleteTime.ToString();
                        this.削除.Text = "■";
                    }

                    isDeleted = false;
                }


                return isDeleted;
            }
            catch (Exception ex)
            {
                // エラーハンドリングを実装
                return true;
            }
        }





        private void コマンド仕入先_Click(object sender, EventArgs e)
        {
            // エラーハンドリングはC#では別の方法を使用するため、ここでは省略
            // On Error Resume Next の代替方法はC#にはありません

            if (ActiveControl == コマンド仕入先)
            {
                GetNextControl(コマンド仕入先, false).Focus();
            }

            // 仕入先フォームを開く
            //Form 仕入先Form = new 仕入先(); // 仕入先フォームの名前に合わせて変更してください
            //仕入先Form.Show();
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

        }

        private void コマンド確定_Click(object sender, EventArgs e)
        {

        }

        private void コマンド登録_Click(object sender, EventArgs e)
        {
            //try
            //{
            this.DoubleBuffered = true;

            //this.Painting = false;

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
                    // 新規モードの場合、版数一覧を更新し、ボタンの状態を変更
                    // Me.メーカー版数.Requery(); // データを再読み込む処理が必要
                    コマンド新規.Enabled = true;
                    コマンド読込.Enabled = false;
                }

                // その他の処理を追加
                // Me.コマンド承認.Enabled = Me.IsDecided;
                // Me.コマンド確定.Enabled = true;
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
                    if (コマンド仕入先.Enabled) コマンド仕入先_Click(sender, e);
                    break;
                case Keys.F6:
                    if (コマンドメーカー.Enabled)
                    {
                        コマンドメーカー.Focus();
 
                    }
                    break;
                case Keys.F8:
                    if (コマンド履歴.Enabled) コマンド印刷_Click(sender, e);
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
                object varValue = controlObject.Text; // Assuming that the Value property is equivalent to the Text property in your case

                switch (strName)
                {
                    case "メーカーコード":
                        LoadData(this.CurrentCode);
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

            FunctionClass.LockData(this, this.IsDeleted, "メーカーコード");
            this.コマンド複写.Enabled = true;
            this.コマンド削除.Enabled = !this.IsDeleted;
            this.コマンドメーカー.Enabled = !this.IsDeleted;
        }


        public bool LoadHeader(Form formObject, string codeString, int editionNumber = -1)
        {
            bool loadHeader = false;

            Connect();


            string strSQL;
            if (editionNumber == -1)
            {
                strSQL = "SELECT * FROM Vメーカーヘッダ WHERE メーカーコード='" + codeString + "'";
            }
            else
            {
                strSQL = "SELECT * FROM Vメーカーヘッダ WHERE メーカーコード'" + codeString + "' AND Revision= " + editionNumber;
            }


            VariableSet.SetTable2Form(this, strSQL, cn);
            loadHeader = true;


            return loadHeader;
        }


      





       



        //private void メーカーコード_Validated(object sender, EventArgs e)
        //{
        //    UpdatedControl((Control)sender);
        //}

        //private void メーカーコード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        //{
        //    FunctionClass.IsError((Control)sender);
        //}

        //private void メーカーコード_KeyDown(object sender, KeyEventArgs e)
        //{
        //    // 入力された値がエラー値の場合、textプロパティが設定できなくなるときの対処
        //    if (e.KeyCode == Keys.Return) // Enter キーが押されたとき
        //    {
        //        string strCode = メーカーコード.Text;
        //        if (string.IsNullOrEmpty(strCode)) return;

        //        strCode = strCode.PadLeft(8, '0'); // ゼロで桁を埋める例
        //        if (strCode != メーカーコード.Text)
        //        {
        //            メーカーコード.Text = strCode;
        //        }
        //    }
        //}



        //private void メーカー名_Validated(object sender, EventArgs e)
        //{
        //    UpdatedControl((Control)sender);
        //}


        //private void メーカー名_TextChanged(object sender, EventArgs e)
        //{
        //    FunctionClass.LimitText(((TextBox)sender), 60);
        //    ChangedData(true);
        //}




        //private void メーカー名_Enter(object sender, EventArgs e)
        //{
        //    toolStripStatusLabel2.Text = "■全角30文字まで入力できます。";
        //}

        //private void メーカー名_Leave(object sender, EventArgs e)
        //{
        //    toolStripStatusLabel2.Text = "各種項目の説明";
        //}

      

     
    }


}
