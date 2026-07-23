using System;
using System.Threading.Tasks;
using System.Threading;
using System.Linq;
/*namespace pro
{
	class Class1
	{
		static void Main()
		{
			var tcs=new TaskCompletionSource<int>();   //初始化对象TaskCompletionSource
			new Thread(()=>
			{
				Thread.Sleep(5000);
				tcs.SetResult(42);   //设置task结果
				})
			{
				IsBackground=true
			}.Start();

			Task<int> task=tcs.Task;    //TaskCompletionSource对象Task属性返回一个Task，返回值为int类型
			Console.WriteLine(task.Result);   //task.Result返回TaskCompletionSource对象Task结果
			Console.ReadKey();
		}
	}
}*/


/*namespace pro
{
	class Class1
	{
		static void Main()
		{
			//此处的Run()是底下我们实现的Run()
			Task<int> task=Run(()=>    //一个委托作为函数参数
			{
				Thread.Sleep(2000);
				return 42;
			});
			Console.WriteLine(task.Result);  //返回任务的结果
			Console.ReadKey();
		}

		//调用此方法相当于调用Task.Factory.StartNew
		//并使用TaskCreationOptions.LongRunning选项来创建非线程池的线程
		static Task<TResult> Run<TResult>(Func<TResult> function)
		{
			var tcs=new TaskCompletionSource<TResult>();
			new Thread(()=>
			{
				try
				{
            		//function()的执行结果作为信号
					tcs.SetResult(function());   //返回42
				}
				catch(System.Exception e)
				{
					tcs.SetException(e);
				}
				}).Start();
			return tcs.Task;
		}
	}
}*/


namespace pro
{
	class Class1
	{
		/*static void Main(string[] args)
		{
			var awaiter=GetAnswerToLife().GetAwaiter();
			awaiter.OnCompleted(()=>
			{
				Console.WriteLine(awaiter.GetResult());
				});
    		//不会占用线程，所以不输出，需要阻塞
			Console.ReadKey();
		}
		static Task<int> GetAnswerToLife()
		{
			var tcs=new TaskCompletionSource<int>();
    		//AutoReset
    		//如果 Timer 应在每次间隔结束时引发 Elapsed 事件，则为 true；如果它仅在间隔第一次结束后引发一次 Elapsed 事件，则为 false。 默认值为 true。 
    		//如果调用 Start 方法时已经启用 Timer，则重置间隔。 如果 AutoReset 为 false，则必须调用 Start 方法才能再次开始计数。
			var timer=new System.Timers.Timer(5000){AutoReset=false};
    		//时间到达指定间隔就会触发Elapsed事件
			timer.Elapsed+=delegate{timer.Dispose();tcs.SetResult(42);};
			timer.Start();
			return tcs.Task;
		}*/


		//创建task但不占用线程
		static void Main(string[] args)
		{
		    Delay(5000).GetAwaiter().OnCompleted(()=>Console.WriteLine(42));
		    //5秒钟之后，Continuation开始的时候，才占用线程，所以无输出
		    Console.ReadKey();
		}
		//注意：没有非泛型版本的TaskCompletionSource
		static Task Delay(int milliseconds)
		{
		    var tcs=new TaskCompletionSource<object>();
		    var timer=new System.Timers.Timer(milliseconds){AutoReset=false};
		    timer.Elapsed+=delegate{timer.Dispose();tcs.SetResult(null);};
		    timer.Start();
		    return tcs.Task;
		}
	}
}