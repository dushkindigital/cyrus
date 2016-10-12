using System.Data.Entity;
using System.Reflection;
using System.Web.Http.Controllers;
using Cyrus.Core.Identity;
using Cyrus.Data;
using Cyrus.Data.Identity;
using Cyrus.Data.Identity.Models;
using Autofac;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Host.SystemWeb;
using System.Web;
using Autofac.Integration.WebApi;
using Module = Autofac.Module;

namespace Cyrus.Bootstrapper
{
    public class IdentityModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {

            builder.RegisterType(typeof(ApplicationUserManager)).As(typeof(IApplicationUserManager)).InstancePerRequest();
            builder.RegisterType(typeof(ApplicationRoleManager)).As(typeof(IApplicationRoleManager)).InstancePerRequest();
            builder.RegisterType(typeof(ApplicationIdentityUser)).As(typeof(IUser<int>)).InstancePerRequest();


            builder.Register(b => b.Resolve<IEntitiesContext>() as DbContext).InstancePerRequest();

            builder.Register(b =>
            {
                var manager = IdentityFactory.CreateUserManager(b.Resolve<DbContext>());
                if (Startup.DataProtectionProvider != null)
                {
                    manager.UserTokenProvider =
                        new DataProtectorTokenProvider<ApplicationIdentityUser, int>(
                            Startup.DataProtectionProvider.Create("ASP.NET Identity"));
                }
                return manager;
            }).InstancePerRequest();

            builder.Register(b => IdentityFactory.CreateRoleManager(b.Resolve<DbContext>())).InstancePerRequest();
            builder.Register(b => HttpContext.Current.Request.GetOwinContext().Authentication).InstancePerRequest();

        }
    }
}
