using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAM.Model.Constant
{
    /// <summary>
    /// 流程实例状态常量
    /// </summary>
    public class ProcessStatusConstant
    {
        public const string 新建 = "Created";
        public const string 进行中 = "Active";
        public const string 暂停 = "Suspended";
        public const string 已终止 = "Terminated";
        public const string 已完成 = "Completed";
    }
}
