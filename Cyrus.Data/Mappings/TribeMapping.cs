using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using Cyrus.Core.DomainModels;

namespace Cyrus.Data.Mappings
{
    public class TribeMapping : EntityTypeConfiguration<Tribe>
    {
        public TribeMapping()
        {
            this.HasKey(t => t.Id);

            this.Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);

            this.Property(t => t.Description)
                .HasMaxLength(250)
                .IsRequired();

            this.Property(t => t.Name)
                .HasMaxLength(250)
                .IsRequired();

            //Foreign Keys
            
        }
    }
}
