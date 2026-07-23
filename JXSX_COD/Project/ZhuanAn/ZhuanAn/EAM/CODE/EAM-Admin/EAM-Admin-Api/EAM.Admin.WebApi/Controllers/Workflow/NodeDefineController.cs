using EAM.Model.Dto;
using EAM.Model.Workflow;
using EAM.Service.Workflow.IWorkflowService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2024-06-07
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 流程节点定义
    /// </summary>
    [Verify]
    [Route("workflow/NodeDefine")]
    public class NodeDefineController : BaseController
    {
        /// <summary>
        /// 流程节点定义接口
        /// </summary>
        private readonly INodeDefineService _NodeDefineService;

        /// <summary>
        /// 节点流向接口
        /// </summary>
        private readonly INodeFlowService _NodeFlowService;

        /// <summary>
        /// 节点字段控件配置接口
        /// </summary>
        private readonly INodeFieldControlService _NodeFieldControlService;

        /// <summary>
        /// 节点审批人配置接口
        /// </summary>
        private readonly INodeApproverService _NodeApproverService;

        public NodeDefineController(
            INodeDefineService nodeDefineService,
            INodeFlowService nodeFlowService,
            INodeFieldControlService nodeFieldControlService,
            INodeApproverService nodeApproverService)
        {
            _NodeDefineService = nodeDefineService;
            _NodeFlowService = nodeFlowService;
            _NodeFieldControlService = nodeFieldControlService;
            _NodeApproverService = nodeApproverService;
        }

        #region 节点定义

        /// <summary>
        /// 查询流程节点定义列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "process:list")]
        public IActionResult QueryNodeDefine([FromQuery] NodeDefineQueryDto parm)
        {
            var response = _NodeDefineService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询流程节点定义列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("detailList")]
        [ActionPermissionFilter(Permission = "process:list")]
        public IActionResult QueryNodeDefineDetailList([FromQuery] NodeDefineQueryDto parm)
        {
            var response = _NodeDefineService.GetDetailList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询流程节点定义列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("dict")]
        public IActionResult QueryNodeDefineDict([FromQuery] NodeDefineQueryDto parm)
        {
            var response = _NodeDefineService.GetDict(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询流程节点定义详情
        /// </summary>
        /// <param name="NodeId"></param>
        /// <returns></returns>
        [HttpGet("{NodeId}")]
        [ActionPermissionFilter(Permission = "process:query")]
        public IActionResult GetNodeDefine(int NodeId)
        {
            var response = _NodeDefineService.GetInfo(NodeId);

            var info = response.Adapt<NodeDefineDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加流程节点定义
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "process:add")]
        [Log(Title = "流程节点定义", BusinessType = BusinessType.INSERT)]
        public IActionResult AddNodeDefine([FromBody] NodeDefineDto parm)
        {
            var modal = parm.Adapt<NodeDefine>().ToCreate(HttpContext);

            var response = _NodeDefineService.AddNodeDefine(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新流程节点定义
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "process:edit")]
        [Log(Title = "流程节点定义", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateNodeDefine([FromBody] NodeDefineDto parm)
        {
            var modal = parm.Adapt<NodeDefine>().ToUpdate(HttpContext);
            var response = _NodeDefineService.UpdateNodeDefine(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除流程节点定义
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{nodeId}")]
        [ActionPermissionFilter(Permission = "process:delete")]
        [Log(Title = "流程节点定义", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteNodeDefine([FromRoute] int nodeId)
        {
            return ToResponse(_NodeDefineService.DeleteNodeDefine(nodeId));
        }

        #endregion 节点定义

        #region 节点流向处理

        /// <summary>
        /// 查询节点流向列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("flow/list")]
        [ActionPermissionFilter(Permission = "process:list")]
        public IActionResult QueryNodeFlow([FromQuery] NodeFlowQueryDto parm)
        {
            var response = _NodeFlowService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询节点流向详情
        /// </summary>
        /// <param name="NodeFlowId"></param>
        /// <returns></returns>
        [HttpGet("flow/{NodeFlowId}")]
        [ActionPermissionFilter(Permission = "process:query")]
        public IActionResult GetNodeFlow(int NodeFlowId)
        {
            var response = _NodeFlowService.GetInfo(NodeFlowId);

            var info = response.Adapt<NodeFlowDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加节点流向
        /// </summary>
        /// <returns></returns>
        [HttpPost("flow")]
        [ActionPermissionFilter(Permission = "process:add")]
        [Log(Title = "节点流向", BusinessType = BusinessType.INSERT)]
        public IActionResult AddNodeFlow([FromBody] NodeFlowDto parm)
        {
            var modal = parm.Adapt<NodeFlow>().ToCreate(HttpContext);

            var response = _NodeFlowService.AddNodeFlow(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新节点流向
        /// </summary>
        /// <returns></returns>
        [HttpPut("flow")]
        [ActionPermissionFilter(Permission = "process:edit")]
        [Log(Title = "节点流向", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateNodeFlow([FromBody] NodeFlowDto parm)
        {
            var modal = parm.Adapt<NodeFlow>().ToUpdate(HttpContext);
            var response = _NodeFlowService.UpdateNodeFlow(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除节点流向
        /// </summary>
        /// <returns></returns>
        [HttpDelete("flow/delete/{ids}")]
        [ActionPermissionFilter(Permission = "process:delete")]
        [Log(Title = "节点流向", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteNodeFlow([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_NodeFlowService.Delete(idArr));
        }

        #endregion 节点流向处理

        #region 节点表单控制

        /// <summary>
        /// 查询节点字段控件配置列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("field/list")]
        [ActionPermissionFilter(Permission = "process:list")]
        public IActionResult QueryNodeFieldControl([FromQuery] NodeFieldControlQueryDto parm)
        {
            var response = _NodeFieldControlService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询节点字段控件配置说情列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("field/detailList")]
        [ActionPermissionFilter(Permission = "process:list")]
        public IActionResult QueryNodeFieldControlDetailList([FromQuery] NodeFieldControlQueryDto parm)
        {
            var response = _NodeFieldControlService.GetDetailList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询节点字段控件配置详情
        /// </summary>
        /// <param name="NodeId"></param>
        /// <returns></returns>
        [HttpGet("field/{NodeId}")]
        [ActionPermissionFilter(Permission = "process:query")]
        public IActionResult GetNodeFieldControl(int NodeId)
        {
            var response = _NodeFieldControlService.GetInfo(NodeId);

            var info = response.Adapt<NodeFieldControlDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加节点字段控件配置
        /// </summary>
        /// <returns></returns>
        [HttpPost("field")]
        [ActionPermissionFilter(Permission = "process:add")]
        [Log(Title = "节点字段控件配置", BusinessType = BusinessType.INSERT)]
        public IActionResult AddNodeFieldControl([FromBody] NodeFieldControlDto parm)
        {
            var modal = parm.Adapt<NodeFieldControl>().ToCreate(HttpContext);

            var response = _NodeFieldControlService.AddNodeFieldControl(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新节点字段控件配置
        /// </summary>
        /// <returns></returns>
        [HttpPut("field")]
        [ActionPermissionFilter(Permission = "process:edit")]
        [Log(Title = "节点字段控件配置", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateNodeFieldControl([FromBody] NodeFieldControlDto parm)
        {
            var modal = parm.Adapt<NodeFieldControl>().ToUpdate(HttpContext);
            var response = _NodeFieldControlService.UpdateNodeFieldControl(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 批量更新节点字段控件配置
        /// </summary>
        /// <returns></returns>
        [HttpPut("field/batch")]
        [ActionPermissionFilter(Permission = "process:edit")]
        [Log(Title = "节点字段控件配置", BusinessType = BusinessType.UPDATE)]
        public IActionResult BatchUpdateNodeFieldControl([FromBody] List<NodeFieldControl> parm)
        {
            var response = _NodeFieldControlService.BatchUpdateNodeFieldControl(parm);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除节点字段控件配置
        /// </summary>
        /// <returns></returns>
        [HttpDelete("field/delete/{ids}")]
        [ActionPermissionFilter(Permission = "process:delete")]
        [Log(Title = "节点字段控件配置", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteNodeFieldControl([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_NodeFieldControlService.Delete(idArr));
        }

        #endregion 节点表单控制

        #region 节点审批人

        /// <summary>
        /// 查询节点审批人配置列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("approver/list")]
        [ActionPermissionFilter(Permission = "process:list")]
        public IActionResult QueryNodeApprover([FromQuery] NodeApproverQueryDto parm)
        {
            var response = _NodeApproverService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 根据审批人类型，查询可选项目
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("approver/dict")]
        [ActionPermissionFilter(Permission = "process:list")]
        public IActionResult QueryNodeApproverDict([FromQuery] NodeApproverDictQueryDto parm)
        {
            var response = _NodeApproverService.GetDictByType(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询节点审批人配置详情
        /// </summary>
        /// <param name="NodeId"></param>
        /// <returns></returns>
        [HttpGet("approver/{NodeId}")]
        [ActionPermissionFilter(Permission = "process:query")]
        public IActionResult GetNodeApprover(int NodeId)
        {
            var response = _NodeApproverService.GetInfo(NodeId);

            var info = response.Adapt<NodeApproverDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加节点审批人配置
        /// </summary>
        /// <returns></returns>
        [HttpPost("approver")]
        [ActionPermissionFilter(Permission = "process:add")]
        [Log(Title = "节点审批人配置", BusinessType = BusinessType.INSERT)]
        public IActionResult AddNodeApprover([FromBody] NodeApproverDto parm)
        {
            var modal = parm.Adapt<NodeApprover>().ToCreate(HttpContext);

            var response = _NodeApproverService.AddNodeApprover(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新节点审批人配置
        /// </summary>
        /// <returns></returns>
        [HttpPut("approver")]
        [ActionPermissionFilter(Permission = "process:edit")]
        [Log(Title = "节点审批人配置", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateNodeApprover([FromBody] NodeApproverDto parm)
        {
            var modal = parm.Adapt<NodeApprover>().ToUpdate(HttpContext);
            var response = _NodeApproverService.UpdateNodeApprover(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除节点审批人配置
        /// </summary>
        /// <returns></returns>
        [HttpDelete("approver/delete/{ids}")]
        [ActionPermissionFilter(Permission = "process:delete")]
        [Log(Title = "节点审批人配置", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteNodeApprover([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_NodeApproverService.Delete(idArr));
        }

        #endregion 节点审批人
    }
}