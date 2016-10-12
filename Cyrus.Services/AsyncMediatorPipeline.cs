using System.Threading.Tasks;
using Cyrus.Core.DomainServices.Decorators;
using Cyrus.Services.Bases;
using MediatR;

namespace Cyrus.Services
{
    public class AsyncMediatorPipeline<TRequest, TResponse>
    : IAsyncRequestHandler<TRequest, TResponse>
    where TRequest : BaseRequest, IAsyncRequest<TResponse>
    {
        private readonly IAsyncRequestHandler<TRequest, TResponse> _inner;
        private readonly IAsyncPreRequestHandler<TRequest>[] _preRequestHandlers;

        public AsyncMediatorPipeline(
            IAsyncRequestHandler<TRequest, TResponse> inner,
            IAsyncPreRequestHandler<TRequest>[] preRequestHandlers
            )
        {
            _inner = inner;
            _preRequestHandlers = preRequestHandlers;
        }

        public async Task<TResponse> Handle(TRequest message)
        {
            foreach (var preRequestHandler in _preRequestHandlers)
            {
                await preRequestHandler.Handle(message);
            }

            return await _inner.Handle(message);
        }
    }
}
