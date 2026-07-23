namespace EAM.Model.Dto
{
    /// <summary>
    /// 治具尺寸量测验收单_治具测量值查询对象
    /// </summary>
    public class SizeMeasureTicketItemQueryDto : PagerInfo
    {
    }

    /// <summary>
    /// 治具尺寸量测验收单_治具测量值输入输出对象
    /// </summary>
    public class SizeMeasureTicketItemDto
    {
        [Required(ErrorMessage = "业务编号不能为空")]
        public string TicketNo { get; set; }

        [Required(ErrorMessage = "治具编号不能为空")]
        public string FixtureNo { get; set; }

        [Required(ErrorMessage = "标准值不能为空")]
        public int OrderNo { get; set; }

        public decimal ActualValue { get; set; }
    }
}