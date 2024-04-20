using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Smasher.Controllers
{
    public class DefaultController : ApiController
    {
        private readonly IApiProxy _apiProxy;

        public DefaultController(IApiProxy apiProxy)
        {
            _apiProxy = apiProxy;
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
                    Console.WriteLine(e);
                }
            });

            return Ok();
        }
    }
}