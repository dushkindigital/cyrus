namespace Cyrus.Core.DomainServices.Decorators
{
    public interface IPreRequestHandler<in TRequest>
    {
        void Handle(TRequest request);
    }
}
