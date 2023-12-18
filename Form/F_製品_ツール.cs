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
    public partial class F_製品_ツール : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "製品_ツール";
        private int selected_frame = 0;

        private string strUnitCode;

        public F_製品_ツール()
        {
            this.Text = "製品_ツール";       // ウィンドウタイトルを設定
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


            strUnitCode = args;
        }

        private void Form_Unload(object sender, FormClosingEventArgs e)
        {
           

        }

      

        private void 閉じるボタン_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 構成番号検索ボタン_Click(object sender, EventArgs e)
        {
            F_ユニット構成番号検索 targetform = new F_ユニット構成番号検索();

            F_製品? f_製品 = Application.OpenForms.OfType<F_製品>().FirstOrDefault();

            targetform.args = f_製品.CurrentCode + "," + f_製品.CurrentEdition;
            targetform.ShowDialog();
        }

        private void 全印刷ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 全印刷ボタン_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■ユニット表および部品表を印刷します。";
        }
    }
}
