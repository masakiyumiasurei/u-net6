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
    public partial class F_製品管理_汎用キー抽出 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "製品管理_汎用キー抽出";
        private int selected_frame = 0;

        public F_製品管理_汎用キー抽出()
        {
            this.Text = "製品管理_汎用キー抽出";       // ウィンドウタイトルを設定
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
                    //コマンド新規_Click(sender, e);
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

        private bool ErrCheck()
        {
            //入力確認    
            //if (!FunctionClass.IsError(this.部品コード)) return false;
            //if (!FunctionClass.IsError(this.版数)) return false;
            return true;
        }

        private void 抽出ボタン_Click(object sender, EventArgs e)
        {

        }

        private void キャンセルボタン_Click(object sender, EventArgs e)
        {

        }

    }
}
