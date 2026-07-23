using System;
using System.Timers;

namespace TimerExample1
{
    class Program
    {

        static void Main(string[] args)
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Enabled = true;
            timer.Interval = 2000;//执行间隔时间,单位为毫秒;此时时间间隔为1分钟
            timer.Start();
            timer.Elapsed += new System.Timers.ElapsedEventHandler(test); 

            Console.ReadKey();
        }

        private static void test(object source, ElapsedEventArgs e)
        {
            // if (DateTime.Now.Hour == 13 && DateTime.Now.Minute == 41)  //如果当前时间是10点30分
            Console.WriteLine("OK, event fired at: " + DateTime.Now.ToString());
        }
    }
}