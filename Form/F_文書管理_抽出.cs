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
    public partial class F_文書管理_抽出 : Form
    {

        public F_文書管理_抽出()
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
                if (Application.OpenForms["F_文書管理"] == null)
                {
                    MessageBox.Show("[F_文書管理]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }

                //開いているフォームのインスタンスを作成する
                F_文書管理 frmTarget = Application.OpenForms.OfType<F_文書管理>().FirstOrDefault();

                文書コード開始.Text = frmTarget.str文書コード開始;
                文書コード開始.Text = frmTarget.str文書コード開始;
                文書名.Text = frmTarget.str文書名;
                件名.Text = frmTarget.str件名;
                発信者名.Text = frmTarget.str発信者名;
                if (frmTarget.dtm確定日開始 != DateTime.MinValue)
                    確定日開始.Text = frmTarget.dtm確定日開始.ToString("yyyy/MM/dd");
                if (frmTarget.dtm確定日終了 != DateTime.MinValue)
                    確定日終了.Text = frmTarget.dtm確定日終了.ToString("yyyy/MM/dd");
                if (frmTarget.dtm期限日開始 != DateTime.MinValue)
                    期限日開始.Text = frmTarget.dtm期限日開始.ToString("yyyy/MM/dd");
                if (frmTarget.dtm期限日終了 != DateTime.MinValue)
                    期限日終了.Text = frmTarget.dtm期限日終了.ToString("yyyy/MM/dd");

                switch (frmTarget.lng確定指定)
                {
                    case 1:
                        確定指定Button1.Checked = true;
                        break;
                    case 2:
                        確定指定Button2.Checked = true;
                        break;
                    case 0:
                        確定指定Button3.Checked = true;
                        break;

                    default:

                        break;
                }

                switch (frmTarget.lng承認指定)
                {
                    case 1:
                        承認指定button1.Checked = true;
                        break;
                    case 2:
                        承認指定button2.Checked = true;
                        break;
                    case 0:
                        承認指定button3.Checked = true;
                        break;

                    default:

                        break;
                }

                switch (frmTarget.lng完了承認指定)
                {
                    case 1:
                        完了承認指定Button1.Checked = true;
                        break;
                    case 2:
                        完了承認指定Button2.Checked = true;
                        break;
                    case 0:
                        完了承認指定Button3.Checked = true;
                        break;

                    default:

                        break;
                }

                switch (frmTarget.lng削除指定)
                {
                    case 1:
                        削除指定Button1.Checked = true;
                        break;
                    case 2:
                        削除指定Button2.Checked = true;
                        break;
                    case 0:
                        削除指定Button3.Checked = true;
                        break;

                    default:

                        break;
                }

            }
            catch (Exception ex)
            {
                // 例外処理が必要な場合はここで処理を追加
                MessageBox.Show("エラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private void 抽出ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                F_文書管理? frmTarget = Application.OpenForms.OfType<F_文書管理>().FirstOrDefault();

                frmTarget.str文書コード開始 = Nz(文書コード開始.Text);
                frmTarget.str文書コード終了 = Nz(文書コード終了.Text);
                frmTarget.str文書名 = Nz(文書名.Text);
                frmTarget.str件名 = Nz(件名.Text);
                frmTarget.str発信者名 = Nz(発信者名.Text);

                frmTarget.dtm確定日開始 = string.IsNullOrEmpty(確定日開始.Text) ?
                   DateTime.MinValue : DateTime.Parse(確定日開始.Text);
                frmTarget.dtm確定日終了 = string.IsNullOrEmpty(確定日終了.Text) ?
                    DateTime.MinValue : DateTime.Parse(確定日終了.Text);
                frmTarget.dtm期限日開始 = string.IsNullOrEmpty(期限日開始.Text) ?
                   DateTime.MinValue : DateTime.Parse(期限日開始.Text);
                frmTarget.dtm期限日終了 = string.IsNullOrEmpty(期限日終了.Text) ?
                    DateTime.MinValue : DateTime.Parse(期限日終了.Text);

                if (確定指定Button1.Checked)
                {
                    frmTarget.lng確定指定 = 1;
                }
                else if (確定指定Button2.Checked)
                {
                    frmTarget.lng確定指定 = 2;
                }
                else if (確定指定Button3.Checked)
                {
                    frmTarget.lng確定指定 = 0;
                }

                if (承認指定button1.Checked)
                {
                    frmTarget.lng承認指定 = 1;
                }
                else if (承認指定button2.Checked)
                {
                    frmTarget.lng承認指定 = 2;
                }
                else if (承認指定button3.Checked)
                {
                    frmTarget.lng承認指定 = 0;
                }

                if (完了承認指定Button1.Checked)
                {
                    frmTarget.lng完了承認指定 = 1;
                }
                else if (完了承認指定Button2.Checked)
                {
                    frmTarget.lng完了承認指定 = 2;
                }
                else if (完了承認指定Button3.Checked)
                {
                    frmTarget.lng完了承認指定 = 0;
                }

                if (削除指定Button1.Checked)
                {
                    frmTarget.lng削除指定 = 1;
                }
                else if (削除指定Button2.Checked)
                {
                    frmTarget.lng削除指定 = 2;
                }
                else if (削除指定Button3.Checked)
                {
                    frmTarget.lng削除指定 = 0;
                }

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
                //this.Painting = true;
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

        private F_カレンダー dateSelectionForm;

        private void 確定日開始選択_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(確定日開始.Text))
            {
                dateSelectionForm.args = 確定日開始.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                確定日開始.Text = selectedDate;
                確定日開始.Focus();
            }
        }

        private void 確定日開始_DoubleClick(object sender, EventArgs e)
        {
            確定日開始選択_Click(sender, e);
        }

        private void 確定日開始_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                確定日開始選択_Click(sender, e);
            }
        }

        private void 確定日開始_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(確定日開始, 確定日終了, sender as Control);
        }

        private void 確定日終了選択_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(確定日終了.Text))
            {
                dateSelectionForm.args = 確定日終了.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                確定日終了.Text = selectedDate;
                確定日終了.Focus();
            }
        }

        private void 確定日終了_DoubleClick(object sender, EventArgs e)
        {
            確定日終了選択_Click(sender, e);
        }

        private void 確定日終了_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                確定日終了選択_Click(sender, e);
            }
        }

        private void 確定日終了_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(確定日開始, 確定日終了, sender as Control);
        }

        private void 文書コード開始_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void 文書コード終了_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void 期限日開始選択ボタン_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(期限日開始.Text))
            {
                dateSelectionForm.args = 期限日開始.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                期限日開始.Text = selectedDate;
                期限日開始.Focus();
            }
        }

        private void 期限日開始_DoubleClick(object sender, EventArgs e)
        {
            期限日開始選択ボタン_Click(sender, e);
        }

        private void 期限日開始_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                期限日開始選択ボタン_Click(sender, e);
            }
        }

        private void 期限日開始_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(期限日開始, 期限日終了, sender as Control);
        }

        private void 期限日終了選択ボタン_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(期限日終了.Text))
            {
                dateSelectionForm.args = 期限日終了.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                期限日終了.Text = selectedDate;
                期限日終了.Focus();
            }
        }

        private void 期限日終了_DoubleClick(object sender, EventArgs e)
        {
            期限日終了選択ボタン_Click(sender, e);
        }

        private void 期限日終了_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(期限日開始, 期限日終了, sender as Control);
        }

        private void 期限日終了_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                期限日終了選択ボタン_Click(sender, e);
            }
        }

        private void F_文書管理_抽出_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;
            }
        }
    }
}
