using Microsoft.Owin;
using Owin;
using EDL4.App_Start;

[assembly: OwinStartupAttribute(typeof(EDL4.Startup))]
namespace EDL4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}