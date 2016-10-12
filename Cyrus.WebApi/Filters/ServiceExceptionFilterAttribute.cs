using System.Net;
using System.Net.Http;
using Cyrus.Core.DomainServices;
using System.Web.Http.Filters;

namespace Cyrus.WebApi.Filters
{
    public class ServiceExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            var request = actionExecutedContext.ActionContext.Request;

            var exception = actionExecutedContext.Exception as ServiceException;
            if (exception != null)
            {
                var response = new
                {
                    Message = exception.Message,
                    TrackingId = exception.TrackingId
                };
                actionExecutedContext.Response = request.CreateResponse(HttpStatusCode.InternalServerError, response);
            }
        }
    }
}