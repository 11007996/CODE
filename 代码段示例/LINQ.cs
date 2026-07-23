using System;
using System.Linq;
namespace pore
{
	 class pro
	 {
		 static void Main()
		 {
			 int[] arr = new int[]{1,2,3,4,5,6,7};
			 int[] arr2 = new int[]{6,7,8,9,10,11};
			 int[] arr3 = new int[]{2,7,5,9,6,11,5};
			 string ab = "dkjfeisdf";
			/* var a = from int n in arr
			         where n > 3
					 // orderby n descending
					 select n;*/
		    // var a = arr.Skip(5);//跳过前5位对象
			// var a = arr.Take(5);//取前5位对象
			// var a = arr.TakeWhile(p => p<4);//返回遇到flase就停止获取对象
			// var a = arr.SkipWhile(p => p<4);//返回遇到false就开始获取对象
			//// var a = arr.Select(p => p>3).Count;
			// var a = arr.Where(p => p>3);//根据条码查找
			//// var a = arr.Select();//选择对象的某一部分
			//// var a = arr.Join(arr2);//对两个序列执行内联结
			// var a = arr.Concat(arr2);//合并两个序列
			////var a = arr.OrderBy();//元素排序
			// var a = arr.Reverse();//反转排序
			// var a = arr.GroupBy();//元素分组
			// var a = arr3.Distinct();//去除重复项
			// var a = arr.Union(arr3);//两个序列并集且没有重复项
			// var a = arr.Intersect(arr3);//两个序列交集
			// var a = arr3.Except(arr2);//返回第一个序列中不重复的元素减去同样位于第二个序列中的元素
			// var a = arr.AsEnumerable();//将序列作为IEnumerable<TSource>返回
			// var a = ab.ToArray();//将序列作为数组返回
			// var a = ab.ToList();//将序列作为List<T>返回
			// var a = arr.ToDictionary();//将序列作为Dictionart<TKey,TElement>返回
			// var a = arr.ToLookUp();//将序列作为LoopUp<TKey,TElement>返回
			// var a = arr.OfType();//所返回的序列中的元素是指定的类型
			// var a = arr.OfType();//将序列中的所有元素强制转换为给定的类型
			// var a = arr.SequenceEqual(arr2);//返回两个序列是否相等
			// var a = arr.First(p => p==1);//返回第一个匹配的元素，没匹配到则抛出错误
			// var a = arr.FirstOrDefault(p => p==2);//返回第一个匹配的元素，没有给定谓词则返回第一个元素，没有匹配到则返回类型的默认值
			// var a = arr.Last();//返回序列的最后一个元素，没有匹配到则报错
			// var a = arr.LastOrDefault();//返回最后一个元素，没有匹配到则返回类型的默认值
			// var a = arr3.Single(p => p==2);//返回匹配到的单 个元素，如果匹配不到或匹配到多个则报错
			 // var a = arr3.SingleOrDefault(p => p==2);//返回匹配到的单 个元素，如果匹配不到或匹配到多个则返回类型默认值
			 // var a = arr.ElementAt(4);//返回序列第n+1个元素
			 // var a = arr.ElementAtOrDefault(5);//返回序列第n+1个元素,超出索引范围就返回类型默认值
			 // var a = arr.DefaultIfEmpty(4);//提供一个在序列为空时的默认值
			 ////var a = Range(start,count);//返回一个序列以start为第一个元素，序列长度为count，每个元素+1
			 // var a = Repeat(2,3);//给定一个T类型的element和一个count整数，该方法返回的序列具有count个element副本
			 // var a = Empty();//返回给定类型T的空序列
			 // var a = arr.Any(p => p>3);//返回布尔值，是否包含满足条件的元素
			 // var a = arr.All(p => p>0);//返回布尔值，是否所有元素都满足条件
			 // var a = arr.Contains(3);//返回布尔值，是否包含给定的元素
			 // var a = arr.Count();//返回序列的元素的个数
			 // var a = arr.Sum();//返回序列中值的总和
			 // var a = arr.Min();//返回序列中最小的值
			 // var a = arr.Max();//返回序列中最大的值
			 // var a = arr.Average();//返回序列中平均值
			//// var a = arr.Aggregate(Sum());//连续对序列中的各个元素应用给定的函数
			foreach(int aa in a)
			{
				Console.WriteLine(aa);
			}
			    // Console.WriteLine(a);
				Console.ReadKey();
			
		 }
	 }
}