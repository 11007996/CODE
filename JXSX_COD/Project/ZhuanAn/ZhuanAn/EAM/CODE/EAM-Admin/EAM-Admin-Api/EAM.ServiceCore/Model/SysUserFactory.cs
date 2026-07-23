namespace EAM.Model.System
{
    /// <summary>
    /// 用户关联厂区
    /// </summary>
    [SugarTable("sys_user_factory")]
    public class SysUserFactory
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false)]
        public long UserId { get; set; }

        /// <summary>
        /// 工厂代码
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false)]
        public string FactoryId { get; set; }
    }
}