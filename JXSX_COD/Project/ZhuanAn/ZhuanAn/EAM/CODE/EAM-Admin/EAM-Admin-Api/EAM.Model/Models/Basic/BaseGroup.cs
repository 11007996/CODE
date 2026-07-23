namespace EAM.Model.Basic
{
    /// <summary>
    /// 基础分组
    /// </summary>
    [SugarTable("BASE_Group")]
    public class BaseGroup
    {
        /// <summary>
        /// 组ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "group_ID")]
        public int GroupId { get; set; }

        /// <summary>
        /// 组名称
        /// </summary>
        [SugarColumn(ColumnName = "group_Name")]
        public string GroupName { get; set; }

        /// <summary>
        /// 员工个数
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public int UserNum { get; set; }
    }
}