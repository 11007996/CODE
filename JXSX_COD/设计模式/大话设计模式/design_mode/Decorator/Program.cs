using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decorator    //装饰模式
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcreteComponent c = new ConcreteComponent();
            ConcreteDecoratorA d1 = new ConcreteDecoratorA();
            ConcreteDecoratorB d2 = new ConcreteDecoratorB();

            d1.SetComponent(c);     //通过一层调用上一层基类，就可以像穿衣服一层套一层，灵活套用不同的类实现
            d2.SetComponent(d1);
            d2.Operation();   //多米诺骨牌触发点

            Console.Read();
        }
    }
}
