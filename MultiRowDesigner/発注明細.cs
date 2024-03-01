using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using System.Windows.Forms;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.InkML;
using GrapeCity.Win.BarCode.ValueType;
using GrapeCity.Win.MultiRow;
using Microsoft.IdentityModel.Tokens;
using u_net;
using u_net.Public;



namespace MultiRowDesigner
{
    public partial class 発注明細 : UserControl
    {
        public bool sortFlg = false;
        public bool validateFlg = false; 　//validateを行うかのフラグ　変更してる場合validatingでtrueをセット　validatedを実行させない
        private DataTable dataTable = new DataTable();
        private Dictionary<int, bool> rowChanged = new Dictionary<int, bool>();

        private SqlConnection cn;
        F_部品選択 form = new F_部品選択();

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
                var currentRow = gcMultiRow1.CurrentRow;

                if (currentRow != null && !string.IsNullOrEmpty(gcMultiRow1.CurrentRow.Cells["部品コード"].Value?.ToString()))
                {
                    return gcMultiRow1.CurrentRow.Cells["部品コード"].Value?.ToString();
                }
                else
                {
                    // 選択された行がない場合の処理
                    return "";
                }

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


        private void 発注明細_Load(object sender, EventArgs e)
        {
            gcMultiRow1.ShortcutKeyManager.Unregister(Keys.Enter);
            gcMultiRow1.ShortcutKeyManager.Register(SelectionActions.MoveToNextCell, Keys.Enter);

            gcMultiRow1.ProcessFirstClick = false;
            // gcMultiRow1.["発注数量"].ForeColor = Color.Red;
        }

        private void gcMultiRow1_DefaultValuesNeeded(object sender, RowEventArgs e)
        {
            F_発注 ParentForm = Application.OpenForms.OfType<F_発注>().FirstOrDefault();

            e.Row.Cells["発注コード"].Value = ParentForm.CurrentCode;
            e.Row.Cells["発注版数"].Value = ParentForm.CurrentEdition;
            e.Row.Cells["明細番号"].Value = GetDetailNumber();

            e.Row.Cells["必要数量"].Value = 1;
            e.Row.Cells["発注数量"].Value = 1;
            e.Row.Cells["単位数量"].Value = 1;

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

                textBox.DoubleClick -= gcMultiRow1_CellDoubleClick;
                textBox.DoubleClick += gcMultiRow1_CellDoubleClick;

            }
            else if (comboBox != null)
            {
                comboBox.PreviewKeyDown -= gcMultiRow1_PreviewKeyDown;
                comboBox.PreviewKeyDown += gcMultiRow1_PreviewKeyDown;
                comboBox.KeyPress -= new KeyPressEventHandler(gcMultiRow1_KeyPress);
                comboBox.KeyPress += new KeyPressEventHandler(gcMultiRow1_KeyPress);

                comboBox.DoubleClick -= gcMultiRow1_CellDoubleClick;
                comboBox.DoubleClick += gcMultiRow1_CellDoubleClick;
                comboBox.DoubleClick -= comboBox_CellDoubleClick;
                comboBox.DoubleClick += comboBox_CellDoubleClick;


                if (gcMultiRow1.CurrentCell.Name == "買掛区分")
                {
                    comboBox.DrawMode = DrawMode.OwnerDrawFixed;
                    comboBox.DrawItem -= 買掛区分_DrawItem;
                    comboBox.DrawItem += 買掛区分_DrawItem;
                    comboBox.SelectedIndexChanged -= 買掛区分_SelectedIndexChanged;
                    comboBox.SelectedIndexChanged += 買掛区分_SelectedIndexChanged;

                }
                else
                {
                    comboBox.DrawMode = DrawMode.Normal;
                }
            }
        }

