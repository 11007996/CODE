namespace EAM.Model.Dto
{
    /// <summary>
    /// 产品测量报告_测量值查询对象
    /// </summary>
    public class ProdMeasureTicketItemQueryDto : PagerInfo
    {
    }

    /// <summary>
    /// 产品测量报告_测量值输入输出对象
    /// </summary>
    public class ProdMeasureTicketItemDto
    {
        [Required(ErrorMessage = "业务编号不能为空")]
        public string TicketNo { get; set; }

        [Required(ErrorMessage = "治具编号不能为空")]
        public string ProductNo { get; set; }

        [Required(ErrorMessage = "测试项不能为空")]
        public int OrderNo { get; set; }

        public decimal ActualValue { get; set; }
    }
}