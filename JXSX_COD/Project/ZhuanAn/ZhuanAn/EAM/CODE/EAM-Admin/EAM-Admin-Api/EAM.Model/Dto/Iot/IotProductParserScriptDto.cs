namespace EAM.Model.Dto
{
    /// <summary>
    /// 数据解析脚本查询对象
    /// </summary>
    public class IotProductParserScriptQueryDto : PagerInfo
    {
        public int? ProductId { get; set; }
    }

    /// <summary>
    /// 数据解析脚本输入输出对象
    /// </summary>
    public class IotProductParserScriptDto
    {
        [Required(ErrorMessage = "产品ID不能为空")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "脚本代码不能为空")]
        public string ScriptCode { get; set; }

        [Required(ErrorMessage = "是否可用不能为空")]
        public bool Enabled { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateTime { get; set; }

        [ExcelColumn(Name = "产品ID")]
        public string ProductIdLabel { get; set; }
    }
}