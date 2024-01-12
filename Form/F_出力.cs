using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data.OleDb;
using Microsoft.Data.SqlClient;
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
using static u_net.CommonConstants;
using static u_net.Public.FunctionClass;
using System.Data.Common;

namespace u_net
{
    public partial class F_出力 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "出力";
        private int selected_frame = 0;

        public F_出力()
        {
            this.Text = "出力";       // ウィンドウタイトルを設定
            this.MaximizeBox = false;  // 最大化ボタンを無効化
            this.MinimizeBox = false; //最小化ボタンを無効化

            InitializeComponent();
        }

        //SqlConnection cn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter();

        private void Form_Load(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                control.PreviewKeyDown += OriginalClass.ValidateCheck;
            }
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;
                case Keys.Space: //コンボボックスならドロップダウン
                    {
                        Control activeControl = this.ActiveControl;
                        if (activeControl is System.Windows.Forms.ComboBox)
                        {
                            System.Windows.Forms.ComboBox activeComboBox = (System.Windows.Forms.ComboBox)activeControl;
                            activeComboBox.DroppedDown = true;
                        }
                    }
                    break;
            }
        }

        private void ファイル選択ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 実行ボタン_Click(object sender, EventArgs e)
        {

        }

        private void キャンセルボタン_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}
