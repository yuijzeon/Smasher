using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Smasher
{
    public class ApiProxy : IApiProxy
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<ApiProxy> _logger;

        public ApiProxy(HttpClient httpClient, ILogger<ApiProxy> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task JustThrowException()
        {
            await Task.Delay(1000);
            throw new Exception("JustThrowException");
        }

        public async Task<string> DoSomethingAsync()
        {
            _logger.LogInformation("DoSomethingAsync Start");
            await Task.Delay(1000);
            _logger.LogInformation("DoSomethingAsync Done");
            return "DoSomethingAsync Done";
        }
    }
}