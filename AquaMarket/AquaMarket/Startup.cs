using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AquaMarket.Startup))]
namespace AquaMarket
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
