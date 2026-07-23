using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC260625.Startup))]
namespace MVC260625
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
