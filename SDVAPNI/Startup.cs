using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SDVAPNI.Startup))]
namespace SDVAPNI
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
