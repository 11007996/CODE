using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactoryFun
{
    class Program
    {
        static void Main(string[] args)
        {
            IFactory factory = new UndergraduateFactory();   //工厂方法模式
            LeiFeng student = factory.CreateLeiFeng();   //可能实例化多个学雷锋的学生
            student.BuyRice();
            student.Sweep();
            student.Wash();
            Console.Read();
        }
    }
}
