﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using u_net.Public;
using Microsoft.Data.SqlClient;

namespace u_net
{
    public partial class F_MdiParent : Form
    {

        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;

        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        public F_MdiParent()
        {
            InitializeComponent();
        }


        private void F_MidParent_Load(object sender, EventArgs e)
        {

            F_商品管理 frm = new F_商品管理();
           
            //親フォームをこのフォームにする
            frm.MdiParent = this;
            
            //子フォームを表示する
            frm.Show();
        }
    }
}
