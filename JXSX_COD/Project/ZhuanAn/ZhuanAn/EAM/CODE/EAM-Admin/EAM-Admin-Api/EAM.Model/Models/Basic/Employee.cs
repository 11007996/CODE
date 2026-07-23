namespace EAM.Model.Basic
{
    /// <summary>
    /// 员工信息
    /// </summary>
    [SugarTable("Base_Employee")]
    public class Employee
    {
        /// <summary>
        /// 员工工号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "Emp_Code")]
        public string EmpCode { get; set; }

        /// <summary>
        /// 员工姓名
        /// </summary>
        [SugarColumn(ColumnName = "Emp_Name")]
        public string EmpName { get; set; }

        /// <summary>
        /// 部门ID
        /// </summary>
        [SugarColumn(ColumnName = "Dept_Id")]
        public long DeptId { get; set; }

        /// <summary>
        /// 用户邮箱
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 手机号码
        /// </summary>
        [SugarColumn(ColumnName = "Phone_Number")]
        public string PhoneNumber { get; set; }

        /// <summary>
        /// 用户性别（0男 1女 2未知）
        /// </summary>
        public int? Sex { get; set; }

        /// <summary>
        /// 头像地址
        /// </summary>
        public string Avatar { get; set; }

        /// <summary>
        /// 帐号状态（0正常 1停用）
        /// </summary>
        public int Status { get; set; }

        /// <summary>
        /// 删除标志（0代表存在 1代表删除）
        /// </summary>
        [SugarColumn(ColumnName = "Del_Flag")]
        public int DelFlag { get; set; }

        /// <summary>
        /// 创建人编码
        /// </summary>
        [SugarColumn(ColumnName = "create_by")]
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnName = "create_time")]
        public DateTime? CreateTime { get; set; }

        /// <summary>
        /// 更新人编码
        /// </summary>
        [SugarColumn(ColumnName = "update_by")]
        public string UpdateBy { get; set; }

        /// <summary>
        /// 最后更新时间
        /// </summary>
        [SugarColumn(ColumnName = "update_time")]
        public DateTime? UpdateTime { get; set; }

        /// <summary>
        /// 岗位集合
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public int[] PostIds { get; set; }
    }
}