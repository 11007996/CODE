using EAM.Model.Dto;
using EAM.Model.Equipment;
using EAM.Service.Equipment.IEquipmentService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2024-12-09
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 设备运行数据
    /// </summary>
    [Verify]
    [Route("equipment/EquipmentRuningRecord")]
    public class EquipmentRuningRecordController : BaseController
    {
        /// <summary>
        /// 设备运行数据接口
        /// </summary>
        private readonly IEquipmentRuningRecordService _EquipmentRuningRecordService;

        public EquipmentRuningRecordController(IEquipmentRuningRecordService EquipmentRuningRecordService)
        {
            _EquipmentRuningRecordService = EquipmentRuningRecordService;
        }

        /// <summary>
        /// 查询设备运行数据列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "equipment:runing:record:list")]
        public IActionResult QueryEquipmentRuningRecord([FromQuery] EquipmentRuningRecordQueryDto parm)
        {
            var response = _EquipmentRuningRecordService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 添加设备运行数据
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "equipment:runing:record:add")]
        [Log(Title = "设备运行数据", BusinessType = BusinessType.INSERT)]
        public IActionResult AddEquipmentRuningRecord([FromBody] EquipmentRuningRecordDto parm)
        {
            var modal = parm.Adapt<EquipmentRuningRecord>().ToCreate(HttpContext);

            var response = _EquipmentRuningRecordService.AddEquipmentRuningRecord(modal);

            return SUCCESS(response);
        }

        /// <summary>
        /// 删除设备运行数据
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "equipment:runing:record:delete")]
        [Log(Title = "设备运行数据", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteEquipmentRuningRecord([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<int>(ids);

            return ToResponse(_EquipmentRuningRecordService.Delete(idArr));
        }

        /// <summary>
        /// 导出设备运行数据
        /// </summary>
        /// <returns></returns>
        [Log(Title = "设备运行数据", BusinessType = BusinessType.EXPORT, IsSaveResponseData = false)]
        [HttpGet("export")]
        [ActionPermissionFilter(Permission = "equipment:runing:record:export")]
        public IActionResult Export([FromQuery] EquipmentRuningRecordQueryDto parm)
        {
            parm.PageNum = 1;
            parm.PageSize = 100000;
            var list = _EquipmentRuningRecordService.ExportList(parm).Result;
            if (list == null || list.Count <= 0)
            {
                return ToResponse(ResultCode.FAIL, "没有要导出的数据");
            }
            var result = ExportExcelMini(list, "设备运行数据", "设备运行数据");
            return ExportExcel(result.Item2, result.Item1);
        }

        /// <summary>
        /// 查询设备时实监控的记录
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("watch")]
        [ActionPermissionFilter(Permission = "equipment:watch:list")]
        public IActionResult QueryEquipmentRuningWatch([FromQuery] EquipmentRuningRecordQueryDto parm)
        {
            var response = _EquipmentRuningRecordService.GetWatchList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询设备时实监控的记录
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("watch/detail")]
        [ActionPermissionFilter(Permission = "equipment:watch:list")]
        public IActionResult QueryEquipmentRuningWatchDetail([FromQuery] EquipmentWatchDetailQueryDto parm)
        {
            var response = _EquipmentRuningRecordService.GetWatchDetail(parm);
            return SUCCESS(response);
        }
    }
}