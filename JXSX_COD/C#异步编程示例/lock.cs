using System;
using System.Threading;
namespace pro{
	class Class1{
		static bool _done;
		static readonly object _locker = new object();
		static void Main()
		{
			new Thread(Go).Start();
			Go();
			Console.ReadKey();
		}
		static void Go()
		{
			lock(_locker)    //同时只能有一个线程可能运行，其他线程会等待或阻塞，直到锁变成可用状态
			{
				if(false == _done)
			{
				Console.WriteLine("Done");
				_done = true;
			}
			}
			
		}
	}
}