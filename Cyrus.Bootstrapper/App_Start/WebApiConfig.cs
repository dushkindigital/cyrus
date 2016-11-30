using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Cyrus.WebApi.Filters;
using FluentValidation.WebApi;

namespace Cyrus.Bootstrapper
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

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always; //Was "Never". Remove for upper environs.

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/v1/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            // For JSON-formatted Serialization
            var jsonSettings = config.Formatters.JsonFormatter.SerializerSettings;
            jsonSettings.Formatting = Formatting.Indented;
            jsonSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        }
    }
}