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
    public partial class F_検索コード : Form
    {
        public F_検索コード()
        {
            InitializeComponent();
        }
        private void Form_Load(object sender, EventArgs e)
        {
            F_商品管理 frmTarget = Application.OpenForms.OfType<F_商品管理>().FirstOrDefault();
            this.検索コード.Text = frmTarget.str検索コード;
        }
    }
}
