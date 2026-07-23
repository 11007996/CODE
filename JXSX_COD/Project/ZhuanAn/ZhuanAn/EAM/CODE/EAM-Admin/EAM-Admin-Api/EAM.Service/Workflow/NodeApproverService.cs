using EAM.Model;
using EAM.Model.Constant;

using EAM.Model.Dto;
using EAM.Model.Workflow;
using EAM.Repository;
using EAM.Service.Workflow.IWorkflowService;
using EAM.ServiceCore.Services;
using Infrastructure;

using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Workflow
{
    /// <summary>
    /// 节点审批人配置Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(INodeApproverService), ServiceLifetime = LifeTime.Transient)]
    public class NodeApproverService : BaseService<NodeApprover>, INodeApproverService
    {
        private readonly ISysUserService _userService;
        private readonly ISysRoleService _roleService;
        private readonly ISysDeptService _deptService;
        private readonly ISysPostService _postService;

        public NodeApproverService(
            IHttpContextAccessor httpContextAccessor,
            ISysUserService userService,
            ISysRoleService roleService,
            ISysDeptService deptService,
            ISysPostService postService) : base(httpContextAccessor)
        {
            _userService = userService;
            _roleService = roleService;
            _deptService = deptService;
            _postService = postService;
        }

        /// <summary>
        /// 查询节点审批人配置列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<NodeApproverDto> GetList(NodeApproverQueryDto parm)
        {
            var predicate = QueryExp(parm);

            var response = Queryable()
                .Where(predicate.ToExpression())
                .ToPage<NodeApprover, NodeApproverDto>(parm);

            return response;
        }

        /// <summary>
        /// 根据审批类型获取可选项
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<DictDataDto> GetDictByType(NodeApproverDictQueryDto parm)
        {
            if (string.IsNullOrEmpty(parm.ApproverType))
            {
                throw new CustomException("审批人类型不能为空");
            }
            PagedInfo<DictDataDto> response;
            switch (parm.ApproverType)
            {
                case ApproverTypeConstant.用户:
                    response = _userService.Queryable().ClearFilter()
                        .WhereIF(!string.IsNullOrEmpty(parm.KeyWord), it => it.NickName.Contains(parm.KeyWord))
                        .Select(it => new DictDataDto()
                        {
                            DictValue = it.UserName,
                            DictLabel = it.UserName + " - " + it.NickName
                        }).ToPage(parm);
                    break;

                case ApproverTypeConstant.角色:
                    response = _roleService.Queryable().ClearFilter()
                        .WhereIF(!string.IsNullOrEmpty(parm.KeyWord), it => it.RoleName.Contains(parm.KeyWord))
                        .Select(it => new DictDataDto()
                        {
                            DictValue = it.RoleId.ToString(),
                            DictLabel = it.RoleKey + " - " + it.RoleName
                        }).ToPage(parm);
                    break;

                case ApproverTypeConstant.职位:
                    response = _postService.Queryable()
                        .WhereIF(!string.IsNullOrEmpty(parm.KeyWord), it => it.PostName.Contains(parm.KeyWord))
                        .Select(it => new DictDataDto()
                        {
                            DictValue = it.PostId.ToString(),
                            DictLabel = it.PostCode + " - " + it.PostName
                        }).ToPage(parm);
                    break;

                case ApproverTypeConstant.部门领导:
                    response = _deptService.Queryable().ClearFilter()
                        .WhereIF(!string.IsNullOrEmpty(parm.KeyWord), it => it.DeptName.Contains(parm.KeyWord))
                        .Select(it => new DictDataDto()
                        {
                            DictValue = it.DeptId.ToString(),
                            DictLabel = it.DeptId.ToString() + " - " + it.DeptName
                        }).ToPage(parm);
                    break;

                case ApproverTypeConstant.直属主管:
                    List<DictDataDto> list = new()
                    {
                        new() {
                            DictValue = "",
                            DictLabel = "发起人主管"
                        }
                    };
                    response = new PagedInfo<DictDataDto>
                    {
                        Result = list
                    };
                    break;

                case ApproverTypeConstant.表单字段:
                    int? formId = Context.Queryable<ProcessDefine>().InnerJoin<NodeDefine>((p, n) => p.ProcessId == n.ProcessId && n.NodeId == parm.NodeId).Select(p => p.FormId).First();
                    response = Context.Queryable<FormField>()
                    .Where(it => it.FormId == formId)
                    .WhereIF(!string.IsNullOrEmpty(parm.KeyWord), it => it.FieldDesc.Contains(parm.KeyWord))
                    .Select(it => new DictDataDto()
                    {
                        DictValue = it.FieldName,
                        DictLabel = it.FieldName + " - " + it.FieldDesc
                    }).ToPage(parm);
                    break;

                default:
                    throw new CustomException($"审批类型[{parm.ApproverType}]无效");
            }
            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="NodeId"></param>
        /// <returns></returns>
        public NodeApprover GetInfo(int NodeId)
        {
            var response = Queryable()
                .Where(x => x.NodeId == NodeId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加节点审批人配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public NodeApprover AddNodeApprover(NodeApprover model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 修改节点审批人配置
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UpdateNodeApprover(NodeApprover model)
        {
            return Update(model, true);
        }

        /// <summary>
        /// 查询导出表达式
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        private static Expressionable<NodeApprover> QueryExp(NodeApproverQueryDto parm)
        {
            var predicate = Expressionable.Create<NodeApprover>();
            predicate.AndIF(parm.NodeId > 0, it => it.NodeId == parm.NodeId);
            return predicate;
        }
    }
}