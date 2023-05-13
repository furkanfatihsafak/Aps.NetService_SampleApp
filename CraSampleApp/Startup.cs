using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CraSampleApp.Startup))]
namespace CraSampleApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
