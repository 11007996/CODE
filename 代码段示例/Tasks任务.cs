using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections.Concurrent;

namespace ConsoleApp1
{
    class Program
    {

       /* static Task<int> CreateTask(string name)
        {
            return new Task<int>(() => TaskMethod(name));
        }

        static void Main(string[] args)
        {
            TaskMethod("Main Thread Task");
            Task<int> task = CreateTask("Task 1");
            task.Start();
            int result = task.Result;
            Console.WriteLine("Task 1 Result is: {0}", result);

            task = CreateTask("Task 2"); 
            task.RunSynchronously(); //该任务会运行在主线程中
            result = task.Result;
            Console.WriteLine("Task 2 Result is: {0}", result);

            task = CreateTask("Task 3");
            Console.WriteLine(task.Status);
            task.Start();

            while (!task.IsCompleted)
            {
                Console.WriteLine(task.Status);
                Thread.Sleep(TimeSpan.FromSeconds(2));
            }

            Console.WriteLine(task.Status);
            result = task.Result;
            Console.WriteLine("Task 3 Result is: {0}", result);


            #region 常规使用方式
            //创建任务
            Task<int> getsumtask = new Task<int>(() => Getsum());
            //启动任务,并安排到当前任务队列线程中执行任务(System.Threading.Tasks.TaskScheduler)
            getsumtask.Start();
            Console.WriteLine("主线程执行其他处理");
            getsumtask.Wait(); //等待任务的完成执行过程
            Console.WriteLine("任务执行结果：{0}", getsumtask.Result.ToString());//获得任务的执行结果
            Console.ReadKey();
            #endregion
        }

        static int TaskMethod(string name)
        {
            Console.WriteLine("Task {0} is running on a thread id {1}. Is thread pool thread: {2}",
                name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
            Thread.Sleep(TimeSpan.FromSeconds(2));
            return 42;
        }

        static int Getsum()
        {
            int sum = 0;
            Console.WriteLine("使用`Task`执行异步操作.");
            for (int i = 0; i < 100; i++)
            {
                sum += i;
            }
            return sum;
            }*/


		/*
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
		*/

		/*
		static void Main(string[] args)
        {
            var t1 = new Task(() => TaskMethod("Task 1"));
            var t2 = new Task(() => TaskMethod("Task 2"));
            t2.Start();
            t1.Start();
            Task.WaitAll(t1, t2);  //待t1、t2任务完后再执行主线程
            Task.Run(() => TaskMethod("Task 3"));
            Task.Factory.StartNew(() => TaskMethod("Task 4"));
            //标记为长时间运行任务,则任务不会使用线程池,而在单独的线程中运行。
            Task.Factory.StartNew(() => TaskMethod("Task 5"), TaskCreationOptions.LongRunning);
            
            #region 常规的使用方式
            Console.WriteLine("主线程执行业务处理.");
            //创建任务
            Task task = new Task(() =>
                                 {
                                     Console.WriteLine("使用`System.Threading.Tasks.Task`执行异步操作.");
                                     for (int i = 0; i < 10; i++)
                                     {
                                         Console.WriteLine(i);
                                     }
                                 });
            //启动任务,并安排到当前任务队列线程中执行任务(System.Threading.Tasks.TaskScheduler)
            task.Start();
            Console.WriteLine("主线程执行其他处理");
            task.Wait(); //任务队列线程中排队执行任务
            #endregion
        
            Thread.Sleep(TimeSpan.FromSeconds(1));
            Console.ReadLine();
        }
        
        static void TaskMethod(string name)
        {
            Console.WriteLine("Task {0} is running on a thread id {1}. Is thread pool thread: {2}",
                              name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
        }
		*/

		/*
		async static void AsyncFunction()
        {
            await Task.Delay(5);  //推迟执行5毫秒
            Console.WriteLine("使用`System.Threading.Tasks.Task`执行异步操作.");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(string.Format("AsyncFunction:i={0}", i));
            }
        }

        public static void Main()
        {
            Console.WriteLine("主线程执行业务处理.");
            AsyncFunction();
            Console.WriteLine("主线程执行其他处理");
            for (int i = 0; i < 10; i++)
            {
                Console.WriteLine(string.Format("Main:i={0}", i));
            }
            Console.ReadLine();
        }
		*/

		/*
		 public static void Main()
        {
            var ret1 = AsyncGetsum();
            Console.WriteLine("主线程执行其他处理");
            for (int i = 1; i <= 3; i++)
                Console.WriteLine("Call Main()");
            int result = ret1.Result;     //阻塞主线程
            Console.WriteLine("任务执行结果：{0}", result);
        }

        async static Task<int> AsyncGetsum()
        {
            await Task.Delay(1);
            int sum = 0;
            Console.WriteLine("使用`Task`执行异步操作.");
            for (int i = 0; i < 100; i++)
            {
                sum += i;
            }
            return sum;
        }
		*/

