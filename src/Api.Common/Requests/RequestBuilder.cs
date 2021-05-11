using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;

namespace Api.Common
{
    public class RequestBuilder
    {
        private IDictionary<string, string> PathParameters { get; set; }
        public string Path { get; set; }

        private static IEnumerable<string> SupportedMethods = new List<string>
            {
                HttpConsts.Get, HttpConsts.Post, HttpConsts.Put, HttpConsts.Delete, HttpConsts.Patch
            };

        public Uri BaseUri;

        private string method;
        public string Method
        {
            get { return method; }
            set
            {
                if (!SupportedMethods.Contains(value))
                    throw new ArgumentOutOfRangeException("Method");
                method = value;
            }
        }

        public RequestBuilder()
        {
            Method = HttpConsts.Get;
            PathParameters = new Dictionary<string, string>();
        }

        public Uri BuildUri()
        {
            var restPath = BuildRestPath();
            return new Uri($"{BaseUri.AbsoluteUri}{restPath}");
        }

        public void AddParameter(string name, string value)
        {
            if (!PathParameters.ContainsKey(name))
            {
                PathParameters[name] = value;
            }
        }

        private StringBuilder BuildRestPath()
        {
            var joiner = "/";
            if (string.IsNullOrEmpty(Path))
            {
                return new StringBuilder(string.Empty);
            }
            var restPath = new StringBuilder(Path);
            foreach (var param in PathParameters)
            {
                var value = string.Join(joiner, param.Value);
                restPath.Append(value);
            }

            return restPath;
        }

        public HttpRequestMessage CreateRequest()
        {
            return new HttpRequestMessage(new HttpMethod(Method), BuildUri());
        }
    }
}
