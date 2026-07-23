using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asset.Model
{
    public class MaintenanceReportDO
    {
        public string AssetNo { get; set; }

        public int Year { get; set; }

        public int? Month { get; set; }
    }
}
