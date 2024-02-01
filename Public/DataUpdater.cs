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
using Microsoft.IdentityModel.Tokens;

namespace u_net.Public
{
    public class DataUpdater
    {/// <summary>
     /// フォームのコントロールと同名のカラムに保存する
     /// 子要素のコントロールを取得するため再帰的に呼び出している
     /// </summary>
     /// <param name="form">対象フォーム</param>
     /// <param name="connection"></param>
     /// <param name="tableName">登録テーブル</param>
     /// <param name="condition">where文　そのまま</param>
     /// <param name="ukname">主キー（コンボボックスの場合TEXTの値を登録したい時）</param>
     /// <param name="transaction"></param>
     /// <param name="ukname2">（コンボボックスの場合TEXTの値を登録したい時）</param>
     /// <param name="cmbname1">（コンボボックスの場合TEXTの値を登録したい時）</param>
     /// <param name="cmbname2">（コンボボックスの場合TEXTの値を登録したい時）</param>
     /// <param name="cmbname3">（コンボボックスの場合TEXTの値を登録したい時）</param>
     /// <param name="cmbname4">（コンボボックスの場合TEXTの値を登録したい時）</param>
     /// <param name="cmbname5">（コンボボックスの場合TEXTの値を登録したい時）</param>
     /// <returns></returns>
        public static bool UpdateOrInsertDataFrom(Form form, SqlConnection connection, string tableName, string condition,
            string ukname, SqlTransaction transaction,string ukname2 = "", string cmbname1 = "", string cmbname2 = "", string cmbname3 = "", string cmbname4 = "", string cmbname5 = "")
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

                        SetControlValues(form.Controls, row, connection, tableName, ukname, transaction, ukname2 ,cmbname1,cmbname2,cmbname3,cmbname4,cmbname5);

