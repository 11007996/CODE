using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace easyFactory
{
    class OperationDiv:Operation   //继承
    {
        public override double GetResult()   //重写虚方法
        {
            double result = 0;
            result = NumberA / NumberB;
            return result;
        }
    }
}
