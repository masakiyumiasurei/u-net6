﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using u_net.Public;

namespace u_net
{
    public partial class F_売上分析 : Form
    {
        public F_売上分析()
        {
            InitializeComponent();
        }

        private SqlConnection cn;



        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        private void Form_Load(object sender, EventArgs e)
        {

            string LoginUserCode = "000";//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(LoginUserCode, this);

           
        }

      
        private void キャンセルボタン_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private void F_売上分析_FormClosing(object sender, FormClosingEventArgs e)
        {
            string LoginUserCode = "000";//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            LocalSetting test = new LocalSetting();
            test.SavePlace(LoginUserCode, this);
        }

    


        private void 閉じるボタン_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 担当者別売上一覧ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 顧客別売上一覧ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 顧客グループ別売上一覧ボタン_Click(object sender, EventArgs e)
        {

        }
        private void 売上一覧_売上地区別ボタン_Click(object sender, EventArgs e)
        {

        }
        private void 全国地区別売上一覧ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 依頼主別売上一覧ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 顧客毎依頼主別売上一覧ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 区分別年度売上一覧ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 売上明細参照ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 依頼主毎シリーズ別売上一覧ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 顧客グループ別売上明細一覧ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 受注残推移グラフボタン_Click(object sender, EventArgs e)
        {

        }

        private void 売上計画ボタン_Click(object sender, EventArgs e)
        {

        }
    }

   

        
}
