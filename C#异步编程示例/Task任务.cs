using System;
using System.Threading;
using System.Threading.Tasks;
namespace pro
{
	class Class1
	{
		static void Main(string[] args)
		{
			Task.Run(()=> Console.WriteLine(@"Hello from the thread{0} pool",i));
			Console.ReadKey();
		}
	}

}