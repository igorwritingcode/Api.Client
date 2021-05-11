using System.Collections.Generic;

namespace Api.Common
{
    public abstract class MessagePipeline
    {
        protected List<IHttpExecuteInterceptor> _executeInterceptor;
    }
}
