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
    /// 流程实例表单数据Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IInstanceFormDataService), ServiceLifetime = LifeTime.Transient)]
    public class InstanceFormDataService : BaseService<InstanceFormData>, IInstanceFormDataService
    {
        public InstanceFormDataService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        /// <summary>
        /// 查询流程实例表单数据列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<InstanceFormDataDto> GetList(InstanceFormDataQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .ToPage<InstanceFormData, InstanceFormDataDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="FormDataId"></param>
        /// <returns></returns>
        public InstanceFormData GetInfo(int FormDataId)
        {
            var response = Queryable()
                // .Where(x => x.FormDataId == FormDataId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加流程实例表单数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public InstanceFormData AddInstanceFormData(InstanceFormData model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改流程实例表单数据
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateInstanceFormData(InstanceFormData model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<InstanceFormData> QueryExp(InstanceFormDataQueryDto parm)
        {
            var predicate = Expressionable.Create<InstanceFormData>();

            return predicate;
        }
    }
}