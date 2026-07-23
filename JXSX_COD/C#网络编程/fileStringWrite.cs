using System;
using System.Text;
using System.IO;

namespace FileStreamWrite
{
	class Program
	{
		static void Main(string[] args)
		{
		FileStream fs = null;
		string filePath = @"C:\Users\mh.guo\Desktop\2.txt";
		Encoding encoder = Encoding.UTF8;
		byte[] bytes = encoder.GetBytes("Hello world!\n");
		try 
		{
			//通过File类实例化FileStream
			// fs = File.OpenWrite(filePath);
			//直接实例化FileStream
			fs = new FileStream(filePath,FileMode.Append,FileAccess.Write);
			// fs.Position = fs.Length;
			fs.Write(bytes, 0, bytes.Length);
		}
		catch (Exception ex)
		{
			Console.WriteLine("文件打开失败{0}",ex.ToString());
		}
		finally
		{
			fs.Close();
		}
		Console.ReadKey();
		}
	}
}