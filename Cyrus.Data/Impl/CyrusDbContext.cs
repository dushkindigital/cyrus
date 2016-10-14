using System.Data.Entity;
using Cyrus.Core.DomainModels;
using Cyrus.Data.Mappings;

namespace Cyrus.Data
{
    public class CyrusDbContext : DbContext, ICyrusDbContext
    {
        public CyrusDbContext()
            : base("name=CyrusDbContext") // use app.config transforms or web.config transforms to change this in the UI/Presentation project
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new TribeMapping());
            modelBuilder.Configurations.Add(new TribeMemberMapping());
            modelBuilder.Configurations.Add(new TribeRequestMapping());
        }

        public DbSet<Tribe> Tribes { get; set; }
        public DbSet<TribeMember> TribeMembers { get; set; }
        public DbSet<TribeRequest> TribeRequests { get; set; }
        
    }

}
