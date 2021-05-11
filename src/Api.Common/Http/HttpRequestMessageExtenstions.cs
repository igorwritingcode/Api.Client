using System.Net.Http;
using System.Text;

namespace Api.Common
{
    static class HttpRequestMessageExtenstions
    {
        internal static void SetRequestSerailizedContent(this HttpRequestMessage request, IClient client, object body)
        {
            if (body == null)
            {
                return;
            }

            var mediaType = "application/json";
            var serializedObject = client.SerializeObject(body);

            request.Content = new StringContent(serializedObject, Encoding.UTF8, mediaType);
        }
    }
}
