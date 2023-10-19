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
        public F_メーカー()
        {
            this.Text = "メーカー";       // ウィンドウタイトルを設定
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


        //SqlConnection cn = new SqlConnection();
        SqlCommand cmd = new SqlCommand();
        DataSet ds = new DataSet();
        DataTable dt = new DataTable();
        SqlDataAdapter adapter = new SqlDataAdapter();

        int new_cnt;
        int kokyaku_id;
        string kokyaku_cd;
        int tantou;

        private void Form_Load(object sender, EventArgs e)
        {


            this.combBoxメーカーコードTableAdapter.Fill(this.uiDataSet.CombBoxメーカーコード);
            this.combBoxMシリーズTableAdapter.Fill(this.uiDataSet.Mシリーズ);
            this.mメーカー分類TableAdapter.Fill(this.uiDataSet.Mメーカー分類);
            this.comboBox売上区分TableAdapter.Fill(this.uiDataSet.M売上区分);
            this.m単位TableAdapter.Fill(this.uiDataSet.M単位);
            this.comboBoxManufactureFlowTableAdapter.Fill(this.uiDataSet.ManufactureFlow);

            previousControl = null;
            try
            {
                if (true)
                {
                    if (!GoNewMode())
                    {
                        return;
                    }
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("初期化に失敗しました。", "エラー");
            }
        }

        private bool GoNewMode()
        {
            try
            {
                string strSQL;
                Connect(); // Connect メソッドの呼び出し

                //FunctionClass functionClass = new FunctionClass(); staticに変更

                // ヘッダ部を初期化
                //SetControls(this, null);
                //バインドソースの新規追加
                //this.MメーカーBindingSource.AddNew();

                string original = FunctionClass.採番(cn, "ITM");
                メーカーコード.Text = original.Substring(original.Length - 8);
                Revision.Text = "1";
                掛率有効.Checked = true;

                FlowCategoryCode.SelectedValue = "001";

                //FlowCategoryName.Text = (FlowCategoryCode.SelectedItem as DataRowView)["Name"].ToString();


                数量単位コード.SelectedValue = 1;


                CustomerSerialNumberRequired.Checked = false;
                IsUnit.Checked = false;
                Discontinued.Checked = false;

                string CurrentCode = メーカーコード.Text;
                // 明細部を初期化
                this.Mメーカー明細TableAdapter.Fill(this.uiDataSet.Mメーカー明細, CurrentCode);
                //strSQL = "SELECT * FROM Mメーカー明細 WHERE メーカーコード='" + CurrentCode + "' ORDER BY 明細番号";
                //LoadDetails(strSQL, SubForm, dbWork, "メーカー明細");

                // ヘッダ部を制御
                //LockData(this, false);
                品名.Focus();
                メーカーコード.Enabled = false;
                コマンド新規.Enabled = false;
                コマンド読込.Enabled = true;
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
                // 表示データをクリア 空文字のときにnullにする処理。不要？？
                //SetControls(this, null);

                FunctionClass.LockData(this, true, "メーカーコード");
                this.メーカーコード.Enabled = true;
                this.メーカーコード.Focus();
                this.コマンド新規.Enabled = true;
                this.コマンド読込.Enabled = false;
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
            if (!FunctionClass.IsError(this.メーカー名)) return false;
            if (!FunctionClass.IsError(this.メーカーコード)) return false;
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
            //管理情報の設定
            if (!SetModelNumber()) return false;

            Connect();
            SqlTransaction transaction = cn.BeginTransaction();
            {
                try
                {
                    // Mメーカーデータを保存
                    //this.Validate();
                    //this.MメーカーBindingSource.EndEdit();
                    //this.MメーカーTableAdapter.Connection = cn;
                    //this.MメーカーTableAdapter.Transaction = transaction;
                    //this.MメーカーTableAdapter.Update(this.uiDataSet.Mメーカー);

                    string strwhere = " メーカーコード='" + this.メーカーコード.Text + "' and Revision=" + this.Revision.Text;

                    if (!DataUpdater.UpdateOrInsertDataFrom(this, cn, "Mメーカー", strwhere, "メーカーコード", transaction))
                    {
                        //transaction.Rollback(); 関数内でロールバック入れた
                        return false;
                    }


                    // Mメーカー明細データを保存
                    this.Mメーカー明細TableAdapter.Connection = cn;
                    this.Mメーカー明細TableAdapter.Transaction = transaction;
                    this.Mメーカー明細TableAdapter.Update(this.uiDataSet.Mメーカー明細);

                    // トランザクションをコミット
                    transaction.Commit();

                    // データベースへの変更を適用
                    this.tableAdapterManager.UpdateAll(this.uiDataSet);
                    MessageBox.Show("登録を完了しました");

                    メーカーコード.Enabled = true;

                    // 新規モードのときは修正モードへ移行する
                    if (true)//IsNewData)
                    {
                        コマンド新規.Enabled = true;
                        コマンド読込.Enabled = false;
                    }

                    コマンド複写.Enabled = true;
                    コマンド削除.Enabled = true;
                    コマンド登録.Enabled = false;

                    return true;

                }
                catch (Exception ex)
                {
                    // トランザクション内でエラーが発生した場合、ロールバックを実行
                    if (transaction != null)
                    {
                        transaction.Rollback();
                    }

                    コマンド登録.Enabled = true;
                    // エラーメッセージを表示またはログに記録
                    MessageBox.Show("データの保存中にエラーが発生しました: " + ex.Message, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
        }

        private int detailNumber = 1; // 最初の連番
        セルのデフォルト値
        private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["メーカーコードDataGridViewTextBoxColumn"].Value = this.メーカーコード.Text; //Convert.ToInt32(this.顧客ID);
            e.Row.Cells["RevisionDataGridViewTextBoxColumn"].Value = this.Revision.Text;
            e.Row.Cells["明細番号DataGridViewTextBoxColumn"].Value = detailNumber.ToString();
            detailNumber++; // 連番を増やす
            //e.Row.Cells["担当者コード"].Value = tantou;
            //e.Row.Cells["時刻"].Value = DateTime.Now.ToString("HH:mm");            
        }

        // DataGridViewの初期設定
        private void InitializeDataGridView()
        {

            // DefaultValuesNeededイベントハンドラを登録
            dataGridView1.DefaultValuesNeeded += new DataGridViewRowEventHandler(dataGridView1_DefaultValuesNeeded);
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
        //セルのフォーマット
        private void dataGridView1_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            try
            {
                DataGridView dgv = (DataGridView)sender;
                //セルの列を確認
                if (dgv.Columns[e.ColumnIndex].Name == "重要FLG" && e.Value is Boolean)
                {
                    Boolean val = (Boolean)e.Value;
                    //セルの値により、背景色を変更する
                    if (val == true)
                    {
                        dgv.Rows[e.RowIndex].Cells["約定内容"].Style.ForeColor = Color.Red;
                    }
                    else
                    {
                        dgv.Rows[e.RowIndex].Cells["約定内容"].Style.ForeColor = Color.Black;
                    }
                }
                else if (dgv.Columns[e.ColumnIndex].Name == "時刻" && e.Value is string)
                {
                    string val = (string)e.Value;

                    dataGridView1.Columns["時刻"].DefaultCellStyle.Format = "HH:mm";
                    //val.ToString("HH:mm");
                }
                else if (dgv.Columns[e.ColumnIndex].Name == "支払先" && e.Value != null)
                {
                    var combo = dgv.Columns[e.ColumnIndex] as DataGridViewComboBoxColumn;
                    var item = combo.Items.Cast<DataRowView>().FirstOrDefault(row => row[combo.ValueMember].ToString() == e.Value.ToString());

                    if (item != null)
                    {
                        e.Value = item[combo.DisplayMember];
                        e.FormattingApplied = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void dataGridView1_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {

            DataGridView dgv = (DataGridView)sender;
            //該当する列か調べる
            //クリックされたカラムが「約定内容」「区分」カラムなら、ユーザーが文字列を入力できるようにする

            if (dgv.CurrentCell.OwningColumn.Name == "約定内容" || dgv.CurrentCell.OwningColumn.Name == "区分"
                || dgv.CurrentCell.OwningColumn.Name == "交渉相手")
            {
                //編集のために表示されているコントロールを取得
                DataGridViewComboBoxEditingControl cb =
                    (DataGridViewComboBoxEditingControl)e.Control;
                cb.DropDownStyle = ComboBoxStyle.DropDown;
            }
            //表示されているコントロールがDataGridViewTextBoxEditingControlか調べる
            if (e.Control is DataGridViewTextBoxEditingControl)
            {
                //編集のために表示されているコントロールを取得
                DataGridViewTextBoxEditingControl tb = (DataGridViewTextBoxEditingControl)e.Control;

                tb.KeyPress -=
                 new KeyPressEventHandler(dataGridViewTextBox_KeyPress);

                //該当する列か調べる
                if (dgv.CurrentCell.OwningColumn.Name == "時刻")//クリックされたカラムが「時刻」カラムなら
                {
                    //KeyPressイベントハンドラを追加
                    tb.KeyPress +=
                    new KeyPressEventHandler(dataGridViewTextBox_KeyPress);
                    // this.KeyDown += new KeyEventHandler(Form1_KeyDown);
                }
            }
        }
        //DataGridViewに表示されているテキストボックスのKeyPressイベントハンドラ
        private void dataGridViewTextBox_KeyPress(object sender,
            KeyPressEventArgs e)
        {
            //数字とコロンしか入力できないようにする
            if ((e.KeyChar < '0' || e.KeyChar > '9') && e.KeyChar != ':')
            {
                e.Handled = true;
            }
        }

        private void dataGridView1_CellValidating(object sender, DataGridViewCellValidatingEventArgs e)
        {//コンボボックスにフリー入力し、その内容をコンボボックスの項目に追加
            DataGridView dgv = (DataGridView)sender;
            //該当する列か調べる
            if ((dgv.Columns[e.ColumnIndex].Name == "約定内容" &&
                dgv.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn) ||
                (dgv.Columns[e.ColumnIndex].Name == "区分" &&
                dgv.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn) ||
                (dgv.Columns[e.ColumnIndex].Name == "交渉相手" &&
                dgv.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn)
                )
            {
                DataGridViewComboBoxColumn cbc = (DataGridViewComboBoxColumn)dgv.Columns[e.ColumnIndex];
                //コンボボックスの項目に追加する
                if (!cbc.Items.Contains(e.FormattedValue))
                {
                    cbc.Items.Add(e.FormattedValue);
                }
                //セルの値を設定しないと、元に戻ってしまう
                dgv[e.ColumnIndex, e.RowIndex].Value = e.FormattedValue;
            }
        }
        private void dataGridView1_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            //e.Cancel = true;
        }
        private void dataGridView1_CellEnter(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = (DataGridView)sender;
            switch (dgv.Columns[e.ColumnIndex].Name)
            {
                case "時刻":
                case "約定額":
                    dgv.ImeMode = ImeMode.Disable;
                    break;

                case "内容":
                case "交渉相手":
                    dgv.ImeMode = ImeMode.Hiragana;
                    break;
            }
            //コンボボックスのプルダウン
            if ((dgv.Columns[e.ColumnIndex].Name == "約定内容" || dgv.Columns[e.ColumnIndex].Name == "支払先"
                || dgv.Columns[e.ColumnIndex].Name == "区分") || dgv.Columns[e.ColumnIndex].Name == "交渉相手" &&
                   dgv.Columns[e.ColumnIndex] is DataGridViewComboBoxColumn)
            {
                SendKeys.Send("{F4}");
            }
            //カレンダーを表示する
            if (dgv.Columns[e.ColumnIndex].Name == "約定日")
            {
                SendKeys.Send("{F2}");
                SendKeys.Send("{F4}");
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

                            if (!SaveData())
                            {
                                MessageBox.Show("エラーのため登録できませんでした。", "修正コマンド", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            this.DoubleBuffered = true;
                            break;

                        case DialogResult.No:
                            // 新規モードのときに登録しない場合はコードを戻す
                            if (this.コマンド新規.Enabled)
                            {
                                Connect();
                                if (!FunctionClass.ReturnCode(cn, "ITM" + this.メーカーコード.Text))
                                {
                                    MessageBox.Show("エラーのためコードは破棄されました。\n\nメーカーコード： " + this.メーカーコード.Text, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
                    if (this.コマンド新規.Enabled)
                    {
                        if (!FunctionClass.ReturnCode(cn, "ITM" + this.メーカーコード.Text))
                        {
                            MessageBox.Show("エラーのためコードは破棄されました。\n\nメーカーコード： " + this.メーカーコード.Text, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
            //新規採番したコードをメーカー明細にセット
            string original = FunctionClass.採番(cn, "ITM");
            string originalcode = original.Substring(original.Length - 8);

            if (CopyData(originalcode))
            {
                // ヘッダ部制御
                FunctionClass.LockData(this, false);
                メーカー名.Focus();
                メーカーコード.Enabled = false;
                コマンド新規.Enabled = false;
                コマンド読込.Enabled = true;
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
                        // メーカーコードカラムのセルを取得
                        DataGridViewCell productCodeCell = row.Cells["dgvメーカーコード"]; // カラム名に応じて変更

                        if (productCodeCell != null)
                        {
                            // メーカーコードカラムのセルの値を新しいメーカーコードに変更
                            productCodeCell.Value = codeString;
                        }
                    }
                }
                // コントロールのフィールドを初期化
                メーカーコード.Text = codeString;
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

        }

        private void コマンドシリーズ_Click(object sender, EventArgs e)
        {

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
        private void コマンド確定_Click(object sender, EventArgs e)
        {

        }

        //form_Loadの処理だと、グリッドビューがアクティブにならないので、グリッドビューにカーソルを持っていきたい時はこちら
        private void F_メーカー_Shown(object sender, EventArgs e)
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

        //メーカー明細の型式番号と構成番号を設定する 同一のメーカーコード内での連番　と型式名ごとの番号
        private bool SetModelNumber()
        {
            try
            {
                //  DataTable table = this.uiDataSet.Mメーカー明細;
                int lngi = 1;
                // string 列名1 = dataGridView1.Columns["型式名"].Name;

                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    if (!row.IsNewRow)
                    {
                        string 型式名 = row.Cells["型式名DataGridViewTextBoxColumn"].Value as string;

                        if (!string.IsNullOrEmpty(型式名) && 型式名 != "---")
                        {
                            // データグリッドビューから値を取得してデータテーブル内の値を変更
                            dataGridView1.Rows[row.Index].Cells["型式番号DataGridViewTextBoxColumn"].Value = lngi;
                            dataGridView1.Rows[row.Index].Cells["構成番号DataGridViewTextBoxColumn"].Value = DBNull.Value;
                            lngi++;
                        }
                        else
                        {
                            dataGridView1.Rows[row.Index].Cells["構成番号DataGridViewTextBoxColumn"].Value = lngi;
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

            if (this.ActiveControl == this.メーカーコード)
            {
                this.メーカー名.Focus();
            }

            this.メーカーコード.Enabled = !dataChanged;
            this.コマンド複写.Enabled = !dataChanged;
            this.コマンド削除.Enabled = !dataChanged;
            this.コマンド登録.Enabled = dataChanged;
        }

        private void F_メーカー_KeyDown(object sender, KeyEventArgs e)
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
                        コマンド新規_Click(this, EventArgs.Empty); // クリックイベントを呼び出す
                    }
                    break;
                case Keys.F2:
                    if (コマンド読込.Enabled)
                    {
                        コマンド読込.Focus();
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
                    if (コマンド仕入先.Enabled)
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
            FunctionClass.LimitText(this.品名, 48);
            ChangedData(true);
        }
        private void 品名_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■受注時などに表示されるメーカーの品名です。　■全角２４文字まで入力できます。";
        }

        private void メーカー分類コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.メーカー分類コード.SelectedItem != null)
            {
                分類内容.Text = (メーカー分類コード.SelectedItem as DataRowView)["分類内容"].ToString();
            }
        }

        private void FlowCategoryCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(FlowCategoryCode.SelectedValue.ToString());

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(メーカー分類コード.Text);
            MessageBox.Show(数量単位コード.Text);
            MessageBox.Show(メーカーコード.Text);
            MessageBox.Show(メーカーコード.SelectedValue.ToString());
        }

        private void メーカーコード_TextChanged(object sender, EventArgs e)
        {

            //  this.vメーカーヘッダTableAdapter.Fill(this.uiDataSet.Vメーカーヘッダ, this.メーカーコード.Text);

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void F_メーカー_Load(object sender, EventArgs e)
        {

        }
    }


}
