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
    public partial class 商品明細 : UserControl
    {
        public GcMultiRow Detail
        {
            get
            {
                return gcMultiRow1;
            }
        }

        public 商品明細()
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
            u_net.F_商品 objForm = (u_net.F_商品)Application.OpenForms["F_商品"];

            if (objForm != null)
            {
                switch (gcMultiRow1.CurrentCell)
                {
                    //テキストボックスEnter時の処理
                    case TextBoxCell:
                        switch (e.CellName)
                        {
                            case "明細削除ボタン":
                                //objForm.toolStripStatusLabel1.Text = "■明細行の削除または取消を行います。";
                                break;
                            case "構成番号":
                                //objForm.toolStripStatusLabel1.Text = "■半角１０文字まで入力できます。　■ユニット内での構成番号の重複はできません。";
                                break;
                            case "部品コード":
                                //objForm.toolStripStatusLabel1.Text = "■部品コードを入力するか、[space]キー押下またはダブルクリックで部品選択ウィンドウを表示します。";
                                break;
                            case "型番":
                                //objForm.toolStripStatusLabel1.Text = "■半角５０文字まで入力できます。";
                                break;
                            case "メーカー名":
                                //objForm.toolStripStatusLabel1.Text = "■全角２５文字まで入力できます。";
                                break;
                            default:
                                //objForm.toolStripStatusLabel1.Text = "各種項目の説明";
                                break;
                        }
                        break;

                    case ComboBoxCell:
                        switch (e.CellName)
                        {
                            case "変更操作コード":
                                //objForm.toolStripStatusLabel1.Text = "■改版時に入力します。変更操作を選択してください。";
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
