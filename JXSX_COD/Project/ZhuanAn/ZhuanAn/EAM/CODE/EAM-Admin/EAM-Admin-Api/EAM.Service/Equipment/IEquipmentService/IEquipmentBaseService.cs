using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Equipment;

namespace EAM.Service.Equipment.IEquipmentService
{
    /// <summary>
    /// 设备资产信息service接口
    /// </summary>
    public interface IEquipmentBaseService : IBaseService<EquipmentBase>
    {
        PagedInfo<EquipmentBaseDto> GetList(EquipmentBaseQueryDto parm);

        EquipmentBase GetInfo(int equipmentId);

        EquipmentBase GetInfoByAssetNo(string assetNo);

        EquipmentBase AddEquipmentBase(EquipmentBase parm);

        int UpdateEquipmentBase(EquipmentBase parm);

        int DeleteEquipmentBase(int[] idArr);

        (string, object, object) ImportEquipmentBase(List<EquipmentBase> list);

        PagedInfo<EquipmentBaseDto> ExportList(EquipmentBaseQueryDto parm);

        PagedInfo<DictDataDto> GetDict(EquipmentBaseQueryDto parm);

        PagedInfo<EquipmentIdleDto> GetIdle(EquipmentBaseQueryDto parm);

        PagedInfo<DictDataDto> GetCostCenterDict(EquipmentBaseQueryDto parm);

        PagedInfo<DictDataDto> GetCustomModelDict(EquipmentBaseQueryDto parm);
    }
}