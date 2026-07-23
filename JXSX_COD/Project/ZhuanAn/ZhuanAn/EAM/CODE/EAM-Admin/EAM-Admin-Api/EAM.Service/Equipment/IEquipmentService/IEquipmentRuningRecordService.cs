using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Equipment;
using EAM.Model.Statistics;

namespace EAM.Service.Equipment.IEquipmentService
{
    /// <summary>
    /// 设备运行数据service接口
    /// </summary>
    public interface IEquipmentRuningRecordService : IBaseService<EquipmentRuningRecord>
    {
        PagedInfo<EquipmentRuningRecordDto> GetList(EquipmentRuningRecordQueryDto parm);

        EquipmentRuningRecord AddEquipmentRuningRecord(EquipmentRuningRecord parm);

        PagedInfo<EquipmentRuningRecordDto> ExportList(EquipmentRuningRecordQueryDto parm);

        PagedInfo<EquipmentRuningWatchDto> GetWatchList(EquipmentRuningRecordQueryDto parm);

        StatEquipmentRuningRecordDto GetWatchDetail(EquipmentWatchDetailQueryDto parm);
    }
}