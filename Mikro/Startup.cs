using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Mikro.Startup))]
namespace Mikro
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
