using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy
{
    interface GiveGift     //代理类与实际执行者都实现这个接口
    {
        void GiveDolls();
        void GiveFlowers();
        void GiveChocolate();
    }
}
