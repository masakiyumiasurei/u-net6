using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Diagnostics;
using Microsoft.Data.SqlClient;
using Newtonsoft.Json.Linq;
using TextBox = System.Windows.Forms.TextBox;
using ComboBox = System.Windows.Forms.ComboBox;
using System.Drawing.Imaging;
using System.Drawing.Printing;

namespace u_net.Public
{
    internal class VariableSet
    {

        public static void SetForm2Table(Form formObject, DataRow dataRow, string exControlName1, string exControlName2)
        {
            try
            {
                foreach (DataColumn column in dataRow.Table.Columns)
                {
                    string fieldName = column.ColumnName;

                    if (fieldName != exControlName1 && fieldName != exControlName2)
                    {
                        if (formObject.Controls.ContainsKey(fieldName))
                        {
                            // フォームのコントロール名がテーブルのフィールド名と一致する場合
                            // データを設定
                            formObject.Controls[fieldName].Text = dataRow[fieldName].ToString();
                        }
                        else
                        {
                            // フィールド名とコントロール名が一致しない場合、エラー処理を行う
                            
                            Debug.Print("SetForm2Table - フィールド " + fieldName + " のコントロールがフォーム上で見つかりません。");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print("SetForm2Table - " + ex.Message);
            }
        }


        public static bool SetTable2Form(Form formObject, string sourceSQL, SqlConnection cn)
        {
            //タブコントロール、グループボックスにアクセスするため、再帰関数とする
            if (string.IsNullOrWhiteSpace(sourceSQL) || cn == null) return false; // クエリまたは接続が無効な場合は何もしない

                using (SqlCommand command = new SqlCommand(sourceSQL, cn)) 

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        reader.Read();

                    SetControlValues(formObject.Controls, reader);

                    // データベースの列名をフォームのコントロール名と一致させておく
                    //for (int i = 0; i < reader.FieldCount; i++)
                    //    {
                    //        string columnName = reader.GetName(i);

                    //        if (formObject.Controls.ContainsKey(columnName))
                    //        {
                    //            Control control = formObject.Controls[columnName];

                    //            // TextBoxとComboBoxで処理を分ける
                    //            if (control is TextBox)
                    //            {
                    //                control.Text = reader[columnName].ToString();
                    //            }
                    //            else if (control is ComboBox)
                    //            {
                    //                ComboBox comboBox = (ComboBox)control;
                    //                comboBox.SelectedValue = reader[columnName];
                    //            }
                    //            else if (control is CheckBox)
                    //            {
                    //                CheckBox checkBox = (CheckBox)control;
                    //                bool value;
                    //                if (reader[columnName] == DBNull.Value || Convert.ToInt32(reader[columnName]) == 0)
                    //                {
                    //                    checkBox.Checked = false; // 値が0またはnullの場合、チェックを外す
                    //                }
                    //                else
                    //                {
                    //                    checkBox.Checked = true; // それ以外の場合、チェックを入れる
                    //                }
                    //            }
                    //        }
                    //    }
                    }
                }
                return true;           
        }

        //タブコントロール、グループボックスにアクセスするため、再帰関数とする
        private static void SetControlValues(Control.ControlCollection controls, SqlDataReader reader)
        {
            foreach (Control control in controls)
            {
                if (control is TabControl tabControl)
                {
                    foreach (TabPage tabPage in tabControl.TabPages)
                    {
                        // タブコントロール内のコントロールに再帰的にアクセスする
                        SetControlValues(tabPage.Controls, reader);
                    }
                }
                else if (control is GroupBox groupBox)
                {
                    // グループボックス内のコントロールに再帰的にアクセスする
                    SetControlValues(groupBox.Controls, reader);
                }
                else
                {
                    if (!(control is TextBox) && !(control is ComboBox) && !(control is CheckBox))
                    {
                        continue;
                    }
                                        
                        string columnName = control.Name;

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string getedName = reader.GetName(i);

                        if (columnName != getedName) continue;

                        if (reader[columnName] != DBNull.Value)
                        {
                            SetControlValue(control, reader[columnName]);
                            break;
                        }
                    }
                }
            }
        }

        private static void SetControlValue(Control control, object value)
        {
            if (control is TextBox textBox)
            {
                textBox.Text = value.ToString();
            }
            else if (control is ComboBox comboBox)
            {
                comboBox.SelectedValue = value;
            }
            else if (control is CheckBox checkBox)
            {
                checkBox.Checked = Convert.ToInt32(value) != 0;
            }
           
        }






        public static void SetControls(Control control)
        {
            foreach (Control childControl in control.Controls)
            {
                SetControls(childControl); // 再帰的に子コントロールを処理

                if (childControl is TextBox textBox)
                {
                    textBox.Text = null; // TextBoxのTextプロパティをクリア
                }
                else if (childControl is ComboBox comboBox)
                {
                    comboBox.SelectedIndex = -1; // ComboBoxの選択をクリア
                }
                else if (childControl is CheckBox checkBox)
                {
                    checkBox.Checked = false; // CheckBoxのチェックを外す
                }
            }
        }

    }
}
