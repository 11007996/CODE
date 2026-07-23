namespace EAM.Model.Dto
{
    /// <summary>
    /// 设备保养记录查询对象
    /// </summary>
    public class MaintainRecordQueryDto : PagerInfo
    {
        public int? EquipmentId { get; set; }
        public int? Year { get; set; }
        public string DateMark { get; set; }
        public int? DateMarkStamp { get; set; }
    }

    /// <summary>
    /// 保养记录查询
    /// </summary>
    public class MaintainRecordQueryDetailDto
    {
        public int? Id { get; set; }
        public int? EquipmentId { get; set; }
        public string AssetNo { get; set; }
        public int Year { get; set; }
        public string DateMark { get; set; }
        public int DateMarkStamp { get; set; }
        public string TimeMark { get; set; }

        public DateTime? MaintainDate { get; set; }
    }

    /// <summary>
    /// 全局维护输入对象
    /// </summary>
    public class GlobalMaintainRecordDto
    {
        /// <summary>
        /// 日期标记
        /// </summary>
        [Required(ErrorMessage = "日期标记不能为空")]
        public string DateMark { get; set; }

        /// <summary>
        /// 保养日期(用于计算日期戳)
        /// </summary>
        public DateTime? MaintainDate { get; set; }

        /// <summary>
        /// 设备ID集合，为空表示所有设备
        /// </summary>
        public List<int> EquipmentList { get; set; }

        public string CostCenter { get; set; }

        public int? DeptId { get; set; }

        /// <summary>
        /// 保养人列表
        /// </summary>
        public List<string> ExecutorList { get; set; }

        /// <summary>
        /// 是否强制覆盖
        /// </summary>
        public bool Cover { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateTime { get; set; }
    }

    /// <summary>
    /// 设备保养记录输入输出对象
    /// </summary>
    public class MaintainRecordDto
    {
        [Required(ErrorMessage = "Id不能为空")]
        [ExcelColumn(Name = "Id")]
        [ExcelColumnName("Id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "设备ID不能为空")]
        [ExcelColumn(Name = "设备ID")]
        public int? EquipmentId { get; set; }

        [ExcelColumn(Name = "资产编号")]
        [ExcelColumnName("资产编号")]
        public string AssetNo { get; set; }

        [ExcelColumn(Name = "资产名称")]
        public string AssetName { get; set; }

        [Required(ErrorMessage = "年份标记不能为空")]
        [ExcelColumn(Name = "年份标记")]
        [ExcelColumnName("年份标记")]
        public int Year { get; set; }

        [Required(ErrorMessage = "日期标记不能为空")]
        [ExcelColumn(Name = "日期标记")]
        [ExcelColumnName("日期标记")]
        public string DateMark { get; set; }

        [Required(ErrorMessage = "日期标记戳不能为空")]
        [ExcelColumn(Name = "日期标记戳")]
        [ExcelColumnName("日期标记戳")]
        public int DateMarkStamp { get; set; }

        [ExcelColumn(Name = "时间标记")]
        [ExcelColumnName("时间标记")]
        public string TimeMark { get; set; }

        [ExcelColumn(Name = "执行人工号")]
        [ExcelColumnName("执行人工号")]
        public string ExecutorId { get; set; }

        [ExcelColumn(Name = "执行人名称")]
        [ExcelColumnName("执行人名称")]
        public string ExecutorName { get; set; }

        /// <summary>
        /// 保养日期(用于计算日期戳)
        /// </summary>
        public DateTime? MaintainDate { get; set; }

        [ExcelColumn(Name = "创建人")]
        [ExcelColumnName("创建人")]
        public string CreateBy { get; set; }

        [ExcelColumn(Name = "创建时间", Format = "yyyy-MM-dd HH:mm:ss", Width = 20)]
        [ExcelColumnName("创建时间")]
        public DateTime? CreateTime { get; set; }

        [ExcelColumn(Name = "更新人员")]
        [ExcelColumnName("更新人员")]
        public string UpdateBy { get; set; }

        [ExcelColumn(Name = "更新时间", Format = "yyyy-MM-dd HH:mm:ss", Width = 20)]
        [ExcelColumnName("更新时间")]
        public DateTime? UpdateTime { get; set; }

        [ExcelIgnore]
        public List<MaintainRecordDetailDto> MaintainRecordDetailNav { get; set; }
    }
}