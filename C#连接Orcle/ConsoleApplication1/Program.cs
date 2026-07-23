using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.ManagedDataAccess.Client;
using System.Data;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string sqlstr = "select * from sajet.sys_part where rownum < 2";
            dbConn dbconn = new dbConn();
            DataTable dt = new DataTable();
            dt = dbconn.GetDataSetBySql(sqlstr).Tables[0];
            string result = dt.Rows[0][0].ToString();
            Console.WriteLine(result);
            Console.ReadKey();
            /*
            try
            {
                dbConn dbconn = new dbConn();
                string Constr = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 172.18.32.208)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME =jxsxmesa)));User Id=SX11007996;Password=JA,passWord)!;";
                OracleConnection conn = new OracleConnection(Constr);
                //conn.ConnectionString = "Data Source=(DESCRIPTION =(ADDRESS = (PROTOCOL = TCP)(HOST = 172.18.32.208)(PORT = 1521))(CONNECT_DATA =(SERVER = DEDICATED)(SERVICE_NAME =jxsxmesa)));User Id=SX11007996;Password=JA,passWord)!;";
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "SELECT * FROM sajet.sys_part WHERE ROWNUM < 2";
                cmd.CommandType = CommandType.Text;
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine(string.Format("PART_ID:{0},PART_NO:{1}", reader["PART_ID"], reader["PART_NO"]));
                    Console.Read();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
             * */
        }
    }
}
