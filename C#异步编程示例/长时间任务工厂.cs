using System;
using System.Threading;
using System.Threading.Tasks;
namespace pro
{
	class Class1
	{
		static void Main()
		{
			Task task = Task.Factory.StartNew(()=>
			{
				Thread.Sleep(3000);
				Console.WriteLine("Foo");
				},TaskCreationOptions.LongRunning);   //避免同时运行多个长时间任务，对性能影响较大
			Console.ReadKey();
		}
	}
}