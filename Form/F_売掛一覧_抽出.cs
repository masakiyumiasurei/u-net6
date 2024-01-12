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
    public partial class F_売掛一覧_抽出 : Form
    {

        public F_売掛一覧_抽出()
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



                //開いているフォームのインスタンスを作成する
                F_売掛一覧 frmTarget = Application.OpenForms.OfType<F_売掛一覧>().FirstOrDefault();

                // F_仕入先管理クラスからデータを取得し、現在のフォームのコントロールに設定
                this.顧客名.Text = frmTarget.str顧客名;
                担当者名.Text = frmTarget.str担当者名;
                締日.Text = frmTarget.str締日;


                if (frmTarget.dtm支払日開始 != DateTime.MinValue)
                    支払日開始.Text = frmTarget.dtm支払日開始.ToString("yyyy/MM/dd");
                if (frmTarget.dtm支払日終了 != DateTime.MinValue)
                    支払日終了.Text = frmTarget.dtm支払日終了.ToString("yyyy/MM/dd");


                switch (frmTarget.lng完了指定)
                {
                    case 1:
                        完了指定1.Checked = true;
                        break;
                    case 2:
                        完了指定2.Checked = true;
                        break;
                    case 0:
                        完了指定3.Checked = true;
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
                F_売掛一覧? frmTarget = Application.OpenForms.OfType<F_売掛一覧>().FirstOrDefault();

                frmTarget.str顧客名 = Nz(顧客名.Text);
                frmTarget.str担当者名 = Nz(担当者名.Text);
                frmTarget.str締日 = Nz(締日.Text);

                frmTarget.dtm支払日開始 = string.IsNullOrEmpty(支払日開始.Text) ?
                   DateTime.MinValue : DateTime.Parse(支払日開始.Text);

                frmTarget.dtm支払日終了 = string.IsNullOrEmpty(支払日終了.Text) ?
                    DateTime.MinValue : DateTime.Parse(支払日終了.Text);

                if (完了指定1.Checked)
                {
                    frmTarget.lng完了指定 = 1;
                }
                else if (完了指定2.Checked)
                {
                    frmTarget.lng完了指定 = 2;
                }
                else if (完了指定3.Checked)
                {
                    frmTarget.lng完了指定 = 0;
                }


                if (!frmTarget.DoUpdate())
                {
                    MessageBox.Show("抽出できませんでした。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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



        private void 支払日開始選択_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(支払日開始.Text))
            {
                dateSelectionForm.args = 支払日開始.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                支払日開始.Text = selectedDate;
                支払日開始.Focus();
            }
        }

        private void 支払日開始_DoubleClick(object sender, EventArgs e)
        {
            支払日開始選択_Click(sender, e);
        }

        private void 支払日開始_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                支払日開始選択_Click(sender, e);
            }
        }




        private void 支払日終了選択_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(支払日終了.Text))
            {
                dateSelectionForm.args = 支払日終了.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                支払日終了.Text = selectedDate;
                支払日終了.Focus();
            }
        }

        private void 支払日終了_DoubleClick(object sender, EventArgs e)
        {
            支払日終了選択_Click(sender, e);
        }

        private void 支払日終了_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                支払日終了選択_Click(sender, e);
            }
        }




        private void 支払日開始_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(支払日開始, 支払日終了, sender as Control);
        }

        private void 支払日終了_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(支払日開始, 支払日終了, sender as Control);
        }

        private void F_売掛一覧_抽出_KeyDown(object sender, KeyEventArgs e)
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
