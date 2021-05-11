using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Common
{
    public class ConfigurableMessageHandler : DelegatingHandler
    {
        private readonly List<IHttpExecuteInterceptor> executeInterceptors = new();
        private readonly object executeInterceptorsLock = new();

        public ConfigurableMessageHandler(HttpMessageHandler httpMessageHandler) : base(httpMessageHandler)
        {
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            try
            {
                foreach (var interceptor in executeInterceptors)
                {
                    await interceptor.InterceptAsync(request, cancellationToken).ConfigureAwait(false);
                };

                return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void AddExecuteInterceptor(IHttpExecuteInterceptor interceptor)
        {
            lock (executeInterceptorsLock)
            {
                executeInterceptors.Add(interceptor);
            }
        }
    }
}
