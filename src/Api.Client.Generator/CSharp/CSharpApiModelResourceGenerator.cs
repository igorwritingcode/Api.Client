using Api.Client.Generator.CSharp.Extension;
using Api.Client.Generator.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Api.Client.Generator.CSharp
{
    public static class CSharpApiModelResourceGenerator
    {
        public static string GenerateRequestBody(KeyValuePair<string, ApiRequest> request)
        {
            if(request.Value.Body == null)
            {
                return string.Empty;
            }

            var indent = new Indent();
            var builder = new StringBuilder();

            var requestName = $"{request.Value.Name}Request";
            builder.AppendLine($"{indent}public class {requestName}Body ");
            builder.AppendLine($"{indent}{{");
            
            indent.Increment();
            foreach (var field in request.Value.Body.Fields)
            {
                if (field.Required)
                {
                   builder.AppendLine($"{indent}[Required]");
                }

                builder.AppendLine($"{indent}[JsonPropertyName(\"{field.Name}\")]");
                if(field.Type is ApiFieldType.Primitive primitiveType)
                {
                    builder.AppendLine($"{indent}public {primitiveType.ToPrimitiveType()} {field.Name} {{ get; set; }}");
                }

                if(field.Type is ApiFieldType.Array arrayType)
                {
                    builder.AppendLine($"{indent}public {arrayType.ToArrayType()} {field.Name} {{ get; set; }}");
                }
                
                if(field.Type is ApiFieldType.Object)
                {
                    builder.AppendLine($"{indent}public {field.Name} {field.Name} {{ get; set; }}");
                }
            }

            indent.Decrement();
            builder.AppendLine($"{indent}}}");
            return builder.ToString();
        }

        public static string GenerateApiClientRequests(KeyValuePair<string, ApiRequest> request)
        {
            var indent = new Indent();
            var builder = new StringBuilder();
            var requestName = $"{request.Value.Name}Request";

            builder.AppendLine($"{indent}public class {requestName} ");
            builder.AppendLine($"{indent}{{");
            indent.Increment();

            builder.AppendLine($"{indent}public override string MethodName => \"{request.Key}\";");
            builder.AppendLine($"{indent}public override string RestPath => \"{request.Value.RestPath}\";");
            builder.AppendLine($"{indent}public override string HttpMethod => \"{request.Value.HttpMethod}\";");

            if (request.Value.Body != null)
            {
                builder.AppendLine($"{indent}private {requestName}Body {requestName}Body {{ get; init; }}");
            }

            var bodyParamName = string.Empty;
            var classParams = new Dictionary<string, string>();

            if(request.Value.Body != null)
            {
                bodyParamName = $"{requestName}Body";
                classParams.Add(bodyParamName, bodyParamName.FirstLower());
            }

            if (request.Value.Parameters?.Length > 0)
            {
                foreach (var param in request.Value.Parameters)
                {
                    classParams.Add("string", param);
                }
                
            }
            
            builder.AppendLine($"{indent}public {requestName} (IClient client, {string.Join(", ", classParams.Select(x => $"{x.Key} {x.Value}"))}) : base(client)");
            builder.AppendLine($"{indent}{{");
            indent.Increment();

            if (!string.IsNullOrEmpty(bodyParamName))
            {
                builder.AppendLine($"{indent}Body = body;");
            }
            
            builder.AppendLine($"{indent}base.InitParameters();");

            if(request.Value.Parameters != null)
            {
                foreach (var param in request.Value.Parameters)
                {
                    builder.AppendLine($"{indent}RequestParameters.Add(\"{param}\", new Parameter(Name = \"{param}\", IsRequired = true, ParameterType=\"path\"));");
                }
            }
            
            indent.Decrement();
            
            builder.AppendLine($"{indent}}}");
            builder.AppendLine($"{indent}protected override object GetBody() => Body;");
            
            indent.Decrement();
            
            builder.AppendLine($"{indent}}}");

            return builder.ToString();
        }

        public static string GenerateApiClientResources(KeyValuePair<string, ApiResource> resource)
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

        public static string GenerateApiClient(KeyValuePair<string, ApiResource> resource, string clientClassName)
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

        private static string GenerateClientDefinition(string apiName)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"public class {apiName}Client : BaseClient {{");
            builder.AppendLine($"public override string Name => \"{apiName}\"");
            //public QuoteInsuranceResource Insurance { get; private set; }

            builder.AppendLine("}}");
            return builder.ToString();
        }

        public static string GenerateResourceDefinition(string apiName)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"public override string Name => \"{apiName}\"");
            return builder.ToString();
        }
    }
}
