using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyFactory
{
    class Program
    {
        static void Main()
        {
            //简单工厂模式
            Operation operation =OperationFactory.createOperate("+");    //如果需要增加其它运算模式，只需修改工厂类及运算类
            operation.NumberA = 20;
            operation.NumberB = 50;
            double result = operation.GetResult();
            Console.WriteLine("NumberA + NumberB = {0}", result);
            Console.ReadKey();
        }
    }
}