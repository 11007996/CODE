namespace EAM.Model.Equipment
{
    /// <summary>
    /// 保养通知记录
    /// </summary>
    [SugarTable("EQU_Maintain_Notice")]
    public class MaintainNotice
    {
        /// <summary>
        /// 设备ID
        /// </summary>
        [SugarColumn(ColumnName = "equipment_Id")]
        public int EquipmentId { get; set; }

        /// <summary>
        /// 年份
        /// </summary>
        [SugarColumn(ColumnName = "year")]
        public int Year { get; set; }

        /// <summary>
        /// 日期标识
        /// </summary>
        [SugarColumn(ColumnName = "date_Mark")]
        public string DateMark { get; set; }

        /// <summary>
        /// 日期标识值
        /// </summary>
        [SugarColumn(ColumnName = "date_Mark_Stamp")]
        public int DateMarkStamp { get; set; }

        /// <summary>
        /// 微信通知Id
        /// </summary>
        [SugarColumn(ColumnName = "wx_Notice_Id")]
        public int? WxNoticeId { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [SugarColumn(ColumnName = "create_Time")]
        public DateTime? CreateTime { get; set; }
    }
}