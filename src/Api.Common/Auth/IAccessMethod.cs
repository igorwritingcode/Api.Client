using System.Net.Http;

namespace Api.Common.Auth
{
    public interface IAccessMethod
    {
        void Intercept(HttpRequestMessage request, string accessToken);
        string GetAccessToken(HttpRequestMessage request);
    }
}
