using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator
{
    class Program
    {
        static void Main(string[] args)
        {
            UnitedNationsSecurityCouncil UNSC = new UnitedNationsSecurityCouncil();   //中介者
            USA c1 = new USA(UNSC);
            Iraq c2 = new Iraq(UNSC);
            UNSC.Colleagu1 = c1;
            UNSC.Colleagu2 = c2;
            c1.Declare("不准研制核武器，否则要改动战争");
            c2.Declare("我们没有核武器，也不怕侵略");
            Console.Read();
        }
    }
}
