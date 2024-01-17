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


                // 対象フォームが読み込まれていないときはすぐに終了する
                if (Application.OpenForms["F_ユニット管理"] == null)
                {
                    MessageBox.Show("[ユニット管理]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }


                OriginalClass ofn = new OriginalClass();

                ofn.SetComboBox(請求書様式, "SELECT 氏名 as Value , 氏名 as Display FROM M社員 WHERE (退社 IS NULL) AND ([パート] = 0) AND (ふりがな <> N'ん') OR (退社 IS NULL) AND ([パート] IS NULL) AND (ふりがな <> N'ん') ORDER BY ふりがな");

                //this.非含有証明書.DataSource = new KeyValuePair<long, String>[] {
                //    new KeyValuePair<long, String>(1, "返却済み"),
                //    new KeyValuePair<long, String>(2, "未返却"),
                //    new KeyValuePair<long, String>(3, "未提出"),
                //    new KeyValuePair<long, String>(4, "不明"),
                //    new KeyValuePair<long, String>(0, "指定しない"),
                //};
                //this.非含有証明書.DisplayMember = "Value";
                //this.非含有証明書.ValueMember = "Key";

                //開いているフォームのインスタンスを作成する
                F_ユニット管理 frmTarget = Application.OpenForms.OfType<F_ユニット管理>().FirstOrDefault();

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
