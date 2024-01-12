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
using static u_net.CommonConstants;
using static u_net.Public.FunctionClass;
using System.Data.Common;

namespace u_net
{
    public partial class F_入庫完了 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "入庫完了";
        private int selected_frame = 0;

        public F_入庫完了()
        {
            this.Text = "入庫完了";       // ウィンドウタイトルを設定
            this.MaximizeBox = false;  // 最大化ボタンを無効化
            this.MinimizeBox = false; //最小化ボタンを無効化

            InitializeComponent();
        }

        public string SP支払年月入力
        {
            get
            {
                return "SELECT A.CloseMonth as Display,A.CloseMonth as Value FROM (SELECT STR({ fn YEAR(DATEADD(month,-12,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-12,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,-11,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-11,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,-10,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-10,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,-9,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-9,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,-8,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-8,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,-7,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-7,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,-6,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-6,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,-5,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-5,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,-4,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-4,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,-3,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-3,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,-2,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-2,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,-1,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-1,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(GETDATE()) }, 4, 0) + '/' + STR({ fn MONTH(GETDATE()) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,1,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,1,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,2,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,2,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,3,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,3,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,4,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,4,GETDATE())) }, 2, 0) AS CloseMonth " +
                    ") AS A LEFT OUTER JOIN(SELECT STR( YEAR(CloseMonth) ,4 ,0 ) + '/' + STR( MONTH(CloseMonth) ,2 ,0 ) AS CloseMonth " +
                    "FROM T振込繰越残高 GROUP BY CloseMonth) AS B ON A.CloseMonth = B.CloseMonth WHERE B.CloseMonth IS NULL ORDER BY A.CloseMonth";
            }
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

            OriginalClass ofn = new OriginalClass();
            ofn.SetComboBox(完了年月, SP支払年月入力);
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;
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
            }
        }

        private void 実行ボタン_Click(object sender, EventArgs e)
        {

        }

        private void キャンセルボタン_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
