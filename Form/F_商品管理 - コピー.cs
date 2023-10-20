using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace u_net
{
    public partial class F_商品管理_ : Form
    {
        int intWindowHeight = 0;
        int intWindowWidth = 0;
        public F_商品管理_()
        {
            InitializeComponent();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            this.q商品管理TableAdapter.Fill(this.newDataSet.Q商品管理);

            intWindowHeight = this.Height;
            intWindowWidth = this.Width;

            // DataGridViewの設定
            dataGridView1.AllowUserToResizeColumns = true;
            dataGridView1.Font = new System.Drawing.Font("MS ゴシック", 10);
            dataGridView1.DefaultCellStyle.SelectionBackColor = System.Drawing.Color.FromArgb(210, 210, 255);
            dataGridView1.DefaultCellStyle.SelectionForeColor = System.Drawing.Color.Black;
            dataGridView1.GridColor = System.Drawing.Color.FromArgb(230, 230, 230);
            dataGridView1.ColumnHeadersDefaultCellStyle.Font = new System.Drawing.Font("MS ゴシック", 10);
            dataGridView1.DefaultCellStyle.Font = new System.Drawing.Font("MS ゴシック", 10);
            dataGridView1.DefaultCellStyle.ForeColor = System.Drawing.Color.Black;

            // 列の幅を設定
            dataGridView1.Columns[0].Width = 500;
            dataGridView1.Columns[1].Width = 1150;
            dataGridView1.Columns[2].Width = 3500;
            dataGridView1.Columns[3].Width = 1500;
            dataGridView1.Columns[4].Width = 500;
            dataGridView1.Columns[5].Width = 1350;
            dataGridView1.Columns[6].Width = 1350;
            dataGridView1.Columns[7].Width = 2200;
            dataGridView1.Columns[8].Width = 1300;
            dataGridView1.Columns[9].Width = 500;
            dataGridView1.Columns[10].Width = 500;
            dataGridView1.Columns[11].Width = 500;
            // dataGridView1.Columns[12].Width = 500;
        }
    }
}
