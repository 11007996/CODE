using LuxMMS;
using Common;
using Common.Util;
using Machine;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using LuxMMS.Base;
using Call;

namespace LuxMMS
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
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
                ConfigUtil.LoadXmlConfig();
#if DEBUG
                //Debug模式不检查更新
                BaseInfo.AutoUpdate = false;
#endif

                //检查数据库连接
                string checkResult = DBUtil.CheckServerConnState();
                if (checkResult.Length > 0)
                {
                    new FrmMsgDialog("连接异常", checkResult).Show();
                    Thread.Sleep(5000);
                    System.Environment.Exit(0);
                }

                //开启同步服务器时间任务
                OpenSyncServerTimeTask();

                //判断是否是通过update.exe启动的
                if (args.Length > 0 && "更新完成".Equals(args[0]))
                {
                    for (int i = 0; i < args.Length; i++)
                    {
                        //if (i == 0) GlobalData.StartType = args[0];
                        if (i == 1) BaseInfo.LoginUserNo = string.IsNullOrWhiteSpace(args[1]) ? null : args[1];
                        //判断更新前是打开的什么页面，更新后打开对应页面
                        if (i == 2 && !string.IsNullOrWhiteSpace(args[2]))
                        {
                            if (args[2] == typeof(FrmKanBan).ToString())
                            {
                                BaseInfo.CurrFrmType = typeof(FrmKanBan);
                            }
                            else if (args[2] == typeof(FrmMaster).ToString())
                            {
                                BaseInfo.CurrFrmType = typeof(FrmMaster);
                            }
                        }
                    }
                }
                else if (BaseInfo.AutoUpdate)
                {
                    //检查并更新Update.exe文件。
                    UpdateHelper.CheckUpdateApp();
                    //检查并更新主应用
                    UpdateHelper.AutoUpdateApp();
                }


                //检查桌面是否有快捷方式
                //bool hasLnk = DesktopShortcut.IsDesktopShortcutExists();
                //if (!hasLnk)
                //{
                //    string targetPath = Path.Combine(Application.StartupPath, "LuxMMS.exe");
                //    DesktopShortcut.CreateDesktopShortcut(targetPath, "设备系统");
                //}
                //清理因更新产生后的原垃圾文件 
                //UpdateHelper.ClearObsoleteFiles();

                Application.Run(new Auxiliary());
            }
        }


        //开启同步服务器时间任务
        public static void OpenSyncServerTimeTask()
        {
            Console.WriteLine("开启服务器时间同步任务");
            try
            {
                Task task = Task.Run(() =>
                {
                    while (true)
                    {
                        DBUtil.SyncServerTime();
                        Thread.Sleep(60 * 60 * 1000);
                    }
                });
            }
            catch (Exception ex)
            {
                LogHelper.Error(typeof(Program), "服务器同步时间任务异常结束，原因：" + ex.Message);
            }
        }

        //查找是否有运行实例，并返回实例进程
        public static Process RunningInstance()
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
        public static void HandleRunningInstance(Process instance)
        {
            ShowWindowAsync(instance.MainWindowHandle, SW_SHOWMAXIMIZED);
            SetForegroundWindow(instance.MainWindowHandle);
        }

        //ShowWindow操作窗口状态
        [DllImport("User32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int cmdShow);

        private const int SW_HIDE = 0; //隐藏窗口，并激活其他窗口
        private const int SW_SHOWNORMAL = 1; //激活并正常大小显示窗口
        private const int SW_SHOWMINIMIZED = 2; //最小化窗口
        private const int SW_SHOWMAXIMIZED = 3; //最大化窗口
        private const int SW_SHOWNOACTIVATE = 4; //显示但不激活

        //获取当前激活的正在工作的窗口句柄
        [DllImport("User32.dll")]
        private static extern bool SetForegroundWindow(IntPtr hWnd);
    }
}
