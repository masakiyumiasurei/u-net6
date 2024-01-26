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
using System.Reflection.Emit;
using System.Data.Common;
using System.Net.Sockets;
using Microsoft.VisualBasic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.IO;
using System.Drawing.Text;
using System.ComponentModel;
using MultiRowDesigner;


namespace u_net
{
    public partial class F_文書 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "文書";
        private int selected_frame = 0;

        const string TRANSMIT = "-1";

        public string strFlow;
        private string strLockPC;
        private bool blnEmergency;
        private bool blnEditOn;
        private int intKeyCode;
        private string CustomFormName;
        private int page = 1;
        private string strDocName;

        private bool setflg = false;
        public F_文書()
        {
            this.Text = "文書";       // ウィンドウタイトルを設定
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
                return Nz(文書コード.Text);
            }
        }

        public int CurrentEdition
        {
            get
            {
                return string.IsNullOrEmpty(版数.Text) ? 0 : Int32.Parse(版数.Text);
            }
        }

        public bool EditOn
        {
            get
            {
                return blnEditOn;
            }

            set
            {
                blnEditOn = value;
            }

        }


        public bool IsAnswered
        {
            get
            {
                return !IsNull(結果内容.Text);
            }
        }

        public bool IsApproved
        {
            get
            {
                return !IsNull(承認者コード.Text);
            }
        }

        public bool IsChanged
        {
            get
            {
                return コマンド登録.Enabled;
            }
        }

        public bool IsNewData
        {
            get
            {
                return !コマンド新規.Enabled;
            }
        }



        public bool IsDecided
        {
            get
            {
                return !IsNull(確定日時.Text);
            }
        }

        public bool IsDeleted
        {
            get
            {
                return !IsNull(無効日時.Text);
            }
        }

        public bool IsFinished
        {
            get
            {
                return !IsNull(完了承認者コード.Text);
            }
        }

        public bool IsReplied
        {
            get
            {
                return !(IsNull(本文1.Text) &&
                         IsNull(本文2.Text) &&
                         IsNull(本文3.Text) &&
                         IsNull(本文4.Text) &&
                         IsNull(本文5.Text) &&
                         IsNull(本文6.Text));
            }
        }

        public static bool IsLoadedLink()
        {
            // F_リンクフォームのインスタンスを取得
            Form fLinkForm = Application.OpenForms["F_リンク"];

            // F_リンクフォームが存在し、表示されているかどうかを判定
            return fLinkForm != null;
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












        private void Form_Load(object sender, EventArgs e)
        {
            foreach (Control control in Controls)
            {
                control.PreviewKeyDown += OriginalClass.ValidateCheck;
            }



            //実行中フォーム起動
            string LoginUserCode = CommonConstants.LoginUserCode;//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(LoginUserCode, this);

            MyApi myapi = new MyApi();
            int xSize, ySize, intpixel, twipperdot;

            //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
            intpixel = myapi.GetLogPixel();
            twipperdot = myapi.GetTwipPerDot(intpixel);


            OriginalClass ofn = new OriginalClass();
            ofn.SetComboBox(文書コード, "SELECT 文書コード as Value, 文書コード as Display , MAX(版数) AS Display2 FROM T処理文書 WHERE (無効日時 IS NULL) GROUP BY 文書コード ORDER BY 文書コード DESC");
            文書コード.DrawMode = DrawMode.OwnerDrawFixed;

            ofn.SetComboBox(文書名, "SELECT 文書名 as Value, 文書名 as Display, フォーム名 as Display2 FROM M文書 ORDER BY 表示順序");
            文書名.DrawMode = DrawMode.OwnerDrawFixed;

            ofn.SetComboBox(分類コード, "SELECT 文書分類コード as Value, 分類名 as Display FROM M文書分類");

            ofn.SetComboBox(文書フローコード, "SELECT 文書フローコード as Value, 文書フロー名 as Display FROM M文書フロー");

            ofn.SetComboBox(発信者コード, "SELECT [社員コード] as Value, 社員コード as Display, 氏名 as Display2 FROM M社員 WHERE (退社 IS NULL) AND (削除日時 IS NULL) AND ([パート] = 0) ORDER BY [ふりがな]");
            発信者コード.DrawMode = DrawMode.OwnerDrawFixed;

            ofn.SetComboBox(担当者コード6, "SELECT [社員コード] as Value, 社員コード as Display, 氏名 as Display2 FROM M社員 WHERE ([社員コード] = N'002') ORDER BY ふりがな");
            担当者コード6.DrawMode = DrawMode.OwnerDrawFixed;

            ofn.SetComboBox(担当者コード1, "SELECT [社員コード] as Value, 社員コード as Display, 氏名 as Display2 FROM M社員 WHERE (退社 IS NULL) AND (部 = N'営業部') AND (削除日時 IS NULL) ORDER BY ふりがな");
            担当者コード1.DrawMode = DrawMode.OwnerDrawFixed;

            ofn.SetComboBox(担当者コード2, "SELECT [社員コード] as Value, 社員コード as Display, 氏名 as Display2 FROM M社員 WHERE (部 = N'技術部') AND (退社 IS NULL) AND (削除日時 IS NULL) ORDER BY ふりがな");
            担当者コード2.DrawMode = DrawMode.OwnerDrawFixed;

            ofn.SetComboBox(担当者コード3, "SELECT [社員コード] as Value, 社員コード as Display, 氏名 as Display2 FROM M社員 WHERE (退社 IS NULL) AND (部 = N'製造部') AND (削除日時 IS NULL) ORDER BY ふりがな");
            担当者コード3.DrawMode = DrawMode.OwnerDrawFixed;

            ofn.SetComboBox(担当者コード5, "SELECT [社員コード] as Value, 社員コード as Display, 氏名 as Display2 FROM M社員 WHERE (退社 IS NULL) AND (部 = N'管理部') AND (削除日時 IS NULL) ORDER BY ふりがな");
            担当者コード5.DrawMode = DrawMode.OwnerDrawFixed;

            ofn.SetComboBox(担当者コード4, "SELECT [社員コード] as Value, 社員コード as Display, 氏名 as Display2 FROM M社員 WHERE (退社 IS NULL) AND (部 = N'会長') AND (削除日時 IS NULL) ORDER BY ふりがな");
            担当者コード4.DrawMode = DrawMode.OwnerDrawFixed;

            ofn.SetComboBox(承認者コード, "SELECT [社員コード] as Value, 氏名 as Display FROM M社員 ORDER BY [ふりがな]");

            ofn.SetComboBox(完了承認者コード, "SELECT [社員コード] as Value, 氏名 as Display FROM M社員 ORDER BY [ふりがな]");

            ofn.SetComboBox(製品企画書_売上区分コード, "SELECT [売上区分コード] as Value, 売上区分名 as Display FROM M売上区分 ORDER BY 番号");

            this.出向依頼書_出向分類.DataSource = new KeyValuePair<String, String>[] {
                new KeyValuePair<String, String>("技術部へ依頼します", "技術部へ依頼します"),
                new KeyValuePair<String, String>("管理部（品質保証チーム）へ依頼します", "管理部（品質保証チーム）へ依頼します"),
                new KeyValuePair<String, String>("営業部へ依頼します", "営業部へ依頼します"),
                 new KeyValuePair<String, String>("製造部へ依頼します", "製造部へ依頼します"),
                new KeyValuePair<String, String>("技術部が出向します", "技術部が出向します"),
                new KeyValuePair<String, String>("管理部（品質保証チーム）が出向します", "管理部（品質保証チーム）が出向します"),
                 new KeyValuePair<String, String>("営業部が出向します", "営業部が出向します"),
                 new KeyValuePair<String, String>("製造部が出向します", "製造部が出向します"),
            };
            this.出向依頼書_出向分類.DisplayMember = "Value";
            this.出向依頼書_出向分類.ValueMember = "Key";


            this.出向依頼書_費用.DataSource = new KeyValuePair<byte, String>[] {
                new KeyValuePair<byte, String>(1, "無償"),
                new KeyValuePair<byte, String>(2, "有償"),
                new KeyValuePair<byte, String>(0, "未定"),


            };
            this.出向依頼書_費用.DisplayMember = "Value";
            this.出向依頼書_費用.ValueMember = "Key";

            this.設備購買申請書_支払方法コード.DataSource = new KeyValuePair<String, String>[] {
                new KeyValuePair<String, String>("01", "振込"),
                new KeyValuePair<String, String>("02", "現金"),
                new KeyValuePair<String, String>("03", "リース"),
                new KeyValuePair<String, String>("04", "その他"),

            };
            this.設備購買申請書_支払方法コード.DisplayMember = "Value";
            this.設備購買申請書_支払方法コード.ValueMember = "Key";


            this.非該当証明発行依頼書_郵送方法コード.DataSource = new KeyValuePair<String, String>[] {
                new KeyValuePair<String, String>("01", "商品同梱"),
                new KeyValuePair<String, String>("02", "郵便"),
                new KeyValuePair<String, String>("03", "郵便速達"),
                new KeyValuePair<String, String>("04", "ヤマト宅配"),
                 new KeyValuePair<String, String>("05", "ヤマトメール"),
                new KeyValuePair<String, String>("06", "ヤマトタイムサービス"),

            };
            this.非該当証明発行依頼書_郵送方法コード.DisplayMember = "Value";
            this.非該当証明発行依頼書_郵送方法コード.ValueMember = "Key";


            this.不具合調査修理依頼書_代替有無.DataSource = new KeyValuePair<String, String>[] {
                new KeyValuePair<String, String>("有り", "有り"),
                new KeyValuePair<String, String>("無し", "無し"),
                

            };
            this.不具合調査修理依頼書_代替有無.DisplayMember = "Value";
            this.不具合調査修理依頼書_代替有無.ValueMember = "Key";

            try
            {
                this.SuspendLayout();

                int intWindowHeight = this.Height;
                int intWindowWidth = this.Width;


                if (string.IsNullOrEmpty(args)) // 新規
                {
                    if (!GoNewMode())
                    {
                        MessageBox.Show("初期化に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }
                }
                else // 読込se
                {
                    if (!GoModifyMode())
                    {
                        MessageBox.Show("初期化に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.Close();
                    }

                    this.版数.Text = Convert.ToInt32(args.Substring(0, args.IndexOf(","))).ToString();
                    this.文書コード.Focus();
                    this.文書コード.Text = args.Substring(0, args.IndexOf(","));
                }
                args = null;




                // 成功時の処理
                return;
            }
            catch (Exception ex)
            {
                MessageBox.Show("初期化に失敗しました。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                blnEmergency = true;
                this.Close();
            }
            finally
            {
                this.ResumeLayout();

            }
        }

        public bool GoNewMode()
        {
            try
            {
                bool success = false;
                string strSQL = "";

                Connect();




                // ヘッダ部の初期化
                VariableSet.SetControls(this);

                承認者コード.SelectedIndex = -1;


                DoInitialize(FunctionClass.採番(cn, CommonConstants.CH_DOCUMENT), 1);

                strFlow = "1";
                VariableSet.FlowControl(this, true, strFlow, "文書コード");
                通信欄.ReadOnly = true;

                //this.文書コード.Text = Right(FunctionClass.採番(cn, "UNI"), 8);
                //this.版数.Text = 1.ToString();

                this.文書名.Focus();
                this.文書コード.Enabled = false;
                this.版数.Enabled = false;


                this.改版ボタン.Enabled = false;
                this.コマンド新規.Enabled = false;
                this.コマンド修正.Enabled = true;
                this.コマンド複写.Enabled = false;
                this.コマンド削除.Enabled = false;
                this.コマンド改版.Enabled = false;
                this.コマンド編集.Enabled = false;
                コマンドリンク.Enabled = false;
                this.コマンド承認.Enabled = false;
                this.コマンド確定.Enabled = false;
                this.コマンド登録.Enabled = false;


                if (Setlock(CurrentCode, CurrentEdition, CommonConstants.MyComputerName))
                {
                    strLockPC = CommonConstants.MyComputerName;
                    EditOn = true;
                    UpdateCaption(IsChanged, EditOn);
                }
                else
                {
                    return success;
                }

                success = true;
                return success;
            }
            catch (Exception ex)
            {
                Console.WriteLine(this.Name + "_GoNewMode - " + ex.Message);
                return false;
            }
        }


        private bool Setlock(string dataCode, int dataEdition, string computerName)
        {
            try
            {

                Connect();

                // これからロックする情報が残っているときは、前回強制終了したということ
                // よって、強制的にロックを解除する
                string deleteLockSQL = $"DELETE FROM S編集ロック WHERE データコード = '{dataCode}' AND データ版数 = {dataEdition} AND コンピュータ名 = '{computerName}'";
                using (SqlCommand deleteLockCommand = new SqlCommand(deleteLockSQL, cn))
                {
                    deleteLockCommand.ExecuteNonQuery();
                }

                // 今回のロック情報を登録する
                string insertLockSQL = $"INSERT INTO S編集ロック VALUES ('{dataCode}', {dataEdition}, '{computerName}', '{CommonConstants.LoginUserCode}', NULL, GETDATE())";
                using (SqlCommand insertLockCommand = new SqlCommand(insertLockSQL, cn))
                {
                    insertLockCommand.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SetLock: {ex.Message}");
                return false;

            }
        }


        private bool SetUnlock(string dataCode, int dataEdition)
        {
            try
            {
                Connect();

                string unlockSQL = $"DELETE FROM S編集ロック WHERE データコード = '{dataCode}' AND データ版数 = {dataEdition}";
                using (SqlCommand unlockCommand = new SqlCommand(unlockSQL, cn))
                {
                    unlockCommand.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in SetUnlock: {ex.Message}");
                return false;
            }

        }

        private void DoInitialize(string codeString, int editionNumber)
        {
            文書コード.Text = codeString;
            版数.Text = editionNumber.ToString();
            登録者コード.Text = CommonConstants.LoginUserCode;
            発信者コード.Text = CommonConstants.LoginUserCode;
            if (string.IsNullOrEmpty(((DataRowView)発信者コード.SelectedItem)?.Row.Field<string>("Display2")))
            {
                発信者名.Text = ((DataRowView)発信者コード.SelectedItem)?.Row.Field<string>("Display2");
            }
            else
            {
                発信者名.Text = null;
            }
            文書フローコード.Text = "02";
            送信先コード1.Text = "002";  // 送信先を固定する
            送信先コード2.Text = CommonConstants.USER_CODE_TECH;
            送信先コード3.Text = "006";
            送信先コード4.Text = "001";
            送信先コード5.Text = "019";
            送信先コード6.Text = "019";

        }


        private bool GoModifyMode()
        {
            try
            {
                bool success = false;
                string strSQL = "";

                // 各コントロール値をクリア
                VariableSet.SetControls(this);





                this.文書コード.Enabled = true;
                版数.Enabled = true;

                FunctionClass.LockData(this, true, "文書コード");

                版数.DataSource = null;
                版数.Text = "1";

                送信先コード1.Text = "002";
                送信先コード2.Text = CommonConstants.USER_CODE_TECH;
                送信先コード3.Text = "006";
                送信先コード4.Text = "001";
                送信先コード5.Text = "019";
                送信先コード6.Text = "007";


                ChangedData(false);

                文書添付.Enabled = false;
                文書コード.Focus();


                this.コマンド新規.Enabled = true;
                this.コマンド修正.Enabled = false;
                コマンド複写.Enabled = false;
                コマンド登録.Enabled = false;


                success = true;
                return success;
            }
            catch (Exception ex)
            {
                Console.WriteLine(this.Name + "_GoModifyMode - " + ex.Message);
                return false;
            }
        }

        private void Form_Unload(object sender, FormClosingEventArgs e)
        {
            string LoginUserCode = CommonConstants.LoginUserCode;//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            LocalSetting test = new LocalSetting();
            test.SavePlace(LoginUserCode, this);

            if (blnEmergency) return;

            try
            {

                Connect();

                // データへの変更がないときの処理
                if (!IsChanged)
                {
                    // 新規モードのときは内部の更新データを元に戻す
                    if (IsNewData && !string.IsNullOrEmpty(CurrentCode))
                    {
                        if (CurrentEdition == 1)
                        {
                            // 初版時のみ採番された番号を戻す
                            if (!FunctionClass.Recycle(cn, CurrentCode))
                            {
                                MessageBox.Show("文書コードは破棄されました。" + Environment.NewLine + Environment.NewLine +
                                                "文書コード　：　" + CurrentCode, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                        //文書添付を戻す
                        if (!DelAttachedDoc(CurrentCode, CurrentEdition))
                        {
                            return;
                        }

                    }
                    return;
                }

                // 修正されているときは登録確認を行う
                var intRes = MessageBox.Show("文書コード : " + CurrentCode + " （第 " + CurrentEdition + " 版）\n\n変更内容を登録しますか？", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                switch (intRes)
                {
                    case DialogResult.Yes:
                        // エラーチェック
                        if (!ErrCheck())
                        {
                            return;
                        }
                        // 登録処理
                        if (!SaveData())
                        {
                            if (MessageBox.Show("エラーのため登録できませんでした。" + Environment.NewLine +
                                                "強制終了しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            {
                                return;
                            }
                        }
                        break;
                    case DialogResult.No:
                        //新規コードを取得していたときはコードを戻す
                        if (IsNewData && !string.IsNullOrEmpty(CurrentCode) && CurrentEdition == 1)
                        {
                            if (!FunctionClass.Recycle(cn, CurrentCode))
                            {
                                MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine +
                                                "ユニットコード　：　" + CurrentCode, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }

                            //文書添付を戻す
                            if (!DelAttachedDoc(CurrentCode, CurrentEdition))
                            {
                                return;
                            }
                        }
                        break;
                    case DialogResult.Cancel:
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
                if (!blnEmergency)
                {
                    //自分が編集中のときは参照モードへ移行しておく
                    if (strLockPC == CommonConstants.MyComputerName)
                    {
                        //参照モードへ移行する
                        if (SetUnlock(CurrentCode, CurrentEdition))
                        {
                            UpdateCaption(IsChanged, false);
                        }
                        else
                        {
                            MessageBox.Show("参照モードへの移行に失敗しました。\n他のユーザーがこのデータを開くと編集できない可能性があります。", "編集コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }

            }
        }



        private bool DelAttachedDoc(string codeString, int editionNumber)
        {
            try
            {
                Connect();


                string deleteAttachedDocSQL = $"DELETE FROM T添付文書 WHERE 文書コード = '{codeString}' AND 版数 = {editionNumber}";
                using (SqlCommand deleteAttachedDocCommand = new SqlCommand(deleteAttachedDocSQL, cn))
                {
                    deleteAttachedDocCommand.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in DelAttachedDoc: {ex.Message}");
                return false;
            }

        }

        private bool ErrCheck()
        {
            //入力確認    
            if (IsError(this.文書コード)) return false;
            if (IsError(this.版数)) return false;

            return true;
        }

        public void ChangedData(bool isChanged)
        {
            if (ActiveControl == null) return;


            UpdateCaption(isChanged, EditOn);

            if (ActiveControl == 文書コード)
            {
                文書名.Focus();
            }

            文書コード.Enabled = !isChanged;

            if (ActiveControl == 版数)
            {
                文書名.Focus();
            }

            版数.Enabled = !isChanged;
            コマンド複写.Enabled = !isChanged;
            コマンド削除.Enabled = !isChanged;
            コマンドリンク.Enabled = !isChanged;



            if (isChanged && !IsApproved)
            {

                コマンド確定.Enabled = true;
            }

            コマンド登録.Enabled = isChanged;
        }

        private bool IsError(Control controlObject)
        {
            try
            {

                FunctionClass fn = new FunctionClass();

                switch (intKeyCode)
                {
                    case (int)Keys.F1:
                    case (int)Keys.F2:
                    case (int)Keys.F3:
                    case (int)Keys.F4:
                    case (int)Keys.F5:
                    case (int)Keys.F6:
                    case (int)Keys.F7:
                    case (int)Keys.F8:
                    case (int)Keys.F9:
                    case (int)Keys.F10:
                    case (int)Keys.F11:
                    case (int)Keys.F12:
                        return false;
                }

                object varValue = controlObject.Text;

                TextBox textBox = controlObject as TextBox;
                if (textBox != null)
                {
                    if (textBox.Modified == false)
                    {
                        return false;
                    }
                }

                Connect();

                switch (controlObject.Name)
                {
                    case "文書コード":
                        if (文書コード.SelectedIndex == -1) return false;
                        if (!CheckDocument(varValue.ToString(),
                            ((DataRowView)文書コード.SelectedItem).Row.Field<Int16>("Display2") > 0 ? this.CurrentEdition : ((DataRowView)文書コード.SelectedItem).Row.Field<Int16>("Display2")))
                        {
                            return true;
                        }
                        break;
                    case "版数":
                        if (IsNull(varValue))
                        {
                            MessageBox.Show(controlObject.Name + "を入力してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;
                    case "文書名":
                    case "件名":
                        if (IsNull(varValue))
                        {
                            MessageBox.Show(controlObject.Name + "を入力してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;
                    case "回答期限":
                        if (IsNull(varValue))
                        {
                            MessageBox.Show("期限日を入力してください。", "期限日", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        if (!DateTime.TryParse(varValue.ToString(), out _))
                        {
                            MessageBox.Show("日付を入力してください。", "期限日", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        if (!this.IsApproved && DateTime.Parse(varValue.ToString()) < DateTime.Today)
                        {
                            MessageBox.Show("過去日付は入力できません。", "期限日", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;
                    case "文書フローコード":
                        if (IsNull(varValue))
                        {
                            MessageBox.Show("文書フローを選択してください。", "文書フロー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;

                    case "発信者コード":
                        if (IsNull(varValue))
                        {
                            MessageBox.Show("発信者名を選択してください。", "発信者", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;

                    case "回答日1":
                    case "回答日2":
                    case "回答日3":
                    case "回答日4":
                    case "回答日5":
                        if (IsNull(varValue)) return false;

                        if (!DateTime.TryParse(varValue.ToString(), out _))
                        {
                            MessageBox.Show("日付を入力してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }

                        if (DateTime.Today < DateTime.Parse(varValue.ToString()))
                        {
                            MessageBox.Show("未来日付は入力できません。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;

                    case "製品企画書_売上区分コード":
                        if (!FunctionClass.IsLimit(varValue, 2, false, controlObject.Name))
                        {
                            return true;
                        }

                        break;
                    case "製品企画書_自社開発費比率":
                        if (!FunctionClass.IsLimit(varValue, 10, false, controlObject.Name))
                        {
                            return true;
                        }

                        break;

                    case "是正予防会議通知書_受付文書コード":
                    case "是正予防処置報告書_受付文書コード":

                        if (!FunctionClass.IsLimit(varValue, 11, false, controlObject.Name))
                        {

                            return true;
                        }

                        break;

                    case "システム配布記録_配布バージョン":
                    case "製品企画書_品名":
                    case "製品企画書_型番":
                    case "製品企画書_競合メーカー":
                    case "製品企画書_競合製品型番":
                        if (!FunctionClass.IsLimit(varValue, 50, false, controlObject.Name))
                        {
                            return true;
                        }

                        break;



                    case "教育訓練実施要領書_受講者名":
                    case "教育訓練実施要領書_訓練名":
                    case "教育訓練実施要領書_実施場所":
                        if (!FunctionClass.IsLimit(varValue, 100, false, controlObject.Name))
                        {
                            return true;
                        }
                        break;

                    case "教育訓練実施要領書_目的":
                    case "製品企画書_開発目的":
                    case "製品企画書_製品概要":
                        if (!FunctionClass.IsLimit(varValue, 1000, false, controlObject.Name))
                        {
                            return true;
                        }
                        break;

                    case "システム配布記録_配布目的":
                    case "製品企画書_要求事項":
                        if (!FunctionClass.IsLimit(varValue, 2000, false, controlObject.Name))
                        {
                            return true;
                        }
                        break;

                    case "教育訓練実施要領書_内容":
                        if (!FunctionClass.IsLimit(varValue, 3000, false, controlObject.Name))
                        {
                            return true;
                        }
                        break;

                    case "教育訓練実施要領書_期待効果":

                        if (IsNull(varValue)) return false;
                        if (!FunctionClass.IsLimit(varValue, 1000, false, controlObject.Name))
                        {
                            return true;
                        }
                        break;

                    case "教育訓練実施要領書_日付1":
                    case "教育訓練実施要領書_日付2":
                        if (!IsDate(varValue))
                        {
                            MessageBox.Show("日付を入力してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }


                        break;

                    case "システム配布記録_配布日":
                    case "出向依頼書_出向日開始":
                    case "出向依頼書_出校日終了":
                        if (!IsDate(varValue))
                        {
                            MessageBox.Show("日付を入力してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }

                        if (DateTime.Now < Convert.ToDateTime(varValue))
                        {
                            MessageBox.Show("未来日付は入力できません。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;

                    case "環境連絡所_発生日":
                    case "記録_日付1":
                    case "記録_日付2":
                    case "議事録_開催日":
                    case "出向依頼書_受付日":
                    case "是正予防処置報告書_環境_受付日":
                    case "品質異常報告書_発生日":
                    case "不具合調査修理依頼書_受付日":
                    case "不具合調査修理依頼書_発生日":

                        if (IsNull(varValue))
                        {
                            MessageBox.Show(controlObject.Name + "を入力してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        if (!IsDate(varValue))
                        {
                            MessageBox.Show("日付を入力してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }

                        if (DateTime.Today < Convert.ToDateTime(varValue))
                        {
                            MessageBox.Show("未来日付は入力できません。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;
                    case "製品企画書_発売予定日":
                    case "製品企画書_会議開催日":
                    case "設備購買申請書_購買予定日":
                    case "非該当証明発行依頼書_発行日":
                    case "非該当証明発行依頼書_郵送日":

                        if (!IsDate(varValue))
                        {
                            MessageBox.Show("日付を入力してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }

                        if (DateTime.Today > Convert.ToDateTime(varValue))
                        {
                            MessageBox.Show("過去日付は入力できません。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;
                    case "環境連絡所_発生場所":
                    case "環境連絡所_型番":
                    case "環境連絡所_異常内容":
                    case "記録_参加者":
                    case "記録_場所":
                    case "記録_目的":
                    case "記録_報告内容":
                    case "議事録_開催場所":
                    case "議事録_参加者":
                    case "議事録_内容":
                    case "出向依頼書_顧客名":
                    case "出向依頼書_出向先会社名":
                    case "出向依頼書_製品型番":
                    case "出向依頼書_依頼内容":
                    case "是正予防会議通知書_不具合現象":
                    case "是正予防処置報告書_議事録":
                    case "是正要望書地報告書_環境_議事録":
                    case "設計審査会議事録_企画書との相違":
                    case "設計審査会議事録_計画書との相違":
                    case "設計審査会議事録_構想資料との相違":
                    case "設計審査会議事録_仕様書の確認":
                    case "設計審査会議事録_改善点":
                    case "設計審査会議事録_要望":
                    case "設計審査会議事録_結論":
                    case "設計製作依頼書_依頼内容":
                    case "年間教育計画表_教育目的":
                    case "品質異常報告書_発生場所":
                    case "品質異常報告書_型番":
                    case "品質異常報告書_異常内容":
                    case "設備購買申請書_型番":
                    case "設備購買申請書_目的":
                    case "非該当証明発行依頼書_顧客名":
                    case "非該当証明発行依頼書_代理店名":
                    case "非該当証明発行依頼書_品名":
                    case "非該当証明発行依頼書_型番":
                    case "非該当証明発行依頼書_輸出先国名":
                    case "非該当証明発行依頼書_郵送方法コード":
                    case "不具合調査修理依頼書_連絡者社名":
                    case "不具合調査修理依頼書_連絡者氏名":
                    case "不具合調査修理依頼書_型番":
                    case "不具合調査修理依頼書_発生場所":
                    case "不具合調査修理依頼書_現象":
                    case "不具合調査修理依頼書_顧客の声":
                    case "不具合調査修理依頼書_代替有無":
                        if (IsNull(varValue))
                        {
                            MessageBox.Show(controlObject.Name + "を入力してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;
                    case "記録_記録分類コード":
                        if (IsNull(varValue))
                        {
                            MessageBox.Show("記録分類コードを指定してください。", "記録分類", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;
                    case "不具合調査修理依頼書_依頼分類コード":
                        if (IsNull(varValue))
                        {
                            MessageBox.Show("依頼分類を選択してください。", "依頼分類", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;
                    case "設計製作依頼書_付属書類指定":
                        if (IsNull(varValue))
                        {
                            MessageBox.Show("付属書類の有無を指定してください。", "入力制限", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;
                    case "出向依頼書_顧客コード":
                        if (!出向依頼書_SetCustomerInfo(varValue.ToString()))
                        {
                            return true;
                        }
                        break;

                    case "出向依頼書_依頼分類その他":
                        if (出向依頼書_その他.Checked)
                        {
                            if (IsNull(varValue))
                            {
                                MessageBox.Show("依頼分類が「その他」のときは分類項目を入力してください。", "依頼分類-その他", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return true;
                            }
                        }
                        break;
                    case "不具合調査修理依頼書_その他":
                        if (不具合調査修理依頼書_その他.Checked)
                        {
                            if (IsNull(varValue))
                            {
                                MessageBox.Show("依頼分類が「その他」のときは分類項目を入力してください。", "依頼分類-その他", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return true;
                            }
                        }
                        break;
                    case "製品企画書_顧客コード":
                        製品企画書_顧客コード.Text = FunctionClass.GetCustomerName(cn, varValue.ToString());
                        break;

                    case "新規販売取引申請書_顧客コード":
                        if (IsNull(varValue))
                        {
                            MessageBox.Show(controlObject.Name + "を入力してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        string str1;
                        str1 = FunctionClass.GetCustomerName(cn, Nz(varValue));
                        if (string.IsNullOrEmpty(str1))
                        {
                            MessageBox.Show("指定された顧客データはありません。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        else
                        {
                            新規販売取引申請書_顧客名.Text = str1;
                        }
                        break;
                    case "不具合調査修理依頼書_顧客コード":
                        if (IsNull(varValue))
                        {
                            MessageBox.Show(controlObject.Name + "を入力してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        string str2;
                        str2 = FunctionClass.GetCustomerName(cn, Nz(varValue));
                        if (string.IsNullOrEmpty(str2))
                        {
                            MessageBox.Show("指定された顧客データはありません。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        else
                        {
                            不具合調査修理依頼書_顧客名.Text = str2;
                        }
                        break;

                    case "是正予防会議通知書_顧客コード":

                        if (!FunctionClass.IsLimit(varValue, 8, false, controlObject.Name))
                        {
                            return true;
                        }
                        if (IsNull(varValue)) return false;
                        是正予防会議通知書_顧客名.Text = FunctionClass.GetCustomerName(cn, varValue.ToString());
                        break;
                    case "是正予防処置報告書_顧客コード":
                        if (!FunctionClass.IsLimit(varValue, 8, false, controlObject.Name))
                        {
                            return true;
                        }
                        if (IsNull(varValue)) return false;
                        是正予防処置報告書_顧客名.Text = FunctionClass.GetCustomerName(cn, varValue.ToString());
                        break;

                    case "是正予防会議通知書_数量":
                    case "是正予防処置報告書_数量":
                    case "是正予防処置報告書_環境_数量":
                    case "製品企画書_年間販売数量":
                    case "設計製作依頼書_数量":
                    case "設備購買申請書_数量":
                    case "非該当証明発行依頼書_発行部数":
                    case "不具合調査修理依頼書_数量":
                        if (IsNull(varValue)) return false;
                        if (!FunctionClass.IsLimit_N(varValue, 7, 0, controlObject.Name)) return true;
                        if (Convert.ToInt32(varValue) <= 0)
                        {
                            MessageBox.Show("整数値を入力してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;
                    case "製品企画書_標準価格":
                    case "設計製作依頼書_標準価格":
              
                        if (!FunctionClass.IsLimit_N(varValue, 9, 2, controlObject.Name)) return true;

                        break;
                    case "製品企画書_競合価格":
       
                        if (IsNull(varValue)) return false;
                        if (!FunctionClass.IsLimit_N(varValue, 9, 2, controlObject.Name)) return true;

                        break;
 
                    case "設備購買申請書_単価":
                        if (!FunctionClass.IsLimit_N(varValue, 9, 0, controlObject.Name)) return true;

                        break;
 
                    case "設備購買申請書_標準価格":
                        if (IsNull(varValue)) return false;
                        if (!FunctionClass.IsLimit_N(varValue, 9, 0, controlObject.Name)) return true;

                        break;
                    case "設計製作依頼書_顧客コード":
                        try
                        {
                            if (string.IsNullOrEmpty(varValue.ToString()))
                            {
                                設計製作依頼書_顧客名.Text = null;
                            }
                            else
                            {
                                using (SqlCommand cmd = new SqlCommand())
                                {
                                    string strKey = "顧客コード='" + varValue + "' AND 取引開始日 IS NOT NULL";
                                    string strSQL = "SELECT * FROM M顧客 WHERE " + strKey;

                                    cmd.CommandText = strSQL;
                                    cmd.Connection = cn;

                                    using (SqlDataReader reader = cmd.ExecuteReader())
                                    {
                                        if (!reader.HasRows)   // 顧客コードなし
                                        {
                                            設計製作依頼書_顧客名.Text = null;
                                            reader.Close();
                                            MessageBox.Show("入力された顧客コードは有効ではありません。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                            return true;
                                        }

                                        reader.Read();
                                        設計製作依頼書_顧客名.Text = reader["顧客名"].ToString();

                                        if (IsNull(設計製作依頼書_客先担当者名.Text))
                                        {
                                            設計製作依頼書_客先担当者名.Text = reader["顧客担当者名"].ToString();
                                        }
                                    }
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"{ex.GetType().Name}: {ex.Message}");
                        }
                        break;

                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // エラーハンドリングが必要に応じて行われるべきです
                return true;
            }
        }

        private bool IsDate(object value)
        {
            if (value == null)
            {
                return false;
            }

            if (DateTime.TryParse(value.ToString(), out _))
            {
                return true;
            }

            return false;
        }
        private bool CheckDocument(string codeString, int editionNumber)
        {
            try
            {
                Connect();

                bool documentExists = false;
                string strKey = $"文書コード = '{codeString}' AND 版数 = {editionNumber}";
                string strSQL = $"SELECT * FROM T処理文書 WHERE {strKey}";

                using (SqlDataAdapter adapter = new SqlDataAdapter(strSQL, cn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        documentExists = true;
                    }
                }

                return documentExists;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in CheckDocument: {ex.Message}");
                return false;
            }
        }

        private void UpdatedControl(Control controlObject)
        {
            FunctionClass fn = new FunctionClass();

            try
            {
                string strSQL;


                Connect();

                switch (controlObject.Name)
                {
                    case "文書コード":
                    case "版数":

                        fn.DoWait("読み込んでいます...");

                        // コードが更新されたときは版数のソースを更新する
                        if (controlObject.Name == "文書コード")
                        {
                            UpdateEditionList(controlObject.Text);
                        }

                        LoadHeader(this, this.CurrentCode, this.CurrentEdition);

                        if (IsNull(fn.Zn(((DataRowView)文書名.SelectedItem)?.Row.Field<string>("Display2"))))
                        {
                            RemoveCustomForm();
                            CustomFormName = null;
                            SetCustomForm();
                        }
                        else
                        {
                            RemoveCustomForm();
                            CustomFormName = ((DataRowView)文書名.SelectedItem)?.Row.Field<string>("Display2");
                            SetCustomForm();

                            LoadHeaderSub(this, this.CurrentCode, this.CurrentEdition);

                            if (BeControl(CustomFormName + "_顧客コード") && BeControl(CustomFormName + "_顧客名"))
                            {
                                if (!GetCustomFormEnabled(CustomFormName + "_顧客名"))
                                {
                                    SetCustomFormValue(CustomFormName + "_顧客名", GetCustomerName(Nz(GetCustomFormValue(CustomFormName + "_顧客コード"))));
                                }
                            }

                        }

                        FunctionClass.LockData(this, true, "文書コード");

                        if (!IsNull(this.文書名.Text))
                        {
                            this.分類コード.Enabled =
                                (this.文書名.Text.ToString() == "検討依頼書") ||
                                (this.文書名.Text.ToString() == "製品企画書") ||
                                (this.文書名.Text.ToString() == "設計製作依頼書");
                        }



                        // 添付文書数を取得する
                        this.添付文書数.Text = GetAttaches(this.CurrentCode, this.CurrentEdition).ToString();
                        this.文書添付.Enabled = true;

                        this.送信先1ボタン.Enabled = false;
                        this.送信先2ボタン.Enabled = false;
                        this.送信先3ボタン.Enabled = false;
                        this.送信先4ボタン.Enabled = false;
                        this.送信先5ボタン.Enabled = false;
                        this.送信先6ボタン.Enabled = false;
                        this.版数.Enabled = true;          // 版数を編集可にする
                        this.コマンド複写.Enabled = true;
                        this.コマンド削除.Enabled = !this.IsAnswered;
                        this.コマンド編集.Enabled = !this.IsFinished;
                        this.コマンドリンク.Enabled = !IsLoadedLink();

                        ChangedData(false);

                        fn.WaitForm.Close();
                        break;

                    case "文書名":
                        this.分類コード.Enabled = (controlObject.Text == "検討依頼書") || (controlObject.Text == "製品企画書") || (controlObject.Text == "設計製作依頼書");


                        if (IsNull(fn.Zn(((DataRowView)文書名.SelectedItem)?.Row.Field<string>("Display2"))))
                        {
                            RemoveCustomForm();
                            CustomFormName = null;
                            SetCustomForm();
                        }
                        else
                        {
                            RemoveCustomForm();
                            CustomFormName = ((DataRowView)文書名.SelectedItem)?.Row.Field<string>("Display2");
                            SetCustomForm();

                            SetCustomFormValue(CustomFormName + "_文書コード", CurrentCode);
                            SetCustomFormValue(CustomFormName + "_版数", CurrentEdition.ToString());
                        }
                        break;

                    case "出向依頼書_顧客名":
                        if (setflg) return;
                        出向依頼書_顧客コード = null;
                        break;


                    case "設計製作依頼書_付属書類指定":
                        設計製作依頼書_付属書類.Enabled = 設計製作依頼書_付属書類指定.Checked;
                        設計製作依頼書_付属文書1.Enabled = 設計製作依頼書_付属書類指定.Checked;
                        設計製作依頼書_付属文書2.Enabled = 設計製作依頼書_付属書類指定.Checked;
                        設計製作依頼書_付属文書3.Enabled = 設計製作依頼書_付属書類指定.Checked;
                        if (設計製作依頼書_付属書類指定.Checked)
                        {
                            設計製作依頼書_その他文書名.Enabled = 設計製作依頼書_付属文書3.Checked;

                        }
                        else
                        {
                            設計製作依頼書_その他文書名.Enabled = false;
                        }
                        break;

                    case "設計製作依頼書_付属文書3":
                        設計製作依頼書_その他文書名.Enabled = 設計製作依頼書_付属文書3.Checked;
                        if (設計製作依頼書_付属文書3.Checked)
                        {
                            設計製作依頼書_その他文書名.Text = strDocName;
                            設計製作依頼書_その他文書名.Focus();
                        }
                        else
                        {
                            設計製作依頼書_その他文書名.Text = null;
                        }
                        break;

                    case "設計製作依頼書_その他文書名":
                        if (!IsNull(controlObject.Text))
                        {
                            strDocName = controlObject.Text;
                        }
                        break;


                    default:
                        break;
                }




            }
            catch (Exception ex)
            {
                // 例外処理
                Debug.Print(this.Name + "_UpdatedControl - " + ex.Message);
                if (fn.WaitForm != null)
                {
                    fn.WaitForm.Close();
                }

            }
        }


        private int GetAttaches(string codeString, int editionNumber)
        {
            try
            {
                int attachesCount = 0;
                string strSQL = $"SELECT COUNT(添付文書コード) AS 添付文書数 FROM T添付文書 " +
                                 $"WHERE 文書コード = '{codeString}' AND 版数 = {editionNumber}";

                Connect();

                using (SqlCommand command = new SqlCommand(strSQL, cn))
                {

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            attachesCount = Convert.ToInt32(reader["添付文書数"]);
                        }
                    }
                }

                return attachesCount;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetAttaches: {ex.Message}");
                return 0;
            }

        }

        private string GetCustomerName(string customerCode)
        {
            try
            {
                string customerName = "";
                if (string.IsNullOrEmpty(customerCode))
                {
                    return customerName;
                }

                string strKey = $"顧客コード = '{customerCode}'";
                string strSQL = $"SELECT * FROM M顧客 WHERE {strKey}";

                Connect();

                using (SqlCommand command = new SqlCommand(strSQL, cn))
                {
                    cn.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            customerName = $"{reader["顧客名"]} {reader["顧客名2"]}";
                        }
                    }
                }

                return customerName.Trim();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in GetCustomerName: {ex.Message}");
                return "";
            }

        }

        private bool BeControl(string controlName)
        {
            foreach (Control control in Controls)
            {
                if (control.Name == controlName)
                {
                    return true;
                }
            }

            return false;
        }

        private void SetCustomForm()
        {


            switch (CustomFormName)
            {
                case "システム配布記録":
                    this.Controls.Add(システム配布記録パネル);
                    システム配布記録パネル.BringToFront();
                    break;
                case "環境連絡書":
                    this.Controls.Add(環境連絡書パネル);
                    環境連絡書パネル.BringToFront();
                    break;
                case "記録":
                    this.Controls.Add(記録パネル);
                    記録パネル.BringToFront();
                    break;
                case "議事録":
                    this.Controls.Add(議事録パネル);
                    議事録パネル.BringToFront();
                    break;
                case "教育訓練実施要領書":
                    this.Controls.Add(教育訓練実施要領書パネル);
                    教育訓練実施要領書パネル.BringToFront();
                    break;
                case "出向依頼書":
                    this.Controls.Add(出向依頼書パネル);
                    出向依頼書パネル.BringToFront();
                    break;
                case "新規販売取引申請書":
                    this.Controls.Add(新規販売取引申請書パネル);
                    新規販売取引申請書パネル.BringToFront();
                    break;
                case "是正予防会議通知書":
                    this.Controls.Add(是正予防会議通知書パネル);
                    是正予防会議通知書パネル.BringToFront();
                    break;
                case "是正予防会議通知書_環境":
                    //this.Controls.Add(是正予防会議通知書_環境パネル);
                    //是正予防会議通知書_環境パネル.BringToFront();
                    break;
                case "是正予防処置報告書":
                    this.Controls.Add(是正予防処置報告書パネル);
                    是正予防処置報告書パネル.BringToFront();
                    break;
                case "是正予防処置報告書_環境":
                    this.Controls.Add(是正予防処置報告書_環境パネル);
                    是正予防処置報告書_環境パネル.BringToFront();
                    break;
                case "製品企画書":
                    this.Controls.Add(製品企画書パネル);
                    製品企画書パネル.BringToFront();
                    break;
                case "設計審査会議事録":
                    this.Controls.Add(設計審査会議事録パネル);
                    設計審査会議事録パネル.BringToFront();
                    break;
                case "設計製作依頼書":
                    this.Controls.Add(設計製作依頼書パネル);
                    設計製作依頼書パネル.BringToFront();
                    break;
                case "設備購買申請書":
                    this.Controls.Add(設備購買申請書パネル);
                    設備購買申請書パネル.BringToFront();
                    break;
                case "年間教育計画表":
                    this.Controls.Add(年間教育計画パネル);
                    年間教育計画パネル.BringToFront();
                    break;
                case "非該当証明発行依頼書":
                    this.Controls.Add(非該当証明発行依頼書パネル);
                    非該当証明発行依頼書パネル.BringToFront();
                    break;
                case "品質異常報告書":
                    this.Controls.Add(品質異常報告書パネル);
                    品質異常報告書パネル.BringToFront();
                    break;
                case "不具合調査修理依頼書":
                    this.Controls.Add(不具合調査修理依頼書パネル);
                    不具合調査修理依頼書パネル.BringToFront();
                    break;
                default:
                    break;
            }




        }


        private void RemoveCustomForm()
        {


            switch (CustomFormName)
            {
                case "システム配布記録":
                    this.Controls.Remove(システム配布記録パネル);
                    break;
                case "環境連絡書":
                    this.Controls.Remove(環境連絡書パネル);
                    break;
                case "記録":
                    this.Controls.Remove(記録パネル);
                    break;
                case "議事録":
                    this.Controls.Remove(議事録パネル);
                    break;
                case "教育訓練実施要領書":
                    this.Controls.Remove(教育訓練実施要領書パネル);
                    break;
                case "出向依頼書":
                    this.Controls.Remove(出向依頼書パネル);
                    break;
                case "新規販売取引申請書":
                    this.Controls.Remove(新規販売取引申請書パネル);
                    break;
                case "是正予防会議通知書":
                    this.Controls.Remove(是正予防会議通知書パネル);
                    break;
                case "是正予防会議通知書_環境":
                    //this.Controls.Remove(是正予防会議通知書_環境パネル);
                    break;
                case "是正予防処置報告書":
                    this.Controls.Remove(是正予防処置報告書パネル);
                    break;
                case "是正予防処置報告書_環境":
                    this.Controls.Remove(是正予防処置報告書_環境パネル);
                    break;
                case "製品企画書":
                    this.Controls.Remove(製品企画書パネル);
                    break;
                case "設計審査会議事録":
                    this.Controls.Remove(設計審査会議事録パネル);
                    break;
                case "設計製作依頼書":
                    this.Controls.Remove(設計製作依頼書パネル);
                    break;
                case "設備購買申請書":
                    this.Controls.Remove(設備購買申請書パネル);
                    break;
                case "年間教育計画表":
                    this.Controls.Remove(年間教育計画パネル);
                    break;
                case "非該当証明発行依頼書":
                    this.Controls.Remove(非該当証明発行依頼書パネル);
                    break;
                case "品質異常報告書":
                    this.Controls.Remove(品質異常報告書パネル);
                    break;
                case "不具合調査修理依頼書":
                    this.Controls.Remove(不具合調査修理依頼書パネル);
                    break;
                default:
                    break;
            }




        }

        private void SetCustomFormEnabled(string ctlName, bool setValue)
        {

            Control targetCtl = this.Controls.OfType<Control>().FirstOrDefault(ctl => ctl.Name == ctlName);

            targetCtl.Enabled = setValue;
        }

        private bool GetCustomFormEnabled(string ctlName)
        {

            Control targetCtl = this.Controls.OfType<Control>().FirstOrDefault(ctl => ctl.Name == ctlName);

            return targetCtl.Enabled;
        }

        private void SetCustomFormValue(string ctlName, string setValue)
        {

            Control targetCtl = this.Controls.OfType<Control>().FirstOrDefault(ctl => ctl.Name == ctlName);

            if (targetCtl == null) return;

            targetCtl.Text = setValue;
        }

        private string GetCustomFormValue(string ctlName)
        {

            Control targetCtl = this.Controls.OfType<Control>().FirstOrDefault(ctl => ctl.Name == ctlName);

            return targetCtl.Text;
        }


        private void UpdateEditionList(string codeString)
        {

            OriginalClass ofn = new OriginalClass();


            ofn.SetComboBox(版数, "SELECT 版数 AS Value, 版数 AS Display, " +
                    "{ fn REPLACE(STR(CONVERT(bit, 承認者コード), 1, 0), '1', '■') } AS Display2 " +
                    "FROM T処理文書 " +
                    "WHERE (文書コード = '" + codeString + "') " +
                    "ORDER BY 版数 DESC");
            版数.DrawMode = DrawMode.OwnerDrawFixed;

        }

        private void UpdateCaption(bool changedOn, bool editedOn)
        {
            string strChanged = changedOn ? "*" : "";
            string strEdited = editedOn ? " - 編集中" : "";

            this.Text = this.Name + strChanged + strEdited;
        }

        private bool LoadHeader(Form formObject, string codeString, int editionNumber)
        {
            try
            {
                Connect();

                string strSQL;

                strSQL = "SELECT * FROM T処理文書 WHERE 文書コード ='" + codeString + "' and 版数 = " + editionNumber;




                VariableSet.SetTable2Form(this, strSQL, cn, "担当者コード1", "担当者コード2", "担当者コード3", "担当者コード4", "担当者コード5");

                if (!string.IsNullOrEmpty(回答期限.Text))
                {
                    DateTime tempDate = DateTime.Parse(回答期限.Text);
                    回答期限.Text = tempDate.ToString("yyyy/MM/dd");

                }

                if (!string.IsNullOrEmpty(回答日1.Text))
                {
                    DateTime tempDate = DateTime.Parse(回答日1.Text);
                    回答日1.Text = tempDate.ToString("yyyy/MM/dd");

                }

                if (!string.IsNullOrEmpty(回答日2.Text))
                {
                    DateTime tempDate = DateTime.Parse(回答日2.Text);
                    回答日2.Text = tempDate.ToString("yyyy/MM/dd");

                }

                if (!string.IsNullOrEmpty(回答日3.Text))
                {
                    DateTime tempDate = DateTime.Parse(回答日3.Text);
                    回答日3.Text = tempDate.ToString("yyyy/MM/dd");

                }

                if (!string.IsNullOrEmpty(回答日4.Text))
                {
                    DateTime tempDate = DateTime.Parse(回答日4.Text);
                    回答日4.Text = tempDate.ToString("yyyy/MM/dd");

                }

                if (!string.IsNullOrEmpty(回答日5.Text))
                {
                    DateTime tempDate = DateTime.Parse(回答日5.Text);
                    回答日5.Text = tempDate.ToString("yyyy/MM/dd");

                }

                if (!string.IsNullOrEmpty(回答日6.Text))
                {
                    DateTime tempDate = DateTime.Parse(回答日6.Text);
                    回答日6.Text = tempDate.ToString("yyyy/MM/dd");

                }

                if (!string.IsNullOrEmpty(結果日付.Text))
                {
                    DateTime tempDate = DateTime.Parse(結果日付.Text);
                    結果日付.Text = tempDate.ToString("yyyy/MM/dd");

                }

                return true;



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // エラーハンドリングが必要に応じて行われるべきです
                return false;
            }
        }


        private bool LoadHeaderSub(Form formObject, string codeString, int editionNumber)
        {
            try
            {
                Connect();

                string strSQL;

                strSQL = "SELECT * FROM T" + CustomFormName + " WHERE 文書コード ='" + codeString + "' and 版数 = " + editionNumber;




                VariableSet.SetTable2FormCustom(this, strSQL, cn, CustomFormName);

                if (CustomFormName == "設計製作依頼書")
                {
                    strDocName = 設計製作依頼書_その他文書名.Text;
                }


                return true;



            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // エラーハンドリングが必要に応じて行われるべきです
                return false;
            }
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
                        objControl2 = 確定日時;
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
                    if (RegTrans(CurrentCode, CurrentEdition))
                    {
                        if (string.IsNullOrEmpty(更新版数.Text))
                        {
                            更新版数.Text = "1";
                        }
                        else
                        {
                            更新版数.Text = (Convert.ToInt32(更新版数.Text) + 1).ToString();
                        }

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

        private bool RegTrans(string codeString, int editionNumber)
        {
            Connect();
            SqlTransaction transaction = cn.BeginTransaction();
            {

                try
                {

                    string strwhere = "文書コード='" + codeString + "' and 版数 =" + editionNumber;
                    // ヘッダ部の登録
                    if (!DataUpdater.UpdateOrInsertDataFrom(this, cn, "T処理文書", strwhere, "文書コード", transaction, "版数"))
                    {
                        transaction.Rollback();  // 変更をキャンセル
                        return false;
                    }


                    if (CustomFormName != null)
                    {
                        // カスタム部の登録
                        if (!DataUpdater.UpdateOrInsertDataFromCustom(this, cn, "T" + CustomFormName, strwhere, "文書コード", transaction, CustomFormName, "版数"))
                        {
                            transaction.Rollback();  // 変更をキャンセル
                            return false;
                        }
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










        private void Form_KeyDown(object sender, KeyEventArgs e)
        {



            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;

                case Keys.F1:
                    if (コマンド新規.Enabled) コマンド新規_Click(sender, e);
                    break;
                case Keys.F2:
                    if (コマンド修正.Enabled) コマンド修正_Click(sender, e);
                    break;
                case Keys.F3:
                    if (コマンド複写.Enabled) コマンド複写_Click(sender, e);
                    break;
                case Keys.F4:
                    if (コマンド削除.Enabled) コマンド削除_Click(sender, e);
                    break;
                case Keys.F5:
                    if (コマンド改ページ.Enabled) コマンド改ページ_Click(sender, e);
                    break;
                case Keys.F6:

                    break;
                case Keys.F7:
                    if (コマンド編集.Enabled) コマンド編集_Click(sender, e);
                    break;
                case Keys.F8:
                    if (コマンドリンク.Enabled) コマンドリンク_Click(sender, e);
                    break;
                case Keys.F9:
                    if (コマンド承認.Enabled) コマンド承認_Click(sender, e);
                    break;
                case Keys.F10:
                    if (コマンド確定.Enabled) コマンド確定_Click(sender, e);
                    break;
                case Keys.F11:
                    if (コマンド登録.Enabled) コマンド登録_Click(sender, e);
                    break;
                case Keys.F12:
                    if (コマンド終了.Enabled) コマンド終了_Click(sender, e);
                    break;
            }
        }

        private void コマンド新規_Click(object sender, EventArgs e)
        {
            try
            {

                Connect();

                // データへの変更がないときの処理
                if (!IsChanged)
                {
                    // 新規モードのときは内部の更新データを元に戻す
                    if (IsNewData && !string.IsNullOrEmpty(CurrentCode))
                    {
                        if (CurrentEdition == 1)
                        {
                            // 初版時のみ採番された番号を戻す
                            if (!FunctionClass.Recycle(cn, CurrentCode))
                            {
                                MessageBox.Show("文書コードは破棄されました。" + Environment.NewLine + Environment.NewLine +
                                                "文書コード　：　" + CurrentCode, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                        }
                        //文書添付を戻す
                        if (!DelAttachedDoc(CurrentCode, CurrentEdition))
                        {
                            return;
                        }

                    }

                }
                else
                {
                    // 修正されているときは登録確認を行う
                    var intRes = MessageBox.Show("文書コード : " + CurrentCode + " （第 " + CurrentEdition + " 版）\n\n変更内容を登録しますか？", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    switch (intRes)
                    {
                        case DialogResult.Yes:
                            // エラーチェック
                            if (!ErrCheck())
                            {
                                return;
                            }
                            // 登録処理
                            if (!SaveData())
                            {
                                if (MessageBox.Show("エラーのため登録できませんでした。" + Environment.NewLine +
                                                    "強制終了しますか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                {

                                }
                            }
                            break;
                        case DialogResult.No:
                            //新規コードを取得していたときはコードを戻す
                            if (IsNewData && !string.IsNullOrEmpty(CurrentCode) && CurrentEdition == 1)
                            {
                                if (!FunctionClass.Recycle(cn, CurrentCode))
                                {
                                    MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine +
                                                    "ユニットコード　：　" + CurrentCode, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }

                                //文書添付を戻す
                                if (!DelAttachedDoc(CurrentCode, CurrentEdition))
                                {
                                    return;
                                }
                            }
                            break;
                        case DialogResult.Cancel:
                            return;
                    }
                }



                //自分が編集中のときは参照モードへ移行しておく
                if (strLockPC == CommonConstants.MyComputerName)
                {
                    //参照モードへ移行する
                    if (SetUnlock(CurrentCode, CurrentEdition))
                    {
                        UpdateCaption(IsChanged, false);
                    }
                    else
                    {
                        MessageBox.Show("参照モードへの移行に失敗しました。\n他のユーザーがこのデータを開くと編集できない可能性があります。", "編集コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }


                if (!GoNewMode())
                {
                    MessageBox.Show($"エラーが発生しました。{Environment.NewLine}" +
                                    $"システム管理者へ連絡してください。{Environment.NewLine}{Environment.NewLine}" +
                                    $"[{this.Name}]を終了します。",
                                    "新規コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    blnEmergency = true;
                    this.Close();
                }


            }
            catch (Exception ex)
            {
                Debug.Print(Name + "_Unload - " + ex.Message);
                blnEmergency = true;
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }

        private void コマンド改版_Click(object sender, EventArgs e)
        {

            FunctionClass fn = new FunctionClass();
            try
            {

                fn.DoWait("改版しています...");


                string strNewCode;
                int intNewEdition;



                // 新ID（コード、版数）取得
                strNewCode = this.CurrentCode;
                intNewEdition = this.CurrentEdition + 1;

                // 添付文書複写
                if (!AttachCopied(this.CurrentCode, this.CurrentEdition, strNewCode, intNewEdition))
                {
                    MessageBox.Show($"エラーが発生しました。", "改版コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // カスタム情報があるときは（ID情報を）複写する
                if (!string.IsNullOrEmpty(CustomFormName))
                {
                    SetCustomFormValue(CustomFormName + "_文書コード", strNewCode);
                    SetCustomFormValue(CustomFormName + "_版数", intNewEdition.ToString());
                }


                // ID設定
                this.文書コード.Text = strNewCode;
                this.版数.Text = intNewEdition.ToString();

                // 進捗情報初期化
                InitialAdvance();

                // 動作制御
                string strFlow = "1"; // 適切な値に置き換える必要があります
                VariableSet.FlowControl(this, true, strFlow, "文書コード");

                if (this.文書名.Enabled)
                {
                    this.文書名.Focus();
                }
                else
                {
                    this.件名.Focus();
                }

                this.文書コード.Enabled = false;
                this.版数.Enabled = false;
                this.コマンド新規.Enabled = false;
                this.コマンド修正.Enabled = true;
                this.コマンド複写.Enabled = false;
                this.コマンド削除.Enabled = false;
                this.コマンド改版.Enabled = false;
                this.コマンド登録.Enabled = true;
                this.改版ボタン.Enabled = false;

                // 各コントロールを編集可とする
                // 承認後の文書データを複写した場合の対処
                FunctionClass.LockData(this, false);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error in コマンド改版_Click: {ex.Message}");
                MessageBox.Show($"エラーが発生しました。{Environment.NewLine}{Environment.NewLine}" +
                                $"{ex.Message}", "改版コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                fn.WaitForm.Close();
            }
        }

        private void InitialAdvance()
        {
            回答期限.Text = null;
            承認者コード1.Text = null;
            承認者コード2.Text = null;
            承認者コード3.Text = null;
            承認者コード4.Text = null;
            承認者コード5.Text = null;
            承認者コード6.Text = null;
            担当者コード1.SelectedIndex = -1;
            担当者名1.Text = null;
            担当者コード2.SelectedIndex = -1;
            担当者名2.Text = null;
            担当者コード3.SelectedIndex = -1;
            担当者名3.Text = null;
            担当者コード4.SelectedIndex = -1;
            担当者名4.Text = null;
            担当者コード5.SelectedIndex = -1;
            担当者名5.Text = null;
            担当者コード6.SelectedIndex = -1;
            担当者名6.Text = null;
            回答日1.Text = null;
            回答日2.Text = null;
            回答日3.Text = null;
            回答日4.Text = null;
            回答日5.Text = null;
            回答日6.Text = null;
            本文1.Text = null;
            本文2.Text = null;
            本文3.Text = null;
            本文4.Text = null;
            本文5.Text = null;
            本文6.Text = null;
            結果日付.Text = null;
            結果内容.Text = null;
            通信欄.Text = null;
            作成日時.Text = null;
            確定日時.Text = null;
            確定者コード.Text = null;
            承認日時.Text = null;
            承認者コード.SelectedIndex = -1;
            完了日.Text = null;
            完了承認日時.Text = null;
            完了承認者コード.SelectedIndex = -1;
            無効日時.Text = null;
            更新版数.Text = "0";
        }

        private bool AttachCopied(string srcCodeString, int srcEditionNumber, string tgtCodeString, int tgtEditionNumber)
        {
            try
            {

                Connect();

                SqlCommand cmd = new SqlCommand("usp_複写_添付文書", cn);
                cmd.CommandType = CommandType.StoredProcedure;

                // パラメータの設定
                cmd.Parameters.AddWithValue("@元文書コード", srcCodeString);
                cmd.Parameters.AddWithValue("@元版数", srcEditionNumber);
                cmd.Parameters.AddWithValue("@先文書コード", tgtCodeString);
                cmd.Parameters.AddWithValue("@先版数", tgtEditionNumber);


                cmd.ExecuteNonQuery();

                return true;
            }
            catch (Exception ex)
            {
                // エラーハンドリング
                Console.WriteLine($"Error in AttachCopied: {ex.Message}");
                return false;
            }
        }

        private void 改版ボタン_Click(object sender, EventArgs e)
        {
            コマンド改版_Click(sender, e);
        }

        private void コマンド修正_Click(object sender, EventArgs e)
        {
            try
            {

                if (!AskSave())
                    return;

                // 修正モードへ移行する
                if (!GoModifyMode())
                    throw new Exception("Error in GoModifyMode");

            }
            catch (Exception ex)
            {
                // エラーが発生したときは強制終了する
                Debug.Print(this.Name + "_コマンド修正_Click - " + ex.HResult + " : " + ex.Message);
                MessageBox.Show("エラーが発生しました。" + Environment.NewLine +
                    "[" + this.Name + "]を終了します。", "修正コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);


                blnEmergency = true;

                this.Close();
            }
        }

        private bool AskSave()
        {
            try
            {
                string strSQL;

                Connect();

                if (IsChanged)
                {
                    var intRes = MessageBox.Show(
                        "文書コード : " + this.CurrentCode + Environment.NewLine + Environment.NewLine +
                        "変更内容を登録しますか？", "保存の確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    switch (intRes)
                    {
                        case DialogResult.Yes:
                            // エラー検出
                            if (!ErrCheck())
                            {
                                return false;
                            }

                            // 登録処理
                            if (RegTrans(this.CurrentCode, this.CurrentEdition))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }

                        case DialogResult.No:
                            break;

                        case DialogResult.Cancel:
                            return false;
                    }
                }

                // 登録しない場合
                // 新規モードのときは内部の更新データを元に戻す
                if (this.IsNewData && !string.IsNullOrEmpty(this.CurrentCode))
                {
                    if (this.CurrentEdition == 1)
                    {
                        // 初版時のみ採番された番号を戻す
                        if (!FunctionClass.Recycle(cn, this.CurrentCode))
                        {
                            MessageBox.Show(
                                "文書コードは破棄されました。" + Environment.NewLine + Environment.NewLine +
                                "文書コード　：　" + this.CurrentCode, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }


                    // 文書添付を戻す
                    strSQL = "DELETE FROM T添付文書" +
                    " WHERE 文書コード = @CurrentCode AND 版数 = @CurrentEdition";

                    SqlTransaction transaction = null;

                    try
                    {
                        // トランザクション開始
                        transaction = cn.BeginTransaction();

                        // SqlCommandの作成
                        using (SqlCommand cmd = new SqlCommand(strSQL, cn, transaction))
                        {
                            // パラメーターの追加
                            cmd.Parameters.AddWithValue("@CurrentCode", this.CurrentCode);
                            cmd.Parameters.AddWithValue("@CurrentEdition", this.CurrentEdition);

                            // SQLコマンド実行
                            cmd.ExecuteNonQuery();
                        }

                        // トランザクションのコミット
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        // トランザクションのロールバック
                        if (transaction != null)
                        {
                            transaction.Rollback();
                        }

                        Debug.Print("Error: " + ex.Message);
                    }

                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.Print("AskSave - " + ex.HResult + " : " + ex.Message);
                return false;
            }
        }



        private void コマンド確定_Click(object sender, EventArgs e)
        {
            FunctionClass fn = new FunctionClass();
            Connect();

            try
            {
                object varSaved1 = null;  // 確定日時保存用（エラー発生時の対策）
                object varSaved2 = null;  // 確定者コード保存用（エラー発生時の対策）


                DialogResult result = MessageBox.Show(
        "現在システムにログインしているユーザーはこの文書の発信者ではありません。" + Environment.NewLine +
        "確定する前に発信者が入力中でないことを確認してください。" + Environment.NewLine + Environment.NewLine +
        "確定しますか？", "確定コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (result == DialogResult.No)
                {
                    return;
                }

                fn.DoWait("確定しています...");

                // エラーチェック
                if (!ErrCheck())
                {
                    return;
                };




                // 登録前の確定日を保存しておく
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
                    確定日時.Text = FunctionClass.GetServerDate(cn).ToString("yyyy/MM/dd");
                    確定者コード.Text = CommonConstants.LoginUserCode;
                }

                // 登録する
                if (SaveData())
                {

                    // 版数のソース更新
                    UpdateEditionList(CurrentCode);


                    //FunctionClass.LockData(this, IsDecided || IsDeleted, "ユニットコード", "ユニット版数");

                    ChangedData(false);

                    // 新規モードのときは修正モードへ移行する
                    if (IsNewData)
                    {
                        コマンド新規.Enabled = true;
                        コマンド修正.Enabled = false;
                        コマンド編集.Enabled = true;
                    }

                    コマンド承認.Enabled = IsDecided;

                    ControlDocument(ref strFlow);

                    承認ボタン.Enabled = IsDecided;
                }
                else
                {
                    確定日時.Text = varSaved1.ToString();
                    確定者コード.Text = varSaved2.ToString();
                    MessageBox.Show("登録できませんでした。", "確定コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Debug.Print($"{Name}_コマンド確定_Click - {ex.GetType().Name}: {ex.Message}");
                MessageBox.Show("エラーが発生したため、確定できませんでした。", "確定コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                fn.WaitForm.Close();
            }
        }


        public void ControlDocument(ref string FlowString)
        {
            try
            {
                if (!IsDecided)
                {
                    // 確定されていない状態
                    FunctionClass.LockData(this, false, "文書コード");
                    FlowString = "1";
                    VariableSet.FlowControl(this, true, strFlow);
                    通信欄.ReadOnly = true;
                    承認ボタン.Enabled = !(
                        IsNull(承認者コード1.Text) &&
                        IsNull(承認者コード2.Text) &&
                        IsNull(承認者コード3.Text) &&
                        IsNull(承認者コード4.Text) &&
                        IsNull(承認者コード5.Text) &&
                        IsNull(承認者コード6.Text));
                }
                else if (!IsApproved)
                {
                    // 確定されているが、承認されていない状態
                    FunctionClass.LockData(this, true, "文書コード");
                }
                else if (IsNull(結果内容.Text))
                {
                    // 承認されているが、結果が出ていない状態
                    if (Nz(承認者コード1.Text) != TRANSMIT &&
                        Nz(承認者コード2.Text) != TRANSMIT &&
                        Nz(承認者コード3.Text) != TRANSMIT &&
                        Nz(承認者コード4.Text) != TRANSMIT &&
                        Nz(承認者コード5.Text) != TRANSMIT &&
                        Nz(承認者コード6.Text) != TRANSMIT)
                    {
                        FlowString = "5";
                        VariableSet.FlowControl(this, true, strFlow);
                    }
                    else
                    {
                        FlowString = "3";
                        VariableSet.FlowControl(this, true, strFlow);
                        // 今後はコントロール配列を使いグループ化する
                        SetControlAccessibility(承認者コード1.Text, 担当者コード1, 回答日1, 回答日1選択ボタン, 本文1);
                        SetControlAccessibility(承認者コード2.Text, 担当者コード2, 回答日2, 回答日2選択ボタン, 本文2);
                        SetControlAccessibility(承認者コード3.Text, 担当者コード3, 回答日3, 回答日3選択ボタン, 本文3);
                        SetControlAccessibility(承認者コード4.Text, 担当者コード4, 回答日4, 回答日4選択ボタン, 本文4);
                        SetControlAccessibility(承認者コード5.Text, 担当者コード5, 回答日5, 回答日5選択ボタン, 本文5);
                        SetControlAccessibility(承認者コード6.Text, 担当者コード6, 回答日6, 回答日6選択ボタン, 本文6);

                        承認ボタン.Enabled =
                            (IsNull(承認者コード1) || 承認者コード1.Text == TRANSMIT) &&
                            (IsNull(承認者コード2) || 承認者コード2.Text == TRANSMIT) &&
                            (IsNull(承認者コード3) || 承認者コード3.Text == TRANSMIT) &&
                            (IsNull(承認者コード4) || 承認者コード4.Text == TRANSMIT) &&
                            (IsNull(承認者コード5) || 承認者コード5.Text == TRANSMIT) &&
                            (IsNull(承認者コード6) || 承認者コード6.Text == TRANSMIT);
                    }
                    switch (CommonConstants.LoginDep)
                    {
                        case "営業部":
                            // 無理やりユーザーコードで分岐
                            // ユーザーをグループに所属させるシステムが必要
                            if (CommonConstants.LoginUserCode == "002")
                            {
                                // 社長（兼営業部長）
                                承認6ボタン.Enabled = !IsNull(承認者コード6);
                                承認1ボタン.Enabled = !IsNull(承認者コード1);
                                承認2ボタン.Enabled = false;
                                承認3ボタン.Enabled = false;
                                承認5ボタン.Enabled = false;
                                承認4ボタン.Enabled = false;
                            }
                            else
                            {
                                // 営業部
                                承認6ボタン.Enabled = false;
                                承認1ボタン.Enabled = !IsNull(承認者コード1);
                                承認2ボタン.Enabled = false;
                                承認3ボタン.Enabled = false;
                                承認5ボタン.Enabled = false;
                                承認4ボタン.Enabled = false;
                            }
                            break;
                        case "技術部":
                            承認2ボタン.Enabled = !IsNull(承認者コード2);
                            承認1ボタン.Enabled = false;
                            承認3ボタン.Enabled = false;
                            承認4ボタン.Enabled = false;
                            承認5ボタン.Enabled = false;
                            承認6ボタン.Enabled = false;
                            break;
                        case "製造部":
                            承認3ボタン.Enabled = !IsNull(承認者コード3);
                            承認1ボタン.Enabled = false;
                            承認2ボタン.Enabled = false;
                            承認4ボタン.Enabled = false;
                            承認5ボタン.Enabled = false;
                            承認6ボタン.Enabled = false;
                            break;
                        case "管理部":
                            承認5ボタン.Enabled = !IsNull(承認者コード5);
                            承認1ボタン.Enabled = false;
                            承認2ボタン.Enabled = false;
                            承認3ボタン.Enabled = false;
                            承認4ボタン.Enabled = false;
                            承認6ボタン.Enabled = false;
                            break;
                        case "会長":
                            承認4ボタン.Enabled = !IsNull(承認者コード4);
                            承認1ボタン.Enabled = false;
                            承認2ボタン.Enabled = false;
                            承認3ボタン.Enabled = false;
                            承認5ボタン.Enabled = false;
                            承認6ボタン.Enabled = false;
                            // 社長ユーザーではログオンしない
                            break;
                            //case "社長":
                            //    SetControlAccessibility(承認者コード6, 承認6ボタン);
                            //    承認1ボタン.Enabled = false;
                            //    承認2ボタン.Enabled = false;
                            //    承認3ボタン.Enabled = false;
                            //    承認4ボタン.Enabled = false;
                            //    承認5ボタン.Enabled = false;
                            //    break;
                    }
                    // ログインユーザーが専務の場合は総務も承認可とする
                    //if (LoginUserCode == "002")
                    //{
                    //    承認2ボタン.Enabled = false;
                    //    承認3ボタン.Enabled = false;
                    //    承認4ボタン.Enabled = false;
                    //    承認5ボタン.Enabled = !IsNull(承認者コード5);
                    //}
                    通信欄.Enabled = true;
                }
                else if (!IsFinished)
                {
                    // 結果が出ているが完了承認されていない状態
                    FlowString = "5";
                    VariableSet.FlowControl(this, true, strFlow);
                    通信欄.Enabled = true;
                    // コマンド完了承認.Enabled = true;
                    完了承認ボタン.Enabled = true;
                }
                else
                {
                    // 完了承認されている状態
                    FlowString = "7";
                    VariableSet.FlowControl(this, true, strFlow);
                    通信欄.Enabled = false;
                }

                // ボタンの有効性を設定
                送信先1ボタン.Enabled = IsDecided && !IsAnswered;
                送信先2ボタン.Enabled = IsDecided && !IsAnswered;
                送信先3ボタン.Enabled = IsDecided && !IsAnswered;
                送信先4ボタン.Enabled = IsDecided && !IsAnswered;
                送信先5ボタン.Enabled = IsDecided && !IsAnswered;
                送信先6ボタン.Enabled = IsDecided && !IsAnswered;
                文書添付.Enabled = !IsFinished;
            }
            catch (Exception ex)
            {
                Debug.Print("Error: " + ex.Message);
            }
        }

        private void SetControlAccessibility(string approvalCode, Control code, Control date, Control dateButton, Control text)
        {
            if (approvalCode == TRANSMIT)
            {
                code.Enabled = true;
                date.Enabled = true;
                dateButton.Enabled = true;
                text.Enabled = true;
            }
            else
            {
                code.Enabled = false;
                date.Enabled = false;
                dateButton.Enabled = false;
                text.Enabled = false;

            }
        }


        private void コマンド承認_Click(object sender, EventArgs e)
        {

            FunctionClass fn = new FunctionClass();
            Connect();

            try
            {
                string strAppUserCode;   // 承認ユーザーコード
                object varSaved1;
                object varSaved2;
                string str1;             // 承認が許可されるユーザーコード


                // 送信先の確認
                if (IsNull(承認者コード1) &&
                    IsNull(承認者コード2) &&
                    IsNull(承認者コード3) &&
                    IsNull(承認者コード4) &&
                    IsNull(承認者コード5) &&
                    IsNull(承認者コード6))
                {
                    MessageBox.Show("送信先を指定してください。", "承認コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    送信先1ボタン.Focus();
                    return;
                }

                // 発信者の長のユーザーコードを得る
                str1 = GetHeadUserCode(発信者コード.Text);
                if (string.IsNullOrEmpty(str1))
                {
                    MessageBox.Show("承認者を特定できませんでした。" + Environment.NewLine +
                                    "社員マスタを確認してください。", "承認コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // 認証処理

                // ログオンユーザーが指定ユーザーなら認証者コードにユーザーコードを設定する
                if (CommonConstants.LoginUserCode == str1)
                {
                    strAppUserCode = str1;
                }
                else
                {
                    using (var authenticationForm = new F_認証())
                    {
                        authenticationForm.args = str1;
                        authenticationForm.ShowDialog();

                        if (string.IsNullOrEmpty(CommonConstants.strCertificateCode))
                        {
                            MessageBox.Show("認証できません。" + Environment.NewLine + "承認できません。", "承認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            strAppUserCode = CommonConstants.strCertificateCode;
                        }
                    }
                }




                // 値を退避させる
                varSaved1 = 承認日時.Text;
                varSaved2 = 承認者コード.SelectedValue;

                // 値をセットする
                if (IsApproved)
                {
                    承認日時.Text = null;
                    承認者コード.SelectedIndex = -1;
                }
                else
                {
                    承認日時.Text = FunctionClass.GetServerDate(cn).ToString();
                    承認者コード.SelectedValue = strAppUserCode;
                }


                // サーバーへ登録する
                if (RegTrans(CurrentCode, CurrentEdition))
                {

                    if (string.IsNullOrEmpty(更新版数.Text))
                    {
                        更新版数.Text = "1";
                    }
                    else
                    {
                        更新版数.Text = (Convert.ToInt32(更新版数.Text) + 1).ToString();
                    }

                    ChangedData(false);

                    if (ApprovedDoc(文書名.Text))
                    {
                        コマンド確定.Enabled = !IsApproved;
                        ControlDocument(ref strFlow);
                    }
                    else
                    {
                        MessageBox.Show("承認後処理でエラーが発生しました。", "承認コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    コマンド改版.Enabled = IsApproved && !IsFinished;
                    改版ボタン.Enabled = IsApproved && !IsFinished;
                }
                else
                {
                    承認日時.Text = varSaved1.ToString();
                    承認者コード.SelectedValue = varSaved2;
                    MessageBox.Show("登録できませんでした。", "承認コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }



            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_コマンド承認_Click - " + ex.Message);
            }

        }

        private string GetHeadUserCode(string userCode)
        {
            string headUserCode = "";

            Connect();

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandText = "SELECT 社員コード FROM M社員 WHERE " +
                                      "部=(" +
                                      "SELECT 部 FROM M社員 WHERE 社員コード=@userCode) " +
                                      "AND (" +
                                      "ユーザグループ２ = 'Director' " +
                                      "OR ユーザグループ２ = 'Boarder' " +
                                      "OR ユーザグループ２ = 'President')" +
                                      "OR ユーザグループ２ = 'Executive President'";
                    cmd.Parameters.AddWithValue("@userCode", userCode);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            headUserCode = reader["社員コード"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"エラーが発生しました。{Environment.NewLine}{Environment.NewLine}" +
                                  $"{ex.HResult} : {ex.Message}");
            }

            return headUserCode;
        }

        private bool ApprovedDoc(string docName)
        {
            int result = 0;

            Connect();

            switch (docName)
            {
                case "新規販売取引申請書":
                    result = 新規販売取引申請書_ApplyCustomer(新規販売取引申請書_顧客コード.Text);
                    if (result == -1)
                    {
                        MessageBox.Show($"顧客コード : {Nz(新規販売取引申請書_顧客コード)}" +
                                        $"{Environment.NewLine}{Environment.NewLine}" +
                                        "新規販売取引申請処理に失敗しました。",
                                        "新規販売取引申請", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }
                    break;

                default:
                    // 対象文書がない場合は処理成功とする
                    return true;
                    break;
            }

            return true;
        }


        private void コマンド複写_Click(object sender, EventArgs e)
        {

            FunctionClass fn = new FunctionClass();
            Connect();

            string strNewCode;
            int intNewEdition;

            try
            {

                fn.DoWait("複写しています...");


                strNewCode = FunctionClass.採番(cn, CommonConstants.CH_DOCUMENT);
                intNewEdition = 1;

                // 添付文書複写
                if (!AttachCopied(this.CurrentCode, this.CurrentEdition, strNewCode, intNewEdition))
                {
                    // 採番された番号を戻す
                    if (!FunctionClass.ReturnCode(cn, strNewCode))
                    {
                        MessageBox.Show($"エラーのためコードは破棄されました。{Environment.NewLine}{Environment.NewLine}" +
                                        $"文書コード　：　{strNewCode}",
                                        "複写コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                    return;
                }

                // カスタム情報があるときは（ID情報を）複写する
                if (!string.IsNullOrEmpty(CustomFormName))
                {
                    SetCustomFormValue(CustomFormName + "_文書コード", strNewCode);
                    SetCustomFormValue(CustomFormName + "_版数", intNewEdition.ToString());
                }

                // ID設定
                this.文書コード.Text = strNewCode;
                this.版数.Text = intNewEdition.ToString();

                // 編集モードへ移行する
                if (Setlock(this.CurrentCode, this.CurrentEdition, CommonConstants.MyComputerName))
                {
                    strLockPC = CommonConstants.MyComputerName;
                    this.EditOn = true;
                    UpdateCaption(this.IsChanged, this.EditOn);
                }
                else
                {
                    MessageBox.Show("新規モードへの移行に失敗しました。",
                                    "複写コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // 進捗情報初期化
                InitialAdvance();
                this.発信者コード.SelectedValue = CommonConstants.LoginUserCode;
                if (string.IsNullOrEmpty(((DataRowView)発信者コード.SelectedItem)?.Row.Field<string>("Display2")))
                {
                    発信者名.Text = ((DataRowView)発信者コード.SelectedItem)?.Row.Field<string>("Display2");
                }
                else
                {
                    発信者名.Text = null;
                }

                // 変更されたことになる
                ChangedData(true);

                // 動作制御
                this.strFlow = "1";
                VariableSet.FlowControl(this, true, strFlow, "文書コード");
                if (this.文書名.Enabled)
                    this.文書名.Focus();
                else
                    this.件名.Focus();

                this.文書コード.Enabled = false;
                this.版数.Enabled = false;
                this.コマンド新規.Enabled = false;
                this.コマンド修正.Enabled = true;
                this.コマンド複写.Enabled = false;
                this.コマンド削除.Enabled = false;
                this.コマンド改版.Enabled = false;
                this.コマンド編集.Enabled = false;
                this.コマンド登録.Enabled = true;
                this.改版ボタン.Enabled = false;

                // 各コントロールを編集可とする
                FunctionClass.LockData(this, false);


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

                    // 新規モードのときは修正モードへ移行する
                    if (IsNewData)
                    {
                        コマンド新規.Enabled = true;
                        コマンド修正.Enabled = false;
                        コマンド編集.Enabled = true;
                        コマンド承認.Enabled = IsDecided && !IsApproved;
                        承認ボタン.Enabled = IsDecided && !IsApproved;

                        OriginalClass ofn = new OriginalClass();
                        ofn.SetComboBox(文書コード, "SELECT 文書名 as Value,文書名 as Display, [フォーム名] as Display2 FROM M文書 ORDER BY 表示順序");



                    }


                    ControlDocument(ref strFlow);
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

        private void コマンド削除_Click(object sender, EventArgs e)
        {
            Connect();

            try
            {
                string strCertifier;
                string strMsg;



                strMsg = "文書コード　：　" + CurrentCode + Environment.NewLine +
                         "版数　：　" + CurrentEdition + Environment.NewLine + Environment.NewLine +
                         "この文書を削除しますか？" + Environment.NewLine +
                         "削除後、元に戻すことはできません。" + Environment.NewLine +
                         "また、改版された文書は前版の文書が有効になります。";

                if (MessageBox.Show(strMsg, "削除コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                // 認証するユーザーを特定する
                strCertifier = IsApproved ? 承認者コード.SelectedValue.ToString() : 発信者コード.SelectedValue.ToString();


                if (IsApproved)
                {
                    strCertifier = 承認者コード.Text;
                }
                else
                {
                    strCertifier = 発信者コード.Text;
                }

                using (var authenticationForm = new F_認証())
                {
                    authenticationForm.args = strCertifier;
                    authenticationForm.ShowDialog();

                    if (string.IsNullOrEmpty(CommonConstants.strCertificateCode))
                    {
                        MessageBox.Show("削除は中止されました。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                }

                // 削除処理
                if (DeleteData(cn, CurrentCode, CurrentEdition))
                {
                    MessageBox.Show("削除しました。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);


                    // 削除後、現在の版数を基に表示データを更新する
                    if (CurrentEdition - 1 > 0)
                    {
                        // 前版を表示する
                        版数.Focus();
                        版数.Text = (CurrentEdition - 1).ToString();
                    }
                    else
                    {
                        // 新規モードへ移行する
                        if (!GoNewMode())
                        {
                            MessageBox.Show("エラーが発生しました。" + Environment.NewLine +
                                            "システム管理者へ連絡してください。" + Environment.NewLine +
                                            "[" + Name + "]を終了します。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            Close();
                        }
                    }
                }
                else
                {
                    MessageBox.Show("削除できませんでした。" + Environment.NewLine +
                                    "他のユーザーにより承認されている可能性があります。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("削除できませんでした。" + Environment.NewLine + Environment.NewLine +
                                ex.HResult + " : " + ex.Message, "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private bool DeleteData(SqlConnection connection, string codeString, int editionNumber)
        {
            bool success = false;
            SqlTransaction transaction = null;

            try
            {

                // 他のユーザーによって承認されているかどうか確認する
                string strKey = $"文書コード = '{codeString}' AND 版数 = {editionNumber}";



                // 承認されていない場合にのみ削除処理を実行
                string strSQL1 = $"DELETE FROM T処理文書 WHERE {strKey}";
                string strSQL2 = $"DELETE FROM T添付文書 WHERE {strKey}";
                string strSQL3 = $"DELETE FROM T{CustomFormName} WHERE {strKey}";

                using (SqlCommand cmdDelete1 = new SqlCommand(strSQL1, connection))
                using (SqlCommand cmdDelete2 = new SqlCommand(strSQL2, connection))
                using (SqlCommand cmdDelete3 = new SqlCommand(strSQL3, connection))
                {
                    transaction = connection.BeginTransaction();

                    cmdDelete1.Transaction = transaction;
                    cmdDelete2.Transaction = transaction;
                    cmdDelete3.Transaction = transaction;

                    // 削除処理実行
                    cmdDelete1.ExecuteNonQuery();
                    cmdDelete2.ExecuteNonQuery();
                    if (!string.IsNullOrEmpty(CustomFormName))
                    {
                        cmdDelete3.ExecuteNonQuery();
                    }

                    // トランザクションのコミット
                    transaction.Commit();

                    success = true;
                }


            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                if (transaction != null)
                {
                    // トランザクションのロールバック
                    transaction.Rollback();
                }

                Console.WriteLine($"DeleteData - Error: {ex.Message}");
            }

            return success;
        }





        private void コマンド編集_Click(object sender, EventArgs e)
        {

            FunctionClass fn = new FunctionClass();
            Connect();

            try
            {

                fn.DoWait("しばらくお待ちください...");



                strLockPC = GetLockPC(this.CurrentCode, this.CurrentEdition);

                if (strLockPC == "")
                {
                    if (Convert.ToInt32(更新版数.Text) < Convert.ToInt32(GetLastEdition(this.CurrentCode, this.CurrentEdition)))
                    {
                        var intRes = MessageBox.Show("他のユーザーにより内容が変更されています。" +
                                                 Environment.NewLine + Environment.NewLine +
                                                 "表示データを最新の情報に更新しますか？",
                                                 "編集コマンド", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);

                        if (intRes == DialogResult.OK)
                        {
                            this.文書コード.Focus();

                            try
                            {
                                this.文書コード.Text = this.CurrentCode;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show("再読込に失敗しました。" + Environment.NewLine +
                                                "編集はできません。" + Environment.NewLine + Environment.NewLine +
                                                "いったん閉じてから再度開き直してください。",
                                                "編集コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                                return;
                            }
                        }
                        else
                        {
                            MessageBox.Show("古い内容での編集はできません。", "編集コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                    }

                    if (Setlock(this.CurrentCode, this.CurrentEdition, CommonConstants.MyComputerName))
                    {
                        strLockPC = CommonConstants.MyComputerName;
                        this.EditOn = true;
                        UpdateCaption(this.IsChanged, this.EditOn);
                        ControlDocument(ref strFlow);

                        this.コマンド承認.Enabled = this.IsDecided && !this.IsReplied;
                        this.コマンド確定.Enabled = !this.IsApproved;

                        if (!this.IsDeleted && this.IsApproved && !this.IsFinished &&
                            !CheckDocument(this.CurrentCode, this.CurrentEdition + 1))
                        {
                            this.コマンド改版.Enabled = true;
                            this.改版ボタン.Enabled = true;
                        }
                        else
                        {
                            this.コマンド改版.Enabled = false;
                            this.改版ボタン.Enabled = false;
                        }

                        this.承認ボタン.Enabled = this.IsDecided && !this.IsReplied;

                    }
                    else
                    {
                        MessageBox.Show("編集モードへの移行に失敗しました。", "編集コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else if (strLockPC == CommonConstants.MyComputerName)
                {
                    if (this.IsChanged)
                    {
                        var intRes = MessageBox.Show("この文書は変更されています。" + Environment.NewLine +
                                                 "保存しますか？", "編集コマンド",
                                                 MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                        switch (intRes)
                        {
                            case DialogResult.Yes:
                                if (RegTrans(this.CurrentCode, this.CurrentEdition))
                                {
                                    if (string.IsNullOrEmpty(更新版数.Text))
                                    {
                                        更新版数.Text = "1";
                                    }
                                    else
                                    {
                                        更新版数.Text = (Convert.ToInt32(更新版数.Text) + 1).ToString();
                                    }
                                    ChangedData(false);
                                }
                                else
                                {
                                    MessageBox.Show("エラーが発生したため、保存できませんでした。",
                                                    "編集コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                                break;
                            case DialogResult.Cancel:
                                return;
                        }
                    }

                    if (SetUnlock(this.CurrentCode, this.CurrentEdition))
                    {
                        strLockPC = "";
                        this.EditOn = false;
                        UpdateCaption(this.IsChanged, this.EditOn);
                        FunctionClass.LockData(this, true, "文書コード");

                        this.コマンド改版.Enabled = false;
                        this.コマンド承認.Enabled = false;
                        this.コマンド確定.Enabled = false;
                        this.送信先1ボタン.Enabled = false;
                        this.送信先2ボタン.Enabled = false;
                        this.送信先3ボタン.Enabled = false;
                        this.送信先4ボタン.Enabled = false;
                        this.送信先5ボタン.Enabled = false;
                        this.送信先6ボタン.Enabled = false;
                        this.承認1ボタン.Enabled = false;
                        this.承認2ボタン.Enabled = false;
                        this.承認3ボタン.Enabled = false;
                        this.承認4ボタン.Enabled = false;
                        this.承認5ボタン.Enabled = false;
                        this.承認6ボタン.Enabled = false;
                        this.承認ボタン.Enabled = false;
                        this.改版ボタン.Enabled = false;
                        this.完了承認ボタン.Enabled = false;
                    }
                    else
                    {
                        MessageBox.Show("参照モードへの移行に失敗しました。", "編集コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    MessageBox.Show("このデータは" + strLockPC + "上で編集中のため、編集できません。",
                                    "編集コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(this.Name + "_コマンド編集_Click - " + ex.HResult + " : " + ex.Message);
            }
            finally
            {
                fn.WaitForm.Close();
            }
        }


        private string GetLastEdition(string dataCode, int dataEdition)
        {
            string result = "";

            try
            {
                Connect();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = cn;
                    command.CommandText = "SELECT 更新版数 FROM T処理文書 " +
                                          "WHERE 文書コード = @DataCode AND 版数 = @DataEdition";
                    command.Parameters.AddWithValue("@DataCode", dataCode);
                    command.Parameters.AddWithValue("@DataEdition", dataEdition);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = reader["更新版数"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetLastEdition - " + ex.Message);
            }

            return result;
        }


        private string GetLockPC(string dataCode, int dataEdition)
        {
            string result = "";

            try
            {
                Connect();

                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = cn;
                    command.CommandText = "SELECT コンピュータ名 FROM S編集ロック " +
                                          "WHERE データコード = @DataCode AND データ版数 = @DataEdition";
                    command.Parameters.AddWithValue("@DataCode", dataCode);
                    command.Parameters.AddWithValue("@DataEdition", dataEdition);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = reader["コンピュータ名"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetLockPC - " + ex.Message);
            }


            return result;
        }

        private Button button = new Button();

        private void コマンド改ページ_Click(object sender, EventArgs e)
        {
            if (page == 1)
            {

                // 新しいパネルをフォームに追加
                //this.文書添付パネル.Location = new Point(10, 10 + 22);
                //this.文書添付パネル.Size = new Size(80, 20);
                this.Controls.Add(文書添付パネル);
                文書添付パネル.Visible = true;
                文書添付パネル.BringToFront();
                //this.Refresh();
                page = 2;
            }
            else
            {
                // 名前が一致しないパネルを削除
                this.Controls.Remove(文書添付パネル);
                //this.Refresh();
                page = 1;
            }



        }
        private void コマンドリンク_Click(object sender, EventArgs e)
        {
            try
            {

                Connect();

                // 本データのコードを取得
                string strDocumentCode = CurrentCode;

                // 本データがグループに登録済みかどうかを判断する
                int result = FunctionClass.DetectGroupMember(cn, strDocumentCode);

                switch (result)
                {
                    case 0:
                        // グループに登録されていない場合
                        F_グループ targetform = new F_グループ();

                        targetform.args = strDocumentCode;
                        targetform.ShowDialog();
                        break;
                    case 1:
                        // グループに登録されている場合
                        F_リンク targetform2 = new F_リンク();

                        targetform2.args = strDocumentCode;
                        targetform2.ShowDialog();
                        break;
                    case -1:
                        // エラーの場合
                        Console.WriteLine("エラーのため実行できません。");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("エラーが発生しました。" + Environment.NewLine + Environment.NewLine +
                                  ex.HResult + " : " + ex.Message);
            }
        }



        private void コマンド終了_Click(object sender, EventArgs e)
        {
            Close(); // フォームを閉じる
        }


        private void 印刷ボタン_Click(object sender, EventArgs e)
        {
            // デスクトップフォルダのパスを取得
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            // 画面のキャプチャをデスクトップに保存
            string screenshotFileName = "screenshot.png";
            string screenshotFilePath = Path.Combine(desktopPath, screenshotFileName);
            OriginalClass.CaptureActiveForm(screenshotFilePath);

            // 印刷ダイアログを表示
            OriginalClass.PrintScreen(screenshotFilePath);
        }

        private void 完了承認ボタン_Click(object sender, EventArgs e)
        {

            FunctionClass fn = new FunctionClass();
            Connect();

            try
            {
                string strAppUserCode;
                object varSaved1;
                object varSaved2;
                string str1;

                fn.DoWait("完了承認しています...");

                // ログインユーザーが発信者の長でないときは認証する
                str1 = GetHeadUserCode(発信者コード.Text);
                if (CommonConstants.LoginUserCode == str1)
                {
                    strAppUserCode = str1;
                }
                else
                {
                    using (var authenticationForm = new F_認証())
                    {
                        authenticationForm.args = str1;
                        authenticationForm.ShowDialog();

                        if (string.IsNullOrEmpty(CommonConstants.strCertificateCode))
                        {
                            MessageBox.Show("認証に失敗しました。" + Environment.NewLine + "完了承認はできません。", "完了承認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            strAppUserCode = CommonConstants.strCertificateCode;
                        }
                    }
                }




                // 承認情報を設定する前に現在の情報を保存しておく
                varSaved1 = this.完了承認日時.Text;
                varSaved2 = this.完了承認者コード.SelectedValue;

                // 承認情報を設定する
                if (this.IsFinished)
                {
                    this.完了承認日時.Text = null;
                    this.完了承認者コード.SelectedIndex = -1;
                }
                else
                {
                    this.完了承認日時.Text = FunctionClass.GetServerDate(cn).ToString();
                    this.完了承認者コード.SelectedValue = strAppUserCode;
                }

                // 表示されている内容で登録する
                if (RegTrans(this.CurrentCode, this.CurrentEdition))
                {
                    // 表示されている更新版数を１上げる（実際のデータはサーバー側のトリガで１増加）
                    if (string.IsNullOrEmpty(更新版数.Text))
                    {
                        更新版数.Text = "1";
                    }
                    else
                    {
                        更新版数.Text = (Convert.ToInt32(更新版数.Text) + 1).ToString();
                    }
                    // 完了承認後処理（■トランザクション内で処理すること）
                    if (FinishedDoc(this.文書名.Text))
                    {
                        ControlDocument(ref strFlow);
                    }
                    else
                    {
                        MessageBox.Show("完了承認後処理でエラーが発生しました。", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    this.完了承認日時.Text = varSaved1.ToString();
                    this.完了承認者コード.SelectedValue = varSaved2;
                    MessageBox.Show("登録できませんでした。", "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }


                return;



            }
            catch (Exception ex)
            {

                MessageBox.Show("予期しないエラーが発生しました。", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                fn.WaitForm.Close();
            }
        }


        private bool FinishedDoc(string docName)
        {
            try
            {
                int result = 0;

                Connect();

                switch (docName)
                {
                    case "新規販売取引申請書":
                        result = 新規販売取引申請書_ApprovalCustomer(新規販売取引申請書_顧客コード.Text);
                        if (result == -1)
                        {
                            MessageBox.Show($"顧客コード : {Nz(新規販売取引申請書_顧客コード.Text)}" +
                                            $"{Environment.NewLine}{Environment.NewLine}" +
                                            "顧客との取引開始処理に失敗しました。",
                                            "顧客取引開始",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation);
                            return false;
                        }
                        break;

                    case "年間教育計画表":
                        result = 年間教育計画表_Approval( CurrentCode, CurrentEdition, CommonConstants.LoginUserCode);
                        if (result == -1)
                        {
                            MessageBox.Show($"文書コード : {CurrentCode}{Environment.NewLine}" +
                                            $"版数 : {CurrentEdition}{Environment.NewLine}{Environment.NewLine}" +
                                            "年間教育計画での承認処理に失敗しました。",
                                            "年間教育計画承認",
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation);
                            return false;
                        }
                        break;

                    default:
                        // 対象文書がない場合は処理成功とする
                        return true;
                        break;
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.Print($"{this.Name}_FinishedDoc - {ex.Message}");
                return false;
            }
        }

        private void 否認ボタン_Click(object sender, EventArgs e)
        {
            MessageBox.Show("登録文書を否認するコマンドです。" + Environment.NewLine +
                            "このバージョンではサポートされません。",
                            "否認",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
        }

        private void 承認ボタン_Click(object sender, EventArgs e)
        {

            // コマンド承認_Clickを呼び出す
            コマンド承認_Click(sender, e);



        }

        private void 結果内容削除ボタン_Click()
        {
            if (MessageBox.Show("結果内容を削除すると、各発信先の承認を取り消すことができるようになりますが、" + Environment.NewLine +
                                "完了承認はできなくなります。" + Environment.NewLine + Environment.NewLine +
                                "削除しますか？", "結果内容削除確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            this.結果内容.Text = null;
            this.結果内容.Focus();
            ChangedData(true);
            ControlDocument(ref strFlow);
        }


        private void 承認1ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                string strAppUserCode; // 承認ユーザーコード
                string str1; // 承認が許可されるユーザーコード

                if (IsNull(担当者コード1.Text))
                    return;
                if (IsNull(承認者コード1.Text))
                    return;

                // ログインユーザーが発信者の長でないときは認証する
                str1 = GetHeadUserCode(担当者コード1.Text);
                if (CommonConstants.LoginUserCode == str1)
                {
                    strAppUserCode = str1;
                }
                else
                {
                    using (var authenticationForm = new F_認証())
                    {
                        authenticationForm.args = str1;
                        authenticationForm.ShowDialog();

                        if (string.IsNullOrEmpty(CommonConstants.strCertificateCode))
                        {
                            MessageBox.Show("認証できません。" + Environment.NewLine + "承認できません。", "回答承認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            strAppUserCode = CommonConstants.strCertificateCode;
                        }
                    }
                }

                // 承認者コードの切り替え
                承認者コード1.Text = (承認者コード1.Text == TRANSMIT) ? strAppUserCode : TRANSMIT;

                // 変更されたことにする
                ChangedData(true);

                // 文書の制御を行う
                ControlDocument(ref strFlow);
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーが発生しました。" + Environment.NewLine + Environment.NewLine +
                                ex.HResult + " : " + ex.Message, "回答承認", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 承認2ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                string strAppUserCode; // 承認ユーザーコード
                string str2; // 承認が許可されるユーザーコード

                if (IsNull(担当者コード2.Text))
                    return;
                if (IsNull(承認者コード2.Text))
                    return;

                // ログインユーザーが発信者の長でないときは認証する
                str2 = GetHeadUserCode(担当者コード2.Text);
                if (CommonConstants.LoginUserCode == str2)
                {
                    strAppUserCode = str2;
                }
                else
                {
                    using (var authenticationForm = new F_認証())
                    {
                        authenticationForm.args = str2;
                        authenticationForm.ShowDialog();

                        if (string.IsNullOrEmpty(CommonConstants.strCertificateCode))
                        {
                            MessageBox.Show("認証できません。" + Environment.NewLine + "承認できません。", "回答承認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            strAppUserCode = CommonConstants.strCertificateCode;
                        }
                    }
                }

                // 承認者コードの切り替え
                承認者コード2.Text = (承認者コード2.Text == TRANSMIT) ? strAppUserCode : TRANSMIT;

                // 変更されたことにする
                ChangedData(true);

                // 文書の制御を行う
                ControlDocument(ref strFlow);
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーが発生しました。" + Environment.NewLine + Environment.NewLine +
                                ex.HResult + " : " + ex.Message, "回答承認", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void 承認3ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                string strAppUserCode; // 承認ユーザーコード
                string str3; // 承認が許可されるユーザーコード

                if (IsNull(担当者コード3.Text))
                    return;
                if (IsNull(承認者コード3.Text))
                    return;

                // ログインユーザーが発信者の長でないときは認証する
                str3 = GetHeadUserCode(担当者コード3.Text);
                if (CommonConstants.LoginUserCode == str3)
                {
                    strAppUserCode = str3;
                }
                else
                {
                    using (var authenticationForm = new F_認証())
                    {
                        authenticationForm.args = str3;
                        authenticationForm.ShowDialog();

                        if (string.IsNullOrEmpty(CommonConstants.strCertificateCode))
                        {
                            MessageBox.Show("認証できません。" + Environment.NewLine + "承認できません。", "回答承認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            strAppUserCode = CommonConstants.strCertificateCode;
                        }
                    }
                }

                // 承認者コードの切り替え
                承認者コード3.Text = (承認者コード3.Text == TRANSMIT) ? strAppUserCode : TRANSMIT;

                // 変更されたことにする
                ChangedData(true);

                // 文書の制御を行う
                ControlDocument(ref strFlow);
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーが発生しました。" + Environment.NewLine + Environment.NewLine +
                                ex.HResult + " : " + ex.Message, "回答承認", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 承認4ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                string strAppUserCode; // 承認ユーザーコード
                string str4; // 承認が許可されるユーザーコード

                if (IsNull(担当者コード4.Text))
                    return;
                if (IsNull(承認者コード4.Text))
                    return;

                // ログインユーザーが発信者の長でないときは認証する
                str4 = GetHeadUserCode(担当者コード4.Text);
                if (CommonConstants.LoginUserCode == str4)
                {
                    strAppUserCode = str4;
                }
                else
                {
                    using (var authenticationForm = new F_認証())
                    {
                        authenticationForm.args = str4;
                        authenticationForm.ShowDialog();

                        if (string.IsNullOrEmpty(CommonConstants.strCertificateCode))
                        {
                            MessageBox.Show("認証できません。" + Environment.NewLine + "承認できません。", "回答承認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            strAppUserCode = CommonConstants.strCertificateCode;
                        }
                    }
                }

                // 承認者コードの切り替え
                承認者コード4.Text = (承認者コード4.Text == TRANSMIT) ? strAppUserCode : TRANSMIT;

                // 変更されたことにする
                ChangedData(true);

                // 文書の制御を行う
                ControlDocument(ref strFlow);
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーが発生しました。" + Environment.NewLine + Environment.NewLine +
                                ex.HResult + " : " + ex.Message, "回答承認", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 承認5ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                string strAppUserCode; // 承認ユーザーコード
                string str5; // 承認が許可されるユーザーコード

                if (IsNull(担当者コード5.Text))
                    return;
                if (IsNull(承認者コード5.Text))
                    return;

                // ログインユーザーが発信者の長でないときは認証する
                str5 = GetHeadUserCode(担当者コード5.Text);
                if (CommonConstants.LoginUserCode == str5)
                {
                    strAppUserCode = str5;
                }
                else
                {
                    using (var authenticationForm = new F_認証())
                    {
                        authenticationForm.args = str5;
                        authenticationForm.ShowDialog();

                        if (string.IsNullOrEmpty(CommonConstants.strCertificateCode))
                        {
                            MessageBox.Show("認証できません。" + Environment.NewLine + "承認できません。", "回答承認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            strAppUserCode = CommonConstants.strCertificateCode;
                        }
                    }
                }

                // 承認者コードの切り替え
                承認者コード5.Text = (承認者コード5.Text == TRANSMIT) ? strAppUserCode : TRANSMIT;

                // 変更されたことにする
                ChangedData(true);

                // 文書の制御を行う
                ControlDocument(ref strFlow);
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーが発生しました。" + Environment.NewLine + Environment.NewLine +
                                ex.HResult + " : " + ex.Message, "回答承認", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 承認6ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                string strAppUserCode; // 承認ユーザーコード
                string str6; // 承認が許可されるユーザーコード

                if (IsNull(担当者コード6.Text))
                    return;
                if (IsNull(承認者コード6.Text))
                    return;

                // ログインユーザーが発信者の長でないときは認証する
                str6 = GetHeadUserCode(担当者コード6.Text);
                if (CommonConstants.LoginUserCode == str6)
                {
                    strAppUserCode = str6;
                }
                else
                {
                    using (var authenticationForm = new F_認証())
                    {
                        authenticationForm.args = str6;
                        authenticationForm.ShowDialog();

                        if (string.IsNullOrEmpty(CommonConstants.strCertificateCode))
                        {
                            MessageBox.Show("認証できません。" + Environment.NewLine + "承認できません。", "回答承認", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            return;
                        }
                        else
                        {
                            strAppUserCode = CommonConstants.strCertificateCode;
                        }
                    }
                }

                // 承認者コードの切り替え
                承認者コード6.Text = (承認者コード6.Text == TRANSMIT) ? strAppUserCode : TRANSMIT;

                // 変更されたことにする
                ChangedData(true);

                // 文書の制御を行う
                ControlDocument(ref strFlow);
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーが発生しました。" + Environment.NewLine + Environment.NewLine +
                                ex.HResult + " : " + ex.Message, "回答承認", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




        private void 送信先1ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsNull(担当者コード1.Text))
                {
                    MessageBox.Show("既に回答されているため、変更できません。", "発信先", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // 本文1にフォーカスを設定
                本文1.Focus();

                // 承認者コード1の切り替え
                承認者コード1.Text = IsNull(承認者コード1.Text) ? TRANSMIT : null;

                // 変更されたことにする
                ChangedData(true);

                // 承認者コード1の更新
                UpdatedControl(承認者コード1);
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーが発生しました。" + Environment.NewLine + Environment.NewLine +
                                ex.HResult + " : " + ex.Message, "発信先", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void 送信先2ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsNull(担当者コード2.Text))
                {
                    MessageBox.Show("既に回答されているため、変更できません。", "発信先", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // 本文2にフォーカスを設定
                本文2.Focus();

                // 承認者コード2の切り替え
                承認者コード2.Text = IsNull(承認者コード2.Text) ? TRANSMIT : null;

                // 変更されたことにする
                ChangedData(true);

                // 承認者コード2の更新
                UpdatedControl(承認者コード2);
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーが発生しました。" + Environment.NewLine + Environment.NewLine +
                                ex.HResult + " : " + ex.Message, "発信先", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void 送信先3ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsNull(担当者コード3.Text))
                {
                    MessageBox.Show("既に回答されているため、変更できません。", "発信先", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // 本文3にフォーカスを設定
                本文3.Focus();

                // 承認者コード3の切り替え
                承認者コード3.Text = IsNull(承認者コード3.Text) ? TRANSMIT : null;

                // 変更されたことにする
                ChangedData(true);

                // 承認者コード3の更新
                UpdatedControl(承認者コード3);
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーが発生しました。" + Environment.NewLine + Environment.NewLine +
                                ex.HResult + " : " + ex.Message, "発信先", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void 送信先4ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsNull(担当者コード4.Text))
                {
                    MessageBox.Show("既に回答されているため、変更できません。", "発信先", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // 本文4にフォーカスを設定
                本文4.Focus();

                // 承認者コード4の切り替え
                承認者コード4.Text = IsNull(承認者コード4.Text) ? TRANSMIT : null;

                // 変更されたことにする
                ChangedData(true);

                // 承認者コード4の更新
                UpdatedControl(承認者コード4);
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーが発生しました。" + Environment.NewLine + Environment.NewLine +
                                ex.HResult + " : " + ex.Message, "発信先", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void 送信先5ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsNull(担当者コード5.Text))
                {
                    MessageBox.Show("既に回答されているため、変更できません。", "発信先", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // 本文5にフォーカスを設定
                本文5.Focus();

                // 承認者コード5の切り替え
                承認者コード5.Text = IsNull(承認者コード5.Text) ? TRANSMIT : null;

                // 変更されたことにする
                ChangedData(true);

                // 承認者コード5の更新
                UpdatedControl(承認者コード5);
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーが発生しました。" + Environment.NewLine + Environment.NewLine +
                                ex.HResult + " : " + ex.Message, "発信先", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void 送信先6ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                if (!IsNull(担当者コード6.Text))
                {
                    MessageBox.Show("既に回答されているため、変更できません。", "発信先", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                // 本文6にフォーカスを設定
                本文6.Focus();

                // 承認者コード6の切り替え
                承認者コード6.Text = IsNull(承認者コード6.Text) ? TRANSMIT : null;

                // 変更されたことにする
                ChangedData(true);

                // 承認者コード6の更新
                UpdatedControl(承認者コード6);
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーが発生しました。" + Environment.NewLine + Environment.NewLine +
                                ex.HResult + " : " + ex.Message, "発信先", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private object ApprovalDocument(string documentCode, int edition, string approverCode, string fieldName, bool enableCancel)
        {
            object result = null;

            try
            {

                Connect();

                using (SqlDataAdapter adapter = new SqlDataAdapter())
                {
                    using (SqlCommand selectCommand = new SqlCommand())
                    {
                        selectCommand.Connection = cn;
                        selectCommand.CommandText = "SELECT * FROM T処理文書 WHERE 文書コード = @DocumentCode AND 版数 = @Edition";
                        selectCommand.Parameters.AddWithValue("@DocumentCode", documentCode);
                        selectCommand.Parameters.AddWithValue("@Edition", edition);

                        adapter.SelectCommand = selectCommand;

                        using (DataSet dataSet = new DataSet())
                        {
                            adapter.Fill(dataSet, "T処理文書");

                            DataTable table = dataSet.Tables["T処理文書"];

                            if (table.Rows.Count > 0)
                            {
                                DataRow row = table.Rows[0];

                                if (row[fieldName] == DBNull.Value || row[fieldName].ToString() != approverCode)
                                {
                                    if (enableCancel)
                                    {
                                        // 未承認または不在承認かつ取り消し不可のとき → 承認
                                        // 改版なら以前の版を旧版に更新する
                                        if (edition > 1)
                                        {
                                            DataRow previousRow = table.Select($"文書コード = '{documentCode}' AND 版数 = {edition - 1}").FirstOrDefault();
                                            if (previousRow != null)
                                            {
                                                previousRow["無効日時"] = DateTime.Now;
                                            }
                                        }

                                        // 初期コード保存
                                        result = row[fieldName];
                                        // 承認
                                        row[fieldName] = approverCode;

                                        using (SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter))
                                        {
                                            adapter.Update(dataSet, "T処理文書");
                                        }
                                    }
                                    else
                                    {
                                        // 取り消し不可の場合は承認
                                        result = approverCode;
                                        row[fieldName] = approverCode;

                                        using (SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter))
                                        {
                                            adapter.Update(dataSet, "T処理文書");
                                        }
                                    }
                                }
                                else
                                {
                                    // 承認初期化
                                    result = row[fieldName];
                                    row[fieldName] = DBNull.Value;

                                    using (SqlCommandBuilder commandBuilder = new SqlCommandBuilder(adapter))
                                    {
                                        adapter.Update(dataSet, "T処理文書");
                                    }
                                }
                            }
                            else
                            {
                                Console.WriteLine("対象となる文書データが登録されていないか削除されています。\n続行できません。");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("ApprovalDocument - " + ex.Message);
            }


            return result;
        }




        //各コントロールの処理


        private F_カレンダー dateSelectionForm;

        private void 期限日選択ボタン_Click(object sender, EventArgs e)
        {
            if (IsApproved)
            {
                MessageBox.Show("承認後の修正はできません", "期限日入力", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {
                // 日付選択フォームを作成し表示
                dateSelectionForm = new F_カレンダー();

                if (!string.IsNullOrEmpty(回答期限.Text))
                {
                    dateSelectionForm.args = 回答期限.Text;
                }

                if (dateSelectionForm.ShowDialog() == DialogResult.OK && 回答期限.Enabled && !回答期限.ReadOnly)
                {
                    // 日付選択フォームから選択した日付を取得
                    string selectedDate = dateSelectionForm.SelectedDate;

                    // フォームAの日付コントロールに選択した日付を設定
                    回答期限.Text = selectedDate;
                    UpdatedControl(回答期限);
                }
            }
        }
        private void 回答期限_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 回答期限_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 回答期限_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 回答期限_DoubleClick(object sender, EventArgs e)
        {
            期限日選択ボタン_Click(sender, e);
        }

        private void 回答期限_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                期限日選択ボタン_Click(sender, e);
                e.Handled = true;
            }
        }

        private void 回答期限_KeyDown(object sender, KeyEventArgs e)
        {

        }





        private void 回答日1_DoubleClick(object sender, EventArgs e)
        {
            回答日1選択ボタン_Click(sender, e);
        }

        private void 回答日1_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 回答日1_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 回答日1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }


        private void 回答日1選択ボタン_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(回答日1.Text))
            {
                dateSelectionForm.args = 回答日1.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK && 回答日1.Enabled && !回答日1.ReadOnly)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                回答日1.Text = selectedDate;
                UpdatedControl(回答日1);
            }
        }


        private void 回答日2_DoubleClick(object sender, EventArgs e)
        {
            回答日2選択ボタン_Click(sender, e);
        }

        private void 回答日2_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 回答日2_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 回答日2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }


        private void 回答日2選択ボタン_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(回答日2.Text))
            {
                dateSelectionForm.args = 回答日2.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK && 回答日2.Enabled && !回答日2.ReadOnly)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                回答日2.Text = selectedDate;
                UpdatedControl(回答日2);
            }
        }


        private void 回答日3_DoubleClick(object sender, EventArgs e)
        {
            回答日3選択ボタン_Click(sender, e);
        }

        private void 回答日3_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 回答日3_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 回答日3_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }


        private void 回答日3選択ボタン_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(回答日3.Text))
            {
                dateSelectionForm.args = 回答日3.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK && 回答日3.Enabled && !回答日3.ReadOnly)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                回答日3.Text = selectedDate;
                UpdatedControl(回答日3);
            }
        }

        private void 回答日4_DoubleClick(object sender, EventArgs e)
        {
            回答日4選択ボタン_Click(sender, e);
        }

        private void 回答日4_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 回答日4_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 回答日4_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }


        private void 回答日4選択ボタン_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(回答日4.Text))
            {
                dateSelectionForm.args = 回答日4.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK && 回答日4.Enabled && !回答日4.ReadOnly)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                回答日4.Text = selectedDate;
                UpdatedControl(回答日4);
            }
        }

        private void 回答日5_DoubleClick(object sender, EventArgs e)
        {
            回答日5選択ボタン_Click(sender, e);
        }

        private void 回答日5_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 回答日5_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 回答日5_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }


        private void 回答日5選択ボタン_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(回答日5.Text))
            {
                dateSelectionForm.args = 回答日5.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK && 回答日5.Enabled && !回答日5.ReadOnly)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                回答日5.Text = selectedDate;
                UpdatedControl(回答日5);
            }
        }

        private void 回答日6_DoubleClick(object sender, EventArgs e)
        {
            回答日6選択ボタン_Click(sender, e);
        }

        private void 回答日6_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 回答日6_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 回答日6_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }


        private void 回答日6選択ボタン_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(回答日6.Text))
            {
                dateSelectionForm.args = 回答日6.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK && 回答日6.Enabled && !回答日6.ReadOnly)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                回答日6.Text = selectedDate;
                UpdatedControl(回答日6);
            }
        }



        private void 結果日付_DoubleClick(object sender, EventArgs e)
        {
            結果日付選択ボタン_Click(sender, e);
        }

        private void 結果日付_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 結果日付_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 結果日付_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 結果日付選択ボタン_Click(object sender, EventArgs e)
        {
            // 日付選択フォームを作成し表示
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(結果日付.Text))
            {
                dateSelectionForm.args = 結果日付.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK && 結果日付.Enabled && !結果日付.ReadOnly)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                結果日付.Text = selectedDate;
                UpdatedControl(結果日付);
            }
        }

        private void 通信欄_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 通信欄_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 通信欄_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 通信欄_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 4000);
        }


        private void 結果内容_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            ChangedData(true);
        }

        private void 結果内容_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 結果内容_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }
        private void 結果内容_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 4000);
        }


        private void 件名_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 件名_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }


        private void 件名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 100);
            ChangedData(true);
        }


        private void 承認者コード_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 承認者コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }


        private void 担当者コード1_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 30, 100 }, new string[] { "Display", "Display2" });
            担当者コード1.Invalidate();
            担当者コード1.DroppedDown = true;
        }

        private void 担当者コード2_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 30, 100 }, new string[] { "Display", "Display2" });
            担当者コード2.Invalidate();
            担当者コード2.DroppedDown = true;
        }

        private void 担当者コード3_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 30, 100 }, new string[] { "Display", "Display2" });
            担当者コード3.Invalidate();
            担当者コード3.DroppedDown = true;
        }

        private void 担当者コード4_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 30, 100 }, new string[] { "Display", "Display2" });
            担当者コード4.Invalidate();
            担当者コード4.DroppedDown = true;
        }

        private void 担当者コード5_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 30, 100 }, new string[] { "Display", "Display2" });
            担当者コード5.Invalidate();
            担当者コード5.DroppedDown = true;
        }

        private void 担当者コード6_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 30, 100 }, new string[] { "Display", "Display2" });
            担当者コード6.Invalidate();
            担当者コード6.DroppedDown = true;
        }

        private void 発信者コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 30, 100 }, new string[] { "Display", "Display2" });
            発信者コード.Invalidate();
            発信者コード.DroppedDown = true;
        }

        private void 担当者コード6_Validated(object sender, EventArgs e)
        {

        }

        private void 担当者コード6_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 担当者コード6_SelectedIndexChanged(object sender, EventArgs e)
        {
            担当者名6.Text = ((DataRowView)担当者コード6.SelectedItem)?.Row.Field<String>("Display2")?.ToString();

            UpdatedControl(sender as Control);
        }


        private void 担当者コード6_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);

            if (担当者コード6.SelectedValue == null)
            {
                担当者名6.Text = null;
            }
        }




        private void 担当者コード1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 担当者コード1_SelectedIndexChanged(object sender, EventArgs e)
        {
            担当者名1.Text = ((DataRowView)担当者コード1.SelectedItem)?.Row.Field<String>("Display2")?.ToString();

            UpdatedControl(sender as Control);
        }


        private void 担当者コード1_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);

            if (担当者コード1.SelectedValue == null)
            {
                担当者名1.Text = null;
            }
        }




        private void 担当者コード2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 担当者コード2_SelectedIndexChanged(object sender, EventArgs e)
        {
            担当者名2.Text = ((DataRowView)担当者コード2.SelectedItem)?.Row.Field<String>("Display2")?.ToString();

            UpdatedControl(sender as Control);
        }


        private void 担当者コード2_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);

            if (担当者コード2.SelectedValue == null)
            {
                担当者名2.Text = null;
            }
        }



        private void 担当者コード3_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 担当者コード3_SelectedIndexChanged(object sender, EventArgs e)
        {
            担当者名3.Text = ((DataRowView)担当者コード3.SelectedItem)?.Row.Field<String>("Display2")?.ToString();

            UpdatedControl(sender as Control);
        }


        private void 担当者コード3_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);

            if (担当者コード3.SelectedValue == null)
            {
                担当者名3.Text = null;
            }
        }



        private void 担当者コード4_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 担当者コード4_SelectedIndexChanged(object sender, EventArgs e)
        {
            担当者名4.Text = ((DataRowView)担当者コード4.SelectedItem)?.Row.Field<String>("Display2")?.ToString();

            UpdatedControl(sender as Control);
        }


        private void 担当者コード4_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);

            if (担当者コード4.SelectedValue == null)
            {
                担当者名4.Text = null;
            }
        }


        private void 担当者コード5_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 担当者コード5_SelectedIndexChanged(object sender, EventArgs e)
        {
            担当者名5.Text = ((DataRowView)担当者コード5.SelectedItem)?.Row.Field<String>("Display2")?.ToString();

            UpdatedControl(sender as Control);
        }


        private void 担当者コード5_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);

            if (担当者コード5.SelectedValue == null)
            {
                担当者名5.Text = null;
            }
        }


        private void 発信者コード_Validated(object sender, EventArgs e)
        {

        }

        private void 発信者コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 発信者コード_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void 発信者コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            発信者名.Text = ((DataRowView)発信者コード.SelectedItem)?.Row.Field<String>("Display2")?.ToString();

            UpdatedControl(sender as Control);
        }

        private void 発信者コード_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);

            if (発信者コード.SelectedValue == null)
            {
                発信者名.Text = null;
            }
        }



        private void 発信者コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                ComboBox comboBox = sender as ComboBox;
                if (comboBox != null)
                {
                    comboBox.DroppedDown = true;
                }
                e.KeyChar = (char)0;
            }
        }


        private void 版数_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 版数_TextChanged(object sender, EventArgs e)
        {

        }

        private void 版数_Validated(object sender, EventArgs e)
        {

        }

        private void 版数_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 版数_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 30, 30 }, new string[] { "Display", "Display2" });
            版数.Invalidate();
            版数.DroppedDown = true;
        }




        private void 分類コード_Validated(object sender, EventArgs e)
        {

        }

        private void 分類コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 分類コード_KeyDown(object sender, KeyEventArgs e)
        {

        }


        private void 分類コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }


        private void 分類コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                ComboBox comboBox = sender as ComboBox;
                if (comboBox != null)
                {
                    comboBox.DroppedDown = true;
                }
                e.KeyChar = (char)0;
            }
        }
















        private void 文書コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }


        private void 文書コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 200, 0 }, new string[] { "Display", "Display2" });
            文書コード.Invalidate();
            文書コード.DroppedDown = true;
        }


        private void 文書コード_TextChanged(object sender, EventArgs e)
        {

        }

        private void 文書コード_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■文書コードを入力あるいは選択します。　■コードは簡易入力できます。";
        }

        private void 文書コード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 文書コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                string strCode = 文書コード.Text;
                if (string.IsNullOrEmpty(strCode))
                {
                    return;
                }

                strCode = FunctionClass.FormatDocumentCode(strCode);

                if (strCode != 文書コード.Text)
                {
                    文書コード.Text = strCode;
                }
            }
        }

        private void 文書コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)FunctionClass.ChangeBig((int)e.KeyChar);
        }


        private void 文書コード_Validated(object sender, EventArgs e)
        {

        }

        private void 文書コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }



        private void 文書フローコード_Validated(object sender, EventArgs e)
        {

        }

        private void 文書フローコード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 文書フローコード_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 文書フローコード_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void 文書フローコード_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }


        private void 文書フローコード_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                ComboBox comboBox = sender as ComboBox;
                if (comboBox != null)
                {
                    comboBox.DroppedDown = true;
                }
                e.KeyChar = (char)0;
            }
        }




        private void 文書名_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 400, 0 }, new string[] { "Display", "Display2" });
            文書名.Invalidate();
            文書名.DroppedDown = true;
        }
        private void 文書名_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);

        }


        private void 文書名_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void 文書名_Validated(object sender, EventArgs e)
        {

        }

        private void 文書名_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 文書名_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                ComboBox comboBox = sender as ComboBox;
                if (comboBox != null)
                {
                    comboBox.DroppedDown = true;
                }
                e.KeyChar = (char)0;
            }
        }

        private void 文書名_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }


        private void 本文1_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角2,000文字まで入力可。　■別ウィンドウに表示するには入力欄をダブルクリックしてください。";
        }

        private void 本文1_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
            本文1.SelectionStart = 0;
        }



        private void 本文1_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            ChangedData(true);
        }

        private void 本文1_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 本文1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 本文1_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 4000);
        }


        private void 本文6_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 本文6_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }



        private void 本文6_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角2,000文字まで入力可。　■ダブルクリックでズーム表示。";
            本文6.SelectionStart = 0;
        }

        private void 本文6_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 本文6_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            ChangedData(true);
        }

        private void 本文6_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 4000);
        }




        private void 本文2_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 4000);
        }

        private void 本文2_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            ChangedData(true);
        }

        private void 本文2_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 本文2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 本文2_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角2,000文字まで入力可。　■ダブルクリックでズーム表示。";
            本文2.SelectionStart = 0;
        }

        private void 本文2_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }





        private void 本文3_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角2,000文字まで入力可。　■ダブルクリックでズーム表示。";
            本文3.SelectionStart = 0;
        }

        private void 本文3_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 本文3_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 4000);
        }

        private void 本文3_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 本文3_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 本文3_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            ChangedData(true);
        }





        private void 本文5_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 4000);
        }

        private void 本文5_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角2,000文字まで入力可。　■ダブルクリックでズーム表示。";
            本文5.SelectionStart = 0;
        }

        private void 本文5_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 本文5_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            ChangedData(true);
        }

        private void 本文5_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 本文5_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }


        private void 本文4_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 4000);
        }

        private void 本文4_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角2,000文字まで入力可。　■ダブルクリックでズーム表示。";
            本文4.SelectionStart = 0;
        }

        private void 本文4_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 本文4_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            ChangedData(true);
        }

        private void 本文4_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 本文4_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }



        private void 本文_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 4000);
        }

        private void 本文_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角2,000文字まで入力可。　■ダブルクリックでズーム表示。";
            本文.SelectionStart = 0;
        }

        private void 本文_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 本文_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            ChangedData(true);
        }

        private void 本文_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 本文_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }




















        private void 文書名_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■[space]キーでドロップダウンリストを表示できます。";
        }

        private void 文書名_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 文書フローコード_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■文書の処理フローを指定します。　■[space]キーで選択します。　■現時点では使用できません。";
        }

        private void 文書フローコード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }





        private void 発信者コード_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■[space]キーで氏名を選択します。";
        }

        private void 発信者コード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }


        private void 担当者コード6_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■担当者コードを入力あるいはドロップダウンリストから選択します。";
        }

        private void 担当者コード6_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 担当者コード1_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■担当者コードを入力あるいはドロップダウンリストから選択します。";
        }

        private void 担当者コード1_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 担当者コード2_Validated(object sender, EventArgs e)
        {

        }



        private void 担当者コード2_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■担当者コードを入力あるいはドロップダウンリストから選択します。";
        }

        private void 担当者コード2_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 担当者コード3_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■担当者コードを入力あるいはドロップダウンリストから選択します。";
        }

        private void 担当者コード3_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }



        private void 担当者コード3_Validated(object sender, EventArgs e)
        {

        }


        private void 担当者コード5_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■担当者コードを入力あるいはドロップダウンリストから選択します。";
        }

        private void 担当者コード5_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }



        private void 担当者コード5_Validated(object sender, EventArgs e)
        {

        }



        private void 担当者コード4_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■担当者コードを入力あるいはドロップダウンリストから選択します。";
        }

        private void 担当者コード4_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 担当者コード4_TextUpdate(object sender, EventArgs e)
        {

        }

        private void 担当者コード4_Validated(object sender, EventArgs e)
        {

        }



        private void 結果内容_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角2,000文字まで入力可。　■ダブルクリックでズーム表示。";
        }

        private void 結果内容_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }



        private void 通信欄_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■承認後完了承認されるまでの間入力できます。　■全角２，０００文字まで入力できます。";
        }

        private void 通信欄_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }



        #region システム配布記録

        private void システム配布記録_配布日_KeyDown(object sender, KeyEventArgs e)
        {

        }
        private void システム配布記録_配布日_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                システム配布記録_配布日選択ボタン_Click(sender, e);
            }
        }

        private void システム配布記録_配布日_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void システム配布記録_配布日_Validated(object sender, EventArgs e)
        {

        }

        private void システム配布記録_配布日_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void システム配布記録_配布日選択ボタン_Click(object sender, EventArgs e)
        {
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(システム配布記録_配布日.Text))
            {
                dateSelectionForm.args = システム配布記録_配布日.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK && システム配布記録_配布日.Enabled && !システム配布記録_配布日.ReadOnly)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                システム配布記録_配布日.Text = selectedDate;
                システム配布記録_配布日.Focus();
                UpdatedControl(結果日付);
            }
        }

        private void システム配布記録_配布バージョン_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void システム配布記録_配布バージョン_Validated(object sender, EventArgs e)
        {

        }

        private void システム配布記録_配布バージョン_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void システム配布記録_配布目的_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 4000);

        }

        private void システム配布記録_配布目的_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void システム配布記録_配布目的_Validated(object sender, EventArgs e)
        {

        }

        private void システム配布記録_配布目的_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        #endregion


        #region 環境連絡書

        private void 環境連絡書_発生日_DoubleClick(object sender, EventArgs e)
        {
            環境連絡書_発生日選択ボタン_Click(sender, e);
        }

        private void 環境連絡書_発生日_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void 環境連絡書_発生日_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 環境連絡書_発生日_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                環境連絡書_発生日選択ボタン_Click(sender, e);
            }
        }

        private void 環境連絡書_発生日_Validated(object sender, EventArgs e)
        {

        }

        private void 環境連絡書_発生日_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 環境連絡書_発生日選択ボタン_Click(object sender, EventArgs e)
        {
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(環境連絡書_発生日.Text))
            {
                dateSelectionForm.args = 環境連絡書_発生日.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK && 環境連絡書_発生日.Enabled && !環境連絡書_発生日.ReadOnly)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                環境連絡書_発生日.Text = selectedDate;
                環境連絡書_発生日.Focus();
                UpdatedControl(環境連絡書_発生日);
            }

        }

        private void 環境連絡書_連絡先_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角100文字まで入力できます。";
        }

        private void 環境連絡書_連絡先_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 環境連絡書_連絡先_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 200);
            ChangedData(true);
        }

        private void 環境連絡書_連絡先_Validated(object sender, EventArgs e)
        {

        }

        private void 環境連絡書_連絡先_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 環境連絡書_環境負荷_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■ドロップダウンリストから選択します。";
        }

        private void 環境連絡書_環境負荷_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 環境連絡書_環境負荷_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 環境連絡書_環境負荷_Validated(object sender, EventArgs e)
        {

        }

        private void 環境連絡書_環境負荷_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 環境連絡書_異常内容_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 4000);
        }

        private void 環境連絡書_異常内容_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角2,000文字まで入力可。　■ダブルクリックでズーム表示。";
        }

        private void 環境連絡書_異常内容_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 環境連絡書_異常内容_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            ChangedData(true);
        }

        private void 環境連絡書_異常内容_Validated(object sender, EventArgs e)
        {

        }

        private void 環境連絡書_異常内容_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 環境連絡書_回答書ボタン_Click(object sender, EventArgs e)
        {

        }

        #endregion


        #region 記録

        private void 記録_日付1_DoubleClick(object sender, EventArgs e)
        {
            記録_日付1選択ボタン_Click(sender, e);
        }

        private void 記録_日付1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void 記録_日付1_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(記録_日付1, 記録_日付2, sender as Control);
        }

        private void 記録_日付1_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 記録_日付1_Validated(object sender, EventArgs e)
        {

        }

        private void 記録_日付1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 記録_日付1選択ボタン_Click(object sender, EventArgs e)
        {
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(記録_日付1.Text))
            {
                dateSelectionForm.args = 記録_日付1.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK && 記録_日付1.Enabled && !記録_日付1.ReadOnly)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                記録_日付1.Text = selectedDate;
                記録_日付1.Focus();
                UpdatedControl(記録_日付1);
            }
        }

        private void 記録_日付1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                記録_日付1選択ボタン_Click(sender, e);
            }
        }

        private void 記録_日付2_DoubleClick(object sender, EventArgs e)
        {
            記録_日付2選択ボタン_Click(sender, e);
        }

        private void 記録_日付2_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void 記録_日付2_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(記録_日付1, 記録_日付2, sender as Control);
        }

        private void 記録_日付2_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 記録_日付2_Validated(object sender, EventArgs e)
        {

        }

        private void 記録_日付2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 記録_日付2選択ボタン_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(記録_日付2.Text))
            {
                dateSelectionForm.args = 記録_日付2.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK && 記録_日付2.Enabled && !記録_日付2.ReadOnly)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                記録_日付2.Text = selectedDate;
                記録_日付2.Focus();
                UpdatedControl(記録_日付2);
            }
        }

        private void 記録_日付2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                記録_日付2選択ボタン_Click(sender, e);
            }
        }

        private void 記録_参加者_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 200);
            ChangedData(true);
        }

        private void 記録_参加者_Validated(object sender, EventArgs e)
        {

        }

        private void 記録_参加者_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 記録_場所_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 100);
            ChangedData(true);
        }

        private void 記録_場所_Validated(object sender, EventArgs e)
        {

        }

        private void 記録_場所_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 記録_目的_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 2000);
            ChangedData(true);
        }

        private void 記録_目的_Validated(object sender, EventArgs e)
        {

        }

        private void 記録_目的_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 記録_目的_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 2000);
        }

        private void 記録_目的_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■2,000字（全角・半角の区別なし）まで入力できます。";
        }

        private void 記録_目的_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 記録_報告内容_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 4000);
        }

        private void 記録_報告内容_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■4,000字（全角・半角の区別なし）まで入力できます。";
        }

        private void 記録_報告内容_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 記録_報告内容_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            ChangedData(true);
        }

        private void 記録_報告内容_Validated(object sender, EventArgs e)
        {

        }

        private void 記録_報告内容_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        #endregion


        #region 議事録

        private void 議事録_開催日_DoubleClick(object sender, EventArgs e)
        {
            議事録_開催日選択ボタン_Click(sender, e);
        }

        private void 議事録_開催日_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■入力欄をダブルクリックすると、日付をカレンダーから入力することができます。";
        }

        private void 議事録_開催日_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 議事録_開催日_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void 議事録_開催日_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 議事録_開催日_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 議事録_開催日選択ボタン_Click(object sender, EventArgs e)
        {
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(議事録_開催日.Text))
            {
                dateSelectionForm.args = 議事録_開催日.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK && 議事録_開催日.Enabled && !議事録_開催日.ReadOnly)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                議事録_開催日.Text = selectedDate;
                議事録_開催日.Focus();
                UpdatedControl(議事録_開催日);
            }
        }

        private void 議事録_開催日_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                議事録_開催日選択ボタン_Click(sender, e);
            }
        }

        private void 議事録_開催場所_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角５０文字まで入力できます。";
        }

        private void 議事録_開催場所_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 議事録_開催場所_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 100);
            ChangedData(true);
        }

        private void 議事録_開催場所_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 議事録_参加者_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 議事録_参加者_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 200);
            ChangedData(true);
        }

        private void 議事録_参加者_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角１００文字まで入力できます。";
        }

        private void 議事録_参加者_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 議事録_内容_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 4000);
        }

        private void 議事録_内容_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角２，０００文字まで入力できます。";
        }

        private void 議事録_内容_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 議事録_内容_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            ChangedData(true);
        }

        private void 議事録_内容_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        #endregion


        #region 教育訓練実施要領書

        private void 教育訓練実施要領書_受講者名_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 教育訓練実施要領書_受講者名_Validated(object sender, EventArgs e)
        {

        }

        private void 教育訓練実施要領書_受講者名_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 教育訓練実施要領書_訓練名_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 教育訓練実施要領書_訓練名_Validated(object sender, EventArgs e)
        {

        }

        private void 教育訓練実施要領書_訓練名_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 教育訓練実施要領書_実施場所_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 教育訓練実施要領書_実施場所_Validated(object sender, EventArgs e)
        {

        }

        private void 教育訓練実施要領書_実施場所_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 教育訓練実施要領書_日付1_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void 教育訓練実施要領書_日付1_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 教育訓練実施要領書_日付1_Validated(object sender, EventArgs e)
        {

        }

        private void 教育訓練実施要領書_日付1_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 教育訓練実施要領書_日付1選択ボタン_Click(object sender, EventArgs e)
        {
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(教育訓練実施要領書_日付1.Text))
            {
                dateSelectionForm.args = 教育訓練実施要領書_日付1.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK && 教育訓練実施要領書_日付1.Enabled && !教育訓練実施要領書_日付1.ReadOnly)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                教育訓練実施要領書_日付1.Text = selectedDate;
                教育訓練実施要領書_日付1.Focus();
                UpdatedControl(教育訓練実施要領書_日付1);
            }
        }

        private void 教育訓練実施要領書_日付1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                教育訓練実施要領書_日付1選択ボタン_Click(sender, e);
            }
        }

        private void 教育訓練実施要領書_日付2_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void 教育訓練実施要領書_日付2_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 教育訓練実施要領書_日付2_Validated(object sender, EventArgs e)
        {

        }

        private void 教育訓練実施要領書_日付2_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 教育訓練実施要領書_日付2選択ボタン_Click(object sender, EventArgs e)
        {
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(教育訓練実施要領書_日付2.Text))
            {
                dateSelectionForm.args = 教育訓練実施要領書_日付2.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK && 教育訓練実施要領書_日付2.Enabled && !教育訓練実施要領書_日付2.ReadOnly)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                教育訓練実施要領書_日付2.Text = selectedDate;
                教育訓練実施要領書_日付2.Focus();
                UpdatedControl(教育訓練実施要領書_日付2);
            }
        }

        private void 教育訓練実施要領書_日付2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                教育訓練実施要領書_日付2選択ボタン_Click(sender, e);
            }
        }

        private void 教育訓練実施要領書_目的_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 1000);
        }

        private void 教育訓練実施要領書_目的_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角５００文字まで入力できます。　■別ウィンドウから入力するときは入力欄をダブルクリックしてください。";
        }

        private void 教育訓練実施要領書_目的_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 教育訓練実施要領書_目的_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 1000);
            ChangedData(true);
        }

        private void 教育訓練実施要領書_目的_Validated(object sender, EventArgs e)
        {

        }

        private void 教育訓練実施要領書_目的_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 教育訓練実施要領書_内容_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 3000);
        }

        private void 教育訓練実施要領書_内容_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角１，５００文字まで入力できます。　■別ウィンドウから入力するときは入力欄をダブルクリックしてください。";
        }

        private void 教育訓練実施要領書_内容_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 教育訓練実施要領書_内容_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 3000);
            ChangedData(true);
        }

        private void 教育訓練実施要領書_内容_Validated(object sender, EventArgs e)
        {

        }

        private void 教育訓練実施要領書_内容_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 教育訓練実施要領書_期待効果_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 1000);
        }

        private void 教育訓練実施要領書_期待効果_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角５００文字まで入力できます。　■別ウィンドウから入力するときは入力欄をダブルクリックしてください。";
        }

        private void 教育訓練実施要領書_期待効果_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 教育訓練実施要領書_期待効果_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 1000);
            ChangedData(true);
        }

        private void 教育訓練実施要領書_期待効果_Validated(object sender, EventArgs e)
        {

        }

        private void 教育訓練実施要領書_期待効果_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        #endregion


        #region 出向依頼書

        private bool 出向依頼書_SetCustomerInfo(string customerCode)
        {
            try
            {
                Connect();

                bool result = false;
                string strKey = "顧客コード='" + customerCode + "' AND 取引開始日 IS NOT NULL";
                string strSQL = "SELECT * FROM V顧客 WHERE " + strKey;

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            出向依頼書_顧客名.Text = reader["顧客名"].ToString() + " " + reader["顧客名2"].ToString();
                            出向依頼書_顧客担当者名.Text = reader["顧客担当者名"].ToString();
                            出向依頼書_顧客電話番号.Text = reader["電話番号1"].ToString() +
                                                    "-" + reader["電話番号2"].ToString() +
                                                    "-" + reader["電話番号3"].ToString();
                            出向依頼書_顧客電話番号.Text = 出向依頼書_顧客電話番号.Text.Replace(" ", "");
                            result = true;
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        private void 出向依頼書_その他_CheckedChanged(object sender, EventArgs e)
        {
            if (出向依頼書_その他.Checked)
            {
                出向依頼書_依頼分類その他.Focus();
            }
        }


        private void 出向依頼書_受付日_DoubleClick(object sender, EventArgs e)
        {
            出向依頼書_受付日選択ボタン_Click(sender, e);
        }

        private void 出向依頼書_受付日_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void 出向依頼書_受付日_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 出向依頼書_受付日_Validated(object sender, EventArgs e)
        {

        }

        private void 出向依頼書_受付日_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 出向依頼書_受付日選択ボタン_Click(object sender, EventArgs e)
        {
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(出向依頼書_受付日.Text))
            {
                dateSelectionForm.args = 出向依頼書_受付日.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK && 出向依頼書_受付日.Enabled && !出向依頼書_受付日.ReadOnly)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                出向依頼書_受付日.Text = selectedDate;
                出向依頼書_受付日.Focus();
                UpdatedControl(出向依頼書_受付日);
            }
        }

        private void 出向依頼書_受付日_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                出向依頼書_受付日選択ボタン_Click(sender, e);
            }
        }

        private void 出向依頼書_出向分類_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void 出向依頼書_出向分類_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ') // スペースキーが押されたかを確認
            {
                if (sender is ComboBox comboBox)
                {
                    comboBox.DroppedDown = true; // コンボボックスのドロップダウンを開く
                    e.Handled = true; // イベントの処理が完了したことを示す
                }
            }
        }

        private void 出向依頼書_出向分類_TextUpdate(object sender, EventArgs e)
        {

        }

        private void 出向依頼書_出向分類_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 出向依頼書_出向分類_Validated(object sender, EventArgs e)
        {

        }

        private void 出向依頼書_出向分類_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 出向依頼書_顧客コード_DoubleClick(object sender, EventArgs e)
        {
            出向依頼書_顧客コード選択ボタン_Click(sender, e);
        }

        private void 出向依頼書_顧客コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                TextBox textBox = (TextBox)sender;
                string formattedCode = textBox.Text.Trim().PadLeft(8, '0');

                if (formattedCode != textBox.Text || string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.Text = formattedCode;
                    出向依頼書_顧客コード_Validated(sender, e);
                }
            }
        }

        private void 出向依頼書_顧客コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                出向依頼書_顧客コード選択ボタン_Click(sender, e);
            }
        }

        private void 出向依頼書_顧客コード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 8);
            ChangedData(true);
        }

        private void 出向依頼書_顧客コード_Validated(object sender, EventArgs e)
        {



        }

        private void 出向依頼書_顧客コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            setflg = true;
            if (IsError(sender as Control) == true) e.Cancel = true;
            setflg = false;
        }

        private F_検索 SearchForm;

        private void 出向依頼書_顧客コード選択ボタン_Click(object sender, EventArgs e)
        {
            SearchForm = new F_検索();
            SearchForm.FilterName = "顧客名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK && 出向依頼書_顧客コード.Enabled && !出向依頼書_顧客コード.ReadOnly)
            {
                string SelectedCode = SearchForm.SelectedCode;

                出向依頼書_顧客コード.Text = SelectedCode;
            }
        }

        private void 出向依頼書_顧客参照ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 出向依頼書_顧客名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 50);
            ChangedData(true);
        }

        private void 出向依頼書_顧客名_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 出向依頼書_顧客名_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 出向依頼書_顧客担当者名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 50);
            ChangedData(true);
        }

        private void 出向依頼書_顧客担当者名_Validated(object sender, EventArgs e)
        {

        }

        private void 出向依頼書_顧客担当者名_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 出向依頼書_顧客電話番号_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 20);
            ChangedData(true);
        }

        private void 出向依頼書_顧客電話番号_Validated(object sender, EventArgs e)
        {

        }

        private void 出向依頼書_顧客電話番号_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 出向依頼書_製品型番_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 50);
            ChangedData(true);
        }

        private void 出向依頼書_製品型番_Validated(object sender, EventArgs e)
        {
        }

        private void 出向依頼書_製品型番_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 出向依頼書_シリアル番号_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 50);
            ChangedData(true);
        }

        private void 出向依頼書_シリアル番号_Validated(object sender, EventArgs e)
        {

        }

        private void 出向依頼書_シリアル番号_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 出向依頼書_依頼分類_Validated(object sender, EventArgs e)
        {

        }

        private void 出向依頼書_依頼分類_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void 出向依頼書_依頼分類その他_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 50);
            ChangedData(true);
        }

        private void 出向依頼書_依頼分類その他_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 出向依頼書_依頼内容_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 4000);
        }

        private void 出向依頼書_依頼内容_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            ChangedData(true);
        }

        private void 出向依頼書_依頼内容_Validated(object sender, EventArgs e)
        {

        }

        private void 出向依頼書_依頼内容_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 出向依頼書_出向日開始_DoubleClick(object sender, EventArgs e)
        {
            出向依頼書_出向日開始選択ボタン_Click(sender, e);
        }

        private void 出向依頼書_出向日開始_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                出向依頼書_出向日開始選択ボタン_Click(sender, e);
            }
        }

        private void 出向依頼書_出向日開始_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(出向依頼書_出向日開始, 出向依頼書_出向日終了, sender as Control);
        }

        private void 出向依頼書_出向日開始_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 出向依頼書_出向日開始_Validated(object sender, EventArgs e)
        {

        }

        private void 出向依頼書_出向日開始_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            ChangedData(true);
        }

        private void 出向依頼書_出向日開始選択ボタン_Click(object sender, EventArgs e)
        {
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(出向依頼書_出向日開始.Text))
            {
                dateSelectionForm.args = 出向依頼書_出向日開始.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK && 出向依頼書_出向日開始.Enabled && !出向依頼書_出向日開始.ReadOnly)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                出向依頼書_出向日開始.Text = selectedDate;
                出向依頼書_出向日開始.Focus();
                UpdatedControl(出向依頼書_出向日開始);
            }
        }

        private void 出向依頼書_出向日終了_DoubleClick(object sender, EventArgs e)
        {
            出向依頼書_出向日終了選択ボタン_Click(sender, e);
        }

        private void 出向依頼書_出向日終了_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                出向依頼書_出向日終了選択ボタン_Click(sender, e);
            }
        }

        private void 出向依頼書_出向日終了_Leave(object sender, EventArgs e)
        {
            FunctionClass.AdjustRange(出向依頼書_出向日開始, 出向依頼書_出向日終了, sender as Control);
        }

        private void 出向依頼書_出向日終了_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 出向依頼書_出向日終了_Validated(object sender, EventArgs e)
        {

        }

        private void 出向依頼書_出向日終了_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 出向依頼書_出向日終了選択ボタン_Click(object sender, EventArgs e)
        {
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(出向依頼書_出向日終了.Text))
            {
                dateSelectionForm.args = 出向依頼書_出向日終了.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK && 出向依頼書_出向日終了.Enabled && !出向依頼書_出向日終了.ReadOnly)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                出向依頼書_出向日終了.Text = selectedDate;
                出向依頼書_出向日終了.Focus();
                UpdatedControl(出向依頼書_出向日終了);
            }
        }

        private void 出向依頼書_出向先会社名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 50);
            ChangedData(true);
        }

        private void 出向依頼書_出向先会社名_Validated(object sender, EventArgs e)
        {

        }

        private void 出向依頼書_出向先会社名_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 出向依頼書_出向先住所_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 50);
            ChangedData(true);
        }

        private void 出向依頼書_出向先住所_Validated(object sender, EventArgs e)
        {

        }

        private void 出向依頼書_出向先住所_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 出向依頼書_出向先電話番号_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 20);
            ChangedData(true);
        }

        private void 出向依頼書_出向先電話番号_Validated(object sender, EventArgs e)
        {

        }

        private void 出向依頼書_出向先電話番号_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 出向依頼書_費用_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ') // スペースキーが押されたかを確認
            {
                if (sender is ComboBox comboBox)
                {
                    comboBox.DroppedDown = true; // コンボボックスのドロップダウンを開く
                    e.Handled = true; // イベントの処理が完了したことを示す
                }
            }
        }

        private void 出向依頼書_費用_TextUpdate(object sender, EventArgs e)
        {

        }

        private void 出向依頼書_費用_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }


        private void 出向依頼書_費用_Validated(object sender, EventArgs e)
        {

        }

        private void 出向依頼書_費用_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        #endregion


        #region 新規販売取引申請書

        public int 新規販売取引申請書_ApplyCustomer(string customerCode)
        {
            try
            {
                Connect();

                int result = 0;
                string strKey = "顧客コード = '" + customerCode + "'";
                string strSQL = "UPDATE M顧客 SET 取引申請日 = getdate() WHERE " + strKey;

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    result = cmd.ExecuteNonQuery();
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.GetType().Name}: {ex.Message}");
                return -1; // エラーが発生した場合、適切なエラーコードを返すか、例外をスローするなど適切な処理を行ってください
            }
        }

        public int 新規販売取引申請書_ApprovalCustomer(string customerCode)
        {
            try
            {
                Connect();

                int result = 0;
                string strKey = "顧客コード ='" + customerCode + "'";
                string strSQL = "UPDATE M顧客 SET 取引開始日 = getdate() WHERE " + strKey;

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    result = cmd.ExecuteNonQuery();
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.GetType().Name}: {ex.Message}");
                return -1; // エラーが発生した場合、適切なエラーコードを返すか、例外をスローするなど適切な処理を行ってください
            }
        }

        private void 新規販売取引申請書_顧客コード_DoubleClick(object sender, EventArgs e)
        {
            新規販売取引申請書_顧客コード検索ボタン_Click(sender, e);
        }

        private void 新規販売取引申請書_顧客コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                ComboBox comboBox = sender as ComboBox;
                if (comboBox != null)
                {
                    string strCode = comboBox.Text.Trim();
                    if (!string.IsNullOrEmpty(strCode))
                    {
                        strCode = strCode.PadLeft(8, '0');
                        if (strCode != comboBox.Text)
                        {
                            comboBox.Text = strCode;
                        }
                    }
                }
            }
        }

        private void 新規販売取引申請書_顧客コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                新規販売取引申請書_顧客コード検索ボタン_Click(sender, e);
            }
        }

        private void 新規販売取引申請書_顧客コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }


        private void 新規販売取引申請書_顧客コード_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■コードを入力するか、右側のボタンを押して顧客を選択します。　■8文字入力可。　■[space]キーで選択できます。";
        }

        private void 新規販売取引申請書_顧客コード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 新規販売取引申請書_顧客コード検索ボタン_Click(object sender, EventArgs e)
        {
            dateSelectionForm = new F_カレンダー();

            if (IsApproved)
            {
                MessageBox.Show("承認後の修正はできません。", "顧客選択", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            SearchForm = new F_検索();
            SearchForm.FilterName = "申請顧客名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK && 新規販売取引申請書_顧客コード.Enabled && !新規販売取引申請書_顧客コード.ReadOnly)
            {
                string SelectedCode = SearchForm.SelectedCode;

                新規販売取引申請書_顧客コード.Text = SelectedCode;
            }
        }

        private void 新規販売取引申請書_顧客コード_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 新規販売取引申請書_顧客コード検索ボタン_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■顧客検索ダイアログを表示";
        }

        private void 新規販売取引申請書_顧客コード検索ボタン_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 新規販売取引申請書_顧客参照ボタン_Click(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■顧客データを参照します。";
        }

        private void 新規販売取引申請書_顧客参照ボタン_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 新規販売取引申請書_新規顧客登録ボタン_Click(object sender, EventArgs e)
        {
            string selectedData = Nz(新規販売取引申請書_顧客コード.Text);

            string trimmedAndReplaced = selectedData.TrimEnd().Replace(" ", "_");

            string replacedServerInstanceName = CommonConstants.ServerInstanceName.Replace(" ", "_");

            string param = $" -sv:{replacedServerInstanceName} -open:saleslistbyparentcustomer, {trimmedAndReplaced},1";
            FunctionClass.GetShell(param);
        }

        #endregion


        #region 是正予防会議通知書

        private void 是正予防会議通知書_受付文書コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                是正予防会議通知書_受付文書コード.Text = FunctionClass.FormatDocumentCode(是正予防会議通知書_受付文書コード.Text);
            }
        }

        private void 是正予防会議通知書_受付文書コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)FunctionClass.ChangeBig((int)e.KeyChar);
        }

        private void 是正予防会議通知書_受付文書コード_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 是正予防会議通知書_受付文書コード_Validated(object sender, EventArgs e)
        {

        }

        private void 是正予防会議通知書_受付文書コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 是正予防会議通知書_顧客コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                ComboBox comboBox = sender as ComboBox;
                if (comboBox != null)
                {
                    string strCode = comboBox.Text.Trim();
                    if (!string.IsNullOrEmpty(strCode))
                    {
                        strCode = strCode.PadLeft(8, '0');
                        if (strCode != comboBox.Text)
                        {
                            comboBox.Text = strCode;

                        }
                    }
                }
            }
        }

        private void 是正予防会議通知書_顧客コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                是正予防会議通知書_顧客コード検索ボタン_Click(sender, e);
            }
        }

        private void 是正予防会議通知書_顧客コード_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 是正予防会議通知書_顧客コード_Validated(object sender, EventArgs e)
        {

        }

        private void 是正予防会議通知書_顧客コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 是正予防会議通知書_顧客コード検索ボタン_Click(object sender, EventArgs e)
        {
            SearchForm = new F_検索();
            SearchForm.FilterName = "顧客名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                string SelectedCode = SearchForm.SelectedCode;

                是正予防会議通知書_顧客コード.Text = SelectedCode;
            }
        }

        private void 是正予防会議通知書_使用者名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 60);
            ChangedData(true);
        }

        private void 是正予防会議通知書_使用者名_Validated(object sender, EventArgs e)
        {

        }

        private void 是正予防会議通知書_使用者名_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 是正予防会議通知書_型番_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 50);
            ChangedData(true);
        }

        private void 是正予防会議通知書_型番_Validated(object sender, EventArgs e)
        {

        }

        private void 是正予防会議通知書_型番_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 是正予防会議通知書_数量_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 是正予防会議通知書_数量_Validated(object sender, EventArgs e)
        {

        }

        private void 是正予防会議通知書_数量_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 是正予防会議通知書_不具合現象_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 4000);

        }

        private void 是正予防会議通知書_不具合現象_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            ChangedData(true);
        }

        private void 是正予防会議通知書_不具合現象_Validated(object sender, EventArgs e)
        {

        }

        private void 是正予防会議通知書_不具合現象_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 是正予防会議通知書_不具合現象_Enter(object sender, EventArgs e)
        {

        }

        private void 是正予防会議通知書_不具合現象_Leave(object sender, EventArgs e)
        {

        }

        #endregion


        #region 是正予防処置報告書

        private void 是正予防処置報告書_受付文書コード_KeyDown(object sender, KeyEventArgs e)
        {
            是正予防処置報告書_受付文書コード.Text = FunctionClass.FormatDocumentCode(是正予防処置報告書_受付文書コード.Text);
        }

        private void 是正予防処置報告書_受付文書コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)FunctionClass.ChangeBig((int)e.KeyChar);
        }

        private void 是正予防処置報告書_受付文書コード_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 是正予防処置報告書_受付文書コード_Validated(object sender, EventArgs e)
        {

        }

        private void 是正予防処置報告書_受付文書コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 是正予防処置報告書_顧客コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                ComboBox comboBox = sender as ComboBox;
                if (comboBox != null)
                {
                    string strCode = comboBox.Text.Trim();
                    if (!string.IsNullOrEmpty(strCode))
                    {
                        strCode = strCode.PadLeft(8, '0');
                        if (strCode != comboBox.Text)
                        {
                            comboBox.Text = strCode;
                        }
                    }
                }
            }
        }

        private void 是正予防処置報告書_顧客コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                是正予防処置報告書_顧客コード検索ボタン_Click(sender, e);
            }
        }

        private void 是正予防処置報告書_顧客コード検索ボタン_Click(object sender, EventArgs e)
        {
            SearchForm = new F_検索();
            SearchForm.FilterName = "顧客名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                string SelectedCode = SearchForm.SelectedCode;

                是正予防処置報告書_顧客コード.Text = SelectedCode;
            }
        }

        private void 是正予防処置報告書_顧客コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 是正予防処置報告書_顧客コード_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 是正予防処置報告書_顧客コード検索ボタン_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■顧客検索ダイアログを表示";
        }

        private void 是正予防処置報告書_顧客コード検索ボタン_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 是正予防処置報告書_使用者名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 60);
            ChangedData(true);
        }

        private void 是正予防処置報告書_使用者名_Validated(object sender, EventArgs e)
        {

        }

        private void 是正予防処置報告書_使用者名_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 是正予防処置報告書_型番_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 50);
            ChangedData(true);
        }

        private void 是正予防処置報告書_型番_Validated(object sender, EventArgs e)
        {

        }

        private void 是正予防処置報告書_型番_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 是正予防処置報告書_数量_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 是正予防処置報告書_数量_Validated(object sender, EventArgs e)
        {

        }

        private void 是正予防処置報告書_数量_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 是正予防処置報告書_議事録_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 4000);
        }

        private void 是正予防処置報告書_議事録_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角２，０００文字まで入力できます。　■別ウィンドウから入力するときは入力欄をダブルクリックしてください。";
        }

        private void 是正予防処置報告書_議事録_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 是正予防処置報告書_議事録_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            ChangedData(true);
        }

        private void 是正予防処置報告書_議事録_Validated(object sender, EventArgs e)
        {

        }

        private void 是正予防処置報告書_議事録_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 是正予防処置報告書_回答ボタン_Click(object sender, EventArgs e)
        {

        }

        #endregion


        #region 是正予防処置報告書_環境

        private void 是正予防処置報告書_環境_発生日_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 60);
            ChangedData(true);
        }

        private void 是正予防処置報告書_環境_発生日_Validated(object sender, EventArgs e)
        {

        }

        private void 是正予防処置報告書_環境_発生日_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 是正予防処置報告書_環境_発生日選択ボタン_Click(object sender, EventArgs e)
        {
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(是正予防処置報告書_環境_発生日.Text))
            {
                dateSelectionForm.args = 是正予防処置報告書_環境_発生日.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK && 是正予防処置報告書_環境_発生日.Enabled && !是正予防処置報告書_環境_発生日.ReadOnly)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                是正予防処置報告書_環境_発生日.Text = selectedDate;
                是正予防処置報告書_環境_発生日.Focus();
                UpdatedControl(是正予防処置報告書_環境_発生日);
            }
        }

        private void 是正予防処置報告書_環境_発生日選択ボタン_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■カレンダーの表示";
        }

        private void 是正予防処置報告書_環境_発生日選択ボタン_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 是正予防処置報告書_環境_発生所在地_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角100文字まで入力できます。";
        }

        private void 是正予防処置報告書_環境_発生所在地_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 是正予防処置報告書_環境_発生所在地_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 200);
            ChangedData(true);
        }

        private void 是正予防処置報告書_環境_発生所在地_Validated(object sender, EventArgs e)
        {

        }

        private void 是正予防処置報告書_環境_発生所在地_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 是正予防処置報告書_環境_議事録_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 4000);
        }

        private void 是正予防処置報告書_環境_議事録_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角２，０００文字まで入力できます。　■別ウィンドウから入力するときは入力欄をダブルクリックしてください。";
        }

        private void 是正予防処置報告書_環境_議事録_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 是正予防処置報告書_環境_議事録_TextChanged(object sender, EventArgs e)
        {


            FunctionClass.LimitText(sender as Control, 4000);
            ChangedData(true);
        }

        private void 是正予防処置報告書_環境_議事録_Validated(object sender, EventArgs e)
        {

        }

        private void 是正予防処置報告書_環境_議事録_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 是正予防処置報告書_環境_回答ボタン_Click(object sender, EventArgs e)
        {

        }

        #endregion


        #region 製品企画書

        private void 製品企画書_品名_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 製品企画書_品名_Validated(object sender, EventArgs e)
        {

        }

        private void 製品企画書_品名_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 製品企画書_型番_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 製品企画書_型番_Validated(object sender, EventArgs e)
        {

        }

        private void 製品企画書_型番_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 製品企画書_売上区分コード_TextUpdate(object sender, EventArgs e)
        {

        }

        private void 製品企画書_売上区分コード_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 製品企画書_売上区分コード_Validated(object sender, EventArgs e)
        {

        }

        private void 製品企画書_売上区分コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 製品企画書_顧客コード_DoubleClick(object sender, EventArgs e)
        {
            製品企画書_顧客コード検索ボタン_Click(sender, e);
        }

        private void 製品企画書_顧客コード_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■企画に関係する顧客の顧客コードを入力します。";
        }

        private void 製品企画書_顧客コード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 製品企画書_顧客コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                ComboBox comboBox = sender as ComboBox;
                if (comboBox != null)
                {
                    string strCode = comboBox.Text.Trim();
                    if (!string.IsNullOrEmpty(strCode))
                    {
                        strCode = strCode.PadLeft(8, '0');
                        if (strCode != comboBox.Text)
                        {
                            comboBox.Text = strCode;
                        }
                    }
                }
            }
        }

        private void 製品企画書_顧客コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                製品企画書_顧客コード検索ボタン_Click(sender, e);
            }
        }

        private void 製品企画書_顧客コード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 8);
            ChangedData(true);
        }

        private void 製品企画書_顧客コード_Validated(object sender, EventArgs e)
        {

        }

        private void 製品企画書_顧客コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 製品企画書_顧客コード検索ボタン_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■顧客検索ダイアログを表示。";
        }

        private void 製品企画書_顧客コード検索ボタン_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 製品企画書_顧客コード検索ボタン_Click(object sender, EventArgs e)
        {
            if (IsApproved)
            {
                MessageBox.Show("承認後の修正はできません", "顧客検索", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {

                SearchForm = new F_検索();
                SearchForm.FilterName = "顧客名フリガナ";
                if (SearchForm.ShowDialog() == DialogResult.OK)
                {
                    string SelectedCode = SearchForm.SelectedCode;

                    製品企画書_顧客コード.Text = SelectedCode;
                }
            }
        }

        private void 製品企画書_年間販売数量_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 製品企画書_年間販売数量_Validated(object sender, EventArgs e)
        {

        }

        private void 製品企画書_年間販売数量_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 製品企画書_標準価格_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 製品企画書_標準価格_Validated(object sender, EventArgs e)
        {

        }

        private void 製品企画書_標準価格_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 製品企画書_自社開発費比率_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 製品企画書_自社開発費比率_Validated(object sender, EventArgs e)
        {

        }

        private void 製品企画書_自社開発費比率_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 製品企画書_自社開発費比率_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■自社対他社の開発費分担における自社比率を入力。";
        }

        private void 製品企画書_自社開発費比率_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 製品企画書_競合価格_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 製品企画書_競合価格_Validated(object sender, EventArgs e)
        {

        }

        private void 製品企画書_競合価格_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 製品企画書_競合メーカー_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 製品企画書_競合メーカー_Validated(object sender, EventArgs e)
        {

        }

        private void 製品企画書_競合メーカー_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 製品企画書_競合製品型番_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 製品企画書_競合製品型番_Validated(object sender, EventArgs e)
        {

        }

        private void 製品企画書_競合製品型番_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 製品企画書_発売予定日_DoubleClick(object sender, EventArgs e)
        {
            製品企画書_発売予定日選択ボタン_Click(sender, e);
        }

        private void 製品企画書_発売予定日_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                製品企画書_発売予定日選択ボタン_Click(sender, e);
            }
        }

        private void 製品企画書_発売予定日_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 製品企画書_発売予定日_Validated(object sender, EventArgs e)
        {

        }

        private void 製品企画書_発売予定日_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 製品企画書_発売予定日選択ボタン_Click(object sender, EventArgs e)
        {
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(製品企画書_発売予定日.Text))
            {
                dateSelectionForm.args = 製品企画書_発売予定日.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK && 製品企画書_発売予定日.Enabled && !製品企画書_発売予定日.ReadOnly)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                製品企画書_発売予定日.Text = selectedDate;
                製品企画書_発売予定日.Focus();
                UpdatedControl(製品企画書_発売予定日);
            }
        }

        private void 製品企画書_会議開催日_DoubleClick(object sender, EventArgs e)
        {
            製品企画書_会議開催日選択ボタン_Click(sender, e);
        }

        private void 製品企画書_会議開催日_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                製品企画書_会議開催日選択ボタン_Click(sender, e);
            }
        }

        private void 製品企画書_会議開催日_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 製品企画書_会議開催日_Validated(object sender, EventArgs e)
        {

        }

        private void 製品企画書_会議開催日_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 製品企画書_会議開催日選択ボタン_Click(object sender, EventArgs e)
        {
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(製品企画書_会議開催日.Text))
            {
                dateSelectionForm.args = 製品企画書_会議開催日.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK && 製品企画書_会議開催日.Enabled && !製品企画書_会議開催日.ReadOnly)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                製品企画書_会議開催日.Text = selectedDate;
                製品企画書_会議開催日.Focus();
                UpdatedControl(製品企画書_会議開催日);
            }
        }

        private void 製品企画書_開発目的_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 1000);
        }

        private void 製品企画書_開発目的_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■ズームするには入力欄をダブルクリックしてください。";
        }

        private void 製品企画書_開発目的_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 製品企画書_開発目的_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 1000);
            ChangedData(true);
        }

        private void 製品企画書_開発目的_Validated(object sender, EventArgs e)
        {

        }

        private void 製品企画書_開発目的_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 製品企画書_製品概要_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 1000);
        }

        private void 製品企画書_製品概要_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 1000);
            ChangedData(true);
        }

        private void 製品企画書_製品概要_Validated(object sender, EventArgs e)
        {

        }

        private void 製品企画書_製品概要_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 製品企画書_製品概要_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■ズームするには入力欄をダブルクリックしてください。";
        }

        private void 製品企画書_製品概要_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 製品企画書_要求事項_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 2000);
        }

        private void 製品企画書_要求事項_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 2000);
            ChangedData(true);
        }

        private void 製品企画書_要求事項_Validated(object sender, EventArgs e)
        {

        }

        private void 製品企画書_要求事項_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 製品企画書_要求事項_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■ズームするには入力欄をダブルクリックしてください。";
        }

        private void 製品企画書_要求事項_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        #endregion


        #region 設計審査会議事録

        private void 設計審査会議事録_開催日_DoubleClick(object sender, EventArgs e)
        {
            設計審査会議事録_開催日選択ボタン_Click(sender, e);
        }

        private void 設計審査会議事録_開催日_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                設計審査会議事録_開催日選択ボタン_Click(sender, e);
            }
        }

        private void 設計審査会議事録_開催日_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■入力欄をダブルクリックすると、日付をカレンダーから入力することができます。";
        }

        private void 設計審査会議事録_開催日_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 設計審査会議事録_開催日_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 設計審査会議事録_開催日_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 設計審査会議事録_開催日選択ボタン_Click(object sender, EventArgs e)
        {
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(設計審査会議事録_開催日.Text))
            {
                dateSelectionForm.args = 設計審査会議事録_開催日.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK && 設計審査会議事録_開催日.Enabled && !設計審査会議事録_開催日.ReadOnly)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                設計審査会議事録_開催日.Text = selectedDate;
                設計審査会議事録_開催日.Focus();
                UpdatedControl(設計審査会議事録_開催日);
            }
        }

        private void 設計審査会議事録_開催場所_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角５０文字まで入力できます。";
        }

        private void 設計審査会議事録_開催場所_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 設計審査会議事録_開催場所_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 100);
            ChangedData(true);
        }

        private void 設計審査会議事録_開催場所_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 設計審査会議事録_参加者_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角１００文字まで入力できます。";
        }

        private void 設計審査会議事録_参加者_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 設計審査会議事録_参加者_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 2);
            ChangedData(true);
        }

        private void 設計審査会議事録_参加者_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 設計審査会議事録_企画書との相異_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 4000);
        }

        private void 設計審査会議事録_企画書との相異_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角２，０００文字まで入力できます。";
        }

        private void 設計審査会議事録_企画書との相異_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 設計審査会議事録_企画書との相異_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            ChangedData(true);
        }

        private void 設計審査会議事録_計画書との相異_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 4000);
        }

        private void 設計審査会議事録_計画書との相異_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角２，０００文字まで入力できます。";
        }

        private void 設計審査会議事録_計画書との相異_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 設計審査会議事録_計画書との相異_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            ChangedData(true);
        }

        private void 設計審査会議事録_構想資料との相異_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 4000);
        }

        private void 設計審査会議事録_構想資料との相異_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角２，０００文字まで入力できます。";
        }

        private void 設計審査会議事録_構想資料との相異_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 設計審査会議事録_構想資料との相異_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            ChangedData(true);
        }

        private void 設計審査会議事録_仕様書の確認_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 4000);
        }

        private void 設計審査会議事録_仕様書の確認_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角２，０００文字まで入力できます。";
        }

        private void 設計審査会議事録_仕様書の確認_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 設計審査会議事録_仕様書の確認_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            ChangedData(true);
        }

        private void 設計審査会議事録_改善点_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 4000);
        }

        private void 設計審査会議事録_改善点_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角２，０００文字まで入力できます。";
        }

        private void 設計審査会議事録_改善点_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 設計審査会議事録_改善点_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            ChangedData(true);
        }

        private void 設計審査会議事録_要望_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 4000);
        }

        private void 設計審査会議事録_要望_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角２，０００文字まで入力できます。";
        }

        private void 設計審査会議事録_要望_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 設計審査会議事録_要望_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            ChangedData(true);
        }

        private void 設計審査会議事録_結論_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 4000);
        }

        private void 設計審査会議事録_結論_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角２，０００文字まで入力できます。";
        }

        private void 設計審査会議事録_結論_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 設計審査会議事録_結論_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            ChangedData(true);
        }

        #endregion


        #region 設計製作依頼書

        private void 設計製作依頼書_検討依頼書コード_KeyDown(object sender, KeyEventArgs e)
        {
            設計製作依頼書_検討依頼書コード.Text = FunctionClass.FormatDocumentCode(設計製作依頼書_検討依頼書コード.Text);
        }

        private void 設計製作依頼書_検討依頼書コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)FunctionClass.ChangeBig((int)e.KeyChar);
        }

        private void 設計製作依頼書_検討依頼書コード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 11);
            ChangedData(true);
        }

        private void 設計製作依頼書_検討依頼書コード_Validated(object sender, EventArgs e)
        {

        }

        private void 設計製作依頼書_検討依頼書コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 設計製作依頼書_リンク1ボタン_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(設計製作依頼書_検討依頼書コード.Text)) return;

            F_文書 targetform = new F_文書();

            targetform.args = 設計製作依頼書_検討依頼書コード.Text + ",1";
            targetform.ShowDialog();
        }

        private void 設計製作依頼書_リンク1ボタン_Enter(object sender, EventArgs e)
        {

        }

        private void 設計製作依頼書_リンク1ボタン_Leave(object sender, EventArgs e)
        {

        }

        private void 設計製作依頼書_リンク2ボタン_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(設計製作依頼書_受注コード.Text)) return;

            F_受注 targetform = new F_受注();

            targetform.varOpenArgs = 設計製作依頼書_受注コード.Text;
            targetform.ShowDialog();
        }

        private void 設計製作依頼書_リンク2ボタン_Enter(object sender, EventArgs e)
        {

        }

        private void 設計製作依頼書_リンク2ボタン_Leave(object sender, EventArgs e)
        {

        }

        private void 設計製作依頼書_受注コード_DoubleClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(設計製作依頼書_受注コード.Text)) return;

            F_受注 targetform = new F_受注();

            targetform.varOpenArgs = 設計製作依頼書_受注コード.Text;
            targetform.ShowDialog();
        }

        private void 設計製作依頼書_受注コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                設計製作依頼書_受注コード.Text = FunctionClass.FormatCode("A", 設計製作依頼書_受注コード.Text);
            }
        }

        private void 設計製作依頼書_受注コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)FunctionClass.ChangeBig((int)e.KeyChar);

        }

        private void 設計製作依頼書_受注コード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 9);
            ChangedData(true);
        }

        private void 設計製作依頼書_受注コード_Validated(object sender, EventArgs e)
        {

        }

        private void 設計製作依頼書_受注コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 設計製作依頼書_顧客コード_DoubleClick(object sender, EventArgs e)
        {
            if (IsApproved)
            {
                MessageBox.Show("承認後の修正はできません", "顧客検索", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {

                SearchForm = new F_検索();
                SearchForm.FilterName = "顧客名フリガナ";
                if (SearchForm.ShowDialog() == DialogResult.OK)
                {
                    string SelectedCode = SearchForm.SelectedCode;

                    設計製作依頼書_顧客コード.Text = SelectedCode;
                }
            }
        }

        private void 設計製作依頼書_顧客コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                TextBox textBox = (TextBox)sender;
                string formattedCode = textBox.Text.Trim().PadLeft(8, '0');

                if (formattedCode != textBox.Text || string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.Text = formattedCode;
                }
            }
        }

        private void 設計製作依頼書_顧客コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                設計製作依頼書_顧客コード検索ボタン_Click(sender, e);
            }
        }

        private void 設計製作依頼書_顧客コード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 8);
            ChangedData(true);
        }

        private void 設計製作依頼書_顧客コード検索ボタン_Click(object sender, EventArgs e)
        {
            if (IsApproved)
            {
                MessageBox.Show("承認後の修正はできません", "顧客検索", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
            else
            {

                SearchForm = new F_検索();
                SearchForm.FilterName = "顧客名フリガナ";
                if (SearchForm.ShowDialog() == DialogResult.OK)
                {
                    string SelectedCode = SearchForm.SelectedCode;

                    設計製作依頼書_顧客コード.Text = SelectedCode;
                }
            }
        }

        private void 設計製作依頼書_顧客コード_Validated(object sender, EventArgs e)
        {

        }

        private void 設計製作依頼書_顧客コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 設計製作依頼書_客先担当者名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 80);
            ChangedData(true);
        }

        private void 設計製作依頼書_客先担当者名_Validated(object sender, EventArgs e)
        {

        }

        private void 設計製作依頼書_客先担当者名_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 設計製作依頼書_型番_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 100);
            ChangedData(true);
        }

        private void 設計製作依頼書_型番_Validated(object sender, EventArgs e)
        {

        }

        private void 設計製作依頼書_型番_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 設計製作依頼書_標準価格_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 設計製作依頼書_標準価格_Validated(object sender, EventArgs e)
        {

        }

        private void 設計製作依頼書_標準価格_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 設計製作依頼書_数量_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 設計製作依頼書_数量_Validated(object sender, EventArgs e)
        {

        }

        private void 設計製作依頼書_数量_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 設計製作依頼書_依頼内容_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 4000);
        }

        private void 設計製作依頼書_依頼内容_Enter(object sender, EventArgs e)
        {

        }

        private void 設計製作依頼書_依頼内容_Leave(object sender, EventArgs e)
        {

        }

        private void 設計製作依頼書_依頼内容_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            ChangedData(true);
        }

        private void 設計製作依頼書_依頼内容_Validated(object sender, EventArgs e)
        {

        }

        private void 設計製作依頼書_依頼内容_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 設計製作依頼書_付属書類指定_Validated(object sender, EventArgs e)
        {

        }

        private void 設計製作依頼書_付属書類指定_CheckedChanged(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 設計製作依頼書_付属書類指定_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 設計製作依頼書_付属文書1_Validated(object sender, EventArgs e)
        {

        }

        private void 設計製作依頼書_付属文書2_Validated(object sender, EventArgs e)
        {

        }

        private void 設計製作依頼書_付属文書3_Validated(object sender, EventArgs e)
        {

        }

        private void 設計製作依頼書_付属文書3_CheckedChanged(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
            ChangedData(true);
        }

        private void 設計製作依頼書_付属文書2_CheckedChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 設計製作依頼書_付属文書1_CheckedChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 設計製作依頼書_その他文書名_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 設計製作依頼書_その他文書名_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 設計製作依頼書_その他文書名_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        #endregion


        #region 設備購買申請書


        private void 設備購買申請書_購買予定日_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                設備購買申請書_購買予定日選択ボタン_Click(sender, e);
            }
        }

        private void 設備購買申請書_購買予定日_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 設備購買申請書_購買予定日_Validated(object sender, EventArgs e)
        {

        }

        private void 設備購買申請書_購買予定日_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 設備購買申請書_購買予定日選択ボタン_Click(object sender, EventArgs e)
        {
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(設備購買申請書_購買予定日.Text))
            {
                dateSelectionForm.args = 設備購買申請書_購買予定日.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK && 設備購買申請書_購買予定日.Enabled && !設備購買申請書_購買予定日.ReadOnly)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                設備購買申請書_購買予定日.Text = selectedDate;
                設備購買申請書_購買予定日.Focus();
                UpdatedControl(設備購買申請書_購買予定日);
            }
        }

        private void 設備購買申請書_品名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 50);
            ChangedData(true);
        }

        private void 設備購買申請書_品名_Validated(object sender, EventArgs e)
        {

        }

        private void 設備購買申請書_品名_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 設備購買申請書_型番_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 50);
            ChangedData(true);
        }

        private void 設備購買申請書_型番_Validated(object sender, EventArgs e)
        {

        }

        private void 設備購買申請書_型番_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 設備購買申請書_メーカー名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 50);
            ChangedData(true);
        }

        private void 設備購買申請書_メーカー名_Validated(object sender, EventArgs e)
        {

        }

        private void 設備購買申請書_メーカー名_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 設備購買申請書_仕入先名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 50);
            ChangedData(true);
        }

        private void 設備購買申請書_仕入先名_Validated(object sender, EventArgs e)
        {

        }

        private void 設備購買申請書_仕入先名_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 設備購買申請書_数量_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 設備購買申請書_数量_Validated(object sender, EventArgs e)
        {

        }

        private void 設備購買申請書_数量_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 設備購買申請書_標準価格_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 設備購買申請書_標準価格_Validated(object sender, EventArgs e)
        {

        }

        private void 設備購買申請書_標準価格_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 設備購買申請書_単価_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 設備購買申請書_単価_Validated(object sender, EventArgs e)
        {

        }

        private void 設備購買申請書_単価_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 設備購買申請書_支払方法コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 設備購買申請書_支払方法コード_Validated(object sender, EventArgs e)
        {

        }

        private void 設備購買申請書_支払方法コード_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 設備購買申請書_使用部署_Enter(object sender, EventArgs e)
        {

        }

        private void 設備購買申請書_使用部署_Leave(object sender, EventArgs e)
        {

        }

        private void 設備購買申請書_使用部署_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 60);
            ChangedData(true);
        }

        private void 設備購買申請書_使用部署_Validated(object sender, EventArgs e)
        {

        }

        private void 設備購買申請書_使用部署_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 設備購買申請書_用途_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 4000);
        }

        private void 設備購買申請書_用途_Enter(object sender, EventArgs e)
        {

        }

        private void 設備購買申請書_用途_Leave(object sender, EventArgs e)
        {

        }

        private void 設備購買申請書_用途_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            ChangedData(true);
        }

        private void 設備購買申請書_用途_Validated(object sender, EventArgs e)
        {

        }

        private void 設備購買申請書_用途_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 設備購買申請書_購入理由_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 4000);
        }

        private void 設備購買申請書_購入理由_Enter(object sender, EventArgs e)
        {

        }

        private void 設備購買申請書_購入理由_Leave(object sender, EventArgs e)
        {

        }

        private void 設備購買申請書_購入理由_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            ChangedData(true);
        }

        private void 設備購買申請書_購入理由_Validated(object sender, EventArgs e)
        {

        }

        private void 設備購買申請書_購入理由_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        #endregion


        #region 年間教育計画表

        public int 年間教育計画表_Approval(string documentCode, int edition, string approvalUser)
        {
            try
            {
                Connect();

                int result = 0;
                string strKey = $"文書コード = '{documentCode}' and 版数 = {edition}";
                string strSQL = $"UPDATE T年間教育計画 SET 承認者コード = '{approvalUser}' WHERE {strKey}";

                using (SqlCommand cmd = new SqlCommand(strSQL, cn))
                {
                    result = cmd.ExecuteNonQuery();
                }

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{ex.GetType().Name}: {ex.Message}");
                return -1; // エラーが発生した場合、適切なエラーコードを返すか、例外をスローするなど適切な処理を行ってください
            }
        }

        private void 年間教育計画表_教育目的_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 400);
            ChangedData(true);
        }

        private void 年間教育計画表_教育目的_Validated(object sender, EventArgs e)
        {

        }

        private void 年間教育計画表_教育目的_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 年間教育計画表_年間教育計画ボタン_Click(object sender, EventArgs e)
        {
            F_年間教育計画 targetform = new F_年間教育計画();

            targetform.args = CurrentCode + "," + CurrentEdition;
            targetform.ShowDialog();
        }

        #endregion


        #region 非該当証明発行依頼書
        private void 非該当証明発行依頼書_受注コード_DoubleClick(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(非該当証明発行依頼書_受注コード.Text)) return;

            F_受注 targetform = new F_受注();

            targetform.varOpenArgs = 非該当証明発行依頼書_受注コード.Text;
            targetform.ShowDialog();
        }

        private void 非該当証明発行依頼書_受注コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                非該当証明発行依頼書_受注コード.Text = FunctionClass.FormatCode("A", 非該当証明発行依頼書_受注コード.Text);
            }
        }

        private void 非該当証明発行依頼書_受注コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)FunctionClass.ChangeBig((int)e.KeyChar);

        }

        private void 非該当証明発行依頼書_受注コード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 9);
            ChangedData(true);
        }



        private void 非該当証明発行依頼書_受注コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 非該当証明発行依頼書_受注コード_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■関連する受注コードを入力します。　■簡易入力ができます。　■入力されているコードの受注データを参照するには入力欄をダブルクリックしてください。";
        }

        private void 非該当証明発行依頼書_受注コード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }


        private void 非該当証明発行依頼書_受注コード_Validated(object sender, EventArgs e)
        {

        }



        private void 非該当証明発行依頼書_受注リンクボタン_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■対象コードの情報を表示。版が登録されている情報は最新のものを表示。";
        }

        private void 非該当証明発行依頼書_受注リンクボタン_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 非該当証明発行依頼書_受注リンクボタン_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(非該当証明発行依頼書_受注コード.Text)) return;

            F_受注 targetform = new F_受注();

            targetform.varOpenArgs = 非該当証明発行依頼書_受注コード.Text;
            targetform.ShowDialog();
        }

        private void 非該当証明発行依頼書_顧客名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 48);
            ChangedData(true);
        }

        private void 非該当証明発行依頼書_顧客名_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 非該当証明発行依頼書_代理店名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 48);
            ChangedData(true);
        }

        private void 非該当証明発行依頼書_代理店名_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 非該当証明発行依頼書_品名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 48);
            ChangedData(true);
        }

        private void 非該当証明発行依頼書_品名_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 非該当証明発行依頼書_型番_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 48);
            ChangedData(true);
        }

        private void 非該当証明発行依頼書_型番_Validated(object sender, EventArgs e)
        {

        }

        private void 非該当証明発行依頼書_型番_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 非該当証明発行依頼書_発行日_DoubleClick(object sender, EventArgs e)
        {
            非該当証明発行依頼書_発行日選択ボタン_Click(sender, e);
        }

        private void 非該当証明発行依頼書_発行日_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                非該当証明発行依頼書_発行日選択ボタン_Click(sender, e);
            }
        }

        private void 非該当証明発行依頼書_発行日_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 10);
            ChangedData(true);
        }

        private void 非該当証明発行依頼書_発行日_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 非該当証明発行依頼書_発行日選択ボタン_Click(object sender, EventArgs e)
        {
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(非該当証明発行依頼書_発行日.Text))
            {
                dateSelectionForm.args = 非該当証明発行依頼書_発行日.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK && 非該当証明発行依頼書_発行日.Enabled && !非該当証明発行依頼書_発行日.ReadOnly)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                非該当証明発行依頼書_発行日.Text = selectedDate;
                非該当証明発行依頼書_発行日.Focus();
                UpdatedControl(非該当証明発行依頼書_発行日);
            }
        }

        private void 非該当証明発行依頼書_発行部数_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 7);
            ChangedData(true);
        }

        private void 非該当証明発行依頼書_発行部数_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 非該当証明発行依頼書_輸出先国名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 48);
            ChangedData(true);
        }

        private void 非該当証明発行依頼書_輸出先国名_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 非該当証明発行依頼書_郵送方法コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 非該当証明発行依頼書_郵送方法コード_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 非該当証明発行依頼書_郵送日_DoubleClick(object sender, EventArgs e)
        {
            非該当証明発行依頼書_郵送日選択ボタン_Click(sender, e);
        }

        private void 非該当証明発行依頼書_郵送日_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                非該当証明発行依頼書_郵送日選択ボタン_Click(sender, e);
            }
        }

        private void 非該当証明発行依頼書_郵送日_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 非該当証明発行依頼書_郵送日_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 非該当証明発行依頼書_郵送日選択ボタン_Click(object sender, EventArgs e)
        {
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(非該当証明発行依頼書_郵送日.Text))
            {
                dateSelectionForm.args = 非該当証明発行依頼書_郵送日.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK && 非該当証明発行依頼書_郵送日.Enabled && !非該当証明発行依頼書_郵送日.ReadOnly)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                非該当証明発行依頼書_郵送日.Text = selectedDate;
                非該当証明発行依頼書_郵送日.Focus();
                UpdatedControl(非該当証明発行依頼書_郵送日);
            }
        }

        private void 非該当証明発行依頼書_郵送先郵便番号_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 8);
            ChangedData(true);
        }

        private void 非該当証明発行依頼書_郵送先郵便番号_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 非該当証明発行依頼書_郵送先住所1_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 48);
            ChangedData(true);
        }

        private void 非該当証明発行依頼書_郵送先住所1_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 非該当証明発行依頼書_郵送先住所2_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 48);
            ChangedData(true);
        }

        private void 非該当証明発行依頼書_郵送先住所2_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 非該当証明発行依頼書_郵送先名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 48);
            ChangedData(true);
        }

        private void 非該当証明発行依頼書_郵送先名_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 非該当証明発行依頼書_郵送先担当者名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 48);
            ChangedData(true);
        }

        private void 非該当証明発行依頼書_郵送先担当者名_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 非該当証明発行依頼書_備考_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 1000);
        }

        private void 非該当証明発行依頼書_備考_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角５００文字まで入力できます。";
        }

        private void 非該当証明発行依頼書_備考_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 非該当証明発行依頼書_備考_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 1000);
            ChangedData(true);
        }

        #endregion


        #region 品質異常報告書

        private void 品質異常報告書_発生日_DoubleClick(object sender, EventArgs e)
        {
            品質異常報告書_発生日選択ボタン_Click(sender, e);
        }

        private void 品質異常報告書_発生日_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                品質異常報告書_発生日選択ボタン_Click(sender, e);
            }
        }

        private void 品質異常報告書_発生日_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 10);
        }

        private void 品質異常報告書_発生日_Validated(object sender, EventArgs e)
        {

        }

        private void 品質異常報告書_発生日_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 品質異常報告書_発生日選択ボタン_Click(object sender, EventArgs e)
        {
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(品質異常報告書_発生日.Text))
            {
                dateSelectionForm.args = 品質異常報告書_発生日.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK && 品質異常報告書_発生日.Enabled && !品質異常報告書_発生日.ReadOnly)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                品質異常報告書_発生日.Text = selectedDate;
                品質異常報告書_発生日.Focus();
                UpdatedControl(品質異常報告書_発生日);
            }
        }

        private void 品質異常報告書_発生場所_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角２５文字まで入力できます。";
        }

        private void 品質異常報告書_発生場所_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 品質異常報告書_発生場所_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 50);
            ChangedData(true);
        }

        private void 品質異常報告書_発生場所_Validated(object sender, EventArgs e)
        {

        }

        private void 品質異常報告書_発生場所_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 品質異常報告書_型番_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角２５文字まで入力できます。";
        }

        private void 品質異常報告書_型番_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 品質異常報告書_型番_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 50);
            ChangedData(true);
        }

        private void 品質異常報告書_型番_Validated(object sender, EventArgs e)
        {

        }

        private void 品質異常報告書_型番_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 品質異常報告書_異常内容_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 4000);
        }

        private void 品質異常報告書_異常内容_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角２，０００文字まで入力できます。　■別ウィンドウで表示するには入力欄をダブルクリックします。";
        }

        private void 品質異常報告書_異常内容_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 品質異常報告書_異常内容_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            ChangedData(true);
        }

        private void 品質異常報告書_異常内容_Validated(object sender, EventArgs e)
        {

        }

        private void 品質異常報告書_異常内容_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 品質異常報告書_回答書ボタン_Click(object sender, EventArgs e)
        {

        }

        #endregion


        #region 不具合調査修理依頼書

        private void 不具合調査修理依頼書_受注コード_DoubleClick(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(不具合調査修理依頼書_受注コード.Text))
            {
                F_受注 targetform = new F_受注();

                targetform.varOpenArgs = 不具合調査修理依頼書_受注コード.Text;
                targetform.ShowDialog();
            }
            else
            {
                F_受注管理 targetform = new F_受注管理();

                targetform.ShowDialog();
            }
        }

        private void 不具合調査修理依頼書_受注コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                不具合調査修理依頼書_受注コード.Text = FunctionClass.FormatCode("A", 不具合調査修理依頼書_受注コード.Text);
            }
        }

        private void 不具合調査修理依頼書_受注コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            e.KeyChar = (char)FunctionClass.ChangeBig((int)e.KeyChar);

        }

        private void 不具合調査修理依頼書_受注コード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 9);
            ChangedData(true);
        }

        private void 不具合調査修理依頼書_受注コード_Validated(object sender, EventArgs e)
        {

        }

        private void 不具合調査修理依頼書_受注コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 不具合調査修理依頼書_受注コード参照ボタン_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(不具合調査修理依頼書_受注コード.Text))
            {
                F_受注 targetform = new F_受注();

                targetform.varOpenArgs = 不具合調査修理依頼書_受注コード.Text;
                targetform.ShowDialog();
            }
            else
            {
                F_受注管理 targetform = new F_受注管理();

                targetform.ShowDialog();
            }

            
        }

        private void 不具合調査修理依頼書_受注コード参照ボタン_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■対象コードの情報を表示。版が登録されている情報は最新のものを表示。";
        }

        private void 不具合調査修理依頼書_受注コード参照ボタン_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 不具合調査修理依頼書_受付日_DoubleClick(object sender, EventArgs e)
        {
            不具合調査修理依頼書_受付日選択ボタン_Click(sender, e);
        }

        private void 不具合調査修理依頼書_受付日_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                不具合調査修理依頼書_受付日選択ボタン_Click(sender, e);
            }
        }

        private void 不具合調査修理依頼書_受付日_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 不具合調査修理依頼書_受付日_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 不具合調査修理依頼書_受付日選択ボタン_Click(object sender, EventArgs e)
        {
 
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(不具合調査修理依頼書_受付日.Text))
            {
                dateSelectionForm.args = 不具合調査修理依頼書_受付日.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK && 不具合調査修理依頼書_受付日.Enabled && !不具合調査修理依頼書_受付日.ReadOnly)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                不具合調査修理依頼書_受付日.Text = selectedDate;
                不具合調査修理依頼書_受付日.Focus();
                UpdatedControl(不具合調査修理依頼書_受付日);
            }
        }

     


        private void 不具合調査修理依頼書_顧客コード_DoubleClick(object sender, EventArgs e)
        {
            不具合調査修理依頼書_顧客コード選択ボタン_Click(sender, e);
        }

        private void 不具合調査修理依頼書_顧客コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                TextBox textBox = (TextBox)sender;
                string formattedCode = textBox.Text.Trim().PadLeft(8, '0');

                if (formattedCode != textBox.Text || string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.Text = formattedCode;
  
                }
            }
        }

        private void 不具合調査修理依頼書_顧客コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                不具合調査修理依頼書_顧客コード選択ボタン_Click(sender, e);
            }
        }

        private void 不具合調査修理依頼書_顧客コード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 8);
            ChangedData(true);
        }

        private void 不具合調査修理依頼書_顧客コード_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 不具合調査修理依頼書_顧客コード選択ボタン_Click(object sender, EventArgs e)
        {
            SearchForm = new F_検索();
            SearchForm.FilterName = "顧客名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                string SelectedCode = SearchForm.SelectedCode;

                不具合調査修理依頼書_顧客コード.Text = SelectedCode;
            }
        }

        private void 不具合調査修理依頼書_顧客コード選択ボタン_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■顧客検索ダイアログを表示。";
        }

        private void 不具合調査修理依頼書_顧客コード選択ボタン_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 不具合調査修理依頼書_顧客参照ボタン_Click(object sender, EventArgs e)
        {

        }

        private void 不具合調査修理依頼書_顧客参照ボタン_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■顧客データを参照します。";
        }

        private void 不具合調査修理依頼書_顧客参照ボタン_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 不具合調査修理依頼書_依頼分類_Validated(object sender, EventArgs e)
        {

        }

        private void 不具合調査修理依頼書_依頼分類_Validating(object sender, CancelEventArgs e)
        {

        }

        private void 不具合調査修理依頼書_依頼分類その他_Enter(object sender, EventArgs e)
        {

        }

        private void 不具合調査修理依頼書_依頼分類その他_Leave(object sender, EventArgs e)
        {

        }

        private void 不具合調査修理依頼書_依頼分類その他_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 50);
            ChangedData(true);
        }

        private void 不具合調査修理依頼書_依頼分類その他_Validating(object sender, CancelEventArgs e)
        {
            
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 不具合調査修理依頼書_発生日_DoubleClick(object sender, EventArgs e)
        {
            不具合調査修理依頼書_発生日選択ボタン_Click(sender, e);
        }

        private void 不具合調査修理依頼書_発生日_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                不具合調査修理依頼書_発生日選択ボタン_Click(sender, e);
            }
        }

        private void 不具合調査修理依頼書_発生日_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 不具合調査修理依頼書_発生日_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 不具合調査修理依頼書_発生日選択ボタン_Click(object sender, EventArgs e)
        {
            dateSelectionForm = new F_カレンダー();

            if (!string.IsNullOrEmpty(不具合調査修理依頼書_発生日.Text))
            {
                dateSelectionForm.args = 不具合調査修理依頼書_発生日.Text;
            }

            if (dateSelectionForm.ShowDialog() == DialogResult.OK && 不具合調査修理依頼書_発生日.Enabled && !不具合調査修理依頼書_発生日.ReadOnly)
            {
                // 日付選択フォームから選択した日付を取得
                string selectedDate = dateSelectionForm.SelectedDate;

                // フォームAの日付コントロールに選択した日付を設定
                不具合調査修理依頼書_発生日.Text = selectedDate;
                不具合調査修理依頼書_発生日.Focus();
                UpdatedControl(不具合調査修理依頼書_発生日);
            }
        }

        private void 不具合調査修理依頼書_連絡者社名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 50);
            ChangedData(true);
        }

        private void 不具合調査修理依頼書_連絡者社名_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 不具合調査修理依頼書_連絡者氏名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 50);
            ChangedData(true);
        }

        private void 不具合調査修理依頼書_連絡者氏名_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 不具合調査修理依頼書_型番_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 50);
            ChangedData(true);
        }

        private void 不具合調査修理依頼書_型番_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 不具合調査修理依頼書_シリアル番号1_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 10);
            ChangedData(true);
        }

        private void 不具合調査修理依頼書_シリアル番号1_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 不具合調査修理依頼書_シリアル番号2_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 10);
            ChangedData(true);
        }

        private void 不具合調査修理依頼書_シリアル番号2_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 不具合調査修理依頼書_数量_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 不具合調査修理依頼書_数量_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 不具合調査修理依頼書_発生場所_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 50);
            ChangedData(true);
        }

        private void 不具合調査修理依頼書_発生場所_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 不具合調査修理依頼書_現象_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 4000);
        }

        private void 不具合調査修理依頼書_現象_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角２０００文字まで入力できます。　■入力欄をダブルクリックすると別ウィンドウに表示されます。";
        }

        private void 不具合調査修理依頼書_現象_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 不具合調査修理依頼書_現象_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            ChangedData(true);
        }

        private void 不具合調査修理依頼書_現象_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 不具合調査修理依頼書_顧客の声_DoubleClick(object sender, EventArgs e)
        {
            FunctionClass.DocZoom(sender as Control, CurrentCode, CurrentEdition, 4000);
        }

        private void 不具合調査修理依頼書_顧客の声_Enter(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "■全角２０００文字まで入力できます。　■入力欄をダブルクリックすると別ウィンドウに表示されます。";
        }

        private void 不具合調査修理依頼書_顧客の声_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel1.Text = "各種項目の説明";
        }

        private void 不具合調査修理依頼書_顧客の声_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            ChangedData(true);
        }

        private void 不具合調査修理依頼書_顧客の声_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 不具合調査修理依頼書_代替有無_Validated(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 不具合調査修理依頼書_代替有無_Validating(object sender, CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 不具合調査修理依頼書_回答書ボタン_Click(object sender, EventArgs e)
        {

        }

        #endregion

    }
}
