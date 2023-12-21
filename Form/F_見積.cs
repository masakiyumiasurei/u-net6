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
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Data.Common;
using GrapeCity.Win.MultiRow;
using System.ComponentModel;
using System.DirectoryServices;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace u_net
{
    public partial class F_見積 : Form
    {
        public string varOpenArgs = ""; // オープン時引数保存用

        private SqlConnection cn;
        private bool setCombo = true;
        private string tmpCCode = ""; // 依頼主コード退避用

        private string BASE_CAPTION = "見積";


        public string CurrentCode
        {
            get
            {
                return string.IsNullOrEmpty(見積コード.Text) ? "" : 見積コード.Text;
            }
        }

        public int CurrentEdition
        {
            get
            {
                int result;
                return int.TryParse(見積版数.Text, out result) ? result : 0;
            }
        }

        public F_見積()
        {
            this.Text = "見積";       // ウィンドウタイトルを設定
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

        private void Form_Load(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                control.PreviewKeyDown += OriginalClass.ValidateCheck;
            }

            //実行中フォーム起動
            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");

            //コンボボックスの設定
            OriginalClass ofn = new OriginalClass();

            ofn.SetComboBox(担当者コード, "SELECT 社員コード AS Value, 社員コード AS Display, 氏名 AS Display2 FROM M社員 WHERE (退社 IS NULL) AND ([パート] = 0) AND (削除日時 IS NULL) AND (ふりがな <> N'ん') ORDER BY ふりがな");

            this.納入場所.DataSource = new KeyValuePair<String, String>[] {
                new KeyValuePair<String, String>("御社指定場所", "御社指定場所"),
            };
            this.納入場所.DisplayMember = "Value";
            this.納入場所.ValueMember = "Key";

            this.支払条件.DataSource = new KeyValuePair<String, String>[] {
                new KeyValuePair<String, String>("従来どおり", "従来どおり"),
                new KeyValuePair<String, String>("別途打ち合わせ", "別途打ち合わせ"),
            };
            this.支払条件.DisplayMember = "Value";
            this.支払条件.ValueMember = "Key";

            this.有効期間.DataSource = new KeyValuePair<String, String>[] {
                new KeyValuePair<String, String>("3ヶ月", "3ヶ月"),
                new KeyValuePair<String, String>("1ヶ月", "1ヶ月"),
                new KeyValuePair<String, String>("2週間", "2週間"),
            };
            this.有効期間.DisplayMember = "Value";
            this.有効期間.ValueMember = "Key";

            this.要承認.DataSource = new KeyValuePair<Int16, String>[] {
                new KeyValuePair<Int16, String>(1, "必要"),
                new KeyValuePair<Int16, String>(0, "不要"),
            };
            this.要承認.DisplayMember = "Value";
            this.要承認.ValueMember = "Key";

            this.合計金額表示.DataSource = new KeyValuePair<Int16, String>[] {
                new KeyValuePair<Int16, String>(1, "表示する"),
                new KeyValuePair<Int16, String>(0, "表示しない"),
            };
            this.合計金額表示.DisplayMember = "Value";
            this.合計金額表示.ValueMember = "Key";

            try
            {
                this.SuspendLayout();

                // データベース接続
                Connect();

                // 入力モードの分岐
                if (string.IsNullOrEmpty(varOpenArgs))
                {
                    // 新規
                    if (!GoNewMode())
                    {
                        throw new Exception("初期化に失敗しました。");
                    }
                }
                else
                {
                    // 読込
                    if (!GoModifyMode())
                    {
                        throw new Exception("初期化に失敗しました。");
                    }

                    this.見積コード.Text = varOpenArgs.Substring(varOpenArgs.IndexOf(','));
                    this.見積版数.Text = varOpenArgs.Substring(varOpenArgs.IndexOf(',') + 1);

                    varOpenArgs = string.Empty;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("初期化に失敗しました。", "エラー");
            }
            finally
            {
                setCombo = false;

                // ウィンドウを配置する
                LocalSetting localSetting = new LocalSetting();
                localSetting.LoadPlace(CommonConstants.LoginUserCode, this);

                fn.WaitForm.Close();

                this.ResumeLayout();
            }
        }

        private bool GoNewMode()
        {
            try
            {
                // 各コントロール値を初期化
                VariableSet.SetControls(this);

                Connect();

                this.見積コード.Text = FunctionClass.採番(cn, CommonConstants.CH_ESTIMATE);
                this.見積版数.Text = "1";
                this.見積日.Text = DateTime.Now.ToString("yyyy/MM/dd");
                this.担当者コード.SelectedValue = CommonConstants.LoginUserCode;
                this.要承認.SelectedValue = (Int16)0;
                this.合計金額表示.SelectedValue = (Int16)1;

                // Call 担当者コード_AfterUpdate ↓に置き換え
                this.担当者名.Text = ((DataRowView)担当者コード.SelectedItem)?.Row.Field<String>("Display2")?.ToString();

                //明細部を初期化
                LoadDetails(this.見積明細1.Detail, this.CurrentCode);

                //ヘッダ部を制御
                FunctionClass.LockData(this, false);

                this.見積日.Focus();
                this.見積コード.Enabled = false;
                this.見積版数.Enabled = false;
                this.改版ボタン.Enabled = false;
                this.コマンド新規.Enabled = false;
                this.コマンド読込.Enabled = true;
                this.コマンド複写.Enabled = false;
                this.コマンド削除.Enabled = false;
                this.コマンド見積書.Enabled = false;
                this.コマンド送信.Enabled = false;
                this.コマンド承認.Enabled = false;
                this.コマンド確定.Enabled = false;
                this.コマンド登録.Enabled = false;

                //明細部を制御する
                見積明細1.Detail.Enabled = true;

                return true;
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_GoNewMode - " + ex.Message);
                return false;
            }
        }

        private bool GoModifyMode()
        {
            try
            {
                //各コントロール値をクリア
                VariableSet.SetControls(this);
                //LoadDetails(this.受注明細1.Detail, this.CurrentCode);

                //// 未変更状態にする
                //ChangedData(false);
                //FunctionClass.LockData(this, true, "受注コード");

                //this.受注コード.Enabled = true;
                //this.受注版数.Enabled = true;
                //this.受注コード.Focus();
                //this.コマンド新規.Enabled = true;
                //this.コマンド読込.Enabled = false;
                //this.コマンド複写.Enabled = false;
                //this.コマンド登録.Enabled = false;

                return true;
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_GoModifyMode - " + ex.Message);
                return false;
            }
        }

        public void ChangedData(bool isChanged)
        {
            if (isChanged)
            {
                this.Text = this.Text.Replace("*", "") + "*";
            }
            else
            {
                this.Text = this.Text.Replace("*", "");
            }

            // キー情報を表示するコントロールを制御する
            // コードにフォーカスがある状態でサブフォームから呼び出されたときの対処
            if (this.ActiveControl == this.見積コード) this.見積日.Focus();
            this.見積コード.Enabled = !isChanged;
            if (this.ActiveControl == this.見積版数) this.見積日.Focus();
            this.見積版数.Enabled = !isChanged;

            this.コマンド複写.Enabled = !isChanged;
            this.コマンド削除.Enabled = !isChanged;
            this.コマンド見積書.Enabled = !isChanged;
            if (isChanged) this.コマンド送信.Enabled = false;
            if (isChanged) this.コマンド確定.Enabled = true;
            this.コマンド登録.Enabled = isChanged;
        }

        private void UpdatedControl(Control controlObject)
        {
            try
            {
                switch (controlObject.Name)
                {
                    case "受注コード":
                        FunctionClass fn = new FunctionClass();
                        fn.DoWait("読み込んでいます...");

                        //版数のソース更新
                        UpdateEditionList(controlObject.Text);

                        //OpenArgsが設定されていなければ版数を最新版とする
                        //開いてからコードを変えて読み込むときはOpenArgsはnullに
                        //設定されているため、最新版となる
                        if (string.IsNullOrEmpty(varOpenArgs))
                        {
                            this.見積版数.SelectedIndex = 0;
                        }

                        //状態の表示
                        //SetEditionStatus();

                        //ヘッダ部の表示
                        LoadHeader(this, this.CurrentCode, this.CurrentEdition);

                        //状態の表示（その２）■SetEditionStatusに統合すべき

                        //明細部の表示
                        //LoadDetails(this.受注明細1.Detail, this.CurrentCode, this.CurrentEdition);

                        fn.WaitForm.Close();
                        break;
                    case "受注版数":
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_UpdatedControl - " + ex.Message);
            }
        }

        private void UpdateEditionList(string codeString)
        {
            //版数のソースを更新する
            //CodeString - 受注コード
            string sqlQuery = "SELECT 受注版数 as Value, 受注版数," +
                              "{ fn REPLACE(STR(CONVERT(bit, 承認者コード + '1'), 1, 0), '1', '■') } AS 承認 " +
                              "FROM T受注 " +
                              "WHERE (受注コード = N'" + codeString + "') " +
                              "ORDER BY 受注版数 DESC";

            //コンボボックスの設定
            OriginalClass ofn = new OriginalClass();

            ofn.SetComboBox(見積版数, sqlQuery);
        }

        /// <summary>
        /// ヘッダ部を読み込む
        /// </summary>
        /// <param name="formObject"></param>
        /// <param name="codeString">見積コード</param>
        /// <param name="editionNumber">見積版数</param>
        /// <returns></returns>
        public bool LoadHeader(Form formObject, string codeString, int editionNumber = -1)
        {
            bool loadHeader = false;
            string strSQL;

            try
            {
                Connect();

                strSQL = "SELECT * FROM V受注ヘッダ WHERE 受注コード='" + codeString + "' AND 受注版数=" + editionNumber;
                VariableSet.SetTable2Form(this, strSQL, cn);
                loadHeader = true;
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_LoadHeader - " + ex.Message);
            }

            return loadHeader;
        }

        /// <summary>
        /// 明細部を読み込む
        /// </summary>
        /// <param name="multiRow"></param>
        /// <param name="codeString">見積コード</param>
        /// <param name="editionNumber">見積版数</param>
        /// <returns></returns>
        public bool LoadDetails(GcMultiRow multiRow, string codeString, int editionNumber = -1)
        {
            bool loadDetails = false;
            string strSQL;

            try
            {
                Connect();

                strSQL = "SELECT * FROM T見積明細 WHERE 見積コード='" + CurrentCode + "' AND 見積版数=" + CurrentEdition + " ORDER BY 行番号";

                using (SqlCommand command = new SqlCommand(strSQL, cn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);
                        multiRow.DataSource = dataTable;
                    }
                }
                loadDetails = true;
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_LoadDetails - " + ex.Message);
            }
            return loadDetails;
        }

        private void コマンド読込_Click(object sender, EventArgs e)
        {

        }

        private void コマンド終了_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void F_受注_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
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
                case Keys.F1:
                    if (コマンド新規.Enabled)
                    {
                        コマンド新規.Focus();
                        //コマンド新規_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F2:
                    if (コマンド読込.Enabled)
                    {
                        コマンド読込.Focus();
                        //コマンド修正_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F3:
                    if (コマンド複写.Enabled)
                    {
                        //コマンド複写_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F4:
                    if (コマンド削除.Enabled)
                    {
                        //コマンド削除_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F5:
                    if (コマンド見積書.Enabled)
                    {
                        //コマンドシリーズ_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F9:
                    if (コマンド承認.Enabled)
                    {
                        //コマンド承認_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F10:
                    if (コマンド確定.Enabled)
                    {
                        //コマンド確定_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F11:
                    if (コマンド登録.Enabled)
                    {
                        コマンド登録.Focus();
                        //コマンド登録_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F12:
                    if (コマンド終了.Enabled)
                    {
                        コマンド終了_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
            }
        }

        private void 見積日選択ボタン_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            F_カレンダー calendar = new F_カレンダー();
            if (calendar.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = calendar.SelectedDate;

                // 日付コントロールに選択した日付を設定
                見積日.Text = selectedDate;
            }
        }

        private void 担当者コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 50, 130 }, new string[] { "Display", "Display2" });
            担当者コード.Invalidate();
            担当者コード.DroppedDown = true;
        }

        private void 担当者コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            担当者名.Text = ((DataRowView)担当者コード.SelectedItem)?.Row.Field<String>("Display2")?.ToString();
            ChangedData(true);
        }

        private void 見積コード_Validating(object sender, CancelEventArgs e)
        {

        }

        private void 見積コード_Validated(object sender, EventArgs e)
        {

        }

        private void 見積コード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(((Control)sender), 11);
        }
    }
}


