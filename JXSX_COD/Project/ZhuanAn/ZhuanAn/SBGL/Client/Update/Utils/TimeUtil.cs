using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Update.Utils
{
    public static class TimeUtil
    {
        //部署的电脑与服务器的时间差（秒数）
        public static int SpanSeconds = 0;
        public static DateTime Now
        {
            get { return DateTime.Now.AddSeconds(SpanSeconds); }
        }
    }
}
