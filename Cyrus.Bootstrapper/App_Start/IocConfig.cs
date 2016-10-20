using Autofac;
using Autofac.Integration.WebApi;
using Mehdime.Entity;
using System.Web.Http;
using System.Reflection;
using Cyrus.Bootstrapper;
using Cyrus.Bootstrapper.Config;
using Cyrus.WebApi;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(IocConfig), "RegisterDependencies")]

namespace Cyrus.Bootstrapper
{
    public class IocConfig
    {
        public static void RegisterDependencies()
        {
            DbContextScopeExtensionConfig.Setup();

            var builder = new ContainerBuilder();

            // Get HttpConfiguration
            var config = GlobalConfiguration.Configuration;
            
            builder.RegisterApiControllers(typeof(WebApiApplication).Assembly);

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

            var container = builder.Build();

            // Glimpse nuget package - helps view registered autofac dependencies.
            // container.ActivateGlimpse(); -- causes problems loading Autofac 
                
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            
        }
    }
}
