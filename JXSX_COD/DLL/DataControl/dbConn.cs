using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using Oracle.ManagedDataAccess.Client;

public class dbConn
{
    OracleConnection dbconn;
    public dbConn()
    {
        string strConStr = this.GetConStr();
        dbconn = new OracleConnection(strConStr);
    }
    // Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
    private string GetConStr()
    {
        return ConfigurationManager.AppSettings["DefaultConnection"].ToString();
    }
    // Token: 0x06000003 RID: 3 RVA: 0x00002088 File Offset: 0x00000288
    public DataSet GetDataSetBySql(string sql)
    {
        string strConStr = this.GetConStr();
        dbconn.Open();
        DataSet ds = new DataSet();
        OracleCommand sqlcon = new OracleCommand();
        sqlcon.Connection = dbconn;
        sqlcon.CommandText = sql;
        OracleDataAdapter da = new OracleDataAdapter(sql, dbconn);
        dbconn.Close();
        da.Fill(ds);
        return ds;
    }

    // Token: 0x06000004 RID: 4 RVA: 0x000020E8 File Offset: 0x000002E8
    public DataSet GetDataSetBySql(string sql, Dictionary<string, object> para)
    {
        dbconn.Open();
        OracleCommand sqlcon = new OracleCommand();
        sqlcon.Connection = dbconn;
        sqlcon.CommandText = sql;
        sqlcon.CommandType = CommandType.Text;
        if (para != null)
        {
            foreach (KeyValuePair<string, object> kvp in para)
            {
                OracleParameter p = sqlcon.Parameters.Add(new OracleParameter(kvp.Key, OracleDbType.NVarchar2));
                p.Value = kvp.Value;
                p.Direction = ParameterDirection.InputOutput;
            }
        }
        OracleDataAdapter da = new OracleDataAdapter();
        da.SelectCommand = sqlcon;
        DataSet ds = new DataSet();
        ds.Locale = CultureInfo.InvariantCulture;
        dbconn.Close();
        da.Fill(ds);
        return ds;
    }

    // Token: 0x06000005 RID: 5 RVA: 0x000021EC File Offset: 0x000003EC
    public bool ExcuteSql(string sql)
    {
        try
        {
            dbconn.Open();
            OracleCommand sqlcon = new OracleCommand();
            sqlcon.Connection = dbconn;
            sqlcon.CommandText = sql;
            this.dbtran = dbconn.BeginTransaction();
            sqlcon.Transaction = this.dbtran;
            sqlcon.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            this.dbtran.Rollback();
            dbconn.Close();
            throw ex;
        }
        this.dbtran.Commit();
        dbconn.Close();
        return true;
    }

    // Token: 0x06000006 RID: 6 RVA: 0x0000228C File Offset: 0x0000048C
    public bool ExcuteSql(string sql, Dictionary<string, object> para)
    {
        try
        {
            dbconn.Open();
            OracleCommand sqlcon = new OracleCommand();
            sqlcon.Connection = dbconn;
            sqlcon.CommandText = sql;
            sqlcon.CommandType = CommandType.Text;
            if (para != null)
            {
                foreach (KeyValuePair<string, object> kvp in para)
                {
                    OracleParameter p = sqlcon.Parameters.Add(new OracleParameter(kvp.Key, OracleDbType.NVarchar2));
                    p.Value = kvp.Value;
                    p.Direction = ParameterDirection.InputOutput;
                }
            }
            sqlcon.Connection = dbconn;
            sqlcon.CommandText = sql;
            this.dbtran = dbconn.BeginTransaction();
            sqlcon.Transaction = this.dbtran;
            sqlcon.ExecuteNonQuery();
        }
        catch (Exception ex)
        {
            this.dbtran.Rollback();
            dbconn.Close();
            throw ex;
        }
        this.dbtran.Commit();
        dbconn.Close();
        return true;
    }
    public DataSet GetDataSetBySp(string spName, OracleParameter[] parameters)
    {
        return this.GetDataSetBySp(spName, parameters,"");
    }
    public DataSet GetDataSetBySp(string spName, OracleParameter[] parameters,string dsName)
    {
        DataSet result;
        try
        {
            string strConString = this.GetConStr();
            if (strConString.Equals(null))
            {
                throw new Exception("Connection is null");
            }
            OracleCommand sqlCommand = new OracleCommand();
            sqlCommand.Connection = new OracleConnection(strConString);
            sqlCommand.CommandType = CommandType.StoredProcedure;
            sqlCommand.CommandText = spName;
            sqlCommand.Parameters.AddRange(parameters);
            sqlCommand.Connection.Open();
            OracleDataAdapter sqlDataAdapter = new OracleDataAdapter();
            sqlDataAdapter.SelectCommand = sqlCommand;
            DataSet ds = new DataSet();
            if (dsName == "")
            {
                sqlDataAdapter.Fill(ds);
                sqlCommand.Connection.Close();
            }
            else
            {
                sqlDataAdapter.Fill(ds, dsName);
                sqlCommand.Connection.Close();
            }
            sqlCommand.Parameters.Clear();
            result = ds;
        }
        catch (Exception ex)
        {
            throw ex;
        }
        return result;
    }
    public int ExecuteSQLDML(string sqlstring, Dictionary<string, object> parameters, string database)
    {
        int result = -1;
        dbconn.Open();
        OracleCommand command = dbconn.CreateCommand();
        OracleTransaction myTrans = dbconn.BeginTransaction(IsolationLevel.ReadCommitted);
        command.Transaction = myTrans;
        try
        {
            command.CommandType = CommandType.Text;
            command.CommandText = sqlstring;
            if (parameters != null)
            {
                foreach (KeyValuePair<string, object> param in parameters)
                {
                    OracleParameter p = command.Parameters.Add(new OracleParameter(param.Key, OracleDbType.NVarchar2));
                    p.Value = param.Value;
                    p.Direction = ParameterDirection.InputOutput;
                }
            }
            result = command.ExecuteNonQuery();
            myTrans.Commit();
        }
        catch (Exception ex)
        {
            result = -1;
            myTrans.Rollback();
        }
        finally
        {
            dbconn.Close();
        }
        return result;
    }

    // Token: 0x04000001 RID: 1
    public static string MES = "DefaultConnection";
    // Token: 0x04000004 RID: 4
    private OracleTransaction dbtran = null;
}
