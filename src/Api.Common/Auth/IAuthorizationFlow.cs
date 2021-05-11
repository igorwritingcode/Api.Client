using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Common.Auth
{
    public interface IAuthorizationFlow : IDisposable
    {
        string AuthServerUrl { get; }
        IAccessMethod AccessMethod { get; }
        ClientSecrets ClientSecrets { get; }
        IDataStore DataStore { get; }
        ConfigurableHttpClient HttpClient { get; }
        IEnumerable<string> Scopes { get; }
        Task<TokenResponse> LoadTokenAsync(string clientId, CancellationToken cancellationToken);
        Task DeleteTokenAsync(string clientId, CancellationToken cancellationToken);
        Task<TokenResponse> FetchTokenAsync(CancellationToken cancellationToken);
    }
}
