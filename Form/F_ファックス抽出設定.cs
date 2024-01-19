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
    public partial class F_ファックス抽出設定 : Form
    {

        public F_ファックス抽出設定()
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
                if (Application.OpenForms["F_ファックス管理"] == null)
                {
                    MessageBox.Show("[ファックス管理]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }

                //開いているフォームのインスタンスを作成する
                F_ファックス管理 frmTarget = Application.OpenForms.OfType<F_ファックス管理>().FirstOrDefault();

                if (frmTarget.dtm送信日開始 != DateTime.MinValue)
                    送信日開始.Text = frmTarget.dtm送信日開始.ToString("yyyy-MM-dd");

                if (frmTarget.dtm送信日終了 != DateTime.MinValue)
                    送信日終了.Text = frmTarget.dtm送信日終了.ToString("yyyy-MM-dd");

            }
            catch (Exception ex)
            {
                // 例外処理が必要な場合はここで処理を追加
                MessageBox.Show("エラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 抽出ボタン_Click(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();
            try
            {
                
                fn.DoWait("しばらくお待ちください...");

                F_ファックス管理 frmTarget = Application.OpenForms.OfType<F_ファックス管理>().FirstOrDefault();

                送信日開始.Text = 送信日開始.Text.Replace("/", "-");
                送信日終了.Text = 送信日終了.Text.Replace("/", "-");
                frmTarget.dtm送信日開始 = DateTime.TryParse(送信日開始.Text, out var dt) ? dt : default(DateTime);
                frmTarget.dtm送信日終了 = DateTime.TryParse(送信日終了.Text, out var dt1) ? dt1 : default(DateTime);
               

                long cnt = frmTarget.DoUpdate();

                if (cnt == 0)
                {
                    MessageBox.Show("抽出条件に一致するデータはありません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                else if (cnt < 0)
                {
                    MessageBox.Show("エラーが発生したため、抽出できませんでした。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(this.Name + "_抽出ボタン_Click - " + ex.Message);
                MessageBox.Show("エラーが発生しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (fn.WaitForm != null) fn.WaitForm.Close();
                this.Close();
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

        private void F_ファックス抽出設定_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;
            }
        }

        private void 送信日開始選択_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            F_カレンダー form = new F_カレンダー();

            if (!string.IsNullOrEmpty(送信日開始.Text))
            {
                form.args = 送信日開始.Text;
            }

            if (form.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = form.SelectedDate;

                送信日開始.Text = selectedDate;
                送信日開始.Focus();
            }
        }

        private void 送信日終了選択_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            F_カレンダー form = new F_カレンダー();

            if (!string.IsNullOrEmpty(送信日終了.Text))
            {
                form.args = 送信日終了.Text;
            }

            if (form.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = form.SelectedDate;

                送信日終了.Text = selectedDate;
                送信日終了.Focus();
            }
        }

        private void 送信日開始_DoubleClick(object sender, EventArgs e)
        {
            送信日開始選択_Click(sender,e);
        }

        private void 送信日開始_Click(object sender, EventArgs e)
        {
            送信日開始選択_Click(sender, e);
        }

        private void 送信日終了_DoubleClick(object sender, EventArgs e)
        {
            送信日終了選択_Click(sender, e);
        }

        private void 送信日終了_Click(object sender, EventArgs e)
        {
            送信日終了選択_Click(sender, e);
        }
    }
}
