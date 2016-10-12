using System.Data.Entity;
using Cyrus.Core.DomainModels;

namespace Cyrus.Data
{
    public interface ICyrusDbContext : IDbContext
    {
        DbSet<Tribe> Tribes { get; }
        DbSet<TribeMember> TribeMembers { get; }
        DbSet<TribeRequest> TribeRequests { get; }
    }
}
