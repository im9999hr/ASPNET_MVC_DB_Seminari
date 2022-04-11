using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SeminarUpisi.Startup))]
namespace SeminarUpisi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
