// 1.事件可以在不同类中传递信息，委托只是在同一类中
// 2.委托没有发布订阅概念，它只是委托执行某个方法；事件是利用了委托的特性，以一个类去触发另一个类执行某个方法，更多是起到通知的作用，通知另一个类执行特定的方法





using System;
using System.Threading;

namespace del
{
	public delegate void myDele(string message);
	class publish
	{
		public event myDele myEvent;
		public void pubMyDele(string message)
		{
			myDele dele = myEvent;
			if(dele!=null)
			{
				dele.Invoke(message);
			// dele(message);
			}
		}
	}

	class DyClass
	{
		public void showMes1(string message)
		{

			Console.WriteLine(DateTime.Now);
			Thread.Sleep(2000);
		}

		public void showMes2(string message)
		{

			Console.WriteLine(DateTime.Now);
			Thread.Sleep(2000);
		}
	}

	class Class1
	{
		static void Main()
		{
			publish p = new publish();
			DyClass d = new DyClass();
			p.myEvent += d.showMes1;
			p.myEvent += d.showMes2;
			p.pubMyDele("hello");
			Console.ReadKey();
		}
	}
	
}





//异步事件
/*using System;
using System.Threading.Tasks;
namespace del
{
	public delegate Task myDele(string message);
	class publish
	{
		public event myDele myEvent;
		public async Task pubMyDele(string message)
		{
			myDele dele = myEvent;
			if(dele!=null)
			{
				await dele.Invoke(message);
			// dele(message);
			}
		}
	}

	class DyClass
	{
		public async Task showMes(string message)
		{

			Console.WriteLine(DateTime.Now);
			await Task.Delay(2000);

		}
	}

	class Class1
	{
		static void Main()
		{
			publish p = new publish();
			DyClass d1 = new DyClass();
			DyClass d2 = new DyClass();
			p.myEvent += d1.showMes;
			p.myEvent += d2.showMes;
			p.pubMyDele("hello");
			Console.ReadKey();
		}
	}
	
}*/