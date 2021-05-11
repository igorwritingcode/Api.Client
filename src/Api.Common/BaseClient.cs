using Api.Common.Auth;
using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Api.Common
{
    public abstract class BaseClient : IClient
    {
        public abstract string Name { get; }
        public string BaseUrl { get; init; }
        public string BasePath { get; }
        public ConfigurableHttpClient HttpClient { get; init; }
        public ICredential Credentials { get; init; }
        public BaseClient(ICredential credentials, string baseUrl)
        {
            BaseUrl = baseUrl;
            Credentials = credentials;
            HttpClient = CreateHttpClient(credentials);
        }
        private ConfigurableHttpClient CreateHttpClient(ICredential credentials)
        {
            var handler = new HttpClientHandler();
            var configurableHandler = new ConfigurableMessageHandler(handler);
            var client = new ConfigurableHttpClient(configurableHandler);
            client.MessageHandler.AddExecuteInterceptor(credentials);

            return client;
        }
        public async Task<RequestError> DeserializeError(HttpResponseMessage response)
        {
            if (response?.Content == null)
            {
                throw new ApiException(Name, "response or response content unexpectedly null.");
            }

            var responseText = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            RequestError error;
            try
            {
                if (responseText.Length == 0)
                {
                    error = response.StatusCode switch
                    {
                        HttpStatusCode.Unauthorized => new RequestError() { Code = 401, Message = "Unauthorized" },
                        HttpStatusCode.Forbidden => new RequestError() { Code = 403, Message = "Forbidden" },
                        HttpStatusCode.BadRequest => new RequestError() { Code = 400, Message = "BadRequest" },
                        _ => new RequestError() { Code = 500, Message = "Internal SDK Error." },
                    };
                }
                else
                {
                    error = JsonSerializer.Deserialize<StandardResponse<object>>(responseText)?.Error;
                }

                if (error == null)
                {
                    throw new ApiException(Name, $"Error response is null. {response?.ReasonPhrase}")
                    {
                        HttpStatusCode = response.StatusCode
                    };
                }
            }
            catch (JsonException ex)
            {
                throw new ApiException(Name, responseText, ex)
                {
                    HttpStatusCode = response.StatusCode
                };
            }

            return error;
        }
        public async Task<T> DeserializeResponse<T>(HttpResponseMessage response)
        {
            var text = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (Equals(typeof(T), typeof(string)))
            {
                return (T)(object)text;
            }

            T result = default;

            try
            {
                result = JsonSerializer.Deserialize<T>(text);
            }
            catch (Exception ex)
            {
                throw new ApiException(Name, $"Failed to parse response as json [{text}]", ex);
            }

            return result;
        }
        public virtual string SerializeObject(object obj) => JsonSerializer.Serialize(obj);
        public void Dispose()
        {
            if (HttpClient != null)
            {
                HttpClient.Dispose();
            }
        }

    }
}
