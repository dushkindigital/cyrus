using Mehdime.Entity;
using Cyrus.Services.Bases;
using Cyrus.Data;

namespace Cyrus.Services.Query
{
    public class GenericQueryHandler<TEntity> : BaseGenericQueryHandler<TEntity, ICyrusDbContext>
        where TEntity : class
    {
        public GenericQueryHandler(IDbContextScopeFactory dbContextScopeFactory)
            : base(dbContextScopeFactory) { }
    }
}
