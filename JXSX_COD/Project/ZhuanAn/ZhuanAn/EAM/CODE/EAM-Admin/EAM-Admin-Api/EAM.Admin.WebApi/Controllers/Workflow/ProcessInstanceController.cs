using EAM.Model.Dto;
using EAM.Service.Workflow.IWorkflowService;

using Microsoft.AspNetCore.Mvc;

//创建时间：2024-06-13
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 流程实例
    /// </summary>
    [Verify]
    [Route("workflow/ProcessInstance")]
    public class ProcessInstanceController : BaseController
    {
        /// <summary>
        /// 流程实例接口
        /// </summary>
        private readonly IProcessInstanceService _ProcessInstanceService;

        public ProcessInstanceController(IProcessInstanceService ProcessInstanceService)
        {
            _ProcessInstanceService = ProcessInstanceService;
        }

        /// <summary>
        /// 查询流程实例列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "processInstance:list")]
        public IActionResult QueryProcessInstance([FromQuery] ProcessInstanceQueryDto parm)
        {
            var response = _ProcessInstanceService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询当前用户关联的流程实例列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("mylist")]
        [ActionPermissionFilter(Permission = "processInstance:list")]
        public IActionResult QueryProcessInstanceByStatus([FromQuery] ProcessInstanceQueryDto parm)
        {
            parm.UserName = HttpContext.GetName();
            var response = _ProcessInstanceService.GetListByStatus(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询流程实例详情
        /// </summary>
        /// <param name="ProcessInstanceId"></param>
        /// <returns></returns>
        [HttpGet("{ProcessInstanceId}")]
        [ActionPermissionFilter(Permission = "processInstance:query")]
        public IActionResult GetProcessInstance(string ProcessInstanceId)
        {
            string username = HttpContext.GetName();
            var response = _ProcessInstanceService.GetInfo(ProcessInstanceId, username);

            var info = response.Adapt<ProcessInstanceDto>();
            return SUCCESS(info);
        }

        [HttpGet("init/{ProcessId}")]
        [ActionPermissionFilter(Permission = "processInstance:query")]
        public IActionResult GetProcessInstanceInitInfo(string ProcessId)
        {
            ProcessInstanceInitDto parm = new()
            {
                ProcessId = ProcessId,
                InitiatorId = HttpContext.GetName()
            };
            var response = _ProcessInstanceService.GetInitInfo(parm);

            var info = response.Adapt<ProcessInstanceDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加流程实例
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "processInstance:add")]
        [Log(Title = "流程实例", BusinessType = BusinessType.INSERT)]
        public IActionResult AddProcessInstance([FromBody] ProcessInstanceActionDto parm)
        {
            var modal = parm.Adapt<ProcessInstanceActionDto>().ToCreate(HttpContext).ToUpdate(HttpContext);
            modal.InitiatorId = modal.CreateBy;

            var response = _ProcessInstanceService.AddProcessInstance(modal);
            return SUCCESS(response);
        }

        /// <summary>
        /// 更新流程实例
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "processInstance:edit")]
        [Log(Title = "流程实例", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateProcessInstance([FromBody] ProcessInstanceActionDto parm)
        {
            var modal = parm.ToUpdate(HttpContext);
            var response = _ProcessInstanceService.UpdateProcessInstance(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除流程实例
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{id}")]
        [ActionPermissionFilter(Permission = "processInstance:delete")]
        [Log(Title = "流程实例", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteProcessInstance([FromRoute] string id)
        {
            string currUser = HttpContext.GetName();
            return ToResponse(_ProcessInstanceService.DeleteProcessInstance(id, currUser));
        }

        /// <summary>
        /// 查询流程实例详情
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("userNode")]
        [ActionPermissionFilter(Permission = "processInstance:query")]
        public IActionResult GetProcessInstanceNodeInfo([FromQuery] QueryUserActionInProcessDto parm)
        {
            parm.UserId = User?.Identity?.Name;
            var response = _ProcessInstanceService.QueryUserActionForProcess(parm);

            return SUCCESS(response);
        }

        /// <summary>
        /// 获取当前用户待处理流程总数
        /// </summary>
        /// <returns></returns>
        [HttpGet("count/Pending")]
        public IActionResult GetProcessInstancePendingCount()
        {
            string username = User?.Identity?.Name;
            var response = _ProcessInstanceService.GetUserPendingCount(username);

            return SUCCESS(response);
        }
    }
}