namespace EAM.Dashboard.Model.Dto
{
    public class StatEquipmentRuningRecordVo
    {
        public int StatId { get; set; }

        public DateTime? StatDate { get; set; }

        public int EquipmentId { get; set; }

        public string EquipmentName { get; set; }

        public decimal TheoryCT { get; set; }

        public string SplitTime { get; set; }

        public DateTime? StatStartTime { get; set; }

        public DateTime? StatEndTime { get; set; }

        public DateTime? DataStartTime { get; set; }

        public DateTime? DataEndTime { get; set; }

        public int? ProductCount { get; set; }

        public int? DefectCount { get; set; }

        public int? RunSeconds { get; set; }

        public int? PlanEffectSeconds { get; set; }

        public int? StopSeconds { get; set; }

        public int? FaultSeconds { get; set; }

        public int? FaultCount { get; set; }

        public int? LastProductCount { get; set; }

        public decimal AvailabilityRate { get; set; }

        public decimal PerformanceRate { get; set; }

        public decimal QualityRate { get; set; }

        public decimal OEE { get; set; }

        public DateTime? UpdateTime { get; set; }

        public string LineName { get; set; }
    }
}