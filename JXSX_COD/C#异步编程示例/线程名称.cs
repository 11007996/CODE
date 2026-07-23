using System;
using System.Threading;
namespace sp
{
	class Class1
	{
		public static void Main(string[] sr)
		{
			Thread t = new Thread(WriteY);
			t.Name = "Y Thread ...";
			t.Start();

			Console.WriteLine("Thread.CurretThread.Name");
			Thread.Sleep(5);   //导致输出x无序
			for(int i =0;i<10;i++)
			{
				Console.Write("x");
			}
			Console.ReadKey();
		}
		public static void WriteY()
		{
			Console.WriteLine(Thread.CurrentThread.Name);   //线程名称
			for(int i=0;i<1000;i++)
			{
				Console.Write("Y");
			}
		}
	}
}
