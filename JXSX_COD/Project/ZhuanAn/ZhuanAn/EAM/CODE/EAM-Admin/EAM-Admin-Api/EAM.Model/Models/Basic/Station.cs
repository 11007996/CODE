namespace EAM.Model.Basic
{
    /// <summary>
    /// 工站信息
    /// </summary>
    [SugarTable("BASE_Station")]
    public class Station
    {
        /// <summary>
        /// 工站ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "station_Id")]
        public int StationId { get; set; }

        /// <summary>
        /// 工站名称
        /// </summary>
        [SugarColumn(ColumnName = "station_Name")]
        public string StationName { get; set; }

        /// <summary>
        /// 工站编码
        /// </summary>
        [SugarColumn(ColumnName = "station_Code")]
        public string StationCode { get; set; }

        /// <summary>
        /// 所属产线
        /// </summary>
        [SugarColumn(ColumnName = "Line_Id")]
        public int? LineId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool Enabled { get; set; }

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
        /// 更新人
        /// </summary>
        [SugarColumn(ColumnName = "update_by")]
        public string UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [SugarColumn(ColumnName = "update_time")]
        public DateTime? UpdateTime { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string LineName { get; set; }
    }
}