namespace EAM.Model.Business
{
    /// <summary>
    /// 产品开发需求单_需求清单
    /// </summary>
    [SugarTable("BU_Product_Dev_Demand_Ticket_Item")]
    public class ProductDevDemandTicketItem
    {
        /// <summary>
        /// 业务编号
        /// </summary>
        [SugarColumn(ColumnName = "ticket_No")]
        public string TicketNo { get; set; }

        /// <summary>
        /// 制程名称
        /// </summary>
        [SugarColumn(ColumnName = "process_Name")]
        public string ProcessName { get; set; }

        /// <summary>
        /// 标准规格
        /// </summary>
        [SugarColumn(ColumnName = "standard_Spec")]
        public string StandardSpec { get; set; }

        /// <summary>
        /// 器材类型
        /// </summary>
        [SugarColumn(ColumnName = "equipment_Type")]
        public string EquipmentType { get; set; }

        /// <summary>
        /// 说明
        /// </summary>
        public string Caption { get; set; }

        /// <summary>
        /// 数量
        /// </summary>
        public int Qty { get; set; }

        /// <summary>
        /// 开发方式
        /// </summary>
        [SugarColumn(ColumnName = "dev_Mode")]
        public string DevMode { get; set; }

        /// <summary>
        /// 延用目标ID
        /// </summary>
        [SugarColumn(ColumnName = "extend_Target_ID")]
        public int? ExtendTargetId { get; set; }

        /// <summary>
        /// 延用目标描述
        /// </summary>
        [SugarColumn(ColumnName = "extend_Target_Desc")]
        public string ExtendTargetDesc { get; set; }

        /// <summary>
        /// 附件信息（Json数组）
        /// </summary>
        [SugarColumn(ColumnName = "file_List")]
        public string FileList { get; set; }
    }
}