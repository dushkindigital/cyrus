using AutoMapper;
using Cyrus.WebApi.AutoMapperProfiles;

namespace Cyrus.WebApi
{
    public class AutoMapperConfig
    {
        public static void Initialize()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new AutoMapperProfile());
            });
        }
    }
}