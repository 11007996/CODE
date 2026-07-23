using EAM.Model.Dto;
using EAM.Model.Workflow;
using EAM.Service.Workflow.IWorkflowService;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Workflow
{
    /// <summary>
    /// 流程实例Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IProcessInstanceBusinessService), ServiceLifetime = LifeTime.Transient)]
    public class ProcessInstanceBusinessService : BaseService<ProcessInstance>, IProcessInstanceBusinessService
    {
        public ProcessInstanceBusinessService(IHttpContextAccessor httpContextAccessor)
            : base(httpContextAccessor)
        {
        }

        public bool HandleProcessFiled(string processId, string processInstance, List<FormItemData> formData)
        {
            return true;
        }
    }
}