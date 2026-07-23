namespace EAM.Model.Call
{
    /// <summary>
    /// 呼叫盒信息
    /// </summary>
    [SugarTable("CALL_Box_Base")]
    public class CallBoxBase
    {
        /// <summary>
        /// 呼叫盒Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "box_Id")]
        public int BoxId { get; set; }

        /// <summary>
        /// 呼叫盒名称
        /// </summary>
        [SugarColumn(ColumnName = "box_Name")]
        public string BoxName { get; set; }

        /// <summary>
        /// Mac地址
        /// </summary>
        public string Mac { get; set; }

        /// <summary>
        /// IP地址
        /// </summary>
        public string Ip { get; set; }

        /// <summary>
        /// 电量
        /// </summary>
        [SugarColumn(ColumnName = "battery_Level")]
        public decimal? BatteryLevel { get; set; }

        /// <summary>
        /// 是否在线(true:在线,false:离线）
        /// </summary>
        [SugarColumn(ColumnName = "is_Online")]
        public bool IsOnline { get; set; }

        /// <summary>
        /// 最后在线时间
        /// </summary>
        [SugarColumn(ColumnName = "last_Online_Time")]
        public DateTime? LastOnlineTime { get; set; }

        /// <summary>
        /// 绑定产线
        /// </summary>
        [SugarColumn(ColumnName = "line_Id")]
        public int? LineId { get; set; }

        /// <summary>
        /// 绑定工站
        /// </summary>
        [SugarColumn(ColumnName = "station_Id")]
        public int? StationId { get; set; }

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

        [SugarColumn(IsIgnore = true)]
        public string StationName { get; set; }
    }
}