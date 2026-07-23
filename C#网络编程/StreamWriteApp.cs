using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace StreamWriteApp
{
    class Program
	{
		public static void Main(string[] args)
		{
			StreamWriter sw = null;
            string strPath = @"C:\Users\mh.guo\Desktop\2.txt";
			try
			{
			    sw = new StreamWriter(strPath);
				sw.WriteLine("当前时间为{0}",DateTime.Now);
				Console.WriteLine("写文件成功！");
			}
			catch(Exception ex)
			{
				Console.WriteLine("写文件失败：{0}",ex.ToString());
			}
			finally
			{
				if(sw != null)
					sw.Close();
			}
		}
	}
}