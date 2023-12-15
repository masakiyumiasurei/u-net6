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

                if (frmTarget.dtm申請日開始.ToString() != "")
                    this.申請日開始.Text = this.申請日開始.Text = frmTarget.dtm申請日開始.ToString();
                if (frmTarget.dtm申請日終了.ToString() != "")
                    this.申請日終了.Text = this.申請日終了.Text = frmTarget.dtm申請日終了.ToString();
                if (frmTarget.dtm購買納期開始.ToString() != "")
                    this.購買納期開始.Text = this.購買納期開始.Text = frmTarget.dtm購買納期開始.ToString();
                if (frmTarget.dtm購買納期終了.ToString() != "")
                    this.購買納期終了.Text = this.購買納期終了.Text = frmTarget.dtm購買納期終了.ToString();
                if (frmTarget.dtm出荷予定日開始.ToString() != "")
                    this.出荷予定日開始.Text = this.出荷予定日開始.Text = frmTarget.dtm出荷予定日開始.ToString();
                if (frmTarget.dtm出荷予定日終了.ToString() != "")
                    this.出荷予定日終了.Text = this.出荷予定日終了.Text = frmTarget.dtm出荷予定日終了.ToString();
                //this.基本型式名.Text = FunctionClass.Zn(frmTarget.str基本型式名);
                //this.シリーズ名.Text = FunctionClass.Zn(frmTarget.strシリーズ名);
                //this.申請者コード.Text = FunctionClass.Zn(frmTarget.str申請者コード);
                //this.承認指定.Text = FunctionClass.Zn(frmTarget.lng承認指定);
                //this.終了指定.Text = FunctionClass.Zn(frmTarget.lng終了指定);
                //this.完了指定.Text = FunctionClass.Zn(frmTarget.lng完了指定);
                //this.削除指定.Text = FunctionClass.Zn(frmTarget.lng削除指定);

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
                //Application.DoEvents(); // Mimic On Error Resume Next

                Application.DoEvents();

                FunctionClass fn = new FunctionClass();
                fn.DoWait("しばらくお待ちください...");

                ////
                frmTarget = new F_購買申請管理();

                frmTarget.dtm申請日開始 = DateTime.Parse(Nz(申請日開始.Text));
                frmTarget.dtm申請日終了 = DateTime.Parse(Nz(申請日終了.Text));
                frmTarget.dtm購買納期開始 = DateTime.Parse(Nz(購買納期開始.Text));
                frmTarget.dtm購買納期終了 = DateTime.Parse(Nz(購買納期終了.Text));
                frmTarget.dtm出荷予定日開始 = DateTime.Parse(Nz(出荷予定日開始.Text));
                frmTarget.dtm出荷予定日終了 = DateTime.Parse(Nz(出荷予定日終了.Text));
                frmTarget.str基本型式名 = Nz(基本型式名.Text);
                frmTarget.strシリーズ名 = Nz(シリーズ名.Text);
                frmTarget.str申請者コード = Nz(申請者コード.Text);
                // frm.str申請者名 = Nz(申請者名.Value);

                // testのため退避
                ////frmTarget.lng承認指定 = Nz(Convert.ToInt32(承認指定.Text));
                ////frmTarget.lng終了指定 = Nz(Convert.ToInt32(終了指定.Text));
                ////frmTarget.lng完了指定 = Nz(Convert.ToInt32(完了指定.Text));
                ////frmTarget.lng削除指定 = Nz(Convert.ToInt32(削除指定.Text));

                // test(ダミーの値)
                frmTarget.lng承認指定 = 0;
                frmTarget.lng終了指定 = 0;
                frmTarget.lng完了指定 = 0;
                frmTarget.lng削除指定 = 0;

                ////((F_購買申請管理)this.Owner).DoUpdate();

                if (!frmTarget.DoUpdate())
                {
                    MessageBox.Show("抽出処理は失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (frmTarget.appEXT)
                {
                    MessageBox.Show("抽出条件に一致するデータはありません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                    return;
                }
            }
            finally
            {
                this.Close();
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
                // objParent に購買納期開始の参照を設定
                sender = this.購買納期開始;

                // カレンダーフォームを作成して表示
                form.ShowDialog();
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
                // 日付選択フォームを作成し表示
                F_カレンダー form = new F_カレンダー();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    購買納期開始.Text = selectedDate;
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
                //// objParent に購買納期開始の参照を設定
                //sender = this.購買納期開始;

                //// カレンダーフォームを作成して表示
                //form.ShowDialog();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    購買納期開始.Text = selectedDate;
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
                // objParent に購買納期開始の参照を設定
                sender = this.購買納期終了;

                // カレンダーフォームを作成して表示
                form.ShowDialog();
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
                // 日付選択フォームを作成し表示
                F_カレンダー form = new F_カレンダー();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    購買納期終了.Text = selectedDate;
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
                //// objParent に購買納期開始の参照を設定
                //sender = this.購買納期終了;

                //// カレンダーフォームを作成して表示
                //form.ShowDialog();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    購買納期終了.Text = selectedDate;
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
                // objParent に購買納期開始の参照を設定
                sender = this.出荷予定日開始;

                // カレンダーフォームを作成して表示
                form.ShowDialog();
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
                // 日付選択フォームを作成し表示
                F_カレンダー form = new F_カレンダー();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    出荷予定日開始.Text = selectedDate;
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
                //// objParent に購買納期開始の参照を設定
                //sender = this.出荷予定日開始;

                //// カレンダーフォームを作成して表示
                //form.ShowDialog();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    出荷予定日開始.Text = selectedDate;
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
                // objParent に購買納期開始の参照を設定
                sender = this.出荷予定日終了;

                // カレンダーフォームを作成して表示
                form.ShowDialog();
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
                // 日付選択フォームを作成し表示
                F_カレンダー form = new F_カレンダー();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    出荷予定日終了.Text = selectedDate;
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
                //// objParent に購買納期開始の参照を設定
                //sender = this.出荷予定日終了;

                //// カレンダーフォームを作成して表示
                //form.ShowDialog();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    出荷予定日終了.Text = selectedDate;
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
                // objParent に申請日開始の参照を設定
                sender = this.申請日開始;

                // カレンダーフォームを作成して表示
                form.ShowDialog();
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 申請日開始_KeyPress(object sender, KeyPressEventArgs e)
        {
            F_カレンダー form = new F_カレンダー();
            if (form.ShowDialog() == DialogResult.OK)
            {
                // objParent に申請日開始の参照を設定
                sender = this.申請日開始;

                // 日付選択フォームから選択した日付を取得
                string selectedDate = form.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                申請日開始.Text = selectedDate;
            }

            if (e.KeyChar == ' ')
            {
                // 日付選択フォームを作成し表示
                //F_カレンダー form = new F_カレンダー();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // objParent に申請日開始の参照を設定
                    sender = this.申請日開始;

                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    申請日開始.Text = selectedDate;
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
                //// objParent に購買納期開始の参照を設定
                //sender = this.申請日開始;

                //// カレンダーフォームを作成して表示
                //form.ShowDialog();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    申請日開始.Text = selectedDate;
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
                // objParent に申請日終了の参照を設定
                sender = this.申請日終了;

                // カレンダーフォームを作成して表示
                form.ShowDialog();
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
                // 日付選択フォームを作成し表示
                F_カレンダー form = new F_カレンダー();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // objParent に申請日終了の参照を設定
                    sender = this.申請日終了;

                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    申請日終了.Text = selectedDate;
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
                //// objParent に申請日終了の参照を設定
                //sender = this.申請日終了;

                //// カレンダーフォームを作成して表示
                //form.ShowDialog();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    申請日終了.Text = selectedDate;
                }
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                MessageBox.Show($"エラーが発生しました: {ex.Message}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
