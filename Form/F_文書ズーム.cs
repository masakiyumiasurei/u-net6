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

namespace u_net
{
    public partial class F_文書ズーム : Form
    {

        public F_文書ズーム()
        {
            InitializeComponent();
        }

        SqlConnection cn;

        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {

                foreach (Control control in Controls)
                {
                    control.PreviewKeyDown += OriginalClass.ValidateCheck;
                }


                // 対象フォームが読み込まれていないときはすぐに終了する
                if (Application.OpenForms["F_文書"] == null)
                {
                    MessageBox.Show("[F_文書]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }

                //開いているフォームのインスタンスを作成する
                //F_文書 frmTarget = Application.OpenForms.OfType<F_文書>().FirstOrDefault();

                //品名.Text = frmTarget.str品名;

            }
            catch (Exception ex)
            {
                // 例外処理が必要な場合はここで処理を追加
                MessageBox.Show("エラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void キャンセルボタン_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }


        // Nz メソッドの代替
        private T Nz<T>(T value)
        {
            if (value == null)
            {
                return default(T);
            }
            return value;
        }

        private void F_文書ズーム_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void F_文書ズーム_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void F_文書ズーム_Resize(object sender, EventArgs e)
        {

        }

        private void 印刷ボタン_Click(object sender, EventArgs e)
        {

        }

        private void OKボタン_Click(object sender, EventArgs e)
        {

        }

        private void 文字サイズ_Validated(object sender, EventArgs e)
        {

        }

        private void 文字拡大ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 文字縮小ボタン_Click(object sender, EventArgs e)
        {

        }

        private void テキスト_TextChanged(object sender, EventArgs e)
        {

        }

        private void テキスト_Enter(object sender, EventArgs e)
        {

        }

        private void テキスト_Leave(object sender, EventArgs e)
        {

        }

        private void テキスト_KeyDown(object sender, KeyEventArgs e)
        {

        }
    }
}
