using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using u_net.Public;

namespace u_net
{
    public partial class F_締め処理 : Form
    {

        public F_締め処理()
        {
            InitializeComponent();
        }

        SqlConnection cn;

        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {

                foreach (Control control in Controls)
                {
                    control.PreviewKeyDown += OriginalClass.ValidateCheck;
                }


                //// 対象フォームが読み込まれていないときはすぐに終了する
                //if (Application.OpenForms["F_請求処理"] == null)
                //{
                //    MessageBox.Show("[請求処理]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //    this.Close();
                //    return;
                //}

                締め処理.Checked = true;
                印刷.Checked = true;
                請求書様式.Text = "請求明細書";
                
            }
            catch (Exception ex)
            {
                // 例外処理が必要な場合はここで処理を追加
                MessageBox.Show("エラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void キャンセルボタン_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        // Nz メソッドの代替
        private T Nz<T>(T value)
        {
            if (value == null)
            {
                return default(T);
            }
            return value;
        }

        private F_カレンダー dateSelectionForm;

        private void 開始ボタン_Click(object sender, EventArgs e)
        {
            F_請求処理 frmTarget = Application.OpenForms.OfType<F_請求処理>().FirstOrDefault();
            frmTarget.DoStart(締め処理.Checked, 印刷.Checked);
            this.Close();
        }

        private void F_締め処理_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;
            }

        }
    }
}
