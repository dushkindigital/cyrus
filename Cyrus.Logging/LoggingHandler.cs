using Cyrus.Core.DomainServices;
using MediatR;

namespace Cyrus.Services
{
    public class LoggingHandler<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : BaseRequest, IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _inner;

        public LoggingHandler(IRequestHandler<TRequest, TResponse> inner)
        {
            _inner = inner;
        }

        public TResponse Handle(TRequest request)
        {
            var baseRequest = (BaseRequest)request;

            //log4net.LogicalThreadContext.Properties["TrackingId"] = baseRequest.TrackingId.ToString();
            NLog.MappedDiagnosticsLogicalContext.Set("TrackingId", baseRequest.TrackingId.ToString());

            return _inner.Handle(request);
        }
    }
}
