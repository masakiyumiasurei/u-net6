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
using System.Data.Common;
using GrapeCity.Win.MultiRow;
using System.ComponentModel;
using System.DirectoryServices;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Globalization;
using System.Security.Cryptography;
using Microsoft.VisualBasic;
using System.Threading.Channels;
using static u_net.CommonConstants;
using static u_net.Public.FunctionClass;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.Transactions;
using Microsoft.Identity.Client.NativeInterop;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;

namespace u_net
{
    public partial class F_受注 : Form
    {
        public string varOpenArgs = ""; // オープン時引数保存用

        private SqlConnection cn;
        private bool setCombo = true;
        private string tmpCCode = ""; // 依頼主コード退避用
        private string beforjuchu = "";
        private string beforkokyaku = "";
        private string befornouhin = "";
        private string beforshuka = "";


        private string BASE_CAPTION = "受注（製図指図書）";

        /// <summary>
        /// 現在の受注コードを取得する
        /// </summary>
        public string CurrentCode
        {
            get
            {
                return string.IsNullOrEmpty(受注コード.Text) ? "" : 受注コード.Text;
            }
        }

        /// <summary>
        /// 現在の受注版数を取得する
        /// </summary>
        public int CurrentEdition
        {
            get
            {
                int result;
                return int.TryParse(受注版数.Text, out result) ? result : 0;
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
        /// 現在のデータが完了承認されているかどうかを取得する
        /// </summary>
        public bool IsApprovedCompletion
        {
            get
            {
                return !string.IsNullOrEmpty(完了承認者コード.Text);
            }
        }

        /// <summary>
        /// 現在のデータが請求済みかどうかを取得する
        /// </summary>
        public bool IsBilled
        {
            get
            {
                return !string.IsNullOrEmpty(請求コード.Text);
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
        /// 現在のデータの処理が完了しており、完了承認待ちかどうかを取得する
        /// </summary>
        public bool IsFinished
        {
            get
            {
                return !string.IsNullOrEmpty(ManufacturingCompletionApprovedDate.Text);
            }
        }

        /// <summary>
        /// 現在のデータが無効になっているかどうかを取得する
        /// </summary>
        public bool IsInvalid
        {
            get
            {
                return !string.IsNullOrEmpty(無効日.Text);
            }
        }

        /// <summary>
        /// 現在のデータが生産計画済みかどうかを取得する
        /// </summary>
        public bool IsProductionPlanned
        {
            get
            {
                return !string.IsNullOrEmpty(ProductionPlanned.Text);
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
        /// シリーズ在庫が締められているかどうかを取得する（現在の受注データ中シリーズ１件以上）
        /// </summary>
        public bool SeriesStockClosed
        {
            get
            {
                return !string.IsNullOrEmpty(在庫締め.Text);
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

            //実行中フォーム起動
            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");

            //コンボボックスの設定
            OriginalClass ofn = new OriginalClass();

            ofn.SetComboBox(受注コード, "SELECT A.受注コード AS Value, A.最新版数, A.受注コード AS Display, { fn REPLACE(STR(CONVERT(bit, T受注.無効日), 1, 0), '1', '×') } AS Display2 " +
                "FROM T受注 INNER JOIN (SELECT TOP 100 受注コード, MAX(受注版数) AS 最新版数 FROM T受注 GROUP BY 受注コード ORDER BY T受注.受注コード DESC) A ON T受注.受注コード = A.受注コード AND T受注.受注版数 = A.最新版数 ORDER BY A.受注コード DESC");
            //ofn.SetComboBox(受注版数, "SELECT 1 AS Value, 1 AS Display, '' AS Display2");
            //this.受注版数.SelectedIndex = 0;

            ofn.SetComboBox(ClientCode, "SELECT Code AS Value, Name AS Display FROM ClientDataSource");
            ofn.SetComboBox(納品書送付コード, "SELECT right(replace(str(納品書送付コード),' ','0'),2) AS Value, right(replace(str(納品書送付コード),' ','0'),2) AS Display, 送付処理 AS Display2 FROM M納品書送付処理");
            ofn.SetComboBox(請求書送付コード, "SELECT right(replace(str(請求書送付コード),' ','0'),2) AS Value, right(replace(str(請求書送付コード),' ','0'),2) AS Display, 送付処理 AS Display2 FROM M請求書送付処理");
            ofn.SetComboBox(発送方法コード, "SELECT right(replace(str(発送方法コード),  ' ','0'),2) AS Value, right(replace(str(発送方法コード),  ' ','0'),2) AS Display, 発送方法 AS Display2 FROM M発送方法");
            ofn.SetComboBox(自社担当者コード, "SELECT 社員コード AS Value, 社員コード AS Display, 氏名 AS Display2 FROM M社員 WHERE (退社 IS NULL) AND (部 = N'営業部') AND (削除日時 IS NULL) AND ([パート] = 0) ORDER BY ふりがな");
            this.TaxCalcCode.DataSource = new KeyValuePair<byte, String>[] {
                new KeyValuePair<byte, String>(1, "明細行"),
                new KeyValuePair<byte, String>(2, "伝票"),
                new KeyValuePair<byte, String>(3, "請求書"),
                new KeyValuePair<byte, String>(4, "非課税"),
            };
            this.TaxCalcCode.DisplayMember = "Value";
            this.TaxCalcCode.ValueMember = "Key";
            this.税端数処理.DataSource = new KeyValuePair<byte, String>[] {
                new KeyValuePair<byte, String>(1, "切り捨て"),
                new KeyValuePair<byte, String>(2, "切り上げ"),
                new KeyValuePair<byte, String>(3, "四捨五入"),
            };
            this.税端数処理.DisplayMember = "Value";
            this.税端数処理.ValueMember = "Key";
            ofn.SetComboBox(PackingSlipInputCode, "SELECT Code AS Value, Code AS Display, Name AS Display2 FROM PackingSlipOutputNote");
            ofn.SetComboBox(InvoiceInputCode, "SELECT '01' As Value, '01' As Display, '有り' As Display2 UNION ALL SELECT '02' As Value, '02' As Display, '無し' As Display2");
            ofn.SetComboBox(ReceiptCommentCode, "SELECT '01' As Value, '01' As Display, '表示する' As Display2 UNION ALL SELECT '02' As Value, '02' As Display, '表示しない' As Display2");
            ofn.SetComboBox(InvoiceFaxCode, "SELECT '01' As Value, '01' As Display, '有り' As Display2 UNION ALL SELECT '02' As Value, '02' As Display, '無し' As Display2");

            this.状態.ForeColor = Color.Black;
            try
            {
                this.SuspendLayout();

                // 作業用テーブルを開く
                Connect();

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
                    // 読込モードへ
                    if (!GoModifyMode())
                    {
                        throw new Exception("初期化に失敗しました。");
                    }

                    if (varOpenArgs.IndexOf(',') == -1)
                    {
                        // オープン時引数にカンマ（,）がないときは最新版データを表示する
                        this.受注コード.Text = varOpenArgs;
                        UpdatedControl(this.受注コード);
                        ChangedData(false);
                    }
                    else
                    {
                        this.受注コード.Text = varOpenArgs.Substring(0, varOpenArgs.IndexOf(','));
                        this.受注版数.Text = varOpenArgs.Substring(varOpenArgs.IndexOf(',') + 1);
                        UpdatedControl(this.受注コード);

                        ChangedData(false);
                    }

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
                this.ResumeLayout();
                fn.WaitForm.Close();
            }
        }

        private void Form_Unload(object sender, FormClosingEventArgs e)
        {
            try
            {
                Connect();

                // 新規モードのときに登録しない場合は内部の更新データを元に戻す
                if (IsNewData && !string.IsNullOrEmpty(CurrentCode) && CurrentEdition == 1)
                {
                    // 採番された番号を戻す
                    if (!FunctionClass.Recycle(cn, CurrentCode))
                    {
                        MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine + Environment.NewLine +
                                        "受注コード　：　" + CurrentCode, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }

                // 修正されているときは登録確認を行う
                if (IsChanged)
                {
                    var intRes = MessageBox.Show("変更内容を登録しますか？", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    switch (intRes)
                    {
                        case DialogResult.Yes:
                            // 登録処理
                            if (!RegTrans(CurrentCode, CurrentEdition))
                            {
                                if (MessageBox.Show("エラーのため登録できませんでした。" + Environment.NewLine + Environment.NewLine +
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
                // ウィンドウの配置を保存する（Accessにて、受注では保存していない）
                //LocalSetting localSetting = new LocalSetting();
                //localSetting.SavePlace(CommonConstants.LoginUserCode, this);
            }
        }

        private bool GoNewMode()
        {
            try
            {
                //ヘッダ部を初期化する
                VariableSet.SetControls(this);
                this.受注コード.Text = 採番(cn, "A");

                setCombo = true;
                OriginalClass ofn = new OriginalClass();
                ofn.SetComboBox(受注版数, "SELECT 1 AS Value, 1 AS Display, '' AS Display2");
                this.受注版数.SelectedIndex = 0;


                this.受注日.Text = DateTime.Now.ToString("yyyy/MM/dd");
                this.発送方法コード.SelectedValue = "01";
                this.発送方法.Text = ((DataRowView)発送方法コード.SelectedItem)?.Row.Field<String>("Display2")?.ToString();

                //出荷情報初期化
                this.PaymentConfirmation.Checked = false;
                this.PackingSlipInputCode.SelectedValue = "04";
                this.PackingSlipInput.Text = ((DataRowView)PackingSlipInputCode.SelectedItem)?.Row.Field<String>("Display2")?.ToString();
                this.InvoiceInputCode.SelectedValue = "02";
                this.InvoiceInput.Text = ((DataRowView)InvoiceInputCode.SelectedItem)?.Row.Field<String>("Display2")?.ToString();
                this.InvoiceFaxCode.SelectedValue = "02";
                this.InvoiceFax.Text = ((DataRowView)InvoiceFaxCode.SelectedItem)?.Row.Field<String>("Display2")?.ToString();
                this.ReceiptCommentCode.SelectedValue = "01";
                this.ReceiptComment.Text = ((DataRowView)ReceiptCommentCode.SelectedItem)?.Row.Field<String>("Display2")?.ToString();
                this.ProductionPlanned.Text = "0";

                setCombo = false;
                //明細部を初期化する
                LoadDetails(this.受注明細1.Detail, this.CurrentCode);

                //ヘッダ部を制御する
                LockData(this, false);
                this.受注日.Focus();
                this.受注コード.Enabled = false;
                this.受注版数.Enabled = false;
                this.改版ボタン.Enabled = false;
                this.受注承認ボタン.Enabled = false;
                this.否認ボタン.Enabled = false;
                this.受注完了承認ボタン.Enabled = false;
                this.コマンド新規.Enabled = false;
                this.コマンド読込.Enabled = true;
                this.コマンド複写.Enabled = false;
                this.コマンド削除.Enabled = false;
                this.コマンド承認.Enabled = false;
                this.コマンド確定.Enabled = false;
                this.コマンド登録.Enabled = false;

                //明細部を制御する
                受注明細1.Detail.AllowUserToAddRows = true;
                受注明細1.Detail.AllowUserToDeleteRows = true;
                受注明細1.Detail.ReadOnly = false; //readonlyなのでaccessと真偽が逆になる 

                this.状態.Text = "";
                this.状態.ForeColor = Color.Black;
                ChangedData(false);

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
                LoadDetails(this.受注明細1.Detail, this.CurrentCode);

                // 未変更状態にする
                ChangedData(false);
                LockData(this, true, "受注コード");

                this.受注コード.Enabled = true;
                this.受注版数.Enabled = true;
                this.受注コード.Focus();
                this.コマンド新規.Enabled = true;
                this.コマンド読込.Enabled = false;
                this.コマンド複写.Enabled = false;
                this.コマンド登録.Enabled = false;

                return true;
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_GoModifyMode - " + ex.Message);
                return false;
            }
        }

        public void ChangedData(bool dataChanged)
        {
            if (dataChanged)
            {
                this.Text = this.Text.Replace("*", "") + "*";
            }
            else
            {
                this.Text = this.Text.Replace("*", "");
            }

            // コードにフォーカスがある状態でサブフォームから呼び出されたときの対処
            if (this.ActiveControl == this.受注コード)
            {
                this.注文番号.Focus();
            }
            if (this.ActiveControl == this.受注版数)
            {
                this.注文番号.Focus();
            }

            受注版数.Enabled = !dataChanged;
            コマンド複写.Enabled = !dataChanged;
            コマンド削除.Enabled = !dataChanged;

            if (dataChanged)
            {
                コマンド承認.Enabled = false;
                コマンド確定.Enabled = true;
                受注承認ボタン.Enabled = false;
            }

            コマンド登録.Enabled = dataChanged;
        }

        private void UpdatedControl(Control controlObject)
        {
            FunctionClass fn = new FunctionClass();
            OriginalClass ofn = new OriginalClass();
            string sqlQuery = "";

            try
            {
                switch (controlObject.Name)
                {
                    case "受注コード":

                        fn.DoWait("読み込んでいます...");

                        setCombo = true;

                        // 版数のソース更新
                        UpdateEditionList(controlObject.Text);

                        // OpenArgsが設定されていなければ版数を最新版とする
                        // 開いてからコードを変えて読み込むときはOpenArgsはnullに
                        // 設定されているため、最新版となる
                        if (string.IsNullOrEmpty(varOpenArgs))
                        {
                            if (受注版数.Items.Count > 0) this.受注版数.SelectedIndex = 0;
                        }
                        else
                        {
                            //コンボボックスのコントロールソースがセットされた時にIndex[0]になるためもう一度代入
                            this.受注版数.Text = varOpenArgs.Substring(varOpenArgs.IndexOf(',') + 1);
                        }
                        // 状態の表示
                        SetEditionStatus();

                        // ヘッダ部の表示
                        LoadHeader(this, this.CurrentCode, this.CurrentEdition);

                        // 状態の表示（その２）■SetEditionStatusに統合すべき
                        if (!this.IsApproved)
                        {
                            if (this.IsDecided)
                            {
                                this.状態.Text = "承認待ち";
                                状態.ForeColor = Color.Black;
                            }
                            else
                            {
                                this.状態.Text = "未確定";
                                状態.ForeColor = Color.Black;
                            }
                        }

                        // 明細部の表示
                        LoadDetails(this.受注明細1.Detail, this.CurrentCode, this.CurrentEdition);

                        // 合計欄を更新する
                        SetTotalAmount(string.IsNullOrEmpty(this.TaxRate.Text) ? 0m : decimal.Parse(this.TaxRate.Text));

                        // 依頼主のデータソースを更新する
                        sqlQuery = "SELECT Code AS Value, Name AS Display FROM ClientDataSource";
                        if (!string.IsNullOrEmpty(this.顧客コード.Text))
                        {
                            sqlQuery += " WHERE CustomerCode = " + this.顧客コード.Text;
                        }
                        sqlQuery += " ORDER BY OrderNumber";
                        ofn.SetComboBox(this.ClientCode, sqlQuery);
                        if (string.IsNullOrEmpty(this.tmpCCode))
                        {
                            this.ClientCode.SelectedIndex = -1;
                        }
                        else
                        {
                            this.ClientCode.SelectedValue = this.tmpCCode;
                        }

                        // 動作の制御
                        LockData(this, this.IsDecided || this.IsInvalid, "受注コード");

                        this.受注版数.Enabled = true; // 版数を編集可にする
                        受注明細1.Detail.AllowUserToAddRows = !(this.IsDecided || this.IsInvalid);
                        受注明細1.Detail.AllowUserToDeleteRows = !(this.IsDecided || this.IsInvalid);
                        受注明細1.Detail.ReadOnly = (this.IsDecided || this.IsInvalid); //readonlyなのでaccessと真偽が逆になる 

                        // 端数チェック
                        this.帳端処理.Checked = !string.IsNullOrEmpty(this.請求予定日.Text);
                        this.請求予定日ラベル.Enabled = !string.IsNullOrEmpty(this.請求予定日.Text);
                        this.請求予定日.Enabled = !string.IsNullOrEmpty(this.請求予定日.Text);
                        this.請求予定日選択ボタン.Enabled = !string.IsNullOrEmpty(this.請求予定日.Text);
                        this.改版ボタン.Enabled = !this.IsInvalid &&
                            this.IsApproved &&
                            !this.IsApprovedCompletion &&
                            !(this.状態.Text == "改版中");

                        // 受注承認更新による受注修正の可・不可を設定する
                        if (this.IsApproved)
                        {
                            this.否認ボタン.Enabled = false;
                        }
                        else
                        {
                            this.否認ボタン.Enabled = 1 < this.CurrentEdition;
                        }
                        this.コマンド複写.Enabled = true;
                        this.コマンド削除.Enabled = !(this.IsApprovedCompletion || this.IsInvalid || this.状態.Text == "改版中");
                        this.コマンド承認.Enabled = this.IsDecided && !this.IsApproved && !this.IsInvalid;
                        this.コマンド確定.Enabled = (!this.IsApproved) && (!this.IsInvalid);
                        this.受注承認ボタン.Enabled = this.IsDecided && !this.IsApproved && !this.IsInvalid;
                        this.受注完了承認ボタン.Enabled = this.IsFinished && !this.IsBilled;

                        

                        fn.WaitForm.Close();

                        break;

                    case "受注版数":

                        fn.DoWait("読み込んでいます...");

                        setCombo = true;

                        // 状態の表示
                        SetEditionStatus();

                        // ヘッダ部の表示
                        LoadHeader(this, this.CurrentCode, this.CurrentEdition);

                        // 状態の表示（その２）■SetEditionStatusに統合すべき
                        if (!this.IsApproved)
                        {
                            if (this.IsDecided)
                            {
                                this.状態.Text = "承認待ち";
                            }
                            else
                            {
                                this.状態.Text = "未確定";
                            }
                        }

                        // 明細部の表示
                        LoadDetails(this.受注明細1.Detail, this.CurrentCode, this.CurrentEdition);

                        // 合計欄を更新する
                        SetTotalAmount(string.IsNullOrEmpty(this.TaxRate.Text) ? 0m : decimal.Parse(this.TaxRate.Text));

                        // 依頼主のデータソースを更新する
                        sqlQuery = "SELECT Code AS Value, Name AS Display FROM ClientDataSource";
                        if (!string.IsNullOrEmpty(this.顧客コード.Text))
                        {
                            sqlQuery += " WHERE CustomerCode = " + this.顧客コード.Text;
                        }
                        sqlQuery += " ORDER BY OrderNumber";
                        ofn.SetComboBox(this.ClientCode, sqlQuery);
                        if (string.IsNullOrEmpty(this.tmpCCode))
                        {
                            this.ClientCode.SelectedIndex = -1;
                        }
                        else
                        {
                            this.ClientCode.SelectedValue = this.tmpCCode;
                        }

                        // 動作の制御
                        LockData(this, this.IsDecided || this.IsInvalid, "受注コード");
                        this.受注版数.Enabled = true; // 版数を編集可にする
                        受注明細1.Detail.AllowUserToAddRows = !this.IsDecided && !this.IsInvalid;
                        受注明細1.Detail.AllowUserToDeleteRows = !this.IsDecided && !this.IsInvalid;
                        受注明細1.Detail.ReadOnly = !(!this.IsDecided && !this.IsInvalid); //readonlyなのでaccessと真偽が逆になる 

                        // 端数チェック
                        this.帳端処理.Checked = !string.IsNullOrEmpty(this.請求予定日.Text);
                        this.請求予定日ラベル.Enabled = !string.IsNullOrEmpty(this.請求予定日.Text);
                        this.請求予定日.Enabled = !string.IsNullOrEmpty(this.請求予定日.Text);
                        this.請求予定日選択ボタン.Enabled = !string.IsNullOrEmpty(this.請求予定日.Text);
                        this.改版ボタン.Enabled = !this.IsInvalid &&
                            this.IsApproved &&
                            !this.IsApprovedCompletion &&
                            !(this.状態.Text == "改版中");

                        // 受注承認更新による受注修正の可・不可を設定する
                        if (this.IsApproved)
                        {
                            this.否認ボタン.Enabled = false;
                        }
                        else
                        {
                            this.否認ボタン.Enabled = 1 < this.CurrentEdition;
                        }
                        this.コマンド複写.Enabled = true;
                        this.コマンド削除.Enabled = !(this.IsApprovedCompletion || this.IsInvalid || this.状態.Text == "改版中");
                        this.コマンド承認.Enabled = this.IsDecided && !this.IsApproved && !this.IsInvalid;
                        this.コマンド確定.Enabled = (!this.IsApproved) && (!this.IsInvalid);
                        this.受注承認ボタン.Enabled = this.IsDecided && !this.IsApproved && !this.IsInvalid;
                        this.受注完了承認ボタン.Enabled = this.IsFinished && !this.IsBilled;

                        ChangedData(false);

                        fn.WaitForm.Close();
                        break;

                    case "顧客コード":
                        // 顧客の関連情報を表示する
                        SetCusInfo(controlObject.Text);

                        // 依頼主のデータソースを更新する
                        sqlQuery = "SELECT Code AS Value, Name AS Display FROM ClientDataSource";
                        if (!string.IsNullOrEmpty(this.顧客コード.Text))
                        {
                            sqlQuery += " WHERE CustomerCode = " + this.顧客コード.Text;
                        }
                        sqlQuery += " ORDER BY OrderNumber";
                        ofn.SetComboBox(this.ClientCode, sqlQuery);
                        this.ClientCode.SelectedIndex = -1;

                        // 発送先を設定する
                        SetAddress(controlObject.Text, GetShipNumber());

                        break;

                    case "受注納期":
                        if (!this.帳端処理.Checked)
                        {
                            Connect();

                            if (string.IsNullOrEmpty(this.受注納期.Text))
                            {
                                this.TaxRate.Text = GetTaxRate(cn, DateTime.Now).ToString("0.##");
                            }
                            else
                            {
                                this.TaxRate.Text = GetTaxRate(cn, DateTime.Parse(this.受注納期.Text)).ToString("0.##");
                            }
                        }

                        // ■納期時点の顧客情報に基づいて税額計算方法と税端数処理も変更する必要があるのでは？
                        // 合計欄を更新する
                        SetTotalAmount(string.IsNullOrEmpty(this.TaxRate.Text) ? 0m : decimal.Parse(this.TaxRate.Text));

                        break;
                    case "請求予定日":
                        if (string.IsNullOrEmpty(this.請求予定日.Text))
                        {
                            this.TaxRate.Text = GetTaxRate(cn, DateTime.Now).ToString("0.##");
                        }
                        else
                        {
                            this.TaxRate.Text = GetTaxRate(cn, DateTime.Parse(this.請求予定日.Text)).ToString("0.##");
                        }
                        break;
                    case "自社担当者コード":
                        自社担当者名.Text = ((DataRowView)自社担当者コード.SelectedItem)?.Row.Field<String>("Display2")?.ToString();
                        break;
                    case "PackingSlipInputCode":
                        if (this.PackingSlipInputCode.Text == "04")
                        {
                            this.PackingSlipNote.Text = "";
                        }
                        break;
                    case "InvoiceInputCode":
                        if (this.InvoiceInputCode.Text == "02")
                        {
                            this.InvoiceNote.Text = "";
                        }
                        break;
                    case "ReceiptCommentCode":
                        if (this.ReceiptCommentCode.Text == "02")
                        {
                            //this.ReceiptComment.Text = "";
                        }
                        break;
                    case "InvoiceFaxCode":
                        if (this.InvoiceFaxCode.Text == "02")
                        {
                            this.InvoiceFaxToName.Text = "";
                            this.InvoiceFaxToContact.Text = "";
                            this.InvoiceFaxToNumber.Text = "";
                        }
                        break;
                    case "帳端処理":
                        ChangedData(true);
                        if (this.帳端処理.Checked)
                        {
                            if (string.IsNullOrEmpty(this.請求予定日.Text))
                            {
                                this.TaxRate.Text = GetTaxRate(cn, DateTime.Now).ToString("0.##");
                            }
                            else
                            {
                                this.TaxRate.Text = GetTaxRate(cn, DateTime.Parse(this.請求予定日.Text)).ToString("0.##");
                            }

                            this.請求予定日ラベル.Enabled = true;
                            this.請求予定日.Enabled = true;
                            this.請求予定日選択ボタン.Enabled = true;
                            this.請求予定日.Focus();
                        }
                        else
                        {
                            this.請求予定日.Text = "";

                            if (string.IsNullOrEmpty(this.受注納期.Text))
                            {
                                this.TaxRate.Text = GetTaxRate(cn, DateTime.Now).ToString("0.##");
                            }
                            else
                            {
                                this.TaxRate.Text = GetTaxRate(cn, DateTime.Parse(this.受注納期.Text)).ToString("0.##");
                            }

                            this.請求予定日ラベル.Enabled = false;
                            this.請求予定日.Enabled = false;
                            this.請求予定日選択ボタン.Enabled = false;
                        }
                        break;
                    case "税端数処理":
                        // 合計欄を更新する
                        SetTotalAmount(string.IsNullOrEmpty(this.TaxRate.Text) ? 0m : decimal.Parse(this.TaxRate.Text));
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

                setCombo = false;
            }
        }

        private void UpdateEditionList(string codeString)
        {
            //版数のソースを更新する
            //CodeString - 受注コード
            string sqlQuery = "SELECT 受注版数 as Value, 受注版数 as Display," +
                              "{ fn REPLACE(STR(CONVERT(bit, 承認者コード + '1'), 1, 0), '1', '■') } AS Display2 " +
                              "FROM T受注 " +
                              "WHERE (受注コード = N'" + codeString + "') " +
                              "ORDER BY 受注版数 DESC";

            //コンボボックスの設定
            OriginalClass ofn = new OriginalClass();

            ofn.SetComboBox(受注版数, sqlQuery);
        }

        private void SetEditionStatus()
        {
            try
            {
                // 版数が存在しないとき、すなわちコードが存在しないときは空白を表示する。
                if (string.IsNullOrEmpty(this.受注版数.Text))
                {
                    this.状態.Text = string.Empty;
                    return;
                }

                // 削除データは最優先で判断する
                if (!string.IsNullOrEmpty(((DataRowView)受注コード.SelectedItem)?.Row.Field<string>("Display2")?.ToString()))
                {
                    this.状態.Text = "削除済み";
                    this.状態.ForeColor = Color.Black;
                    return;
                }

                // 表示データの承認状態を取得する（this.isapprovedには依存したくないから）
                if (((DataRowView)受注版数.SelectedItem)?.Row.Field<string>("Display2")?.ToString() == "")
                {
                    if (this.IsDecided)
                    {
                        this.状態.Text = "承認待ち";
                        this.状態.ForeColor = Color.Black;
                    }
                    else
                    {
                        this.状態.Text = "未確定";
                        this.状態.ForeColor = Color.Black;
                    }
                }
                else
                {


                    if (this.受注版数.Items.Count > 0 && this.受注版数.Text == 受注版数.GetItemText(this.受注版数.Items[0]))
                    {
                        this.状態.Text = "最新版";
                        this.状態.ForeColor = Color.Red;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(((DataRowView)受注版数.Items[0])?.Row.Field<string>("Display2")?.ToString()))
                        {
                            this.状態.Text = "改版中";
                            this.状態.ForeColor = Color.Black;
                        }
                        else
                        {
                            this.状態.Text = "旧版";
                            this.状態.ForeColor = Color.Black;
                        }
                    }

                }
                //else
                //{
                //    this.状態.Text = "";
                //    this.状態.ForeColor = Color.Black;
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(this.Name + "_SetEditionStatus - " + ex.GetType().Name + " : " + ex.Message);
            }
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

                // 後述の依頼主のデータソース更新処理により、コードがリセットされてしまう為退避
                this.tmpCCode = this.ClientCode.SelectedValue?.ToString();

                if (!string.IsNullOrEmpty(受注日.Text))
                {
                    if (DateTime.TryParse(this.受注日.Text, out DateTime tempDate))
                    {
                        受注日.Text = tempDate.ToString("yyyy/MM/dd");
                    }
                }

                if (!string.IsNullOrEmpty(受注納期.Text))
                {
                    if (DateTime.TryParse(this.受注納期.Text, out DateTime tempDate))
                    {
                        受注納期.Text = tempDate.ToString("yyyy/MM/dd");
                    }
                }

                if (!string.IsNullOrEmpty(出荷予定日.Text))
                {
                    if (DateTime.TryParse(this.出荷予定日.Text, out DateTime tempDate))
                    {
                        出荷予定日.Text = tempDate.ToString("yyyy/MM/dd");
                    }
                }

                if (!string.IsNullOrEmpty(請求予定日.Text))
                {
                    if (DateTime.TryParse(this.請求予定日.Text, out DateTime tempDate))
                    {
                        請求予定日.Text = tempDate.ToString("yyyy/MM/dd");
                    }
                }

                if (!string.IsNullOrEmpty(TaxRate.Text))
                {
                    if (decimal.TryParse(this.TaxRate.Text, out decimal tempRate))
                    {
                        TaxRate.Text = tempRate.ToString("0.##");
                    }
                }

                this.確定.Text = string.IsNullOrEmpty(確定日時.Text) ? "" : "■";
                this.承認.Text = string.IsNullOrEmpty(承認者コード.Text) ? "" : "■";
                this.生産計画.Text = string.IsNullOrEmpty(ProductionPlanned.Text?.Replace("0", "")) ? "" : "■";
                this.完了.Text = string.IsNullOrEmpty(完了承認者コード.Text) ? "" : "■";
                //this.在庫締め.Text = string.IsNullOrEmpty(確定日時.Text) ? "" : "■";
                this.削除.Text = string.IsNullOrEmpty(無効日.Text) ? "" : "■";

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

        private bool RegTrans(string codeString, int editionNumber, bool Approved = false)
        {
            Connect();
            DateTime dtmNow = GetServerDate(cn);

            this.登録日.Text = dtmNow.ToString("yyyy/MM/dd HH:dd:ss");

            //明細部の受注コードと受注版数を更新する
            this.受注明細1.UpdateCodeAndEdition(codeString, editionNumber);

            using (SqlTransaction transaction = cn.BeginTransaction())
            {
                try
                {
                    string strwhere = " 受注コード='" + codeString + "' and 受注版数=" + editionNumber;

                    // ヘッダ部の登録
                    if (!DataUpdater.UpdateOrInsertDataFrom(this, cn, "T受注", strwhere, "受注コード", transaction, "受注版数"))
                    {
                        transaction.Rollback(); // 変更をキャンセル
                        return false;
                    }
                    // 明細部の登録
                    if (!DataUpdater.UpdateOrInsertDetails(this.受注明細1.Detail, cn, "受注明細", strwhere, "受注コード", transaction))
                    {
                        transaction.Rollback(); // 変更をキャンセル
                        return false;
                    }

                    // 承認登録されたとき
                    if (Approved)
                    {
                        // 改版データが承認されたときは旧版を無効にする
                        if (1 < editionNumber)
                        {
                            string strKey = "受注コード='" + codeString + "' AND 受注版数=" + (editionNumber - 1);

                            using (SqlCommand updateCommand = new SqlCommand("UPDATE T受注 SET 無効日=GETDATE() WHERE " + strKey, cn, transaction))
                            {
                                updateCommand.ExecuteNonQuery();
                            }
                        }

                        // 承認後処理
                        if (!ApprovedData(codeString, editionNumber, transaction))
                        {
                            transaction.Rollback(); // 変更をキャンセル
                            return false;
                        }
                    }

                    // トランザクションをコミット
                    transaction.Commit();
                    return true;

                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // 変更をキャンセル
                    Debug.Print(this.Name + "_RegTrans - " + ex.Message);
                    return false;
                }
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
                // ヘッダ部のキー情報を設定する
                this.受注コード.Text = codeString;
                this.受注版数.Text = editionNumber.ToString();

                // 明細部のキー情報を設定する
                受注明細1.UpdateCodeAndEdition(codeString, editionNumber);

                // 進捗情報を初期化する
                確定日時.Text = null;
                確定者コード.Text = null;
                確定.Text = null;
                承認日時.Text = null;
                承認者コード.Text = null;
                承認.Text = null;
                ProductionPlanned.Text = "0";
                生産計画.Text = null;
                在庫締め.Text = null;
                完了承認者コード.Text = null;
                完了.Text = null;
                ManufacturingCompletionApprovedDate.Text = null;
                請求コード.Text = null;
                無効日.Text = null;
                削除.Text = null;
                状態.Text = null;
                PaymentConfirmation.Checked = false;

                return true;
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_CopyData - " + ex.Message);
                return false;
            }
        }

        private bool ApproveCompletion(string codeString, int editionNumber)
        {
            // トランザクション開始
            Connect();
            SqlTransaction transaction = cn.BeginTransaction();
            try
            {
                string strKey = "受注コード ='" + codeString + "' AND 受注版数=" + editionNumber;
                string strSQL = "SELECT * FROM T受注 WHERE " + strKey;


                SqlCommand command = new SqlCommand(strSQL, cn);
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);

                if (dataTable.Rows.Count > 0)
                {
                    if (string.IsNullOrEmpty(dataTable.Rows[0]["完了承認者コード"].ToString()))
                    {
                        strSQL = $"BEGIN " +
                        $"UPDATE T受注 " +
                        $"SET 完了承認者コード = '{LoginUserCode}' " +
                        $"WHERE 受注コード = '{codeString}' AND 受注版数 = {editionNumber} " +
                        $"UPDATE T受注 " +
                        $"SET 完了承認者コード = '000', 非表示 = -1 " +
                        $"WHERE 受注コード = '{codeString}' AND 受注版数 < {editionNumber} " +
                        $"END";

                        SqlCommand updateCommand = new SqlCommand(strSQL, cn, transaction);
                        updateCommand.ExecuteNonQuery();
                    }
                    else
                    {
                        strSQL = $"BEGIN " +
                        $"UPDATE T受注 " +
                        $"SET 完了承認者コード = NULL " +
                        $"WHERE 受注コード = '{codeString}' AND 受注版数 = {editionNumber} " +
                        $"UPDATE T受注 " +
                        $"SET 完了承認者コード = NULL, 非表示 = NULL " +
                        $"WHERE 受注コード = '{codeString}' AND 受注版数 < {editionNumber} " +
                        $"END";

                        SqlCommand updateCommand = new SqlCommand(strSQL, cn, transaction);
                        updateCommand.ExecuteNonQuery();
                    }
                }


                transaction.Commit();
                return true;

            }
            catch (Exception ex)
            {
                transaction.Rollback();

                return false;
            }

        }

        /// <summary>
        /// 承認後処理
        /// </summary>
        /// <param name="codeString"></param>
        /// <param name="editionNumber"></param>
        /// <returns></returns>
        private bool ApprovedData(string codeString, int editionNumber, SqlTransaction transaction)
        {
            try
            {
                bool result = false;

                using (SqlCommand command = new SqlCommand("usp_出荷明細追加直接", cn, transaction))
                {
                    command.CommandType = CommandType.StoredProcedure;

                    // パラメータの追加
                    command.Parameters.AddWithValue("@SalesCode", codeString);
                    command.Parameters.AddWithValue("@Edition", editionNumber);
                    command.Parameters.AddWithValue("@dteZero", 0);

                    command.ExecuteNonQuery();
                }

                result = true;

                return result;
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_ApprovedData - " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// 指定された顧客コードの関連情報を設定する
        /// </summary>
        /// <param name="customerCode">顧客コード</param>
        private void SetCusInfo(string customerCode)
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
                            this.顧客担当者名.Text = dataTable.Rows[0]["顧客担当者名"].ToString();
                            this.自社担当者コード.SelectedValue = dataTable.Rows[0]["自社担当者コード"].ToString();
                            this.自社担当者名.Text = dataTable.Rows[0]["自社担当者名"].ToString();
                            this.TaxCalcCode.SelectedValue = dataTable.Rows[0]["TaxCalcCode"];
                            this.税端数処理.SelectedValue = dataTable.Rows[0]["税端数処理"];
                            this.納品書送付コード.SelectedValue = dataTable.Rows[0]["納品書送付コード"].ToString();
                            //this.納品書送付.Text = 納品書送付コード.Column(1);
                            this.請求書送付コード.SelectedValue = dataTable.Rows[0]["請求書送付コード"].ToString();
                            //this.請求書送付.Text = 請求書送付コード.Column(1);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print("SetCusInfo - " + ex.GetType().Name + " : " + ex.Message);
            }
        }

        /// <summary>
        /// 過去に入力されたデータには税端数処理が未入力になっている。
        /// 互換性を保つ為の独自処理
        /// 今後このプロシージャは使用しない
        /// </summary>
        /// <param name="customerCode"></param>
        private void SetFractionCode(string customerCode)
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
                            this.税端数処理.SelectedValue = dataTable.Rows[0]["税端数処理"].ToString();

                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"SetFractionCode - {ex.Message}");
            }
        }

        private bool InvalidateData(string codeString, int editionNumber)
        {
            try
            {
                // 指定されたデータを無効にする

                // 既に無効日時が設定されているときは、無効にすることはできない。
                if (string.IsNullOrEmpty(this.無効日.Text))
                {
                    return false;
                }

                this.無効日.Text = GetServerDate(cn).ToString("yyyy-MM-dd HH:mm:ss");
                this.削除.Text = "■";

                if (RegTrans(codeString, editionNumber))
                {
                    return true;
                }
                else
                {
                    this.無効日.Text = null;
                    this.削除.Text = null;
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_InvalidateData - " + ex.Message);
                return false;
            }
        }

        private bool DeleteData(SqlConnection connectionObject, string codeString, int editionNumber)
        {
            try
            {
                string strSQL1 = $"DELETE FROM T受注 WHERE 受注コード = '{codeString}' AND 受注版数 = {editionNumber}";
                string strSQL2 = $"DELETE FROM T受注明細 WHERE 受注コード = '{codeString}' AND 受注版数 = {editionNumber}";

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = connectionObject;
                    command.CommandText = strSQL1;
                    connectionObject.Open();
                    command.ExecuteNonQuery();

                    command.CommandText = strSQL2;
                    command.ExecuteNonQuery();

                    connectionObject.Close();
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.Print($"{Name}_DeleteData - {ex.GetType().ToString()} : {ex.Message}");

                if (connectionObject.State == ConnectionState.Open)
                {
                    connectionObject.Close();
                }

                return false;
            }
            finally
            {
                if (connectionObject.State == ConnectionState.Open)
                {
                    connectionObject.Close();
                }
            }
        }

        private int GetShipNumber()
        {
            if (発送先1.Checked == true)
            {
                return 1;
            }
            if (発送先2.Checked == true)
            {
                return 2;
            }
            if (発送先3.Checked == true)
            {
                return 3;
            }
            return 0;
        }
        /// <summary>
        /// 指定された顧客コード・発送先番号から発送先に関する情報を設定する
        /// </summary>
        /// <param name="customerCode">顧客コード</param>
        /// <param name="setNumber">発送先番号</param>
        private void SetAddress(string customerCode, int setNumber)
        {
            try
            {
                Connect();

                string strSELECT = "発送先" + setNumber;
                string strSQL = $"SELECT * FROM V顧客 WHERE 顧客コード='{customerCode}' AND 取引開始日 IS NOT NULL";

                using (SqlCommand command = new SqlCommand(strSQL, cn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count > 0)
                        {
                            // 発送先情報を設定
                            this.発送先名.Text = dataTable.Rows[0][strSELECT + "名"].ToString();
                            this.発送先郵便番号.Text = dataTable.Rows[0][strSELECT + "郵便番号"].ToString();
                            this.発送先住所1.Text = dataTable.Rows[0][strSELECT + "住所1"].ToString();
                            this.発送先住所2.Text = dataTable.Rows[0][strSELECT + "住所2"].ToString();
                            this.発送先TEL.Text = dataTable.Rows[0][strSELECT + "電話番号"].ToString();
                            this.発送先FAX.Text = dataTable.Rows[0][strSELECT + "FAX番号"].ToString();
                            this.発送先メールアドレス.Text = dataTable.Rows[0][strSELECT + "メールアドレス"].ToString();
                            this.発送先担当者名.Text = dataTable.Rows[0][strSELECT + "担当者名"].ToString();
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print("SetAddress - " + ex.GetType().Name + " : " + ex.Message);
            }

        }

        /// <summary>
        /// 処理区分からシリーズ在庫に影響を及ぼす受注データかどうかを判断する
        /// </summary>
        /// <returns></returns>
        private bool CheckWarning(string codeString, int editionNumber)
        {
            bool warning = false;

            try
            {
                Connect();

                using (SqlCommand command = new SqlCommand("SELECT ラインコード AS 処理区分コード FROM 受注明細 WHERE 受注コード='" + codeString + "' AND 受注版数=" + editionNumber, cn))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            string 処理区分コード = reader["処理区分コード"].ToString();

                            if (処理区分コード == "001" || 処理区分コード == "003")
                            {
                                warning = true;
                                break;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_CheckWarning - " + ex.Message);
            }

            return warning;
        }

        private bool IsErrorData(string exFieldName1, string exFieldName2 = null)
        {
            try
            {
                foreach (Control control in Page1.Controls)
                {
                    if (control is TextBox || control is ComboBox)
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


                foreach (Control control in Page2.Controls)
                {
                    if (control is TextBox || control is ComboBox)
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

                    if (control is GroupBox groupBox)
                    {
                        foreach (Control chicontrol in groupBox.Controls)
                        {
                            if (chicontrol is TextBox || chicontrol is ComboBox)
                            {
                                if (chicontrol.Name != exFieldName1 && chicontrol.Name != exFieldName2)
                                {
                                    if (IsError(chicontrol))
                                    {
                                        chicontrol.Focus();
                                        return true;
                                    }
                                }
                            }
                        }
                    }
                }


                // 明細行数のチェック
                int count = 受注明細1.Detail.RowCount - 1;

                if (count == 0)
                {
                    MessageBox.Show("１つ以上の明細が必要です。", "受注", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    受注明細1.Detail.Focus();
                    return true;
                }

                for (int i = 0; i < 受注明細1.Detail.RowCount; i++)
                {
                    Row row = 受注明細1.Detail.Rows[i];

                    if (row.IsNewRow == true) continue;

                    if (row.Cells["原価"].Value?.ToString() == "0")
                    {
                        MessageBox.Show("原価を入力してください。", "原価", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return true;
                    }

                    if (string.IsNullOrEmpty(row.Cells["型番"].DisplayText))
                    {
                        MessageBox.Show("型番を入力してください。", "型番", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return true;
                    }

                    if (string.IsNullOrEmpty(row.Cells["CustomerSerialNumberTo"].DisplayText) && !string.IsNullOrEmpty(row.Cells["CustomerSerialNumberFrom"].DisplayText))
                    {
                        MessageBox.Show("顧客シリアル番号終了を入力してください。", "顧客シリアル番号", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return true;
                    }

                    if (string.IsNullOrEmpty(row.Cells["CustomerSerialNumberFrom"].DisplayText) && !string.IsNullOrEmpty(row.Cells["CustomerSerialNumberTo"].DisplayText))
                    {
                        MessageBox.Show("顧客シリアル番号開始を入力してください。", "顧客シリアル番号", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return true;
                    }
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

                object varValue = controlObject.Text; // Valueプロパティの代わりにTextプロパティを使用
                DateTime inputDate;
                DateTime date1;
                string str1;

                switch (controlObject.Name)
                {
                    case "受注日":
                        if (!DateTime.TryParse(varValue.ToString(), out inputDate))
                        {
                            MessageBox.Show("日付以外は入力できません。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        ((TextBox)controlObject).Text = inputDate.ToString("yyyy/MM/dd");
                        if (DateTime.Now < inputDate)
                        {
                            MessageBox.Show("未来日付は入力できません。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        if (!string.IsNullOrEmpty(this.出荷予定日.Text) && DateTime.TryParse(this.出荷予定日.Text, out date1))
                        {
                            if (date1 < inputDate)
                            {
                                MessageBox.Show("出荷予定日以降の日付は入力できません。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                goto Exit_IsError;
                            }
                        }
                        if (!string.IsNullOrEmpty(this.受注納期.Text) && DateTime.TryParse(this.受注納期.Text, out date1))
                        {
                            if (date1 < inputDate)
                            {
                                MessageBox.Show("受注納期以降の日付は入力できません。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                goto Exit_IsError;
                            }
                        }
                        break;
                    case "注文番号":
                        if (string.IsNullOrEmpty(varValue?.ToString()))
                        {
                            MessageBox.Show(controlObject.Name + " を入力してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        break;
                    case "顧客コード":
                        str1 = GetCustomerName(cn, varValue?.ToString());
                        if (string.IsNullOrEmpty(str1))
                        {
                            MessageBox.Show("有効な顧客コードではありません。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        else
                        {
                            this.顧客名.Text = str1;
                        }
                        break;
                    case "ClientCode":
                        //if (((ComboBox)controlObject).FindStringExact(varValue?.ToString()) == -1)
                        //{
                        //    MessageBox.Show("指定した項目はリストにありません。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    ((ComboBox)controlObject).DroppedDown = true;
                        //    goto Exit_IsError;
                        //}
                        break;
                    case "顧客担当者名":
                        if (!IsLimit(varValue, 30, false, controlObject.Name))
                            goto Exit_IsError;
                        break;
                    case "受注納期":
                        if (string.IsNullOrEmpty(varValue?.ToString()))
                        {
                            MessageBox.Show(controlObject.Name + " を入力してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        if (!DateTime.TryParse(varValue.ToString(), out inputDate))
                        {
                            MessageBox.Show("日付を入力してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        ((TextBox)controlObject).Text = inputDate.ToString("yyyy/MM/dd");
                        if (!string.IsNullOrEmpty(this.受注日.Text) && DateTime.TryParse(this.受注日.Text, out date1))
                        {
                            if (inputDate < date1)
                            {
                                MessageBox.Show("受注日以降（受注日含む）の日付を入力してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                goto Exit_IsError;
                            }
                        }
                        if (!string.IsNullOrEmpty(this.出荷予定日.Text) && DateTime.TryParse(this.出荷予定日.Text, out date1))
                        {
                            if (inputDate < date1)
                            {
                                MessageBox.Show("出荷予定日以前の日付は入力できません。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                goto Exit_IsError;
                            }
                        }
                        break;
                    case "出荷予定日":
                        if (string.IsNullOrEmpty(varValue?.ToString()))
                        {
                            MessageBox.Show(controlObject.Name + " を入力してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        if (!DateTime.TryParse(varValue.ToString(), out inputDate))
                        {
                            MessageBox.Show("日付を入力してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        ((TextBox)controlObject).Text = inputDate.ToString("yyyy/MM/dd");
                        if (!string.IsNullOrEmpty(this.受注日.Text) && DateTime.TryParse(this.受注日.Text, out date1))
                        {
                            if (inputDate < date1)
                            {
                                MessageBox.Show("受注日以降（受注日含む）の日付を入力してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                goto Exit_IsError;
                            }
                        }
                        if (!string.IsNullOrEmpty(this.受注納期.Text) && DateTime.TryParse(this.受注納期.Text, out date1))
                        {
                            if (date1 < inputDate)
                            {
                                MessageBox.Show("受注納期以前の日付を入力してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                goto Exit_IsError;
                            }
                        }
                        break;
                    case "納品書送付コード":
                    case "請求書送付コード":
                    case "発送方法コード":
                        if (((ComboBox)controlObject).FindStringExact(varValue?.ToString()) == -1)
                        {
                            //MessageBox.Show("指定した項目はリストにありません。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ((ComboBox)controlObject).DroppedDown = true;
                            goto Exit_IsError;
                        }
                        if (!IsLimit_N(varValue, 2, 0, controlObject.Name))
                            goto Exit_IsError;
                        break;
                    case "自社担当者コード":
                        if (string.IsNullOrEmpty(varValue?.ToString()))
                        {
                            MessageBox.Show(controlObject.Name + " を入力してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        break;
                    case "PackingSlipInputCode":
                        if (((ComboBox)controlObject).FindStringExact(varValue?.ToString()) == -1)
                        {
                            //MessageBox.Show("指定した項目はリストにありません。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ((ComboBox)controlObject).DroppedDown = true;
                            goto Exit_IsError;
                        }
                        if (string.IsNullOrEmpty(varValue?.ToString()))
                        {
                            MessageBox.Show("伝票記載指示を入力してください。", "伝票記載指示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        else
                        {
                            if (this.PackingSlipInputCode.Text == "02" || this.PackingSlipInputCode.Text == "03" || this.PackingSlipInputCode.Text == "06")
                            {
                                MessageBox.Show("選択できません。", "伝票記載指示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                goto Exit_IsError;
                            }
                        }
                        break;
                    case "InvoiceInputCode":
                        if (((ComboBox)controlObject).FindStringExact(varValue?.ToString()) == -1)
                        {
                            //MessageBox.Show("指定した項目はリストにありません。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ((ComboBox)controlObject).DroppedDown = true;
                            goto Exit_IsError;
                        }
                        if (string.IsNullOrEmpty(varValue?.ToString()))
                        {
                            MessageBox.Show("送り状記載指示を入力してください。", "送り状記載指示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        break;
                    case "ReceiptCommentCode":
                        if (((ComboBox)controlObject).FindStringExact(varValue?.ToString()) == -1)
                        {
                            //MessageBox.Show("指定した項目はリストにありません。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ((ComboBox)controlObject).DroppedDown = true;
                            goto Exit_IsError;
                        }
                        break;
                    case "InvoiceFaxCode":
                        if (((ComboBox)controlObject).FindStringExact(varValue?.ToString()) == -1)
                        {
                            //MessageBox.Show("指定した項目はリストにありません。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Information);
                            ((ComboBox)controlObject).DroppedDown = true;
                            goto Exit_IsError;
                        }
                        if (string.IsNullOrEmpty(varValue?.ToString()))
                        {
                            MessageBox.Show("送り状FAX送付指示を入力してください。", "送り状FAX送付指示", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        break;
                    case "PackingSlipNote":
                        // 納品書の記載が必要のときは入力必須
                        if (this.PackingSlipInputCode.Text != "04" && string.IsNullOrEmpty(varValue?.ToString()))
                        {
                            MessageBox.Show("伝票記載内容を入力してください。", "伝票記載内容", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }

                        // 納品書の記載が不要のときは入力を取り消す
                        if (this.PackingSlipInputCode.Text == "04" && !string.IsNullOrEmpty(varValue?.ToString()))
                        {
                            this.PackingSlipNote.Text = "";
                            return true;
                        }
                        break;

                    case "InvoiceNote":
                        // 送り状の記載が必要のときは入力必須
                        if (this.InvoiceInputCode.Text == "01" && string.IsNullOrEmpty(varValue?.ToString()))
                        {
                            MessageBox.Show("送り状記載内容を入力してください。", "送り状記載内容", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }

                        // 送り状の記載が不要のときは入力を取り消す
                        if (this.InvoiceInputCode.Text == "02" && !string.IsNullOrEmpty(varValue?.ToString()))
                        {
                            this.InvoiceNote.Text = "";
                            return true;
                        }
                        break;
                    case "InvoiceFaxToName":
                    case "InvoiceFaxToContact":
                    case "InvoiceFaxToNumber":
                        // 送り状FAX送付が必要のときは入力必須
                        if (this.InvoiceFaxCode.Text == "01" && string.IsNullOrEmpty(varValue?.ToString()))
                        {
                            MessageBox.Show("送り状FAXの宛先情報を入力してください。", "送り状FAX", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }

                        // 送り状FAX送付が不要のときは入力を取り消す
                        if (this.InvoiceFaxCode.Text == "02" && !string.IsNullOrEmpty(varValue?.ToString()))
                        {
                            ((TextBox)controlObject).Text = "";
                            return true;
                        }
                        break;
                    case "請求予定日":
                        if (this.帳端処理.Checked)
                        {
                            if (!DateTime.TryParse(varValue.ToString(), out inputDate))
                            {
                                MessageBox.Show("日付を入力してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                goto Exit_IsError;
                            }
                            ((TextBox)controlObject).Text = inputDate.ToString("yyyy/MM/dd");
                            if (!string.IsNullOrEmpty(varValue?.ToString()))
                            {
                                // 受注納期以前の日付を許可するが、完了承認しないと請求には反映しない
                                if (!string.IsNullOrEmpty(this.受注納期.Text) && DateTime.TryParse(this.受注納期.Text, out date1))
                                {
                                    if (inputDate < date1)
                                    {
                                        if (MessageBox.Show("請求予定日に受注納期以前の日付が入力されています。" + Environment.NewLine +
                                                        "請求は受注処理が完了承認されてから行われます。" + Environment.NewLine + Environment.NewLine +
                                                        "続行しますか？", controlObject.Name, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                        {
                                            goto Exit_IsError;
                                        }
                                    }
                                }
                            }
                        }
                        break;
                    case "TaxCalcCode":
                    case "税端数処理":
                        if (string.IsNullOrEmpty(varValue?.ToString()))
                        {
                            MessageBox.Show(controlObject.Name + " を入力してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        break;
                    case "発送先名":
                        if (!IsLimit(varValue, 50, false, controlObject.Name))
                            goto Exit_IsError;
                        break;
                    case "発送先郵便番号":
                        if (string.IsNullOrEmpty(varValue?.ToString()))
                        {
                            MessageBox.Show(controlObject.Name + " を入力してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        break;
                    case "発送先住所1":
                        if (!IsLimit(varValue, 100, false, controlObject.Name))
                            goto Exit_IsError;
                        break;
                    case "発送先住所2":
                        if (!IsLimit(varValue, 100, true, controlObject.Name))
                            goto Exit_IsError;
                        break;
                    case "発送先TEL":
                        if (!IsLimit(varValue, 20, false, controlObject.Name))
                            goto Exit_IsError;
                        break;
                    case "発送先FAX":
                        if (!IsLimit(varValue, 20, true, controlObject.Name))
                            goto Exit_IsError;
                        break;
                    case "発送先メールアドレス":
                        if (!IsLimit(varValue, 50, true, controlObject.Name))
                            goto Exit_IsError;
                        break;
                    case "発送先担当者名":
                        if (!IsLimit(varValue, 40, false, controlObject.Name))
                            goto Exit_IsError;
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

        /// <summary>
        /// 顧客コードから顧客名を得る（受注用）
        /// </summary>
        /// <param name="connection"></param>
        /// <param name="customerCode"></param>
        /// <returns></returns>
        public static string GetCustomerName(SqlConnection connection, string customerCode)
        {
            // 顧客名を格納する変数
            string customerName = "";

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;

                    // SQLクエリを構築
                    string query = "SELECT * FROM M顧客 WHERE 顧客コード = @CustomerCode AND 取引開始日 IS NOT NULL AND GETDATE()<=ExpirationDate";
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@CustomerCode", customerCode);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // レコードが存在する場合、顧客名を取得
                            string customerName1 = reader["顧客名"].ToString();
                            string customerName2 = reader["顧客名2"].ToString();

                            if (!string.IsNullOrEmpty(customerName2))
                            {
                                // 顧客名2が存在する場合、顧客名1と結合
                                customerName = customerName1 + " " + customerName2;
                            }
                            else
                            {
                                // 顧客名2が存在しない場合、顧客名1のみを使用
                                customerName = customerName1;
                            }
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                // エラーハンドリングを行うか、エラーログを記録するなどの処理をここに追加できます
                Console.WriteLine("GetCustomerName - " + ex.Message);
            }

            return customerName;
        }

        /// <summary>
        /// 合計欄を更新する
        /// </summary>
        /// <param name="taxRate"></param>
        private void SetTotalAmount(decimal taxRate)
        {
            // 明細部の消費税を更新する
            this.受注明細1.Detail.ColumnFooters[0].Cells["消費税率"].Value = taxRate;
        }

        private void コマンド新規_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                this.DoubleBuffered = true;

                if (this.ActiveControl == this.コマンド新規)
                {
                    this.コマンド新規.Focus();
                }

                // 変更があるときは登録確認を行う
                if (this.IsChanged)
                {
                    var intRes = MessageBox.Show("変更内容を登録しますか？" + Environment.NewLine +
                    "※確定操作は登録後実行してください。", "新規コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    switch (intRes)
                    {
                        case DialogResult.Yes:

                            // 登録処理
                            if (!RegTrans(this.CurrentCode, this.CurrentEdition))
                            {
                                MessageBox.Show("エラーのため登録できませんでした。", "新規コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                goto Bye_コマンド新規_Click;
                            }
                            break;
                        case DialogResult.Cancel:
                            goto Bye_コマンド新規_Click;
                    }
                }

                // 新規モードへ移行
                if (!GoNewMode())
                {
                    goto Err_コマンド新規_Click;
                }
            }
            catch (Exception ex)
            {
                goto Err_コマンド新規_Click;
            }

        Bye_コマンド新規_Click:
            return;

        Err_コマンド新規_Click:
            MessageBox.Show("エラーが発生しました。" + Environment.NewLine +
                "[ " + BASE_CAPTION + " ]を終了します。", "新規コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.Close();
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
                        if (!Recycle(cn, this.CurrentCode))
                        {
                            MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine +
                                "受注コード　：　" + this.CurrentCode, "読込コマンド", MessageBoxButtons.OK);
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
                var intRes = MessageBox.Show("変更内容を登録しますか？" + Environment.NewLine +
                    "※確定操作は登録後実行してください。", "読込コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (intRes)
                {
                    case DialogResult.Yes:
                        // 確定機能が追加されたため、ここでは登録するだけで確定はしない
                        // If IsErrorData("受注コード", "受注版数") Then GoTo Bye_コマンド読込_Click
                        // 登録処理
                        if (!RegTrans(this.CurrentCode, this.CurrentEdition))
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
                            if (!Recycle(cn, this.CurrentCode))
                            {
                                MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine +
                                "受注コード　：　" + this.CurrentCode, "読込コマンド", MessageBoxButtons.OK);
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
                //一時的に複写を禁止する（解除時期未定）
                MessageBox.Show("複写は禁止されています", "複写コマンド", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーが発生しました。" + Environment.NewLine + "複写できません。", "複写コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void コマンド削除_Click(object sender, EventArgs e)
        {
            try
            {
                // 確認→認証→削除→削除後の表示処理
                // ■ もっとスマートにコーディングできないか？
                if (this.IsApproved)
                {
                    // 削除実行の確認
                    if (MessageBox.Show("表示中の受注データを削除します。\n削除後も参照することができます。\n\n削除しますか？", "削除コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }

                    // シリーズ在庫締め処理後、削除するときは在庫が狂うことを警告する
                    if (this.SeriesStockClosed)
                    {
                        if (MessageBox.Show("シリーズ在庫の締め処理が完了しています。\n削除すると関係するシリーズの在庫数が狂います。\n\n続行しますか？", "削除コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        {
                            return;
                        }
                    }

                    // 認証
                    using (F_認証 authForm = new F_認証())
                    {
                        authForm.args = this.承認者コード.Text;
                        authForm.ShowDialog();

                        if (string.IsNullOrEmpty(strCertificateCode))
                        {
                            MessageBox.Show("削除はキャンセルされました。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    // 削除実行
                    Connect();
                    if (InvalidateData(this.CurrentCode, this.CurrentEdition))
                    {
                        MessageBox.Show("削除しました。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (this.CurrentEdition == 1)
                        {
                            // 新規モードへ移行する
                            if (!GoNewMode())
                            {
                                MessageBox.Show("エラーのため新規モードへ移行できません。\n[" + this.Name + "] を終了します。", "削除コマンド", MessageBoxButtons.OK);
                                this.Close();
                            }
                        }
                        else
                        {
                            // 読込モードのまま前版のデータを表示する
                            this.受注版数.SelectedValue = this.CurrentEdition - 1;
                            UpdateEditionList(this.CurrentCode);
                            SetEditionStatus();
                            this.受注コード.Text = this.CurrentCode;
                        }
                    }
                    else
                    {
                        MessageBox.Show("削除できませんでした。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    // ■ 今後、対象コードの受注データ（全ての版）を削除するのか、
                    // 改版を元に戻すのか選択させることとする。
                    if (MessageBox.Show("表示中の受注データを削除します。\n・削除後元に戻すことはできません。\n・表示データが改版データのときは前版データが有効になります。\n\n削除しますか？", "削除コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }

                    // 認証
                    using (F_認証 authForm = new F_認証())
                    {
                        authForm.args = this.自社担当者コード.Text;
                        authForm.ShowDialog();

                        if (string.IsNullOrEmpty(strCertificateCode))
                        {
                            MessageBox.Show("削除はキャンセルされました。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                    }

                    if (DeleteData(cn, this.CurrentCode, this.CurrentEdition))
                    {
                        MessageBox.Show("削除しました。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (this.CurrentEdition == 1)
                        {
                            // 新規モードへ移行する
                            if (!GoNewMode())
                            {
                                MessageBox.Show("エラーのため新規モードへ移行できません。\n[" + this.Name + "] を終了します。", "削除コマンド", MessageBoxButtons.OK);
                                this.Close();
                            }
                        }
                        else
                        {
                            // 読込モードのまま前版のデータを表示する
                            this.受注版数.SelectedValue = this.CurrentEdition - 1;
                            UpdateEditionList(this.CurrentCode);
                            SetEditionStatus();
                            this.受注コード.Text = this.CurrentCode;
                        }
                    }
                    else
                    {
                        MessageBox.Show("削除できませんでした。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("削除できませんでした。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void コマンド顧客_Click(object sender, EventArgs e)
        {
            if (ActiveControl == コマンド顧客)
            {
                GetNextControl(コマンド顧客, false).Focus();
            }

            string param = $" -sv:{ServerInstanceName} -open:customer," + this.顧客コード.Text;
            GetShell(param);
        }

        private void コマンド商品_Click(object sender, EventArgs e)
        {
            if (ActiveControl == コマンド商品)
            {
                GetNextControl(コマンド商品, false).Focus();
            }

            Form form = new F_商品();
            form.Show();
        }

        private void コマンド全在庫_Click(object sender, EventArgs e)
        {
            if (ActiveControl == コマンド全在庫)
            {
                GetNextControl(コマンド全在庫, false).Focus();
            }

            F_シリーズ在庫参照 form = new F_シリーズ在庫参照();
            form.Show();
        }

        private void コマンド在庫_Click(object sender, EventArgs e)
        {
            if (ActiveControl == コマンド在庫)
            {
                GetNextControl(コマンド在庫, false).Focus();
            }

            F_シリーズ危険在庫警告 form = new F_シリーズ危険在庫警告();
            form.args = this.CurrentCode + ',' + this.CurrentEdition;
            form.Show();
        }

        private void コマンド承認_Click(object sender, EventArgs e)
        {
            try
            {
                string varSaved1 = "";    // 承認日時保存用
                string varSaved2 = "";    // 承認者コード保存用
                string varSaved3 = "";

                if (this.ActiveControl == this.コマンド承認)
                {
                    GetNextControl(コマンド承認, false).Focus();
                }

                // シリーズ在庫締め処理後、承認を取り消すときは在庫が狂うことを警告する
                if (this.IsApproved && this.SeriesStockClosed)
                {
                    if (MessageBox.Show("シリーズ在庫の締め処理が完了しています｡ " + Environment.NewLine
                        + "承認を取り消すと関係するシリーズの在庫数が狂います。" + Environment.NewLine + Environment.NewLine
                        + "続行しますか？", "承認コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }

                // 認証する
                using (F_認証 authForm = new F_認証())
                {
                    authForm.args = "007";
                    authForm.ShowDialog();

                    if (string.IsNullOrEmpty(strCertificateCode))
                    {
                        MessageBox.Show("認証に失敗しました。" + Environment.NewLine + "承認はできません。", "承認コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                // 承認情報設定
                varSaved1 = this.承認日時.Text;
                varSaved2 = this.承認者コード.Text;
                varSaved3 = this.承認.Text;
                if (string.IsNullOrEmpty(this.承認者コード.Text) || this.承認者コード.Text == "000")
                {
                    this.承認日時.Text = GetServerDate(cn).ToString("yyyy-MM-dd HH:mm:ss");
                    this.承認者コード.Text = strCertificateCode;
                    this.承認.Text = "■";
                }
                else
                {
                    this.承認日時.Text = null;
                    this.承認者コード.Text = null;
                    this.承認.Text = null;
                }

                // 表示データを登録する
                if (RegTrans(this.CurrentCode, this.CurrentEdition, true))
                {
                    // 版数のソースを更新する
                    UpdateEditionList(this.CurrentCode);

                    // 版状態の表示
                    SetEditionStatus();

                    // インターフェース更新
                    this.改版ボタン.Enabled = this.IsApproved;
                    this.受注承認ボタン.Enabled = !this.IsApproved;
                    this.否認ボタン.Enabled = !this.IsApproved;
                    this.コマンド承認.Enabled = !this.IsApproved;
                    this.コマンド確定.Enabled = !this.IsApproved;
                    受注明細1.Detail.AllowUserToAddRows = !IsApproved;
                    受注明細1.Detail.AllowUserToDeleteRows = !IsApproved;
                    受注明細1.Detail.ReadOnly = IsApproved; //readonlyなのでaccessと真偽が逆になる 

                    // 在庫の警告を表示する
                    if (CheckWarning(this.CurrentCode, this.CurrentEdition))
                    {
                        F_シリーズ危険在庫警告 form = new F_シリーズ危険在庫警告();
                        form.args = this.CurrentCode + "," + this.CurrentEdition;
                        form.Show();
                    }
                }
                else
                {
                    // 登録失敗
                    this.承認日時.Text = varSaved1;
                    this.承認者コード.Text = varSaved2;
                    this.承認.Text = varSaved3;
                    MessageBox.Show("承認処理は取り消されました。", "承認コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_コマンド承認_Click - " + ex.Message);
                MessageBox.Show("エラーが発生しました。" + Environment.NewLine + Environment.NewLine
                    + ex.Message, "承認コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void コマンド確定_Click(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();

            try
            {
                Connect();

                DateTime dtmNow = GetServerDate(cn); // 現在の日時を取得
                string var確定日時 = "";
                string var確定者コード = "";
                bool blnDoApprove = false; // 承認処理実行フラグ
                string varSaved1 = ""; // 承認日時保存用
                string varSaved2 = ""; // 承認者コード保存用
                string varSaved3 = "";

                fn.DoWait("確定しています...");

                if (this.ActiveControl == this.コマンド確定)
                {
                    GetNextControl(コマンド確定, false).Focus();
                }

                // 未確定のときのみエラーチェック
                if (!IsDecided)
                {
                    if (IsErrorData("受注コード", "受注版数")) return;
                }

                // 確定情報を確保する
                var確定日時 = this.確定日時.Text;
                var確定者コード = this.確定者コード.Text;

                // 処理日時を取得する
                dtmNow = DateTime.Now;

                if (IsDecided)
                {
                    this.確定日時.Text = null;
                    this.確定者コード.Text = null;
                    this.確定.Text = null;
                }
                else
                {
                    this.確定日時.Text = dtmNow.ToString("yyyy-MM-dd HH:mm:ss");
                    this.確定者コード.Text = LoginUserCode;
                    this.確定.Text = "■";

                    // 承認者不在を確認し、承認処理実行フラグを設定する
                    if (IsAbsence(cn, "007") >= 0)
                    {
                        blnDoApprove = true;
                    }

                    // 承認処理が必要であれば承認情報を設定する
                    if (blnDoApprove)
                    {
                        varSaved1 = this.承認日時.Text;
                        varSaved2 = this.承認者コード.Text;
                        varSaved3 = this.承認.Text;
                        this.承認日時.Text = dtmNow.ToString("yyyy-MM-dd HH:mm:ss");
                        this.承認者コード.Text = "000";
                        this.承認.Text = "■";
                    }
                }

                if (RegTrans(CurrentCode, CurrentEdition, blnDoApprove))
                {

                    LockData(this, IsDecided || IsInvalid, "受注コード", "受注版数");

                    // 新規モードのときは読込モードへ移行する
                    if (IsNewData || IsApproved)
                    {
                        // 版数のソースを更新する
                        UpdateEditionList(CurrentCode);
                    }

                    // 状態の表示
                    SetEditionStatus();

                    ChangedData(false);
                    // 動作制御
                    this.改版ボタン.Enabled = IsApproved;
                    this.受注承認ボタン.Enabled = IsDecided && !IsApproved;
                    this.否認ボタン.Enabled = IsDecided && !IsApproved;
                    this.受注明細1.Detail.AllowUserToAddRows = !IsDecided;
                    this.受注明細1.Detail.AllowUserToDeleteRows = !IsDecided;
                    this.受注明細1.Detail.ReadOnly = IsDecided; //readonlyなのでaccessと真偽が逆になる 

                    if (IsNewData)
                    {
                        // 新規モードで確定された場合
                        コマンド新規.Enabled = true;
                        コマンド読込.Enabled = false;
                        コマンド複写.Enabled = true;
                        コマンド削除.Enabled = true;
                    }

                    コマンド承認.Enabled = IsDecided && !IsApproved;
                    コマンド確定.Enabled = !IsApproved;

                    // 承認者不在により承認を兼用したときはシリーズ在庫の状況を表示する
                    if (blnDoApprove)
                    {
                        if (CheckWarning(this.CurrentCode, this.CurrentEdition))
                        {
                            F_シリーズ危険在庫警告 form = new F_シリーズ危険在庫警告();
                            form.args = this.CurrentCode + "," + this.CurrentEdition;
                            form.ShowDialog();
                        }
                    }
                }
                else
                {
                    // 登録できなかった場合
                    this.確定日時.Text = var確定日時;
                    this.確定者コード.Text = var確定者コード;

                    // 承認処理が実行されたときは設定値を戻す
                    if (blnDoApprove)
                    {
                        this.承認日時.Text = varSaved1;
                        this.承認者コード.Text = varSaved2;
                        this.承認.Text = varSaved3;
                    }

                    MessageBox.Show("登録できませんでした。", "確定コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_コマンド確定_Click - " + ex.Message);
                MessageBox.Show("エラーが発生したため、確定できませんでした。", "確定コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
            object varSaved1 = null; // 承認日時保存用
            object varSaved2 = null; // 承認者コード保存用

            this.DoubleBuffered = true;

            if (ActiveControl == コマンド登録)
            {
                GetNextControl(コマンド登録, false).Focus();
            }

            FunctionClass fn = new FunctionClass();
            fn.DoWait("登録しています...");

            if (RegTrans(this.CurrentCode, this.CurrentEdition))
            {
                ChangedData(false);

                // 新規モードのときは読込モードへ移行する
                if (IsNewData)
                {
                    //受注コードのソースを更新する
                    //版数のソースを更新する
                    UpdateEditionList(this.CurrentCode);
                    コマンド新規.Enabled = true;
                    コマンド読込.Enabled = false;
                }

                fn.WaitForm.Close();
                MessageBox.Show("登録を完了しました", "登録コマンド", MessageBoxButtons.OK);
            }
            else
            {
                fn.WaitForm.Close();
                MessageBox.Show("登録できませんでした。", "登録コマンド", MessageBoxButtons.OK);
            }

        }

        private void コマンド終了_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void 改版ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ActiveControl == this.改版ボタン)
                {
                    GetNextControl(改版ボタン, false).Focus();
                }


                // シリーズ在庫締め処理後、改版すると在庫が狂うことを警告する
                if (this.SeriesStockClosed)
                {
                    DialogResult seriesStockResult = MessageBox.Show("シリーズ在庫の締め処理が完了しています。\n改版すると関係するシリーズの在庫数が狂います。\n\n続行しますか？", "改版コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (seriesStockResult == DialogResult.No)
                    {
                        return;
                    }
                }

                // 生産計画が入っている受注データはメッセージを出す
                if (this.IsProductionPlanned)
                {
                    DialogResult productionPlannedResult = MessageBox.Show("この受注データは生産計画済みです。\n改版を行う前に製造部へ連絡してください。\n\n続行しますか？", "改版コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (productionPlannedResult == DialogResult.No)
                    {
                        return;
                    }
                }

                // 複写に成功すればインターフェースを更新する
                if (CopyData(this.CurrentCode, this.CurrentEdition + 1))
                {
                    // 変更された
                    ChangedData(true);
                    // ヘッダ部制御
                    LockData(this, false);
                    this.受注日.Focus();
                    this.受注コード.Enabled = false;
                    this.受注版数.Enabled = false;
                    this.改版ボタン.Enabled = false;
                    this.受注承認ボタン.Enabled = false;
                    this.受注完了承認ボタン.Enabled = false;
                    this.コマンド新規.Enabled = false;
                    this.コマンド読込.Enabled = true;
                    this.コマンド複写.Enabled = false;
                    this.コマンド削除.Enabled = false;
                    this.コマンド承認.Enabled = false;
                    this.コマンド確定.Enabled = true;
                    this.コマンド登録.Enabled = true;

                    // 過去データとの互換性を保つための処理
                    if (string.IsNullOrEmpty(this.税端数処理.Text))
                    {
                        SetFractionCode(this.顧客コード.Text);
                        // 合計欄を更新する
                        SetTotalAmount(string.IsNullOrEmpty(this.TaxRate.Text) ? 0m : decimal.Parse(this.TaxRate.Text));
                    }

                    // 明細部制御
                    受注明細1.Detail.AllowUserToAddRows = true;
                    受注明細1.Detail.AllowUserToDeleteRows = true;
                    受注明細1.Detail.ReadOnly = false; //readonlyなのでaccessと真偽が逆になる 
                }
                else
                {
                    MessageBox.Show("エラーが発生しました。", "改版", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_ChangeVersionButton_Click - " + ex.Message);
                MessageBox.Show("エラーが発生しました。", "改版", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }

        private void 受注承認ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                string varSaved1 = ""; // 承認日時保存用
                string varSaved2 = ""; // 承認者コード保存用
                string varSaved3 = "";
                string strApproverCode = ""; // 承認者コード

                if (this.ActiveControl == this.受注承認ボタン)
                {
                    GetNextControl(受注承認ボタン, false).Focus();
                }

                // 処理実行の確認
                /*if (MessageBox.Show("承認しますか？\n承認後、元に戻すことはできません。", "承認コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    goto Bye_受注承認ボタン_Click;*/

                // 認証する
                using (F_認証 authForm = new F_認証())
                {
                    authForm.args = "007";
                    authForm.ShowDialog();

                    if (string.IsNullOrEmpty(strCertificateCode))
                    {
                        MessageBox.Show("認証されませんでした。\n承認はできません。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }

                // 承認情報設定
                varSaved1 = 承認日時.Text;
                varSaved2 = 承認者コード.Text;
                varSaved3 = 承認.Text;

                Connect();

                if (Information.IsDBNull(承認者コード.Text) | 承認者コード.Text == "000")
                {
                    承認日時.Text = GetServerDate(cn).ToString("yyyy-MM-dd HH:mm:ss");
                    承認者コード.Text = strCertificateCode;
                    承認.Text = "■";
                }
                else
                {
                    承認日時.Text = null;
                    承認者コード.Text = null;
                    承認.Text = null;
                }

                // 表示データを登録する
                if (RegTrans(this.CurrentCode, this.CurrentEdition, true))
                {
                    // 版数のソースを更新する
                    UpdateEditionList(this.CurrentCode);

                    // 状態の表示
                    SetEditionStatus();

                    // インターフェース更新
                    改版ボタン.Enabled = IsApproved;
                    受注承認ボタン.Enabled = !IsApproved;
                    否認ボタン.Enabled = !IsApproved;
                    コマンド承認.Enabled = !IsApproved;
                    コマンド確定.Enabled = !IsApproved;
                    受注明細1.Detail.AllowUserToAddRows = !IsApproved;
                    受注明細1.Detail.AllowUserToDeleteRows = !IsApproved;
                    受注明細1.Detail.ReadOnly = IsApproved; //readonlyなのでaccessと真偽が逆になる 

                    // 在庫の警告を表示する
                    if (CheckWarning(CurrentCode, CurrentEdition))
                    {
                        F_シリーズ危険在庫警告 form = new F_シリーズ危険在庫警告();
                        form.args = this.CurrentCode + ',' + this.CurrentEdition;
                        form.Show();
                    }
                }
                else
                {
                    // 登録失敗
                    承認日時.Text = varSaved1;
                    承認者コード.Text = varSaved2;
                    承認.Text = varSaved3;
                    MessageBox.Show("承認処理は取り消されました。", "承認コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"{Name}_受注承認ボタン_Click - {ex.Message}");
            }
        }

        private void 否認ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                string strMsg;
                string strKey;
                string strSQL;

                if (this.ActiveControl == this.否認ボタン)
                {
                    GetNextControl(否認ボタン, false).Focus();
                }

                MessageBox.Show("この機能は定義されていません。", "否認コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;

                //strMsg = "受注コード　：　" + 受注コード.Text + "\n" +
                //         "承認依頼版数　：　" + 受注版数.Text + "\n\n" +
                //         "この内容は破棄されます。\n" +
                //         "否認しますか？";

                //if (MessageBox.Show(strMsg, "否認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                //{
                //    DoCmd.OpenForm("認証", acNormal, null, null, null, null, "007");

                //    while (strCertificateCode == "")
                //    {
                //        // 認証フォームが閉じていれば、認証不成立となる
                //        if (SysCmd(acSysCmdGetObjectState, acForm, "認証") == 0)
                //        {
                //            MessageBox.Show("否認処理はキャンセルされました。", "否認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //            return;
                //        }
                //        DoEvents();
                //    }

                //    // 仮受注データの削除
                //    // 明細の削除はトリガで行われる
                //    strKey = "受注コード = '" + 受注コード.Text + "' AND 受注版数 = " + 受注版数.Text;
                //    strSQL = "DELETE 受注 WHERE " + strKey;

                //    objConnection.BeginTrans();
                //    objConnection.Execute(strSQL);
                //    objConnection.CommitTrans();
                //    // 破棄後、改版処理前のデータを表示
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show("否認できませんでした。\n\n" + ex.Message, "否認", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 受注完了承認ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ActiveControl == this.受注完了承認ボタン)
                {
                    GetNextControl(受注完了承認ボタン, false).Focus();
                }

                // 旧版の確認
                if (IsInvalid)
                {
                    MessageBox.Show("この受注データは旧版です。\n承認はできません。", "承認", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (ApproveCompletion(CurrentCode, CurrentEdition))
                {
                    // コントロール更新
                    完了承認者コード.Text = LoginUserCode;
                    改版ボタン.Enabled = false;
                    完了.Text = "■";
                }
                else
                {
                    完了承認者コード.Text = null;
                    改版ボタン.Enabled = true;
                    完了.Text = null;
                    MessageBox.Show("エラーが発生したため、処理は取り消されました。", "完了承認", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"エラーが発生しました。\n\n{ex.Message}", "完了承認", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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
                            e.Handled = true;
                            System.Windows.Forms.ComboBox activeComboBox = (System.Windows.Forms.ComboBox)activeControl;
                            activeComboBox.DroppedDown = true;
                        }
                    }
                    break;
                case Keys.Return:
                    // 複数行入力可能な項目はEnterでフォーカス移動させない
                    switch (this.ActiveControl.Name)
                    {
                        case "備考":
                        case "改版履歴":
                            return;
                    }
                    SelectNextControl(ActiveControl, true, true, true, true);
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
                    if (コマンド顧客.Enabled)
                    {
                        コマンド顧客.Focus();
                        コマンド顧客_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F6:
                    if (コマンド商品.Enabled)
                    {
                        コマンド商品.Focus();
                        コマンド商品_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F7:
                    if (コマンド全在庫.Enabled)
                    {
                        コマンド全在庫.Focus();
                        コマンド全在庫_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F8:
                    if (コマンド在庫.Enabled)
                    {
                        コマンド在庫.Focus();
                        コマンド在庫_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
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
                        コマンド終了_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
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
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 300, 50 }, new string[] { "Display", "Display2" });
            受注コード.Invalidate();
            受注コード.DroppedDown = true;
        }

        private void 受注コード_TextChanged(object sender, EventArgs e)
        {
            LimitText(((Control)sender), 9);
        }

        private void 受注コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (受注コード.Text.Length >= 9)
            {
                e.Handled = true;
            }

            e.KeyChar = (char)ChangeBig((int)e.KeyChar);
        }

        private void 受注版数_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            UpdatedControl((Control)sender);
        }

        private void 受注版数_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 50, 50 }, new string[] { "Display", "Display2" });
            受注版数.Invalidate();
            受注版数.DroppedDown = true;
        }

        private void 受注日_Validating(object sender, CancelEventArgs e)
        {
            // object originalValue = 受注日.Text;
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true)
            {
                e.Cancel = true;
                受注日.Text = beforjuchu;
            }
        }

        private void 受注日_Validated(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            UpdatedControl((Control)sender);
        }

        private void 受注日_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            ChangedData(true);
        }

        private void 受注日_DoubleClick(object sender, EventArgs e)
        {
            受注日選択ボタン_Click(this, EventArgs.Empty);
        }

        private void 受注日_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    受注日選択ボタン_Click(this, EventArgs.Empty);
                    break;
            }
        }

        private void 受注日選択ボタン_Click(object sender, EventArgs e)
        {
            if (this.IsApproved)
            {
                this.受注日.Focus();
            }
            else
            {
                beforjuchu = 受注日.Text;
                // 日付選択フォームを作成し表示
                F_カレンダー calendar = new F_カレンダー();
                if (!string.IsNullOrEmpty(受注日.Text))
                {
                    calendar.args = 受注日.Text;
                }
                if (calendar.ShowDialog() == DialogResult.OK && !受注日.ReadOnly && 受注日.Enabled)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = calendar.SelectedDate;

                    // 日付コントロールに選択した日付を設定
                    受注日.Text = selectedDate;
                    受注日.Modified = true;
                    受注日.Focus();

                }
            }
        }

        private void 注文番号_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) e.Cancel = true;
        }

        private void 注文番号_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            LimitText(((Control)sender), 30);
            ChangedData(true);
        }

        private void 注文番号_Validated(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            UpdatedControl((Control)sender);
        }

        private void 顧客コード_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true)
            {
                e.Cancel = true;
                顧客コード.Text = beforkokyaku;
            }
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

        private void 顧客コード_DoubleClick(object sender, EventArgs e)
        {
            顧客コード検索ボタン_Click(this, EventArgs.Empty);
        }

        private void 顧客コード_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    string strCode = this.ActiveControl.Text;
                    if (string.IsNullOrEmpty(strCode))
                        return;
                    strCode = strCode.PadLeft(8, '0');
                    this.顧客コード.Text = strCode;

                    if (IsError(this.顧客コード) == true)
                    {
                        顧客コード.Undo();
                    }
                    break;
            }
        }

        private void 顧客コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                顧客コード検索ボタン_Click(this, EventArgs.Empty);
                e.Handled = true;
            }
        }

        private void 顧客コード検索ボタン_Click(object sender, EventArgs e)
        {
            if (this.IsApproved)
            {
                MessageBox.Show("承認後の修正はできません。", this.ActiveControl.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                beforkokyaku = 顧客コード.Text;
                F_検索 SearchForm = new F_検索();
                SearchForm.FilterName = "顧客名フリガナ";
                if (SearchForm.ShowDialog() == DialogResult.OK && !顧客コード.ReadOnly && 顧客コード.Enabled)
                {
                    string SelectedCode = SearchForm.SelectedCode;

                    顧客コード.Text = SelectedCode;

                    if (IsError(顧客コード) == true)
                    {
                        // エラー時は値を元に戻す
                        顧客コード.Undo();

                        //F_検索から入力した時はundoできないので
                        顧客コード.Text = beforkokyaku;
                    }
                    else
                    {
                        UpdatedControl(顧客コード);
                    }
                }
            }
        }

        private void 顧客担当者名_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            LimitText(((Control)sender), 50);
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

        private void ClientCode_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            ChangedData(true);
        }

        private void ClientCode_Validating(object sender, CancelEventArgs e)
        {
            if (IsError((Control)sender) == true) e.Cancel = true;
        }

        private void ClientCode_Validated(object sender, EventArgs e)
        {
            UpdatedControl((Control)sender);
        }

        private void 受注納期_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true)
            {
                e.Cancel = true;
                受注納期.Text = befornouhin;
            }
        }

        private void 受注納期_Validated(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            UpdatedControl((Control)sender);
        }

        private void 受注納期_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            ChangedData(true);
        }

        private void 受注納期_DoubleClick(object sender, EventArgs e)
        {
            if (this.IsApproved)
            {
                MessageBox.Show("承認後の修正はできません。", this.ActiveControl.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                受注納期選択ボタン_Click(this, EventArgs.Empty);
            }
        }

        private void 受注納期_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    受注納期選択ボタン_Click(this, EventArgs.Empty);
                    break;
            }
        }

        private void 受注納期選択ボタン_Click(object sender, EventArgs e)
        {
            if (this.IsApproved)
            {
                MessageBox.Show("承認後の修正はできません。", this.ActiveControl.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                befornouhin = 受注納期.Text;
                // 日付選択フォームを作成し表示
                F_カレンダー calendar = new F_カレンダー();
                if (!string.IsNullOrEmpty(受注納期.Text))
                {
                    calendar.args = 受注納期.Text;
                }

                if (calendar.ShowDialog() == DialogResult.OK && !受注納期.ReadOnly && 受注納期.Enabled)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = calendar.SelectedDate;

                    // 日付コントロールに選択した日付を設定
                    受注納期.Text = selectedDate;
                    受注納期.Modified = true;
                    受注納期.Focus();
                }
            }
        }

        private void 出荷予定日_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true)
            {
                e.Cancel = true;
                出荷予定日.Text = beforshuka;

            }
        }
        private void 出荷予定日_Validated(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            UpdatedControl((Control)sender);
        }

        private void 出荷予定日_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            ChangedData(true);
        }

        private void 出荷予定日_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    出荷予定日選択ボタン_Click(this, EventArgs.Empty);
                    break;
            }
        }

        private void 出荷予定日_DoubleClick(object sender, EventArgs e)
        {
            出荷予定日選択ボタン_Click(this, EventArgs.Empty);
        }

        private void 出荷予定日選択ボタン_Click(object sender, EventArgs e)
        {
            if (this.IsApproved)
            {
                MessageBox.Show("承認後の修正はできません。", "出荷予定日", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                beforshuka = 出荷予定日.Text;
                // 日付選択フォームを作成し表示
                F_カレンダー calendar = new F_カレンダー();
                if (calendar.ShowDialog() == DialogResult.OK && !出荷予定日.ReadOnly && 出荷予定日.Enabled)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = calendar.SelectedDate;

                    // 日付コントロールに選択した日付を設定
                    出荷予定日.Text = selectedDate;
                    出荷予定日.Modified = true;
                    出荷予定日.Focus();
                }
            }
        }

        private void 納品書送付コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 50, 130 }, new string[] { "Display", "Display2" });
            納品書送付コード.Invalidate();
            納品書送付コード.DroppedDown = true;
        }

        private void 納品書送付コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            納品書送付.Text = ((DataRowView)納品書送付コード.SelectedItem)?.Row.Field<String>("Display2")?.ToString();
            ChangedData(true);
        }

        private void 納品書送付コード_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            ChangedData(true);
        }

        private void 納品書送付コード_Validating(object sender, CancelEventArgs e)
        {
            if (IsError((Control)sender) == true) e.Cancel = true;
        }

        private void 請求書送付コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 50, 130 }, new string[] { "Display", "Display2" });
            請求書送付コード.Invalidate();
            請求書送付コード.DroppedDown = true;
        }

        private void 請求書送付コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            請求書送付.Text = ((DataRowView)請求書送付コード.SelectedItem)?.Row.Field<String>("Display2")?.ToString();
            ChangedData(true);
        }

        private void 請求書送付コード_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            ChangedData(true);
        }

        private void 請求書送付コード_Validating(object sender, CancelEventArgs e)
        {
            if (IsError((Control)sender) == true) e.Cancel = true;
        }

        private void 発送方法コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 50, 130 }, new string[] { "Display", "Display2" });
            発送方法コード.Invalidate();
            発送方法コード.DroppedDown = true;
        }

        private void 発送方法コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            発送方法.Text = ((DataRowView)発送方法コード.SelectedItem)?.Row.Field<String>("Display2")?.ToString();
            ChangedData(true);
        }

        private void 発送方法コード_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            ChangedData(true);
        }

        private void 発送方法コード_Validating(object sender, CancelEventArgs e)
        {
            if (IsError((Control)sender) == true) e.Cancel = true;
        }

        private void 自社担当者コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 50, 150 }, new string[] { "Display", "Display2" });
            自社担当者コード.Invalidate();
            自社担当者コード.DroppedDown = true;
        }

        private void 自社担当者コード_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            LimitText(((Control)sender), 3);
            ChangedData(true);
        }

        private void 自社担当者コード_Validating(object sender, CancelEventArgs e)
        {
            if (IsError((Control)sender) == true) e.Cancel = true;
        }

        private void 自社担当者コード_Validated(object sender, EventArgs e)
        {
            UpdatedControl((Control)sender);
        }

        private void 備考_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) e.Cancel = true;
        }

        private void 備考_Validated(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            UpdatedControl((Control)sender);
        }

        private void 備考_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 改訂履歴_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) e.Cancel = true;
        }

        private void 改訂履歴_Validated(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            UpdatedControl((Control)sender);
        }

        private void 改訂履歴_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void ProductionNotice_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) e.Cancel = true;
        }

        private void ProductionNotice_Validated(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            UpdatedControl((Control)sender);
        }

        private void ProductionNotice_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 発送先登録ボタン_Click(object sender, EventArgs e)
        {
            // Accessの「GoToPage 2」の代わり
            this.Page1.Hide();
            this.Page2.Show();
        }

        private void 発送先1_CheckedChanged(object sender, EventArgs e)
        {
            ChangedData(true);
            SetAddress(this.顧客コード.Text, GetShipNumber());
            this.発送先名.Focus();
        }

        private void 発送先2_CheckedChanged(object sender, EventArgs e)
        {
            ChangedData(true);
            SetAddress(this.顧客コード.Text, GetShipNumber());
            this.発送先名.Focus();
        }

        private void 発送先3_CheckedChanged(object sender, EventArgs e)
        {
            ChangedData(true);
            SetAddress(this.顧客コード.Text, GetShipNumber());
            this.発送先名.Focus();
        }

        private void 発送先名_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) e.Cancel = true;
        }

        private void 発送先名_Validated(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            UpdatedControl((Control)sender);
        }

        private void 発送先名_TextChanged(object sender, EventArgs e)
        {
            LimitText(((Control)sender), 58);
            ChangedData(true);
        }

        private void 発送先郵便番号_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) e.Cancel = true;
        }

        private void 発送先郵便番号_Validated(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            UpdatedControl((Control)sender);
        }

        private void 発送先郵便番号_TextChanged(object sender, EventArgs e)
        {
            LimitText(((Control)sender), 11);
            ChangedData(true);
        }

        private void 発送先住所1_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) e.Cancel = true;
        }

        private void 発送先住所1_Validated(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            UpdatedControl((Control)sender);
        }

        private void 発送先住所1_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 発送先住所2_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) e.Cancel = true;
        }

        private void 発送先住所2_Validated(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            UpdatedControl((Control)sender);
        }

        private void 発送先住所2_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 発送先TEL_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) e.Cancel = true;
        }

        private void 発送先TEL_Validated(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            UpdatedControl((Control)sender);
        }

        private void 発送先TEL_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 発送先FAX_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) e.Cancel = true;
        }

        private void 発送先FAX_Validated(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            UpdatedControl((Control)sender);
        }

        private void 発送先FAX_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 発送先担当者名_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) e.Cancel = true;
        }

        private void 発送先担当者名_Validated(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            UpdatedControl((Control)sender);
        }

        private void 発送先担当者名_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 発送先メールアドレス_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) e.Cancel = true;
        }

        private void 発送先メールアドレス_Validated(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            UpdatedControl((Control)sender);
        }

        private void 発送先メールアドレス_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void Email作成ボタン_Click(object sender, EventArgs e)
        {
            string toEmail = Convert.ToString(発送先メールアドレス.Text);

            if (string.IsNullOrEmpty(toEmail))
            {
                MessageBox.Show("メールアドレスを入力してください。", "メールコマンド", MessageBoxButtons.OK);
                発送先メールアドレス.Focus();
                return;
            }

            // デフォルトのメールクライアントを起動して新しいメールを作成
            try
            {
                string mailtoLink = "mailto:" + toEmail;
                System.Diagnostics.Process.Start(new ProcessStartInfo(mailtoLink) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                MessageBox.Show("メールを起動できませんでした。\nエラー: " + ex.Message, "メールコマンド", MessageBoxButtons.OK);
            }
        }

        private void PaymentConfirmation_CheckedChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void PackingSlipInputCode_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 50, 160 }, new string[] { "Display", "Display2" });
            PackingSlipInputCode.Invalidate();
            PackingSlipInputCode.DroppedDown = true;
        }

        private void PackingSlipInputCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            PackingSlipInput.Text = ((DataRowView)PackingSlipInputCode.SelectedItem)?.Row.Field<String>("Display2")?.ToString();
        }

        private void PackingSlipInputCode_Validating(object sender, CancelEventArgs e)
        {
            if (IsError((Control)sender) == true) e.Cancel = true;
        }

        private void PackingSlipInputCode_Validated(object sender, EventArgs e)
        {
            UpdatedControl((Control)sender);
        }

        private void PackingSlipInputCode_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            ChangedData(true);
        }

        private void InvoiceInputCode_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 50, 130 }, new string[] { "Display", "Display2" });
            InvoiceInputCode.Invalidate();
            InvoiceInputCode.DroppedDown = true;
        }

        private void InvoiceInputCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            InvoiceInput.Text = ((DataRowView)InvoiceInputCode.SelectedItem)?.Row.Field<String>("Display2")?.ToString();
        }

        private void InvoiceInputCode_Validating(object sender, CancelEventArgs e)
        {
            if (IsError((Control)sender) == true) e.Cancel = true;
        }

        private void InvoiceInputCode_Validated(object sender, EventArgs e)
        {
            UpdatedControl((Control)sender);
        }

        private void InvoiceInputCode_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            ChangedData(true);
        }

        private void ReceiptCommentCode_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 50, 130 }, new string[] { "Display", "Display2" });
            ReceiptCommentCode.Invalidate();
            ReceiptCommentCode.DroppedDown = true;
        }

        private void ReceiptCommentCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            ReceiptComment.Text = ((DataRowView)ReceiptCommentCode.SelectedItem)?.Row.Field<String>("Display2")?.ToString();
        }

        private void ReceiptCommentCode_Validating(object sender, CancelEventArgs e)
        {
            if (IsError((Control)sender) == true) e.Cancel = true;
        }

        private void ReceiptCommentCode_Validated(object sender, EventArgs e)
        {
            UpdatedControl((Control)sender);
        }

        private void ReceiptCommentCode_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            ChangedData(true);
        }

        private void InvoiceFaxCode_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 50, 130 }, new string[] { "Display", "Display2" });
            InvoiceFaxCode.Invalidate();
            InvoiceFaxCode.DroppedDown = true;
        }

        private void InvoiceFaxCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            InvoiceFax.Text = ((DataRowView)InvoiceFaxCode.SelectedItem)?.Row.Field<String>("Display2")?.ToString();
        }

        private void InvoiceFaxCode_Validating(object sender, CancelEventArgs e)
        {
            if (IsError((Control)sender) == true) e.Cancel = true;
        }

        private void InvoiceFaxCode_Validated(object sender, EventArgs e)
        {
            UpdatedControl((Control)sender);
        }

        private void InvoiceFaxCode_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            ChangedData(true);
        }

        private void InvoiceFaxToName_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) e.Cancel = true;
        }

        private void InvoiceFaxToName_Validated(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            UpdatedControl((Control)sender);
        }

        private void InvoiceFaxToName_TextChanged(object sender, EventArgs e)
        {
            LimitText(((Control)sender), 100);
            ChangedData(true);
        }

        private void InvoiceFaxToContact_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) e.Cancel = true;
        }

        private void InvoiceFaxToContact_Validated(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            UpdatedControl((Control)sender);
        }

        private void InvoiceFaxToContact_TextChanged(object sender, EventArgs e)
        {
            LimitText(((Control)sender), 44);
            ChangedData(true);
        }

        private void InvoiceFaxToNumber_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) e.Cancel = true;
        }

        private void InvoiceFaxToNumber_Validated(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            UpdatedControl((Control)sender);
        }

        private void InvoiceFaxToNumber_TextChanged(object sender, EventArgs e)
        {
            LimitText(((Control)sender), 20);
            ChangedData(true);
        }

        private void PackingSlipNote_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) e.Cancel = true;
        }

        private void PackingSlipNote_Validated(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            UpdatedControl((Control)sender);
        }

        private void PackingSlipNote_TextChanged(object sender, EventArgs e)
        {
            LimitText(((Control)sender), 120);
            ChangedData(true);
        }

        private void InvoiceNote_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) e.Cancel = true;
        }

        private void InvoiceNote_Validated(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            UpdatedControl((Control)sender);
        }

        private void InvoiceNote_TextChanged(object sender, EventArgs e)
        {
            LimitText(((Control)sender), 92);
            ChangedData(true);
        }

        private void 端数処理_CheckedChanged(object sender, EventArgs e)
        {
            UpdatedControl((Control)sender);
        }

        private void 請求予定日_Validating(object sender, CancelEventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            if (IsError(textBox) == true) e.Cancel = true;
        }

        private void 請求予定日_Validated(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            if (textBox.Modified == false) return;

            UpdatedControl((Control)sender);
        }

        private void 請求予定日_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 請求予定日_DoubleClick(object sender, EventArgs e)
        {
            請求予定日選択ボタン_Click(this, EventArgs.Empty);
        }

        private void 請求予定日_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Space:
                    請求予定日選択ボタン_Click(this, EventArgs.Empty);
                    break;
            }
        }

        private void 請求予定日選択ボタン_Click(object sender, EventArgs e)
        {
            if (this.IsApproved)
            {
                MessageBox.Show("承認後の修正はできません。", "請求予定日", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                // 日付選択フォームを作成し表示
                F_カレンダー calendar = new F_カレンダー();
                if (calendar.ShowDialog() == DialogResult.OK && !請求予定日.ReadOnly && 請求予定日.Enabled)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = calendar.SelectedDate;

                    // 日付コントロールに選択した日付を設定
                    請求予定日.Text = selectedDate;
                    請求予定日.Modified = true;
                    請求予定日.Focus();
                }
            }
        }

        private void TaxCalcCode_Validating(object sender, CancelEventArgs e)
        {
            if (IsError((Control)sender) == true) e.Cancel = true;
        }

        private void TaxCalcCode_Validated(object sender, EventArgs e)
        {
            UpdatedControl((Control)sender);
        }

        private void TaxCalcCode_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            ChangedData(true);
        }

        private void 税端数処理_Validating(object sender, CancelEventArgs e)
        {
            if (IsError((Control)sender) == true) e.Cancel = true;
        }

        private void 税端数処理_Validated(object sender, EventArgs e)
        {
            UpdatedControl((Control)sender);
        }

        private void 税端数処理_TextChanged(object sender, EventArgs e)
        {
            if (setCombo) return;
            ChangedData(true);
        }

        private void 戻るボタン_Click(object sender, EventArgs e)
        {
            // Accessの「GoToPage 1」の代わり
            this.Page1.Show();
            this.Page2.Hide();
        }

        private void 受注コード_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■9文字入力。　■入力された受注コードを検索します。　■既存の受注に対する受注コードの変更はできません。";
        }

        private void 受注日_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■西暦部分は2桁で入力。「/」は必要。　■[+]キー、[-]キーで1日増減。　■[space]キーでカレンダー表示。";
        }

        private void 注文番号_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■英数字は原則として半角で入力。　■複数入力するときは\"/\"で区切ります。　■半角30文字まで入力できます。";
        }

        private void 顧客コード_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■８文字まで入力できます。　■[space]キーで顧客検索画面を表示します。　■発送先及び自社担当者名は自動的に更新されます。";
        }

        private void 顧客担当者名_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■顧客の担当者名を入力。　■全角25文字まで入力できます。　■敬称は不要です。";
        }

        private void 受注納期_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■西暦部分は2桁で入力。「/」は必要。　■[+]キー、[-]キーで1日増減。　■[space]キーでカレンダー表示。";
        }

        private void 出荷予定日_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■西暦部分は2桁で入力。「/」は必要。　■[+]キー、[-]キーで1日増減。　■[space]キーでカレンダー表示。";
        }

        private void 納品書送付コード_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■2文字入力。　■[space]キーで選択。";
        }

        private void 請求書送付コード_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■2文字入力。　■[space]キーで選択。";
        }

        private void 発送方法コード_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■2文字入力。　■[space]キーで選択。";
        }

        private void 自社担当者コード_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■3字入力。　■[space]キーで選択。";
        }

        private void 備考_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■文字数制限はありません。　■この欄への入力内容は生産ライン上では表示されません。";
        }

