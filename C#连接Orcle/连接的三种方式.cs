//************************方式一************************
/// <summary>
        /// 批量处理插入数据，使用常规方式
        /// </summary>
        /// <param name="dt">数据源</param>
        /// <returns></returns>
        public int Insert(DataTable dt)
        {
            int count = 0;
            string conString = orcHelper.GetConn();
            using (OracleConnection conn = new OracleConnection(conString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }
                using (OracleTransaction transaction = conn.BeginTransaction())
                {
                    using (OracleCommand cmd = conn.CreateCommand())
                    { 
                        cmd.Transaction = transaction;
                        string sql = @"insert into t_test(id, name, age, createdate) values(:id, :name, :age, :createdate)";
                        foreach (DataRow dw in dt.Rows)
                        {
                            OracleParameter[] parametersList = new OracleParameter[]
                            {
                                new OracleParameter(":id", int.Parse(dw["id"].ToString())),
                                new OracleParameter(":name", dw["name"].ToString()),
                                new OracleParameter(":age",int.Parse(dw["age"].ToString())),
                                new OracleParameter(":createdate",DateTime.Parse(dw["createdate"].ToString())),
                            };
                            cmd.CommandText = sql;
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddRange(parametersList);

                            try
                            {
                                count = cmd.ExecuteNonQuery();
                                if (count < 0) { 
                                    transaction.Rollback();
                                    return count;
                                }
                            }
                            catch (Exception)
                            {
                                transaction.Rollback();
                                return count;
                            }
                        }
                        transaction.Commit();
                        return count;
                    }
                }
            }
        }








//************************方式二************************
/// <summary>
        /// 批量处理插入数据，使用OracleBulkCopy
        /// </summary>
        /// <param name="dt">数据源</param> 
        public bool InsertOracleBulkCopy(DataTable dt)
        { 
            string conString = orcHelper.GetConn();
            using (OracleConnection conn = new OracleConnection(conString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (OracleTransaction transaction = conn.BeginTransaction())
                {
                    //创建 OracleBulkCopy 对象，并指定数据库连接信息 
                    using (OracleBulkCopy bulkCopy = new OracleBulkCopy(conn))
                    { 
                        //数据库表名称
                        bulkCopy.DestinationTableName = dt.TableName;      
                        //指定批量插入的行数 
                        bulkCopy.BatchSize = dt.Rows.Count;             

                        //指定 DataTable 和数据表的列名映射关系
                        for (int i = 0; i < dt.Columns.Count; i++)
                        {
                            bulkCopy.ColumnMappings.Add(dt.Columns[i].ColumnName, dt.Columns[i].ColumnName);
                        }
                        try
                        {  
                            //将数据源添加到 OracleBulkCopy 对象中
                            bulkCopy.WriteToServer(dt);              
                            transaction.Commit();
                            return true; 
                        }
                        catch (Exception)
                        {
                            transaction.Rollback();
                            return false; 
                        }
                    }
                } 
            } 
        }








//************************方式三************************
/// <summary>
        /// 批量处理插入数据，使用ArrayBind
        /// <param name="dt">数据源</param>
        /// </summary>
        public int InsertArrayBind(DataTable dt)
        {
            string conString = orcHelper.GetConn();
            using (OracleConnection conn = new OracleConnection(conString))
            {
                if (conn.State != ConnectionState.Open)
                {
                    conn.Open();
                }

                using (OracleTransaction transaction = conn.BeginTransaction())
                {
                    int recordCount = dt.Rows.Count, i = 0, count = 0;

                    using (OracleCommand cmd = conn.CreateCommand())
                    {
                        cmd.Transaction = transaction;
                        cmd.CommandText = "insert into t_test(id, name, age, createdate) values(:id, :name, :age, :createdate)";
                        //指定单次需要处理的条数
                        cmd.ArrayBindCount = recordCount;
                        int[] p_col1 = new int[recordCount];
                        string[] p_col2 = new string[recordCount];
                        int[] p_col3 = new int[recordCount];
                        DateTime[] p_col4 = new DateTime[recordCount];

                        cmd.Parameters.Add(new OracleParameter("id", OracleDbType.Int32, p_col1, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("name", OracleDbType.Varchar2, p_col2, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("age", OracleDbType.Int32, p_col3, ParameterDirection.Input));
                        cmd.Parameters.Add(new OracleParameter("createdate", OracleDbType.Date, p_col4, ParameterDirection.Input));

                        foreach (DataRow dr in dt.Rows)
                        {
                            p_col1[i] = Convert.ToInt32(dr["id"].ToString());
                            p_col2[i] = dr["name"].ToString();
                            p_col3[i] = Convert.ToInt32(dr["age"].ToString());
                            p_col4[i] = Convert.ToDateTime(dr["createdate"].ToString());
                            i++;
                        }

                        try
                        {  
                            count = cmd.ExecuteNonQuery();
                            if (count > 0) { transaction.Commit(); } 
                        }
                        catch (Exception)
                        {
                            transaction.Rollback(); 
                        } 
                        return count;
                    }
                }
            }
        }