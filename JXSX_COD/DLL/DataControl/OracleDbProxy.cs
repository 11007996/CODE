using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.OracleClient;

namespace MesToSapApi.DataControl
{
    public class OracleDbProxy
    {
        
        private OracleConnection _conn = null;
        private string _connString = ConfigurationManager.AppSettings["DefaultConnection"].ToString();
        /// <summary>
        /// Initializes a new instance of the <see cref="OracleDB"/> class.
        /// </summary>
        public OracleDbProxy()
        {
            this._conn = new OracleConnection(_connString);
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="OracleDB"/> class.
        /// </summary>
        /// <param name="connString">The conn string.</param>
        public OracleDbProxy(string connString)
        {
            this._connString = connString;
            this._conn = new OracleConnection(this._connString);
        }
        /// <summary>
        /// Releases unmanaged resources and performs other cleanup operations before the
        /// <see cref="OracleDB"/> is reclaimed by garbage collection.
        /// </summary>
        ~OracleDbProxy()
        {
            try
            {
                if (this._conn != null)
                {
                    this._conn.Close();
                }

                this.Dispose();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                throw ex;
            }
        }

        #region IDatabase Members

        /// <summary>
        /// Opens this instance.
        /// </summary>
        public void Open()
        {
            if (this._conn == null)
            {
                this._conn = new OracleConnection(this._connString);
            }

            if (this._conn.State != ConnectionState.Open)
            {
                this._conn.Open();
            }
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
            if (this._conn != null)
            {
                this._conn.Close();
            }
        }

        /// <summary>
        /// Releases unmanaged and - optionally - managed resources
        /// </summary>
        public void Dispose()
        {
            if (this._conn != null)
            {
                this._conn.Close();
                this._conn = null;
            }
        }
        /// <summary>
        /// Executes the SQL.
        /// </summary>
        /// <param name="SqlString">The SQL string.</param>
        /// <returns></returns>
        public int ExecuteSQL(string SqlString)
        {
            int count = -1;

            this.Open();

            try
            {
                OracleCommand cmd = new OracleCommand(SqlString, this._conn);
                count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                throw ex;
            }
            finally
            {
                this.Close();
            }
            return count;
        }

        /// <summary>
        /// Executes the SQL.
        /// </summary>
        /// <param name="SqlString">The SQL string.</param>
        /// <param name="Parameters">The parameters.</param>
        /// <returns></returns>
        public int ExecuteSQL(string SqlString, Hashtable Parameters)
        {
            int count = -1;

            this.Open();

            try
            {
                OracleCommand cmd = new OracleCommand();

                PrepareCommand(cmd, SqlString, Parameters);

                count = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                throw ex;
            }
            finally
            {
                this.Close();
            }
            return count;
        }

        /// <summary>
        /// Executes the SQL.
        /// </summary>
        /// <param name="SqlStrings">The SQL strings.</param>
        /// <returns></returns>
        public bool ExecuteSQL(ArrayList SqlStrings)
        {
            bool success = true;

            this.Open();

            OracleCommand cmd = new OracleCommand();

            OracleTransaction trans = this._conn.BeginTransaction();
            cmd.Transaction = trans;

            try
            {
                foreach (string str in SqlStrings)
                {
                    cmd.CommandText = str;
                    cmd.ExecuteNonQuery();
                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                success = false;
                trans.Rollback();
                Trace.WriteLine(ex.ToString());
                throw ex;
            }
            finally
            {
                this.Close();
            }

            return success;
        }


        /// <summary>
        /// Executes the SQL.
        /// </summary>
        /// <param name="SqlStrings">The SQL strings.</param>
        /// <param name="Parameters">The parameters.</param>
        /// <returns></returns>
        public bool ExecuteSQL(List<string> SqlStrings, List<Hashtable> Parameters)
        {
            bool success = true;

            if (SqlStrings.Count != Parameters.Count)
                throw new ArgumentException("SQL Strings Count Not Equel Parameters Count");

            this.Open();

            OracleCommand cmd = new OracleCommand();

            OracleTransaction trans = this._conn.BeginTransaction();
            cmd.Transaction = trans;

            string[] arrSqlString = SqlStrings.ToArray();
            Hashtable[] arrParameters = Parameters.ToArray();

            try
            {
                for (int i = 0; i < arrSqlString.Length; i++)
                {
                    PrepareCommand(cmd, arrSqlString[i], arrParameters[i]);

                    cmd.ExecuteNonQuery();
                }

                trans.Commit();
            }
            catch (Exception ex)
            {
                success = false;
                trans.Rollback();
                Trace.WriteLine(ex.ToString());
                throw ex;
            }
            finally
            {
                this.Close();
            }

            return success;
        }

        /// <summary>
        /// Inserts the record into specified table name.
        /// </summary>
        /// <param name="TableName">Name of the table.</param>
        /// <param name="Cols">The cols.</param>
        /// <returns></returns>
        public bool Insert(string TableName, Hashtable Cols)
        {
            int Count = 0;

            if (Cols.Count <= 0)
                return true;

            string Fields = " (";
            string Values = " Values(";

            foreach (DictionaryEntry item in Cols)
            {
                if (Count != 0)
                {
                    Fields += ",";
                    Values += ",";
                }

                Fields += item.Key.ToString();

                if (item.Value.GetType().ToString() == "System.String")
                    Values += "'" + item.Value.ToString() + "'";
                else
                    Values += item.Value.ToString();

                Count++;
            }

            Fields += ")";
            Values += ")";

            string SqlString = "Insert Into " + TableName + Fields + Values;

            return Convert.ToBoolean(this.ExecuteSQL(SqlString));
        }

        /// <summary>
        /// Updates the specified table name.
        /// </summary>
        /// <param name="TableName">Name of the table.</param>
        /// <param name="Cols">The cols.</param>
        /// <param name="Where">The where.</param>
        /// <returns></returns>
        public bool Update(string TableName, Hashtable Cols, string Where)
        {
            int Count = 0;

            if (Cols.Count <= 0)
                return true;

            string Fields = " ";

            foreach (DictionaryEntry item in Cols)
            {
                if (Count != 0)
                    Fields += ",";

                Fields += item.Key.ToString();
                Fields += "=";
                if (item.Value.GetType().ToString() == "System.String")
                    Fields += "'" + item.Value.ToString() + "'";
                else
                    Fields += item.Value.ToString();
                Count++;
            }

            Fields += " ";

            string SqlString = "Update " + TableName + " Set " + Fields + " " + Where;
            return Convert.ToBoolean(this.ExecuteSQL(SqlString));
        }

        /// <summary>
        /// Gets the data set.
        /// </summary>
        /// <param name="SqlString">The SQL string.</param>
        /// <returns></returns>
        public DataSet GetDataSet(string SqlString)
        {
            this.Open();

            DataSet ds = new DataSet();

            try
            {
                OracleDataAdapter da = new OracleDataAdapter(SqlString, this._conn);
                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                throw ex;
            }
            finally
            {
                this.Close();
            }

            return ds;
        }

        /// <summary>
        /// Gets the data set.
        /// </summary>
        /// <param name="SqlString">The SQL string.</param>
        /// <returns></returns>
        public DataSet GetDataSet(string SqlString, Hashtable Parameters)
        {
            this.Open();

            DataSet ds = new DataSet();

            try
            {
                OracleCommand cmd = new OracleCommand();

                PrepareCommand(cmd, SqlString, Parameters);

                OracleDataAdapter da = new OracleDataAdapter(cmd);

                da.Fill(ds);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                throw ex;
            }
            finally
            {
                this.Close();
            }

            return ds;
        }

        /// <summary>
        /// Gets the data table.
        /// </summary>
        /// <param name="SqlString">The SQL string.</param>
        /// <returns></returns>
        public DataTable GetDataTable(string SqlString)
        {
            this.Open();

            DataTable dt = new DataTable();

            try
            {
                OracleDataAdapter da = new OracleDataAdapter(SqlString, this._conn);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                throw ex;
            }
            finally
            {
                this.Close();
            }

            return dt;
        }

        /// <summary>
        /// Gets the data table.
        /// </summary>
        /// <param name="SqlString">The SQL string.</param>
        /// <param name="Parameters">The parameters.</param>
        /// <returns></returns>
        public DataTable GetDataTable(string SqlString, Hashtable Parameters)
        {
            this.Open();

            DataTable dt = new DataTable();

            OracleCommand cmd = new OracleCommand();

            PrepareCommand(cmd, SqlString, Parameters);

            try
            {
                OracleDataAdapter da = new OracleDataAdapter(cmd);
                da.Fill(dt);
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                throw ex;
            }
            finally
            {
                this.Close();
            }

            return dt;
        }

        /// <summary>
        /// Gets the data row.
        /// </summary>
        /// <param name="SqlString">The SQL string.</param>
        /// <returns></returns>
        public DataRow GetDataRow(string SqlString)
        {
            using (DataSet ds = this.GetDataSet(SqlString))
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0];
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets the data row.
        /// </summary>
        /// <param name="SqlString">The SQL string.</param>
        /// <param name="Parameters">The parameters.</param>
        /// <returns></returns>
        public DataRow GetDataRow(string SqlString, Hashtable Parameters)
        {
            using (DataSet ds = this.GetDataSet(SqlString, Parameters))
            {
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    return ds.Tables[0].Rows[0];
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets the scalar.
        /// </summary>
        /// <param name="SqlString">The SQL string.</param>
        /// <returns></returns>
        public object GetScalar(string SqlString)
        {
            object Scalar = null;

            Open();

            try
            {
                OracleCommand cmd = new OracleCommand(SqlString, this._conn);
                Scalar = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                throw ex;
            }
            finally
            {
                Close();
            }

            return Scalar;
        }

        /// <summary>
        /// Gets the scalar.
        /// </summary>
        /// <param name="SqlString">The SQL string.</param>
        /// <param name="Parameters">The parameters.</param>
        /// <returns></returns>
        public object GetScalar(string SqlString, Hashtable Parameters)
        {
            object Scalar = null;

            Open();

            try
            {
                OracleCommand cmd = new OracleCommand();
                PrepareCommand(cmd, SqlString, Parameters);
                Scalar = cmd.ExecuteScalar();
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                throw ex;
            }
            finally
            {
                Close();
            }

            return Scalar;
        }

        /// <summary>
        /// Prepares the command.
        /// </summary>
        /// <param name="cmd">The CMD.</param>
        /// <param name="SqlString">The SQL string.</param>
        /// <param name="Parameters">The parameters.</param>
        private void PrepareCommand(OracleCommand cmd, string SqlString, Hashtable Parameters)
        {
            cmd.Parameters.Clear();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = this._conn;
            cmd.CommandText = SqlString;

            if (Parameters != null)
            {
                foreach (DictionaryEntry item in Parameters)
                {
                    cmd.Parameters.Add(item);
                    //cmd.Parameters.AddWithValue(item.Key.ToString(), item.Value);
                }
            }
        }

        #endregion

        /// <summary>
        /// Clones the oracle parameter.
        /// </summary>
        /// <param name="parameter">The parameter.</param>
        /// <returns></returns>
        private OracleParameter CloneOracleParameter(StoreProcedureParameter parameter)
        {
            OracleParameter p = new OracleParameter();
            p.DbType = parameter.DbType;
            p.Direction = parameter.Direction;
            p.ParameterName = parameter.ParameterName;
            p.Value = parameter.Value;

            if (parameter.Size != 0)
                p.Size = parameter.Size;

            return p;
        }

        #region IDatabase Members

        /// <summary>
        /// Executes the oracle sotre procedure.
        /// </summary>
        /// <param name="SpName">Name of the sp.</param>
        /// <param name="Parameters">The parameters.</param>
        /// <returns></returns>
        public Hashtable ExecuteSotreProcedure(string SpName, List<StoreProcedureParameter> Parameters)
        {
            Hashtable ht = new Hashtable();

            try
            {
                OracleCommand cmd = new OracleCommand(SpName, _conn);
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (StoreProcedureParameter param in Parameters)
                {
                    cmd.Parameters.Add(CloneOracleParameter(param));
                }
                this.Open();
                cmd.ExecuteNonQuery();
                foreach (OracleParameter param in cmd.Parameters)
                {
                    if (param.Direction == ParameterDirection.Output)
                    {
                        ht.Add(param.ParameterName, param.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                throw ex;
            }
            finally
            {
                this.Close();
            }

            return ht;
        }

        /// <summary>
        /// Executes the sotre procedure.
        /// </summary>
        /// <param name="SpName">Name of the sp.</param>
        /// <param name="Parameters">The parameters.</param>
        /// <param name="OutParameters">The out parameters.</param>
        /// <returns></returns>
        public DataTable ExecuteSotreProcedure(string SpName, List<StoreProcedureParameter> Parameters, ref Hashtable OutParameters)
        {
            DataTable dt = new DataTable();

            try
            {
                OracleCommand cmd = new OracleCommand(SpName, _conn);
                cmd.CommandType = CommandType.StoredProcedure;

                foreach (StoreProcedureParameter param in Parameters)
                {
                    cmd.Parameters.Add(CloneOracleParameter(param));
                }

                this.Open();

                OracleDataAdapter oda = new OracleDataAdapter(cmd);


                oda.Fill(dt);


                foreach (OracleParameter param in cmd.Parameters)
                {
                    if (param.Direction == ParameterDirection.Output)
                    {
                        OutParameters.Add(param.ParameterName, param.Value);
                    }
                }
            }
            catch (Exception ex)
            {
                Trace.WriteLine(ex.ToString());
                throw ex;
            }
            finally
            {
                this.Close();
            }

            return dt;
        }
        #endregion
    }
}
