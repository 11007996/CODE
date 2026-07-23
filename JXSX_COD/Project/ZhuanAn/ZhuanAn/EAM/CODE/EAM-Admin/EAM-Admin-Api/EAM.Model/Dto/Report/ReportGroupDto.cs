namespace EAM.Model.Dto
{
    /// <summary>
    /// 报表分组查询对象
    /// </summary>
    public class ReportGroupQueryDto : PagerInfo
    {
        public string GroupName { get; set; }
    }

    /// <summary>
    /// 报表分组输入输出对象
    /// </summary>
    public class ReportGroupDto
    {
        [Required(ErrorMessage = "分组Id不能为空")]
        public int GroupId { get; set; }

        [Required(ErrorMessage = "分组名称不能为空")]
        public string GroupName { get; set; }
    }
}