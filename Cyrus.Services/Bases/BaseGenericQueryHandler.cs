﻿using MediatR;
using Mehdime.Entity;
using Cyrus.Core.DomainServices.Extensions;
using Cyrus.Data.Extensions;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Cyrus.Core.DomainServices.Query;
using Cyrus.Data;

namespace Cyrus.Services.Bases
{
    public abstract class BaseGenericQueryHandler<TEntity, TDbContext>
        : IRequestHandler<GenericQuery<TEntity>, IEnumerable<TEntity>>
        where TEntity : class
        where TDbContext : class, IDbContext
    {
        private readonly IDbContextScopeFactory _dbContextScopeFactory;

        public BaseGenericQueryHandler(IDbContextScopeFactory dbContextScopeFactory)
        {
            _dbContextScopeFactory = dbContextScopeFactory;
        }

        public IEnumerable<TEntity> Handle(GenericQuery<TEntity> args)
        {
            using (var dbContextScope = _dbContextScopeFactory.CreateReadOnly())
            {
                IDbContext dbCtx = dbContextScope.DbContexts.GetByInterface<TDbContext>();

                ((DbContext)dbCtx).Configuration.ProxyCreationEnabled = false;

                IQueryable<TEntity> entities = dbCtx.Set<TEntity>();

                entities = entities.Include(args);
                entities = entities.Where(args);
                entities = entities.OrderBy(args);

                if (args.PageSize != 0)
                    entities = entities.Take(args.PageSize);

                // We may not want to have '.Take' be mandatory
                return entities.ToList();
            }
        }
    }
}
