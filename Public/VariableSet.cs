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
using GrapeCity.Win.MultiRow;
using System.Xml.Linq;
using Microsoft.IdentityModel.Tokens;

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


        public static bool SetTable2Form(Form formObject, string sourceSQL, SqlConnection cn, string cmbname1="", string cmbname2="", string cmbname3 = "", string cmbname4 = "", string cmbname5 = "", string cmbname6 = "")
        {
            //タブコントロール、グループボックスにアクセスするため、再帰関数とする
            //cmbnameはコンボボックスのテキストに登録する。selectedvalueに存在しない値を表示させるため
                                 
            if (string.IsNullOrWhiteSpace(sourceSQL) || cn == null) return false; // クエリまたは接続が無効な場合は何もしない

            using (SqlCommand command = new SqlCommand(sourceSQL, cn))

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();

                    SetControlValues(formObject.Controls, reader, cmbname1, cmbname2,cmbname3,cmbname4,cmbname5,cmbname6);
                    
                }
            }
            return true;
        }

        //タブコントロール、グループボックスにアクセスするため、再帰関数とする
        private static void SetControlValues(Control.ControlCollection controls, SqlDataReader reader, string cmbname1 = "", string cmbname2 = "", string cmbname3 = "", string cmbname4 = "", string cmbname5 = "", string cmbname6 = "")
        {
            foreach (Control control in controls)
            {
                if (control is TabControl tabControl)
                {
                    foreach (TabPage tabPage in tabControl.TabPages)
                    {
                        // タブコントロール内のコントロールに再帰的にアクセスする
                        SetControlValues(tabPage.Controls, reader, cmbname1, cmbname2,cmbname3,cmbname4,cmbname5,cmbname6);
                    }
                }
                else if (control is GroupBox groupBox)
                {
                    // グループボックス内のコントロールに再帰的にアクセスする
                    SetControlValues(groupBox.Controls, reader, cmbname1, cmbname2, cmbname3, cmbname4, cmbname5, cmbname6);
                }
                else if (control is Panel panel)
                {
                    // パネル内のコントロールに再帰的にアクセスする
                    SetControlValues(panel.Controls, reader, cmbname1, cmbname2, cmbname3, cmbname4, cmbname5, cmbname6);
                }
                else
                {
                    if (!(control is TextBox) && !(control is ComboBox) && !(control is CheckBox) && !(control is MaskedTextBox))
                    {
                        continue;
                    }

                    string columnName = control.Name;

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string getedName = reader.GetName(i);

                        if (columnName != getedName) continue;

                        //if (reader[columnName] != DBNull.Value)
                        //{
                            SetControlValue(control, reader[columnName],cmbname1,cmbname2, cmbname3, cmbname4, cmbname5, cmbname6);
                            break;
                        //}
                    }
                }
            }
        }

        private static void SetControlValue(Control control, object value, string cmbname1 = "", string cmbname2 = "", string cmbname3 = "", string cmbname4 = "", string cmbname5 = "", string cmbname6 = "")
        {
            if (control is TextBox textBox)
            {
                textBox.Text = value.ToString();
            }
            else if (control is ComboBox comboBox)
            {
                if (value == DBNull.Value)
                {
                    comboBox.SelectedIndex = -1;
                }
                else if (control.Name == cmbname1 || control.Name == cmbname2 || control.Name == cmbname3 || control.Name == cmbname4 || control.Name == cmbname5 || control.Name == cmbname6)
                {
                    comboBox.Text = value.ToString();
                }
                else
                {
                    comboBox.SelectedValue = value;
                }
                    
            }
            else if (control is CheckBox checkBox)
            {
                if(value == DBNull.Value)
                {
                    checkBox.Checked = false;
                }
                else
                {
                    checkBox.Checked = Convert.ToInt32(value) != 0;
                }

                
            }
            else if (control is MaskedTextBox maskedTextBox)
            {
                maskedTextBox.Text = value.ToString();
            }

        }







        public static bool SetTable2FormCustom(Form formObject, string sourceSQL, SqlConnection cn,string CustomFormName, string cmbname1 = "", string cmbname2 = "", string cmbname3 = "", string cmbname4 = "", string cmbname5 = "")
        {
            //タブコントロール、グループボックスにアクセスするため、再帰関数とする
            //cmbnameはコンボボックスのテキストに登録する。selectedvalueに存在しない値を表示させるため

            if (string.IsNullOrWhiteSpace(sourceSQL) || cn == null) return false; // クエリまたは接続が無効な場合は何もしない

            using (SqlCommand command = new SqlCommand(sourceSQL, cn))

            using (SqlDataReader reader = command.ExecuteReader())
            {
                if (reader.HasRows)
                {
                    reader.Read();

                    SetControlValuesCustom(formObject.Controls, reader, CustomFormName, cmbname1, cmbname2, cmbname3, cmbname4, cmbname5);

                }
            }
            return true;
        }

        //タブコントロール、グループボックスにアクセスするため、再帰関数とする
        private static void SetControlValuesCustom(Control.ControlCollection controls, SqlDataReader reader,string CustomFormName, string cmbname1 = "", string cmbname2 = "", string cmbname3 = "", string cmbname4 = "", string cmbname5 = "")
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
                        SetControlValuesCustom(tabPage.Controls, reader, CustomFormName, cmbname1, cmbname2, cmbname3, cmbname4, cmbname5);
                    }
                }
                else if (control is GroupBox groupBox)
                {
                    // グループボックス内のコントロールに再帰的にアクセスする
                    SetControlValuesCustom(groupBox.Controls, reader, CustomFormName, cmbname1, cmbname2, cmbname3, cmbname4, cmbname5);
                }
                else if (control is Panel panel)
                {
                    // パネル内のコントロールに再帰的にアクセスする
                    SetControlValuesCustom(panel.Controls, reader, CustomFormName, cmbname1, cmbname2, cmbname3, cmbname4, cmbname5);
                }
                else
                {
                    if (!(control is TextBox) && !(control is ComboBox) && !(control is CheckBox) && !(control is MaskedTextBox))
                    {
                        continue;
                    }

                    string columnName = control.Name;

                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        string getedName = reader.GetName(i);

                        //SQLから取得した列名にパネル名を付与して一致しているか比較
                        if (columnName != CustomFormName + "_" + getedName) continue;

                        //if (reader[columnName] != DBNull.Value)
                        //{
                        SetControlValue(control, reader[getedName], cmbname1, cmbname2, cmbname3, cmbname4, cmbname5);
                        break;
                        //}
                    }
                }
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
                    comboBox.Text= null;
                }
                else if (childControl is CheckBox checkBox)
                {
                    checkBox.Checked = false; // CheckBoxのチェックを外す
                }
            }
        }

        public static bool SetTable2Details(GcMultiRow multiRow, string sourceSQL, SqlConnection cn)
        {
            if (string.IsNullOrWhiteSpace(sourceSQL) || cn == null) return false; // クエリまたは接続が無効な場合は何もしない

            using (SqlCommand command = new SqlCommand(sourceSQL, cn))
            {
                using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                {
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);
                    multiRow.DataSource = dataTable;
                }
            }

            return true;
        }







        public static void FlowControl(Form formObject, bool isReady, string controlTag = "", string exControlName1 = "", string exControlName2 = "")
        {
            //foreach (Control ctlFetch in formObject.Controls)
            //{
            //    if (ctlFetch.Name != exControlName1 && ctlFetch.Name != exControlName2 )
            //    {
            //        if (ctlFetch.Tag == controlTag)
            //        {
            //            if (ctlFetch is TextBox textBox || ctlFetch is ComboBox comboBox)
            //            {
            //                if (ctlFetch.TabStop)
            //                {
            //                    ctlFetch.Enabled = isReady;
            //                    ctlFetch.BackColor = isReady ? System.Drawing.Color.White : System.Drawing.Color.FromArgb(255, 200, 200);
            //                }
            //            }
            //            else
            //            {
            //                ctlFetch.Enabled = isReady;
            //            }
            //        }
            //        else
            //        {
            //            if (ctlFetch is TextBox textBox || ctlFetch is ComboBox comboBox)
            //            {
            //                if (ctlFetch.TabStop)
            //                {
            //                    ctlFetch.Enabled = !isReady;
            //                    ctlFetch.BackColor = isReady ? System.Drawing.Color.White : System.Drawing.Color.FromArgb(255, 200, 200);
            //                }
            //            }
            //            else
            //            {
            //                ctlFetch.Enabled = !isReady;
            //            }
            //        }
            //    }
            //}
        }

    }
}
