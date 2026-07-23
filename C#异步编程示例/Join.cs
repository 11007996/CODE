using System;
using System.Threading;
namespace pro
{
	class Class1
	{
		static void Main()
		{
			Thread t = new Thread(Go);
			t.Start();
			t.Join();   //等待该线程终止。只有线程执行完才可能执行后面的代码
			Console.WriteLine("Thread t has ended!");
			Console.ReadKey();
		}
		public static void Go()
		{
			for(int i =0;i<1000;i++)
			{
				Console.Write("Y");
			}
		}

	}
}