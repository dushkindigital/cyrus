using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Cyrus.Core.DomainModels;

namespace Cyrus.Data.Mappings
{
    public class TribeMemberMapping : EntityTypeConfiguration<TribeMember>
    {
        public TribeMemberMapping()
        {
            this.HasKey(t => t.Id);

            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.TribeId)
                .IsRequired();

            this.Property(t => t.UserId)
                .IsRequired();

            this.Property(t => t.IsAdmin)
                .IsRequired();

            this.Property(t => t.IsApproved)
                .IsRequired();
            
        }
    }
}