        private void 買掛区分_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBoxEditingControl combo = sender as ComboBoxEditingControl;
            OriginalClass.SetComboBoxAppearance(combo, e, new int[] { 150, 0, 0 }, new string[] { "Display", "Display2", "Display3" });
        }

        private void 買掛区分_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEditingControl combo = sender as ComboBoxEditingControl;
            gcMultiRow1.CurrentRow.Cells["買掛区分コード"].Value = ((DataRowView)combo.SelectedItem)?.Row.Field<String>("Display2")?.ToString();
            gcMultiRow1.CurrentRow.Cells["買掛明細コード"].Value = ((DataRowView)combo.SelectedItem)?.Row.Field<Int16?>("Display3")?.ToString();
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
            DataTable dataTable = (DataTable)gcMultiRow1.DataSource;
            int num = 0;
            if (dataTable != null && dataTable.Rows.Count > 0)
            {
                int max明細番号 = dataTable.AsEnumerable()
                    .Max(row => row.Field<int>("明細番号"));

                num = max明細番号 + 1;

            }
            return num;
        }

        private void gcMultiRow1_Sorted(object sender, EventArgs e)
        {
            sortFlg = true;
            //ソート後に明細番号の振り直し
            NumberDetails("明細番号");
        }

        private void NumberDetails(string fieldName, long start = 1)
        {
            //明細番号を振り直す
            // FieldName - 番号を格納するフィールド名
            // start     - 開始番号

            try
            {
                DataTable dataTable = (DataTable)gcMultiRow1.DataSource;

                if (dataTable != null)
                {
                    int rowcnt = 1;
                    // DataTable をループしてフィールドの値を更新
                    for (int i = 0; i < dataTable.Rows.Count; i++)
                    {
                        // 行の状態が Deleted の時は次の行へ
                        if (dataTable.Rows[i].RowState == DataRowState.Deleted)
                        {
                            continue;
                        }

                        // 特定のフィールド（fieldName）の値を更新
                        dataTable.Rows[i][fieldName] = rowcnt;
                        rowcnt++;
                    }

                    gcMultiRow1.DataSource = dataTable; // 更新した DataTable を再セット
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

        private void gcMultiRow1_CellFormatting(object sender, CellFormattingEventArgs e)
        {
            //セルがマイナスの場合の処理
            // ヘッダーセルの場合は無視

            if (gcMultiRow1.Rows.Count == 0 || e.RowIndex < 0 || e.RowIndex >= gcMultiRow1.Rows.Count)
                return;

            // 行が存在しない場合は処理をスキップ
            if (gcMultiRow1.Rows.Count == 0)
                return;

            string columnName = gcMultiRow1.Columns[e.CellIndex].Name;

            // セルの値が数値で、かつマイナスの場合
            if (!gcMultiRow1.Rows[e.RowIndex].IsNewRow && (columnName == "発注数量")
                && e.Value != null && e.Value != DBNull.Value)
            {
                if (Convert.ToDecimal(e.Value) < 0)
                {   // 赤色のフォントを設定
                    e.CellStyle.ForeColor = Color.Red;
                }
            }
        }
        private void ResetNumber()
        {
            try
            {
                DataTable dataTable = (DataTable)gcMultiRow1.DataSource;

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
                    gcMultiRow1.DataSource = null; // データソースを一度解除
                    gcMultiRow1.DataSource = dataTable; // 更新した DataTable を再セット
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

            Connect();
            try
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM M部品", cn))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    DataRow newRow = dataTable.NewRow();
                    string strCode = FunctionClass.採番(cn, "PAR"); // 採番メソッドを実装してください

                    newRow["部品コード"] = strCode.Substring(strCode.Length - 8);
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

                    gcMultiRow1.Rows[0].Cells["部品コード"].Value = strCode.Substring(strCode.Length - 8); ;
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

                            gcMultiRow1.CurrentRow.Cells["品名"].Value = reader["品名"].ToString();
                            gcMultiRow1.CurrentRow.Cells["型番"].Value = reader["型番"].ToString();
                            gcMultiRow1.CurrentRow.Cells["メーカー名"].Value = reader["メーカー名"].ToString();
                            gcMultiRow1.CurrentRow.Cells["発注単価"].Value = Convert.ToDecimal(reader["発注単価"]);
                            gcMultiRow1.CurrentRow.Cells["入数"].Value = Convert.ToInt32(reader["入数"]);
                            gcMultiRow1.CurrentRow.Cells["単位数量"].Value = Convert.ToInt32(reader["単位数量"]);

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

                        ParentForm.ChangedData(true);

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
                        string str = gcMultiRow1.Rows[idx].Cells["買掛区分"].Value?.ToString();
                        string sql = "select * from V買掛区分_発注 where 買掛区分='" + str + "'";

                        SqlCommand command = new SqlCommand(sql, cn);
                        SqlDataReader reader = command.ExecuteReader();
                        if (reader.Read())
                        {
                            gcMultiRow1.Rows[idx].Cells["買掛区分コード"].Value = reader["買掛区分コード"].ToString();
                            gcMultiRow1.Rows[idx].Cells["買掛明細コード"].Value = Convert.ToInt16(reader["買掛明細コード"].ToString());
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



        private void gcMultiRow1_CellEnter(object sender, CellEventArgs e)
        {
            F_発注? parentform = Application.OpenForms.OfType<F_発注>().FirstOrDefault();


            switch (e.CellName)
            {
                case "部品コード":
                    parentform.toolStripStatusLabel1.Text = "■部品コードを8文字以内で入力します。　■ダブルクリックするか、[space]キーを押して部品選択ウィンドウを表示します。　■マウスの左ボタンを押しながら右ボタンをクリックすると入力履歴を表示します。";
                    break;
                case "品名":
                    parentform.toolStripStatusLabel1.Text = "■全角２５文字まで入力できます。";
                    break;
                case "型番":
                    parentform.toolStripStatusLabel1.Text = "■半角５０文字まで入力できます。";
                    break;
                case "メーカー名":
                    parentform.toolStripStatusLabel1.Text = "■全角２５文字まで入力できます。";
                    break;
                case "入数":
                    parentform.toolStripStatusLabel1.Text = "■部品の現在の入数です。　■修正は出来ません。";
                    break;
                case "発注納期":
                    parentform.toolStripStatusLabel1.Text = "■ダブルクリックするか、[space]キーを押してカレンダーを開くことができます。";
                    break;
                case "必要数量":
                    parentform.toolStripStatusLabel1.Text = "■実際に必要な数量を入力します。";
                    break;
                case "発注数量":
                    parentform.toolStripStatusLabel1.Text = "■仕入先に発注する数量を入力します。　■在庫管理する場合は自動的に計算されます。";
                    break;
                case "発注単価":
                    parentform.toolStripStatusLabel1.Text = "";
                    break;
                case "回答納期":
                    parentform.toolStripStatusLabel1.Text = "■ダブルクリックするか、[space]キーを押してカレンダーを開くことができます。";
                    break;
                case "買掛区分":
                    ComboBoxEditingControl combo = sender as ComboBoxEditingControl;
                    if (combo != null) combo.DroppedDown = true;
                    parentform.toolStripStatusLabel1.Text = "■買掛区分を選択します。　■確定後入力するには左の「修正」ボタンをクリックしてください。";
                    break;
                default:
                    parentform.toolStripStatusLabel1.Text = "各種項目の説明";
                    break;
            }
            //break;

            //    default:
            //        break;
            //}
        }



        private void gcMultiRow1_RowsAdding(object sender, RowsAddingEventArgs e)
        {
            //accessのForm_BeforeUpdateの処理

            //if (IsError(gcMultiRow1.CurrentRow.Cells["部品コード"], true)) e.Cancel = true;
            //if (IsError(gcMultiRow1.CurrentRow.Cells["品名"], true)) e.Cancel = true;
            //if (IsError(gcMultiRow1.CurrentRow.Cells["型番"], true)) e.Cancel = true;
            //if (IsError(gcMultiRow1.CurrentRow.Cells["メーカー名"], true)) e.Cancel = true;
            //if (IsError(gcMultiRow1.CurrentRow.Cells["発注単価"], true)) e.Cancel = true;
            //if (IsError(gcMultiRow1.CurrentRow.Cells["必要数量"], true)) e.Cancel = true;
            //if (IsError(gcMultiRow1.CurrentRow.Cells["発注数量"], true)) e.Cancel = true;
            //if (IsError(gcMultiRow1.CurrentRow.Cells["発注納期"], true)) e.Cancel = true;
            //if (IsError(gcMultiRow1.CurrentRow.Cells["買掛区分"], true)) e.Cancel = true;
            //var targetRow = e.RowIndex;
            //if (IsErrorData(targetRow))            
            //{
            //    e.Cancel = true;
            //    return;
            //}

            //if (string.IsNullOrEmpty(gcMultiRow1.CurrentRow.Cells["部品コード"].Value?.ToString()) &&
            //    !string.IsNullOrEmpty(gcMultiRow1.CurrentRow.Cells["部品コード"].Value?.ToString()) &&
            //    !string.IsNullOrEmpty(gcMultiRow1.CurrentRow.Cells["部品コード"].Value?.ToString()))
            //{
            //    Connect();
            //    if (!SaveNewParts(FunctionClass.GetServerDate(cn), CommonConstants.LoginUserCode))
            //    {
            //        e.Cancel = true;
            //    }
            //}

            ////accessのForm_AfterUpdateの処理
            //if (string.IsNullOrEmpty(gcMultiRow1.CurrentRow.Cells["必要数量"].Value?.ToString()))
            //{
            //    gcMultiRow1.CurrentRow.Cells["必要数量"].Value = gcMultiRow1.CurrentRow.Cells["発注数量"].Value;
            //}

        }

        private string IsErrorData(int rowIndex, bool cancel)
        {
            F_発注 ParentForm = Application.OpenForms.OfType<F_発注>().FirstOrDefault();
            bool isError = false;
            try
            {
                // セルのループ
                foreach (var cell in gcMultiRow1.Rows[rowIndex].Cells)
                {
                    // セル内のコントロールにアクセス
                    var cellControl = cell;
                    var varValue = cell.Value;
                    string strName = cell.Name;
                    string strMsg;

                    //money型は少数第4位まであるため、value値が少数第3位以下
                    if (varValue is decimal decimalValue)
                    {
                        // 小数第3位以下が00の場合にフォーマット
                        if (decimalValue * 100 % 1 == 0)
                        {
                            varValue = decimalValue.ToString("F2"); // 少数第2位まで表示
                        }
                    }

                    switch (strName)
                    {
                        case "部品コード":
                            if (!cancel && string.IsNullOrEmpty((string)varValue))
                            {
                                MessageBox.Show("部品コードを未入力にすることはできません。\nこの発注部品を削除するときは、明細行を削除してください。",
                                    "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return strName;
                            }

                            //if (IsAbolished(((string)varValue).PadLeft(8, '0')))
                            //{
                            //    if (MessageBox.Show("指定された部品は廃止されています。\nよろしいですか？", "確認", MessageBoxButtons.YesNo,
                            //            MessageBoxIcon.Question) == DialogResult.No)
                            //    {

                            //        return null;
                            //    }
                            //}
                            break;

                        case "品名":
                        case "型番":
                            if (string.IsNullOrEmpty((string)varValue))
                            {
                                MessageBox.Show("部品を選択するか、" + strName + " を入力してください。",
                                    "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return strName;
                            }
                            break;

                        case "メーカー名":

                            if (ParentForm.InvManageOn && string.IsNullOrEmpty((string)varValue))
                            {
                                MessageBox.Show(strName + " を入力してください。",
                                    "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return strName;
                            }
                            // 追加の制約やチェックがあればここに追加
                            break;

                        case "発注単価":

                            if (!FunctionClass.IsLimit_N(varValue, 12, 2, strName))
                            {
                                return strName;
                            }
                            break;

                        case "必要数量":
                        case "発注数量":


                            if (!FunctionClass.IsLimit_N(varValue, 8, 2, strName))
                            {
                                return strName;
                            }
                            break;

                        case "発注納期":
                            if (varValue == DBNull.Value)
                            {
                                MessageBox.Show(strName + " を入力してください。",
                                    "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return strName;
                            }
                            else if (varValue is not DateTime)
                            {
                                strMsg = "日付以外は入力できません。" + "\n\n" + strName;
                                MessageBox.Show(strMsg, "入力", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                return strName;
                            }
                            break;

                        case "買掛区分":
                            if (varValue == DBNull.Value)
                            {
                                MessageBox.Show(strName + " を入力してください。" + "\n\n"
                                    + "※ 買掛区分は入庫時に確認されるため、わからない場合でも入力してください。",
                                    "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return strName;
                            }
                            break;


                    }

                }

                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine("エラー: " + ex.Message);
                return null;
            }
        }

        private void gcMultiRow1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            // gcMultiRow1.EndEdit();

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

        private void gcMultiRow1_CellValidating(object sender, CellValidatingEventArgs e)
        {
            //更新前の値の取得
            //object oldValue = e.FormattedValue;

            // BeforeUpdateの処理
            //gcMultiRow1.EndEdit();

            switch (e.CellName)
            {
                case "メーカー名":
                case "回答納期":
                case "型番":
                case "買掛区分":
                case "発注数量":
                case "発注単価":
                case "発注納期":
                case "必要数量":
                case "品名":
                case "部品コード":
                    string oldValue = "";
                    validateFlg = false;
                    GcMultiRow grid = (GcMultiRow)sender;
                    // セルが編集中の場合
                    if (grid.IsCurrentCellInEditMode)
                    {
                        int currentRowIndex = e.RowIndex;

                        //string a = gcMultiRow1.Rows[currentRowIndex].Cells["発注納期"].Value.ToString();
                        if (spaceFlg)
                        {
                            oldValue = gcMultiRow1.CurrentCell.DisplayText;
                        }
                        else
                        {
                            oldValue = e.FormattedValue?.ToString() ?? string.Empty;
                        }



                        // 値が変更されていなければエラーチェックを行わない validatedも実行しない様にするためフラグをfalseに
                        //ダブルクリックイベントから入力後に来た時はDisplayTextが変更後の値になっているので、dblflgでチェックする
                        if (grid.EditingControl.Text == gcMultiRow1.CurrentCell.DisplayText && dblflg == false)
                        {
                            spaceFlg = false;
                            validateFlg = false;
                            return;
                        }

                        //フラグを戻す
                        spaceFlg = false;
                        dblflg = false;
                        //validatedも実行するためフラグをtrueに
                        validateFlg = true;
                        // 編集用コントロールに不正な文字列が設定されている場合
                        if (IsError(grid.EditingControl, false, e.CellName, oldValue) == true)
                        {
                            // 元の値に戻す
                            grid.EditingControl.Text = gcMultiRow1.CurrentCell.DisplayText;
                            e.Cancel = true;

                        }

                        if (e.CellName == "発注数量")
                        {
                            // e.FormattedValue が新しい値（ユーザーが入力しようとしている値）
                            // 単位数量のセル値を取得する必要がある
                            var rowIndex = e.RowIndex; // 編集中の行インデックス
                            var 発注数量 = Convert.ToDecimal(e.FormattedValue); // 新しい発注数量を取得
                            var 単位数量Cell = gcMultiRow1[rowIndex, "単位数量"]; // 単位数量セルを特定
                            var 単位数量 = Convert.ToDecimal(単位数量Cell.Value); // 単位数量を取得
                            string oldvalue = gcMultiRow1.CurrentCell.DisplayText;

                            // 発注単位数量に依存しない数量が指定されたときは警告する
                            if (発注数量 % 単位数量 != 0)
                            {
                                var message = $"この部品の発注単位数量は {単位数量:N0} です。\n" +
                                              "通常、入力した数量を発注することはありません。\n\n" +
                                              "よろしいですか？";
                                var caption = "発注数量";
                                var result = MessageBox.Show(message, caption, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                if (result == DialogResult.No)
                                {
                                    e.Cancel = true;
                                    grid.EditingControl.Text = gcMultiRow1.CurrentCell.DisplayText;

                                }
                            }

                        }

                    }
                    break;
            }

        }

        public bool IsError(Control controlObject, bool cancel, string strName, object varValue)
        {
            F_発注 ParentForm = Application.OpenForms.OfType<F_発注>().FirstOrDefault();
            try
            {

                //object varValue = controlObject.Text;
                //string strName = controlObject.Name;
                string strMsg;
                bool isError = false;


                if (varValue is decimal decimalValue)
                {
                    // 小数第3位以下が00の場合にフォーマット
                    if (decimalValue * 100 % 1 == 0)
                    {
                        varValue = decimalValue.ToString("F2"); // 少数第2位まで表示
                    }
                }

                switch (strName)
                {
                    case "部品コード":
                        if (!cancel && string.IsNullOrEmpty((string)varValue))
                        {
                            MessageBox.Show("部品コードを未入力にすることはできません。\nこの発注部品を削除するときは、明細行を削除してください。",
                                "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            isError = true;
                        }

                        //if (IsAbolished(((string)varValue).PadLeft(8, '0')))
                        //{
                        //    if (MessageBox.Show("指定された部品は廃止されています。\nよろしいですか？", "確認", MessageBoxButtons.YesNo,
                        //            MessageBoxIcon.Question) == DialogResult.No)
                        //    {

                        //        isError = true;
                        //    }
                        //}
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
                        if (varValue == DBNull.Value || varValue == "")
                        {
                            MessageBox.Show(strName + " を入力してください。",
                                "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            isError = true;
                        }
                        else if (!DateTime.TryParse(varValue.ToString(), out _))
                        {
                            strMsg = "日付以外は入力できません。" + "\n\n" + strName;
                            MessageBox.Show(strMsg, "入力", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            isError = true;
                        }
                        break;

                    case "買掛区分":
                        //コンボボックスはeditcontrolの挙動が異なるのでeditcontrolから値を取得
                        if (varValue == "")
                        {
                            MessageBox.Show(strName + " を入力してください。" + "\n\n"
                                + "※ 買掛区分は入庫時に確認されるため、わからない場合でも入力してください。",
                                "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            isError = true;
                        }
                        break;

                }

                return isError;
            }
            catch (Exception ex)
            {
                Console.WriteLine("エラー: " + ex.Message);
                return true;
            }
        }

        private void gcMultiRow1_RowsRemoved(object sender, RowsRemovedEventArgs e)
        {
            F_発注? Parentform = Application.OpenForms.OfType<F_発注>().FirstOrDefault();
            Parentform.ChangedData(true);
            NumberDetails("明細番号");
        }

        private void gcMultiRow1_CellContentButtonClick(object sender, CellEventArgs e)
        {
            GcMultiRow gcMultiRow = sender as GcMultiRow;
            F_発注? parentform = Application.OpenForms.OfType<F_発注>().FirstOrDefault();
            switch (e.CellName)
            {
                case "行番号ボタン":

                    if (IsOrderByOn)
                    {
                        DialogResult result = MessageBox.Show("並べ替えを解除してもよろしいですか？",
                            "並べ替え解除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (result == DialogResult.Yes)
                        {
                            CancelOrderBy();
                        }
                    }

                    int idx = gcMultiRow1.CurrentRow.Index;
                    int columnIndex = gcMultiRow1.Rows[0].Cells["部品コード"].CellIndex;
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
                case "明細削除ボタン":
                    // 新規行の場合、何もしない
                    if (gcMultiRow.Rows[e.RowIndex].IsNewRow == true) return;

                    // 削除確認
                    if (MessageBox.Show("明細行(" + (e.RowIndex + 1) + ")を削除しますか？", "明細削除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        gcMultiRow.Rows.RemoveAt(e.RowIndex);
                        parentform.ChangedData(true);
                        NumberDetails("行番号");
                    }

                    break;
                case "買掛区分修正ボタン":

                    if (!parentform.IsDecided)
                    {
                        //コンボボックスのダブルクリックイベントではこうなっていた
                        //ComboBoxEditingControl combo = sender as ComboBoxEditingControl;
                        //combo.DroppedDown = true;
                    }
                    else
                    {
                        if (parentform.IsApproved)
                        {
                            if (parentform.buttonCnt == 0)
                            {
                                MessageBox.Show("この発注データは承認されています。\n承認後の区分設定はできません。",
                                                parentform.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                parentform.buttonCnt = 1;
                            }
                            else
                            {
                                //元に戻す
                                parentform.buttonCnt = 0;
                            }
                        }
                        else
                        {
                            F_発注_買掛区分設定 form = new F_発注_買掛区分設定();
                            //form.ShowDialog();
                            if (parentform.buttonCnt == 0)
                            {

                                form.ShowDialog();

                                return;
                            }
                            else
                            {
                                //元に戻す
                                parentform.buttonCnt = 0;
                            }


                            //{
                            //区分の各種値はF_発注_買掛区分設定でセットする

                            //string selectedCode = form.SelectedCode;

                            //gcMultiRow1.CurrentRow.Cells["買掛区分"].Value = selectedCode;

                            //gcMultiRow1.CurrentCellPosition =
                            //    new CellPosition(gcMultiRow1.CurrentRow.Index, gcMultiRow1.CurrentRow.Cells["品名"].CellIndex);
                            //}
                        }
                    }
                    break;
            }
        }

        bool spaceFlg = false;
        private void gcMultiRow1_KeyPress(object sender, KeyPressEventArgs e)
        {
            F_カレンダー fm = new F_カレンダー();
            //spaceキー
            if (e.KeyChar == ' ')
            {
                if (gcMultiRow1.CurrentCell.RowIndex == null || gcMultiRow1.CurrentCell.CellIndex == null) return;

                switch (gcMultiRow1.CurrentCell.Name)
                {
                    case "買掛区分":
                        ComboBoxEditingControl combo = sender as ComboBoxEditingControl;
                        combo.DroppedDown = true;
                        e.Handled = true;
                        break;

                    case "部品コード":
                        e.Handled = true; //スペースの本来の挙動（空白入力）を制御する

                        if (form.ShowDialog() == DialogResult.OK && gcMultiRow1.ReadOnly == false)
                        {
                            string selectedCode = form.SelectedCode;

                            if (IsAbolished(selectedCode.PadLeft(8, '0')))
                            {
                                if (MessageBox.Show("指定された部品は廃止されています。\nよろしいですか？", "確認", MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.No)
                                {
                                    return;
                                }
                            }


                            gcMultiRow1.EditingControl.Text = selectedCode; // <== 対応策
                            gcMultiRow1.CurrentCell.Value = selectedCode;
                            UpdatedControl(gcMultiRow1.CurrentCell);

                            gcMultiRow1.CurrentCellPosition =
                                new CellPosition(gcMultiRow1.CurrentRow.Index, gcMultiRow1.CurrentRow.Cells["発注納期"].CellIndex);

                        }
                        break;

                    case "発注納期":
                        spaceFlg = true;
                        e.Handled = true; //スペースの本来の挙動（空白入力）を制御する

                        if (!string.IsNullOrEmpty(gcMultiRow1.CurrentRow.Cells["発注納期"].Value.ToString()))
                        {
                            fm.args = gcMultiRow1.CurrentRow.Cells["発注納期"].Value.ToString();
                        }

                        if (fm.ShowDialog() == DialogResult.OK && gcMultiRow1.ReadOnly == false)
                        {
                            // 日付選択フォームから選択した日付を取得
                            string selectedDate = fm.SelectedDate;
                            gcMultiRow1.CurrentCell.Value = selectedDate;
                            gcMultiRow1.CurrentCellPosition =
                                new CellPosition(gcMultiRow1.CurrentRow.Index, gcMultiRow1.CurrentRow.Cells["必要数量"].CellIndex);
                        }
                        break;
                    case "回答納期":
                        e.Handled = true; //スペースの本来の挙動（空白入力）を制御する

                        if (fm.ShowDialog() == DialogResult.OK && gcMultiRow1.ReadOnly == false)
                        {
                            // 日付選択フォームから選択した日付を取得
                            string selectedDate = fm.SelectedDate;
                            gcMultiRow1.CurrentCell.Value = selectedDate;
                            gcMultiRow1.CurrentCellPosition =
                                new CellPosition(gcMultiRow1.CurrentRow.Index, gcMultiRow1.CurrentRow.Cells["買掛区分"].CellIndex);
                        }
                        break;
                }
            }
        }

        private void comboBox_CellDoubleClick(object sender, EventArgs e)
        {
            F_発注? parentform = Application.OpenForms.OfType<F_発注>().FirstOrDefault();
            switch (gcMultiRow1.CurrentCell.Name)
            {
                case "買掛区分":
                    if (!parentform.IsDecided)
                    {
                        ComboBoxEditingControl combo = sender as ComboBoxEditingControl;
                        combo.DroppedDown = true;
                    }
                    else
                    {
                        if (parentform.IsApproved)
                        {
                            MessageBox.Show("この発注データは承認されています。\n承認後の区分設定はできません。",
                                            parentform.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        else
                        {
                            F_発注_買掛区分設定 form = new F_発注_買掛区分設定();
                            //form.ShowDialog();
                            //if (parentform.buttonCnt == 0)
                            //{
                            //    form.ShowDialog();

                            //    return;
                            //}
                            //else
                            //{
                            //    //元に戻す
                            //    parentform.buttonCnt = 0;
                            //}
                        }
                    }
                    break;
            }
        }

        bool dblflg = false;
        private void gcMultiRow1_CellDoubleClick(object sender, EventArgs e)
        {

            F_カレンダー fm = new F_カレンダー();
            F_発注? parentform = Application.OpenForms.OfType<F_発注>().FirstOrDefault();
            F_発注_買掛区分設定 kaiform = new F_発注_買掛区分設定();
            try
            {
                switch (gcMultiRow1.CurrentCell.Name)
                {
                    case "部品コード":


                        if (form.ShowDialog() == DialogResult.OK && gcMultiRow1.ReadOnly == false)
                        {
                            string selectedCode = form.SelectedCode;

                            if (IsAbolished(selectedCode.PadLeft(8, '0')))
                            {
                                if (MessageBox.Show("指定された部品は廃止されています。\nよろしいですか？", "確認", MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.No)
                                {

                                    return;
                                }
                            }
                            gcMultiRow1.EditingControl.Text = selectedCode; // <== 対応策
                            gcMultiRow1.CurrentCell.Value = selectedCode;
                            UpdatedControl(gcMultiRow1.CurrentCell);

                            gcMultiRow1.CurrentCellPosition =
                                new CellPosition(gcMultiRow1.CurrentRow.Index, gcMultiRow1.CurrentRow.Cells["品名"].CellIndex);
                        }

                        break;
                    case "買掛区分枠":
                        if (!parentform.IsDecided)
                        {
                            break;
                        }
                        else
                        {
                            if (parentform.IsApproved)
                            {
                                MessageBox.Show("この発注データは承認されています。\n承認後の区分設定はできません。",
                                                parentform.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                            {

                                //form.ShowDialog();
                                if (parentform.buttonCnt == 0)
                                {
                                    kaiform.ShowDialog();

                                    return;
                                }
                                else
                                {
                                    //元に戻す
                                    parentform.buttonCnt = 0;
                                }
                            }
                        }
                        break;
                    case "買掛区分": //コンボボックスはダブルクリックイベントがきかないため移動
                        if (!parentform.IsDecided)
                        {
                            ComboBoxEditingControl combo = sender as ComboBoxEditingControl;
                            combo.DroppedDown = true;
                        }
                        else
                        {
                            if (parentform.IsApproved)
                            {
                                MessageBox.Show("この発注データは承認されています。\n承認後の区分設定はできません。",
                                                parentform.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                            {
                                //form.ShowDialog();
                                if (parentform.buttonCnt == 0)
                                {
                                    kaiform.ShowDialog();

                                    return;
                                }
                                else
                                {
                                    //元に戻す
                                    parentform.buttonCnt = 0;
                                }
                            }
                        }
                        break;
                    case "発注納期":

                        if (!string.IsNullOrEmpty(gcMultiRow1.CurrentRow.Cells["発注納期"].Value.ToString()))
                        {
                            fm.args = gcMultiRow1.CurrentRow.Cells["発注納期"].Value.ToString();
                        }

                        if (fm.ShowDialog() == DialogResult.OK && gcMultiRow1.ReadOnly == false)
                        {
                            dblflg = true;
                            // 日付選択フォームから選択した日付を取得
                            string selectedDate = fm.SelectedDate;
                            gcMultiRow1.CurrentCell.Value = selectedDate;
                            gcMultiRow1.EditingControl.Text = selectedDate; // <== 対応策
                            gcMultiRow1.CurrentCellPosition =
                                    new CellPosition(gcMultiRow1.CurrentRow.Index, gcMultiRow1.CurrentRow.Cells["必要数量"].CellIndex);

                        }
                        break;
                    case "回答納期":
                        if (parentform.IsApproved)
                        {
                            if (fm.ShowDialog() == DialogResult.OK && gcMultiRow1.ReadOnly == false)
                            {
                                dblflg = true;
                                // 日付選択フォームから選択した日付を取得
                                string selectedDate = fm.SelectedDate;
                                gcMultiRow1.CurrentCell.Value = selectedDate;
                                gcMultiRow1.EditingControl.Text = selectedDate; // <== 対応策
                                gcMultiRow1.CurrentCellPosition =
                                        new CellPosition(gcMultiRow1.CurrentRow.Index, gcMultiRow1.CurrentRow.Cells["買掛区分"].CellIndex);

                            }
                        }
                        else
                        {
                            MessageBox.Show("この発注データは承認されていません。\n承認前の回答納期の設定はできません。",
                                                parentform.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        break;
                    default:

                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_CellDoubleClick - " + ex.Message);
                return;
            }
        }

        private void gcMultiRow1_CellValidated(object sender, CellEventArgs e)
        {
            if (!validateFlg) return;

            //各項目のChangeイベント
            if (e.RowIndex >= 0 && e.CellIndex >= 0)
            {
                switch (e.CellName)
                {
                    case "メーカー名":

                        //  FunctionClass.LimitText(gcMultiRow1.CurrentCell, 50);
                        break;
                    case "回答納期":
                        break;
                    case "型番":
                        UpdatedControl(gcMultiRow1.CurrentCell);
                        break;
                    case "買掛区分":
                        UpdatedControl(gcMultiRow1.CurrentCell);
                        break;
                    case "発注数量":
                        UpdatedControl(gcMultiRow1.CurrentCell);
                        break;
                    case "発注単価":
                        break;
                    case "発注納期":
                        object value = gcMultiRow1.CurrentRow.Cells["発注納期"].Value;

                        if (value != DBNull.Value)
                        {
                            // DBNullでない場合、DateTimeに変換して処理
                            gcMultiRow1.CurrentRow.Cells["発注納期"].Value = FunctionClass.DateConvert((DateTime)value).ToString();
                        }
                        else
                        {
                            // DBNullの場合　 空文字列を設定
                            gcMultiRow1.CurrentRow.Cells["発注納期"].Value = DBNull.Value;
                        }
                        UpdatedControl(gcMultiRow1.CurrentCell);

                        break;
                    case "必要数量":
                        UpdatedControl(gcMultiRow1.CurrentCell);
                        break;
                    case "品名":
                        UpdatedControl(gcMultiRow1.CurrentCell);
                        break;
                    case "部品コード":
                        var currentValue = gcMultiRow1.CurrentCell.Value?.ToString();

                        if (int.TryParse(currentValue, out int numericValue))
                        {
                            // 変換成功: 数値を8桁のゼロパディング形式でフォーマットし、セルに設定
                            gcMultiRow1.CurrentCell.Value = numericValue.ToString("D8");
                            UpdatedControl(gcMultiRow1.CurrentCell);
                        }

                        break;
                }
                F_発注? Parentform = Application.OpenForms.OfType<F_発注>().FirstOrDefault();
                Parentform.ChangedData(true);

            }
        }

        private void gcMultiRow1_RowValidating(object sender, CellCancelEventArgs e)
        {
            // 行が変更されたかどうかをチェック
            if (rowChanged.TryGetValue(e.RowIndex, out bool changed) && changed)
            {
                var targetRow = e.RowIndex;
                if (gcMultiRow1.Rows[e.RowIndex].IsNewRow) return;

                if (string.IsNullOrEmpty(gcMultiRow1.CurrentRow.Cells["部品コード"].Value?.ToString()) &&
                    !string.IsNullOrEmpty(gcMultiRow1.CurrentRow.Cells["品名"].Value?.ToString()) &&
                    !string.IsNullOrEmpty(gcMultiRow1.CurrentRow.Cells["型番"].Value?.ToString()))
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

                string errorCellName = IsErrorData(targetRow, false);
                if (errorCellName != null)
                {
                    e.Cancel = true;
                    gcMultiRow1.CurrentCell = gcMultiRow1.Rows[targetRow].Cells[errorCellName]; // エラーがあったセルをアクティブに
                    gcMultiRow1.BeginEdit(true); // 編集モードにする

                }
                // 処理が完了したら、行の変更フラグをリセット
                rowChanged[e.RowIndex] = false;
            }
        }

        private void gcMultiRow1_CellBeginEdit(object sender, CellBeginEditEventArgs e)
        {
            if (!rowChanged.ContainsKey(e.RowIndex))
            {
                rowChanged[e.RowIndex] = false;
            }
        }

        // 値が変更された時
        private void gcMultiRow1_CellValueChanged(object sender, CellEventArgs e)
        {
            rowChanged[e.RowIndex] = true;
        }

        private void gcMultiRow1_CellMouseDoubleClick(object sender, CellMouseEventArgs e)
        {
            F_カレンダー fm = new F_カレンダー();
            F_発注? parentform = Application.OpenForms.OfType<F_発注>().FirstOrDefault();
            F_発注_買掛区分設定 kaiform = new F_発注_買掛区分設定();
            try
            {
                switch (gcMultiRow1.CurrentCell.Name)
                {
                    case "部品コード":


                        if (form.ShowDialog() == DialogResult.OK && gcMultiRow1.ReadOnly == false)
                        {
                            string selectedCode = form.SelectedCode;

                            if (IsAbolished(selectedCode.PadLeft(8, '0')))
                            {
                                if (MessageBox.Show("指定された部品は廃止されています。\nよろしいですか？", "確認", MessageBoxButtons.YesNo,
                                        MessageBoxIcon.Question) == DialogResult.No)
                                {

                                    return;
                                }
                            }
                            gcMultiRow1.EditingControl.Text = selectedCode; // <== 対応策
                            gcMultiRow1.CurrentCell.Value = selectedCode;
                            UpdatedControl(gcMultiRow1.CurrentCell);

                            gcMultiRow1.CurrentCellPosition =
                                new CellPosition(gcMultiRow1.CurrentRow.Index, gcMultiRow1.CurrentRow.Cells["品名"].CellIndex);
                        }

                        break;
                    case "買掛区分枠":
                        if (!parentform.IsDecided)
                        {
                            break;
                        }
                        else
                        {
                            if (parentform.IsApproved)
                            {
                                MessageBox.Show("この発注データは承認されています。\n承認後の区分設定はできません。",
                                                parentform.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                            {

                                //form.ShowDialog();
                                if (parentform.buttonCnt == 0)
                                {
                                    kaiform.ShowDialog();

                                    return;
                                }
                                else
                                {
                                    //元に戻す
                                    parentform.buttonCnt = 0;
                                }
                            }
                        }
                        break;
                    
                    case "発注納期":

                        if (!string.IsNullOrEmpty(gcMultiRow1.CurrentRow.Cells["発注納期"].Value.ToString()))
                        {
                            fm.args = gcMultiRow1.CurrentRow.Cells["発注納期"].Value.ToString();
                        }

                        if (fm.ShowDialog() == DialogResult.OK && gcMultiRow1.ReadOnly == false)
                        {
                            dblflg = true;
                            // 日付選択フォームから選択した日付を取得
                            string selectedDate = fm.SelectedDate;
                            gcMultiRow1.CurrentCell.Value = selectedDate;
                            gcMultiRow1.EditingControl.Text = selectedDate; // <== 対応策
                            gcMultiRow1.CurrentCellPosition =
                                    new CellPosition(gcMultiRow1.CurrentRow.Index, gcMultiRow1.CurrentRow.Cells["必要数量"].CellIndex);

                        }
                        break;
                    case "回答納期":
                        if (parentform.IsApproved)
                        {
                            if (fm.ShowDialog() == DialogResult.OK && gcMultiRow1.ReadOnly == false)
                            {
                                dblflg = true;
                                // 日付選択フォームから選択した日付を取得
                                string selectedDate = fm.SelectedDate;
                                gcMultiRow1.CurrentCell.Value = selectedDate;
                                gcMultiRow1.EditingControl.Text = selectedDate; // <== 対応策
                                gcMultiRow1.CurrentCellPosition =
                                        new CellPosition(gcMultiRow1.CurrentRow.Index, gcMultiRow1.CurrentRow.Cells["買掛区分"].CellIndex);

                            }
                        }
                        else
                        {
                            MessageBox.Show("この発注データは承認されていません。\n承認前の回答納期の設定はできません。",
                                                parentform.Text, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        }
                        break;
                    default:

                        break;
                }
            }
            catch (Exception ex)
            {
                Debug.Print(this.Name + "_CellDoubleClick - " + ex.Message);
                return;
            }
        }
    }
}
