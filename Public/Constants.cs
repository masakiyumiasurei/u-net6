using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using u_net;

namespace u_net
{
    public class CommonConstants
    {
        public const string STR_APPTITLE = "Uinics U-net Client";  // アプリケーション名
        public const string STR_APPVERSION = "2.6.52";             // アプリケーションのバージョン

        public const int WS_SYSMENU = 0x80000;
        public const int GWL_STYLE = -16;
        public const int WM_UNDO = 0x304;
        public const int MF_BYCOMMAND = 0x0;
        public const int SC_CLOSE = 0xF060;
        public const int SC_MAXIMIZE = 0xF030;
        public const int SC_MINIMIZE = 0xF020;
        public const int SC_RESTORE = 0xF120;

        // 各登録データのコードヘッダ
        public const string CH_DOCUMENT = "DOC";  // 文書データ
        public const string CH_EMPLOYEE = "USR";  // 社員マスタ
        public const string CH_ESTIMATE = "EST";  // 見積データ
        public const string CH_MAKER = "MAK";     // メーカーマスタ
        public const string CH_ORDER = "ORD";     // 発注データ
        public const string CH_SUPPLIER = "SUP";  // 仕入先マスタ

        public const string USER_CODE_PRESIDENT = "056";   // 社長のユーザーコード
        public const string USER_CODE_SALES = "007";       // 営業部の承認者のユーザーコード
        public const string USER_CODE_TECH = "015";        // 技術部の承認者のユーザーコード
        public const string USER_CODE_MANAGE = "018";      // 管理部の承認者のユーザーコード
        public const string USER_CODE_GA = "110";          // 業務チームの承認者のユーザーコード

        private const int BUF_SIZE = 256;

        public static string strCertificateCode;              // 認証コード（認証社員コード）
        public static string LoginUserCode = "029";           // ログインユーザーコード
        public static string LoginUserName;                   // ログインユーザー名
        public static string LoginUserFullName = "浅野 俊之";  // ログインユーザーの氏名
        public static string LoginDep;                        // ログインユーザーが所属する部


        public static string MyAppPath;                // アプリケーションパス
        public static string MyAppName;                // アプリケーション名
        public static string MyComputerName;           // コンピュータ名
        public static string MyUserName;               // ユーザー名
        public static string MyOsName;                 // 使用OS
        public static string ServerInstanceName;       // 接続先サーバーのインスタンス名

        public static string codeString;               // コード選択フォーム用
        public static object objParent;                // 通信用変数
        public static Dictionary<string, object> colReports = new Dictionary<string, object>(); // レポート生成用ディクショナリ
        public static Form frmParent;                  // 呼び出し元フォーム
        public static Control ctlParent;               // 呼び出し元コントロール
        public static Control ctlNext;                 // フォーカス移動先コントロール
        public static object varShowArgs;              // Accessオブジェクト可視時引数 不要？
        public static bool bleShowPrice;


        //データベース操作のモジュールに設定されていた変数
        public static byte bytConStatus;               // 接続状態（レベル）
        public static int ConnNumber;                   // 接続番号
        public static string strAppVer= System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();                // アプリケーションのバージョン
        public static string strAppLastVer;            // アプリケーションの最終リリースバージョン
        public static string ConnectionStr;

        //ローカル設定のモジュールに設定されていた変数
        public static object varPaperSize;
        public static string m_strServerName;    //     接続先サーバー名


        //OSのバージョンを返す
        public static string SysVersion()
        {
            var version = Environment.OSVersion;
            string osInfo = $"Windows Version: {version.Version.Major}.{version.Version.Minor}\n" +
                            $"Build Number: {version.Version.Build}\n" +
                            $"Platform: {version.Platform}";

            return osInfo;
        }

        // 
        /// <summary>
        /// 親フォームを検索するメソッド
        /// </summary>
        /// <returns></returns>
        public static Form GetParent()
        {
            // F_MdiParent フォームが既にインスタンス化されている場合
            foreach (Form form in Application.OpenForms)
            {
                if (form.Name == "F_MdiParent")
                {
                    return form;
                }
            }

            // F_MdiParent フォームがまだインスタンス化されていない場合 ありえないか。。。
            F_MdiParent mdiParent = new F_MdiParent();
            mdiParent.Show();
            return mdiParent;
        }

    }

    


}
