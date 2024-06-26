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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Microsoft.Identity.Client.NativeInterop;

namespace u_net
{



    public partial class F_部品履歴 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        public int args2;
        private string BASE_CAPTION = "部品履歴";
        private int selected_frame = 0;





        public F_部品履歴()
        {
            this.Text = "部品履歴";       // ウィンドウタイトルを設定
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

            // DataGridViewの設定
            //部品使用先.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //部品使用先.AllowUserToResizeColumns = true;
            //部品使用先.ReadOnly = true;
            //部品使用先.AllowUserToAddRows = false;
            //部品使用先.AllowUserToDeleteRows = false;
            //部品使用先.Font = new Font("MS ゴシック", 10);
            //部品使用先.DefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 210, 255);
            //部品使用先.DefaultCellStyle.SelectionForeColor = Color.Black;
            //部品使用先.GridColor = Color.FromArgb(230, 230, 230);
            //部品使用先.ColumnHeadersDefaultCellStyle.Font = new Font("MS ゴシック", 9);
            //部品使用先.DefaultCellStyle.Font = new Font("MS ゴシック", 10);
            //部品使用先.DefaultCellStyle.ForeColor = Color.Black;

            OriginalClass ofn = new OriginalClass();
            ofn.SetComboBox(分類コード, "SELECT 分類記号 as Display,対象部品名 as Display2,分類コード as Value FROM M部品分類");
            分類コード.DrawMode = DrawMode.OwnerDrawFixed;
            分類コード.DropDownWidth = 600;
            ofn.SetComboBox(形状分類コード, "SELECT 部品形状名 as Display,部品形状コード as Value FROM M部品形状");
            ofn.SetComboBox(RoHS在庫状況, "SELECT Name as Display,Code as Value FROM rohsStatusCode");


            this.非含有証明書.DataSource = new KeyValuePair<byte, String>[] {
                new KeyValuePair<byte, String>(1, "返却済み"),
                new KeyValuePair<byte, String>(2, "未返却"),
                new KeyValuePair<byte, String>(3, "未提出"),
            };
            this.非含有証明書.DisplayMember = "Value";
            this.非含有証明書.ValueMember = "Key";

            this.RoHS資料.DataSource = new KeyValuePair<Int16, String>[] {
                new KeyValuePair<Int16, String>(1, "有り"),
                new KeyValuePair<Int16, String>(2, "無し"),
            };
            this.RoHS資料.DisplayMember = "Value";
            this.RoHS資料.ValueMember = "Key";



            this.RoHS.DataSource = new KeyValuePair<int, String>[] {
                new KeyValuePair<int, String>(1, "対応"),
                new KeyValuePair<int, String>(2, "対応予定"),
                new KeyValuePair<int, String>(3, "非対応"),
                new KeyValuePair<int, String>(4, "未調査"),
            };
            this.RoHS.DisplayMember = "Value";
            this.RoHS.ValueMember = "Key";

            this.RoHS在庫状況.DataSource = new KeyValuePair<int, String>[] {
                new KeyValuePair<int, String>(1, "対応"),
                new KeyValuePair<int, String>(2, "対応予定"),
                new KeyValuePair<int, String>(3, "非対応"),
                new KeyValuePair<int, String>(4, "未調査"),
            };
            this.RoHS在庫状況.DisplayMember = "Value";
            this.RoHS在庫状況.ValueMember = "Key";

            this.受入検査ランク.DataSource = new KeyValuePair<string, string>[] {
            new KeyValuePair<string, string>("A         ", "A         "),
            new KeyValuePair<string, string>("B1        ", "B1        "),
            new KeyValuePair<string, string>("B2        ", "B2        "),
            new KeyValuePair<string, string>("C         ", "C         "),
            new KeyValuePair<string, string>("D         ", "D         "),
};
            this.受入検査ランク.DisplayMember = "Value";
            this.受入検査ランク.ValueMember = "Key";

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
                    コマンド修正_Click(sender, e);
                   
                    部品コード.Text = args;
                    UpdatedControl(部品コード);
                    版数.Text = args2.ToString();
                    UpdatedControl(版数);
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


            AskSave();

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
                return string.IsNullOrEmpty(部品コード.Text) ? "" : 部品コード.Text;
            }
        }

        public int CurrentEdition
        {
            get
            {
                return int.TryParse(版数.Text, out int 変換結果) ? 変換結果 : 0;

                //return 0;
            }
        }

        public bool IsIncluded
        {
            get
            {
                //return !string.IsNullOrEmpty(部品集合コード.Text);
                return false;
            }
        }

        private bool DoRegister()
        {

            //Accessの登録処理に不具合があるため、この処理は行わない
            return false;


            Connect();
            SqlTransaction transaction = cn.BeginTransaction();
            {
                try
                {

                    DateTime dteNow = DateTime.Now;
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
                        objControl3 = this.CreatorName;
                        varSaved1 = objControl1.Text;
                        varSaved2 = objControl2.Text;
                        varSaved3 = objControl3.Text;
                        objControl1.Text = dteNow.ToString(); // ここでDateTimeをstringに変換して設定
                        objControl2.Text = CommonConstants.LoginUserCode;
                        objControl3.Text = CommonConstants.LoginUserFullName;
                    }

                    objControl4 = this.更新日時;
                    objControl5 = this.更新者コード;
                    objControl6 = this.UpdaterName;

                    varSaved4 = objControl4.Text;
                    varSaved5 = objControl5.Text;
                    varSaved6 = objControl6.Text;

                    objControl4.Text = dteNow.ToString(); // ここでDateTimeをstringに変換して設定
                    objControl5.Text = CommonConstants.LoginUserCode;
                    objControl6.Text = CommonConstants.LoginUserFullName;


                    string strwhere = " 部品コード='" + this.部品コード.Text + "'";

                    if (!DataUpdater.UpdateOrInsertDataFrom(this, cn, "M部品", strwhere, "部品コード", transaction))
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

                    // トランザクションをコミット
                    transaction.Commit();


                    部品コード.Enabled = true;

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
                    Console.WriteLine("Error in DoRegister: " + ex.Message);
                    return false;
                }
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
            if (DoRegister())
            {
                // 登録に成功した
                ChangedControl(false); // データ変更取り消し

                if (this.IsNewData)
                {
                    // 新規モードのとき
                    this.コマンド新規.Enabled = true;
                    this.コマンド修正.Enabled = false;
                    this.コマンド入出庫.Enabled = true;
                }
                // UpdatePurGridの呼び出し
                // Call UpdatePurGrid();
                fn.WaitForm.Close();
                MessageBox.Show("登録を完了しました", "登録コマンド", MessageBoxButtons.OK);
            }
            else
            {
                // 登録に失敗したとき
                fn.WaitForm.Close();
                this.コマンド登録.Enabled = true;
                MessageBox.Show("登録できませんでした。", "登録コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        Bye_コマンド登録_Click:
            return;

        }


        private bool ErrCheck()
        {
            //入力確認    
            if (!FunctionClass.IsError(this.部品コード)) return false;
            //if (!FunctionClass.IsError(this.版数)) return false;
            return true;
        }

        private void ChangedControl(bool isChanged)
        {
            if (部品コード.Enabled)
            {
                部品コード.Enabled = !isChanged;
            }
            コマンド複写.Enabled = !isChanged;
            コマンド削除.Enabled = !isChanged;
            コマンド登録.Enabled = isChanged;
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

            if (部品コード.Enabled)
            {
                部品コード.Enabled = !isChanged;
            }

            this.部品コード.Enabled = !isChanged;
            this.コマンド複写.Enabled = !isChanged;
            this.コマンド削除.Enabled = !isChanged;
            this.コマンド登録.Enabled = isChanged;

            // RoHSの状態表示を更新する
            //ShowRohsStatus();
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
                            if (!DoRegister())
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

                string code = FunctionClass.採番(cn, "PAR");
                部品コード.Text = code.Substring(Math.Max(0, code.Length - 8));
                版数.Text = 1.ToString();
                入数.Text = 1.ToString();
                単位数量.Text = 1.ToString();
                ロス率.Text = 0f.ToString();
                RoHS.SelectedValue = 4;
                在庫数量.Text = 0.ToString();
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


                ChangedControl(false);

                //InventoryAmount.Text = 0.ToString();
                //ShowRohsStatus();
                品名.Focus();
                部品コード.Enabled = false;
                コマンド新規.Enabled = false;
                コマンド修正.Enabled = true;
                コマンド削除.Enabled = false;
                コマンド入出庫.Enabled = false;
                コマンド複写.Enabled = false;
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



      

        //private void ShowRohsStatus()
        //{
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
        //}

        private void コマンド修正_Click(object sender, EventArgs e)
        {
            //if (!AskSave()) { return; }


            // strOpenArgsがどのように設定されているかに依存します。
            // もしstrOpenArgsに関連する処理が必要な場合はここに追加してください。

            // 各コントロールの値をクリア
            VariableSet.SetControls(this);

            // コントロールを操作
            部品コード.Enabled = true;
            部品コード.Focus();
            コマンド新規.Enabled = true;
            コマンド修正.Enabled = false;
            コマンド入出庫.Enabled = false;
            コマンド登録.Enabled = false;
        }


        private bool AskSave()
        {
            try
            {
                Connect();

                DialogResult response;
                bool isNewData = IsNewData; // 仮定されたIsNewDataプロパティの取得
                string currentCode = CurrentCode; // 仮定されたCurrentCodeプロパティの取得

                if (コマンド登録.Enabled)
                {
                    response = MessageBox.Show("変更内容を登録しますか？", "質問", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    switch (response)
                    {
                        case DialogResult.Yes:
                            if (DoRegister())
                            {
                                //UpdatePurGrid();
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        case DialogResult.No:
                            break;
                        case DialogResult.Cancel:
                            return false;
                    }
                }

                // 登録しない場合
                if (isNewData)
                {
                    if (!string.IsNullOrEmpty(currentCode))
                    {
                        // 部品コードが採番された場合、番号を戻す処理
                        if (FunctionClass.Recycle(cn, "PAR" + currentCode))
                        {
                            MessageBox.Show("部品コードは破棄されました。" + Environment.NewLine + Environment.NewLine +
                                            "部品コード： " + currentCode, "通知", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("部品コードを戻す際にエラーが発生しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

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

        private void コマンドメーカー_Click(object sender, EventArgs e)
        {
            try
            {


                F_メーカー管理 targetform = new F_メーカー管理();

                targetform.MdiParent = this.MdiParent;
                targetform.FormClosed += (s, args) => { this.Enabled = true; };
                this.Enabled = false;

                targetform.Show();


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in コマンドメーカー_Click: " + ex.Message);
            }
        }


        private void コマンド入出庫_Click(object sender, EventArgs e)
        {
            try
            {

                F_入出庫履歴 targetform = new F_入出庫履歴();

                targetform.args = 部品コード.Text;
                targetform.MdiParent = this.MdiParent;
                targetform.FormClosed += (s, args) => { this.Enabled = true; };
                this.Enabled = false;

                targetform.Show();


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in コマンドメーカー_Click: " + ex.Message);
            }
        }



        private F_検索 SearchForm;

        private void メーカーコード検索ボタン_Click(object sender, EventArgs e)
        {
            SearchForm = new F_検索();
            SearchForm.FilterName = "メーカー名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                string SelectedCode = SearchForm.SelectedCode;

                メーカーコード.Text = SelectedCode;
                string str1 = FunctionClass.GetMakerName(cn, SelectedCode);
                string str2 = FunctionClass.GetMakerShortName(cn, SelectedCode);
                MakerName.Text = str1;
                //MakerShortName.Text = str2;

            }
        }







        private void コマンド削除_Click(object sender, EventArgs e)
        {
            try
            {
                if (ActiveControl == this.コマンド削除)
                {
                    GetNextControl(コマンド削除, false).Focus();
                }


                string strMsg = "部品コード　：　" + this.CurrentCode + "\n\nこのデータを削除しますか？\n削除後、管理者により復元することができます。";

                if (MessageBox.Show(strMsg, "削除コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                // ログインユーザーが表示データの登録ユーザーでなければ認証する

                using (var targetform = new F_認証())
                {
                    targetform.args = CommonConstants.USER_CODE_TECH;
                    //targetform.MdiParent = this.MdiParent;
                    //targetform.FormClosed += (s, args) => { this.Enabled = true; };
                    //this.Enabled = false;

                    targetform.ShowDialog();


                    if (string.IsNullOrEmpty(CommonConstants.strCertificateCode))
                    {
                        MessageBox.Show("認証できません。" + Environment.NewLine + "削除はキャンセルされました。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }


                // 部品情報削除
                FunctionClass fn = new FunctionClass();
                fn.DoWait("削除しています...");

                Connect();

                // 削除に成功すれば新規モードへ移行する
                if (DeleteData(cn, CurrentCode, CurrentEdition))
                {
                    fn.WaitForm.Close();
                    MessageBox.Show("削除しました。\n部品コード　：　" + this.CurrentCode, "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    コマンド新規_Click(sender, e);
                }
                else
                {
                    fn.WaitForm.Close();
                    MessageBox.Show("削除できませんでした。\n部品コード　：　" + this.CurrentCode, "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in コマンド削除_Click: " + ex.Message);
            }
        }


        public bool DeleteData(SqlConnection cn, string codeString, int editionNumber = -1, bool completed = false)
        {
            SqlTransaction transaction = null;
            try
            {
                string strKey = "部品コード='" + codeString + "' AND 部品版数 =" + editionNumber;
                string strSQL;
                bool deleteData = false;

                transaction = cn.BeginTransaction();

                if (completed)
                {
                    strSQL = "DELETE FROM M部品 WHERE " + strKey;
                    using (SqlCommand cmd = new SqlCommand(strSQL, cn, transaction))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    strSQL = "UPDATE M部品 SET 無効日時 = GETDATE() WHERE " + strKey;
                    using (SqlCommand cmd = new SqlCommand(strSQL, cn, transaction))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                using (SqlCommand cmd = new SqlCommand("SPユニット管理", cn, transaction))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PartsCode", codeString));
                    cmd.ExecuteNonQuery();
                }

                // トランザクションをコミット
                transaction.Commit();

                deleteData = true;
                return deleteData;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DeleteData: " + ex.Message);
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                return false;
            }
        }

        private void コマンド複写_Click(object sender, EventArgs e)
        {
            try
            {

                this.品名.Focus();
                ChangedControl(true);

                Connect();

                // 以下、初期値の設定
                string code = FunctionClass.採番(cn, "PAR");
                部品コード.Text = code.Substring(Math.Max(0, code.Length - 8));
                //this.版数.Text = 1.ToString();
                //this.InventoryAmount.Text = 0.ToString();
                this.作成日時.Text = null;
                //this.作成者コード.Text = null;
                //this.CreatorName.Text = null;
                this.更新日時.Text = null;
                //this.更新者コード.Text = null;
                //this.UpdaterName.Text = null;

                // インターフェース更新
                this.コマンド新規.Enabled = false;
                this.コマンド修正.Enabled = true;
                コマンド複写.Enabled = false;
                コマンド削除.Enabled = false;
                コマンド登録.Enabled = true;


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


        private void コマンド仕入先_Click(object sender, EventArgs e)
        {
            try
            {
                F_仕入先管理 targetform = new F_仕入先管理();

                targetform.MdiParent = this.MdiParent;
                targetform.FormClosed += (s, args) => { this.Enabled = true; };
                this.Enabled = false;

                targetform.Show();

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

        private bool LoadData(Form formObject, string codeString, int editionNumber = 0)
        {
            try
            {
                Connect();

                string strSQL;
                if (editionNumber == 0)
                {
                    strSQL = "SELECT * FROM V部品履歴読込 WHERE 部品コード ='" + codeString + "'";
                }
                else
                {
                    strSQL = "SELECT * FROM V部品履歴読込 WHERE 部品コード ='" + codeString + "' AND 版数= " + editionNumber;
                }


                VariableSet.SetTable2Form(this, strSQL, cn);


                if (!string.IsNullOrEmpty(this.無効日時.Text))
                {
                    this.削除.Text = "■";
                }

                return true;
                //}




            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // エラーハンドリングが必要に応じて行われるべきです
                return false;
            }
        }

        private int CheckParts(SqlConnection connectionObject, string codeString, int edition = 0)
        {
            try
            {
                string strKey;
                string strSQL;
                int recordCount = 0;

                if (string.IsNullOrEmpty(codeString))
                    return 0;

                if (edition == 0)
                {
                    strKey = "部品コード = '" + codeString + "'";
                }
                else
                {
                    strKey = "部品コード = '" + codeString + "' and 版数 = " + edition;
                }

                strSQL = "SELECT COUNT(*) FROM M部品 WHERE " + strKey;

                using (SqlCommand cmd = new SqlCommand(strSQL, connectionObject))
                {
                    connectionObject.Open();
                    recordCount = Convert.ToInt32(cmd.ExecuteScalar());
                    connectionObject.Close();
                }

                return recordCount;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // エラーハンドリングが必要に応じて行われるべきです
                return 0;
            }
        }


        private bool IsError(Control controlObject)
        {
            try
            {



                object varValue = controlObject.Text;
                string controlName = controlObject.Name;

                switch (controlName)
                {
                    case "部品コード":
                    case "品名":
                        if (string.IsNullOrEmpty(varValue.ToString()))
                        {
                            MessageBox.Show(controlName + "を入力してください.", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;
                    case "型番":
                        //if (Cancel)
                        //{
                        if (string.IsNullOrEmpty(varValue.ToString()))
                        {
                            MessageBox.Show(controlName + "を入力してください.", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        //}

                        // 重複チェックなどを行う必要があれば、ここに追加してください。
                        DataTable rs1 = null;
                        string strPartsList = null;

                        if (DetectRepeatedParts(varValue.ToString(), CurrentCode, ref rs1))
                        {
                            if (rs1.Rows.Count > 0)
                            {
                                foreach (DataRow row in rs1.Rows)
                                {
                                    strPartsList += row[0].ToString() + " ： ";
                                    strPartsList += row[1].ToString() + " ： ";
                                    strPartsList += row[2].ToString() + Environment.NewLine;
                                }

                                MessageBox.Show($"入力された型番は既に登録されています。{Environment.NewLine}{strPartsList}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return true;
                            }

                        }
                        break;
                    case "メーカーコード":
                        if (string.IsNullOrEmpty(varValue.ToString()))
                        {
                            MessageBox.Show(controlName + "を入力してください.", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        else
                        {
                            string str1 = FunctionClass.GetMakerName(cn, controlObject.Text.ToString());
                            //string str2 = FunctionClass.GetMakerShortName(cn, controlObject.Text.ToString());
                            if (string.IsNullOrEmpty(str1))
                            {
                                return true;
                            }
                            else
                            {
                                MakerName.Text = str1;
                                //MakerShortName.Text = str2;
                            }
                        }

                        break;
                    case "仕入先1単価":
                    case "仕入先2単価":
                    case "仕入先3単価":
                        if (!string.IsNullOrEmpty(varValue.ToString()))
                        {
                            if (!FunctionClass.IsLimit_N(varValue, 8, 2, controlName))
                            {
                                return true;
                            }
                        }
                        break;
                    case "分類コード":
                        if (string.IsNullOrEmpty(varValue.ToString()))
                        {
                            MessageBox.Show(controlName + "を入力してください.", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        //GroupNumber.Text = 分類コード.Text;
                        break;
                    case "形状分類コード":
                        if (string.IsNullOrEmpty(varValue.ToString()))
                        {
                            MessageBox.Show("形状分類を指定してください.", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }
                        //FormGroupShortName.Text = 形状分類コード.Text;
                        break;
                    case "入数":
                        if (!FunctionClass.IsLimit_N(varValue, 8, 0, controlName))
                        {
                            return true;
                        }
                        break;
                    case "単位数量":
                        if (!FunctionClass.IsLimit_N(varValue, 8, 0, controlName))
                        {
                            return true;
                        }
                        break;
                    //case "StandardDeliveryDay":
                    //    // カスタムのチェックロジックが必要であればここに追加してください。
                    //    break;
                    //case "CalcInventoryCode":
                    //    if (string.IsNullOrEmpty(varValue.ToString()))
                    //    {
                    //        MessageBox.Show("在庫計算を指定してください.", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //        return true;
                    //    }
                    //    break;
                    case "受入検査ランク":
                        if (string.IsNullOrEmpty(varValue.ToString()))
                        {
                            MessageBox.Show("受入検査ランクを指定してください.", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }
                        break;
                    case "ロス率":
                        if (!FunctionClass.IsLimit_N(varValue, 8, 7, controlName))
                        {
                            return true;
                        }
                        break;
                    case "RoHS":

                        if (string.IsNullOrEmpty(varValue.ToString()))
                        {
                            MessageBox.Show(controlName + "を指定してください。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return true;
                        }
                        break;

                    case "在庫数量":
                        if (!FunctionClass.IsLimit_N(varValue, 8, 0, controlName))
                        {
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


        public bool DetectRepeatedParts(string ModelString, string ExCodeString, ref DataTable recordsetObject)
        {
            bool detectRepeatedParts = false;

            try
            {

                Connect();

                using (SqlCommand command = new SqlCommand("SP部品重複検出", cn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@strModel", SqlDbType.VarChar).Value = ModelString;
                    command.Parameters.Add("@strExCode", SqlDbType.VarChar).Value = ExCodeString;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        recordsetObject = new DataTable();
                        adapter.Fill(recordsetObject);
                    }
                    detectRepeatedParts = true;
                }

            }
            catch (Exception ex)
            {
                // エラーハンドリング
                Console.WriteLine("DetectRepeatedPartsエラー: " + ex.Message);
            }

            return detectRepeatedParts;
        }

        private void UpdatedControl(Control controlObject)
        {
            try
            {
                Connect();

                switch (controlObject.Name)
                {
                    case "部品コード":

                        FunctionClass fn = new FunctionClass();
                        fn.DoWait("読み込んでいます...");




                        OriginalClass ofn = new OriginalClass();
                        ofn.SetComboBox(版数, "select 版数 as Display , 版数 as Value from V部品履歴読込 where 部品コード='" + 部品コード.Text + "' order by 版数 DESC");


                        // 内容の表示
                        LoadData(this, CurrentCode, CurrentEdition);
                        // 使用先の表示
                        //DispGrid(部品コード.Text);
                        // コマンド複写.Enabled = true;
                        コマンド削除.Enabled = true;
                        コマンド入出庫.Enabled = true;
                        コマンド複写.Enabled = true;

                        ChangedControl(false);

                        fn.WaitForm.Close();
                        break;

                    case "版数":
                        // 内容の表示
                        LoadData(this, CurrentCode, CurrentEdition);
                        // 使用先の表示
                        //DispGrid(部品コード.Text);
                        // コマンド複写.Enabled = true;
                        コマンド削除.Enabled = true;
                        コマンド入出庫.Enabled = true;
                        コマンド複写.Enabled = true;

                        ChangedControl(false);

                        break;
                    case "入数":
                    case "単位数量":
                        int parsedValue = int.Parse(controlObject.Text);
                        if (parsedValue < 1)
                        {
                            controlObject.Text = 1.ToString();
                        }
                        break;
                    case "ロス率":
                        controlObject.Text = float.Parse(controlObject.Text.ToString()).ToString();
                        // Me.Controls(ControlName).Value = Me.Controls(ControlName).Value / 100;
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.Print(Name + "_UpdatedControl - " + ex.Message);
            }
            finally
            {


            }
        }




        private void 部品集合参照ボタン_Click(object sender, EventArgs e)
        {
            if (IsIncluded)
            {
                F_部品集合 targetform = new F_部品集合();

                targetform.args = 部品集合コード.Text + "," + 部品集合版数.Text;
                targetform.MdiParent = this.MdiParent;
                targetform.FormClosed += (s, args) => { this.Enabled = true; };
                this.Enabled = false;

                targetform.Show();

            }
            else
            {
                MessageBox.Show("この部品は部品集合に構成されていません。", BASE_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }





        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;

                case Keys.F1:
                    if (コマンド新規.Enabled)
                    {
                        //コマンド新規.Focus();
                        コマンド新規_Click(sender, e);
                    }
                    break;
                case Keys.F2:
                    if (コマンド修正.Enabled)
                    {
                        //コマンド修正.Focus();
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
                    if (コマンド仕入先.Enabled) コマンド仕入先_Click(sender, e);
                    break;
                case Keys.F6:
                    if (コマンドメーカー.Enabled) コマンドメーカー_Click(sender, e);
                    break;
                case Keys.F7:
                    if (コマンド入出庫.Enabled) コマンド入出庫_Click(sender, e);
                    break;
                case Keys.F8:
                    if (コマンド最新版.Enabled) コマンド最新版_Click(sender, e);
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

      
     

        private void メーカーコード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void メーカーコード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 8);
            ChangedControl(true);
        }

        private void メーカーコード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                TextBox textBox = (TextBox)sender;
                string formattedCode = textBox.Text.Trim().PadLeft(8, '0');

                if (formattedCode != textBox.Text || string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.Text = formattedCode;
                    UpdatedControl(sender as Control);
                }
            }
        }

        private void メーカーコード_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                メーカーコード検索ボタン_Click(sender, e);
                e.Handled = true; // イベントの処理が完了したことを示す
            }
        }

        private void ロス率_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void ロス率_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void ロス率_TextChanged(object sender, EventArgs e)
        {
            ChangedControl(true);
        }

        private void 型番_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 型番_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 50);
            ChangedControl(true);
        }

        private void 形状分類コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 形状分類コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangedControl(true);
        }

        private void 形状分類コード_KeyPress(object sender, KeyPressEventArgs e)
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

        private void 仕入先1コード_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 仕入先1コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 仕入先1コード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 8);
            ChangedControl(true);
        }

        private void 仕入先1コード_Enter(object sender, EventArgs e)
        {
            selected_frame = 1;
            toolStripStatusLabel1.Text = "■仕入先コードを入力します。　■8文字まで入力可。　■[space]キーで検索ウィンドウを開きます。";
        }

        private void 仕入先1コード_Leave(object sender, EventArgs e)
        {

            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 仕入先1コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Control control = (Control)sender;
                string strCode = control.Text.Trim();

                if (string.IsNullOrEmpty(strCode))
                {
                    return;
                }

                strCode = strCode.PadLeft(8, '0');

                if (strCode != control.Text)
                {
                    control.Text = strCode;
                    UpdatedControl(sender as Control);
                }
            }
        }

        private void 仕入先1コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                仕入先1コード検索ボタン_Click(sender, e);
            }


        }

        private void 仕入先2コード_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 仕入先2コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 仕入先2コード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 8);
            ChangedControl(true);
        }

        private void 仕入先2コード_Enter(object sender, EventArgs e)
        {
            selected_frame = 2;
            toolStripStatusLabel1.Text = "■仕入先コードを入力します。　■8文字まで入力可。　■[space]キーで検索ウィンドウを開きます。";
        }

        private void 仕入先2コード_Leave(object sender, EventArgs e)
        {

            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 仕入先2コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Control control = (Control)sender;
                string strCode = control.Text.Trim();

                if (string.IsNullOrEmpty(strCode))
                {
                    return;
                }

                strCode = strCode.PadLeft(8, '0');

                if (strCode != control.Text)
                {
                    control.Text = strCode;
                    UpdatedControl(sender as Control);
                }
            }
        }

        private void 仕入先2コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                仕入先2コード検索ボタン_Click(sender, e);
            }


        }

        private void 仕入先3コード_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 仕入先3コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 仕入先3コード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 8);
            ChangedControl(true);
        }

        private void 仕入先3コード_Enter(object sender, EventArgs e)
        {
            selected_frame = 3;
            toolStripStatusLabel1.Text = "■仕入先コードを入力します。　■8文字まで入力可。　■[space]キーで検索ウィンドウを開きます。";
        }

        private void 仕入先3コード_Leave(object sender, EventArgs e)
        {

            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 仕入先3コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Control control = (Control)sender;
                string strCode = control.Text.Trim();

                if (string.IsNullOrEmpty(strCode))
                {
                    return;
                }

                strCode = strCode.PadLeft(8, '0');

                if (strCode != control.Text)
                {
                    control.Text = strCode;
                    UpdatedControl(sender as Control);
                }
            }
        }

        private void 仕入先3コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                仕入先3コード検索ボタン_Click(sender, e);
            }


        }

        private void 仕入先1コード検索ボタン_Click(object sender, EventArgs e)
        {
            SearchForm = new F_検索();
            SearchForm.FilterName = "仕入先名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                string SelectedCode = SearchForm.SelectedCode;

                仕入先1コード.Text = SelectedCode;
                UpdatedControl(仕入先1コード);
            }
        }

        private void 仕入先2コード検索ボタン_Click(object sender, EventArgs e)
        {
            SearchForm = new F_検索();
            SearchForm.FilterName = "仕入先名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                string SelectedCode = SearchForm.SelectedCode;

                仕入先2コード.Text = SelectedCode;
                UpdatedControl(仕入先2コード);
            }
        }

        private void 仕入先3コード検索ボタン_Click(object sender, EventArgs e)
        {
            SearchForm = new F_検索();
            SearchForm.FilterName = "仕入先名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                string SelectedCode = SearchForm.SelectedCode;

                仕入先3コード.Text = SelectedCode;
                UpdatedControl(仕入先3コード);
            }
        }

        private void 仕入先1フレーム_Enter(object sender, EventArgs e)
        {
            仕入先1コード.Focus();
        }

        private void 仕入先2フレーム_Enter(object sender, EventArgs e)
        {
            仕入先2コード.Focus();
        }

        private void 仕入先3フレーム_Enter(object sender, EventArgs e)
        {
            仕入先3コード.Focus();

        }

        private void 仕入先1単価_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 仕入先1単価_TextChanged(object sender, EventArgs e)
        {
            ChangedControl(true);
        }

        private void 仕入先1単価_Enter(object sender, EventArgs e)
        {
            selected_frame = 1;
            toolStripStatusLabel1.Text = "単価が不明であるときは何も入力しないでください。　■数値全体で8桁まで、少数部は2桁まで入力できます。";
        }

        private void 仕入先1単価_Leave(object sender, EventArgs e)
        {

            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 仕入先2単価_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 仕入先2単価_TextChanged(object sender, EventArgs e)
        {
            ChangedControl(true);
        }

        private void 仕入先2単価_Enter(object sender, EventArgs e)
        {
            selected_frame = 2;
            toolStripStatusLabel1.Text = "単価が不明であるときは何も入力しないでください。　■数値全体で8桁まで、少数部は2桁まで入力できます。";
        }

        private void 仕入先2単価_Leave(object sender, EventArgs e)
        {

            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 仕入先3単価_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 仕入先3単価_TextChanged(object sender, EventArgs e)
        {
            ChangedControl(true);
        }

        private void 仕入先3単価_Enter(object sender, EventArgs e)
        {
            selected_frame = 3;
            toolStripStatusLabel1.Text = "単価が不明であるときは何も入力しないでください。　■数値全体で8桁まで、少数部は2桁まで入力できます。";
        }

        private void 仕入先3単価_Leave(object sender, EventArgs e)
        {

            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 受入検査ランク_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 受入検査ランク_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangedControl(true);
        }

        private void 受入検査ランク_KeyPress(object sender, KeyPressEventArgs e)
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

        private void 単位数量_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 単位数量_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 単位数量_TextChanged(object sender, EventArgs e)
        {
            ChangedControl(true);
        }

        private void 入数_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 入数_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 入数_TextChanged(object sender, EventArgs e)
        {
            ChangedControl(true);
        }

        private void 廃止_Validated(object sender, EventArgs e)
        {
            ChangedControl(true);
        }

        private void 品名_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 品名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 50);
            ChangedControl(true);
        }

        private void 部品コード_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 部品コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 部品コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 8);
        }

        private void 部品コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                ComboBox comboBox = sender as ComboBox;
                if (comboBox != null)
                {
                    string strCode = comboBox.Text.Trim();
                    if (!string.IsNullOrEmpty(strCode))
                    {
                        strCode = strCode.PadLeft(8, '0');
                        if (strCode != comboBox.Text)
                        {
                            comboBox.Text = strCode;
                            部品コード_Validated(sender, e);
                        }
                    }
                }
            }
        }

        private void 分類コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 分類コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            GroupName.Text = ((DataRowView)分類コード.SelectedItem)?.Row.Field<String>("Display2")?.ToString();
            ChangedControl(true);
        }

        private void 分類コード_TextChanged(object sender, EventArgs e)
        {
            if (分類コード.SelectedValue == null)
            {
                GroupName.Text = null;
            }
        }

        private void 分類コード_KeyPress(object sender, KeyPressEventArgs e)
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

        private void 分類コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 50, 550 }, new string[] { "Display", "Display2" });
            分類コード.Invalidate();
            分類コード.DroppedDown = true;
        }

        private void 部品コード_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■読み込む部品データの部品コードを入力します。";
        }

        private void 部品コード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 品名_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角で25文字まで入力可。　■機種依存文字は入力できません。";
        }

        private void 品名_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 型番_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■半角50文字まで入力可。　■機種依存文字は入力できません。";
        }

        private void 型番_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void メーカーコード_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■メーカーコードを入力します。　■8文字まで入力可。　■[space]キーで検索ウィンドウを開きます。";
        }

        private void メーカーコード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 分類コード_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■[space]キーでドロップダウンリストを開きます。";
        }

        private void 分類コード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 形状分類コード_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■[space]キーでドロップダウンリストを開きます。";
        }

        private void 形状分類コード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 入数_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■部品を構成している数量を入力します。購買時にこの値によって除算されます。";
        }

        private void 入数_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 単位数量_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■購買時の最小単位数量を入力します。";
        }

        private void 単位数量_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 受入検査ランク_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■[space]キーでドロップダウンリストを表示します。";
        }

        private void 受入検査ランク_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.Black, new Point(5, 0), new Point(19, 0));
            e.Graphics.DrawLine(Pens.Black, new Point(19, 0), new Point(19, 254));
            e.Graphics.DrawLine(Pens.Black, new Point(5, 254), new Point(19, 254));
        }

        private void 部品集合コード_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■この部品が含まれる部品集合です。";
        }

        private void 部品集合コード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void RoHS_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■RoHSについての対応状況を入力します。　■[space] キーでドロップダウンリストを表示します。";
        }

        private void RoHS_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void RohsStatusCode_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■在庫部品について、RoHSの対応状況を入力します。　■[space]キーでドロップダウンリストを表示します。";
        }

        private void RohsStatusCode_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 非含有証明書_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■RoHS指令に基づく非含有証明書の進捗状況を選択します。　■[space]キーでドロップダウンリストを表示します。";
        }

        private void 非含有証明書_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void RoHS資料_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■RoHSに関する資料の有無を入力します。　■[space]キーでドロップダウンリストを表示します。";
        }

        private void RoHS資料_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void ロス率_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■係数で指定します。　■小数部は7桁まで入力できますが、表示は3桁です。";
        }

        private void ロス率_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 調整在庫数量_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■システム上での在庫数量です。　■通常は初期導入時に入力します。";
        }

        private void 調整在庫数量_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void コマンド最新版_Click(object sender, EventArgs e)
        {
            F_部品 targetform = new F_部品();
            targetform.args = CurrentCode;

            targetform.MdiParent = this.MdiParent;
            targetform.FormClosed += (s, args) => { this.Enabled = true; };
            this.Enabled = false;

            targetform.Show();

        }

        private void RoHS_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void RoHS_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void RoHS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                ComboBox comboBox = sender as ComboBox;
                if (comboBox != null)
                {
                    comboBox.DroppedDown = true;
                }
                e.Handled = true;
            }
        }

        private void 在庫数量_TextChanged(object sender, EventArgs e)
        {
            ChangedControl(true);
        }

        private void 在庫数量_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 在庫数量_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 版数_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 版数_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 版数_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }
    }
}
