using SqlSugar;

namespace webapi.Model
{
    [SugarTable("IMES.M_AWG_GB")]
    public class T_emp
    {
        [SugarColumn(ColumnName = "AWG")]
        public string AWG { get; set; }

        [SugarColumn(ColumnName = "GB")]
        public string GB { get; set; }

        //[SugarColumn(IsPrimaryKey = true, IsIdentity = true, ColumnName = "id")]   //IsPrimaryKey表示主键，IsIdentity表示自增长
        //public int Id { get; set; }
    }
}
