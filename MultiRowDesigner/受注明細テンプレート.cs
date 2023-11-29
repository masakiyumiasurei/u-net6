using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using u_net;
using u_net.Public;

namespace MultiRowDesigner
{
    public sealed partial class 受注明細テンプレート : Template
    {
        private SqlConnection? cn;
        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }
        public 受注明細テンプレート()
        {
            InitializeComponent();

            Connect();
            OriginalClass ofn = new OriginalClass();

            ofn.SetComboBox(受注区分, "SELECT 受注区分コード AS Value,  受注区分名 AS Display From M受注区分");

            //DataTable dt = new DataTable();

            //dt.Columns.Add("受注区分コード", typeof(byte));
            //dt.Columns.Add("受注区分", typeof(String));

            //dt.Rows.Add(1, CommonConstants.CH_ORDER.ToString());
            //dt.Rows.Add(2, "代替品");
            //dt.Rows.Add(3, "返品");
            //dt.Rows.Add(4, "値引き");
            //dt.Rows.Add(5, "コメント");

            //this.受注区分.DataSource = dt;
        }
    }
}
