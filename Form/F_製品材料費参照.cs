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
    public partial class F_製品材料費参照 : Form
    {

        public F_製品材料費参照()
        {
            InitializeComponent();
        }

        SqlConnection cn;

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {

                // 対象フォームが読み込まれていないときはすぐに終了する
                //if (Application.OpenForms["F_ユニット管理"] == null)
                //{
                //    MessageBox.Show("[ユニット管理]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    this.Close();
                //    return;
                //}

                //開いているフォームのインスタンスを作成する
                //F_製品管理 frmTarget = Application.OpenForms.OfType<F_製品管理>().FirstOrDefault();

                // F_仕入先管理クラスからデータを取得し、現在のフォームのコントロールに設定
                //this.シリーズ名.Text = frmTarget.str仕入先名;
                //仕入先名フリガナ.Text = frmTarget.str仕入先名フリガナ;

                //switch (frmTarget.lng削除指定)
                //{
                //    case 1:
                //        //削除指定Button1.Checked = true;
                //        break;
                //    case 2:
                //        //削除指定Button2.Checked = true;
                //        break;
                //    case 0:
                //        //削除指定Button3.Checked = true;
                //        break;

                //    default:
                //        // intComposedChipMount の値に対応するラジオボタンがない場合の処理
                //        break;
                //}

            }
            catch (Exception ex)
            {
                // 例外処理が必要な場合はここで処理を追加
                MessageBox.Show("エラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 閉じるボタン_Click(object sender, EventArgs e)
        {

        }

    }
}
