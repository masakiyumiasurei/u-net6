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
        public bool validatedflg = false;

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
                if (gcMultiRow1.RowCount > 0)
                {
                    var CurrentRow = gcMultiRow1.CurrentRow;

                    if (CurrentRow.Cells["部品コード"] != null && CurrentRow.Cells["部品コード"].Value != null)
                    {
                        return CurrentRow.Cells["部品コード"].Value.ToString();
                    }
                }

                return "";
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
            //// テンプレートの作成
            //var template1 = Template.CreateGridTemplate(2);            

            //// MultiRowの設定
            //gcMultiRow1.Template = template1;
            //gcMultiRow1.EditMode = EditMode.EditOnEnter; // 常時入力モード


            gcMultiRow1.ShortcutKeyManager.Unregister(Keys.Enter);
            gcMultiRow1.ShortcutKeyManager.Register(SelectionActions.MoveToNextCell, Keys.Enter);
        }

        private void gcMultiRow1_DefaultValuesNeeded(object sender, RowEventArgs e)
        {
            F_部品集合 ParentForm = Application.OpenForms.OfType<F_部品集合>().FirstOrDefault();

            e.Row.Cells["部品集合コード"].Value = ParentForm.CurrentCode;
            e.Row.Cells["部品集合版数"].Value = ParentForm.CurrentEdition;
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

                //textBox.DoubleClick -= gcMultiRow1_CellDoubleClick;
                //textBox.DoubleClick += gcMultiRow1_CellDoubleClick;

            }
        }

        private void gcMultiRow1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Return)
            {
                gcMultiRow1.EndEdit();

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

        private F_部品選択 form = new F_部品選択();

        private void gcMultiRow1_KeyPress(object sender, KeyPressEventArgs e)
        {
            //spaceキー
            if (e.KeyChar == ' ')
            {
                if (gcMultiRow1.CurrentCell.RowIndex == null || gcMultiRow1.CurrentCell.CellIndex == null) return;

                switch (gcMultiRow1.CurrentCell.Name)
                {

                    case "部品コード":
                        e.Handled = true; //スペースの本来の挙動（空白入力）を制御する

                        if (form.ShowDialog() == DialogResult.OK)
                        {
                            
                            string selectedCode = form.SelectedCode;

                            gcMultiRow1.EditingControl.Text = selectedCode; // <== 対応策

                            gcMultiRow1.CurrentCell.Value = selectedCode;

                            //品名にセル移動した時にvaledatedを実行しないようにするため
                            validatedflg = true;
                            UpdatedControl(gcMultiRow1.CurrentCell);
                            
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
                            F_部品集合 ParentForm = Application.OpenForms.OfType<F_部品集合>().FirstOrDefault();
                            F_部品 targetform = new F_部品();
                            targetform.args = PartsCode;
                            targetform.MdiParent = ParentForm.MdiParent;
                            targetform.FormClosed += (s, args) => { this.Enabled = true; };
                            this.Enabled = false;

                            targetform.Show();

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
                    case "部品コード":
                        if (string.IsNullOrEmpty(gcMultiRow1.CurrentCell.DisplayText))
                        {
                            MessageBox.Show("部品コードを選択してください。", "部品コード", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        if (PartsIsExist(varValue))
                        {
                            MessageBox.Show($"部品コード{varValue}は既に存在するため入力できません。", cellName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        if (RegedParts(varValue))
                        {
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
                gcMultiRow1.BeginInvoke(() =>
                {
                    for (int i = 0; i < gcMultiRow1.RowCount; i++)
                    {
                        if (gcMultiRow1.Rows[i].IsNewRow == true)
                        {
                            //新規行の場合は、処理をスキップ
                            continue;
                        }

                        gcMultiRow1.Rows[i].Cells[fieldName].Value = i + 1;
                    }

                });

                F_部品集合 ParentForm = Application.OpenForms.OfType<F_部品集合>().FirstOrDefault();
                ParentForm.ChangedData(true);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"NumberDetails - {ex.GetType().Name}: {ex.Message}");
            }
        }

        private void UpdatedControl(Cell cell)
        {
            F_部品集合? parentform = Application.OpenForms.OfType<F_部品集合>().FirstOrDefault();
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
                                    if (Convert.ToInt32(reader["廃止"]) == 0)
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
                                    // parentform.ChangedData(true);
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
            F_部品集合? parentform = Application.OpenForms.OfType<F_部品集合>().FirstOrDefault();
            switch (e.CellName)
            {
                case "明細削除ボタン":
                    // 新規行の場合、何もしない
                    if (gcMultiRow1.Rows[e.RowIndex].IsNewRow == true) return;


                    if (gcMultiRow1.ReadOnly == true)
                    {
                        MessageBox.Show("編集はできません。", "削除", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    // 削除確認
                    if (MessageBox.Show("明細行(" + (e.RowIndex + 1) + ")を削除しますか？", "明細削除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        gcMultiRow1.Rows.RemoveAt(e.RowIndex);
                        parentform.ChangedData(true);
                        NumberDetails("明細番号");
                    }

                    break;
                //case "行挿入ボタン":
                //    gcMultiRow1.Rows.Insert(e.RowIndex);
                //    break;

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
                case "部品コード":

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

        private void gcMultiRow1_CellValidated(object sender, CellEventArgs e)
        {
            if (validatedflg)
            {
                validatedflg = false;
                return;
            }
            switch (e.CellName)
            {
                case "部品コード":

                    UpdatedControl(gcMultiRow1.CurrentRow.Cells["部品コード"]);

                    break;
            }
        }

        private void gcMultiRow1_RowDragMoveCompleted(object sender, DragMoveCompletedEventArgs e)
        {
            F_部品集合 ParentForm = Application.OpenForms.OfType<F_部品集合>().FirstOrDefault();
            ParentForm.ChangedData(true);
            //行変更後の値を取得するためのラムダ式
            gcMultiRow1.BeginInvoke(() =>
            {
                NumberDetails("明細番号");

            });
        }

        private void gcMultiRow1_CellDoubleClick(object sender, CellEventArgs e)
        {
            switch (gcMultiRow1.CurrentCell.Name)
            {
                case "部品コード":


                    if (form.ShowDialog() == DialogResult.OK)
                    {
                        //gcMultiRow1.EndEdit(); //編集の終了
                        string selectedCode = form.SelectedCode;
                        int idx = gcMultiRow1.CurrentRow.Index;

                        gcMultiRow1.EditingControl.Text = selectedCode; // <== 対応策

                        gcMultiRow1.CurrentRow.Cells["部品コード"].Value = selectedCode;

                        //品名にセル移動した時にvaledatedを実行しないようにするため
                        validatedflg = true;
                        UpdatedControl(gcMultiRow1.CurrentCell);

                        F_部品集合 ParentForm = Application.OpenForms.OfType<F_部品集合>().FirstOrDefault();
                        ParentForm.ChangedData(true);

                        gcMultiRow1.CurrentCellPosition =
                               new CellPosition(idx, gcMultiRow1.CurrentRow.Cells["品名"].CellIndex);

                    }

                    break;
            }
        }
    }
}
