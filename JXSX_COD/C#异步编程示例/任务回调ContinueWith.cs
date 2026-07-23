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
			
			primeNumberTask.ContinueWith(task=>{    //任务执行完全成后执行此回调函数
				int result = task.Result;
				Console.WriteLine(result);
				});
			Console.ReadKey();
		}
	}
}