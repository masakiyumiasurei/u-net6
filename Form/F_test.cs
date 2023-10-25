using System;
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




        // 日付選択フォームへの参照を保持するための変数
        private F_カレンダー dateSelectionForm;


        private void 日付選択_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();
            if (dateSelectionForm.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                日付.Text = selectedDate;
            }
        }
    }
}
