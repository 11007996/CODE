namespace EAM.Model.Workflow
{
    /// <summary>
    /// 节点流向
    /// </summary>
    [SugarTable("WF_Node_Flow")]
    public class NodeFlow
    {
        /// <summary>
        /// 节点流向ID
        /// </summary>
        [SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "node_Flow_ID")]
        public int NodeFlowId { get; set; }

        /// <summary>
        /// 来源节点
        /// </summary>
        [SugarColumn(ColumnName = "from_Node_ID")]
        public int FromNodeId { get; set; }

        /// <summary>
        /// 操作类型
        /// </summary>
        [SugarColumn(ColumnName = "action_Type")]
        public string ActionType { get; set; }

        /// <summary>
        /// 目标节点
        /// </summary>
        [SugarColumn(ColumnName = "to_Node_ID")]
        public int ToNodeId { get; set; }

        /// <summary>
        /// 条件表达式
        /// </summary>
        public string ConditionExpression { get; set; }
    }
}