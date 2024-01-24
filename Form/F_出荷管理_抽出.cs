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
using static u_net.Public.FunctionClass;

namespace u_net
{
    public partial class F_出荷管理_抽出 : Form
    {

        public F_出荷管理_抽出()
        {
            InitializeComponent();
        }

        SqlConnection cn;
        bool setflg = false;
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
                if (Application.OpenForms["F_出荷管理旧"] == null)
                {
                    MessageBox.Show("[出荷管理]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }

                //開いているフォームのインスタンスを作成する
                F_出荷管理旧 frmTarget = Application.OpenForms.OfType<F_出荷管理旧>().FirstOrDefault();

                受注コード開始.Text = frmTarget.str受注コード開始.ToString();
                受注コード終了.Text = frmTarget.str受注コード終了.ToString();

                if (frmTarget.dte出荷予定日1 != DateTime.MinValue)
                    出荷予定日開始.Text = frmTarget.dte出荷予定日1.ToString("yyyy/MM/dd");

                if (frmTarget.dte出荷予定日2 != DateTime.MinValue)
                    出荷予定日終了.Text = frmTarget.dte出荷予定日2.ToString("yyyy/MM/dd");


                出荷コード開始.Text = frmTarget.str出荷コード開始.ToString();
                出荷コード終了.Text = frmTarget.str出荷コード終了.ToString();
                型番.Text = frmTarget.str型番.ToString();
                型番詳細.Text = frmTarget.str型番詳細.ToString();


                switch (frmTarget.lngシリアル番号指定)
                {
                    case 1:
                        シリアル番号指定1.Checked = true;
                        break;
                    case 2:
                        シリアル番号指定2.Checked = true;
                        break;
                    case 0:
                        シリアル番号指定3.Checked = true;
                        break;

                    default:
                        break;
                }

                if (frmTarget.lngシリアル番号開始 != -1)
                    シリアル番号開始.Text = frmTarget.lngシリアル番号開始.ToString();

                if (frmTarget.lngシリアル番号終了 != -1)
                    シリアル番号終了.Text = frmTarget.lngシリアル番号終了.ToString();

                発送先名.Text = frmTarget.str発送先名.ToString();
                顧客コード.Text = frmTarget.str顧客コード.ToString();
                顧客名.Text = frmTarget.str顧客名.ToString();

                switch (frmTarget.lng作業終了指定)
                {
                    case 1:
                        作業終了指定1.Checked = true;
                        break;
                    case 2:
                        作業終了指定2.Checked = true;
                        break;
                    case 0:
                        作業終了指定3.Checked = true;
                        break;

                    default:
                        break;
                }

                if (frmTarget.dte作業終了日開始 != DateTime.MinValue)
                    作業終了日開始.Text = frmTarget.dte作業終了日開始.ToString("yyyy/MM/dd");

                if (frmTarget.dte作業終了日終了 != DateTime.MinValue)
                    作業終了日終了.Text = frmTarget.dte作業終了日終了.ToString("yyyy/MM/dd");

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
                F_出荷管理旧? frmTarget = Application.OpenForms.OfType<F_出荷管理旧>().FirstOrDefault();
                DateTime dt;
                int num;

                frmTarget.str受注コード開始 = Nz(受注コード開始.Text);
                frmTarget.str受注コード終了 = Nz(受注コード終了.Text);
                frmTarget.str出荷コード開始 = 出荷コード開始.Text;
                frmTarget.str出荷コード終了 = 出荷コード終了.Text;
                frmTarget.dte出荷予定日1 = DateTime.TryParse(出荷予定日開始.Text, out dt) ? dt : default(DateTime);
                frmTarget.dte出荷予定日2 = DateTime.TryParse(出荷予定日終了.Text, out dt) ? dt : default(DateTime);

                frmTarget.str型番 = Nz(型番.Text);
                frmTarget.str型番詳細 = Nz(型番詳細.Text);


                if (シリアル番号指定1.Checked)
                {
                    frmTarget.lngシリアル番号指定 = 1;
                }
                else if (シリアル番号指定2.Checked)
                {
                    frmTarget.lngシリアル番号指定 = 2;
                }
                else if (シリアル番号指定3.Checked)
                {
                    frmTarget.lngシリアル番号指定 = 0;
                }


                if (string.IsNullOrEmpty(シリアル番号開始.Text) || string.IsNullOrEmpty(シリアル番号終了.Text))
                {
                    frmTarget.lngシリアル番号開始 = -1;
                    frmTarget.lngシリアル番号終了 = -1;
                }
                else
                {
                    frmTarget.lngシリアル番号開始 = int.Parse(シリアル番号開始.Text);
                    frmTarget.lngシリアル番号終了 = int.Parse(シリアル番号終了.Text);
                }

                frmTarget.str発送先名 = Nz(発送先名.Text);
                frmTarget.str顧客コード = Nz(顧客コード.Text);
                frmTarget.str顧客名 = Nz(顧客名.Text);


                if (作業終了指定1.Checked)
                {
                    frmTarget.lng作業終了指定 = 1;
                }
                else if (作業終了指定2.Checked)
                {
                    frmTarget.lng作業終了指定 = 2;
                }
                else if (作業終了指定3.Checked)
                {
                    frmTarget.lng作業終了指定 = 0;
                }


                frmTarget.dte作業終了日開始 = DateTime.TryParse(作業終了日開始.Text, out dt) ? dt : default(DateTime);
                frmTarget.dte作業終了日終了 = DateTime.TryParse(作業終了日終了.Text, out dt) ? dt : default(DateTime);

                // 抽出更新

                frmTarget.Filtering();

            }
            catch (Exception ex)
            {
                Debug.WriteLine(this.Name + "_抽出ボタン_Click - " + ex.Message);
                MessageBox.Show("エラーが発生しました。" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private F_カレンダー dateSelectionForm;

        private void 出荷コード開始_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(出荷コード開始, 出荷コード終了, sender as Control);
        }

        private void 出荷コード終了_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(出荷コード開始, 出荷コード終了, sender as Control);
        }

        private void 出荷予定日開始選択ボタン_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(出荷予定日開始.Text))
            {
                dateSelectionForm.args = 出荷予定日開始.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                出荷予定日開始.Text = selectedDate;

                出荷予定日開始.Focus();
            }
        }

