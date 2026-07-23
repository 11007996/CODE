using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Oracle.ManagedDataAccess.Client;

namespace ConsoleApplication2
{
    class saveHelper
    {
        public bool checkHas(string ck)
        {
            string timeFlag = DateTime.Now.ToString("yyyyMMddHH00");
            string sql = "SELECT to_char(MAX(create_time)+2/24,'YYYYMMDDHH24') ||'00' from sajet.g_wsdj where DEVICE_CODE=:ck and create_time >sysdate-2/24";  //2小时收集一次数据
            OracleParameter[] Params = new OracleParameter[1];
            Params[0] = new OracleParameter(":ck", OracleDbType.Varchar2, 100, ck, ParameterDirection.Input);
            DataTable dt = OracleDB.OracleDB.ExecuteDataTable(sql, Params);
            if(dt.Rows[0][0].ToString() == timeFlag || dt.Rows[0][0].ToString()=="00")
            {
                return true;
            }
            return false;
        }

        public void saveData(Decimal temperature, Decimal temperatureMax, Decimal temperatureMin, Decimal humidity, Decimal humidityMax, Decimal humidityMin, double abnormal,string ck)
        {
            try
            {
                string timeFlag = DateTime.Now.ToString("yyyyMMddHH00");
                string sqlInsert = "INSERT INTO sajet.G_WSDJ VALUES(:temperature,:temperatureMax,:temperatureMin,:humidity,:humidityMax,:humidityMin,:abnormal,sysdate,:timeFlag,:ck)";
                OracleParameter[] Params = new OracleParameter[9];
                Params[0] = new OracleParameter(":temperature", OracleDbType.Varchar2, 32, temperature, ParameterDirection.Input);
                Params[1] = new OracleParameter(":temperatureMax", OracleDbType.Varchar2, 32, temperatureMax, ParameterDirection.Input);
                Params[2] = new OracleParameter(":temperatureMin", OracleDbType.Varchar2, 32, temperatureMin, ParameterDirection.Input);
                Params[3] = new OracleParameter(":humidity", OracleDbType.Varchar2, 32, humidity, ParameterDirection.Input);
                Params[4] = new OracleParameter(":humidityMax", OracleDbType.Varchar2, 32, humidityMax, ParameterDirection.Input);
                Params[5] = new OracleParameter(":humidityMin", OracleDbType.Varchar2, 32, humidityMin, ParameterDirection.Input);
                Params[6] = new OracleParameter(":abnormal", OracleDbType.Int32, 8, abnormal, ParameterDirection.Input);
                Params[7] = new OracleParameter(":timeFlag", OracleDbType.Varchar2, 100, timeFlag, ParameterDirection.Input);
                Params[8] = new OracleParameter(":ck", OracleDbType.Varchar2, 100, ck, ParameterDirection.Input);
                OracleDB.OracleDB.ExecuteScalar(sqlInsert, Params);
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }
    }
}
