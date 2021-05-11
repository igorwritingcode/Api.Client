using System.Text.Json.Serialization;

namespace Api.Common
{
    public sealed class StandardResponse<InnerType>
    {
        [JsonPropertyName("data")]
        public InnerType Data { get; set; }

        [JsonPropertyName("error")]
        public RequestError Error { get; set; }
    }
}
