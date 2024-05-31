using System.Threading.Tasks;

namespace Smasher
{
    public interface IApiProxy
    {
        Task JustThrowException();
        Task<string> DoSomethingAsync();
    }
}