using Mehdime.Entity;
using Cyrus.Services.Bases;
using Cyrus.Data;

namespace Cyrus.Services.Query
{
    public class AsyncPaginateQueryHandler<TEntity> : BaseAsyncPaginateQueryHandler<TEntity, ICyrusDbContext>
        where TEntity : class
    {
        public AsyncPaginateQueryHandler(IDbContextScopeFactory dbContextScopeFactory)
            : base(dbContextScopeFactory) { }
    }
}
