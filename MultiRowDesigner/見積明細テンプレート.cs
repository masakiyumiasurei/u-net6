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
    public sealed partial class 見積明細テンプレート : Template
    {
        private SqlConnection? cn;
        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }
        public 見積明細テンプレート()
        {
            InitializeComponent();

            Connect();
            OriginalClass ofn = new OriginalClass();

            ofn.SetComboBox(受注区分コード, "SELECT 受注区分コード AS Value,  受注区分名 AS Display From M受注区分");
            ofn.SetComboBox(売上区分コード, "SELECT 売上区分コード AS Value,  売上区分名 AS Display From M売上区分");
            ofn.SetComboBox(ラインコード, "SELECT Code AS Value, Name AS Display, Note AS Display2 FROM ManufactureFlow ORDER BY Number");
            ofn.SetComboBox(単位コード, "SELECT 単位コード AS Value,  単位名 AS Display From M単位");

            this.SettingSheet.DataSource = new KeyValuePair<String, String>[] {
                new KeyValuePair<String, String>("01", "付属する"),
                new KeyValuePair<String, String>("02", "付属しない"),
            };
            this.SettingSheet.DisplayMember = "Value";
            this.SettingSheet.ValueMember = "Key";

            this.InspectionReport.DataSource = new KeyValuePair<String, String>[] {
                new KeyValuePair<String, String>("01", "付属する"),
                new KeyValuePair<String, String>("02", "付属しない"),
            };
            this.InspectionReport.DisplayMember = "Value";
            this.InspectionReport.ValueMember = "Key";

            this.Specification.DataSource = new KeyValuePair<String, String>[] {
                new KeyValuePair<String, String>("01", "付属する"),
                new KeyValuePair<String, String>("02", "付属しない"),
            };
            this.Specification.DisplayMember = "Value";
            this.Specification.ValueMember = "Key";

            this.ParameterSheet.DataSource = new KeyValuePair<String, String>[] {
                new KeyValuePair<String, String>("01", "付属する"),
                new KeyValuePair<String, String>("02", "付属しない"),
            };
            this.ParameterSheet.DisplayMember = "Value";
            this.ParameterSheet.ValueMember = "Key";

        }
    }
}
