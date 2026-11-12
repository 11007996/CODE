using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace user32
{
    
    class Program
    {
        /// <summary>
        /// 鼠标事件
        /// </summary>
        [Flags]
        public enum MouseEventFlags
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,    //dx/dy 使用【绝对归一化坐标】
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010,
            XDOWN = 0x0080,  // 侧键按下
            XUP = 0x0100,  // 侧键抬起
            WHEEL = 0x0800,  // 滚轮滚动
            HWHEEL = 0x1000,  // 横向滚轮
        }



        #region user32.dll声明
        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="hWnd">父窗口句柄（null表示无父窗口）</param>
        /// <param name="lpText">弹窗内容</param>
        /// <param name="lpCaption">弹窗标题</param>
        /// <param name="uType">弹窗样式（按钮、图标组合）</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]   //CharSet.Unicode可显示中文
        public static extern int MessageBox(IntPtr hWnd, string lpText, string lpCaption, uint uType);

        /// <summary>
        /// 获取屏幕分辨率
        /// </summary>
        /// <param name="nIndex">指定要获取的信息（比如 0 = 屏幕宽度，1 = 屏幕高度）</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern int GetSystemMetrics(int nIndex);


        /// <summary>
        /// 查找窗口（根据类名和窗口名）
        /// </summary>
        /// <param name="lpClassName">类名</param>
        /// <param name="lpWindowName">标题名</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        /// <summary>
        /// 修改窗口标题
        /// </summary>
        /// <param name="hWnd">句柄</param>
        /// <param name="lpString">标题名</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern bool SetWindowText(IntPtr hWnd, string lpString);


        /// <summary>
        /// 查找子窗口（一个窗口内含有其它窗口）
        /// </summary>
        /// <param name="hWndParent">父窗口句柄</param>
        /// <param name="hWndChildAfter">起始子窗口（null表示从第一个开始）</param>
        /// <param name="lpClassName">子窗口类名</param>
        /// <param name="lpWindowName">子窗口标题</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindowEx(IntPtr hWndParent,IntPtr hWndChildAfter,string lpClassName,string lpWindowName);



        /// <summary>
        /// 移动鼠标
        /// </summary>
        /// <param name="X"></param>
        /// <param name="Y"></param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        public static extern bool SetCursorPos(int X, int Y);


        /// <summary>
        /// 向指定窗口发送各种消息（如点击按钮、输入文本、关闭窗口等），实现对窗口的精细控制
        /// </summary>
        /// <param name="hWnd">目标窗口句柄（通过 FindWindow 获取）</param>
        /// <param name="Msg">消息类型（如 WM_CLOSE 表示关闭窗口）</param>
        /// <param name="wParam">消息参数1（根据消息类型变化）</param>
        /// <param name="lParam">消息参数2（字符串类型，如输入的文本）</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr SendMessage(IntPtr hWnd,int Msg,IntPtr wParam,string lParam);
        // 常用消息常量（可在微软文档中查询更多）
        public const int WM_CLOSE = 0x0010;         // 关闭窗口消息
        public const int WM_SETTEXT = 0x000C;       // 设置窗口文本（如输入框内容）
        public const int WM_LBUTTONDOWN = 0x0201;   // 鼠标左键按下
        public const int WM_LBUTTONUP = 0x0202;     // 鼠标左键释放（模拟点击）
        public const int WM_CREATE = 0x0001;        // 创建窗口


        /// <summary>
        /// 鼠标事件(已废弃，推荐用SendInput)
        /// </summary>
        /// <param name="dwFlags"></param>
        /// <param name="dx">相对当前鼠标的x轴</param>
        /// <param name="dy">相对当前鼠标的y轴</param>
        /// <param name="cButtons">滚轮数据 / 侧键</param>
        /// <param name="dwExtraInfo">附加信息，一般填0</param>
        [DllImport("user32.dll")]
        private static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);


        /// <summary>
        /// 鼠标事件,代替mouse_event
        /// </summary>
        /// <param name="nInputs">数组里 INPUT 元素个数</param>
        /// <param name="pInputs">INPUT 结构体数组</param>
        /// <param name="cbSize">Marshal.SizeOf(typeof(INPUT))，单个 INPUT 结构体大小</param>
        /// <returns></returns>
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern uint SendInput(uint nInputs, user32.Fun.INPUT[] pInputs, int cbSize);

        #endregion

        static void Main(string[] args)
        {
            #region MessageBox显示消息框
            //int result = MessageBox(IntPtr.Zero, "示例内容", "消息框标题", 0x0);   //0x0表示是按钮
            //int result = MessageBox(IntPtr.Zero, "示例内容", "消息框标题", 0x03 | 0x30);   //0x03表示是否按钮，0x30表示取消按钮+警告图标
            //Console.WriteLine("点击了按钮" + result);  //result表示点击的按钮
            #endregion

            #region GetSystemMetrics获取屏幕分辨率
            //int sizeW = GetSystemMetrics(0);
            //int sizeH = GetSystemMetrics(1);
            //Console.WriteLine("屏幕宽：" + sizeW + "屏幕高：" + sizeH);
            //int taskbarHeight = GetSystemMetrics(29);
            //Console.WriteLine("任务栏高度：" + taskbarHeight);
            #endregion

            #region FindWindow/SetWindowText查找并修改窗口标题
            // 查找记事本窗口（类名"Notepad"，标题"无标题 - 记事本"）
            //vs 工具->Syp++->搜索->查找窗口    获取窗口类名和窗口标题
            //IntPtr notepadWnd = FindWindow("Notepad", "222.txt - 记事本");
            //if (notepadWnd != IntPtr.Zero)
            //{
            //    // 找到窗口后，修改标题
            //    bool success = SetWindowText(notepadWnd, "被C#修改过的标题 - 记事本");
            //    if (success)
            //    {
            //        Console.WriteLine("记事本标题修改成功！");
            //    }
            //    else
            //    {
            //        Console.WriteLine("标题修改失败");
            //    }
            //}
            //else
            //{
            //    Console.WriteLine("未找到记事本窗口，请先打开记事本");
            //}
            #endregion

            #region SetCursorPos移动鼠标
            //bool b = SetCursorPos(0, 0);  //以整个屏幕的左上角为原点
            //if(b)
            //{
            //    Console.WriteLine("鼠标移动成功");
            //}
            #endregion

            #region SendMessage向指定窗口发送消息
            // 1. 先找到记事本窗口（确保已打开，标题为"无标题 - 记事本"）
            //IntPtr notepadWnd = FindWindow("Notepad", "222.txt - 记事本");
            //if (notepadWnd == IntPtr.Zero)
            //{
            //    Console.WriteLine("未找到记事本窗口");
            //    return;
            //}


            // 2. 发送关闭窗口消息（WM_CLOSE）
            //SendMessage(notepadWnd, WM_CLOSE, IntPtr.Zero, null);
            //Console.WriteLine("已发送关闭消息，记事本应该会关闭");


            // 2. 找到记事本的编辑区域子窗口（类名"Edit"，无标题）
            //IntPtr editWnd = FindWindowEx(notepadWnd, IntPtr.Zero, "Edit", null);
            //if (editWnd == IntPtr.Zero)
            //{
            //    Console.WriteLine("未找到编辑区域");
            //    return;
            //}
            // 3. 发送输入文本消息（WM_SETTEXT）
            //SendMessage(editWnd, WM_SETTEXT, IntPtr.Zero, "这是通过 SendMessage 输入的文本！");
            //Console.WriteLine("已向记事本输入文本");

            #endregion

            #region mouse_event相对当前位置移动鼠标
            //移动鼠标
            //mouse_event((int)MouseEventFlags.MOVE, 0X0000, 0X0100, 0X0001, 0X0000);   //往y轴方向向下移动100

            //鼠标右键
            //mouse_event((int)MouseEventFlags.RIGHTDOWN, 0, 0, 0, 0);
            //mouse_event((int)MouseEventFlags.RIGHTUP, 0, 0, 0, 0);

            //Thread.Sleep(5000);
            //mouse_event((int)MouseEventFlags.WHEEL, 0, 0, 500, 0);   //滚轮向上滚动500,坐标不是像素，是 0~65535 归一化
            #endregion

            #region SendInput鼠标操作
            //user32.Fun.INPUT[] inputs = new user32.Fun.INPUT[1];
            //inputs[0].type = user32.Fun.InputType.INPUT_MOUSE;
            //inputs[0].u.mi.dx = 0X5000;
            //inputs[0].u.mi.dy = 0X5000;
            //inputs[0].u.mi.dwFlags = user32.Fun.MouseEventFlags.MOVE | user32.Fun.MouseEventFlags.ABSOLUTE;
            //inputs[0].u.mi.time = 0;
            //inputs[0].u.mi.dwExtraInfo = IntPtr.Zero;
            //SendInput(1, inputs, Marshal.SizeOf(typeof(user32.Fun.INPUT)));  //移动鼠标

            //Thread.Sleep(3000);
            //user32.Fun.INPUT[] inputs = new user32.Fun.INPUT[2];
            //// 左键按下
            //inputs[0].type = user32.Fun.InputType.INPUT_MOUSE;
            //inputs[0].u.mi.dwFlags = user32.Fun.MouseEventFlags.LEFTDOWN;
            //// 左键抬起
            //inputs[1].type = user32.Fun.InputType.INPUT_MOUSE;
            //inputs[1].u.mi.dwFlags = user32.Fun.MouseEventFlags.LEFTUP;
            //SendInput(2, inputs, Marshal.SizeOf(typeof(user32.Fun.INPUT)));    //点击左键
            #endregion

            Console.ReadKey();
        }
    }
}
