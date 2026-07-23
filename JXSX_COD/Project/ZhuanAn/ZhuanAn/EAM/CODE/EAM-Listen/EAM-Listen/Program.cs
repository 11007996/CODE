using EAM.Listen.Common.Config;
using EAM.Listen.Common.Utils;
using EAM.Listen.Communication;
using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace EAM.Listen
{
    internal static class Program
    {
        private const int SW_HIDE = 0;

        //隐藏窗口，并激活其他窗口
        private const int SW_SHOWNORMAL = 1;

        //激活并正常大小显示窗口
        private const int SW_SHOWMINIMIZED = 2;

        //最小化窗口
        private const int SW_SHOWMAXIMIZED = 3;

        //最大化窗口
        private const int SW_SHOWNOACTIVATE = 4;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            //查找是否有实例，同一应用只能启动一个
            Process instance = RunningInstance();
            if (instance != null)
            {
                HandleRunningInstance(instance);
            }
            else
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                //处理未捕获的异常
                Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
                //ThreadException 处理UI线程异常
                Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(ExceptionHandle.Application_ThreadException);
                //UnhandledException 处理未捕获的异常
                AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExceptionHandle.CurrentDomain_UnhandledException);

                //加载自定义的Config.xml配置文件
                Setting.LoadConfig();
                FactoryBaseConfig.LoadConfig();
                IotProductConfig.LoadConfig();

                //开启Mqtt订阅
                if (Setting.MqttConfig.MqttSubFlag)
                {
                    MqttProxy.Start();
                }

                //开启Http监听
                if (Setting.HttpConfig.HttpListenFlag)
                {
                    HttpProxy.Start();
                }

                //开启TCP监听
                if (Setting.TCPConfig.TCPListenFlag)
                {
                    TcpProxy.Start();
                }

                //开启串口监听
                if (Setting.SerialPortConfig.SerialListenFlag)
                {
                    SerialPortProxy.Open();
                }
                Application.Run(new MainForm());
            }
        }

        //查找是否有运行实例，并返回实例进程
        private static Process RunningInstance()
        {
            Process current = Process.GetCurrentProcess();  // 取得目前作用中的處理序
            Process[] processes = Process.GetProcessesByName(current.ProcessName);  // 取得指定的處理緒名稱的所有處理序
            foreach (Process process in processes)
            {
                if (process.Id != current.Id)
                {
                    if (Assembly.GetExecutingAssembly().Location.Replace("/", "\\") == current.MainModule.FileName)
                    {
                        return process;
                    }
                }
            }
            return null;
        }

        //打开正在运行的应用实例
        private static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, SW_SHOWMAXIMIZED);
            SetForegroundWindow(instance.MainWindowHandle);
        }

        //ShowWindow操作窗口状态
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

        //显示但不激活

        //获取当前激活的正在工作的窗口句柄
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}