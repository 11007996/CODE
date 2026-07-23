using EAM.Model;
using EAM.Model.Dto;
using EAM.Model.Statistics;

namespace EAM.Service.Statistics.IStatisticsService
{
    /// <summary>
    /// 统计设备运行记录service接口
    /// </summary>
    public interface IStatEquipmentRuningRecordService : IBaseService<StatEquipmentRuningRecord>
    {
        PagedInfo<StatEquipmentRuningRecordDto> GetList(StatEquipmentRuningRecordQueryDto parm);

        StatEquipmentRuningRecord GetInfo(int Id);

        StatEquipmentRuningRecord AddStatEquipmentRuningRecord(StatEquipmentRuningRecord parm);

        int UpdateStatEquipmentRuningRecord(StatEquipmentRuningRecord parm);

        StatEquipmentRuningRecord StatOneEquipmentRunData(int equipmentId, DateTime statStartTime, DateTime statEndTime);

        List<StatEquipmentRuningRecord> StatEquipmentRunData(List<int> equipmentIds, DateTime startDate, DateTime endDate);
    }
}