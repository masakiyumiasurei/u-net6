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
    public partial class test : Form
    {
        public test()
        {
            InitializeComponent();
        }

        private void m商品BindingNavigatorSaveItem_Click(object sender, EventArgs e)
        {
            this.Validate();
            this.m商品BindingSource.EndEdit();
            this.m商品TableAdapter.Update(this.uiDataSet);
            this.tableAdapterManager.UpdateAll(this.uiDataSet);

        }

        private void test_Load(object sender, EventArgs e)
        {
            // TODO: このコード行はデータを 'uiDataSet.M商品' テーブルに読み込みます。必要に応じて移動、または削除をしてください。
            this.m商品TableAdapter.Fill(this.uiDataSet.M商品);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.m商品BindingSource.AddNew();
        }
    }
}
