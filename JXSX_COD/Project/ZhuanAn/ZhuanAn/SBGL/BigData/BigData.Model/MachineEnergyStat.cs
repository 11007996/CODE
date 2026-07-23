using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigData.Models
{
    public class MachineEnergyStat
    {
        public EnergyItem day90 { get; set; }
        public EnergyItem day30 { get; set; }
        public EnergyItem day1 { get; set; }
    }

    public class EnergyItem{
        public int Hour{get;set;}
        public int Energy{get;set;}
    }
}
