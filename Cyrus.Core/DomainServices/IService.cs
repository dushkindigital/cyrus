using System;
using Cyrus.Core.Data;

namespace Cyrus.Core.DomainServices
{
    public interface IService : IDisposable
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
