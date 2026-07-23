using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Workflow;

namespace EAM.Service.Workflow.IWorkflowService
{
    /// <summary>
    /// 节点字段控件配置service接口
    /// </summary>
    public interface INodeFieldControlService : IBaseService<NodeFieldControl>
    {
        PagedInfo<NodeFieldControlDto> GetList(NodeFieldControlQueryDto parm);

        List<NodeFieldControlDetailDto> GetDetailList(NodeFieldControlQueryDto parm);

        List<NodeFieldControlDetailDto> GetDetailList(int nodeId);

        NodeFieldControl GetInfo(int NodeId);

        NodeFieldControl AddNodeFieldControl(NodeFieldControl parm);

        int UpdateNodeFieldControl(NodeFieldControl parm);

        int BatchUpdateNodeFieldControl(List<NodeFieldControl> parm);
    }
}