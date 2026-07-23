using EF_CODE.Models;
using System;
using System.Data;
using System.Linq;

namespace EF_CODE
{
    class Program
    {
        static void Main(string[] args)
        {
            //DbFirst生成实体类的命令，在程序包管理控制台执行
            //Scaffold - DbContext "Data Source=172.18.32.205;Initial Catalog=MESDB;User ID=sfcuser;Password=sfcuser!@#'songyy" Microsoft.EntityFrameworkCore.SqlServer - OutputDir Models - Tables m_Asset_t - Force

            //覆盖原有实体类，重新生成类
            //Scaffold - DbContext "Data Source=172.18.32.205;Initial Catalog=MESDB;User ID=sfcuser;Password=sfcuser!@#'songyy" Microsoft.EntityFrameworkCore.SqlServer - OutputDir Models - Tables m_Users_t,m_Asset_t - Force

            //添加新的实体类,不重新生成DbContext上下文，避免覆盖原有上下文
            //Scaffold - DbContext "Data Source=172.18.32.205;Initial Catalog=MESDB;User ID=sfcuser;Password=sfcuser!@#'songyy" Microsoft.EntityFrameworkCore.SqlServer - OutputDir Models - Tables NewTable1,NewTable2 - NoContext
            using (var db = new MESDBContext())
            {
                //var customer = new MAssetT
                //{
                //    Name = "John Doe",
                //    Email = "john.doe@example.com"
                //};

                #region 查询
                //var dt = db.MAssetT.Find(1);
                //Console.Write(dt.AssetNo);
                //Console.ReadKey();


                //按条件取10条记录
                //var dt = db.MAssetT.Where(o => o.Cstatus == "使用中").Take(10);
                //foreach (var item in dt)  //返回类型为IQueryable说明是延迟查询，在读取才真正连接数据库进行查询
                //{
                //    string model = item.Model;
                //    Console.WriteLine(model);
                //}

                //联表查询
                /*
                var dt = db.MAssetT.Where(o => o.Id == 1) //内连接左表
                    .Join(db.MUsersT,    //内连接右表
                            u => u.ModifyUserId,     //连接左表字段
                            o => o.UserId,    //连接右表字段
                            (u, o) => new { o.UserName })    //获取字段
                    .ToList();
                Console.WriteLine(dt[0].UserName);
                */

                //左连接
                /*
                var dt = db.MAssetT.Where(o => o.Id == 1) //内连接左表
                    .GroupJoin(db.MUsersT,    //内连接右表
                            u => u.ModifyUserId,     //连接左表字段
                            o => o.UserId,    //连接右表字段
                            (u, o) => new { u, o })   //内连接取出所有字段或指定字段
                    .SelectMany(t => t.o.DefaultIfEmpty(),   //左连接
                                (t, y) => new { y.UserName,t.u.Qty}).ToList();  //获取字段
                Console.WriteLine(dt[0].UserName);
                Console.WriteLine(dt[0].Qty);
                */
                #endregion

                #region 插入
                //MAssetT ma = new MAssetT
                //{
                //    AssetNo = "srgthy"
                //};
                //db.MAssetT.Add(ma);
                ////db.Entry(ma).State = Microsoft.EntityFrameworkCore.EntityState.Added;
                //int i = db.SaveChanges();
                //if (i > 0)
                //{
                //    Console.Write("添加数据成功");
                //}
                #endregion

                #region 批量插入（自动开启数据库事务）
                //for (int i = 0; i < 5; i++)
                //{
                //    MAssetT ma = new MAssetT
                //    {
                //        AssetNo = "srgthy" + i
                //    };
                //    db.MAssetT.Add(ma);
                //}
                db.SaveChanges();
                #endregion

                #region 修改
                //var ma = db.MAssetT.Where(x => x.Id == 36154).ToList();
                //ma[0].AssetNo = "srgthy4";
                //db.SaveChanges();
                #endregion

                #region 删除
                var ma = db.MAssetT.Where(q => q.AssetNo.StartsWith("srgthy")).ToList();
                foreach(var item in ma)
                {
                    db.MAssetT.Attach(item);
                    db.MAssetT.Remove(item);
                }
                db.SaveChanges();
                #endregion
            }
        }
    }
}
