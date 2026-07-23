using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Workflow;

namespace EAM.Service.Workflow.IWorkflowService
{
    /// <summary>
    /// 流程实例表单数据service接口
    /// </summary>
    public interface IInstanceFormDataService : IBaseService<InstanceFormData>
    {
        PagedInfo<InstanceFormDataDto> GetList(InstanceFormDataQueryDto parm);

        InstanceFormData GetInfo(int FormDataId);

        InstanceFormData AddInstanceFormData(InstanceFormData parm);

        int UpdateInstanceFormData(InstanceFormData parm);
    }
}