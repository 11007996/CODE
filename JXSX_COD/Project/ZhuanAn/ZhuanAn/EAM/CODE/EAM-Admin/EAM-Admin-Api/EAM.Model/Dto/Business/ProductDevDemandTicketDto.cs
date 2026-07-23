namespace EAM.Model.Dto
{
    /// <summary>
    /// 产品开发需求单查询对象
    /// </summary>
    public class ProductDevDemandTicketQueryDto : PagerInfo
    {
        public string TicketNo { get; set; }
        public string ProcessInstanceId { get; set; }
    }

    /// <summary>
    /// 产品开发需求单输入输出对象
    /// </summary>
    public class ProductDevDemandTicketDto
    {
        [Required(ErrorMessage = "业务编号不能为空")]
        public string TicketNo { get; set; }

        [Required(ErrorMessage = "发起人不能为空")]
        public string InitiatorId { get; set; }

        public string InitiatorName { get; set; }

        [Required(ErrorMessage = "客户ID不能为空")]
        public string CustomId { get; set; }

        [Required(ErrorMessage = "料号不能为空")]
        public int? PartId { get; set; }

        [Required(ErrorMessage = "预订单量不能为空")]
        public int OrderQty { get; set; }

        [Required(ErrorMessage = "需求时间不能为空")]
        public DateTime? NeedDate { get; set; }

        public string ProcessInstanceId { get; set; }

        public string EngineerId { get; set; }

        public string EngineerName { get; set; }

        public string EngineerLeaderId { get; set; }

        public string EngineerLeaderName { get; set; }

        public string LeaderId { get; set; }

        public string LeaderName { get; set; }

        public string Status { get; set; }

        [Required(ErrorMessage = "删除标志不能为空")]
        public int DelFlag { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }

        public string Remark { get; set; }

        public List<ProductDevDemandTicketItemDto> ProductDevDemandTicketItemNav { get; set; }

        [ExcelColumn(Name = "业务状态")]
        public string StatusLabel { get; set; }
    }
}