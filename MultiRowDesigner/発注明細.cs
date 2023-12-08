using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GrapeCity.Win.BarCode.ValueType;
using GrapeCity.Win.MultiRow;
using u_net;
using u_net.Public;



namespace MultiRowDesigner
{
    public partial class 発注明細 : UserControl
    {
        public bool sortFlg = false;
        private DataTable dataTable = new DataTable();
        private DataGridView multiRow = new DataGridView();
        private SqlConnection cn;


        public GcMultiRow Detail
        {
            get
            {
                return gcMultiRow1;
            }
        }
        public string CurrentPartsCode
        {
            get
            {
                return gcMultiRow1.SelectedRows[0].Cells["部品コード"].Value.ToString();
            }
        }

        private bool IsOrderByOn
        {
            get
            {
                return sortFlg;
            }
        }
        //親フォームへの参照を保存
        //private F_発注 f_発注;

        public 発注明細()
        {
            InitializeComponent();
        }

        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }
        public void CancelOrderBy()
        {
            try
            {
                sortFlg = false;

                NumberDetails("行番号");
                ResetOrderColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CancelOrderBy - {ex.GetType().Name}: {ex.Message}");
            }
        }

        public int GetDetailNumber()
        {
            int num = 0;
            num = gcMultiRow1.RowCount + 1;
            return num;
        }

        private void gcMultiRow1_Sorted(object sender, EventArgs e)
        {
            sortFlg = true;
        }

