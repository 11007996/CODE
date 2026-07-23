namespace EAM.Model.Dto
{
    /// <summary>
    /// 产品测量报告查询对象
    /// </summary>
    public class ProdMeasureTicketQueryDto : PagerInfo
    {
        public string TicketNo { get; set; }
        public string ProcessInstanceId { get; set; }
    }

    /// <summary>
    /// 产品测量报告输入输出对象
    /// </summary>
    public class ProdMeasureTicketDto
    {
        [Required(ErrorMessage = "业务编号不能为空")]
        public string TicketNo { get; set; }

        [Required(ErrorMessage = "发起人不能为空")]
        public string InitiatorId { get; set; }

        public string InitiatorName { get; set; }

        [Required(ErrorMessage = "创建模式不能为空")]
        public string CreateMode { get; set; }

        [Required(ErrorMessage = "产品料号不能为空")]
        public int? PartId { get; set; }

        [Required(ErrorMessage = "治具名称不能为空")]
        public string FixtureName { get; set; }

        [Required(ErrorMessage = "治具图号不能为空")]
        public string DrawingNo { get; set; }

        [Required(ErrorMessage = "治具编号描述不能为空")]
        public string FixtureNoDesc { get; set; }

        [Required(ErrorMessage = "版本不能为空")]
        public string Version { get; set; }

        public string ProcessInstanceId { get; set; }

        public string MakerId { get; set; }

        public string MakerName { get; set; }

        public string EngineerId { get; set; }

        public string EngineerName { get; set; }

        public string EngineerLeaderId { get; set; }

        public string EngineerLeaderName { get; set; }

        public string QcId { get; set; }

        public string QcName { get; set; }

        public string QcLeaderId { get; set; }

        public string QcLeaderName { get; set; }

        public string Status { get; set; }

        [Required(ErrorMessage = "删除标志不能为空")]
        public int DelFlag { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }

        public string Remark { get; set; }

        public List<ProdMeasureTicketItemDefineDto> ProdMeasureTicketItemDefineNav { get; set; }

        [ExcelColumn(Name = "业务状态")]
        public string StatusLabel { get; set; }
    }
}