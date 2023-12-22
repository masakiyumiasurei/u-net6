using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;

namespace MultiRowDesigner
{
    public partial class 業務日報明細予定 : UserControl
    {
        public GcMultiRow Detail
        {
            get
            {
                return gcMultiRow1;
            }
        }

        public 業務日報明細予定()
        {
            InitializeComponent();
        }

        private void gcMultiRow1_CellContentClick(object sender, CellEventArgs e)
        {

            switch (gcMultiRow1.CurrentCell)
            {
                //ボタンClick時の処理
                case ButtonCell:
                    switch (e.CellName)
                    {
                        case "明細削除ボタン":
                            MessageBox.Show("削除します。", "削除", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            break;
                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }

        }

        private void gcMultiRow1_CellEnter(object sender, CellEventArgs e)
        {
            u_net.F_業務日報 objForm = (u_net.F_業務日報)Application.OpenForms["F_業務日報"];

            if (objForm != null)
            {
                switch (gcMultiRow1.CurrentCell)
                {
                    //コンボボックスEnter時の処理
                    case ComboBoxCell:
                        switch (e.CellName)
                        {
                            case "項目コード":
                                objForm.toolStripStatusLabel1.Text = "■予定項目を選択します。　■[space]キーでドロップダウンリストが表示されます。";
                                break;
                            case "予定内容":
                                objForm.toolStripStatusLabel1.Text = "■予定内容を入力または選択します。　■40文字まで入力可。（全角換算）　■[space]キーで過去の入力履歴を表示します。";
                                break;
                            default:
                                break;

                        }
                        break;

                    default:
                        break;
                }
            }

        }

    }
}
