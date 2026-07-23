using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Equipment;
using EAM.Model.System;
using EAM.Repository;
using EAM.Service.Equipment.IEquipmentService;

using EAM.ServiceCore.Services;

using Infrastructure.Attribute;
using Microsoft.AspNetCore.Http;

namespace EAM.Service.Equipment
{
    /// <summary>
    /// 设备文件关联Service业务层处理
    /// </summary>
    [AppService(ServiceType = typeof(IEquipmentFileService), ServiceLifetime = LifeTime.Transient)]
    public class EquipmentFileService : BaseService<EquipmentFile>, IEquipmentFileService
    {
        private readonly ISysFileService _sysFileService;

        public EquipmentFileService(
            IHttpContextAccessor contextAccessor,
            ISysFileService sysFileService)
            : base(contextAccessor)
        {
            _sysFileService = sysFileService;
        }

        /// <summary>
        /// 查询设备文件列表
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<SysFile> GetList(EquipmentFileQueryDto parm)
        {
            List<long> fileIds = Queryable().Select(it => it.FileId).ToList();//查出所有文件Id

            var response = _sysFileService.Queryable()
                .Where(it => fileIds.Contains(it.Id))
                .WhereIF(!string.IsNullOrEmpty(parm.RealName), it => it.RealName.Contains(parm.RealName))
                .ToPage(parm);

            return response;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="FileId"></param>
        /// <returns></returns>
        public SysFile GetInfo(long FileId)
        {
            var response = _sysFileService.Queryable()
                .Where(x => x.Id == FileId)
                .First();

            return response;
        }

        /// <summary>
        /// 添加设备文件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public EquipmentFile AddEquipmentFile(EquipmentFile model)
        {
            return Insertable(model).ExecuteReturnEntity();
        }

        /// <summary>
        /// 批量添加设备文件
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int BatchAddEquipmentFile(List<EquipmentFile> model)
        {
            return Insert(model);
        }

        /// <summary>
        /// 删除设备文件
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        public int DeleteEquipmentFile(long[] ids)
        {
            //先删除关联记录
            Context.Deleteable<EquipmentFileBind>().Where(it => ids.Contains((long)it.FileId)).ExecuteCommand();
            return Deleteable().Where(it => ids.Contains(it.FileId)).ExecuteCommand();
        }

        /// <summary>
        /// 查询绑定文件
        /// </summary>
        /// <param name="parm"></param>
        /// <returns></returns>
        public PagedInfo<EquipmentFileBindDto> GetBindList(EquipmentFileBindQueryDto parm)
        {
            PagedInfo<EquipmentFileBindDto> info = Context.Queryable<EquipmentFileBind>()
                .WhereIF(parm.EquipmentId != null && parm.EquipmentId > 0, it => it.EquipmentId == parm.EquipmentId)
                .WhereIF(parm.FileId > 0, it => it.FileId == parm.FileId)
                .LeftJoin<EquipmentBase>((it, e) => it.EquipmentId == e.EquipmentId)
                .Select((it, e) => new EquipmentFileBindDto() { AssetName = e.AssetName }, true)
                .ToPage(parm);

            List<long> ids = info.Result.Select(it => it.FileId).ToList();
            //连接主库的文件表,填充映射文件名
            List<SysFile> files = _sysFileService.Queryable().Where(it => ids.Contains(it.Id)).ToList();
            foreach (EquipmentFileBindDto item in info.Result)
            {
                SysFile file = files.Find(it => it.Id == item.FileId);
                if (file != null)
                {
                    item.FileName = file.RealName;
                    item.AccessUrl = file.AccessUrl;
                }
            }
            return info;
        }

        /// <summary>
        /// 设备文件绑定
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int BindEquipmentFile(List<EquipmentFileBind> model)
        {
            //先删除关联记录
            var x = Context.Storageable(model).ToStorage();
            return x.AsInsertable.ExecuteCommand();
        }

        /// <summary>
        /// 设备文件解绑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public int UnbindEquipmentFile(EquipmentFileBind model)
        {
            //先删除关联记录
            return Context.Deleteable(model).ExecuteCommand();
        }
    }
}