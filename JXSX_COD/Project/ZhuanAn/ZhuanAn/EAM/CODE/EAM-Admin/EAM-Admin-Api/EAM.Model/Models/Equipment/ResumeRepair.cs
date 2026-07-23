namespace EAM.Model.Equipment
{
    /// <summary>
    /// 履历维修记录
    /// </summary>
    [SugarTable("EQU_Resume_Repair")]
    public class ResumeRepair
    {
        /// <summary>
        /// 履历维修ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        [SugarColumn(ColumnName = "Equipment_Id")]
        public int? EquipmentId { get; set; }

        /// <summary>
        /// 维修日期
        /// </summary>
        [SugarColumn(ColumnName = "repair_Date")]
        public DateTime? RepairDate { get; set; }

        /// <summary>
        /// 异常描述
        /// </summary>
        [SugarColumn(ColumnName = "abnormal_Desc")]
        public string AbnormalDesc { get; set; }

        /// <summary>
        /// 维修原因
        /// </summary>
        [SugarColumn(ColumnName = "repair_Reason")]
        public string RepairReason { get; set; }

        /// <summary>
        /// 维修人员
        /// </summary>
        [SugarColumn(ColumnName = "repair_User")]
        public string RepairUser { get; set; }

        /// <summary>
        /// 验收结果
        /// </summary>
        [SugarColumn(ColumnName = "check_Result")]
        public string CheckResult { get; set; }

        /// <summary>
        /// 备注／维修单号
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 创建用户
        /// </summary>
        [SugarColumn(ColumnName = "Create_By")]
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnName = "Create_Time")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 更新用户
        /// </summary>
        [SugarColumn(ColumnName = "update_By")]
        public string UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [SugarColumn(ColumnName = "update_Time")]
        public DateTime? UpdateTime { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string AssetNo { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string AssetName { get; set; }
    }
}