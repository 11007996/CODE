using System;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
namespace pro
{
	class Class1
	{
		static void Main()
		{
			Task<int> primeNumberTask = Task.Run(()=>
				Enumerable.Range(2,200000).Count(n=>
					Enumerable.Range(2,(int)Math.Sqrt(n)-1).All(i=>n % i >0)));
			var awaiter = primeNumberTask.GetAwaiter();
			var awaiter = primeNumberTask.ConfigureAwait(false).GetAwaiter();  //回调函数与任务运行在同一个线程上，避免了上下文切换
			awaiter.OnCompleted(()=>     //任务完成后执行委托
			{
				int result = awaiter.GetResult();
				Console.WriteLine(result);
				});
			Console.ReadKey();
		}
	}
}