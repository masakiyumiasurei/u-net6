using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using u_net.Public;


namespace u_net
{
    public partial class F_検索コード : Form
    {
        object Obj;
        public string str検索コード;
        public string formName;
        public string _callerFormName;
        public string _検索コード;

        //Form frmTarget;
        public F_検索コード(object o, string 検索コード)　　//(string callerFormName, string 検索コード)
        {
            //_callerFormName = callerFormName;
            _検索コード = 検索コード;
            Obj = o;
            InitializeComponent();
        }
        private void Form_Load(object sender, EventArgs e)
        {
            this.検索コード.Text = _検索コード;

        }

        private void 検索ボタン_Click(object sender, EventArgs e)
        {
            
            MidForm parentform = (MidForm)Obj;
            parentform.SearchCode(this.検索コード.Text);
        }

        private void 検索コード_KeyDown(object sender, KeyEventArgs e)
        {
            // 入力された値がエラー値の場合、Text プロパティが設定できなくなるときの対処
            string strCode = "";

            if (e.KeyCode == Keys.Return)
            {
                strCode = this.ActiveControl.Text;
                if (string.IsNullOrEmpty(strCode))
                    return;

                strCode = FunctionClass.FormatCode(OriginalClass.Nz(this.str検索コード, ""), strCode);

                if (strCode != OriginalClass.Nz(this.ActiveControl.Text, ""))
                {
                    this.ActiveControl.Text = strCode;
                }
            }
        }
        private void 検索コード_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel1.Text = "■検索したいコードを入力してください。　■11文字まで入力できます。";
        }

        private void キャンセルボタン_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 検索コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)FunctionClass.ChangeBig((int)e.KeyChar);
        }
    }
}
