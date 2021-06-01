using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AssignmentMVC.Startup))]
namespace AssignmentMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
