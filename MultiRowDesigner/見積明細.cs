using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GrapeCity.Win.BarCode.ValueType;
using GrapeCity.Win.MultiRow;
using GrapeCity.Win.MultiRow.InputMan;
using u_net;
using u_net.Public;

namespace MultiRowDesigner
{
    public partial class 見積明細 : UserControl
    {
        public bool IsOrderByOn = false; // 現在のデータが並べ替えられているかどうかを取得する

        public GcMultiRow Detail
        {
            get
            {
                return gcMultiRow1;
            }
        }

        public 見積明細()
        {
            InitializeComponent();
        }

        private void 受注明細_Load(object sender, EventArgs e)
        {
            gcMultiRow1.ShortcutKeyManager.Unregister(Keys.Enter);
            gcMultiRow1.ShortcutKeyManager.Register(SelectionActions.MoveToNextCell, Keys.Enter);

        }

        private void gcMultiRow1_EditingControlShowing(object sender, EditingControlShowingEventArgs e)
        {
            TextBoxEditingControl textBox = e.Control as TextBoxEditingControl;
            ComboBoxEditingControl comboBox = e.Control as ComboBoxEditingControl;
            if (textBox != null)
            {
                textBox.TextChanged -= gcMultiRow1_TextChanged;
                textBox.TextChanged += gcMultiRow1_TextChanged;
            }
        }

        private void gcMultiRow1_TextChanged(object sender, EventArgs e)
        {
            Control control = sender as Control;
            
            switch (gcMultiRow1.CurrentCell.Name)
            {
                case "品名":
                    FunctionClass.LimitText(control, 44);
                    break;
                case "型番":
                    FunctionClass.LimitText(control, 44);
                    break;
                case "明細備考":
                    FunctionClass.LimitText(control, 44);
                    break;

            }

            //フォーカスインしただけで変更状態になってしまう為コメントアウト、Validatingイベント内に記載
            //ParentForm.ChangedData(true);
        }

        private void gcMultiRow1_CellEnter(object sender, CellEventArgs e)
        {
            F_見積 ParentForm = (F_見積)Application.OpenForms["F_見積"];

            gcMultiRow1.BeginEdit(true);

            if (ParentForm != null)
            {
                switch (e.CellName)
                {
                    case "明細削除ボタン":
                        ParentForm.toolStripStatusLabel2.Text = "■明細行を削除します。";
                        break;
                    case "行番号":
                        ParentForm.toolStripStatusLabel2.Text = "■入力できません。";
                        break;
                    case "品名":
                        ParentForm.toolStripStatusLabel2.Text = "■最大入力文字数は22文字（全角文字）です。";
                        break;
                    case "型番":
                        ParentForm.toolStripStatusLabel2.Text = "■最大入力文字数は44文字（半角文字）です。";
                        break;
                    case "数量":
                        ParentForm.toolStripStatusLabel2.Text = "■見積品の数量を入力します。";
                        break;
                    case "単位":
                        ParentForm.toolStripStatusLabel2.Text = "■数量の単位を入力します。";
                        break;
                    case "明細備考":
                        ParentForm.toolStripStatusLabel2.Text = "■最大入力文字数は22文字（全角文字）です。";
                        break;
                    default:
                        ParentForm.toolStripStatusLabel2.Text = "各種項目の説明";
                        break;
                }
            }
        }

        private void gcMultiRow1_CellContentButtonClick(object sender, CellEventArgs e)
        {
            GcMultiRow gcMultiRow = sender as GcMultiRow;

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
                    break;
                case "明細削除ボタン":
                    // 新規行の場合、何もしない
                    if (gcMultiRow.Rows[e.RowIndex].IsNewRow == true) return;

                    F_見積 ParentForm = (F_見積)Application.OpenForms["F_見積"];
                    if (ParentForm.IsDecided) return;

                    // 削除確認
                    if (MessageBox.Show("明細行(" + (e.RowIndex + 1) + ")を削除しますか？", "承認コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {

                        gcMultiRow.Rows.RemoveAt(e.RowIndex);

                        ParentForm.ChangedData(true);
                    }

                    break;
            }

        }

        private void gcMultiRow1_DefaultValuesNeeded(object sender, RowEventArgs e)
        {
            e.Row.Cells["数量"].Value = 0;
            e.Row.Cells["単位"].Value = "台";
            e.Row.Cells["標準単価"].Value = 0;
            e.Row.Cells["単価"].Value = 0;

            // 新規行に初期ソート順を設定
            decimal maxValues = this.gcMultiRow1.Rows.Max<Row, Decimal>(row => (Decimal)row.Cells["初期ソート順"].Value) + 1m;
            e.Row.Cells["初期ソート順"].Value = maxValues;
        }

        private void gcMultiRow1_CellValidating(object sender, CellValidatingEventArgs e)
        {
            GcMultiRow grid = (GcMultiRow)sender;

            switch (e.CellName)
            {
                case "品名":
                case "型番":
                case "数量":
                case "単価":
                case "単位":
                case "標準単価":
                    
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

            // 値が変更されていれば変更済みとして処理
            if (grid.EditingControl != null && grid.EditingControl.Text != gcMultiRow1.CurrentCell.DisplayText)
            {
                F_見積 ParentForm = (F_見積)Application.OpenForms["F_見積"];
                ParentForm.ChangedData(true);
            }
        }

        /// <summary>
        /// 明細部の見積コードと見積版数を更新する
        /// </summary>
        /// <param name="code">見積コード</param>
        /// <param name="edition">見積版数</param>
        public void UpdateCodeAndEdition(string code, int edition)
        {
            for (int i = 0; i < Detail.RowCount - 1; i++)
            {
                Detail.Rows[i].Cells["見積コード"].Value = code;
                Detail.Rows[i].Cells["見積版数"].Value = edition;
            }
        }

        public void CancelOrderBy()
        {
            try
            {
                // 明細並び順を元に戻す
                this.gcMultiRow1.Sort("初期ソート順");

                // 明細ヘッダー部のソートマークを元に戻す
                foreach (ColumnHeaderSection section in this.gcMultiRow1.ColumnHeaders)
                {
                    foreach (Cell cell in section.Cells)
                    {
                        if (cell is ColumnHeaderCell)
                        {
                            ColumnHeaderCell columnHeaderCell = cell as ColumnHeaderCell;
                            columnHeaderCell.SortGlyphDirection = System.Windows.Forms.SortOrder.None;
                        }
                    }
                }

                this.IsOrderByOn = false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CancelOrderBy - {ex.GetType().Name}: {ex.Message}");
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
                    case "品名":
                        if (string.IsNullOrEmpty(varValue))
                        {
                            MessageBox.Show(cellName + "を入力してください。", "見積", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;
                    case "型番":
                        // 必須としない
                        break;
                    case "数量":
                    case "単価":
                        if (string.IsNullOrEmpty(varValue))
                        {
                            MessageBox.Show(cellName + "を入力してください。", "見積", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        if (OriginalClass.IsNumeric(varValue) == false)
                        {
                            MessageBox.Show("数字を入力してください。", "見積", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                            return true;
                        }
                        break;
                    case "単位":
                        // 必須としない
                        break;
                    case "標準単価":
                        // 必須としない
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

        private void gcMultiRow1_Sorted(object sender, EventArgs e)
        {
            this.IsOrderByOn = true;
        }

    }
}
