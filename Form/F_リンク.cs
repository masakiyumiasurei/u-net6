using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using u_net.Public;

namespace u_net
{
    public partial class F_リンク : Form
    {
        SqlConnection cn;
        public string args = "";
        int lngGroupCode = 0;

        public F_リンク()
        {
            InitializeComponent();
        }

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
                lngGroupCode = GetGroupCode(args);

                グループ名.Text = GetGroupName(lngGroupCode);

                SetSourceDocument(args);


                Connect();
                string strSQL = "SELECT 明細番号,行番号 AS [No.],文書コード,版数,文書名,件名 " +
                                "FROM Vグループ明細 " +
                                "WHERE グループコード=" + lngGroupCode + " AND 文書コード<>'" + args + "' " +
                                "ORDER BY 行番号";
                DataGridUtils.SetDataGridView(cn, strSQL, this.dataGridView1);

                dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                dataGridView1.Rows[0].Selected = true;

                //MyApi myapi = new MyApi();
                //int xSize, ySize, intpixel, twipperdot;
                               
                //intpixel = myapi.GetLogPixel();
                //twipperdot = myapi.GetTwipPerDot(intpixel);

                dataGridView1.Columns[0].Visible = false;
                dataGridView1.Columns[1].Width = 32;
                dataGridView1.Columns[2].Width = 110;
                dataGridView1.Columns[3].Visible = false;
                dataGridView1.Columns[4].Width = 200;
                dataGridView1.Columns[5].Width = 270;

            }
            catch (Exception ex)
            {
                // 例外処理が必要な場合はここで処理を追加
                MessageBox.Show("エラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private int GetGroupCode(string code)
        {
            int groupCode = 0;
            Connect();
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT グループコード FROM Mグループ明細 WHERE 文書コード = @Code", cn))
                {
                    cmd.Parameters.AddWithValue("@Code", code);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            groupCode = Convert.ToInt32(reader["グループコード"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetGroupCode: {ex.Message}");
            }

            return groupCode;
        }

        private string GetGroupName(int code)
        {
            string GetGroupName = "";
            Connect();
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT グループ名 FROM Mグループ WHERE グループコード = @Code", cn))
                {
                    cmd.Parameters.AddWithValue("@Code", code);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            GetGroupName = reader["グループ名"]?.ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetGroupCode: {ex.Message}");
            }

            return GetGroupName;
        }

        private void SetSourceDocument(string code)
        {
            Connect();

            string strSQL = "SELECT 文書コード,版数,文書名,件名 " +
                            "FROM Vグループ明細 " +
                            "WHERE 文書コード=@Code";

            using (SqlCommand cmd = new SqlCommand(strSQL, cn))
            {
                cmd.Parameters.AddWithValue("@Code", code);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // this.行番号.Text = this.関連文書.Columns[1].Text; // 行番号の設定
                        this.コード.Text = reader["文書コード"].ToString();
                        this.文書名.Text = reader["文書名"].ToString();
                        this.件名.Text = reader["件名"].ToString();
                    }
                }
            }

        }

        private void 閉じるボタン_MouseClick(object sender, MouseEventArgs e)
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

        private void 開くボタン_Click(object sender, EventArgs e)
        {
            string strCode = dataGridView1.SelectedRows[0].Cells[2].Value?.ToString();
            //string strCode = dataGridView1.CurrentRow.Cells[2].Value?.ToString();
            int intEdition = Convert.ToInt32(dataGridView1.SelectedRows[0].Cells[3].Value);
            //int intEdition = Convert.ToInt32(dataGridView1.CurrentRow.Cells[3].Value);

            if (string.IsNullOrEmpty(strCode) || intEdition == 0)
            {
                return;
            }

            if (!DoOpen(strCode, intEdition))
            {
                MessageBox.Show("現在のバージョンでは選択文書の参照は対応していません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            string strCode = dataGridView1.SelectedRows[0].Cells[2].Value?.ToString();


            if (string.IsNullOrEmpty(strCode))
            {
                return;
            }

            if (!DoOpen(strCode, 1))
            {
                MessageBox.Show("現在のバージョンでは選択文書の参照は対応していません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void dataGridView1_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    if (ActiveControl == this.dataGridView1)
                    {
                        string strCode = dataGridView1.SelectedRows[0].Cells[2].Value?.ToString();
                        if (string.IsNullOrEmpty(strCode))
                        {
                            return;
                        }
                        if (!DoOpen(strCode, 1))
                        {
                            MessageBox.Show("現在のバージョンでは選択文書の参照は対応していません。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    break;
            }
        }
        private bool DoOpen(string documentCode, int documentEdition)
        {
            bool isOpen = false;

            switch (documentCode.Substring(0, 3))
            {
                case CommonConstants.CH_DOCUMENT:
                    Form documentForm = Application.OpenForms.Cast<Form>()
                        .FirstOrDefault(f => f.Name == "F_文書");

                    if (documentForm != null)
                    {
                        // 同一フォームが既に開かれている場合
                        F_文書 bunshoform2 = new F_文書();
                        bunshoform2.args = $"{documentCode},{documentEdition}";
                        bunshoform2.ShowDialog();
                    }
                    else
                    {
                        // 同一フォームが開かれていない場合
                        F_文書 bunshoform = new F_文書();
                        bunshoform.args = $"{documentCode},{documentEdition}";
                        bunshoform.ShowDialog();
                    }

                    isOpen = true;
                    break;

                case CommonConstants.CH_ESTIMATE:
                    F_見積 fm = new F_見積();
                    fm.varOpenArgs = $"{documentCode},{documentEdition}";
                    fm.ShowDialog();
                    isOpen = true;
                    break;

                case "ORD":
                    F_発注 fm2 = new F_発注();
                    fm2.args = $"{documentCode},{documentEdition}";
                    fm2.ShowDialog();
                    isOpen = true;
                    break;
            }

            return isOpen;
        }

        private void リンク解除ボタン_Click(object sender, EventArgs e)
        {
            int lngNumber = Convert.ToInt32(Nz(dataGridView1.SelectedRows[0].Cells[0].Value));
            string strCode1 = Nz(dataGridView1.SelectedRows[0].Cells[2].Value.ToString());

            if (lngGroupCode == 0 || lngNumber == 0 || strCode1 == "")
            {
                return;
            }

            if (MessageBox.Show("文書 " + strCode1 + " をグループから削除しますか？", "確認",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            Connect();
            if (FunctionClass.DeleteGroupMember(lngGroupCode, lngNumber, cn))
            {               
                dataGridView1.Rows.RemoveAt(dataGridView1.SelectedRows[0].Index);

                if (dataGridView1.RowCount == 0)
                {
                    this.開くボタン.Enabled = false;
                }
                else
                {
                    dataGridView1.Rows[0].Selected = true;
                }
            }
            else
            {
                MessageBox.Show("削除できませんでした。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        
    }
}
