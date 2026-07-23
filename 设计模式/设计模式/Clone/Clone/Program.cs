using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Clone
{
    class Program
    {
        static void Main(string[] args)
        {
            Resume a = new Resume("大鸟");
            a.SetPersonalInfo("男", "29");
            a.SetWorkExperience("1998-2000", "XXX公司");

            Resume b = (Resume)a.Clone();   //复制a实例给b
            a.SetWorkExperience("1998-2006", "YY公司");

            Resume c = (Resume)a.Clone();    //克隆对于值类型进行深复制，对引用类型进行浅复制，当属性为类等引用类型，此处错误
            c.SetPersonalInfo("男", "24");

            a.Display();
            b.Display();
            c.Display();

            Console.ReadKey();
        }
    }
}
