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


namespace u_net
{
    public partial class F_商品 : Form
    {
        private Control previousControl;
        private SqlConnection cn;
        private SqlTransaction tx;
        public string args="";

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


            this.combBox商品コードTableAdapter.Fill(this.uiDataSet.CombBox商品コード);
            this.combBoxMシリーズTableAdapter.Fill(this.uiDataSet.Mシリーズ);
            this.m商品分類TableAdapter.Fill(this.uiDataSet.M商品分類);
            this.comboBox売上区分TableAdapter.Fill(this.uiDataSet.M売上区分);
            this.m単位TableAdapter.Fill(this.uiDataSet.M単位);
            this.comboBoxManufactureFlowTableAdapter.Fill(this.uiDataSet.ManufactureFlow);

            previousControl = null;
            try
            {
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
                //this.M商品BindingSource.AddNew();

                string original = FunctionClass.採番(cn, "ITM");
                商品コード.Text = original.Substring(original.Length - 8);
                Revision.Text = "1";
                掛率有効.Checked = true;

                FlowCategoryCode.SelectedValue = "001";

                //FlowCategoryName.Text = (FlowCategoryCode.SelectedItem as DataRowView)["Name"].ToString();


                数量単位コード.SelectedValue = 1;


                CustomerSerialNumberRequired.Checked = false;
                IsUnit.Checked = false;
                Discontinued.Checked = false;

                string CurrentCode = 商品コード.Text;
                // 明細部を初期化
                this.M商品明細TableAdapter.Fill(this.uiDataSet.M商品明細, CurrentCode);
                //strSQL = "SELECT * FROM M商品明細 WHERE 商品コード='" + CurrentCode + "' ORDER BY 明細番号";
                //LoadDetails(strSQL, SubForm, dbWork, "商品明細");

                // ヘッダ部を制御
                //LockData(this, false);
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
                // 表示データをクリア 空文字のときにnullにする処理。不要？？
                //SetControls(this, null);
                if (!string.IsNullOrEmpty(args))
                {
                    this.商品コード.Text = args; 
                }

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
            //管理情報の設定
            if (!SetModelNumber()) return false;

            Connect();
            SqlTransaction transaction = cn.BeginTransaction();
            {
                try
                {
                    // M商品データを保存
                    
                    string strwhere = " 商品コード='" + this.商品コード.Text + "' and Revision=" + this.Revision.Text;

                    if (!DataUpdater.UpdateOrInsertDataFrom(this, cn, "M商品", strwhere, "商品コード", transaction))
                    {
                        //transaction.Rollback(); 関数内でロールバック入れた
                        return false;
                    }


                    // M商品明細データを保存
                    this.M商品明細TableAdapter.Connection = cn;
                    this.M商品明細TableAdapter.Transaction = transaction;
                    this.M商品明細TableAdapter.Update(this.uiDataSet.M商品明細);

                    // トランザクションをコミット
                    transaction.Commit();

                    // データベースへの変更を適用
                    this.tableAdapterManager.UpdateAll(this.uiDataSet);
                    MessageBox.Show("登録を完了しました");

                    商品コード.Enabled = true;

                    // 新規モードのときは修正モードへ移行する
                    if (true)//IsNewData)
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
        //セルのデフォルト値
        private void dataGridView1_DefaultValuesNeeded(object sender, DataGridViewRowEventArgs e)
        {
            e.Row.Cells["商品コードDataGridViewTextBoxColumn"].Value = this.商品コード.Text; //Convert.ToInt32(this.顧客ID);
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
                    if (this.コマンド新規.Enabled)
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

        //商品明細の型式番号と構成番号を設定する 同一の商品コード内での連番　と型式名ごとの番号
        private bool SetModelNumber()
        {
            try
            {
                //  DataTable table = this.uiDataSet.M商品明細;
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
            FunctionClass.LimitText(this.品名, 48);
            ChangedData(true);
        }
        private void 品名_Enter(object sender, EventArgs e)
        {
            this.toolStripStatusLabel2.Text = "■受注時などに表示される商品の品名です。　■全角２４文字まで入力できます。";
        }

        private void 商品分類コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.商品分類コード.SelectedItem != null)
            {
                分類内容.Text = (商品分類コード.SelectedItem as DataRowView)["分類内容"].ToString();
            }
        }

        private void FlowCategoryCode_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(FlowCategoryCode.SelectedValue.ToString());

        }

        private void button1_Click(object sender, EventArgs e)
        {
            MessageBox.Show(商品分類コード.Text);
            MessageBox.Show(数量単位コード.Text);
            MessageBox.Show(商品コード.Text);
            MessageBox.Show(商品コード.SelectedValue.ToString());
        }

        private void 商品コード_TextChanged(object sender, EventArgs e)
        {

            //  this.v商品ヘッダTableAdapter.Fill(this.uiDataSet.V商品ヘッダ, this.商品コード.Text);

        }

    }


    //public class DataGridViewEx : DataGridView
    //{
    //    [System.Security.Permissions.UIPermission(
    //        System.Security.Permissions.SecurityAction.Demand,
    //        Window = System.Security.Permissions.UIPermissionWindow.AllWindows)]
    //    protected override bool ProcessDialogKey(Keys keyData)
    //    {
    //        //Enterキーが押された時は、Tabキーが押されたようにする
    //        if ((keyData & Keys.KeyCode) == Keys.Enter)
    //        {
    //            return this.ProcessTabKey(keyData);
    //        }
    //        // 既定の処理を行う
    //        return base.ProcessDialogKey(keyData);
    //    }

    //    [System.Security.Permissions.SecurityPermission(
    //        System.Security.Permissions.SecurityAction.Demand,
    //        Flags = System.Security.Permissions.SecurityPermissionFlag.UnmanagedCode)]
    //    protected override bool ProcessDataGridViewKey(KeyEventArgs e)
    //    {
    //        //Enterキーが押された時は、Tabキーが押されたようにする
    //        if (e.KeyCode == Keys.Enter)
    //        {
    //            return this.ProcessTabKey(e.KeyCode);
    //        }
    //        return base.ProcessDataGridViewKey(e);
    //    }
    //}

    //private void b_内容検索_KeyDown(object sender, KeyEventArgs e)
    //{//内容検索の文字列を含む内容の行を選択する
    //    if (e.KeyCode == Keys.Enter)
    //    {
    //        this.dataGridView1.ClearSelection();

    //        DataGridView dgv = this.dataGridView1;
    //        System.Collections.IList list = dgv.Rows;
    //        for (int i = 0; i < list.Count; i++)
    //        {
    //            //nullを比較するとエラーになるので先に省く
    //            if (dataGridView1["内容", i].FormattedValue.ToString() != null)
    //            {
    //                //ボックスの文字列を比較
    //                if ((dataGridView1["内容", i].FormattedValue.ToString().Contains(this.b_内容検索.Text)) && (true))
    //                {
    //                    //ボックスを選択
    //                    this.dataGridView1["内容", i].Selected = true;
    //                }
    //            }
    //        }
    //    }
    //}

    //private void b_相手検索_KeyDown(object sender, KeyEventArgs e)
    //{//相手検索の文字列を含む相手の行を選択する

    //    if (e.KeyCode == Keys.Enter)
    //    {
    //        this.dataGridView1.ClearSelection();

    //        DataGridView dgv = this.dataGridView1;
    //        System.Collections.IList list = dgv.Rows;
    //        for (int i = 0; i < list.Count; i++)
    //        {
    //            //nullを比較するとエラーになるので先に省く
    //            if (dataGridView1["交渉相手", i].FormattedValue.ToString() != null)
    //            {
    //                //ボックスの文字列を比較
    //                if ((dataGridView1["交渉相手", i].FormattedValue.ToString().Contains(this.b_相手検索.Text)) && (true))
    //                {
    //                    //ボックスを選択
    //                    this.dataGridView1["交渉相手", i].Selected = true;
    //                }
    //            }
    //        }
    //    }
    //}


    //private void Form1_Load(object sender, EventArgs e)
    //{
    //              //上部の設定
    //    Connect();
    //    cmd = cn.CreateCommand();

    //    cmd.CommandText = "select 顧客コード from T_顧客 where id=" + kokyaku_id;

    //    SqlDataReader dr = cmd.ExecuteReader();

    //    if (dr.HasRows)
    //    {
    //        dr.Read();
    //        顧客コード.Text = dr["顧客コード"].ToString();
    //        kokyaku_cd = dr["顧客コード"].ToString();                
    //        dr.Close();
    //    }

    //    cmd.CommandText = "select isnull(sum(滞納額),0) as 滞納額合計,isnull(sum(変動水道代),0) as 水道代合計 from T_滞納 " +
    //        "where 顧客コード='" + kokyaku_cd + "'";
    //    dr = cmd.ExecuteReader();
    //    if (dr.HasRows)
    //    {
    //        dr.Read();
    //        滞納額合計.Text = dr["滞納額合計"].ToString();
    //        水道代合計.Text = dr["水道代合計"].ToString();
    //        dr.Close();
    //    }

    //    cmd.CommandText = "select isnull(sum(入金額),0) as 入金額合計 from T_滞納入金 " +
    //       "where 顧客コード='" + kokyaku_cd + "'";

    //    dr = cmd.ExecuteReader();
    //    if (dr.HasRows)
    //    {
    //        dr.Read();
    //        入金額合計.Text = dr["入金額合計"].ToString();
    //        dr.Close();
    //    }
    //    int zankin;
    //    zankin = Convert.ToInt32(滞納額合計.Text) + Convert.ToInt32(水道代合計.Text) - Convert.ToInt32(入金額合計.Text);
    //    滞納残金.Text = zankin.ToString();


    //    cmd.CommandText = "SELECT * FROM " +            
    //    "(SELECT IIf(IsNull(T_契約緊急連絡先.id, 0) > 0, T_契約緊急連絡先.名称, T_契約保証人.氏名) as 関係人氏名," +
    //    "IIf(IsNull(T_契約緊急連絡先.id, 0) > 0, T_契約緊急連絡先.カナ, T_契約保証人.カナ) as 関係人カナ," +
    //    "IIf(IsNull(T_契約緊急連絡先.id, 0) > 0, T_契約緊急連絡先.TEL, T_契約保証人.TEL) as 関係人TEL," +
    //    "IIf(IsNull(T_契約緊急連絡先.id, 0) > 0, T_契約緊急連絡先.TEL携帯, T_契約保証人.TEL携帯) as 関係人TEL携帯," +
    //    "IIf(IsNull(T_契約緊急連絡先.id, 0) > 0, T_契約緊急連絡先.続柄, T_契約保証人.続柄) as 関係人続柄," +
    //    "IIf(IsNull(T_契約緊急連絡先.id, 0) > 0, T_契約緊急連絡先.生年月日, T_契約保証人.生年月日) as 関係人生年月日," +
    //    "IIf(IsNull(T_契約緊急連絡先.id, 0) > 0, T_契約緊急連絡先.住所, T_契約保証人.住所1) as 関係人住所1," +
    //    "IIf(IsNull(T_契約緊急連絡先.id, 0) > 0, T_契約緊急連絡先.住所2, T_契約保証人.住所2) as 関係人住所2," +
    //    "IIf(IsNull(T_契約緊急連絡先.id, 0) > 0, '緊急連絡先', '契約保証人') as 関係人種別, " +
    //    "T_顧客.生年月日,T_顧客.性別 " +
    //    "FROM T_顧客 left join T_契約保証人 ON T_顧客.顧客コード = T_契約保証人.顧客コード " +
    //    "and T_契約保証人.id in (SELECT MIN(id) FROM T_契約保証人 group by 顧客コード) " +
    //    "left join T_契約緊急連絡先 ON T_顧客.顧客コード = T_契約緊急連絡先.顧客コード " +
    //    "and T_契約緊急連絡先.id in (SELECT MIN(id) FROM T_契約緊急連絡先 group by 顧客コード) " +
    //    "where T_顧客.ID = " + kokyaku_id + ") as T_kokyaku  "; 

    //    dr = cmd.ExecuteReader();
    //    if (dr.HasRows)
    //    {
    //        dr.Read();
    //        関係人氏名.Text = dr["関係人氏名"].ToString();
    //        関係人カナ.Text = dr["関係人カナ"].ToString();
    //        関係人TEL.Text = dr["関係人TEL"].ToString();
    //        関係人TEL携帯.Text = dr["関係人TEL携帯"].ToString();
    //        関係人続柄.Text = dr["関係人続柄"].ToString();                
    //        関係人種別.Text = dr["関係人種別"].ToString();
    //        関係人氏名.Text = dr["関係人氏名"].ToString();

    //        if (dr["性別"] != DBNull.Value)
    //        {
    //            int genderCode = Convert.ToInt32(dr["性別"]);

    //            if (genderCode == 1)
    //            {
    //                性別.Text = "男";
    //            }
    //            else if (genderCode == 2)
    //            {
    //                性別.Text = "女";
    //            }
    //        }
    //        if (dr["生年月日"] != DBNull.Value)
    //        {
    //            DateTime dateOfBirth = (DateTime)dr["生年月日"];
    //            Age.Text = Myage.CalculateAge(dateOfBirth).ToString();
    //        }

    //        //生年月日がnullでも空文字でもない場合
    //        if (!dr.IsDBNull(dr.GetOrdinal("関係人生年月日")) && dr.GetDateTime(dr.GetOrdinal("関係人生年月日")) != DateTime.MinValue)
    //        {
    //            関係人年齢.Text = (GetAge((DateTime)dr["関係人生年月日"]).ToString());
    //            関係人生年月日.Text = ((DateTime)dr["関係人生年月日"]).Date.ToString("yyyy/MM/dd");
    //        }

    //        //$""内で{}で囲んだ部分は式として解釈され、if-else文と同じように動作する
    //        関係人住所.Text = $"{(dr.IsDBNull(dr.GetOrdinal("関係人住所1")) ? "" : dr["関係人住所1"].ToString())}" +
    //        $"{(dr.IsDBNull(dr.GetOrdinal("関係人住所2")) ? "" : dr["関係人住所2"].ToString())}";
    //    dr.Close();
    //    }

    //    cn.Close();

    //    //this.v_顧客TableAdapter.Fill(this.rentDataSet.V_顧客, kokyaku_id);
    //    //this.koushoTableAdapter.Fill(this.rentDataSet.kousho, kokyaku_id);
    //    //this.t_約定内容TableAdapter.Fill(this.rentDataSet.T_約定内容);
    //    //this.t_滞納TableAdapter.Fill(this.rentDataSet.T_滞納, kokyaku_cd);
    //    //this.t_CODETableAdapter.Fill(this.rentDataSet.T_CODE);

    //    DataGridView dgv = this.dataGridView1;
    //    string col="";

    //    for (int j = 0; j < 3; j++)
    //    {
    //        switch (j)
    //        {
    //            case 0:
    //                col = "約定内容";
    //                break;
    //            case 1:
    //                col = "区分";
    //                break;
    //            case 2:
    //                col = "交渉相手";
    //                break;
    //        }

    //        DataGridViewComboBoxColumn cbc = (DataGridViewComboBoxColumn)dgv.Columns[col];
    //        System.Collections.IList list = dgv.Rows;

    //        for (int i = 0; i < list.Count; i++)//プルダウンのソースである約定内容カラム　の内容をプルダウンにセットする
    //        {
    //            DataGridViewRow datarow = (DataGridViewRow)list[i];
    //            //nullを比較するとエラーになるので先に省く
    //            if (datarow.Cells[col].Value != null)
    //            {
    //                //コンボボックスのItemsに無く、かつ""でないものを判別
    //                if ((!cbc.Items.Contains(datarow.Cells[col].Value)) && (datarow.Cells[col].Value.ToString() != ""))
    //                {
    //                    //コンボボックスの項目に追加する
    //                    cbc.Items.Add(datarow.Cells[col].Value);
    //                }
    //            }
    //        }
    //        if (j == 0)
    //        {
    //            //foreach (DataRow DTdr in this.rentDataSet.T_約定内容.Rows)//DataSetT_約定内容　の内容をプルダウンにセットする
    //            //{
    //            //    //nullを比較するとエラーになるので先に省く
    //            //    if (DTdr["約定内容"] != null)
    //            //    {
    //            //        //コンボボックスのItemsに無く、かつ""でないものを判別
    //            //        if ((!cbc.Items.Contains(DTdr["約定内容"])) && (DTdr["約定内容"].ToString() != ""))
    //            //        {
    //            //            //コンボボックスの項目に追加する
    //            //            cbc.Items.Add(DTdr["約定内容"]);
    //            //        }
    //            //    }
    //            //}
    //        }
    //        //リストの数
    //        new_cnt = list.Count-1;
    //    }                        
    //}


    //    DataGridView dgv = this.dataGridView1;


    //    Connect();
    //    string sql;
    //    sql = "update T_顧客 set 特記事項 = N'" + 備考.Text + "' where id =" + kokyaku_id;
    //    SqlCommand cmd = new SqlCommand(sql, cn);

    //    var transaction = cn.BeginTransaction();
    //    cmd.Transaction = transaction;

    //    try
    //    {
    //       cmd.ExecuteNonQuery();

    //        //確定されてない時　何故か1回endedit　を行うと2回目はendeditでもセルが確定されない。
    //        //入力セルを移動すると確定される
    //        //2回目はvalueが空白（DBNULL）になっている この条件でfalseにする

    //        if (dataGridView1.CurrentRow != null)
    //            if (dataGridView1.CurrentCell.EditedFormattedValue.ToString() == dataGridView1.CurrentCell.Value.ToString())
    //            {
    //                this.Validate();
    //                //this.koushoBindingSource.EndEdit();
    //                //this.koushoTableAdapter.Update(this.rentDataSet);
    //                MessageBox.Show("変更を保存しました");
    //                transaction.Commit();
    //                cn.Close();
    //            }
    //            else
    //            {
    //                MessageBox.Show(dataGridView1.CurrentCell.OwningColumn.HeaderText + "項目の「" + dataGridView1.CurrentCell.EditedFormattedValue.ToString() +
    // "」は確定されてません。\r\n そのセルを確定してください。違うセルを選択すれば確定されます。");
    //                transaction.Commit();
    //                cn.Close();
    //                return;
    //            }
    //        else
    //        {
    //            MessageBox.Show("変更を保存しました");
    //            transaction.Commit();
    //            cn.Close();
    //            return;
    //        }
    //    }
    //    catch (Exception err)
    //    {
    //        MessageBox.Show("保存できませんでした:" + err.Message);
    //        transaction.Rollback();
    //        cn.Close();
    //        return;
    //    }
    //    //約定額に登録のあるレコードがあればメッセージ
    //    foreach (DataGridViewRow row in dgv.Rows)
    //    {
    //        //最終行はインスタンスがないため null チェック
    //        if (dgv["約定額", row.Index].Value != null)
    //        {
    //            int tmpint = DBNull.Value.Equals(dgv["約定額", row.Index].Value) ? 0 : (int)(dgv["約定額", row.Index].Value);

    //            if (tmpint > 0)
    //            {
    //                MessageBox.Show("約定の登録があります。約定画面で確認してください");
    //                break;
    //            }
    //        }
    //    }
}
