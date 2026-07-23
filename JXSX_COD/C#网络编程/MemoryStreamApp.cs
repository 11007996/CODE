using System.Text;
using System;
using System.IO;

namespace MemoryStreamApp
{
	class Program
	{
		static void Main(string[] args)
		{
			MemoryStream mem = new MemoryStream();
			Console.WriteLine("初始分配容量：{0}",mem.Capacity);
			Console.WriteLine("初始使用量：{0}",mem.Length);
			UnicodeEncoding encoder = new UnicodeEncoding();
			Byte[] bytes = encoder.GetBytes("新增数据");
			for (int i = 0; i < 4;i++)
			{
				Console.WriteLine("第{0}写入新数据",i);
				mem.Write(bytes,0,bytes.Length);
			}
			Console.WriteLine("当前分配容量：{0}",mem.Capacity);
			Console.WriteLine("当前使用量：{0}",mem.Length);
			Console.ReadKey();
			
		}
	}
}