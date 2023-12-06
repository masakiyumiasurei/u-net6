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
    public partial class 購買明細 : UserControl
    {
        public GcMultiRow Detail
        {
            get
            {
                return gcMultiRow1;
            }
        }

        public 購買明細()
        {
            InitializeComponent();
        }

        private void gcMultiRow1_CellEnter(object sender, CellEventArgs e)
        {
            u_net.F_購買 objForm = (u_net.F_購買)Application.OpenForms["F_購買"];

            if (objForm != null)
            {
                switch (gcMultiRow1.CurrentCell)
                {
                    //テキストボックスEnter時の処理
                    case TextBoxCell:
                        switch (e.CellName)
                        {
                            case "発注納期":
                                objForm.toolStripStatusLabel1.Text = "■部品の発注納期を入力します。　■カレンダーから選択するには入力欄をダブルクリックします。";
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
