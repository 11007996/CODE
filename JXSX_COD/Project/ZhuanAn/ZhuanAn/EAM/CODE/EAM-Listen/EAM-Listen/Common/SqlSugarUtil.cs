using EAM.Listen.Common.Config;
using EAM.Listen.Common.Utils;
using SqlSugar;
using System;

namespace EAM.Listen.Common
{
    public class SqlSugarUtil
    {
        //创建数据库对象 (用法和EF Dappper一样通过new保证线程安全)
        public static string GetServerIP()
        {
            try
            {
                return Setting.DbConfig.Conn.Split(';')[0].Split('=')[1];
            }
            catch
            {
                return "0.0.0.0";
            }
        }

        /// <summary>
        /// 获取连接对象
        /// </summary>
        /// <returns></returns>
        public static SqlSugarClient Conn()
        {
            if (Setting.DbConfig.ConfigId == null)
                return null;
            return new SqlSugarClient(new ConnectionConfig()
            {
                ConfigId = Setting.DbConfig.ConfigId,
                ConnectionString = Setting.DbConfig.Conn,
                DbType = (DbType)Setting.DbConfig.DbType,
                IsAutoCloseConnection = Setting.DbConfig.IsAutoCloseConnection,
            }, db =>
            {
                //5.1.3.24统一了语法和SqlSugarScope一样，老版本AOP可以写外面
                db.Aop.OnLogExecuting = (sql, pars) =>
                {
                    //Console.WriteLine(sql);//输出sql,查看执行sql 性能无影响
                    //获取原生SQL推荐 5.1.4.63  性能OK
                    //Console.WriteLine(UtilMethods.GetNativeSql(sql, pars));
                    //获取无参数化SQL 对性能有影响，特别大的SQL参数多的，调试使用
                    //Console.WriteLine(UtilMethods.GetSqlString(DbType.SqlServer, sql, pars));
                };
            });
        }

        /// <summary>
        /// 检查数据库服务器连接状态
        /// </summary>
        /// <returns></returns>
        public static string CheckServerConnState()
        {
            //网络检查
            string r = NetWorkHelper.PingIP(GetServerIP());
            if (!string.IsNullOrWhiteSpace(r))
            {
                return "无法Ping通服务器IP,状态:" + r;
            }
            //数据库连接检查
            try
            {
                Conn().Close();
            }
            catch (Exception ex)
            {
                return "数据库连接异常:" + ex.Message;
            }
            return "";
        }
    }
}