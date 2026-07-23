using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace ComTools.Util
{
    public class MouseSimulator
    {
        private const uint INPUT_MOUSE = 0;

        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;

        private const uint MOUSEEVENTF_LEFTUP = 0x0004;

        public static void ClickAt(Rectangle rect)
        {
            Point point = new Point(rect.X + rect.Width / 2, rect.Y + rect.Height / 2);
            Console.WriteLine(point);
            ClickAt(point);
        }

        public static void ClickAt(Point location)
        {
            Point screenLocation = location;
            // 移动鼠标到指定位置
            SetCursorPos(screenLocation.X, screenLocation.Y);

            INPUT mouseDownInput = new INPUT { type = INPUT_MOUSE };
            mouseDownInput.mi.dx = screenLocation.X;
            mouseDownInput.mi.dy = screenLocation.Y;
            mouseDownInput.mi.dwFlags = MOUSEEVENTF_LEFTDOWN;

            INPUT mouseUpInput = new INPUT { type = INPUT_MOUSE };
            mouseUpInput.mi.dx = screenLocation.X;
            mouseUpInput.mi.dy = screenLocation.Y;
            mouseUpInput.mi.dwFlags = MOUSEEVENTF_LEFTUP;

            // Send the input
            INPUT[] inputs = { mouseDownInput, mouseUpInput };
            if (SendInput(2, inputs, Marshal.SizeOf(typeof(INPUT))) != 2)
            {
                throw new Exception("Failed to send mouse input.");
            }
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool SetCursorPos(int x, int y);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, INPUT[] pInputs, int cbSize);

        [StructLayout(LayoutKind.Sequential)]
        private struct MOUSEINPUT
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct INPUT
        {
            public uint type;
            public MOUSEINPUT mi;
        }
    }
}