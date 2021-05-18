using Api.Common.Requests;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Runtime.ExceptionServices;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Common
{
    public abstract class ClientBaseRequest<TResponse> : MessagePipeline, IClientRequest<TResponse>
    {
        public abstract string MethodName { get; }
        public abstract string RestPath { get; }
        public abstract string HttpMethod { get; }
        public IClient Client { get; init; }
        public Action<HttpRequestMessage> ModifyRequest { get; set; }
        public IDictionary<string, IParameter> RequestParameters { get; private set; }
        public ClientBaseRequest(IClient client)
        {
            Client = client;
        }
        public HttpRequestMessage CreateRequest()
        {
            var builder = CreateRequestBuilder();
            var request = builder.CreateRequest();
            object body = GetBody();
            request.SetRequestSerailizedContent(Client, body);
                

            if (_executeInterceptor != null)
            {
                request.Options.Set(new HttpRequestOptionsKey<List<IHttpExecuteInterceptor>>(), _executeInterceptor);
            }

            ModifyRequest?.Invoke(request);
            return request;
        }

        public TResponse Execute()
        {
            try
            {
                using var response = ExecuteUnparsedAsync(CancellationToken.None).Result;
                return ParseResponse(response).Result;
            }
            catch (AggregateException aex)
            {
                ExceptionDispatchInfo.Capture(aex.InnerException ?? aex).Throw();
                throw;
            }
        }

        public Stream ExecuteAsStream()
        {
            try
            {
                var response = ExecuteUnparsedAsync(CancellationToken.None).Result;
                return response.Content.ReadAsStreamAsync().Result;
            }
            catch (AggregateException aex)
            {
                throw aex.InnerException;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Stream> ExecuteAsStreamAsync()
        {
            return await ExecuteAsStreamAsync(CancellationToken.None).ConfigureAwait(false);
        }

        public async Task<Stream> ExecuteAsStreamAsync(CancellationToken cancellationToken)
        {
            var response = await ExecuteUnparsedAsync(cancellationToken).ConfigureAwait(false);

            cancellationToken.ThrowIfCancellationRequested();
            return await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
        }

        public async Task<TResponse> ExecuteAsync()
        {
            return await ExecuteAsync(CancellationToken.None).ConfigureAwait(false);
        }

        public async Task<TResponse> ExecuteAsync(CancellationToken cancellationToken)
        {
            using var response = await ExecuteUnparsedAsync(cancellationToken).ConfigureAwait(false);
            cancellationToken.ThrowIfCancellationRequested();
            return await ParseResponse(response).ConfigureAwait(false);
        }

        public virtual void InitParameters()
        {
            RequestParameters = new Dictionary<string, IParameter>();
        }

        private async Task<HttpResponseMessage> ExecuteUnparsedAsync(CancellationToken cancellationToken)
        {
            using var request = CreateRequest();
            return await Client.HttpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }

        private async Task<TResponse> ParseResponse(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
            {
                return await Client.DeserializeResponse<TResponse>(response).ConfigureAwait(false);
            }

            var error = await Client.DeserializeError(response).ConfigureAwait(false);
            throw new ApiException(Client.Name, error.ToString())
            {
                Error = error,
                HttpStatusCode = response.StatusCode
            };
        }

        protected virtual object GetBody() => null;

        private RequestBuilder CreateRequestBuilder()
        {
            return new RequestBuilder()
            {
                BaseUri = new Uri(Client.BaseUrl),
                Path = RestPath,
                Method = HttpMethod
            };
        }
    }
}
