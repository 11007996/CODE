using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Equipment;

namespace EAM.Service.Equipment.IEquipmentService
{
    /// <summary>
    /// 机台类型service接口
    /// </summary>
    public interface IEquipmentTypeService : IBaseService<EquipmentType>
    {
        PagedInfo<EquipmentTypeDto> GetList(EquipmentTypeQueryDto parm);

        EquipmentType GetInfo(string EquipmentTypeName);

        EquipmentType AddEquipmentType(EquipmentType parm);

        int UpdateEquipmentType(EquipmentType parm);

        PagedInfo<DictDataDto> GetDict(EquipmentTypeQueryDto parm);
    }
}