using System;
using System.Linq.Expressions;

namespace Cyrus.Core.DomainServices
{
    public interface IIncludeQuery<TEntity>
    {
        Expression<Func<TEntity, object>>[] IncludeProperties { get; }
    }
}