        private void 改訂履歴_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■文字数制限はありません。　■この欄への入力内容は生産ライン上では表示されません。";
        }

        private void ProductionNotice_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■全角280文字まで入力できます。";
        }

        private void 発送先名_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■全角２９文字まで入力できます。　■敬称は不要。";
        }

        private void 発送先住所1_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■発送先の住所を入力します。建物名は入力しません。";
        }

        private void 発送先住所2_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■発送先の建物名を入力します。";
        }

        private void 発送先担当者名_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■発送先の担当者名を入力。　■敬称は不要。 ■最大28文字まで入力可能。";
        }

        private void 請求予定日_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■帳端処理での請求予定日を入力します。　■[+]キー、[-]キーで1日増減。　■[space]キーでカレンダー表示。";
        }

        private void TaxCalcCode_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■消費税の端数処理方法を選択します。顧客コードを入力すると自動的に選択されます。";
        }

        private void 税端数処理_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■消費税の端数処理方法を選択します。顧客コードを入力すると自動的に選択されます。";
        }

        private void PackingSlipInputCode_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■2文字入力。　■[space]キーで選択。";
        }

        private void InvoiceInputCode_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■2文字入力。　■[space]キーで選択。";
        }

        private void ReceiptCommentCode_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■2文字入力。　■[space]キーで選択。";
        }

        private void InvoiceFaxCode_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■2文字入力。　■[space]キーで選択。";
        }

        private void InvoiceFaxToName_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■送り状送付の確認をFAXで送る場合の宛先名を入力。　■全角41文字まで入力できます。　■敬称は不要です。";
        }

        private void InvoiceFaxToContact_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■送り状送付の確認をFAXで送る場合の宛先担当者名を入力。　■全角22文字まで入力できます。　■敬称は不要です。";
        }

        private void InvoiceFaxToNumber_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■送り状送付の確認をFAXで送る場合のFAX番号を入力。　■半角20文字まで入力できます。　■敬称は不要です。";
        }

        private void PackingSlipNote_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■納品書に記載する内容を入力。　■全角60文字まで入力できます。　■敬称は不要です。";
        }

        private void InvoiceNote_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■顧客の担当者名を入力。　■全角46文字まで入力できます。　■敬称は不要です。";
        }

        private void 請求コード_Validated(object sender, EventArgs e)
        {
            請求コード.Text = 請求コード.Text.PadLeft(8, '0');
        }


    }
}


