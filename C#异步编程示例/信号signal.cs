using System;
using System.Threading;
namespace pro
{
	class Class1
	{
		static void Main()
		{
			var signal = new ManualResetEvent(false);   //信号处于关闭状态
			new Thread(()=>
			{
				Console.WriteLine("Waiting for signal ...");
				signal.WaitOne();   //线程阻塞等待Set信号
				signal.Dispose();   //销毁signal信号
				Console.WriteLine("Get signal!");
			}).Start();

			Thread.Sleep(3000);
			signal.Set();  //发送打开信号，线程继续执行
			signal.Reset();  //发送关闭信号，线程阻塞
			Console.ReadKey();
		}
	}
}