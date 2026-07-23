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
    /// 节点字段控件配置Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(INodeFieldControlService), ServiceLifetime = LifeTime.Transient)]
    public class NodeFieldControlService : BaseService<NodeFieldControl>, INodeFieldControlService
    {
        public NodeFieldControlService(IHttpContextAccessor httpContextAccessor) : base(httpContextAccessor)
        {
        }
        /// <summary>
        /// 查询节点字段控件配置列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<NodeFieldControlDto> GetList(NodeFieldControlQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .ToPage<NodeFieldControl, NodeFieldControlDto>(parm);

            return response;
        }

        /// <summary>
        /// 查询节点字段控件配置列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public List<NodeFieldControlDetailDto> GetDetailList(NodeFieldControlQueryDto parm)
        {
            //获取节点所属表单
            ProcessDefine processInfo = Context.Queryable<ProcessDefine>()
                .InnerJoin<NodeDefine>((pd, nd) => pd.ProcessId == nd.ProcessId)
                .Where((pd, nd) => nd.NodeId == parm.NodeId)
                .Select((pd, nd) => new ProcessDefine() { }, true)
                .First();
            if (processInfo == null || processInfo.FormId == null || processInfo.FormId == 0)
                throw new CustomException("流程未关联表单");
            //获取节点的表单的字段设置
            var response = Context.Queryable<FormField>().LeftJoin<NodeFieldControl>((it, nfc) => nfc.NodeId == parm.NodeId && it.FormId == nfc.FormId && it.FieldName == nfc.FieldName)
                .Where((it, nfc) => it.FormId == processInfo.FormId)
                .Select((it, nfc) => new NodeFieldControlDetailDto
                {
                    NodeId = parm.NodeId,
                    FieldName = it.FieldName,
                    FieldDesc = it.FieldDesc,
                    Hidden = nfc.Hidden == null ? false : nfc.Hidden,
                    Editable = nfc.Editable == null ? false : nfc.Editable,
                    Required = nfc.Required == null ? false : nfc.Required
                }, true)
                .ToList();
            return response;
        }

        public List<NodeFieldControlDetailDto> GetDetailList(int nodeId)
        {
            //获取节点所属表单
            ProcessDefine processInfo = Context.Queryable<ProcessDefine>()
                .InnerJoin<NodeDefine>((pd, nd) => pd.ProcessId == nd.ProcessId)
                .Where((pd, nd) => nd.NodeId == nodeId)
                .Select((pd, nd) => new ProcessDefine() { }, true)
                .First();
            if (processInfo == null || processInfo.FormId == null || processInfo.FormId == 0)
                throw new CustomException("流程未关联表单");
            //获取节点的表单的字段设置
            var response = Context.Queryable<FormField>().LeftJoin<NodeFieldControl>((it, nfc) => nfc.NodeId == nodeId && it.FormId == nfc.FormId && it.FieldName == nfc.FieldName)
                .Where((it, nfc) => it.FormId == processInfo.FormId)
                .Select((it, nfc) => new NodeFieldControlDetailDto
                {
                    NodeId = nodeId,
                    FieldName = it.FieldName,
                    FieldDesc = it.FieldDesc,
                    Hidden = nfc.Hidden == null ? false : nfc.Hidden,
                    Editable = nfc.Editable == null ? false : nfc.Editable,
                    Required = nfc.Required == null ? false : nfc.Required
                }, true)
                .ToList();
            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="NodeId"></param>
        /// <returns></returns>
        public NodeFieldControl GetInfo(int NodeId)
        {
            var response = Queryable()
                .Where(x => x.NodeId == NodeId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加节点字段控件配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public NodeFieldControl AddNodeFieldControl(NodeFieldControl model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改节点字段控件配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateNodeFieldControl(NodeFieldControl model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 批量修改节点字段控件配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int BatchUpdateNodeFieldControl(List<NodeFieldControl> model)
        {
            Context.Deleteable<NodeFieldControl>().Where(it => it.NodeId == model.First().NodeId).ExecuteCommand();
            return Insert(model);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<NodeFieldControl> QueryExp(NodeFieldControlQueryDto parm)
        {
            var predicate = Expressionable.Create<NodeFieldControl>();

            predicate = predicate.AndIF(parm.NodeId != null, it => it.NodeId == parm.NodeId);
            return predicate;
        }
    }
}