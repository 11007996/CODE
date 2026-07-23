namespace EAM.Model.Dto
{
    /// <summary>
    /// 呼叫盒信息查询对象
    /// </summary>
    public class CallBoxBaseQueryDto : PagerInfo
    {
        public string BoxName { get; set; }

        public string Mac { get; set; }

        public string Ip { get; set; }

        public int? LineId { get; set; }

        public int? StationId { get; set; }

        public string Keyword { get; set; }
    }

    /// <summary>
    /// 呼叫盒信息输入输出对象
    /// </summary>
    public class CallBoxBaseDto
    {
        [Required(ErrorMessage = "呼叫盒Id不能为空")]
        public int BoxId { get; set; }

        [Required(ErrorMessage = "呼叫盒名称不能为空")]
        public string BoxName { get; set; }

        [Required(ErrorMessage = "Mac地址不能为空")]
        public string Mac { get; set; }

        public string Ip { get; set; }

        public decimal? BatteryLevel { get; set; }

        public bool IsOnline { get; set; }

        public DateTime? LastOnlineTime { get; set; }

        public int? LineId { get; set; }

        public string LineName { get; set; }

        public int? StationId { get; set; }

        public string StationName { get; set; }

        public bool Enabled { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateTime { get; set; }
    }
}