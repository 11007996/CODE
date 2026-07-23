using EAM.Model.Dto;
using EAM.Model.Equipment;
using EAM.Service.Equipment.IEquipmentService;
using Microsoft.AspNetCore.Mvc;

//创建时间：2024-09-30
namespace EAM.Admin.WebApi.Controllers
{
    /// <summary>
    /// 设备文件管理
    /// </summary>
    [Verify]
    [Route("equipment/EquipmentFile")]
    public class EquipmentFileController : BaseController
    {
        /// <summary>
        /// 设备文件信息接口
        /// </summary>
        private readonly IEquipmentFileService _EquipmentFileService;

        public EquipmentFileController(IEquipmentFileService EquipmentFileService)
        {
            _EquipmentFileService = EquipmentFileService;
        }

        #region 设备文件操作

        /// <summary>
        /// 查询设备文件列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("list")]
        [ActionPermissionFilter(Permission = "equipment:file:list")]
        public IActionResult QueryEquipmentFile([FromQuery] EquipmentFileQueryDto parm)
        {
            var response = _EquipmentFileService.GetList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 查询设备文件详情
        /// </summary>
        /// <param name="fileId"></param>
        /// <returns></returns>
        [HttpGet("{fileId}")]
        [ActionPermissionFilter(Permission = "equipment:file:query")]
        public IActionResult GetEquipmentFile(long fileId)
        {
            var response = _EquipmentFileService.GetInfo(fileId);

            return SUCCESS(response);
        }

        /// <summary>
        /// 添加设备文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [ActionPermissionFilter(Permission = "equipment:file:add")]
        [Log(Title = "设备文件", BusinessType = BusinessType.INSERT)]
        public IActionResult AddEquipmentFile([FromBody] EquipmentFile parm)
        {
            var response = _EquipmentFileService.AddEquipmentFile(parm);

            return SUCCESS(response);
        }

        /// <summary>
        /// 批量添加设备文件
        /// </summary>
        /// <returns></returns>
        [HttpPost("batchAdd")]
        [ActionPermissionFilter(Permission = "equipment:file:add")]
        [Log(Title = "设备文件", BusinessType = BusinessType.INSERT)]
        public IActionResult BatchAddEquipmentFile([FromBody] List<EquipmentFileDto> parm)
        {
            var model = parm.Adapt<List<EquipmentFile>>();

            var response = _EquipmentFileService.BatchAddEquipmentFile(model);

            return SUCCESS(response);
        }

        /// <summary>
        /// 删除设备文件
        /// </summary>
        /// <returns></returns>
        [HttpDelete("delete/{ids}")]
        [ActionPermissionFilter(Permission = "equipment:file:delete")]
        [Log(Title = "设备文件", BusinessType = BusinessType.DELETE)]
        public IActionResult DeleteEquipmentFile([FromRoute] string ids)
        {
            var idArr = Tools.SplitAndConvert<long>(ids);

            return ToResponse(_EquipmentFileService.DeleteEquipmentFile(idArr));
        }

        #endregion 设备文件操作

        #region 设备文件关联

        /// <summary>
        /// 查询设备文件关联列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        [HttpGet("bind/list")]
        [ActionPermissionFilter(Permission = "equipment:file:list")]
        public IActionResult QueryEquipmentFileBind([FromQuery] EquipmentFileBindQueryDto parm)
        {
            var response = _EquipmentFileService.GetBindList(parm);
            return SUCCESS(response);
        }

        /// <summary>
        /// 设备文件绑定
        /// </summary>
        /// <returns></returns>
        [HttpPost("bind")]
        [ActionPermissionFilter(Permission = "equipment:file:bind")]
        [Log(Title = "设备文件绑定", BusinessType = BusinessType.INSERT)]
        public IActionResult BindEquipmentFile([FromBody] List<EquipmentFileBindDto> parm)
        {
            var model = parm.Adapt<List<EquipmentFileBind>>();
            var response = _EquipmentFileService.BindEquipmentFile(model);

            return SUCCESS(response);
        }

        /// <summary>
        /// 设备文件解绑
        /// </summary>
        /// <returns></returns>
        [HttpDelete("unbind")]
        [ActionPermissionFilter(Permission = "equipment:file:bind")]
        [Log(Title = "设备文件解绑", BusinessType = BusinessType.DELETE)]
        public IActionResult UnbindEquipmentFile([FromBody] EquipmentFileBindDto parm)
        {
            var model = parm.Adapt<EquipmentFileBind>();
            var response = _EquipmentFileService.UnbindEquipmentFile(model);

            return SUCCESS(response);
        }

        #endregion 设备文件关联
    }
}