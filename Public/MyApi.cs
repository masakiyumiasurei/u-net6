using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace u_net.Public
{
    internal class MyApi
    {
        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int nIndex);

        // nIndex 定数
        private const int SM_CXFULLSCREEN = 16; // 最大化したときのクライアント領域の幅
        private const int SM_CYFULLSCREEN = 17; // 同、高さ

        public MyApi()
        {
            // SM_CXFULLSCREEN と SM_CYFULLSCREEN を使用してシステムメトリクスを取得
            int width = GetSystemMetrics(SM_CXFULLSCREEN);
            int height = GetSystemMetrics(SM_CYFULLSCREEN);

            Console.WriteLine("Screen Width: " + width);
            Console.WriteLine("Screen Height: " + height);
        }
    }
}
