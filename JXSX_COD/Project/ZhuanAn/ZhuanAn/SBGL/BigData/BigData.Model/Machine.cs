using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigData.Models
{
    public class Machine
    {
        public int MachineCode { get; set; }
        public string Line { get; set; }
        public string MachineName { get; set; }
        public int WarnState { get; set; }
        public string RunState { get; set; }
        public int ProductCount { get; set; }
        public IList<MachineReport> Reports { get; set; }

    }

    public class MachinePower
    {
        public int MachineCode { get; set; }
        public decimal Power { get; set; }


    }
}
