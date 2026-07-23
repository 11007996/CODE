namespace EAM.Model.System
{
    /// <summary>
    /// 系统部门扩展
    /// </summary>
    [Tenant("0")]
    [SugarTable("sys_dept_expand")]
    public class SysDeptExpand
    {
        /// <summary>
        /// 系统部门ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "sys_Dept_Id")]
        public long SysDeptId { get; set; }

        /// <summary>
        /// 立讯部门ID
        /// </summary>
        [SugarColumn(ColumnName = "lux_Dept_Id")]
        public string LuxDeptId { get; set; }

        /// <summary>
        /// 微信部门ID
        /// </summary>
        [SugarColumn(ColumnName = "wx_Dept_Id")]
        public string WxDeptId { get; set; }

        /// <summary>
        /// 默认工厂
        /// </summary>
        [SugarColumn(ColumnName = "default_Factory_Id")]
        public string DefaultFactoryId { get; set; }
    }
}