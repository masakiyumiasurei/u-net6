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
                if (gcMultiRow1.CurrentCell.Name == "ラインコード")
                {
                    comboBox.DrawMode = DrawMode.OwnerDrawFixed;
                    comboBox.DrawItem -= ラインコード_DrawItem;
                    comboBox.DrawItem += ラインコード_DrawItem;
                }
                else
                {
                    comboBox.DrawMode = DrawMode.Normal;
                }

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
                    case "受注区分コード":
                    case "売上区分コード":
                    case "ラインコード":
                    case "単位コード":
                    case "SettingSheet":
                    case "InspectionReport":
                    case "Specification":
                    case "ParameterSheet":
                        ComboBoxEditingControl combo = sender as ComboBoxEditingControl;
                        combo.DroppedDown = true;
                        e.Handled = true;
                        break;
                }
            }

        }

        private void ラインコード_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBoxEditingControl combo = sender as ComboBoxEditingControl;

            OriginalClass.SetComboBoxAppearance(combo, e, new int[] { 50, 150 }, new string[] { "Display", "Display2" });
        }

        private void gcMultiRow1_CellEnter(object sender, CellEventArgs e)
        {
            F_受注 objForm = (F_受注)Application.OpenForms["F_受注"];

            if (objForm != null)
            {
                switch (gcMultiRow1.CurrentCell)
                {
                    //テキストボックスEnter時の処理
                    case TextBoxCell:
                        switch (e.CellName)
                        {
                            case "受注区分":
                                objForm.toolStripStatusLabel2.Text = "■[space]キーで選択。";
                                break;
                            case "売上区分":
                                objForm.toolStripStatusLabel2.Text = "■[space]キーで選択。";
                                break;
                            case "処理区分":
                                objForm.toolStripStatusLabel2.Text = "■[space]キーで選択。";
                                break;
                            case "単位コード":
                                objForm.toolStripStatusLabel2.Text = "■[space]キーで選択。";
                                break;
                            case "商品コード":
                                objForm.toolStripStatusLabel2.Text = "■商品コードを入力あるいは商品を選択します。　■商品を選択するには入力欄をダブルクリックするか[space]キーを押すと表示される商品選択ダイアログから選択してください。";
                                break;
                            case "型番":
                                objForm.toolStripStatusLabel2.Text = "■半角４８文字まで入力できます。";
                                break;
                            case "CustomerSerialNumberFrom":
                                objForm.toolStripStatusLabel2.Text = "■顧客シリアル番号の開始番号を入力してください。";
                                break;
                            case "CustomerSerialNumberTo":
                                objForm.toolStripStatusLabel2.Text = "■顧客シリアル番号の終了番号を入力してください。";
                                break;
                            case "SettingSheet":
                                objForm.toolStripStatusLabel2.Text = "■[space]キーで選択。";
                                break;
                            case "InspectionReport":
                                objForm.toolStripStatusLabel2.Text = "■[space]キーで選択。";
                                break;
                            case "Specification":
                                objForm.toolStripStatusLabel2.Text = "■[space]キーで選択。";
                                break;
                            case "ParameterSheet":
                                objForm.toolStripStatusLabel2.Text = "■[space]キーで選択。";
                                break;
                            case "備考":
                                objForm.toolStripStatusLabel2.Text = "■設定明細内容を入力。■全角８２文字まで入力できます。";
                                break;
                            default:
                                objForm.toolStripStatusLabel2.Text = "各種項目の説明";
                                break;
                        }
                        break;

                    default:
                        break;
                }
            }
        }

        private void gcMultiRow1_CellContentButtonClick(object sender, CellEventArgs e)
        {
            GcMultiRow gcMultiRow = sender as GcMultiRow;

            switch (e.CellName)
            {
                case "明細削除ボタン":
                    // 新規行の場合、何もしない
                    if (gcMultiRow.Rows[e.RowIndex].IsNewRow == true) return;

                    // 削除確認
                    if (MessageBox.Show("明細行(" + (e.RowIndex + 1) + ")を削除しますか？", "承認コマンド", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        gcMultiRow.Rows.RemoveAt(e.RowIndex);
                    }

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

        /// <summary>
        /// 明細部の受注コードと受注版数を更新する
        /// </summary>
        /// <param name="code">受注コード</param>
        /// <param name="edition">受注版数</param>
        public void UpdateCodeAndEdition(string code, int edition)
        {
            for (int i = 0; i < Detail.RowCount - 1; i++)
            {
                Detail.Rows[i].Cells["受注コード"].Value = code;
                Detail.Rows[i].Cells["受注版数"].Value = edition;
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
                    case "ラインコード":
                        if (string.IsNullOrEmpty(varValue))
                        {
                            MessageBox.Show("処理区分を選択してください。", cellName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;
                    case "型番":
                    case "品名":
                    case "単位コード":
                        if (string.IsNullOrEmpty(varValue))
                        {
                            MessageBox.Show(cellName + "を入力してください。", cellName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;
                    case "数量":
                        if (!FunctionClass.IsLimit_N(varValue, 7, 0, cellName))
                            return true;
                        if (int.Parse(varValue) < 1)
                        {
                            MessageBox.Show(" 1 以上の値を入力してください。", cellName, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;
                    case "単価":
                        if (!FunctionClass.IsLimit_N(varValue, 9, 2, cellName))
                            return true;
                        break;
                    case "SettingSheet":
                        if (string.IsNullOrEmpty(varValue))
                        {
                            MessageBox.Show("設定明細書の処理を入力してください。", "設定明細書の処理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;
                    case "InspectionReport":
                        if (string.IsNullOrEmpty(varValue))
                        {
                            MessageBox.Show("検査成績書の処理を入力してください。", "検査成績書の処理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;
                    case "Specification":
                        if (string.IsNullOrEmpty(varValue))
                        {
                            MessageBox.Show("納入仕様書の処理を入力してください。", "納入仕様書の処理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;
                    case "ParameterSheet":
                        if (string.IsNullOrEmpty(varValue))
                        {
                            MessageBox.Show("非該当証明書の処理を入力してください。", "非該当証明書の処理", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;
                    case "備考":
                        if (gcMultiRow1.CurrentRow.Cells["SettingSheet"].DisplayText == "01" && string.IsNullOrEmpty(varValue))
                        {
                            MessageBox.Show("設定明細内容を入力してください。", "設定明細内容", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;
                    case "シリアル番号付加":
                        if (string.IsNullOrEmpty(varValue))
                        {
                            MessageBox.Show("シリアル番号の指示を選択してください。", "入力", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return true;
                        }
                        break;
                    case "CustomerSerialNumberFrom":
                        if ((bool?)gcMultiRow1.CurrentRow.Cells["CustomerSerialNumberRequired"].Value == true
                            && (gcMultiRow1.CurrentRow.Cells["ラインコード"].Value?.ToString() == "001" || gcMultiRow1.CurrentRow.Cells["ラインコード"].Value?.ToString() == "005"))
                        {
                            if (string.IsNullOrEmpty(varValue))
                            {
                                MessageBox.Show("顧客シリアル番号を入力してください。", "顧客シリアル番号", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return true;
                            }
                        }
                        break;
                    case "CustomerSerialNumberTo":
                        if ((bool?)gcMultiRow1.CurrentRow.Cells["CustomerSerialNumberRequired"].Value == true
                            && (gcMultiRow1.CurrentRow.Cells["ラインコード"].Value?.ToString() == "001" || gcMultiRow1.CurrentRow.Cells["ラインコード"].Value?.ToString() == "005"))
                        {
                            if (string.IsNullOrEmpty(varValue))
                            {
                                MessageBox.Show("顧客シリアル番号を入力してください。", "顧客シリアル番号", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return true;
                            }
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




    }
}
