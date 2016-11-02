using Autofac;
using Autofac.Integration.WebApi;
using Mehdime.Entity;
using System.Web.Http;
using System.Reflection;
using Cyrus.Bootstrapper.Config;
using Microsoft.Owin.Cors;
//using Cyrus.WebApi;
using Owin;

//[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(IocConfig), "RegisterDependencies")]

namespace Cyrus.Bootstrapper
{
    public class IocConfig
    {
        /// <summary>
        /// Autofac type registration / template construction
        /// </summary>
        /// <param name="config"></param>
        public static void RegisterDependencies(IAppBuilder app)
        {
            DbContextScopeExtensionConfig.Setup();

            // Get your HttpConfiguration. In OWIN, you'll create one
            // rather than using GlobalConfiguration.
            var config = new HttpConfiguration();

            // register api routing.
            WebApiConfig.Register(config);
            
            // Run optional steps, like registering filters,
            // per-controller-type services, etc. 

            var builder = new ContainerBuilder();

            // Register Web API controller in executing assembly.
               builder.RegisterApiControllers(Assembly.Load("Cyrus.WebApi"));
            
            //Helper nuget for managing the DbContext lifetime in Entity Framework. Please see: http://mehdi.me/ambient-dbcontext-in-ef6/
            builder.RegisterType<DbContextScopeFactory>().As<IDbContextScopeFactory>().SingleInstance();
            builder.RegisterType<AmbientDbContextLocator>().As<IAmbientDbContextLocator>().SingleInstance();

            // Registers our IMediator (abstraction for observer pattern, which lets us use CQRS)
            builder.RegisterModule(new MediatorModule(Assembly.Load("Cyrus.Services")));

            // Registers our Fluent Validations that we use on our Models
            builder.RegisterModule(new FluentValidationModule(Assembly.Load("Cyrus.WebApi"), Assembly.Load("Cyrus.Services")));

            // Registers our AutoMapper Profiles
            builder.RegisterModule(new AutoMapperModule(Assembly.Load("Cyrus.WebApi"), Assembly.Load("Cyrus.Services")));

            // Registers our ASP.NET Identity custom classes.
            builder.RegisterModule(new IdentityModule(Assembly.Load("Cyrus.Data"), Assembly.Load("Cyrus.Core")));

            // Set the dependency resolver to be Autofac.
            var container = builder.Build();

            // Glimpse nuget package - helps view registered autofac dependencies.
            // container.ActivateGlimpse(); -- causes problems loading Autofac 

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            // Register the Autofac middleware FIRST, then the Autofac Web API middleware,
            // and finally the standard Web API middleware.
            app.UseAutofacMiddleware(container);
            app.UseAutofacWebApi(config);
            app.UseCors(CorsOptions.AllowAll); // Enables crossdomain requests
            app.UseWebApi(config);

        }
    }
}
