using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listen.Model
{
    public class MaintenanceStatus
    {
        /// <summary>
        /// 设备编码
        /// </summary>
        public int MachineCode { get; set; }
        /// <summary>
        /// 资产编号
        /// </summary>
        public string AssetNo { get; set; }
        /// <summary>
        /// 有无日保养
        /// </summary>
        public bool DayMaintenanceFlag { get; set; }
        /// <summary>
        /// 有无周保养
        /// </summary>
        public bool WeekMaintenanceFlag { get; set; }
        /// <summary>
        /// 有无月保养
        /// </summary>
        public bool MonthMaintenanceFlag { get; set; }
    }
}
