using System.Data.Entity;
using Cyrus.Core.DomainModels;
using Cyrus.Data.Mappings;
using Microsoft.AspNet.Identity.EntityFramework;
using Cyrus.Data.Identity.Models;


namespace Cyrus.Data
{
    //public class CyrusDbContext : DbContext, ICyrusDbContext
    public class CyrusDbContext : IdentityDbContext<ApplicationIdentityUser, ApplicationIdentityRole, int, ApplicationIdentityUserLogin, ApplicationIdentityUserRole, ApplicationIdentityUserClaim>, ICyrusDbContext
    {
       
        public CyrusDbContext()
            : base("name=CyrusDbContext") // use app.config transforms or web.config transforms to change this in the UI/Presentation project
        {   
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            EfConfig.ConfigureEf(modelBuilder);
        }

        public DbSet<Tribe> Tribes { get; set; }
        public DbSet<TribeMember> TribeMembers { get; set; }
        public DbSet<TribeRequest> TribeRequests { get; set; }
        public DbSet<ProfileAttribute> ProfileAttributes { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        
    }

}
