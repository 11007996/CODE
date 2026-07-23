namespace EAM.Model.Dto
{
    /// <summary>
    /// 上线通知单_治具需求清单查询对象
    /// </summary>
    public class OnlineNoticeTicketFixtureQueryDto : PagerInfo
    {
    }

    /// <summary>
    /// 上线通知单_治具需求清单输入输出对象
    /// </summary>
    public class OnlineNoticeTicketFixtureDto
    {
        public string TicketNo { get; set; }

        [Required(ErrorMessage = "治具ID不能为空")]
        public int FixtureId { get; set; }

        public string FixtureName { get; set; }

        public string Series { get; set; }

        public decimal Price { get; set; }

        public int NeedQty { get; set; }
    }

    /// <summary>
    /// 上线通知单治具需求清单概要
    /// </summary>
    public class OnlineNoticeTicketFixtureSummaryDto
    {
        public string TicketNo { get; set; }
        public string TicketType { get; set; }

        public List<FixtureDemandDto> DemandList { get; set; }

        public List<FixtureReceiveDto> ReceiveList { get; set; }

        public List<FixtureStorageRecordDto> StorageRecordList { get; set; }
    }

    public class FixtureDemandDto
    {
        public string FixtureName { get; set; }
        public int NeedQty { get; set; }
    }

    public class FixtureReceiveDto
    {
        public int FixtureId { get; set; }
        public string Series { get; set; }
        public string FixtureName { get; set; }
        public int ReceiveQty { get; set; }
        public int BackQty { get; set; }
        public int UsingQty { get; set; }
    }

    /// <summary>
    /// 上线通知单，批量领取
    /// </summary>
    public class BatchReceiveFixtureDto
    {
        public string TicketNo { get; set; }
        public string TicketType { get; set; }
        public List<OperateFixtureStorageDto> Fixtures { get; set; }
        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}