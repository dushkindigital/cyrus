using Cyrus.Data;
using Mehdime.Entity;
using Cyrus.Services.Bases;

namespace Cyrus.Services.Query
{
    public class AsyncAutoMapperQueryHandler<TSrcEntity, TDestModel> : BaseAsyncAutoMapperQueryHandler<TSrcEntity, TDestModel, ICyrusDbContext>
        where TSrcEntity : class
    {
        public AsyncAutoMapperQueryHandler(IDbContextScopeFactory dbContextScopeFactory)
            : base(dbContextScopeFactory) { }
    }
}
