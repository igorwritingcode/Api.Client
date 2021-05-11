using System.Text.Json.Serialization;

namespace Api.Common.Auth
{
    public class TokenRequest
    {
        [JsonPropertyName("client_id")]
        public string ClientId { get; set; }
        [JsonPropertyName("client_secret")]
        public string ClientSecret { get; set; }
        [JsonPropertyName("grant_type")]
        public string GrantType { get; set; }
        [JsonPropertyName("scope")]
        public string Scope { get; set; }
    }
}
