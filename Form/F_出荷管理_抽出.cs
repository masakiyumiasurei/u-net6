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
    public partial class F_出荷管理_抽出 : Form
    {

        public F_出荷管理_抽出()
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
                //if (Application.OpenForms["F_出荷管理"] == null)
                //{
                //    MessageBox.Show("[出荷管理]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    this.Close();
                //    return;
                //}

                //this.非含有証明書.DataSource = new KeyValuePair<long, String>[] {
                //    new KeyValuePair<long, String>(1, "返却済み"),
                //    new KeyValuePair<long, String>(2, "未返却"),
                //    new KeyValuePair<long, String>(3, "未提出"),
                //    new KeyValuePair<long, String>(4, "不明"),
                //    new KeyValuePair<long, String>(0, "指定しない"),
                //};
                //this.非含有証明書.DisplayMember = "Value";
                //this.非含有証明書.ValueMember = "Key";

                //開いているフォームのインスタンスを作成する
                F_出荷管理旧 frmTarget = Application.OpenForms.OfType<F_出荷管理旧>().FirstOrDefault();

                //品名.Text = frmTarget.str品名;
                //型番.Text = frmTarget.str型番;

                //if (frmTarget.dtm更新日開始 != DateTime.MinValue)
                //    出荷予定日開始.Text = frmTarget.dtm更新日開始.ToString("yyyy/MM/dd");
                //if (frmTarget.dtm更新日終了 != DateTime.MinValue)
                //    出荷予定日終了.Text = frmTarget.dtm更新日終了.ToString("yyyy/MM/dd");
                //更新者名.Text = frmTarget.str更新者名;


                //switch (frmTarget.lng廃止指定)
                //{
                //    case 1:
                //        廃止指定Button1.Checked = true;
                //        break;
                //    case 2:
                //        廃止指定Button2.Checked = true;
                //        break;
                //    case 0:
                //        廃止指定Button3.Checked = true;
                //        break;

                //    default:

                //        break;
                //}

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
                F_出荷管理旧? frmTarget = Application.OpenForms.OfType<F_出荷管理旧>().FirstOrDefault();


                //frmTarget.str品名 = Nz(品名.Text);
                //frmTarget.str型番 = Nz(型番.Text);


                //if (シリアル番号指定1.Checked)
                //{
                //    frmTarget.lngRoHS対応 = 1;
                //}
                //else if (シリアル番号指定2.Checked)
                //{
                //    frmTarget.lngRoHS対応 = 2;
                //}
                //else if (シリアル番号指定3.Checked)
                //{
                //    frmTarget.lngRoHS対応 = 0;
                //}

                //frmTarget.lng非含有証明書 = 非含有証明書.SelectedValue != null ? Convert.ToInt64(非含有証明書.SelectedValue) : 0;

                //frmTarget.dtm更新日開始 = string.IsNullOrEmpty(出荷予定日開始.Text) ?
                //   DateTime.MinValue : DateTime.Parse(出荷予定日開始.Text);

                //frmTarget.dtm更新日終了 = string.IsNullOrEmpty(出荷予定日終了.Text) ?
                //    DateTime.MinValue : DateTime.Parse(出荷予定日終了.Text);

                //frmTarget.str更新者名 = Nz(更新者名.Text);



                //long cnt = frmTarget.DoUpdate();

                //if (cnt == 0)
                //{
                //    MessageBox.Show("抽出条件に一致するデータはありません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                //    return;
                //}
                //else if (cnt < 0)
                //{
                //    MessageBox.Show("エラーが発生したため、抽出できませんでした。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    return;
                //}
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

        private void 出荷コード開始_Leave(object sender, EventArgs e)
        {

        }

        private void 出荷コード終了_Leave(object sender, EventArgs e)
        {

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

        private void 受注コード開始_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void 受注コード開始_Leave(object sender, EventArgs e)
        {

        }

        private void 受注コード終了_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void 受注コード終了_Leave(object sender, EventArgs e)
        {

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

        }

        private void シリアル番号開始_Validated(object sender, EventArgs e)
        {

        }

        private void シリアル番号終了_Leave(object sender, EventArgs e)
        {

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

        }

        private void 作業終了日終了_Validated(object sender, EventArgs e)
        {

        }
    }
}
