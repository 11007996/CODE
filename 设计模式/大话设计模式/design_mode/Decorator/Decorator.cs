using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decorator
{
    abstract class Decorator : Component
    {
        protected Component component;
        public void SetComponent(Component component)   //把基类传参
        {
            this.component = component;
        }

        public override void Operation()
        {
            if(component != null)
            {
                component.Operation();    //调用基类的方法
            }
        }
    }
}
