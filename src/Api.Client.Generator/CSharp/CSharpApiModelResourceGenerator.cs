using Api.Client.Generator.Model;
using System.Collections.Generic;
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
