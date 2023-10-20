using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using u_net.Public;
using Microsoft.Data.SqlClient;

namespace u_net
{
    public partial class F_test : Form
    {

        public F_test()
        {
            InitializeComponent();
        }

        private void テスト_Click(object sender, EventArgs e)
        {
            MyApi yourClass = new MyApi();
            //Connection connectionInfo = new Connection();
            //string connectionString = connectionInfo.Getconnect();
            //SqlConnection connection = new SqlConnection(connectionString);
            //connection.Open();

            //FunctionClass functionClass = new FunctionClass();
            //// 採番メソッドを呼び出し
            //string 採番コード = FunctionClass.採番(connection, "A");


        }
    }
}
