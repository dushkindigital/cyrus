using System;
using System.Linq.Expressions;


namespace Cyrus.Core.DomainServices
{
    public interface IFilterQuery<TEntity>
    {
        Expression<Func<TEntity, bool>> Predicate { get; }
    }
}
