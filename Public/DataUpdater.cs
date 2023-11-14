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

namespace u_net.Public
{
    public class DataUpdater
    {
        public static bool UpdateOrInsertDataFrom(Form form, SqlConnection connection, string tableName, string condition, 
            string ukname ,SqlTransaction transaction)
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


                        //foreach (Control control in form.Controls)
                        //{
                        //    if (control is TextBox || control is ComboBox || control is CheckBox)
                        //    {
                        //        string controlName = control.Name;
                        //        object controlValue = null;

                        //        switch (control)
                        //        {
                        //            case TextBox textBox:
                        //                if (!string.IsNullOrEmpty(textBox.Text))
                        //                {
                        //                    controlValue = textBox.Text;
                        //                }
                        //                break;

                        //            case ComboBox comboBox:
                        //                //ユニークキーは初期値で表示され、selectedvalueが異なるための処理
                        //                if (control.Name == ukname)
                        //                {
                        //                    controlValue = comboBox.Text;
                        //                }
                        //                else
                        //                {
                        //                    controlValue = comboBox.SelectedValue;
                        //                }
                        //                break;

                        //            case CheckBox checkBox:
                        //                //controlValue = checkBox.Checked;
                        //                if (checkBox.Checked)
                        //                {
                        //                    controlValue = -1;
                        //                }
                        //                else
                        //                {
                        //                    controlValue = 0;
                        //                }
                        //                break;
                        //        }

                        //        if (controlValue != null && dataSet.Tables[0].Columns.Contains(controlName))
                        //        {
                        //            row[controlName] = controlValue;
                        //        }
                        //    }
                        //}

                        // データベースに変更を反映
                        SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adapter);
                        adapter.Update(dataSet, tableName);
                    }
                    else
                    {
                        // 既存データが見つからなかった場合（新規モード）
                        //DataRow newRow = dataSet.Tables[0].NewRow();
                        //foreach (Control control in form.Controls)
                        //{
                        //    if (control is TextBox || control is ComboBox || control is CheckBox)
                        //    {
                        //        string controlName = control.Name;
                        //        object controlValue = null;

                        //        switch (control)
                        //        {
                        //            case TextBox textBox:
                        //                if (!string.IsNullOrEmpty(textBox.Text))
                        //                {
                        //                    controlValue = textBox.Text;
                        //                }
                        //                break;

                        //            case ComboBox comboBox:
                        //                //ユニークキーは初期値で表示され、selectedvalueが異なるための処理
                        //                if (control.Name == ukname)
                        //                {
                        //                    controlValue = comboBox.Text;
                        //                }
                        //                else
                        //                {
                        //                    controlValue = comboBox.SelectedValue;
                        //                }
                        //                break;

                        //            case CheckBox checkBox:
                        //                if (checkBox.Checked)
                        //                {
                        //                    controlValue = -1;
                        //                }
                        //                else
                        //                {
                        //                    controlValue = 0;
                        //                }

                        //                break;
                        //        }

                        //        if (controlValue != null && dataSet.Tables[0].Columns.Contains(controlName))
                        //        {
                        //            newRow[controlName] = controlValue;
                        //        }
                        //    }
                        //}

                       // dataSet.Tables[0].Rows.Add(newRow);

                        // データベースに新規データを追加
                        SqlCommandBuilder cmdBuilder = new SqlCommandBuilder(adapter);
                        adapter.Update(dataSet, tableName);
                    }
                }

                // トランザクション内の操作が正常に終了したことをコミット
                //transaction.Commit();

                return true; // 成功した場合は true を返す
            }
            catch (Exception ex)
            {
                try
                {
                    // トランザクション内の操作にエラーがあった場合はロールバック
                    transaction.Rollback();
                }
                catch (Exception rollbackEx)
                {
                    MessageBox.Show($"ロールバック中にエラーが発生しました: {rollbackEx.Message}");
                }

                // エラーが発生した場合の処理
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
                    if (!(control is TextBox) && !(control is ComboBox) && !(control is CheckBox))
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
                    }

                    if (controlValue != null && row.Table.Columns.Contains(controlName))
                    {
                        row[controlName] = controlValue;
                    }
                }
            }
        }

    }

}


