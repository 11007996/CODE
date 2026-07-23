using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Listen.Base
{
    enum OperateCode : byte
    {
        /// <summary>
        /// 不执行任何检查操作
        /// </summary>
        NONE = 0,
        /// <summary>
        /// 检查保养状态
        /// </summary>
        Maintenance = 1
    }
}
