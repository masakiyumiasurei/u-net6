﻿using u_net.Public;

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
            
            F_メイン frm = new F_メイン();
            //親フォームをこのフォームにする
            frm.MdiParent = this;
            //子フォームを表示する
            frm.Show();
        }

        
    }
}
