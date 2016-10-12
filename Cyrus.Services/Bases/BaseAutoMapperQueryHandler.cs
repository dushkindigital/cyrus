using Cyrus.Core.DomainServices.Extensions;
using Cyrus.Data.Extensions;
using MediatR;
using Mehdime.Entity;
using System.Linq;
using System.Collections.Generic;
using AutoMapper.QueryableExtensions;
using Cyrus.Core.DomainServices.Query;
using Cyrus.Data;

namespace Cyrus.Services.Query
{
    public abstract class BaseAutoMapperQueryHandler<TSrcEntity, TDestModel, TDbContext>
        : IRequestHandler<AutoMapperQuery<TSrcEntity, TDestModel>, IEnumerable<TDestModel>>
        where TSrcEntity : class
        where TDbContext : class, IDbContext
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;

        public BaseAutoMapperQueryHandler(IDbContextScopeFactory dbContextScopeFactory)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
        }

        public IEnumerable<TDestModel> Handle(AutoMapperQuery<TSrcEntity, TDestModel> args)
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                // Gets our context from our context scope
                var dbCtx = dbContextScope.DbContexts.GetByInterface<TDbContext>();

                IQueryable<TSrcEntity> srcEntities = dbCtx.Set<TSrcEntity>();

                srcEntities = srcEntities.Where(args);
                IQueryable<TDestModel> destEntities = srcEntities.ProjectTo<TDestModel>();
                destEntities = destEntities.OrderBy(args);
                if (args.PageSize != 0)
                    destEntities = destEntities.Take(args.PageSize);
                return destEntities.ToList();
            }
        }
    }
}
