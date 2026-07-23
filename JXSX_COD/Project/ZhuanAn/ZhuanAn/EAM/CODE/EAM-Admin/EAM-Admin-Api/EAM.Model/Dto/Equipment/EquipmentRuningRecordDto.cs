namespace EAM.Model.Dto
{
    /// <summary>
    /// 设备运行数据查询对象
    /// </summary>
    public class EquipmentRuningRecordQueryDto : PagerInfo
    {
        public int? EquipmentId { get; set; }
        public int? RunState { get; set; }
        public DateTime? BeginCreateTime { get; set; }
        public DateTime? EndCreateTime { get; set; }
    }

    /// <summary>
    /// 设备运行数据输入输出对象
    /// </summary>
    public class EquipmentRuningRecordDto
    {
        [ExcelColumn(Name = "设备ID")]
        [Required(ErrorMessage = "设备Id不能为空")]
        public int EquipmentId { get; set; }

        [ExcelColumn(Name = "设备名称")]
        public string EquipmentName { get; set; }

        [ExcelColumn(Name = "资产编号")]
        public string AssetNo { get; set; }

        [ExcelColumn(Name = "资产名称")]
        public string AssetName { get; set; }

        [ExcelColumn(Name = "运行状态")]
        [ExcelColumnName("运行状态")]
        public int? RunState { get; set; }

        [ExcelColumn(Name = "产能数量")]
        [ExcelColumnName("产能数量")]
        public int? ProductCount { get; set; }

        [ExcelColumn(Name = "不良数量")]
        [ExcelColumnName("不良数量")]
        public int? DefectCount { get; set; }

        [ExcelColumn(Name = "报警状态")]
        [ExcelColumnName("报警状态")]
        public int? WarnState { get; set; }

        [ExcelColumn(Name = "报警代码")]
        [ExcelColumnName("报警代码")]
        public int? WarnCode { get; set; }

        [Required(ErrorMessage = "创建时间不能为空")]
        [ExcelColumn(Name = "创建时间", Format = "yyyy-MM-dd HH:mm:ss", Width = 20)]
        [ExcelColumnName("创建时间")]
        public DateTime? CreateTime { get; set; }
    }

    /// <summary>
    /// 设备运行数据输入输出对象
    /// </summary>
    public class EquipmentRuningWatchDto
    {
        public int? EquipmentId { get; set; }

        public string EquipmentName { get; set; }

        public string AssetNo { get; set; }

        public string AssetName { get; set; }

        public int EquipmentNo { get; set; }

        public int? RunState { get; set; }

        public string LineName { get; set; }

        public int? ProductCount { get; set; }

        public int? DefectCount { get; set; }

        public int? WarnState { get; set; }

        public int? WarnCode { get; set; }

        public DateTime? CreateTime { get; set; }
    }

    public class EquipmentWatchDetailQueryDto
    {
        public int? EquipmentId { get; set; }
        public DateTime? BeginCreateTime { get; set; }
        public DateTime? EndCreateTime { get; set; }
    }
}