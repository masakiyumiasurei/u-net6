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
    public partial class F_購買申請管理_抽出 : Form
    {
        private F_購買申請管理 frmTarget;

        public F_購買申請管理_抽出()
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

                // 対象フォームが読み込まれていないときはすぐに終了する
                if (Application.OpenForms["F_購買申請管理"] == null)
                {
                    MessageBox.Show("[購買申請管理]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }

                //開いているフォームのインスタンスを作成する
                F_購買申請管理 frmTarget = Application.OpenForms.OfType<F_購買申請管理>().FirstOrDefault();


                if (frmTarget.dtm申請日開始 != DateTime.MinValue)
                    this.申請日開始.Text = frmTarget.dtm申請日開始.ToString("yyyy/MM/dd");
                if (frmTarget.dtm申請日終了 != DateTime.MinValue)
                    this.申請日終了.Text = frmTarget.dtm申請日終了.ToString("yyyy/MM/dd");
                if (frmTarget.dtm購買納期開始 != DateTime.MinValue)
                    this.購買納期開始.Text = frmTarget.dtm購買納期開始.ToString("yyyy/MM/dd");
                if (frmTarget.dtm購買納期終了 != DateTime.MinValue)
                    this.購買納期終了.Text = frmTarget.dtm購買納期終了.ToString("yyyy/MM/dd");
                if (frmTarget.dtm出荷予定日開始 != DateTime.MinValue)
                    this.出荷予定日開始.Text = frmTarget.dtm出荷予定日開始.ToString("yyyy/MM/dd");
                if (frmTarget.dtm出荷予定日終了 != DateTime.MinValue)
                    this.出荷予定日終了.Text = frmTarget.dtm出荷予定日終了.ToString("yyyy/MM/dd");

                this.基本型式名.Text = Nz(frmTarget.str基本型式名);
                this.シリーズ名.Text = Nz(frmTarget.strシリーズ名);

                // コンボボックスの設定
                OriginalClass ofn = new OriginalClass();
                ofn.SetComboBox(申請者コード, "SELECT 氏名 as Display, 社員コード as Display2, 社員コード as Value FROM M社員 WHERE (ふりがな <> N'ん') ORDER BY ふりがな");
                if (string.IsNullOrEmpty(frmTarget.str申請者コード))
                {
                    申請者コード.SelectedIndex = -1;
                }
                else
                {
                    申請者コード.SelectedValue = frmTarget.str申請者コード;
                }

                // 承認指定
                if (frmTarget.lng承認指定 == 1)
                {
                    this.承認指定Button1.Checked = true;
                }
                else if (frmTarget.lng承認指定 == 2)
                {
                    this.承認指定Button2.Checked = true;
                }
                else if (frmTarget.lng承認指定 == 0)
                {
                    this.承認指定Button3.Checked = true;
                }

                // 終了指定
                if (frmTarget.lng終了指定 == 1)
                {
                    this.終了指定Button1.Checked = true;
                }
                else if (frmTarget.lng終了指定 == 2)
                {
                    this.終了指定Button2.Checked = true;
                }
                else if (frmTarget.lng終了指定 == 0)
                {
                    this.終了指定Button3.Checked = true;
                }

                // 完了指定
                if (frmTarget.lng完了指定 == 1)
                {
                    this.完了指定Button1.Checked = true;
                }
                else if (frmTarget.lng完了指定 == 2)
                {
                    this.完了指定Button2.Checked = true;
                }
                else if (frmTarget.lng完了指定 == 0)
                {
                    this.完了指定Button3.Checked = true;
                }

                // 削除指定
                if (frmTarget.lng削除指定 == 1)
                {
                    this.削除指定Button1.Checked = true;
                }
                else if (frmTarget.lng削除指定 == 2)
                {
                    this.削除指定Button2.Checked = true;
                }
                else if (frmTarget.lng削除指定 == 0)
                {
                    this.削除指定Button3.Checked = true;
                }
            }
            catch (Exception ex)
            {
                // 例外処理が必要な場合はここで処理を追加
                MessageBox.Show("_Form_Load - " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 抽出ボタン_Click(object sender, EventArgs e)
        {
            try
            {

                Application.DoEvents();

                FunctionClass fn = new FunctionClass();
                fn.DoWait("しばらくお待ちください...");

                F_購買申請管理? frmTarget = Application.OpenForms.OfType<F_購買申請管理>().FirstOrDefault();

                if (!string.IsNullOrEmpty(申請日開始.Text))
                    frmTarget.dtm申請日開始 = Nz(DateTime.Parse(申請日開始.Text));

                if (!string.IsNullOrEmpty(申請日終了.Text))
                    frmTarget.dtm申請日終了 = Nz(DateTime.Parse(申請日終了.Text));

                if (!string.IsNullOrEmpty(購買納期開始.Text))
                    frmTarget.dtm購買納期開始 = Nz(DateTime.Parse(購買納期開始.Text));

                if (!string.IsNullOrEmpty(購買納期終了.Text))
                    frmTarget.dtm購買納期終了 = Nz(DateTime.Parse(購買納期終了.Text));

                if (!string.IsNullOrEmpty(出荷予定日開始.Text))
                    frmTarget.dtm出荷予定日開始 = Nz(DateTime.Parse(出荷予定日開始.Text));

                if (!string.IsNullOrEmpty(出荷予定日終了.Text))
                    frmTarget.dtm出荷予定日終了 = Nz(DateTime.Parse(出荷予定日終了.Text));

                frmTarget.str基本型式名 = Nz(基本型式名.Text);
                frmTarget.strシリーズ名 = Nz(シリーズ名.Text);
                frmTarget.str申請者コード = Nz(申請者コード.Text);
                if (申請者コード.SelectedIndex != -1)
                {
                    frmTarget.str申請者コード = 申請者コード.SelectedValue.ToString();
                    frmTarget.str申請者名 = Nz(申請者コード.Text);
                }


                // 承認指定
                if (承認指定Button1.Checked)
                {
                    frmTarget.lng承認指定 = 1;
                }
                else if (承認指定Button2.Checked)
                {
                    frmTarget.lng承認指定 = 2;
                }
                else if (承認指定Button3.Checked)
                {
                    frmTarget.lng承認指定 = 0;
                }

                // 終了指定
                if (終了指定Button1.Checked)
                {
                    frmTarget.lng終了指定 = 1;
                }
                else if (終了指定Button2.Checked)
                {
                    frmTarget.lng終了指定 = 2;
                }
                else if (終了指定Button3.Checked)
                {
                    frmTarget.lng終了指定 = 0;
                }

                // 完了指定
                if (完了指定Button1.Checked)
                {
                    frmTarget.lng完了指定 = 1;
                }
                else if (完了指定Button2.Checked)
                {
                    frmTarget.lng完了指定 = 2;
                }
                else if (完了指定Button3.Checked)
                {
                    frmTarget.lng完了指定 = 0;
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
                    return;
                }

                if (frmTarget.appEXT)
                {
                    MessageBox.Show("抽出条件に一致するデータはありません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //this.Close();
                    fn.WaitForm.Close();
                    return;
                }

                fn.WaitForm.Close();

                this.Close();
            }
            catch
            {

            }
        }

        private void キャンセルボタン_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }



        private F_検索 SearchForm;

        private void 購買納期開始_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                F_カレンダー form = new F_カレンダー();

                if (!string.IsNullOrEmpty(購買納期開始.Text))
                {
                    form.args = 購買納期開始.Text;
                }

                //// カレンダーフォームを作成して表示
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    購買納期開始.Text = selectedDate;
                    購買納期開始.Focus();
                }
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 購買納期開始_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                F_カレンダー form = new F_カレンダー();

                if (!string.IsNullOrEmpty(購買納期開始.Text))
                {
                    form.args = 購買納期開始.Text;
                }

                //// カレンダーフォームを作成して表示
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    購買納期開始.Text = selectedDate;
                    購買納期開始.Focus();
                }
            }
        }

        private void 購買納期開始_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(購買納期開始, 購買納期終了, sender as Control);
        }

        private void 購買納期開始選択_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                F_カレンダー form = new F_カレンダー();

                if (!string.IsNullOrEmpty(購買納期開始.Text))
                {
                    form.args = 購買納期開始.Text;
                }

                //// カレンダーフォームを作成して表示
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    購買納期開始.Text = selectedDate;
                    購買納期開始.Focus();
                }
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 購買納期終了_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                F_カレンダー form = new F_カレンダー();

                if (!string.IsNullOrEmpty(購買納期終了.Text))
                {
                    form.args = 購買納期終了.Text;
                }

                //// カレンダーフォームを作成して表示
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    購買納期終了.Text = selectedDate;
                    購買納期終了.Focus();
                }
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 購買納期終了_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                F_カレンダー form = new F_カレンダー();

                if (!string.IsNullOrEmpty(購買納期終了.Text))
                {
                    form.args = 購買納期終了.Text;
                }

                //// カレンダーフォームを作成して表示
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    購買納期終了.Text = selectedDate;
                    購買納期終了.Focus();
                }
            }
        }

        private void 購買納期終了_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(購買納期開始, 購買納期終了, sender as Control);
        }

        private void 購買納期終了選択_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                F_カレンダー form = new F_カレンダー();

                if (!string.IsNullOrEmpty(購買納期終了.Text))
                {
                    form.args = 購買納期終了.Text;
                }

                //// カレンダーフォームを作成して表示
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    購買納期終了.Text = selectedDate;
                    購買納期終了.Focus();
                }
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 出荷予定日開始_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                F_カレンダー form = new F_カレンダー();

                if (!string.IsNullOrEmpty(出荷予定日開始.Text))
                {
                    form.args = 出荷予定日開始.Text;
                }

                //// カレンダーフォームを作成して表示
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    出荷予定日開始.Text = selectedDate;
                    出荷予定日開始.Focus();
                }
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 出荷予定日開始_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                F_カレンダー form = new F_カレンダー();

                if (!string.IsNullOrEmpty(出荷予定日開始.Text))
                {
                    form.args = 出荷予定日開始.Text;
                }

                //// カレンダーフォームを作成して表示
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    出荷予定日開始.Text = selectedDate;
                    出荷予定日開始.Focus();
                }
            }
        }

        private void 出荷予定日開始_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(出荷予定日開始, 出荷予定日終了, sender as Control);
        }

        private void 出荷予定日開始選択_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                F_カレンダー form = new F_カレンダー();

                if (!string.IsNullOrEmpty(出荷予定日開始.Text))
                {
                    form.args = 出荷予定日開始.Text;
                }

                //// カレンダーフォームを作成して表示
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    出荷予定日開始.Text = selectedDate;
                    出荷予定日開始.Focus();
                }
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 出荷予定日終了_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                F_カレンダー form = new F_カレンダー();

                if (!string.IsNullOrEmpty(出荷予定日終了.Text))
                {
                    form.args = 出荷予定日終了.Text;
                }

                //// カレンダーフォームを作成して表示
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    出荷予定日終了.Text = selectedDate;
                    出荷予定日終了.Focus();
                }
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 出荷予定日終了_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                F_カレンダー form = new F_カレンダー();

                if (!string.IsNullOrEmpty(出荷予定日終了.Text))
                {
                    form.args = 出荷予定日終了.Text;
                }

                //// カレンダーフォームを作成して表示
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    出荷予定日終了.Text = selectedDate;
                    出荷予定日終了.Focus();
                }
            }
        }

        private void 出荷予定日終了_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(出荷予定日開始, 出荷予定日終了, sender as Control);
        }

        private void 出荷予定日終了選択_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                F_カレンダー form = new F_カレンダー();

                if (!string.IsNullOrEmpty(出荷予定日終了.Text))
                {
                    form.args = 出荷予定日終了.Text;
                }

                //// カレンダーフォームを作成して表示
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    出荷予定日終了.Text = selectedDate;
                    出荷予定日終了.Focus();
                }
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 申請者コード_Enter(object sender, EventArgs e)
        {
            try
            {
                // 申請者コードのリストのアイテム数を取得
                int listCount = this.申請者コード.Items.Count;

                // 取得したアイテム数を使って必要な処理を実行
                // 例: メッセージボックスに表示するなど
                Console.WriteLine($"申請者コードのリストアイテム数: {listCount}");
            }
            catch (Exception ex)
            {
                // エラー処理
                Console.WriteLine("エラーが発生しました: " + ex.Message);
            }
        }

        private void 申請日開始_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                F_カレンダー form = new F_カレンダー();

                if (!string.IsNullOrEmpty(申請日開始.Text))
                {
                    form.args = 申請日開始.Text;
                }

                //// カレンダーフォームを作成して表示

                if (form.ShowDialog() == DialogResult.OK)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    申請日開始.Text = selectedDate;
                    申請日開始.Focus();
                }
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 申請日開始_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                F_カレンダー form = new F_カレンダー();

                if (!string.IsNullOrEmpty(申請日開始.Text))
                {
                    form.args = 申請日開始.Text;
                }

                //// カレンダーフォームを作成して表示

                if (form.ShowDialog() == DialogResult.OK)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    申請日開始.Text = selectedDate;
                    申請日開始.Focus();
                }
            }
        }

        private void 申請日開始_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(申請日開始, 申請日終了, sender as Control);
        }

        private void 申請日開始選択_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                F_カレンダー form = new F_カレンダー();

                if (!string.IsNullOrEmpty(申請日開始.Text))
                {
                    form.args = 申請日開始.Text;
                }

                //// カレンダーフォームを作成して表示

                if (form.ShowDialog() == DialogResult.OK)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    申請日開始.Text = selectedDate;
                    申請日開始.Focus();
                }
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 申請日終了_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            try
            {
                F_カレンダー form = new F_カレンダー();

                if (!string.IsNullOrEmpty(申請日終了.Text))
                {
                    form.args = 申請日終了.Text;
                }

                //// カレンダーフォームを作成して表示
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    申請日終了.Text = selectedDate;
                    申請日終了.Focus();
                }
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 申請日終了_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                F_カレンダー form = new F_カレンダー();

                if (!string.IsNullOrEmpty(申請日終了.Text))
                {
                    form.args = 申請日終了.Text;
                }

                //// カレンダーフォームを作成して表示
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    申請日終了.Text = selectedDate;
                    申請日終了.Focus();
                }
            }
        }

        private void 申請日終了_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(申請日開始, 申請日終了, sender as Control);
        }

        private void 申請日終了選択_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                F_カレンダー form = new F_カレンダー();

                if (!string.IsNullOrEmpty(申請日終了.Text))
                {
                    form.args = 申請日終了.Text;
                }

                //// カレンダーフォームを作成して表示
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    申請日終了.Text = selectedDate;
                    申請日終了.Focus();
                }
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 申請者コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 200, 0 }, new string[] { "Display", "Display2" });
            申請者コード.Invalidate();
            申請者コード.DroppedDown = true;
        }

        private void F_購買申請管理_抽出_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;
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
    }
}
