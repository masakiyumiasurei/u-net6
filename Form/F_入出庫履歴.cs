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
using Microsoft.Data.SqlClient;
using u_net.Public;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace u_net
{
    public partial class F_入出庫履歴 : MidForm
    {
        

        int intWindowHeight = 0;
        int intWindowWidth = 0;

        public string args = "";

        private Control? previousControl;
        private SqlConnection? cn;
        public F_入出庫履歴()
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

      
        private void Form_Load(object sender, EventArgs e)
        {

            System.Type dgvtype = typeof(DataGridView);
            System.Reflection.PropertyInfo dgvPropertyInfo = dgvtype.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            dgvPropertyInfo.SetValue(dataGridView1, true, null);

            string LoginUserCode = CommonConstants.LoginUserCode;
            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(LoginUserCode, this);

            MyApi myapi = new MyApi();
            int xSize, ySize, intpixel, twipperdot;

            //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
            intpixel = myapi.GetLogPixel();
            twipperdot = myapi.GetTwipPerDot(intpixel);

            intWindowHeight = this.Height;
            intWindowWidth = this.Width;


            myapi.GetFullScreen(out xSize, out ySize);

            int x = 10, y = 10;

            this.Size = new Size(this.Width, ySize * myapi.GetTwipPerDot(intpixel) - 1200);
            //accessのmovesizeメソッドの引数の座標単位はtwipなので以下で

            this.Size = new Size(this.Width, ySize - 1200 / twipperdot);

            this.StartPosition = FormStartPosition.Manual; // 手動で位置を指定
            int screenWidth = Screen.PrimaryScreen.Bounds.Width; // プライマリスクリーンの幅
            x = (screenWidth - this.Width) / 2;
            this.Location = new Point(x, y);


            OriginalClass ofn = new OriginalClass();
            ofn.SetComboBox(部品コード, "SELECT 部品コード as Display,品名 as Display2,型番 as Display3,部品コード as Value FROM M部品 ORDER BY 品名, 型番");
            部品コード.DrawMode = DrawMode.OwnerDrawFixed;

            部品コード.DropDownWidth = 700;

            if (!string.IsNullOrEmpty(args))
            {
                部品コード.SelectedValue = args;
                部品コード_SelectedIndexChanged(sender, e);

            }
            else
            {
                部品コード.SelectedIndex = -1;
            }


        }

        private void Form_Resize(object sender, EventArgs e)
        {
            try
            {
                //if (this.Height > 800)
                //{
                //    dataGridView1.Height = dataGridView1.Height + (this.Height - intWindowHeight);
                //    intWindowHeight = this.Height;  // 高さ保存

                //    dataGridView1.Width = dataGridView1.Width + (this.Width - intWindowWidth);
                //    intWindowWidth = this.Width;    // 幅保存
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.Name + "_Form_Resize - " + ex.Message);
            }
        }


   
        private void コマンド終了_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 部品コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 100, 300,300 }, new string[] { "Display", "Display2","Display3" });
            部品コード.Invalidate();
            部品コード.DroppedDown = true;
        }



        private void 部品コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                ComboBox comboBox = sender as ComboBox;
                if (comboBox != null)
                {
                    string strCode = comboBox.Text.Trim();
                    if (!string.IsNullOrEmpty(strCode))
                    {
                        strCode = strCode.PadLeft(8, '0');
                        if (strCode != comboBox.Text)
                        {
                            comboBox.Text = strCode;
                            部品コード_SelectedIndexChanged(sender, e);
                        }
                    }
                }
            }
        }

        private void 部品コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            品名.Text = ((DataRowView)部品コード.SelectedItem)?.Row.Field<String>("Display2")?.ToString();
            型番.Text = ((DataRowView)部品コード.SelectedItem)?.Row.Field<String>("Display3")?.ToString();
        }

        private void 部品コード_TextChanged(object sender, EventArgs e)
        {
            if (部品コード.SelectedValue == null)
            {
                品名.Text = null;
                型番.Text = null;
            }
        }

        private void F_入出庫履歴_FormClosing(object sender, FormClosingEventArgs e)
        {
            string LoginUserCode = CommonConstants.LoginUserCode;
            LocalSetting test = new LocalSetting();
            test.SavePlace(LoginUserCode, this);
        }
    }
}