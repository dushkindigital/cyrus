using System;
using System.Linq;

namespace Cyrus.Core.DomainServices
{
    public interface IOrderByQuery<TEntity>
    {
        Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> OrderBy { get; }
    }
}
