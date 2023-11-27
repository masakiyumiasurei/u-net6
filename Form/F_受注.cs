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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data.Common;
using GrapeCity.Win.MultiRow;
using System.ComponentModel;
using System.DirectoryServices;
using Microsoft.EntityFrameworkCore.Metadata;

namespace u_net
{
    public partial class F_受注 : Form
    {
        private Control? previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        public string CurrentCode = "";
        public F_受注()
        {
            this.Text = "受注（製図指図書）";       // ウィンドウタイトルを設定
            this.MaximizeBox = false;  // 最大化ボタンを無効化

            InitializeComponent();
        }
        public void Connect()
        {
            Connection connectionInfo = new Connection();
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
            try
            {
                this.SuspendLayout();

                FunctionClass fn = new FunctionClass();
                fn.DoWait("しばらくお待ちください...");

                OriginalClass ofn = new OriginalClass();

                ofn.SetComboBox(受注コード, "SELECT A.受注コード AS Display, A.受注コード AS Value, { fn REPLACE(STR(CONVERT(bit, T受注.無効日), 1, 0), '1', '×') } AS Display2 " +
                    "FROM T受注 INNER JOIN (SELECT TOP 100 受注コード, MAX(受注版数) AS 最新版数 FROM T受注 GROUP BY 受注コード ORDER BY T受注.受注コード DESC) A ON T受注.受注コード = A.受注コード AND T受注.受注版数 = A.最新版数 ORDER BY A.受注コード DESC");

                int intWindowHeight = this.Height;
                int intWindowWidth = this.Width;

                previousControl = null;

                fn.WaitForm.Close();

                //実行中フォーム起動
                string LoginUserCode = "000";//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
                LocalSetting localSetting = new LocalSetting();
                localSetting.LoadPlace(LoginUserCode, this);

            }
            catch (Exception ex)
            {
                MessageBox.Show("初期化に失敗しました。", "エラー");
            }
            finally
            {
                this.ResumeLayout();
            }
        }

        private void コマンド終了_Click(object sender, EventArgs e)
        {
            this.Close();
        }


        private void F_商品_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space: //コンボボックスならドロップダウン
                    {
                        Control activeControl = this.ActiveControl;
                        if (activeControl is System.Windows.Forms.ComboBox)
                        {
                            System.Windows.Forms.ComboBox activeComboBox = (System.Windows.Forms.ComboBox)activeControl;
                            activeComboBox.DroppedDown = true;
                        }
                    }
                    break;
                case Keys.F1:
                    if (コマンド新規.Enabled)
                    {
                        コマンド新規.Focus();
                        //コマンド新規_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F2:
                    if (コマンド読込.Enabled)
                    {
                        コマンド読込.Focus();
                        //コマンド修正_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F3:
                    if (コマンド複写.Enabled)
                    {
                        //コマンド複写_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F4:
                    if (コマンド削除.Enabled)
                    {
                        //コマンド削除_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F5:
                    if (コマンド顧客.Enabled)
                    {
                        //コマンドシリーズ_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F9:
                    if (コマンド承認.Enabled)
                    {
                        //コマンド承認_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F10:
                    if (コマンド確定.Enabled)
                    {
                        //コマンド確定_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F11:
                    if (コマンド登録.Enabled)
                    {
                        コマンド登録.Focus();
                        //コマンド登録_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F12:
                    if (コマンド終了.Enabled)
                    {
                        //コマンド終了_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
            }
        }

        private void 備考_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■全角１００文字まで入力できます。";
        }

        private void 受注コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 300, 50 }, new string[] { "Display", "Display2" });
            受注コード.Invalidate();
            受注コード.DroppedDown = true;
        }
    }
}


