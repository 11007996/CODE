namespace EAM.Dashboard.Model.Dto
{
    /// <summary>
    /// 设备运行数据输入输出对象
    /// </summary>
    public class EquipmentRuningRecordVo
    {
        public int EquipmentId { get; set; }

        public int RunState { get; set; }

        public int? ProductCount { get; set; }

        public int? DefectCount { get; set; }

        public int? WarnState { get; set; }

        public int? WarnCode { get; set; }

        public DateTime? CreateTime { get; set; }

        public string EquipmentName { get; set; }

        public int? LineId { get; set; }

        public string LineName { get; set; }
    }
}