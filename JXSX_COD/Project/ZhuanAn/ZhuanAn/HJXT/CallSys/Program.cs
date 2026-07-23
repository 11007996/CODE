using CallSys;
using CallSys.Utils;
using Common;
using Common.Utils;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

namespace CallSys
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //处理未捕获的异常
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //ThreadException 处理UI线程异常
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(ExceptionHandle.Application_ThreadException);
            //UnhandledException 处理未捕获的异常
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExceptionHandle.CurrentDomain_UnhandledException);

            //同一应用只能启动一个
            if (System.Diagnostics.Process.GetProcessesByName(System.Diagnostics.Process.GetCurrentProcess().ProcessName).Length > 1)
            {
                MessageBox.Show("已经有一个运行了", "重复启动", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                System.Environment.Exit(0);
            }

            //加载自定义的Config.xml配置文件
            ConfigUtil.LoadXmlConfig();
#if DEBUG
            //Debug模式不检查更新
            BaseInfo.AutoUpdate = false;
#endif

            //检查数据库连接
            if (DBUtil.CheckServerConnState().Length > 0)
            {
                new FrmMsgDialog("连接异常", "请检查网络").Show();
                Thread.Sleep(5000);
                System.Environment.Exit(0);
            }

            //同步服务器时间
            DBUtil.SyncServerTime();

            //判断是否是通过update.exe启动的
            if (args.Length > 0 && "更新完成".Equals(args[0]))
            {
                for (int i = 0; i < args.Length; i++)
                {
                    //if (i == 0) GlobalData.StartType = args[0];
                    if (i == 1) BaseInfo.LoginHandlerNo = string.IsNullOrWhiteSpace(args[1]) ? null : args[1];
                    if (i == 2) GlobalData.CurrFrmType = string.IsNullOrWhiteSpace(args[2]) ? null : Type.GetType(args[2]);
                }
            }
            else if (BaseInfo.AutoUpdate)
            {
                //检查并更新Update.exe文件。
                UpdateHelper.CheckUpdateApp();
                //检查并更新CallSys.exe应用
                UpdateHelper.AutoUpdateCallSysApp();
            }
            Application.Run(new Auxiliary());
        }

    }
}
