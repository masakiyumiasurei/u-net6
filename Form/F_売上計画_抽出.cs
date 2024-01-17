using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data.OleDb;
using Microsoft.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using u_net.Public;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json.Linq;
using System.Drawing.Imaging;
using System.Drawing.Printing;

namespace u_net
{
    public partial class F_売上計画_抽出 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "売上実績一覧 - 抽出";
        private int selected_frame = 0;

        public F_売上計画_抽出()
        {
            this.Text = "売上実績一覧 - 抽出";       // ウィンドウタイトルを設定
            this.MaximizeBox = false;  // 最大化ボタンを無効化
            this.MinimizeBox = false; //最小化ボタンを無効化

            InitializeComponent();

        }

        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        public void CommonConnect()
        {
            CommonConnection connectionInfo = new CommonConnection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        //SqlConnection cn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter();

        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (Control control in Controls)
                {
                    control.PreviewKeyDown += OriginalClass.ValidateCheck;
                }

                // 対象フォームが読み込まれていないときはすぐに終了する
                if (Application.OpenForms["F_売上計画"] == null)
                {
                    MessageBox.Show("[売上計画]画面が起動していない状態では実行できません。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.Close();
                    return;
                }

                Connect();

                using (SqlCommand cmd = new SqlCommand("SP売上年度", cn))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    SqlDataReader reader = cmd.ExecuteReader();

                    // レコードセットを設定
                    DataTable dataTable = new DataTable();
                    dataTable.Load(reader);

                    集計年度.DisplayMember = "売上年度";
                    集計年度.ValueMember = "売上年度";
                    集計年度.DataSource = dataTable;


                }

                OriginalClass ofn = new OriginalClass();
                ofn.SetComboBox(担当者コード, "SELECT [社員コード] as Value, 社員コード as Display, 氏名 AS Display2 FROM M社員 WHERE ([パート] = 0) AND (退社 IS NULL) AND (ふりがな <> N'ん') AND (部 = N'営業部') AND (削除日時 IS NULL) ORDER BY ふりがな");
                担当者コード.DrawMode = DrawMode.OwnerDrawFixed;

                //開いているフォームのインスタンスを作成する
                F_売上計画 frmTarget = Application.OpenForms.OfType<F_売上計画>().FirstOrDefault();

                //F_売上計画クラスからデータを取得し、現在のフォームのコントロールに設定

                FunctionClass fn = new FunctionClass();


                this.集計年度.SelectedValue = fn.Zn(frmTarget.TheYear);
                this.顧客コード.Text = frmTarget.str顧客コード;
                this.顧客名.Text = frmTarget.str顧客名;
                if (frmTarget.SalesmanCode != "")
                {
                    this.担当者コード.SelectedValue = fn.Zn(frmTarget.SalesmanCode);
                }
                else
                {
                    this.担当者コード.SelectedIndex = -1;
                }
                担当者名.Text = FunctionClass.GetUserFullName(cn, 担当者コード.Text);


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

                if (集計年度.SelectedIndex == -1)
                {
                    MessageBox.Show("抽出できませんでした。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    集計年度.Focus();
                    集計年度.DroppedDown = true;
                    return;

                }

                F_売上計画? frmTarget = Application.OpenForms.OfType<F_売上計画>().FirstOrDefault();

                frmTarget.TheYear = Nz(Convert.ToInt32(集計年度.SelectedValue));
                frmTarget.str顧客コード = Nz(this.顧客コード.Text);
                frmTarget.str顧客名 = Nz(this.顧客名.Text);
                frmTarget.SalesmanCode = Nz(担当者コード.SelectedValue.ToString());


                if (frmTarget.DoUpdate())
                {

                    frmTarget.コマンドコピー.Enabled = true;
                    frmTarget.コマンド更新.Enabled = true;
                    frmTarget.コマンド出力.Enabled = true;
                }
                else
                {
                    MessageBox.Show("抽出できませんでした。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    frmTarget.コマンドコピー.Enabled = false;
                    frmTarget.コマンド更新.Enabled = false;
                    frmTarget.コマンド出力.Enabled = false;

                }

                Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(this.Name + "_抽出ボタン_Click - " + ex.Message);
                MessageBox.Show("エラーが発生しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void キャンセルボタン_Click(object sender, EventArgs e)
        {
            Close();
        }

        F_検索 SearchForm = new F_検索();
        private void 顧客コード検索ボタン_Click(object sender, EventArgs e)
        {
            SearchForm = new F_検索();
            SearchForm.FilterName = "顧客名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                string SelectedCode = SearchForm.SelectedCode;

                顧客コード.Text = SelectedCode;
                顧客コード_Validated(sender, e);
            }
        }

        private void 顧客コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            switch (e.KeyChar)
            {
                case (char)Keys.Space:
                    e.Handled = true;
                    顧客コード検索ボタン_Click(sender, e);
                    break;
            }
        }
        bool setflg = false;
        private void 顧客コード_Validated(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(顧客コード.Text)) return;
            Connect();
            setflg = true;
            顧客名.Text = FunctionClass.GetCustomerName(cn, 顧客コード.Text);
            setflg = false;
        }



        private void 顧客コード_DoubleClick(object sender, EventArgs e)
        {
            顧客コード検索ボタン_Click(sender, e);
        }

        private void 顧客コード_TextChanged(object sender, EventArgs e)
        {

            FunctionClass.LimitText(sender as Control, 8);
        }

        private void 顧客コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                System.Windows.Forms.Control control = (System.Windows.Forms.Control)sender;
                string strCode = control.Text.Trim();

                if (string.IsNullOrEmpty(strCode))
                {
                    return;
                }

                strCode = strCode.PadLeft(8, '0');
                if (strCode != control.Text)
                {
                    control.Text = strCode;
                }
            }
        }



        private void 顧客名_TextChanged(object sender, EventArgs e)
        {
            if (setflg) return;
            顧客コード.Text = null;
        }
















        private void 顧客名_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■顧客名を入力します。名前や事業所名の一部でも構いません。";
        }

        private void 顧客名_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
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

        private void 担当者コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            担当者名.Text = ((DataRowView)担当者コード.SelectedItem)?.Row.Field<String>("Display2")?.ToString();
        }

        private void 担当者コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 50, 250 }, new string[] { "Display", "Display2" });
            担当者コード.Invalidate();
            担当者コード.DroppedDown = true;
        }

        private void 担当者コード_TextChanged(object sender, EventArgs e)
        {
            if (担当者コード.SelectedValue == null)
            {
                担当者名.Text = null;
            }
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

    }
}
