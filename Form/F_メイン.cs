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
using System.Data.Common;
using static u_net.Public.FunctionClass;
using static u_net.CommonConstants;

namespace u_net
{
    public partial class F_メイン : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "メイン";

        int INT_COUNTER = 300;
        int INT_COUNTER2 = 60;
        int MessageBarTimer;

        public F_メイン()
        {
            this.Text = "メイン";       // ウィンドウタイトルを設定
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



            //LocalSetting localSetting = new LocalSetting();
            //localSetting.LoadPlace(CommonConstants.LoginUserCode, this);

            try
            {
                FunctionClass fn = new FunctionClass();
                fn.DoWait("しばらくお待ちください...");

                Connect();
                string strLastVersion;
                int inti;

                this.Text = STR_APPTITLE + " Ver." + STR_APPVERSION;

                //// クライアント情報取得
                MyOsName = SysVersion();
                MyComputerName = Environment.MachineName;
                MyUserName = Environment.UserName;

                Connect();
                //アプリケーションの最終リリースバージョンを設定する
                strAppLastVer = GetLastVersion(cn);

                //恐らくPCの所有者は認証なしでログインできる様にするためかな？
                LoginUserCode = employeeCode(cn, MyUserName);
                LoginUserName = GetUserName(cn, LoginUserCode);
                LoginDep = GetDepartment(cn, LoginUserCode);
                LoginUserFullName = EmployeeName(cn, LoginUserCode);

                //サーバー名を設定する
                m_strServerName = GetServerName(cn);

                this.ログインユーザー名.Text = fn.Zn(LoginUserFullName).ToString();

                if (LoginUserCode == "")
                {
                    if (LoginUserCode == "")
                    {
                        MessageBox.Show("システムにログインする必要があります。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        this.ログインボタン.Text = "ログイン";
                        this.ログインボタン.Focus();
                    }
                    else
                    {
                        this.ログインボタン.Text = "ログアウト";
                    }
                }
                else
                {
                    this.ログインボタン.Text = "ログアウト";
                }

                if (IsAbsence(cn, USER_CODE_SALES) == -1)
                {
                   // this.営業部部長不在ボタン.PictureData = this.不在イメージコマンド.PictureData;
                }
                else
                {
                   // this.営業部部長不在ボタン.PictureData = this.在席イメージコマンド.PictureData;
                }

                this.日付.Text = DateTime.Now.ToString("yyyy年M月d日");

            }
            finally
            {

            }
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


            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_FormClosing - " + ex.Message);
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


    }
}
