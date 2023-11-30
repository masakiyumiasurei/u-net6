﻿using System;
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
            ofn.SetComboBox(売上区分, "SELECT 売上区分コード AS Value,  売上区分名 AS Display From M売上区分");
            ofn.SetComboBox(処理区分, "SELECT Code AS Value, Name AS Display, Note FROM ManufactureFlow ORDER BY Number");
            ofn.SetComboBox(単位, "SELECT 単位コード AS Value,  単位名 AS Display From M単位");

        }
    }
}
