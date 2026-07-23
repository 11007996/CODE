using System;
using System.Collections.Generic;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Collections;

namespace Asset.DAL.Util
{
    internal class DBUtil
    {
        //数据库连接状态
        public static bool DBConnState = false;
        //数据库连接字符串
        public static string ConnectionStr = System.Web.Configuration.WebConfigurationManager.ConnectionStrings["HJDBConnection"].ToString();


        /// <summary>
        /// 连接数据库
        /// </summary>
        /// <returns></returns>
        public static SqlConnection CallCon()
        {
            return new SqlConnection(ConnectionStr);
        }


        public static string GetServerIP()
        {
            try
            {
                return ConnectionStr.Split(';')[0].Split('=')[1];
            }
            catch
            {
                return "0.0.0.0";
            }
        }

        /// <summary>
        /// 测试数据库连接状态
        /// </summary>
        /// <returns></returns>
        public static bool ConnnectTest()
        {
            bool result = false;

            if (!ConnectionStr.ToLower().Contains("timeout"))
            {
                ConnectionStr += ";Connect Timeout=2;";   //增加超时时间，缩短默认连接时间
            }

            //创建连接对象
            SqlConnection conn = new SqlConnection(ConnectionStr);
            try
            {
                conn.Open();
                if (conn.State == ConnectionState.Open)
                {
                    result = true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                conn.Close();
            }
            return result;
        }


        /// <summary>
        /// 执行SQL,返回DataTable类型数据
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public static DataTable GetDataTable(string strSQL)
        {

            SqlConnection conn = CallCon();
            try
            {
                conn.Open();
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand(strSQL, conn);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                }
                return dt;
            }
            catch (Exception ex)
            {
                //判断是否因为数据库连接导致的异常
                if (conn.State == ConnectionState.Closed && DBUtil.DBConnState == true)
                {
                    DBUtil.DBConnState = false;
                }
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public static DataTable GetDataTable(string strSQL, List<SqlParameter> param)
        {

            SqlConnection conn = CallCon();
            try
            {
                conn.Open();
                DataTable dt = new DataTable();
                SqlCommand cmd = new SqlCommand(strSQL, conn);
                if (param != null)
                {
                    cmd.Parameters.AddRange(param.ToArray());
                }
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    sda.SelectCommand = cmd;
                    sda.Fill(dt);
                }
                return dt;
            }
            catch (Exception ex)
            {
                //判断是否因为数据库连接导致的异常
                if (conn.State == ConnectionState.Closed && DBUtil.DBConnState == true)
                {
                    DBUtil.DBConnState = false;
                }
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }



        /// <summary>
        /// 执行'查'返回DataSet数据集
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public static DataSet GetDataSet(string strSQL, string[] tableNames)
        {
            SqlConnection con = CallCon();
            try
            {
                con.Open();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand(strSQL, con);
                using (SqlDataAdapter sda = new SqlDataAdapter())
                {
                    if (tableNames != null && tableNames.Length > 0)
                    {
                        for (int i = 0; i < tableNames.Length; i++)
                        {
                            if (i == 0)
                            {
                                sda.TableMappings.Add("Table", tableNames[i]);
                            }
                            else
                            {
                                sda.TableMappings.Add("Table" + i, tableNames[i]);
                            }
                        }
                    }
                    sda.SelectCommand = cmd;
                    sda.Fill(ds);
                }
                return ds;
            }
            catch (Exception ex)
            {
                if (con.State == ConnectionState.Closed && DBUtil.DBConnState == true)
                {
                    DBUtil.DBConnState = false;
                }
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }


        /// <summary>
        /// 执行'增删改'
        /// </summary>
        /// <param name="strSQL"></param>
        /// <returns></returns>
        public static int ExecSQL(string strSQL)
        {
            SqlConnection con = CallCon();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(strSQL, con);
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //判断是否因为数据库连接导致的异常
                if (con.State == ConnectionState.Closed && DBUtil.DBConnState == true)
                {
                    DBUtil.DBConnState = false;
                }
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
        //批量插入
        public static void BatchInsertData(string tableName, DataTable data, List<SqlBulkCopyColumnMapping> colMappings)
        {

            SqlConnection conn = CallCon();
            try
            {
                conn.Open();
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(conn))
                {
                    bulkCopy.DestinationTableName = tableName;

                    // 设置列映射
                    if (colMappings != null)
                    {

                        foreach (SqlBulkCopyColumnMapping map in colMappings)
                        {
                            bulkCopy.ColumnMappings.Add(map);
                        }
                    }
                    else
                    { //如果未传列映射，则以DataTable的列名映射
                        foreach (DataColumn col in data.Columns)
                        {
                            bulkCopy.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                        }
                    }

                    // 执行批量插入操作
                    bulkCopy.WriteToServer(data);
                }
            }
            catch (Exception ex)
            {
                //判断是否因为数据库连接导致的异常
                if (conn.State == ConnectionState.Closed && DBUtil.DBConnState == true)
                {
                    DBUtil.DBConnState = false;
                }
                throw ex;
            }
            finally
            {
                conn.Close();
            }
        }

        public static int ExecSQL(string strSQL, List<SqlParameter> param)
        {
            SqlConnection con = CallCon();
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(strSQL, con);
                if (param != null)
                {
                    cmd.Parameters.AddRange(param.ToArray());
                }
                return cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                //判断是否因为数据库连接导致的异常
                if (con.State == ConnectionState.Closed && DBUtil.DBConnState == true)
                {
                    DBUtil.DBConnState = false;
                }
                throw ex;
            }
            finally
            {
                con.Close();
            }
        }
    }
}
