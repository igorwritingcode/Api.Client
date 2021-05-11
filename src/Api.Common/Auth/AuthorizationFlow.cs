using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Common.Auth
{
    public class AuthorizationFlow : IAuthorizationFlow
    {
        public string AuthServerUrl { get; init; }
        public IAccessMethod AccessMethod { get; init; }
        public ClientSecrets ClientSecrets { get; init; }
        public IDataStore DataStore { get; init; }
        public ConfigurableHttpClient HttpClient { get; init; }
        public IEnumerable<string> Scopes { get; init; }

        public AuthorizationFlow(ClientSecrets clientSecrets, IEnumerable<string> scopes, string authServerUrl, IDataStore dataStore = null)
        {
            AuthServerUrl = authServerUrl;
            AccessMethod = new BearerToken();
            ClientSecrets = clientSecrets;
            DataStore = dataStore ?? new InMemoryDataStore();
            HttpClient = new ConfigurableHttpClient(new ConfigurableMessageHandler(new HttpClientHandler())); //move to HttpClientFactory
            Scopes = scopes;
        }

        public async Task<TokenResponse> LoadTokenAsync(string clientId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (DataStore == null)
            {
                return null;
            }
            return await DataStore.GetAsync<TokenResponse>(clientId).ConfigureAwait(false);
        }

        public async Task DeleteTokenAsync(string clientId, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (DataStore != null)
            {
                await DataStore.DeleteAsync<TokenResponse>(clientId).ConfigureAwait(false);
            }
        }

        public async Task<TokenResponse> FetchTokenAsync(CancellationToken cancellationToken)
        {
            var request = new TokenRequest()
            {
                ClientId = ClientSecrets.ClientId,
                ClientSecret = ClientSecrets.ClientSecret,
                GrantType = "client_credentials",
                Scope = string.Join(" ", Scopes)
            };

            try
            {
                var token = await request.ExecuteAsync(HttpClient, $"{AuthServerUrl}oauth/v2/oauth-token", cancellationToken).ConfigureAwait(false);
                token.IssuedUtc = DateTime.UtcNow;
                await StoreTokenAsync(request.ClientId, token, cancellationToken).ConfigureAwait(false);
                return token;
            }
            catch (TokenResponseException ex)
            {
                int statusCode = (int)(ex.StatusCode ?? 0);
                bool serverError = statusCode >= 500 && statusCode < 600;
                if (!serverError)
                {
                    await DeleteTokenAsync(ClientSecrets.ClientId, cancellationToken);
                }
                throw;
            }
        }

        private async Task StoreTokenAsync(string clientId, TokenResponse token, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            if (DataStore != null)
            {
                await DataStore.StoreAsync(clientId, token).ConfigureAwait(false);
            }
        }

        public void Dispose()
        {
            if (HttpClient != null)
            {
                HttpClient.Dispose();
            }
        }
    }
}
