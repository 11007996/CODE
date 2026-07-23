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
    /// 流程附件Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IInstanceAttachmentService), ServiceLifetime = LifeTime.Transient)]
    public class InstanceAttachmentService : BaseService<InstanceAttachment>, IInstanceAttachmentService
    {
        public InstanceAttachmentService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        /// <summary>
        /// 查询流程附件列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<InstanceAttachmentDto> GetList(InstanceAttachmentQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .ToPage<InstanceAttachment, InstanceAttachmentDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="AttachmentId"></param>
        /// <returns></returns>
        public InstanceAttachment GetInfo(int AttachmentId)
        {
            var response = Queryable()
                .Where(x => x.AttachmentId == AttachmentId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加流程附件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public InstanceAttachment AddInstanceAttachment(InstanceAttachment model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改流程附件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateInstanceAttachment(InstanceAttachment model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<InstanceAttachment> QueryExp(InstanceAttachmentQueryDto parm)
        {
            var predicate = Expressionable.Create<InstanceAttachment>();

            return predicate;
        }
    }
}