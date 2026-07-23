namespace EAM.Model.Dto
{
    /// <summary>
    /// 耗品表查询对象
    /// </summary>
    public class ConsumableBaseQueryDto : PagerInfo
    {
        public string ConsumablePart { get; set; }
        public string ConsumableName { get; set; }
        public string Spec { get; set; }
        public string Category {  get; set; }
        public string Keyword {  get; set; }
        /// <summary>
        /// 库存警告，ture:库存低于安全库存，false:库存大于等于安全库存
        /// </summary>
        public bool? IsStackWarning { get; set; }
    }

    /// <summary>
    /// 耗品表输入输出对象
    /// </summary>
    public class ConsumableBaseDto
    {
        [Required(ErrorMessage = "ID不能为空")]
        [ExcelColumn(Name = "ID")]
        [ExcelColumnName("ID")]
        public int ConsumableId { get; set; }

        [Required(ErrorMessage = "请购料号不能为空")]
        [ExcelColumn(Name = "请购料号")]
        [ExcelColumnName("请购料号")]
        public string ConsumablePart { get; set; }

        [Required(ErrorMessage = "耗品名称不能为空")]
        [ExcelColumn(Name = "耗品名称")]
        [ExcelColumnName("耗品名称")]
        public string ConsumableName { get; set; }

        [Required(ErrorMessage = "规格不能为空")]
        [ExcelColumn(Name = "规格")]
        [ExcelColumnName("规格")]
        public string Spec { get; set; }

        [Required(ErrorMessage = "单价不能为空")]
        [ExcelColumn(Name = "单价")]
        [ExcelColumnName("单价")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "安全库存不能为空")]
        [ExcelColumn(Name = "安全库存")]
        [ExcelColumnName("安全库存")]
        public int SafetyQty { get; set; }

        [Required(ErrorMessage = "状态不能为空")]
        [ExcelColumn(Name = "状态")]
        [ExcelColumnName("状态")]
        public string Status { get; set; }

        [ExcelColumn(Name = "类别")]
        [ExcelColumnName("类别")]
        public string Category { get; set; }

        [ExcelColumn(Name = "创建人")]
        [ExcelColumnName("创建人")]
        public string CreateBy { get; set; }

        [ExcelColumn(Name = "创建时间", Format = "yyyy-MM-dd HH:mm:ss", Width = 20)]
        [ExcelColumnName("创建时间")]
        public DateTime? CreateTime { get; set; }

        [ExcelColumn(Name = "更新人")]
        [ExcelColumnName("更新人")]
        public string UpdateBy { get; set; }

        [ExcelColumn(Name = "最后更新", Format = "yyyy-MM-dd HH:mm:ss", Width = 20)]
        [ExcelColumnName("最后更新")]
        public DateTime? UpdateTime { get; set; }

        [ExcelColumn(Name = "备注")]
        [ExcelColumnName("备注")]
        public string Remark { get; set; }

        [ExcelColumn(Name = "状态")]
        [ExcelIgnore]
        public string StatusLabel { get; set; }
    }

    public class ConsumableDetailDto : ConsumableBaseDto
    {
        [Required(ErrorMessage = "库存不能为空")]
        [ExcelColumn(Name = "库存")]
        [ExcelColumnName("库存")]
        public int TotalStackQty { get; set; }
    }

    public class IdleConsumableDto
    {
        public int ConsumableId { get; set; }

        public string ConsumablePart { get; set; }

        public string ConsumableName { get; set; }

        public string Spec { get; set; }

        public decimal Price { get; set; }

        public int TotalStackQty { get; set; }
    }
}