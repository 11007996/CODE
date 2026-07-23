namespace EAM.Model.Dto
{
    /// <summary>
    /// 履历维修记录查询对象
    /// </summary>
    public class ResumeRepairQueryDto : PagerInfo
    {
        public int? EquipmentId { get; set; }
        public DateTime? BeginRepairDate { get; set; }
        public DateTime? EndRepairDate { get; set; }
    }

    /// <summary>
    /// 履历维修记录输入输出对象
    /// </summary>
    public class ResumeRepairDto
    {
        [Required(ErrorMessage = "履历维修ID不能为空")]
        [ExcelColumn(Name = "ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "设备ID不能为空")]
        [ExcelColumn(Name = "设备ID")]
        public int? EquipmentId { get; set; }

        [ExcelColumn(Name = "资产编号")]
        public string AssetNo { get; set; }

        [ExcelColumn(Name = "资产名称")]
        public string AssetName { get; set; }

        [ExcelColumn(Name = "维修日期")]
        public DateTime? RepairDate { get; set; }

        [ExcelColumn(Name = "异常描述")]
        public string AbnormalDesc { get; set; }

        [ExcelColumn(Name = "维修原因")]
        public string RepairReason { get; set; }

        [ExcelColumn(Name = "维修人员")]
        public string RepairUser { get; set; }

        [ExcelColumn(Name = "验收结果")]
        public string CheckResult { get; set; }

        [ExcelColumn(Name = "备注／维修单号")]
        public string Remark { get; set; }

        [ExcelIgnore]
        public string CreateBy { get; set; }

        [ExcelIgnore]
        public DateTime? CreateTime { get; set; }

        [ExcelIgnore]
        public string UpdateBy { get; set; }

        [ExcelIgnore]
        public DateTime? UpdateTime { get; set; }
    }
}