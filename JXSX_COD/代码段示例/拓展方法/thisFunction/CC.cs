using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thisFunction
{
    class CC
    {
        public void Show(string message)
        {
            Console.WriteLine(message);
        }
    }

    //拓展方法要求静态类及静态方法
    static class ThisCC
    {
        public static void ThisShow(this CC c, string message)   //this 表示拓展方法，在不方便修改CC类的情况下添加ThisShow方法
        {
            Console.WriteLine(message + " ThisCC");
        }
    }
}
