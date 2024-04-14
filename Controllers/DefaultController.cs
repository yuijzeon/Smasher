using System;
using System.Threading.Tasks;
using System.Web.Http;

namespace Smasher.Controllers
{
    public class DefaultController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Crash()
        {
            ThrowException();
            return Ok();
        }

        [HttpGet]
        public IHttpActionResult NotCrash()
        {
            Task.Run(ThrowException);
            return Ok();
        }

        private static async Task ThrowException()
        {
            await Task.Delay(1000);
            throw new InvalidOperationException();
        }
    }
}