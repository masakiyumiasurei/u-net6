using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using u_net.Public;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Drawing.Imaging;
using System.Drawing.Printing;


namespace u_net
{
    public partial class F_カレンダー : Form
    {
        public string SelectedDate { get; private set; }

        public F_カレンダー()
        {

            this.Text = "カレンダー";       // ウィンドウタイトルを設定
            this.MaximizeBox = false;  // 最大化ボタンを無効化
            this.MinimizeBox = false; //最小化ボタンを無効化

            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            // 日付選択フォームから選択した日付を取得
            SelectedDate = this.日付.Text;

            // ダイアログをOKで閉じる
            DialogResult = DialogResult.OK;
            Close();
        }
    }
}
