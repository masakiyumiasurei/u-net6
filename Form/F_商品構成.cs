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
using MultiRowDesigner;

namespace u_net
{
    public partial class F_商品構成2 : Form
    {
        private Control? previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        //public string CurrentCode = "";
        private bool setCombo = false;
        public F_商品構成2()
        {
            this.Text = "商品構成";       // ウィンドウタイトルを設定
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

        //public bool IsDeleted
        //{
        //    get
        //    {
        //        bool isEmptyOrDbNull = string.IsNullOrEmpty(this.削除日時.Text) || Convert.IsDBNull(this.削除日時.Text);

        //        return !isEmptyOrDbNull;
        //    }
        //}

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

            OriginalClass ofn = new OriginalClass();
            //ofn.SetComboBox(数量単位コード, "SELECT 単位名 as Display,単位コード as Value FROM M単位");
            //ofn.SetComboBox(シリーズコード, "SELECT シリーズ名 as Display,シリーズコード as Value FROM Mシリーズ");
            //ofn.SetComboBox(商品分類名, "SELECT 分類名 as Display,商品分類コード as Value FROM M商品分類");
            //ofn.SetComboBox(売上区分コード, "SELECT 売上区分名 as Display,売上区分コード as Value FROM M売上区分");
            //ofn.SetComboBox(FlowCategoryCode, "SELECT Name as Display,Code as Value FROM ManufactureFlow");
            setCombo = true;



            int intWindowHeight = this.Height;
            int intWindowWidth = this.Width;

            previousControl = null;

            try
            {
                this.SuspendLayout();

                if (string.IsNullOrEmpty(args))
                {
                        return;
                }
                else
                {
                    if (!string.IsNullOrEmpty(args))
                    {

                        this.商品コード.Text = args;
                    }
                }

                fn.WaitForm.Close();

                //実行中フォーム起動              
                LocalSetting localSetting = new LocalSetting();
                localSetting.LoadPlace(CommonConstants.LoginUserCode, this);

            }
            catch (Exception ex)
            {
                ChangedData(false);
                MessageBox.Show("初期化に失敗しました。", "エラー");
            }
            finally
            {
                setCombo = false;
                this.ResumeLayout();
            }
        }

        public void ChangedData(bool dataChanged)
        {

        }

        private void 商品分類コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ActiveControl == null) return;
            if (this.商品分類名.SelectedItem != null)
            {
                Connect();
                string sql = $"SELECT 分類名 FROM M商品分類 WHERE 商品分類コード= {商品分類名.Text}";
                分類内容.Text = OriginalClass.GetScalar<string>(cn, sql);
                cn.Close();
            }
        }

        private void 商品分類コード_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = "■商品が所属する分類を指定します。";
        }

        private void 商品分類コード_TextChanged(object sender, EventArgs e)
        {

        }

        private void 掛率有効_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var point1 = new Point(10, 20);
            var point2 = new Point(200, 150);

            e.Graphics.DrawLine(Pens.Red, point1, point2);
        }
    }
}


