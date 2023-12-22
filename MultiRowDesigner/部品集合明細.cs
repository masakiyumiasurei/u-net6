using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using Microsoft.EntityFrameworkCore.Diagnostics;
using u_net;
using u_net.Public;
//using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace MultiRowDesigner
{
    public partial class 部品集合明細 : UserControl
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
        public string PartsCode
        {
            get
            {
                return string.IsNullOrEmpty(gcMultiRow1.CurrentRow.Cells["部品コード"].Value?.ToString()) ? ""
                    : gcMultiRow1.CurrentRow.Cells["部品コード"].Value?.ToString();
            }
        }
        public 部品集合明細()
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

        private void 部品集合明細_Load(object sender, EventArgs e)
        {
            gcMultiRow1.ShortcutKeyManager.Unregister(Keys.Enter);
            gcMultiRow1.ShortcutKeyManager.Register(SelectionActions.MoveToNextCell, Keys.Enter);
        }

        private void gcMultiRow1_DefaultValuesNeeded(object sender, RowEventArgs e)
        {
            F_部品集合 ParentForm = Application.OpenForms.OfType<F_部品集合>().FirstOrDefault();

            e.Row.Cells["発注コード"].Value = ParentForm.CurrentCode;
            e.Row.Cells["発注版数"].Value = ParentForm.CurrentEdition;
        }

        private void gcMultiRow1_EditingControlShowing(object sender, EditingControlShowingEventArgs e)
        {

            TextBoxEditingControl textBox = e.Control as TextBoxEditingControl;

            if (textBox != null)
            {
                textBox.PreviewKeyDown -= gcMultiRow1_PreviewKeyDown;
                textBox.PreviewKeyDown += gcMultiRow1_PreviewKeyDown;

                textBox.KeyPress -= new KeyPressEventHandler(gcMultiRow1_KeyPress);
                textBox.KeyPress += new KeyPressEventHandler(gcMultiRow1_KeyPress);

                textBox.DoubleClick -= gcMultiRow1_CellDoubleClick;
                textBox.DoubleClick += gcMultiRow1_CellDoubleClick;

            }
        }

        private void gcMultiRow1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
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
            //spaceキー
            if (e.KeyChar == ' ')
            {
                if (gcMultiRow1.CurrentCell.RowIndex == null || gcMultiRow1.CurrentCell.CellIndex == null) return;

                switch (gcMultiRow1.CurrentCell.Name)
                {

                    case "部品コード":
                        e.Handled = true;　//スペースの本来の挙動（空白入力）を制御する
                        F_部品選択 form = new F_部品選択();
                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            string selectedCode = form.SelectedCode;

                            gcMultiRow1.CurrentCell.Value = selectedCode;

                            //次のセルに変更しないと値が反映しないため
                            gcMultiRow1.CurrentCellPosition =
                           new CellPosition(gcMultiRow1.CurrentRow.Index, gcMultiRow1.CurrentRow.Cells["品名"].CellIndex);
                        }
                        break;

                }
            }
        }

        private void gcMultiRow1_CellDoubleClick(object sender, EventArgs e)
        {

            switch (gcMultiRow1.CurrentCell.Name)
            {
                case "部品コード":

                    F_部品選択 form = new F_部品選択();
                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        string selectedCode = form.SelectedCode;

                        gcMultiRow1.CurrentCell.Value = selectedCode;

                        UpdatedControl(gcMultiRow1.CurrentCell);

                        gcMultiRow1.CurrentCellPosition =
                            new CellPosition(gcMultiRow1.CurrentRow.Index, gcMultiRow1.CurrentRow.Cells["品名"].CellIndex);
                    }

                    break;
            }
        }
        private void gcMultiRow1_CellContentClick(object sender, CellEventArgs e)
        {

            switch (gcMultiRow1.CurrentCell)
            {
                //ボタンClick時の処理
                case ButtonCell:
                    switch (e.CellName)
                    {
                        case "部品参照ボタン":
                            F_部品 fm = new F_部品();
                            fm.args = PartsCode;
                            fm.ShowDialog();
                            break;
                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }
        }

        private void gcMultiRow1_CellEnter(object sender, CellEventArgs e)
        {
            u_net.F_部品集合 objForm = (u_net.F_部品集合)Application.OpenForms["F_部品集合"];

            if (objForm != null)
            {
                switch (gcMultiRow1.CurrentCell)
                {
                    //テキストボックスEnter時の処理
                    case TextBoxCell:
                        switch (e.CellName)
                        {
                            case "部品コード":
                                objForm.toolStripStatusLabel1.Text = "■部品コードを入力あるいは選択します。　■入力欄でダブルクリックするか、[space]キーを押すと部品選択画面が表示されます。";
                                break;
                            default:
                                objForm.toolStripStatusLabel1.Text = "各種項目の説明";
                                break;
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        private bool IsError(Control controlObject, string cellName)
        {
            try
            {
                // エラーチェック
                bool isError = false;


                string varValue = controlObject.Text;
                switch (cellName)
                {
                    case "受注区分コード":
                        if (string.IsNullOrEmpty(varValue))
                        {
                            MessageBox.Show("受注区分を選択してください。", cellName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;
                    case "売上区分コード":
                        if (string.IsNullOrEmpty(varValue))
                        {
                            MessageBox.Show("売上区分を選択してください。", "入力", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;

                }

                return false; // エラーなしの場合

            }
            catch (Exception ex)
            {
                Console.WriteLine($"{Name}_IsError - {ex.Message}");
                return true;
            }
        }

        private bool PartsIsExist(string partCode)
        {
            //重複をチェックする
            try
            {
                foreach (var row in gcMultiRow1.Rows)
                {
                    var partCodeCellValue = row.Cells["部品コード"].Value?.ToString();

                    if (partCodeCellValue != null && partCodeCellValue.Equals(partCode))
                    {
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PartsIsExist error: {ex.Message}");
                return false;
            }
        }

        private bool RegedParts(string partsCode)
        {
            try
            {
                Connect();
                // 登録済みの部品集合グループに対して指定部品が存在するかどうかを返す
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = cn;
                    cmd.CommandType = CommandType.Text;

                    cmd.CommandText = "SELECT * FROM V部品集合_部品検出 WHERE 部品コード=@PartsCode";
                    cmd.Parameters.AddWithValue("@PartsCode", partsCode);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            MessageBox.Show("指定部品は以下の部品集合に登録済みです。" + Environment.NewLine +
                                "部品集合コード： " + reader["部品集合コード"] +
                                " （第 " + reader["部品集合版数"] + " 版）" + Environment.NewLine +
                                "集合名： " + reader["集合名"], "警告", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                            return true;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("RegedParts error: " + ex.Message);
                return true;
            }
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

        private void UpdatedControl(Cell cell)
        {
            try
            {
                switch (cell.Name)
                {
                    case "部品コード":

                        string strKey;
                        string strSQL;

                        Connect();
                        strKey = $"部品コード='{cell.Value}'";
                        strSQL = $"SELECT * FROM V部品集合_部品表示 WHERE {strKey}";

                        using (SqlCommand command = new SqlCommand(strSQL, cn))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    if (Convert.ToInt32(reader["廃止"])==0)
                                    {
                                        gcMultiRow1.CurrentRow.Cells["廃止表示"].Value = DBNull.Value;
                                    }
                                    else
                                    {
                                        gcMultiRow1.CurrentRow.Cells["廃止表示"].Value = "■";
                                    }    
                                    gcMultiRow1.CurrentRow.Cells["廃止"].Value = reader["廃止"];
                                    gcMultiRow1.CurrentRow.Cells["分類記号"].Value = reader["分類記号"];
                                    gcMultiRow1.CurrentRow.Cells["品名"].Value = reader["品名"];
                                    gcMultiRow1.CurrentRow.Cells["型番"].Value = reader["型番"];
                                    gcMultiRow1.CurrentRow.Cells["メーカー名"].Value = reader["メーカー名"];
                                }
                            }
                        }

                        break;
                }

            }
            catch (Exception ex)
            {
                // エラーログ出力（この例ではコンソールに出力）
                Console.WriteLine($"{this.GetType().Name}_UpdatedControl - {ex.GetType().Name} : {ex.Message}");
            }
        }

        private void gcMultiRow1_CellContentButtonClick(object sender, CellEventArgs e)
        {

            switch (e.CellName)
            {
                case "メーカー名ボタン":
                    int col = gcMultiRow1.CurrentRow.Cells["メーカー名"].CellIndex;
                    int row = 0;
                    if (gcMultiRow1.CurrentRow.Index > 0)
                    {
                        row = gcMultiRow1.CurrentRow.Index;
                    }
                    gcMultiRow1.CurrentCellPosition = new CellPosition(row, col);
                    break;

                case "型番ボタン":
                    int col2 = gcMultiRow1.CurrentRow.Cells["型番"].CellIndex;
                    int row2 = 0;
                    if (gcMultiRow1.CurrentRow.Index > 0)
                    {
                        row2 = gcMultiRow1.CurrentRow.Index;
                    }
                    gcMultiRow1.CurrentCellPosition = new CellPosition(row2, col2);
                    break;


                case "品名ボタン":
                    int col5 = gcMultiRow1.CurrentRow.Cells["品名"].CellIndex;
                    int row5 = 0;
                    if (gcMultiRow1.CurrentRow.Index > 0)
                    {
                        row5 = gcMultiRow1.CurrentRow.Index;
                    }
                    gcMultiRow1.CurrentCellPosition = new CellPosition(row5, col5);
                    break;

                case "部品コードボタン":
                    int col6 = gcMultiRow1.CurrentRow.Cells["部品コード"].CellIndex;
                    int row6 = 0;
                    if (gcMultiRow1.CurrentRow.Index > 0)
                    {
                        row6 = gcMultiRow1.CurrentRow.Index;
                    }
                    gcMultiRow1.CurrentCellPosition = new CellPosition(row6, col6);
                    break;

            }
        }

        private void gcMultiRow1_CellValidating(object sender, CellValidatingEventArgs e)
        {
            switch (e.CellName)
            {
                case "受注区分コード":
                case "売上区分コード":
                case "ラインコード":
                case "型番":
                case "品名":
                case "単位コード":
                case "数量":
                case "単価":
                case "SettingSheet":
                case "InspectionReport":
                case "Specification":
                case "ParameterSheet":
                case "備考":
                case "シリアル番号付加":
                case "CustomerSerialNumberFrom":
                case "CustomerSerialNumberTo":
                    GcMultiRow grid = (GcMultiRow)sender;
                    // セルが編集中の場合
                    if (grid.IsCurrentCellInEditMode)
                    {
                        // 値が変更されていなければエラーチェックを行わない
                        if (grid.EditingControl.Text == gcMultiRow1.CurrentCell.DisplayText) return;

                        // 編集用コントロールに不正な文字列が設定されている場合
                        if (IsError(grid.EditingControl, e.CellName) == true)
                        {
                            // 元の値に戻す
                            grid.EditingControl.Text = gcMultiRow1.CurrentCell.DisplayText;
                            e.Cancel = true;
                        }
                    }
                    break;

            }
        }





    }
}
