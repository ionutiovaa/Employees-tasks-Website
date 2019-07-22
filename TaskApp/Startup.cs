using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TaskApp.Startup))]
namespace TaskApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
