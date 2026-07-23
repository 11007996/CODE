using System;
namespace pro
{
	class diaoyon
	{
		public delegate void mydel(string str);    //定义一个公共委托
		public event mydel myEvent;   //通过委托定义一个事件
		public void mydiaoEvent(string str)
		{
			myEvent(str);    //调用事件
		}
	}
 
	class Class1
	{
		static void Main()
		{
			diaoyon d = new diaoyon();
			class2 c = new class2();
			d.myEvent += c.showMes;   //事件订阅文件

			d.mydiaoEvent("hello");    //调用事件
		}
	}

	class class2
	{
		public void showMes(string mes)
		{
			Console.WriteLine(mes);
			Console.ReadKey();
		}
	}
}