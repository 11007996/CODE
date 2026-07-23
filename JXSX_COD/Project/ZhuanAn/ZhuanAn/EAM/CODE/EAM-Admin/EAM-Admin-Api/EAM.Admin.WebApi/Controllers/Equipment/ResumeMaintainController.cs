using EAM.Model.Dto;
using EAM.Model.Equipment;
using EAM.Service.Equipment.IEquipmentService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2024-10-18
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 履历保养记录
    /// </summary>
    [Verify]
    [Route("equipment/ResumeMaintain")]
    public class ResumeMaintainController : BaseController
    {
        /// <summary>
        /// 履历保养记录接口
        /// </summary>
        private readonly IResumeMaintainService _ResumeMaintainService;

        public ResumeMaintainController(IResumeMaintainService ResumeMaintainService)
        {
            _ResumeMaintainService = ResumeMaintainService;
        }

        /// <summary>
        /// 查询履历保养记录列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "resume:maintain:list")]
        public IActionResult QueryResumeMaintain([FromQuery] ResumeMaintainQueryDto parm)
        {
            var response = _ResumeMaintainService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询履历保养记录详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [ActionPermissionFilter(Permission = "resume:maintain:query")]
        public IActionResult GetResumeMaintain(int Id)
        {
            var response = _ResumeMaintainService.GetInfo(Id);

            var info = response.Adapt<ResumeMaintainDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加履历保养记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "resume:maintain:add")]
        [Log(Title = "履历保养记录", BusinessType = BusinessType.INSERT)]
        public IActionResult AddResumeMaintain([FromBody] ResumeMaintainDto parm)
        {
            var modal = parm.Adapt<ResumeMaintain>().ToCreate(HttpContext);

            var response = _ResumeMaintainService.AddResumeMaintain(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新履历保养记录
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "resume:maintain:edit")]
        [Log(Title = "履历保养记录", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateResumeMaintain([FromBody] ResumeMaintainDto parm)
        {
            var modal = parm.Adapt<ResumeMaintain>().ToUpdate(HttpContext);
            var response = _ResumeMaintainService.UpdateResumeMaintain(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除履历保养记录
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "resume:maintain:delete")]
        [Log(Title = "履历保养记录", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteResumeMaintain([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_ResumeMaintainService.Delete(idArr));
        }

        /// <summary>
        /// 导出履历保养记录
        /// </summary>
        /// <returns></returns>
        [Log(Title = "履历保养记录", BusinessType = BusinessType.EXPORT, IsSaveResponseData = false)]
        [HttpGet("export")]
        [ActionPermissionFilter(Permission = "resume:maintain:export")]
        public IActionResult Export([FromQuery] ResumeMaintainQueryDto parm)
        {
            parm.PageNum = 1;
            parm.PageSize = 100000;
            var list = _ResumeMaintainService.GetList(parm).Result;
            if (list == null || list.Count <= 0)
            {
                return ToResponse(ResultCode.FAIL, "没有要导出的数据");
            }
            var result = ExportExcelMini(list, "履历保养记录", "履历保养记录");
            return ExportExcel(result.Item2, result.Item1);
        }
    }
}