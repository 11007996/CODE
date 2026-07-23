using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComTools.Automation
{
    /// <summary>
    /// 扩展操作类型枚举
    /// </summary>
    public enum OperateTypeEnum
    {
        [Description("无")]
        NONE,
        [Description("单击")]
        MOUSE_CLICK,
        [Description("打开应用")]
        OPEN_APP,
        [Description("关闭窗口")]
        CLOSE_WINDOW,
    }

}
