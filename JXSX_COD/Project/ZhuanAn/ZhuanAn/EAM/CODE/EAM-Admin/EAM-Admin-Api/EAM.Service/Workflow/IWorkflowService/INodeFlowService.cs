using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Workflow;

namespace EAM.Service.Workflow.IWorkflowService
{
    /// <summary>
    /// 节点流向service接口
    /// </summary>
    public interface INodeFlowService : IBaseService<NodeFlow>
    {
        PagedInfo<NodeFlowDto> GetList(NodeFlowQueryDto parm);

        NodeFlow GetInfo(int NodeFlowId);

        NodeFlow AddNodeFlow(NodeFlow parm);

        int UpdateNodeFlow(NodeFlow parm);
    }
}