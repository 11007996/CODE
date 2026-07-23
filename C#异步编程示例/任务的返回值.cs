using System;
using System.Threading.Tasks;
using System.Threading;
namespace pro
{
	class Class1
	{
		static void Main()
		{
			Task<int> task = Task.Run(()=>
			{
				Console.WriteLine("Foo");
				return 3;
				});
			int result = task.Result;  //返回任务的结果
			Console.WriteLine(result);
			Console.ReadKey();
		}
	}
}