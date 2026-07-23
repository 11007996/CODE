using ComTools.Automation;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace ComTools.Util
{
    public class WindowEnumerator
    {
        public delegate bool EnumWindowsProc(IntPtr hWnd, IntPtr lParam);

        //获取所有任务栏的窗口
        public static List<WindowInfo> GetAllWindows()
        {
            List<WindowInfo> windowInfos = new List<WindowInfo>();
            EnumWindows(delegate (IntPtr hWnd, IntPtr lParam)
            {
                StringBuilder sb = new StringBuilder(256);
                if (GetWindowText(hWnd, sb, sb.Capacity) > 0 && IsWindowVisible(hWnd))
                {
                    WindowInfo wi = new WindowInfo(hWnd, sb.ToString());
                    windowInfos.Add(wi);
                }
                return true;
            }, IntPtr.Zero);
            return windowInfos;
        }

        // 导入user32.dll中的FindWindow函数
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        public static IntPtr GetWindowHandleByName(string windowName)
        {
            // 使用窗口标题查找窗口句柄
            IntPtr hwnd = FindWindow(null, windowName);
            return hwnd;
        }

        /// <summary>
        /// 最小化所有窗口
        /// </summary>
        public static void MinimizeAllWindows()
        {
            // 使用EnumWindows遍历所有顶层窗口
            EnumWindows(delegate (IntPtr hWnd, IntPtr lParam)
            {
                // 检查窗口是否可见，以避免对非任务栏窗口操作
                if (IsWindowVisible(hWnd))
                {
                    // 使用ShowWindow函数将窗口最小化
                    ShowWindow(hWnd, 6 /* SW_MINIMIZE */);
                }
                // 继续枚举下一个窗口
                return true;
            }, IntPtr.Zero);
        }

        [DllImport("user32.dll")]
        private static extern bool EnumWindows(EnumWindowsProc enumProc, IntPtr lParam);

        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        //[DllImport("user32.dll", SetLastError = true)]
        //public static extern bool SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        #region 关闭窗口

        // 请求关闭窗口的消息常量
        public const uint WM_CLOSE = 0x0010;

        // 关闭窗口命令的消息常量
        public const uint WM_SYSCOMMAND = 0x0112;

        public const uint SC_CLOSE = 0xF060;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

        [DllImport("user32.dll", SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out uint lpdwProcessId);

        #endregion 关闭窗口

        #region 设置目标窗口显示

        private const int SW_RESTORE = 9;

        private const int SW_MAXIMIZE = 3;

        public static void ShowWindowsByHWnd(IntPtr hWnd)
        {
            //如果窗口被最小化，可以通过改变窗口状态显示
            if (IsIconic(hWnd))
            {
                ShowWindow(hWnd, SW_RESTORE);
            }
        }

        [DllImport("user32.dll")]
        private static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

        #endregion 设置目标窗口显示

        #region 根据窗口句柄获取窗口的信息

        public static void GetWindowInfo(IntPtr hwnd, out string className, out string windowTitle)
        {
            const int MAX_TITLE_LENGTH = 256;
            const int MAX_CLASS_NAME_LENGTH = 256;

            StringBuilder classNameBuilder = new StringBuilder(MAX_CLASS_NAME_LENGTH);
            GetClassName(hwnd, classNameBuilder, classNameBuilder.Capacity);
            className = classNameBuilder.ToString().Trim();

            StringBuilder windowTitleBuilder = new StringBuilder(MAX_TITLE_LENGTH);
            GetWindowText(hwnd, windowTitleBuilder, windowTitleBuilder.Capacity);
            windowTitle = windowTitleBuilder.ToString().Trim();
        }

        [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        private static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);

        #endregion 根据窗口句柄获取窗口的信息

        #region 设置窗口显示在最上层

        private const int HWND_TOPMOST = -1;

        //顶层窗口标志
        private const int HWND_NOTOPMOST = -2;

        // 非顶层窗口标志
        private const uint SWP_NOSIZE = 0x0001;

        private const uint SWP_NOMOVE = 0x0002;

        public static void SetAsTopMost(IntPtr hWnd)
        {
            SetWindowPos(hWnd, new IntPtr(HWND_TOPMOST), 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
        }

        public static void RemoveTopMost(IntPtr hwnd)
        {
            SetWindowPos(hwnd, new IntPtr(HWND_NOTOPMOST), 0, 0, 0, 0, SWP_NOMOVE | SWP_NOSIZE);
        }

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        #endregion 设置窗口显示在最上层
    }
}