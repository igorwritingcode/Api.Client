using System.Net.Http;

namespace Api.Common
{
    public class ConfigurableHttpClient : HttpClient
    {
        public ConfigurableMessageHandler MessageHandler { get; init; }
        public ConfigurableHttpClient(ConfigurableMessageHandler handler)
            : base(handler)
        {
            MessageHandler = handler;
            DefaultRequestHeaders.ExpectContinue = false;
        }
    }
}
