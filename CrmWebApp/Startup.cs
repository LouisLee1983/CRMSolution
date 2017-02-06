using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CrmWebApp.Startup))]
namespace CrmWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
