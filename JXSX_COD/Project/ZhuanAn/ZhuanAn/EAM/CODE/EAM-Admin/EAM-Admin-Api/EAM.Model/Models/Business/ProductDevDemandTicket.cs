namespace EAM.Model.Business
{
    /// <summary>
    /// 产品开发需求单
    /// </summary>
    [SugarTable("BU_Product_Dev_Demand_Ticket")]
    public class ProductDevDemandTicket
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
        /// 客户ID
        /// </summary>
        [SugarColumn(ColumnName = "custom_ID")]
        public string CustomId { get; set; }

        /// <summary>
        /// 料号
        /// </summary>
        [SugarColumn(ColumnName = "part_ID")]
        public int? PartId { get; set; }

        /// <summary>
        /// 预订单量
        /// </summary>
        [SugarColumn(ColumnName = "order_Qty")]
        public int OrderQty { get; set; }

        /// <summary>
        /// 需求时间
        /// </summary>
        [SugarColumn(ColumnName = "need_Date")]
        public DateTime? NeedDate { get; set; }

        /// <summary>
        /// 流程编号
        /// </summary>
        [SugarColumn(ColumnName = "process_Instance_ID")]
        public string ProcessInstanceId { get; set; }

        /// <summary>
        /// 工程师ID
        /// </summary>
        [SugarColumn(ColumnName = "engineer_ID")]
        public string EngineerId { get; set; }

        /// <summary>
        /// 工程师
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string EngineerName { get; set; }

        /// <summary>
        /// 工程师主管ID
        /// </summary>
        [SugarColumn(ColumnName = "engineer_Leader_ID")]
        public string EngineerLeaderId { get; set; }

        /// <summary>
        /// 工程师主管ID
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string EngineerLeaderName { get; set; }

        /// <summary>
        /// 上级领导ID
        /// </summary>
        [SugarColumn(ColumnName = "leader_ID")]
        public string LeaderId { get; set; }

        /// <summary>
        /// 上级领导
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string LeaderName { get; set; }

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

        [Navigate(NavigateType.OneToMany, nameof(ProductDevDemandTicketItem.TicketNo), nameof(TicketNo))] //自定义关系映射
        public List<ProductDevDemandTicketItem> ProductDevDemandTicketItemNav { get; set; }
    }
}