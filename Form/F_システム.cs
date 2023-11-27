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

            string LoginUserCode = CommonConstants.LoginUserCode;//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(LoginUserCode, this);


            Connect();

            サーバー日時.Text = FunctionClass.GetServerDate(cn).ToString();

            
        }

      


        private void F_システム_FormClosing(object sender, FormClosingEventArgs e)
        {
            string LoginUserCode = CommonConstants.LoginUserCode;//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            LocalSetting test = new LocalSetting();
            test.SavePlace(LoginUserCode, this);
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

        }

        private void バージョンアップボタン_Click(object sender, EventArgs e)
        {
            string message = "現在開発中です。\n\nこのコマンドはクライアントアプリケーションの\nアップデートを自動的に行うためのものです。";

            MessageBox.Show(message, "バージョンアップ", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void 管理者用バージョンアップボタン_Click(object sender, EventArgs e)
        {

        }

        private void 進捗状況テストボタン_Click(object sender, EventArgs e)
        {

        }

        private void 接続テスト2ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 接続テストボタン_Click(object sender, EventArgs e)
        {

        }

        private void 接続運用ボタン_Click(object sender, EventArgs e)
        {

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
