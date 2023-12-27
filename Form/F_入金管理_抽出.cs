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
    public partial class F_入金管理_抽出 : Form
    {
        F_入金管理 frmTarget;
        public object objParent;
        public Control ctlNext;

        public F_入金管理_抽出()
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
                if (Application.OpenForms["F_入金管理"] == null)
                {
                    MessageBox.Show("[入金管理]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }

                //開いているフォームのインスタンスを作成する
                F_入金管理 frmTarget = Application.OpenForms.OfType<F_入金管理>().FirstOrDefault();

                // F_入金管理クラスからデータを取得し、現在のフォームのコントロールに設定
                入金コード開始.Text = frmTarget.str入金コード開始;
                入金コード終了.Text = frmTarget.str入金コード終了;
                if (frmTarget.dtm入金日開始!=DateTime.MinValue)
                    入金日開始.Text = frmTarget.dtm入金日開始.ToString();
                if (frmTarget.dtm入金日終了!=DateTime.MinValue)
                    入金日終了.Text = frmTarget.dtm入金日終了.ToString();
                顧客コード.Text = frmTarget.str顧客コード;
                顧客名.Text = frmTarget.str顧客名;
                入金金額開始.Text = frmTarget.str入金金額開始;
                入金金額終了.Text = frmTarget.str入金金額終了;
                //請求指定.Text = frmTarget.lng請求指定.ToString();
                //削除指定.Text = frmTarget.lng削除指定.ToString();
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

                F_入金管理? frmTarget = Application.OpenForms.OfType<F_入金管理>().FirstOrDefault();
                frmTarget.str入金コード開始 = Nz(入金コード開始.Text);
                frmTarget.str入金コード終了 = Nz(入金コード終了.Text);
                frmTarget.dtm入金日開始 = DateTime.TryParse(入金日開始.Text, out var dt) ? dt : default(DateTime);
                frmTarget.dtm入金日終了 = DateTime.TryParse(入金日終了.Text, out var dt1) ? dt1 : default(DateTime);
                frmTarget.str顧客コード = Nz(顧客コード.Text);
                frmTarget.str顧客名 = Nz(顧客名.Text);
                frmTarget.str入金金額開始 = Nz(入金金額開始.Text);
                frmTarget.str入金金額終了 = Nz(入金金額終了.Text);
                frmTarget.lng請求指定 = int.Parse(請求指定.Text);
                frmTarget.lng削除指定 = int.Parse(削除指定.Text);

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

        private void 仕入先参照ボタン_Click(object sender, EventArgs e)
        {
            F_仕入先 fm = new F_仕入先();
            fm.args = this.入金日開始.Text;
            fm.ShowDialog();
        }

        private void 顧客コード_Validated(object sender, EventArgs e)
        {
            Connect();
            if (!string.IsNullOrEmpty(顧客コード.Text))
                顧客名.Text = FunctionClass.GetCustomerName(cn, 顧客コード.Text);
        }

        private void 顧客コード_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    if (this.ActiveControl.Text != "")
                    {
                        this.ActiveControl.Text = FunctionClass.FormatCode(this.ActiveControl.Text, "00000000");
                    }
                    break;
            }
        }

        private void 顧客コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (e.KeyChar == (char)Keys.Space)
                {
                    ctlNext = 顧客名;
                    objParent = 顧客コード;
                    // "検索" フォームを開く処理
                    F_検索 searchForm = new F_検索();
                    searchForm.Show();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
        }

        private void 顧客コード検索ボタン_Click(object sender, EventArgs e)
        {
            objParent = this;
            F_検索 form = new F_検索();
            form.FilterName = "顧客名フリガナ";
            if (form.ShowDialog() == DialogResult.OK)
            {
                string SelectedCode = form.SelectedCode;
                顧客コード.Text = SelectedCode;
            }
        }

        private void 顧客名_Validated(object sender, EventArgs e)
        {
            顧客コード.Text = null;
        }

        private void 入金コード開始_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                string strCode = ActiveControl.Text;

                if (string.IsNullOrEmpty(strCode))
                    return;

                strCode = FunctionClass.FormatCode("B", strCode);

                if (strCode != ActiveControl.Text)
                {
                    ActiveControl.Text = strCode;
                }
            }
        }

        private void 入金コード開始_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(入金コード開始, 入金コード終了, sender as Control);
        }

        private void 入金コード終了_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                string strCode = ActiveControl.Text;

                if (string.IsNullOrEmpty(strCode))
                    return;

                strCode = FunctionClass.FormatCode("B", strCode);

                if (strCode != ActiveControl.Text)
                {
                    ActiveControl.Text = strCode;
                }
            }
        }

        private void 入金コード終了_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(入金コード開始, 入金コード終了, sender as Control);
        }

        private void 入金金額開始_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(入金金額開始, 入金金額終了, sender as Control);
        }

        private void 入金金額終了_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(入金金額開始, 入金金額終了, sender as Control);
        }

        private void 入金区分コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                // アクティブなコントロールがコンボボックスであることを仮定
                ComboBox activeComboBox = ActiveControl as ComboBox;

                if (activeComboBox != null)
                {
                    activeComboBox.DroppedDown = true;
                    e.Handled = true; // イベントを処理済みとしてマーク
                }
            }
        }

        private void 入金日開始_KeyPress(object sender, KeyPressEventArgs e)
        {
            F_カレンダー form = new F_カレンダー();
            if (form.ShowDialog() == DialogResult.OK)
            {
                // objParent に入金日開始の参照を設定
                sender = this.入金日開始;

                // 日付選択フォームから選択した日付を取得
                string selectedDate = form.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                入金日開始.Text = selectedDate;
            }

            if (e.KeyChar == ' ')
            {
                // 日付選択フォームを作成し表示
                //F_カレンダー form = new F_カレンダー();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // objParent に入金日開始の参照を設定
                    sender = this.入金日開始;

                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    入金日開始.Text = selectedDate;
                }
            }
        }

        private void 入金日開始_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(入金日開始, 入金日終了, sender as Control);
        }

        private void 入金日開始選択ボタン_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            F_カレンダー form = new F_カレンダー();
            if (form.ShowDialog() == DialogResult.OK)
            {
                // objParent に入金日開始の参照を設定
                sender = this.入金日開始;

                // 日付選択フォームから選択した日付を取得
                string selectedDate = form.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                入金日開始.Text = selectedDate;
            }
        }

        private void 入金日終了_KeyPress(object sender, KeyPressEventArgs e)
        {
            F_カレンダー form = new F_カレンダー();
            if (form.ShowDialog() == DialogResult.OK)
            {
                // objParent に入金日終了の参照を設定
                sender = this.入金日終了;

                // 日付選択フォームから選択した日付を取得
                string selectedDate = form.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                入金日終了.Text = selectedDate;
            }

            if (e.KeyChar == ' ')
            {
                // 日付選択フォームを作成し表示
                //F_カレンダー form = new F_カレンダー();
                if (form.ShowDialog() == DialogResult.OK)
                {
                    // objParent に入金日終了の参照を設定
                    sender = this.入金日終了;

                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = form.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    入金日終了.Text = selectedDate;
                }
            }
        }

        private void 入金日終了_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(入金日開始, 入金日終了, sender as Control);
        }

        private void 入金日終了選択ボタン_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            F_カレンダー form = new F_カレンダー();
            if (form.ShowDialog() == DialogResult.OK)
            {
                // objParent に入金日終了の参照を設定
                sender = this.入金日終了;

                // 日付選択フォームから選択した日付を取得
                string selectedDate = form.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                入金日終了.Text = selectedDate;
            }
        }
    }
}
