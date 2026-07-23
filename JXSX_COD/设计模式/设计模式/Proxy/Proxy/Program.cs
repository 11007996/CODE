using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy
{
    /// <summary>
    /// 代理模式，在代理类中调用实际执行的类函数
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            SchoolGirl jiaojiao = new SchoolGirl();
            jiaojiao.Name = "李娇娇";

            Proxy daili = new Proxy(jiaojiao);   //代理类
            daili.GiveDolls();
            daili.GiveChocolate();
            daili.GiveFlowers();

            Console.ReadKey();
        }
    }
}
