using Microsoft.Win32.SafeHandles;
using System;
using System.IO;
using System.Runtime.InteropServices;

namespace ComTools.Util
{
    internal class WinApiFunctions
    {
        public const int ULW_COLORKEY = 1;

        public const int ULW_ALPHA = 2;

        public const int ULW_OPAQUE = 4;

        public const byte AC_SRC_OVER = 0;

        public const byte AC_SRC_ALPHA = 1;

        public const uint FILE_FLAG_NO_BUFFERING = 0x20000000;

        public const uint FILE_FLAG_SEQUENTIAL_SCAN = 0x8000000;

        private const int FSCTL_SET_COMPRESSION = 639040;

        private const short COMPRESSION_FORMAT_DEFAULT = 1;

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool UpdateLayeredWindow(IntPtr hwnd, IntPtr hdcDst, ref WinApiFunctions.Point pptDst, ref WinApiFunctions.Size psize, IntPtr hdcSrc, ref WinApiFunctions.Point pprSrc, int crKey, ref WinApiFunctions.BLENDFUNCTION pblend, int dwFlags);

        [DllImport("user32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr GetDC(IntPtr hWnd);

        [DllImport("user32.dll", ExactSpelling = true)]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool DeleteDC(IntPtr hdc);

        [DllImport("gdi32.dll", ExactSpelling = true)]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        [DllImport("gdi32.dll", ExactSpelling = true, SetLastError = true)]
        public static extern bool DeleteObject(IntPtr hObject);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern SafeFileHandle CreateFile(string lpFileName, FileAccess dwDesiredAccess, FileShare dwShareMode, IntPtr lpSecurityAttributes, FileMode dwCreationDisposition, uint dwFlagsAndAttributes, IntPtr hTemplateFile);

        [DllImport("kernel32", SetLastError = true)]
        public static extern unsafe bool ReadFile(SafeFileHandle hFile, void* pBuffer, int NumberOfBytesToRead, out uint pNumberOfBytesRead, IntPtr Overlapped);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int DeviceIoControl(SafeFileHandle hDevice, int dwIoControlCode, ref short lpInBuffer, int nInBufferSize, IntPtr lpOutBuffer, int nOutBufferSize, ref int lpBytesReturned, IntPtr lpOverlapped);

        //文件不缓存

        //顺序扫描

        public struct Point
        {
            public int x;

            public int y;

            public Point(int x, int y)
            {
                this.x = x;
                this.y = y;
            }
        }

        public struct Size
        {
            public int cx;

            public int cy;

            public Size(int cx, int cy)
            {
                this.cx = cx;
                this.cy = cy;
            }
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        public struct BLENDFUNCTION
        {
            public byte BlendOp;
            public byte BlendFlags;
            public byte SourceConstantAlpha;
            public byte AlphaFormat;
        }

        [StructLayout(LayoutKind.Sequential, Pack = 1)]
        private struct ARGB
        {
            public byte Blue;
            public byte Green;
            public byte Red;
            public byte Alpha;
        }
    }
}