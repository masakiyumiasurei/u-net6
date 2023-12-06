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
                            case "型式名":
                                objForm.toolStripStatusLabel1.Text = "■半角４８文字まで入力できます。　■英数字は半角文字で入力し、半角カタカナは使用しないでください。";
                                break;
                            case "定価":
                                objForm.toolStripStatusLabel1.Text = "■型式ごとの定価を設定します。　■マイナス価格を設定することも可能です。";
                                break;
                            case "機能":
                                objForm.toolStripStatusLabel1.Text = "■全角２５文字まで入力できます。";
                                break;
                            case "型式番号":
                                objForm.toolStripStatusLabel1.Text = "■型式名を順序付けるための番号です。入力はできません。";
                                break;
                            case "構成番号":
                                objForm.toolStripStatusLabel1.Text = "■１～９９までの構成番号を入力します。　■商品構成時、番号順に構成され、同一番号のものは同時に構成できなくなります。";
                                break;
                            default:
                                objForm.toolStripStatusLabel1.Text = "各種項目の説明";
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
