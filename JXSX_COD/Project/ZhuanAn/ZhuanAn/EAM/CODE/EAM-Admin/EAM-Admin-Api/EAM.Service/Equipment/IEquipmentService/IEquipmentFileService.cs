using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Equipment;
using EAM.Model.System;

namespace EAM.Service.Equipment.IEquipmentService
{
    /// <summary>
    /// 设备文件service接口
    /// </summary>
    public interface IEquipmentFileService : IBaseService<EquipmentFile>
    {
        PagedInfo<SysFile> GetList(EquipmentFileQueryDto parm);

        SysFile GetInfo(long fileId);

        EquipmentFile AddEquipmentFile(EquipmentFile parm);

        int BatchAddEquipmentFile(List<EquipmentFile> parm);

        int DeleteEquipmentFile(long[] ids);

        PagedInfo<EquipmentFileBindDto> GetBindList(EquipmentFileBindQueryDto parm);

        int BindEquipmentFile(List<EquipmentFileBind> parm);

        int UnbindEquipmentFile(EquipmentFileBind parm);
    }
}