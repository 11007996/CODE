using SqlSugar;

namespace EAM.Dashboard.Model
{
    [SugarTable("EQU_Kanban_Equipment_State")]
    public class KanBanEquipmentState
    {
        public string StateName { get; set; }
        public int Count { get; set; }
    }
}