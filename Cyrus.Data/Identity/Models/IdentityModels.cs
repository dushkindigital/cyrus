using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Cyrus.Core.DomainModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Cyrus.Data.Identity.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationIdentityUser class, 
    // please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.

    public class ApplicationIdentityUser :
        IdentityUser<int, ApplicationIdentityUserLogin, ApplicationIdentityUserRole, ApplicationIdentityUserClaim>
    {
        public virtual UserProfile ProfileInfo { get; set; }
        
    }


    public class ApplicationIdentityRole : IdentityRole<int, ApplicationIdentityUserRole>
    {
        public ApplicationIdentityRole()
        {
        }

        public ApplicationIdentityRole(string name)
        {
            Name = name;
        }
    }

    public class ApplicationIdentityUserRole : IdentityUserRole<int>
    {
    }

    public class ApplicationIdentityUserClaim : IdentityUserClaim<int>
    {
    }

    public class ApplicationIdentityUserLogin : IdentityUserLogin<int>
    {
    }

    public class UserProfile : BaseEntity
    {
        public int UserId { get; set; }
        public string ProfileName { get; set; }

        #region navigation properties
        public ICollection<ProfileAttribute> Attributes { get; set; }
        #endregion

    }

}