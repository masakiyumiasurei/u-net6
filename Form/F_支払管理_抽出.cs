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
    public partial class F_支払管理_抽出 : Form
    {

        public F_支払管理_抽出()
        {
            InitializeComponent();
        }

        private bool setflg = false;
        SqlConnection cn;
        public string SP支払年月入力
        {
            get
            {
                return "SELECT A.CloseMonth as Display,A.CloseMonth as Value FROM (SELECT STR({ fn YEAR(DATEADD(month,-12,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-12,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,-11,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-11,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,-10,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-10,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,-9,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-9,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,-8,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-8,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,-7,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-7,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,-6,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-6,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,-5,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-5,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,-4,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-4,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,-3,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-3,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,-2,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-2,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,-1,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,-1,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(GETDATE()) }, 4, 0) + '/' + STR({ fn MONTH(GETDATE()) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,1,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,1,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,2,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,2,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,3,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,3,GETDATE())) }, 2, 0) AS CloseMonth " +
                    "UNION ALL SELECT STR({ fn YEAR(DATEADD(month,4,GETDATE())) }, 4, 0) + '/' + STR({ fn MONTH(DATEADD(month,4,GETDATE())) }, 2, 0) AS CloseMonth " +
                    ") AS A LEFT OUTER JOIN(SELECT STR( YEAR(CloseMonth) ,4 ,0 ) + '/' + STR( MONTH(CloseMonth) ,2 ,0 ) AS CloseMonth " +
                    "FROM T振込繰越残高 GROUP BY CloseMonth) AS B ON A.CloseMonth = B.CloseMonth WHERE B.CloseMonth IS NULL ORDER BY A.CloseMonth";
            }
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

                // 対象フォームが読み込まれていないときはすぐに終了する
                if (Application.OpenForms["F_支払管理"] == null)
                {
                    MessageBox.Show("[支払管理]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }
                OriginalClass ofn = new OriginalClass();
                ofn.SetComboBox(支払年月, SP支払年月入力);

                this.振込指定.DataSource = new KeyValuePair<int, String>[] {
               new KeyValuePair<int, String>(1, "振り込む"),
                new KeyValuePair<int, String>(2, "振り込まない"),
                };
                this.振込指定.DisplayMember = "Value";
                this.振込指定.ValueMember = "Key";

                //開いているフォームのインスタンスを作成する
                F_支払管理 frmTarget = Application.OpenForms.OfType<F_支払管理>().FirstOrDefault();

                this.支払年月.Text = frmTarget.str支払年月;
                if (frmTarget.lng振込指定 == 0)
                {
                    this.振込指定.SelectedIndex = -1;
                }
                else
                {
                    this.振込指定.SelectedValue = frmTarget.lng振込指定;
                }

                this.支払先コード.Text = frmTarget.str支払先コード.ToString();
                this.支払先名.Text = frmTarget.str支払先名.ToString();



                switch (frmTarget.lng確定指定)
                {
                    case 1:
                        確定指定Button1.Checked = true;
                        break;
                    case 2:
                        確定指定Button2.Checked = true;
                        break;
                    case 0:
                        確定指定Button3.Checked = true;
                        break;

                    default:
                        break;
                }

                switch (frmTarget.lng承認指定)
                {
                    case 1:
                        承認指定button1.Checked = true;
                        break;
                    case 2:
                        承認指定button2.Checked = true;
                        break;
                    case 0:
                        承認指定button3.Checked = true;
                        break;

                    default:
                        break;
                }

                switch (frmTarget.lng削除指定)
                {
                    case 1:
                        削除指定Button1.Checked = true;
                        break;
                    case 2:
                        削除指定Button2.Checked = true;
                        break;
                    case 0:
                        削除指定Button3.Checked = true;
                        break;

                    default:

                        break;
                }

            }
            catch (Exception ex)
            {
                // 例外処理が必要な場合はここで処理を追加
                MessageBox.Show("エラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 抽出ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                F_支払管理? frmTarget = Application.OpenForms.OfType<F_支払管理>().FirstOrDefault();

                frmTarget.str支払年月 = Nz(支払年月.Text);
                frmTarget.lng振込指定 = (振込指定.SelectedValue as int?) ?? 0;
                frmTarget.str支払先コード = Nz(支払先コード.Text);
                frmTarget.str支払先名 = Nz(支払先名.Text);

                if (確定指定Button1.Checked)
                {
                    frmTarget.lng確定指定 = 1;
                }
                else if (確定指定Button2.Checked)
                {
                    frmTarget.lng確定指定 = 2;
                }
                else if (確定指定Button3.Checked)
                {
                    frmTarget.lng確定指定 = 0;
                }

                if (承認指定button1.Checked)
                {
                    frmTarget.lng承認指定 = 1;
                }
                else if (承認指定button2.Checked)
                {
                    frmTarget.lng承認指定 = 2;
                }
                else if (承認指定button3.Checked)
                {
                    frmTarget.lng承認指定 = 0;
                }

                if (削除指定Button1.Checked)
                {
                    frmTarget.lng削除指定 = 1;
                }
                else if (削除指定Button2.Checked)
                {
                    frmTarget.lng削除指定 = 2;
                }
                else if (削除指定Button3.Checked)
                {
                    frmTarget.lng削除指定 = 0;
                }

                long cnt = frmTarget.DoUpdate();

                if (cnt == 0)
                {
                    MessageBox.Show("抽出条件に一致するデータはありません。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);

                    return;
                }
                else if (cnt < 0)
                {
                    MessageBox.Show("エラーが発生したため、抽出できませんでした。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
                else
                {
                    this.Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(this.Name + "_抽出ボタン_Click - " + ex.Message);
                MessageBox.Show("エラーが発生しました。\n" + ex.Message, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void キャンセルボタン_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private F_検索 SearchForm;

        private void 支払先選択ボタン_Click(object sender, EventArgs e)
        {
            SearchForm = new F_検索();
            SearchForm.FilterName = "支払先名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                setflg = true;
                string SelectedCode = SearchForm.SelectedCode;
                支払先コード.Text = SelectedCode;
                支払先名.Text = GetPayee(SelectedCode);
                支払先名.Focus();
            }
        }

        private void 支払先コード_DoubleClick(object sender, EventArgs e)
        {
            SearchForm = new F_検索();
            SearchForm.FilterName = "支払先名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                setflg = true;
                string SelectedCode = SearchForm.SelectedCode;
                支払先コード.Text = SelectedCode;
                支払先名.Focus();
            }
        }

        private void 支払先コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Space:
                    e.Handled = true;
                    SearchForm = new F_検索();
                    SearchForm.FilterName = "支払先名フリガナ";
                    if (SearchForm.ShowDialog() == DialogResult.OK)
                    {
                        setflg = true;
                        string SelectedCode = SearchForm.SelectedCode;
                        支払先コード.Text = SelectedCode;
                        支払先名.Focus();
                    }
                    break;
            }
        }
        private void 支払先コード_Validated(object sender, EventArgs e)
        {
            setflg = true;
            this.支払先名.Text = GetPayee(Nz(this.支払先コード.Text));
        }

        private void 支払先コード_KeyDown(object sender, KeyEventArgs e)
        {
            // 入力された値がエラー値の場合、Textプロパティが設定できなくなるときの対処
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Return:
                        string strCode = ((Control)sender).Text;
                        if (string.IsNullOrEmpty(strCode)) return;

                        strCode = strCode.PadLeft(8, '0');
                        if (strCode != ((Control)sender).Text)
                        {
                            ((Control)sender).Text = strCode;
                        }
                        支払先名.Focus();
                        break;
                }
            }
            catch (Exception ex)
            {
                // エラーハンドリング: 例外が発生した場合に処理を行う
                Console.WriteLine("エラーが発生しました: " + ex.Message);
            }
        }

        private void 支払先名_TextChanged(object sender, EventArgs e)
        {
            if (!setflg)
            {
                支払先コード.Text = null;
            }
            setflg = false;
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



        private void Form_KeyDown(object sender, KeyEventArgs e)
        {

            bool intShiftDown = (Control.ModifierKeys & Keys.Shift) == Keys.Shift;

            if (intShiftDown)
            {
                Debug.Print(Name + " - Shiftキーが押されました");
            }

            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;
                case Keys.Space: //コンボボックスならドロップダウン
                    {
                        Control activeControl = this.ActiveControl;
                        if (activeControl is System.Windows.Forms.ComboBox)
                        {
                            System.Windows.Forms.ComboBox activeComboBox = (System.Windows.Forms.ComboBox)activeControl;
                            activeComboBox.DroppedDown = true;
                        }
                    }
                    break;
            }
        }

        private void キャンセルボタン_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private string GetPayee(string payeeCode)
        {
            Connect();
            string payeeName = "";

            if (string.IsNullOrEmpty(payeeCode))
                return payeeName;

            string query = "SELECT 仕入先名 AS 支払先名 FROM M仕入先 WHERE 仕入先コード = @PayeeCode";

            using (SqlCommand command = new SqlCommand(query, cn))
            {
                command.Parameters.AddWithValue("@PayeeCode", payeeCode);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        payeeName = reader["支払先名"].ToString();
                    }
                }
            }

            return payeeName;
        }

        private void 支払先参照ボタン_Click(object sender, EventArgs e)
        {
            F_仕入先 fm = new F_仕入先();
            fm.args = 支払先コード.Text;
            fm.ShowDialog();
        }

        private void 振込指定_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■振込の要/不要を指定します。　■指定しないときは入力欄を空白にしてください。";
        }
    }
}
