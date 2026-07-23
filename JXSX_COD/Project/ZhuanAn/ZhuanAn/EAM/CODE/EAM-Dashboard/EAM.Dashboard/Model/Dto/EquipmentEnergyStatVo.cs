namespace EAM.Dashboard.Model.Dto
{
    /// <summary>
    /// 设备能耗
    /// </summary>
    public class EquipmentEnergyStatVo
    {
        public EnergyItem day90 { get; set; }
        public EnergyItem day30 { get; set; }
        public EnergyItem day1 { get; set; }
    }

    public class EnergyItem
    {
        public int Hour { get; set; }
        public int Energy { get; set; }
    }
}