using System;
using Owin;

/// <summary>
/// Initialization and configurations are handled here. 
/// </summary>
/// <remarks>
/// The owin pipeline initialization is handled in keys.config in WebApi.
/// </remarks>
namespace Cyrus.Bootstrapper
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            string trackingId = Guid.NewGuid().ToString();
            NLog.MappedDiagnosticsLogicalContext.Set("TrackingId", trackingId);

            ConfigureOAuth(app);

            IocConfig.RegisterDependencies(app);
            
        }
    }
}
