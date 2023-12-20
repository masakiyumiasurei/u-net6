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
using MultiRowDesigner;

namespace u_net
{
    public partial class F_発注_買掛区分設定 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";


        public F_発注_買掛区分設定()
        {
            this.Text = "発注_買掛区分設定";       // ウィンドウタイトルを設定
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

            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");

            try
            {
                OriginalClass ofn = new OriginalClass();
                string sql = "SELECT * from V買掛区分";
                ofn.SetComboBox(買掛区分, sql);


                F_発注 frmOrder = Application.OpenForms.OfType<F_発注>().FirstOrDefault();
                //Form frmOrder = Application.OpenForms["F_発注"];
                if (frmOrder == null) return;
                発注明細 subform = Application.OpenForms.OfType<発注明細>().FirstOrDefault();

                Form frmTarget = frmOrder.Controls["発注明細1"] as Form;

                if (frmTarget != null)
                {

                    this.発注コード.Text = frmTarget.Controls["発注コード"].Text;
                    this.発注版数.Text = frmTarget.Controls["発注版数"].Text;
                    // 他のコントロールにも同様に値をコピーする
                    // 例: this.textBox明細番号.Text = frmTarget.Controls["明細番号"].Text;
                    // ...
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("初期化に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            finally
            {
                this.ResumeLayout();
                fn.WaitForm.Close();
            }
        }
              

        private void OKボタン_Click(object sender, EventArgs e)
        {

        }

        private void キャンセルボタン_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 買掛区分_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■買掛区分を選択します。";
        }

        private void 買掛区分_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }
    }
}
