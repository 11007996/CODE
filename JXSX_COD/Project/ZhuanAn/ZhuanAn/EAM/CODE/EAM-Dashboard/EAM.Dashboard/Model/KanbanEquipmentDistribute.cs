using SqlSugar;

namespace EAM.Dashboard.Model
{
    [SugarTable("EQU_Kanban_Equipment_Distribute")]
    [Tenant("HJDB")]
    public class KanbanEquipmentDistribute
    {
        public string PointName { get; set; }
        public int Count { get; set; }
    }
}