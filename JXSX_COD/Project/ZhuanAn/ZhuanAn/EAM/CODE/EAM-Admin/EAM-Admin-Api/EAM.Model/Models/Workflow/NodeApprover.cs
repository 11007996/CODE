namespace EAM.Model.Workflow
{
    /// <summary>
    /// 节点审批人配置
    /// </summary>
    [SugarTable("WF_Node_Approver")]
    public class NodeApprover
    {
        /// <summary>
        /// 节点ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = false, ColumnName = "node_ID")]
        public int NodeId { get; set; }

        /// <summary>
        /// 审批人类型
        /// </summary>
        [SugarColumn(ColumnName = "approver_Type")]
        public string ApproverType { get; set; }

        /// <summary>
        /// 审批类型值
        /// </summary>
        [SugarColumn(ColumnName = "approver_Value")]
        public string ApproverValue { get; set; }

        /// <summary>
        /// 审批人描述
        /// </summary>

        [SugarColumn(ColumnName = "approver_Desc")]
        public string ApproverDesc { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        [SugarColumn(IsIgnore = true)]
        public string NodeName { get; set; }
    }
}