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
    /// 流程节点定义Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(INodeDefineService), ServiceLifetime = LifeTime.Transient)]
    public class NodeDefineService : BaseService<NodeDefine>, INodeDefineService
    {
        public NodeDefineService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        /// <summary>
        /// 查询流程节点定义列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<NodeDefineDto> GetList(NodeDefineQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                //.OrderBy("OrderNo asc")
                .Where(predicate.ToExpression())
                .ToPage<NodeDefine, NodeDefineDto>(parm);

            return response;
        }

        /// <summary>
        /// 根据流程ID，获取的所有节点
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public List<DictDataDto> GetDict(NodeDefineQueryDto parm)
        {
            var response = Queryable()
                .Where(it => it.ProcessId == parm.ProcessId)
                .OrderBy(it => it.OrderNo)
                .Select(it => new DictDataDto()
                {
                    DictValue = it.NodeId.ToString(),
                    DictLabel = it.NodeName
                })
                .ToList();

            return response;
        }

        /// <summary>
        /// 查询流程节点定义列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<NodeDetailDto> GetDetailList(NodeDefineQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .LeftJoin<ProcessDefine>((it, p) => it.ProcessId == p.ProcessId)
                .Where(predicate.ToExpression())
                .OrderByIF(parm.Sort?.ToLower() == "orderno", it => it.OrderNo, parm.SortType.ToLower().StartsWith("asc") ? OrderByType.Asc : OrderByType.Desc)
                .Select((it, p) => new NodeDetailDto()
                {
                    ProcessName = p.ProcessName,
                }, true)
               .ToPageNoSort<NodeDetailDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="NodeId"></param>
        /// <returns></returns>
        public NodeDefine GetInfo(int NodeId)
        {
            var response = Queryable()
                .Where(x => x.NodeId == NodeId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加流程节点定义
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public NodeDefine AddNodeDefine(NodeDefine model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改流程节点定义
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateNodeDefine(NodeDefine model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 删除流程节点定义
        /// </summary>
        /// <param name="nodeId"></param>
        /// <returns></returns>
        public int DeleteNodeDefine(int nodeId)
        {
            return Context.DeleteNav<NodeDefine>(it => it.NodeId == nodeId)
                .Include(it => it.NodeFlowNav)
                .Include(it => it.NodeFieldControlNav)
                .Include(it => it.NodeApproverNav).ExecuteCommand() ? 1 : 0;
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<NodeDefine> QueryExp(NodeDefineQueryDto parm)
        {
            var predicate = Expressionable.Create<NodeDefine>();
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.ProcessId), it => it.ProcessId == parm.ProcessId);
            predicate = predicate.AndIF(!string.IsNullOrEmpty(parm.NodeName), it => it.NodeName.Contains(parm.NodeName));
            return predicate;
        }
    }
}