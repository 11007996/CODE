namespace EAM.Model.Dto
{
    /// <summary>
    /// 治具信息查询对象
    /// </summary>
    public class FixtureBaseQueryDto : PagerInfo
    {
        public string FixtureName { get; set; }
        public string Series { get; set; }
        public int? PartId { get; set; }
        public string Keyword {  get; set; }
    }

    /// <summary>
    /// 治具信息输入输出对象
    /// </summary>
    public class FixtureBaseDto
    {
        [Required(ErrorMessage = "ID不能为空")]
        public int FixtureId { get; set; }

        [Required(ErrorMessage = "治具名称不能为空")]
        [ExcelColumn(Name = "治具名称")]
        public string FixtureName { get; set; }

        [Required(ErrorMessage = "系列不能为空")]
        [ExcelColumn(Name = "系列")]
        public string Series { get; set; }

        [ExcelColumn(Name = "图纸编号")]
        public string DrawingNo { get; set; }

        [Required(ErrorMessage = "单价不能为空")]
        [ExcelColumn(Name = "单价")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "安全库存不能为空")]
        [ExcelColumn(Name = "安全库存")]
        public int SafetyQty { get; set; }

        [Required(ErrorMessage = "状态不能为空")]
        [ExcelColumn(Name = "状态")]
        public string Status { get; set; }

        [ExcelColumn(Name = "创建人")]
        public string CreateBy { get; set; }

        [ExcelColumn(Name = "创建时间")]
        public DateTime? CreateTime { get; set; }

        [ExcelColumn(Name = "更新人")]
        public string UpdateBy { get; set; }

        [ExcelColumn(Name = "更新时间")]
        public DateTime? UpdateTime { get; set; }

        [ExcelColumn(Name = "备注")]
        public string Remark { get; set; }

        [ExcelColumn(Name = "系列")]
        [ExcelIgnore]
        public string SeriesLabel { get; set; }

        [ExcelColumn(Name = "状态")]
        [ExcelIgnore]
        public string StatusLabel { get; set; }
    }

    public class FixtureDetailDto
    {
        [Required(ErrorMessage = "ID不能为空")]
        public int FixtureId { get; set; }

        [Required(ErrorMessage = "治具名称不能为空")]
        [ExcelColumn(Name = "治具名称")]
        public string FixtureName { get; set; }

        [Required(ErrorMessage = "系列不能为空")]
        [ExcelColumn(Name = "系列")]
        public string Series { get; set; }

        [ExcelColumn(Name = "图纸编号")]
        public string DrawingNo { get; set; }

        [Required(ErrorMessage = "单价不能为空")]
        [ExcelColumn(Name = "单价")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "安全库存不能为空")]
        [ExcelColumn(Name = "安全库存")]
        public int SafetyQty { get; set; }

        [Required(ErrorMessage = "状态不能为空")]
        [ExcelColumn(Name = "状态")]
        public string Status { get; set; }

        [ExcelColumn(Name = "创建人")]
        public string CreateBy { get; set; }

        [ExcelColumn(Name = "创建时间")]
        public DateTime? CreateTime { get; set; }

        [ExcelColumn(Name = "更新人")]
        public string UpdateBy { get; set; }

        [ExcelColumn(Name = "更新时间")]
        public DateTime? UpdateTime { get; set; }

        [ExcelColumn(Name = "备注")]
        public string Remark { get; set; }

        [ExcelColumn(Name = "总库存")]
        public int TotalQty { get; set; }

        [ExcelColumn(Name = "闲置数量")]
        public int TotalIdleQty { get; set; }

        [ExcelColumn(Name = "占用数量")]
        public int TotalUsingQty { get; set; }

        [ExcelColumn(Name = "系列")]
        [ExcelIgnore]
        public string SeriesLabel { get; set; }

        [ExcelColumn(Name = "状态")]
        [ExcelIgnore]
        public string StatusLabel { get; set; }
    }

    /// <summary>
    /// 料号关联治具表输出对象
    /// </summary>
    public class IdleFixtureDto
    {
        public int? PartId { get; set; }

        public int? FixtureId { get; set; }

        public string FixtureName { get; set; }

        public string Series { get; set; }

        public decimal Price { get; set; }

        public int DefaultQty { get; set; }

        //闲置数量
        public int TotalIdleQty { get; set; }
    }
}