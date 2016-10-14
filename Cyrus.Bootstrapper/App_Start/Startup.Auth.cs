using Microsoft.Owin.Security.DataProtection;
using Owin;

namespace Cyrus.Bootstrapper
{
    public partial class Startup
    {
        public static IDataProtectionProvider DataProtectionProvider { get; private set; }

        public void ConfigureAuth(IAppBuilder app)
        {
            DataProtectionProvider = app.GetDataProtectionProvider();

            // Load IdentityServer Stuff

        }
    }

}
