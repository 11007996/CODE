namespace EAM.Model.Dto
{
    /// <summary>
    /// 员工信息查询对象
    /// </summary>
    public class EmployeeQueryDto : PagerInfo
    {
        public string EmpCode { get; set; }

        public string EmpName { get; set; }

        public int? DeptId { get; set; }

        public string Keyword { get; set; }//关键字查询，工号与姓名模糊查询
    }

    /// <summary>
    /// 员工信息输入输出对象
    /// </summary>
    public class EmployeeDto
    {
        [Required(ErrorMessage = "员工工号不能为空")]
        [ExcelColumn(Name = "员工工号")]
        public string EmpCode { get; set; }

        [Required(ErrorMessage = "员工姓名不能为空")]
        [ExcelColumn(Name = "员工姓名")]
        public string EmpName { get; set; }

        [ExcelColumn(Ignore = true)]
        public long? DeptId { get; set; }

        [ExcelColumn(Name = "邮箱")]
        public string Email { get; set; }

        [ExcelColumn(Name = "手机号码")]
        public string PhoneNumber { get; set; }

        [ExcelColumn(Ignore = true)]
        public int? Sex { get; set; }

        [ExcelColumn(Ignore = true)]
        public string Avatar { get; set; }

        [ExcelColumn(Ignore = true)]
        public int? Status { get; set; }

        [ExcelColumn(Ignore = true)]
        public string CreateBy { get; set; }

        [ExcelColumn(Ignore = true)]
        public DateTime? CreateTime { get; set; }

        [ExcelColumn(Ignore = true)]
        public string UpdateBy { get; set; }

        [ExcelColumn(Ignore = true)]
        public DateTime? UpdateTime { get; set; }

        [ExcelColumn(Ignore = true)]
        public string SexLabel { get; set; }

        [ExcelColumn(Ignore = true)]
        public string StatusLabel { get; set; }

        /// <summary>
        /// 岗位集合
        /// </summary>
        [ExcelColumn(Ignore = true)]
        public int[] PostIds { get; set; }
    }

    /// <summary>
    /// 用户简单信息
    /// </summary>
    public class EmpSimpleDto
    {
        [ExcelColumn(Name = "员工工号")]
        public string EmpCode { get; set; }

        [ExcelColumn(Name = "员工姓名")]
        public string EmpName { get; set; }
    }
}