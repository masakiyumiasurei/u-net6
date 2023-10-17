using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace u_net
{
    public static class GC
    {
        static string ConnectionString = @"Data Source=ACCESSSRV\SQLEXPRESS,1433;Initial Catalog=Rent;User Id=sa;Password=Hbm-0855;";
        public static bool ExecSql(string sql)//sqlを実行、一つのレコードのみ更新されればtrueを、それ以外やエラーが起きたらfalseを返す※見直し必要
        {
            bool return_flag;
            try
            {
                // コネクションを生成します。
                using (var connection = new SqlConnection(ConnectionString))
                // コマンドオブジェクトを作成します。
                using (var command = connection.CreateCommand())
                {
                    // コネクションをオープンします。
                    connection.Open();
                    // データ登録のSQLを実行します。
                    command.CommandText = sql;
                    var result = command.ExecuteNonQuery();
                    // 実行された結果が1行ではない場合
                    if (result == 1)
                    {
                        return_flag = true;
                    }
                    else
                    {
                        return_flag = false;
                    }
                }
            }
            // 例外が発生した場合
            catch (System.Exception error)
            {
                // 例外の内容を表示します。
                System.Windows.Forms.MessageBox.Show(error.Message);
                return false;
            }
            return return_flag;
        }

        public static bool ExecSql_Multi(string sql)//sqlを実行、一つのレコードのみ更新されればtrueを、それ以外やエラーが起きたらfalseを返す※見直し必要
        {
            bool return_flag;
            try
            {
                // コネクションを生成します。
                using (var connection = new SqlConnection(ConnectionString))
                // コマンドオブジェクトを作成します。
                using (var command = connection.CreateCommand())
                {
                    // コネクションをオープンします。
                    connection.Open();
                    // データ登録のSQLを実行します。
                    command.CommandText = sql;
                    var result = command.ExecuteNonQuery();
                    // 実行された結果が0行ではない場合
                    if (result > 0)
                    {
                        return_flag = true;
                    }
                    else
                    {
                        return_flag = false;
                    }
                }
            }
            // 例外が発生した場合
            catch (System.Exception error)
            {
                // 例外の内容を表示します。
                System.Windows.Forms.MessageBox.Show(error.Message);
                return false;
            }
            return return_flag;
        }


        public static DataTable GetDataTable(string sql)//DataTableを返す、DataGridViewのSourceに入れればReqeryになる
        {
            SqlConnection connection = new SqlConnection();
            SqlCommand command = new SqlCommand();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            // 接続文字列を設定します。
            using (SqlConnection conn = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        dt.Clear();
                        adapter.Fill(dt);
                        return dt;
                    }
                }
            }
        }

        public static void CloseMethod()//終了ボタンを押したら確認ダイアログのあとプログラム終了する
        {
            if (System.Windows.Forms.MessageBox.Show("終了しますか？", "終了確認", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
            {
                return;
            }
            else
            {
                Application.Exit();
            }
        }

        public static int CountCheckedListBox(CheckedListBox LB)//CheckedListBox内のチェックされている項目数を返す、ItemCheck時に呼び出すこと
        {
            int count;
            if (LB.GetItemChecked(LB.SelectedIndex))
            {
                count = -1;
            }
            else
            {
                count = 1;
            }
            return (count + LB.CheckedItems.Count);
        }

        static public void DataTableToCsv(DataTable dt, string filePath, bool header)
        {
            string sp = string.Empty;
            List<int> filterIndex = new List<int>();
            string output_path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.DesktopDirectory) + "\\" + filePath;
            using (StreamWriter sw = new StreamWriter(output_path, false, Encoding.GetEncoding("Shift_JIS")))
            {
                //----------------------------------------------------------//
                // DataColumnの型から値を出力するかどうか判別します         //
                // 出力対象外となった項目は[データ]という形で出力します     //
                //----------------------------------------------------------//
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    switch (dt.Columns[i].DataType.ToString())
                    {
                        case "System.Boolean":
                        case "System.Byte":
                        case "System.Char":
                        case "System.DateTime":
                        case "System.Decimal":
                        case "System.Double":
                        case "System.Int16":
                        case "System.Int32":
                        case "System.Int64":
                        case "System.SByte":
                        case "System.Single":
                        case "System.String":
                        case "System.TimeSpan":
                        case "System.UInt16":
                        case "System.UInt32":
                        case "System.UInt64":
                            break;

                        default:
                            filterIndex.Add(i);
                            break;
                    }
                }
                //----------------------------------------------------------//
                // ヘッダーを出力します。                                   //
                //----------------------------------------------------------//
                if (header)
                {
                    foreach (DataColumn col in dt.Columns)
                    {
                        sw.Write(sp + "\"" + col.ToString().Replace("\"", "\"\"") + "\"");
                        sp = ",";
                    }
                    sw.WriteLine();
                }
                //----------------------------------------------------------//
                // 内容を出力します。                                       //
                //----------------------------------------------------------//
                foreach (DataRow row in dt.Rows)
                {
                    sp = string.Empty;
                    for (int i = 0; i < dt.Columns.Count; i++)
                    {
                        if (filterIndex.Contains(i))
                        {
                            sw.Write(sp + "\"[データ]\"");
                            sp = ",";
                        }
                        else
                        {
                            sw.Write(sp + "\"" + row[i].ToString().Replace("\"", "\"\"") + "\"");
                            sp = ",";
                        }
                    }
                    sw.WriteLine();
                }
            }
        }
    }
}
