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
    public partial class F_製品管理_汎用キー設定 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "製品管理_汎用キー設定";
        private int selected_frame = 0;
        public string strCode;
        public int intEdition;

        public F_製品管理_汎用キー設定()
        {
            this.Text = "製品管理_汎用キー設定";       // ウィンドウタイトルを設定
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

        public string CurrentCode
        {
            get { return strCode; }
        }

        public int CurrentEdition
        {
            get { return intEdition; }
        }

        private void Form_Load(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                control.PreviewKeyDown += OriginalClass.ValidateCheck;
            }

          

            MyApi myapi = new MyApi();
            int xSize, ySize, intpixel, twipperdot;

            //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
            intpixel = myapi.GetLogPixel();
            twipperdot = myapi.GetTwipPerDot(intpixel);

            コード.Text = strCode;
            版数.Text = intEdition.ToString();

            try
            {
                Connect();

                string strSQL = $"SELECT * FROM M製品 WHERE 製品コード = '{CurrentCode}' AND 製品版数 = {CurrentEdition}";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        汎用キー1.Text = reader["汎用キー1"] is DBNull ? null : (string)reader["汎用キー1"];
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{GetType().Name}_LoadData - {ex.GetType().Name}: {ex.Message}");
            }


        }




  
        public void SaveData()
        {
            try
            {
                Connect();

                string strSET = 汎用キー1.Text == null ? "汎用キー1=NULL" : $"汎用キー1='{汎用キー1.Text}'";
                string strSQL = $"UPDATE M製品 SET {strSET} WHERE 製品コード='{CurrentCode}' AND 製品版数={CurrentEdition}";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{GetType().Name}_SaveData - {ex.GetType().Name}: {ex.Message}");
            }

        }



        private void OKボタン_Click(object sender, EventArgs e)
        {
            SaveData();

            F_製品管理? f_製品管理 = Application.OpenForms.OfType<F_製品管理>().FirstOrDefault();

            f_製品管理.UpdateRow();

            Close();
        }

        private void キャンセルボタン_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 汎用キー1_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角２０文字まで入力できます。";
        }

        private void 汎用キー1_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 汎用キー1_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control,40);
        }
    }
}
