using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CarrerCloudV2.Startup))]
namespace CarrerCloudV2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
