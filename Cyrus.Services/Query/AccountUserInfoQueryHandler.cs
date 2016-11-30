using System;
using System.Linq;
using MediatR;
using Mehdime.Entity;
using Cyrus.Data.Extensions;
using Cyrus.Core.DomainServices.Dto;
using Cyrus.Core.DomainServices.Query;
using Cyrus.Data;
using Cyrus.Core.Identity;

namespace Cyrus.Services.Query
{
    public class AccountUserInfoQueryHandler : IRequestHandler<AccountUserInfoQuery, AccountUserInfoDto>
    {
        private readonly IApplicationUserManager _userManager;

        public AccountUserInfoQueryHandler(IApplicationUserManager userManager)
        {
            if (userManager == null)
                throw new ArgumentNullException(nameof(userManager), "userManager");

            _userManager = userManager;

        }

        public AccountUserInfoDto Handle(AccountUserInfoQuery query)
        {
            var externalLogin =  _userManager.GetExternalLoginInfo();

            if (externalLogin == null)
            {
                throw new InvalidOperationException("No External Login data found for this user");
            }

            var externalLoginDto = new AccountUserInfoDto()
            {
                Email = externalLogin.Email,
                HasRegistered = externalLogin == null,
                LoginProvider = externalLogin != null ? externalLogin.Login.LoginProvider : null
            };
            
            return externalLoginDto;
            
        }
    }
}
