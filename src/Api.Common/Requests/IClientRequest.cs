using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Common
{
    public interface IClientRequest
    {
        string MethodName { get; }
        string RestPath { get; }
        string HttpMethod { get; }
        //TBD IDictionary<string, IParameter> RequestParameters { ģet; }
        IClient Client { get; }
        HttpRequestMessage CreateRequest();
        Task<Stream> ExecuteAsStreamAsync();
        Task<Stream> ExecuteAsStreamAsync(CancellationToken cancellationToken);
        Stream ExecuteAsStream();
    }
    public interface IClientRequest<TResponse> : IClientRequest
    {
        Task<TResponse> ExecuteAsync();
        Task<TResponse> ExecuteAsync(CancellationToken cancellationToken);
        TResponse Execute();
    }
}
