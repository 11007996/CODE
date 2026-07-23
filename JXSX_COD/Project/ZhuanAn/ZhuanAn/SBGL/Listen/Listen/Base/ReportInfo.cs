using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listen.Base
{
    public class ReportInfo
    {
        public int MachineCode { get; set; }
        public int LineCode { get; set; }
        public int ProductCount { get; set; }
        public int FailedCount { get; set; }
    }
}
