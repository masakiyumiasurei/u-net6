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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Microsoft.Identity.Client.NativeInterop;
using GrapeCity.Win.MultiRow;

namespace u_net
{
    public partial class F_部品_資料添付 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "資料添付";
        private int selected_frame = 0;

        public F_部品_資料添付()
        {
            this.Text = "資料添付";       // ウィンドウタイトルを設定
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
            Connect();
            //テスト用　コメントにすること
            //args = "00000123";
            string strSQL = "SELECT * FROM PartAttach WHERE PartCode='" + args + "' ORDER BY OrderNumber";
            VariableSet.SetTable2Details(部品_資料添付1.Detail, strSQL, cn);

            RefreshAttachmentColumn();
        }

        private void RefreshAttachmentColumn()
        {
            foreach (var row in 部品_資料添付1.Detail.Rows)
            {
                // 各行の添付カラムを更新する処理を書く
                if (row.Cells["Data"].Value != DBNull.Value)
                {
                    row.Cells["添付"].Value = 部品_資料添付1.GetIcon((byte[])row.Cells["Data"].Value, row.Cells["DataName"].Value.ToString());
                }
            }
        }

        private void Form_Unload(object sender, FormClosingEventArgs e)
        {

        }
    }
}
