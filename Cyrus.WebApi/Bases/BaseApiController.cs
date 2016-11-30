using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using Cyrus.Core.DomainModels.Identity;
using Cyrus.Core.Identity;
using Cyrus.Data.Identity;

namespace Cyrus.WebApi.Bases
{
    public class BaseApiController : ApiController
    {
        private AppUser _member;

        public int UserIdentityId
        {
            get
            {
                var user = FindByName(User.Identity.Name);
                return user.Id;
            }
        }
        
        //public async Task<AppUser> UserRecord
        //{
        //    get
        //    {
        //        if (_member != null)
        //        {
        //            return _member;
        //        }
        //        _member = UserManager.FindByEmailAsync(Thread.CurrentPrincipal.Identity.Name);
        //        return _member;
        //    }
        //    set { _member = value; }
        //}
    }
}