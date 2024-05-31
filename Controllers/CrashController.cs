using System;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Extensions.Logging;

namespace Smasher.Controllers
{
    public class CrashController : ApiController
    {
        private readonly IApiProxy _apiProxy;
        private readonly ILogger<CrashController> _logger;

        public CrashController(IApiProxy apiProxy, ILogger<CrashController> logger)
        {
            _apiProxy = apiProxy;
            _logger = logger;
        }

        [HttpGet]
        public IHttpActionResult Crash()
        {
            try
            {
                _apiProxy.JustThrowException();

                // This line will print
                _logger.LogInformation("Fire Task Success");
            }
            catch (Exception e)
            {
                _logger.LogError("{Exception}", e.ToString());
            }

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult NotCrashButNoLog()
        {
            try
            {
                Task.Run(async () => await _apiProxy.JustThrowException());

                // This line will print
                _logger.LogInformation("Fire Task Success");
            }
            catch (Exception e)
            {
                // This line will not print
                _logger.LogError("{Exception}", e.ToString());
            }

            return Ok();
        }

        [HttpGet]
        public IHttpActionResult NotCrashWithErrorLog()
        {
            Task.Run(async () =>
            {
                try
                {
                    await _apiProxy.JustThrowException();

                    // This line will not print
                    _logger.LogInformation("Fire Task Success");
                }
                catch (Exception e)
                {
                    // This line will print
                    _logger.LogError("{Exception}", e.ToString());
                }
            });

            return Ok();
        }
    }
}