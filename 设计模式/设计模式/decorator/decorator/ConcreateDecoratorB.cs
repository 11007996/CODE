using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decorator
{
    class ConcreateDecoratorB:Decorator
    {
        public override void Operation()
        {
            base.Operation();   //调用基类的Operation函数
            AddedBehavior();
            Console.WriteLine("具体装饰对象B的操作");
        }
        private void AddedBehavior()
        {
            
        }
    }
}
