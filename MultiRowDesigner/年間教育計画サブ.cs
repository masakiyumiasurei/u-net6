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
using u_net;
using u_net.Public;

namespace MultiRowDesigner
{
    public partial class 年間教育計画サブ : UserControl
    {
        public GcMultiRow Detail
        {
            get
            {
                return gcMultiRow1;
            }
        }

        private SqlConnection? cn;
        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        public 年間教育計画サブ()
        {
            InitializeComponent();
        }

        private void ユニット明細_Load(object sender, EventArgs e)
        {
            gcMultiRow1.ShortcutKeyManager.Unregister(Keys.Enter);
            gcMultiRow1.ShortcutKeyManager.Register(SelectionActions.MoveToNextCell, Keys.Enter);

            

        }


        private void gcMultiRow1_CellContentClick(object sender, CellEventArgs e)
        {
            int idx = gcMultiRow1.CurrentRow.Index;

            F_年間教育計画? fm = Application.OpenForms.OfType<F_年間教育計画>().FirstOrDefault();

            fm.年度.Text= gcMultiRow1.Rows[idx].Cells["年度"].Value.ToString();
            fm.登録コード.Text = gcMultiRow1.Rows[idx].Cells["登録コード"].Value.ToString();
            fm.教育名.Text = gcMultiRow1.Rows[idx].Cells["教育名"].Value.ToString();
            fm.教育機関名.Text = gcMultiRow1.Rows[idx].Cells["教育機関名"].Value.ToString();
            fm.受講者コード.Text = gcMultiRow1.Rows[idx].Cells["受講者コード"].Value.ToString();
            fm.受講者名.Text = gcMultiRow1.Rows[idx].Cells["受講者名"].Value.ToString();
            fm.日付1.Text = Convert.ToDateTime(gcMultiRow1.Rows[idx].Cells["日付1"].Value).ToString("yyyy/MM/dd");
            fm.日付2.Text = Convert.ToDateTime(gcMultiRow1.Rows[idx].Cells["日付2"].Value).ToString("yyyy/MM/dd");
            fm.備考.Text = gcMultiRow1.Rows[idx].Cells["備考"].Value.ToString();
            fm.キャンセル待ち.Checked = (bool)gcMultiRow1.Rows[idx].Cells["キャンセル待ち"].Value;
            fm.キャンセル.Checked = (bool)gcMultiRow1.Rows[idx].Cells["キャンセル"].Value;            

        } 

    }
}
