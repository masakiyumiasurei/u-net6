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
using System.Runtime.InteropServices;

namespace u_net
{
    public partial class F_メイン : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "メイン";
        string param;
        object var1;
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

            toolTip1.SetToolTip(営業部部長不在ボタン, "営業部部長の在席状況を切り替えます");



            try
            {
                FunctionClass fn = new FunctionClass();
                fn.DoWait("しばらくお待ちください...");

                //テストコメント
                //Connect();
                string strLastVersion;
                int inti;

                this.Text = STR_APPTITLE + " Ver." + STR_APPVERSION;

                //// クライアント情報取得
                MyOsName = SysVersion();
                MyComputerName = Environment.MachineName;
                MyUserName = Environment.UserName;

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
                    this.営業部部長不在ボタン.Image = this.不在イメージコマンド.Image;
                }
                else
                {
                    this.営業部部長不在ボタン.Image = this.在席イメージコマンド.Image;
                }

                this.日付.Text = DateTime.Now.ToString("yyyy年M月d日");

            }
            finally
            {

            }
        }

        private void コマンド終了_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Form_Unload(object sender, FormClosingEventArgs e)
        {
            LoginUserCode = "";
        }

        private void ログインボタン_Click(object sender, EventArgs e)
        {
            try
            {
                LoginUserCode = "test";

                if (LoginUserCode == "")
                {
                    // ログインコードが未設定の場合、認証フォームを開く
                    F_認証 fm = new F_認証();
                    fm.ShowDialog();

                    if (fm.DialogResult == DialogResult.OK)
                    {
                        LoginUserCode = employeeCode(cn, MyUserName);
                        LoginUserName = GetUserName(cn, LoginUserCode);
                        LoginDep = GetDepartment(cn, LoginUserCode);
                        ログインボタン.Text = "ログアウト";

                    }
                    else
                    {
                        // 認証が不成立の場合
                        return;
                    }
                }
                else
                {
                    // ログアウトの確認
                    DialogResult result = MessageBox.Show("ログアウトしますか？", "ログアウト", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result == DialogResult.Yes)
                    {
                        LoginUserCode = "";
                        ログインボタン.Text = "ログイン";
                        ログインボタン_Click(sender, e);  // ログインボタンを再度クリックしてログイン処理を実行
                    }
                    else
                    {
                        return;
                    }
                }

                LoginUserFullName = EmployeeName(cn, LoginUserCode);

                FunctionClass fn = new FunctionClass();
                this.ログインユーザー名.Text = fn.Zn(LoginUserFullName).ToString();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "ログイン", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 受注入力ボタン_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(LoginUserCode))
            {
                MessageBox.Show("システムにログインする必要があります。。", CommonConstants.STR_APPTITLE, MessageBoxButtons.OK);
                this.ログインボタン_Click(this, EventArgs.Empty);
                ;
            }

            if (string.IsNullOrEmpty(LoginUserCode)) return;
            F_受注 fm = new F_受注();
            fm.ShowDialog();
        }

        private void 商品登録ボタン_Click(object sender, EventArgs e)
        {
            F_商品 fm = new F_商品();
            fm.ShowDialog();
        }

        private void 商品管理ボタン_Click(object sender, EventArgs e)
        {
            F_商品管理 fm = new F_商品管理();
            fm.ShowDialog();
        }

        private void シリーズ登録ボタン_Click(object sender, EventArgs e)
        {
            F_シリーズ fm = new F_シリーズ();
            fm.ShowDialog();
        }


        private void 顧客登録ボタン_Click(object sender, EventArgs e)
        {
            param = $" -sv:{ServerInstanceName} -open:customer";
            GetShell(param);
        }

        private void 顧客管理ボタン_Click(object sender, EventArgs e)
        {
            string replacedServerInstanceName = ServerInstanceName.Replace(" ", "_");
            string param = $" -sv:{replacedServerInstanceName} -open:customerlist";
            GetShell(param);
        }

        private void 依頼主登録_Click(object sender, EventArgs e)
        {
            string replacedServerInstanceName = ServerInstanceName.Replace(" ", "_");
            string param = $" -sv:{replacedServerInstanceName} -open:client";
            GetShell(param);
        }

        private void 依頼主管理ボタン_Click(object sender, EventArgs e)
        {
            string replacedServerInstanceName = ServerInstanceName.Replace(" ", "_");
            string param = $" -sv:{replacedServerInstanceName} -open:clientlist";
            GetShell(param);
        }

        private void RunShippingListButton_Click(object sender, EventArgs e)
        {
            string replacedServerInstanceName = ServerInstanceName.Replace(" ", "_");
            string param = $" -sv:{replacedServerInstanceName} -open:shippingloglist";
            GetShell(param);
        }

        private void 出荷管理ボタン_Click(object sender, EventArgs e)
        {

        }

        private void シリーズ在庫参照ボタン_Click(object sender, EventArgs e)
        {
            F_シリーズ在庫参照 fm = new F_シリーズ在庫参照();
            fm.ShowDialog();
        }

        private void 売上分析ボタン_Click(object sender, EventArgs e)
        {
            // F_売上分析 fm = new F_売上分析();
            //fm.ShowDialog();
        }

        private void 売上計画ボタン_Click(object sender, EventArgs e)
        {
            string folderPath = @"\\headsv2\group\sales\tools\売上計画";
            //string folderPath = @"D:\";
            // フォルダをエクスプローラーで開く
            Process.Start("explorer.exe", folderPath);
        }

        private void システム設定ボタン_Click(object sender, EventArgs e)
        {
            F_システム fm = new F_システム();
            fm.ShowDialog();
        }

        private void ユーアイホームボタン_Click(object sender, EventArgs e)
        {
            string ad = "http://headsv4/";
            try
            {
                Process.Start(ad);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ad + "を開けませんでした。。", "OPEN時エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ProductionSystemButton_Click(object sender, EventArgs e)
        {
            try
            {
                string param = $" -user:{LoginUserName} -p";
                Process process = Process.Start($"{Environment.GetEnvironmentVariable("ProgramFiles")}\\Uinics\\Uinics U-net 3 Client\\unetc.exe", param);

                if (process == null || process.Id == 0)
                {
                    // プロセスが正常に起動できなかった場合
                    if (process == null && process.ExitCode == 53)
                    {
                        process = Process.Start($"{Environment.GetEnvironmentVariable("ProgramFiles")}\\Uinics\\Uinics U-net 3 Client\\unetc.exe", param);

                        if (process == null || process.Id == 0)
                        {
                            // 再度プロセスが正常に起動できなかった場合
                            if (process == null && process.ExitCode == 53)
                            {
                                MessageBox.Show("生産管理システムがインストールされていないか、カスタムインストールされています。" +
                                                "\nお使いの環境では本機能は使用できません。",
                                                "生産管理システム起動", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                    }
                    else
                    {
                        MessageBox.Show("生産管理システムを起動できません。", "生産管理システム起動", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "生産管理システム起動", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 入庫入力ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 入庫管理ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 入庫完了ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 部品納期管理ボタン_Click(object sender, EventArgs e)
        {
            string replacedServerInstanceName = ServerInstanceName.Replace(" ", "_");
            string param = $" -open:shippingloglist";
            GetShell(param);
        }

        private void 購買買掛一覧表ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 仕入先別買掛一覧表ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 仕入先登録ボタン_Click(object sender, EventArgs e)
        {
            F_仕入先 fm = new F_仕入先();
            fm.ShowDialog();
        }

        private void 支払先管理ボタン_Click(object sender, EventArgs e)
        {
            F_仕入先管理 fm = new F_仕入先管理();
            fm.ShowDialog();
        }

        private void 部品棚卸入力ボタン_Click(object sender, EventArgs e)
        {
            string replacedServerInstanceName = ServerInstanceName.Replace(" ", "_");
            string param = $" -sv:{replacedServerInstanceName} -open:inventorylistinput";
            GetShell(param);
        }

        private void 部品棚卸管理ボタン_Click(object sender, EventArgs e)
        {
            string replacedServerInstanceName = ServerInstanceName.Replace(" ", "_");
            string param = $" -sv:{replacedServerInstanceName} -open:inventorylist";
            GetShell(param);
        }

        private void 棚卸作業ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 製品登録ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 製品管理ボタン_Click(object sender, EventArgs e)
        {

        }

        private void ユニット登録ボタン_Click(object sender, EventArgs e)
        {

        }

        private void ユニット管理ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 部品登録ボタン_Click(object sender, EventArgs e)
        {
            F_部品 fm = new F_部品();
            fm.ShowDialog();
        }

        private void 部品管理_Click(object sender, EventArgs e)
        {
            F_部品管理 fm = new F_部品管理();
            fm.ShowDialog();
        }

        private void メーカー登録ボタン_Click(object sender, EventArgs e)
        {
            F_メーカー fm = new F_メーカー();
            fm.ShowDialog();
        }

        private void メーカー管理ボタン_Click(object sender, EventArgs e)
        {
            F_メーカー管理 fm = new F_メーカー管理();
            fm.ShowDialog();
        }

        private void 部品集合登録ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 部品集合管理ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 製品情報ボタン_Click(object sender, EventArgs e)
        {
            string folderPath = @"\\headsv2\documents\製品情報";
            Process.Start("explorer.exe", folderPath);
        }

        private void 入金入力ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 入金管理ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 支払入力ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 支払管理ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 売掛一覧ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 請求処理ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 支払一覧_年間ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 支払一覧_月間ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 振込一覧表ボタン_Click(object sender, EventArgs e)
        {

        }

        private void サポート記録ボタン_Click(object sender, EventArgs e)
        {
            string folderPath = @"\\headsv2\group\manage\品質保証\サポート記録";
            Process.Start("explorer.exe", folderPath);
        }

        private void 総務会計関連ボタン_Click(object sender, EventArgs e)
        {
            string folderPath = @"\\headsv2\documents\総務・会計関連";
            Process.Start("explorer.exe", folderPath);
        }

        private void ISO関連ボタン_Click(object sender, EventArgs e)
        {
            string folderPath = @"\\headsv2\documents\ISO関連";
            Process.Start("explorer.exe", folderPath);
        }

        private void 与信情報ボタン_Click(object sender, EventArgs e)
        {
            string folderPath = @"\\headsv2\documents\顧客与信情報";
            Process.Start("explorer.exe", folderPath);
        }


        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        private Button startDocumentManagementButton;

        private void 文書管理システム起動ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                IntPtr hWnd = FindWindow(null, "Uinics U-net 3 Client");

                if (hWnd == IntPtr.Zero)
                {
                    // ウィンドウが見つからない場合
                    string param = $" -user:{LoginUserName} -d";
                    Process.Start($"{Environment.GetEnvironmentVariable("ProgramFiles")}\\Uinics\\Uinics U-net 3 Client\\unetc.exe", param);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "文書管理システム起動", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void 経営計画書ボタン_Click(object sender, EventArgs e)
        {
            string folderPath = @"\\headsv2\documents\経営計画書";
            Process.Start("explorer.exe", folderPath);
        }

        private void 会議報告書_業績ボタン_Click(object sender, EventArgs e)
        {
            string folderPath = @"\\headsv2\documents\会議報告書\業績会議報告書";
            Process.Start("explorer.exe", folderPath);
        }

        private void 会議報告書_営業ボタン_Click(object sender, EventArgs e)
        {
            string folderPath = @"\\headsv2\documents\会議報告書\営業部会議報告書";
            Process.Start("explorer.exe", folderPath);
        }

        private void 会議報告書_技術ボタン_Click(object sender, EventArgs e)
        {
            string folderPath = @"\\headsv2\documents\会議報告書\技術部会議報告書";
            Process.Start("explorer.exe", folderPath);
        }

        private void 会議報告書_製造ボタン_Click(object sender, EventArgs e)
        {
            string folderPath = @"\\headsv2\documents\会議報告書\製造部会議報告書";
            Process.Start("explorer.exe", folderPath);
        }

        private void 会議報告書_管理ボタン_Click(object sender, EventArgs e)
        {
            string folderPath = @"\\headsv2\documents\ISO関連\管理部会議資料";
            Process.Start("explorer.exe", folderPath);
        }

        private void 会議報告書_品質保証ボタン_Click(object sender, EventArgs e)
        {
            string folderPath = @"\\headsv2\documents\ISO関連\ISO統合（9001,14001）\品質保証・環境会議";
            Process.Start("explorer.exe", folderPath);
        }

        private void 旧文書管理ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 月間予定表ボタン_Click(object sender, EventArgs e)
        {
            string folderPath = @"\\headsv2\documents\月間予定表";
            Process.Start("explorer.exe", folderPath);
        }

        private void ファックス管理ボタン_Click(object sender, EventArgs e)
        {

        }

        private void ファックス送付ボタン_Click(object sender, EventArgs e)
        {
            param = $" -sv:{ServerInstanceName} -open:sendfax";
            GetShell(param);
        }

        private void ファックス送付管理ボタン_Click(object sender, EventArgs e)
        {
            string replacedServerInstanceName = ServerInstanceName.Replace(" ", "_");
            string param = $" -sv:{replacedServerInstanceName} -open:sendfaxlist";
            GetShell(param);
        }

        private void Plog起動ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                Process.Start($"{Environment.GetEnvironmentVariable("ProgramFiles")}\\Uinics\\uinics plog client\\uiplogc.exe");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Plog起動エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void 旧業務日報ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 教育計画管理ボタン_Click(object sender, EventArgs e)
        {
            string replacedServerInstanceName = ServerInstanceName.Replace(" ", "_");
            string param = $" -sv:{replacedServerInstanceName} -open:etplanlist";
            GetShell(param);
        }

        private void 教育訓練資料ボタン_Click(object sender, EventArgs e)
        {
            string folderPath = @"\\headsv2\documents\共有スペース資料";
            Process.Start("explorer.exe", folderPath);
        }

        private void 年間教育計画表ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 資格認定登録ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                IntPtr hWnd = FindWindow(null, "Uinics U-net 3 Client");

                if (hWnd == IntPtr.Zero)
                {
                    // ウィンドウが見つからない場合
                    string param = $" -user:{LoginUserName}-open:qualification";
                    Process.Start($"{Environment.GetEnvironmentVariable("ProgramFiles")}\\Uinics\\Uinics U-net 3 Client\\unetc.exe", param);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "資格認定登録起動エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 資格認定管理ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                IntPtr hWnd = FindWindow(null, "Uinics U-net 3 Client");

                if (hWnd == IntPtr.Zero)
                {
                    // ウィンドウが見つからない場合
                    string param = $" -user:{LoginUserName}-open:qualificationlist";
                    Process.Start($"{Environment.GetEnvironmentVariable("ProgramFiles")}\\Uinics\\Uinics U-net 3 Client\\unetc.exe", param);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "資格認定管理起動エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 社員登録ボタン_Click(object sender, EventArgs e)
        {
            F_社員 fm = new F_社員();
            fm.ShowDialog();
        }

        private void 社員管理ボタン_Click(object sender, EventArgs e)
        {
            F_社員管理 fm = new F_社員管理();
            fm.ShowDialog();
        }

        private void 消費税登録ボタン_Click(object sender, EventArgs e)
        {
            param = $" -sv:{ServerInstanceName} -open:consumptiontax";
            GetShell(param);
        }

        private void 単位登録ボタン_Click(object sender, EventArgs e)
        {
            param = $" -sv:{ServerInstanceName} -open:unitcategory";
            GetShell(param);
        }

        private void 地区マスタメンテボタン_Click(object sender, EventArgs e)
        {

        }

        private void マスタメンテボタン_Click(object sender, EventArgs e)
        {

        }

        private void 休日ボタン_Click(object sender, EventArgs e)
        {
            string replacedServerInstanceName = ServerInstanceName.Replace(" ", "_");
            string param = $" -sv:{replacedServerInstanceName} -open:holiday";
            GetShell(param);
        }
    }
}
