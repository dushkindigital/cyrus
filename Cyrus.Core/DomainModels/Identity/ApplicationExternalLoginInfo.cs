﻿using System.Security.Claims;

namespace Cyrus.Core.DomainModels.Identity
{
    public class ApplicationExternalLoginInfo
    {
        public string DefaultUserName
        {
            get;
            set;
        }

        public string Email
        {
            get;
            set;
        }

        public ClaimsIdentity ExternalIdentity
        {
            get;
            set;
        }

        public ApplicationUserLoginInfo Login
        {
            get;
            set;
        }
    }
}
