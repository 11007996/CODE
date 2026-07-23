using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listen.Base
{
    public enum MaintenanceCode
    {
        /// <summary>
        /// 保养正常
        /// </summary>
        OK = 10,
        /// <summary>
        /// 未做日保养
        /// </summary>
        DAY_FALSE = 1,
        /// <summary>
        /// 未做周保养
        /// </summary>
        WEEK_FALSE = 2,
        /// <summary>
        /// 未做月保养
        /// </summary>
        MONTH_FALSE = 3,
    }
}
