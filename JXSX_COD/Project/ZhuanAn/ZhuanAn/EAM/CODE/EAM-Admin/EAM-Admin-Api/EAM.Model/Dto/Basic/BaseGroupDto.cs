namespace EAM.Model.Dto
{
    /// <summary>
    /// 基础分组查询对象
    /// </summary>
    public class BaseGroupQueryDto : PagerInfo
    {
    }

    /// <summary>
    /// 基础分组输入输出对象
    /// </summary>
    public class BaseGroupDto
    {
        [Required(ErrorMessage = "组ID不能为空")]
        public int GroupId { get; set; }

        [Required(ErrorMessage = "组名称不能为空")]
        public string GroupName { get; set; }

        /// <summary>
        /// 分配的员工数
        /// </summary>
        public int UserNum { get; set; }
    }
}