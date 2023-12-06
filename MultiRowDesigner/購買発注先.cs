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
    public partial class 購買発注先 : UserControl
    {
        public GcMultiRow Detail
        {
            get
            {
                return gcMultiRow1;
            }
        }

        public 購買発注先()
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
                        case "発注書プレビューボタン":
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

    }
}
