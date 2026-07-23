using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator
{
    abstract class Country
    {
        protected UnitdeNations mediator;
        public Country(UnitdeNations mediator)
        {
            this.mediator = mediator;
        }
    }
}
