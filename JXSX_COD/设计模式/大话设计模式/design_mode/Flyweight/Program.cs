using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flyweight
{
    class Program
    {
        static void Main(string[] args)
        {
            WebSiteFactory f = new WebSiteFactory();
            WebSite fx = f.GetWebSiteCatategory("产品展示");
            fx.Use();
            WebSite fy = f.GetWebSiteCatategory("产品展示");
            fy.Use();
            WebSite fz = f.GetWebSiteCatategory("产品展示");
            fz.Use();
            WebSite fl = f.GetWebSiteCatategory("博客");
            fl.Use();
            WebSite fm = f.GetWebSiteCatategory("博客");
            fm.Use();
            WebSite fn = f.GetWebSiteCatategory("博客");
            fn.Use();

            Console.WriteLine("网站分类总数为{0}", f.GetWebSiteCount());
            Console.Read();
        }
    }
}
