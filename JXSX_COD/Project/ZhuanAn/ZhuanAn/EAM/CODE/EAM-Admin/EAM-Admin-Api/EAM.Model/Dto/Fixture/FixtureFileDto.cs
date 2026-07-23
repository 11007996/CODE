namespace EAM.Model.Dto
{
    /// <summary>
    /// 治具文件关联查询对象
    /// </summary>
    public class FixtureFileQueryDto : PagerInfo
    {
        public int? FixtureId { get; set; }
    }

    /// <summary>
    /// 治具文件关联输入输出对象
    /// </summary>
    public class FixtureFileDto
    {
        [Required(ErrorMessage = "治具ID不能为空")]
        public int FixtureId { get; set; }

        [Required(ErrorMessage = "文件Id不能为空")]
        public long FileId { get; set; }
    }
}