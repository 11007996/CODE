using log4net;
using System;

namespace ComTools
{
    internal class ExceptionHandle
    {
        private static ILog log = LogManager.GetLogger(typeof(ExceptionHandle));

        /// <summary>
        /// UI线程异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal static void Application_ThreadException(object sender, System.Threading.ThreadExceptionEventArgs e)
        {
            Exception error = e.Exception as Exception;
            log.Error("捕获到未处理的异常", error);
        }

        /// <summary>
        /// 未捕获的异常
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        internal static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception error = e.ExceptionObject as Exception;
            log.Error("捕获到未处理的异常", error);
        }
    }
}