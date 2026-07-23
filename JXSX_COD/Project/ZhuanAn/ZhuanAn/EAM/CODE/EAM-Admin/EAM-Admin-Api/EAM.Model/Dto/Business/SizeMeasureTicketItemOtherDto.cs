namespace EAM.Model.Dto
{
    /// <summary>
    /// 治具尺寸量测验收单_外观、结构、使用效果评估查询对象
    /// </summary>
    public class SizeMeasureTicketItemOtherQueryDto : PagerInfo
    {
    }

    /// <summary>
    /// 治具尺寸量测验收单_外观、结构、使用效果评估输入输出对象
    /// </summary>
    public class SizeMeasureTicketItemOtherDto
    {
        [Required(ErrorMessage = "业务编号不能为空")]
        public string TicketNo { get; set; }

        [Required(ErrorMessage = "项目序号不能为空")]
        public int OrderNo { get; set; }

        public string ItemDesc { get; set; }

        public string AutomationResult { get; set; }

        public string QcResult { get; set; }
    }
}