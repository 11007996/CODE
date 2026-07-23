
namespace EAM.Model.Dto
{
    /// <summary>
    /// 属性扩展描述查询对象
    /// </summary>
    public class IotProductThingPropertyExtendQueryDto : PagerInfo 
    {
    }

    /// <summary>
    /// 属性扩展描述输入输出对象
    /// </summary>
    public class IotProductThingPropertyExtendDto
    {
        [Required(ErrorMessage = "属性ID不能为空")]
        public int PropertyId { get; set; }

        [Required(ErrorMessage = "操作类型不能为空")]
        public string OperateType { get; set; }

        [Required(ErrorMessage = "寄存器地址不能为空")]
        public int RegisterAddress { get; set; }

        [Required(ErrorMessage = "原始数据类型不能为空")]
        public string OriginalDataType { get; set; }

        public int? RegisterCount { get; set; }

        [Required(ErrorMessage = "交换寄存器内高低序不能为空")]
        public bool Swap16 { get; set; }

        [Required(ErrorMessage = "交换寄存器顺序不能为空")]
        public bool ReverseRegister { get; set; }

        [Required(ErrorMessage = "缩放因子不能为空")]
        public decimal Scaling { get; set; }

        public int? LowLimit { get; set; }

        public int? UpperLimit { get; set; }

        public string FunctionCode { get; set; }

        [Required(ErrorMessage = "数据上报方式不能为空")]
        public string ReportingMethod { get; set; }



        [ExcelColumn(Name = "操作类型")]
        public string OperateTypeLabel { get; set; }
        [ExcelColumn(Name = "原始数据类型")]
        public string OriginalDataTypeLabel { get; set; }
    }
}