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
    public partial class F_部品集合管理_抽出 : Form
    {

        public F_部品集合管理_抽出()
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
                if (Application.OpenForms["F_部品集合管理"] == null)
                {
                    MessageBox.Show("[部品集合管理]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }

                //開いているフォームのインスタンスを作成する
                F_部品集合管理 frmTarget = Application.OpenForms.OfType<F_部品集合管理>().FirstOrDefault();

                // F_仕入先管理クラスからデータを取得し、現在のフォームのコントロールに設定
                //this.シリーズ名.Text = frmTarget.str仕入先名;
                //仕入先名フリガナ.Text = frmTarget.str仕入先名フリガナ;

                switch (frmTarget.lng削除指定)
                {
                    case 1:
                        //削除指定Button1.Checked = true;
                        break;
                    case 2:
                        //削除指定Button2.Checked = true;
                        break;
                    case 0:
                        //削除指定Button3.Checked = true;
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
                F_部品集合管理? frmTarget = Application.OpenForms.OfType<F_部品集合管理>().FirstOrDefault();
                //F_仕入先管理 frmTarget = new F_仕入先管理();

                // frmTarget.仕入先コード = Nz(仕入先コード.Text);
                //frmTarget.str仕入先名 = Nz(シリーズ名.Text);
                //frmTarget.str仕入先名フリガナ = Nz(仕入先名フリガナ.Text);


                //if (削除指定Button1.Checked)
                //{
                //    frmTarget.lng削除指定 = 1;
                //}
                //else if (削除指定Button2.Checked)
                //{
                //    frmTarget.lng削除指定 = 2;
                //}
                //else if (削除指定Button3.Checked)
                //{
                //    frmTarget.lng削除指定 = 0;
                //}

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

        private void 分類名_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■[space]キーでドロップダウンリストを表示します。";
        }

        private void 分類名_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }
    }
}
