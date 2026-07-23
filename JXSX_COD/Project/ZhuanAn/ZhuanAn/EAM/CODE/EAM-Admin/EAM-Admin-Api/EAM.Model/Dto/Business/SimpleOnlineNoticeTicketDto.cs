namespace EAM.Model.Dto
{
    /// <summary>
    /// 上线通知单查询对象
    /// </summary>
    public class SimpleOnlineNoticeTicketQueryDto : PagerInfo
    {
        public string TicketNo { get; set; }
        public string InitiatorId { get; set; }
        public int? LineId { get; set; }
        public string NewPartName { get; set; }
        public string ProcessInstanceId { get; set; }
    }

    /// <summary>
    /// 项目字典查询
    /// </summary>
    public class SimpleOnlineNoticeTicketDictQueryDto : PagerInfo
    {
        public string Keyword { get; set; }
    }

    /// <summary>
    /// 上线通知单输入输出对象
    /// </summary>
    public class SimpleOnlineNoticeTicketDto
    {
        public string TicketNo { get; set; }

        [Required(ErrorMessage = "发起人ID不能为空")]
        public string InitiatorId { get; set; }

        [Required(ErrorMessage = "发起人名称不能为空")]
        public string InitiatorName { get; set; }

        [Required(ErrorMessage = "新上线料号")]
        public string NewPartName { get; set; }

        public string OldPartName { get; set; }

        [Required(ErrorMessage = "生产数量不能为空")]
        public int? ProductQty { get; set; }

        [Required(ErrorMessage = "产线不能为空")]
        public int? LineId { get; set; }

        public string LineName { get; set; }

        public DateTime? NeedTime { get; set; }

        public string Status { get; set; }

        public string ProcessInstanceId { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }

        public List<SimpleOnlineNoticeTicketItemDto> ItemNav { get; set; }
    }
}