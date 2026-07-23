using System.Web.Mvc;

namespace MVC260625.Areas.sport
{
    public class sportAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "sport";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "sport_default",
                "sport/{controller}/{action}/{id}",
                new { action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}