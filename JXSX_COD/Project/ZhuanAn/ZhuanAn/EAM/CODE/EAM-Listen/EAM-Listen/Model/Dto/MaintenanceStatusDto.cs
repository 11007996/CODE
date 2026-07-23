namespace EAM.Listen.Model
{
    public class MaintenanceStatusDto
    {
        /// <summary>
        /// 资产编号
        /// </summary>
        public int EquipmentId { get; set; }

        /// <summary>
        /// 有无日保养
        /// </summary>
        public bool DayMaintenanceFlag { get; set; }

        /// <summary>
        /// 有无周保养
        /// </summary>
        public bool WeekMaintenanceFlag { get; set; }

        /// <summary>
        /// 有无月保养
        /// </summary>
        public bool MonthMaintenanceFlag { get; set; }
    }
}