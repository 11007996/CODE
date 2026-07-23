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
    /// 流程实例任务Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IInstanceTaskService), ServiceLifetime = LifeTime.Transient)]
    public class InstanceTaskService : BaseService<InstanceTask>, IInstanceTaskService
    {
        public InstanceTaskService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        /// <summary>
        /// 查询流程实例任务列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<InstanceTaskDto> GetList(InstanceTaskQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .ToPage<InstanceTask, InstanceTaskDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="TaskId"></param>
        /// <returns></returns>
        public InstanceTask GetInfo(int TaskId)
        {
            var response = Queryable()
                .Where(x => x.TaskId == TaskId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加流程实例任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public InstanceTask AddInstanceTask(InstanceTask model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改流程实例任务
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateInstanceTask(InstanceTask model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<InstanceTask> QueryExp(InstanceTaskQueryDto parm)
        {
            var predicate = Expressionable.Create<InstanceTask>();

            return predicate;
        }
    }
}