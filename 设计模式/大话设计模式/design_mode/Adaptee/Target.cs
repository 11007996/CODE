using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adaptee
{
    class Target
    {
        public virtual void Request()
        {
            Console.WriteLine("普通请求");
        }
        public void 说话()
        {
            Console.WriteLine("中文方法");
        }
    }
}
