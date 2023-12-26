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
    public sealed partial class 支払明細テンプレート : Template
    {
      //  private SqlConnection cn;
        public 支払明細テンプレート()
        {
            InitializeComponent();

            //Connect();
            //OriginalClass ofn = new OriginalClass();

            //ofn.SetComboBox(買掛区分, "SELECT 買掛区分 as Display,買掛区分コード as Display2, 買掛明細コード as Display3 , 買掛区分 as Value FROM V買掛区分");
        }

        //public void Connect()
        //{
        //    Connection connectionInfo = new Connection();
        //    string connectionString = connectionInfo.Getconnect();
        //    cn = new SqlConnection(connectionString);
        //    cn.Open();
        //}
    }
}
