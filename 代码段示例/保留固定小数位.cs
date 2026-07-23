using System;
namespace Dd
{
	class df
	{
		static void Main()
		{
			string msg = "20.1kg";
			msg=msg.Replace("kg","");
			if (msg.Contains("."))
			{
				Console.WriteLine(msg);

					string[] StrNew = msg.Split(new char[] { '.' });
					string s1 = StrNew[1].ToString().PadRight(3, '0');
					msg = StrNew[0] + "." + s1;
					Console.WriteLine(msg);
			}
			else
				{
					msg += ".000";
					Console.WriteLine(msg);
				}
			Console.ReadKey();
		}
	}
}