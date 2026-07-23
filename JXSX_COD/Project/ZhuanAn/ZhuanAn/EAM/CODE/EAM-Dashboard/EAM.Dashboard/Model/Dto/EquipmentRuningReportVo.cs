using EAM.Model.Equipment;

namespace EAM.Dashboard.Model.Dto
{
    public class EquipmentRuningReportVo
    {
        public int EquipmentId { get; set; }
        public int? LineId { get; set; }
        public string LineName { get; set; }
        public string EquipmentName { get; set; }
        public int? WarnState { get; set; }
        public int? RunState { get; set; }
        public int? ProductCount { get; set; }
        public List<EquipmentRuningRecord> Records { get; set; }
    }
}