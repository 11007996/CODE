using System;
using System.Threading.Tasks;
using System.Linq;
namespace pro
{
	class Class1
	{
		static void Main(string[] args)
		{
			DisplayPrimesCountAsync();
			Console.ReadKey();
		}
		//使用await的函数一定要async修饰
		//await不能调用无返回值的函数
		static async Task DisplayPrimesCountAsync()
		{
    		int result = await GetPrimesCountAsync(2, 1000000);   //GetPrimesCountAsync异步执行，执行完成后返回值
    		Console.WriteLine(0);
    		Console.WriteLine(result);
		}

		static Task<int> GetPrimesCountAsync(int start, int count)
		{
			return Task.Run(() =>
				ParallelEnumerable.Range(start, count).Count(n =>
					Enumerable.Range(2, (int)Math.Sqrt(n) - 1).All(i => n % i > 0)));
		}
	}
}