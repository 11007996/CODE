using EAM.Model.Dto;
using EAM.Model.Workflow;

namespace EAM.Service.Workflow.IWorkflowService
{
    /// <summary>
    /// 流程实例service接口
    /// </summary>
    public interface IProcessInstanceBusinessService : IBaseService<ProcessInstance>
    {
        bool HandleProcessFiled(string processId, string processInstance, List<FormItemData> formData);
    }
}