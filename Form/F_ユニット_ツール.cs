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
using Pao.Reports;
using GrapeCity.Win.MultiRow;

namespace u_net
{
    public partial class F_ユニット_ツール : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "ユニット_ツール";
        private int selected_frame = 0;

        private string strUnitCode;

        public F_ユニット_ツール()
        {
            this.Text = "ユニット - ツール";       // ウィンドウタイトルを設定
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

        private void 部品一括変更ボタン_Click(object sender, EventArgs e)
        {
            F_ユニット? f_ユニット = Application.OpenForms.OfType<F_ユニット>().FirstOrDefault();

            if (f_ユニット.IsDecided)
            {
                MessageBox.Show("確定されているので、データを変更できません。", "ツールコマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            F_ユニット部品一括変更 targetform = new F_ユニット部品一括変更();

            targetform.ShowDialog();

        }

        private void 使用製品検索ボタン_Click(object sender, EventArgs e)
        {
            F_ユニット使用製品参照 targetform = new F_ユニット使用製品参照();

            targetform.args = strUnitCode;
            targetform.ShowDialog();
        }

        private void 材料費の強制取得ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                F_ユニット? f_ユニット = Application.OpenForms.OfType<F_ユニット>().FirstOrDefault();

                MessageBox.Show("現在の材料費は　" + f_ユニット.ユニット明細1.Detail.ColumnFooters[0].Cells["製品材料費"].Value.ToString() + "　円です。", "ツールコマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーのため取得できませんでした。", "ツールコマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }
    }
}
