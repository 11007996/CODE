using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAM.Model.Constant
{
    public class TaskStatusConstant
    {
        //未查看
        public const string 待处理 = "Pending";

        //已查看
        public const string 处理中 = "Handling";

        //已提交
        public const string 已完成 = "Completed";

        //已终止
        public const string 已取消 = "Canceled";
    }
}