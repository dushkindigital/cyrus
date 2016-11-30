using AutoMapper;
using System.Web.Http;

namespace Cyrus.Bootstrapper
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            // use GlobalConfiguration or HttpConfiguration?
            var profiles = GlobalConfiguration.Configuration.DependencyResolver.GetServices(typeof(Profile));

            Mapper.Initialize(cfg =>
            {
                foreach (Profile profile in profiles)
                    cfg.AddProfile(profile);
            });
        }
    }
}