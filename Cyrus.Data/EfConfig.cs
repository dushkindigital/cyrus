using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using Cyrus.Core.DomainModels;
using Cyrus.Data.Identity.Models;

namespace Cyrus.Data
{
    public class EfConfig
    {
        public static void ConfigureEf(DbModelBuilder modelBuilder)
        {

            #region TRIBES
            modelBuilder.Entity<Tribe>()
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<TribeMember>()
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<TribeRequest>()
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            #endregion

            #region PROFILES
            modelBuilder.Entity<ProfileAttribute>()
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            #endregion

            #region IDENTITY
            modelBuilder.Entity<ApplicationIdentityUser>()
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ApplicationIdentityRole>()
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<ApplicationIdentityUserClaim>()
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            modelBuilder.Entity<UserProfile>()
                .Property(e => e.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            #endregion
        }
    }
}
