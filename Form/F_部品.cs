﻿using System;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using Microsoft.Identity.Client.NativeInterop;

namespace u_net
{



    public partial class F_部品 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        private string BASE_CAPTION = "部品";
        private int selected_frame = 0;





        public F_部品()
        {
            this.Text = "部品";       // ウィンドウタイトルを設定
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

            System.Type dgvtype = typeof(DataGridView);
            System.Reflection.PropertyInfo dgvPropertyInfo = dgvtype.GetProperty("DoubleBuffered", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            dgvPropertyInfo.SetValue(部品使用先, true, null);

            foreach (Control control in Controls)
            {
                control.PreviewKeyDown += OriginalClass.ValidateCheck;
            }

            FunctionClass fn = new FunctionClass();
            fn.DoWait("しばらくお待ちください...");

            //実行中フォーム起動
            string LoginUserCode = CommonConstants.LoginUserCode;//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            LocalSetting localSetting = new LocalSetting();
            localSetting.LoadPlace(LoginUserCode, this);

            MyApi myapi = new MyApi();
            int xSize, ySize, intpixel, twipperdot;

            //1インチ当たりのピクセル数 アクセスのサイズの引数がtwipなのでピクセルに変換する除算値を求める
            intpixel = myapi.GetLogPixel();
            twipperdot = myapi.GetTwipPerDot(intpixel);

            // DataGridViewの設定
            部品使用先.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            部品使用先.AllowUserToResizeColumns = true;
            部品使用先.ReadOnly = true;
            部品使用先.AllowUserToAddRows = false;
            部品使用先.AllowUserToDeleteRows = false;
            部品使用先.Font = new Font("MS ゴシック", 10);
            部品使用先.DefaultCellStyle.SelectionBackColor = Color.FromArgb(210, 210, 255);
            部品使用先.DefaultCellStyle.SelectionForeColor = Color.Black;
            部品使用先.GridColor = Color.FromArgb(230, 230, 230);
            部品使用先.ColumnHeadersDefaultCellStyle.Font = new Font("MS ゴシック", 9);
            部品使用先.DefaultCellStyle.Font = new Font("MS ゴシック", 10);
            部品使用先.DefaultCellStyle.ForeColor = Color.Black;

            OriginalClass ofn = new OriginalClass();
            ofn.SetComboBox(分類コード, "SELECT 分類記号 as Display,対象部品名 as Display2,分類コード as Value FROM M部品分類");
            分類コード.DrawMode = DrawMode.OwnerDrawFixed;
            分類コード.DropDownWidth = 600;
            ofn.SetComboBox(形状分類コード, "SELECT 部品形状名 as Display,部品形状コード as Value FROM M部品形状");
            ofn.SetComboBox(RohsStatusCode, "SELECT Name as Display,Code as Value FROM rohsStatusCode");


            this.JampAis.DataSource = new KeyValuePair<int, String>[] {
                new KeyValuePair<int, String>(2, "有り"),
                new KeyValuePair<int, String>(1, "無し"),
            };
            this.JampAis.DisplayMember = "Value";
            this.JampAis.ValueMember = "Key";

            this.非含有証明書.DataSource = new KeyValuePair<byte, String>[] {
                new KeyValuePair<byte, String>(1, "返却済み"),
                new KeyValuePair<byte, String>(2, "未返却"),
                new KeyValuePair<byte, String>(3, "未提出"),
            };
            this.非含有証明書.DisplayMember = "Value";
            this.非含有証明書.ValueMember = "Key";

            this.RoHS資料.DataSource = new KeyValuePair<Int16, String>[] {
                new KeyValuePair<Int16, String>(2, "有り"),
                new KeyValuePair<Int16, String>(1, "無し"),
            };
            this.RoHS資料.DisplayMember = "Value";
            this.RoHS資料.ValueMember = "Key";

            this.Rohs1ChemSherpaStatusCode.DataSource = new KeyValuePair<int, String>[] {
                new KeyValuePair<int, String>(2, "有り"),
                new KeyValuePair<int, String>(1, "無し"),
            };
            this.Rohs1ChemSherpaStatusCode.DisplayMember = "Value";
            this.Rohs1ChemSherpaStatusCode.ValueMember = "Key";

            this.Rohs2JampAisStatusCode.DataSource = new KeyValuePair<int, String>[] {
                new KeyValuePair<int, String>(2, "有り"),
                new KeyValuePair<int, String>(1, "無し"),
            };
            this.Rohs2JampAisStatusCode.DisplayMember = "Value";
            this.Rohs2JampAisStatusCode.ValueMember = "Key";

            this.Rohs2NonInclusionCertificationStatusCode.DataSource = new KeyValuePair<int, String>[] {
                new KeyValuePair<int, String>(1, "返却済み"),
                new KeyValuePair<int, String>(2, "未返却"),
                new KeyValuePair<int, String>(3, "未提出"),
            };
            this.Rohs2NonInclusionCertificationStatusCode.DisplayMember = "Value";
            this.Rohs2NonInclusionCertificationStatusCode.ValueMember = "Key";

            this.Rohs2DocumentStatusCode.DataSource = new KeyValuePair<int, String>[] {
                new KeyValuePair<int, String>(2, "有り"),
                new KeyValuePair<int, String>(1, "無し"),
            };
            this.Rohs2DocumentStatusCode.DisplayMember = "Value";
            this.Rohs2DocumentStatusCode.ValueMember = "Key";

            this.Rohs2ChemSherpaStatusCode.DataSource = new KeyValuePair<int, String>[] {
                new KeyValuePair<int, String>(2, "有り"),
                new KeyValuePair<int, String>(1, "無し"),
            };
            this.Rohs2ChemSherpaStatusCode.DisplayMember = "Value";
            this.Rohs2ChemSherpaStatusCode.ValueMember = "Key";

            this.CalcInventoryCode.DataSource = new KeyValuePair<string, string>[] {
            new KeyValuePair<string, string>("01", "する"),
            new KeyValuePair<string, string>("02", "しない"),
};
            this.CalcInventoryCode.DisplayMember = "Value";
            this.CalcInventoryCode.ValueMember = "Key";


            this.受入検査ランク.DataSource = new KeyValuePair<string, string>[] {
            new KeyValuePair<string, string>("A         ", "A         "),
            new KeyValuePair<string, string>("B1        ", "B1        "),
            new KeyValuePair<string, string>("B2        ", "B2        "),
            new KeyValuePair<string, string>("C         ", "C         "),
            new KeyValuePair<string, string>("D         ", "D         "),
};
            this.受入検査ランク.DisplayMember = "Value";
            this.受入検査ランク.ValueMember = "Key";

            try
            {
                this.SuspendLayout();

                int intWindowHeight = this.Height;
                int intWindowWidth = this.Width;


                if (string.IsNullOrEmpty(args))
                {
                    コマンド新規_Click(sender, e);
                }
                else
                {
                    コマンド読込_Click(sender, e);
                    if (!string.IsNullOrEmpty(args))
                    {
                        this.部品コード.Text = args;
                        UpdatedControl(部品コード);
                    }
                }
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
                fn.WaitForm.Close();
            }
        }


        private void Form_Unload(object sender, FormClosingEventArgs e)
        {

            string LoginUserCode = CommonConstants.LoginUserCode;//テスト用 ログインユーザを実行中にどのように管理するか決まったら修正
            LocalSetting test = new LocalSetting();
            test.SavePlace(LoginUserCode, this);

            try
            {

                Connect();

                // データへの変更がないときの処理
                if (!IsChanged)
                {
                    // 新規モードで且つコードが取得済みのときはコードを戻す
                    if (IsNewData && !string.IsNullOrEmpty(CurrentCode) && CurrentEdition == 1)
                    {
                        // 採番された番号を戻す
                        if (!FunctionClass.ReturnCode(cn, "PAR" + CurrentCode))
                        {
                            MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine + Environment.NewLine +
                                            "部品コード　：　" + CurrentCode, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                    return;
                }

                // 修正されているときは登録確認を行う
                var intRes = MessageBox.Show("変更内容を登録しますか？", "確認", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
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
                        // 新規コードを取得していたときはコードを戻す
                        if (IsNewData && !string.IsNullOrEmpty(CurrentCode) && CurrentEdition == 1)
                        {
                            if (!FunctionClass.ReturnCode(cn, "PAR" + CurrentCode))
                            {
                                MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine +
                                                "部品コード　：　" + CurrentCode, "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        public string CurrentCode
        {
            get
            {
                return string.IsNullOrEmpty(部品コード.Text) ? "" : 部品コード.Text;
            }
        }

        public int CurrentEdition
        {
            get
            {
                return string.IsNullOrEmpty(版数.Text) ? 0 : int.Parse(版数.Text);
            }
        }

        public bool IsIncluded
        {
            get
            {
                return !string.IsNullOrEmpty(部品集合コード.Text);
            }
        }

        private bool SaveData()
        {

            Connect();
            SqlTransaction transaction = cn.BeginTransaction();
            {
                try
                {

                    DateTime dteNow = DateTime.Now;
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
                        objControl1 = this.作成日時;
                        objControl2 = this.作成者コード;
                        objControl3 = this.CreatorName;
                        varSaved1 = objControl1.Text;
                        varSaved2 = objControl2.Text;
                        varSaved3 = objControl3.Text;
                        objControl1.Text = dteNow.ToString(); // ここでDateTimeをstringに変換して設定
                        objControl2.Text = CommonConstants.LoginUserCode;
                        objControl3.Text = CommonConstants.LoginUserFullName;


                    }

                    objControl4 = this.更新日時;
                    objControl5 = this.更新者コード;
                    objControl6 = this.UpdaterName;

                    varSaved4 = objControl4.Text;
                    varSaved5 = objControl5.Text;
                    varSaved6 = objControl6.Text;

                    objControl4.Text = dteNow.ToString(); // ここでDateTimeをstringに変換して設定
                    objControl5.Text = CommonConstants.LoginUserCode;
                    objControl6.Text = CommonConstants.LoginUserFullName;


                    string strwhere = " 部品コード='" + this.部品コード.Text + "'";

                    随時登録.Text = "0";
                    Revision.Text = "1";


                    if (!DataUpdater.UpdateOrInsertDataFrom(this, cn, "M部品", strwhere, "部品コード", transaction))
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

                        return false;
                    }

                    // トランザクションをコミット
                    transaction.Commit();


                    部品コード.Enabled = true;

                    // 新規モードのときは修正モードへ移行する
                    if (IsNewData)
                    {
                        コマンド新規.Enabled = true;
                    }

                    コマンド複写.Enabled = true;
                    コマンド削除.Enabled = true;
                    コマンド登録.Enabled = false;

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error in SaveData: " + ex.Message);
                    return false;
                }
            }
        }


        private void コマンド登録_Click(object sender, EventArgs e)
        {

            if (this.ActiveControl == this.コマンド登録)
            {
                GetNextControl(コマンド登録, false).Focus();
            }

            // エラーチェック
            if (!ErrCheck())
            {
                goto Bye_コマンド登録_Click;
            }

            FunctionClass fn = new FunctionClass();
            fn.DoWait("登録しています...");


            // 登録処理
            if (SaveData())
            {
                // 登録に成功した
                ChangedData(false); // データ変更取り消し

                if (this.IsNewData)
                {
                    // 新規モードのとき
                    this.コマンド新規.Enabled = true;
                    this.コマンド読込.Enabled = false;
                    this.コマンド入出庫.Enabled = true;
                }
                // UpdatePurGridの呼び出し
                // Call UpdatePurGrid();
                fn.WaitForm.Close();
                MessageBox.Show("登録を完了しました", "登録コマンド", MessageBoxButtons.OK);
            }
            else
            {
                // 登録に失敗したとき
                fn.WaitForm.Close();
                this.コマンド登録.Enabled = true;
                MessageBox.Show("登録できませんでした。", "登録コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

        Bye_コマンド登録_Click:
            return;

        }


        private bool ErrCheck()
        {
            //入力確認    
            if (IsError(this.部品コード)) return false;
            if (IsError(this.版数)) return false;
            if (IsError(this.品名)) return false;
            if (IsError(this.型番, true)) return false;
            if (IsError(this.メーカーコード)) return false;
            if (IsError(this.分類コード)) return false;
            if (IsError(this.形状分類コード)) return false;
            if (IsError(this.受入検査ランク)) return false;
            if (IsError(this.CalcInventoryCode)) return false;

            return true;
        }



        private void ChangedData(bool isChanged)
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

            // コードにフォーカスがある状態でサブフォームから呼び出されたときの対処
            if (this.ActiveControl == this.部品コード)
            {
                this.品名.Focus();
            }

            this.部品コード.Enabled = !isChanged;
            this.改版ボタン.Enabled = !isChanged;
            // this.コマンド複写.Enabled = !isChanged; // コマンド複写についての情報が提供されていないためコメントアウト
            this.コマンド削除.Enabled = !isChanged;
            this.コマンド登録.Enabled = isChanged;

            // RoHSの状態表示を更新する
            ShowRohsStatus();
        }


        private void コマンド新規_Click(object sender, EventArgs e)
        {
            try
            {
                Connect();

                Cursor.Current = Cursors.WaitCursor;
                this.DoubleBuffered = true;

                if (this.ActiveControl == this.コマンド新規)
                {
                    this.コマンド新規.Focus();
                }

                // 変更がある
                if (this.IsChanged)
                {
                    var intRes = MessageBox.Show("変更内容を登録しますか？", "新規コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    switch (intRes)
                    {
                        case DialogResult.Yes:
                            // 登録処理
                            if (!SaveData())
                            {
                                MessageBox.Show("エラーのため登録できません。", "新規コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                goto Bye_コマンド新規_Click;
                            }
                            break;
                        case DialogResult.Cancel:
                            goto Bye_コマンド新規_Click;
                    }
                }

                VariableSet.SetControls(this);
                DispGrid(CurrentCode);

                string code = FunctionClass.採番(cn, "PAR");
                部品コード.Text = code.Substring(Math.Max(0, code.Length - 8));
                版数.Text = 1.ToString();
                入数.Text = 1.ToString();
                単位数量.Text = 1.ToString();
                ロス率.Text = 0f.ToString();
                Rohs1ChemSherpaStatusCode.SelectedValue = 1;
                JampAis.SelectedValue = 1;
                非含有証明書.SelectedValue = (byte)3;
                RoHS資料.SelectedValue = (Int16)1;
                Rohs2ChemSherpaStatusCode.SelectedValue = 1;
                Rohs2JampAisStatusCode.SelectedValue = 1;
                Rohs2NonInclusionCertificationStatusCode.SelectedValue = 3;
                Rohs2DocumentStatusCode.SelectedValue = 1;
                Rohs2ProvisionalRegisteredStatusCode.Checked = true;
                廃止.Checked = false;


                ChangedData(false);

                InventoryAmount.Text = 0.ToString();
                ShowRohsStatus();
                品名.Focus();
                部品コード.Enabled = false;
                改版ボタン.Enabled = false;
                コマンド新規.Enabled = false;
                コマンド読込.Enabled = true;
                コマンド削除.Enabled = false;
                コマンド入出庫.Enabled = false;
                コマンド履歴.Enabled = false;
                コマンド登録.Enabled = false;



            }
            finally
            {

                this.DoubleBuffered = false;
                Cursor.Current = Cursors.Default;
            }

        Bye_コマンド新規_Click:
            return;
        }



        private void DispGrid(string codeString)
        {
            try
            {

                Connect();

                using (SqlCommand command = new SqlCommand("SP部品使用先", cn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@PartsCode", codeString);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        // DataGridViewを設定していると仮定
                        部品使用先.DataSource = dataTable;

                        if (dataTable.Rows.Count <= 0)
                        {
                            // データがない場合はDataGridViewにダミーデータを表示する
                            dataTable.Rows.Add();
                        }

                        部品使用先.Columns[0].Width = 70;
                        部品使用先.Columns[1].Width = 20;
                        部品使用先.Columns[2].Width = 195;
                        部品使用先.Columns[3].Width = 20;
                        部品使用先.Font = new Font("MS ゴシック", 9);
                        部品使用先.Columns[部品使用先.ColumnCount - 1].Selected = true;
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DispGrid: " + ex.Message);
            }
        }

        private void ShowRohsStatus()
        {
            if (this.Rohs2ProvisionalRegisteredStatusCode.Checked)
            {
                this.RohsStatusCode.SelectedValue = 5;
                // this.RohsStatusName = "仮RoHS2";
            }
            else
            {
                if ((Rohs2ChemSherpaStatusCode.SelectedValue != null && (int)Rohs2ChemSherpaStatusCode.SelectedValue == 2) ||
    (Rohs2JampAisStatusCode.SelectedValue != null && (int)Rohs2JampAisStatusCode.SelectedValue == 2) ||
    (Rohs2NonInclusionCertificationStatusCode.SelectedValue != null && (int)Rohs2NonInclusionCertificationStatusCode.SelectedValue == 1) ||
    (Rohs2DocumentStatusCode.SelectedValue != null && (int)Rohs2DocumentStatusCode.SelectedValue == 2))
                {
                    this.RohsStatusCode.SelectedValue = 2;
                    // this.RohsStatusName = "RoHS2";
                }
                else if ((Rohs1ChemSherpaStatusCode.SelectedValue != null && (int)Rohs1ChemSherpaStatusCode.SelectedValue == 2) ||
         (JampAis.SelectedValue != null && (int)JampAis.SelectedValue == 2) ||
         (非含有証明書.SelectedValue != null && (byte)非含有証明書.SelectedValue == 1) ||
         (RoHS資料.SelectedValue != null && (Int16)RoHS資料.SelectedValue == 2))
                {
                    this.RohsStatusCode.SelectedValue = 6;
                    // this.RohsStatusName = "RoHS2非対応";
                }
                else
                {
                    this.RohsStatusCode.SelectedValue = 3;
                    // this.RohsStatusName = "RoHS1非対応";
                }
            }
        }

        private void コマンド読込_Click(object sender, EventArgs e)
        {
            if (!AskSave()) { return; }


            // strOpenArgsがどのように設定されているかに依存します。
            // もしstrOpenArgsに関連する処理が必要な場合はここに追加してください。

            // 各コントロールの値をクリア
            VariableSet.SetControls(this);

            ChangedData(false);

            // コントロールを操作
            部品コード.Enabled = true;
            部品コード.Focus();
            改版ボタン.Enabled = false;
            コマンド新規.Enabled = true;
            コマンド読込.Enabled = false;
            コマンド入出庫.Enabled = false;
            コマンド登録.Enabled = false;
        }


        private bool AskSave()
        {
            try
            {
                Connect();

                DialogResult response;
                bool isNewData = IsNewData; // 仮定されたIsNewDataプロパティの取得
                string currentCode = CurrentCode; // 仮定されたCurrentCodeプロパティの取得

                if (コマンド登録.Enabled)
                {
                    response = MessageBox.Show("変更内容を登録しますか？", "質問", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    switch (response)
                    {
                        case DialogResult.Yes:
                            if (SaveData())
                            {
                                // 保存が成功した場合の処理
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
                if (isNewData)
                {
                    if (!string.IsNullOrEmpty(currentCode))
                    {
                        // 部品コードが採番された場合、番号を戻す処理
                        if (!FunctionClass.Recycle(cn, "PAR" + currentCode))
                        {
                            MessageBox.Show("部品コードは破棄されました。" + Environment.NewLine + Environment.NewLine +
                                            "部品コード： " + currentCode, "通知", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in AskSave: " + ex.Message);
                return false;
            }
        }






        private void コマンド承認_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。", "承認コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド確定_Click(object sender, EventArgs e)
        {
            MessageBox.Show("現在開発中です。", "確定コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }



        private void 改版ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ActiveControl == this.改版ボタン)
                {
                    GetNextControl(改版ボタン, false).Focus();
                }


                MessageBox.Show("部品の改版機能は未完成です。\n履歴に登録される情報は完全ではありません。", "改版", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                if (MessageBox.Show("改版しますか？\n\n・旧版データは履歴コマンドから参照できます。\n・最新版の部品データが有効になります。\n・この操作を元に戻すことはできません。",
                                    "改版", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    return;
                }


                FunctionClass fn = new FunctionClass();
                fn.DoWait("改版しています...");

                CommonConnect();

                if (SaveData())
                {
                    if (AddHistory(cn, this.CurrentCode, this.CurrentEdition))
                    {
                        //this.部品コード.Requery;
                        // ■ なぜかRequeryしてもColumn(1)がNULLとなるので、版数を+1する
                        this.版数.Text = (Convert.ToInt32(this.CurrentEdition) + 1).ToString();
                        this.コマンド履歴.Enabled = true;

                        fn.WaitForm.Close();
                    }
                    else
                    {
                        fn.WaitForm.Close();
                        MessageBox.Show("改版できませんでした。", "改版", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }
                }
                else
                {
                    fn.WaitForm.Close();
                    MessageBox.Show("改版できませんでした。", "改版", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_改版ボタン_Click - " + ex.Message);
                MessageBox.Show("エラーが発生しました。", BASE_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
        private void コマンドメーカー_Click(object sender, EventArgs e)
        {
            try
            {

                if (this.ActiveControl == this.コマンドメーカー)
                {
                    GetNextControl(コマンドメーカー, false).Focus();
                }

                string strCode = this.メーカーコード.Text;
                if (string.IsNullOrEmpty(strCode))
                {
                    MessageBox.Show("メーカーコードを入力してください。", BASE_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.メーカーコード.Focus();
                }
                else
                {
                    F_メーカー targetform = new F_メーカー();

                    targetform.args = strCode;
                    targetform.MdiParent = this.MdiParent;
                    targetform.FormClosed += (s, args) => { this.Enabled = true; };
                    this.Enabled = false;

                    targetform.Show();

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in コマンドメーカー_Click: " + ex.Message);
            }
        }


        private void コマンド入出庫_Click(object sender, EventArgs e)
        {
            try
            {

                F_入出庫履歴 targetform = new F_入出庫履歴();

                targetform.args = 部品コード.Text;
                targetform.MdiParent = this.MdiParent;
                targetform.FormClosed += (s, args) => { this.Enabled = true; };
                this.Enabled = false;

                targetform.Show();


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in コマンドメーカー_Click: " + ex.Message);
            }
        }



        private void コマンド履歴_Click(object sender, EventArgs e)
        {
            try
            {


                F_部品履歴 targetform = new F_部品履歴();

                targetform.args = CurrentCode;
                targetform.args2 = CurrentEdition - 1;
                targetform.MdiParent = this.MdiParent;
                targetform.FormClosed += (s, args) => { this.Enabled = true; };
                this.Enabled = false;

                targetform.Show();


            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in コマンド履歴_Click: " + ex.Message);
            }
        }


        private F_検索 SearchForm;

        private void メーカーコード検索ボタン_Click(object sender, EventArgs e)
        {
            SearchForm = new F_検索();
            SearchForm.FilterName = "メーカー名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                string SelectedCode = SearchForm.SelectedCode;

                メーカーコード.Text = SelectedCode;
                string str1 = FunctionClass.GetMakerName(cn, SelectedCode);
                string str2 = FunctionClass.GetMakerShortName(cn, SelectedCode);
                MakerName.Text = str1;
                MakerShortName.Text = str2;

            }
        }





        private bool AddHistory(SqlConnection connection, string codeString, int editionNumber)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand("SP部品履歴追加", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@strCode", SqlDbType.VarChar).Value = codeString;
                    cmd.Parameters.Add("@intEdition", SqlDbType.Int).Value = editionNumber;
                    cmd.ExecuteNonQuery(); // ストアドプロシージャを実行
                }

                return true; // 成功
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_AddHistory - " + ex.Message);
                return false; // 失敗
            }
            finally
            {
                if (connection.State == ConnectionState.Open)
                {
                    connection.Close(); // 接続を閉じる
                }
            }
        }

        private void 資料ボタン_Click(object sender, EventArgs e)
        {
            try
            {
                if (ActiveControl == 資料ボタン)
                {
                    GetNextControl(資料ボタン, false).Focus();
                }

                string strCode = 部品コード.Text;
                if (string.IsNullOrEmpty(strCode))
                {
                    MessageBox.Show("部品コードを入力してください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    部品コード.Focus();
                }
                else
                {
                    F_部品_資料添付 targetform = new F_部品_資料添付();

                    targetform.args = strCode;
                    targetform.MdiParent = this.MdiParent;
                    targetform.FormClosed += (s, args) => { this.Enabled = true; };
                    this.Enabled = false;

                    targetform.Show();

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void コマンド削除_Click(object sender, EventArgs e)
        {
            try
            {
                if (ActiveControl == this.コマンド削除)
                {
                    GetNextControl(コマンド削除, false).Focus();
                }

                if (IsIncluded)
                {
                    MessageBox.Show("この部品は部品集合に構成されているため、削除できません。\n削除するには対象となる部品集合から構成を解除する必要があります。",
                        "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                string strMsg = "部品コード　：　" + this.CurrentCode + "\n\nこのデータを削除しますか？\n削除後、管理者により復元することができます。";

                if (MessageBox.Show(strMsg, "削除コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                // ログインユーザーが表示データの登録ユーザーでなければ認証する

                using (var targetform = new F_認証())
                {
                    targetform.args = CommonConstants.USER_CODE_TECH;
                    //targetform.MdiParent = this.MdiParent;
                    //targetform.FormClosed += (s, args) => { this.Enabled = true; };
                    //this.Enabled = false;

                    targetform.ShowDialog();


                    if (string.IsNullOrEmpty(CommonConstants.strCertificateCode))
                    {
                        MessageBox.Show("認証できません。" + Environment.NewLine + "削除はキャンセルされました。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }
                }


                // 部品情報削除
                FunctionClass fn = new FunctionClass();
                fn.DoWait("削除しています...");

                Connect();

                // 削除に成功すれば新規モードへ移行する
                if (DeleteData(cn, CurrentCode, CurrentEdition))
                {
                    fn.WaitForm.Close();
                    MessageBox.Show("削除しました。\n部品コード　：　" + this.CurrentCode, "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    コマンド新規_Click(sender, e);
                }
                else
                {
                    fn.WaitForm.Close();
                    MessageBox.Show("削除できませんでした。\n部品コード　：　" + this.CurrentCode, "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in コマンド削除_Click: " + ex.Message);
            }
        }


        public bool DeleteData(SqlConnection cn, string codeString, int editionNumber = -1, bool completed = false)
        {
            SqlTransaction transaction = null;
            try
            {
                string strKey = "部品コード='" + codeString + "'";
                string strSQL;
                bool deleteData = false;

                transaction = cn.BeginTransaction();

                if (completed)
                {
                    strSQL = "DELETE FROM M部品 WHERE " + strKey;
                    using (SqlCommand cmd = new SqlCommand(strSQL, cn, transaction))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    strSQL = "UPDATE M部品 SET 無効日時 = GETDATE() WHERE " + strKey;
                    using (SqlCommand cmd = new SqlCommand(strSQL, cn, transaction))
                    {
                        cmd.ExecuteNonQuery();
                    }
                }

                using (SqlCommand cmd = new SqlCommand("SPユニット管理", cn, transaction))
                {
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@PartsCode", codeString));
                    cmd.ExecuteNonQuery();
                }

                // トランザクションをコミット
                transaction.Commit();

                deleteData = true;
                return deleteData;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in DeleteData: " + ex.Message);
                if (transaction != null)
                {
                    transaction.Rollback();
                }
                return false;
            }
        }

        private void コマンド複写_Click(object sender, EventArgs e)
        {
            try
            {

                this.品名.Focus();
                ChangedData(true);

                Connect();

                // 以下、初期値の設定
                string code = FunctionClass.採番(cn, "PAR");
                部品コード.Text = code.Substring(Math.Max(0, code.Length - 8));
                this.版数.Text = 1.ToString();
                this.InventoryAmount.Text = 0.ToString();
                this.作成日時.Text = null;
                this.作成者コード.Text = null;
                this.CreatorName.Text = null;
                this.更新日時.Text = null;
                this.更新者コード.Text = null;
                this.UpdaterName.Text = null;

                // インターフェース更新
                this.コマンド新規.Enabled = false;
                this.コマンド読込.Enabled = true;

                // 使用先の表示
                DispGrid(this.CurrentCode);

                // RoHS対応状態の表示を更新する
                ShowRohsStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "複写コマンド", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {

            }
        }


        private void コマンド仕入先_Click(object sender, EventArgs e)
        {
            try
            {
                if (selected_frame == 1)
                {
                    string code = OriginalClass.Nz(仕入先1コード.Text, null);
                    if (string.IsNullOrEmpty(code))
                    {
                        MessageBox.Show("仕入先1を入力してください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        仕入先1コード.Focus();
                    }
                    else
                    {
                        F_仕入先 targetform = new F_仕入先();

                        targetform.args = code;
                        targetform.MdiParent = this.MdiParent;
                        targetform.FormClosed += (s, args) => { this.Enabled = true; };
                        this.Enabled = false;

                        targetform.Show();

                    }
                }
                else if (selected_frame == 2)
                {
                    string code = OriginalClass.Nz(仕入先2コード.Text, null);
                    if (string.IsNullOrEmpty(code))
                    {
                        MessageBox.Show("仕入先2を入力してください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        仕入先2コード.Focus();
                    }
                    else
                    {
                        F_仕入先 targetform = new F_仕入先();

                        targetform.args = code;
                        targetform.MdiParent = this.MdiParent;
                        targetform.FormClosed += (s, args) => { this.Enabled = true; };
                        this.Enabled = false;

                        targetform.Show();

                    }
                }
                else if (selected_frame == 3)
                {
                    string code = OriginalClass.Nz(仕入先3コード.Text, null);
                    if (string.IsNullOrEmpty(code))
                    {
                        MessageBox.Show("仕入先3を入力してください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        仕入先3コード.Focus();
                    }
                    else
                    {
                        F_仕入先 targetform = new F_仕入先();

                        targetform.args = code;
                        targetform.MdiParent = this.MdiParent;
                        targetform.FormClosed += (s, args) => { this.Enabled = true; };
                        this.Enabled = false;

                        targetform.Show();

                    }
                }
                else
                {
                    MessageBox.Show("参照する仕入先を選択してください。", "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error in コマンド仕入先_Click: " + ex.Message);
            }
        }


        private void コマンド終了_Click(object sender, EventArgs e)
        {


            Close(); // フォームを閉じる
        }



        //private void UpdatePurGrid()
        //{
        //■現在未使用
        //登録後の処理として、購買フォームが開いている場合はグリッドを更新する
        //    try
        //    {
        //        // 購買フォームが開いているかを確認
        //        if (Application.OpenForms["購買"] == null)
        //        {
        //            return;
        //        }

        //        // 購買フォームのグリッドを取得
        //        Form frmPurchase = Application.OpenForms["購買"];
        //        MSHierarchicalFlexGridLib.MSHFlexGrid obj1 = (MSHierarchicalFlexGridLib.MSHFlexGrid)frmPurchase.Controls["objGrid"];

        //        if (obj1 != null)
        //        {
        //            int currentRow = obj1.row;
        //            string supplierCode = OriginalClass.Nz(仕入先1コード.Text,null);
        //            string supplierName = OriginalClass.Nz(Supplier1Name.Text, null);
        //            string productName = OriginalClass.Nz(品名.Text, null);
        //            string modelNumber = OriginalClass.Nz(型番.Text, null);
        //            double unitPrice = OriginalClass.Nz(仕入先1単価.Text, null);

        //            if (obj1.TextMatrix[currentRow, 5] != supplierCode)
        //            {
        //                obj1.TextMatrix[currentRow, 5] = supplierCode;
        //                obj1.TextMatrix[currentRow, 6] = supplierName;
        //                frmPurchase.GetType().GetMethod("ChangedControl").Invoke(frmPurchase, new object[] { true });
        //            }

        //            obj1.TextMatrix[currentRow, 8] = productName;
        //            obj1.TextMatrix[currentRow, 9] = modelNumber;
        //            obj1.TextMatrix[currentRow, 13] = unitPrice.ToString("###,###,##0.00");

        //            frmPurchase.GetType().GetMethod("CalcAmount").Invoke(frmPurchase, new object[] { currentRow });
        //            frmPurchase.GetType().GetProperty("bleGridUpdated").SetValue(frmPurchase, true, null);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.Print(Name + "_UpdatedPurGrid - " + ex.Message);
        //    }
        //}


        private bool LoadData(Form formObject, string codeString, int editionNumber = 0)
        {
            try
            {
                Connect();

                string strSQL;
                if (editionNumber == 0)
                {
                    strSQL = "SELECT * FROM V部品読込 WHERE 部品コード ='" + codeString + "'";
                }
                else
                {
                    strSQL = "SELECT * FROM V部品読込 WHERE 部品コード ='" + codeString + "' AND 部品版数= " + editionNumber;
                }


                VariableSet.SetTable2Form(this, strSQL, cn);


                if (!string.IsNullOrEmpty(this.無効日時.Text))
                {
                    this.削除.Text = "■";
                }

                return true;
                //}




            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // エラーハンドリングが必要に応じて行われるべきです
                return false;
            }
        }
        private int CheckParts(SqlConnection connectionObject, string codeString, int edition = 0)
        {
            try
            {
                string strKey;
                string strSQL;
                int recordCount = 0;

                if (string.IsNullOrEmpty(codeString))
                    return 0;

                if (edition == 0)
                {
                    strKey = "部品コード = '" + codeString + "'";
                }
                else
                {
                    strKey = "部品コード = '" + codeString + "' and 版数 = " + edition;
                }

                strSQL = "SELECT COUNT(*) FROM M部品 WHERE " + strKey;

                using (SqlCommand cmd = new SqlCommand(strSQL, connectionObject))
                {
                    connectionObject.Open();
                    recordCount = Convert.ToInt32(cmd.ExecuteScalar());
                    connectionObject.Close();
                }

                return recordCount;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                // エラーハンドリングが必要に応じて行われるべきです
                return 0;
            }
        }


        private bool IsError(Control controlObject, bool Cancel = false)
        {
            try
            {



                object varValue = controlObject.Text;
                string controlName = controlObject.Name;

                switch (controlName)
                {
                    case "部品コード":
                    case "品名":
                        if (string.IsNullOrEmpty(varValue.ToString()))
                        {
                            MessageBox.Show(controlName + "を入力してください.", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            controlObject.Focus();
                            return true;
                        }
                        break;
                    case "型番":
                        if (Cancel)
                        {
                            if (string.IsNullOrEmpty(varValue.ToString()))
                            {
                                MessageBox.Show(controlName + "を入力してください.", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                controlObject.Focus();
                                return true;
                            }
                        }


                        DataTable rs1 = null;
                        string strPartsList = null;

                        if (DetectRepeatedParts(varValue.ToString(), CurrentCode, ref rs1))
                        {
                            if (rs1.Rows.Count > 0)
                            {
                                foreach (DataRow row in rs1.Rows)
                                {
                                    strPartsList += row[0].ToString() + " ： ";
                                    strPartsList += row[1].ToString() + " ： ";
                                    strPartsList += row[2].ToString() + Environment.NewLine;
                                }

                                MessageBox.Show($"入力された型番は既に登録されています。{Environment.NewLine}{strPartsList}", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                controlObject.Focus();
                                return true;
                            }

                        }
                        break;
                    case "メーカーコード":
                        if (string.IsNullOrEmpty(varValue.ToString()))
                        {
                            MessageBox.Show(controlName + "を入力してください.", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            controlObject.Focus();
                            return true;
                        }
                        //else
                        //{
                        //    string str1 = FunctionClass.GetMakerName(cn, controlObject.Text.ToString());
                        //    string str2 = FunctionClass.GetMakerShortName(cn, controlObject.Text.ToString());
                        //    if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
                        //    {
                        //        MessageBox.Show("存在しないメーカーです。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        //        controlObject.Focus();
                        //        return true;
                        //    }
                        //    else
                        //    {
                        //        MakerName.Text = str1;
                        //        MakerShortName.Text = str2;
                        //    }
                        //}

                        break;
                    case "仕入先1単価":
                    case "仕入先2単価":
                    case "仕入先3単価":
                        if (!string.IsNullOrEmpty(varValue.ToString()))
                        {
                            if (!FunctionClass.IsLimit_N(varValue, 8, 2, controlName))
                            {
                                controlObject.Focus();
                                return true;
                            }
                        }
                        break;
                    case "分類コード":
                        if (string.IsNullOrEmpty(varValue.ToString()))
                        {
                            MessageBox.Show(controlName + "を入力してください.", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            controlObject.Focus();
                            return true;
                        }
                        GroupNumber.Text = 分類コード.Text;
                        break;
                    case "形状分類コード":
                        if (string.IsNullOrEmpty(varValue.ToString()))
                        {
                            MessageBox.Show("形状分類を指定してください.", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            controlObject.Focus();
                            return true;
                        }
                        FormGroupShortName.Text = 形状分類コード.Text;
                        break;
                    case "入数":
                        if (!FunctionClass.IsLimit_N(varValue, 8, 0, controlName))
                        {
                            controlObject.Focus();
                            return true;
                        }
                        break;
                    case "単位数量":
                        if (!FunctionClass.IsLimit_N(varValue, 8, 0, controlName))
                        {
                            controlObject.Focus();
                            return true;
                        }
                        break;
                    case "StandardDeliveryDay":
                        // カスタムのチェックロジックが必要であればここに追加してください。
                        break;
                    case "CalcInventoryCode":
                        if (string.IsNullOrEmpty(varValue.ToString()))
                        {
                            MessageBox.Show("在庫計算を指定してください.", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            controlObject.Focus();
                            return true;
                        }
                        break;
                    case "受入検査ランク":
                        if (string.IsNullOrEmpty(varValue.ToString()))
                        {
                            MessageBox.Show("受入検査ランクを指定してください.", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            controlObject.Focus();
                            return true;
                        }
                        break;
                    case "ロス率":
                        if (!FunctionClass.IsLimit_N(varValue, 8, 7, controlName))
                        {
                            controlObject.Focus();
                            return true;
                        }
                        break;
                    case "Rohs1ChemSherpaStatusCode":
                    case "JampAis":
                    case "非含有証明書":
                    case "RoHS資料":
                    case "Rohs2ChemSherpaStatusCode":
                    case "Rohs2JampAisStatusCode":
                    case "Rohs2NonInclusionCertificationStatusCode":
                    case "Rohs2DocumentStatusCode":
                        if (string.IsNullOrEmpty(varValue.ToString()))
                        {
                            MessageBox.Show("[" + controlObject.Tag + "]を入力してください.", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            controlObject.Focus();
                            return true;
                        }
                        break;
                    case "ChemSherpaVersion":
                        // カスタムのチェックロジックが必要であればここに追加してください。
                        break;
                    case "在庫数量":
                        if (!FunctionClass.IsLimit_N(varValue, 8, 0, controlName))
                        {
                            controlObject.Focus();
                            return true;
                        }
                        break;
                    default:
                        // 他のコントロールに対するエラーチェックロジックを追加してください。
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


        public bool DetectRepeatedParts(string ModelString, string ExCodeString, ref DataTable recordsetObject)
        {
            bool detectRepeatedParts = false;

            try
            {

                Connect();

                using (SqlCommand command = new SqlCommand("SP部品重複検出", cn))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.Add("@strModel", SqlDbType.VarChar).Value = ModelString;
                    command.Parameters.Add("@strExCode", SqlDbType.VarChar).Value = ExCodeString;

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        recordsetObject = new DataTable();
                        adapter.Fill(recordsetObject);
                    }
                    detectRepeatedParts = true;
                }

            }
            catch (Exception ex)
            {
                // エラーハンドリング
                Console.WriteLine("DetectRepeatedPartsエラー: " + ex.Message);
            }

            return detectRepeatedParts;
        }

        private void UpdatedControl(Control controlObject)
        {
            try
            {
                Connect();

                switch (controlObject.Name)
                {
                    case "部品コード":

                        FunctionClass fn = new FunctionClass();
                        fn.DoWait("読み込んでいます...");




                        string query = "select max(版数) as 最終版数 from M部品履歴 where 部品コード='" + 部品コード.Text + "' group by 部品コード";

                        using (SqlCommand command = new SqlCommand(query, cn))
                        {
                            SqlDataAdapter adapter = new SqlDataAdapter(command);
                            DataTable dataTable = new DataTable();
                            adapter.Fill(dataTable);
                            if (dataTable.Rows.Count > 0)
                            {
                                版数.Text = dataTable.Rows[0]["最終版数"].ToString();
                            }
                        }


                        // 内容の表示
                        LoadData(this, 部品コード.Text);
                        // 使用先の表示
                        DispGrid(部品コード.Text);
                        // 動作制御
                        改版ボタン.Enabled = true;
                        // コマンド複写.Enabled = true;
                        コマンド削除.Enabled = true;
                        コマンド入出庫.Enabled = true;
                        コマンド履歴.Enabled = !(CurrentEdition <= 1);

                        ChangedData(false);

                        fn.WaitForm.Close();
                        break;
                    case "仕入先1コード":
                        // 仕入先コードからの関連情報表示
                        if (string.IsNullOrEmpty(controlObject.Text))
                        {
                            Supplier1Name.Text = null;
                            return;
                        }
                        Supplier1Name.Text = FunctionClass.GetSupplierName(cn, controlObject.Text.ToString());
                        break;
                    case "仕入先2コード":
                        // 仕入先コードからの関連情報表示
                        if (string.IsNullOrEmpty(controlObject.Text))
                        {
                            Supplier2Name.Text = null;
                            return;
                        }
                        Supplier2Name.Text = FunctionClass.GetSupplierName(cn, controlObject.Text.ToString());
                        break;
                    case "仕入先3コード":
                        // 仕入先コードからの関連情報表示
                        if (string.IsNullOrEmpty(controlObject.Text))
                        {
                            Supplier3Name.Text = null;
                            return;
                        }
                        Supplier3Name.Text = FunctionClass.GetSupplierName(cn, controlObject.Text.ToString());
                        break;
                    case "入数":
                    case "単位数量":
                        int parsedValue = int.Parse(controlObject.Text);
                        if (parsedValue < 1)
                        {
                            controlObject.Text = 1.ToString();
                        }
                        break;
                    case "ロス率":
                        controlObject.Text = float.Parse(controlObject.Text.ToString()).ToString();
                        // Me.Controls(ControlName).Value = Me.Controls(ControlName).Value / 100;
                        break;
                    case "メーカーコード":

                        string str1 = FunctionClass.GetMakerName(cn, controlObject.Text.ToString());
                        string str2 = FunctionClass.GetMakerShortName(cn, controlObject.Text.ToString());
                        if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty(str2))
                        {
                            MessageBox.Show("存在しないメーカーです。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            controlObject.Focus();
                            return;
                        }
                        else
                        {
                            MakerName.Text = str1;
                            MakerShortName.Text = str2;
                        }

                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.Print(Name + "_UpdatedControl - " + ex.Message);
            }
            finally
            {


            }
        }




        private void 部品使用先_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (Application.OpenForms["F_製品管理"] != null)
            {
                MessageBox.Show("[ユニット]画面が既に開かれているため実行できません。", BASE_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            if (部品使用先.SelectedRows.Count > 0)
            {
                F_ユニット targetform = new F_ユニット();

                string strCode = 部品使用先.SelectedRows[0].Cells[0].Value.ToString();
                string strEdition = 部品使用先.SelectedRows[0].Cells[1].Value.ToString();

                targetform.args = strCode + "," + strEdition;
                targetform.MdiParent = this.MdiParent;
                targetform.FormClosed += (s, args) => { this.Enabled = true; };
                this.Enabled = false;

                targetform.Show();

            }
            else
            {
                MessageBox.Show("使用しているユニットはありません。", BASE_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }




        }

        private void 部品集合参照ボタン_Click(object sender, EventArgs e)
        {
            if (IsIncluded)
            {
                F_部品集合 targetform = new F_部品集合();

                targetform.args = 部品集合コード.Text + "," + 部品集合版数.Text;
                targetform.MdiParent = this.MdiParent;
                targetform.FormClosed += (s, args) => { this.Enabled = true; };
                this.Enabled = false;

                targetform.Show();

            }
            else
            {
                MessageBox.Show("この部品は部品集合に構成されていません。", BASE_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
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
                    switch (this.ActiveControl.Name)
                    {
                        case "備考":
                            return;

                        case "部品コード":
                            SelectNextControl(ActiveControl, true, true, true, true);
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
                        コマンド新規_Click(sender, e);
                    }
                    break;
                case Keys.F2:
                    if (コマンド読込.Enabled)
                    {
                        コマンド読込.Focus();
                        コマンド読込_Click(sender, e);
                    }
                    break;
                case Keys.F3:
                    if (コマンド複写.Enabled) コマンド複写_Click(sender, e);
                    break;
                case Keys.F4:
                    if (コマンド削除.Enabled) コマンド削除_Click(sender, e);
                    break;
                case Keys.F5:
                    if (コマンド仕入先.Enabled) コマンド仕入先_Click(sender, e);
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

        private void CalcInventoryCode_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void CalcInventoryCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void CalcInventoryCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                ComboBox comboBox = sender as ComboBox;
                if (comboBox != null)
                {
                    comboBox.DroppedDown = true;
                }
                e.Handled = true;
            }
        }

        private void ChemSherpaVersion_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;

        }

        private void ChemSherpaVersion_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void JampAis_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void JampAis_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void JampAis_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                ComboBox comboBox = sender as ComboBox;
                if (comboBox != null)
                {
                    comboBox.DroppedDown = true;
                }
                e.Handled = true;
            }
        }

        private void Rohs1ChemSherpaStatusCode_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void Rohs1ChemSherpaStatusCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void Rohs1ChemSherpaStatusCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                ComboBox comboBox = sender as ComboBox;
                if (comboBox != null)
                {
                    comboBox.DroppedDown = true;
                }
                e.Handled = true;
            }
        }

        private void Rohs2ChemSherpaStatusCode_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void Rohs2ChemSherpaStatusCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void Rohs2ChemSherpaStatusCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                ComboBox comboBox = sender as ComboBox;
                if (comboBox != null)
                {
                    comboBox.DroppedDown = true;
                }
                e.Handled = true;
            }
        }

        private void Rohs2DocumentStatusCode_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void Rohs2DocumentStatusCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void Rohs2DocumentStatusCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                ComboBox comboBox = sender as ComboBox;
                if (comboBox != null)
                {
                    comboBox.DroppedDown = true;
                }
                e.Handled = true;
            }
        }

        private void Rohs2JampAisStatusCode_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void Rohs2JampAisStatusCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void Rohs2JampAisStatusCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                ComboBox comboBox = sender as ComboBox;
                if (comboBox != null)
                {
                    comboBox.DroppedDown = true;
                }
                e.Handled = true;
            }
        }

        private void Rohs2NonInclusionCertificationStatusCode_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void Rohs2NonInclusionCertificationStatusCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void Rohs2NonInclusionCertificationStatusCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                ComboBox comboBox = sender as ComboBox;
                if (comboBox != null)
                {
                    comboBox.DroppedDown = true;
                }
                e.Handled = true;
            }
        }

        private void Rohs2ProvisionalRegisteredStatusCode_Validated(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void RoHS資料_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void RoHS資料_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void RoHS資料_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                ComboBox comboBox = sender as ComboBox;
                if (comboBox != null)
                {
                    comboBox.DroppedDown = true;
                }
                e.Handled = true;
            }
        }

        private void ShelfNumber_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void ShelfNumber_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void ShelfNumber_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 10);
            ChangedData(true);
        }

        private void StandardDeliveryDay_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void StandardDeliveryDay_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void StandardDeliveryDay_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void メーカーコード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (メーカーコード.Modified == false) return;
            if (IsError(sender as Control) == true)
            {
                メーカーコード.Undo();
                e.Cancel = true;
            }
        }

        private void メーカーコード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 8);
            ChangedData(true);
        }

        private void メーカーコード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                TextBox textBox = (TextBox)sender;
                string formattedCode = textBox.Text.Trim().PadLeft(8, '0');

                if (formattedCode != textBox.Text || string.IsNullOrEmpty(textBox.Text))
                {
                    textBox.Text = formattedCode;
                    UpdatedControl(sender as Control);
                }
            }
        }

        private void メーカーコード_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                メーカーコード検索ボタン_Click(sender, e);
                e.Handled = true; // イベントの処理が完了したことを示す
            }
        }

        private void ロス率_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void ロス率_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void ロス率_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 型番_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 型番_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 50);
            ChangedData(true);
        }

        private void 形状分類コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 形状分類コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 形状分類コード_KeyPress(object sender, KeyPressEventArgs e)
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

        private void InventoryAmount_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void InventoryAmount_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            DialogResult result = MessageBox.Show("在庫数量はシステムにより更新されます。\n通常、ユーザーが更新することはありません。\n\n更新しますか？", "在庫数量", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
            {
                e.Cancel = true;
            }
            else
            {
                if (IsError(sender as Control) == true) e.Cancel = true;
            }
        }

        private void InventoryAmount_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 仕入先1コード_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 仕入先1コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 仕入先1コード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 8);
            ChangedData(true);
        }

        private void 仕入先1コード_Enter(object sender, EventArgs e)
        {
            selected_frame = 1;
            toolStripStatusLabel2.Text = "■仕入先コードを入力します。　■8文字まで入力可。　■[space]キーで検索ウィンドウを開きます。";
        }

        private void 仕入先1コード_Leave(object sender, EventArgs e)
        {

            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 仕入先1コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Control control = (Control)sender;
                string strCode = control.Text.Trim();

                if (string.IsNullOrEmpty(strCode))
                {
                    return;
                }

                strCode = strCode.PadLeft(8, '0');

                if (strCode != control.Text)
                {
                    control.Text = strCode;
                    UpdatedControl(sender as Control);
                }
            }
        }

        private void 仕入先1コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                e.Handled = true;
                仕入先1コード検索ボタン_Click(sender, e);
            }


        }


        private void 仕入先2コード_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 仕入先2コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 仕入先2コード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 8);
            ChangedData(true);
        }

        private void 仕入先2コード_Enter(object sender, EventArgs e)
        {
            selected_frame = 2;
            toolStripStatusLabel2.Text = "■仕入先コードを入力します。　■8文字まで入力可。　■[space]キーで検索ウィンドウを開きます。";
        }

        private void 仕入先2コード_Leave(object sender, EventArgs e)
        {

            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 仕入先2コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Control control = (Control)sender;
                string strCode = control.Text.Trim();

                if (string.IsNullOrEmpty(strCode))
                {
                    return;
                }

                strCode = strCode.PadLeft(8, '0');

                if (strCode != control.Text)
                {
                    control.Text = strCode;
                    UpdatedControl(sender as Control);
                }
            }
        }

        private void 仕入先2コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                e.Handled = true;
                仕入先2コード検索ボタン_Click(sender, e);
            }
        }

        private void 仕入先3コード_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 仕入先3コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 仕入先3コード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 8);
            ChangedData(true);
        }

        private void 仕入先3コード_Enter(object sender, EventArgs e)
        {
            selected_frame = 3;
            toolStripStatusLabel2.Text = "■仕入先コードを入力します。　■8文字まで入力可。　■[space]キーで検索ウィンドウを開きます。";
        }

        private void 仕入先3コード_Leave(object sender, EventArgs e)
        {

            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 仕入先3コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                Control control = (Control)sender;
                string strCode = control.Text.Trim();

                if (string.IsNullOrEmpty(strCode))
                {
                    return;
                }

                strCode = strCode.PadLeft(8, '0');

                if (strCode != control.Text)
                {
                    control.Text = strCode;
                    UpdatedControl(sender as Control);
                }
            }
        }

        private void 仕入先3コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Space)
            {
                e.Handled = true;
                仕入先3コード検索ボタン_Click(sender, e);
            }
        }


        private void 仕入先1コード検索ボタン_Click(object sender, EventArgs e)
        {
            SearchForm = new F_検索();
            SearchForm.FilterName = "仕入先名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                string SelectedCode = SearchForm.SelectedCode;

                仕入先1コード.Text = SelectedCode;
                UpdatedControl(仕入先1コード);
            }
        }

        private void 仕入先2コード検索ボタン_Click(object sender, EventArgs e)
        {
            SearchForm = new F_検索();
            SearchForm.FilterName = "仕入先名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                string SelectedCode = SearchForm.SelectedCode;

                仕入先2コード.Text = SelectedCode;
                UpdatedControl(仕入先2コード);
            }
        }

        private void 仕入先3コード検索ボタン_Click(object sender, EventArgs e)
        {
            SearchForm = new F_検索();
            SearchForm.FilterName = "仕入先名フリガナ";
            if (SearchForm.ShowDialog() == DialogResult.OK)
            {
                string SelectedCode = SearchForm.SelectedCode;

                仕入先3コード.Text = SelectedCode;
                UpdatedControl(仕入先3コード);
            }
        }

        private void 仕入先1フレーム_Enter(object sender, EventArgs e)
        {
            仕入先1コード.Focus();
        }

        private void 仕入先2フレーム_Enter(object sender, EventArgs e)
        {
            仕入先2コード.Focus();
        }

        private void 仕入先3フレーム_Enter(object sender, EventArgs e)
        {
            仕入先3コード.Focus();

        }

        private void 仕入先1単価_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 仕入先1単価_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 仕入先1単価_Enter(object sender, EventArgs e)
        {
            selected_frame = 1;
            toolStripStatusLabel2.Text = "単価が不明であるときは何も入力しないでください。　■数値全体で8桁まで、少数部は2桁まで入力できます。";
        }

        private void 仕入先1単価_Leave(object sender, EventArgs e)
        {

            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 仕入先2単価_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 仕入先2単価_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 仕入先2単価_Enter(object sender, EventArgs e)
        {
            selected_frame = 2;
            toolStripStatusLabel2.Text = "単価が不明であるときは何も入力しないでください。　■数値全体で8桁まで、少数部は2桁まで入力できます。";
        }

        private void 仕入先2単価_Leave(object sender, EventArgs e)
        {

            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 仕入先3単価_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 仕入先3単価_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 仕入先3単価_Enter(object sender, EventArgs e)
        {
            selected_frame = 3;
            toolStripStatusLabel2.Text = "単価が不明であるときは何も入力しないでください。　■数値全体で8桁まで、少数部は2桁まで入力できます。";
        }

        private void 仕入先3単価_Leave(object sender, EventArgs e)
        {

            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 受入検査ランク_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 受入検査ランク_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 受入検査ランク_KeyPress(object sender, KeyPressEventArgs e)
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

        private void 単位数量_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 単位数量_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 単位数量_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 入数_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 入数_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 入数_TextChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 廃止_Validated(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 非含有証明書_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 非含有証明書_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 非含有証明書_KeyPress(object sender, KeyPressEventArgs e)
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

        private void 備考_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 備考_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 4000);
            ChangedData(true);
        }

        private void 品名_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (品名.Modified == false) return;
            if (IsError(sender as Control) == true) e.Cancel = true;
        }

        private void 品名_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 50);
            ChangedData(true);
        }

        private void 部品コード_Validated(object sender, EventArgs e)
        {
            UpdatedControl(sender as Control);
        }

        private void 部品コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (部品コード.Text == 変更前部品コード) return;

            if (IsError(sender as Control) == true)
            {
                e.Cancel = true;
                部品コード.Text = 変更前部品コード;
            }
        }

        private void 部品コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control, 8);
        }

        private void 部品コード_KeyDown(object sender, KeyEventArgs e)
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
                            部品コード_Validated(sender, e);
                        }
                    }
                }
            }
        }


        private void 分類コード_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (IsError(sender as Control) == true) e.Cancel = true;
            bool isPartialMatch = 分類コード.Items.Cast<DataRowView>()
            .Any(item => item.Row.Field<string>("Display").IndexOf(分類コード.Text, StringComparison.OrdinalIgnoreCase) >= 0);

            if (!isPartialMatch)
            {
                MessageBox.Show("その値はリストにありません", "", MessageBoxButtons.OK, MessageBoxIcon.Error);
                分類コード.Text = tmpstr; // 入力を元に戻す
                e.Cancel = true;
            }
            GroupName.Text = ((DataRowView)分類コード.SelectedItem)?.Row.Field<String>("Display2")?.ToString();
        }

        private void 分類コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            GroupName.Text = ((DataRowView)分類コード.SelectedItem)?.Row.Field<String>("Display2")?.ToString();
            ChangedData(true);
        }

        private void 分類コード_TextChanged(object sender, EventArgs e)
        {
            if (分類コード.SelectedValue == null)
            {
                GroupName.Text = null;
            }

            //var matchingItem = 分類コード.Items.Cast<DataRowView>().FirstOrDefault(item => item.Row.Field<String>("Display") == 分類コード.Text);
            //if (matchingItem != null)
            //{
            //    // 一致する項目があれば、そのDisplay2の値をテキストボックスに設定
            //    GroupName.Text = matchingItem.Row.Field<String>("Display2");
            //}
            //else
            //{
            //    // 一致する項目がない場合は、テキストボックスをクリア
            //    GroupName.Text = "";
            //}
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

        private void 分類コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            OriginalClass.SetComboBoxAppearance((ComboBox)sender, e, new int[] { 50, 550 }, new string[] { "Display", "Display2" });
            分類コード.Invalidate();
            分類コード.DroppedDown = true;
        }

        string 変更前部品コード;
        private void 部品コード_Enter(object sender, EventArgs e)
        {
            変更前部品コード = 部品コード.Text;
            selected_frame = 0;
            toolStripStatusLabel2.Text = "■読み込む部品データの部品コードを入力します。";
        }

        private void 部品コード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 品名_Enter(object sender, EventArgs e)
        {
            selected_frame = 0;
            toolStripStatusLabel2.Text = "■全角で25文字まで入力可。　■機種依存文字は入力できません。";
        }

        private void 品名_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 型番_Enter(object sender, EventArgs e)
        {
            selected_frame = 0;
            toolStripStatusLabel2.Text = "■半角50文字まで入力可。　■機種依存文字は入力できません。";
        }

        private void 型番_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void メーカーコード_Enter(object sender, EventArgs e)
        {
            selected_frame = 0;
            toolStripStatusLabel2.Text = "■メーカーコードを入力します。　■8文字まで入力可。　■[space]キーで検索ウィンドウを開きます。";
        }

        private void メーカーコード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private string tmpstr = "";
        private void 分類コード_Enter(object sender, EventArgs e)
        {
            selected_frame = 0;
            tmpstr = 分類コード.Text;
            toolStripStatusLabel2.Text = "■[space]キーでドロップダウンリストを開きます。";
        }

        private void 分類コード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 形状分類コード_Enter(object sender, EventArgs e)
        {
            selected_frame = 0;
            toolStripStatusLabel2.Text = "■[space]キーでドロップダウンリストを開きます。";
        }

        private void 形状分類コード_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void JampAis_Enter(object sender, EventArgs e)
        {
            selected_frame = 0;
            toolStripStatusLabel2.Text = "■在庫部品について、RoHSの対応状況を入力します。　■[space]キーでドロップダウンリストを表示します。";
        }

        private void JampAis_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 非含有証明書_Enter(object sender, EventArgs e)
        {
            selected_frame = 0;
            toolStripStatusLabel2.Text = "■RoHS指令に基づく非含有証明書の進捗状況を選択します。　■[space]キーでドロップダウンリストを表示します。";
        }

        private void 非含有証明書_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void RoHS資料_Enter(object sender, EventArgs e)
        {
            selected_frame = 0;
            toolStripStatusLabel2.Text = "■RoHSに関する資料の有無を入力します。　■[space]キーでドロップダウンリストを表示します。";
        }

        private void RoHS資料_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void Rohs1ChemSherpaStatusCode_Enter(object sender, EventArgs e)
        {
            selected_frame = 0;
            toolStripStatusLabel2.Text = "■RoHS1のchemSHERPAデータの入手状況を入力します。　■[space]キーでドロップダウンリストを表示します。";
        }
        private void Rohs1ChemSherpaStatusCode_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void Rohs2JampAisStatusCode_Enter(object sender, EventArgs e)
        {
            selected_frame = 0;
            toolStripStatusLabel2.Text = "■在庫部品について、RoHSの対応状況を入力します。　■[space]キーでドロップダウンリストを表示します。";
        }

        private void Rohs2JampAisStatusCode_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void Rohs2NonInclusionCertificationStatusCode_Enter(object sender, EventArgs e)
        {
            selected_frame = 0;
            toolStripStatusLabel2.Text = "■RoHS指令に基づく非含有証明書の進捗状況を選択します。　■[space]キーでドロップダウンリストを表示します。";
        }

        private void Rohs2NonInclusionCertificationStatusCode_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void Rohs2DocumentStatusCode_Enter(object sender, EventArgs e)
        {
            selected_frame = 0;
            toolStripStatusLabel2.Text = "■RoHSに関する資料の有無を入力します。　■[space]キーでドロップダウンリストを表示します。";
        }

        private void Rohs2DocumentStatusCode_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void Rohs2ChemSherpaStatusCode_Enter(object sender, EventArgs e)
        {
            selected_frame = 0;
            toolStripStatusLabel2.Text = "■RoHS2のchemSHERPAデータの入手状況を入力します。　■[space]キーでドロップダウンリストを表示します。";
        }

        private void Rohs2ChemSherpaStatusCode_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void ChemSherpaVersion_Enter(object sender, EventArgs e)
        {
            selected_frame = 0;
            toolStripStatusLabel2.Text = "■入手したchemSHERPAのバージョンを入力します。　■10文字まで入力可。";
        }

        private void ChemSherpaVersion_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 入数_Enter(object sender, EventArgs e)
        {
            selected_frame = 0;
            toolStripStatusLabel2.Text = "■含まれる部品の数量です。購買時にこの値によって除算されます。";
        }

        private void 入数_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 単位数量_Enter(object sender, EventArgs e)
        {
            selected_frame = 0;
            toolStripStatusLabel2.Text = "■発注可能な最低数量です。";
        }

        private void 単位数量_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void StandardDeliveryDay_Enter(object sender, EventArgs e)
        {
            selected_frame = 0;
            toolStripStatusLabel2.Text = "■部品の標準納期を入力します。単位は[日]で入力してください。";
        }

        private void StandardDeliveryDay_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void ShelfNumber_Enter(object sender, EventArgs e)
        {
            selected_frame = 0;
            toolStripStatusLabel2.Text = "■部品が保管されている棚番号です。";
        }

        private void ShelfNumber_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void CalcInventoryCode_Enter(object sender, EventArgs e)
        {
            selected_frame = 0;
            toolStripStatusLabel2.Text = "■[space]キーでドロップダウンリストを表示します。";
        }

        private void CalcInventoryCode_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void ロス率_Enter(object sender, EventArgs e)
        {
            selected_frame = 0;
            toolStripStatusLabel2.Text = "■在庫のロス率を係数で入力します。　■少数部は7桁まで入力できますが、表示は3桁です。";
        }

        private void ロス率_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 受入検査ランク_Enter(object sender, EventArgs e)
        {
            selected_frame = 0;
            toolStripStatusLabel2.Text = "■[space]キーでドロップダウンリストを表示します。";
        }

        private void 受入検査ランク_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void 備考_Enter(object sender, EventArgs e)
        {
            selected_frame = 0;
            toolStripStatusLabel2.Text = "■全角2000文字まで入力できます。";
        }

        private void 備考_Leave(object sender, EventArgs e)
        {
            toolStripStatusLabel2.Text = "各種項目の説明";
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawLine(Pens.Black, new Point(5, 0), new Point(19, 0));
            e.Graphics.DrawLine(Pens.Black, new Point(19, 0), new Point(19, 254));
            e.Graphics.DrawLine(Pens.Black, new Point(5, 254), new Point(19, 254));
        }

        private void Rohs2ProvisionalRegisteredStatusCode_CheckedChanged(object sender, EventArgs e)
        {
            ChangedData(true);
        }

        private void 部品コード_TextChanged(object sender, EventArgs e)
        {
            FunctionClass.LimitText(sender as Control,9);
        }
    }
}
