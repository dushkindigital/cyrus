#define DEBUG // FOR TESTING, REMOVE
using System;
using System.Configuration;
using Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.DataProtection;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;
using System.IdentityModel.Tokens;

namespace Cyrus.Bootstrapper
{
    public partial class Startup
    {
        // Don't remove this. 
        public static IDataProtectionProvider DataProtectionProvider { get; private set; }
        //public static string PublicClientId { get; private set; }

        private readonly string issuer = ConfigurationManager.AppSettings["issuer"];
        
        public void ConfigureOAuth(IAppBuilder app)
        {
            try
            { 
                app.SetDefaultSignInAsAuthenticationType(DefaultAuthenticationTypes.ExternalCookie);

                app.UseCookieAuthentication(new CookieAuthenticationOptions
                {
                    AuthenticationType = DefaultAuthenticationTypes.ExternalCookie,
                    AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Passive,
                    LoginPath = new PathString("/accounts/signin"),
                    CookieHttpOnly = true,
                    CookieName = CookieAuthenticationDefaults.CookiePrefix + "External",
                    CookieDomain = ".mydomain.com"
                });

            }
            catch (Exception e)
            {
                var outter = e.Message;
                var inner = e.InnerException;
            }


            ConfigureOAuthTokenGeneration(app);
            ConfigureOAuthTokenConsumption(app);
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureOAuthTokenConsumption(IAppBuilder app)
        {
            // Don't remove this. 
            DataProtectionProvider = app.GetDataProtectionProvider();
            
            byte[] secret = TextEncodings.Base64Url.Decode(ConfigurationManager.AppSettings["as:AudienceSecret"]);
            var audience = ConfigurationManager.AppSettings["as:AudienceId"];

            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audience },
                    IssuerSecurityTokenProviders = new IIssuerSecurityTokenProvider[]
                    {
                        new SymmetricKeyIssuerSecurityTokenProvider(issuer, secret)
                    }
                });

            // Uncomment the following lines to enable logging in with third party login providers

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "3241880646-k9orb2d42b77gc452qkahi64bljpa6br.apps.googleusercontent.com",
                ClientSecret = "txxli0vv-J5ByhHN9y74Bw8y"
            });

            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //    consumerKey: "",
            //    consumerSecret: "");

            //app.UseFacebookAuthentication(
            //    appId: "",
            //    appSecret: "");

        }

        private void ConfigureOAuthTokenGeneration(IAppBuilder app)
        {
            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                //For Dev enviroment only (on production should be AllowInsecureHttp = false)
#if DEBUG
                AllowInsecureHttp = true,
#else
                AllowInsecureHttp = false,
#endif
                TokenEndpointPath = new PathString("/token"),
                // force user to refresh login token every two weeks.
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(14),
                Provider = new Auth.CustomOAuthProvider(),
                AccessTokenFormat = new Auth.CustomJwtFormat(issuer)

            };

            // OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);

        }

    }

}
