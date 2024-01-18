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
                if (Application.OpenForms["F_請求処理"] == null)
                {
                    MessageBox.Show("[請求処理]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }

                請求締日.Text = DateTime.Now.ToString("yyyy/MM/dd");

                //開いているフォームのインスタンスを作成する
                F_請求処理 frmTarget = Application.OpenForms.OfType<F_請求処理>().FirstOrDefault();

                FunctionClass fn = new FunctionClass();
                顧客コード.Text = fn.Zn(frmTarget.str顧客コード)?.ToString();
                顧客名.Text = fn.Zn(frmTarget.str顧客名)?.ToString();


                if (frmTarget.dte請求締日 != DateTime.MinValue)
                    請求締日.Text = frmTarget.dte請求締日.ToString("yyyy/MM/dd");

                switch (frmTarget.byt表示方法)
                {
                    case 1:
                        表示方法Button1.Checked = true;
                        break;
                    case 2:
                        表示方法Button2.Checked = true;
                        break;
                    case 3:
                        表示方法Button3.Checked = true;
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

        private void OKボタン_Click(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();
            try
            {                
                fn.DoWait("しばらくお待ちください...");
                F_請求処理? frmTarget = Application.OpenForms.OfType<F_請求処理>().FirstOrDefault();

                frmTarget.str顧客コード = Nz(顧客コード.Text);
                frmTarget.str顧客名 = Nz(顧客名.Text);
                frmTarget.dte請求締日 = string.IsNullOrEmpty(請求締日.Text) ? DateTime.MinValue : DateTime.Parse(請求締日.Text);

                if (表示方法Button1.Checked)
                {
                    frmTarget.byt表示方法 = 1;
                }
                else if (表示方法Button2.Checked)
                {
                    frmTarget.byt表示方法 = 2;
                }
                else if (表示方法Button3.Checked)
                {
                    frmTarget.byt表示方法 = 3;
                }

                frmTarget.Filtering();

                int tmpint = 0;
                int.TryParse(frmTarget.表示件数.Text, out tmpint);
                if (tmpint == 0 )
                {
                    MessageBox.Show("抽出条件に一致するデータはありません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }
                else if (tmpint < 0)
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

        private void 顧客検索ボタン_Click(object sender, EventArgs e)
        {
            F_検索 SearchForm = new F_検索();
            SearchForm.FilterName = "顧客名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                // setflg = true;
                string SelectedCode = SearchForm.SelectedCode;
                顧客コード.Text = SelectedCode;
                顧客コード.Focus();
                Connect();
                顧客名.Text = FunctionClass.GetCustomerName(cn, Nz(顧客コード.Text));
            }
        }

        private void F_請求条件設定_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;
            }
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

        private void 請求締日選択ボタン_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(請求締日.Text))
            {
                dateSelectionForm.args = 請求締日.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                //// フォームAの日付コントロールに選択した日付を設定
                請求締日.Text = selectedDate;
                請求締日.Focus();
            }
        }
    }
}
