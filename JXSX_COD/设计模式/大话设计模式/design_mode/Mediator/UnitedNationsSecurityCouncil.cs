using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mediator
{
    class UnitedNationsSecurityCouncil:UnitdeNations
    {
        private USA colleague1;
        private Iraq colleague2;
        public USA Colleagu1
        {
            set { colleague1 = value; }
        }
        public Iraq Colleagu2
        {
            set { colleague2 = value; }
        }
        public override void Declare(string message, Country colleague)
        {
            if(colleague == colleague1)
            {
                colleague2.GetMessage(message);
            }
            else
            {
                colleague1.GetMessage(message);
            }
        }
    }
}
