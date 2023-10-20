using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using u_net.Public;


namespace u_net
{
    public partial class F_メーカー : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        private string BASE_CAPTION = "メーカー"; 
        public F_メーカー()
        {
            this.Text = "メーカー";       // ウィンドウタイトルを設定
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
                this.SuspendLayout();
                //DoWait("しばらくお待ちください...");

                //IntPtr hIcon = LoadIconFromPath(CurrentProject.Path + "\\card.ico");
                //SendMessage(this.Handle, WM_SETICON, new IntPtr(1), hIcon);

                //object varOpenArgs = this.OpenArgs;

                int intWindowHeight = this.Height;
                int intWindowWidth = this.Width;

                string code = null;

                if (string.IsNullOrEmpty(code))
                {
                    // 新規モードへ
                    //if (!GoNewMode())
                    //{
                    //    throw new Exception("初期化に失敗しました。");
                    //}
                }
                else
                {
                    // 修正モードへ
                    if (!GoModifyMode())
                    {
                        throw new Exception("初期化に失敗しました。");
                    }
                    //this.メーカーコード.Text = varOpenArgs.ToString();
                    // コードを設定したことでイベント発生
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
            }
        }


        private bool GoNewMode()
        {
            try
            {
                // 各コントロール値を初期化
                //SetControls(this, null);

                CommonConnect();

                string code = FunctionClass.GetNewCode(cn, CommonConstants.CH_MAKER);
                this.メーカーコード.Text = code.Substring(code.Length - 8);
                // this.メーカーコード.Text = 採番(objConnection, CH_MAKER).Substring(採番(objConnection, CH_MAKER).Length - 8);
                this.Revision.Text = 1.ToString();

                // 編集による変更がない状態へ遷移する
                ChangedData(false);

                // ヘッダ部動作制御
                FunctionClass.LockData(this, false);
                this.メーカー名.Focus();
                this.メーカーコード.Enabled = false;
                this.コマンド新規.Enabled = false;
                this.コマンド読込.Enabled = true;
                this.コマンド複写.Enabled = false;
                this.コマンド削除.Enabled = false;
                this.コマンドメール.Enabled = false;
                // this.コマンド承認.Enabled = false;
                // this.コマンド確定.Enabled = false;
                this.コマンド登録.Enabled = false;

                return true;
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_GoNewMode - " + ex.Message);
                return false;
            }
        }

        public void ChangedData(bool isChanged)
        {
            try
            {
                if (isChanged)
                {
                    this.Text = BASE_CAPTION + "*";
                }
                else
                {
                    this.Text = BASE_CAPTION;
                }

                // キー情報を表示するコントロールを制御
                // コードにフォーカスがある状態でサブフォームから呼び出されたときの対処
                if (this.ActiveControl == this.メーカーコード)
                {
                    this.メーカー名.Focus();
                }
                this.メーカーコード.Enabled = !isChanged;
                this.コマンド複写.Enabled = !isChanged;
                this.コマンド削除.Enabled = !isChanged;
                this.コマンドメール.Enabled = !isChanged;
                this.コマンド登録.Enabled = isChanged;
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_ChangedData - " + ex.Message);
                // エラー処理が必要に応じて実装
            }
        }


        private bool GoModifyMode()
        {
            try
            {
                bool result = false;

                // 各コントロールの値をクリア
                //SetControls(this, null);

                // 編集による変更がない状態へ遷移
                ChangedData(false);

                this.メーカーコード.Enabled = true;
                this.メーカーコード.Focus();
                // メーカーコードコントロールが使用可能になってから LockData を呼び出す
                FunctionClass.LockData(this, true, "メーカーコード");
                this.コマンド新規.Enabled = true;
                this.コマンド読込.Enabled = false;
                this.コマンド複写.Enabled = false;
                // this.コマンド確定.Enabled = false;
                this.コマンド登録.Enabled = false;

                result = true;
                return result;
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_GoModifyMode - " + ex.Message);
                return false;
            }
        }

