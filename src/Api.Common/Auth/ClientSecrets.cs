using System.Text.Json.Serialization;

namespace Api.Common.Auth
{
    public class ClientSecrets
    {
        [JsonPropertyName("client_id")]
        public string ClientId { get; set; }

        [JsonPropertyName("client_secret")]
        public string ClientSecret { get; set; }
    }
}
