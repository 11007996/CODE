using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Workflow;

namespace EAM.Service.Workflow.IWorkflowService
{
    /// <summary>
    /// 流程定义service接口
    /// </summary>
    public interface IProcessDefineService : IBaseService<ProcessDefine>
    {
        PagedInfo<ProcessDefineDto> GetList(ProcessDefineQueryDto parm);

        PagedInfo<DictDataDto> GetDict(ProcessDefineQueryDto parm);

        ProcessDefine GetInfo(string ProcessId);

        ProcessDefine AddProcessDefine(ProcessDefine parm);

        int UpdateProcessDefine(ProcessDefine parm);

        List<NodeApprover> GetProcessNodeApprover(string processId);
    }
}