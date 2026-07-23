using System;
using System.Text;
using System.IO;
// using System.Text.Linq;
namespace File
{
	class Program
	{
		static void Main(string[] args)
		{
			FileStream fs;
			string filePath = @"C:\Users\mh.guo\Desktop\2.txt";
			try
			{
				fs = new FileStream(filePath,FileMode.Open,FileAccess.Read);
				}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				Console.WriteLine("文件打开失败");
				return;
			}
			int Length = Convert.ToInt32(fs.Length);
			byte[] bytes = new byte[Length];
			//当前文件指针在流的第4位
			fs.Position = 0;			
			//读取fs流1到18个字节，每个中文3个字节，返回字节长度
			long num = fs.Read(bytes,0,Length);
			//将字节转换为字符
            // Console.WriteLine(Encoding.UTF8.GetString(bytes));
            Console.WriteLine(Encoding.UTF8.GetString(bytes));
			Console.WriteLine("end of file");
			Console.WriteLine("Length:{0},num:{1}",Length,num);
			Console.ReadLine();
			fs.Close();	
		}
	}
}

