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
    public partial class F_受注 : Form
    {
        private Control? previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string varOpenArgs = "";
        private bool setCombo = true;


        public string CurrentCode
        {
            get
            {
                return string.IsNullOrEmpty(受注コード.Text) ? "" : 受注コード.Text;
            }
        }

        public int CurrentEdition
        {
            get
            {
                int result;
                return int.TryParse(受注版数.Text, out result) ? result : 0;
            }
        }

        public F_受注()
        {
            this.Text = "受注（製図指図書）";       // ウィンドウタイトルを設定
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

            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");


            //実行中フォーム起動
            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(CommonConstants.LoginUserCode, this);

            //コンボボックスの設定
            OriginalClass ofn = new OriginalClass();

            ofn.SetComboBox(受注コード, "SELECT A.受注コード AS Value, A.受注コード, { fn REPLACE(STR(CONVERT(bit, T受注.無効日), 1, 0), '1', '×') } AS 削除 " +
                "FROM T受注 INNER JOIN (SELECT TOP 100 受注コード, MAX(受注版数) AS 最新版数 FROM T受注 GROUP BY 受注コード ORDER BY T受注.受注コード DESC) A ON T受注.受注コード = A.受注コード AND T受注.受注版数 = A.最新版数 ORDER BY A.受注コード DESC");

            try
            {
                this.SuspendLayout();

                // 登録モードの分岐
                if (string.IsNullOrEmpty(varOpenArgs))
                {
                    // 新規モードへ
                    if (!GoNewMode())
                    {
                        throw new Exception("初期化に失敗しました。");
                    }
                }
                else
                {
                    // 修正モードへ
                    //if (!GoModifyMode())
                    //{
                    //    throw new Exception("初期化に失敗しました。");
                    //}
                    //if (!string.IsNullOrEmpty(args))
                    //{
                    //    //this.仕入先コード.SelectedValue = args;
                    //    this.仕入先コード.Text = args;
                    //    UpdatedControl(this.仕入先コード);
                    //    ChangedData(false);

                    //}
                }
                varOpenArgs = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show("初期化に失敗しました。", "エラー");
            }
            finally
            {
                setCombo = false;
                this.ResumeLayout();
                fn.WaitForm.Close();
            }
        }

        private bool GoNewMode()
        {
            try
            {
                // 各コントロール値を初期化
                VariableSet.SetControls(this);

                Connect();

                this.受注コード.Text = FunctionClass.採番(cn, "A");
                this.受注版数.Text = "1";
                this.受注日.Text = DateTime.Now.ToString("yyyy/MM/dd");
                //string code = FunctionClass.GetNewCode(cn, CommonConstants.CH_SUPPLIER);
                //this.仕入先コード.Text = code.Substring(code.Length - 8);
                //// 
                //this.Revision.Text = "1";
                //this.支払先専用.SelectedValue = 0;
                //this.CloseDay.SelectedValue = 20;

                //// 編集による変更がない状態へ遷移する
                //ChangedData(false);

                //// ヘッダ部動作制御
                //this.仕入先コード.Enabled = false;
                //this.コマンド新規.Enabled = false;
                //this.コマンド修正.Enabled = true;
                //this.コマンド複写.Enabled = false;
                //this.コマンド削除.Enabled = false;
                //this.コマンドメール.Enabled = false;
                //// this.コマンド承認.Enabled = false;
                //// this.コマンド確定.Enabled = false;
                //this.コマンド登録.Enabled = false;

                return true;
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_GoNewMode - " + ex.Message);
                return false;
            }
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
                            this.受注版数.SelectedIndex = 0;
                        }

                        //状態の表示
                        //SetEditionStatus();

                        //ヘッダ部の表示
                        LoadHeader(this, this.CurrentCode, this.CurrentEdition);

                        //状態の表示（その２）■SetEditionStatusに統合すべき

                        //明細部の表示
                        LoadDetails(this.受注明細1.Detail, this.CurrentCode, this.CurrentEdition);

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

            ofn.SetComboBox(受注版数, sqlQuery);
        }

        /// <summary>
        /// ヘッダ部を読み込む
        /// </summary>
        /// <param name="formObject"></param>
        /// <param name="codeString">受注コード</param>
        /// <param name="editionNumber">受注版数</param>
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
        /// <param name="codeString">受注コード</param>
        /// <param name="editionNumber">受注版数</param>
        /// <returns></returns>
        public bool LoadDetails(GcMultiRow multiRow, string codeString, int editionNumber = -1)
        {
            bool loadDetails = false;
            string strSQL;

            try
            {
                Connect();

                strSQL = "SELECT * FROM V受注明細 WHERE 受注コード='" + CurrentCode + "' AND 受注版数=" + CurrentEdition + " ORDER BY 明細番号";

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
                    if (コマンド顧客.Enabled)
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
                        //コマンド終了_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
            }
        }

        private void 受注コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            UpdatedControl((Control)sender);
        }

        private void 受注コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 300, 50 }, new string[] { "受注コード", "削除" });
            受注コード.Invalidate();
            受注コード.DroppedDown = true;
        }

        private void 受注版数_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 50, 50 }, new string[] { "受注版数", "承認" });
            受注版数.Invalidate();
            受注版数.DroppedDown = true;
        }

        private void 備考_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■全角１００文字まで入力できます。";
        }

        private void 受注日選択ボタン_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            F_カレンダー calendar = new F_カレンダー();
            if (calendar.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = calendar.SelectedDate;

                // 日付コントロールに選択した日付を設定
                受注日.Text = selectedDate;
            }
        }

        private void 受注納期選択ボタン_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            F_カレンダー calendar = new F_カレンダー();
            if (calendar.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = calendar.SelectedDate;

                // 日付コントロールに選択した日付を設定
                受注納期.Text = selectedDate;
            }
        }

        private void 出荷予定日選択ボタン_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            F_カレンダー calendar = new F_カレンダー();
            if (calendar.ShowDialog() == DialogResult.OK)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = calendar.SelectedDate;

                // 日付コントロールに選択した日付を設定
                出荷予定日.Text = selectedDate;
            }
        }


    }
}


