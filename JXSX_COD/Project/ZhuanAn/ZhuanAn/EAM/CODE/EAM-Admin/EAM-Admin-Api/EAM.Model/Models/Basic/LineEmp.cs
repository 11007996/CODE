namespace EAM.Model.Basic
{
    /// <summary>
    /// 产线员工关联
    /// </summary>
    [SugarTable("BASE_Line_Emp")]
    public class LineEmp
    {
        /// <summary>
        /// 主键ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        public int Id { get; set; }

        /// <summary>
        /// 线别ID
        /// </summary>
        [SugarColumn(ColumnName = "line_Id")]
        public int LineId { get; set; }

        /// <summary>
        /// 员工工号
        /// </summary>
        [SugarColumn(ColumnName = "emp_Code")]
        public string EmpCode { get; set; }

        /// <summary>
        /// 员工职位
        /// </summary>
        public string Position { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string EmpName { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string LineName { get; set; }
    }
}