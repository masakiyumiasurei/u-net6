using Microsoft.IdentityModel.Tokens;
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
    public partial class F_見積管理_抽出 : Form
    {
        private F_見積管理 frmTarget;
        public object objParent;

        public F_見積管理_抽出()
        {
            InitializeComponent();
        }

        private bool setflg = false;
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
                // 対象フォームが読み込まれていないときはすぐに終了する
                if (Application.OpenForms["F_見積管理"] == null)
                {
                    MessageBox.Show("[見積管理]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }

                OriginalClass ofn = new OriginalClass();

                ofn.SetComboBox(担当者名, "SELECT 社員コード AS Value, 氏名 AS Display FROM M社員 " +
                    "WHERE (退社 IS NULL) AND ([パート] = 0) AND (削除日時 IS NULL) AND (ふりがな <> N'ん') ORDER BY ふりがな");

                //開いているフォームのインスタンスを作成する
                frmTarget = Application.OpenForms.OfType<F_見積管理>().FirstOrDefault();

                if (frmTarget.dtm見積日開始 != DateTime.MinValue)
                {
                    this.見積日開始.Text = frmTarget.dtm見積日開始.ToShortDateString();
                }

                if (frmTarget.dtm見積日終了 != DateTime.MinValue)
                {
                    this.見積日終了.Text = frmTarget.dtm見積日終了.ToShortDateString();
                }

                this.担当者名.Text = frmTarget.str担当者名;
                this.顧客コード.Text = frmTarget.str顧客コード;
                this.顧客名.Text = frmTarget.str顧客名;
                this.件名.Text = frmTarget.str件名;

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
                Application.DoEvents();

                FunctionClass fn = new FunctionClass();
                fn.DoWait("しばらくお待ちください...");

                F_見積管理? frmTarget = Application.OpenForms.OfType<F_見積管理>().FirstOrDefault();

                if (!string.IsNullOrEmpty(見積日開始.Text))
                {
                    frmTarget.dtm見積日開始 = Nz(DateTime.Parse(見積日開始.Text));
                }
                else
                {
                    frmTarget.dtm見積日開始 = DateTime.MinValue;
                }

                if (!string.IsNullOrEmpty(見積日終了.Text))
                {
                    frmTarget.dtm見積日終了 = Nz(DateTime.Parse(見積日終了.Text));
                }
                else
                {
                    frmTarget.dtm見積日終了 = DateTime.MinValue;
                }

                frmTarget.str担当者名 = Nz(担当者名.Text);
                frmTarget.str顧客コード = Nz(顧客コード.Text);
                frmTarget.str顧客名 = Nz(顧客名.Text);
                frmTarget.str件名 = Nz(件名.Text);

                // 承認指定
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

                // 確定指定
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

                // 削除指定
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

                if (!frmTarget.DoUpdate())
                {
                    MessageBox.Show("抽出処理は失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    fn.WaitForm.Close();

                    return;
                }

                if (frmTarget.estEXT)
                {
                    MessageBox.Show("抽出条件に一致するデータはありません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    fn.WaitForm.Close();
                    frmTarget.estEXT = false;

                    return;
                }

                fn.WaitForm.Close();
                this.Close();
            }
            finally
            {

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

        private void 見積日開始_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // 日付選択フォームを作成し表示
            F_カレンダー form = new F_カレンダー();
            if (!string.IsNullOrEmpty(見積日開始.Text))
            {
                form.args = 見積日開始.Text;
            }

            if (form.ShowDialog() == DialogResult.OK)
            {
                // objParent に申請日終了の参照を設定
                sender = this.見積日開始;

                // 日付選択フォームから選択した日付を取得
                string selectedDate = form.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                見積日開始.Text = selectedDate;
            }
        }

        private void 見積日開始_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                // 日付選択フォームを作成し表示
                F_カレンダー form = new F_カレンダー();
                if (!string.IsNullOrEmpty(見積日開始.Text))
                {
                    form.args = 見積日開始.Text;
                }

                if (form.ShowDialog() == DialogResult.OK)
                {
                    // objParent に申請日終了の参照を設定
                    sender = this.見積日開始;

                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    見積日開始.Text = selectedDate;
                }
            }
        }

        private void 見積日開始_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(見積日開始, 見積日終了, sender as Control);
        }

        private void 見積日開始選択ボタン_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            F_カレンダー form = new F_カレンダー();
            if (!string.IsNullOrEmpty(見積日開始.Text))
            {
                form.args = 見積日開始.Text;
            }

            if (form.ShowDialog() == DialogResult.OK)
            {
                // objParent に見積日開始の参照を設定
                sender = this.見積日開始;

                // 日付選択フォームから選択した日付を取得
                string selectedDate = form.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                見積日開始.Text = selectedDate;
            }
        }

        private void 見積日終了_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            // 日付選択フォームを作成し表示
            F_カレンダー form = new F_カレンダー();
            if (!string.IsNullOrEmpty(見積日終了.Text))
            {
                form.args = 見積日終了.Text;
            }

            if (form.ShowDialog() == DialogResult.OK)
            {
                // objParent に申請日終了の参照を設定
                sender = this.見積日終了;

                // 日付選択フォームから選択した日付を取得
                string selectedDate = form.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                見積日終了.Text = selectedDate;
            }
        }

        private void 見積日終了_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                // 日付選択フォームを作成し表示
                F_カレンダー form = new F_カレンダー();
                if (!string.IsNullOrEmpty(見積日終了.Text))
                {
                    form.args = 見積日終了.Text;
                }

                if (form.ShowDialog() == DialogResult.OK)
                {
                    // objParent に申請日終了の参照を設定
                    sender = this.見積日終了;

                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    見積日終了.Text = selectedDate;
                }
            }
        }

        private void 見積日終了_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(見積日開始, 見積日終了, sender as Control);
        }

        private void 見積日終了選択ボタン_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            F_カレンダー form = new F_カレンダー();
            if (!string.IsNullOrEmpty(見積日終了.Text))
            {
                form.args = 見積日終了.Text;
            }

            if (form.ShowDialog() == DialogResult.OK)
            {
                // objParent に申請日終了の参照を設定
                sender = this.見積日終了;

                // 日付選択フォームから選択した日付を取得
                string selectedDate = form.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                見積日終了.Text = selectedDate;
            }
        }

        private void 顧客コード_Validated(object sender, EventArgs e)
        {
            setflg = true;
            if (!string.IsNullOrEmpty(this.顧客コード.Text))
            {
                FunctionClass fn = new FunctionClass();
                Connect();
                object customerNameObj = FunctionClass.GetCustomerName(cn, this.顧客コード.Text);

                if (customerNameObj == DBNull.Value || customerNameObj == null || customerNameObj == "")
                {
                    this.顧客名.Text = null;
                }
                else
                {
                    this.顧客名.Text = (string)fn.Zn(customerNameObj);
                }
            }
        }

        private void 顧客コード選択ボタン_Click(object sender, EventArgs e)
        {
            顧客コード.Focus();
            objParent = this;
            F_検索 form = new F_検索();
            form.FilterName = "顧客名フリガナ";
            if (form.ShowDialog() == DialogResult.OK)
            {
                setflg = true;
                string SelectedCode = form.SelectedCode;
                顧客コード.Text = SelectedCode;
                顧客名.Focus();
            }
        }


        private void 担当者名_Enter(object sender, EventArgs e)
        {
            long lng1;
            lng1 = this.担当者名.Items.Count;
        }

        private void 顧客名_TextChanged(object sender, EventArgs e)
        {
            if (!setflg)
            {
                this.顧客コード.Text = null;
            }
            setflg = false;
        }
    }
}
