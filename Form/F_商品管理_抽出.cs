using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace u_net
{
    public partial class F_商品管理_抽出 : Form
    {
        public F_商品管理_抽出()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                // 対象フォームが読み込まれていないときはすぐに終了する
                if (Application.OpenForms["商品管理"] == null)
                {
                    MessageBox.Show("[商品管理]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }

                F_商品管理 frmTarget = new F_商品管理(); // F_商品管理フォームのインスタンスを作成

                // F_商品管理クラスからデータを取得し、現在のフォームのコントロールに設定
                 this.基本型式名.Text = frmTarget.str基本型式名;
                //シリーズ名.Text = frmTarget.strシリーズ名;
                //if (frmTarget.dtm更新日開始 != 0)
                //    更新日開始.Value = frmTarget.dtm更新日開始;
                //if (frmTarget.dtm更新日終了 != 0)
                //    更新日終了.Value = frmTarget.dtm更新日終了;
                //更新者名.Text = frmTarget.str更新者名;
                //ComposedChipMount.Value = frmTarget.intComposedChipMount;
                //IsUnit = frmTarget.intIsUnit;
                //Discontinued.Value = frmTarget.lngDiscontinued;
                //Deleted.Value = frmTarget.lngDeleted;
            }
            catch (Exception ex)
            {
                // 例外処理が必要な場合はここで処理を追加
                MessageBox.Show("エラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
