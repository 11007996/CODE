using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiManager.AssetSystem.Base
{
    /// <summary>
    /// 文件分类
    /// </summary>
    public enum FileClassEnum
    {
        [Description("")]
        None = 0,
        /// <summary>
        /// 设备操作SOP (Machine SOP)
        /// </summary>
        [Description("设备操作SOP")]
        MSOP = 1,
        /// <summary>
        /// 保养周期表(Machine Maintenance Interval)
        /// </summary>
        [Description("设备保养周期表")]
        MMI = 2,
        /// <summary>
        /// 保养作业标准书(Machine Maintenance Operate  Standard)
        /// </summary>
        [Description("设备保养作业标准书")]
        MMOS = 3,

    }
}
