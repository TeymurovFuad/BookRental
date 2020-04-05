using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BookRental.Startup))]
namespace BookRental
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
