namespace EAM.Model.Call
{
    /// <summary>
    /// 盒子操作记录
    /// </summary>
    [SugarTable("CALL_Box_Operate")]
    public class CallBoxOperate
    {
        /// <summary>
        /// 操作记录Id
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "operate_Id")]
        public long OperateId { get; set; }

        /// <summary>
        /// 盒子ID
        /// </summary>
        [SugarColumn(ColumnName = "box_Id")]
        public int BoxId { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        [SugarColumn(ColumnName = "operate_Type")]
        public int OperateType { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [SugarColumn(ColumnName = "create_By")]
        public string CreateBy { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnName = "create_Time")]
        public DateTime? CreateTime { get; set; }
    }
}