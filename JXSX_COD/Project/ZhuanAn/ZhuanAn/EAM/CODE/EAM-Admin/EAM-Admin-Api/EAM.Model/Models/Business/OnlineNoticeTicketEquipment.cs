namespace EAM.Model.Business
{
    /// <summary>
    /// 上线通知单_设备需求清单
    /// </summary>
    [SugarTable("BU_Online_Notice_Ticket_Equipment")]
    public class OnlineNoticeTicketEquipment
    {
        /// <summary>
        /// 业务编号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnName = "ticket_No")]
        public string TicketNo { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, ColumnName = "equipment_Name")]
        public string EquipmentName { get; set; }

        /// <summary>
        /// 需求数量
        /// </summary>
        [SugarColumn(ColumnName = "need_Qty")]
        public int NeedQty { get; set; }
    }
}