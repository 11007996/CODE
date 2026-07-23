using EAM.Model.Business;

namespace EAM.Model.Business
{
    /// <summary>
    /// 耗品领用单
    /// </summary>
    [SugarTable("BU_Consumable_Receive_Ticket")]
    public class ConsumableReceiveTicket
    {
        /// <summary>
        /// 业务编号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "ticket_No")]
        public string TicketNo { get; set; }

        /// <summary>
        /// 发起人
        /// </summary>
        [SugarColumn(ColumnName = "initiator_ID")]
        public string InitiatorId { get; set; }

        /// <summary>
        /// 发起人姓名
        /// </summary>
        [SugarColumn(ColumnName = "initiator_Name")]
        public string InitiatorName { get; set; }

        /// <summary>
        /// 线别ID
        /// </summary>
        [SugarColumn(ColumnName = "line_ID")]
        public int? LineId { get; set; }

        /// <summary>
        /// 需求时间
        /// </summary>
        [SugarColumn(ColumnName = "need_Date")]
        public DateTime? NeedDate { get; set; }

        /// <summary>
        /// 用途
        /// </summary>
        public string Purpose { get; set; }

        /// <summary>
        /// 流程编号
        /// </summary>
        [SugarColumn(ColumnName = "process_Instance_ID")]
        public string ProcessInstanceId { get; set; }

        /// <summary>
        /// 业务状态
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 删除标志
        /// </summary>
        public int DelFlag { get; set; }

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

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

        [Navigate(NavigateType.OneToMany, nameof(ConsumableReceiveTicketItem.TicketNo), nameof(TicketNo))] //自定义关系映射
        public List<ConsumableReceiveTicketItem> ConsumableNav { get; set; }
    }
}