        private void 出荷予定日開始_DoubleClick(object sender, EventArgs e)
        {
            出荷予定日開始選択ボタン_Click(sender, e);
        }

        private void 出荷予定日開始_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                出荷予定日開始選択ボタン_Click(sender, e);
            }

        }

        private void 出荷予定日終了選択ボタン_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(出荷予定日終了.Text))
            {
                dateSelectionForm.args = 出荷予定日終了.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                出荷予定日終了.Text = selectedDate;
                出荷予定日終了.Focus();
            }
        }

        private void 出荷予定日終了_DoubleClick(object sender, EventArgs e)
        {
            出荷予定日終了選択ボタン_Click(sender, e);
        }

        private void 出荷予定日終了_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                出荷予定日終了選択ボタン_Click(sender, e);
            }
        }

        private void 出荷予定日開始_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■入力欄をダブルクリックすると、カレンダーから日付を選択することができます。";
        }

        private void 出荷予定日終了_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■入力欄をダブルクリックすると、カレンダーから日付を選択することができます。";
        }

        private void 出荷予定日開始_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(出荷予定日開始, 出荷予定日終了, sender as Control);
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 出荷予定日終了_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(出荷予定日開始, 出荷予定日終了, sender as Control);
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void F_出荷管理_抽出_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;
            }
        }


        private void 受注コード開始_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(受注コード開始, 受注コード終了, sender as Control);
        }


        private void 受注コード終了_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(受注コード開始, 受注コード終了, sender as Control);
        }

        private void 型番_TextChanged(object sender, EventArgs e)
        {

        }

        private void 型番_Validating(object sender, CancelEventArgs e)
        {

        }

        private void 型番_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■半角４８文字まで入力できます。";
        }

        private void 型番_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 型番詳細_TextChanged(object sender, EventArgs e)
        {

        }

        private void 型番詳細_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■型番について詳細な条件を指定します。　■半角１００文字まで入力できます。";
        }

        private void 型番詳細_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void シリアル番号指定_Validated(object sender, EventArgs e)
        {

        }

        private void シリアル番号開始_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(シリアル番号開始, シリアル番号終了, sender as Control);
        }

        private void シリアル番号開始_Validated(object sender, EventArgs e)
        {

        }

        private void シリアル番号終了_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(シリアル番号開始, シリアル番号終了, sender as Control);
        }

        private void シリアル番号終了_Validated(object sender, EventArgs e)
        {

        }

        private void 発送先名_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■発送先名の部分文字列を入力します。";
        }

        private void 発送先名_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 顧客コード_DoubleClick(object sender, EventArgs e)
        {
            F_検索 SearchForm = new F_検索();
            SearchForm.FilterName = "顧客名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                setflg = true;
                string SelectedCode = SearchForm.SelectedCode;
                顧客コード.Text = SelectedCode;
                顧客名.Focus();
            }
        }

        private void 顧客名_Validated(object sender, EventArgs e)
        {
            if (!setflg)
            {
                顧客コード.Text = null;
            }
            setflg = false;
        }
        private void 顧客コード_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■顧客を検索するには入力欄をダブルクリックしてください。";
        }

        private void 顧客コード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 顧客コード_Validating(object sender, CancelEventArgs e)
        {

        }

        private void 顧客コード検索ボタン_Click(object sender, EventArgs e)
        {
            F_検索 SearchForm = new F_検索();
            SearchForm.FilterName = "顧客名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                setflg = true;
                string SelectedCode = SearchForm.SelectedCode;
                顧客コード.Text = SelectedCode;
                顧客名.Focus();
            }
        }

        private void 作業終了日開始選択ボタン_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(作業終了日開始.Text))
            {
                dateSelectionForm.args = 作業終了日開始.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                作業終了日開始.Text = selectedDate;

                作業終了日開始.Focus();
            }
        }

        private void 作業終了日開始_DoubleClick(object sender, EventArgs e)
        {
            作業終了日開始選択ボタン_Click(sender, e);
        }

        private void 作業終了日開始_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                作業終了日開始選択ボタン_Click(sender, e);
            }
        }

        private void 作業終了日終了選択ボタン_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(作業終了日終了.Text))
            {
                dateSelectionForm.args = 作業終了日終了.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                作業終了日終了.Text = selectedDate;
                作業終了日終了.Focus();
            }
        }

        private void 作業終了日終了_DoubleClick(object sender, EventArgs e)
        {
            作業終了日終了選択ボタン_Click(sender, e);
        }

        private void 作業終了日終了_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                作業終了日終了選択ボタン_Click(sender, e);
            }
        }

        private void 作業終了日開始_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■入力欄をダブルクリックすると、カレンダーから日付を選択することができます。";
        }

        private void 作業終了日終了_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■入力欄をダブルクリックすると、カレンダーから日付を選択することができます。";
        }

        private void 作業終了日開始_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(作業終了日開始, 作業終了日終了, sender as Control);
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 作業終了日終了_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(作業終了日開始, 作業終了日終了, sender as Control);
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 作業終了日開始_Validated(object sender, EventArgs e)
        {
            作業終了指定.Text = "1";
        }

        private void 作業終了日終了_Validated(object sender, EventArgs e)
        {
            作業終了指定.Text = "1";
        }

        private void 受注コード開始_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                string strCode = ActiveControl.Text;

                if (string.IsNullOrEmpty(strCode))
                    return;

                strCode = FunctionClass.FormatCode("A", strCode);

                if (strCode != ActiveControl.Text)
                {
                    ActiveControl.Text = strCode;
                }
            }
        }

        private void 受注コード終了_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                string strCode = ActiveControl.Text;

                if (string.IsNullOrEmpty(strCode))
                    return;

                strCode = FunctionClass.FormatCode("A", strCode);

                if (strCode != ActiveControl.Text)
                {
                    ActiveControl.Text = strCode;
                }
            }
        }

        private void 顧客コード_TextChanged(object sender, EventArgs e)
        {
            Connect();
            if (!string.IsNullOrEmpty(顧客コード.Text))
                顧客名.Text = FunctionClass.GetCustomerName(cn, 顧客コード.Text);
        }
    }
}
