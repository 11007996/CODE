using System;
using System.Threading;
namespace name
{
	class Class1
	{
		public static void Main()
		{
			Thread t = new Thread(WriteY);   //线程无序
			t.Start();
			Console.WriteLine("Thread t has ended!");
			for(int i =0;i<10;i++)
			{
				Console.Write("x");
			}
			Console.ReadKey();
		}

		public static void WriteY()
		{
			for(int i =0;i<1000;i++)
			{
				Console.Write("y");
			}
		}
	}
}