using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ZaloraClone101.Startup))]
namespace ZaloraClone101
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
