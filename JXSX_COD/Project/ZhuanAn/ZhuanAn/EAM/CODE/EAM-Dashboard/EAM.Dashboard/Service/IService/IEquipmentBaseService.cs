using EAM.Dashboard.Model;
using EAM.Dashboard.Model.Dto;
using EAM.Model.Equipment;
using EAM.ServiceCore;

namespace EAM.Dashboard.Service.IService
{
    public interface IEquipmentBaseService : IBaseService<EquipmentRuningRecord>
    {
        public List<KanBanEquipmentState> GetEquipmentStateStat();

        public List<KanbanEquipmentDistribute> GetEquipmentDistributeStat();

        public List<EquipmentRuningReportVo> GetLastEquipmentRuningRecord(int minutes, int count);

        public EquipmentEnergyStatVo StatEquipmentEnergy();

        public List<EquipmentRuningStateCountVo> StatEquipmentStateCount(int minutes);

        public List<EquipmentRuningRecordVo> GetWarnEquipmentRuningRecord(int minutes);

        public EquipmentOEEStatVo StatEquipmentOEE();

        public ChartXYData<string, string, List<int>> StatEquipmentProductRate();

        public ChartXYData<string, string, List<decimal>> StatLinePerformanceRate(int days);

        public ChartXYData<string, string, List<decimal>> StatEquipmentFaultTime(int count);

        public List<StatTopEmp> StatTopEmp(int count);
    }
}