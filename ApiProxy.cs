using System.Net.Http;
using System.Threading.Tasks;

namespace Smasher
{
    public class ApiProxy : IApiProxy
    {
        private readonly HttpClient _httpClient;

        public ApiProxy(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task DoSomethingAsync()
        {
            await Task.Delay(1000);
            await _httpClient.GetAsync("any-for-exception");
        }
    }
}