namespace EAM.Model.Dto
{
    /// <summary>
    /// 治具料号关联表查询对象
    /// </summary>
    public class FixturePartQueryDto : PagerInfo
    {
        public int? PartId { get; set; }
        public int? FixtureId { get; set; }
    }

    /// <summary>
    /// 治具料号关联表输入输出对象
    /// </summary>
    public class FixturePartDto
    {
        [Required(ErrorMessage = "料号不能为空")]
        public int? PartId { get; set; }

        public string PartNo { get; set; }

        [Required(ErrorMessage = "治具ID不能为空")]
        public int? FixtureId { get; set; }

        public string FixtureName { get; set; }

        [Required(ErrorMessage = "默认数量不能为空")]
        public int DefaultQty { get; set; }
    }
}