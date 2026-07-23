using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Equipment;

namespace EAM.Service.Equipment.IEquipmentService
{
    /// <summary>
    /// 设备保管service接口
    /// </summary>
    public interface IEquipmentStorageService : IBaseService<EquipmentStorageUsing>
    {
        PagedInfo<EquipmentStorageUsingDto> GetList(EquipmentStorageUsingQueryDto parm);

        EquipmentStorageUsing GetInfo(int equipmentId);

        EquipmentStorageUsing AddEquipmentStorage(EquipmentStorageUsing parm);

        int UpdateEquipmentStorage(EquipmentStorageUsing parm);

        bool ReceiveEquipmentStorage(OperateEquipmentStorageDto parm);

        int BatchReceiveEquipment(List<OperateEquipmentStorageDto> models);

        bool BackEquipmentStorage(OperateEquipmentStorageDto model);

        PagedInfo<EquipmentStorageRecordDto> GetRecordList(EquipmentStorageRecordQueryDto parm);
    }
}