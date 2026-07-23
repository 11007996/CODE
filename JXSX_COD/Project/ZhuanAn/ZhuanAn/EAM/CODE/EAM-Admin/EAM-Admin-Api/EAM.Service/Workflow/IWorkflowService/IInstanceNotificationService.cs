using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Workflow;

namespace EAM.Service.Workflow.IWorkflowService
{
    /// <summary>
    /// 流程通知service接口
    /// </summary>
    public interface IInstanceNotificationService : IBaseService<InstanceNotification>
    {
        PagedInfo<InstanceNotificationDto> GetList(InstanceNotificationQueryDto parm);

        InstanceNotification GetInfo(int NotificationId);

        InstanceNotification AddInstanceNotification(InstanceNotification parm);

        int UpdateInstanceNotification(InstanceNotification parm);
    }
}