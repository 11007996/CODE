using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Equipment;

namespace EAM.Service.Equipment.IEquipmentService
{
    /// <summary>
    /// 设备报警代码service接口
    /// </summary>
    public interface IEquipmentWarnCodeService : IBaseService<EquipmentWarnCode>
    {
        PagedInfo<EquipmentWarnCodeDto> GetList(EquipmentWarnCodeQueryDto parm);

        EquipmentWarnCode GetInfo(int equipmentId, int warnCode);

        EquipmentWarnCode AddEquipmentWarnCode(EquipmentWarnCode parm);

        int UpdateEquipmentWarnCode(EquipmentWarnCode parm);

        int DeleteEquipmentWarnCode(EquipmentWarnCode model);

        (string, object, object) ImportEquipmentWarnCode(List<EquipmentWarnCode> list);
    }
}