using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using Microsoft.Extensions.Logging;

namespace Smasher
{
    public class AutofacConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var builder = new ContainerBuilder();
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            builder.RegisterWebApiFilterProvider(config);
            builder.RegisterWebApiModelBinderProvider();

            var loggerFactory = LoggerFactory.Create(x => x.AddSimpleConsole());
            builder.RegisterInstance(loggerFactory).As<ILoggerFactory>().SingleInstance();
            builder.RegisterGeneric(typeof(Logger<>)).As(typeof(ILogger<>)).SingleInstance();

            builder.Register(x =>
            {
                var httpClient = new HttpClient();
                return new ApiProxy(httpClient, x.Resolve<ILogger<ApiProxy>>());
            }).As<IApiProxy>();

            var container = builder.Build();
            config.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}