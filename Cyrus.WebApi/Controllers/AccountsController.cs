using System;
using System.Web;
using System.Web.Http;
using System.Configuration;
using System.Threading.Tasks;
using System.Security.Claims;
using System.Collections.Generic;
using System.Security.Cryptography;
using Cyrus.Core.Identity;
using Cyrus.Core.DomainModels.Identity;
using Cyrus.Core.DomainServices.Command;
using Cyrus.WebApi.Extensions;
using Cyrus.WebApi.ViewModels;
using Cyrus.WebApi.Results;
using MediatR;

namespace Cyrus.WebApi.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Authorize]
    [RoutePrefix("api/v1/accounts")]
    public class AccountsController : ApiController
    {
        private const string LocalLoginProvider = "Local";
        private readonly IMediator _mediator;
        private readonly IApplicationUserManager _userManager;
        private string[] AuthenticationTypes
        {
            get
            {
                string[] authenticationTypes = null;

                IEnumerable<ApplicationAuthenticationDescription> descriptions =
                _userManager.GetExternalAuthenticationTypes();

                foreach (ApplicationAuthenticationDescription description in descriptions)
                {
                    authenticationTypes = new string[]
                    {
                        description.AuthenticationType
                    };
                }

                return authenticationTypes;
            }

            set
            {
            }

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="userManager"></param>
        public AccountsController(
            IMediator mediator, 
            IApplicationUserManager userManager)
        {
            if (mediator == null)
                throw new ArgumentNullException(nameof(mediator), "mediator");
            _mediator = mediator;

            if (userManager == null)
                throw new ArgumentNullException(nameof(userManager), "userManager");
            _userManager = userManager;
            
        }

        // GET api/Account/UserInfo 
        //[HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        /// <summary>
        /// Gets user registration information
        /// </summary>
        /// <returns></returns>
        [Route("UserInfo")]
        public UserInfoViewModel GetUserInfo()
        {
            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);
            
            return new UserInfoViewModel
            {
                Email = User.Identity.GetUserName(),
                HasRegistered = externalLogin == null,
                LoginProvider = externalLogin != null ? externalLogin.LoginProvider : null
            };
        }

        // GET api/Account/ExternalLogin 
        /// <summary>
        /// Gets external login information for the authenticated user
        /// </summary>
        /// <param name="provider"></param>
        /// <param name="error"></param>
        /// <returns></returns>
        [OverrideAuthentication]
        //[HostAuthentication(DefaultAuthenticationTypes.ExternalCookie)]
        [AllowAnonymous]
        [Route("ExternalLogin", Name = "ExternalLogin")]
        public async Task<IHttpActionResult> GetExternalLogin(string provider, string error = null)
        {

            if (error != null)
            {
                return Redirect(Url.Content("~/") + "#error=" + Uri.EscapeDataString(error));
            }

            if (!User.Identity.IsAuthenticated)
            {
                return new ChallengeResult(provider, this);
            }

            ExternalLoginData externalLogin = ExternalLoginData.FromIdentity(User.Identity as ClaimsIdentity);

            if (externalLogin == null)
            {
                return InternalServerError();
            }

            if (externalLogin.LoginProvider != provider)
            {

                _userManager.SignOut(AuthenticationTypes);

                return new ChallengeResult(provider, this);

            }

            var user = await _userManager.FindAsync(new ApplicationUserLoginInfo(externalLogin.LoginProvider,
                externalLogin.ProviderKey));

            bool hasRegistered = user != null;

            if (hasRegistered)
            {
                //_userManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);

                _userManager.SignOut(AuthenticationTypes); 

                //ClaimsIdentity oAuthIdentity = await _userManager.GetExternalIdentityAsync(_userManager,
                //   OAuthDefaults.AuthenticationType);
                //ClaimsIdentity cookieIdentity = await user.GenerateUserIdentityAsync(_userManager,
                //    CookieAuthenticationDefaults.AuthenticationType);

                //AuthenticationProperties properties = ApplicationOAuthProvider.CreateProperties(user.UserName);
                //Authentication.SignIn(properties, oAuthIdentity, cookieIdentity);
            }
            else
            {
                IList<Claim> claims = await _userManager.GetClaimsAsync(User.Identity.GetUserId().Value); //externalLogin.GetClaims();
                //ClaimsIdentity identity = new ClaimsIdentity(claims, OAuthDefaults.AuthenticationType);
                //_userManager.SignIn(identity);
            }

            return Ok();
        }

        // GET api/Account/ExternalLogins?returnUrl=%2F&generateState=true 
        /// <summary>
        /// Gets a list of available external providers.
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <param name="generateState"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("ExternalLogins")]
        public IEnumerable<ExternalLoginViewModel> GetExternalLogins(string returnUrl, bool generateState = false)
        {
            IEnumerable<ApplicationAuthenticationDescription> descriptions = _userManager.GetExternalAuthenticationTypes();
            List<ExternalLoginViewModel> logins = new List<ExternalLoginViewModel>();

            string state;

            if (generateState)
            {
                const int strengthInBits = 256;
                state = RandomOAuthStateGenerator.Generate(strengthInBits);
            }
            else
            {
                state = null;
            }

            foreach (ApplicationAuthenticationDescription description in descriptions)
            {
                ExternalLoginViewModel login = new ExternalLoginViewModel
                {
                    Name = description.Caption,
                    Url = Url.Route("ExternalLogin", new
                    {
                        provider = description.AuthenticationType,
                        response_type = "token",
                        client_id = ConfigurationManager.AppSettings["as:AudienceId"], //Startup.PublicClientId,
                        redirect_uri = new Uri(Request.RequestUri, returnUrl).AbsoluteUri,
                        state = state
                    }),
                    State = state
                };
                logins.Add(login);
            }

            return logins;
        }
        
        // POST api/Account/Logout 
        [Route("Logout")]
        public IHttpActionResult Logout()
        {
            _userManager.SignOut("External Cookie"); // TEMP TEMP TEMP, REMOVE

                                                     // _userManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
                                                     //_userManager.SignOut(CookieAuthenticationDefaults.AuthenticationType);
                                                     // _userManager.SignOut(CookieAuthenticationDefaults.AuthenticationType);
                                                     
            return Ok();
        }

        // GET api/Account/ManageInfo?returnUrl=%2F&generateState=true 
        [Route("ManageInfo")]
        public async Task<ManageInfoViewModel> GetManageInfo(string returnUrl, bool generateState = false)
        {
            AppUser user = await _userManager.FindByIdAsync(User.Identity.GetUserId().Value);

            if (user == null)
            {
                return null;
            }

            List<UserLoginInfoViewModel> logins = new List<UserLoginInfoViewModel>();

            foreach (var linkedAccount in user.Logins)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = linkedAccount.LoginProvider,
                    ProviderKey = linkedAccount.ProviderKey
                });
            }

            if (user.PasswordHash != null)
            {
                logins.Add(new UserLoginInfoViewModel
                {
                    LoginProvider = LocalLoginProvider,
                    ProviderKey = user.UserName,
                });
            }

            return new ManageInfoViewModel
            {
                LocalLoginProvider = LocalLoginProvider,
                Email = user.UserName,
                Logins = logins,
                ExternalLoginProviders = GetExternalLogins(returnUrl, generateState)
            };
        }

        // POST api/Account/ChangePassword 
        [Route("ChangePassword")]
        public async Task<IHttpActionResult> ChangePassword(ChangePasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationIdentityResult result = await _userManager.ChangePasswordAsync(
                User.Identity.GetUserId().Value,
                model.OldPassword, model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        // POST api/Account/SetPassword 
        [Route("SetPassword")]
        public async Task<IHttpActionResult> SetPassword(SetPasswordBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _userManager.AddPasswordAsync(User.Identity.GetUserId().Value,
                model.NewPassword);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }
        
        // POST api/Account/RemoveLogin 
        [Route("RemoveLogin")]
        public async Task<IHttpActionResult> RemoveLogin(RemoveLoginBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ApplicationIdentityResult result;

            if (model.LoginProvider == LocalLoginProvider)
            {
                result = await _userManager.RemovePasswordAsync(User.Identity.GetUserId().Value);
            }
            else
            {
                result = await _userManager.RemoveLoginAsync(User.Identity.GetUserId().Value,
                    new ApplicationUserLoginInfo(model.LoginProvider, model.ProviderKey));
            }

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }
        
        // POST api/v1/accounts/Register 
        /// <summary>
        /// Registration
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [Route("Register")]
        //[ValidateAntiForgeryToken]
        public async Task<IHttpActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = new AppUser
            {
                UserName = model.Email,
                Email = model.Email
            };

            // Register user
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            // Create email confirmation token
            var emailConfirmationToken = await _userManager.GenerateEmailConfirmationTokenAsync(user.Id);

            var callbackUrl = new Uri(Url.Link("ConfirmEmailRoute", new { userId = user.Id, code = emailConfirmationToken }));
            
            await _userManager.SendEmailAsync(user.Id,
                "Cyrus Confirmation: Almost Done",
                $"Please confirm your account by clicking this link: <a href=\"{callbackUrl}\">Click Here To Confirm Your Registration</a>");
            
            //if (user.TwoFactorEnabled)
            //{
            //    // Why do we need this?
            //    var changePhoneNumberToken =
            //        await _userManager.GenerateChangePhoneNumberTokenAsync(user.Id, model.PhoneNumber);

            //    await _userManager.SendSmsAsync(user.Id, "Your Cyrus verification code is ");
            //}
            
            await _userManager.AddClaimAsync(user.Id, 
                new Claim("profile_status", "Incomplete"));

            await _userManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
            
            return Ok();
        }

        // GET: /Account/ConfirmEmail
        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="code"></param>
        /// <returns></returns>
        [AllowAnonymous]
        [HttpGet]
        [Route("ConfirmEmail", Name = "ConfirmEmailRoute")]
        public async Task<IHttpActionResult> ConfirmEmail(int? userId, string code)
        {
            if (userId == null || code == null)
            {
                return BadRequest("null parameter");
            }
            var result = await _userManager.ConfirmEmailAsync(userId.Value, code);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }
        
        // POST api/Account/RegisterExternal 
        [OverrideAuthentication]
        //[HostAuthentication(DefaultAuthenticationTypes.ExternalBearer)]
        [Route("RegisterExternal")]
        public async Task<IHttpActionResult> RegisterExternal(RegisterExternalBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var info = await _userManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return InternalServerError();
            }

            var user = new AppUser()
            {
                UserName = model.Email,
                Email = model.Email
            };

            var result = await _userManager.CreateAsync(user);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            result = await _userManager.AddLoginAsync(user.Id, info.Login);

            if (!result.Succeeded)
            {
                return GetErrorResult(result);
            }

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && _userManager != null)
            {
                _userManager.Dispose();
            }

            base.Dispose(disposing);
        }
        
        #region Helpers 

        private IHttpActionResult GetErrorResult(ApplicationIdentityResult result)
        {
            if (result == null)
            {
                return InternalServerError();
            }

            if (!result.Succeeded)
            {
                if (result.Errors != null)
                {
                    foreach (string error in result.Errors)
                    {
                        ModelState.AddModelError("", error);
                    }
                }

                if (ModelState.IsValid)
                {
                    // No ModelState errors are available to send, so just return an empty BadRequest. 
                    return BadRequest();
                }

                return BadRequest(ModelState);
            }

            return null;
        }

        private class ExternalLoginData
        {
            public string LoginProvider { get; set; }
            public string ProviderKey { get; set; }
            public string UserName { get; set; }

            public IList<Claim> GetClaims()
            {
                IList<Claim> claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.NameIdentifier, ProviderKey, null, LoginProvider));

                if (UserName != null)
                {
                    claims.Add(new Claim(ClaimTypes.Name, UserName, null, LoginProvider));
                }

                return claims;
            }

            public static ExternalLoginData FromIdentity(ClaimsIdentity identity)
            {
                if (identity == null)
                {
                    return null;
                }

                Claim providerKeyClaim = identity.FindFirst(ClaimTypes.NameIdentifier);

                if (providerKeyClaim == null || String.IsNullOrEmpty(providerKeyClaim.Issuer)
                    || String.IsNullOrEmpty(providerKeyClaim.Value))
                {
                    return null;
                }

                if (providerKeyClaim.Issuer == ClaimsIdentity.DefaultIssuer)
                {
                    return null;
                }

                return new ExternalLoginData
                {
                    LoginProvider = providerKeyClaim.Issuer,
                    ProviderKey = providerKeyClaim.Value,
                    UserName = identity.FindFirstValue(ClaimTypes.Name)
                };
            }
        }

        private static class RandomOAuthStateGenerator
        {
            private static RandomNumberGenerator _random = new RNGCryptoServiceProvider();

            public static string Generate(int strengthInBits)
            {
                const int bitsPerByte = 8;

                if (strengthInBits % bitsPerByte != 0)
                {
                    throw new ArgumentException("strengthInBits must be evenly divisible by 8.", "strengthInBits");
                }

                int strengthInBytes = strengthInBits / bitsPerByte;

                byte[] data = new byte[strengthInBytes];
                _random.GetBytes(data);
                return HttpServerUtility.UrlTokenEncode(data);
            }

        }

        #endregion
    }

}
