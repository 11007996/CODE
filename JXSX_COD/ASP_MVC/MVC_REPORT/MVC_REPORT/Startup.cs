using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC_REPORT.Startup))]
namespace MVC_REPORT
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
