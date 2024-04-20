using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Extensions.Logging;

namespace Smasher.Controllers
{
    public class DefaultController : ApiController
    {
        private readonly IApiProxy _apiProxy;
        private readonly ILogger<DefaultController> _logger;

        public DefaultController(IApiProxy apiProxy, ILogger<DefaultController> logger)
        {
            _apiProxy = apiProxy;
            _logger = logger;
        }

        [HttpGet]
        public IHttpActionResult Crash()
        {
            _apiProxy.DoSomethingAsync();
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult NotCrashNoLog()
        {
            Task.Run(async () => await _apiProxy.DoSomethingAsync());
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult NotCrashWithLog()
        {
            Task.Run(async () =>
            {
                try
                {
                    await _apiProxy.DoSomethingAsync();
                }
                catch (Exception e)
                {
                    _logger.LogError("{Exception}", e.ToString());
                }
            });

            return Ok();
        }
    }
}