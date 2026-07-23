using EAM.Model.Workflow;

namespace EAM.Model.Dto
{
    public class NodeActionAndFieldDto
    {
        /// <summary>
        /// 节点信息
        /// </summary>
        public NodeDefine NodeInfo { get; set; }

        /// <summary>
        /// 表单字段的控制
        /// </summary>
        public List<NodeFieldControlDetailDto> FieldControls { get; set; }

        /// <summary>
        /// 允许的操作
        /// </summary>
        public List<string> AllowedActions { get; set; }

        /// <summary>
        /// 下节点点的审批人类型为表单的审批人值
        /// </summary>
        public string NextNodeFormApprover { get; set; }
    }
}