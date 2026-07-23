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
    /// 流程审批记录Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IInstanceApprovalService), ServiceLifetime = LifeTime.Transient)]
    public class InstanceApprovalService : BaseService<InstanceApproval>, IInstanceApprovalService
    {
        public InstanceApprovalService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        /// <summary>
        /// 查询流程审批记录列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<InstanceApprovalDto> GetList(InstanceApprovalQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .ToPage<InstanceApproval, InstanceApprovalDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="ApprovalId"></param>
        /// <returns></returns>
        public InstanceApproval GetInfo(int ApprovalId)
        {
            var response = Queryable()
                .Where(x => x.ApprovalId == ApprovalId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加流程审批记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public InstanceApproval AddInstanceApproval(InstanceApproval model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改流程审批记录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateInstanceApproval(InstanceApproval model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<InstanceApproval> QueryExp(InstanceApprovalQueryDto parm)
        {
            var predicate = Expressionable.Create<InstanceApproval>();

            return predicate;
        }
    }
}