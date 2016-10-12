using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Cyrus.Core.DomainModels;

namespace Cyrus.Data.Mappings
{
    class TribeRequestMapping : EntityTypeConfiguration<TribeRequest>
    {
        public TribeRequestMapping()
        {
            this.HasKey(t => t.UserId);

            this.Property(t => t.UserId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None)
                .IsRequired();

        }
    }
}
