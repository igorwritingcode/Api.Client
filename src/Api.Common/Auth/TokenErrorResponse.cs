using System.Text.Json.Serialization;

namespace Api.Common.Auth
{
    public class TokenErrorResponse
    {
        [JsonPropertyName("error")]
        public string Error { get; set; }
        [JsonPropertyName("error_description")]
        public string ErrorDescription { get; set; }

        public override string ToString()
        {
            return $"Error:{Error}, Description:{ErrorDescription}";
        }
    }
}
