using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComTools.Automation
{
    /// <summary>
    /// 运算符枚举
    /// </summary>
    public enum OperatorEnum
    {
        [Description("=")]
        等于,
        [Description("!=")]
        不等于,
        [Description(">")]
        大于,
        [Description(">=")]
        大于等于,
        [Description("<")]
        小于,
        [Description("<=")]
        小于等于,
    }
}
