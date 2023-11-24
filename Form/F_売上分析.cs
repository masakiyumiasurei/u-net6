using System;
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
using static u_net.Public.FunctionClass;
using static u_net.CommonConstants;

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

        

        private void 顧客別売上一覧ボタン_Click(object sender, EventArgs e)
        {
            string replacedServerInstanceName = ServerInstanceName.Replace(" ", "_");
            string param = $" -sv:{replacedServerInstanceName} -open:saleslistbycustomer";
            GetShell(param);
        }

        private void 顧客グループ別売上一覧ボタン_Click(object sender, EventArgs e)
        {
            string replacedServerInstanceName = ServerInstanceName.Replace(" ", "_");
            string param = $" -sv:{replacedServerInstanceName} -open:saleslistbyparentcustomer";
            GetShell(param);
        }

        private void 依頼主別売上一覧ボタン_Click(object sender, EventArgs e)
        {
            string replacedServerInstanceName = ServerInstanceName.Replace(" ", "_");
            string param = $" -sv:{replacedServerInstanceName} -open:saleslistbyclient";
            GetShell(param);
        }

        private void 顧客毎依頼主別売上一覧ボタン_Click(object sender, EventArgs e)
        {
            string replacedServerInstanceName = ServerInstanceName.Replace(" ", "_");
            string param = $" -sv:{replacedServerInstanceName} -open:saleslistbyclientascustomer";
            GetShell(param);
        }

        private void 依頼主毎シリーズ別売上一覧ボタン_Click(object sender, EventArgs e)
        {
            string replacedServerInstanceName = ServerInstanceName.Replace(" ", "_");
            string param = $" -sv:{replacedServerInstanceName} -open:saleslistbyseriesasclient";
            GetShell(param);
        }

        private void 顧客グループ別売上明細一覧ボタン_Click(object sender, EventArgs e)
        {
            string replacedServerInstanceName = ServerInstanceName.Replace(" ", "_");
            string param = $" -sv:{replacedServerInstanceName} -open:saleslistbycustomerdetail";
            GetShell(param);
        }

        private void 受注残推移グラフボタン_Click(object sender, EventArgs e)
        {
            string replacedServerInstanceName = ServerInstanceName.Replace(" ", "_");
            string param = $" -sv:{replacedServerInstanceName} -open:salesbackorderchart";
            GetShell(param);
        }




       
        private void 全国地区別売上一覧ボタン_Click(object sender, EventArgs e)
        {
            F_売上一覧_全国地区別 fm = new F_売上一覧_全国地区別();
            fm.ShowDialog();
        }

        private void 売上一覧_売上地区別ボタン_Click(object sender, EventArgs e)
        {
            F_売上一覧_売上地区別 fm = new F_売上一覧_売上地区別();
            fm.ShowDialog();
        }

        private void 区分別年度売上一覧ボタン_Click(object sender, EventArgs e)
        {
            //F_売上一覧_区分別_年度 fm = new F_売上一覧_区分別_年度();
            //fm.ShowDialog();
        }


        private void 売上一覧_区分別ボタン_Click(object sender, EventArgs e)
        {
            //F_売上一覧_区分別 fm = new F_売上一覧_区分別();
            //fm.ShowDialog();
        }

       
        private void 売上明細参照ボタン_Click(object sender, EventArgs e)
        {
            //F_売上明細参照 fm = new F_売上明細参照();
            //fm.ShowDialog();
        }

        

        private void 売上計画ボタン_Click(object sender, EventArgs e)
        {
            //F_売上計画 fm = new F_売上計画();
            //fm.ShowDialog();
        }

        private void 担当者別売上一覧ボタン_Click(object sender, EventArgs e)
        {
            F_売上一覧_担当者別 fm = new F_売上一覧_担当者別();
            fm.ShowDialog();
        }

        
    }

   

        
}
