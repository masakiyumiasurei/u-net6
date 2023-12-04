using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using GrapeCity.Win.MultiRow;

namespace u_net.Public
{
    public class DataUpdater
    {
        public static bool UpdateOrInsertDataFrom(Form form, SqlConnection connection, string tableName, string condition,
            string ukname, SqlTransaction transaction)
        {
            try
            {
                // 指定された条件で既存のデータを検索
                string selectQuery = $"SELECT * FROM {tableName} WHERE {condition}";
                using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection, transaction))
                using (SqlDataAdapter adapter = new SqlDataAdapter(selectCommand))
                using (DataSet dataSet = new DataSet())
                using (Control control = new Control())
                {
                    adapter.Fill(dataSet, tableName);

                    if (dataSet.Tables[0].Rows.Count == 1)
                    {
                        // 既存データが見つかった場合（更新モード）
                        DataRow row = dataSet.Tables[0].Rows[0];

                        SetControlValues(form.Controls, row, connection, tableName, ukname, transaction);

                        // データベースに変更を反映
                        SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adapter);
                        adapter.Update(dataSet, tableName);
                    }
                    else
                    {
                        // 既存データが見つからなかった場合（新規モード）
                        DataRow newRow = dataSet.Tables[0].NewRow();

                        SetControlValues(form.Controls, newRow, connection, tableName, ukname, transaction);

                        dataSet.Tables[0].Rows.Add(newRow);

                        // データベースに新規データを追加
                        SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adapter);
                        adapter.Update(dataSet, tableName);
                    }
                }


                return true; // 成功した場合は true を返す
            }
            catch (Exception ex)
            {

                MessageBox.Show($"データの更新中にエラーが発生しました: {ex.Message}");
                return false; // エラーがある場合は false を返す
            }
        }

        private static void SetControlValues(Control.ControlCollection controls, DataRow row, SqlConnection connection,
            string tableName, string ukname, SqlTransaction transaction)
        {
            foreach (Control control in controls)
            {
                if (control is TabControl tabControl)
                {
                    foreach (TabPage tabPage in tabControl.TabPages)
                    {
                        // タブコントロール内のコントロールに再帰的にアクセスする
                        SetControlValues(tabPage.Controls, row, connection, tableName, ukname, transaction);
                    }
                }
                else if (control is GroupBox groupBox)
                {
                    // グループボックス内のコントロールに再帰的にアクセスする
                    SetControlValues(groupBox.Controls, row, connection, tableName, ukname, transaction);
                }
                else
                {
                    if (!(control is TextBox) && !(control is ComboBox) && !(control is CheckBox) && !(control is MaskedTextBox))
                    {
                        continue;
                    }

                    string controlName = control.Name;
                    object controlValue = null;

                    switch (control)
                    {
                        case TextBox textBox:
                            if (!string.IsNullOrEmpty(textBox.Text))
                            {
                                controlValue = textBox.Text;
                            }
                            break;

                        case ComboBox comboBox:
                            if (control.Name == ukname)
                            {
                                controlValue = comboBox.Text;
                            }
                            else
                            {
                                controlValue = comboBox.SelectedValue;
                            }
                            break;

                        case CheckBox checkBox:
                            controlValue = checkBox.Checked ? -1 : 0;
                            break;

                        case MaskedTextBox maskedtextBox:
                            if (!string.IsNullOrEmpty(maskedtextBox.Text))
                            {
                                controlValue = maskedtextBox.Text;
                            }
                            break;
                    }

                    if (controlValue != null && row.Table.Columns.Contains(controlName))
                    {
                        row[controlName] = controlValue;
                    }
                }
            }
        }


        public static bool UpdateOrInsertDetails(GcMultiRow multiRow, SqlConnection connection, string tableName, string condition,
            string ukname, SqlTransaction transaction)
        {
            try
            {
                
                string selectQuery = $"SELECT * FROM {tableName} WHERE {condition}";
                string deleteQuery = $"DELETE FROM {tableName} WHERE {condition}";
                using (SqlCommand selectCommand = new SqlCommand(selectQuery, connection, transaction))
                using (SqlCommand deleteCommand = new SqlCommand(deleteQuery, connection, transaction))
                using (SqlDataAdapter adapter = new SqlDataAdapter(selectCommand))
                using (DataSet dataSet = new DataSet())
                using (Control control = new Control())
                {
                    // 指定された条件で既存のデータを全削除
                    deleteCommand.ExecuteNonQuery();

                    // 指定された条件で既存のデータを検索
                    adapter.Fill(dataSet, tableName);

                    // 明細行数分データを登録 (編集行を除く為、RowCountを-1しているが、別の方法を検討した方がよさそう)
                    for (int i = 0; i < multiRow.RowCount -1; i++)
                    {
                        DataRow newRow = dataSet.Tables[0].NewRow();
                        SetControlValuesMultiRow(multiRow.Rows[i].Cells, newRow, connection, tableName, ukname, transaction);
                        dataSet.Tables[0].Rows.Add(newRow);
                    }

                    // データベースに新規データを追加
                    SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adapter);
                    adapter.Update(dataSet, tableName);
                }

                return true; // 成功した場合は true を返す
            }
            catch (Exception ex)
            {

                MessageBox.Show($"データの更新中にエラーが発生しました: {ex.Message}");
                return false; // エラーがある場合は false を返す
            }
        }

        private static void SetControlValuesMultiRow(CellCollection cells, DataRow row, SqlConnection connection,
            string tableName, string ukname, SqlTransaction transaction)
        {
            foreach (Cell cell in cells)
            {
                if (!(cell is RowHeaderCell) && !(cell is TextBoxCell) && !(cell is ComboBoxCell) && !(cell is CheckBoxCell) && !(cell is NumericUpDownCell))
                {
                    continue;
                }

                string controlName = cell.Name;
                object controlValue = null;

                switch (cell)
                {
                    case RowHeaderCell rowHeaderCell:
                        if (!string.IsNullOrEmpty(rowHeaderCell.DisplayText))
                        {
                            controlValue = rowHeaderCell.DisplayText;
                        }
                        break;

                    case TextBoxCell textBoxCell:
                        if (!string.IsNullOrEmpty(textBoxCell.Value.ToString()))
                        {
                            controlValue = textBoxCell.Value;
                        }
                        break;

                    case ComboBoxCell comboBoxCell:
                        if (cell.Name == ukname)
                        {
                            //controlValue = comboBox.Text;
                            controlValue = comboBoxCell.DisplayText;
                        }
                        else
                        {
                            //controlValue = comboBox.SelectedValue;
                            controlValue = comboBoxCell.Value;
                        }
                        break;

                    case CheckBoxCell checkBoxCell:
                        controlValue = (bool)checkBoxCell.Value ? -1 : 0;
                        break;

                    case NumericUpDownCell numericUpDownCell:
                        if (numericUpDownCell.Value != null)
                        {
                            controlValue = numericUpDownCell.Value;
                        }
                        break;
                }

                if (controlValue != null && row.Table.Columns.Contains(controlName))
                {
                    row[controlName] = controlValue;
                }
            }
        }
    }

}


