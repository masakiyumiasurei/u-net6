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
    public partial class 年間教育計画サブ : UserControl
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

        public 年間教育計画サブ()
        {
            InitializeComponent();
        }

        private void ユニット明細_Load(object sender, EventArgs e)
        {
            gcMultiRow1.ShortcutKeyManager.Unregister(Keys.Enter);
            gcMultiRow1.ShortcutKeyManager.Register(SelectionActions.MoveToNextCell, Keys.Enter);

        }

        private void gcMultiRow1_RowsRemoved(object sender, RowsRemovedEventArgs e)
        {
            F_年間教育計画? f_年間教育計画 = Application.OpenForms.OfType<F_年間教育計画>().FirstOrDefault();

            for (int i = 0; i < gcMultiRow1.RowCount; i++)
            {
                if (gcMultiRow1.Rows[i].IsNewRow == true)
                {
                    //新規行の場合は、処理をスキップ
                    continue;
                }

                gcMultiRow1.Rows[i].Cells["明細番号"].Value = i + 1;
                //gcMultiRow1.Rows[i].Cells["ユニットコード"].Value = f_年間教育計画.ユニットコード.Text;
                //gcMultiRow1.Rows[i].Cells["ユニット版数"].Value = f_年間教育計画.ユニット版数.Text;


            }

            f_年間教育計画.ChangedData(true);
        }

        private void gcMultiRow1_RowsAdded(object sender, RowsAddedEventArgs e)
        {
            F_年間教育計画? f_年間教育計画 = Application.OpenForms.OfType<F_年間教育計画>().FirstOrDefault();

            for (int i = 0; i < gcMultiRow1.RowCount; i++)
            {
                if (gcMultiRow1.Rows[i].IsNewRow == true)
                {
                    //新規行の場合は、処理をスキップ
                    continue;
                }

                gcMultiRow1.Rows[i].Cells["明細番号"].Value = i + 1;
                //gcMultiRow1.Rows[i].Cells["ユニットコード"].Value = f_年間教育計画.ユニットコード.Text;
                //gcMultiRow1.Rows[i].Cells["ユニット版数"].Value = f_年間教育計画.ユニット版数.Text;

            }

            f_年間教育計画.ChangedData(true);
        }

        private void gcMultiRow1_CellContentButtonClick(object sender, CellEventArgs e)
        {

            switch (e.CellName)
            {
                case "登録コードボタン":
                    int col = gcMultiRow1.CurrentRow.Cells["登録コードボタン"].CellIndex;
                    int row = 0;
                    if (gcMultiRow1.CurrentRow.Index > 0)
                    {
                        row = gcMultiRow1.CurrentRow.Index;
                    }
                    gcMultiRow1.CurrentCellPosition = new CellPosition(row, col);
                    break;
                default:
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
            u_net.F_ユニット objForm = (u_net.F_ユニット)Application.OpenForms["F_ユニット"];

            if (objForm != null)
            {
                switch (gcMultiRow1.CurrentCell)
                {
                    //テキストボックスEnter時の処理
                    case TextBoxCell:
                        switch (e.CellName)
                        {
                            case "カレント":
                                //int row = gcMultiRow1.CurrentRow.Index;
                                //int col = gcMultiRow1.CurrentRow.Cells["構成番号"].CellIndex;
                                //gcMultiRow1.CurrentCellPosition = new CellPosition(row, col);
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


        private bool IsError(Cell controlObject)
        {
            try
            {
                // エラーチェック
                bool isError = false;


                object varValue = controlObject.Value;
                switch (controlObject.Name)
                {
                    case "構成番号":
                        if (varValue == null)
                        {
                            MessageBox.Show($"[{controlObject.Name}] を入力してください。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }

                        if (DetectRepeatedID(varValue.ToString()))
                        {
                            MessageBox.Show($"構成番号が重複しています。", "エラー", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            goto Exit_IsError;
                        }


                        break;
                    case "部品コード":
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




        private bool DetectRepeatedID(string TargetID)
        {

            if (string.IsNullOrEmpty(TargetID)) return false;

            for (int i = 0; i < gcMultiRow1.RowCount; i++)
            {
                if (gcMultiRow1.Rows[i].IsNewRow == true)
                {
                    //新規行の場合は、処理をスキップ
                    continue;
                }

                if(gcMultiRow1.CurrentRow.Index == i)
                {
                    //自分自身は対象としない
                    continue;
                }

                if (gcMultiRow1.Rows[i].Cells["構成番号"].Value?.ToString() == TargetID)
                {
                    return true;
                }


            }

            return false;
        }




        private void gcMultiRow1_CellValidating(object sender, CellValidatingEventArgs e)
        {
            gcMultiRow1.EndEdit();
            //gcMultiRow1.CancelEdit();
            //e.Cancel = true;

            switch (e.CellName)
            {
                case "構成番号":
                    if (IsError(gcMultiRow1.CurrentCell) == true) e.Cancel = true;
                    break;

                case "部品コード":
                    if (IsError(gcMultiRow1.CurrentCell) == true) e.Cancel = true;
                    break;

            }
        }

        private void gcMultiRow1_CellValidated(object sender, CellEventArgs e)
        {

            switch (e.CellName)
            {


                case "部品コード":
                    UpdatedControl(gcMultiRow1.CurrentCell);
                    break;

               

            }

            F_ユニット? f_ユニット = Application.OpenForms.OfType<F_ユニット>().FirstOrDefault();
            f_ユニット.ChangedData(true);
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
                    case "部品コード":
                        string strKey = $"部品コード='{varParm}'";
                        string strSQL = $"SELECT *,(単価/入数) as ユニット材料費 " +
                                        $"FROM Vユニット明細_部品詳細 " +
                                        $"WHERE {strKey}";


                        using (SqlCommand command = new SqlCommand(strSQL, cn))
                        {
                            using (SqlDataReader reader = command.ExecuteReader())
                            {
                                if (reader.Read())
                                {
                                    gcMultiRow1.CurrentRow.Cells["廃止"].Value = reader["廃止"].ToString();
                                    gcMultiRow1.CurrentRow.Cells["品名"].Value = reader["品名"].ToString();
                                    gcMultiRow1.CurrentRow.Cells["型番"].Value = reader["型番"].ToString();
                                    gcMultiRow1.CurrentRow.Cells["メーカー名"].Value = reader["メーカー省略名"].ToString();
                                    gcMultiRow1.CurrentRow.Cells["仕入先名"].Value = reader["仕入先名"].ToString();
                                    gcMultiRow1.CurrentRow.Cells["単価"].Value = reader["単価"].ToString();
                                    gcMultiRow1.CurrentRow.Cells["形状名"].Value = reader["形状名"].ToString();
                                    gcMultiRow1.CurrentRow.Cells["RohsStatusSign"].Value = reader["RohsStatusSign"].ToString();
                                    gcMultiRow1.CurrentRow.Cells["ChemSherpaVersion"].Value = reader["ChemSherpaVersion"].ToString();
                                    gcMultiRow1.CurrentRow.Cells["入数"].Value = reader["入数"].ToString();
                                    gcMultiRow1.CurrentRow.Cells["ユニット材料費"].Value = reader["ユニット材料費"].ToString();


                                }
                            }
                        }

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

                if (gcMultiRow1.CurrentCell.Name == "部品コード")
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


        private F_部品選択 codeSelectionForm;

        private void gcMultiRow1_CellDoubleClick(object sender, EventArgs e)
        {
            if (gcMultiRow1.CurrentCell.RowIndex == null || gcMultiRow1.CurrentCell.CellIndex == null) return;

            switch (gcMultiRow1.CurrentCell.Name)
            {
                case "部品コード":

                    codeSelectionForm = new F_部品選択();
                    if (codeSelectionForm.ShowDialog() == DialogResult.OK)
                    {
                        string selectedCode = codeSelectionForm.SelectedCode;

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
                    case "部品コード":
                        e.Handled = true;


                        codeSelectionForm = new F_部品選択();
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

        private void gcMultiRow1_RowEnter(object sender, CellEventArgs e)
        {
            gcMultiRow1.CurrentRow.Cells["カレント"].Style.BackColor = Color.DeepPink;
        }

        private void gcMultiRow1_RowLeave(object sender, CellEventArgs e)
        {
            gcMultiRow1.CurrentRow.Cells["カレント"].Style.BackColor = Color.White;
        }

    }
}
