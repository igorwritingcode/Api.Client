using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Api.Common
{
    public interface IClient : IDisposable
    {
        string BaseUrl { get; }
        string BasePath { get; }
        string Name { get; }
        ConfigurableHttpClient HttpClient { get; }
        string SerializeObject(object data);
        Task<T> DeserializeResponse<T>(HttpResponseMessage response);
        Task<RequestError> DeserializeError(HttpResponseMessage response);
    }
}
