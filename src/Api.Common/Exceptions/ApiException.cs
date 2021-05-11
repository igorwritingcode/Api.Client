using System;
using System.Net;

namespace Api.Common
{
    public class ApiException : Exception
    {
        private readonly string serviceName;
        public string ServiceName
        {
            get { return serviceName; }
        }

        public ApiException(string serviceName, string message, Exception inner)
            : base(message, inner)
        {
            serviceName.ThrowIfNull("serviceName");
            this.serviceName = serviceName;
        }

        public ApiException(string serviceName, string message) : this(serviceName, message, null) { }

        public RequestError Error { get; set; }

        public HttpStatusCode HttpStatusCode { get; set; }
        public override string ToString()
        {
            return string.Format("The service {1} has thrown an exception: {0}", base.ToString(), serviceName);
        }
    }
}
