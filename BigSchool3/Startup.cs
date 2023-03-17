using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BigSchool3.Startup))]
namespace BigSchool3
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
