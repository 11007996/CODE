namespace EAM.Model.System
{
    /// <summary>
    /// 厂区管理
    /// </summary>
    [SugarTable("sys_factory")]
    public class SysFactory
    {
        /// <summary>
        /// 厂区代码
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false)]
        public string FactoryId { get; set; }

        /// <summary>
        /// 厂区名称
        /// </summary>
        public string FactoryName { get; set; }

        /// <summary>
        /// 厂区对应最大部门
        /// </summary>
        [SugarColumn(ColumnName = "root_dept_id")]
        public int? RootDeptId { get; set; }

        /// <summary>
        /// 厂区默认基础用户的角色
        /// </summary>
        [SugarColumn(ColumnName = "default_role_id")]
        public int? DefaultRoleId { get; set; }

        [SugarColumn(IsIgnore = true)]
        public string DefaultRoleName { get; set; }
    }
}