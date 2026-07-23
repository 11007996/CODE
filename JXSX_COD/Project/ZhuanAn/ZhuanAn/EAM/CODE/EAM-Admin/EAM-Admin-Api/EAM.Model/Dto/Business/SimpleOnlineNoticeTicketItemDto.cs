namespace EAM.Model.Dto
{
    /// <summary>
    /// 上线通知单_设备需求清单查询对象
    /// </summary>
    public class SimpleOnlineNoticeTicketItemQueryDto : PagerInfo
    {
    }

    /// <summary>
    /// 上线通知单_设备需求清单输入输出对象
    /// </summary>
    public class SimpleOnlineNoticeTicketItemDto
    {
        public string TicketNo { get; set; }

        public string ItemName { get; set; }

        public int NeedQty { get; set; }

        public bool IsReady { get; set; }
    }

}