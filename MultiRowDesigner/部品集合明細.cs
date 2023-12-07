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
    public partial class 部品集合明細 : UserControl
    {
        public GcMultiRow Detail
        {
            get
            {
                return gcMultiRow1;
            }
        }

        public 部品集合明細()
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
                        case "部品参照ボタン":
                            MessageBox.Show("", "", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            u_net.F_部品集合 objForm = (u_net.F_部品集合)Application.OpenForms["F_部品集合"];

            if (objForm != null)
            {
                switch (gcMultiRow1.CurrentCell)
                {
                    //テキストボックスEnter時の処理
                    case TextBoxCell:
                        switch (e.CellName)
                        {
                            case "部品コード":
                                objForm.toolStripStatusLabel1.Text = "■部品コードを入力あるいは選択します。　■入力欄でダブルクリックするか、[space]キーを押すと部品選択画面が表示されます。";
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
