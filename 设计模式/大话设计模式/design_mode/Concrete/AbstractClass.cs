using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concrete
{
    abstract class AbstractClass
    {
        public abstract void PrimitiveOperation1();
        public abstract void PrimitiveOperation2();

        public void TemplateMethod()   //模板方法
        {
            PrimitiveOperation1();    //抽象方法，不同类调用相同的方法实现不同的算法
            PrimitiveOperation2();
            Console.WriteLine("");
        }
    }
}
