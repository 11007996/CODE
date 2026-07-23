using EAM.Model.Dto;
using EAM.Model.Equipment;
using EAM.Service.Equipment.IEquipmentService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2024-10-11
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 设备保养记录
    /// </summary>
    [Verify]
    [Route("equipment/MaintainRecord")]
    public class MaintainRecordController : BaseController
    {
        /// <summary>
        /// 设备保养记录接口
        /// </summary>
        private readonly IMaintainRecordService _MaintainRecordService;

        public MaintainRecordController(IMaintainRecordService MaintainRecordService)
        {
            _MaintainRecordService = MaintainRecordService;
        }

        /// <summary>
        /// 查询设备保养记录列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "maintain:record:list")]
        public IActionResult QueryMaintainRecord([FromQuery] MaintainRecordQueryDto parm)
        {
            var response = _MaintainRecordService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询设备保养记录详情
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id}")]
        [ActionPermissionFilter(Permission = "maintain:record:query")]
        public IActionResult GetMaintainRecord(int Id)
        {
            var response = _MaintainRecordService.GetInfo(Id);

            var info = response.Adapt<MaintainRecordDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 添加设备保养记录
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "maintain:record:add")]
        [Log(Title = "设备保养记录", BusinessType = BusinessType.INSERT)]
        public IActionResult AddMaintainRecord([FromBody] MaintainRecordDto parm)
        {
            var modal = parm.Adapt<MaintainRecord>().ToCreate(HttpContext).ToUpdate(HttpContext);

            var response = _MaintainRecordService.AddMaintainRecord(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 更新设备保养记录
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [ActionPermissionFilter(Permission = "maintain:record:edit")]
        [Log(Title = "设备保养记录", BusinessType = BusinessType.UPDATE)]
        public IActionResult UpdateMaintainRecord([FromBody] MaintainRecordDto parm)
        {
            var modal = parm.Adapt<MaintainRecord>().ToUpdate(HttpContext);
            var response = _MaintainRecordService.UpdateMaintainRecord(modal);

            return ToResponse(response);
        }

        /// <summary>
        /// 删除设备保养记录
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "maintain:record:delete")]
        [Log(Title = "设备保养记录", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteMaintainRecord([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_MaintainRecordService.DeleteMaintainRecord(idArr));
        }

        /// <summary>
        /// 导出设备保养记录
        /// </summary>
        /// <returns></returns>
        [Log(Title = "设备保养记录", BusinessType = BusinessType.EXPORT, IsSaveResponseData = false)]
        [HttpGet("export")]
        [ActionPermissionFilter(Permission = "maintain:record:export")]
        public IActionResult Export([FromQuery] MaintainRecordQueryDto parm)
        {
            parm.PageNum = 1;
            parm.PageSize = 100000;
            var list = _MaintainRecordService.ExportList(parm).Result;
            if (list == null || list.Count <= 0)
            {
                return ToResponse(ResultCode.FAIL, "没有要导出的数据");
            }
            var result = ExportExcelMini(list, "设备保养记录", "设备保养记录");
            return ExportExcel(result.Item2, result.Item1);
        }

        /// <summary>
        /// 查询设备保养记录详情
        /// </summary>
        /// <param name = "param" ></param>
        /// <returns></returns>
        [HttpGet("detail")]
        [ActionPermissionFilter(Permission = "maintain:record:query")]
        public IActionResult DetailMaintainRecord(MaintainRecordQueryDetailDto param)
        {
            var response = _MaintainRecordService.GetDetail(param);

            var info = response.Adapt<MaintainRecordDto>();
            return SUCCESS(info);
        }

        /// <summary>
        /// 全局维护
        /// </summary>
        /// <returns></returns>
        [HttpPost("globalMaintain")]
        [ActionPermissionFilter(Permission = "maintain:record:globalMaintain")]
        [Log(Title = "设备保养记录", BusinessType = BusinessType.INSERT)]
        public IActionResult GlobalMaintainRecord([FromBody] GlobalMaintainRecordDto parm)
        {
            parm.ToUpdate(HttpContext);
            var response = _MaintainRecordService.GlobalMaintainRecord(parm);

            return SUCCESS(response);
        }
    }
}