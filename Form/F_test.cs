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

        public F_test()
        {
            InitializeComponent();
            
        }
        private void button1_Click(object sender, EventArgs e)
        {
            LocalSetting test = new LocalSetting();
            //test.LoadPlace("000", this);
            test.SavePlace("000", this);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            LocalSetting test = new LocalSetting();
            test.LoadPlace("000", this);
        }

        private void テスト_Click(object sender, EventArgs e)
        {
            //MyApi yourClass = new MyApi();
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

            if (!string.IsNullOrEmpty(日付.Text))
            {
                dateSelectionForm.args = 日付.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                日付.Text = selectedDate;
            }
        }





        // 検索フォームへの参照を保持するための変数
        private F_検索 SearchForm;


        private void 顧客選択ボタン_Click(object sender, EventArgs e)
        {
            SearchForm = new F_検索();
            SearchForm.FilterName = "顧客名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                string SelectedCode = SearchForm.SelectedCode;

                顧客コード.Text = SelectedCode;
            }
        }

        private  void button3_Click(object sender, EventArgs e)
        {

            Connect();

            string hoge;

            FunctionClass fn = new FunctionClass();
            fn.DoWait("ボタン3を実行中...");

            int i = 0;
            while (i < 1000000000)
            {
                i++;
            }

            hoge = FunctionClass.GetAddupMonth(cn,DateTime.Today,0);

            textBox1.Text = hoge.ToString();

            fn.WaitForm.Close();

        }

        private void 日付_KeyDown(object sender, KeyEventArgs e)
        {
            FunctionClass fn = new FunctionClass();

            DateTime DateValue;

            DateTime.TryParse(日付.Text, out DateValue);

            if (e.KeyCode == Keys.Add)
            {
                // "+"キーが押された場合

                DateTime newDate = fn.InputDate('+', DateValue);
                日付.Text = newDate.ToString();
                e.SuppressKeyPress = true;

            }
            else if (e.KeyCode == Keys.Subtract)
            {
                // "-"キーが押された場合

                DateTime newDate = fn.InputDate('-', DateValue);
                日付.Text = newDate.ToString();
                e.SuppressKeyPress = true;

            }
        }
    }
}
