namespace EAM.Model.Dto
{
    /// <summary>
    /// 分组用户查询对象
    /// </summary>
    public class BaseGroupUserQueryDto : PagerInfo
    {
        public int GroupId { get; set; }
        public string EmpCode { get; set; }
        public string EmpName { get; set; }
    }

    /// <summary>
    /// 分组用户输入输出对象
    /// </summary>
    public class BaseGroupUserDto
    {
        [Required(ErrorMessage = "组ID不能为空")]
        public int GroupId { get; set; }

        [Required(ErrorMessage = "工号不能为空")]
        public string EmpCode { get; set; }

        public string EmpName { get; set; }
    }

    /// <summary>
    /// 批量添加、删除分组用户输入对象
    /// </summary>
    public class BatchBaseGroupUserDto
    {
        [Required(ErrorMessage = "组ID不能为空")]
        public int GroupId { get; set; }

        [Required(ErrorMessage = "用户名不能为空")]
        public List<string> EmpCodes { get; set; }
    }
}