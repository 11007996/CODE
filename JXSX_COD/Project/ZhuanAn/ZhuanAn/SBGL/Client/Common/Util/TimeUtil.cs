using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Common.Util
{
    public static class TimeUtil
    {
        //部署的电脑与服务器的时间差（秒数）
        public static int SpanSeconds = 0;
        public static DateTime Now
        {
            get { return DateTime.Now.AddSeconds(SpanSeconds); }
        }

        /// <summary>
        /// 秒数转换为对应时间单位的值  
        /// (返回匹配最大的单位与值 如：130秒钟 转换后只会返回 'timeVal:2 timeUnit:分钟' 剩下的秒数会忽略)
        /// </summary>
        /// <param name="totalSeconds">秒数</param>
        /// <param name="timeVal">时间值</param>
        /// <param name="timeUnit">时间单位（秒钟、分钟、小时）</param>
        public static void ConvertTime(int totalSeconds, ref int timeVal, ref string timeUnit)
        {
            if (totalSeconds <= 59)
            {
                timeUnit = "秒钟";
                timeVal = totalSeconds;
            }
            else if (totalSeconds < 3600)
            {
                timeUnit = "分钟";
                timeVal = (int)Math.Floor((double)totalSeconds / 60);
            }
            else if (totalSeconds >= 3600)
            {
                timeUnit = "小时";
                timeVal = (int)Math.Floor((double)totalSeconds / 3600);
            }
        }

        /// <summary>
        /// 计算两个时间时隔，固定反回格式：00天00时00分，不足一分钟返回'0分钟'； 前面为0无值的数据不显示单位及值。
        /// </summary>
        /// <param name="t1"></param>
        /// <param name="t2"></param>
        public static string ConvertDiffTime(DateTime startTime, DateTime endTime)
        {
            string time = string.Empty;
            if (startTime == null || endTime == null) return time;

            int days = DiffDays(startTime, endTime);
            int hours = DiffHours(startTime, endTime);
            int minutes = DiffMinutes(startTime, endTime);
            //int seconds =  DiffSeconds(startTime, endTime);
            if (days > 0)
            {
                time += days + "天";
            }
            if (hours > 0)
            {
                time += hours + "时";
            }
            if (minutes > 0)
            {
                time += minutes + "分";
            }
            else
            {
                time += "0分";
            }
            return time;
        }

        //两个时间相差秒数
        public static int DiffSeconds(DateTime startTime, DateTime endTime)
        {
            TimeSpan span = new TimeSpan(endTime.Ticks - startTime.Ticks);
            return span.Seconds;
        }

        //两个时间相差分钟数
        public static int DiffMinutes(DateTime startTime, DateTime endTime)
        {
            TimeSpan span = new TimeSpan(endTime.Ticks - startTime.Ticks);
            return span.Minutes;
        }
        //两个时间相差小时数
        public static int DiffHours(DateTime startTime, DateTime endTime)
        {
            TimeSpan span = new TimeSpan(endTime.Ticks - startTime.Ticks);
            return span.Hours;
        }

        //两个时间相差天数
        public static int DiffDays(DateTime startTime, DateTime endTime)
        {
            TimeSpan span = new TimeSpan(endTime.Ticks - startTime.Ticks);
            return span.Days;
        }

        /// <summary>
        /// 根据输入的星期序号，返回指定星期的开始与结束时间
        /// </summary>
        /// <param name="preWeeks">星期序号（当前星期：0，上一周：-1，下一周：1，以此类推）</param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        public static void WeekStartToEnd(int preWeeks, out DateTime start, out DateTime end)
        {
            DateTime dt = TimeUtil.Now.Date;
            //Sunday = 0,Monday = 1,Tuesday = 2,Wednesday = 3,Thursday = 4,Friday = 5,Saturday = 6,        
            int dayOfWeek = -1 * (int)dt.Date.DayOfWeek;
            start = dt.AddDays(dayOfWeek + 1 + (preWeeks * 7));//取本周一
            //如果今天是周日，则开始时间是上周一
            if (dayOfWeek == 0)
            {
                start = start.AddDays(-7);
            }
            end = start.AddDays(7);
        }

        /// <summary>
        /// 根据秒数转为字符说明，如122秒转换后为：2分2秒
        /// </summary>
        /// <param name="seconds"></param>
        /// <returns></returns>
        public static string ConvertSecondsToDesc(int seconds)
        {
            string time = "";
            TimeSpan ts = new TimeSpan(0, 0, seconds);
            int days = ts.Days;
            int hours = ts.Hours;
            int minutes = ts.Minutes;
            if (days > 0)
            {
                time += days + "天";
            }
            if (hours > 0)
            {
                time += hours + "时";
            }
            if (minutes > 0)
            {
                time += minutes + "分";
            }
            else
            {
                time += "0分";
            }
            return time;
        }


    }
}
