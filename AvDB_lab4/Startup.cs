using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AvDB_lab4.Startup))]
namespace AvDB_lab4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
