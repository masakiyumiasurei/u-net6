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
using Microsoft.Web.WebView2.Core;
using System.Reflection;

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

            string appPath = Application.StartupPath;
            // Image yourImage = Properties.Resources.your_image_resource_name;

            // アイコン画像のファイルパス
            //string iconPath = Path.Combine(appPath, "icon", "共通.png");

            //string filename = "共通.png";
            string iconPath = "共通.png";
            
            // タブにアイコンを設定
            //SetTabImage(tabControl1, iconPath, 5);

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

        public ImageList ImageList
        {
            get; set;
        }

        private void SetTabImage(TabControl tabControl, string imageName, int tabIndex)
        {
            try
            {

                ImageList imageList = new ImageList();

                imageList.Images.Add(this.不在イメージコマンド.Image);
                imageList.Images.Add(this.在席イメージコマンド.Image);
                imageList.ImageSize = new Size(16, 16);
                tabControl.HotTrack = true;
                tabControl.Appearance = TabAppearance.FlatButtons;
                tabControl.ImageList = imageList;



                tabControl.TabPages[tabIndex].BackgroundImage = imageList.Images[0];
                tabControl.TabPages[tabIndex].BackgroundImageLayout = ImageLayout.Center;

                tabControl.TabPages[0].BackgroundImage = imageList.Images[1];
                tabControl.TabPages[0].BackgroundImageLayout = ImageLayout.None;
                //tabControl.TabPages[tabIndex].BackgroundImage = this.不在イメージコマンド.Image;//image;
                // tabControl.TabPages[tabIndex].BackgroundImageLayout = ImageLayout.Stretch; // 画像をタブ全体に広げる場合

            }
            catch (Exception ex)
            {
                MessageBox.Show($"画像の設定に失敗しました。\n\n{ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void Form_Load(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                control.PreviewKeyDown += OriginalClass.ValidateCheck;
            }

            toolTip1.SetToolTip(営業部部長不在ボタン, "営業部部長の在席状況を切り替えます");
            toolTip1.SetToolTip(ユーアイホームボタン, "UI Homeへジャンプします");

            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");

            try
            {
                Connect();
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
                ServerInstanceName = m_strServerName;

                this.ログインユーザー名.Text = fn.Zn(LoginUserFullName).ToString();

                if (LoginUserCode == "")
                {
                    ログインボタン_Click(sender, e);

                    if (LoginUserCode == "")
                    {
                        MessageBox.Show("システムにログインする必要があります。", "認証してください", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                fn.WaitForm.Close();
            }
        }

        private void コマンド終了_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Form_Unload(object sender, FormClosingEventArgs e)
        {
            LoginUserCode = "";
        }

        private void ログインボタン_Click(object sender, EventArgs e)
        {
            try
            {

                if (LoginUserCode == "")
                {
                    // ログインコードが未設定の場合、認証フォームを開く
                    F_認証 fm = new F_認証();
                    fm.ShowDialog();

                    if (!string.IsNullOrEmpty(strCertificateCode))
                    {
                        LoginUserCode = strCertificateCode;
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

                //親フォームにログインユーザ名と接続先を表示する
                F_MdiParent frmTarget = Application.OpenForms.OfType<F_MdiParent>().FirstOrDefault();
                frmTarget.toolStripStatusLabel1.Text = LoginUserFullName;

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

        private void 受注管理ボタン_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(LoginUserCode))
            {
                MessageBox.Show("システムにログインする必要があります。。", CommonConstants.STR_APPTITLE, MessageBoxButtons.OK);
                this.ログインボタン_Click(this, EventArgs.Empty);
                ;
            }

            if (string.IsNullOrEmpty(LoginUserCode)) return;
            F_受注管理 fm = new F_受注管理();
                        
            fm.ShowDialog();
        }

        private void 商品登録ボタン_Click(object sender, EventArgs e)
        {
            F_商品 fm = new F_商品();
            fm.MdiParent = this.MdiParent;
            fm.FormClosed += (s, args) => { this.Enabled = true; };
            this.Enabled = false;
            fm.Show();
            //fm.ShowDialog();
        }

        private void 商品管理ボタン_Click(object sender, EventArgs e)
        {
            F_商品管理 fm = new F_商品管理();
            fm.MdiParent = this.MdiParent;
            fm.FormClosed += (s, args) => { this.Enabled = true; };
            this.Enabled = false;

            fm.Show();
            //fm.ShowDialog();
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
            F_出荷管理旧 fm = new F_出荷管理旧();
            fm.ShowDialog();
        }

        private void シリーズ在庫参照ボタン_Click(object sender, EventArgs e)
        {
            F_シリーズ在庫参照 fm = new F_シリーズ在庫参照();
            fm.ShowDialog();
        }

        private void 売上分析ボタン_Click(object sender, EventArgs e)
        {
            F_売上分析 fm = new F_売上分析();
            fm.ShowDialog();
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
                ユーアイホームボタン.ForeColor = Color.FromArgb(0, 0, 255);
                Process.Start(ad);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ad + "を開けませんでした。。", "OPEN時エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ユーアイホームボタン_MouseMove(object sender, MouseEventArgs e)
        {
            if (ユーアイホームボタン.ForeColor == Color.FromArgb(0, 0, 255))
            {
                ユーアイホームボタン.ForeColor = Color.FromArgb(255, 0, 0);
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
            F_入庫 fm = new F_入庫();
            fm.ShowDialog();
        }

        private void 入庫管理ボタン_Click(object sender, EventArgs e)
        {
            F_入庫管理 fm = new F_入庫管理();
            fm.ShowDialog();
        }

        private void 入庫完了ボタン_Click(object sender, EventArgs e)
        {
            F_入庫完了 fm = new F_入庫完了();
            fm.ShowDialog();
        }

        private void 部品納期管理ボタン_Click(object sender, EventArgs e)
        {
            string replacedServerInstanceName = ServerInstanceName.Replace(" ", "_");
            string param = $" -open:shippingloglist";
            GetShell(param);
        }

        private void 購買買掛一覧表ボタン_Click(object sender, EventArgs e)
        {
            F_購買買掛一覧表 fm = new F_購買買掛一覧表();
            fm.ShowDialog();
        }

        private void 仕入先別買掛一覧表ボタン_Click(object sender, EventArgs e)
        {
            F_仕入先別買掛一覧表 fm = new F_仕入先別買掛一覧表();
            fm.ShowDialog();
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
            F_棚卸作業 fm = new F_棚卸作業();
            fm.ShowDialog();
        }

        private void 製品登録ボタン_Click(object sender, EventArgs e)
        {
            F_製品 fm = new F_製品();
            fm.ShowDialog();
        }

        private void 製品管理ボタン_Click(object sender, EventArgs e)
        {
            F_製品管理 fm = new F_製品管理();
            fm.ShowDialog();
        }

        private void ユニット登録ボタン_Click(object sender, EventArgs e)
        {
            F_ユニット fm = new F_ユニット();
            fm.ShowDialog();
        }

        private void ユニット管理ボタン_Click(object sender, EventArgs e)
        {
            F_ユニット管理 fm = new F_ユニット管理();
            fm.ShowDialog();
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
            F_部品集合 fm = new F_部品集合();
            fm.ShowDialog();
        }

        private void 部品集合管理ボタン_Click(object sender, EventArgs e)
        {
            F_部品集合管理 fm = new F_部品集合管理();
            fm.ShowDialog();
        }

        private void 製品情報ボタン_Click(object sender, EventArgs e)
        {
            string folderPath = @"\\headsv2\documents\製品情報";
            Process.Start("explorer.exe", folderPath);
        }

        private void 入金入力ボタン_Click(object sender, EventArgs e)
        {
            F_入金 fm = new F_入金();
            fm.ShowDialog();
        }

        private void 入金管理ボタン_Click(object sender, EventArgs e)
        {
            F_入金管理 fm = new F_入金管理();
            fm.ShowDialog();
        }

        private void 支払入力ボタン_Click(object sender, EventArgs e)
        {
            F_支払 fm = new F_支払();
            fm.ShowDialog();
        }

        private void 支払管理ボタン_Click(object sender, EventArgs e)
        {
            F_支払管理 fm = new F_支払管理();
            fm.ShowDialog();
        }

        private void 売掛一覧ボタン_Click(object sender, EventArgs e)
        {
            F_売掛一覧 fm = new F_売掛一覧();
            fm.ShowDialog();
        }

        private void 請求処理ボタン_Click(object sender, EventArgs e)
        {
            F_請求処理 fm = new F_請求処理();
            fm.ShowDialog();
        }

        private void 支払一覧_年間ボタン_Click(object sender, EventArgs e)
        {
            F_支払一覧_年間 fm = new F_支払一覧_年間();
            fm.ShowDialog();
        }

        private void 支払一覧_月間ボタン_Click(object sender, EventArgs e)
        {
            F_支払一覧_月間 fm = new F_支払一覧_月間();
            fm.ShowDialog();
        }

        private void 振込一覧表ボタン_Click(object sender, EventArgs e)
        {
            F_振込一覧 fm = new F_振込一覧();
            fm.ShowDialog();
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
            //F_文書管理 fm = new F_文書管理();
            //fm.ShowDialog();
        }

        private void 月間予定表ボタン_Click(object sender, EventArgs e)
        {
            string folderPath = @"\\headsv2\documents\月間予定表";
            Process.Start("explorer.exe", folderPath);
        }

        private void ファックス管理ボタン_Click(object sender, EventArgs e)
        {
            F_ファックス管理 fm = new F_ファックス管理();
            fm.ShowDialog();
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
            F_業務日報 fm = new F_業務日報();
            fm.ShowDialog();
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
            F_年間教育計画 fm = new F_年間教育計画();
            fm.ShowDialog();
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
            F_地区マスタ fm = new F_地区マスタ();
            fm.ShowDialog();
        }

        private void マスタメンテボタン_Click(object sender, EventArgs e)
        {
            F_マスタメンテ fm = new F_マスタメンテ();
            fm.ShowDialog();
        }

        private void 休日ボタン_Click(object sender, EventArgs e)
        {
            string replacedServerInstanceName = ServerInstanceName.Replace(" ", "_");
            string param = $" -sv:{replacedServerInstanceName} -open:holiday";
            GetShell(param);
        }

        private void 営業部部長不在ボタン_Click(object sender, EventArgs e)
        {
            Connect();
            try
            {
                string strEmployeeCode = USER_CODE_SALES;

                // ログインユーザーが承認者でなければ認証する
                if (LoginUserCode != strEmployeeCode)
                {
                    F_認証 fm = new F_認証();
                    fm.args = strEmployeeCode;
                    fm.ShowDialog();

                    // 認証が不成立ならば処理終了
                    if (string.IsNullOrEmpty(strCertificateCode))
                        return;
                }

                string strSQL = $"SELECT * FROM T不在社員 WHERE 社員コード = '{strEmployeeCode}'";
                using (SqlDataAdapter adapter = new SqlDataAdapter(strSQL, cn))
                {
                    using (SqlCommandBuilder builder = new SqlCommandBuilder(adapter))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count == 0)
                        {
                            // レコードが存在しない場合は追加
                            DataRow newRow = dataTable.NewRow();
                            newRow["社員コード"] = strEmployeeCode;
                            dataTable.Rows.Add(newRow);
                            adapter.Update(dataTable);
                            MessageBox.Show("不在にしました");

                            this.営業部部長不在ボタン.Image = this.不在イメージコマンド.Image;
                        }
                        else
                        {
                            // レコードが存在する場合は削除
                            dataTable.Rows[0].Delete();
                            adapter.Update(dataTable);
                            MessageBox.Show("在席にしました");

                            this.営業部部長不在ボタン.Image = this.在席イメージコマンド.Image;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                Debug.Print($"営業部部長不在ボタン_Click - {ex.Message}");
                MessageBox.Show("エラーが発生しました。\n" + ex.Message);
            }

        }

        private void 承認管理ボタン_Click(object sender, EventArgs e)
        {
            F_承認管理 fm = new F_承認管理();
            fm.ShowDialog();
        }

        private void 見積入力ボタン_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(LoginUserCode))
            {
                MessageBox.Show("システムにログインする必要があります。。", CommonConstants.STR_APPTITLE, MessageBoxButtons.OK);
                this.ログインボタン_Click(this, EventArgs.Empty);
                ;
            }

            if (string.IsNullOrEmpty(LoginUserCode)) return;
            F_見積 fm = new F_見積();
            fm.ShowDialog();
        }

        private void 見積管理ボタン_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(LoginUserCode))
            {
                MessageBox.Show("システムにログインする必要があります。。", CommonConstants.STR_APPTITLE, MessageBoxButtons.OK);
                this.ログインボタン_Click(this, EventArgs.Empty);
                ;
            }

            if (string.IsNullOrEmpty(LoginUserCode)) return;
            F_見積管理 fm = new F_見積管理();
            fm.ShowDialog();
        }

        private void 商品構成ボタン_Click(object sender, EventArgs e)
        {
            F_商品構成2 fm = new F_商品構成2();
            fm.ShowDialog();
        }

        private void 購買申請入力ボタン_Click(object sender, EventArgs e)
        {
            F_購買申請 fm = new F_購買申請();
            fm.ShowDialog();
        }

        private void 購買申請管理ボタン_Click(object sender, EventArgs e)
        {
            F_購買申請管理 fm = new F_購買申請管理();
            fm.ShowDialog();
        }

        private void 発注入力ボタン_Click(object sender, EventArgs e)
        {
            F_発注 fm = new F_発注();
            fm.ShowDialog();
        }

        private void 発注管理ボタン_Click(object sender, EventArgs e)
        {
            F_発注管理 fm = new F_発注管理();
            fm.ShowDialog();
        }

        private void 会社情報ボタン_Click(object sender, EventArgs e)
        {
            F_会社情報 fm = new F_会社情報();
            fm.ShowDialog();
        }
    }
}
