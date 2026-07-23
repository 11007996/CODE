using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proxy
{
    class Proxy:GiveGift
    {
        Pursuit gg;    //通过Proxy类调用Pursuit类函数，即Proxy代理Pursuit
        public Proxy(SchoolGirl mm)
        {
            gg = new Pursuit(mm);
        }
        public void GiveDolls()
        {
            gg.GiveDolls();
        }
        public void GiveFlowers()
        {
            gg.GiveFlowers();
        }
        public void GiveChocolate()
        {
            gg.GiveChocolate();
        }
    }
}
