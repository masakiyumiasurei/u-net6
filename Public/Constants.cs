using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static string LoginUserCode = "855";
        public static string LoginUserFullName = "阪南太郎";

    }

}
