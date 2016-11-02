using System;
using Owin;

// Owin startup is handled in Web.config
namespace Cyrus.Bootstrapper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            string trackingId = Guid.NewGuid().ToString();
            NLog.MappedDiagnosticsLogicalContext.Set("TrackingId", trackingId);
            
            IocConfig.RegisterDependencies(app);

            ConfigureOAuth(app);
            
        }
    }
}
