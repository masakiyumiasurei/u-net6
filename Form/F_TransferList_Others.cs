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
    public partial class F_TransferList_Others : Form
    {
        public DateTime dtm支払年月;

        public string str支払先コード;
        public F_TransferList_Others()
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
                if (Application.OpenForms["F_振込一覧"] == null)
                {
                    MessageBox.Show("[振込一覧]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }

                System.Type dgvtype = typeof(DataGridView);
                System.Reflection.PropertyInfo dgvPropertyInfo = dgvtype.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                dgvPropertyInfo.SetValue(dataGridView1, true, null);

                // 表示する月の範囲を設定
                int numberOfMonths = 2;  // 今月を含む前後の月数
                DateTime currentDate = DateTime.Now;

                for (int i = -numberOfMonths + 1; i <= numberOfMonths; i++)
                {
                    DateTime displayDate = currentDate.AddMonths(i);
                    string formattedDate = displayDate.ToString("yyyy/MM");
                    支払年月.Items.Add(formattedDate);
                }

                this.種類コード.DataSource = new KeyValuePair<Int32, String>[] {
                    new KeyValuePair<Int32, String>(1, "現金"),
                    new KeyValuePair<Int32, String>(2, "切手"),
                    new KeyValuePair<Int32, String>(3, "その他"),
                };
                this.種類コード.DisplayMember = "Value";
                this.種類コード.ValueMember = "Key";
                種類コード.SelectedIndex = -1;

                //開いているフォームのインスタンスを作成する
                F_振込一覧 frmTarget = Application.OpenForms.OfType<F_振込一覧>().FirstOrDefault();

                dtm支払年月 = frmTarget.dtm支払年月;
                str支払先コード = frmTarget.str支払先コード;

                Connect();

                支払年月.Text = dtm支払年月.ToString("yyyy/MM");
                支払先コード.Text = str支払先コード;
                支払先名.Text = FunctionClass.GetSupplierName(cn, str支払先コード);


                SetList(dtm支払年月, str支払先コード);

            }
            catch (Exception ex)
            {
                // 例外処理が必要な場合はここで処理を追加
                MessageBox.Show("エラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetList(DateTime TotalDate, string PayeeCode)
        {

            string strSQL = "SELECT Number AS [No.], ItemCode, " +
           "REPLACE(REPLACE(ItemCode, '1', '現金'), '2', '切手') AS 種類, " +
           "Amount AS 金額, Note AS 備考 FROM TransferList_Others " +
           "WHERE PaymentMonth='" + TotalDate + "' AND SupplierCode='" + PayeeCode + "' AND DeleteDate IS NULL";

            Connect();

            DataGridUtils.SetDataGridView(cn, strSQL, dataGridView1);

            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dataGridView1.MultiSelect = true;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;

            MyApi myapi = new MyApi();
            int xSize, ySize, intpixel, twipperdot;

            //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
            intpixel = myapi.GetLogPixel();
            twipperdot = myapi.GetTwipPerDot(intpixel);

            //0列目はaccessでは行ヘッダのため、ずらす
            dataGridView1.Columns[0].Width = 600 / twipperdot;
            dataGridView1.Columns[1].Visible = false;
            dataGridView1.Columns[2].Width = 1200 / twipperdot;
            dataGridView1.Columns[3].Width = 1500 / twipperdot;
            dataGridView1.Columns[4].Width = 3280 / twipperdot;
            dataGridView1.Columns[3].DefaultCellStyle.Format = "C";




        }


        private T Nz<T>(T value)
        {
            if (value == null)
            {
                return default(T);
            }
            return value;
        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {

            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;
            }
        }

        private void 削除ボタン_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count <= 0) return;

            try
            {
                Connect();

                string strSQL = "UPDATE TransferList_Others SET DeleteDate=GETDATE(),DeleteUserCode='" +
                CommonConstants.LoginUserCode + "' " +
                "WHERE PaymentMonth='" + dtm支払年月.ToString("yyyy/MM/dd") +
                "' AND SupplierCode='" + str支払先コード +
                "' AND Number=" + dataGridView1.SelectedRows[0].Cells[0].Value.ToString();

                using (SqlCommand command = new SqlCommand(strSQL, cn))
                {
                    command.ExecuteNonQuery();
                }

                SetList(dtm支払年月, str支払先コード);
            }
            catch (Exception ex)
            {

            }
        }

        private void 追加ボタン_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(種類コード.SelectedValue?.ToString()) || string.IsNullOrEmpty(支払金額.Text)) return;

            try
            {
                Connect();

                dtm支払年月 = DateTime.ParseExact(支払年月.Text + "/1", "yyyy/MM/d", null);
                str支払先コード = 支払先コード.Text;

                string strSQL = "INSERT TransferList_Others SELECT '" +
                    dtm支払年月.ToString("yyyy/MM/dd") + "','" + str支払先コード + "'," +
                    "ISNULL(MAX(Number)+1,1)," +
                    種類コード.SelectedValue + ",'" + 備考.Text + "'," +
                    支払金額.Text + ",NULL,GETDATE(),'" + CommonConstants.LoginUserCode + "',NULL,NULL" +
                    " FROM TransferList_Others WHERE PaymentMonth='" + dtm支払年月.ToString("yyyy/MM/dd") +
                    "' AND SupplierCode='" + str支払先コード + "'";

                using (SqlCommand command = new SqlCommand(strSQL, cn))
                {
                    command.ExecuteNonQuery();
                }

                SetList(dtm支払年月, str支払先コード);
            }
            catch (Exception ex)
            {

            }
        }

        private void 閉じるボタン_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void 支払先参照ボタン_Click(object sender, EventArgs e)
        {
            F_仕入先 targetform = new F_仕入先();
            targetform.args = 支払先コード.Text;
            targetform.ShowDialog();
        }

        private F_検索 SearchForm;
        private void 支払先選択ボタン_Click(object sender, EventArgs e)
        {
            SearchForm = new F_検索();
            SearchForm.FilterName = "支払先名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                string SelectedCode = SearchForm.SelectedCode;

                支払先コード.Text = SelectedCode;
                支払先コード_Validated(sender, e);
            }
        }

        private void 支払先コード_Validated(object sender, EventArgs e)
        {
            Connect();

            str支払先コード = Nz(支払先コード.Text);
            支払先名.Text = FunctionClass.GetSupplierName(cn, str支払先コード);

            if (string.IsNullOrEmpty(支払年月.Text))
            {
                支払年月.Focus();
                return;
            }

            SetList(dtm支払年月, str支払先コード);
        }


        private void 支払先コード_Validating(object sender, CancelEventArgs e)
        {
            if (支払先コード.Modified == false) return;

            if (string.IsNullOrEmpty(支払先コード.Text))
            {
                MessageBox.Show("支払先コードを入力してください。", "Caption", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                e.Cancel = true;
            }
        }

        private void 支払先コード_DoubleClick(object sender, EventArgs e)
        {
            支払先選択ボタン_Click(sender, e);
        }

        private void 支払先コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                TextBox textBox = (TextBox)sender;
                string formattedCode = textBox.Text.Trim().PadLeft(8, '0');

                if (formattedCode != textBox.Text || string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.Text = formattedCode;
                    支払先コード_Validated(sender, e);
                }
            }
        }

        private void 支払先コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                支払先選択ボタン_Click(sender, e);
                e.Handled = true; // イベントの処理が完了したことを示す
            }
        }

        private void 支払年月_SelectedIndexChanged(object sender, EventArgs e)
        {
            dtm支払年月 = DateTime.ParseExact(支払年月.Text + "/1", "yyyy/MM/d", null);
        }

        private void 支払先参照ボタン_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■支払先データを参照します。";
        }

        private void 支払先参照ボタン_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■各種項目の説明";
        }
    }
}
