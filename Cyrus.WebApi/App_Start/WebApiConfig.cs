using System.Web.Http;
using Cyrus.WebApi.Filters;
using FluentValidation.WebApi;

namespace Cyrus.WebApi
{
    public class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Enable debugging, remove in upper env.
            config.EnableSystemDiagnosticsTracing();

            // Web API configuration and services
            var validatorFactory = new FluentValidatorFactory();
            FluentValidationModelValidatorProvider.Configure(config, provider => provider.ValidatorFactory = validatorFactory);

            // Action Filters
            config.Filters.Add(new FluentValidationActionFilter());

            // Exception Filter
            config.Filters.Add(new ServiceExceptionFilterAttribute());

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Never;

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}