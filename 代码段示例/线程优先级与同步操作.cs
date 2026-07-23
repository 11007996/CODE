using System;
using System.Threading;

namespace ConsoleApp1
{
    class Program
    {
        private int n1, n2, n3;
        //将信号状态设置为非终止
		//initialState设置初始状态，如果为true，则WaitOne()在调用Reset()方法前不会阻塞线程，先调用Reset()再调用WaitOne()会阻塞WaitOne()所在线程；如果设置为false，则WaitOne()会阻塞，直到调用Set()
		//mode设置为EventResetMode.ManualReset，在调用Set()时所有WaitOne()阻塞线程都会继续执行；设置为EventResetMode.AutoReset则每次调用Set()会使一个WaitOne()阻塞的线程继续执行，一般按照阻塞先后顺序执行
        // EventWaitHandle myEventWaitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);AboveNormal
        EventWaitHandle myEventWaitHandle = new EventWaitHandle(false, EventResetMode.ManualReset);
        static void Main(string[] args)
        {
            Program p = new Program();
            Thread t0 = new Thread(new ThreadStart(p.WriteThread));
            Thread t1 = new Thread(new ThreadStart(p.ReadThread1));
            Thread t2 = new Thread(new ThreadStart(p.ReadThread2));
			//线程优先级Highest、AboveNormal、Normal、BelowNormal
			// t2.Priority = ThreadPriority.AboveNormal;   
            t0.Start();
            t1.Start();
            t2.Start();
            Console.ReadLine();
        }

        private void WriteThread()
        {
            //允许其他需要等待的线程阻塞
            myEventWaitHandle.Reset();
            Console.WriteLine("t1");
            n1 = 1;
            n2 = 2;
            n3 = 3;
            //允许其他等待线程继续
            myEventWaitHandle.Set();
        }

        private void ReadThread1()
        {
            //堵塞当前线程，直到收到Set()信号
            myEventWaitHandle.WaitOne();
            Console.WriteLine("{0}+{1}+{2}={3}", n1, n2, n3, n1 + n2 + n3);
        }

        private void ReadThread2()
        {
            //堵塞当前线程，直到收到Set()信号
            myEventWaitHandle.WaitOne();
            Console.WriteLine("{0}+{1}+{2}={3}", n3, n2, n1, 2*(n1 + n2 + n3));
        }
    }
}