		/*说明
		Wait：针对单个Task的实例，可以task1.wait进行线程等待，等待task运行完成
        WaitAny：线程列表中任何一个线程执行完毕即可执行（阻塞主线程）
        WaitAll：线程列表中所有线程执行完毕方可执行（阻塞主线程）
        WhenAny：与ContinueWith配合,线程列表中任何一个执行完毕，则继续ContinueWith中的任务（开启新线程，不阻塞主线程）
        WhenAll：与ContinueWith配合,线程列表中所有线程执行完毕，则继续ContinueWith中的任务（开启新线程，不阻塞主线程）
        ContinueWith：与WhenAny或WhenAll配合使用
        ContinueWhenAny：等价于Task的WhenAny+ContinueWith
        ContinueWhenAll：等价于Task的WhenAll+ContinueWith
        */
		/*
		
		public static void Main()
        {
            //创建一个任务
            Task<int> task = new Task<int>(() =>
            {
                int sum = 0;
                Console.WriteLine("使用`Task`执行异步操作.");
                for (int i = 0; i < 100; i++)
                {
                    sum += i;
                }
                return sum;
            });
            //启动任务,并安排到当前任务队列线程中执行任务(System.Threading.Tasks.TaskScheduler)
            task.Start();
            Console.WriteLine("主线程执行其他处理");
            //任务完成时执行处理。
            Task cwt = task.ContinueWith(t =>
            {
                Console.WriteLine("任务完成后的执行结果：{0}", t.Result.ToString());
            });
            task.Wait();
            cwt.Wait();
            
            Action<string,int> log = (name,time) =>
            {
                Console.WriteLine(@"{0}任务开始...",name);
                Thread.Sleep(time);
                Console.WriteLine(@"{0}任务结束!",name);
            };
            List<Task> tasks = new List<Task>
            {
                Task.Run(() => log("张三",3000)),
                Task.Run(() => log("李四",1000)),
                Task.Run(() => log("王五",2000))
            };
            //以下语句逐个测试效果
            // Task.WaitAny(tasks.ToArray());
            // Task.WaitAll(tasks.ToArray());
            // Task.WhenAny(tasks.ToArray()).ContinueWith(x => Console.WriteLine("某个Task执行完毕"));
            // Task.WhenAll(tasks.ToArray()).ContinueWith(x => Console.WriteLine("所有Task执行完毕"));
            // Task.Factory.ContinueWhenAny(tasks.ToArray(), x => Console.WriteLine("某个Task执行完毕"));
            Task.Factory.ContinueWhenAll(tasks.ToArray(), x => Console.WriteLine("所有Task执行完毕"));
            Console.Read();
        }
		*/


		/*static void Main(string[] args)
        {
            ConcurrentStack<int> stack = new ConcurrentStack<int>();
        
            //t1先串行
            var t1 = Task.Factory.StartNew(() =>
            {
                stack.Push(1);
                stack.Push(2);
            });
        
            //t2,t3并行执行
            var t2 = t1.ContinueWith(t =>
            {
                int result;
                stack.TryPop(out result);
                Console.WriteLine("Task t2 result={0},Thread id {1}", result, Thread.CurrentThread.ManagedThreadId);
            });
        
            //t2,t3并行执行
            var t3 = t1.ContinueWith(t =>
            {
                int result;
                stack.TryPop(out result);
                Console.WriteLine("Task t3 result={0},Thread id {1}", result, Thread.CurrentThread.ManagedThreadId);
            });
        
            //等待t2和t3执行完
            Task.WaitAll(t2, t3);
        
            //t4串行执行
            var t4 = Task.Factory.StartNew(() =>
            {
                Console.WriteLine("当前集合元素个数：{0},Thread id {1}", stack.Count, Thread.CurrentThread.ManagedThreadId);
            });
            t4.Wait();

            Console.ReadKey();
            }*/


		//子任务
		/*public static void Main()
        {
            Task<string[]> parent = new Task<string[]>(state =>
            {
                Console.WriteLine(state);
                string[] result = new string[2];
                //创建并启动子任务
                new Task(() => { result[0] = "我是子任务1"; }, TaskCreationOptions.AttachedToParent).Start();   //TaskCreationOptions.AttachedToParent表示父任务等待所有子任务完成后整个任务才算完成
                new Task(() => { result[1] = "我是子任务2"; }, TaskCreationOptions.AttachedToParent).Start();
                return result;
            }, "我是父任务，并在我的处理过程中创建多个子任务，所有子任务完成以后我才会结束执行");
            //任务完成后执行
            parent.ContinueWith(t =>
            {
                Array.ForEach(t.Result, r => Console.WriteLine(r));
            });    
            parent.Start(); //启动父任务
            parent.Wait();//等待任务结束Wait只能等待父线程结束,没办法等到父线程的ContinueWith结束
            Console.ReadLine();
            }*/




