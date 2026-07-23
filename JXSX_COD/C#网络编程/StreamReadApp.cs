using System;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace StreamReadApp
{
    class Program
	{
		public static void Main(string[] args)
		{
			string s = "";
            StreamReader sr = new StreamReader(@"C:\Users\mh.guo\Desktop\2.txt");
			s = sr.ReadToEnd();
            sr.Close();
			Console.WriteLine(s);
			Console.ReadKey();
		}
	}
}