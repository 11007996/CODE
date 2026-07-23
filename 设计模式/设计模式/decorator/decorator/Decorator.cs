using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace decorator
{
    class Decorator:Component
    {
        protected Component component;
        //装饰类，转换执行参数类的函数
        public void SetComponent(Component component)     //参数是哪个类，则Operation函数就执行谁
        {
            this.component = component;
        }

        public override void Operation()
        {
            if(component != null)
            {
                component.Operation();
            }
        }
    }
}
