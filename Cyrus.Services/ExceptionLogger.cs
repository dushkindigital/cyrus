using System;
using MediatR;
using Cyrus.Services.Logging;
using Cyrus.Services.Bases;

namespace Cyrus.Services
{
    public class ExceptionLogger<TRequest, TResponse>
    : IRequestHandler<TRequest, TResponse>
    where TRequest : BaseRequest, IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> _inner;

        public ExceptionLogger(
            IRequestHandler<TRequest, TResponse> inner
            )
        {
            _inner = inner;
        }

        public TResponse Handle(TRequest message)
        {
            var log = LogProvider.For<ExceptionLogger<TRequest, TResponse>>();
            log.Debug("Begin");
            try
            {
                return _inner.Handle(message);

            }
            catch (ServiceException e)
            {
                log.ErrorException(e.StackTrace, e);
                e.TrackingId = message.TrackingId.ToString();
                throw;
            }
            catch (Exception e)
            {
                log.ErrorException(e.StackTrace, e);
                throw new ServiceException(e.Message, message.TrackingId.ToString(), e);
            }
            finally
            {
                log.Debug("End");
            }
        }
    }
}
