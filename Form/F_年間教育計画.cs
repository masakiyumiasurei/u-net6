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
using Pao.Reports;
using GrapeCity.Win.MultiRow;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using static u_net.Public.FunctionClass;
using static u_net.Public.OriginalClass;
using Microsoft.EntityFrameworkCore.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace u_net
{
    public partial class F_年間教育計画 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "年間教育計画";
        private int selected_frame = 0;
        public bool IsDirty = false;
        private int intWindowHeight;
        private int intWindowWidth;

        public object? var年度;
        public object? var部;

        public F_年間教育計画()
        {
            this.Text = "年間教育計画";       // ウィンドウタイトルを設定
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

        private string Right(string value, int length)
        {
            if (value.Length <= length)
                return value;
            else
                return value.Substring(value.Length - length, length);
        }

        public string CurrentCode
        {
            get
            {
                //return Nz(ユニットコード.Text);
                return "";
            }
        }

        public int CurrentEdition
        {
            get
            {
                return string.IsNullOrEmpty(受講者コード.Text) ? 0 : Int32.Parse(受講者コード.Text);
            }
        }

        public string CurrentPartsCode
        {
            get
            {
                //return ユニット明細1.Detail.CurrentRow.Cells["部品コード"].Value?.ToString();
                return "";
            }
        }

        public bool IsNewData
        {
            get
            {
                return !コマンド抽出.Enabled;
            }
        }

        public bool IsApproved
        {
            get
            {
                //return !IsNull(承認日時.Text);
                return false;
            }
        }

        // Nz関数の代用
        private string Nz(object value)
        {
            return value == null ? "" : value.ToString();
        }

        // IsNull関数の代用
        private bool IsNull(object value)
        {
            return value == null || Convert.IsDBNull(value) || string.IsNullOrEmpty((string?)value);
        }

        //SqlConnection cn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter();

        private void SetAll()
        {
            var年度 = null;
            var部 = null;

        }

        private void SetInitial()
        {
            //今年の条件の時点で対象レコードはないので、不要であるが．．．
            var年度 = DateTime.Now.Year; ;
            var部 = null;

        }
        private void Form_Load(object sender, EventArgs e)
        {
            try
            {
                foreach (Control control in Controls)
                {
                    control.PreviewKeyDown += OriginalClass.ValidateCheck;
                }

                //実行中フォーム起動
                string LoginUserCode = CommonConstants.LoginUserCode;//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
                LocalSetting localSetting = new LocalSetting();
                localSetting.LoadPlace(LoginUserCode, this);


                OriginalClass ofn = new OriginalClass();
                ofn.SetComboBox(受講者コード, "SELECT 社員コード as Value,氏名 as Display FROM M社員" +
                    " WHERE 退社 IS NULL AND 削除日時 IS NULL ORDER BY ふりがな");
                this.受講者コード.DropDownWidth = 150;

                this.SuspendLayout();

                int intWindowHeight = this.Height;
                int intWindowWidth = this.Width;


                // 成功時の処理
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("初期化に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }
            finally
            {
                this.ResumeLayout();

            }
        }

        public void Filtering()
        {
            try
            {
                string strSQL = "";
                string strFilter = "";
                string strSource = "";
                string[] arr1;
                string str1 = "";

                //入金コード指定
                if (!string.IsNullOrEmpty(var部?.ToString()))
                {
                    strFilter = WhereString(strFilter, "部='" + var部.ToString() + "'");
                }

                // 入金日指定
                if (int.TryParse(var年度?.ToString(), out int num))
                {
                    strFilter = WhereString(strFilter, "年度=" + num);
                }


                if (strFilter == "")
                {
                    strSQL = "SELECT * FROM V年間教育計画サブ ORDER BY 登録コード DESC";
                }
                else
                {
                    strSQL = "SELECT * FROM V年間教育計画サブ WHERE " + strFilter + " ORDER BY 登録コード DESC";
                }

                Connect();
                if (!VariableSet.SetTable2Details(年間教育計画サブ1.Detail, strSQL, cn))
                    throw new Exception("初期化に失敗しました。");

                年間教育計画サブ1.Detail.AllowUserToAddRows = false;
                年間教育計画サブ1.Detail.AllowUserToDeleteRows = false;
                年間教育計画サブ1.Detail.ReadOnly = true;

                表示件数.Text = 年間教育計画サブ1.Detail.RowCount.ToString();

                for (int i = 0; i < 年間教育計画サブ1.Detail.RowCount; i++)
                {
                    if ((bool)年間教育計画サブ1.Detail.Rows[i].Cells["キャンセル待ち"].Value == true)
                    {
                        年間教育計画サブ1.Detail.Rows[i].Cells["キャンセル待ち表示"].Value = "キャンセル待ち";
                    }
                    else
                    {
                        年間教育計画サブ1.Detail.Rows[i].Cells["キャンセル待ち表示"].Value = "";
                    }

                    if ((bool)年間教育計画サブ1.Detail.Rows[i].Cells["キャンセル"].Value == true)
                    {
                        年間教育計画サブ1.Detail.Rows[i].Cells["キャンセル表示"].Value = "=======================" +
                            "==================================================================================" +
                            "===========";
                    }
                    else
                    {
                        年間教育計画サブ1.Detail.Rows[i].Cells["キャンセル表示"].Value = "";
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Filtering - " + ex.Message);
                return;
            }
        }




        private void Form_Unload(object sender, FormClosingEventArgs e)
        {
            string LoginUserCode = CommonConstants.LoginUserCode;
            LocalSetting test = new LocalSetting();
            test.SavePlace(LoginUserCode, this);
        }


        public void ChangedData(bool isChanged)
        {
            if (ActiveControl == null) return;

            if (isChanged)
            {
                this.Text = BASE_CAPTION + "*";
            }
            else
            {
                this.Text = BASE_CAPTION;
            }

            //if (ActiveControl == ユニットコード)
            //{
            //    品名.Focus();
            //}

            //ユニットコード.Enabled = !isChanged;

            if (ActiveControl == 受講者コード)
            {
                品名.Focus();
            }

            IsDirty = isChanged;

            受講者コード.Enabled = !isChanged;
            コマンド全表示.Enabled = !isChanged;
            コマンド印刷.Enabled = !isChanged;
            コマンド商品.Enabled = !isChanged;
            コマンド行挿入.Enabled = !isChanged;


            if (isChanged && !IsApproved)
            {
                コマンド複写.Enabled = false;
                コマンド更新.Enabled = true;
            }

            コマンド登録.Enabled = isChanged;
        }

        private void UpdateEditionList(string codeString)
        {

            OriginalClass ofn = new OriginalClass();


            ofn.SetComboBox(受講者コード, "SELECT ユニット版数 AS Value, ユニット版数 AS Display, " +
                    "{ fn REPLACE(STR(CONVERT(bit, 承認日時), 1, 0), '1', '■') } AS Display2 " +
                    "FROM Mユニット " +
                    "WHERE (ユニットコード = '" + codeString + "') " +
                    "ORDER BY ユニット版数 DESC");
            受講者コード.DrawMode = DrawMode.OwnerDrawFixed;

        }

        private string GetRohsStatus()
        {
            try
            {
                int stPriority = 100; // RoHSの対応状況を優先的に選定するための番号

                //if ((IsDecided && ユニット明細1.Detail.RowCount < 1) || (!IsDecided && ユニット明細1.Detail.RowCount <= 1))
                //{
                //    stPriority = 1;
                //    return "×";
                //}

                //for (int i = 0; i < ユニット明細1.Detail.RowCount; i++)
                //{
                //    if (ユニット明細1.Detail.Rows[i].IsNewRow) continue;

                //    if (!Convert.ToBoolean(ユニット明細1.Detail.Rows[i].Cells["削除対象"].Value))
                //    {
                //        int priority = Convert.ToInt32(ユニット明細1.Detail.Rows[i].Cells["RohsStatusPriority"].Value);
                //        if (priority < stPriority)
                //        {
                //            stPriority = priority;
                //            return ユニット明細1.Detail.Rows[i].Cells["RohsStatusSign"].Value.ToString();
                //        }
                //    }
                //}
            }
            catch (Exception ex)
            {
                // 例外処理
                Console.WriteLine("GetRohsStatus Error: " + ex.Message);
            }

            return ""; // エラー時は空文字列を返す（またはエラー処理に応じて適切な値を返す）
        }

        private bool SaveData()
        {

            Connect();

            {
                try
                {
                    Connect();

                    DateTime dtmNow = FunctionClass.GetServerDate(cn);

                    Control objControl1 = null;
                    Control objControl2 = null;
                    Control objControl3 = null;
                    Control objControl4 = null;
                    Control objControl5 = null;
                    Control objControl6 = null;

                    object varSaved1 = null;
                    object varSaved2 = null;
                    object varSaved3 = null;
                    object varSaved4 = null;
                    object varSaved5 = null;
                    object varSaved6 = null;

                    if (IsNewData)
                    {
                        objControl1 = 作成日時;
                        //objControl2 = 作成者コード;
                        objControl3 = 作成者名;

                        varSaved1 = objControl1.Text;
                        varSaved2 = objControl2.Text;
                        varSaved3 = objControl3.Text;

                        objControl1.Text = dtmNow.ToString();
                        objControl2.Text = CommonConstants.LoginUserCode;
                        objControl3.Text = CommonConstants.LoginUserFullName;
                    }

                    objControl4 = 更新日時;
                    objControl5 = 更新者コード;
                    objControl6 = 更新者名;

                    varSaved4 = objControl4.Text;
                    varSaved5 = objControl5.Text;
                    varSaved6 = objControl6.Text;

                    objControl4.Text = dtmNow.ToString();
                    objControl5.Text = CommonConstants.LoginUserCode;
                    objControl6.Text = CommonConstants.LoginUserFullName;


                    // 登録処理
                    if (RegTrans(CurrentCode, CurrentEdition, false))
                    {
                        return true;
                    }
                    else
                    {
                        if (IsNewData)
                        {
                            objControl1.Text = varSaved1.ToString();
                            objControl2.Text = varSaved2.ToString();
                            objControl3.Text = varSaved3.ToString();
                        }

                        objControl4.Text = varSaved4.ToString();
                        objControl5.Text = varSaved5.ToString();
                        objControl6.Text = varSaved6.ToString();
                    }
                }
                catch (Exception ex)
                {
                    // エラーハンドリングが必要な場合には追加してください
                    Console.WriteLine($"Error in SaveData: {ex.Message}");
                }

                return false;
            }
        }

        private bool RegTrans(string codeString, int editionNumber, bool updatePreEdition)
        {
            Connect();
            SqlTransaction transaction = cn.BeginTransaction();
            {

                try
                {

                    string strwhere = "ユニットコード='" + codeString + "' and ユニット版数 =" + editionNumber;
                    // ヘッダ部の登録
                    if (!DataUpdater.UpdateOrInsertDataFrom(this, cn, "Mユニット", strwhere, "ユニットコード", transaction, "ユニット版数"))
                    {
                        transaction.Rollback();  // 変更をキャンセル
                        return false;
                    }

                    // 明細部の登録
                    //if (!DataUpdater.UpdateOrInsertDetails(this.ユニット明細1.Detail, cn, "Mユニット明細", strwhere, "ユニットコード", transaction))
                    //{
                    //    transaction.Rollback();  // 変更をキャンセル
                    //    return false;
                    //}

                    // 前版データの更新処理
                    if (updatePreEdition)
                    {
                        string sql = "";
                        strwhere = "ユニットコード='" + codeString + "' and ユニット版数 =" + (editionNumber - 1);

                        if (IsApproved) // 改版データを承認した場合
                        {
                            //sql = $"UPDATE Mユニット SET 無効日時=GETDATE(), 無効者コード='{承認者コード.Text}' WHERE " + strwhere;
                        }
                        else // 改版データを承認しない場合
                        {
                            sql = $"UPDATE Mユニット SET 無効日時=NULL, 無効者コード=NULL WHERE " + strwhere;
                        }

                        using (SqlCommand cmd = new SqlCommand(sql, cn, transaction))
                        {
                            cmd.ExecuteNonQuery();
                        }
                    }




                    string rohsStatus = GetRohsStatus() == "" ? "NULL" : "'1'";
                    string nonCor = IsBasedNonCor() == 9 ? "NULL" : "'" + IsBasedNonCor().ToString() + "'";

                    string sql2 = "";

                    sql2 = $"UPDATE Mユニット SET RoHS対応 = {rohsStatus}, 非含有証明書 = {nonCor} WHERE " + strwhere;

                    using (SqlCommand cmd = new SqlCommand(sql2, cn, transaction))
                    {
                        cmd.ExecuteNonQuery();
                    }

                    transaction.Commit();

                    return true;
                }
                catch (Exception ex)
                {
                    Debug.Print($"{Name}_RegTrans - {ex.GetType().ToString()} : {ex.Message}");
                    transaction.Rollback();
                    return false;

                }
            }
        }

        private int IsBasedNonCor()
        {
            try
            {


                //if ((IsDecided && ユニット明細1.Detail.RowCount < 1) || (!IsDecided && ユニット明細1.Detail.RowCount <= 1))
                //{
                //    return 0;
                //}

                //int result = 0;

                //foreach (Row row in ユニット明細1.Detail.Rows)
                //{

                //    if (row.IsNewRow) continue;

                //    // ユニットの場合、製品と違いRoHSフィールドの値がNULLであることはあり得ないが、
                //    // 万が一NULL値を取得してしまった場合に備え条件を含める
                //    if (row.Cells["非含有証明書"].Value is DBNull)
                //    {
                //        result = 9;
                //        break;
                //    }
                //    else if (row.Cells["非含有証明書"].Value.ToString() == "？")
                //    {
                //        result = 3;
                //    }
                //    else if (row.Cells["非含有証明書"].Value.ToString() == "△" && result < 3)
                //    {
                //        result = 2;
                //    }
                //    else if (row.Cells["非含有証明書"].Value.ToString() == "○" && result < 2)
                //    {
                //        result = 1;
                //    }
                //}

                //return result;
                return 0;

            }
            catch (Exception ex)
            {
                Debug.Print($"{GetType().Name}_IsBasedNonCor - {ex.Message}");
                return 0;
            }

        }

        private void Form_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    e.Handled = true;
                    e.SuppressKeyPress = true;
                    break;
            }
        }

        private bool CopyData(string codeString, int editionNumber)
        {
            try
            {


                //for (int i = 0; i < ユニット明細1.Detail.RowCount; i++)
                //{
                //    if (ユニット明細1.Detail.Rows[i].IsNewRow == true)
                //    {
                //        //新規行の場合は、処理をスキップ
                //        continue;
                //    }

                //    ユニット明細1.Detail.Rows[i].Cells["ユニットコード"].Value = codeString;
                //    ユニット明細1.Detail.Rows[i].Cells["ユニット版数"].Value = editionNumber;


                //}


                // 表示情報の更新
                this.受講者コード.Text = editionNumber.ToString();

                this.作成日時.Text = null;
                this.作成者名.Text = null;
                this.更新日時.Text = null;
                this.更新者コード.Text = null;
                this.更新者名.Text = null;


                return true;
            }
            catch (Exception ex)
            {
                // エラーハンドリング
                Debug.Print($"{this.Name}_CopyData - {ex.Message}");
                return false;
            }
        }

        private bool ClearHistory()
        {
            try
            {

                //if (ユニット明細1.Detail.RowCount == 0)
                //{
                //    return true;
                //}

                //int lngi = 1; // 明細番号の初期化

                //for (int i = 0; i < ユニット明細1.Detail.RowCount; i++)
                //{
                //    if (ユニット明細1.Detail.Rows[i].IsNewRow == true)
                //    {
                //        //新規行の場合は、処理をスキップ
                //        continue;
                //    }

                //    if (string.IsNullOrEmpty(ユニット明細1.Detail.Rows[i].Cells["削除対象"].Value?.ToString()))
                //    {
                //        ユニット明細1.Detail.Rows[i].Cells["削除対象"].Value = false;
                //    }

                //    if (Convert.ToBoolean(ユニット明細1.Detail.Rows[i].Cells["削除対象"].Value))
                //    {
                //        ユニット明細1.Detail.Rows.RemoveAt(i);
                //    }
                //    else
                //    {
                //        ユニット明細1.Detail.Rows[i].Cells["明細番号"].Value = lngi;
                //        ユニット明細1.Detail.Rows[i].Cells["変更操作コード"].Value = null;
                //        ユニット明細1.Detail.Rows[i].Cells["変更内容"].Value = null;
                //        lngi++;
                //    }
                //}


                return true;
            }
            catch (Exception ex)
            {
                // エラーハンドリング
                Debug.Print($"{this.Name}_ClearHistory - {ex.Message}");
                return false;
            }
        }

        private void コマンド複写_Click(object sender, EventArgs e)
        {

            FunctionClass fn = new FunctionClass();
            Connect();

            try
            {

                fn.DoWait("複写しています...");


                // 複写に成功すればインターフェースを更新する
                if (CopyData(Right(FunctionClass.採番(cn, "UNI"), 8), 1) && ClearHistory())
                {
                    // データ変更とする
                    ChangedData(true);
                    // ヘッダ部制御
                    FunctionClass.LockData(this, false);

                    // ■ 値集合ソースをクリアする必要はないのか？

                    品名.Focus();
                    実行ボタン.Enabled = false;
                    コマンド抽出.Enabled = false;
                    コマンド初期化.Enabled = true;
                    コマンド複写.Enabled = false;

                    // 明細部制御
                    //ユニット明細1.Detail.AllowUserToAddRows = true;
                    //ユニット明細1.Detail.AllowUserToDeleteRows = true;
                    //ユニット明細1.Detail.ReadOnly = false;
                }
            }
            catch (Exception ex)
            {
                // エラーメッセージボックスを表示
                MessageBox.Show($"{ex.GetType().Name}: {ex.Message}", "複写コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                fn.WaitForm.Close();
            }

        }

        private void コマンド登録_Click(object sender, EventArgs e)
        {

            FunctionClass fn = new FunctionClass();
            Connect();

            try
            {

                fn.DoWait("登録しています...");

                if (SaveData())
                {

                    // 版数のソース更新
                    UpdateEditionList(CurrentCode);
                    // 製品版数.Requery();

                    ChangedData(false);

                    // RoHS対応状況を表示する
                    RoHS対応.Text = GetRohsStatus();


                    // 新規モードのときは修正モードへ移行する
                    if (IsNewData)
                    {
                        コマンド抽出.Enabled = true;
                        コマンド初期化.Enabled = false;

                        OriginalClass ofn = new OriginalClass();
                        //ofn.SetComboBox(ユニットコード, "SELECT A.ユニットコード as Value, A.ユニットコード as Display , A.最新版数 as Display3, { fn REPLACE(STR(CONVERT(bit, Mユニット.無効日時), 1, 0), '1', '×') } AS Display2 FROM Mユニット INNER JOIN (SELECT ユニットコード, MAX(ユニット版数) AS 最新版数 FROM Mユニット GROUP BY ユニットコード) A ON Mユニット.ユニットコード = A.ユニットコード AND Mユニット.ユニット版数 = A.最新版数 ORDER BY A.ユニットコード DESC");

                    }

                    if (!IsApproved)
                    {
                        コマンド更新.Enabled = true;
                    }
                }
                else
                {
                    MessageBox.Show("登録できませんでした。", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                // エラーメッセージボックスを表示
                MessageBox.Show($"{ex.GetType().Name}: {ex.Message}", "登録コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                fn.WaitForm.Close();
            }

        }

        private void コマンド終了_Click(object sender, EventArgs e)
        {
            Close(); // フォームを閉じる
        }

        private void コマンド更新_Click(object sender, EventArgs e)
        {

        }

        private void コマンド行削除_Click(object sender, EventArgs e)
        {

        }

        private void コマンド行挿入_Click(object sender, EventArgs e)
        {

        }

        private void コマンド商品_Click(object sender, EventArgs e)
        {

        }

        private void コマンド改ページ_Click(object sender, EventArgs e)
        {

        }

        private void コマンド印刷_Click(object sender, EventArgs e)
        {

        }

        private void コマンド全表示_Click(object sender, EventArgs e)
        {
            SetAll();
            Filtering();

        }

        private void コマンド初期化_Click(object sender, EventArgs e)
        {
            SetInitial();
            Filtering();
        }

        private void コマンド抽出_Click(object sender, EventArgs e)
        {
            F_年間教育計画_抽出 fm = new F_年間教育計画_抽出();
            fm.ShowDialog();
        }

        private void 実行ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 年度_Leave(object sender, EventArgs e)
        {

        }

        private void 年度_TextChanged(object sender, EventArgs e)
        {

        }

        private void 年度_Enter(object sender, EventArgs e)
        {

        }

        private void 年度_Validated(object sender, EventArgs e)
        {

        }

        private void 年度_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void 受講者コード_Enter(object sender, EventArgs e)
        {

        }

        private void 受講者コード_Leave(object sender, EventArgs e)
        {

        }

        private void 受講者コード_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void 受講者コード_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void 受講者コード_Validated(object sender, EventArgs e)
        {

        }

        private void 受講者コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void 教育名_Enter(object sender, EventArgs e)
        {
            // toolStripStatusLabel1.Text = "■改行する場合は、[Ctrl]キーを押しながら[Enter]キーを押します。";
        }

        private void 教育名_Leave(object sender, EventArgs e)
        {
            //toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 教育名_TextChanged(object sender, EventArgs e)
        {

        }

        private void 教育名_Validated(object sender, EventArgs e)
        {

        }

        private void 教育名_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void 教育機関名_Enter(object sender, EventArgs e)
        {
            // toolStripStatusLabel1.Text = "■改行する場合は、[Ctrl]キーを押しながら[Enter]キーを押します。";
        }

        private void 教育機関名_Leave(object sender, EventArgs e)
        {
            //toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 教育機関名_TextChanged(object sender, EventArgs e)
        {

        }

        private void 教育機関名_Validated(object sender, EventArgs e)
        {

        }

        private void 教育機関名_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void 日付1選択ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 日付1_Enter(object sender, EventArgs e)
        {

        }

        private void 日付1_Leave(object sender, EventArgs e)
        {

        }

        private void 日付1_TextChanged(object sender, EventArgs e)
        {

        }

        private void 日付1_Validated(object sender, EventArgs e)
        {

        }

        private void 日付1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void 日付2_Enter(object sender, EventArgs e)
        {

        }

        private void 日付2_Leave(object sender, EventArgs e)
        {

        }

        private void 日付2_TextChanged(object sender, EventArgs e)
        {

        }

        private void 日付2_Validated(object sender, EventArgs e)
        {

        }

        private void 日付2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void 日付2選択ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 備考_Enter(object sender, EventArgs e)
        {

        }

        private void 備考_Leave(object sender, EventArgs e)
        {

        }

        private void 備考_TextChanged(object sender, EventArgs e)
        {

        }

        private void 備考_Validated(object sender, EventArgs e)
        {

        }

        private void 備考_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void キャンセル待ち_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void キャンセル_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void Form_Resize(object sender, EventArgs e)
        {
            try
            {
                年間教育計画サブ1.Detail.Height = 年間教育計画サブ1.Height + (this.Height - intWindowHeight);
                intWindowHeight = this.Height;  // 高さ保存

                年間教育計画サブ1.Detail.Width = 年間教育計画サブ1.Width + (this.Width - intWindowWidth);
                intWindowWidth = this.Width;    // 幅保存     　
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.Name + "_Form_Resize - " + ex.Message);
            }
        }

        private void 受講者コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 30, 120 }, new string[] { "Value", "Display" });
            受講者コード.Invalidate();
            受講者コード.DroppedDown = true;
        }

        private void 受講者名_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
