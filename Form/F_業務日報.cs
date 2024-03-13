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
using GrapeCity.Win.MultiRow;
using static System.ComponentModel.Design.ObjectSelectorEditor;

namespace u_net
{
    public partial class F_業務日報 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "業務日報";
        private int selected_frame = 0;
        private string str状況 = "";
        private bool cmbflg = true;
        int intWindowHeight;
        int intWindowWidth;

        public F_業務日報()
        {
            this.Text = "業務日報";       // ウィンドウタイトルを設定
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
            foreach (Control control in Controls)
            {
                control.PreviewKeyDown += OriginalClass.ValidateCheck;
            }

            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");

            try
            {


                //実行中フォーム起動
                string LoginUserCode = CommonConstants.LoginUserCode;
                LocalSetting localSetting = new LocalSetting();
                localSetting.LoadPlace(LoginUserCode, this);

                OriginalClass ofn = new OriginalClass();
                ofn.SetComboBox(日付, "SELECT TOP 5 convert(date,日付) as Value, format(convert(date,日付),'yyyy/MM/dd') as Display,LEFT(DATENAME(weekday, 日付), 1) as Display2" +
                    " FROM T業務日報 " +
                    "WHERE 日付 < getdate() GROUP BY 日付 ORDER BY 日付 DESC; ");
                日付.DrawMode = DrawMode.OwnerDrawFixed;
                日付.DropDownWidth = 150;

                ofn.SetComboBox(社員コード, "SELECT 社員コード as Value, 氏名 as Display " +
                    "FROM M社員 WHERE [ふりがな]<>'ん' And 業務日報順序 Is Not Null And Not (部='製造部' And [パート]=1) ORDER BY [ふりがな]; ");
                //社員コード.SelectedIndex = -1;
                this.SuspendLayout();

                社員コード.SelectedValue = LoginUserCode;
                日付.Text = DateTime.Now.Date.ToString("yyyy/MM/dd");
                日報コード.Text = GetCode(DateTime.Now, 社員コード.SelectedValue.ToString().PadLeft(3, '0'));

                SetRowSource(DateTime.Now.Date);
                str状況 = 社員コード.SelectedValue.ToString();

                業務日報明細実績1.Detail.AllowUserToAddRows = false;
                業務日報明細実績1.Detail.AllowUserToDeleteRows = false;
                業務日報明細実績1.Detail.ReadOnly = true;

                業務日報明細予定1.Detail.AllowUserToAddRows = false;
                業務日報明細予定1.Detail.AllowUserToDeleteRows = false;
                業務日報明細予定1.Detail.ReadOnly = true;

                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("初期化に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            finally
            {
                cmbflg = false;
                this.ResumeLayout();
                fn.WaitForm.Close();
            }
        }

        private string GetCode(DateTime dteInput, string strInput)
        {
            //long ticks = new DateTime(dteInput.Year, dteInput.Month, dteInput.Day).Ticks;
            int tmpint = (int)dteInput.ToOADate();
            return tmpint.ToString() + strInput;

        }

  

        private void SetRowSource(DateTime targetDate)
        {
            string strTargetDate = targetDate.ToString("yyyy/MM/dd");

            Connect();

            string query = "SELECT " +
                           "M社員.部," +
                           "M社員.氏名," +
                           "V業務日報状況_登録済み.確定, " +
                           "V業務日報状況_登録済み.社長, " +
                           "V業務日報状況_登録済み.専務, " +
                           "V業務日報状況_登録済み.村中, " +
                           "V業務日報状況_登録済み.菅野, " +
                           "V業務日報状況_登録済み.高松, " +
                           "V業務日報状況_登録済み.坂口, " +
                           "COALESCE(V業務日報状況_登録済み.日付, CONVERT(DATETIME, '" + strTargetDate + "', 102)) AS 日付," +
                           "M社員.社員コード " +
                           "FROM M社員 LEFT JOIN V業務日報状況_登録済み ON V業務日報状況_登録済み.社員コード = M社員.社員コード " +
                           "AND V業務日報状況_登録済み.日付 = CONVERT(DATETIME, '" + strTargetDate + "', 102) " +
                           "WHERE 業務日報順序 IS NOT NULL AND NOT(部 = '製造部' AND パート = 1) " +
                           "ORDER BY M社員.ふりがな";

            DataGridUtils.SetDataGridView(cn, query, this.状況);

            状況.Columns[0].Width = 60;
            状況.Columns[1].Width = 100;
            状況.Columns[2].Width = 40;
            状況.Columns[3].Width = 40;
            状況.Columns[4].Width = 40;
            状況.Columns[5].Width = 40;
            状況.Columns[6].Width = 40;
            状況.Columns[7].Width = 40;
            状況.Columns[8].Width = 40;
            状況.Columns[9].Visible = false; //日付
            状況.Columns[10].Visible = false;//社員コード

            状況.AllowUserToResizeColumns = true;
            状況.Font = new Font("MS ゴシック", 9);
            状況.DefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 210, 255);
            状況.DefaultCellStyle.SelectionForeColor = Color.Black;
            状況.GridColor = Color.FromArgb(230, 230, 230);
            状況.ColumnHeadersDefaultCellStyle.Font = new Font("MS ゴシック", 9);
            状況.DefaultCellStyle.Font = new Font("MS ゴシック", 9);

        }

        public bool LoadHeader()
        {
            try
            {
                Connect();
                string strSQL;
                strSQL = "SELECT * FROM V業務日報 WHERE 日報コード='" + 日報コード.Text + "'";

                確定コード.Text = null;

                if (!VariableSet.SetTable2Form(this, strSQL, cn)) return false;


                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("読み込み時エラーです" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }


        public bool LoadDetails(string strSQL, GcMultiRow multiRow)
        {
            try
            {
                Connect();

                using (SqlCommand command = new SqlCommand(strSQL, cn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        multiRow.DataSource = dataTable;
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("読み込み時エラーです" + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return false;
        }

        private void UpdatedControl(System.Windows.Forms.Control controlObject)
        {
            FunctionClass fn = new FunctionClass();
            try
            {


                string strSQL = "";
                string strSQL2 = "";

                switch (controlObject.Name)
                {
                    case "日付":
                    case "社員コード":
                        Connect();
                        fn.DoWait("読み込んでいます...");
                        if (DateTime.TryParse(日付.Text, out DateTime date))
                            日報コード.Text = GetCode(date, 社員コード.SelectedValue.ToString().PadLeft(3, '0'));

                        // ヘッダ部の表示
                        if (controlObject.Name == "日付" && DateTime.TryParse(日付.Text, out DateTime parsedDate))
                            SetRowSource(parsedDate);

                        str状況 = 社員コード.SelectedValue.ToString();

                        if (!LoadHeader()) throw new Exception("初期化に失敗しました。");

                        strSQL = $"SELECT 明細番号,実績内容,項目 " +
                              $"FROM T業務日報明細実績 left join M業務日報項目 on T業務日報明細実績.項目コード=M業務日報項目.項目コード " +
                              $"WHERE 日報コード= '{日報コード.Text}' ORDER BY 明細番号";

                        strSQL2 = $"SELECT 明細番号,予定内容,項目 " +
                                       $"FROM T業務日報明細予定 left join M業務日報項目 on T業務日報明細予定.項目コード = M業務日報項目.項目コード " +
                                       $"WHERE 日報コード= '{日報コード.Text}' ORDER BY 明細番号";


                        if (!VariableSet.SetTable2Details(業務日報明細実績1.Detail, strSQL, cn))
                            throw new Exception("初期化に失敗しました。");


                        if (!VariableSet.SetTable2Details(業務日報明細予定1.Detail, strSQL2, cn))
                            throw new Exception("初期化に失敗しました。");

                        break;

                    case "状況":
                        Connect();
                        fn.DoWait("読み込んでいます...");

                        社員コード.Text = str状況;

                        if (DateTime.TryParse(日付.Text, out DateTime dt))
                            日報コード.Text = GetCode(dt, 社員コード.Text.PadLeft(3, '0'));


                        if (!LoadHeader()) throw new Exception("初期化に失敗しました。");

                        strSQL = $"SELECT 明細番号,実績内容,項目 " +
                                 $"FROM T業務日報明細実績 left join M業務日報項目 on T業務日報明細実績.項目コード=M業務日報項目.項目コード " +
                                 $"WHERE 日報コード= '{日報コード.Text}' ORDER BY 明細番号";

                        strSQL2 = $"SELECT 明細番号,予定内容,項目 " +
                                  $"FROM T業務日報明細予定 left join M業務日報項目 on T業務日報明細予定.項目コード = M業務日報項目.項目コード " +
                                  $"WHERE 日報コード= '{日報コード.Text}' ORDER BY 明細番号";


                        if (!VariableSet.SetTable2Details(業務日報明細実績1.Detail, strSQL, cn))
                            throw new Exception("初期化に失敗しました。");


                        if (!VariableSet.SetTable2Details(業務日報明細予定1.Detail, strSQL2, cn))
                            throw new Exception("初期化に失敗しました。");

                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_UpdatedControl - " + ex.Message);
            }
            finally
            {
                if (fn.WaitForm != null) fn.WaitForm.Close();
            }
        }

        private void Form_Unload(object sender, FormClosingEventArgs e)
        {
            string LoginUserCode = CommonConstants.LoginUserCode;//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            LocalSetting test = new LocalSetting();
            test.SavePlace(LoginUserCode, this);
        }

        private void コマンド終了_Click(object sender, EventArgs e)
        {
            Close(); // フォームを閉じる
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

                case Keys.F1:
                    //if (コマンド新規.Enabled)
                    //{
                    //    コマンド新規.Focus();
                    //    コマンド新規_Click(sender, e);
                    //}
                    break;
                case Keys.F2:
                    //if (コマンド読込.Enabled)
                    //{
                    //    コマンド読込.Focus();
                    //    コマンド読込_Click(sender, e);
                    //}
                    break;
                case Keys.F3:
                    //if (コマンド複写.Enabled) コマンド複写_Click(sender, e);
                    break;
                case Keys.F4:
                    //if (コマンド削除.Enabled) コマンド削除_Click(sender, e);
                    break;
                case Keys.F5:
                    //if (コマンドユニット.Enabled) コマンドユニット_Click(sender, e);
                    break;
                case Keys.F6:
                    //if (コマンドユニット表.Enabled) コマンドユニット表_Click(sender, e);
                    break;
                case Keys.F7:
                    //if (コマンド廃止.Enabled) コマンド廃止_Click(sender, e);
                    break;
                case Keys.F8:
                    //if (コマンドツール.Enabled) コマンドツール_Click(sender, e);
                    break;
                case Keys.F9:
                    //if (コマンド承認.Enabled) コマンド承認_Click(sender, e);
                    break;
                case Keys.F10:
                    //if (コマンド確定.Enabled) コマンド確定_Click(sender, e);
                    break;
                case Keys.F11:
                //if (コマンド登録.Enabled) コマンド登録_Click(sender, e);
                //break;
                case Keys.F12:
                    //if (コマンド終了.Enabled) コマンド終了_Click(sender, e);
                    break;
            }
        }

        private void コマンド詳細_Click(object sender, EventArgs e)
        {
            string message = "日報コード　：　" + 日報コード.Text + Environment.NewLine +
                 "登録日　：　" + 登録日.Text + Environment.NewLine +
                 "登録者　：　[" + 登録者コード.Text + "]" + 登録者名.Text;

            MessageBox.Show(message, "詳細", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void 日付選択ボタン_Click(object sender, EventArgs e)
        {
            F_カレンダー dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(日付.Text))
            {
                dateSelectionForm.args = 日付.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK && 日付.Enabled)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;
                日付.Text = selectedDate;
                UpdatedControl(this.日付);
                日付.Focus();
            }
        }

        private void 日付_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■日付を入力・選択します。未来の日付は入力できません。";
        }

        private void 日付_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 社員コード_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■氏名を選択します。追加登録はできません。";
        }

        private void 社員コード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 状況_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■表示日付での各ユーザーの登録状態です。表示したいユーザーを選択してください。";
            //  社員コード.SelectedValue = 状況.SelectedRows[0].Cells[10].Value;
        }

        private void 状況_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 社員コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbflg) return;
            UpdatedControl(this.社員コード);
        }

        private void ログインユーザーボタン_Click(object sender, EventArgs e)
        {
            社員コード.SelectedValue = CommonConstants.LoginUserCode;
            UpdatedControl(this.社員コード);
        }

        private void 日付_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbflg) return;
            UpdatedControl(this.日付);
        }

        private void 状況_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            社員コード.SelectedValue = 状況.SelectedRows[0].Cells[10].Value;
        }

        private void Form_Resize(object sender, EventArgs e)
        {
            try
            {
                業務日報明細実績1.Detail.Height = 業務日報明細実績1.Height + (this.Height - intWindowHeight);
                intWindowHeight = this.Height;  // 高さ保存

                業務日報明細実績1.Detail.Width = 業務日報明細実績1.Width + (this.Width - intWindowWidth);
                intWindowWidth = this.Width;    // 幅保存     　
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.Name + "_Form_Resize - " + ex.Message);
            }
        }

        private void コマンド確定_Click(object sender, EventArgs e)
        {

        }

        private void 日付_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 100, 50 }, new string[] { "Display", "Display2" });
            日付.Invalidate();
        }

        private void 確定コード_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(確定コード.Text))
            {
                確定表示.Visible = false;
            }
            else
            {
                確定表示.Visible = true;
            }
        }
    }
}
