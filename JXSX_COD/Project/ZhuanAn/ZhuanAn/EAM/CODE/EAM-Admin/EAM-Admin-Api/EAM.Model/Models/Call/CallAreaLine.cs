namespace EAM.Model.Call
{
    /// <summary>
    /// 产线区域绑定
    /// </summary>
    [SugarTable("CALL_Area_Line")]
    public class CallAreaLine
    {
        /// <summary>
        /// 区域Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnName = "area_Id")]
        public int AreaId { get; set; }

        /// <summary>
        /// 产线ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnName = "line_Id")]
        public int LineId { get; set; }

        /// <summary>
        /// 产线ID
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string LineName { get; set; }
    }
}