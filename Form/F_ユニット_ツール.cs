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
            F_ユニット? fm = Application.OpenForms.OfType<F_ユニット>().FirstOrDefault();
            F_ユニット参照? fm2 = Application.OpenForms.OfType<F_ユニット参照>().FirstOrDefault();

            if (fm != null)
            {
                if (fm.IsDecided) 
                {
                    MessageBox.Show("確定されているので、データを変更できません。", "ツールコマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            else
           // if (fm2 != null)
            {
                //if (fm2.IsDecided)
                //{
                    MessageBox.Show("参照からは、データを変更できません。", "ツールコマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                //}
            }

            F_ユニット部品一括変更 targetform = new F_ユニット部品一括変更();
            targetform.MdiParent = this.MdiParent;
            targetform.FormClosed += (s, args) => { this.Enabled = true; };
            this.Enabled = false;

            targetform.Show();

        }

        private void 使用製品検索ボタン_Click(object sender, EventArgs e)
        {
            F_ユニット使用製品参照 targetform = new F_ユニット使用製品参照();

            targetform.args = strUnitCode;
            targetform.MdiParent = this.MdiParent;
            targetform.FormClosed += (s, args) => { this.Enabled = true; };
            this.Enabled = false;

            targetform.Show();

        }

        private void 材料費の強制取得ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                F_ユニット? fm = Application.OpenForms.OfType<F_ユニット>().FirstOrDefault();
                F_ユニット参照? fm2 = Application.OpenForms.OfType<F_ユニット参照>().FirstOrDefault();
                if (fm != null)
                {
                    MessageBox.Show("現在の材料費は　" + fm.ユニット明細1.Detail.ColumnFooters[0].Cells["製品材料費"].Value.ToString() + "　円です。", "ツールコマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                if (fm2 != null)
                {
                    MessageBox.Show("現在の材料費は　" + fm2.ユニット明細参照1.Detail.ColumnFooters[0].Cells["製品材料費"].Value.ToString() + "　円です。", "ツールコマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーのため取得できませんでした。", "ツールコマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void コマンド出力_Click(object sender, EventArgs e)
        {
            Connect();
            string sqlQuery = "";
            F_ユニット? fm = Application.OpenForms.OfType<F_ユニット>().FirstOrDefault();
            F_ユニット参照? fm2 = Application.OpenForms.OfType<F_ユニット参照>().FirstOrDefault();
            if (fm != null)
            {
                sqlQuery = "SELECT * FROM V部品表 where ユニットコード='" + fm.CurrentCode + "' and ユニット版数=" + fm.CurrentEdition + " ORDER BY 明細番号";
            }
            else if (fm2!=null)
            {
                sqlQuery = "SELECT * FROM V部品表 where ユニットコード='" + fm2.CurrentCode + "' and ユニット版数=" + fm2.CurrentEdition + " ORDER BY 明細番号";
            }

            // 新しいDataGridViewを作成
            DataGridView dataGridView1 = new DataGridView();
            dataGridView1.Visible = false;
            this.Controls.Add(dataGridView1);

            using (SqlDataAdapter adapter = new SqlDataAdapter(sqlQuery, cn))
            {
                dataGridView1.SuspendLayout();
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                dataGridView1.ResumeLayout();
            }


            F_出力 targetform = new F_出力();
            targetform.DataGridView = dataGridView1;
            targetform.MdiParent = this.MdiParent;
            targetform.FormClosed += (s, args) => { this.Enabled = true; };
            this.Enabled = false;

            targetform.Show();

        }
    }
}
