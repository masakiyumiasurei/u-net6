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
using GrapeCity.Win.MultiRow;

namespace u_net
{
    public partial class F_発注_買掛区分設定 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        public string SelectedCode = "";

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

        F_発注 frmOrder = Application.OpenForms.OfType<F_発注>().FirstOrDefault();
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
                string sql = "SELECT 買掛区分 as Display,買掛区分コード as Display2, 買掛明細コード as Display3 , 買掛区分 as Value from V買掛区分";
                ofn.SetComboBox(買掛区分, sql);


                //F_発注 frmOrder = Application.OpenForms.OfType<F_発注>().FirstOrDefault();
                //Form frmOrder = Application.OpenForms["F_発注"];
                if (frmOrder == null) return;


                if (frmOrder != null)
                {
                    this.発注コード.Text = frmOrder.発注コード.Text;
                    this.発注版数.Text = frmOrder.発注版数.Text;

                    this.明細番号.Text = frmOrder.発注明細1.Detail.CurrentRow.Cells["明細番号"].Value?.ToString();
                    this.行番号.Text = frmOrder.発注明細1.Detail.CurrentRow.Cells["行番号"].Value?.ToString();
                    this.買掛区分.Text = frmOrder.発注明細1.Detail.CurrentRow.Cells["買掛区分"].Value?.ToString();
                    this.部品コード.Text = frmOrder.発注明細1.Detail.CurrentRow.Cells["部品コード"].Value?.ToString();
                    this.品名.Text = frmOrder.発注明細1.Detail.CurrentRow.Cells["品名"].Value?.ToString();
                    this.型番.Text = frmOrder.発注明細1.Detail.CurrentRow.Cells["型番"].Value?.ToString();
                    this.メーカー名.Text = frmOrder.発注明細1.Detail.CurrentRow.Cells["メーカー名"].Value?.ToString();

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

        string kubun = "";
        string meisai = "";
        private void 買掛区分_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox combo = sender as ComboBox;
            kubun = ((DataRowView)combo.SelectedItem)?.Row.Field<String>("Display2")?.ToString();
            meisai = ((DataRowView)combo.SelectedItem)?.Row.Field<Int16?>("Display3")?.ToString();
        }

        private void OKボタン_Click(object sender, EventArgs e)
        {
            frmOrder.発注明細1.Detail.CurrentRow.Cells["買掛区分"].Value = this.買掛区分.Text;
            frmOrder.発注明細1.Detail.CurrentRow.Cells["買掛区分コード"].Value = kubun;
            frmOrder.発注明細1.Detail.CurrentRow.Cells["買掛明細コード"].Value = meisai;
            //DialogResult = DialogResult.OK;
            frmOrder.buttonCnt = 1;
            this.Close();
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

        private void F_発注_買掛区分設定_FormClosed(object sender, FormClosedEventArgs e)
        {
            frmOrder.buttonCnt = 1;
        }
    }
}
