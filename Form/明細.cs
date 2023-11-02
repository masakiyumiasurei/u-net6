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
    public partial class 明細 : Form
    {
        public 明細()
        {
            InitializeComponent();
        }

        private void 明細_Load(object sender, EventArgs e)
        {
            string code = "01";
            //this.m商品分類TableAdapter.FillBy(this.uiDataSet.M商品分類,code);
             this.mshomeisaiTableAdapter.Fill(this.newDataSet1.M商品明細, "00000373");
            this.mtaniTableAdapter.Fill(this.newDataSet1.M単位, 1);
        }
    }
}
