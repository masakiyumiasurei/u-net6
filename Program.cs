using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace u_net
{
    internal static class Program
    {
        /// <summary>
        /// アプリケーションのメイン エントリ ポイントです。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Application.Run(new F_入庫管理());
            // Application.Run(new F_シリーズ());

            //Application.Run(new F_売上一覧_担当者別());
            Application.Run(new F_支払管理());
            //Application.Run(new F_部品管理());

        }
    }
}
