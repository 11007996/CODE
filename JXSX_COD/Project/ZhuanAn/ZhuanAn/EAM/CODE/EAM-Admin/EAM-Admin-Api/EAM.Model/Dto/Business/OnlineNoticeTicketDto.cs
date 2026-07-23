namespace EAM.Model.Dto
{
    /// <summary>
    /// 上线通知单查询对象
    /// </summary>
    public class OnlineNoticeTicketQueryDto : PagerInfo
    {
        public string TicketNo { get; set; }
        public string InitiatorId { get; set; }
        public int? LineId { get; set; }
        public string ProcessInstanceId { get; set; }
    }

    /// <summary>
    /// 上线通知单输入输出对象
    /// </summary>
    public class OnlineNoticeTicketDto
    {
        public string TicketNo { get; set; }

        [Required(ErrorMessage = "发起人ID不能为空")]
        public string InitiatorId { get; set; }

        [Required(ErrorMessage = "发起人名称不能为空")]
        public string InitiatorName { get; set; }

        [Required(ErrorMessage = "料号不能为空")]
        public int? PartId { get; set; }

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

        public List<OnlineNoticeTicketEquipmentDto> EquipmentNav { get; set; }

        public List<OnlineNoticeTicketFixtureDto> FixtureNav { get; set; }

        [ExcelColumn(Name = "业务状态")]
        public string StatusLabel { get; set; }
    }
}