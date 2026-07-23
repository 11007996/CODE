using System;
using System.Collections.Generic;
using System.Collections;     //枚举器接口命名空间
using System.Xml.Linq;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
// using System.Console;


namespace MyGeneric
{
	 class ColorEnumerator: IEnumerator    //枚举器接口
	{
		string[] colors;
		int position = -1;
		
		public ColorEnumerator(string[] theColors)
		{
			colors = new string[theColors.Length];
			for (int i = 0; i < theColors.Length; i++ )
			{
				colors[i] = theColors[i];
			}
		}
		
		public object Current            //实现当前位置属性
		{
			get 
			{
				if (position == -1)
				{
					throw new InvalidOperationException();
				}
				if (position >= colors.Length)
				{
					throw new InvalidOperationException();
				}
				return colors[position];
			}
		}
		
		public bool MoveNext()          //实现下一位置方法
		{
			if (position <= colors.Length - 1)
			{
				position++;
				return true;
			}
			else
			{
				return false;
			}
		}
		
		public void Reset()       //实现重置位置方法
		{
			position = -1;
		}
	}
	
	class Spectrum : IEnumerable   //枚举器接口
	{
		string[] colors = {"violet","blue","cyan","green","yellow","orange","red"};
		public IEnumerator GetEnumerator()          //实现启动枚举器方法
		{
			return new ColorEnumerator(colors);
		}
	}
	
	class Program
	{
		static void Main()
		{
			Spectrum spectrum = new Spectrum();
			foreach(string color in spectrum)
			Console.WriteLine(color);
		}
	}
	
	
}




namespace MyGeneric    //迭代器
{

class Program
{
    static void Main() 
	{
        foreach (int fib in Fibs(20))           //foreach是IEnumerable的消费者
		{
           Console.Write (fib + " ");
        }
    }
    static IEnumerable<int> Fibs (int fibCount)
	{
        for (int i = 0; i < fibCount; )
		{
            yield return i;          //返回IEnumerable对象，yield是IEnumerable的生成器
            i += 2;
            
        }
    }
}
}






/*
namespace MyGeneric
{
    class MyClass
	{
		public IEnumerator<string> GetEnumerator()   //2.调用BlackAndWhite方法
		{
			return BlackAndWhite();
		}
		
		public IEnumerator<string> BlackAndWhite()   //3.返回IEnumerator对象
		{
			yield return "black";
			yield return "gray";
			yield return "white";
		}
	}
	
	class Program
	{
		static void Main()
		{
			MyClass mc = new MyClass();
			
			foreach(string shade in mc)     //1.foreach检测mc是否实现GetEnumerator方法      //4.遍历IEnumerator对象
			{
				Console.WriteLine(shade);
			}
		}
	}
}
*/
