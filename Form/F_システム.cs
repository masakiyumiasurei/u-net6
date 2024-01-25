using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using u_net.Public;
using Timer = System.Windows.Forms.Timer;
using static u_net.CommonConstants;
using static u_net.Public.CommonModule;
using static u_net.Public.FunctionClass;
using System.Data.Common;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace u_net
{
    public partial class F_システム : Form
    {
        public F_システム()
        {
            InitializeComponent();

            // タイマーの設定
            timer = new Timer();
            timer.Interval = 1000; // 1秒ごとに更新
            timer.Tick += Timer_Tick;

        }

        private SqlConnection cn;
        private Timer timer;

        private void Timer_Tick(object sender, EventArgs e)
        {
            // 現在の日時を取得してテキストボックスに表示
            クライアント日時.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
            DateTime date = DateTime.Parse(サーバー日時.Text);
            サーバー日時.Text = date.AddSeconds(1).ToString();

        }

        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        private void Form_Load(object sender, EventArgs e)
        {

            timer.Start();

            string LoginUserCode = CommonConstants.LoginUserCode;
            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(LoginUserCode, this);

            Connect();

            Connection connection = new Connection();
            接続文字列.Text = connection.Getconnect();

            if (cn.State == ConnectionState.Open)
            {
                接続状況.Text = "接続されています";
            }
            else
            {
                接続状況.Text = "接続されていません";
            }

            MyOsName = SysVersion();
            MyComputerName = GetComputerName();
            MyUserName = NetUserName();

        // 接続対象サーバー設定
            接続対象サーバー.Text = 接続文字列.Text.Contains("Secondary") ? "テストサーバー" : "運用サーバー";

            // その他の情報設定
            サーバー名.Text = GetServerName(cn);
            サーバー日時.Text = FunctionClass.GetServerDate(cn).ToString();
            バージョン.Text = strAppVer;
            最新バージョン.Text = strAppLastVer;
            コンピュータ名.Text = MyComputerName;
            OS.Text = MyOsName;
            ランタイムバージョン.Text = GetVersionInfo();
            ネットワークユーザー名.Text = MyUserName;
            接続タイムアウト_プロジェクト.Text = cn.ConnectionTimeout.ToString();
            接続タイムアウト_ADO.Text = cn.ConnectionTimeout.ToString();
            接続タイムアウト_Command.Text = cn.CommandTimeout.ToString();

            
        }




        private void F_システム_FormClosing(object sender, FormClosingEventArgs e)
        {
            string LoginUserCode = CommonConstants.LoginUserCode;
            LocalSetting ls = new LocalSetting();
            ls.SavePlace(LoginUserCode, this);
        }

        private void F_システム_FormClosed(object sender, FormClosedEventArgs e)
        {

        }

        private void サーバー日時更新ボタン_Click(object sender, EventArgs e)
        {
            Connect();

            サーバー日時.Text = FunctionClass.GetServerDate(cn).ToString();
        }

        private void サウンドテストボタン_Click(object sender, EventArgs e)
        {
            OriginalClass.PlaySound("logon.wav");
        }

        private void バージョンアップボタン_Click(object sender, EventArgs e)
        {
            string message = "現在開発中です。\n\nこのコマンドはクライアントアプリケーションの\nアップデートを自動的に行うためのものです。";

            MessageBox.Show(message, "バージョンアップ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void 管理者用バージョンアップボタン_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("このアプリケーションのバージョンアップを行いますか？\nバージョンを戻すことはできません。",
                                              "バージョンアップ", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                F_認証 fm = new F_認証();
                fm.args = "014";
                fm.ShowDialog();

                if (string.IsNullOrEmpty(strCertificateCode))
                {
                    MessageBox.Show("認証に失敗しました。" + Environment.NewLine + "承認できません。。", "承認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Connect();
                string strSQL = "INSERT Tバージョン (登録日) Values (getdate())";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.ExecuteNonQuery();
                }

                最新バージョン.Text = GetNewVersion(cn).ToString(); 
            }
        }

        System.Windows.Forms.ProgressBar progressBar;
        private void 進捗状況テストボタン_Click(object sender, EventArgs e)
        {
            progressBar = new System.Windows.Forms.ProgressBar();

            progressBar.Maximum = 1000;
            progressBar.Step = 1;

            Thread progressThread = new Thread(UpdateProgressBar);
            progressThread.Start();

        }

        private void UpdateProgressBar()
        {
            // 進捗状況の更新
            for (int i = 1; i <= progressBar.Maximum; i++)
            {
                Thread.Sleep(10); // 実際の処理をシミュレートするためのスリープ
                //progressBar.PerformStep();
                //test.Text = i.ToString();

                this.Invoke(new Action(() =>
                {
                    progressBar.PerformStep();
                    test.Text = i.ToString();
                }));
            }
        }

        private void 接続テスト2ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 接続テストボタン_Click(object sender, EventArgs e)
        {
            string message = "接続はDBアクセスの度に行っているため、";

            MessageBox.Show(message, "バージョンアップ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void 接続運用ボタン_Click(object sender, EventArgs e)
        {
            string message = "OPEN処理はDBアクセスの度に行っているため、この処理は不要かと思います";
            MessageBox.Show(message, "確認", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void 接続設定ボタン_Click(object sender, EventArgs e)
        {
            Connect();

            // Accessデータベースへの接続
            try
            {
                cn.Open();
                Console.WriteLine("Connected to the database.");

                // ここにデータベースへのクエリや処理を追加する

                // 接続を切断
                cn.Close();
                Console.WriteLine("Disconnected from the database.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

        }



        private void 閉じる_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
