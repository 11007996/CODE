namespace EAM.Model.Consumable
{
    /// <summary>
    /// 耗品库存通知
    /// </summary>
    [SugarTable("CON_Storage_Notice")]
    public class ConsumableStorageNotice
    {
        /// <summary>
        /// 耗品ID
        /// </summary>
        [SugarColumn(ColumnName = "consumable_Id")]
        public int ConsumableId { get; set; }

        /// <summary>
        /// 通知ID
        /// </summary>
        [SugarColumn(ColumnName = "wx_Notice_Id")]
        public long? WxNoticeId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnName = "create_Time")]
        public DateTime? CreateTime { get; set; }
    }
}