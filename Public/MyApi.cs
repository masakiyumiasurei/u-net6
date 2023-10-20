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
        // GetSystemMetrics をインポート
        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int nIndex);

        // nIndex 定数
        private const int SM_CXFULLSCREEN = 16; // 最大化したときのクライアント領域の幅
        private const int SM_CYFULLSCREEN = 17; // 同、高さ

        public void GetFullScreen(out int Xsize, out int Ysize)
        {
            Xsize = GetSystemMetrics(SM_CXFULLSCREEN);
            Ysize = GetSystemMetrics(SM_CYFULLSCREEN);
        }

        public int GetTwipPerDot(int dpi)
        {
            // DPI値から１ドットあたりの twip 値を得る
            return 1440 / dpi;
        }


        // ウィンドウハンドルからデバイスコンテキストを取得
        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);

        // デスクトップウィンドウのウィンドウハンドルを取得する
        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        // デバイスに関する情報を取得する
        [DllImport("gdi32.dll")]
        public static extern int GetDeviceCaps(IntPtr hDC, int nIndex);

        // nIndexの定数
        private const int LOGPIXELSX = 88; // 横方向の１論理インチあたりのピクセル数

        public int GetLogPixel()
        {
            //１論理インチあたりのピクセル数を得る
            IntPtr hDC = GetDC(GetDesktopWindow());
            int logPixelX = GetDeviceCaps(hDC, LOGPIXELSX);
            return logPixelX;
        }


    }
}
