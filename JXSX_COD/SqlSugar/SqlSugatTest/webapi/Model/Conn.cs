using Dm;
using SqlSugar;

namespace webapi.Model
{
    public class Conn
    {
        public SqlSugarClient GetInstance()
        {
            SqlSugarClient DB = new SqlSugarClient(new ConnectionConfig
            {
                DbType = DbType.Oracle,
                ConnectionString = "Data Source =//172.18.32.208:1521/JXIMESADEV;User Id=ICTIMES;Password=IC7%imes#83622",
                IsAutoCloseConnection = true,
                InitKeyType = InitKeyType.Attribute,
                ConfigId = "OracleProd"
            });
            return DB;
        }
        
        
        
    }
}