        private void MyForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {

                // 変更された場合
                if (IsChanged)
                {
                    var intRes = MessageBox.Show("変更内容を登録しますか？", BASE_CAPTION, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    switch (intRes)
                    {
                        case DialogResult.Yes:
                            if (SaveData(CurrentCode, CurrentRevision))
                            {
                                // 保存に成功した場合
                                return;
                            }
                            else
                            {
                                if (MessageBox.Show("登録できませんでした。" + Environment.NewLine +
                                    "強制終了しますか？" + Environment.NewLine +
                                    "[はい]を選択した場合、メーカーコードは破棄されます。" + Environment.NewLine +
                                    "[いいえ]を選択した場合、終了しません。", BASE_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                                {
                                    e.Cancel = false;
                                }
                                else
                                {
                                    e.Cancel = true;
                                }
                                return;
                            }
                        case DialogResult.No:
                            // 新規モードのときはコードを戻す
                            break;
                        case DialogResult.Cancel:
                            e.Cancel = true;
                            return;
                    }
                }

                // 新規モードのときに登録しない場合は内部の更新データを元に戻す
                if (IsNewData)
                {
                    if (!string.IsNullOrEmpty(CurrentCode) && CurrentRevision == 1)
                    {

                        CommonConnect();

                        // 初版データのときのみ採番された番号を戻す
                        if (!ReturnNewCode(cn, CommonConstants.CH_MAKER, CurrentCode))
                        {
                            MessageBox.Show("エラーのためコードは破棄されました。" + Environment.NewLine +
                                "メーカーコード　：　" + CurrentCode, BASE_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_FormClosing - " + ex.Message);
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
                return string.IsNullOrEmpty(メーカーコード.Text) ? "" : メーカーコード.Text;
            }
        }

        public int CurrentRevision
        {
            get
            {
                return string.IsNullOrEmpty(Revision.Text) ? 0 : int.Parse(Revision.Text);
            }
        }

        private bool SaveData(string SaveCode, int SaveEdition = -1)
        {
            try
            {
                DateTime now = DateTime.Now;
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
                object varSaved7 = null;

                bool isNewData = IsNewData;

                if (isNewData)
                {
                    objControl1 = 作成日時;
                    objControl2 = 作成者コード;
                    objControl3 = 作成者名;
                    varSaved1 = objControl1.Text;
                    varSaved2 = objControl2.Text;
                    varSaved3 = objControl3.Text;
                    varSaved7 = ActiveDate.Text;
                    objControl1.Text = now.ToString();
                    //objControl2.Text = LoginUserCode;
                    //objControl3.Text = LoginUserFullName;
                    ActiveDate.Text = now.ToString();
                }

                objControl4 = 更新日時;
                objControl5 = 更新者コード;
                objControl6 = 更新者名;

                // 登録前の状態を退避しておく
                varSaved4 = objControl4.Text;
                varSaved5 = objControl5.Text;
                varSaved6 = objControl6.Text;

                // 値の設定
                objControl4.Text = now.ToString();
                //objControl5.Text = LoginUserCode;
                //objControl6.Text = LoginUserFullName;

                // 登録処理
                if (RegTrans(SaveCode, SaveEdition))
                {
                    // 登録成功
                    return true;
                }
                else
                {
                    // 登録失敗
                    if (isNewData)
                    {
                        objControl1.Text = (string)varSaved1;
                        objControl2.Text = (string)varSaved2;
                        objControl3.Text = (string)varSaved3;
                        ActiveDate.Text = (string)varSaved7;
                    }

                    objControl4.Text = (string)varSaved4;
                    objControl5.Text = (string)varSaved5;
                    objControl6.Text = (string)varSaved6;
                }

                return false;
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_SaveData - " + ex.Message);
                return false;
            }
        }

        public bool RegTrans(string codeString, int editionNumber = -1)
        {
            try
            {
                bool success = false;
                string strKey = "";

                Connect();

                using (SqlTransaction trans = cn.BeginTransaction())
                {
                    try
                    {
                        // ヘッダ部の登録
                        if (SaveHeader(this, codeString, editionNumber))
                        {
                            trans.Commit(); // トランザクション完了
                            success = true;
                        }
                        else
                        {
                            trans.Rollback(); // 変更をキャンセル
                        }
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback(); // 変更をキャンセル
                        throw ex;
                    }
                }

                return success;
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close(); // データベース接続を閉じる
                }

                Debug.Print(this.Name + "_RegTrans - " + ex.Message);
                return false;
            }
        }

        public bool SaveHeader(Form formObject, string codeString, int editionNumber = -1)
        {
            try
            {
                bool success = false;

                Connect();

                using (SqlTransaction trans = cn.BeginTransaction())
                {
                    try
                    {
                        string strKey = (editionNumber == -1) ? "メーカーコード = @CodeString" : "メーカーコード = @CodeString AND Revision = @EditionNumber";
                        string strSQL = "SELECT * FROM Mメーカー WHERE " + strKey;

                        using (SqlCommand cmd = new SqlCommand(strSQL, cn, trans))
                        {
                            cmd.Parameters.AddWithValue("@CodeString", codeString);
                            if (editionNumber != -1)
                            {
                                cmd.Parameters.AddWithValue("@EditionNumber", editionNumber);
                            }

                            using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                            {
                                DataTable dataTable = new DataTable();
                                adapter.Fill(dataTable);

                                if (dataTable.Rows.Count == 0)
                                {
                                    // 新しいレコードを追加
                                    DataRow newRow = dataTable.NewRow();
                                    FunctionClass.SetForm2Table(formObject, newRow, "", "");
                                    dataTable.Rows.Add(newRow);
                                }
                                else
                                {
                                    // 既存のレコードを更新
                                    FunctionClass.SetForm2Table(formObject, dataTable.Rows[0], "メーカーコード", "Revision");
                                }

                                // データベースに変更を保存
                                using (SqlCommandBuilder builder = new SqlCommandBuilder(adapter))
                                {
                                    adapter.Update(dataTable);
                                }
                            }
                        }

                        trans.Commit(); // トランザクション完了
                        success = true;
                    }
                    catch (Exception ex)
                    {
                        trans.Rollback(); // 変更をキャンセル
                        throw ex;
                    }
                }

                return success;
            }
            catch (Exception ex)
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close(); // データベース接続を閉じる
                }

                Debug.Print(this.Name + "_SaveHeader - " + ex.Message);
                return false;
            }
        }



        //private void コマンド新規_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        if (this.ActiveControl == this.コマンド新規)
        //        {
        //            if (previousControl != null)
        //            {
        //                previousControl.Focus();
        //            }
        //        }

        //        // 変更があるときは登録確認を行う
        //        if (this.コマンド登録.Enabled)
        //        {
        //            var Res = MessageBox.Show("変更内容を登録しますか？", "新規コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

        //            switch (Res)
        //            {
        //                case DialogResult.Yes:

        //                    if (!ErrCheck()) return;
        //                    // 登録処理
        //                    if (!SaveData())
        //                    {
        //                        MessageBox.Show("登録できませんでした。", "新規コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //                        return;
        //                    }

        //                    break;
        //                case DialogResult.Cancel:
        //                    return;
        //            }
        //        }
        //        // 新規モードへ移行
        //        if (!GoNewMode())
        //        {
        //            goto Err_コマンド新規_Click;
        //        }
        //        return;

        //    Err_コマンド新規_Click:
        //        MessageBox.Show("エラーのため新規モードへ移行できません。", "新規コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

        //    }
        //    catch (Exception ex)
        //    {
        //        // 例外処理
        //        Debug.Print(this.Name + "_コマンド新規 - " + ex.Message);
        //        MessageBox.Show("エラーのため新規モードへ移行できません。", "新規コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //    }
        //}

        //private void コマンド修正_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        // データに変更があった場合の処理
        //        if (this.コマンド登録.Enabled)
        //        {

        //            var res = MessageBox.Show("変更内容を登録しますか？", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

        //            switch (res)
        //            {
        //                case DialogResult.Yes:

        //                    if (!ErrCheck()) return;
        //                    this.DoubleBuffered = false;

        //                    if (!SaveData())
        //                    {
        //                        MessageBox.Show("エラーのため登録できませんでした。", "修正コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //                    }
        //                    this.DoubleBuffered = true;
        //                    break;

        //                case DialogResult.No:
        //                    // 新規モードのときに登録しない場合はコードを戻す
        //                    if (this.コマンド新規.Enabled)
        //                    {
        //                        Connect();
        //                        if (!FunctionClass.ReturnCode(cn, "ITM" + this.メーカーコード.Text))
        //                        {
        //                            MessageBox.Show("エラーのためコードは破棄されました。\n\nメーカーコード： " + this.メーカーコード.Text, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //                        }
        //                    }
        //                    break;

        //                case DialogResult.Cancel:
        //                    return;
        //            }
        //        }
        //        else
        //        {
        //            // 新規モードのときに変更がない場合はコードを戻す
        //            if (this.コマンド新規.Enabled)
        //            {
        //                if (!FunctionClass.ReturnCode(cn, "ITM" + this.メーカーコード.Text))
        //                {
        //                    MessageBox.Show("エラーのためコードは破棄されました。\n\nメーカーコード： " + this.メーカーコード.Text, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //                }
        //            }
        //        }

        //        // 修正モードへ移行する
        //        if (!GoModifyMode())
        //        {
        //            // 移行に失敗した場合の処理
        //            Debug.Print(this.Name + "_コマンド修正_Click - Error");
        //            if (MessageBox.Show("エラーが発生しました。\n\n管理者に連絡してください。\n\n強制終了しますか？", "修正コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
        //            {
        //                this.Close();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Debug.Print(this.Name + "_コマンド修正_Click - " + ex.Message);
        //        MessageBox.Show("エラーが発生しました。\n\n管理者に連絡してください。\n\n強制終了しますか？", "修正コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
        //        this.Close();
        //    }
        //}

        //private void コマンド複写_Click(object sender, EventArgs e)
        //{
        //    if (this.ActiveControl == this.コマンド複写)
        //    {
        //        if (previousControl != null)
        //        {
        //            previousControl.Focus();
        //        }
        //    }
        //    //新規採番したコードをメーカー明細にセット
        //    string original = FunctionClass.採番(cn, "ITM");
        //    string originalcode = original.Substring(original.Length - 8);

        //    if (CopyData(originalcode))
        //    {
        //        // ヘッダ部制御
        //        FunctionClass.LockData(this, false);
        //        メーカー名.Focus();
        //        メーカーコード.Enabled = false;
        //        コマンド新規.Enabled = false;
        //        コマンド読込.Enabled = true;
        //        コマンド複写.Enabled = false;
        //        コマンド削除.Enabled = false;
        //        // コマンド承認.Enabled = false;
        //        // コマンド確定.Enabled = false;
        //        コマンド登録.Enabled = true;
        //    }
        //    else
        //    {
        //        MessageBox.Show("エラーが発生しました。\n複写できません。", "複写コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        //        return;
        //    }
        //}

        //private bool CopyData(string codeString)
        //{
        //    try
        //    {
        //        // DataGridView内の各行にアクセス
        //        foreach (DataGridViewRow row in dataGridView1.Rows)
        //        {
        //            // 行が新しい行を示す場合など、データ行でない場合は無視
        //            if (!row.IsNewRow)
        //            {
        //                // メーカーコードカラムのセルを取得
        //                DataGridViewCell productCodeCell = row.Cells["dgvメーカーコード"]; // カラム名に応じて変更

        //                if (productCodeCell != null)
        //                {
        //                    // メーカーコードカラムのセルの値を新しいメーカーコードに変更
        //                    productCodeCell.Value = codeString;
        //                }
        //            }
        //        }
        //        // コントロールのフィールドを初期化
        //        メーカーコード.Text = codeString;
        //        作成日時.Text = null;
        //        作成者コード.Text = null;
        //        作成者名.Text = null;
        //        更新日時.Text = null;
        //        更新者コード.Text = null;
        //        更新者名.Text = null;
        //        削除.Text = null;

        //        //明細行のリセット
        //        detailNumber = 1;


        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        // エラーが発生した場合の処理
        //        MessageBox.Show("_CopyData - " + ex.Message);
        //        return false;
        //    }
        //}


        //private void コマンド削除_Click(object sender, EventArgs e)
        //{

        //}

        //private void コマンドシリーズ_Click(object sender, EventArgs e)
        //{

        //}

        //private void コマンド承認_Click(object sender, EventArgs e)
        //{
        //    if (ActiveControl == コマンド承認)
        //    {
        //        if (previousControl != null)
        //        {
        //            previousControl.Focus();
        //        }
        //    }
        //    MessageBox.Show("このコマンドは使用できません。", "承認コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //}

        //private void コマンド終了_Click(object sender, EventArgs e)
        //{
        //    this.Close();
        //}
        //private void コマンド確定_Click(object sender, EventArgs e)
        //{

        //}

        ////form_Loadの処理だと、グリッドビューがアクティブにならないので、グリッドビューにカーソルを持っていきたい時はこちら
        //private void F_メーカー_Shown(object sender, EventArgs e)
        //{
        //    //this.Activate();
        //    //this.ActiveControl = dataGridView1;
        //    //dataGridView1.CurrentCell = dataGridView1[1, new_cnt];
        //}


        //// コントロールがフォーカスを受け取ったとき、前回のフォーカスを記憶
        //private void Control_GotFocus(object sender, EventArgs e)
        //{

        //    previousControl = sender as Control;
        //}

        ////メーカー明細の型式番号と構成番号を設定する 同一のメーカーコード内での連番　と型式名ごとの番号
        //private bool SetModelNumber()
        //{
        //    try
        //    {
        //        //  DataTable table = this.uiDataSet.Mメーカー明細;
        //        int lngi = 1;
        //        // string 列名1 = dataGridView1.Columns["型式名"].Name;

        //        foreach (DataGridViewRow row in dataGridView1.Rows)
        //        {
        //            if (!row.IsNewRow)
        //            {
        //                string 型式名 = row.Cells["型式名DataGridViewTextBoxColumn"].Value as string;

        //                if (!string.IsNullOrEmpty(型式名) && 型式名 != "---")
        //                {
        //                    // データグリッドビューから値を取得してデータテーブル内の値を変更
        //                    dataGridView1.Rows[row.Index].Cells["型式番号DataGridViewTextBoxColumn"].Value = lngi;
        //                    dataGridView1.Rows[row.Index].Cells["構成番号DataGridViewTextBoxColumn"].Value = DBNull.Value;
        //                    lngi++;
        //                }
        //                else
        //                {
        //                    dataGridView1.Rows[row.Index].Cells["構成番号DataGridViewTextBoxColumn"].Value = lngi;
        //                }
        //            }
        //        }

        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("SetModelNumber Error: " + ex.Message);
        //        return false;
        //    }
        //}

        //public void ChangedData(bool dataChanged)
        //{
        //    if (dataChanged)
        //    {
        //        this.Text = this.Name + "*";
        //    }
        //    else
        //    {
        //        this.Text = this.Name;
        //    }

        //    if (this.ActiveControl == this.メーカーコード)
        //    {
        //        this.メーカー名.Focus();
        //    }

        //    this.メーカーコード.Enabled = !dataChanged;
        //    this.コマンド複写.Enabled = !dataChanged;
        //    this.コマンド削除.Enabled = !dataChanged;
        //    this.コマンド登録.Enabled = dataChanged;
        //}

        //private void F_メーカー_KeyDown(object sender, KeyEventArgs e)
        //{
        //    switch (e.KeyCode)
        //    {
        //        case Keys.Space: //コンボボックスならドロップダウン
        //            {
        //                Control activeControl = this.ActiveControl;
        //                if (activeControl is System.Windows.Forms.ComboBox)
        //                {
        //                    System.Windows.Forms.ComboBox activeComboBox = (System.Windows.Forms.ComboBox)activeControl;
        //                    activeComboBox.DroppedDown = true;
        //                }
        //            }
        //            break;
        //        case Keys.F1:
        //            if (コマンド新規.Enabled)
        //            {
        //                コマンド新規.Focus();
        //                コマンド新規_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
        //            }
        //            break;
        //        case Keys.F2:
        //            if (コマンド読込.Enabled)
        //            {
        //                コマンド読込.Focus();
        //                コマンド修正_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
        //            }
        //            break;
        //        case Keys.F3:
        //            if (コマンド複写.Enabled)
        //            {
        //                コマンド複写_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
        //            }
        //            break;
        //        case Keys.F4:
        //            if (コマンド削除.Enabled)
        //            {
        //                コマンド削除_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
        //            }
        //            break;
        //        case Keys.F5:
        //            if (コマンド仕入先.Enabled)
        //            {
        //                コマンドシリーズ_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
        //            }
        //            break;
        //        case Keys.F9:
        //            if (コマンド承認.Enabled)
        //            {
        //                コマンド承認_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
        //            }
        //            break;
        //        case Keys.F10:
        //            if (コマンド確定.Enabled)
        //            {
        //                コマンド確定_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
        //            }
        //            break;
        //        case Keys.F11:
        //            if (コマンド登録.Enabled)
        //            {
        //                コマンド登録.Focus();
        //                コマンド登録_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
        //            }
        //            break;
        //        case Keys.F12:
        //            if (コマンド終了.Enabled)
        //            {
        //                コマンド終了_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
        //            }
        //            break;
        //    }
        //}
        //private void 品名_TextChanged(object sender, EventArgs e)
        //{
        //    FunctionClass.LimitText(this.品名, 48);
        //    ChangedData(true);
        //}
        //private void 品名_Enter(object sender, EventArgs e)
        //{
        //    this.toolStripStatusLabel2.Text = "■受注時などに表示されるメーカーの品名です。　■全角２４文字まで入力できます。";
        //}

        //private void メーカー分類コード_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    if (this.メーカー分類コード.SelectedItem != null)
        //    {
        //        分類内容.Text = (メーカー分類コード.SelectedItem as DataRowView)["分類内容"].ToString();
        //    }
        //}

        //private void FlowCategoryCode_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    //MessageBox.Show(FlowCategoryCode.SelectedValue.ToString());

        //}

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    MessageBox.Show(メーカー分類コード.Text);
        //    MessageBox.Show(数量単位コード.Text);
        //    MessageBox.Show(メーカーコード.Text);
        //    MessageBox.Show(メーカーコード.SelectedValue.ToString());
        //}

        //private void メーカーコード_TextChanged(object sender, EventArgs e)
        //{

        //    //  this.vメーカーヘッダTableAdapter.Fill(this.uiDataSet.Vメーカーヘッダ, this.メーカーコード.Text);

        //}

        //private void label17_Click(object sender, EventArgs e)
        //{

        //}

        //private void label2_Click(object sender, EventArgs e)
        //{

        //}


    }


}
