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
    public partial class F_是正予防処置回答 : Form
    {

        public F_是正予防処置回答()
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

        private void F_是正予防処置回答_KeyDown(object sender, KeyEventArgs e)
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

        private void 回答者コード_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void 回答者コード_Validated(object sender, EventArgs e)
        {

        }

        private void 回答者コード_Validating(object sender, CancelEventArgs e)
        {

        }

        private void 回答者コード_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■回答者を選択します。";
        }

        private void 回答者コード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 状況_DoubleClick(object sender, EventArgs e)
        {

        }

        private void 状況_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角2,000文字まで入力できます。　■入力欄をダブルクリックすると表示される別ウィンドウ上からも編集できます。";
        }

        private void 状況_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 状況_TextChanged(object sender, EventArgs e)
        {

        }

        private void 状況_Validating(object sender, CancelEventArgs e)
        {

        }

        private void 状況_Validated(object sender, EventArgs e)
        {

        }

        private void 原因_DoubleClick(object sender, EventArgs e)
        {

        }

        private void 原因_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角2,000文字まで入力できます。　■入力欄をダブルクリックすると表示される別ウィンドウ上からも編集できます。";
        }

        private void 原因_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 原因_TextChanged(object sender, EventArgs e)
        {

        }

        private void 原因_Validating(object sender, CancelEventArgs e)
        {

        }

        private void 原因_Validated(object sender, EventArgs e)
        {

        }

        private void 影響範囲_DoubleClick(object sender, EventArgs e)
        {

        }

        private void 影響範囲_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角2,000文字まで入力できます。　■入力欄をダブルクリックすると表示される別ウィンドウ上からも編集できます。";
        }

        private void 影響範囲_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 影響範囲_TextChanged(object sender, EventArgs e)
        {

        }

        private void 影響範囲_Validated(object sender, EventArgs e)
        {

        }

        private void 影響範囲_Validating(object sender, CancelEventArgs e)
        {

        }

        private void 是正処置_DoubleClick(object sender, EventArgs e)
        {

        }

        private void 是正処置_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角2,000文字まで入力できます。　■入力欄をダブルクリックすると表示される別ウィンドウ上からも編集できます。";
        }

        private void 是正処置_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 是正処置_TextChanged(object sender, EventArgs e)
        {

        }

        private void 是正処置_Validated(object sender, EventArgs e)
        {

        }

        private void 是正処置_Validating(object sender, CancelEventArgs e)
        {

        }

        private void 予防処置_DoubleClick(object sender, EventArgs e)
        {

        }

        private void 予防処置_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角2,000文字まで入力できます。　■入力欄をダブルクリックすると表示される別ウィンドウ上からも編集できます。";
        }

        private void 予防処置_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 予防処置_TextChanged(object sender, EventArgs e)
        {

        }

        private void 予防処置_Validated(object sender, EventArgs e)
        {

        }

        private void 予防処置_Validating(object sender, CancelEventArgs e)
        {

        }
    }
}
