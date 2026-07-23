using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adaptee  //适配器模式
{
    class Program
    {
        static void Main(string[] args)
        {
            Target target = new Adapter();
            target.Request();  //统一接口，在一个类中调用另一个类的方法，在无法维护类方法的时候考虑使用
            target.说话();  //方法名是中文
            Console.Read();
        }
    }
}
