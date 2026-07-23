using System;
using System.Threading;
using System.Threading.Tasks;
namespace pro
{
	class Class1
	{
		static void Main()
		{
			Task task = Task.Run(()=>
			{
				Thread.Sleep(3000);
				Console.WriteLine("Foo");
				});
			Console.WriteLine(task.IsCompleted);
			task.Wait();  //阻塞直到task完成操作，相当于调用Thread.Join
			Console.WriteLine(task.IsCompleted);
			Console.ReadKey();
		}
	}
}