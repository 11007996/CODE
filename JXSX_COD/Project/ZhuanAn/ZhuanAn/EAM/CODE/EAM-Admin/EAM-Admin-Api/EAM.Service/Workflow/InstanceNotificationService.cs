using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Workflow;
using EAM.Repository;
using EAM.Service.Workflow.IWorkflowService;

using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Workflow
{
    /// <summary>
    /// 流程通知Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IInstanceNotificationService), ServiceLifetime = LifeTime.Transient)]
    public class InstanceNotificationService : BaseService<InstanceNotification>, IInstanceNotificationService
    {
        public InstanceNotificationService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        /// <summary>
        /// 查询流程通知列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<InstanceNotificationDto> GetList(InstanceNotificationQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .ToPage<InstanceNotification, InstanceNotificationDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="NotificationId"></param>
        /// <returns></returns>
        public InstanceNotification GetInfo(int NotificationId)
        {
            var response = Queryable()
                .Where(x => x.NotificationId == NotificationId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加流程通知
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public InstanceNotification AddInstanceNotification(InstanceNotification model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改流程通知
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateInstanceNotification(InstanceNotification model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<InstanceNotification> QueryExp(InstanceNotificationQueryDto parm)
        {
            var predicate = Expressionable.Create<InstanceNotification>();

            return predicate;
        }
    }
}