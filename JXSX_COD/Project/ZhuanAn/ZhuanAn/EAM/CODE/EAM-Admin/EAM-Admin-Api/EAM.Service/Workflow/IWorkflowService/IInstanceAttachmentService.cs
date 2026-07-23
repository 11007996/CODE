using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Workflow;

namespace EAM.Service.Workflow.IWorkflowService
{
    /// <summary>
    /// 流程附件service接口
    /// </summary>
    public interface IInstanceAttachmentService : IBaseService<InstanceAttachment>
    {
        PagedInfo<InstanceAttachmentDto> GetList(InstanceAttachmentQueryDto parm);

        InstanceAttachment GetInfo(int AttachmentId);

        InstanceAttachment AddInstanceAttachment(InstanceAttachment parm);

        int UpdateInstanceAttachment(InstanceAttachment parm);
    }
}