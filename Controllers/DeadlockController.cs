using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Extensions.Logging;

namespace Smasher.Controllers
{
    public class DeadlockController : ApiController
    {
        private readonly IApiProxy _apiProxy;
        private readonly ILogger<DeadlockController> _logger;

        public DeadlockController(IApiProxy apiProxy, ILogger<DeadlockController> logger)
        {
            _apiProxy = apiProxy;
            _logger = logger;
        }

        [HttpGet]
        public IHttpActionResult Deadlock()
        {
            var result = _apiProxy.DoSomethingAsync().GetAwaiter().GetResult();

            // This line will not print
            _logger.LogInformation("If see me, no deadlock");

            return Ok(result);
        }

        [HttpGet]
        public IHttpActionResult NoDeadlock()
        {
            var result = Task.Run(async () => await _apiProxy.DoSomethingAsync()).GetAwaiter().GetResult();

            // This line will print
            _logger.LogInformation("If see me, no deadlock");

            return Ok(result);
        }
    }
}