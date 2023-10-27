using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace u_net.Public
{
    internal class LocalSetting
    {
        private SqlConnection cn;
        private SqlTransaction tx;
        public void Connect()
        {
            Connection connectionInfo = new Connection();
            string connectionString = connectionInfo.Getconnect();
            cn = new SqlConnection(connectionString);
            cn.Open();
        }
        //ユーザごと、フォームごとのサイズと配置を呼び出して配置する
        public void LoadPlace(string userCode, Form form, bool isFixed = false)
        {
            try
            {
                string objectName = form.Name;
                // フォーム名に"F_"があれば、取り除く
                if (objectName.StartsWith("F_"))
                {
                    objectName = objectName.Substring(2);
                }

                Connect();
                MyApi myapi = new MyApi();

                string sqlQuery = "SELECT WindowLeft, WindowTop, WindowWidth, WindowHeight FROM Mオブジェクト配置 WHERE UserCode = @UserCode AND ObjectName = @ObjectName";

                using (SqlCommand command = new SqlCommand(sqlQuery, cn))
                {
                    command.Parameters.AddWithValue("@UserCode", userCode);
                    command.Parameters.AddWithValue("@ObjectName", objectName);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        long lngLeft, lngTop, lngWidth, lngHeight;

                        if (reader.Read())
                        {
                            //Mオブジェクト配置にレコードがある場合は登録
                            lngLeft = Convert.ToInt64(reader["WindowLeft"]);
                            lngTop = Convert.ToInt64(reader["WindowTop"]);
                            lngWidth = Convert.ToInt64(reader["WindowWidth"]);
                            lngHeight = Convert.ToInt64(reader["WindowHeight"]);
                        }
                        else
                        {
                            // 配置情報が保存されていない場合の処理
                            if (isFixed)
                            {
                                lngLeft = 0;
                                lngTop = 0;
                                lngWidth = form.Width;
                                lngHeight = form.Height;
                            }
                            else
                            {
                                int xSize, ySize, intpixel;
                                myapi.GetFullScreen(out xSize, out ySize);
                                lngLeft = 0;
                                lngTop = 0;
                                intpixel = myapi.GetLogPixel();

                                //accessではtwipがサイズ単位のため、ピクセルをapiから取得してtwipに変換していたが、
                                //こちらでは不要。微調整用の数値がtwipのため、１ドットあたりの twip値を逆に割ると丁度か

                                lngWidth = xSize - 150 / myapi.GetTwipPerDot(intpixel);
                                lngHeight = ySize - 1200 / myapi.GetTwipPerDot(intpixel);
                            }
                        }

                        // オブジェクトの配置を調整
                        if (form is Control control)
                        {
                            control.Left = (int)lngLeft;
                            control.Top = (int)lngTop;
                            control.Width = (int)lngWidth;
                            control.Height = (int)lngHeight;
                        }
                    }
                }

            }
            catch (Exception ex)
            {
                // エラーハンドリング
                Console.WriteLine("LoadPlace error: " + ex.Message);
            }
        }

        public void SavePlace(string userCode, Form form)
        {
            Connect();

            SqlTransaction transaction = cn.BeginTransaction();

            try
            {
                string objectName = form.Name;
                // フォーム名に"F_"があれば、取り除く
                if (objectName.StartsWith("F_"))
                {
                    objectName = objectName.Substring(2);
                }


                string strKey = "UserCode='" + userCode + "' AND ObjectName='" + objectName + "'";
                string strSQL = "DELETE FROM Mオブジェクト配置 WHERE " + strKey;

                using (SqlCommand deleteCommand = new SqlCommand(strSQL, cn, transaction))
                {
                    deleteCommand.CommandType = CommandType.Text;
                    deleteCommand.ExecuteNonQuery();
                }

                strSQL = "INSERT INTO Mオブジェクト配置 (UserCode, ObjectName, WindowLeft, WindowTop, WindowWidth, WindowHeight) " +
                         "VALUES ('" + userCode + "', '" + objectName + "', " + form.Left + ", " + form.Top +
                         ", " + form.Width + ", " + form.Height + ")";

                using (SqlCommand insertCommand = new SqlCommand(strSQL, cn, transaction))
                {
                    insertCommand.CommandType = CommandType.Text;
                    insertCommand.ExecuteNonQuery();
                }

                transaction.Commit();
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine("SavePlace - " + ex.Message);
            }
            finally
            {
                cn.Close();
            }
        }

    }

}
