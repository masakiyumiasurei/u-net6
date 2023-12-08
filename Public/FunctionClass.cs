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
using System.ComponentModel;

namespace u_net.Public
{
    public class FunctionClass
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

                            int 最新カウンタ = 0;
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
        public static bool LimitText(Control control, int limitBytes)
        {
            try
            {
                if (control == null)
                {
                    return false;
                }

                if (control is TextBox textBox)
                {
                    int inputBytes = Encoding.GetEncoding("Shift_JIS").GetByteCount(textBox.Text);
                    if (inputBytes > limitBytes)
                    {
                        int selStart = textBox.SelectionStart;
                        int selLength = textBox.SelectionLength;

                        string originalText = textBox.Text;
                        byte[] bytes = Encoding.GetEncoding("Shift_JIS").GetBytes(originalText);

                        if (selStart + selLength >= originalText.Length)
                        {
                            selStart = originalText.Length - 1;
                            selLength = 0;
                        }

                        byte[] truncatedBytes = new byte[limitBytes];
                        Array.Copy(bytes, truncatedBytes, limitBytes);

                        string truncatedText = Encoding.GetEncoding("Shift_JIS").GetString(truncatedBytes);

                        textBox.Text = truncatedText;
                        textBox.SelectionStart = selStart;
                        textBox.SelectionLength = selLength;
                    }
                }
                else if (control is ComboBox comboBox)
                {
                    if (comboBox.SelectedItem != null)
                    {
                        // ComboBox の場合、SelectedItem に対して制限をかける
                        string selectedItemText = comboBox.SelectedItem.ToString();
                        int inputBytes = Encoding.GetEncoding("Shift_JIS").GetByteCount(selectedItemText);
                        if (inputBytes > limitBytes)
                        {
                            byte[] bytes = Encoding.GetEncoding("Shift_JIS").GetBytes(selectedItemText);

                            byte[] truncatedBytes = new byte[limitBytes];
                            Array.Copy(bytes, truncatedBytes, limitBytes);

                            string truncatedText = Encoding.GetEncoding("Shift_JIS").GetString(truncatedBytes);

                            comboBox.SelectedItem = truncatedText; // 選択されたアイテムを制限したテキストに変更
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("LimitText error: " + ex.Message);
                return false;
            }
        }

        //取得したコードをリサイクルテーブルへ戻す 登録処理を中断した時用
        public static bool ReturnCode(SqlConnection connection, string codeString)
        {
            bool success = false;
            string strAdoptCode = "";
            int lngCounter = 0;
            string strKey = "";
            string strSQL = "";
            string strFetch = "";
            int lngLength = 0;

            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }

            for (lngLength = 1; lngLength <= codeString.Length; lngLength++)
            {
                strFetch = codeString.Substring(lngLength - 1, 1);
                if (char.IsDigit(strFetch[0]))
                {
                    break;
                }
            }

            strAdoptCode = codeString.Substring(0, (int)lngLength - 1);
            lngCounter = int.Parse(codeString.Substring((int)lngLength - 1, 8));

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
                    else
                    {
                        success = false;
                        return success;
                    }
                }
            }
            return success;
        }


        public static void LockData(Form formObject, bool lockedOn, string exControlName1 = null, string exControlName2 = null)
        {
            foreach (Control control in formObject.Controls)
            {
                //if (control.Enabled)
                //{
                //アクセスでは、enabledのがtureのコントロールをLockするようにしていたが、
                //.netではコンボボックスとチェックボックスにはreadonlyがないため、enabledで制御する。
                //コンボボックスとチェックボックスにはenabledの状態を条件に入れない
                if (control is TextBox)
                {
                    if (control.Enabled && control.Name != exControlName1 && control.Name != exControlName2)
                    {
                        ((TextBox)control).ReadOnly = lockedOn;
                    }
                }
                else if (control is ComboBox)
                {
                    if (control.Name != exControlName1 && control.Name != exControlName2)
                    {
                        ((ComboBox)control).Enabled = !lockedOn;
                    }
                }
                else if (control is CheckBox)
                {
                    if (control.Name != exControlName1 && control.Name != exControlName2)
                    {
                        ((CheckBox)control).Enabled = !lockedOn;
                    }
                }
                //}
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

                if (!AbbreviatedCode.StartsWith(HeaderString, StringComparison.Ordinal))
                {
                    AbbreviatedCode = HeaderString + AbbreviatedCode;
                }

                string strBody;

                //整数部分だけを取り出す
                string numericPart = new string(AbbreviatedCode.Skip(intHeaderLen).ToArray());

                strBody = Math.Abs(long.Parse(numericPart)).ToString("00000000");
                return HeaderString + strBody;//.Substring(0, 8);
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



        public static void AdjustRange(Control control1, Control control2, Control inputctl)
        {
            // エラーハンドリングを追加
            //accessではactivecontrolをcontrol1と比較していたが、C#では更新イベントではactivcontrolが次のコントロールに
            //変わるため、第3引数として、現在入力したコントロールを入れることとした。
            try
            {
                Form frmOn = control1.FindForm(); // コントロールが所属するフォーム
                Control ctlCurrent = control1;    // 調整元の値のあるコントロール
                Control ctlTarget = control2;     // 調整先のコントロール

                // ２つのコントロールを含むフォームが違っていれば何もしない
                if (ctlCurrent.Parent != ctlTarget.Parent)
                    return;

                if (inputctl.Name == ctlCurrent.Name)
                {
                    ctlCurrent = control1;
                    ctlTarget = control2;
                }
                else
                {
                    ctlCurrent = control2;
                    ctlTarget = control1;
                }

                // どちらかのコントロール値が null のときは両方の値を同値にする
                if (ctlCurrent.Text == "" || ctlTarget.Text == "")
                {
                    ctlTarget.Text = ctlCurrent.Text;
                }

                // コントロール1の値 <= コントロール2の値 とする
                int comparisonResult = string.Compare(control1.Text, control2.Text);
                
                if (comparisonResult > 0)
                {
                    // control1.Text が control2.Text より大きい
                    ctlTarget.Text = ctlCurrent.Text;
                }
                                
            }
            catch (FormatException)
            {
                Console.WriteLine("Control1またはControl2のConvertに失敗しました。");
            }
            catch (Exception ex)
            {
                Console.WriteLine("AdjustRange - " + ex.GetType().Name + " : " + ex.Message);
            }
        }



        public static long AnsiInStrB(object? start = null, string? str1 = null, object? str2 = null, int? compare = 0)
        {
            long result = 0;

            if (str2 == null)
            {
                str2 = str1;
                str1 = (string)start;
                start = 1;
            }

            if (string.IsNullOrEmpty(str1) || string.IsNullOrEmpty((string)str2))
            {
                return result;
            }

            int startIndex = (int)start;
            string searchString = (string)str1;
            string targetString = (string)str2;

            if (compare == 0)
            {
                startIndex = searchString.IndexOf(targetString, startIndex - 1);
            }
            else
            {
                startIndex = searchString.IndexOf(targetString, startIndex - 1, StringComparison.Ordinal);
            }

            if (startIndex >= 0)
            {
                result = Encoding.Default.GetBytes(searchString.Substring(0, startIndex)).Length + 1;
            }

            return result;
        }






        public static int CheckHead(SqlConnection connection, string userID1, string userID2)
        {
            int result = 0; // デフォルト値は0

            try
            {
                if (string.IsNullOrEmpty(userID1) || string.IsNullOrEmpty(userID2))
                {
                    return result; // ユーザーIDが空の場合は0を返す
                }

                string query = "SELECT ユーザグループ１ FROM M社員 WHERE 社員コード = @UserID1";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@UserID1", userID1);
                    //connection.Open();
                    object userGroup = command.ExecuteScalar();

                    if (userGroup != null)
                    {
                        query = "SELECT 社員コード FROM M社員 WHERE ユーザグループ１ = @UserGroup " +
                                "AND (ユーザグループ２ = 'Director' OR ユーザグループ２ = 'Boarder' OR ユーザグループ２ = 'President')";

                        command.Parameters.Clear();
                        command.CommandText = query;
                        command.Parameters.AddWithValue("@UserGroup", userGroup);

                        object headCode = command.ExecuteScalar();

                        if (headCode != null && headCode.ToString() == userID2)
                        {
                            result = 1; // ユーザー2はユーザー1の上司
                        }
                    }

                    //connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("CheckHead - " + ex.GetType().Name + " : " + ex.Message);
            }

            return result;
        }









        public static string Dec2Str(decimal source)
        {
            string result;

            if (decimal.Truncate(source) == source)
            {
                result = source.ToString("N0"); // 整数の場合、カンマで桁区切りを含む文字列に変換
            }
            else
            {
                result = source.ToString("N1"); // 小数を含む場合、1位の小数点まで表示
            }

            return result;
        }





        public static bool DeleteGroupMember(long groupCode, long number, SqlConnection connection)
        {
            bool isDeleted = false;

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "DELETE FROM Mグループ明細 WHERE グループコード = @GroupCode AND 明細番号 = @Number";
                    cmd.Parameters.AddWithValue("@GroupCode", groupCode);
                    cmd.Parameters.AddWithValue("@Number", number);

                    //connection.Open();
                    int rowsAffected = cmd.ExecuteNonQuery();
                   // connection.Close();

                    if (rowsAffected > 0)
                    {
                        isDeleted = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DeleteGroupMember - " + ex.GetType().Name + " : " + ex.Message);
            }

            return isDeleted;
        }



        public static int DetectGroupMember(SqlConnection connection, string code)
        {
            int result = -1; // 初期値をエラーとする

            try
            {
                string query = "SELECT * FROM Mグループ明細 WHERE 文書コード = @Code";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Code", code);
                    //connection.Open();

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            result = 1; // 所属済み
                        }
                        else
                        {
                            result = 0; // 所属していない
                        }
                    }

                    //connection.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("DetectGroupMember - " + ex.GetType().Name + " : " + ex.Message);
            }

            return result;
        }


