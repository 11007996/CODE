using System;
using System.Threading.Tasks;
using System.Threading;
/*namespace pro
{
	class Class1
	{
		static void Main()
		{
			Task task = Task.Run(()=>{throw null;});
			try 
			{
				task.Wait();    //任务抛出异常会被task.Wait抛出
			}
			catch(AggregateException aex)
			{
				if(aex.InnerException is NullReferenceException)
				{
					Console.WriteLine("Null");
				}
				else
				{
					throw;
				}
			}
			Console.ReadKey();
		}
	}
}*/

namespace pro
{
	class Class1
	{
		static void Main()
		{
			Task<int> task = Task.Run(()=>{throw null; return 3;});
			try 
			{
				int i = task.Result;   //任务抛出异常会被task.Result抛出
			}
			catch(AggregateException aex)
			{
				if(aex.InnerException is NullReferenceException)
				{
					Console.WriteLine("Null");
				}
				else
				{
					throw;
				}
			}
			Console.ReadKey();
		}
	}
}