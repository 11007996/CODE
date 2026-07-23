namespace EAM.Model.Dto
{
    /// <summary>
    /// 料号查询对象
    /// </summary>
    public class PartQueryDto : PagerInfo
    {
        public int? PartId { get; set; }
        public string PartNo { get; set; }
    }

    /// <summary>
    /// 料号输入输出对象
    /// </summary>
    public class PartDto
    {
        public int? PartId { get; set; }

        [Required(ErrorMessage = "料号名称不能为空")]
        public string PartNo { get; set; }

        public string Remark { get; set; }
    }
}