        private void NumberDetails(string fieldName, long start = 1)
        {
            //明細番号を振り直す
            // FieldName - 番号を格納するフィールド名
            // start     - 開始番号

            try
            {
                DataTable dataTable = (DataTable)multiRow.DataSource;

                if (dataTable != null)
                {
                    // DataTable をループしてフィールドの値を更新
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        // 特定のフィールド（fieldName）の値を更新
                        dataTable.Rows[i][fieldName] = i + 1;
                    }

                    // DataGridView を再描画  おまじない的に．．．
                    multiRow.DataSource = null; // データソースを一度解除
                    multiRow.DataSource = dataTable; // 更新した DataTable を再セット
                }
                else
                {
                    Console.WriteLine("データソースがありません。");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"NumberDetails - {ex.GetType().Name}: {ex.Message}");
            }
        }

        private void ResetOrderColor()
        {
            try
            {
                // MultiRow の ButtonCell を探し、ForeColor を初期化
                foreach (Cell cell in gcMultiRow1.ColumnHeaders[0].Cells)
                {
                    if (cell is ButtonCell buttonCell)
                    {
                        // ButtonCell の ForeColor を初期値（黒）に設定
                        buttonCell.Style.ForeColor = Color.FromArgb(0, 0, 0);
                        break; // 色を変更したらループを抜ける
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ResetOrderColor - {ex.GetType().Name}: {ex.Message}");
            }
        }


        private int GetOrderAmount(int NeedAmounts, int Units = 1, int Stock = 0)
        {
            //発注数量を得る
            //'NeedAmounts - 必要数量
            //'Units       - 発注単位数量
            //'Stock       - 理論在庫数量

            int BuyAmounts = (NeedAmounts - Stock) < 0 ? 0 : (NeedAmounts - Stock);

            // 発注単位数量を考慮する
            return (BuyAmounts == 0) ? 0 : Units * ((BuyAmounts / Units) + ((BuyAmounts % Units == 0) ? 0 : 1));
        }

        private bool IsAbolished(string PartsCode)
        {
            //指定部品が廃止されているかどうかを判定する

            string strSQL = $"SELECT count(*) as cnt FROM M部品 WHERE 部品コード = '{PartsCode}' AND 廃止 = -1";
            Connect();
            int cnt = OriginalClass.GetScalar<int>(cn, strSQL);
            if (cnt == 0)
                return false;
            else
            {
                return true;
            }

        }

        public bool IsError(Cell controlObject, bool cancel)
        {
            F_発注 ParentForm = Application.OpenForms.OfType<F_発注>().FirstOrDefault();
            try
            {
                object varValue = controlObject.Value;
                string strName = controlObject.Name;
                string strMsg;
                bool isError = false;

                switch (strName)
                {
                    case "部品コード":
                        if (!cancel && string.IsNullOrEmpty((string)varValue))
                        {
                            MessageBox.Show("部品コードを未入力にすることはできません。\nこの発注部品を削除するときは、明細行を削除してください。",
                                "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            isError = true;
                        }

                        if (IsAbolished(((string)varValue).PadLeft(8, '0')))
                        {
                            if (MessageBox.Show("指定された部品は廃止されています。\nよろしいですか？", "確認", MessageBoxButtons.YesNo,
                                    MessageBoxIcon.Question) == DialogResult.No)
                            {
                                isError = true;
                            }
                        }
                        break;

                    case "品名":
                    case "型番":
                        if (string.IsNullOrEmpty((string)varValue))
                        {
                            MessageBox.Show("部品を選択するか、" + strName + " を入力してください。",
                                "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            isError = true;
                        }
                        break;

                    case "メーカー名":

                        if (ParentForm.InvManageOn && string.IsNullOrEmpty((string)varValue))
                        {
                            MessageBox.Show(strName + " を入力してください。",
                                "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            isError = true;
                        }
                        // 追加の制約やチェックがあればここに追加
                        break;

                    case "発注単価":
                        if (!FunctionClass.IsLimit_N(varValue, 12, 2, strName))
                        {
                            isError = true;
                        }
                        break;

                    case "必要数量":
                    case "発注数量":
                        if (!FunctionClass.IsLimit_N(varValue, 8, 2, strName))
                        {
                            isError = true;
                        }
                        break;

                    case "発注納期":
                        if (string.IsNullOrEmpty((string)varValue))
                        {
                            MessageBox.Show(strName + " を入力してください。",
                                "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            isError = true;
                        }
                        else if (!DateTime.TryParse((string)varValue, out _))
                        {
                            strMsg = "日付以外は入力できません。" + "\n\n" + strName;
                            MessageBox.Show(strMsg, "入力", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            isError = true;
                        }
                        break;

                    case "買掛区分":
                        if (string.IsNullOrEmpty((string)varValue))
                        {
                            MessageBox.Show(strName + " を入力してください。" + "\n\n"
                                + "※ 買掛区分は入庫時に確認されるため、わからない場合でも入力してください。",
                                "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            isError = true;
                        }
                        break;
                        // 他のケースも同様に追加

                }

                return isError;
            }
            catch (Exception ex)
            {
                Console.WriteLine("エラー: " + ex.Message);
                return true;
            }
        }

        //private bool IsErrorData(Form formObject)
        //{
        //    bool isErrorData = false;

        //    foreach (Control objControl in formObject.Controls)
        //    {
        //        // ここで objControl.ControlType の定義に基づいて判定を行う
        //        if ((objControl is TextBoxCell || objControl is ComboBoxCell) && objControl.Visible)
        //        {
        //            bool Cancel = true;
        //            if (IsError(objControl, Cancel))
        //            {
        //                objControl.Focus();
        //                isErrorData = true;
        //                break;
        //            }
        //        }
        //    }

        //    return isErrorData;
        //}

        private void ResetNumber()
        {
            try
            {
                DataTable dataTable = (DataTable)multiRow.DataSource;

                if (dataTable != null)
                {
                    // DataTable をループしてフィールドの値を更新
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        // 特定のフィールド（"明細番号"）の値を更新
                        int currentValue = Convert.ToInt32(dataTable.Rows[i]["明細番号"]);
                        dataTable.Rows[i]["明細番号"] = currentValue - 1;
                    }

                    // DataGridView を再描画  おまじない的に．．．
                    multiRow.DataSource = null; // データソースを一度解除
                    multiRow.DataSource = dataTable; // 更新した DataTable を再セット
                }
                else
                {
                    Console.WriteLine("データソースがありません。");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ResetNumber - {ex.GetType().Name}: {ex.Message}");
            }
        }

        private bool SaveNewParts(DateTime savedDate, string userCode)
        {
            //1レコードだけの登録なのか？？
            Connect();
            try
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM M部品", cn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    DataRow newRow = dataTable.NewRow();
                    string strCode = FunctionClass.採番(cn, "PAR"); // 採番メソッドを実装してください

                    newRow["部品コード"] = strCode.Substring(strCode.Length - 8); ;
                    newRow["品名"] = gcMultiRow1.Rows[0].Cells["品名"].Value;
                    newRow["型番"] = gcMultiRow1.Rows[0].Cells["型番"].Value;
                    newRow["仕入先1単価"] = gcMultiRow1.Rows[0].Cells["発注単価"].Value;
                    newRow["単位数量"] = 1;
                    newRow["随時登録"] = 1;
                    newRow["作成日時"] = savedDate;
                    newRow["作成者コード"] = userCode;

                    dataTable.Rows.Add(newRow);

                    // データベースに変更を反映
                    new SqlCommandBuilder(adapter);
                    adapter.Update(dataTable);

                    gcMultiRow1.Rows[0].Cells["部品コード"].Value = strCode;
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SaveNewParts - {ex.GetType().Name}: {ex.Message}");
                return false;
            }
        }

        private void SetPartsInfo(string codeString)
        {
            //最初の行に登録でよいのか？　カレントレコードに変更する必要あるか？
            Connect();
            try
            {
                using (SqlCommand command = new SqlCommand())
                {
                    command.Connection = cn;
                    string strSQL = "SELECT M部品.部品コード, " +
                                    "M部品.品名, " +
                                    "M部品.型番, " +
                                    "Mメーカー.メーカー名, " +
                                    "M部品.仕入先1単価 AS 発注単価, " +
                                    "M部品.入数, " +
                                    "M部品.単位数量 " +
                                    "FROM M部品 LEFT OUTER JOIN " +
                                    "Mメーカー ON M部品.メーカーコード = Mメーカー.メーカーコード " +
                                    $"WHERE M部品.部品コード = '{codeString}'";

                    command.CommandText = strSQL;

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            gcMultiRow1.Rows[0].Cells["品名"].Value = reader["品名"].ToString();
                            gcMultiRow1.Rows[0].Cells["型番"].Value = reader["型番"].ToString();
                            gcMultiRow1.Rows[0].Cells["メーカー名"].Value = reader["メーカー名"].ToString();
                            gcMultiRow1.Rows[0].Cells["発注単価"].Value = Convert.ToDecimal(reader["発注単価"]);
                            gcMultiRow1.Rows[0].Cells["入数"].Value = Convert.ToInt32(reader["入数"]);
                            gcMultiRow1.Rows[0].Cells["単位数量"].Value = Convert.ToInt32(reader["単位数量"]);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"SetPartsInfo - {ex.GetType().Name}: {ex.Message}");
            }
        }

        private void UpdatedControl(Cell cell)
        {
            F_発注 ParentForm = Application.OpenForms.OfType<F_発注>().FirstOrDefault();

            try
            {
                object varValue = cell.Value;
                int idx = cell.RowIndex;
                Connect();
                switch (cell.Name)
                {
                    case "部品コード":
                        // 部品情報を設定する
                        SetPartsInfo(varValue.ToString());

                        // 発注数量を設定する
                        if (!string.IsNullOrEmpty(gcMultiRow1.Rows[idx].Cells["発注納期"].Value?.ToString()) && ParentForm.在庫管理.Checked)
                        {

                            // 在庫管理するとき、かつ納期が指定されているとき
                            gcMultiRow1.Rows[idx].Cells["発注数量"].Value =
                                GetOrderAmount(int.Parse(gcMultiRow1.Rows[idx].Cells["必要数量"].Value?.ToString()),
                                int.Parse(gcMultiRow1.Rows[idx].Cells["単位数量"].Value?.ToString()),
                                FunctionClass.GetStock(cn,
                                DateTime.Parse(gcMultiRow1.Rows[idx].Cells["発注納期"].Value?.ToString()),
                                gcMultiRow1.Rows[idx].Cells["部品コード"].Value?.ToString()));

                        }
                        else
                        {
                            // 在庫管理しないとき、または納期が指定されていないとき
                            // （理論在庫数量 = 0 で計算）
                            gcMultiRow1.Rows[idx].Cells["発注数量"].Value =
                                 GetOrderAmount(int.Parse(gcMultiRow1.Rows[idx].Cells["必要数量"].Value?.ToString()),
                                int.Parse(gcMultiRow1.Rows[idx].Cells["単位数量"].Value?.ToString()), 0);

                        }

                        // 発注数量が０のときは警告する
                        if (gcMultiRow1.Rows[idx].Cells["発注数量"].Value == "0")
                        {
                            MessageBox.Show("発注数量が０です。\nこの部品を発注する必要はありません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        // メーカー名がない場合（履歴入力した場合）はメーカー名にフォーカスする
                        if (string.IsNullOrEmpty(gcMultiRow1.Rows[idx].Cells["メーカー名"].Value?.ToString()))
                        {
                            int columnIndex = gcMultiRow1.Rows[0].Cells["メーカー名"].CellIndex;
                            gcMultiRow1.CurrentCellPosition = new CellPosition(idx, columnIndex);

                        }
                        else
                        {
                            int columnIndex = gcMultiRow1.Rows[0].Cells["発注納期"].CellIndex;
                            gcMultiRow1.CurrentCellPosition = new CellPosition(idx, columnIndex);

                        }
                        break;

                    case "発注納期":
                    case "必要数量":
                        // 発注数量を設定する
                        if (!string.IsNullOrEmpty(gcMultiRow1.Rows[idx].Cells["発注納期"].Value?.ToString())
                            && !string.IsNullOrEmpty(gcMultiRow1.Rows[idx].Cells["部品コード"].Value?.ToString())
                            && ParentForm.在庫管理.Checked)
                        {
                            // 在庫管理するとき
                            gcMultiRow1.Rows[idx].Cells["発注数量"].Value =
                            GetOrderAmount(int.Parse(gcMultiRow1.Rows[idx].Cells["必要数量"].Value?.ToString()),
                                int.Parse(gcMultiRow1.Rows[idx].Cells["単位数量"].Value?.ToString()),
                                FunctionClass.GetStock(cn,
                                DateTime.Parse(gcMultiRow1.Rows[idx].Cells["発注納期"].Value?.ToString()),
                                gcMultiRow1.Rows[idx].Cells["部品コード"].Value?.ToString()));

                        }
                        else
                        {
                            // 在庫管理しないとき（理論在庫数量 = 0 で計算）
                            gcMultiRow1.Rows[idx].Cells["発注数量"].Value =
                                GetOrderAmount(int.Parse(gcMultiRow1.Rows[idx].Cells["必要数量"].Value?.ToString()),
                                int.Parse(gcMultiRow1.Rows[idx].Cells["単位数量"].Value?.ToString()), 0);
                        }

                        // 発注数量が０のときは警告する
                        if (gcMultiRow1.Rows[idx].Cells["発注数量"].Value?.ToString() == "0")
                        {
                            MessageBox.Show("発注数量が０です。\nこの部品を発注する必要はありません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }

                        // 適用消費税率を設定する accessではどこにも設定がないが．．．
                        //TaxRate.Text = FunctionClass.GetTaxRate(cn, DateTime.Parse(gcMultiRow1.Rows[idx].Cells["発注納期"].Value?.ToString())).ToString();

                        break;

                    case "品名":
                    case "型番":
                        gcMultiRow1.Rows[idx].Cells["部品コード"].Value = string.Empty;
                        ParentForm.blnNewParts = true;
                        break;

                    case "発注数量":
                        // 在庫管理をしない場合のみ必要数量と発注数量は一致させる
                        if (!ParentForm.在庫管理.Checked)
                        {
                            gcMultiRow1.Rows[idx].Cells["必要数量"].Value = gcMultiRow1.Rows[idx].Cells["発注数量"].Value;

                        }

                        if (gcMultiRow1.Rows[idx].Cells["発注数量"].Value?.ToString() == "0")
                        {
                            // 発注数量が０のときは警告する
                            MessageBox.Show("発注数量が０です。" +
                                "\nこの部品を発注する必要はありません。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else if (int.Parse(gcMultiRow1.Rows[idx].Cells["発注数量"].Value?.ToString()) <
                            int.Parse(gcMultiRow1.Rows[idx].Cells["必要数量"].Value?.ToString()))
                        {
                            // 必要数量が発注数量を超えているときは警告する
                            MessageBox.Show("必要数量が発注数量を超えています。" +
                                "\nこの状態で発注すると在庫が不足する恐れがあります。", "情報", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        break;

                    case "買掛区分":
                        string str = gcMultiRow1.Rows[idx].Cells["買掛区分コード"].Value?.ToString();
                        string sql = "select * from V買掛区分_発注 where 買掛区分='" + str + "'";

                        SqlCommand command = new SqlCommand(sql, cn);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            gcMultiRow1.Rows[idx].Cells["買掛区分コード"].Value = reader["買掛区分コード"].ToString();
                            gcMultiRow1.Rows[idx].Cells["買掛区分明細コード"].Value = Convert.ToInt16(reader["買掛区分明細コード"].ToString());
                        }
                        break;

                }
                cn.Close();

                // SetFocusNextメソッドを呼ぶ
                // SetFocusNext();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"UpdatedControl - {ex.GetType().Name}: {ex.Message}");
            }
        }

        private void gcMultiRow1_CellContentClick(object sender, CellEventArgs e)
        {

            //switch(gcMultiRow1.CurrentCell)
            //{
            //    //ボタンClick時の処理
            //    case ButtonCell:
            //        switch (e.CellName)
            //        {
            //            case "明細削除ボタン":
            //                MessageBox.Show("削除します。", "削除", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //                break;
            //            default:
            //                break;
            //        }
            //        break;

            //    default:
            //        break;
            //}

        }

        private void gcMultiRow1_CellEnter(object sender, CellEventArgs e)
        {

            //switch(gcMultiRow1.CurrentCell)
            //{
            //    //テキストボックスEnter時の処理
            //    case TextBoxCell:
            //        switch (e.CellName)
            //        {
            //            case "部品コード":
            //                this.f_発注.toolStripStatusLabel1.Text = "■部品コードを8文字以内で入力します。　■ダブルクリックするか、[space]キーを押して部品選択ウィンドウを表示します。　■マウスの左ボタンを押しながら右ボタンをクリックすると入力履歴を表示します。";
            //                break;
            //            case "品名":
            //                this.f_発注.toolStripStatusLabel1.Text = "■全角２５文字まで入力できます。";
            //                break;
            //            case "型番":
            //                this.f_発注.toolStripStatusLabel1.Text = "■半角５０文字まで入力できます。";
            //                break;
            //            case "メーカー名":
            //                this.f_発注.toolStripStatusLabel1.Text = "■全角２５文字まで入力できます。";
            //                break;
            //            case "入数":
            //                this.f_発注.toolStripStatusLabel1.Text = "■部品の現在の入数です。　■修正は出来ません。";
            //                break;
            //            case "発注納期":
            //                this.f_発注.toolStripStatusLabel1.Text = "■ダブルクリックするか、[space]キーを押してカレンダーを開くことができます。";
            //                break;
            //            case "必要数量":
            //                this.f_発注.toolStripStatusLabel1.Text = "■実際に必要な数量を入力します。";
            //                break;
            //            case "発注数量":
            //                this.f_発注.toolStripStatusLabel1.Text = "■仕入先に発注する数量を入力します。　■在庫管理する場合は自動的に計算されます。";
            //                break;
            //            case "発注単価":
            //                this.f_発注.toolStripStatusLabel1.Text = "";
            //                break;
            //            case "回答納期":
            //                this.f_発注.toolStripStatusLabel1.Text = "■ダブルクリックするか、[space]キーを押してカレンダーを開くことができます。";
            //                break;
            //            case "買掛区分":
            //                this.f_発注.toolStripStatusLabel1.Text = "■買掛区分を選択します。　■確定後入力するには入力欄をダブルクリックしてください。";
            //                break;
            //            default:
            //                this.f_発注.toolStripStatusLabel1.Text = "各種項目の説明";
            //                break;
            //        }
            //        break;

            //    default:
            //        break;
            //}

        }

        private void gcMultiRow1_RowsAdded(object sender, RowsAddedEventArgs e)
        {
            F_発注 ParentForm = Application.OpenForms.OfType<F_発注>().FirstOrDefault();

            gcMultiRow1.CurrentRow.Cells["発注コード"].Value = ParentForm.CurrentCode;
            gcMultiRow1.CurrentRow.Cells["発注版数"].Value = ParentForm.CurrentEdition;
            gcMultiRow1.CurrentRow.Cells["明細番号"].Value = GetDetailNumber();
            gcMultiRow1.CurrentRow.Cells["行番号"].Value = gcMultiRow1.RowCount + 1;
            gcMultiRow1.CurrentRow.Cells["必要数量"].Value = 1;
            gcMultiRow1.CurrentRow.Cells["発注数量"].Value = 1;
            gcMultiRow1.CurrentRow.Cells["単位数量"].Value = 1;

        }

        private void gcMultiRow1_RowsAdding(object sender, RowsAddingEventArgs e)
        {
            //accessのForm_BeforeUpdateの処理

            if (IsError(gcMultiRow1.CurrentRow.Cells["部品コード"], true)) e.Cancel = true;
            if (IsError(gcMultiRow1.CurrentRow.Cells["品名"], true)) e.Cancel = true;
            if (IsError(gcMultiRow1.CurrentRow.Cells["型番"], true)) e.Cancel = true;
            if (IsError(gcMultiRow1.CurrentRow.Cells["メーカー名"], true)) e.Cancel = true;
            if (IsError(gcMultiRow1.CurrentRow.Cells["発注単価"], true)) e.Cancel = true;
            if (IsError(gcMultiRow1.CurrentRow.Cells["必要数量"], true)) e.Cancel = true;
            if (IsError(gcMultiRow1.CurrentRow.Cells["発注数量"], true)) e.Cancel = true;
            if (IsError(gcMultiRow1.CurrentRow.Cells["発注納期"], true)) e.Cancel = true;
            if (IsError(gcMultiRow1.CurrentRow.Cells["買掛区分"], true)) e.Cancel = true;

            if (string.IsNullOrEmpty(gcMultiRow1.CurrentRow.Cells["部品コード"].Value?.ToString()) &&
                !string.IsNullOrEmpty(gcMultiRow1.CurrentRow.Cells["部品コード"].Value?.ToString()) &&
                !string.IsNullOrEmpty(gcMultiRow1.CurrentRow.Cells["部品コード"].Value?.ToString()))
            {
                Connect();
                if (!SaveNewParts(FunctionClass.GetServerDate(cn), CommonConstants.LoginUserCode))
                {
                    e.Cancel = true;
                }
            }

            //accessのForm_AfterUpdateの処理
            if (string.IsNullOrEmpty(gcMultiRow1.CurrentRow.Cells["必要数量"].Value?.ToString()))
            {
                gcMultiRow1.CurrentRow.Cells["必要数量"].Value = gcMultiRow1.CurrentRow.Cells["発注数量"].Value;
            }

        }

        private void gcMultiRow1_EditingControlShowing(object sender, EditingControlShowingEventArgs e)
        {

            TextBoxEditingControl textBox = e.Control as TextBoxEditingControl;
            ComboBoxEditingControl comboBox = e.Control as ComboBoxEditingControl;
            if (textBox != null)
            {
                textBox.PreviewKeyDown -= gcMultiRow1_PreviewKeyDown;
                textBox.PreviewKeyDown += gcMultiRow1_PreviewKeyDown;
                textBox.KeyPress -= new KeyPressEventHandler(gcMultiRow1_KeyPress);
                textBox.KeyPress += new KeyPressEventHandler(gcMultiRow1_KeyPress);
            }
            else if (comboBox != null)
            {
                comboBox.PreviewKeyDown -= gcMultiRow1_PreviewKeyDown;
                comboBox.PreviewKeyDown += gcMultiRow1_PreviewKeyDown;
                comboBox.KeyPress -= new KeyPressEventHandler(gcMultiRow1_KeyPress);
                comboBox.KeyPress += new KeyPressEventHandler(gcMultiRow1_KeyPress);
            }
        }


        private void gcMultiRow1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            gcMultiRow1.EndEdit();

            if (e.KeyCode == Keys.Return)
            {

                if (gcMultiRow1.CurrentCell.RowIndex == null || gcMultiRow1.CurrentCell.CellIndex == null) return;

                if (gcMultiRow1.CurrentCell.Name == "部品コード")
                {
                    string strCode = gcMultiRow1.CurrentCell.Value.ToString();
                    string formattedCode = strCode.Trim().PadLeft(8, '0');

                    if (formattedCode != strCode || string.IsNullOrEmpty(strCode))
                    {
                        gcMultiRow1.CurrentCell.Value = formattedCode;
                    }
                }
            }
        }

        private void gcMultiRow1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                if (gcMultiRow1.CurrentCell.RowIndex == null || gcMultiRow1.CurrentCell.CellIndex == null) return;

                switch (gcMultiRow1.CurrentCell.Name)
                {
                    case "買掛区分":
                        ComboBoxCell combo = gcMultiRow1.CurrentCell as ComboBoxCell;
                        //コンボボックスのドロップダウンを行う処理が必要
                        e.Handled = true;
                        break;

                    case "部品コード":
                        //F_部品選択が必要
                        break;
                }
            }
        }

        private void gcMultiRow1_CellValidating(object sender, CellValidatingEventArgs e)
        {
            //BeforeUpdateの処理
            //   gcMultiRow1.EndEdit();

            //switch (e.CellName)
            //{
            //    case "メーカー名":
            //        if (IsError(gcMultiRow1.CurrentCell) == true) e.Cancel = true;

            //        // 入庫数量が発注数量を超えた場合、確認する。
            //        if (string.IsNullOrEmpty(gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["発注残数量"].Value?.ToString()) || string.IsNullOrEmpty(gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["入庫数量"].Value?.ToString())) return;
            //        if (Math.Abs(Convert.ToDecimal(gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["発注残数量"].Value)) < Math.Abs(Convert.ToDecimal(gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["入庫数量"].Value)) - Math.Abs(varPre入庫数量))
            //        {
            //            DialogResult result = MessageBox.Show("入庫数量が発注残数量を超えています。\nよろしいですか？", "確認", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            //            if (result == DialogResult.No)
            //            {
            //                e.Cancel = true;
            //                return;
            //            }
            //        }

            //        // 発注残数量を更新する
            //        gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["発注残数量"].Value = Convert.ToDecimal(gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["発注残数量"].Value) - (Convert.ToDecimal(gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["入庫数量"].Value) - Math.Abs(varPre入庫数量));

            //        if (Convert.ToDecimal(gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["発注残数量"].Value.ToString()) == 0)
            //        {
            //            gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["全入庫"].Value = "■";
            //        }
            //        else
            //        {
            //            gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["全入庫"].Value = "";
            //        }

            //        if (Convert.ToDecimal(gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["発注残数量"].Value.ToString()) < 0)
            //        {
            //            gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["発注残数量"].Style.ForeColor = Color.Red;
            //        }
            //        else
            //        {
            //            gcMultiRow1.Rows[gcMultiRow1.CurrentCell.RowIndex].Cells["発注残数量"].Style.ForeColor = Color.Black;
            //        }

            //        break;

            //    case "買掛区分":
            //        if (IsError(gcMultiRow1.CurrentCell) == true) e.Cancel = true;
            //        break;

            //    case "入庫単価":
            //        if (IsError(gcMultiRow1.CurrentCell) == true) e.Cancel = true;
            //        break;

            //}

        }

        private void gcMultiRow1_ModifiedChanged(object sender, EventArgs e)
        {
            F_発注? Parentform = Application.OpenForms.OfType<F_発注>().FirstOrDefault();
            Parentform.ChangedData(true);
        }

        private void gcMultiRow1_RowsRemoved(object sender, RowsRemovedEventArgs e)
        {
            F_発注? Parentform = Application.OpenForms.OfType<F_発注>().FirstOrDefault();
            Parentform.ChangedData(true);
        }

        private void 発注明細_Load(object sender, EventArgs e)
        {
            gcMultiRow1.ShortcutKeyManager.Unregister(Keys.Enter);
            gcMultiRow1.ShortcutKeyManager.Register(SelectionActions.MoveToNextCell, Keys.Enter);

        }


        private void gcMultiRow1_CellContentButtonClick(object sender, CellEventArgs e)
        {
            switch (e.CellName)
            {
                case "メーカー名ボタン":
                    int idx = gcMultiRow1.CurrentRow.Index;
                    int columnIndex = gcMultiRow1.Rows[0].Cells["メーカー名"].CellIndex;
                    gcMultiRow1.CurrentCellPosition = new CellPosition(idx, columnIndex);
                    break;

                case "型番ボタン":
                    int idx2 = gcMultiRow1.Rows[0].Cells["型番"].CellIndex;
                    gcMultiRow1.CurrentCellPosition = new CellPosition(0, idx2);
                    break;

                case "納期ボタン":
                    int idx3 = gcMultiRow1.Rows[0].Cells["納期"].CellIndex;
                    gcMultiRow1.CurrentCellPosition = new CellPosition(0, idx3);
                    break;

                case "買掛区分ボタン":
                    int idx4 = gcMultiRow1.Rows[0].Cells["買掛区分"].CellIndex;
                    gcMultiRow1.CurrentCellPosition = new CellPosition(0, idx4);
                    break;

                case "品名ボタン":
                    int idx5 = gcMultiRow1.Rows[0].Cells["品名"].CellIndex;
                    gcMultiRow1.CurrentCellPosition = new CellPosition(0, idx5);
                    break;

                case "部品コードボタン":
                    int idx6 = gcMultiRow1.Rows[0].Cells["部品コード"].CellIndex;
                    gcMultiRow1.CurrentCellPosition = new CellPosition(0, idx6);
                    break;

            }
        }



    }
}
