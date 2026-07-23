using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Workflow;
using EAM.Repository;
using EAM.Service.Workflow.IWorkflowService;
using Infrastructure;

using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Workflow
{
    /// <summary>
    /// 节点流向Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(INodeFlowService), ServiceLifetime = LifeTime.Transient)]
    public class NodeFlowService : BaseService<NodeFlow>, INodeFlowService
    {
        public NodeFlowService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }

        /// <summary>
        /// 查询节点流向列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<NodeFlowDto> GetList(NodeFlowQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .ToPage<NodeFlow, NodeFlowDto>(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="NodeFlowId"></param>
        /// <returns></returns>
        public NodeFlow GetInfo(int NodeFlowId)
        {
            var response = Queryable()
                .Where(x => x.NodeFlowId == NodeFlowId)
                .First();

            return response;
        }

        /// <summary>
        /// 检查是否存在相同的流向
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool IsExist(NodeFlow model)
        {
            return Queryable().Where(it => it.FromNodeId == model.FromNodeId && it.ActionType == model.ActionType && it.ConditionExpression == model.ConditionExpression && it.ToNodeId == model.ToNodeId && it.NodeFlowId != model.NodeFlowId).Count() > 0;
        }

        /// <summary>
        /// 添加节点流向
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public NodeFlow AddNodeFlow(NodeFlow model)
        {
            if (IsExist(model))
                throw new CustomException($"节点存在相同操作与条件的流程");
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改节点流向
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateNodeFlow(NodeFlow model)
        {
            if (IsExist(model))
                throw new CustomException($"节点存在相同操作与条件的流程");
            return Update(model, true);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<NodeFlow> QueryExp(NodeFlowQueryDto parm)
        {
            var predicate = Expressionable.Create<NodeFlow>();

            predicate = predicate.AndIF(parm.FromNodeId != null, it => it.FromNodeId == parm.FromNodeId);
            return predicate;
        }
    }
}