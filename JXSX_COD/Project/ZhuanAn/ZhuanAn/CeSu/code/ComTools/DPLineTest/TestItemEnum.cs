using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComTools.DPLineTest
{
    /// <summary>
    /// 测试项目枚举
    /// </summary>
    public enum TestItemEnum
    {
        [Description("屏幕分辨率")]
        Screen_Resolution,
        [Description("屏幕刷新率")]
        Screen_Refresh_Rate,
        [Description("输出颜色格式")]
        Output_Color_Format,
        [Description("磁盘写入速度")]
        Disk_Write_Speed,
        [Description("磁盘读取速度")]
        Disk_Read_Speed,
    }
}
