using System.Data.Entity;
using Cyrus.Core.Identity;
using Cyrus.Data;
using Cyrus.Data.Identity;
using Cyrus.Data.Identity.Models;
using Autofac;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.AspNet.Identity;
using System.Web;
using Module = Autofac.Module;
using System;

namespace Cyrus.Bootstrapper
{
    public class IdentityModule : Module
    {
        private readonly System.Reflection.Assembly[] _assembliesToScan;

        public IdentityModule(params System.Reflection.Assembly[] assembliesToScan)
            : base()
        {
            _assembliesToScan = assembliesToScan;
        }

        protected override void Load(ContainerBuilder builder)
        {
            
                builder.RegisterType(typeof(ApplicationUserManager)).As(typeof(IApplicationUserManager)).InstancePerRequest();
                builder.RegisterType(typeof(ApplicationRoleManager)).As(typeof(IApplicationRoleManager)).InstancePerRequest();
                builder.RegisterType(typeof(ApplicationIdentityUser)).As(typeof(IUser<int>)).InstancePerRequest();


                builder.Register(b => b.Resolve<ICyrusDbContext>() as DbContext).InstancePerRequest();

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
