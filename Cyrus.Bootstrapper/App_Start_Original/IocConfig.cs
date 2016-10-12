using System.Reflection;
using System.Web.Http;
using Cyrus.Bootstrapper;
using Cyrus.Core.Data;
using Cyrus.Core.DomainServices;
using Cyrus.Data;
using Cyrus.Infrastructure.Logging;
using Cyrus.Services;
using Autofac;
using Autofac.Integration.WebApi;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(IocConfig), "RegisterDependencies")]

namespace Cyrus.Bootstrapper
{
    public class IocConfig
    {
        public static void RegisterDependencies()
        {
            var builder = new ContainerBuilder();
            var config = new HttpConfiguration();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            //builder.RegisterApiControllers(Assembly.Load("Cyrus.WebApi"));

            const string nameOrConnectionString = "name=AppContext";

            builder.RegisterGeneric(typeof(EntityRepository<>)).As(typeof(IRepository<>)).InstancePerRequest();
            builder.RegisterGeneric(typeof(Service<>)).As(typeof(IService<>)).InstancePerRequest();
            builder.RegisterType(typeof(UnitOfWork)).As(typeof(IUnitOfWork)).InstancePerRequest();

            builder.Register<IEntitiesContext>(b =>
            {
                var logger = b.Resolve<Core.Logging.ILogger>();
                var context = new CyrusDataContext(nameOrConnectionString, logger);
                return context;
            }).InstancePerRequest();

            builder.Register(b => NLogLogger.Instance).SingleInstance();
            builder.RegisterModule(new IdentityModule());

            var container = builder.Build();
            
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);

            

        }
    }
}