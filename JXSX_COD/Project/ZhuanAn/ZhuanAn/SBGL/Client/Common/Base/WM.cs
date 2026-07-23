using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Base
{
    //window message
    public class WM
    {
        public const int DBT_DEVNODES_CHANGED = 0x0007;//已向系统添加或删除设备。
        public const int WM_DEVICE_CHANGE = 0x219;//设备改变
        public const int DBT_DEVICEARRIVAL = 0x8000;//设备插入
        public const int DBT_DEVICE_REMOVE_COMPLETE = 0x8004;//设备移除
    }
}
