using Cyrus.Core.DomainServices;
using System.Threading.Tasks;
using MediatR;

namespace Cyrus.Services
{
    public class AsyncLoggingHandler<TRequest, TResponse> : IAsyncRequestHandler<TRequest, TResponse>
        where TRequest : BaseRequest, IAsyncRequest<TResponse>
    {
        private readonly IAsyncRequestHandler<TRequest, TResponse> _inner;

        public AsyncLoggingHandler(IAsyncRequestHandler<TRequest, TResponse> inner)
        {
            _inner = inner;
        }

        public async Task<TResponse> Handle(TRequest request)
        {
            var baseRequest = (BaseRequest)request;

            NLog.MappedDiagnosticsLogicalContext.Set("TrackingId", baseRequest.TrackingId.ToString());

            //log4net.LogicalThreadContext.Properties["TrackingId"] = baseRequest.TrackingId.ToString();

            return await _inner.Handle(request);
        }
    }
}
