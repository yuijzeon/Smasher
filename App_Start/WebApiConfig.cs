using System.Web.Http;
using System.Web.Routing;
using NSwag.AspNet.Owin;

namespace Smasher
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            const string routeTemplate = "api/{controller}/{action}";

            // Web API 設定和服務
            RouteTable.Routes.MapOwinPath("swagger", app =>
            {
                app.UseSwaggerUi(typeof(WebApiApplication).Assembly, settings =>
                {
                    settings.MiddlewareBasePath = "/swagger";
                    settings.GeneratorSettings.DefaultUrlTemplate = routeTemplate;
                });
            });

            // Web API 路由
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute("DefaultApi", routeTemplate);
        }
    }
}