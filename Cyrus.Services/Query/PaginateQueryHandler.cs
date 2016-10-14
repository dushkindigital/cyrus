using Mehdime.Entity;
using Cyrus.Services.Bases;
using Cyrus.Data;

namespace Cyrus.Services.Query
{
    public class PaginateQueryHandler<TEntity> : BasePaginateQueryHandler<TEntity, ICyrusDbContext>
        where TEntity : class
    {
        public PaginateQueryHandler(IDbContextScopeFactory dbContextScopeFactory)
            : base(dbContextScopeFactory) { }
    }
}
