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
                if (Application.OpenForms["F_仕入先管理"] == null)
                {
                    MessageBox.Show("[仕入先管理]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }

                //開いているフォームのインスタンスを作成する
                F_仕入先管理 frmTarget = Application.OpenForms.OfType<F_仕入先管理>().FirstOrDefault();

                // F_仕入先管理クラスからデータを取得し、現在のフォームのコントロールに設定
                this.顧客名.Text = frmTarget.str仕入先名;
                注文番号.Text = frmTarget.str仕入先名フリガナ;

                switch (frmTarget.lng削除指定)
                {
                    case 1:
                        削除指定Button1.Checked = true;
                        break;
                    case 2:
                        削除指定Button2.Checked = true;
                        break;

                    default:
                        // intComposedChipMount の値に対応するラジオボタンがない場合の処理
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
                F_仕入先管理? frmTarget = Application.OpenForms.OfType<F_仕入先管理>().FirstOrDefault();
                //F_仕入先管理 frmTarget = new F_仕入先管理();

                // frmTarget.仕入先コード = Nz(仕入先コード.Text);
                frmTarget.str仕入先名 = Nz(顧客名.Text);
                frmTarget.str仕入先名フリガナ = Nz(注文番号.Text);


                if (削除指定Button1.Checked)
                {
                    frmTarget.lng削除指定 = 1;
                }
                else if (削除指定Button2.Checked)
                {
                    frmTarget.lng削除指定 = 2;
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

        private F_検索 SearchForm;
        private void 仕入先選択ボタン_Click(object sender, EventArgs e)
        {
            SearchForm = new F_検索();
            SearchForm.FilterName = "仕入先名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                string SelectedCode = SearchForm.SelectedCode;
                入金日開始.Text = SelectedCode;
            }
        }

        private void 仕入先コード_DoubleClick(object sender, EventArgs e)
        {
            SearchForm = new F_検索();
            SearchForm.FilterName = "仕入先名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                string SelectedCode = SearchForm.SelectedCode;
                入金日開始.Text = SelectedCode;
            }
        }
        private void 仕入先コード_Click(object sender, EventArgs e)
        {
            SearchForm = new F_検索();
            SearchForm.FilterName = "仕入先名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                string SelectedCode = SearchForm.SelectedCode;
                入金日開始.Text = SelectedCode;
            }
        }
        private void 仕入先コード_Validated(object sender, EventArgs e)
        {
            Connect();
            this.顧客名.Text = FunctionClass.GetSupplierName(cn, Nz(this.入金日開始.Text));
        }
        private void 仕入先コード_TextChanged(object sender, EventArgs e)
        {
            Connect();
            this.顧客名.Text = FunctionClass.GetSupplierName(cn, Nz(this.入金日開始.Text));
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
        private void 仕入先コード_KeyDown(object sender, KeyEventArgs e)
        {
            // 入力された値がエラー値の場合、Textプロパティが設定できなくなるときの対処
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Return:
                        string strCode = ((Control)sender).Text;
                        if (string.IsNullOrEmpty(strCode)) return;

                        strCode = strCode.PadLeft(8, '0');
                        if (strCode != ((Control)sender).Text)
                        {
                            ((Control)sender).Text = strCode;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                // エラーハンドリング: 例外が発生した場合に処理を行う
                Console.WriteLine("エラーが発生しました: " + ex.Message);
            }
        }

        private void 仕入先参照ボタン_Click(object sender, EventArgs e)
        {
            F_仕入先 fm = new F_仕入先();
            fm.args = this.入金日開始.Text;
            fm.ShowDialog();
        }
    }
}
