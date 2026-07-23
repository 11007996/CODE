using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ComTools
{
    internal static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //处理未捕获的异常
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //ThreadException 处理UI线程异常
            Application.ThreadException += new System.Threading.ThreadExceptionEventHandler(ExceptionHandle.Application_ThreadException);
            //UnhandledException 处理未捕获的异常
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(ExceptionHandle.CurrentDomain_UnhandledException);

            //加载配置
            ConfigUtil.LoadConfig();
            Application.Run(new FrmPlatform());
            //保存配置
            ConfigUtil.SaveConfig();
        }
    }
}