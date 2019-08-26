using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HMS.Web.Startup))]
namespace HMS.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
