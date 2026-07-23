namespace EAM.Model.Dto
{
    /// <summary>
    /// 产品物模型属性查询对象
    /// </summary>
    public class IotProductThingPropertyQueryDto : PagerInfo
    {
        public int? ProductId { get; set; }
        public string PropertyName { get; set; }
        public string Identifier { get; set; }
    }

    /// <summary>
    /// 产品物模型属性输入输出对象
    /// </summary>
    public class IotProductThingPropertyDto
    {
        [Required(ErrorMessage = "属性ID不能为空")]
        public int PropertyId { get; set; }

        [Required(ErrorMessage = "产品ID不能为空")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "属性名称不能为空")]
        public string PropertyName { get; set; }

        [Required(ErrorMessage = "属性标识不能为空")]
        public string Identifier { get; set; }

        [Required(ErrorMessage = "数据类型不能为空")]
        public string DataType { get; set; }

        [Required(ErrorMessage = "读写标记不能为空")]
        public string RwFlag { get; set; }

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

        [ExcelColumn(Name = "数据类型")]
        public string DataTypeLabel { get; set; }
    }
}