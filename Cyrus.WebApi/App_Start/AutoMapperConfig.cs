using AutoMapper;
using System.Web.Http;

namespace Cyrus.WebApi
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            var profiles = GlobalConfiguration.Configuration.DependencyResolver.GetServices(typeof(Profile));

            Mapper.Initialize(cfg =>
            {
                foreach (Profile profile in profiles)
                    cfg.AddProfile(profile);
            });
        }
    }
}