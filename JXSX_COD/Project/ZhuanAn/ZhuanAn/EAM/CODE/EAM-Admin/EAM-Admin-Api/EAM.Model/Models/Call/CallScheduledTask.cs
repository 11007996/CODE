namespace EAM.Model.Call
{
    /// <summary>
    /// 广播定时任务
    /// </summary>
    [SugarTable("CALL_Scheduled_Task")]
    public class CallScheduledTask
    {
        /// <summary>
        /// 呼叫任务ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "call_Task_Id")]
        public int CallTaskId { get; set; }

        /// <summary>
        /// 任务名称
        /// </summary>
        [SugarColumn(ColumnName = "task_Name")]
        public string TaskName { get; set; }

        /// <summary>
        /// 任务时间
        /// </summary>
        [SugarColumn(ColumnName = "task_Time")]
        public string TaskTime { get; set; }

        /// <summary>
        /// 播放次数
        /// </summary>
        [SugarColumn(ColumnName = "play_Count")]
        public int PlayCount { get; set; }

        /// <summary>
        /// 播放介质
        /// </summary>
        [SugarColumn(ColumnName = "play_Medium")]
        public string PlayMedium { get; set; }

        /// <summary>
        /// 文件Id
        /// </summary>
        [JsonConverter(typeof(ValueToStringConverter))]
        [SugarColumn(ColumnName = "file_Id")]
        public long? FileId { get; set; }

        /// <summary>
        /// 文本内容
        /// </summary>
        [SugarColumn(ColumnName = "text_Content")]
        public string TextContent { get; set; }

        /// <summary>
        /// 广播区域Id
        /// </summary>
        [SugarColumn(ColumnName = "area_Id")]
        public int? AreaId { get; set; }

        /// <summary>
        /// 是否可用
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [SugarColumn(ColumnName = "create_By")]
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnName = "create_Time")]
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新人
        /// </summary>
        [SugarColumn(ColumnName = "update_By")]
        public string UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [SugarColumn(ColumnName = "update_Time")]
        public DateTime? UpdateTime { get; set; }
    }
}