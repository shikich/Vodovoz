using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DriversAPI.Startup))]
namespace DriversAPI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
