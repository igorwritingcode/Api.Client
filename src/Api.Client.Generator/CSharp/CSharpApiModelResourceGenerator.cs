using Api.Client.Generator.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Api.Client.Generator.CSharp
{
    public class CSharpApiModelResourceGenerator
    {
        private readonly ApiModelContext _apiModelContext;
        public CSharpApiModelResourceGenerator(ApiModelContext apiModelContext)
        {
            _apiModelContext = apiModelContext;
        }

        public string GenerateApiClientRequests(KeyValuePair<string, ApiRequest> request)
        {
            var builder = new StringBuilder();
            var requestName = $"{request.Value.Name}Request";

            builder.AppendLine($"public class {requestName} ");
            builder.AppendLine($"{{");

            builder.AppendLine($"public override string MethodName => \"{request.Key}\";");
            builder.AppendLine($"public override string RestPath => \"{request.Value.RestPath}\";");
            builder.AppendLine($"public override string HttpMethod => \"{request.Value.HttpMethod}\";");
            
            builder.AppendLine($"private {requestName}Body {{ get; init; }}");

            var bodyParamName = string.Empty;
            var classParams = new List<string>();

            if(request.Value.Body != null)
            {
                bodyParamName = $"{requestName}Body";
                classParams.Add(bodyParamName);
            }

            if (request.Value.Parameters?.Length > 0)
            {
                classParams.AddRange(request.Value.Parameters.Select(s=>s));
            }
            
            builder.AppendLine($"public {requestName} (IClient client, {string.Join(", ", classParams)}) : base(client)");
            builder.AppendLine($"{{");

            if (!string.IsNullOrEmpty(bodyParamName))
            {
                builder.AppendLine($"Body = body;");
            }

            builder.AppendLine($"base.InitParameters();");

            if(request.Value.Parameters != null)
            {
                foreach (var param in request.Value.Parameters)
                {
                    builder.AppendLine($"RequestParameters.Add(\"{param}\", new Parameter(Name = \"{param}\", IsRequired = true, ParameterType=\"path\"));");
                }
            }
            
            builder.AppendLine($"}}");
            builder.AppendLine($"protected override object GetBody() => Body;");
            builder.AppendLine($"}}");
            return builder.ToString();
        }

        public string GenerateApiClientResources(KeyValuePair<string, ApiResource> resource)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"public class {resource.Key}Resource");
            builder.AppendLine($"{{");
            
            builder.AppendLine($"IClient Client {{ get; init; }}");
            
            builder.AppendLine($"public {resource.Key}Resource (IClient client)");
            builder.AppendLine($"{{");
            builder.AppendLine($"Client = client;");
            builder.AppendLine($"}}");

            //TODO: change there.
            foreach (var request in resource.Value.Requests)
            {
                builder.AppendLine($"public {request.Value.Name} {request.Value.Name} ({request.Value.Body.ClassName} body)");
                builder.AppendLine($"{{");
                builder.AppendLine($"return new {request.Value.Name}(Client, body);");
                builder.AppendLine($"}}");
            }

            builder.AppendLine($"}}");

            return builder.ToString();
        }

        public string GenerateApiClient(KeyValuePair<string, ApiResource> resource, string clientClassName)
        {
            var builder = new StringBuilder();

            builder.AppendLine($"public class {clientClassName} : BaseClient");
            builder.AppendLine($"{{");
            builder.AppendLine($"public override string Name => \"{clientClassName}\";");
            builder.AppendLine($"public {resource.Key}Resource Insurance {{ get; private set; }}");
            builder.AppendLine($"public {clientClassName}(ICredential credentials, string baseUrl) : base(credentials, baseUrl)");
            builder.AppendLine($"Insurance = new {resource.Key}Resource(this);");
            builder.AppendLine($"}}");
            builder.AppendLine($"}}");

            return builder.ToString();
        }

        private string GenerateClientDefinition(string apiName)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"public class {apiName}Client : BaseClient {{");
            builder.AppendLine($"public override string Name => \"{apiName}\"");
            //public QuoteInsuranceResource Insurance { get; private set; }

            builder.AppendLine("}}");
            return builder.ToString();
        }

        public string GenerateResourceDefinition(string apiName)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"public override string Name => \"{apiName}\"");
            return builder.ToString();
        }
    }
}
