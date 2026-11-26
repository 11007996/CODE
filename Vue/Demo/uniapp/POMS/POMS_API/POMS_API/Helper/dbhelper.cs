using System;
using System.Collections.Generic;
using System.Data;
using System.Configuration;
using Oracle.ManagedDataAccess.Client;

namespace DBhelper
{
    public class mesdbhelper
    {
        public static string connString { get; set; } = string.Empty;
        public static DataSet ExecuteStoreProc(string procedureName, OracleParameter[] parameters)
        {
            DataSet ds = new DataSet();
            using (OracleConnection connection = new OracleConnection(connString))
            {
                connection.Open();
                using (OracleCommand command = new OracleCommand())
                {
                    try
                    {
                        command.Connection = connection;
                        command.CommandType = CommandType.StoredProcedure;
                        command.CommandText = procedureName;
                        foreach (OracleParameter parameter in parameters)
                        {
                            command.Parameters.Add(parameter);
                        }
                        OracleDataAdapter oda = new OracleDataAdapter();
                        oda.SelectCommand = command;

                        oda.Fill(ds);
                        connection.Close();
                    }
                    catch (Exception ex)
                    {
                        connection.Close();
                        throw ex;
                    }
                }
            }
            return ds;
        }
        public static Dictionary<string, object> ExecuteStoreProc(string procedureName, Dictionary<string, object> parameters)
        {

            var result = new Dictionary<string, object>();

            using (OracleConnection connection = new OracleConnection(connString))
            {
                connection.Open();
                using (OracleCommand command = new OracleCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = procedureName;
                    foreach (var param in parameters)
                    {

                        var p = command.Parameters.Add(new OracleParameter(param.Key, param.Value));
                        p.Direction = ParameterDirection.InputOutput;


                    }

                    command.ExecuteNonQuery();
                    foreach (OracleParameter param in command.Parameters)
                    {
                        result.Add(param.ParameterName, param.Value);
                    }
                }
                connection.Close();
            }
            return result;
        }
        public static string ExecSql(string SqlStr)
        {
            try
            {
                using (OracleConnection conn = new OracleConnection(connString))
                {
                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        conn.Open();
                        cmd.CommandText = SqlStr;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
                return "OK";
            }
            catch (Exception e)
            {
                return "NG" + e.Message;
            }

        }

        public static DataTable ExecuteDataTable(string sqlstring)
        {
            DataTable result = null;
            //using (OracleConnection connection = new OracleConnection(connectionString: OraConnectionString))
            using (OracleConnection connection = new OracleConnection(connString))
            {
                connection.Open();
                using (OracleCommand command = new OracleCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sqlstring;
                    // setup parameters
                    /* if (parameters != null)
                     {
                         foreach (var param in parameters)
                         {
                             var p = command.Parameters.Add(new OracleParameter(param.Key, param.Value));
                             p.Direction = ParameterDirection.InputOutput;
                         }
                     }*/
                    OracleDataAdapter da = new OracleDataAdapter();
                    da.SelectCommand = command;


                    DataSet table = new DataSet();
                    table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                    da.Fill(table);
                    result = table.Tables[0];
                }
                connection.Close();
            }
            return result;
        }

        public static object ExecuteSQL(string sqlstring, Dictionary<string, object> parameters)
        {
            object result = null;
            using (OracleConnection connection = new OracleConnection(connString))
            {
                connection.Open();
                using (OracleCommand command = new OracleCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sqlstring;
                    // setup parameters
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            var p = command.Parameters.Add(new OracleParameter(param.Key, param.Value));
                            p.Direction = ParameterDirection.InputOutput;
                        }
                    }
                    OracleDataAdapter da = new OracleDataAdapter();
                    da.SelectCommand = command;
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    result = ds;
                }
                connection.Close();
            }
            return result;
        }

