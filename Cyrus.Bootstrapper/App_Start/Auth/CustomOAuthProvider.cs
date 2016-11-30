using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.OAuth;
using Cyrus.Data.Identity;
using System.Security.Cryptography;
using System;

namespace Cyrus.Bootstrapper.Auth
{
    public class CustomOAuthProvider : OAuthAuthorizationServerProvider
    {

        public override Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
            return Task.FromResult<object>(null);
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {

            using (var userManager = IdentityFactory.CreateUserManager(new Data.CyrusDbContext()))
            {
                var user = await userManager.FindAsync(context.UserName, context.Password);

                if (user == null)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }

                if (!user.EmailConfirmed)
                {
                    context.SetError("invalid_grant", "User did not confirm email.");
                    return;
                }

                var oAuthIdentity = await userManager.CreateIdentityAsync(user, "JWT");

                //if (!oAuthIdentity.HasClaim(x => x.Type == ClaimTypes.Hash))
                //{
                //    var bytes = new byte[16];
                //    using (var rng = new RNGCryptoServiceProvider())
                //    {
                //        rng.GetBytes(bytes);
                //        oAuthIdentity.AddClaim(new System.Security.Claims.Claim(ClaimTypes.Hash,
                //            BitConverter.ToString(bytes).Replace("-", "").ToLower()));
                //    }
                //}
                
                var ticket = new AuthenticationTicket(oAuthIdentity, null);

                context.Validated(ticket);

            }

        }
        
    }
}