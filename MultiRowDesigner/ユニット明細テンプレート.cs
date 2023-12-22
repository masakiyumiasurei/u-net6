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
    public sealed partial class ユニット明細テンプレート : Template
    {
        private SqlConnection? cn;
        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }
        public ユニット明細テンプレート()
        {
            InitializeComponent();

            Connect();
            OriginalClass ofn = new OriginalClass();

            ofn.SetComboBox(変更操作コード, "SELECT 変更操作 as Display,削除操作 as Display2,変更コード as Value FROM M変更表示");


        }

    }
}
