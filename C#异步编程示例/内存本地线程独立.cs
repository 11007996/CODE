using System;
using System.Threading;
namespace pro
{
	class Class1
	{
		static void Main()
		{
			new Thread(Go).Start();
			Go();
			Console.ReadKey();
		}
		static void Go()
		{
			for(int i=0;i<5;i++)
			{
				Console.Write(i);
			}
		}
	}
}