		/*public static void Main()
        {
            Task<string[]> parent = new Task<string[]>(state =>
            {
                Console.WriteLine(state);
                string[] result = new string[2];
                //创建并启动子任务
                // new Task(() => { result[0] = "我是子任务1"; }, TaskCreationOptions.AttachedToParent).Start();   //TaskCreationOptions.AttachedToParent表示父任务等待所有子任务完成后整个任务才算完成
                // new Task(() => { result[1] = "我是子任务2"; }, TaskCreationOptions.AttachedToParent).Start();
                return result;
            }, "我是父任务，并在我的处理过程中创建多个子任务，所有子任务完成以后我才会结束执行");
            //任务完成后执行
            parent.ContinueWith(t =>
            {
                Array.ForEach(t.Result, r => Console.WriteLine(r));
            });    
            parent.Start(); //启动父任务
            parent.Wait();//等待任务结束Wait只能等待父线程结束,没办法等到父线程的ContinueWith结束
            Console.ReadLine();
            }*/

		/*取消任务
		private static int TaskMethod(string name, int seconds, CancellationToken token)
        {
            Console.WriteLine("Task {0} 正在运行,当前线程id {1}. Is thread pool thread: {2}",
                name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
            for (int i = 0; i < seconds; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(1));
                if (token.IsCancellationRequested) return -1;
            }
            return 42 * seconds;
        }
        private static void Main(string[] args)
        {
            var cts = new CancellationTokenSource();
            var longTask = new Task<int>(() => TaskMethod("Task 1", 10, cts.Token), cts.Token);
            Console.WriteLine(longTask.Status);
            cts.Cancel();
            Console.WriteLine(longTask.Status);
            Console.WriteLine("第一个任务在执行前已被取消");
            cts = new CancellationTokenSource();
            longTask = new Task<int>(() => TaskMethod("Task 2", 10, cts.Token), cts.Token);
            longTask.Start();
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
                Console.WriteLine(longTask.Status);
            }
            cts.Cancel();
            for (int i = 0; i < 5; i++)
            {
                Thread.Sleep(TimeSpan.FromSeconds(0.5));
                Console.WriteLine(longTask.Status);
            }
        
            Console.WriteLine("任务已完成,结果为 {0}.", longTask.Result);
        }
		*/

		/*多个任务
		static int TaskMethod(string name, int seconds)
        {
            Console.WriteLine("Task {0} is running on a thread id {1}. Is thread pool thread: {2}",
                name, Thread.CurrentThread.ManagedThreadId, Thread.CurrentThread.IsThreadPoolThread);
            Thread.Sleep(TimeSpan.FromSeconds(seconds));
            throw new Exception(string.Format("Task {0} Boom!", name));
            return 42 * seconds;
        }
        
        public static void Main(string[] args)
        {
            try
            {
                var t1 = new Task<int>(() => TaskMethod("Task 3", 3));
                var t2 = new Task<int>(() => TaskMethod("Task 4", 2));
                var complexTask = Task.WhenAll(t1, t2);
                var exceptionHandler = complexTask.ContinueWith(t =>
                        Console.WriteLine("Result: {0}", t.Result),
                        TaskContinuationOptions.OnlyOnFaulted
                    );
                t1.Start();
                t2.Start();
                Task.WaitAll(t1, t2);
            }
            catch (AggregateException ex)
            {
                ex.Handle(exception =>
                {
                    Console.WriteLine(exception.Message);
                    return true;
                });
            }
        }
		*/


		//Task.FromResult返回值
        class Program1
        {
            static IDictionary<string, string> cache = new Dictionary<string, string>()
            {
                {"0001","A"}, {"0002","B"}, {"0003","C"},
                {"0004","D"}, {"0005","E"}, {"0006","F"}
            };

           public static void Main()
            {
                Task<string> task = GetValueFromCache("0006");
                Console.WriteLine("主程序继续执行。。。。");
                string result = task.Result;
                Console.WriteLine("result={0}", result);
                Console.ReadKey();
            }
            private static Task<string> GetValueFromCache(string key)
            {
                Console.WriteLine("GetValueFromCache开始执行。。。。");
                string result = string.Empty;
                // Task.Delay(5000);
                Thread.Sleep(5000);
                Console.WriteLine("GetValueFromCache继续执行。。。。");
                if (cache.TryGetValue(key, out result))
                {
                    return Task.FromResult(result);
                }
                return Task.FromResult("");
            }
        }
    }
}