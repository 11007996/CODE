namespace EAM.Model.Dto
{
    /// <summary>
    /// 产品参数定义查询对象
    /// </summary>
    public class IotProductParamDefineQueryDto : PagerInfo
    {
    }

    /// <summary>
    /// 产品参数定义输入输出对象
    /// </summary>
    public class IotProductParamDefineDto
    {
        public string OwnerType { get; set; }

        public int OwnerId { get; set; }

        [Required(ErrorMessage = "参数标识不能为空")]
        public string Identifier { get; set; }

        [Required(ErrorMessage = "参数名称不能为空")]
        public string ParamName { get; set; }

        [Required(ErrorMessage = "数据类型不能为空")]
        public string DataType { get; set; }

        public string Direction { get; set; }

        public int? SortOrder { get; set; }

        [ExcelColumn(Name = "所属类型")]
        public string OwnerTypeLabel { get; set; }
    }
}