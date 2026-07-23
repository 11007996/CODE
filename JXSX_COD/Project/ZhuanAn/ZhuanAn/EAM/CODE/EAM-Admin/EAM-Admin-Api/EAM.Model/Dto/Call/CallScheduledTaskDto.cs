namespace EAM.Model.Dto
{
    /// <summary>
    /// 广播定时任务查询对象
    /// </summary>
    public class CallScheduledTaskQueryDto : PagerInfo
    {
        public string TaskName { get; set; }
    }

    /// <summary>
    /// 广播定时任务输入输出对象
    /// </summary>
    public class CallScheduledTaskDto
    {
        [Required(ErrorMessage = "呼叫任务ID不能为空")]
        public int CallTaskId { get; set; }

        [Required(ErrorMessage = "任务名称不能为空")]
        public string TaskName { get; set; }

        [Required(ErrorMessage = "任务时间不能为空")]
        public string TaskTime { get; set; }

        [Required(ErrorMessage = "播放次数不能为空")]
        public int PlayCount { get; set; }

        [Required(ErrorMessage = "播放介质不能为空")]
        public string PlayMedium { get; set; }

        [JsonConverter(typeof(ValueToStringConverter))]
        public long? FileId { get; set; }

        public string TextContent { get; set; }

        public int? AreaId { get; set; }

        public string AreaName { get; set; }

        public bool Enable { get; set; }

        public string CreateBy { get; set; }

        public DateTime CreateTime { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateTime { get; set; }

        public string FileAccessUrl { get; set; }

        public string FileOriginalName { get; set; }
    }
}