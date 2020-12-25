using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SingleRDemo.Startup))]
namespace SingleRDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
            ConfigureAuth(app);
        }
    }
}
