using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Common
{
    public interface IHttpExecuteInterceptor
    {
        Task InterceptAsync(HttpRequestMessage request, CancellationToken cancellationToken);
    }
}
