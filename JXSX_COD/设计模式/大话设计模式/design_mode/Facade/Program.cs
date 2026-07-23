using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Facade
{
    class Program
    {
        static void Main(string[] args)
        {
            Facade facade = new Facade();  //外观模式，用一个类调用其它类方法，类似于投资基金由基金经理而不是亲自管理所有股票
            facade.MethodA();
            facade.MethodB();
            Console.Read();
        }
    }
}
