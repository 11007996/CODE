namespace EAM.Model.Dto
{
    /// <summary>
    /// 产线信息查询对象
    /// </summary>
    public class LineQueryDto : PagerInfo
    {
        public string LineName { get; set; }

        public int? LineCode { get; set; }
    }

    /// <summary>
    /// 产线信息输入输出对象
    /// </summary>
    public class LineDto
    {
        public int LineId { get; set; }

        [Required(ErrorMessage = "产线名称不能为空")]
        public string LineName { get; set; }

        public int? LineCode { get; set; }

        public string Remark { get; set; }
    }
}