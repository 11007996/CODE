using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thisFunction
{
    class Program
    {
        static void Main(string[] args)
        {
            CC c = new CC();
            c.Show("hello");
            c.ThisShow("拓展方法");

            Console.ReadKey();
        }
    }
}
