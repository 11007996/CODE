using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Workflow;
using EAM.Repository;
using EAM.Service.Workflow.IWorkflowService;
using EAM.ServiceCore.Model.Enums;
using Infrastructure;
using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Workflow
{
    /// <summary>
    /// 流程定义Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IProcessDefineService), ServiceLifetime = LifeTime.Transient)]
    public class ProcessDefineService : BaseService<ProcessDefine>, IProcessDefineService
    {
        public ProcessDefineService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        /// <summary>
        /// 查询流程定义列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<ProcessDefineDto> GetList(ProcessDefineQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .ToPage<ProcessDefine, ProcessDefineDto>(parm);

            return response;
        }

        /// <summary>
        /// 可选择的流程项
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<DictDataDto> GetDict(ProcessDefineQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .Where(it => it.Status == SysStatusConstant.正常)
                .Select(it => new DictDataDto()
                {
                    DictValue = it.ProcessId,
                    DictLabel = it.ProcessName,
                })
                .ToPage<DictDataDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="ProcessId"></param>
        /// <returns></returns>
        public ProcessDefine GetInfo(string ProcessId)
        {
            var response = Queryable()
                .Where(x => x.ProcessId == ProcessId)
                .First();

            return response;
        }

        public bool IsExist(string processId)
        {
            if (!string.IsNullOrEmpty(processId))
                return Queryable().Where(t => t.ProcessId == processId).Count() > 0;
            return false;
        }

        /// <summary>
        /// 添加流程定义
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public ProcessDefine AddProcessDefine(ProcessDefine model)
        {
            if (IsExist(model.ProcessId))
                throw new CustomException($"此流程编号[{model.ProcessId}]已存在");
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改流程定义
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateProcessDefine(ProcessDefine model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 获取流程所有节点审批人信息
        /// </summary>
        /// <param name="processId"></param>
        /// <returns></returns>
        public List<NodeApprover> GetProcessNodeApprover(string processId)
        {
            return Context.Queryable<NodeDefine>().Where(n => n.ProcessId == processId)
                  .LeftJoin<NodeApprover>((n, a) => n.NodeId == a.NodeId)
                  .OrderBy((n, a) => n.OrderNo)
                  .Select((n, a) => new NodeApprover
                  {
                      NodeName = n.NodeName,
                  }, true).ToList();
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<ProcessDefine> QueryExp(ProcessDefineQueryDto parm)
        {
            var predicate = Expressionable.Create<ProcessDefine>();

            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.ProcessName), it => it.ProcessName.Contains(parm.ProcessName));
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.Status), it => it.Status == parm.Status);
            return predicate;
        }
    }
}