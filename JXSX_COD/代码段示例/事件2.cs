using System;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
// using static System.Console;


namespace MyGeneric
{
	
   class delegatebase       //发布者
   {
	   public delegate void OtherDel(int a,int b);       //声明一个委托
		public event OtherDel myDel;                     //发布一个事件
		public void delegatefun(int a ,int b)            //触发事件的方法
		{
			if (myDel != null)          //检测事件是否订阅
				myDel(a,b);            //激发事件
			    
		}
   }
   class dyzhe        //订阅者
   {
	   
	   public void addab(int a,int b)         //事件关联方法
	   {
		   int c = a + b;
		   Console.WriteLine("Result is: {0}",c);
	   }
   }
	 class Program
	{	
			static void Main() 
			{
			    delegatebase fabu = new delegatebase();          //实例委托类
                dyzhe dy = new dyzhe();                //实例订阅类
				fabu.myDel += new delegatebase.OtherDel(dy.addab);       //事件订阅方法
                int aa = Convert.ToInt16(Console.ReadLine());          //输入参数
                int bb = Convert.ToInt16(Console.ReadLine());              //输入参数
				fabu.delegatefun(aa,bb);                       //激发事件
				Console.ReadKey();
			}
			
	}
}
