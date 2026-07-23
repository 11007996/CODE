namespace EAM.Model.Dto
{
    /// <summary>
    /// 产品物模型事件查询对象
    /// </summary>
    public class IotProductThingEventQueryDto : PagerInfo
    {
        public int? ProductId { get; set; }
        public string EventName { get; set; }
        public string Identifier { get; set; }
    }

    /// <summary>
    /// 产品物模型事件输入输出对象
    /// </summary>
    public class IotProductThingEventDto
    {
        [Required(ErrorMessage = "事件ID不能为空")]
        public int EventId { get; set; }

        [Required(ErrorMessage = "产品ID不能为空")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "事件名称不能为空")]
        public string EventName { get; set; }

        [Required(ErrorMessage = "事件标识不能为空")]
        public string Identifier { get; set; }

        [Required(ErrorMessage = "事件类型不能为空")]
        public string EventType { get; set; }

        public string ExpandDesc { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "是否可用不能为空")]
        public bool Enabled { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateTime { get; set; }

        [ExcelColumn(Name = "产品ID")]
        public string ProductIdLabel { get; set; }

        public List<IotProductParamDefineDto> OutputParams { get; set; }
    }
}