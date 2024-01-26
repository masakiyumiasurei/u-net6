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
    public partial class F_不具合調査修理回答書 : Form
    {

        public F_不具合調査修理回答書()
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

        private void F_不具合調査修理回答書_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;
            }
        }

        private void 登録ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 閉じるボタン_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 処置有無コード_Validated(object sender, EventArgs e)
        {

        }

        private void 処置有無コード_Validating(object sender, CancelEventArgs e)
        {

        }

        private void 対応_DoubleClick(object sender, EventArgs e)
        {

        }

        private void 対応_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角２，０００文字まで入力できます。　■別ウィンドウで表示するには入力欄をダブルクリックします。";
        }

        private void 対応_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 対応_TextChanged(object sender, EventArgs e)
        {

        }

        private void 対応_Validating(object sender, CancelEventArgs e)
        {

        }

        private void 結果_DoubleClick(object sender, EventArgs e)
        {

        }

        private void 結果_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角２，０００文字まで入力できます。　■別ウィンドウで表示するには入力欄をダブルクリックします。";
        }

        private void 結果_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 結果_TextChanged(object sender, EventArgs e)
        {

        }

        private void 結果_Validating(object sender, CancelEventArgs e)
        {

        }

        private void 修理費用明細_DoubleClick(object sender, EventArgs e)
        {

        }

        private void 修理費用明細_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角２，０００文字まで入力できます。　■別ウィンドウで表示するには入力欄をダブルクリックします。";
        }

        private void 修理費用明細_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 修理費用明細_TextChanged(object sender, EventArgs e)
        {

        }

        private void 修理費用明細_Validating(object sender, CancelEventArgs e)
        {

        }

        private void 処理日_DoubleClick(object sender, EventArgs e)
        {

        }

        private void 処理日_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void 処理日_TextChanged(object sender, EventArgs e)
        {

        }

        private void 処理日_Validating(object sender, CancelEventArgs e)
        {

        }

        private void 処理日選択ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 処理者コード_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void 処理者コード_Validated(object sender, EventArgs e)
        {

        }

        private void 処理者コード_Validating(object sender, CancelEventArgs e)
        {

        }

        private void 処理工数_TextChanged(object sender, EventArgs e)
        {

        }

        private void 処理工数_Validating(object sender, CancelEventArgs e)
        {

        }

        private void 責任先_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void 責任先_Validating(object sender, CancelEventArgs e)
        {

        }
    }
}
