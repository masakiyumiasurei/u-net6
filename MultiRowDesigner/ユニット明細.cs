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
    public partial class ユニット明細 : UserControl
    {
        public GcMultiRow Detail
        {
            get
            {
                return gcMultiRow1;
            }
        }

        public ユニット明細()
        {
            InitializeComponent();
        }

    }
}
