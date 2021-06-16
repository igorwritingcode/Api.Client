using Api.Client.Generator.CSharp.Extension;
using Api.Client.Generator.Model;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Api.Client.Generator.CSharp
{
    public class CSharpApiModelResourceGenerator
    {
        private readonly List<ApiField> _allObjectFields = new List<ApiField>();

        private ApiModelContext _context { get; init; }
        public CSharpApiModelResourceGenerator(ApiModelContext context)
        {
            _context = context;
        }

        private void PrepareChildObjectFieldsList(ApiField parentField)
        {
            if (parentField.Type is ApiFieldType.Object parentObjectField)
            {
                _allObjectFields.Add(parentField);

                foreach (var childField in parentObjectField.Fields)
                {
                    if (childField.Type is ApiFieldType.Object)
                    {
                        PrepareChildObjectFieldsList(childField);
                    }
                }
            }
        }

        private void GenerateRequestBodyClasses(StringBuilder builder)
        {
            foreach (var field in _allObjectFields)
            {
                var indent = new Indent();
                var objectField = field.Type as ApiFieldType.Object;

                builder.AppendLine($"{indent}public class {field.Name.FirstUpper()}");
                builder.AppendLine($"{indent}{{");
                indent.Increment();

                foreach (var childField in objectField.Fields)
                {
                    builder.AppendLine($"{indent}[JsonPropertyName(\"{childField.Name}\")]");
                    if (childField.Type is ApiFieldType.Primitive primitiveType)
                    {
                        builder.AppendLine($"{indent}public {primitiveType.ToPrimitiveType()} {childField.Name} {{ get; set; }}");
                    }

                    if(childField.Type is ApiFieldType.Object)
                    {
                        builder.AppendLine($"{indent}public {childField.Name.FirstUpper()} {childField.Name} {{ get; set; }}");
                    }
                }

                indent.Decrement();
                builder.AppendLine($"{indent}}}");
            }
        }

        private void GenerateBodyObjectFields(StringBuilder builder, List<ApiField> bodyObjectFields)
        {
            foreach (var field in bodyObjectFields)
            {
                PrepareChildObjectFieldsList(field);
            }
            
            GenerateRequestBodyClasses(builder);
        }

        public string GenerateRequestBody(KeyValuePair<string, ApiRequest> request)
        {
            var indent = new Indent();
            var builder = new StringBuilder();
            List<ApiField> parentRequestBodyObjectFields = new();

            var requestName = $"{request.Value.Name}Request";
            builder.AppendLine($"{indent}public class {requestName}Body");
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
                    parentRequestBodyObjectFields.Add(field);
                }
            }

            indent.Decrement();
            builder.AppendLine($"{indent}}}");

            GenerateBodyObjectFields(builder, parentRequestBodyObjectFields);

            return builder.ToString();
        }

        public string GenerateApiClientRequests(KeyValuePair<string, ApiRequest> request)
        {
            var indent = new Indent();
            var builder = new StringBuilder();
            var requestName = RequestName(request);

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
                    if (!classParams.ContainsKey(param))
                    {
                        classParams.Add(param, "string");
                    }
                }
            }
            
            builder.AppendLine($"{indent}public {requestName} (IClient client, {string.Join(", ", classParams.Select(x => $"{x.Value} {x.Key}"))}) : base(client)");
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

        public string GenerateApiClientResources(KeyValuePair<string, ApiResource> resource)
        {
            var indent = new Indent();
            var builder = new StringBuilder();

            builder.AppendLine($"{indent}public class {resource.Key}Resource");
            builder.AppendLine($"{indent}{{");
            
            indent.Increment();
            
            builder.AppendLine($"{indent}IClient Client {{ get; init; }}");
            
            // Class constructor
            builder.AppendLine($"{indent}public {resource.Key}Resource (IClient client)");
            builder.AppendLine($"{indent}{{");
            indent.Increment();
            builder.AppendLine($"{indent}Client = client;");
            indent.Decrement();
            builder.AppendLine($"{indent}}}");

            // Resource requests
            foreach (var request in resource.Value.Requests.Where(b => b.Value.Body is not null))
            {
                var responseOk = request.Value.Responses.Where(w => w.StatusCode == "200").FirstOrDefault();
                var requestName = RequestName(request).FirstUpper();

                builder.AppendLine($"{indent}public {responseOk.Body.ClassName} {requestName} ({request.Value.Body.ClassName} body)");
                builder.AppendLine($"{indent}{{");
                indent.Increment();
                builder.AppendLine($"{indent}return new {requestName} (Client, body);");
                indent.Decrement();
                builder.AppendLine($"{indent}}}");
            }
            indent.Decrement();
            builder.AppendLine($"{indent}}}");

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

        public static string GenerateResourceDefinition(string apiName)
        {
            var builder = new StringBuilder();
            builder.AppendLine($"public override string Name => \"{apiName}\"");
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

        private static string RequestName(KeyValuePair<string, ApiRequest> request)
        {
            return $"{request.Key}Request";
        }

        
    }
}
