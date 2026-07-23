using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cloneable
{
    class Program
    {
        static void Main(string[] args)
        {
            Resume a = new Resume("大鸟");
            a.SetPersonalInfo("男", "29");
            a.SetWorkExperience("1998-2000", "xx公司");
            Resume b = (Resume)a.Clone();   //多态开箱深度复制
            b.SetWorkExperience("1998-2006", "YY公司");
            Resume c = (Resume)a.Clone();
            c.SetWorkExperience("1998-2003", "zz企业");

            a.Display();
            b.Display();
            c.Display();
            Console.Read();
        }
    }
}
