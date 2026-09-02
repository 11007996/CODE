using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RPA_TOOL.Startup))]
namespace RPA_TOOL
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
