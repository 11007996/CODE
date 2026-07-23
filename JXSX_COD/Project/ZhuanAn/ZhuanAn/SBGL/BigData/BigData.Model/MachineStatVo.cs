using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BigData.Models
{
    /// <summary>
    /// 设备统计，大屏部门视图
    /// </summary>
    public class MachineStatVo
    {
        /// <summary>
        /// 实时报警设备
        /// </summary>
        public IList<WarnRepert> CurrWarnMachines { get; set; }

        /// <summary>
        /// 平均OEE图表
        /// </summary>
        public MachineAvgOEE MachineAvgOEE { get; set; }

        /// <summary>
        /// 设备OEE图表
        /// </summary>
        public ChartXY<string, decimal> MachineOEE { get; set; }

        /// <summary>
        /// 设备实时状态统计
        /// </summary>
        public IList<MachineCurrState> MachineCurrState { get; set; }

    }

    public class MachineAvgOEE
    {
        public decimal OEE { get; set; }
        public int Count { get; set; }
        public IList<PieData> Rate { get; set; }
    }


    public class MachineStat
    {
        public int MachineCode { get; set; }
        public string MachineName { get; set; }
        public string Line { get; set; }
        public decimal TheoryCT { get; set; }
        public decimal OEE { get; set; }
        public int ErrorCount { get; set; }
        public int ProductCount { get; set; }
        public int LastProductCount { get; set; }
        public int FailedCount { get; set; }
        public int RunState { get; set; }
        public int WarnState { get; set; }
        public decimal TimeUR { get; set; }
        public decimal EfficacyUR { get; set; }
        public decimal PassR { get; set; }
    }

    /// <summary>
    /// 实时运行状态
    /// </summary>
    public class MachineCurrState
    {
        public string StateName { get; set; }
        public int Count { get; set; }
        public string Color { get; set; }
        public int? Rate { get; set; }
    }




    /// <summary>
    /// 饼图数据模型
    /// </summary>
    public class PieData
    {
        public string name { get; set; }
        public decimal value { get; set; }
    }


    public class WarnRepert {
        public string CreateTime { get; set; }
        public string Line { get; set; }
        public string MachineName { get; set; }
        public string WarnCode { get; set; }
    }
}
