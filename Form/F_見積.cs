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
using static u_net.CommonConstants;
using static u_net.Public.FunctionClass;
using Microsoft.Identity.Client.NativeInterop;

namespace u_net
{
    public partial class F_見積 : Form
    {
        public string varOpenArgs = ""; // オープン時引数保存用

        private SqlConnection cn;
        private bool setCombo = true;

        private string BASE_CAPTION = "見積";
        int intWindowHeight;
        int intWindowWidth;

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

        /// <summary>
        /// 現在のデータが承認されているかどうかを取得する
        /// </summary>
        public bool IsApproved
        {
            get
            {
                return !string.IsNullOrEmpty(承認者コード.Text);
            }
        }

        /// <summary>
        /// 現在のデータが確定されているかどうかを取得する
        /// </summary>
        public bool IsDecided
        {
            get
            {
                return !string.IsNullOrEmpty(確定日時.Text);
            }
        }

        /// <summary>
        /// 現在のデータが削除されているかどうかを取得する
        /// </summary>
        public bool IsDeleted
        {
            get
            {
                return !string.IsNullOrEmpty(削除日時.Text);
            }
        }

        /// <summary>
        /// 現在のデータが最終版かどうかを取得する
        /// </summary>
        public bool IsLastEdition
        {
            get
            {
                return (this.見積版数.Items.Count == 0 || this.見積版数.Items.Count == this.CurrentEdition);
            }
        }

        /// <summary>
        /// 現在のデータが新規データかどうかを取得する
        /// </summary>
        public bool IsNewData
        {
            get
            {
                return !コマンド新規.Enabled;
            }
        }

        /// <summary>
        /// 現在のデータが変更されているかどうかを取得する
        /// </summary>
        public bool IsChanged
        {
            get
            {
                return コマンド登録.Enabled;
            }
        }

        /// <summary>
        /// 現在のデータに承認が必要かどうかを取得する
        /// </summary>
        public bool WithApproval
        {
            get
            {
                return (要承認.SelectedValue != null && (Int16)要承認.SelectedValue == 1);
            }
        }

