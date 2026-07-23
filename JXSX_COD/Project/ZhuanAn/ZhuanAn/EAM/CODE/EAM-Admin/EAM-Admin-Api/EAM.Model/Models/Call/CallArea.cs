namespace EAM.Model.Call
{
    /// <summary>
    /// 广播区域
    /// </summary>
    [SugarTable("CALL_Area")]
    public class CallArea
    {
        /// <summary>
        /// 区域ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "area_Id")]
        public int AreaId { get; set; }

        /// <summary>
        /// 区域名称
        /// </summary>
        [SugarColumn(ColumnName = "area_Name")]
        public string AreaName { get; set; }

        [Navigate(NavigateType.OneToMany, nameof(CallAreaLine.AreaId), nameof(AreaId))] //自定义关系映射
        public List<CallAreaLine> CallAreaLineNav { get; set; }
    }
}