                        // データベースに変更を反映
                        SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adapter);
                        adapter.Update(dataSet, tableName);
                    }
                    else
                    {
                        // 既存データが見つからなかった場合（新規モード）
                        DataRow newRow = dataSet.Tables[0].NewRow();

                        SetControlValues(form.Controls, newRow, connection, tableName, ukname, transaction, ukname2, cmbname1, cmbname2, cmbname3, cmbname4, cmbname5);

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
            string tableName, string ukname, SqlTransaction transaction,string? ukname2="", string cmbname1 = "", string cmbname2 = "", string cmbname3 = "", string cmbname4 = "", string cmbname5 = "")
        {
            foreach (Control control in controls)
            {
                if (control is TabControl tabControl)
                {
                    foreach (TabPage tabPage in tabControl.TabPages)
                    {
                        // タブコントロール内のコントロールに再帰的にアクセスする
                        SetControlValues(tabPage.Controls, row, connection, tableName, ukname, transaction, ukname2, cmbname1, cmbname2, cmbname3, cmbname4, cmbname5);
                    }
                }
                else if (control is GroupBox groupBox)
                {
                    // グループボックス内のコントロールに再帰的にアクセスする
                    SetControlValues(groupBox.Controls, row, connection, tableName, ukname, transaction, ukname2, cmbname1, cmbname2, cmbname3, cmbname4, cmbname5);
                }
                else if (control is Panel panel)
                {
                    // パネル内のコントロールに再帰的にアクセスする
                    SetControlValues(panel.Controls, row, connection, tableName, ukname, transaction, ukname2, cmbname1, cmbname2, cmbname3, cmbname4, cmbname5);
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
                            else
                            {
                                controlValue = DBNull.Value;
                            }
                            break;

                        case ComboBox comboBox:
                            if (control.Name == ukname || control.Name == ukname2 || control.Name == cmbname1 || control.Name == cmbname2 || control.Name == cmbname3 || control.Name == cmbname4 || control.Name == cmbname5)
                            {
                                controlValue = comboBox.Text;
                            }
                            else if(comboBox.SelectedValue!=null)
                            {
                                controlValue = comboBox.SelectedValue;
                            }
                            else 
                            {
                                controlValue = DBNull.Value; 
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
                            else
                            {
                                controlValue = DBNull.Value;
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






        /// <summary>
        /// 文書フォーム用
        /// </summary>
        /// <param name="form"></param>
        /// <param name="connection"></param>
        /// <param name="tableName"></param>
        /// <param name="condition"></param>
        /// <param name="ukname"></param>
        /// <param name="transaction"></param>
        /// <param name="CustomFormName"></param>
        /// <param name="ukname2"></param>
        /// <param name="cmbname1"></param>
        /// <param name="cmbname2"></param>
        /// <param name="cmbname3"></param>
        /// <param name="cmbname4"></param>
        /// <param name="cmbname5"></param>
        /// <returns></returns>
        public static bool UpdateOrInsertDataFromCustom(Form form, SqlConnection connection, string tableName, string condition,
            string ukname, SqlTransaction transaction,string CustomFormName, string ukname2 = "", string cmbname1 = "", string cmbname2 = "", string cmbname3 = "", string cmbname4 = "", string cmbname5 = "")
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

                        SetControlValuesCustom(form.Controls, row, connection, tableName, ukname, transaction, CustomFormName, ukname2, cmbname1, cmbname2, cmbname3, cmbname4, cmbname5);

                        // データベースに変更を反映
                        SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adapter);
                        adapter.Update(dataSet, tableName);
                    }
                    else
                    {
                        // 既存データが見つからなかった場合（新規モード）
                        DataRow newRow = dataSet.Tables[0].NewRow();

                        SetControlValuesCustom(form.Controls, newRow, connection, tableName, ukname, transaction, CustomFormName, ukname2, cmbname1, cmbname2, cmbname3, cmbname4, cmbname5);

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

        private static void SetControlValuesCustom(Control.ControlCollection controls, DataRow row, SqlConnection connection,
            string tableName, string ukname, SqlTransaction transaction,string CustomFormName, string? ukname2 = "", string cmbname1 = "", string cmbname2 = "", string cmbname3 = "", string cmbname4 = "", string cmbname5 = "")
        {
            foreach (Control control in controls)
            {
                //該当パネルのコントロールでない場合は次へ
                if (control.Name.IndexOf(CustomFormName) == -1) continue;

                if (control is TabControl tabControl)
                {
                    foreach (TabPage tabPage in tabControl.TabPages)
                    {
                        // タブコントロール内のコントロールに再帰的にアクセスする
                        SetControlValues(tabPage.Controls, row, connection, tableName, ukname, transaction, ukname2, cmbname1, cmbname2, cmbname3, cmbname4, cmbname5);
                    }
                }
                else if (control is GroupBox groupBox)
                {
                    // グループボックス内のコントロールに再帰的にアクセスする
                    SetControlValues(groupBox.Controls, row, connection, tableName, ukname, transaction, ukname2, cmbname1, cmbname2, cmbname3, cmbname4, cmbname5);
                }
                else if (control is Panel panel)
                {
                    // パネル内のコントロールに再帰的にアクセスする
                    SetControlValues(panel.Controls, row, connection, tableName, ukname, transaction, ukname2, cmbname1, cmbname2, cmbname3, cmbname4, cmbname5);
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
                            else
                            {
                                controlValue = DBNull.Value;
                            }
                            break;

                        case ComboBox comboBox:
                            if (control.Name == ukname || control.Name == ukname2 || control.Name == cmbname1 || control.Name == cmbname2 || control.Name == cmbname3 || control.Name == cmbname4 || control.Name == cmbname5)
                            {
                                controlValue = comboBox.Text;
                            }
                            else if (comboBox.SelectedValue != null)
                            {
                                controlValue = comboBox.SelectedValue;
                            }
                            else
                            {
                                controlValue = DBNull.Value;
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
                            else
                            {
                                controlValue = DBNull.Value;
                            }
                            break;
                    }

                    //テーブルの列名と合わせるために_以降のみ取得
                    controlName = controlName.Substring(controlName.IndexOf('_') + 1);

                    if (controlValue != null && row.Table.Columns.Contains(controlName))
                    {
                        row[controlName] = controlValue;
                    }

                }
            }
        }





        /// <summary>
        /// 明細情報の登録
        /// </summary>
        /// <param name="multiRow"></param>
        /// <param name="connection"></param>
        /// <param name="tableName"></param>
        /// <param name="condition">where条件</param>
        /// <param name="ukname">コンボボックスのDisplayTextを登録したい場合</param>
        /// <param name="transaction"></param>
        /// <returns></returns>
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

                    // 明細行数分データを登録
                    for (int i = 0; i < multiRow.RowCount; i++)
                    {
                        if(multiRow.Rows[i].IsNewRow == true)
                        {
                            //新規行の場合は、処理をスキップ
                            continue;
                        }
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
                        else
                        {
                            controlValue = DBNull.Value;
                        }
                        break;

                    case TextBoxCell textBoxCell:
                        if (!string.IsNullOrEmpty(textBoxCell.DisplayText))
                        {
                            controlValue = textBoxCell.Value;
                        }
                        else
                        {
                            controlValue = DBNull.Value;
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


