using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace u_net
{
    public partial class F_実行中 : Form
    {

        public string String1 = "登録中...";
        public F_実行中()
        {
            InitializeComponent();
        }

        private void F_実行中_Load(object sender, EventArgs e)
        {
            this.ControlBox = false;
            this.FormBorderStyle = FormBorderStyle.None;
            CenterFormOnScreen();

            処理内容.Text = String1;
            処理内容.BackColor = Color.Black;
            処理内容.ForeColor = Color.White;
        }


        private void CenterFormOnScreen()
        {
            // フォームが表示されるディスプレイのサイズを取得
            Screen screen = Screen.FromControl(this);
            int screenWidth = screen.WorkingArea.Width;
            int screenHeight = screen.WorkingArea.Height;

            // フォームのサイズを取得
            int formWidth = this.Width;
            int formHeight = this.Height;

            // フォームの位置を設定して画面の中央に表示
            this.Left = (screenWidth - formWidth) / 2;
            this.Top = (screenHeight - formHeight) / 2;
        }


    }
}
