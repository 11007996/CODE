using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EAM.Model.Constant
{
    /// <summary>
    /// 业务单据类型常量
    /// </summary>
    public class TicketTypeConstant
    {
        public const string 上线通知单 = "OnlineNoticeTicket";
        public const string 上线通知单_简单 = "SimpleOnlineNoticeTicket";
        public const string 耗品领用单 = "ConsumableReceiveTicket";
        public const string 新产品开发治具需求单 = "ProductDevDemandTicket";
        public const string 治具尺寸量测单 = "SizeMeasureTicket";
        public const string 产品量测单 = "ProdMeasureTicket";
    }
}
