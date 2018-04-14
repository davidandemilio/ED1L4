using Microsoft.Owin;
using Owin;

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
