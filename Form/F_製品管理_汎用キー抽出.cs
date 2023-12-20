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
            // 対象フォームが読み込まれていないときはすぐに終了する
            if (Application.OpenForms["F_製品管理"] == null)
            {
                MessageBox.Show("[製品管理]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            //開いているフォームのインスタンスを作成する
            F_製品管理 frmTarget = Application.OpenForms.OfType<F_製品管理>().FirstOrDefault();

            汎用キー1.Text = frmTarget.strKey1;

        }



        private void 抽出ボタン_Click(object sender, EventArgs e)
        {
            F_製品管理? frmTarget = Application.OpenForms.OfType<F_製品管理>().FirstOrDefault();


            frmTarget.strKey1 = Nz(汎用キー1.Text);

            long cnt = frmTarget.DoUpdate();

            if (cnt == 0)
            {
                MessageBox.Show("抽出条件に一致するデータはありません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                return;
            }
            else if (cnt < 0)
            {
                MessageBox.Show("エラーが発生したため、抽出できませんでした。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Close();
        }

        private void キャンセルボタン_Click(object sender, EventArgs e)
        {
            Close();
        }



        // Nz メソッドの代替
        private T Nz<T>(T value)
        {
            if (value == null)
            {
                return default(T);
            }
            return value;
        }

    }
}
