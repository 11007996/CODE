using Microsoft.AspNetCore.Mvc;
using webapi.Model;
using System.Text;
using SqlSugar;

namespace webapi.Controllers
{
    //[Route("{controller}/{action}")]
    public class HomeController : Controller
    {
        [HttpGet("Index")]
        public IActionResult Index()
        {
            Conn conn = new Conn();
            using (SqlSugarClient db = conn.GetInstance())
            {
                //只能生成当前用户下的表实体类
                db.DbFirst.Where(new string[] { "P_REWO_BASE" }).IsCreateAttribute().StringNullable().CreateClassFile(@"D:\zzzzzzzz\CodeBlock\SqlSugar\SqlSugatTest\webapi\sqlClass", "webapi.sqlClass");
            }
            return Content("OK");
            //单条查询
            //T_emp pos = db.Queryable<T_emp>().Where(t => t.AWG == "1").First();
            //return Content(pos.GB);

            //多条查询
            //var result = db.Queryable<T_emp>().Where(t => t.AWG.Length < 2).ToList();
            //StringBuilder strB = new StringBuilder();
            //foreach (T_emp paraValue in result)
            //{
            //    strB.Append(paraValue.GB);
            //}
            //return Content(strB.ToString());

            //修改数据
            //db.Updateable<T_emp>()
            //    .SetColumns(p => new T_emp { GB = "7.347" })
            //    .Where(p => p.AWG == "1")
            //    .ExecuteCommand();
            //return Content("OK");

            // 插入数据示例
            //var paraValue = new T_emp { AWG = "John", GB = "John" };
            //db.Insertable(paraValue).ExecuteCommand();
            //return Content("OK");

        }

        //[HttpGet("[action]")]
        [HttpGet("getObject")]
        public ViewResult getObject()
        {
            return View(new T_emp
            {
                AWG = "awg test",
                GB = "gb test"
            });
        }
    }
}
