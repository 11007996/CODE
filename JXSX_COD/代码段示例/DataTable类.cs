using System;
using System.Text;
// using System.Collections.Generic;
// using System.Threading;
using System.Data;
// using System.IO;
namespace sanguo
{
	class Program
	{
// static void Main(string[] args)
// {
	// DataTable dt = new DataTable();
	// dt.Columns.Add("Id");
	// dt.Columns.Add("Name");
	// dt.Columns.Add("Age",typeof(Int32));
	// dt.Rows.Add("1","刘备",31);
	// dt.Rows.Add("2","关羽",29);
	// dt.Rows.Add("3","张飞",28);
	// dt.Rows.Add("4","赵云",27);
	// dt.Rows.Add("5","黄忠",50);
	// dt.DefaultView.Sort = "Age DESC";
	// dt = dt.DefaultView.ToTable();
	// DataRow[] drArr = dt.Select("Id > 1 and Name <>'关羽'","Id desc");
	// foreach(DataRow dr in drArr)
	// {
		// Console.WriteLine(dr["Name"]);
	// }
	// object obj = dt.Compute("Count(Id)","Age > 26");
	// Console.WriteLine(obj.ToString());
	// object obj2 = dt.Compute("Avg(Age)","true");
	// Console.WriteLine(obj2.ToString());
	// object obj3 = dt.Compute("Sum(Age)","true");
	// Console.WriteLine(obj3.ToString());
	
	// Console.ReadKey();
// }



    static void Main(string[] args)
	{
		DataTable dt = new DataTable();
		DataColumn dc = dt.Columns.Add("Id",Type.GetType("System.Int32"));
		dc.AutoIncrement = true;
		dc.AutoIncrementSeed = 1;
		dc.AutoIncrementStep = 1;
		dc.AllowDBNull = false;
		
		dt.Columns.Add("Name",Type.GetType("System.String"));
		dt.Columns.Add("Age",typeof(Int32));
		dt.Rows.Add("1","刘备",31);
	    dt.Rows.Add("2","关羽",29);
	    dt.Rows.Add("3","张飞",28);
	    dt.Rows.Add("4","赵云",27);
	    dt.Rows.Add("5","黄忠",50);
		
		foreach(DataRow dr in dt.Rows)
		{
			Console.WriteLine(dr["Name"] & dr["Age"]);
		}
		Console.ReadKey();
	}
	}
}