using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Cyrus.Bootstrapper.Config;
using Mehdime.Entity;


namespace Cyrus.Bootstrapper
{
    public class IocConfig
    {
        public static void RegisterDependencies()
        {
            DbContextScopeExtensionConfig.Setup();

            var builder = new ContainerBuilder();

            // Get HttpConfiguration
            //var config = GlobalConfiguration.Configuration;
            var config = new HttpConfiguration();

            //builder.RegisterApiControllers(typeof(WebApiApplication).Assembly);
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterType<DbContextScopeFactory>().As<IDbContextScopeFactory>().SingleInstance();

            builder.RegisterType<AmbientDbContextLocator>().As<IAmbientDbContextLocator>().SingleInstance();

            // Registers our IMediator (abstraction for observer pattern, which lets us use CQRS)
            builder.RegisterModule(new MediatorModule(Assembly.Load("Cyrus.Services")));

            // Registers our Fluent Validations that we use on our Models
            builder.RegisterModule(new FluentValidationModule(Assembly.Load("Cyrus.WebApi"), Assembly.Load("Cyrus.Services")));

            // Registers our AutoMapper Profiles
            builder.RegisterModule(new AutoMapperModule(Assembly.Load("Cyrus.WebApi"), Assembly.Load("Cyrus.Services")));

            // Registers our Identity
            builder.RegisterModule(new IdentityModule(Assembly.Load("Cyrus.WebApi"), Assembly.Load("Cyrus.Services")));

            var container = builder.Build();

            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

        }
    }
}
