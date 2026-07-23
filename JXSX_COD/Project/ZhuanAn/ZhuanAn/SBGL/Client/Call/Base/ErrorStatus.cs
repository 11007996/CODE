using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Call.Base
{
    /// <summary>
    ///  A待处理(呼叫) B处理中 C请求支援 D支援处理中 E完成待确认 N解除呼叫 Y已完成 
    /// </summary>
    public enum ErrorStatus
    {
        A,
        B,
        C,
        D,
        E,
        N,
        Y
    }
}
