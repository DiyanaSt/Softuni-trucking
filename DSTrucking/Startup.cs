using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DSTrucking.Startup))]
namespace DSTrucking
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
