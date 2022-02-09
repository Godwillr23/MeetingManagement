using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChilliSoft_Assignment.Startup))]
namespace ChilliSoft_Assignment
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
