using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decorator
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcreateComponent c = new ConcreateComponent();
            ConcreateDecoratorA d1 = new ConcreateDecoratorA();
            ConcreateDecoratorB d2 = new ConcreateDecoratorB();

            d1.SetComponent(c);   //装饰模式，通d1调用c的函数
            d2.SetComponent(d1);   //装饰模式，通d2调用d1的函数
            d2.Operation();

            Console.Read();
        }
    }
}
