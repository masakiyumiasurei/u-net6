using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using u_net.Public;
using static u_net.Public.FunctionClass;
using static u_net.CommonConstants;
using System.Runtime.Intrinsics.X86;

namespace u_net
{
    public partial class F_スタートアップ : Form
    {
        SqlConnection cn;
        public F_スタートアップ()
        {
            InitializeComponent();
        }
        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }
        private void F_スタートアップ_Load(object sender, EventArgs e)
        {
            Connect();
            //アプリケーションの最終リリースバージョンを設定する
            strAppLastVer = GetLastVersion(cn);



            F_メイン fm = new F_メイン();
            fm.Show();

            this.Close();

        }
    }
}
