using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGeneric
{
    public class GenericMethod
    {
        /// <summary>
        /// 泛型方法
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="tParameter"></param>
        public static void Show<T>(T tParameter)
        {
            Console.WriteLine("This is {0},parameter={1},type={2}",
                typeof(GenericMethod), tParameter.GetType().Name, tParameter.ToString());
        }
    }
	
	class Program
    {
        static void Main(string[] args)
        {

            int iValue = 123;
            string sValue = "456";
            DateTime dtValue = DateTime.Now;
           
            GenericMethod.Show<int>(iValue);
            GenericMethod.Show<string>(sValue);
            GenericMethod.Show<DateTime>(dtValue);
            Console.ReadKey();
        }
    }
}