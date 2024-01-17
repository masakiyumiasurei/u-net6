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
    public partial class F_請求条件設定 : Form
    {

        public F_請求条件設定()
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
                if (Application.OpenForms["F_ユニット管理"] == null)
                {
                    MessageBox.Show("[ユニット管理]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }


                OriginalClass ofn = new OriginalClass();

                ofn.SetComboBox(更新者名, "SELECT 氏名 as Value , 氏名 as Display FROM M社員 WHERE (退社 IS NULL) AND ([パート] = 0) AND (ふりがな <> N'ん') OR (退社 IS NULL) AND ([パート] IS NULL) AND (ふりがな <> N'ん') ORDER BY ふりがな");

                this.非含有証明書.DataSource = new KeyValuePair<long, String>[] {
                    new KeyValuePair<long, String>(1, "返却済み"),
                    new KeyValuePair<long, String>(2, "未返却"),
                    new KeyValuePair<long, String>(3, "未提出"),
                    new KeyValuePair<long, String>(4, "不明"),
                    new KeyValuePair<long, String>(0, "指定しない"),
                };
                this.非含有証明書.DisplayMember = "Value";
                this.非含有証明書.ValueMember = "Key";

                //開いているフォームのインスタンスを作成する
                F_ユニット管理 frmTarget = Application.OpenForms.OfType<F_ユニット管理>().FirstOrDefault();

                請求締日.Text = frmTarget.str品名;
                型番.Text = frmTarget.str型番;


                switch (frmTarget.lngRoHS対応)
                {
                    case 1:
                        RoHS対応Button1.Checked = true;
                        break;
                    case 2:
                        RoHS対応Button2.Checked = true;
                        break;
                    case 0:
                        RoHS対応Button3.Checked = true;
                        break;

                    default:

                        break;
                }

                非含有証明書.SelectedValue = frmTarget.lng非含有証明書;


                if (frmTarget.dtm更新日開始 != DateTime.MinValue)
                    更新日開始.Text = frmTarget.dtm更新日開始.ToString("yyyy/MM/dd");
                if (frmTarget.dtm更新日終了 != DateTime.MinValue)
                    更新日終了.Text = frmTarget.dtm更新日終了.ToString("yyyy/MM/dd");
                更新者名.Text = frmTarget.str更新者名;


                switch (frmTarget.lng廃止指定)
                {
                    case 1:
                        廃止指定Button1.Checked = true;
                        break;
                    case 2:
                        廃止指定Button2.Checked = true;
                        break;
                    case 0:
                        廃止指定Button3.Checked = true;
                        break;

                    default:

                        break;
                }

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
                F_ユニット管理? frmTarget = Application.OpenForms.OfType<F_ユニット管理>().FirstOrDefault();


                frmTarget.str品名 = Nz(請求締日.Text);
                frmTarget.str型番 = Nz(型番.Text);


                if (RoHS対応Button1.Checked)
                {
                    frmTarget.lngRoHS対応 = 1;
                }
                else if (RoHS対応Button2.Checked)
                {
                    frmTarget.lngRoHS対応 = 2;
                }
                else if (RoHS対応Button3.Checked)
                {
                    frmTarget.lngRoHS対応 = 0;
                }

                frmTarget.lng非含有証明書 = 非含有証明書.SelectedValue != null ? Convert.ToInt64(非含有証明書.SelectedValue) : 0;

                frmTarget.dtm更新日開始 = string.IsNullOrEmpty(更新日開始.Text) ?
                   DateTime.MinValue : DateTime.Parse(更新日開始.Text);

                frmTarget.dtm更新日終了 = string.IsNullOrEmpty(更新日終了.Text) ?
                    DateTime.MinValue : DateTime.Parse(更新日終了.Text);

                frmTarget.str更新者名 = Nz(更新者名.Text);

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

                if (廃止指定Button1.Checked)
                {
                    frmTarget.lng廃止指定 = 1;
                }
                else if (廃止指定Button2.Checked)
                {
                    frmTarget.lng廃止指定 = 2;
                }
                else if (廃止指定Button3.Checked)
                {
                    frmTarget.lng廃止指定 = 0;
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



        private void 更新日開始選択_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(更新日開始.Text))
            {
                dateSelectionForm.args = 更新日開始.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                更新日開始.Text = selectedDate;

                更新日開始.Focus();
            }
        }

        private void 更新日開始_DoubleClick(object sender, EventArgs e)
        {
            更新日開始選択_Click(sender, e);
        }

        private void 更新日開始_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                更新日開始選択_Click(sender, e);
            }
        }




        private void 更新日終了選択_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(更新日終了.Text))
            {
                dateSelectionForm.args = 更新日終了.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                更新日終了.Text = selectedDate;
                更新日終了.Focus();
            }
        }

        private void 更新日終了_DoubleClick(object sender, EventArgs e)
        {
            更新日終了選択_Click(sender, e);
        }

        private void 更新日終了_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                更新日終了選択_Click(sender, e);
            }
        }

        private void 更新日開始_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(更新日開始, 更新日終了, sender as Control);
        }

        private void 更新日終了_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(更新日開始, 更新日終了, sender as Control);
        }

        private void F_ユニット管理_抽出_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;
            }
        }

        private void 請求締日選択ボタン_Click(object sender, EventArgs e)
        {

        }

        private void OKボタン_Click(object sender, EventArgs e)
        {

        }
    }
}
