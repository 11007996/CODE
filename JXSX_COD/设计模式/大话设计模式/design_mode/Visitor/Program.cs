using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Visitor   //访问者模式
{
    class Program
    {
        static void Main(string[] args)
        {
            ObjectStructure o = new ObjectStructure();  //通知总线类
            o.Attach(new ConcreteElementA());   //增加订阅者
            o.Attach(new ConcreteElementB());
            ConcreteVisitor1 v1 = new ConcreteVisitor1(); //状态变化者
            ConcreteVisitor2 v2 = new ConcreteVisitor2();
            o.Accept(v1);
            o.Accept(v2);   //通知者
            Console.Read();
        }
    }
}
