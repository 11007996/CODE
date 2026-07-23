namespace EAM.Dashboard.Model.Dto
{
    public class StatTopEmp
    {
        public string EmpCode {  get; set; }
        public string EmpName { get; set; }
        public string Avatar {  get; set; }
        public int? LineId { get; set; }
        public string LineName {  get; set; }
        public int? FaultSeconds {  get; set; }
    }
}
