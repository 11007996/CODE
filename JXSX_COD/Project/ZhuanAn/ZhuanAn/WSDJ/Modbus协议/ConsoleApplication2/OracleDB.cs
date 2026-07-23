using Oracle.ManagedDataAccess.Client;
using System.Configuration;
using System.Data;

namespace OracleDB
{
	public class OracleDB
	{
		//private static string oracleConnectionStr = ConfigurationManager.ConnectionStrings["mesoracle"].ToString();
		private static string oracleConnectionStr = ConfigurationManager.AppSettings["ConnectionString"].ToString();
		public static DataTable ExecuteDataTable(string sql, params OracleParameter[] paramList)
		{
			using (OracleConnection oracleConnection = new OracleConnection(oracleConnectionStr))
			{
				oracleConnection.Open();
				using (OracleCommand oracleCommand = oracleConnection.CreateCommand())
				{
					oracleCommand.CommandText = sql;
					oracleCommand.CommandType = CommandType.Text;
					oracleCommand.Parameters.AddRange(paramList);
					DataTable dataTable = new DataTable();
					OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand);
					oracleDataAdapter.Fill(dataTable);
					return dataTable;
				}
			}
		}

		public static DataTable GetDataTableFromProc(string proc, params OracleParameter[] paramList)
		{
			using (OracleConnection oracleConnection = new OracleConnection(oracleConnectionStr))
			{
				oracleConnection.Open();
				using (OracleCommand oracleCommand = new OracleCommand(proc, oracleConnection))
				{
					oracleCommand.CommandType = CommandType.StoredProcedure;
					oracleCommand.Parameters.AddRange(paramList);
					DataTable dataTable = new DataTable();
					OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand);
					oracleDataAdapter.Fill(dataTable);
					return dataTable;
				}
			}
		}

		public static DataTable GetDataTableNonParm(string sql)
		{
			using (OracleConnection oracleConnection = new OracleConnection(oracleConnectionStr))
			{
				oracleConnection.Open();
				using (OracleCommand oracleCommand = new OracleCommand(sql, oracleConnection))
				{
					oracleCommand.CommandType = CommandType.Text;
					DataTable dataTable = new DataTable();
					OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand);
					oracleDataAdapter.Fill(dataTable);
					return dataTable;
				}
			}
		}

		public static DataTable GetDataTableFormProcNonParm(string proc)
		{
			using (OracleConnection oracleConnection = new OracleConnection(oracleConnectionStr))
			{
				oracleConnection.Open();
				using (OracleCommand oracleCommand = new OracleCommand(proc, oracleConnection))
				{
					oracleCommand.CommandType = CommandType.StoredProcedure;
					DataTable dataTable = new DataTable();
					OracleDataAdapter oracleDataAdapter = new OracleDataAdapter(oracleCommand);
					oracleDataAdapter.Fill(dataTable);
					return dataTable;
				}
			}
		}

		public static int ExecuteNonQuery(string sql, params OracleParameter[] paramList)
		{
			using (OracleConnection oracleConnection = new OracleConnection(oracleConnectionStr))
			{
				oracleConnection.Open();
				using (OracleCommand oracleCommand = oracleConnection.CreateCommand())
				{
					oracleCommand.CommandText = sql;
					oracleCommand.Parameters.AddRange(paramList);
					return oracleCommand.ExecuteNonQuery();
				}
			}
		}

		public static int ExecuteNonQuery1(string sql)
		{
			using (OracleConnection oracleConnection = new OracleConnection(oracleConnectionStr))
			{
				oracleConnection.Open();
				using (OracleCommand oracleCommand = oracleConnection.CreateCommand())
				{
					oracleCommand.CommandText = sql;
					return oracleCommand.ExecuteNonQuery();
				}
			}
		}

		public static string GetStringFromDB(string proc, params OracleParameter[] paramList)
		{
			return "0";
		}

		public static object ExecuteScalar(string sql, params OracleParameter[] paramList)
		{
			using (OracleConnection oracleConnection = new OracleConnection(oracleConnectionStr))
			{
				oracleConnection.Open();
				using (OracleCommand oracleCommand = oracleConnection.CreateCommand())
				{
					oracleCommand.CommandText = sql;
					oracleCommand.Parameters.AddRange(paramList);
					return oracleCommand.ExecuteScalar();
				}
			}
		}
	}
}
