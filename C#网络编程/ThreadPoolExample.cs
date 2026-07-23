using System;
using System.Threading;
using System.Text;
using System.Collections;
using System.Collections.Generic;
namespace ThreadP
{
	// class Tpool
	// {
		// static void Main(string[] args)
		// {
			// ThreadPool.QueueUserWorkItem(new WaitCallback(ThreadProc));
			// Console.WriteLine("主线程执行一些操作，然后暂停等待异步操作完成");
            // Thread.Sleep(1000);
            // Console.WriteLine("主线程已退出");
		// }
		 // static void ThreadProc(object stateInfo)
		// {
			// Thread.Sleep(500);
			// Console.WriteLine("这是线程池中的线程输出的内容");
		// }
	// }
	
	class Tpool
	{
		static void Main(string[] args)
		{
			Thread th = new Thread(ThreadProc);
			Console.WriteLine("主线程执行一些操作，然后暂停等待异步操作完成");
			th.Start();
			th.Join(3500);
            Console.WriteLine("主线程已退出");
			th.Abort();
		}
		 static void ThreadProc()
		{
			int i = 0;
			Console.WriteLine("这是线程池中的线程输出的内容");
			while(i<6)
			{
			Console.WriteLine(i);
			i =i+ 1;
			Thread.Sleep(500);
			}
		}
	}
}