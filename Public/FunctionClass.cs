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
    public  class FunctionClass
    {
        public static string 採番(SqlConnection connection, string 採番1桁目)
        {
            
            try
            {
                string p採番コード;
                string wkm;
                int wkm1 = DateTime.Now.Month;

                // 月に対応する文字を決定
                if (wkm1 >= 1 && wkm1 <= 9)
                {
                    wkm = wkm1.ToString("D2");
                }
                else if (wkm1 == 10)
                {
                    wkm = "X";
                }
                else if (wkm1 == 11)
                {
                    wkm = "Y";
                }
                else // 12
                {
                    wkm = "Z";
                }

                p採番コード = 採番1桁目 + DateTime.Now.Year.ToString("D1") + wkm;

                // 採番リサイクルの存在確認
                string strKey2 = "採番コード = @採番コード";
                string strSQL2 = "SELECT * FROM 採番リサイクル WHERE " + strKey2 + " ORDER BY カウンタ";

                using (SqlCommand command = new SqlCommand(strSQL2, connection))
                {
                    command.Parameters.AddWithValue("@採番コード", 採番1桁目);

                    using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                    {
                        DataTable dataTable = new DataTable();
                        adapter.Fill(dataTable);

                        if (dataTable.Rows.Count == 0)
                        {
                            // 採番リサイクルが存在しない場合
                            // 最新カウンタを取得
                            string strKey = "採番コード = @採番コード";
                            string strSQL = "SELECT * FROM 採番 WHERE " + strKey;

                            int 最新カウンタ=0;
                            using (SqlCommand cmd = new SqlCommand(strSQL, connection))
                            {
                                cmd.Parameters.AddWithValue("@採番コード", 採番1桁目);

                                using (SqlDataAdapter adapter2 = new SqlDataAdapter(cmd))
                                {
                                    DataTable dataTable2 = new DataTable();
                                    adapter2.Fill(dataTable2);

                                    if (dataTable2.Rows.Count == 0)
                                    {
                                        // 採番が存在しない場合は新規追加
                                        command.CommandText = "INSERT INTO 採番 (採番コード, 最新カウンタ) VALUES (@採番コード, @最新カウンタ)";
                                        command.Parameters.Clear();
                                        command.Parameters.AddWithValue("@採番コード", 採番1桁目);
                                        command.Parameters.AddWithValue("@最新カウンタ", 1);
                                        command.ExecuteNonQuery();
                                    }
                                    else
                                    {
                                        // 既存の採番がある場合は更新
                                        最新カウンタ = Convert.ToInt32(dataTable2.Rows[0]["最新カウンタ"]);
                                        最新カウンタ++;
                                        command.CommandText = "UPDATE 採番 SET 最新カウンタ = @最新カウンタ WHERE 採番コード = @採番コード";
                                        command.Parameters.Clear();
                                        command.Parameters.AddWithValue("@最新カウンタ", 最新カウンタ);
                                        command.Parameters.AddWithValue("@採番コード", 採番1桁目);
                                        command.ExecuteNonQuery();
                                    }

                                    // 採番を生成
                                    return 採番1桁目 + 最新カウンタ.ToString("D8"); //8桁の0埋めフォーマット
                                }
                            }
                        }
                        else
                        {
                            // 採番リサイクルが存在する場合
                            int カウンタ = Convert.ToInt32(dataTable.Rows[0]["カウンタ"]);
                            // カウンタを取得して削除
                            command.CommandText = "DELETE FROM 採番リサイクル WHERE 採番コード = @採番コード AND カウンタ = @カウンタ";
                            command.Parameters.Clear();
                            command.Parameters.AddWithValue("@採番コード", 採番1桁目);
                            command.Parameters.AddWithValue("@カウンタ", カウンタ);
                            command.ExecuteNonQuery();

                            // 採番を生成
                            return 採番1桁目 + カウンタ.ToString("D8");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("採番 - " + ex.Message);
                return null; // エラー時は null を返すか、適切なエラー処理を行う
            }
        }

        //対象コントロールの入力チェック
        public static bool IsError(Control controlObject)
        {
            try
            {
                object varValue = null; // チェックする値
                bool isError = false;

                varValue = controlObject.Text;
                // 対象コントロールがアクティブコントロールのときはTextプロパティをチェックする
                // そうでないときはValueプロパティをチェックする
                //if (controlObject == ActiveForm.ActiveControl
                //    && !(controlObject is CheckBox))
                //{
                //    varValue = controlObject.Text;
                //}
                //else
                //{
                //    varValue = controlObject.Text; // Valueプロパティを使用する場合は controlObject.Text を適切に変更してください
                //}
                               
                if (string.IsNullOrEmpty(varValue.ToString()))
                {
                    MessageBox.Show(controlObject.Name + " を入力してください。", controlObject.Name, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    isError = true;
                        return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラー: " + ex.Message);
                return false;
            }
        }

        //入力文字列のバイト数制限
        public static void LimitText(System.Windows.Forms.TextBox textBox, int limitBytes)
        {
            try
            {
                int inputBytes = Encoding.Default.GetByteCount(textBox.Text);

                if (inputBytes <= limitBytes)
                {
                    return; // 制限を超えない場合は何もしない
                }

                int selStart = textBox.SelectionStart;
                int selLength = textBox.SelectionLength;

                // 制限を超えた場合、バイト数を制限する
                string originalText = textBox.Text;
                byte[] bytes = Encoding.Default.GetBytes(originalText);

                if (selStart + selLength >= originalText.Length)
                {
                    selStart = originalText.Length - 1;
                    selLength = 0;
                }

                byte[] truncatedBytes = new byte[limitBytes];
                Array.Copy(bytes, truncatedBytes, limitBytes);

                string truncatedText = Encoding.Default.GetString(truncatedBytes);

                textBox.Text = truncatedText;
                textBox.SelectionStart = selStart;
                textBox.SelectionLength = selLength;

                MessageBox.Show("文字数制限オーバー");
            }
            catch (Exception ex)
            {
                MessageBox.Show("エラー" + ex.Message);
            }
        }

        //取得したコードをリサイクルテーブルへ戻す 登録処理を中断した時用
        public static bool ReturnCode(SqlConnection connection, string codeString)
        {
            bool success = false;
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            string strAdoptCode = string.Empty;
            long lngCounter = 0;

            // パターンに基づいてstrAdoptCodeとlngCounterを取得する処理を追加

            string query = "SELECT * FROM 採番リサイクル WHERE 採番コード = @採番コード AND カウンタ = @カウンタ";
            using (SqlCommand command = new SqlCommand(query, connection))
            {
                command.Parameters.AddWithValue("@採番コード", strAdoptCode);
                command.Parameters.AddWithValue("@カウンタ", lngCounter);

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    if (!reader.HasRows)
                    {
                        // レコードが存在しない場合は追加
                        reader.Close();
                        command.CommandText = "INSERT INTO 採番リサイクル (採番コード, カウンタ) VALUES (@採番コード, @カウンタ)";
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            success = true;
                        }
                    }
                }
            }
            return success;
        }

        public static void LockData(Form formObject, bool lockedOn, string exControlName1 = null, string exControlName2 = null)
        {
            foreach (Control control in formObject.Controls)
            {
                if (control.Enabled)
                {
                    if (control is System.Windows.Forms.TextBox)
                    {
                        if (control.Name != exControlName1 && control.Name != exControlName2)
                        {
                            ((System.Windows.Forms.TextBox)control).ReadOnly = lockedOn;
                        }
                    }
                    else if (control is System.Windows.Forms.ComboBox)
                    {
                        if (control.Name != exControlName1 && control.Name != exControlName2)
                        {
                            ((System.Windows.Forms.ComboBox)control).Enabled = !lockedOn;
                        }
                    }
                    else if (control is CheckBox)
                    {
                        if (control.Name != exControlName1 && control.Name != exControlName2)
                        {
                            ((CheckBox)control).Enabled = !lockedOn;
                        }
                    }
                }
            }
        }


        public static string GetNewCode(SqlConnection conn, string header)
        {
            string result = "";

            try
            {
                using (SqlCommand cmd = new SqlCommand("GetNewCode3", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@header", SqlDbType.NVarChar, 255)).Value = header;
                    //conn.Open();

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            result = reader.GetString(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print("GetNewCode - " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

            return result;
        }

        


        public static bool ReturnNewCode(SqlConnection conn, string header, string code)
        {
            try
            {
                string strSQL = "EXEC ReturnCode '" + header + "','" + code + "'";

                using (SqlCommand cmd = new SqlCommand(strSQL, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }

                return true;
            }
            catch (Exception ex)
            {
                Debug.Print("ReturnNewCode - " + ex.Message);
                return false;
            }
        }


        

       

        




        //省略されたコードを補完する
        // 戻り値 → 完全形のコード エラーの時はデフォルト値を返す
        public static string FormatCode(string HeaderString, string AbbreviatedCode)
        {
            try
            {
                if (string.IsNullOrEmpty(AbbreviatedCode))
                    return string.Empty;

                int intHeaderLen = HeaderString.Length;
                string strBody;


                strBody = Math.Abs(long.Parse(AbbreviatedCode.Substring(intHeaderLen))).ToString("00000000");
                return HeaderString + strBody.Substring(0, 8);
            }
            catch (Exception)
            {
                return HeaderString + "00000000";
            }
        }

        

        // 入力英小文字を英大文字に変換する
        public static int ChangeBig(int intKey)
        {
            string strCharacter;

            if (intKey < 21)
            {
                return intKey;
            }

            strCharacter = ((char)intKey).ToString();
            return (int)char.ToUpper(strCharacter[0]);
        }
    }
}
