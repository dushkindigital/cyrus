using System.Web.Http;

namespace Cyrus.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutoMapperConfig.Initialize();
        }
    }
}
