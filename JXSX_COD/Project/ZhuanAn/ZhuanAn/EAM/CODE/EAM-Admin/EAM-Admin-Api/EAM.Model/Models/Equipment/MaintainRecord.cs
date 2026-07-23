namespace EAM.Model.Equipment
{
    /// <summary>
    /// 设备保养记录
    /// </summary>
    [SugarTable("EQU_Maintain_Record")]
    public class MaintainRecord
    {
        /// <summary>
        /// Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int? Id { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        [SugarColumn(ColumnName = "Equipment_Id")]
        public int? EquipmentId { get; set; }

        /// <summary>
        /// 年份标记
        /// </summary>
        public int? Year { get; set; }

        /// <summary>
        /// 日期标记
        /// </summary>
        [SugarColumn(ColumnName = "date_Mark")]
        public string DateMark { get; set; }

        /// <summary>
        /// 日期标记戳
        /// </summary>
        [SugarColumn(ColumnName = "date_Mark_Stamp")]
        public int? DateMarkStamp { get; set; }

        /// <summary>
        /// 时间标记
        /// </summary>
        [SugarColumn(ColumnName = "time_Mark")]
        public string TimeMark { get; set; }

        /// <summary>
        /// 执行人工号
        /// </summary>
        [SugarColumn(ColumnName = "executor_ID")]
        public string ExecutorId { get; set; }

        /// <summary>
        /// 执行人名称
        /// </summary>
        [SugarColumn(ColumnName = "executor_Name")]
        public string ExecutorName { get; set; }

        /// <summary>
        /// 是否可见
        /// </summary>
        public string IsVisible { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [SugarColumn(ColumnName = "create_By")]
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnName = "create_Time")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 更新人员
        /// </summary>
        [SugarColumn(ColumnName = "update_By")]
        public string UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [SugarColumn(ColumnName = "update_Time")]
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 资产编号
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string AssetNo { get; set; }

        /// <summary>
        /// 资产名称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string AssetName { get; set; }

        /// <summary>
        /// 保养日期（用于计算日期戳）
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public DateTime? MaintainDate { get; set; }

        [Navigate(NavigateType.OneToMany, nameof(MaintainRecordDetail.RecordId), nameof(Id))] //自定义关系映射
        public List<MaintainRecordDetail> MaintainRecordDetailNav { get; set; }
    }
}