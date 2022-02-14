using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AttendenceMgmt.Startup))]
namespace AttendenceMgmt
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
