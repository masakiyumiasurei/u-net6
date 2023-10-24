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
                            // フィールド名とコントロール名が一致しない場合、エラー処理を行うか、
                            // または無視するかはアプリケーションの要件に合わせて実装してください
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


        public static void SetTable2Form(Form formObject, SqlDataReader reader)
        {
            try
            {
                if (reader == null || reader.IsClosed) return; // データがない場合や閉じている場合は何もしない

                // データベースの列名をフォームのコントロール名と一致させておく必要があります
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    string columnName = reader.GetName(i);
                    if (formObject.Controls.ContainsKey(columnName))
                    {
                        // フォーム内のコントロールにデータを設定
                        Control control = formObject.Controls[columnName];
                        control.Text = reader[columnName].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                // エラーハンドリングを行うことをお勧めします
                Console.WriteLine("SetTable2Form Error: " + ex.Message);
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