        public F_見積()
        {
            this.Text = "見積";       // ウィンドウタイトルを設定
            this.MaximizeBox = false;  // 最大化ボタンを無効化
            this.MinimizeBox = false; //最小化ボタンを無効化

            InitializeComponent();

            this.担当者コード.DropDownWidth = 204;
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
            // Tab移動でValidatedイベントを走らせている為コメントアウト
            //foreach (Control control in Controls)
            //{
            //    control.PreviewKeyDown += OriginalClass.ValidateCheck;
            //}

            //実行中フォーム起動
            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");

            //コンボボックスの設定
            OriginalClass ofn = new OriginalClass();
            ofn.SetComboBox(担当者コード, "SELECT 社員コード AS Value, 社員コード AS Display1, 氏名 AS Display2 FROM M社員 WHERE (退社 IS NULL) AND (パート = 0) AND (削除日時 IS NULL) AND (ふりがな <> N'ん') ORDER BY ふりがな");

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

                intWindowHeight = this.Height;
                intWindowWidth = this.Width;

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

                    this.見積コード.Text = varOpenArgs.Substring(0, varOpenArgs.IndexOf(','));
                    this.見積版数.Text = varOpenArgs.Substring(varOpenArgs.IndexOf(',') + 1);

                    UpdatedControl(this.見積コード);

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
        private void Form_Resize(object sender, EventArgs e)
        {
            try
            {
                見積明細1.Detail.Height = 見積明細1.Height + (this.Height - intWindowHeight);
                intWindowHeight = this.Height;  // 高さ保存

                見積明細1.Detail.Width = 見積明細1.Width + (this.Width - intWindowWidth);
                intWindowWidth = this.Width;    // 幅保存                
            }
            catch (Exception ex)
            {
                MessageBox.Show(this.Name + "_Form_Resize - " + ex.Message);
            }
        }
        private void Form_Unload(object sender, FormClosingEventArgs e)
        {
            try
            {
                Connect();

                // データへの変更されたときの処理
                if (IsChanged)
                {
                    var intRes = MessageBox.Show("変更内容を登録しますか？", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    switch (intRes)
                    {
                        case DialogResult.Yes:
                            //// 明細行数のみ確認する。エラー検出は確定時に行う
                            if (IsErrorDetails())
                            {
                                e.Cancel = true;
                                return;
                            }
                            //// 明細行が並べ替えられているときはその旨を知らせる
                            if (見積明細1.IsOrderByOn)
                            {
                                if (MessageBox.Show("明細行が並べ替えられています。" + Environment.NewLine +
                                                    "並べ替えを解除して登録しますか？", "登録コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                {
                                    見積明細1.CancelOrderBy();

                                }
                            }
                            if (!SaveData(CurrentCode, CurrentEdition))
                            {
                                if (MessageBox.Show("登録できませんでした。" + Environment.NewLine + Environment.NewLine +
                                 "強制終了しますか？", "エラー", MessageBoxButtons.YesNo,
                                 MessageBoxIcon.Exclamation) == DialogResult.Yes)
                                {
                                    return;
                                }
                                else
                                {
                                    e.Cancel = true;
                                    return;
                                }
                            }

                            break;
                        case DialogResult.No:

                            //accessではメッセージボックスがNOの処理は何も書いてなかった

                            break;
                        case DialogResult.Cancel:
                            e.Cancel = true;
                            return;
                    }

                    // 新規モードのときに登録しない場合は内部の更新データを元に戻す
                    if (IsNewData && !string.IsNullOrEmpty(CurrentCode) && CurrentEdition == 1)
                    {
                        // 採番された番号を戻す
                        if (!FunctionClass.Recycle(cn, CurrentCode))
                        {
                            MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine + Environment.NewLine +
                                            "見積コード　：　" + CurrentCode, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }

                    return;
                }

            }
            catch (Exception ex)
            {
                Debug.Print(Name + "_Unload - " + ex.Message);
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                // ウィンドウの配置を保存する
                LocalSetting localSetting = new LocalSetting();
                localSetting.SavePlace(CommonConstants.LoginUserCode, this);
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
                見積明細1.Detail.AllowUserToAddRows = true;
                見積明細1.Detail.AllowUserToDeleteRows = true;
                見積明細1.Detail.ReadOnly = false; //readonlyなのでaccessと真偽が逆になる  

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
                LoadDetails(this.見積明細1.Detail, this.CurrentCode);

                // 未変更状態にする
                ChangedData(false);

                this.見積コード.Enabled = true;
                this.見積コード.Focus();
                // 見積コードコントロールが使用可能になってからLockDataをコールすること
                LockData(this, true, "見積コード", "見積版数");
                this.コマンド新規.Enabled = true;
                this.コマンド読込.Enabled = false;
                this.コマンド複写.Enabled = false;
                this.コマンド確定.Enabled = false;
                this.コマンド登録.Enabled = false;

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

        private bool IsErrorData(string exFieldName1, string exFieldName2 = null)
        {
            try
            {
                foreach (Control control in Controls)
                {
                    if ((control is TextBox || control is ComboBox) && control.Visible)
                    {
                        if (control.Name != exFieldName1 && control.Name != exFieldName2)
                        {
                            if (IsError(control))
                            {
                                control.Focus();
                                return true;
                            }
                        }
                    }
                }
                // 明細行数のチェック
                if (IsErrorDetails())
                {
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{GetType().Name}_IsErrorData - {ex.GetType().Name} : {ex.Message}");
                return true;
            }
        }

        private bool IsError(Control controlObject)
        {
            try
            {
                Connect();

                string strName = controlObject.Name;
                object varValue = controlObject.Text; // Valueプロパティの代わりにTextプロパティを使用

                DateTime inputDate;

                switch (strName)
                {
                    case "見積コード":
                    case "見積版数":
                        if (string.IsNullOrEmpty(varValue?.ToString()))
                        {
                            MessageBox.Show("データの識別情報がありません。\nシステムエラーです。", strName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        break;
                    case "見積日":
                        if (string.IsNullOrEmpty(varValue?.ToString()))
                        {
                            MessageBox.Show(strName + "を入力してください。", strName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        if (!DateTime.TryParse(varValue.ToString(), out inputDate))
                        {
                            MessageBox.Show("日付を入力してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        ((TextBox)controlObject).Text = inputDate.ToString("yyyy/MM/dd");
                        if (DateTime.Now < inputDate)
                        {
                            MessageBox.Show("未来の日付は入力できません。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        break;
                    case "担当者コード":
                        if (string.IsNullOrEmpty(varValue?.ToString()))
                        {
                            MessageBox.Show("担当者名を選択してください。", "担当者名", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        break;
                    case "件名":
                    case "顧客名":
                    case "顧客担当者名":
                    case "納期":
                    case "納入場所":
                    case "支払条件":
                    case "有効期間":
                        if (string.IsNullOrEmpty(varValue?.ToString()))
                        {
                            MessageBox.Show(strName + "を入力してください。", strName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        break;
                    case "要承認":
                    case "合計金額表示":
                        if (((ComboBox)controlObject).FindStringExact(varValue?.ToString()) == -1)
                        {
                            MessageBox.Show("指定した項目はリストにありません。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ((ComboBox)controlObject).DroppedDown = true;
                            goto Exit_IsError;
                        }
                        break;

                }

                return false;

            Exit_IsError:
                if (controlObject is TextBox textBox)
                {
                    textBox.Undo();
                }

                return true;

            }
            catch (Exception ex)
            {
                Debug.Print("IsError - " + ex.Message);
                return true;
            }
        }

        private bool IsErrorDetails()
        {
            try
            {
              
                if ((IsDecided && 見積明細1.Detail.RowCount < 1) || (!IsDecided && 見積明細1.Detail.RowCount <= 1))
                {
                    MessageBox.Show("明細行を1行以上入力してください。", "見積", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    見積明細1.Detail.Focus();
                    return true;
                }
                else
                {
                    foreach(Row row in 見積明細1.Detail.Rows)
                    {
                        if (row.IsNewRow) continue;

                        if (string.IsNullOrEmpty(row.Cells["品名"].Value?.ToString()))
                        {

                            MessageBox.Show("品名を入力してください。", "見積", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                            
                        }

                        if (string.IsNullOrEmpty(row.Cells["単価"].Value?.ToString()))
                        {
                            MessageBox.Show("単価を入力してください。", "見積", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        if (OriginalClass.IsNumeric(row.Cells["単価"].Value) == false)
                        {
                            MessageBox.Show("数字を入力してください。", "見積", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                            return true;
                        }


                        if (string.IsNullOrEmpty(row.Cells["数量"].Value?.ToString()))
                        {
                            MessageBox.Show("数量を入力してください。", "見積", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        if (OriginalClass.IsNumeric(row.Cells["数量"].Value) == false)
                        {
                            MessageBox.Show("数字を入力してください。", "見積", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{GetType().Name}_IsErrorDetails - {ex.GetType().Name} : {ex.Message}");
                return true;
            }
        }

        private void UpdatedControl(Control controlObject)
        {
            FunctionClass fn = new FunctionClass();

            try
            {
                switch (controlObject.Name)
                {
                    case "見積コード":
                        fn.DoWait("読み込んでいます...");

                        //版数のソース更新
                        SetEditions(controlObject.Text);

                        // OpenArgsが設定されていなければ版数を最新版とする
                        // 開いてからコードを変えて読み込むときはOpenArgsはnullに
                        // 設定されているため、最新版となる
                        if (string.IsNullOrEmpty(varOpenArgs))
                        {
                            this.見積版数.SelectedIndex = 0;
                        }

                        // データの読み込み
                        LoadData(this.CurrentCode, this.CurrentEdition);

                        fn.WaitForm.Close();
                        break;
                    case "見積版数":
                        // データの読み込み
                        LoadData(this.CurrentCode, this.CurrentEdition);

                        break;
                    case "顧客コード":
                        // データの読み込み
                        SetCustomerInfo(controlObject.Text);
                        break;
                    case "顧客名":
                    case "電話番号":
                    case "ファックス番号":
                        if (((TextBox)controlObject).Modified)
                        {
                            this.顧客コード.Text = null;
                        }
                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_UpdatedControl - " + ex.Message);
            }
            finally
            {
                if (fn.WaitForm != null)
                {
                    fn.WaitForm.Close();
                }
            }
        }

        /// <summary>
        /// 版数のソースを更新する
        /// </summary>
        /// <param name="code">見積コード</param>
        private void SetEditions(string code)
        {
            string sqlQuery = "SELECT 見積版数 AS Value, 見積版数 AS Display FROM T見積 " +
                                  "WHERE 見積コード='" + code + "' " +
                                  "ORDER BY 見積版数 DESC";

            //コンボボックスの設定
            OriginalClass ofn = new OriginalClass();

            ofn.SetComboBox(見積版数, sqlQuery);
        }

        /// <summary>
        /// 指定された顧客コードの関連情報を設定する
        /// </summary>
        /// <param name="customerCode">顧客コード</param>
        private void SetCustomerInfo(string customerCode)
        {
            try
            {
                Connect();

                string strSQL = $"SELECT * FROM V顧客 WHERE 顧客コード='{customerCode}' AND 取引開始日 IS NOT NULL";

                using (SqlCommand command = new SqlCommand(strSQL, cn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count > 0)
                        {
                            // 顧客情報を設定
                            this.顧客名.Text = dataTable.Rows[0]["顧客名"].ToString() + " " + dataTable.Rows[0]["顧客名2"].ToString();
                            this.顧客担当者名.Text = dataTable.Rows[0]["顧客担当者名"].ToString();
                            this.電話番号.Text = dataTable.Rows[0]["電話番号"].ToString();
                            this.ファックス番号.Text = dataTable.Rows[0]["ＦＡＸ番号"].ToString();
                        }
                        else
                        {
                            this.顧客名.Text = null;
                            this.顧客担当者名.Text = null;
                            this.電話番号.Text = null;
                            this.ファックス番号.Text = null;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print("_SetCustomerInfo - " + ex.GetType().Name + " : " + ex.Message);
            }
        }

        private void LoadData(string codeString, int editionNumber = -1)
        {
            // ヘッダ部の表示
            LoadHeader(this, codeString, editionNumber);

            // 明細部の表示
            LoadDetails(this.見積明細1.Detail, codeString, editionNumber);

            // 動作を制御する
            LockData(this, this.IsDecided || this.IsDeleted, "見積コード", "見積版数");

            this.見積版数.Enabled = true;
            this.改版ボタン.Enabled = this.IsLastEdition &&
                ((!this.WithApproval && this.IsDecided) || (this.WithApproval && this.IsApproved)) &&
                !this.IsDeleted;

            見積明細1.Detail.AllowUserToAddRows = !this.IsDecided && !this.IsDeleted;
            見積明細1.Detail.AllowUserToDeleteRows = !this.IsDecided && !this.IsDeleted;
            見積明細1.Detail.ReadOnly = !(!this.IsDecided && !this.IsDeleted); //readonlyなのでaccessと真偽が逆になる  

            this.コマンド複写.Enabled = true;
            this.コマンド削除.Enabled = this.IsLastEdition && !this.IsDeleted;
            this.コマンド見積書.Enabled = !this.IsDeleted;
            this.コマンド送信.Enabled = this.IsLastEdition &&
                ((!this.WithApproval && this.IsDecided) || (this.WithApproval && this.IsApproved)) &&
                !this.IsDeleted;

            this.コマンド承認.Enabled = this.IsDecided && this.WithApproval && !this.IsApproved && !this.IsDeleted;
            this.コマンド確定.Enabled = ((!this.WithApproval && !this.IsDecided) ||
                                        (this.WithApproval && !this.IsApproved)) &&
                                       !this.IsDeleted;

            ChangedData(false);
        }

        /// <summary>
        /// ヘッダ部を読み込む
        /// </summary>
        /// <param name="formObject"></param>
        /// <param name="codeString">見積コード</param>
        /// <param name="editionNumber">見積版数</param>
        /// <returns></returns>
        private bool LoadHeader(Form formObject, string codeString, int editionNumber = -1)
        {
            bool loadHeader = false;
            string strSQL;

            try
            {
                Connect();

                if (editionNumber == -1)
                {
                    strSQL = "SELECT * FROM V見積ヘッダ WHERE 見積コード='" + codeString + "'";
                }
                else
                {
                    strSQL = "SELECT * FROM V見積ヘッダ WHERE 見積コード='" + codeString + "' AND 見積版数=" + editionNumber;
                }
                VariableSet.SetTable2Form(this, strSQL, cn, "担当者コード", "納入場所", "支払条件", "有効期間");

                if (!string.IsNullOrEmpty(見積日.Text))
                {
                    if (DateTime.TryParse(this.見積日.Text, out DateTime tempDate))
                    {
                        見積日.Text = tempDate.ToString("yyyy/MM/dd");
                    }
                }

                this.確定.Text = string.IsNullOrEmpty(確定日時.Text) ? "" : "■";
                this.承認.Text = string.IsNullOrEmpty(承認者コード.Text) ? "" : "■";
                this.削除.Text = string.IsNullOrEmpty(削除日時.Text) ? "" : "■";

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
        private bool LoadDetails(GcMultiRow multiRow, string codeString, int editionNumber = -1)
        {
            bool loadDetails = false;
            string strSQL;

            try
            {
                Connect();

                strSQL = $"SELECT * FROM T見積明細 WHERE 見積コード='{codeString}' AND 見積版数={editionNumber} ORDER BY 行番号";

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

        /// <summary>
        /// 現在のデータを保存する
        /// </summary>
        /// <param name="SaveCode"></param>
        /// <param name="SaveEdition"></param>
        /// <returns></returns>
        private bool SaveData(string saveCode, int saveEdition = -1)
        {
            try
            {
                Connect();
                DateTime dteNow = GetServerDate(cn);
                Control objControl1 = this.作成日時;
                Control objControl2 = this.作成者コード;
                Control objControl3 = this.作成者名;
                Control objControl4 = this.更新日時;
                Control objControl5 = this.更新者コード;
                Control objControl6 = this.更新者名;

                string varSaved1 = null;
                string varSaved2 = null;
                string varSaved3 = null;
                string varSaved4 = null;
                string varSaved5 = null;
                string varSaved6 = null;

                bool isNewData = this.IsNewData;

                if (isNewData)
                {
                    varSaved1 = objControl1.Text;
                    varSaved2 = objControl2.Text;
                    varSaved3 = objControl3.Text;

                    objControl1.Text = dteNow.ToString("yyyy/MM/dd HH:dd:ss");
                    objControl2.Text = LoginUserCode;
                    objControl3.Text = LoginUserFullName;
                }

                varSaved4 = objControl4.Text;
                varSaved5 = objControl5.Text;
                varSaved6 = objControl6.Text;

                // 値の設定
                objControl4.Text = dteNow.ToString("yyyy/MM/dd HH:dd:ss");
                objControl5.Text = LoginUserCode;
                objControl6.Text = LoginUserFullName;

                // 登録処理
                if (RegTrans(saveCode, saveEdition, cn))
                {
                    // 登録成功
                    return true;
                }
                else
                {
                    // 登録失敗
                    if (isNewData)
                    {
                        objControl1.Text = varSaved1;
                        objControl2.Text = varSaved2;
                        objControl3.Text = varSaved3;
                    }
                    objControl4.Text = varSaved4;
                    objControl5.Text = varSaved5;
                    objControl6.Text = varSaved6;

                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_SaveData - " + ex.Message);
                return false;
            }
        }

        public bool RegTrans(string codeString, int editionNumber, SqlConnection cn)
        {
            bool success = false;

            try
            {
                // トランザクション開始
                SqlTransaction transaction = cn.BeginTransaction();

                //明細部の受注コードと受注版数を更新する
                this.見積明細1.UpdateCodeAndEdition(codeString, editionNumber);

                try
                {
                    // ヘッダ部の登録
                    if (!SaveHeader(codeString, editionNumber, cn, transaction))
                    {
                        transaction.Rollback();  // 変更をキャンセル
                        return false;
                    }

                    // 明細部の登録

                    if (!SaveDetails(codeString, editionNumber, cn, transaction))
                    {
                        transaction.Rollback();  // 変更をキャンセル
                        return false;
                    }

                    // トランザクション完了
                    transaction.Commit();
                    success = true;
                }
                catch (Exception ex)
                {
                    // トランザクション中にエラーが発生した場合はロールバック
                    transaction.Rollback();
                    Console.WriteLine($"RegTrans-: {ex.Message}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"RegTrans-: {ex.Message}");
            }
            finally
            {
                if (cn.State == System.Data.ConnectionState.Open)
                {
                    cn.Close();  // 接続を閉じる
                }
            }

            return success;
        }

        private bool SaveHeader(string codeString, int editionNumber, SqlConnection cn, SqlTransaction transaction)
        {

            try
            {
                string strwhere = $"見積コード= '{codeString}' AND 見積版数= {editionNumber}";

                if (!DataUpdater.UpdateOrInsertDataFrom(this, cn, "T見積", strwhere, "見積コード", transaction, "見積版数", "担当者コード", "納入場所", "支払条件", "有効期間"))
                {
                    //保存できなかった時の処理 catchで対応する
                    throw new Exception();
                }

                return true;

            }
            catch (Exception ex)
            {
                コマンド登録.Enabled = true;
                MessageBox.Show("データの保存中にエラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }
        }

        public bool SaveDetails(string codeString, int editionNumber, SqlConnection cn, SqlTransaction transaction)
        {
            try
            {
                string strwhere = $"見積コード= '{codeString}' AND 見積版数= {editionNumber}";
                //明細部の登録
                if (!DataUpdater.UpdateOrInsertDetails(this.見積明細1.Detail, cn, "T見積明細", strwhere, "見積コード", transaction))
                {
                    //保存できなかった時の処理                    
                    throw new Exception();
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("データの保存中にエラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

        }

        /// <summary>
        /// 現在のデータを複写する
        /// </summary>
        /// <param name="codeString">複写先コード</param>
        /// <param name="editionNumber">複写先版数</param>
        /// <returns></returns>
        private bool CopyData(string codeString, int editionNumber)
        {
            try
            {
                // 明細部のキー情報を設定する
                見積明細1.UpdateCodeAndEdition(codeString, editionNumber);

                // キー情報を設定する
                this.見積コード.Text = codeString;
                if (editionNumber != -1)
                {
                    this.見積版数.Text = editionNumber.ToString();
                }

                // 初期値を設定する
                this.見積日.Text = DateTime.Now.ToString("yyyy/MM/dd");
                // this.担当者コード.Value = LoginUserCode; // コメントアウトされていた行
                // this.要承認.Value = 0; // コメントアウトされていた行
                this.作成日時.Text = null;
                this.作成者コード.Text = null;
                this.作成者名.Text = null;
                this.更新日時.Text = null;
                this.更新者コード.Text = null;
                this.更新者名.Text = null;
                this.確定日時.Text = null;
                this.確定者コード.Text = null;
                this.承認日時.Text = null;
                this.承認者コード.Text = null;
                this.承認者名.Text = null;
                this.削除日時.Text = null;
                this.削除者コード.Text = null;

                this.確定.Text = string.IsNullOrEmpty(確定日時.Text) ? "" : "■";
                this.承認.Text = string.IsNullOrEmpty(承認者コード.Text) ? "" : "■";
                this.削除.Text = string.IsNullOrEmpty(削除日時.Text) ? "" : "■";

                return true;
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_CopyData - " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 指定された見積データへの削除のマーキング
        /// </summary>
        /// <param name="connectionObject"></param>
        /// <param name="codeString">コード</param>
        /// <param name="editionNumber">版数（■無い場合は-1を指定すること！）</param>
        /// <param name="deleteTime"></param>
        /// <param name="deleteUser"></param>
        /// <returns>成功=False,失敗=True</returns>
        private bool SetDeleted(SqlConnection connectionObject, string codeString, int editionNumber, DateTime deleteTime, string deleteUser)
        {
            SqlTransaction transaction = connectionObject.BeginTransaction();  // トランザクション開始
            try
            {
                string strKey;
                string strUpdate;

                if (editionNumber == -1)
                {
                    strKey = "見積コード='" + codeString + "'";
                }
                else
                {
                    strKey = "見積コード='" + codeString + "' AND 見積版数=" + editionNumber;
                }

                if (this.IsDeleted)  // ■GUIから判断しても良いものか？
                {
                    strUpdate = "削除日時=NULL,削除者コード=NULL";
                }
                else
                {
                    strUpdate = "削除日時='" + deleteTime.ToString("yyyy-MM-dd HH:mm:ss") + "',削除者コード='" + deleteUser + "'";
                }

                SqlCommand command = new SqlCommand("UPDATE T見積 SET " + strUpdate +
                    ",更新日時='" + deleteTime.ToString("yyyy-MM-dd HH:mm:ss") + "',更新者コード='" + deleteUser + "' WHERE " + strKey, connectionObject, transaction);
                command.ExecuteNonQuery();

                // トランザクション完了
                transaction.Commit();

                // GUI更新
                if (this.IsDeleted)
                {
                    削除日時.Text = null;
                    削除者コード.Text = null;
                }
                else
                {
                    削除日時.Text = deleteTime.ToString("yyyy-MM-dd HH:mm:ss");
                    削除者コード.Text = deleteUser;
                    削除.Text = "■";
                }

                return false;
            }
            catch (Exception ex)
            {
                transaction.Rollback(); // 変更をキャンセル
                Debug.Print("_DeleteData - " + ex.Message);
                return true;
            }
        }

        /// <summary>
        /// 指定された版数の見積データを完全削除し、前版を有効にする
        /// </summary>
        /// <param name="connectionObject"></param>
        /// <param name="codeString"></param>
        /// <param name="editionNumber"></param>
        /// <param name="deleteTime"></param>
        /// <param name="deleteUser"></param>
        /// <returns>成功=False,失敗=True</returns>
        private bool UndoEdition(SqlConnection connectionObject, string codeString, int editionNumber, DateTime opTime, string opUser)
        {
            SqlTransaction transaction = connectionObject.BeginTransaction();  // トランザクション開始
            try
            {
                string strKey;

                strKey = "見積コード='" + codeString + "' AND 見積版数=" + editionNumber;

                using (SqlCommand command = new SqlCommand("DELETE T見積 WHERE " + strKey))
                {
                    command.ExecuteNonQuery();
                }
                using (SqlCommand command = new SqlCommand("DELETE T見積明細 WHERE " + strKey))
                {
                    command.ExecuteNonQuery();
                }

                strKey = "見積コード='" + codeString + "' AND 見積版数=" + (editionNumber - 1);
                using (SqlCommand command = new SqlCommand("UPDATE T見積 SET 削除日時=NULL,削除者コード=NULL" + ",更新日時='" + opTime.ToString("yyyy-MM-dd HH:mm:ss")
                    + "',更新者コード='" + opUser + "' WHERE " + strKey))
                {
                    command.ExecuteNonQuery();
                }

                // トランザクション完了
                transaction.Commit();

                return false;
            }
            catch (Exception ex)
            {
                transaction.Rollback(); // 変更をキャンセル
                Debug.Print("_UndoEdition - " + ex.Message);
                return true;
            }
        }

        private void コマンド新規_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.DoubleBuffered = true;

                if (this.ActiveControl == this.コマンド新規)
                {
                    GetNextControl(コマンド新規, false).Focus();
                }

                // 変更がある
                if (this.IsChanged)
                {
                    var intRes = MessageBox.Show("変更内容を登録しますか？", "新規コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    switch (intRes)
                    {
                        case DialogResult.Yes:
                            // 登録処理
                            if (!SaveData(CurrentCode, CurrentEdition))
                            {
                                MessageBox.Show("エラーのため登録できません。", "新規コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }
                            break;
                        case DialogResult.Cancel:
                            return;
                    }
                }

                if (!GoNewMode())
                {
                    MessageBox.Show("エラーのため新規モードへ移行できません。", "新規コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }


            }
            finally
            {
                this.DoubleBuffered = false;
                Cursor.Current = Cursors.Default;
            }
        }

        private void コマンド読込_Click(object sender, EventArgs e)
        {
            try
            {
                this.DoubleBuffered = true;

                Connect();

                //変更されていないときの処理
                if (!this.IsChanged)
                {
                    // 新規モードで且つコードが取得済みのときはコードを戻す
                    if (this.IsNewData && !string.IsNullOrEmpty(this.CurrentCode) && this.CurrentEdition == 1)
                    {
                        // 採番された番号を戻す
                        if (!FunctionClass.Recycle(cn, this.CurrentCode))
                        {
                            MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine +
                                "見積コード　：　" + this.CurrentCode, "読込コマンド", MessageBoxButtons.OK);
                        }
                    }

                    // 読込モードへ移行する
                    if (!GoModifyMode())
                    {
                        goto Err_コマンド読込_Click;
                    }

                    goto Bye_コマンド読込_Click;
                }

                // 変更されているときは登録確認を行う
                var intRes = MessageBox.Show("変更内容を登録しますか？", "読込コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (intRes)
                {
                    case DialogResult.Yes:
                        if (IsErrorData("見積コード")) goto Bye_コマンド読込_Click;

                        // 登録処理
                        if (!SaveData(this.CurrentCode, this.CurrentEdition))
                        {
                            MessageBox.Show("エラーのため登録できません。", "読込コマンド", MessageBoxButtons.OK);
                            goto Bye_コマンド読込_Click;
                        }
                        break;
                    case DialogResult.No:
                        // 新規モードで且つコードが取得済みのときはコードを戻す
                        if (this.IsNewData && !string.IsNullOrEmpty(this.CurrentCode) && this.CurrentEdition == 1)
                        {
                            // 採番された番号を戻す
                            if (!FunctionClass.Recycle(cn, this.CurrentCode))
                            {
                                MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine +
                                "見積コード　：　" + this.CurrentCode, "読込コマンド", MessageBoxButtons.OK);
                            }
                        }
                        break;
                    case DialogResult.Cancel:
                        goto Bye_コマンド読込_Click;
                        break;
                }

                // 読込モードへ移行する
                if (!GoModifyMode())
                {
                    goto Err_コマンド読込_Click;
                }
            }
            catch (Exception ex)
            {
                goto Err_コマンド読込_Click;
            }

        Bye_コマンド読込_Click:
            this.コマンド削除.Enabled = false;
            this.コマンド見積書.Enabled = false;
            return;

        Err_コマンド読込_Click:
            MessageBox.Show("エラーが発生しました。" + Environment.NewLine +
                "[ " + BASE_CAPTION + " ]を終了します。", "読込コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Close();
        }

        private void コマンド複写_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ActiveControl == this.コマンド複写)
                {
                    GetNextControl(コマンド複写, false).Focus();
                }

                Connect();

                if (CopyData(採番(cn, CH_ESTIMATE), 1))
                {
                    // 変更された
                    ChangedData(true);

                    // ヘッダ部制御
                    LockData(this, false);

                    this.見積日.Focus();
                    // this.担当者コード.Value = LoginUserCode; // コメントアウトされていた行
                    this.改版ボタン.Enabled = false;
                    this.コマンド新規.Enabled = false;
                    this.コマンド読込.Enabled = true;
                    this.コマンド承認.Enabled = false;

                    // 明細部制御
                    見積明細1.Detail.AllowUserToAddRows = true;
                    見積明細1.Detail.AllowUserToDeleteRows = true;
                    見積明細1.Detail.ReadOnly = false; //readonlyなのでaccessと真偽が逆になる   
                }
                else
                {
                    throw new InvalidOperationException("CopyData時:エラー"); ;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "複写コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void コマンド削除_Click(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();

            try
            {
                // 第１版のデータは無効化し、第２版以降のデータはレコードを削除する
                // ■削除には２種類あり、見積そのものを削除するのか、前版に戻すのかを確認する必要がある
                // 現バージョンでは前版が存在するものについては前版に戻す

                // 確認メッセージを表示し、ユーザーから応答を得る
                string strCode = this.CurrentCode;
                int intEdition = this.CurrentEdition;
                string strHeadCode;
                string strMsg;
                string strMsgPlus;
                DialogResult intRes;


                if (this.ActiveControl == this.コマンド削除)
                {
                    GetNextControl(コマンド削除, false).Focus();
                }

                if (1 < intEdition)
                {
                    // 版数が２版以上のとき
                    strMsg = $"見積コード　：　{strCode}{Environment.NewLine}{Environment.NewLine}" +
                             $"この見積データの全ての版を削除しますか？{Environment.NewLine}" +
                             $"削除後、元に戻すことはできません。{Environment.NewLine}{Environment.NewLine}" +
                             $"[はい]を選択した場合、見積データ自体を削除します。{Environment.NewLine}" +
                             $"[いいえ]を選択した場合、（第 {intEdition} 版）のみを削除し、前版が有効になります。";
                    intRes = MessageBox.Show(strMsg, "削除コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                }
                else
                {
                    // 版数が初版のとき
                    strMsg = $"見積コード　：　{strCode}{Environment.NewLine}{Environment.NewLine}" +
                             $"この見積データを削除します。{Environment.NewLine}" +
                             $"削除するには[OK]を選択してください。{Environment.NewLine}{Environment.NewLine}" +
                             $"※削除後も参照することはできますが、元に戻すことはできません。";
                    intRes = MessageBox.Show(strMsg, "削除コマンド", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                }

                // ユーザーからの応答を元に処理を分岐する
                if (intRes == DialogResult.Cancel)
                    return;
                else
                {
                    // ログインユーザーが認証ユーザーでなければ認証を要求する
                    // 認証ユーザーを設定する
                    string str1 = this.IsApproved ? this.承認者コード.Text : this.担当者コード.Text;

                    if (LoginUserCode != str1)
                    {
                        using (F_認証 authForm = new F_認証())
                        {
                            authForm.args = str1;
                            authForm.ShowDialog();

                            if (string.IsNullOrEmpty(strCertificateCode))
                            {
                                MessageBox.Show("認証できません。\n操作は取り消されました。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return;
                            }
                        }

                    }

                    // 削除処理
                    fn.DoWait("削除しています...");

                    Connect();

                    if (intRes == DialogResult.Yes || intRes == DialogResult.OK)
                    {
                        // 応答がYesあるいはOKのとき
                        if (SetDeleted(cn, strCode, intEdition, GetServerDate(cn), LoginUserCode))
                            throw new Exception();
                    }
                    else if (intRes == DialogResult.No)
                    {
                        // 応答がNoのとき
                        if (UndoEdition(cn, strCode, intEdition, GetServerDate(cn), LoginUserCode))
                        {
                            // 前版のデータを表示？
                            コマンド新規_Click(this, EventArgs.Empty);
                        }
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("_コマンド削除_Click -" + ex.Message);
            }
            finally
            {
                if (fn.WaitForm != null)
                {
                    fn.WaitForm.Close();
                }
            }

        }

        private void コマンド見積書_Click(object sender, EventArgs e)
        {
            // TODO:帳票出力
        }

        private void コマンド送信_Click(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();
            try
            {
                if (this.ActiveControl == this.コマンド送信)
                {
                    GetNextControl(コマンド送信, false).Focus();
                }

                // ファックス番号の取得
                string strFaxNumber = this.ファックス番号.Text;

                // ファックス番号の確認
                if (string.IsNullOrEmpty(strFaxNumber))
                {
                    MessageBox.Show("ファックス番号を入力してください。", "送信コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // 送信確認
                DialogResult result = MessageBox.Show($"ファックスを送信しますか？{Environment.NewLine}{Environment.NewLine}" +
                                                      $"送信先FAX番号： {strFaxNumber}{Environment.NewLine}{Environment.NewLine}" +
                                                      $"[はい] - 見積書を送信します{Environment.NewLine}" +
                                                      "[いいえ] - 操作を取り消します", "送信コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                    return;

                fn.DoWait("見積書を送信しています...");
                Application.DoEvents();
                Connect();


                string server = cn.ConnectionString.Contains(",1436") || cn.ConnectionString.Contains("\\unet_secondary")
                    ? "secondary"
                    : "primary";

                string param = $" -sv:{server.Replace(" ", "_")}" +
                       $" -sf:estimate,{(this.ファックス番号.Text.TrimEnd()).Replace(" ", "_")},{(this.見積コード.Text.TrimEnd().Replace(" ", "_"))}" +
                       $",{this.見積版数.Text.ToString().Replace(" ", "_")},{(this.顧客名.Text.TrimEnd()).Replace(" ", "_")}" +
                       $",{(this.電話番号.Text.TrimEnd()).Replace(" ", "_")}";

                param = $" -user:{LoginUserName}{param}";

                if(!FunctionClass.GetShell(param)){
                    return;

                }

                MessageBox.Show("見積書の送信を完了しました。\n[ ファックス管理 ] で送信状況を確認できます。",
                    "送信コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                Debug.Print($"{Name}_コマンド送信_Click - {ex.GetType().Name}: {ex.Message}");
                MessageBox.Show("見積書の送信中にエラーが発生しました。", "送信コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (fn.WaitForm != null)
                {
                    fn.WaitForm.Close();
                }
                Application.DoEvents();
            }
        }

        private void 文書グループ登録ボタン_Click(object sender, EventArgs e)
        {
            Connect();
                        
            string strDocumentCode = CurrentCode;
            if (string.IsNullOrEmpty(strDocumentCode)) return;
            // 本データがグループに登録済みかどうかを判断する
            switch (FunctionClass.DetectGroupMember(cn, strDocumentCode))
            {
                case 0:
                    // グループに登録済みでない場合
                    F_グループ form = new F_グループ();
                    form.args= strDocumentCode;
                    form.ShowDialog();
                    break;
                case 1:
                    // グループに登録済みの場合
                    F_リンク form2 = new F_リンク();
                    form2.args = strDocumentCode;
                    form2.ShowDialog();
                    break;
                case -1:
                    // エラーのため実行できない場合
                    MessageBox.Show("エラーのため実行できません。", this.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
            }
        }

        private void コマンド承認_Click(object sender, EventArgs e)
        {
            try
            {
                string strHeadCode;
                string varSaved1;  // 承認日保存用
                string varSaved2;  // 承認者コード保存用
                string varSaved3;  // 承認者名保存用

                if (this.ActiveControl == this.コマンド承認)
                {
                    GetNextControl(コマンド承認, false).Focus();
                }

                Connect();

                // 担当者の長のコードを得る
                strHeadCode = GetHeadCode(cn, 担当者コード.Text);

                // 認証処理
                if (LoginUserCode == strHeadCode)
                {
                    // ログオンユーザーが担当者の長なら認証者コードにユーザーコードを設定する
                    strCertificateCode = LoginUserCode;
                }
                else
                {
                    using (F_認証 authForm = new F_認証())
                    {
                        authForm.args = strHeadCode;
                        authForm.ShowDialog();

                        if (string.IsNullOrEmpty(strCertificateCode))
                        {
                            MessageBox.Show("認証に失敗しました。\n承認できません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }
                }

                // 承認処理
                varSaved1 = 承認日時.Text;
                varSaved2 = 承認者コード.Text;
                varSaved3 = 承認者名.Text;

                if (IsApproved)
                {
                    承認日時.Text = null;
                    承認者コード.Text = null;
                    承認者名.Text = null;
                }
                else
                {
                    承認日時.Text = GetServerDate(cn).ToString("yyyy-MM-dd HH:mm:ss");
                    承認者コード.Text = strCertificateCode;
                    承認者名.Text = EmployeeName(cn, strCertificateCode);
                    承認.Text = "■";
                }

                // 表示データを登録する
                if (RegTrans(CurrentCode, CurrentEdition, cn))
                {
                    // 登録成功（読込モードで呼び出した状態にならないといけない）
                    // 設定が未完了のため、該当箇所はコメントアウト
                    // 要検証
                    /*
                    見積コード.Enabled = true;
                    コマンド複写.Enabled = true;
                    コマンド削除.Enabled = true;
                    コマンド承認.Enabled = IsDecided;
                    コマンド登録.Enabled = false;
                    */
                }
                else
                {
                    // 登録失敗
                    承認日時.Text = varSaved1;
                    承認者コード.Text = varSaved2;
                    承認者名.Text = varSaved3;
                    MessageBox.Show("承認できませんでした。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                // GUI更新
                改版ボタン.Enabled = IsApproved;
                コマンド送信.Enabled = IsApproved;
                コマンド承認.Enabled = IsDecided && WithApproval && !IsApproved && !IsDeleted;
                コマンド確定.Enabled = ((!WithApproval && !IsDecided) || (WithApproval && !IsApproved)) && !IsDeleted;
            }
            catch (Exception ex)
            {
                Debug.Print($"{Name}_コマンド承認_Click - {ex.Message}");
            }
        }

        private void コマンド確定_Click(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();

            try
            {
                string varSaved1;  // 確定日保存用（エラー発生時の対策）
                string varSaved2;  // 確定者保存用（エラー発生時の対策）

                Connect();

                if (this.ActiveControl == this.コマンド確定)
                {
                    GetNextControl(コマンド確定, false).Focus();
                }

                // 登録時におけるエラーチェック
                // F12キーでの登録の場合は明細のチェックはしなくていいのか？
                if (!IsDecided)
                {
                    if (IsErrorData("見積コード", "見積版数"))
                        return;
                }

                // 承認が不要である場合のみ確定の実行を確認する
                if (!WithApproval)
                {
                    if (MessageBox.Show("確定しますか？\n確定後、元に戻すことはできません。", "確定コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return;
                }

                // 明細行が並べ替えられているときはその旨を知らせる
                if (見積明細1.IsOrderByOn)
                {
                    if (MessageBox.Show("明細行が並べ替えられています。\n並べ替えを解除して登録しますか？", "確定コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        見積明細1.CancelOrderBy();
                    else
                        return;
                }

                // 登録前の確定情報を保存しておく
                varSaved1 = 確定日時.Text;
                varSaved2 = 確定者コード.Text;

                // 確定情報を設定する
                if (IsDecided)
                {
                    確定日時.Text = null;
                    確定者コード.Text = null;
                }
                else
                {
                    確定日時.Text = GetServerDate(cn).ToString("yyyy-MM-dd HH:mm:ss");
                    確定者コード.Text = LoginUserCode;
                    確定.Text = "■";
                }

                fn.DoWait("登録しています...");

                // 表示データを登録する
                if (SaveData(CurrentCode, CurrentEdition))
                {
                    ChangedData(false);
                    // 新規モードのときは読込モードへ移行する
                    if (IsNewData)
                    {
                        コマンド新規.Enabled = true;
                        コマンド読込.Enabled = false;
                    }
                    コマンド送信.Enabled = IsLastEdition &&
                        ((!WithApproval && IsDecided) || (WithApproval && IsApproved)) &&
                        !IsDeleted;
                    コマンド承認.Enabled = IsDecided && WithApproval && !IsApproved && !IsDeleted;
                    コマンド確定.Enabled = ((!WithApproval && !IsDecided) || (WithApproval && !IsApproved)) && !IsDeleted;
                    改版ボタン.Enabled = IsLastEdition &&
                        ((!WithApproval && IsDecided) || (WithApproval && IsApproved)) &&
                        !IsDeleted;
                }
                else
                {
                    確定日時.Text = varSaved1;
                    確定者コード.Text = varSaved2;
                    確定.Text = "";
                    MessageBox.Show("確定できませんでした。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                // 確定状態によって動作を制御する
                LockData(this, IsDecided || IsDeleted, "見積コード", "見積版数");
                // AllowEditsはコメントアウト
                // AllowEdits = !IsDecided;
                見積明細1.Detail.AllowUserToAddRows = !IsDecided;
                見積明細1.Detail.AllowUserToDeleteRows = !IsDecided;
                見積明細1.Detail.ReadOnly = IsDecided; //readonlyなのでaccessと真偽が逆になる 
            }
            catch (Exception ex)
            {
                Debug.Print($"{Name}_コマンド確定_Click - {ex.Message}");
            }
            finally
            {
                if (fn.WaitForm != null)
                {
                    fn.WaitForm.Close();
                }
            }
        }

        private void コマンド登録_Click(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();

            try
            {
                Connect();
                // PreviousControlがオプショングループのときにAccessの応答がなくなる
                if (this.ActiveControl == this.コマンド登録)
                {
                    GetNextControl(コマンド登録, false).Focus();
                }

                // 明細行数のみ確認する。エラー検出は確定時に行う
                if (IsErrorDetails())
                    return;

                // 明細行が並べ替えられているときはその旨を知らせる
                if (見積明細1.IsOrderByOn)
                {
                    if (MessageBox.Show("明細行が並べ替えられています。\n並べ替えを解除して登録しますか？", "登録コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        見積明細1.CancelOrderBy();
                    else
                        return;
                }

                fn.DoWait("登録しています...");

                if (SaveData(CurrentCode, CurrentEdition))
                {
                    // 登録成功（読込モードで呼び出した状態にならないといけない）
                    ChangedData(false);
                    // 新規モードのときは読込モードへ移行する
                    if (IsNewData)
                    {
                        // データ追加成功により版数一覧を更新しておく
                        SetEditions(CurrentCode); //見積版数.Requery();
                        コマンド新規.Enabled = true;
                        コマンド読込.Enabled = false;
                    }
                    コマンド承認.Enabled = IsDecided;
                    コマンド確定.Enabled = true;
                }
                else
                {
                    // 登録失敗
                    MessageBox.Show("登録できませんでした。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"{Name}_コマンド登録_Click - {ex.Message}");
            }
            finally
            {
                if (fn.WaitForm != null)
                {
                    fn.WaitForm.Close();
                }
            }
        }

        private void コマンド終了_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void F_見積_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space: //コンボボックスならドロップダウン
                    {
                        Control activeControl = this.ActiveControl;
                        if (activeControl is ComboBox)
                        {
                            ComboBox activeComboBox = (ComboBox)activeControl;
                            activeComboBox.DroppedDown = true;
                        }
                    }
                    break;
                case Keys.Return:
                    // 複数行入力可能な項目はEnterでフォーカス移動させない
                    switch (this.ActiveControl.Name)
                    {
                        case "備考":
                        case "メモ":
                            return;
                    }
                    SelectNextControl(ActiveControl, true, true, true, true);
                    e.Handled = true;
                    e.SuppressKeyPress = true;

                    break;
                case Keys.F1:
                    if (コマンド新規.Enabled)
                    {
                        コマンド新規.Focus();
                        コマンド新規_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F2:
                    if (コマンド読込.Enabled)
                    {
                        コマンド読込.Focus();
                        コマンド読込_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F3:
                    if (コマンド複写.Enabled)
                    {
                        コマンド複写.Focus();
                        コマンド複写_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F4:
                    if (コマンド削除.Enabled)
                    {
                        コマンド削除.Focus();
                        コマンド削除_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F5:
                    if (コマンド見積書.Enabled)
                    {
                        コマンド見積書.Focus();
                        コマンド見積書_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F6:
                    if (コマンド送信.Enabled)
                    {
                        コマンド送信.Focus();
                        コマンド送信_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F8:
                    if (文書グループ登録ボタン.Enabled)
                    {
                        文書グループ登録ボタン.Focus();
                        文書グループ登録ボタン_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F9:
                    if (コマンド承認.Enabled)
                    {
                        コマンド承認.Focus();
                        コマンド承認_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F10:
                    if (コマンド確定.Enabled)
                    {
                        コマンド確定.Focus();
                        コマンド確定_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F11:
                    if (コマンド登録.Enabled)
                    {
                        コマンド登録.Focus();
                        コマンド登録_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F12:
                    if (コマンド終了.Enabled)
                    {
                        コマンド終了.Focus();
                        コマンド終了_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
            }
        }

        private void 見積コード_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) e.Cancel = true;
        }

        private void 見積コード_Validated(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            UpdatedControl((Control)sender);
        }

        private void 見積コード_TextChanged(object sender, EventArgs e)
        {
            LimitText(((Control)sender), 11);
        }

        private void 見積版数_Validating(object sender, CancelEventArgs e)
        {
            if (IsError((Control)sender) == true) e.Cancel = true;
        }

        private void 見積版数_Validated(object sender, EventArgs e)
        {
            if (setCombo) return;

            UpdatedControl((Control)sender);
        }

        private void 改版ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ActiveControl == this.改版ボタン)
                {
                    GetNextControl(改版ボタン, false).Focus();
                }

                // 複写に成功すればインターフェースを更新する
                if (CopyData(this.CurrentCode, this.CurrentEdition + 1))
                {
                    // 変更された
                    ChangedData(true);
                    // ヘッダ部制御
                    FunctionClass.LockData(this, false);
                    this.見積日.Focus();
                    this.コマンド新規.Enabled = false;
                    this.コマンド読込.Enabled = true;
                    this.コマンド承認.Enabled = false;
                    this.改版ボタン.Enabled = this.IsLastEdition &&
                        ((!this.WithApproval && this.IsDecided) || (this.WithApproval && this.IsApproved)) &&
                        !this.IsDeleted;

                    // 明細部制御
                    見積明細1.Detail.AllowUserToAddRows = true;
                    見積明細1.Detail.AllowUserToDeleteRows = true;
                    見積明細1.Detail.ReadOnly = false; //readonlyなのでaccessと真偽が逆になる  

                    
                }
                else
                {
                    MessageBox.Show("エラーが発生しました。", "改版", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_改版ボタン_Click - " + ex.Message);
                MessageBox.Show("エラーが発生しました。", "改版", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void 見積日_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) e.Cancel = true;
        }

        private void 見積日_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;

            ChangedData(true);
        }

        private void 見積日選択ボタン_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            F_カレンダー calendar = new F_カレンダー();

            if (!string.IsNullOrEmpty(見積日.Text))
            {
                calendar.args = 見積日.Text;
            }

            if (calendar.ShowDialog() == DialogResult.OK && 見積日.Enabled && !見積日.ReadOnly)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = calendar.SelectedDate;

                // 日付コントロールに選択した日付を設定
                見積日.Text = selectedDate;
                見積日.Focus();
            }
        }

        private void 担当者コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 34, 170 }, new string[] { "Display1", "Display2" });
            担当者コード.Invalidate();
            担当者コード.DroppedDown = true;
        }

        private void 担当者コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            担当者名.Text = ((DataRowView)担当者コード.SelectedItem)?.Row.Field<String>("Display2")?.ToString();
            ChangedData(true);
        }

        private void 件名_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            LimitText(((Control)sender), 48);
            ChangedData(true);
        }

        private void 顧客コード_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) e.Cancel = true;
        }

        private void 顧客コード_Validated(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            UpdatedControl((Control)sender);
        }

        private void 顧客コード_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            LimitText(((Control)sender), 8);
            ChangedData(true);
        }

        private void 顧客選択ボタン_Click(object sender, EventArgs e)
        {
            if (this.IsApproved)
            {
                MessageBox.Show("承認後の修正はできません。", this.ActiveControl.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                F_検索 SearchForm = new F_検索();
                SearchForm.FilterName = "顧客名フリガナ";
                if (SearchForm.ShowDialog() == DialogResult.OK && 顧客コード.Enabled && !顧客コード.ReadOnly)
                {
                    string SelectedCode = SearchForm.SelectedCode;

                    顧客コード.Text = SelectedCode;

                    if (IsError(顧客コード) == true)
                    {
                        // エラー時は値を元に戻す
                        顧客コード.Undo();
                    }
                    else
                    {
                        UpdatedControl(顧客コード);
                    }
                }
            }
        }

        private void 顧客名_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) e.Cancel = true;
        }

        private void 顧客名_Validated(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            UpdatedControl((Control)sender);
        }

        private void 顧客名_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            LimitText(((Control)sender), 120);
            ChangedData(true);
        }

        private void 顧客担当者名_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) e.Cancel = true;
        }

        private void 顧客担当者名_Validated(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            UpdatedControl((Control)sender);
        }

        private void 顧客担当者名_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            LimitText(((Control)sender), 120);
            ChangedData(true);
        }

        private void 電話番号_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) e.Cancel = true;
        }

        private void 電話番号_Validated(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            UpdatedControl((Control)sender);
        }

        private void 電話番号_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            LimitText(((Control)sender), 20);
            ChangedData(true);
        }

        private void ファックス番号_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) e.Cancel = true;
        }

        private void ファックス番号_Validated(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            UpdatedControl((Control)sender);
        }

        private void ファックス番号_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            LimitText(((Control)sender), 20);
            ChangedData(true);
        }

        private void 納期_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            LimitText(((Control)sender), 44);
            ChangedData(true);
        }

        private void 納入場所_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            LimitText(((Control)sender), 44);
            ChangedData(true);
        }

        private void 支払条件_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            LimitText(((Control)sender), 44);
            ChangedData(true);
        }

        private void 有効期間_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            LimitText(((Control)sender), 44);
            ChangedData(true);
        }

        private void 要承認_Validating(object sender, CancelEventArgs e)
        {
            if (IsError((Control)sender) == true) e.Cancel = true;
        }

        private void 要承認_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            LimitText(((Control)sender), 20);
            ChangedData(true);
        }

        private void 合計金額表示_Validating(object sender, CancelEventArgs e)
        {
            if (IsError((Control)sender) == true) e.Cancel = true;
        }

        private void 合計金額表示_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            ChangedData(true);
        }

        private void 備考_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            LimitText(((Control)sender), 784);
            ChangedData(true);
        }

        private void メモ_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            LimitText(((Control)sender), 1000);
            ChangedData(true);
        }

        private void 見積版数_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■版数を選択あるいは入力します。";
        }

        private void 担当者コード_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■担当者を選択します。　■ドロップダウンリストを表示するには[Alt]+[↓]キーを押します。";
        }

        private void 件名_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■最大入力文字数は24文字（全角文字）です。";
        }

        private void 顧客コード_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■顧客の顧客コードを入力あるいは選択します。";
        }

        private void 顧客名_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■最大入力文字数は60文字（全角文字）です。";
        }

        private void 顧客担当者名_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■最大入力文字数は60文字（全角文字）です。";
        }

        private void 電話番号_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■顧客の電話番号を入力します。";
        }

        private void ファックス番号_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■顧客のファックス番号を入力します。　■入力した番号はFAX送信先番号として利用されます。";
        }

        private void 納期_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■最大入力文字数は22文字（全角文字）です。";
        }

        private void 納入場所_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■最大入力文字数は22文字（全角文字）です。　■ドロップダウンリストを表示するには[Alt]+[↓]キーを押します。";
        }

        private void 支払条件_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■最大入力文字数は22文字（全角文字）です。　■ドロップダウンリストを表示するには[Alt]+[↓]キーを押します。";
        }

        private void 有効期間_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■最大入力文字数は22文字（全角文字）です。　■ドロップダウンリストを表示するには[Alt]+[↓]キーを押します。";
        }

        private void 要承認_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■承認が必要かどうかを選択します。　■ドロップダウンリストを表示するには[Alt]+[↓]キーを押します。";
        }

        private void 合計金額表示_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■見積書に合計金額を表示するかどうかを選択します。　■ドロップダウンリストを表示するには[Alt] + [↓]キーを押します。";
        }

        private void 備考_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■最大入力文字数は392文字（全角文字）です。　■入力行数が８行を超えると見積書へ正しく表示されません。";
        }

        private void メモ_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■入力した内容は見積書へ表示されません。　■最大入力文字数は500文字（全角文字）です。";
        }

        private void 見積コード_KeyDown(object sender, KeyEventArgs e)
        {
            string strCode;

            switch (e.KeyCode)
            {
                case Keys.Return:
                    strCode = 見積コード.Text;
                    if (string.IsNullOrEmpty(strCode))
                        return;

                    strCode = FormatCode(CH_ESTIMATE, strCode);
                    if (strCode != Convert.ToString(見積コード.Text))
                    {
                        見積コード.Text = strCode;
                    }
                    break;
            }
        }
    }
}


