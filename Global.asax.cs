using System.Web;
using System.Web.Http;

namespace Smasher
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(config =>
            {
                AutofacConfig.Register(config);
                WebApiConfig.Register(config);
            });
        }
    }
}