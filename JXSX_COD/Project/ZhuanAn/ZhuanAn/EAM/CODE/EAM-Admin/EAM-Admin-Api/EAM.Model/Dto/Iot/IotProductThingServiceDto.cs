namespace EAM.Model.Dto
{
    /// <summary>
    /// 产品物模型服务查询对象
    /// </summary>
    public class IotProductThingServiceQueryDto : PagerInfo
    {
        public int? ProductId { get; set; }
        public string ServiceName { get; set; }
        public string Identifier { get; set; }
    }

    /// <summary>
    /// 产品物模型服务输入输出对象
    /// </summary>
    public class IotProductThingServiceDto
    {
        [Required(ErrorMessage = "服务ID不能为空")]
        public int ServiceId { get; set; }

        [Required(ErrorMessage = "产品ID不能为空")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "服务名称不能为空")]
        public string ServiceName { get; set; }

        [Required(ErrorMessage = "服务标识不能为空")]
        public string Identifier { get; set; }

        public string InvokeMode { get; set; }

        public string InputParams { get; set; }

        public string OutputParams { get; set; }

        public string ExpandDesc { get; set; }

        public string Description { get; set; }

        [Required(ErrorMessage = "是否可用不能为空")]
        public bool Enabled { get; set; }

        [Required(ErrorMessage = "创建人不能为空")]
        public string CreateBy { get; set; }

        [Required(ErrorMessage = "创建时间不能为空")]
        public DateTime? CreateTime { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateTime { get; set; }

        [ExcelColumn(Name = "调用方式")]
        public string InvokeModeLabel { get; set; }
    }
}