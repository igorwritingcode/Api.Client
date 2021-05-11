using System;
using System.Text.Json.Serialization;

namespace Api.Common.Auth
{
    public class TokenResponse
    {
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyName("expires_in")]
        public long? ExpiresInSeconds { get; set; }

        [JsonPropertyName("token_type")]
        public string TokenType { get; set; }

        [JsonPropertyName("scope")]
        public string Scope { get; set; }

        /// <summary>
        /// This should be set by the client after token was received
        /// </summary>
        public DateTime IssuedUtc { get; set; }

        public bool IsExpired()
        {
            if (AccessToken == null || !ExpiresInSeconds.HasValue)
            {
                return true;
            }

            return IssuedUtc.AddSeconds(ExpiresInSeconds.Value) <= DateTime.UtcNow;
        }
    }
}
