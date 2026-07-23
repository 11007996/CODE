namespace EAM.Model.Equipment
{
    /// <summary>
    /// 履历保养记录
    /// </summary>
    [SugarTable("EQU_Resume_Maintain")]
    public class ResumeMaintain
    {
        /// <summary>
        /// 履历保养ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        [SugarColumn(ColumnName = "Equipment_Id")]
        public int? EquipmentId { get; set; }

        /// <summary>
        /// 实施类别
        /// </summary>
        [SugarColumn(ColumnName = "execute_Category")]
        public string ExecuteCategory { get; set; }

        /// <summary>
        /// 实施日期
        /// </summary>
        [SugarColumn(ColumnName = "execute_Date")]
        public DateTime? ExecuteDate { get; set; }

        /// <summary>
        /// 保管部门
        /// </summary>
        [SugarColumn(ColumnName = "taken_Dept")]
        public string TakenDept { get; set; }

        /// <summary>
        /// 实施状况
        /// </summary>
        [SugarColumn(ColumnName = "execute_State")]
        public string ExecuteState { get; set; }

        /// <summary>
        /// 实施人员
        /// </summary>
        [SugarColumn(ColumnName = "execute_User")]
        public string ExecuteUser { get; set; }

        /// <summary>
        /// 下次实施日期
        /// </summary>
        [SugarColumn(ColumnName = "next_Execute_Date")]
        public DateTime? NextExecuteDate { get; set; }

        /// <summary>
        /// 备注/校验报告编号
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

        [SugarColumn(IsIgnore = true)]
        public string ExecuteUserName { get; set; }
    }
}