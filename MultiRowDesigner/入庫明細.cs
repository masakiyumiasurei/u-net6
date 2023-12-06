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
    public partial class 入庫明細 : UserControl
    {
        public GcMultiRow Detail
        {
            get
            {
                return gcMultiRow1;
            }
        }

        public 入庫明細()
        {
            InitializeComponent();
        }

        private void gcMultiRow1_CellClick(object sender, CellEventArgs e)
        {
            switch (gcMultiRow1.CurrentCell)
            {

                case ButtonCell:
                    switch (e.CellName)
                    {
                        case "メーカー名ボタン":
                            
                            break;

                        case "型番ボタン":

                            break;

                        case "納期ボタン":

                            break;

                        case "買掛区分ボタン":

                            break;

                        case "品名ボタン":

                            break;

                        case "部品コードボタン":
                   
                            break;

                    }
                    break;
            }
        }

        private void gcMultiRow1_CellValidating(object sender, CellValidatingEventArgs e)
        {

        }

        private void gcMultiRow1_CellValidated(object sender, CellEventArgs e)
        {

        }

        private void gcMultiRow1_CellEnter(object sender, CellEventArgs e)
        {

        }
    }
}