        public F_実行中 WaitForm;
        public void DoWait(string String1)
        {
            WaitForm = new F_実行中();
            WaitForm.String1 = String1;
            WaitForm.Show();
            Application.DoEvents();
        }





        public static string employeeCode(SqlConnection connection, string employeeName)
        {
            string employeeCode = string.Empty;

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    // クエリを適切なものに変更する必要があります
                    // ここでは仮に社員コードが"001"であるとしています
                    cmd.CommandText = "SELECT 社員コード FROM M社員 WHERE ユーザー名 = @EmployeeName";
                    cmd.Parameters.AddWithValue("@EmployeeName", employeeName);

                   
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            employeeCode = reader["社員コード"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EmployeeCode - " + ex.Message);
            }
            finally
            {
                //connection.Close();
            }

            return employeeCode;
        }


        public static string EmployeeName(SqlConnection connection, string employeeCode)
        {
            string employeeName = string.Empty;

            if (string.IsNullOrEmpty(employeeCode))
            {
                return employeeName;
            }

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT 氏名 FROM M社員 WHERE 社員コード = @EmployeeCode";
                    cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);


                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            employeeName = reader["氏名"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("EmployeeName - " + ex.Message);
            }
            finally
            {
                //connection.Close();
            }

            return employeeName;
        }


        public static int FirstManuCode(SqlConnection connection)
        {
            int result = -1;

            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_先頭製造コード", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter outputParameter = new SqlParameter("@製造コード", SqlDbType.Int);
                    outputParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParameter);

                    cmd.ExecuteNonQuery();

                    if (outputParameter.Value != DBNull.Value)
                    {
                        result = (int)outputParameter.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("FirstManuCode - " + ex.Message);
            }

            return result;
        }

        public static string FirstOrderCode(SqlConnection connection)
        {
            string firstOrderCode = "";


            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_先頭受注コード", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter outputParameter = new SqlParameter("@受注コード", SqlDbType.NChar,9);
                    outputParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParameter);

                    cmd.ExecuteNonQuery();

                    if (outputParameter.Value != DBNull.Value)
                    {
                        firstOrderCode = (string)outputParameter.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("FirstOrderCode - " + ex.Message);
            }

            return firstOrderCode;
        }





        public static string FormatDocumentCode(string abbreviatedCode)
        {
            // 完全形の文書コードを格納する変数
            string fullDocumentCode = "";

            try
            {
                // 引数が空文字列の場合、空文字列を返す
                if (string.IsNullOrEmpty(abbreviatedCode))
                {
                    return fullDocumentCode;
                }

                // CH_DOCUMENTとの比較を行い、必要に応じて修正
                if (!abbreviatedCode.StartsWith(CommonConstants.CH_DOCUMENT))
                {
                    abbreviatedCode = CommonConstants.CH_DOCUMENT + abbreviatedCode;
                }

                // 文字列を11文字に切り詰める
                abbreviatedCode = abbreviatedCode.Substring(0, Math.Min(abbreviatedCode.Length, 11));

                // CH_DOCUMENTと残りの部分をフォーマット
                fullDocumentCode = CommonConstants.CH_DOCUMENT + abbreviatedCode.Substring(3).PadLeft(8, '0');
            }
            catch (Exception ex)
            {
                // エラーハンドリングを行うか、エラーログを記録するなどの処理をここに追加できます
                fullDocumentCode = CommonConstants.CH_DOCUMENT + "00000000";
            }

            return fullDocumentCode;
        }




        public static string FormatOrderCode(string abbreviatedCode)
        {
            // 完全形の受注コードを格納する変数
            string fullOrderCode = "";

            try
            {
                // 引数が空文字列の場合、空文字列を返す
                if (string.IsNullOrEmpty(abbreviatedCode))
                {
                    return fullOrderCode;
                }

                // "A" との比較を行い、必要に応じて修正
                if (abbreviatedCode[0] != 'A')
                {
                    abbreviatedCode = "A" + abbreviatedCode;
                }

                // 文字列を9文字に切り詰める
                abbreviatedCode = abbreviatedCode.Substring(0, Math.Min(abbreviatedCode.Length, 9));

                // "A" と残りの部分をフォーマット
                fullOrderCode = "A" + abbreviatedCode.Substring(1).PadLeft(8, '0');
            }
            catch (Exception ex)
            {
                // エラーハンドリングを行うか、エラーログを記録するなどの処理をここに追加できます
                fullOrderCode = "A00000000";
            }

            return fullOrderCode;
        }


        public static string FormatReceivedCode(string abbreviatedCode)
        {
            // 完全形の入金コードを格納する変数
            string fullReceivedCode = "";

            try
            {
                // 引数が空文字列の場合、空文字列を返す
                if (string.IsNullOrEmpty(abbreviatedCode))
                {
                    return fullReceivedCode;
                }

                // "B" との比較を行い、必要に応じて修正
                if (abbreviatedCode[0] != 'B')
                {
                    abbreviatedCode = "B" + abbreviatedCode;
                }

                // 文字列を9文字に切り詰める
                abbreviatedCode = abbreviatedCode.Substring(0, Math.Min(abbreviatedCode.Length, 9));

                // "B" と残りの部分をフォーマット
                fullReceivedCode = "B" + abbreviatedCode.Substring(1).PadLeft(8, '0');
            }
            catch (Exception ex)
            {
                // エラーハンドリングを行うか、エラーログを記録するなどの処理をここに追加できます
                fullReceivedCode = "B00000000";
            }

            return fullReceivedCode;
        }




        public static string GetAddupMonth(SqlConnection connection, DateTime receivedDate, int supplierCloseDay)
        {
            // 集計月を格納する変数
            string addupMonth = "0";

            try
            {
                int myCloseDay = 0;  // 自社の締日

                // 自社の締日をデータベースから取得

                string strSQL = "SELECT 自社締日 FROM 会社情報";
                using (SqlCommand command = new SqlCommand(strSQL, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            myCloseDay = (int)reader.GetByte(0);
                        }
                    }
                }
                

                // 仕入先の締日が末日の場合、検収月の末日とする
                if (supplierCloseDay == 0)
                {
                    DateTime nextMonth = receivedDate.AddMonths(1);
                    DateTime lastDayOfNextMonth = new DateTime(nextMonth.Year, nextMonth.Month, 1).AddMonths(1).AddDays(-1);
                    supplierCloseDay = lastDayOfNextMonth.Day;
                }

                // 集計月を調整
                if (receivedDate.Day <= myCloseDay || (myCloseDay < receivedDate.Day && myCloseDay < supplierCloseDay))
                {
                    // 検収日が自社締日より前または仕入先締日が自社締日より後の場合、集計月＝検収月
                    addupMonth = receivedDate.Year + "/" + receivedDate.Month.ToString("00");
                }
                else
                {
                    // それ以外の場合、集計月＝検収月の翌月
                    DateTime nextMonth = receivedDate.AddMonths(1);
                    addupMonth = nextMonth.Year + "/" + nextMonth.Month.ToString("00");
                }
            }
            catch (Exception ex)
            {
                // エラーハンドリングを行うか、エラーログを記録するなどの処理をここに追加できます
                Console.WriteLine("GetAddupMonth - " + ex.Message);
            }

            return addupMonth;
        }



        public static string GetCode(SqlConnection connection, string transactString, int editionNumber)
        {
            // パラメータを元にコードの採番を行う関数

            // 新規コードを格納する変数
            string newCode = "";

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    //connection.Open();

                    // 採番リサイクルテーブルからコードを取得
                    string recycleQuery = "SELECT TOP 1 * FROM 採番リサイクル WHERE 採番コード = @TransactString ORDER BY カウンタ";
                    cmd.CommandText = recycleQuery;
                    cmd.Parameters.AddWithValue("@TransactString", transactString);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // リサイクルテーブルからコードを取得し、同時に削除
                            newCode = transactString + reader["カウンタ"].ToString().PadLeft(8, '0') + "01";
                            reader.Close();

                            // リサイクルテーブルを更新
                            string recycleUpdateQuery = "DELETE FROM 採番リサイクル WHERE 採番コード = @TransactString AND カウンタ = @Counter";
                            cmd.CommandText = recycleUpdateQuery;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@TransactString", transactString);
                            cmd.Parameters.AddWithValue("@Counter", reader["カウンタ"]);
                            cmd.ExecuteNonQuery();
                        }
                        else
                        {
                            reader.Close();

                            // 採番テーブルから最新カウンタを取得
                            string query = "SELECT * FROM 採番 WHERE 採番コード = @TransactString";
                            cmd.CommandText = query;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@TransactString", transactString);

                            using (SqlDataReader innerReader = cmd.ExecuteReader())
                            {
                                if (innerReader.Read())
                                {
                                    // 既存のデータがある場合、最新カウンタを更新
                                    int counter = (int)innerReader["最新カウンタ"] + 1;
                                    innerReader.Close();

                                    string updateQuery = "UPDATE 採番 SET 最新カウンタ = @Counter WHERE 採番コード = @TransactString";
                                    cmd.CommandText = updateQuery;
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@Counter", counter);
                                    cmd.Parameters.AddWithValue("@TransactString", transactString);
                                    cmd.ExecuteNonQuery();

                                    newCode = transactString + counter.ToString().PadLeft(8, '0') + editionNumber.ToString("00");
                                }
                                else
                                {
                                    innerReader.Close();

                                    // データが存在しない場合、新規データを追加
                                    string insertQuery = "INSERT INTO 採番 (採番コード, 最新カウンタ) VALUES (@TransactString, 1)";
                                    cmd.CommandText = insertQuery;
                                    cmd.Parameters.Clear();
                                    cmd.Parameters.AddWithValue("@TransactString", transactString);
                                    cmd.ExecuteNonQuery();

                                    newCode = transactString + "00000001" + editionNumber.ToString("00");
                                }
                            }
                        }
                    }

                    cmd.Parameters.Clear();
                    //connection.Close();
                }
            }
            catch (Exception ex)
            {
                // エラーハンドリングを行うか、エラーログを記録するなどの処理をここに追加できます
                Console.WriteLine("GetCode - " + ex.Message);
            }

            return newCode;
        }




        public static string GetCustomerName(SqlConnection connection, string customerCode)
        {
            // 顧客コードから顧客名を取得する関数

            // 顧客名を格納する変数
            string customerName = "";

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    //connection.Open();

                    // SQLクエリを構築
                    string query = "SELECT * FROM M顧客 WHERE 顧客コード = @CustomerCode";
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@CustomerCode", customerCode);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // レコードが存在する場合、顧客名を取得
                            string customerName1 = reader["顧客名"].ToString();
                            string customerName2 = reader["顧客名2"].ToString();

                            if (!string.IsNullOrEmpty(customerName2))
                            {
                                // 顧客名2が存在する場合、顧客名1と結合
                                customerName = customerName1 + " " + customerName2;
                            }
                            else
                            {
                                // 顧客名2が存在しない場合、顧客名1のみを使用
                                customerName = customerName1;
                            }
                        }
                    }
                }

                //connection.Close();
            }
            catch (Exception ex)
            {
                // エラーハンドリングを行うか、エラーログを記録するなどの処理をここに追加できます
                Console.WriteLine("GetCustomerName - " + ex.Message);
            }

            return customerName;
        }



        public static string GetHeadCode(SqlConnection connection, string userCode)
        {
            // ユーザーの長の社員コードを取得する関数

            // 長の社員コードを格納する変数
            string headCode = "000";

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    //connection.Open();

                    // 社員コードの存在確認
                    string query = "SELECT 部 FROM M社員 WHERE 社員コード = @UserCode";
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@UserCode", userCode);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // UserID1の所属グループを取得
                            string group = reader["部"].ToString();
                            reader.Close();

                            // 所属グループの長の社員コードを取得
                            query = "SELECT 社員コード FROM M社員 " +
                                    "WHERE 部 = @Group " +
                                    "AND (ユーザグループ２ = 'Director' OR " +
                                    "ユーザグループ２ = 'Boarder' OR " +
                                    "ユーザグループ２ = 'President')";
                            cmd.CommandText = query;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@Group", group);

                            using (SqlDataReader innerReader = cmd.ExecuteReader())
                            {
                                if (innerReader.Read())
                                {
                                    // 長の社員コードを取得
                                    headCode = innerReader["社員コード"].ToString();
                                }
                                innerReader.Close();
                            }
                        }
                    }
                }

                //connection.Close();
            }
            catch (Exception ex)
            {
                // エラーハンドリングを行うか、エラーログを記録するなどの処理をここに追加できます
                Console.WriteLine("GetHeadCode - " + ex.Message);
            }

            return headCode;
        }



        public static string GetHeadCode2(SqlConnection connection, string section)
        {
            // 部の長のユーザーコードを取得する関数

            // 部の長のユーザーコードを格納する変数
            string headCode = "";

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    //connection.Open();

                    // SQLクエリを構築
                    string query = "SELECT 社員コード FROM M社員 WHERE 部 = @Section " +
                                   "AND ユーザグループ２ IN ('President', 'Boarder', 'Director')";
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@Section", section);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // レコードが存在する場合、部の長のユーザーコードを取得
                            headCode = reader["社員コード"].ToString();
                        }
                    }
                }

                //connection.Close();
            }
            catch (Exception ex)
            {
                // エラーハンドリングを行うか、エラーログを記録するなどの処理をここに追加できます
                Console.WriteLine("GetHeadCode2 - " + ex.Message);
            }

            return headCode;
        }






        public static DateTime GetServerDate(SqlConnection connection)
        {
            // サーバーの現在日付（時刻）を取得する関数

            // サーバーの現在日付（時刻）を格納する変数
            DateTime serverDate = DateTime.MinValue;

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    //connection.Open();

                    // SQLクエリを構築
                    string query = "SELECT GETDATE() AS 現在日付";
                    cmd.CommandText = query;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // レコードが存在する場合、サーバーの現在日付（時刻）を取得
                            serverDate = (DateTime)reader["現在日付"];
                        }
                    }
                }

                //connection.Close();
            }
            catch (Exception ex)
            {
                // エラーハンドリングを行うか、エラーログを記録するなどの処理をここに追加できます
                Console.WriteLine("GetServerDate - " + ex.Message);
            }

            return serverDate;
        }




        public static string GetDepartment(SqlConnection connection, string employeeCode)
        {
            // 社員コードから所属している部を取得する関数

            // 所属している部を格納する変数
            string department = "";

            try
            {
                if (string.IsNullOrEmpty(employeeCode))
                {
                    return department;
                }

                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    //connection.Open();

                    // 社員コードの存在確認
                    string query = "SELECT 部 FROM M社員 WHERE 社員コード = @EmployeeCode";
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@EmployeeCode", employeeCode);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // レコードが存在する場合、所属している部を取得
                            department = reader["部"].ToString();
                        }
                    }
                }

                //connection.Close();
            }
            catch (Exception ex)
            {
                // エラーハンドリングを行うか、エラーログを記録するなどの処理をここに追加できます
                Console.WriteLine("GetDepartment - " + ex.Message);
            }

            return department;
        }



        public static string GetMakerName(SqlConnection connection, string makerCode)
        {
            // メーカーコードからメーカー名を取得する関数

            // メーカー名を格納する変数
            string makerName = "";

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    //connection.Open();

                    // メーカーコードの存在確認
                    string query = "SELECT メーカー名 FROM Mメーカー WHERE メーカーコード = @MakerCode";
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@MakerCode", makerCode);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // レコードが存在する場合、メーカー名を取得
                            makerName = reader["メーカー名"].ToString();
                        }
                    }
                }

                //connection.Close();
            }
            catch (Exception ex)
            {
                // エラーハンドリングを行うか、エラーログを記録するなどの処理をここに追加できます
                Console.WriteLine("GetMakerName - " + ex.Message);
            }

            return makerName;
        }



        public static string GetMakerShortName(SqlConnection connection, string makerCode)
        {
            // メーカーコードからメーカー省略名を取得する関数

            // メーカー省略名を格納する変数
            string makerShortName = "";

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    //connection.Open();

                    // メーカーコードの存在確認
                    string query = "SELECT メーカー省略名 FROM Mメーカー WHERE メーカーコード = @MakerCode";
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@MakerCode", makerCode);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // レコードが存在する場合、メーカー省略名を取得
                            makerShortName = reader["メーカー省略名"].ToString();
                        }
                    }
                }

                //connection.Close();
            }
            catch (Exception ex)
            {
                // エラーハンドリングを行うか、エラーログを記録するなどの処理をここに追加できます
                Console.WriteLine("GetMakerShortName - " + ex.Message);
            }

            return makerShortName;
        }


        public static string GetLastVersion(SqlConnection connection)
        {
            // システムがリリースされた最終バージョンを取得する関数

            // 最終バージョンを格納する変数
            string lastVersion = "";

            try
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = connection;
                    //connection.Open();

                    // 最終バージョンの取得クエリ
                    string query = "SELECT ISNULL(MAX(バージョン番号), '0.000') AS 最終バージョン FROM Tシステム更新履歴";
                    cmd.CommandText = query;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            // レコードが存在する場合、最終バージョンを取得
                            lastVersion = reader["最終バージョン"].ToString();
                        }
                    }
                }

                //connection.Close();
            }
            catch (Exception ex)
            {
                // エラーハンドリングを行うか、エラーログを記録するなどの処理をここに追加できます
                Console.WriteLine("GetLastVersion - " + ex.Message);
            }

            return lastVersion;
        }



        public static float GetNewVersion(SqlConnection connection)
        {
            float newVersion = 0.0f;

            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_最新バージョン", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter outputParameter = new SqlParameter("@最新バージョン数", SqlDbType.Float);
                    outputParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParameter);

                    cmd.ExecuteNonQuery();

                    if (outputParameter.Value != DBNull.Value)
                    {
                        newVersion = (float)Convert.ToDouble(outputParameter.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetNewVersion - " + ex.Message);
            }

            return newVersion;
        }



        public static DateTime GetPayDay(SqlConnection connection, DateTime targetDate)
        {
            DateTime result = DateTime.MinValue;

            try
            {
                int intClosedDay = 0;   // 自社の締日
                int intPayDiff = 0;     // 支払期間
                int intPayDay = 0;      // 支払期日

                using (SqlCommand cmd = new SqlCommand("SELECT 自社締日, 支払期間, 支払期日 FROM 会社情報", connection))
                {

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            intClosedDay = Convert.ToInt32(reader["自社締日"]);
                            intPayDiff = Convert.ToInt32(reader["支払期間"]);
                            intPayDay = Convert.ToInt32(reader["支払期日"]);
                        }
                    }
                }

                int intAddition = 0; // 締日後の加算月数
                DateTime dat1;

                if (targetDate.Day <= intClosedDay)
                {
                    dat1 = targetDate.AddMonths(intPayDiff);
                }
                else
                {
                    dat1 = targetDate.AddMonths(intPayDiff + 1);
                }

                if (intPayDay == 0)
                {
                    result = new DateTime(dat1.AddMonths(1).Year, dat1.AddMonths(1).Month, 1).AddDays(-1);
                }
                else
                {
                    result = new DateTime(dat1.Year, dat1.Month, intPayDay);
                }


            }
            catch (Exception ex)
            {
                Console.WriteLine("GetPayDay - " + ex.Message);
            }

            return result;
        }



        public static long GetRecordCount(SqlConnection connection, string recordSource, string where = "")
        {
            // 指定されたレコードソースのレコード数を取得する関数
            // where句は省略可能

            long recordCount = 0;

            try
            {
                string sql;
                if (recordSource.ToUpper().StartsWith("SELECT"))
                {
                    sql = recordSource;
                }
                else
                {
                    sql = "SELECT COUNT(*) FROM " + recordSource;
                }

                if (!string.IsNullOrWhiteSpace(where))
                {
                    sql += " WHERE " + where;
                }

                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            recordCount = (long)reader.GetInt32(0);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // エラーハンドリングを行うか、エラーログを記録するなどの処理をここに追加できます
                Console.WriteLine("GetRecordCount - " + ex.Message);
            }

            return recordCount;
        }



        public static string GetServerName(SqlConnection connection)
        {
            // 接続オブジェクトからサーバー名を取得する関数

            string serverName = "";

            try
            {
                if (connection != null && !string.IsNullOrEmpty(connection.ConnectionString))
                {
                    string connectionString = connection.ConnectionString;
                    int startIndex = connectionString.IndexOf("Data Source=");

                    if (startIndex >= 0)
                    {
                        startIndex += 12;
                        int endIndex = connectionString.IndexOf(";", startIndex);
                        if (endIndex == -1)
                        {
                            endIndex = connectionString.Length;
                        }

                        serverName = connectionString.Substring(startIndex, endIndex - startIndex);
                    }
                }
            }
            catch (Exception ex)
            {
                // エラーハンドリングを行うか、エラーログを記録するなどの処理をここに追加できます
                Console.WriteLine("GetServerName - " + ex.Message);
            }

            return serverName;
        }



        public static string GetSignature(SqlConnection connection,string userCode)
        {
            // ユーザーの署名を取得する関数

            string strSignature = "";

            try
            {
      

                    string strSQL = "SELECT * FROM V社員_署名 WHERE 社員コード=@userCode";
                    using (SqlCommand cmd = new SqlCommand(strSQL, connection))
                    {
                        cmd.Parameters.AddWithValue("@userCode", userCode);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                strSignature = "************************************" + Environment.NewLine +
                                    "ユアイニクス株式会社 " + reader["営業所名"].ToString() + Environment.NewLine +
                                    "〒" + reader["郵便番号"].ToString() + Environment.NewLine +
                                    reader["住所１"].ToString() + Environment.NewLine +
                                    reader["住所２"].ToString() + Environment.NewLine +
                                    "TEL: " + reader["電話番号"].ToString() + Environment.NewLine +
                                    "FAX: " + reader["ファックス番号"].ToString() + Environment.NewLine +
                                    "http://www.uinics.co.jp/" + Environment.NewLine +
                                    reader["部"].ToString() + " " + reader["チーム名"].ToString() + " " + reader["氏名"].ToString() + Environment.NewLine +
                                    reader["電子メールアドレス"].ToString() + Environment.NewLine +
                                    "************************************";
                            }
                        }
                    }
                
            }
            catch (Exception ex)
            {
                // エラーハンドリングを行うか、エラーログを記録するなどの処理をここに追加できます
                Console.WriteLine("GetSignature - " + ex.Message);
            }

            return strSignature;
        }




        public static int GetStock(SqlConnection connection,DateTime appointedDate, string codeString)
        {
            // 理論在庫を取得する関数

            int stock = 0;

            try
            {
      
                    using (SqlCommand cmd = new SqlCommand("SP理論在庫出力", connection))
                    {
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@dteDay", SqlDbType.DateTime)).Value = appointedDate;
                        cmd.Parameters.Add(new SqlParameter("@strCode", SqlDbType.VarChar, 255)).Value = codeString;
                        cmd.Parameters.Add(new SqlParameter("@intStock", SqlDbType.BigInt)).Direction = ParameterDirection.Output;

                        cmd.ExecuteNonQuery();

                        stock = (int)cmd.Parameters["@Stock"].Value;
                    }
                
            }
            catch (Exception ex)
            {
                // エラーハンドリングを行うか、エラーログを記録するなどの処理をここに追加できます
                Console.WriteLine("GetStock - " + ex.Message);
            }

            return stock;
        }



        public static string GetSupplierName(SqlConnection connection,string supplierCode)
        {
            // 仕入先コードから仕入先名を取得する関数

            string supplierName = "";

            try
            {               
                    string strSQL = "SELECT * FROM M仕入先 WHERE 仕入先コード = @SupplierCode";
                    using (SqlCommand cmd = new SqlCommand(strSQL, connection))
                    {
                        cmd.Parameters.Add(new SqlParameter("@SupplierCode", SqlDbType.VarChar, 255)).Value = supplierCode;

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string supplierName1 = reader["仕入先名"].ToString();
                                string supplierName2 = reader["仕入先名2"].ToString();

                                if (!string.IsNullOrEmpty(supplierName2))
                                {
                                    supplierName = $"{supplierName1} {supplierName2}";
                                }
                                else
                                {
                                    supplierName = supplierName1;
                                }
                            }
                        }
                    }
                
            }
            catch (Exception ex)
            {
                // エラーハンドリングを行うか、エラーログを記録するなどの処理をここに追加できます
                Console.WriteLine("GetSupplierName - " + ex.Message);
            }

            return supplierName;
        }




        public static decimal GetTaxRate(SqlConnection connection, DateTime targetDate)
        {
            // 指定日の消費税率を取得する関数

            decimal taxRate = 0;

            try
            {
                string strSQL = "SELECT dbo.ConsumptionTaxRate(@TargetDate) AS TaxRate";
                using (SqlCommand cmd = new SqlCommand(strSQL, connection))
                {
                    cmd.Parameters.Add(new SqlParameter("@TargetDate", SqlDbType.Date)).Value = targetDate;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (!reader.IsDBNull(reader.GetOrdinal("TaxRate")))
                            {
                                taxRate = reader.GetDecimal(reader.GetOrdinal("TaxRate"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // エラーハンドリングを行うか、エラーログを記録するなどの処理をここに追加できます
                Console.WriteLine("GetTaxRate - " + ex.Message);
            }

            return taxRate;
        }



        public static string GetUserName(SqlConnection connection, string userCode)
        {
            string userName = "";

            try
            {
                string strSQL = "SELECT ユーザー名 FROM M社員 WHERE 社員コード = @UserCode";
                using (SqlCommand cmd = new SqlCommand(strSQL, connection))
                {
                    cmd.Parameters.Add(new SqlParameter("@UserCode", SqlDbType.VarChar, 50)).Value = userCode;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (!reader.IsDBNull(reader.GetOrdinal("ユーザー名")))
                            {
                                userName = reader.GetString(reader.GetOrdinal("ユーザー名"));
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetUserName - " + ex.Message);
                // エラーハンドリングを行うか、エラーログを記録するなどの処理をここに追加できます
            }

            return userName;
        }


        public static string GetUserFullName(SqlConnection connection, string userCode)
        {
            string userFullName = "";

            try
            {
                string strSQL = "SELECT 氏名 FROM M社員 WHERE 社員コード = @UserCode";
                using (SqlCommand cmd = new SqlCommand(strSQL, connection))
                {
                    cmd.Parameters.Add(new SqlParameter("@UserCode", SqlDbType.VarChar, 50)).Value = userCode;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            if (!reader.IsDBNull(0))
                            {
                                userFullName = reader.GetString(0);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("GetUserFullName - " + ex.Message);
                // エラーハンドリングを行うか、エラーログを記録するなどの処理をここに追加できます
            }

            return userFullName;
        }



        public static int IsAbsence(SqlConnection connection, string employeeCode)
        {
            int isAbsence = 0;

            try
            {
                string strSQL = "SELECT * FROM T不在社員 WHERE 社員コード = @EmployeeCode";
                using (SqlCommand cmd = new SqlCommand(strSQL, connection))
                {
                    cmd.Parameters.Add(new SqlParameter("@EmployeeCode", SqlDbType.VarChar, 50)).Value = employeeCode;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isAbsence = -1; // 不在の場合
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("IsAbsence - " + ex.Message);
                // エラーハンドリングを行うか、エラーログを記録するなどの処理をここに追加できます
            }

            return isAbsence;
        }


        public static bool IsClosedPayment(SqlConnection connection, DateTime payMonth)
        {
            bool isClosedPayment = false;

            try
            {
                string strSQL = "SELECT TOP 1 支払年月 FROM T振込繰越残高 WHERE 支払年月 = @PayMonth";
                using (SqlCommand cmd = new SqlCommand(strSQL, connection))
                {
                    cmd.Parameters.Add(new SqlParameter("@PayMonth", SqlDbType.Date)).Value = payMonth;

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isClosedPayment = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("IsClosedPayment - " + ex.Message);
                // エラーハンドリングを行うか、エラーログを記録するなどの処理をここに追加できます
            }

            return isClosedPayment;
        }





        public static bool IsInventory(SqlConnection connection)
        {
            bool isInventory = false;

            try
            {
                string strSQL = "SELECT TOP 1 1 FROM T棚卸 WHERE 作業終了日 IS NULL";
                using (SqlCommand cmd = new SqlCommand(strSQL, connection))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            isInventory = true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("IsInventory - " + ex.Message);
                // エラーハンドリングを行うか、エラーログを記録するなどの処理をここに追加できます
            }

            return isInventory;
        }



        public static bool IsLimit(object targetValue, int limitLength, bool permitZero, string controlName)
        {
            try
            {
                if (targetValue == null || targetValue == DBNull.Value || targetValue.ToString() == "")
                {
                    if (permitZero)
                    {
                        return true; // ノーチェック
                    }
                    else
                    {
                        string strMsg = "未入力欄があります。" + Environment.NewLine + Environment.NewLine + controlName;
                        MessageBox.Show(strMsg, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        
                        return false; // エラー
                    }
                }

                string strValue = targetValue.ToString();
                if (strValue.Contains("\""))
                {
                    string strMsg = "項目中に \" を含むことはできません。" + Environment.NewLine + Environment.NewLine + controlName;
                    MessageBox.Show(strMsg, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false; // エラー
                }

                if (strValue.Contains("'"))
                {
                    string strMsg = "項目中に ' を含むことはできません。" + Environment.NewLine + Environment.NewLine + controlName;
                    MessageBox.Show(strMsg, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false; // エラー
                }

                if (strValue.Length > limitLength)
                {
                    string strMsg = "項目の長さ(" + strValue.Length + ")が制限(" + limitLength + ")を越えています。" + Environment.NewLine + Environment.NewLine + controlName;
                    MessageBox.Show(strMsg, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false; // エラー
                }

                return true; // チェックOK
            }
            catch (Exception ex)
            {
                Console.WriteLine("IsLimit - " + ex.Message);
                // エラーハンドリングを行うか、エラーログを記録するなどの処理をここに追加できます
                return false;
            }
        }




        public static bool IsLimit_N(object targetValue, int totalLength, int decimalLength, string controlName)
        {
            try
            {
                string msgString;
                int int整数;
                int int点位置;
                decimal[] 範囲 = new decimal[15];

                for (int i = 0; i < 14; i++)
                {
                    範囲[i] = (decimal)Math.Pow(10, i + 1);
                }

                bool isNumeric = decimal.TryParse(targetValue.ToString(), out decimal numericValue);

                if (!isNumeric)
                {
                    msgString = "数値を入力してください。";
                    MessageBox.Show(msgString, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                int整数 = totalLength - decimalLength;

                if (Math.Abs(numericValue) >= 範囲[int整数 - 1])
                {
                    msgString = "整数部が大きすぎます。（桁制限= " + int整数 + ") " + controlName;
                    MessageBox.Show(msgString, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }

                int点位置 = targetValue.ToString().IndexOf('.');
                if (int点位置 != -1)
                {
                    if (decimalLength < targetValue.ToString().Length - int点位置 - 1)
                    {
                        msgString = "小数部が大きすぎます。（桁制限= " + decimalLength + ") " + controlName;
                        MessageBox.Show(msgString, "エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("IsLimit_N - " + ex.Message);
                // エラーハンドリングを行うか、エラーログを記録するなどの処理をここに追加できます
                return false;
            }
        }


        public static int LastManuCode(SqlConnection connection)
        {
            int lastManuCode = -1;

            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_最終製造コード", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter outputParameter = new SqlParameter("@製造コード", SqlDbType.Int);
                    outputParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParameter);

                    cmd.ExecuteNonQuery();

                    if (outputParameter.Value != DBNull.Value)
                    {
                        lastManuCode = (int)outputParameter.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("LastManuCode - " + ex.Message);
            }

            return lastManuCode;
        }


        public static string LastOrderCode(SqlConnection connection)
        {

            string lastOrderCode = "";

            try
            {
                using (SqlCommand cmd = new SqlCommand("usp_最終受注コード", connection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    SqlParameter outputParameter = new SqlParameter("@受注コード", SqlDbType.NChar, 9);
                    outputParameter.Direction = ParameterDirection.Output;
                    cmd.Parameters.Add(outputParameter);

                    cmd.ExecuteNonQuery();

                    if (outputParameter.Value != DBNull.Value)
                    {
                        lastOrderCode = (string)outputParameter.Value;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("LastOrderCode - " + ex.Message);
            }

            return lastOrderCode;
        }



        public static void LimitFieldSize(TextBox textBox, int keyAscii, int maxLength)
        {
            // 印刷可能でない文字が入力された場合は終了
            if (keyAscii < 32)
                return;

            // 既に入力されている文字を範囲選択してから書き換えた場合は終了
            if (textBox.SelectionLength > 0)
                return;

            // 入力された文字の長さを取得
            int currentLength = textBox.Text.Length + 1;

            if (currentLength < textBox.SelectionStart + 1)
                currentLength = textBox.SelectionStart + 1;

            // 文字列の長さが指定した文字数よりも大きい場合は警告音を鳴らす
            if (currentLength > maxLength)
            {
                System.Media.SystemSounds.Beep.Play();
                keyAscii = 0; // キー入力を無効にする
            }
        }




        public long TextRows(string text, long lineB)
        {
            long lngRows = 0;
            long lngi = 0; // 処理対象の先頭位置
            long lngs = 0; // 改行発見位置

            lngi = 0;

            // 先頭位置が総文字数に達するまで繰り返す
            while (lngi < text.Length)
            {
                // 改行位置を取得する
                lngs = text.IndexOf("\r\n", (int)lngi);

                if (lngs >= 0)
                {
                    // 改行が見つかった場合
                    // １文の行数を加算
                    lngRows += (long)(Encoding.Default.GetByteCount(text.Substring((int)lngi, (int)(lngs - lngi + 2))) / (lineB + 1));
                    // 改行による行数を加算
                    lngRows++;
                    // 先頭位置を改行分（文字数：2）進める
                    lngi = lngs + 2;
                }
                else
                {
                    // 改行が見つからなかった場合（最後の１文？）
                    // １文の行数を加算
                    lngRows += (long)(Encoding.Default.GetByteCount(text.Substring((int)lngi)) / (lineB + 1)) + 1;
                    // 先頭位置を残りの文字数分進める
                    // 進めなくてもループを抜けるとダメなのか？
                    lngi += text.Substring((int)lngi).Length;
                }
            }

            return lngRows;
        }



        public object Zn(object value, object? valueIfZero = null)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()) ||
                (value is int intValue && intValue == 0 && !value.ToString().Equals("00000000")))
            {
                if (valueIfZero == null)
                {
                    return DBNull.Value;
                }
                else
                {
                    return valueIfZero;
                }
            }
            else
            {
                return value;
            }
        }





        public object ZnSQL(object value, object? valueIfZero = null)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()) || Convert.IsDBNull(value))
            {
                if (valueIfZero == null)
                {
                    return "Null";
                }
                else
                {
                    return valueIfZero;
                }
            }
            else
            {
                return "'" + value.ToString() + "'";
            }
        }



        public static bool Recycle(SqlConnection connection, string codeString)
        {
            try
            {
                //if (connection.State != ConnectionState.Open)
                //{
                //    connection.Open();
                //}

                string adoptCode = "";
                int counter = 0;

                // 採番コード部とカウンタ部を分割
                for (int i = 0; i < codeString.Length; i++)
                {
                    if (codeString[i] >= '0' && codeString[i] <= '9')
                    {
                        adoptCode = codeString.Substring(0, i);
                        counter = int.Parse(codeString.Substring(i));
                        break;
                    }
                }

                string selectQuery = "SELECT * FROM 採番リサイクル WHERE 採番コード = @AdoptCode AND カウンタ = @Counter";

                using (SqlCommand cmd = new SqlCommand(selectQuery, connection))
                {
                    cmd.Parameters.AddWithValue("@AdoptCode", adoptCode);
                    cmd.Parameters.AddWithValue("@Counter", counter);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            // レコードが見つからない場合、新たに追加
                            reader.Close();

                            string insertQuery = "INSERT INTO 採番リサイクル (採番コード, カウンタ) VALUES (@AdoptCode, @Counter)";
                            using (SqlCommand insertCmd = new SqlCommand(insertQuery, connection))
                            {
                                insertCmd.Parameters.AddWithValue("@AdoptCode", adoptCode);
                                insertCmd.Parameters.AddWithValue("@Counter", counter);
                                insertCmd.ExecuteNonQuery();
                            }

                            return true;
                        }
                        else
                        {
                            // レコードが存在する場合はエラー
                            return false;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Recycle - " + ex.Message);
                return false;
            }
        }



        public void RunApplication()
        {
            // 外部アプリケーションを実行する
            // 試験的に作成

            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo("CALC.EXE");
                startInfo.WindowStyle = ProcessWindowStyle.Normal; // ウィンドウを通常サイズで開く

                Process process = Process.Start(startInfo);

                if (process != null)
                {
                    process.WaitForInputIdle(); // 外部アプリケーションが入力待ち状態になるまで待つ
                                                // ここでアクティブ化のコードを追加
                    NativeMethods.SetForegroundWindow(process.MainWindowHandle);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("RunApplication - " + ex.Message);
            }
        }

        // SetForegroundWindowを呼び出すためのクラス
        internal class NativeMethods
        {
            [System.Runtime.InteropServices.DllImport("user32.dll")]
            public static extern bool SetForegroundWindow(IntPtr hWnd);
        }


        public string 社員コード(SqlConnection Cnn, string strユーザー名)
        {
            string 社員コード = "000"; // デフォルトの社員コード

            try
            {
                string strMsg = string.Empty;
                string strKey = "ユーザー名 = @ユーザー名";

                // 社員コードの存在確認
                using (SqlCommand cmd = new SqlCommand("SELECT 社員コード FROM M社員 WHERE " + strKey, Cnn))
                {
                    cmd.Parameters.AddWithValue("@ユーザー名", strユーザー名);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            社員コード = reader["社員コード"].ToString();
                        }
                        else
                        {
                            strMsg = "システム管理者に連絡してください。" + Environment.NewLine +
                                     "システムにユーザー名 ： " + strユーザー名 + " が存在しません。";
                            Console.WriteLine(strMsg);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("エラー: " + ex.Message);
            }

            return 社員コード;
        }




        public string 社員名(SqlConnection Cnn, string str社員コード)
        {
            string 社員名 = string.Empty;

            try
            {
                string strMsg = string.Empty;
                string strKey = "社員コード = @社員コード";

                // 社員コードの存在確認
                using (SqlCommand cmd = new SqlCommand("SELECT 氏名 FROM M社員 WHERE " + strKey, Cnn))
                {
                    cmd.Parameters.AddWithValue("@社員コード", str社員コード);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            社員名 = reader["氏名"].ToString();
                        }
                        else
                        {
                            strMsg = "システム管理者に連絡してください。" + Environment.NewLine +
                                     "システムに社員コード : " + str社員コード + " が存在しません。";
                            Console.WriteLine(strMsg);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("エラー: " + ex.Message);
            }

            return 社員名;
        }




        public DateTime 締日確認(SqlConnection Cnn, string str締日種類)
        {
            DateTime 締日 = DateTime.MinValue;

            try
            {
                string strMsg = string.Empty;
                string strKey = "締日種類 = @締日種類";

                // 締日種類の存在確認
                using (SqlCommand cmd = new SqlCommand("SELECT 締日 FROM 締日 WHERE " + strKey, Cnn))
                {
                    cmd.Parameters.AddWithValue("@締日種類", str締日種類);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            締日 = Convert.ToDateTime(reader["締日"]);
                        }
                        else
                        {
                            strMsg = "システム管理者に連絡してください。" + Environment.NewLine +
                                     "[締日]に締日種類 = " + str締日種類 + " が存在しません。";
                            Console.WriteLine(strMsg);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("エラー: " + ex.Message);
            }

            return 締日;
        }




        public bool CByteChar(string 対象文字)
        {
            if (string.IsNullOrEmpty(対象文字))
            {
                return false;
            }

            対象文字 = Microsoft.VisualBasic.Strings.StrConv(対象文字, Microsoft.VisualBasic.VbStrConv.Wide, 0);

            if (string.IsNullOrEmpty(対象文字))
            {
                return false;
            }

            long sl = 対象文字.Length;
            if (sl == 0)
            {
                return false;
            }

            for (int i = 0; i < sl; i++)
            {
                int cd = (int)対象文字[i];
                if ((cd < 0x8340) || (cd == 0x837F) || (cd > 0x8396))
                {
                    return false;
                }
            }

            return true;
        }




        public string ChangeKatakana(string strTextData)
        {
            string strRet = "";
            string strWrkKana = "";
            string strTmpChar = "";
            int intTmpAsc = 0;
            bool blnKanaflg = false;

            for (int iintLoop = 0; iintLoop < strTextData.Length; iintLoop++)
            {
                strTmpChar = strTextData.Substring(iintLoop, 1);
                intTmpAsc = (int)strTmpChar[0];

                if (intTmpAsc >= 161 && intTmpAsc <= 223)
                {
                    strWrkKana += strTmpChar;
                    blnKanaflg = true;
                }
                else
                {
                    if (!blnKanaflg)
                    {
                        strRet += strTmpChar;
                    }
                    else
                    {
                        strRet += Microsoft.VisualBasic.Strings.StrConv(strWrkKana, Microsoft.VisualBasic.VbStrConv.Wide, 0) + strTmpChar;
                        strWrkKana = "";
                        blnKanaflg = false;
                    }
                }
            }

            if (blnKanaflg)
            {
                strRet += Microsoft.VisualBasic.Strings.StrConv(strWrkKana, Microsoft.VisualBasic.VbStrConv.Wide, 0);
            }

            return strRet;
        }





        public void ChangeOrder(DataGridView dataGridView, string fieldName1, string fieldName2, DataGridViewColumnHeaderCell currentOrder)
        {
            // On Error Resume Next の代替コードは省略

            currentOrder.Style.ForeColor = System.Drawing.Color.Black;

            // ユーザーがクリックした列を取得
            DataGridViewColumnHeaderCell clickedHeader = currentOrder;

            if (dataGridView.SortedColumn == null || clickedHeader.OwningColumn != dataGridView.SortedColumn)
            {
                // 初めてクリックされた列か、異なる列がクリックされた場合
                dataGridView.Sort(clickedHeader.OwningColumn, System.ComponentModel.ListSortDirection.Ascending);
                clickedHeader.Style.ForeColor = System.Drawing.Color.Red;
            }
            else if (dataGridView.SortOrder == System.Windows.Forms.SortOrder.Ascending)
            {
                // 既に昇順に並んでいる場合
                dataGridView.Sort(clickedHeader.OwningColumn, System.ComponentModel.ListSortDirection.Descending);
                clickedHeader.Style.ForeColor = System.Drawing.Color.Blue;
            }
            else
            {
                // 既に降順に並んでいる場合
                dataGridView.Sort(clickedHeader.OwningColumn, System.ComponentModel.ListSortDirection.Ascending);
                clickedHeader.Style.ForeColor = System.Drawing.Color.Red;
            }
        }










        public static void 範囲指定(Control control1, Control control2)
        {
            // エラーハンドリングを追加
            try
            {
                Form frmOn = control1.FindForm(); // コントロールが所属するフォーム
                Control ctlCurrent = control1;    // 調整元の値のあるコントロール
                Control ctlTarget = control2;     // 調整先のコントロール

                // ２つのコントロールを含むフォームが違っていれば何もしない
                if (ctlCurrent.Parent != ctlTarget.Parent)
                    return;

                if (frmOn.ActiveControl.Name == ctlCurrent.Name)
                {
                    ctlCurrent = control1;
                    ctlTarget = control2;
                }
                else
                {
                    ctlCurrent = control2;
                    ctlTarget = control1;
                }

                // どちらかのコントロール値が null のときは両方の値を null に設定する
                if (ctlCurrent.Text == "" || ctlTarget.Text == "")
                {
                    ctlTarget.Text = ctlCurrent.Text;
                }

                // コントロール1の値 <= コントロール2の値 とする
                if (Convert.ToDouble(control1.Text) > Convert.ToDouble(control2.Text))
                {
                    ctlTarget.Text = ctlCurrent.Text;
                }
            }
            catch (FormatException)
            {
                Console.WriteLine("Control1またはControl2のConvertに失敗しました。");
            }
            catch (Exception ex)
            {
                Console.WriteLine("範囲指定 - " + ex.GetType().Name + " : " + ex.Message);
            }
        }




        public DateTime InputDate(char keyCommand, DateTime date)
        {
            if (keyCommand == '+')
            {
                // "+"の場合、日付を1日進める
                date = date.AddDays(1);
            }
            else if (keyCommand == '-')
            {
                // "-"の場合、日付を1日戻す
                date = date.AddDays(-1);
            }

            return date;
        }





        public decimal GetRoundedAmount(decimal amount, int roundPattern)
        {
            decimal roundedAmount = 0;

            // 0 の場合は常に 0 を返す
            if (amount == 0)
            {
                return roundedAmount;
            }

            int sign = Math.Sign(amount);      // 符号を取得する
            decimal absValue = Math.Abs(amount);  // 絶対値を取得する

            switch (roundPattern)
            {
                case 1:
                    // 切り捨て
                    roundedAmount = Math.Floor(absValue) * sign;
                    break;
                case 2:
                    // 切り上げ
                    if (Math.Floor(absValue) == absValue)
                    {
                        roundedAmount = absValue * sign;
                    }
                    else
                    {
                        roundedAmount = Math.Ceiling(absValue) * sign;
                    }
                    break;
                case 3:
                    // 四捨五入
                    roundedAmount = Math.Round(absValue, MidpointRounding.AwayFromZero) * sign;
                    break;
            }

            return roundedAmount;
        }



        public int OfficeClosed(SqlConnection connection, DateTime date)
        {
            int officeClosed = 0;

            // 日曜日または土曜日は休日
            if (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday)
            {
                officeClosed = 1;
            }
            else
            {
                // 祝祭日・公休日の確認
                using (SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM Holiday WHERE Date = @Date", connection))
                {
                    cmd.Parameters.AddWithValue("@Date", date.Date);

     
                    int count = (int)cmd.ExecuteScalar();
                    //connection.Close();

                    if (count > 0)
                    {
                        officeClosed = 1;
                    }
                }
            }

            return officeClosed;
        }




        public static string WhereString(string bodyString, string addString)
        {           

            if (string.IsNullOrEmpty(addString))
                return bodyString;

            if (string.IsNullOrEmpty(bodyString))
            {
                bodyString = "(" + addString + ")";
                return bodyString;
            }
            else
            {
                if (!bodyString.Contains(addString))
                {
                    return bodyString + " AND (" + addString + ")";
                }
                else
                {
                    return bodyString;
                }

            }
        }

        //外部システムを起動する
        public static void GetShell(string para)
        {
            try
            {
                string param;
                Process process = new Process();
                param = $" -user:{CommonConstants.LoginUserName}{para}";

                string programFilesPath = Environment.GetEnvironmentVariable("ProgramFiles");
                string exePath = Path.Combine(programFilesPath, "Uinics", "Uinics U-net 3 Client", "unetc.exe");

                if (!File.Exists(exePath))
                {
                    //programFilesにない場合は32bitを検索
                    programFilesPath = Environment.GetEnvironmentVariable("ProgramFiles(x86)");
                    exePath = Path.Combine(programFilesPath, "Uinics", "Uinics U-net 3 Client", "unetc.exe");
                }

                    //process.StartInfo.FileName = $"{Environment.GetEnvironmentVariable("ProgramFiles")}\\Uinics\\Uinics U-net 3 Client\\unetc.exe";
                    process.StartInfo.FileName = exePath;
                    process.StartInfo.Arguments = param;
                    process.Start();                
                
            }
            catch
            {
                MessageBox.Show("外部システムを開けませんでした。。", "OPEN時エラー", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



















    }


}
