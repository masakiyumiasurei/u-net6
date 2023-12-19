using GrapeCity.Win.MultiRow;
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
    public partial class F_受注管理出力 : Form
    {
        Form objForm;

        public F_受注管理出力()
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
            //objForm = objParent;
        }


        private void 閉じるボタン_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private F_検索 SearchForm;

        private void エクセル出力ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show(
                    "このシステムのバージョンでは出力形式が古いため、" +
                    "完全に再現されない情報があります。" + Environment.NewLine + Environment.NewLine +
                    "続けますか？",
                    "Excelへ出力",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    return;
                }

                // Display completion message
                MessageBox.Show("完了しました。", "Excelへ出力", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Close the form
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.GetType().Name}: {ex.Message}", "Excelへ出力", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 印刷ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                this.Visible = false; // フォームを非表示にする
                //objForm.RecordSource = "YourRecordSource"; // 適切なレコードソースを設定

                // "受注管理" レポートをプレビューモードで開く
                //objForm.DoCmd.OpenReport("受注管理", Access.AcView.acViewPreview);

                // フォームを閉じる
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.GetType().Name}: {ex.Message}", "印刷", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
