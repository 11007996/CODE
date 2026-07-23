using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Asset.Model.Enum
{
    /// <summary>
    /// 资产相关文件分类枚举
    /// </summary>
    public enum FileClassEnum
    {
        None = 0,
        /// <summary>
        /// 设备操作手册(Machine SOP)
        /// </summary>
        MSOP = 1,
        /// <summary>
        /// 保养周期表(Machine Maintenance Interval)
        /// </summary>
        MMI = 2,
        /// <summary>
        /// 保养作业标准书(Machine Maintenance Operate  Standard)
        /// </summary>
        MMOS = 3
    }
}
