namespace EAM.Model.Dto
{
    /// <summary>
    /// 工站信息查询对象
    /// </summary>
    public class StationQueryDto : PagerInfo
    {
        public string StationName { get; set; }

        public string StationCode { get; set; }

        public int? LineId { get; set; }
    }

    /// <summary>
    /// 工站信息输入输出对象
    /// </summary>
    public class StationDto
    {
        [Required(ErrorMessage = "工站ID不能为空")]
        public int StationId { get; set; }

        [Required(ErrorMessage = "工站名称不能为空")]
        public string StationName { get; set; }

        [Required(ErrorMessage = "工站编码不能为空")]
        public string StationCode { get; set; }

        public int? LineId { get; set; }

        public string LineName { get; set; }

        public string Remark { get; set; }

        public bool Enabled { get; set; }

        public string CreateBy { get; set; }

        public DateTime? CreateTime { get; set; }

        public string UpdateBy { get; set; }

        public DateTime? UpdateTime { get; set; }
    }
}