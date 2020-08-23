using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(UnPeu.Startup))]
namespace UnPeu
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
