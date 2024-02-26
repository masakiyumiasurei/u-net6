using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using u_net.Public;
using u_net;

namespace MultiRowDesigner
{
    public sealed partial class 入金明細テンプレート : Template
    {
        SqlConnection cn;
        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }
        public 入金明細テンプレート()
        {
            InitializeComponent();

            Connect();
            OriginalClass ofn = new OriginalClass();
            
            ofn.SetComboBox(入金区分コード, 
                "SELECT 入金区分コード as Value , 入金区分名 as Display FROM M入金区分 ORDER BY OrderNumber");
            入金区分コード.DropDownWidth = 300;
                
            ofn.SetComboBox(備考コード,
                "SELECT right(replace(str(内容コード),' ','0'),3) as Value," +
                " right(replace(str(内容コード),' ','0'),3) as Display , 摘要内容 as Display2 " +
                "FROM M摘要 WHERE 摘要使用名='入金' AND 項目名='備考'");
            備考コード.DropDownWidth = 600;

            this.領収証出力コード.DataSource = new KeyValuePair<String, String>[] {
                new KeyValuePair<String, String>("01", "する"),
                new KeyValuePair<String, String>("02", "しない"),
            };
            this.領収証出力コード.DisplayMember = "Value";
            this.領収証出力コード.ValueMember = "Key";
                        
        }
    }
}
