using System.Net.Http;
using System.Net.Http.Headers;

namespace Api.Common.Auth
{
    public class BearerToken : IAccessMethod
    {
        const string Schema = "Bearer";
        public string GetAccessToken(HttpRequestMessage request)
        {
            if (request.Headers.Authorization != null && request.Headers.Authorization.Scheme == Schema)
            {
                return request.Headers.Authorization.Parameter;
            }
            return null;
        }

        public void Intercept(HttpRequestMessage request, string accessToken) => request.Headers.Authorization = new AuthenticationHeaderValue(Schema, accessToken);
    }
}
