using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace u_net.Public
{
    internal class CommonModule
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
        public static string GetComputerName()
        {
            try
            {
                // コンピュータ名を取得
                string computerName = Dns.GetHostName();
                return computerName;
            }
            catch (Exception ex)
            {
                // 例外が発生した場合はエラーメッセージを返す
                return "Error: " + ex.Message;
            }
        }


        [DllImport("mpr.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int WNetGetUser(string lpName, StringBuilder lpUserName, ref int lpnLength);

        public static string NetUserName()
        {
            try
            {
                // ユーザー名を取得するためのバッファを準備
                StringBuilder userNameBuffer = new StringBuilder(255);
                int bufferLength = userNameBuffer.Capacity;

                // WNetGetUserを使用してネットワークユーザー名を取得
                int result = WNetGetUser("", userNameBuffer, ref bufferLength);

                if (result == 0)
                {
                    // Null終端を取り除いたユーザー名を返す
                    string netUserName = userNameBuffer.ToString(0, userNameBuffer.Length - 1);
                    return netUserName;
                }
                else
                {
                    // エラーが発生した場合は空文字列を返す
                    return "";
                }
            }
            catch (Exception ex)
            {
                // 例外が発生した場合はエラーメッセージを返す
                return "Error: " + ex.Message;
            }
        }

        public static string GetVersionInfo()
        {
            // アプリケーションの実行ファイルの情報を取得
            Assembly assembly = Assembly.GetEntryAssembly();

            // バージョン情報を取得
            Version version = assembly.GetName().Version;

            // メジャーバージョン、マイナーバージョン、ビルド番号を文字列に連結
            string versionString = $"{version.Major}.{version.Minor}.{version.Build}";

            return versionString;
        }

    }

}
