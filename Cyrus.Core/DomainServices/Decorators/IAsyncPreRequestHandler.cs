using System.Threading.Tasks;

namespace Cyrus.Core.DomainServices.Decorators
{
    public interface IAsyncPreRequestHandler<in TRequest>
    {
        Task Handle(TRequest request);
    }
}
