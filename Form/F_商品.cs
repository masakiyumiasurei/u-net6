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

namespace u_net
{
    public partial class F_商品 : Form
    {
        private Control? previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args = "";
        //public string CurrentCode = "";
        private bool setCombo = false;
        public F_商品()
        {
            this.Text = "商品";       // ウィンドウタイトルを設定
            this.MaximizeBox = false;  // 最大化ボタンを無効化

            InitializeComponent();
        }
        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
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
                return string.IsNullOrEmpty(商品コード.Text) ? "" : 商品コード.Text;
            }
        }

        public int CurrentRevision
        {
            get
            {
                return string.IsNullOrEmpty(Revision.Text) ? 0 : int.Parse(Revision.Text);
            }
        }


        //public bool IsDeleted
        //{
        //    get
        //    {
        //        bool isEmptyOrDbNull = string.IsNullOrEmpty(this.削除日時.Text) || Convert.IsDBNull(this.削除日時.Text);

        //        return !isEmptyOrDbNull;
        //    }
        //}

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

            //this.combBox商品コードTableAdapter.Fill(this.uiDataSet.CombBox商品コード);
            //this.combBoxMシリーズTableAdapter.Fill(this.uiDataSet.Mシリーズ);
            //this.m商品分類TableAdapter.Fill(this.uiDataSet.M商品分類);
            //this.comboBox売上区分TableAdapter.Fill(this.uiDataSet.M売上区分);
            //this.M単位TableAdapter.Fill(this.uiDataSet.M単位);
            //this.comboBoxManufactureFlowTableAdapter.Fill(this.uiDataSet.ManufactureFlow);

            OriginalClass ofn = new OriginalClass();
            ofn.SetComboBox(数量単位コード, "SELECT 単位名 as Display,単位コード as Value FROM M単位");
            ofn.SetComboBox(シリーズコード, "SELECT シリーズ名 as Display,シリーズコード as Value FROM Mシリーズ");
            ofn.SetComboBox(商品分類コード, "SELECT 分類名 as Display,商品分類コード as Value FROM M商品分類");
            ofn.SetComboBox(売上区分コード, "SELECT 売上区分名 as Display,売上区分コード as Value FROM M売上区分");
            ofn.SetComboBox(FlowCategoryCode, "SELECT Name as Display,Code as Value FROM ManufactureFlow");
            setCombo = true;
            ofn.SetComboBox(商品コード, "SELECT M商品.商品コード as Display ,M商品.商品コード as Value FROM M商品 INNER JOIN M商品明細 ON M商品.商品コード = M商品明細.商品コード " +
                                        "GROUP BY M商品.商品コード ORDER BY M商品.商品コード DESC");

            setCombo = false;

            int intWindowHeight = this.Height;
            int intWindowWidth = this.Width;

            previousControl = null;

            try
            {
                this.SuspendLayout();

                if (string.IsNullOrEmpty(args))
                {
                    if (!GoNewMode())
                    {
                        return;
                    }
                }
                else
                {
                    if (!GoModifyMode())
                    {
                        return;
                    }
                    if (!string.IsNullOrEmpty(args))
                    {
                        this.商品コード.Text = args;
                    }
                    ChangedData(false);
                }

                fn.WaitForm.Close();

                //実行中フォーム起動              
                LocalSetting localSetting = new LocalSetting();
                localSetting.LoadPlace(CommonConstants.LoginUserCode, this);

            }
            catch (Exception ex)
            {
                ChangedData(false);
                MessageBox.Show("初期化に失敗しました。", "エラー");
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
                VariableSet.SetControls(this);
                string strSQL;
                Connect();

                //バインドソースの新規追加
                //this.M商品BindingSource.AddNew();

                string original = FunctionClass.採番(cn, "ITM");

                商品コード.Text = original.Substring(original.Length - 8);

                Revision.Text = "1";
                掛率有効.Checked = true;

                FlowCategoryCode.SelectedValue = "001";


                //何故かこれだけ値が入らない
                数量単位コード.SelectedValue = 1;
                //売上区分コード.SelectedValue = 1;


                CustomerSerialNumberRequired.Checked = false;
                IsUnit.Checked = false;
                Discontinued.Checked = false;

                // 明細部を初期化
                //this.M商品明細TableAdapter.Fill(this.uiDataSet.M商品明細, CurrentCode);
                //strSQL = "SELECT * FROM M商品明細 WHERE 商品コード='" + CurrentCode + "' ORDER BY 明細番号";
                //LoadDetails(strSQL, SubForm, dbWork, "商品明細");

                // ヘッダ部を制御
                FunctionClass.LockData(this, false);
                品名.Focus();
                商品コード.Enabled = false;
                コマンド新規.Enabled = false;
                コマンド修正.Enabled = true;
                コマンド複写.Enabled = false;
                コマンド削除.Enabled = false;
                コマンド承認.Enabled = false;
                コマンド確定.Enabled = false;
                コマンド登録.Enabled = false;

                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("GoNewMode - " + ex.Message);
                return false;
            }
        }

        private bool GoModifyMode()
        {
            try
            {
                VariableSet.SetControls(this);

                FunctionClass.LockData(this, true, "商品コード");
                this.商品コード.Enabled = true;
                this.商品コード.Focus();
                this.コマンド新規.Enabled = true;
                this.コマンド修正.Enabled = false;
                this.コマンド複写.Enabled = false;
                this.コマンド登録.Enabled = false;

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("修正Mode - " + ex.HResult + " : " + ex.Message);
                return false;
            }
        }

        private bool ErrCheck()
        {
            //入力確認    
            if (!FunctionClass.IsError(this.商品名)) return false;
            if (!FunctionClass.IsError(this.商品コード)) return false;
            if (!FunctionClass.IsError(this.売上区分コード)) return false;
            return true;
        }
        private void コマンド登録_Click(object sender, EventArgs e)
        {
            //保存確認
            if (MessageBox.Show("変更内容を保存しますか？", "保存確認",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) == DialogResult.OK)
            {
                if (!ErrCheck()) return;

                if (!SaveData()) return;
            }
        }

        private bool SaveData()
        {
            DateTime dtmNow;
            object varSaved1 = null;
            object varSaved2 = null;
            object varSaved3 = null;
            object varSaved4 = null;
            object varSaved5 = null;
            object varSaved6 = null;
            Control objControl1 = null;
            Control objControl2 = null;
            Control objControl3 = null;
            Control objControl4 = null;
            Control objControl5 = null;
            Control objControl6 = null;
            bool headerErr = false;
            Connect();
            dtmNow = FunctionClass.GetServerDate(cn);

            if (IsNewData)
            {
                objControl1 = 作成日時;
                objControl2 = 作成者コード;
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

            // 登録前の状態を退避しておく
            varSaved4 = objControl4.Text;
            varSaved5 = objControl5.Text;
            varSaved6 = objControl6.Text;

            // 値の設定
            objControl4.Text = dtmNow.ToString();
            objControl5.Text = CommonConstants.LoginUserCode;
            objControl6.Text = CommonConstants.LoginUserFullName;

            //管理情報の設定
            if (!SetModelNumber()) return false;



            SqlTransaction transaction = cn.BeginTransaction();
            {
                try
                {
                    // M商品データを保存
                    string strwhere = " 商品コード='" + this.商品コード.Text + "' and Revision=" + this.Revision.Text;

                    if (!DataUpdater.UpdateOrInsertDataFrom(this, cn, "M商品", strwhere, "商品コード", transaction))
                    {
                        //保存失敗の処理　　明細のエラーもあるのでcatchの所へやるか。。。
                        headerErr = true;
                        throw new Exception();

                    }

                    string sql = "DELETE FROM M商品明細 WHERE " + strwhere;
                    SqlCommand command = new SqlCommand(sql, cn, transaction);
                    command.ExecuteNonQuery();

                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            string 商品コード = row.Cells["dgv商品コード"].Value.ToString();
                            string Revision = row.Cells["dgvRevision"].Value.ToString();
                            string 明細番号 = row.Cells["dgv明細番号"].Value.ToString();
                            string 型式番号 = row.Cells["型式番号"].Value.ToString();
                            string 型式名 = row.Cells["型式名"].Value.ToString();
                            decimal 定価 = Convert.ToDecimal(row.Cells["定価"].Value); // 金額の場合、適切なデータ型に変換
                            decimal 原価 = Convert.ToDecimal(row.Cells["原価"].Value); // 金額の場合、適切なデータ型に変換
                            string 機能 = row.Cells["機能"].Value.ToString();
                            string 構成番号 = row.Cells["構成番号"].Value.ToString();

                            // データベースにデータを挿入
                            string insertSql = "INSERT INTO M商品明細 (商品コード, Revision, 明細番号, 型式番号, 型式名, 定価, 原価, 機能, 構成番号) " +
                                "VALUES (@商品コード, @Revision, @明細番号, @型式番号, @型式名, @定価, @原価, @機能, @構成番号)";


                            using (SqlCommand insertCommand = new SqlCommand(insertSql, cn, transaction))
                            {
                                insertCommand.Parameters.AddWithValue("@商品コード", 商品コード);
                                insertCommand.Parameters.AddWithValue("@Revision", Revision);
                                insertCommand.Parameters.AddWithValue("@明細番号", 明細番号);
                                insertCommand.Parameters.AddWithValue("@型式番号", 型式番号);
                                insertCommand.Parameters.AddWithValue("@型式名", 型式名);
                                insertCommand.Parameters.AddWithValue("@定価", 定価);
                                insertCommand.Parameters.AddWithValue("@原価", 原価);
                                insertCommand.Parameters.AddWithValue("@機能", 機能);
                                insertCommand.Parameters.AddWithValue("@構成番号", 構成番号);

                                insertCommand.ExecuteNonQuery();
                            }
                        }
                    }

                    // トランザクションをコミット
                    transaction.Commit();

                    // DataGridViewを更新して新しいデータを表示
                    // this.mshomeisaiTableAdapter.Fill(this.newDataSet.M商品明細, this.商品コード.Text);
                    // データベースへの変更を適用
                    // this.tableAdapterManager.UpdateAll(this.uiDataSet);

                    MessageBox.Show("登録を完了しました");

                    商品コード.Enabled = true;

                    // 新規モードのときは修正モードへ移行する
                    if (IsNewData)
                    {
                        コマンド新規.Enabled = true;
                        コマンド修正.Enabled = false;
                    }

                    コマンド複写.Enabled = true;
                    コマンド削除.Enabled = true;
                    コマンド登録.Enabled = false;

                    return true;

                }
                catch (Exception ex)
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

                    // トランザクション内でエラーが発生した場合、ロールバックを実行
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }

                    コマンド登録.Enabled = true;
                    if (headerErr)
                    {
                        MessageBox.Show("データの保存中にエラーが発生しました: "
                            + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    return false;

                }
            }
        }


        private void コマンド新規_Click(object sender, EventArgs e)
        {
            try
            {
                if (this.ActiveControl == this.コマンド新規)
                {
                    if (previousControl != null)
                    {
                        previousControl.Focus();
                    }
                }

                // 変更があるときは登録確認を行う
                if (this.コマンド登録.Enabled)
                {
                    var Res = MessageBox.Show("変更内容を登録しますか？", "新規コマンド", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    switch (Res)
                    {
                        case DialogResult.Yes:

                            if (!ErrCheck()) return;
                            // 登録処理
                            if (!SaveData())
                            {
                                MessageBox.Show("登録できませんでした。", "新規コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }

                            break;
                        case DialogResult.Cancel:
                            return;
                    }
                }
                // 新規モードへ移行
                if (!GoNewMode())
                {
                    goto Err_コマンド新規_Click;
                }
                return;

            Err_コマンド新規_Click:
                MessageBox.Show("エラーのため新規モードへ移行できません。", "新規コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
            catch (Exception ex)
            {
                // 例外処理
                Debug.Print(this.Name + "_コマンド新規 - " + ex.Message);
                MessageBox.Show("エラーのため新規モードへ移行できません。", "新規コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void コマンド修正_Click(object sender, EventArgs e)
        {
            try
            {
                // データに変更があった場合の処理
                if (this.コマンド登録.Enabled)
                {

                    var res = MessageBox.Show("変更内容を登録しますか？", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    switch (res)
                    {
                        case DialogResult.Yes:

                            if (!ErrCheck()) return;
                            this.DoubleBuffered = false;
                            //Call DoWait("登録しています...")

                            if (!SaveData())
                            {
                                MessageBox.Show("エラーのため登録できませんでした。", "修正コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            this.DoubleBuffered = true;
                            break;

                        case DialogResult.No:
                            // 新規モードのときに登録しない場合はコードを戻す
                            if (!this.コマンド新規.Enabled && !string.IsNullOrEmpty(CurrentCode))
                            {
                                Connect();
                                if (!FunctionClass.ReturnCode(cn, "ITM" + this.商品コード.Text))
                                {
                                    MessageBox.Show("エラーのためコードは破棄されました。\n\n商品コード： " + this.商品コード.Text, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                            }
                            break;

                        case DialogResult.Cancel:
                            return;
                    }
                }
                else
                {
                    // 新規モードのときに変更がない場合はコードを戻す
                    if (!this.コマンド新規.Enabled && !string.IsNullOrEmpty(CurrentCode))
                    {
                        if (!FunctionClass.ReturnCode(cn, "ITM" + this.商品コード.Text))
                        {
                            MessageBox.Show("エラーのためコードは破棄されました。\n\n商品コード： " + this.商品コード.Text, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }

                // 修正モードへ移行する
                if (!GoModifyMode())
                {
                    // 移行に失敗した場合の処理
                    Debug.Print(this.Name + "_コマンド修正_Click - Error");
                    if (MessageBox.Show("エラーが発生しました。\n\n管理者に連絡してください。\n\n強制終了しますか？", "修正コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.Yes)
                    {
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_コマンド修正_Click - " + ex.Message);
                MessageBox.Show("エラーが発生しました。\n\n管理者に連絡してください。\n\n強制終了しますか？", "修正コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                this.Close();
            }
        }

        private void コマンド複写_Click(object sender, EventArgs e)
        {
            if (this.ActiveControl == this.コマンド複写)
            {
                if (previousControl != null)
                {
                    previousControl.Focus();
                }
            }
            //新規採番したコードを商品明細にセット
            string original = FunctionClass.採番(cn, "ITM");
            string originalcode = original.Substring(original.Length - 8);

            if (CopyData(originalcode))
            {
                // ヘッダ部制御
                FunctionClass.LockData(this, false);
                商品名.Focus();
                商品コード.Enabled = false;
                コマンド新規.Enabled = false;
                コマンド修正.Enabled = true;
                コマンド複写.Enabled = false;
                コマンド削除.Enabled = false;
                // コマンド承認.Enabled = false;
                // コマンド確定.Enabled = false;
                コマンド登録.Enabled = true;
            }
            else
            {
                MessageBox.Show("エラーが発生しました。\n複写できません。", "複写コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        private bool CopyData(string codeString)
        {
            try
            {
                // DataGridView内の各行にアクセス
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    // 行が新しい行を示す場合など、データ行でない場合は無視
                    if (!row.IsNewRow)
                    {
                        // 商品コードカラムのセルを取得
                        DataGridViewCell productCodeCell = row.Cells["dgv商品コード"]; // カラム名に応じて変更

                        if (productCodeCell != null)
                        {
                            // 商品コードカラムのセルの値を新しい商品コードに変更
                            productCodeCell.Value = codeString;
                        }
                    }
                }
                // コントロールのフィールドを初期化
                商品コード.Text = codeString;
                作成日時.Text = null;
                作成者コード.Text = null;
                作成者名.Text = null;
                更新日時.Text = null;
                更新者コード.Text = null;
                更新者名.Text = null;
                削除.Text = null;

                //明細行のリセット
                detailNumber = 1;


                return true;
            }
            catch (Exception ex)
            {
                // エラーが発生した場合の処理
                MessageBox.Show("_CopyData - " + ex.Message);
                return false;
            }
        }


        private void コマンド削除_Click(object sender, EventArgs e)
        {
            if (ActiveControl == コマンド削除)
            {
                if (previousControl != null)
                {
                    previousControl.Focus();
                }
            }
            //CurrentCode = this.商品コード.Text;

            if (!string.IsNullOrEmpty(ComposedChipMount.Text) || IsUnit.Checked)
            {
                MessageBox.Show("本商品はマウントデータが構成されています。削除する前にマウントラインで使用されていないことを確認してください.", "削除コマンド",
                    MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                //return;  そのまま削除できる仕様の様
            }

            DialogResult result = MessageBox.Show($"商品コード：{CurrentCode}\n\nこの商品データを削除します。\n削除後元に戻すことはできません。\n\n削除しますか？", "削除コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No)
                return;

            // データベースからデータを削除
            if (DeleteData(CurrentCode))
            {
                削除.Text = "■";
                MessageBox.Show("削除しました。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.SuspendLayout();

                // 新規モードへ移行
                if (!GoNewMode())
                {
                    MessageBox.Show($"エラーのため新規モードへ移行できません。\n[{Name}]を終了します。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    this.ResumeLayout();
                    Close();
                }

                this.ResumeLayout();
            }
            else
            {
                MessageBox.Show("削除できませんでした。他のユーザーにより削除されている可能性があります。", "削除コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private bool DeleteData(string codeString)
        {
            bool success = false;

            // SQL文で使用するパラメータ名
            string codeParam = "@Code";

            // トランザクションを開始
            Connect();
            SqlTransaction transaction = cn.BeginTransaction();

            try
            {
                // データベース操作のためのSQL文
                string selectSql = "SELECT * FROM M商品 WHERE 商品コード = " + codeParam + " AND 無効日時 IS NULL";
                string updateSql = "UPDATE M商品 SET 無効日時 = GETDATE(), 無効者コード = @UserCode WHERE 商品コード = " + codeParam + " AND 無効日時 IS NULL";

                using (SqlCommand selectCommand = new SqlCommand(selectSql, cn, transaction))
                {
                    selectCommand.Parameters.Add(new SqlParameter(codeParam, codeString));
                    using (SqlDataReader reader = selectCommand.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            // 商品が見つかった場合、削除処理を実行
                            reader.Close(); // 同じ接続なのでデータリーダーを閉じておく

                            using (SqlCommand updateCommand = new SqlCommand(updateSql, cn, transaction))
                            {
                                updateCommand.Parameters.Add(new SqlParameter(codeParam, codeString));
                                updateCommand.Parameters.Add(new SqlParameter("@UserCode", CommonConstants.LoginUserCode)); // LoginUserCode に適切な値を設定

                                int rowsAffected = updateCommand.ExecuteNonQuery();

                                if (rowsAffected > 0)
                                {
                                    // 更新が成功した場合、トランザクションをコミット
                                    transaction.Commit();
                                    success = true;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // エラー処理
                Console.WriteLine("DeleteData - " + ex.Message);

                try
                {
                    // エラーが発生した場合、トランザクションをロールバック
                    transaction.Rollback();
                }
                catch (Exception rollbackEx)
                {
                    Console.WriteLine("Rollback Error - " + rollbackEx.Message);
                }
            }
            finally
            {
                cn.Close();
            }

            return success;
        }

        private void コマンドシリーズ_Click(object sender, EventArgs e)
        {
            F_シリーズ fm = new F_シリーズ();
            fm.ShowDialog();
        }

        private void コマンド承認_Click(object sender, EventArgs e)
        {
            if (ActiveControl == コマンド承認)
            {
                if (previousControl != null)
                {
                    previousControl.Focus();
                }
            }
            MessageBox.Show("このコマンドは使用できません。", "承認コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void コマンド終了_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //フォームを閉じる時のロールバック等の処理
        private void Form_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (this.コマンド登録.Enabled)
                {
                    // データに変更がある場合の処理
                    DialogResult result = MessageBox.Show("変更内容を登録しますか？", this.Text, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);

                    switch (result)
                    {
                        case DialogResult.Yes:
                            // エラーチェック
                            if (!ErrCheck()) return;


                            // 登録処理
                            //DoWait("登録しています...");
                            this.SuspendLayout();

                            if (!SaveData())
                            {
                                if (MessageBox.Show("エラーのため登録できませんでした。" + Environment.NewLine +
                                    "強制終了しますか？", this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                                {
                                    e.Cancel = true;
                                    return;
                                }
                            }
                            break;

                        case DialogResult.No:
                            // 新規モードのときに登録しない場合はコードを戻す
                            if (!this.コマンド新規.Enabled && !string.IsNullOrEmpty(CurrentCode))
                            {
                                Connect();
                                if (!FunctionClass.ReturnCode(cn, "ITM" + this.商品コード.Text))
                                {
                                    MessageBox.Show("エラーのためコードは破棄されました。\n\n商品コード： " + this.商品コード.Text, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                }
                            }
                            break;

                        case DialogResult.Cancel:
                            e.Cancel = true;
                            return;
                    }
                }
                else
                {
                    // 新規モードのときに変更がない場合はコードを戻す
                    if (!this.コマンド新規.Enabled && !string.IsNullOrEmpty(CurrentCode))
                    {
                        Connect();
                        if (!FunctionClass.ReturnCode(cn, "ITM" + this.商品コード.Text))
                        {
                            MessageBox.Show("エラーのためコードは破棄されました。\n\n商品コード： " + this.商品コード.Text, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                    }
                }


                LocalSetting test = new LocalSetting();
                test.SavePlace(CommonConstants.LoginUserCode, this);

                //実行中フォーム起動

            }
            catch (Exception ex)
            {
                // エラー処理
                Console.WriteLine("Form_FormClosing error: " + ex.Message);
                MessageBox.Show("終了時にエラーが発生しました。", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void コマンド確定_Click(object sender, EventArgs e)
        {
            MessageBox.Show("確定コマンドは使えません。", "確定コマンド", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //form_Loadの処理だと、グリッドビューがアクティブにならないので、グリッドビューにカーソルを持っていきたい時はこちら
        private void F_商品_Shown(object sender, EventArgs e)
        {
            //this.Activate();
            //this.ActiveControl = dataGridView1;
            //dataGridView1.CurrentCell = dataGridView1[1, new_cnt];
        }


        // コントロールがフォーカスを受け取ったとき、前回のフォーカスを記憶
        private void Control_GotFocus(object sender, EventArgs e)
        {
            previousControl = sender as Control;
        }


        private void dataGridView1_UserDeletingRow(object sender, DataGridViewRowCancelEventArgs e)
        {
            //削除するかユーザーに確認する
            if (MessageBox.Show("この行を削除しますか？",
                "削除の確認",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question) != DialogResult.OK)
            {
                e.Cancel = true;
            }
        }

        //商品明細の型式番号と構成番号を設定する 同一の商品コード内での連番　と型式名ごとの番号
        private bool SetModelNumber()
        {
            try
            {
                int lngi = 1;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        string 型式名 = row.Cells["型式名"].Value as string;

                        if (!string.IsNullOrEmpty(型式名) && 型式名 != "---")
                        {
                            // データグリッドビューから値を取得してデータテーブル内の値を変更
                            dataGridView1.Rows[row.Index].Cells["型式番号"].Value = lngi;
                            dataGridView1.Rows[row.Index].Cells["構成番号"].Value = DBNull.Value;
                            lngi++;
                        }
                        else
                        {
                            dataGridView1.Rows[row.Index].Cells["構成番号"].Value = lngi;
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("SetModelNumber Error: " + ex.Message);
                return false;
            }
        }

        public void ChangedData(bool dataChanged)
        {
            if (dataChanged)
            {
                this.Text = this.Name + "*";
            }
            else
            {
                this.Text = this.Name;
            }

            if (this.ActiveControl == this.商品コード)
            {
                this.商品名.Focus();
            }

            this.商品コード.Enabled = !dataChanged;
            this.コマンド複写.Enabled = !dataChanged;
            this.コマンド削除.Enabled = !dataChanged;
            this.コマンド登録.Enabled = dataChanged;
        }

        private void F_商品_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Return:
                    SelectNextControl(ActiveControl, true, true, true, true);
                    break;
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
                        コマンド新規_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F2:
                    if (コマンド修正.Enabled)
                    {
                        コマンド修正.Focus();
                        コマンド修正_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F3:
                    if (コマンド複写.Enabled)
                    {
                        コマンド複写_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F4:
                    if (コマンド削除.Enabled)
                    {
                        コマンド削除_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F5:
                    if (コマンドシリーズ.Enabled)
                    {
                        コマンドシリーズ_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F9:
                    if (コマンド承認.Enabled)
                    {
                        コマンド承認_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F10:
                    if (コマンド確定.Enabled)
                    {
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
        private void 品名_TextChanged(object sender, EventArgs e)
        {
            if (!FunctionClass.LimitText(this.ActiveControl, 48)) return;

            ChangedData(true);
        }

        private void 品名_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■受注時などに表示される商品の品名です。　■全角２４文字まで入力できます。";
        }

        private void 商品分類コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ActiveControl == null) return;
            if (this.商品分類コード.SelectedItem != null)
            {
                分類内容.Text = (商品分類コード.SelectedItem as DataRowView)["分類内容"].ToString();
            }
        }

        private void 商品コード_SelectedIndexChanged(object sender, EventArgs e)
        {//商品コードのコンボボックスのソースセット時には処理を行わない様にするため
            if (setCombo) return;

            if (!FunctionClass.LimitText((Control)sender, 8)) return;
            UpdatedControl();
        }

        //アクセスではコンボボックスの横のテキストボックスを修正する処理があったが、今回はvalueとdisplayが同じコンボボックスなので不要
        //商品コードの更新処理のみ行う
        private void UpdatedControl()
        {
            //商品コードの更新後処理でレコードの値を表示する
            this.コマンド複写.Enabled = true;
            this.コマンド削除.Enabled = true;
            try
            {
                //CurrentCode = this.商品コード.Text;
                string strSQL = "SELECT * FROM V商品ヘッダ WHERE 商品コード='" + CurrentCode + "'";
                Connect();
                if (!VariableSet.SetTable2Form(this, strSQL, cn)) return;

                //V商品ヘッダにrevisionカラムがないため
                this.Revision.Text = "1";

                //何故かdatagridviewに反映しない？
                // this.M商品明細TableAdapter.Fill(this.uiDataSet.M商品明細, CurrentCode);

                this.mshomeisaiTableAdapter.Fill(this.newDataSet.M商品明細, CurrentCode);

                //strSQL = "SELECT * FROM M商品明細 WHERE 商品コード='" + CurrentCode + "'";
                //DataGridUtils.SetDataGridView(cn, strSQL, this.dataGridView1);


                FunctionClass.LockData(this, false, "商品コード");
                コマンド複写.Enabled = true;
                コマンド削除.Enabled = true;
                cn.Close();
                ChangedData(false);
            }
            catch (Exception ex)
            {
                MessageBox.Show("正しく読み込みが出来ませんでした" + ex.Message);
                cn.Close();
            }
        }

        private void 商品コード_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (string.IsNullOrEmpty(ActiveControl.Text))
                    return;

                string strCode = ActiveControl.Text.PadLeft(8, '0');

                if (strCode != (ActiveControl.Text ?? ""))
                {
                    ActiveControl.Text = strCode;
                }
            }
        }

        private void 商品コード_KeyPress(object sender, KeyPressEventArgs e)
        {
            int keyAscii = e.KeyChar; // キーのASCIIコードを取得（仮の値）

            keyAscii = FunctionClass.ChangeBig(keyAscii);

            switch (keyAscii)
            {
                case ' ': // スペース
                case 12288: // 全角スペース
                    if (ActiveControl is ComboBox comboBox)
                    {
                        comboBox.DroppedDown = true; // ドロップダウンメニューを表示
                    }
                    e.Handled = true; // キー入力を無効にする
                    break;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            dataGridView1.CurrentCell = dataGridView1[7, 2];
            // dataGridView1[6, 2].Selected = true;

            BindingSource bindingSource = (BindingSource)dataGridView1.DataSource;
            DataTable dataTable = ((DataView)bindingSource.List).Table;

            dataTable.DefaultView.Sort = "明細番号";

            bindingSource.ResetBindings(false);

        }

        private void 商品名_TextChanged(object sender, EventArgs e)
        {
            if (!FunctionClass.LimitText(this.ActiveControl, 40)) return;
            ChangedData(true);
        }

        private void 商品名_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■半角４０文字まで入力できます。";
        }

        private void シリーズコード_TextChanged(object sender, EventArgs e)
        {
            if (this.ActiveControl == null) return;
            string enteredText = シリーズコード.Text;
            if (string.IsNullOrEmpty(enteredText)) return;


            if (!OriginalClass.ComboBoxContainsValue(シリーズコード, enteredText))
            {
                MessageBox.Show("シリーズを選択してください。" + Environment.NewLine + "シリーズは事前に登録されている必要があります。",
                    this.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                シリーズコード.Text = ""; // テキストボックスをクリア
                シリーズコード.SelectedValue = DBNull.Value;
            }

            if (!FunctionClass.LimitText(this.ActiveControl, 8)) return;
            ChangedData(true);
        }

        private void シリーズコード_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■この欄を入力すると自動的に在庫管理対象となります。　■半角２０文字まで入力できます。　■[space]キーでドロップダウンリストを表示します。";
        }

        private void 商品分類コード_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■商品が所属する分類を指定します。";
        }

        private void 商品分類コード_TextChanged(object sender, EventArgs e)
        {
            if (!FunctionClass.LimitText(this.ActiveControl, 2)) return;
            ChangedData(true);
        }

        private void 掛率有効_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ActiveControl != null)
                ChangedData(true);
        }

        private void 売上区分コード_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■この商品の売上区分を選択します。　■この入力値は受注入力時の初期値になります。";
        }

        private void FlowCategoryCode_TextChanged(object sender, EventArgs e)
        {
            if (!FunctionClass.LimitText(this.ActiveControl, 3)) return;
            ChangedData(true);
        }

        private void 数量単位コード_TextChanged(object sender, EventArgs e)
        {
            if (!FunctionClass.LimitText(this.ActiveControl, 2)) return;
            ChangedData(true);
        }

        private void ClientName_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■受注時に初期設定される依頼主です。　■全角100文字まで入力できます。";
        }

        private void ClientName_TextChanged(object sender, EventArgs e)
        {
            if (!FunctionClass.LimitText(this.ActiveControl, 200)) return;
            ChangedData(true);
        }

        private void Discontinued_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■この商品を出荷する際、顧客シリアルが必要な時に指定します。";
        }

        private void Discontinued_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ActiveControl != null)
                ChangedData(true);
        }

        private void 備考_TextChanged(object sender, EventArgs e)
        {
            if (!FunctionClass.LimitText(this.ActiveControl, 200)) return;
            ChangedData(true);
        }

        private void 備考_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■全角１００文字まで入力できます。";
        }

        private void IsUnit_CheckedChanged(object sender, EventArgs e)
        {
            if (this.ActiveControl != null)
                ChangedData(true);
        }


        // DataGridViewの初期設定
        private void InitializeDataGridView()
        {
            // DefaultValuesNeededイベントハンドラを登録
            dataGridView1.DefaultValuesNeeded += new DataGridViewRowEventHandler(dataGridView1_DefaultValuesNeeded);
        }

        private int detailNumber = 1; // 最初の連番
        //セルのデフォルト値
        private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["dgv商品コード"].Value = this.商品コード.Text; //Convert.ToInt32(this.顧客ID);
            e.Row.Cells["dgvRevision"].Value = this.Revision.Text;
            e.Row.Cells["dgv明細番号"].Value = detailNumber.ToString();
            detailNumber++; // 連番を増やす
        }

        //セルの変更後の処理
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) // ヘッダーセルの場合は無視
                return;

            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;

            switch (columnName)
            {
                case "型式名":
                    if (!FunctionClass.LimitText(this.ActiveControl, 48)) return;
                    ChangedData(true);
                    break;
                case "定価":
                    if (!FunctionClass.LimitText(this.ActiveControl, 10)) return;
                    ChangedData(true);
                    break;
                case "原価":
                    if (!FunctionClass.LimitText(this.ActiveControl, 10)) return;
                    ChangedData(true);
                    break;
                case "機能":
                    if (!FunctionClass.LimitText(this.ActiveControl, 50)) return;
                    ChangedData(true);
                    break;
                default:
                    // その他のカラムが変更された場合の処理
                    break;
            }
        }

        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) // ヘッダーセルの場合は無視
                return;

            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;

            switch (columnName)
            {
                case "型式名":
                    dataGridView1.ImeMode = System.Windows.Forms.ImeMode.Off;
                    this.toolStripStatusLabel2.Text = "■半角４８文字まで入力できます。　■英数字は半角文字で入力し、半角カタカナは使用しないでください。";
                    break;
                case "定価":
                    this.toolStripStatusLabel2.Text = "■型式ごとの定価を設定します。　■マイナス価格を設定することも可能です。";
                    dataGridView1.ImeMode = ImeMode.Disable;
                    break;

                case "原価":
                    dataGridView1.ImeMode = ImeMode.Disable;
                    break;

                case "機能":
                    dataGridView1.ImeMode = ImeMode.Hiragana;
                    this.toolStripStatusLabel2.Text = "■全角２５文字まで入力できます。";
                    break;

                default:
                    // その他のカラムにエンターされた場合の処理
                    break;
            }
        }

        //セルの変更前の処理
        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {
            if (e.RowIndex < 0) // ヘッダーセルの場合は無視
                return;

            // セルの変更前の値を取得
            object previousValue = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;
            string? newValue = e.FormattedValue.ToString(); // 変更後の値


            switch (columnName)
            {
                case "型式名":
                    //明細番号を取得
                    int currentNumber = int.Parse(dataGridView1.Rows[e.RowIndex].Cells["明細番号"].Value.ToString());

                    if (string.IsNullOrWhiteSpace(newValue))
                    {
                        e.Cancel = true;
                        MessageBox.Show(columnName + " を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    // ここで重複チェックを実行するメソッドを呼び出す
                    if (DetectRepeatedID(currentNumber, previousValue as string, "---"))
                    {
                        e.Cancel = true;
                        MessageBox.Show("型式名が重複しています。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    }

                    break;

                case "定価":

                    if (string.IsNullOrWhiteSpace(newValue))
                    {
                        e.Cancel = true;
                        MessageBox.Show(columnName + " を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    break;

                case "原価":
                    // コメントアウトしていた

                    break;

                case "機能":
                    // コメントアウトしていた

                    break;

            }
        }

        private bool DetectRepeatedID(int currentNumber, string targetID, string exName)
        {
            // 型式名の重複を検出する
            // CurrentNumber - 呼び出し元明細行の明細番号
            // TargetID      - 検出対象となる型式名
            // ExName        - 除外する型式名
            //               - 検出結果　True->重複あり False->重複なし

            bool hasDuplicate = false;

            try
            {
                Connect();
                string query = "SELECT * FROM 商品明細 WHERE 明細番号 <> ? AND 型式名 = ? AND 型式名 <> ?";

                {
                    cmd.Parameters.AddWithValue("@currentNumber", currentNumber);
                    cmd.Parameters.AddWithValue("@targetID", targetID);
                    cmd.Parameters.AddWithValue("@exName", exName);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count > 0)
                        {
                            hasDuplicate = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }

            return hasDuplicate;
        }

        //セルがマイナスの場合の処理
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (e.RowIndex < 0) // ヘッダーセルの場合は無視
                return;
            // セルの値を取得
            object cellValue = dataGridView1[e.ColumnIndex, e.RowIndex].Value;
            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;

            // セルの値が数値で、かつマイナスの場合
            if (cellValue is int intValue && intValue < 0 && (columnName == "定価" || columnName == "原価"))
            {
                // 赤色のフォントを設定
                e.CellStyle.ForeColor = Color.Red;
            }

        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // クリックされたセルがボタンセルであることを確認
            if (e.RowIndex >= 0 && e.ColumnIndex >= 0 && dataGridView1[e.ColumnIndex, e.RowIndex] is DataGridViewButtonCell)
            {
                string buttonName = dataGridView1.Columns[e.ColumnIndex].Name;

                // ボタンの種類に応じて処理を分ける
                if (buttonName == "明細削除ボタン")
                {
                    // 明細削除ボタンの処理を実行
                    明細削除ボタン_Click(sender, e);
                }
                else if (buttonName == "行挿入ボタン")
                {
                    行挿入ボタン_Click(sender, e);
                }
            }
        }

        private void 明細削除ボタン_Click(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                DialogResult result = MessageBox.Show("行を削除しますか？", "行削除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (result == DialogResult.Yes)
                {
                    dataGridView1.Rows.RemoveAt(e.RowIndex);
                    ChangedData(true);
                    NumberDetails("dgv明細番号");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラーが発生しました。\n" + ex.Message, "行削除エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void NumberDetails(string fieldName, long StartValue = 1, long offset = 1)
        {
            //明細番号をふり直す
            //FieldName - 番号を格納するフィールド名
            //共通化すべきか？フォームによって引数が異なっている様だが。。。共通で使用する様に作ってるようだが、datagridviewを使用しないのであれば共通化は不要か

            try
            {
                long lngi = StartValue;
                BindingSource bindingSource = (BindingSource)dataGridView1.DataSource;
                DataTable dataTable = ((DataView)bindingSource.List).Table;
                DataRow currentRow = ((DataRowView)bindingSource.Current).Row;

                dataTable.DefaultView.Sort = "明細番号 ";

                // 既存の行の明細番号を増加させる
                foreach (DataRowView rowView in dataTable.DefaultView)
                {
                    DataRow row = rowView.Row;
                    // short rowDetailNumber = (short)row["明細番号"];
                    //if (rowDetailNumber >= currentDetailNumber)
                    //{
                    row["明細番号"] = lngi.ToString(); //(short)(rowDetailNumber + 1);
                    lngi += offset;
                    //}
                }

                //foreach (DataGridViewRow row in dataGridView1.Rows)
                //{
                //    if (!row.IsNewRow)
                //    {
                //        row.Cells[fieldName].Value = lngi.ToString();
                //        lngi += offset;
                //    }
                //}
            }
            catch (Exception ex)
            {
                Console.WriteLine(this.Name + "_NumberDetails - " + ex.Message);
            }
        }

        private void 行挿入ボタン_Click(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                BindingSource bindingSource = (BindingSource)dataGridView1.DataSource;
                DataTable dataTable = ((DataView)bindingSource.List).Table;
                DataRow currentRow = ((DataRowView)bindingSource.Current).Row;

                int newRowIndex = e.RowIndex + 1;

                // 挿入した行の明細番号を取得
                short currentDetailNumber = (short)dataGridView1.Rows[e.RowIndex].Cells["dgv明細番号"].Value;

                dataTable.DefaultView.Sort = "明細番号 DESC";

                // 既存の行の明細番号を降順に増加させる
                foreach (DataRowView rowView in dataTable.DefaultView)
                {
                    DataRow row = rowView.Row;
                    short rowDetailNumber = (short)row["明細番号"];
                    if (rowDetailNumber >= currentDetailNumber)
                    {
                        row["明細番号"] = (short)(rowDetailNumber + 1);
                    }
                }

                //DataGridViewに新しい行を挿入
                DataRow newRow = dataTable.NewRow();
                newRow["商品コード"] = this.商品コード.Text;
                newRow["Revision"] = this.Revision.Text;
                newRow["明細番号"] = currentDetailNumber;
                //dataTable.Rows.Add(newRow);
                dataTable.Rows.InsertAt(newRow, newRowIndex - 1);
                dataTable.DefaultView.Sort = "明細番号";

                // DataGridViewの特定のセル（"型式名" カラムのセル）をアクティブにする
                int activeIndex = dataGridView1.Columns["型式名"].Index;
                //dataGridView1.CurrentCell = dataGridView1.Rows[newRowIndex].Cells[activeIndex];
                dataGridView1[activeIndex, newRowIndex].Selected = true;
                dataGridView1.BeginEdit(true);

                bindingSource.EndEdit();
                bindingSource.ResetBindings(false);

            }
            catch (Exception ex)
            {
                Debug.WriteLine("行挿入ボタン_Click - " + ex.Message);
            }
        }

    }
}


