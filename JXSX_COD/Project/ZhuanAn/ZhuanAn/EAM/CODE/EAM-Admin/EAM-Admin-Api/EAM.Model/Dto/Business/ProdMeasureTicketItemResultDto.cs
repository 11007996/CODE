namespace EAM.Model.Dto
{
    /// <summary>
    /// 产品测量报告_判定结果查询对象
    /// </summary>
    public class ProdMeasureTicketItemResultQueryDto : PagerInfo
    {
    }

    /// <summary>
    /// 产品测量报告_判定结果输入输出对象
    /// </summary>
    public class ProdMeasureTicketItemResultDto
    {
        [Required(ErrorMessage = "业务编号不能为空")]
        public string TicketNo { get; set; }

        [Required(ErrorMessage = "治具编号不能为空")]
        public string ProductNo { get; set; }

        public string SizeResult { get; set; }

        public string FacadeResult { get; set; }
    }
}