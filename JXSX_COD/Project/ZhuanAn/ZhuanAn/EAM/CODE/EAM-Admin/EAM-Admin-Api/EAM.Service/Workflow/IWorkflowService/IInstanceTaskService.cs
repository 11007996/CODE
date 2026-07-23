using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Workflow;

namespace EAM.Service.Workflow.IWorkflowService
{
    /// <summary>
    /// 流程实例任务service接口
    /// </summary>
    public interface IInstanceTaskService : IBaseService<InstanceTask>
    {
        PagedInfo<InstanceTaskDto> GetList(InstanceTaskQueryDto parm);

        InstanceTask GetInfo(int TaskId);

        InstanceTask AddInstanceTask(InstanceTask parm);

        int UpdateInstanceTask(InstanceTask parm);
    }
}