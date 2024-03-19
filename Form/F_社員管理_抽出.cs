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
    public partial class F_社員管理_抽出 : Form
    {

        public F_社員管理_抽出()
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
                if (Application.OpenForms["F_社員管理"] == null)
                {
                    MessageBox.Show("[社員管理]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }

                //開いているフォームのインスタンスを作成する
                F_社員管理 frmTarget = Application.OpenForms.OfType<F_社員管理>().FirstOrDefault();

                // F_社員管理クラスからデータを取得し、現在のフォームのコントロールに設定
                switch (frmTarget.lng社員区分)
                {
                    case 1:
                        社員区分Button1.Checked = true;
                        break;
                    case 2:
                        社員区分Button2.Checked = true;
                        break;
                    case 0:
                        社員区分Button3.Checked = true;
                        break;

                    default:
                        // intComposedChipMount の値に対応するラジオボタンがない場合の処理
                        break;
                }

                this.氏名.Text = frmTarget.str氏名;

                switch (frmTarget.lng退社指定)
                {
                    case 1:
                        退社指定Button1.Checked = true;
                        break;
                    case 2:
                        退社指定Button2.Checked = true;
                        break;
                    case 0:
                        退社指定Button3.Checked = true;
                        break;

                    default:
                        // intComposedChipMount の値に対応するラジオボタンがない場合の処理
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
                F_社員管理? frmTarget = Application.OpenForms.OfType<F_社員管理>().FirstOrDefault();
                //F_社員管理 frmTarget = new F_社員管理();

                // frmTarget.社員コード = Nz(社員コード.Text);
                if (社員区分Button1.Checked)
                {
                    frmTarget.lng社員区分 = 1;
                }
                else if (社員区分Button2.Checked)
                {
                    frmTarget.lng社員区分 = 2;
                }
                else if (社員区分Button3.Checked)
                {
                    frmTarget.lng社員区分 = 0;
                }

                frmTarget.str氏名 = Nz(氏名.Text);

                if (退社指定Button1.Checked)
                {
                    frmTarget.lng退社指定 = 1;
                }
                else if (退社指定Button2.Checked)
                {
                    frmTarget.lng退社指定 = 2;
                }
                else if (退社指定Button3.Checked)
                {
                    frmTarget.lng退社指定 = 0;
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
                    this.Close();
                    return;
                }

                this.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(this.Name + "_抽出ボタン_Click - " + ex.Message);
                MessageBox.Show("エラーが発生しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            finally
            {
                //this.Painting = true;
                
            }
        }

        private void キャンセルボタン_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private F_検索 SearchForm;

        private void 氏名_Validated(object sender, EventArgs e)
        {
            //Connect();
            //this.仕入先名.Text = FunctionClass.GetSupplierName(cn, Nz(this.仕入先コード.Text));
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

        private void 氏名_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■氏名の一部を入力します。あいまい検索されます。";
        }

        private void 氏名_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void Form_KyeDown(object sender, KeyEventArgs e)
        {

        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;
            }
        }

    }
}
