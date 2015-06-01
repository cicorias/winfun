using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ToastManager.Startup))]
namespace ToastManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