        public static object ExecuteSQLa(string sqlstring)
        {
            object result = null;
            using (OracleConnection connection = new OracleConnection(connString))
            {
                connection.Open();
                using (OracleCommand command = new OracleCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sqlstring;
                    // setup parameter
                    OracleDataAdapter da = new OracleDataAdapter();
                    da.SelectCommand = command;
                    DataSet ds = new DataSet();
                    da.Fill(ds);
                    result = ds;
                }

                connection.Close();
            }
            return result;
        }


        public static DataSet ExecuteSQLList(string sqlstring, Dictionary<string, object> parameters)
        {
            DataSet result = null;
            //using (OracleConnection connection = new OracleConnection(connectionString: OraConnectionString))
            using (OracleConnection connection = new OracleConnection(connString))
            {
                connection.Open();
                using (OracleCommand command = new OracleCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = sqlstring;
                    // setup parameters
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            var p = command.Parameters.Add(new OracleParameter(param.Key, param.Value));
                            p.Direction = ParameterDirection.InputOutput;
                        }
                    }
                    OracleDataAdapter da = new OracleDataAdapter();
                    da.SelectCommand = command;


                    DataSet table = new DataSet();
                    table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                    da.Fill(table);
                    result = table;
                }
                connection.Close();
            }
            return result;
        }

        public static DataSet ExecuteSQLList1(string sqlstring)
        {
            DataSet result = null;
            //using (OracleConnection connection = new OracleConnection(connectionString: OraConnectionString))
            using (OracleConnection connection = new OracleConnection(connString))
            {
                connection.Open();
                using (OracleCommand command = new OracleCommand())
                {
                    command.Connection = connection;
                    command.CommandType = CommandType.Text;
                    command.CommandText = "select * from sajet.g_sn_status where serial_number='DLC83960QM6HDRCAP' ";
                    // setup parameters
                    /* if (parameters != null)
                     {
                         foreach (var param in parameters)
                         {
                             var p = command.Parameters.Add(new OracleParameter(param.Key, param.Value));
                             p.Direction = ParameterDirection.InputOutput;
                         }
                     }*/
                    OracleDataAdapter da = new OracleDataAdapter();
                    da.SelectCommand = command;


                    DataSet table = new DataSet();
                    table.Locale = System.Globalization.CultureInfo.InvariantCulture;
                    da.Fill(table);
                    result = table;
                }
                connection.Close();
            }
            return result;
        }



        //private static string dbProviderName = ConfigurationManager.AppSettings["DbHelperProvider"];

        //private static string dbConnectionString = ConfigurationManager.AppSettings["DbHelperConnectionString"];

        private OracleConnection connection;

        public OracleCommand GetSqlStringCommond(string sqlQuery)
        {

            OracleCommand dbCommand = connection.CreateCommand();

            dbCommand.CommandText = sqlQuery;

            dbCommand.CommandType = CommandType.Text;

            return dbCommand;

        }


        public static DataTable ExecuteDataTable(string sqlstring, OracleConnection connection)
        {
            DataTable result = null;

            using (OracleCommand command = new OracleCommand())
            {
                command.Connection = connection;
                command.CommandType = CommandType.Text;
                command.CommandText = sqlstring;
                OracleDataAdapter da = new OracleDataAdapter();
                da.SelectCommand = command;

                DataSet ds = new DataSet();
                ds.Locale = System.Globalization.CultureInfo.InvariantCulture;
                da.Fill(ds);
                result = ds.Tables[0];
            }
            return result;
        }

        public static DataSet ExecuteStoreProc(string procedureName, OracleParameter[] parameters, OracleConnection connection )
        {
            DataSet ds = new DataSet();
            using (OracleCommand command = new OracleCommand())
            {
                command.Connection = connection;
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = procedureName;
                foreach (OracleParameter parameter in parameters)
                {
                    command.Parameters.Add(parameter);
                }
                OracleDataAdapter oda = new OracleDataAdapter();
                oda.SelectCommand = command;

                oda.Fill(ds);
            }
            return ds;
        }

        //public static object ExecuteSQL(string sqlstring)
        //{
        //    return ExecuteSQL(sqlstring, null);
        //}
    }
}
