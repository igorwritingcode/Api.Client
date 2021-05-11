using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Common.Auth
{
    public static class TokenRequestExtensions
    {
        public static async Task<TokenResponse> ExecuteAsync(this TokenRequest request, HttpClient httpClient, string tokenServerUrl, CancellationToken cancellationToken)
        {
            var httpContent = new FormUrlEncodedContent(new Dictionary<string, string> {
                    {"grant_type", "client_credentials" },
                    {"scope", request.Scope },
                    {"client_id", request.ClientId },
                    {"client_secret", request.ClientSecret }
                    });

            var response = await httpClient.PostAsync(tokenServerUrl, httpContent, cancellationToken).ConfigureAwait(false);
            var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
            {
                var error = JsonSerializer.Deserialize<TokenErrorResponse>(content);
                throw new TokenResponseException(error, response.StatusCode);
            }

            return JsonSerializer.Deserialize<TokenResponse>(content);
        }
    }
}
