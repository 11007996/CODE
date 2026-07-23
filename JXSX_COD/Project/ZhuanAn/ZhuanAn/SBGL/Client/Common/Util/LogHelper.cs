using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Util   
{

    /// <summary>
    /// 日志
    /// </summary>
    public class LogHelper
    {
        public static void Info(Type type, string msg)
        {
            ILog log = LogManager.GetLogger(type.ToString());
            if (log.IsInfoEnabled)
            {
                log.Info(msg);
            }
        }

        public static void Warn(Type type, string msg)
        {
            ILog log = LogManager.GetLogger(type.ToString());
            if (log.IsWarnEnabled)
            {
                log.Warn(msg);
            }
        }

        public static void Error(Type type, string msg)
        {
            ILog log = LogManager.GetLogger(type.ToString());
            if (log.IsErrorEnabled)
            {
                log.Error(msg);
            }
        }

        public static void Error(Type type, string msg, Exception ex)
        {
            ILog log = LogManager.GetLogger(type.ToString());
            if (log.IsErrorEnabled)
            {
                log.Error(msg, ex);
            }
        }
    }
}
