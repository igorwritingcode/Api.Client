using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Common.Auth
{
    public class ClientCredentials : ICredential
    {
        private TokenResponse Token { get; set; }
        public IAuthorizationFlow Flow { get; init; }
        public ClientCredentials(IAuthorizationFlow flow, TokenResponse token)
        {
            Token = token;
            Flow = flow;
        }
        public async Task InterceptAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            if (Token == null || Token.IsExpired())
            {
                Token = await Flow.FetchTokenAsync(cancellationToken);
            }
            Flow.AccessMethod.Intercept(request, Token.AccessToken);
        }
    }
}
