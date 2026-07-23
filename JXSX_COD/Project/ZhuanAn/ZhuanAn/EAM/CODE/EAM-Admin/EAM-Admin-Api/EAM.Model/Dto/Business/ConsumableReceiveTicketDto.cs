namespace EAM.Model.Dto
{
    /// <summary>
    /// 耗品领用单查询对象
    /// </summary>
    public class ConsumableReceiveTicketQueryDto : PagerInfo
    {
        public string TicketNo { get; set; }
        public string InitiatorId { get; set; }
        public int? LineId { get; set; }
        public string ProcessInstanceId { get; set; }
    }

    /// <summary>
    /// 耗品领用单输入输出对象
    /// </summary>
    public class ConsumableReceiveTicketDto
    {
        public string TicketNo { get; set; }

        [Required(ErrorMessage = "发起人不能为空")]
        public string InitiatorId { get; set; }

        public string InitiatorName { get; set; }

        [Required(ErrorMessage = "线别ID不能为空")]
        public int LineId { get; set; }

        public DateTime? NeedDate { get; set; }

        public string Purpose { get; set; }

        public string ProcessInstanceId { get; set; }

        public string Status { get; set; }

        [Required(ErrorMessage = "删除标志不能为空")]
        public int DelFlag { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }

        public string Remark { get; set; }

        public List<ConsumableReceiveTicketItemDto> ConsumableNav { get; set; }

        [ExcelColumn(Name = "发起人")]
        public string InitiatorIdLabel { get; set; }
    }

    /// <summary>
    /// 耗品领用单，批量领取
    /// </summary>
    public class BatchReceiveConsumableDto
    {
        public string TicketNo { get; set; }
        public List<OperateConsumableStorageDto> Consumables { get; set; }

        public string CreateBy { get; set; }
        public DateTime? CreateTime { get; set; }
    }
}