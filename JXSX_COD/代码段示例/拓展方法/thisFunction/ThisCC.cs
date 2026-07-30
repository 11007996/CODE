using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thisFunction
{
    static class ThisCC
    {
        public static void ThisShow(this CC c, string message)
        {
            Console.WriteLine(message + " ThisCC");
        }
    }
}
