using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAM.Model.Constant
{
    /// <summary>
    /// 设备状态
    /// </summary>
    public class EquipmentStatusConstant
    {
        public const string 正常 = "Normal";
        public const string 闲置 = "Idle";
        public const string 占用 = "Using";
        public const string 报废 = "Scrap";
    }
}
