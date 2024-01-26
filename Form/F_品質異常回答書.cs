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
    public partial class F_品質異常回答書 : Form
    {

        public F_品質異常回答書()
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
                //if (Application.OpenForms["F_ユニット管理"] == null)
                //{
                //    MessageBox.Show("[ユニット管理]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    this.Close();
                //    return;
                //}

                //開いているフォームのインスタンスを作成する
                //F_ユニット管理 frmTarget = Application.OpenForms.OfType<F_ユニット管理>().FirstOrDefault();

            }
            catch (Exception ex)
            {
                // 例外処理が必要な場合はここで処理を追加
                MessageBox.Show("エラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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


        private void F_品質異常回答書_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;
            }
        }

        private void 処置日_DoubleClick(object sender, EventArgs e)
        {

        }

        private void 処置日_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■[space]キーでカレンダーを表示できます。";
        }

        private void 処置日_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 処置日_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void 処置日_Validating(object sender, CancelEventArgs e)
        {

        }

        private void 処置日選択ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 登録ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 処置担当者_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角２５文字まで入力できます。　■担当者名を入力します。　■複数入力可能です。";
        }

        private void 処置担当者_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 処置内容_DoubleClick(object sender, EventArgs e)
        {

        }

        private void 処置内容_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角２，０００文字まで入力できます。　■別ウィンドウで表示するには入力欄をダブルクリックします。";
        }

        private void 処置内容_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 処置内容_TextChanged(object sender, EventArgs e)
        {

        }

        private void 処置内容_Validating(object sender, CancelEventArgs e)
        {

        }

        private void 閉じるボタン_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 参照文書コード_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■本回答に関連する是正・予防処置報告書の文書コードを入力します。";
        }

        private void 参照文書コード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 参照文書コード_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void 参照文書コード_TextChanged(object sender, EventArgs e)
        {

        }

        private void 処置方法コード_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■品質異常に対する処置方法を選択します。";
        }

        private void 処置方法コード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

    }
}
