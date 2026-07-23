using SqlSugar;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace sqlsuger
{
    class Program
    {
        static void Main(string[] args)
        {
            SqlSugarClient db = GetDb();

            #region AOP拦截
            //db.Aop.OnLogExecuting = (sql, pars) =>
            //{
            //    // SQL执行前记录
            //    //File.AppendAllText("sql.log", sql + Environment.NewLine);

            //    Console.WriteLine("12345");
            //};
            //db.Aop.OnError = exp =>
            //{
            //    // 错误处理
            //    //Log.Error(exp.Message);
            //    Console.WriteLine("333335");
            //};

            //db.Aop.DataExecuting = (oldValue, entityInfo) =>
            //{
            //    // 数据操作前拦截
            //    if (entityInfo.PropertyName == "Cartonid")
            //    {
            //        entityInfo.SetValue("aaaa");
            //    }
            //};
            #endregion

            

            #region 查询
            //var data = db.Queryable<A>().First();
            //Console.WriteLine(data.PartID);
            #endregion
            #region 条件查询
            //int num = db.Queryable<A>().Where(x => x.PartID == "C0052000014").Count();
            //Console.WriteLine(num);
            #endregion
            #region 条件查询
            //var data = db.Queryable<A>()
            //      .Where(u => u.PartID == "C0052000014")
            //      .OrderBy(u => u.Cartonid, OrderByType.Desc)
            //      .ToList();
            //Console.WriteLine(data[1].Cartonid);
            #endregion
            #region 原生SQL查询
            //long count = db.Ado.GetLong("SELECT COUNT(*) FROM A where PartId='C0052000014'");
            //Console.WriteLine(count);
            #endregion
            #region 原生SQL条件查询
            //long total = db.Ado.GetLong("SELECT COUNT(1) FROM A WHERE PartId = @part", new { part = "C0052000014" });
            //Console.WriteLine(total);
            #endregion
            #region 原生SQL条件查询
            //var users = db.Ado.SqlQuery<A>("SELECT * FROM A WHERE PARTID = @part", new { part = "3" });
            //Console.WriteLine(users[0].Cartonid);
            #endregion
            

            #region 分页查询
            //var pageResult = db.Queryable<A>()
            //.Where(u => u.PartID == "C0052000014")
            //.ToPageList(1, 10);
            //// 当前页数据
            ////List<A> list = pageResult.ToList();
            //// 符合条件总行数
            ////long total = pageResult.Count;
            //var aa = pageResult[0];
            //Console.WriteLine(aa.Cartonid);
            #endregion
            #region 分页查询
            //var page2 = db.Queryable<A>()
            //  .Where(u => u.PartID == "C0052000014")
            //  .OrderBy(u => u.Cartonid)
            //  .Take(20)
            //  .ToList();
            //Console.WriteLine(page2[0].Cartonid);
            #endregion
            #region 聚合查询
            //总和
            //int num = db.Queryable<Cartonsn>().Sum(o => o.ppidQty);
            // 平均值
            //decimal avg = db.Queryable<Cartonsn>().Where(u => u.Cartonid == "P04072002S229A000HW").Avg(o => o.ppidQty);
            // 最大值
            //decimal max = db.Queryable<Cartonsn>().Where(u => u.Cartonid == "P04072002S229A000HW").Max(o => o.ppidQty);
            // 最小值
            //decimal min = db.Queryable<Cartonsn>().Min(o => o.ppidQty);
            // 总行数
            //long count = db.Queryable<Cartonsn>().Count();


            //var agg = db.Queryable<Cartonsn>()
            //    .Where(u => u.Cartonid == "P04072002S229A000HW")
            //    .Select(o => new
            //    {
            //        TotalCount = SqlFunc.AggregateCount(1),
            //        TotalAmount = SqlFunc.AggregateSum(o.ppidQty),
            //        AvgAmount = SqlFunc.AggregateAvg(o.ppidQty),
            //        MaxAmount = SqlFunc.AggregateMax(o.ppidQty),
            //        MinAmount = SqlFunc.AggregateMin(o.ppidQty)
            //    }).First();
            //Console.WriteLine(agg.MaxAmount);
            #endregion
            #region 联表查询
            //var list = db.Queryable<A, Cartonsn>((o, u) => new object[]
            //{JoinType.Left,        // 第一个参数：连接类型
            //o.Cartonid == u.Cartonid &&     // 第二个参数：on关联条件
            //o.PartID == "C0052000014"
            //})
            //.Select((o, u) => new
            //{
            //    o.Cartonid,
            //    o.PartID,
            //    u.ppidQty
            //})
            //.ToList();
            //Console.Write(list[0].Cartonid);

            #endregion
            #region 使用索引提示（SQL Server）
            //var users = db.Queryable<A>().With(SqlWith.RowLock).ToList();
            //Console.WriteLine(users[0].Cartonid);
            #endregion


            #region 单条插入
            //A a = new A() { Cartonid = "1", PartID = "2" };
            //db.Insertable(a).ExecuteCommand();
            //db.Insertable(new A() { Cartonid = "1", PartID = "1" }).ExecuteCommand();
            #endregion
            #region 批量插入
            //var users = new List<A>
            //{
            //    new A { Cartonid = "1", PartID = "3" },
            //    new A { Cartonid = "1", PartID = "4" }
            //};
            //db.Insertable(users).ExecuteCommand();
            #endregion


            #region 更新
            //db.Updateable<A>()
            //.SetColumns(u => u.PartID == "22")
            //.Where(u => u.PartID == "2")
            //.ExecuteCommand();
            #endregion
            #region 条件更新
            ////db.Updateable<A>()
            ////.SetColumns(u => new A { PartID = u.PartID + "1" })
            ////.Where(u => u.PartID == "22")
            ////.ExecuteCommand();
            #endregion

            #region 删除
            //db.Deleteable<A>().Where(x => x.PartID == "221").ExecuteCommand();
            //db.Deleteable<A>().Where("PartID = @ID", new { ID = "1" }).ExecuteCommand();
            #endregion

            #region 调用存储过程
            //var parameters = new SugarParameter[]
            //{
            //    new SugarParameter("@CartonId", "1sdfvg"),
            //    new SugarParameter("@msg", "aaa", System.Data.DbType.String, ParameterDirection.Output)
            //};

            //db.Ado.UseStoredProcedure(() =>
            //{
            //    db.Ado.ExecuteCommand("checkH01Carton2", parameters);
            //    string result = parameters[1].Value.ToString();
            //    Console.WriteLine(result);
            //});
            #endregion

            #region 生成SQL语句
            //var query = db.Queryable<A>().Where(u => u.PartID == "3");
            //Console.WriteLine(query.ToSql().Key); // 输出生成的SQL
            #endregion

            #region 事务管理
            //try
            //{
            //    db.Ado.BeginTran();  // 开始事务

            //    // 执行多个数据库操作
            //    var data = db.Queryable<A>().First();

            //    var user = new A { Cartonid = "1", PartID = data.PartID + "AAA" };
            //    db.Insertable(user).ExecuteReturnIdentity();

            //    db.Ado.CommitTran();  // 提交事务
            //}
            //catch (Exception ex)
            //{
            //    db.Ado.RollbackTran();  // 回滚事务
            //    throw ex;
            //}
            #endregion

            #region 动态 SQL
            //var data = db.Queryable<A>()
            //    .WhereIF(2 == 2, x => x.PartID == "3")  //WhereIF(判断条件, 查询表达式),条件成立才追加 where 语句
            //    .Select(x => x.PartID);
            //Console.WriteLine(data.ToSql().Key);
            //Console.WriteLine(data.ToList()[0]);
            #endregion

            #region DbFirst生成实体
            //db.DbFirst.Where("ABC").CreateClassFile("D:\\zzzzzzzz\\CodeBlock\\EF\\sqlsuger\\sqlsuger", "sqlsuger");  //生成表ABC实体类，命名空间sqlsuger
            //db.DbFirst.Where(it => it.ToLower().StartsWith("aa")).CreateClassFile("D:\\zzzzzzzz\\CodeBlock\\EF\\sqlsuger\\sqlsuger", "sqlsuger");   //生成实体并且带有筛选
            //db.DbFirst.Where("deptlineD_t").IsCreateAttribute().CreateClassFile("D:\\zzzzzzzz\\CodeBlock\\EF\\sqlsuger\\sqlsuger", "sqlsuger");  //生成带有SqlSugar特性的实体

            //db.DbFirst.Where("m_AssysnD_t").IsCreateDefaultValue().CreateClassFile("D:\\zzzzzzzz\\CodeBlock\\EF\\sqlsuger\\sqlsuger", "sqlsuger");  //生成实体带有默认值
            #endregion

            #region CodeFirst 建表
            //db.CodeFirst.SetStringDefaultLength(36).InitTables(typeof(CodeFirstTable));  //数据库创建CodeFirstTable表
            #endregion

            Console.ReadKey();
        }
         
        public static SqlSugarClient GetDb()
        {
            var config = new ConnectionConfig()
            {
                #region SqlServer连接
                ConnectionString = "data source=172.18.32.205;initial catalog=MESDB;uid=sfcuser;pwd= sfcuser!@#'songyy",
                DbType = SqlSugar.DbType.SqlServer,
                IsAutoCloseConnection = true
                #endregion
            };
            return new SqlSugarClient(config);
        }
    }




    [SugarTable("A")]  //数据库表名
    public class A
    {
        //数据是自增需要加上IsIdentity 
        //数据库是主键需要加上IsPrimaryKey 
        //注意：要完全和数据库一致2个属性
        //[SugarColumn(IsPrimaryKey = true, IsIdentity = true)]
        //public int Id { get; set; }
        //public int? SchoolId { get; set; }
        //public string? Name { get; set; }


        //[SugarColumn(IsPrimaryKey = false, IsIdentity = false, ColumnName = "Cartonid")]   //主键 自增 列名
        //[SugarColumn( Length = 50, IsNullable = false)]  //长度50 非空
        //[SugarColumn(DefaultValue = "GETDATE()")]  //默认值为当前时间
        //[SugarColumn(IsIgnore = true)]  // 导航属性，忽略字段（不映射到数据库表），会影响性能
        public string Cartonid { get; set; }
        public string PartID { get; set; }
    }
    [SugarTable("m_Cartonsn_t")]  //数据库表名
    public class Cartonsn
    {
        public string ppid { get; set; }
        public string Cartonid { get; set; }
        public int ppidQty { get; set; }
        public string Userid { get; set; }
        public string Status { get; set; }
        public DateTime Intime { get; set; }
        public string Mark1 { get; set; }
    }


    public class CodeFirstTable
    {
        [SugarColumn(IsIdentity = true, IsPrimaryKey = true)]
        public int Id { get; set; }
        public string Name { get; set; }
        //ColumnDataType 一般用于单个库数据库，如果多库不建议用
        [SugarColumn(ColumnDataType = "Nvarchar(255)")]
        public string Text { get; set; }
        [SugarColumn(IsNullable = true)]//可以为NULL
        public DateTime CreateTime { get; set; }
    }
}
