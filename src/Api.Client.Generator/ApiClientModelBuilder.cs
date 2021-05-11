using Api.Client.Generator.Model;
using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace Api.Client.Generator
{
    /// <summary>
    /// Generates <see cref="ApiClientModel"/> from OpenApiDocument <see cref="Microsoft.OpenApi"/>
    /// </summary>
    public static class ApiClientModelBuilder
    {
        private const string JSON_MEDIA_TYPE = "application/json";

        public static ApiResource BuildTypesFromDocument(OpenApiPaths paths, string resourceName)
        {
            return new ApiResource(resourceName, BuildApiRequestObjects(paths));
        }

        private static ApiRequest CreateApiRequest(OpenApiOperation operation, string httpMethod, string restPath)
        {
            ApiRequest apiRequest = new()
            {
                Name = operation.OperationId.FirstUpper(),
                HttpMethod = httpMethod.ToUpper(),
                RestPath = restPath
            };

            apiRequest.Body = operation.RequestBody is not null
                ? CreateApiRequestBody(operation)
                : null;

            apiRequest.Parameters = operation.Parameters.Any()
                ? CreateApiRequestParameters(operation.Parameters)
                : default!;

            return apiRequest;
        }

        private static ApiFieldType.Object CreateApiRequestBody(OpenApiOperation operation)
        {
            return new ApiFieldType.Object()
            {
                ClassName = $"{operation.OperationId.FirstUpper()}Body",
                Nullable = false,
                Fields = CreateApiFields(operation.RequestBody.Content[JSON_MEDIA_TYPE])
            };
        }
        private static string[] CreateApiRequestParameters(IEnumerable<OpenApiParameter> openApiParameter)
        {
            return openApiParameter.Select(s => s.Name).ToArray();
        }

        private static List<ApiField> CreateApiFields(OpenApiMediaType openApiMediaType)
        {
            var fields = new List<ApiField>();

            foreach (var property in openApiMediaType.Schema.Properties)
            {
                ApiField field = new()
                {
                    Name = property.Key.FirstUpper(),
                    Type = ApiFieldTypeConverter.Convert(property.Value.Type)
                };
                if (field.Type is ApiFieldType.Object)
                {
                    CreateApiTypeFields(property.Value.Properties, field);
                }
                fields.Add(field);
            }

            return fields;
        }

        private static void CreateApiTypeFields(IDictionary<string, OpenApiSchema> schema, ApiField field)
        {
            ((ApiFieldType.Object)field.Type).Fields = new();

            foreach (var element in schema)
            {
                ApiField childField = new();

                childField.Name = element.Key;
                childField.Required = element.Value.Required.Any(s => s == element.Key);
                childField.Type = ApiFieldTypeConverter.Convert(element.Value.Type);

                if (ApiFieldTypeConverter.Convert(element.Value.Type) is ApiFieldType.Object)
                {
                    CreateApiTypeFields(element.Value.Properties, childField);
                }

                ((ApiFieldType.Object)field.Type).Fields.Add(childField);
            }
        }

        private static SortedDictionary<string, ApiRequest> BuildApiRequestObjects(OpenApiPaths paths)
        {
            SortedDictionary<string, ApiRequest> apiRequests = new();

            foreach (var pathItem in paths)
            {
                foreach (var operation in pathItem.Value.Operations)
                {
                    var apiRequest = CreateApiRequest(operation.Value, operation.Key.ToString(), pathItem.Key);
                    apiRequests.Add(operation.Value.OperationId, apiRequest);
                }
            }

            return apiRequests;
        }
    }
}