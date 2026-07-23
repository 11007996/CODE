namespace EAM.Model.Dto
{
    /// <summary>
    /// 产品测量报告_测试项目定义查询对象
    /// </summary>
    public class ProdMeasureTicketItemDefineQueryDto : PagerInfo
    {
    }

    /// <summary>
    /// 产品测量报告_测试项目定义输入输出对象
    /// </summary>
    public class ProdMeasureTicketItemDefineDto
    {
        [Required(ErrorMessage = "业务编号不能为空")]
        public string TicketNo { get; set; }

        [Required(ErrorMessage = "序号不能为空")]
        public int OrderNo { get; set; }

        [Required(ErrorMessage = "项目名称不能为空")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "标准值不能为空")]
        public decimal Standard { get; set; }

        [Required(ErrorMessage = "正公差不能为空")]
        public decimal Positive { get; set; }

        [Required(ErrorMessage = "负公差不能为空")]
        public decimal Caption { get; set; }
    }
}