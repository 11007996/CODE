using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Equipment;

namespace EAM.Service.Equipment.IEquipmentService
{
    /// <summary>
    /// 设备计划停机时间service接口
    /// </summary>
    public interface IEquipmentPlanTimeService : IBaseService<EquipmentPlanTime>
    {
        PagedInfo<EquipmentPlanTimeDto> GetList(EquipmentPlanTimeQueryDto parm);

        EquipmentPlanTime GetInfo(int Id);

        EquipmentPlanTime AddEquipmentPlanTime(EquipmentPlanTime parm);

        int UpdateEquipmentPlanTime(EquipmentPlanTime parm);
    }
}