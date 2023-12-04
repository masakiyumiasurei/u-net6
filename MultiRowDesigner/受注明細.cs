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
    public partial class 受注明細 : UserControl
    {
        public GcMultiRow Detail
        {
            get
            {
                return gcMultiRow1;
            }
        }

        public 受注明細()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 明細部の受注コードと受注版数を更新する
        /// </summary>
        /// <param name="code">受注コード</param>
        /// <param name="edition">受注版数</param>
        public void UpdateCodeAndEdition(string code, int edition)
        {
            for (int i = 0; i < Detail.RowCount - 1; i++)
            {
                Detail.Rows[i].Cells["受注コード"].Value = code;
                Detail.Rows[i].Cells["受注版数"].Value = edition;
            }
        }
    }
}
