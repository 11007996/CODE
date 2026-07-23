namespace EAM.Model.Dto
{
    /// <summary>
    /// 治具尺寸量测验收单_结果判定查询对象
    /// </summary>
    public class SizeMeasureTicketItemResultQueryDto : PagerInfo
    {
    }

    /// <summary>
    /// 治具尺寸量测验收单_结果判定输入输出对象
    /// </summary>
    public class SizeMeasureTicketItemResultDto
    {
        [Required(ErrorMessage = "业务编号不能为空")]
        public string TicketNo { get; set; }

        [Required(ErrorMessage = "治具编号不能为空")]
        public string FixtureNo { get; set; }

        public string SizeResult { get; set; }

        public string InStorage { get; set; }
    }
}