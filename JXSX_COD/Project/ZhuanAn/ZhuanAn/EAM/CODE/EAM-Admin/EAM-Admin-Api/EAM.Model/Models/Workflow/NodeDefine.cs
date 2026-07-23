namespace EAM.Model.Workflow
{
    /// <summary>
    /// 流程节点定义
    /// </summary>
    [SugarTable("WF_Node_Define")]
    public class NodeDefine
    {
        /// <summary>
        /// 节点ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "node_ID")]
        public int NodeId { get; set; }

        /// <summary>
        /// 流程ID
        /// </summary>
        [SugarColumn(ColumnName = "process_ID")]
        public string ProcessId { get; set; }

        /// <summary>
        /// 节点名称
        /// </summary>
        [SugarColumn(ColumnName = "node_Name")]
        public string NodeName { get; set; }

        /// <summary>
        /// 节点类型
        /// </summary>
        [SugarColumn(ColumnName = "node_Type")]
        public string NodeType { get; set; }

        /// <summary>
        /// 允许的操作
        /// </summary>
        [SugarColumn(ColumnName = "allowed_Actions")]
        public string AllowedActions { get; set; }

        /// <summary>
        /// 节点顺序
        /// </summary>
        [SugarColumn(ColumnName = "order_No")]
        public int OrderNo { get; set; }

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
        /// 更新人
        /// </summary>
        [SugarColumn(ColumnName = "update_By")]
        public string UpdateBy { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        [SugarColumn(ColumnName = "update_Time")]
        public DateTime? UpdateTime { get; set; }

        [Navigate(NavigateType.OneToMany, nameof(NodeFlow.FromNodeId), nameof(NodeId))] //流向
        public List<NodeFlow> NodeFlowNav { get; set; }

        [Navigate(NavigateType.OneToMany, nameof(NodeFieldControl.NodeId), nameof(NodeId))] //表单控制
        public List<NodeFieldControl> NodeFieldControlNav { get; set; }

        [Navigate(NavigateType.OneToOne, nameof(NodeApprover.NodeId), nameof(NodeId))] //审批人
        public NodeApprover NodeApproverNav { get; set; }
    }
}