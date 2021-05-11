using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Common.Auth
{
    public class AuthorizationBroker
    {
        public static async Task<ClientCredentials> AuthorizeAsync(ClientSecrets clientSecrets, IEnumerable<string> scopes,
            string authServerUrl, CancellationToken cancellationToken)
        {
            AuthorizationFlow flow = new(clientSecrets, scopes, authServerUrl);
            var token = await flow.LoadTokenAsync(clientSecrets.ClientId, cancellationToken);

            return new(flow, token);
        }
    }
}
