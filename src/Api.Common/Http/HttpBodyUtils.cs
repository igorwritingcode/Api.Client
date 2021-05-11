using System.Net.Http;
using System.Text;
using System.Text.Json;

namespace Api.Common
{
    public static class HttpBodyUtils
    {
        public static StringContent CreateEncodedStringContent(object request)
        {
            return new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
        }
    }
}
