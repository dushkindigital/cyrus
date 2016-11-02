using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.OAuth;
using Owin;

namespace Cyrus.Bootstrapper
{
    public partial class Startup
    {
        public static IDataProtectionProvider DataProtectionProvider { get; private set; }

        public void ConfigureOAuth(IAppBuilder app)
        {
            DataProtectionProvider = app.GetDataProtectionProvider();

            /***** Load IdentityServer Stuff *****/

            //Token Consumption
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
            });

        }
    }

}
