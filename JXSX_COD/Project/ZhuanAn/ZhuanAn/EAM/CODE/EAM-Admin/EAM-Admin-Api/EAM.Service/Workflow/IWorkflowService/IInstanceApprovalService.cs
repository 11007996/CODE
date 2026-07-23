using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Workflow;

namespace EAM.Service.Workflow.IWorkflowService
{
    /// <summary>
    /// 流程审批记录service接口
    /// </summary>
    public interface IInstanceApprovalService : IBaseService<InstanceApproval>
    {
        PagedInfo<InstanceApprovalDto> GetList(InstanceApprovalQueryDto parm);

        InstanceApproval GetInfo(int ApprovalId);

        InstanceApproval AddInstanceApproval(InstanceApproval parm);

        int UpdateInstanceApproval(InstanceApproval parm);
    }
}