using EAM.Model.Dto;
using EAM.Model.Equipment;
using EAM.Service.Equipment.IEquipmentService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2024-10-18
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 履历维修记录
    /// </summary>
    [Verify]
    [Route("equipment/ResumeRepair")]
    public class ResumeRepairController : BaseController
    {
        /// <summary>
        /// 履历维修记录接口
        /// </summary>
        private readonly IResumeRepairService _ResumeRepairService;

        public ResumeRepairController(IResumeRepairService ResumeRepairService)
        {
            _ResumeRepairService = ResumeRepairService;
        }

        /// <summary>
        /// 查询履历维修记录列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "resume:repair:list")]
        public IActionResult QueryResumeRepair([FromQuery] ResumeRepairQueryDto parm)
        {
            var response = _ResumeRepairService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询履历维修记录详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [ActionPermissionFilter(Permission = "resume:repair:query")]
        public IActionResult GetResumeRepair(int Id)
        {
            var response = _ResumeRepairService.GetInfo(Id);

            var info = response.Adapt<ResumeRepairDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加履历维修记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "resume:repair:add")]
        [Log(Title = "履历维修记录", BusinessType = BusinessType.INSERT)]
        public IActionResult AddResumeRepair([FromBody] ResumeRepairDto parm)
        {
            var modal = parm.Adapt<ResumeRepair>().ToCreate(HttpContext);

            var response = _ResumeRepairService.AddResumeRepair(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新履历维修记录
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "resume:repair:edit")]
        [Log(Title = "履历维修记录", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateResumeRepair([FromBody] ResumeRepairDto parm)
        {
            var modal = parm.Adapt<ResumeRepair>().ToUpdate(HttpContext);
            var response = _ResumeRepairService.UpdateResumeRepair(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除履历维修记录
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "resume:repair:delete")]
        [Log(Title = "履历维修记录", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteResumeRepair([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_ResumeRepairService.Delete(idArr));
        }

        /// <summary>
        /// 导出履历维修记录
        /// </summary>
        /// <returns></returns>
        [Log(Title = "履历维修记录", BusinessType = BusinessType.EXPORT, IsSaveResponseData = false)]
        [HttpGet("export")]
        [ActionPermissionFilter(Permission = "resume:repair:export")]
        public IActionResult Export([FromQuery] ResumeRepairQueryDto parm)
        {
            parm.PageNum = 1;
            parm.PageSize = 100000;
            var list = _ResumeRepairService.GetList(parm).Result;
            if (list == null || list.Count <= 0)
            {
                return ToResponse(ResultCode.FAIL, "没有要导出的数据");
            }
            var result = ExportExcelMini(list, "履历维修记录", "履历维修记录");
            return ExportExcel(result.Item2, result.Item1);
        }
    }
}