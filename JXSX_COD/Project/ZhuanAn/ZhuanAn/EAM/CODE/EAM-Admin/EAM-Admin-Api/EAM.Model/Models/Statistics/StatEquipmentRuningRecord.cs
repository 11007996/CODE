namespace EAM.Model.Statistics
{
    /// <summary>
    /// 统计设备运行记录
    /// </summary>
    [SugarTable("STAT_Equipment_Runing_Record")]
    public class StatEquipmentRuningRecord
    {
        /// <summary>
        /// 主键
        /// </summary>
        [SugarColumn(ColumnName = "stat_Id", IsPrimaryKey = true, IsIdentity = true)]
        public int StatId { get; set; }

        /// <summary>
        /// 统计日期
        /// </summary>
        [SugarColumn(ColumnName = "stat_Date")]
        public DateTime? StatDate { get; set; }

        /// <summary>
        /// 班次序号（24小时拆分的班次，根据配置的小时来拆分，如12小时制，会折出班次1、2；8小时制，会折分出1、2、3个班次）
        /// </summary>
        [SugarColumn(ColumnName = "class_seq")]
        public int ClassSeq { get; set; }

        /// <summary>
        /// 设备ID
        /// </summary>
        [SugarColumn(ColumnName = "equipment_Id")]
        public int EquipmentId { get; set; }

        /// <summary>
        /// 理论CT(秒)
        /// </summary>
        [SugarColumn(ColumnName = "theory_CT")]
        public decimal TheoryCT { get; set; }

        /// <summary>
        /// 拆分时间(日期的判断)
        /// </summary>
        [SugarColumn(ColumnName = "split_Time")]
        public string SplitTime { get; set; }

        /// <summary>
        /// 统计开始时间
        /// </summary>
        [SugarColumn(ColumnName = "stat_start_Time")]
        public DateTime? StatStartTime { get; set; }

        /// <summary>
        /// 统计结束时间
        /// </summary>
        [SugarColumn(ColumnName = "stat_end_Time")]
        public DateTime? StatEndTime { get; set; }

        /// <summary>
        /// 数据开始时间
        /// </summary>
        [SugarColumn(ColumnName = "data_start_Time")]
        public DateTime? DataStartTime { get; set; }

        /// <summary>
        /// 数据结束时间
        /// </summary>
        [SugarColumn(ColumnName = "data_end_Time")]
        public DateTime? DataEndTime { get; set; }

        /// <summary>
        /// 总产能
        /// </summary>
        [SugarColumn(ColumnName = "product_Count")]
        public int? ProductCount { get; set; }

        /// <summary>
        /// 不良数量
        /// </summary>
        [SugarColumn(ColumnName = "defect_Count")]
        public int? DefectCount { get; set; }

        /// <summary>
        /// 开机运行时间
        /// </summary>
        [SugarColumn(ColumnName = "run_Seconds")]
        public int? RunSeconds { get; set; }

        /// <summary>
        /// 计划停机时间(生效时间)
        /// </summary>
        [SugarColumn(ColumnName = "Plan_Effect_Seconds")]
        public int? PlanEffectSeconds { get; set; }

        /// <summary>
        /// 停机时间
        /// </summary>
        [SugarColumn(ColumnName = "stop_Seconds")]
        public int? StopSeconds { get; set; }

        /// <summary>
        /// 故障时间
        /// </summary>
        [SugarColumn(ColumnName = "fault_Seconds")]
        public int? FaultSeconds { get; set; }

        /// <summary>
        /// 故障次数
        /// </summary>
        [SugarColumn(ColumnName = "fault_Count")]
        public int? FaultCount { get; set; }

        /// <summary>
        /// 最后上报产能
        /// </summary>
        [SugarColumn(ColumnName = "last_Product_Count")]
        public int? LastProductCount { get; set; }

        /// <summary>
        /// 时间开动率
        /// </summary>
        [SugarColumn(ColumnName = "availability_Rate")]
        public decimal AvailabilityRate { get; set; }

        /// <summary>
        /// 性能开动率
        /// </summary>
        [SugarColumn(ColumnName = "performance_Rate")]
        public decimal PerformanceRate { get; set; }

        /// <summary>
        /// 合格率
        /// </summary>
        [SugarColumn(ColumnName = "quality_Rate")]
        public decimal QualityRate { get; set; }

        /// <summary>
        /// 综合效率
        /// </summary>
        public decimal OEE { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [SugarColumn(ColumnName = "update_Time")]
        public DateTime? UpdateTime { get; set; }

        [Navigate(NavigateType.OneToMany, nameof(StatEquipmentRuningWarn.StatId), nameof(StatId))] //自定义关系映射
        public List<StatEquipmentRuningWarn> StatEquipmentRuningWarnNav { get; set; }
    }
}