using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Equipment;

namespace EAM.Service.Equipment.IEquipmentService
{
    /// <summary>
    /// 设备扩展信息service接口
    /// </summary>
    public interface IEquipmentExtendService : IBaseService<EquipmentExtend>
    {
        PagedInfo<EquipmentExtendDto> GetList(EquipmentExtendQueryDto parm);

        EquipmentExtend GetInfo(int equipmentId);

        EquipmentExtend AddEquipmentExtend(EquipmentExtend parm);

        int UpdateEquipmentExtend(EquipmentExtend parm);
    }
}