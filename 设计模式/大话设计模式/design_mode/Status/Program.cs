using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace State
{
    class Program
    {
        static void Main(string[] args)
        {
            Context c = new Context(new ConcreteStateA());
            c.Request();    //同一对象在不同状态下执行不同中的程序
            c.Request();
            c.Request();
            c.Request();
            Console.Read();
        }
    }
}
