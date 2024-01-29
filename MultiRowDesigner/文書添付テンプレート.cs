using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using System.Data.SqlClient;

namespace MultiRowDesigner
{
    public sealed partial class 文書添付テンプレート : Template
    {
    
        public 文書添付テンプレート()
        {
            InitializeComponent();

            this.文書形態.DataSource = new KeyValuePair<String, String>[] {
                new KeyValuePair<String, String>("電子                  ", "電子                  "),
                new KeyValuePair<String, String>("紙                   ", "紙                   "),


            };
            this.文書形態.DisplayMember = "Value";
            this.文書形態.ValueMember = "Key";

        }
    }
}
