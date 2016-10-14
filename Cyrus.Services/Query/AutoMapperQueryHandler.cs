using Cyrus.Data;
using Mehdime.Entity;

namespace Cyrus.Services.Query
{
    public class AutoMapperQueryHandler<TSrcEntity, TDestModel> : BaseAutoMapperQueryHandler<TSrcEntity, TDestModel, ICyrusDbContext>
        where TSrcEntity : class
    {
        public AutoMapperQueryHandler(IDbContextScopeFactory dbContextScopeFactory)
            : base(dbContextScopeFactory) { }
    }
}
