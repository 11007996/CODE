using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Workflow;

namespace EAM.Service.Workflow.IWorkflowService
{
    /// <summary>
    /// 流程实例service接口
    /// </summary>
    public interface IProcessInstanceService : IBaseService<ProcessInstance>
    {
        PagedInfo<ProcessInstanceDto> GetList(ProcessInstanceQueryDto parm);

        PagedInfo<ProcessInstanceDto> GetListByStatus(ProcessInstanceQueryDto parm);

        ProcessInstance GetInfo(string processInstanceId, string userName);

        ProcessInstance GetInitInfo(ProcessInstanceInitDto parm);

        ProcessInstance AddProcessInstance(ProcessInstanceActionDto parm);

        int UpdateProcessInstance(ProcessInstanceActionDto parm);

        NodeActionAndFieldDto QueryUserActionForProcess(QueryUserActionInProcessDto parm);

        int GetUserPendingCount(string userName);

        int DeleteProcessInstance(string processInstanceId, string username);
    }
}