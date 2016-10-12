using Cyrus.Data;
using Cyrus.Services.Bases;
using Mehdime.Entity;

namespace Cyrus.Services.Query
{
    public class AsyncGenericQueryHandler<TEntity> : BaseAsyncGenericQueryHandler<TEntity, ICyrusDbContext>
        where TEntity : class
    {
        public AsyncGenericQueryHandler(IDbContextScopeFactory dbContextScopeFactory)
            : base(dbContextScopeFactory) { }
    }
}
