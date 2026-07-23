namespace EAM.Model.Business
{
    /// <summary>
    /// 上线通知单
    /// </summary>
    [SugarTable("BU_Simple_Online_Notice_Ticket")]
    public class SimpleOnlineNoticeTicket
    {
        /// <summary>
        /// 业务编号
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "ticket_No")]
        public string TicketNo { get; set; }

        /// <summary>
        /// 发起人
        /// </summary>
        [SugarColumn(ColumnName = "Initiator_ID")]
        public string InitiatorId { get; set; }

        /// <summary>
        /// 发起人姓名
        /// </summary>
        [SugarColumn(ColumnName = "Initiator_Name")]
        public string InitiatorName { get; set; }

        /// <summary>
        /// 新上线料号
        /// </summary>
        [SugarColumn(ColumnName = "New_Part_Name")]
        public string NewPartName { get; set; }

        /// <summary>
        /// 在制料号
        /// </summary>
        [SugarColumn(ColumnName = "Old_Part_Name")]
        public string OldPartName { get; set; }

        /// <summary>
        /// 产量
        /// </summary>
        [SugarColumn(ColumnName = "product_Qty")]
        public int? ProductQty { get; set; }

        /// <summary>
        /// 产线
        /// </summary>
        [SugarColumn(ColumnName = "Line_ID")]
        public int? LineId { get; set; }

        /// <summary>
        /// 需求时间
        /// </summary>
        [SugarColumn(ColumnName = "need_Time")]
        public DateTime? NeedTime { get; set; }

        /// <summary>
        /// 流程编号
        /// </summary>
        [SugarColumn(ColumnName = "process_Instance_ID")]
        public string ProcessInstanceId { get; set; }

        /// <summary>
        /// 业务状态（0:表示在办，1表示结案）
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// 删除标志（0代表存在 1代表删除）
        /// </summary>
        public int? DelFlag { get; set; }

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

        [Navigate(NavigateType.OneToMany, nameof(SimpleOnlineNoticeTicketItem.TicketNo), nameof(TicketNo))] //自定义关系映射
        public List<SimpleOnlineNoticeTicketItem> ItemNav { get; set; }
    }
}