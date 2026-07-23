using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Workflow;

namespace EAM.Service.Workflow.IWorkflowService
{
    /// <summary>
    /// 节点审批人配置service接口
    /// </summary>
    public interface INodeApproverService : IBaseService<NodeApprover>
    {
        PagedInfo<NodeApproverDto> GetList(NodeApproverQueryDto parm);

        PagedInfo<DictDataDto> GetDictByType(NodeApproverDictQueryDto parm);

        NodeApprover GetInfo(int NodeId);

        NodeApprover AddNodeApprover(NodeApprover parm);

        int UpdateNodeApprover(NodeApprover parm);
    }
}