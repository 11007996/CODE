using ComTools.Util;
using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows.Automation;

namespace ComTools.Automation
{
    /// <summary>
    /// 扩展事件处理
    /// </summary>
    public class EventHandle
    {
        #region 单击控件

        /// <summary>
        /// 单击指定控件
        /// </summary>
        public static void MouseClickElement(AutomationElement targetElement)
        {
            //显示要操作的窗口
            string automationId = "";
            if (targetElement != null)
            {
                WindowEnumerator.ShowWindowsByHWnd((IntPtr)targetElement.Current.NativeWindowHandle);
                if (targetElement.Current.ControlType == ControlType.Button)
                {//按钮类型直接用
                    // 确保元素是可以点击的（例如，它实现了InvokePattern）
                    if (targetElement.GetCurrentPattern(InvokePattern.Pattern) != null)
                    {
                        // 强制转换为InvokePattern并执行点击
                        InvokePattern invokePattern = (InvokePattern)targetElement.GetCurrentPattern(InvokePattern.Pattern);
                        invokePattern.Invoke();
                    }
                    else
                    {
                        throw new Exception($"自动化控件{automationId},没有有效的InvokePattern");
                    }
                }
                else
                {//其他类型通过模拟鼠标点击，这种方式需要指定的程序页面显示
                    MouseSimulator.ClickAt(AutomationUtil.ConvertWindowRectToRectangle(targetElement.Current.BoundingRectangle));
                }
            }
            else
            {
                throw new Exception($"自动化控件{automationId}不能为空");
            }
        }

        #endregion 单击控件

        #region 打开应用

        public static bool OpenApp(string appPath)
        {
            // 创建一个新的进程信息对象
            ProcessStartInfo startInfo = new ProcessStartInfo
            {
                // 指定要启动的exe文件的完整路径
                FileName = appPath,
                // 使用操作系统shell启动，这可以帮助识别文件关联（例如.txt文件默认用记事本打开）
                UseShellExecute = true,
                // 设置为true可以继承当前进程的环境变量
                //LoadUserProfile = true
            };

            // 创建进程实例
            Process process = new Process
            {
                StartInfo = startInfo
            };
            // 启动进程
            bool result = process.Start();
            if (!result)
                throw new Win32Exception(Marshal.GetLastWin32Error());
            return result;
        }

        #endregion 打开应用

        #region 关闭窗口

        public static bool CloseWindowByName(string windowName)
        {
            // 使用窗口标题查找窗口句柄
            IntPtr hWnd = WindowEnumerator.FindWindow(null, windowName);

            // 如果找到了窗口
            if (hWnd != IntPtr.Zero)
            {
                // 获取窗口所属进程的ID
                uint processId;
                WindowEnumerator.GetWindowThreadProcessId(hWnd, out processId);
                // 通过进程ID获取进程对象
                Process process = Process.GetProcessById((int)processId);
                if (process != null)
                {
                    // 尝试正常关闭进程
                    process.CloseMainWindow();
                    // 如果CloseMainWindow未能关闭进程，则强制终止
                    if (!process.HasExited)
                    {
                        process.Kill();
                    }
                }
            }
            return true;
        }

        #endregion 关闭窗口
    }
}