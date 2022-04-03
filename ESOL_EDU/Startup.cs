using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ESOL_EDU.Startup))]
namespace ESOL_EDU
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
