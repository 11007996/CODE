using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decorator
{
    class ConcreteDecoratorA:Decorator
    {
        private string addedState;
        public override void Operation()
        {
            base.Operation();    //调用基类的方法
            addedState = "New State";
            Console.WriteLine("具体装饰对象A的操作");
        }
    }
}
