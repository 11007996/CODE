using System;
using System.Threading.Tasks;
using System.Threading;
namespace pro
{
	class Class1
	{
		static void Main(string[] args)
		{
			Task t = new Task(() =>
			{
				Console.WriteLine("任务开始工作……");
        Thread.Sleep(5000);  //模拟工作过程
        });
			t.Start();
			t.ContinueWith(task =>
			{
				Console.WriteLine("任务完成，完成时候的状态为：");
				Console.WriteLine("IsCanceled={0}\tIsCompleted={1}\tIsFaulted={2}", 
					task.IsCanceled, task.IsCompleted, task.IsFaulted);
				});
			Console.ReadKey();
		}
	}
}