namespace EAM.Model.Dto
{
    /// <summary>
    /// 产线员工关联查询对象
    /// </summary>
    public class LineEmpQueryDto : PagerInfo
    {
        public int? LineId { get; set; }
        public string EmpCode { get; set; }
    }

    /// <summary>
    /// 产线员工关联输入输出对象
    /// </summary>
    public class LineEmpDto
    {
        [Required(ErrorMessage = "主键ID不能为空")]
        public int Id { get; set; }

        [Required(ErrorMessage = "线别ID不能为空")]
        public int LineId { get; set; }

        [Required(ErrorMessage = "员工工号不能为空")]
        public string EmpCode { get; set; }

        [Required(ErrorMessage = "员工职位不能为空")]
        public string Position { get; set; }

        public string EmpName { get; set; }

        public string LineName { get; set; }
    }
}