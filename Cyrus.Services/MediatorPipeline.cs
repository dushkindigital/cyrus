using Cyrus.Core.DomainServices.Decorators;
using Cyrus.Services.Bases;
using MediatR;

namespace Cyrus.Services
{
    public class MediatorPipeline<TRequest, TResponse>
    : IRequestHandler<TRequest, TResponse>
    where TRequest : BaseRequest, IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _inner;
        private readonly IPreRequestHandler<TRequest>[] _preRequestHandlers;

        public MediatorPipeline(
            IRequestHandler<TRequest, TResponse> inner,
            IPreRequestHandler<TRequest>[] preRequestHandlers
            )
        {
            _inner = inner;
            _preRequestHandlers = preRequestHandlers;
        }

        public TResponse Handle(TRequest message)
        {
            foreach (var preRequestHandler in _preRequestHandlers)
            {
                preRequestHandler.Handle(message);
            }

            return _inner.Handle(message);
        }
    }
}
