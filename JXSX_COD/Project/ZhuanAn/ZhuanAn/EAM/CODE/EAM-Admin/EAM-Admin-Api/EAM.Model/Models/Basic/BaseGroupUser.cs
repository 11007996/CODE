namespace EAM.Model.Basic
{
    /// <summary>
    /// 分组用户
    /// </summary>
    [SugarTable("BASE_Group_User")]
    public class BaseGroupUser
    {
        /// <summary>
        /// 组ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "group_ID")]
        public int GroupId { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "emp_code")]
        public string EmpCode { get; set; }
    }
}