using System;
using System.Threading;
namespace pro
{
	class Class1
	{
		static void Main()
		{
			Thread t = new Thread(()=>Print("hello world"));
			t.Start();

			Thread t = new Thread(Print);
			t.Start("hello world");
			Console.ReadKey();
		}
		static void Print(object message)  //参数必须为object类型
		{
			string msg = (string)message;
			Console.WriteLine(msg);
		}
	}
}