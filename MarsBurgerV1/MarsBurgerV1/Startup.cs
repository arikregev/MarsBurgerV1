using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MarsBurgerV1.Startup))]
namespace MarsBurgerV1
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
