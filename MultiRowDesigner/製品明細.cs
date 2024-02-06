using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GrapeCity.Win.MultiRow;
using u_net;
using u_net.Public;

namespace MultiRowDesigner
{
    public partial class 製品明細 : UserControl
    {
        public GcMultiRow Detail
        {
            get
            {
                return gcMultiRow1;
            }
        }

        private SqlConnection? cn;
        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }

        public 製品明細()
        {
            InitializeComponent();
        }

        private void 製品明細_Load(object sender, EventArgs e)
        {
            gcMultiRow1.ShortcutKeyManager.Unregister(Keys.Enter);
            gcMultiRow1.ShortcutKeyManager.Register(SelectionActions.MoveToNextCell, Keys.Enter);
            gcMultiRow1.DataError += new EventHandler<DataErrorEventArgs>(gcMultiRow1_DataError);

        }

        bool errFlg = false;
        private void gcMultiRow1_DataError(object sender, DataErrorEventArgs e)
        {
            if (errFlg)
            {
                errFlg = false;
                return;
            }
            GcMultiRow gcMultiRow = sender as GcMultiRow;

            if (e.Exception is ArgumentException || e.Exception is FormatException)
            {
                if (gcMultiRow.Rows[e.RowIndex].Cells[e.CellIndex] is TextBoxCell)
                {
                    TextBoxCell textBoxCell1 = gcMultiRow.Rows[e.RowIndex].Cells[e.CellIndex] as TextBoxCell;

                    if (textBoxCell1.Name == "ユニット版数")
                    {
                        e.ThrowException = false;

                        MessageBox.Show("数値を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        Console.WriteLine(e.Exception.Message);
                    }
                }
            }
        }
        private void gcMultiRow1_RowsRemoved(object sender, RowsRemovedEventArgs e)
        {
            F_製品? f_製品 = Application.OpenForms.OfType<F_製品>().FirstOrDefault();

            for (int i = 0; i < gcMultiRow1.RowCount; i++)
            {
                if (gcMultiRow1.Rows[i].IsNewRow == true)
                {
                    //新規行の場合は、処理をスキップ
                    continue;
                }

                gcMultiRow1.Rows[i].Cells["明細番号"].Value = i + 1;
                gcMultiRow1.Rows[i].Cells["製品コード"].Value = f_製品.製品コード.Text;
                gcMultiRow1.Rows[i].Cells["製品版数"].Value = f_製品.製品版数.Text;


            }

            f_製品.ChangedData(true);
        }

        private void gcMultiRow1_RowsAdded(object sender, RowsAddedEventArgs e)
        {
            F_製品? f_製品 = Application.OpenForms.OfType<F_製品>().FirstOrDefault();

            for (int i = 0; i < gcMultiRow1.RowCount; i++)
            {
                if (gcMultiRow1.Rows[i].IsNewRow == true)
                {
                    //新規行の場合は、処理をスキップ
                    continue;
                }

                gcMultiRow1.Rows[i].Cells["明細番号"].Value = i + 1;
                gcMultiRow1.Rows[i].Cells["製品コード"].Value = f_製品.製品コード.Text;
                gcMultiRow1.Rows[i].Cells["製品版数"].Value = f_製品.製品版数.Text;

            }

            f_製品.ChangedData(true);
        }

        private void gcMultiRow1_ModifiedChanged(object sender, EventArgs e)
        {
            //F_製品? f_製品 = Application.OpenForms.OfType<F_製品>().FirstOrDefault();
            //f_製品.ChangedData(true);
        }

        private void gcMultiRow1_CellContentClick(object sender, CellEventArgs e)
        {

            switch (gcMultiRow1.CurrentCell)
            {
                //ボタンClick時の処理
                case ButtonCell:
                    switch (e.CellName)
                    {
                        case "明細削除ボタン":

                            if (gcMultiRow1.CurrentRow.IsNewRow == true)
                            {
                                //新規行の場合は、処理をスキップ
                                MessageBox.Show("新規行は削除できません。", "削除", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }

                            if (gcMultiRow1.ReadOnly == true)
                            {
                                MessageBox.Show("編集はできません。", "削除", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                return;
                            }

                            if (MessageBox.Show("明細行(" + (e.RowIndex + 1) + ")を削除しますか？", "明細行削除", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                gcMultiRow1.Rows.RemoveAt(e.RowIndex);
                            }
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
            u_net.F_製品 objForm = (u_net.F_製品)Application.OpenForms["F_製品"];

            switch (gcMultiRow1.CurrentCell)
            {
                //テキストボックスEnter時の処理
                case TextBoxCell:
                    switch (e.CellName)
                    {
                        case "型式名":
                            objForm.toolStripStatusLabel1.Text = "■製品の型式名を入力します。　■半角１６文字まで入力できます。　■型式は購買時の最小単位となります。";
                            break;

                        case "ユニットコード":
                            objForm.toolStripStatusLabel1.Text = "";
                            break;

                        case "ユニット版数":
                            objForm.toolStripStatusLabel1.Text = "";
                            break;

                        case "改版中":
                            objForm.toolStripStatusLabel1.Text = "";
                            break;

                        case "品名":
                            objForm.toolStripStatusLabel1.Text = "";
                            break;

                        case "型番":
                            objForm.toolStripStatusLabel1.Text = "";
                            break;

                        case "RohsStatusSign":
                            objForm.toolStripStatusLabel1.Text = "";
                            break;

                        case "非含有証明書":
                            objForm.toolStripStatusLabel1.Text = "";
                            break;

                        case "ユニット材料費":
                            objForm.toolStripStatusLabel1.Text = "";
                            break;

                        case "変更内容":
                            objForm.toolStripStatusLabel1.Text = "■全角文字で30文字まで入力できます。";
                            break;
                        case "カレント":
                            int row = gcMultiRow1.CurrentRow.Index;
                            int col = gcMultiRow1.CurrentRow.Cells["型式名"].CellIndex;
                            gcMultiRow1.CurrentCellPosition = new CellPosition(row, col);
                            break;

                        default:
                            objForm.toolStripStatusLabel1.Text = "各種項目の説明";
                            break;
                    }
                    break;

                //コンボボックスEnter時の処理
                case ComboBoxCell:
                    switch (e.CellName)
                    {
                        case "変更操作コード":
                            objForm.toolStripStatusLabel1.Text = "■改版時に入力します。変更操作を選択してください。";
                            break;

                        default:
                            break;
                    }
                    break;

                default:
                    break;
            }

        }

        private bool IsErrorData(int rowIndex, bool cancel)
        {
            try
            {
                // エラーチェック
                bool isError = false;
                foreach (var cell in gcMultiRow1.Rows[rowIndex].Cells)
                {
                    var varValue = cell.Value;
                    string strName = cell.Name;

                    switch (strName)
                    {
                        case "型式名":
                        case "ユニットコード":
                            if (varValue == DBNull.Value)
                            {
                                MessageBox.Show($"[{strName}] を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                goto Exit_IsError;
                            }
                            break;

                    }
                }
                return false; // エラーなしの場合

            Exit_IsError:
                // エラー発生後の処理
                isError = true;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{Name}_IsError - {ex.Message}");
                return true;
            }
        }
        private bool IsError(Cell controlObject)
        {
            try
            {
                // エラーチェック
                bool isError = false;


                object varValue = controlObject.Value;
                switch (controlObject.Name)
                {
                    case "型式名":
                    case "ユニットコード":
                        if (varValue == null)
                        {
                            MessageBox.Show($"[{controlObject.Name}] を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }
                        break;

                }

                return false; // エラーなしの場合

            Exit_IsError:
                // エラー発生後の処理
                isError = true;

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{Name}_IsError - {ex.Message}");
                return true;
            }
        }

        private void gcMultiRow1_CellValidating(object sender, CellValidatingEventArgs e)
        {
            errFlg = true;
            gcMultiRow1.EndEdit();
            //gcMultiRow1.CancelEdit();
            //e.Cancel = true;

            switch (e.CellName)
            {
                case "ユニットコード":
                    if (IsError(gcMultiRow1.CurrentCell) == true) e.Cancel = true;
                    break;

                case "型式名":
                    if (IsError(gcMultiRow1.CurrentCell) == true) e.Cancel = true;
                    break;

            }
        }

        private void gcMultiRow1_CellValidated(object sender, CellEventArgs e)
        {
            switch (e.CellName)
            {
                case "ユニットコード":
                    UpdatedControl(gcMultiRow1.CurrentCell);
                    break;
                case "型式名":
                    UpdatedControl(gcMultiRow1.CurrentCell);
                    break;

            }            
        }

        private void UpdatedControl(Cell controlObject)
        {
            Connect();

            try
            {
                // UpdatedControlの本体

                object varParm = controlObject.Value;
                switch (controlObject.Name)
                {
                    case "ユニットコード":
                        string strKey = $"ユニットコード='{varParm}'";
                        string strSQL = $"SELECT * " +
                                        $"FROM V製品_ユニット更新 " +
                                        $"WHERE {strKey}";


                        using (SqlCommand command = new SqlCommand(strSQL, cn))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    gcMultiRow1.CurrentRow.Cells["ユニット版数"].Value = reader["ユニット版数"].ToString();
                                    gcMultiRow1.CurrentRow.Cells["改版中"].Value = reader["改版中"].ToString();
                                    gcMultiRow1.CurrentRow.Cells["品名"].Value = reader["品名"].ToString();
                                    gcMultiRow1.CurrentRow.Cells["型番"].Value = reader["型番"].ToString();
                                    gcMultiRow1.CurrentRow.Cells["RohsStatusSign"].Value = reader["RohsStatusSign"].ToString();
                                    gcMultiRow1.CurrentRow.Cells["ユニット材料費"].Value = reader["ユニット材料費"].ToString();


                                }
                            }
                        }

                        F_製品? f_製品 = Application.OpenForms.OfType<F_製品>().FirstOrDefault();
                        f_製品.ChangedData(true);

                        break;

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"{Name}_UpdatedControl - {ex.Message}");
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

                if (gcMultiRow1.CurrentCell.Name == "ユニットコード")
                {
                    textBox.DoubleClick -= gcMultiRow1_CellDoubleClick;
                    textBox.DoubleClick += gcMultiRow1_CellDoubleClick;
                }
            }
            else if (comboBox != null)
            {
                if (gcMultiRow1.CurrentCell.Name == "変更操作コード")
                {
                    comboBox.DrawMode = DrawMode.OwnerDrawFixed;
                    comboBox.DrawItem -= 変更操作コード_DrawItem;
                    comboBox.DrawItem += 変更操作コード_DrawItem;
                    comboBox.SelectedIndexChanged -= 変更操作コード_SelectedIndexChanged;
                    comboBox.SelectedIndexChanged += 変更操作コード_SelectedIndexChanged;
                }

            }
        }

        private void gcMultiRow1_CellDoubleClick(object sender, EventArgs e)
        {
            if (gcMultiRow1.CurrentCell.RowIndex == null || gcMultiRow1.CurrentCell.CellIndex == null) return;

            switch (gcMultiRow1.CurrentCell.Name)
            {
                case "ユニットコード":

                    codeSelectionForm = new F_ユニット選択();
                    if (codeSelectionForm.ShowDialog() == DialogResult.OK)
                    {
                        string selectedCode = codeSelectionForm.SelectedCode;

                        gcMultiRow1.EditingControl.Text = selectedCode;
                        gcMultiRow1.CurrentCell.Value = selectedCode;
                        gcMultiRow1.CurrentCellPosition = new CellPosition(gcMultiRow1.CurrentRow.Index, gcMultiRow1.CurrentRow.Cells["品名"].CellIndex);
                    }
                    break;



            }
        }

        private void 変更操作コード_DrawItem(object sender, DrawItemEventArgs e)
        {
            ComboBoxEditingControl combo = sender as ComboBoxEditingControl;
            OriginalClass.SetComboBoxAppearance(combo, e, new int[] { 50, 0 }, new string[] { "Display", "Display2" });

        }

        private void 変更操作コード_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBoxEditingControl combo = sender as ComboBoxEditingControl;
            if (combo.SelectedIndex < 0) return;
            gcMultiRow1.CurrentRow.Cells["削除対象"].Value = ((DataRowView)combo.SelectedItem)?.Row.Field<bool>("Display2").ToString();



        }


        private void gcMultiRow1_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            errFlg = true;
            //  gcMultiRow1.EndEdit();


            if (e.KeyCode == Keys.Return)
            {
                gcMultiRow1.EndEdit();

                if (gcMultiRow1.CurrentCell.RowIndex == null || gcMultiRow1.CurrentCell.CellIndex == null) return;

                if (gcMultiRow1.CurrentCell.Name == "ユニットコード")
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



        private F_ユニット選択 codeSelectionForm;
        private void gcMultiRow1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == ' ')
            {
                if (gcMultiRow1.CurrentCell.RowIndex == null || gcMultiRow1.CurrentCell.CellIndex == null) return;

                switch (gcMultiRow1.CurrentCell.Name)
                {
                    case "ユニットコード":
                        e.Handled = true;


                        codeSelectionForm = new F_ユニット選択();
                        if (codeSelectionForm.ShowDialog() == DialogResult.OK)
                        {
                            string selectedCode = codeSelectionForm.SelectedCode;

                            gcMultiRow1.CurrentCell.Value = selectedCode;
                            gcMultiRow1.CurrentCellPosition = new CellPosition(gcMultiRow1.CurrentRow.Index, gcMultiRow1.CurrentRow.Cells["品名"].CellIndex);
                        }
                        break;



                }
            }

        }



        private void gcMultiRow1_RowDragMoveCompleted(object sender, DragMoveCompletedEventArgs e)
        {

            F_製品? f_製品 = Application.OpenForms.OfType<F_製品>().FirstOrDefault();

            gcMultiRow1.BeginInvoke(() =>
            {
                for (int i = 0; i < gcMultiRow1.RowCount; i++)
                {
                    if (gcMultiRow1.Rows[i].IsNewRow == true)
                    {
                        //新規行の場合は、処理をスキップ
                        continue;
                    }

                    gcMultiRow1.Rows[i].Cells["明細番号"].Value = i + 1;
                    gcMultiRow1.Rows[i].Cells["製品コード"].Value = f_製品.製品コード.Text;
                    gcMultiRow1.Rows[i].Cells["製品版数"].Value = f_製品.製品版数.Text;

                }

            });

            f_製品.ChangedData(true);
        }

        private void gcMultiRow1_RowEnter(object sender, CellEventArgs e)
        {
            gcMultiRow1.CurrentRow.Cells["カレント"].Style.BackColor = Color.DeepPink;
        }

        private void gcMultiRow1_RowLeave(object sender, CellEventArgs e)
        {
            gcMultiRow1.CurrentRow.Cells["カレント"].Style.BackColor = Color.White;
        }

        private void gcMultiRow1_RowValidating(object sender, CellCancelEventArgs e)
        {
            var targetRow = e.RowIndex;
            if (gcMultiRow1.Rows[e.RowIndex].IsNewRow) return;

            if (IsErrorData(targetRow, false)) e.Cancel = true;
        }
    }
}
