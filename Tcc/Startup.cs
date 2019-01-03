using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Tcc.Startup))]
namespace Tcc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
