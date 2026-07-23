using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Workflow;

namespace EAM.Service.Workflow.IWorkflowService
{
    /// <summary>
    /// 流程节点定义service接口
    /// </summary>
    public interface INodeDefineService : IBaseService<NodeDefine>
    {
        PagedInfo<NodeDefineDto> GetList(NodeDefineQueryDto parm);

        PagedInfo<NodeDetailDto> GetDetailList(NodeDefineQueryDto parm);

        List<DictDataDto> GetDict(NodeDefineQueryDto parm);

        NodeDefine GetInfo(int NodeId);

        NodeDefine AddNodeDefine(NodeDefine parm);

        int UpdateNodeDefine(NodeDefine parm);

        int DeleteNodeDefine(int nodeId);
    }
}