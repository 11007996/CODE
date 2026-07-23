namespace EAM.Dashboard.Model.Dto
{
    /// <summary>
    /// 实时运行状态
    /// </summary>
    public class EquipmentRuningStateCountVo
    {
        public string StateName { get; set; }
        public int Count { get; set; }
        public string Color { get; set; }
        public int? Rate { get; set; }
    }
}