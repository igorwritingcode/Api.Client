using Api.Client.Generator.CSharp;
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
            if (operation.RequestBody.Content.ContainsKey(JSON_MEDIA_TYPE))
            {
                return new ApiFieldType.Object()
                {
                    ClassName = $"{operation.OperationId.FirstUpper()}Body",
                    Nullable = false,
                    Fields = CreateApiFields(operation.RequestBody.Content[JSON_MEDIA_TYPE])
                };
            }
            return null;
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
                    CreateApiTypeFields(property.Value.Properties, field, property.Value.Required);
                }
                else if (field.Type is ApiFieldType.Array array)
                {
                    array.Type = property.Value.Items.Type;
                }

                fields.Add(field);
            }

            return fields;
        }

        private static void CreateApiTypeFields(IDictionary<string, OpenApiSchema> schema, ApiField field, ISet<string> required = null)
        {
            ((ApiFieldType.Object)field.Type).Fields = new();

            foreach (var element in schema)
            {
                ApiField childField = new();

                childField.Name = element.Key;
                childField.Type = ApiFieldTypeConverter.Convert(element.Value.Type);

                if (ApiFieldTypeConverter.Convert(element.Value.Type) is ApiFieldType.Object)
                {
                    CreateApiTypeFields(element.Value.Properties, childField, element.Value.Required);
                }

                if(required != null)
                {
                    childField.Required = required.Any(s => s == element.Key);
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
                    apiRequest.Responses = CreateApiResponses(operation.Value);
                    apiRequests.Add(operation.Value.OperationId, apiRequest);
                }
            }

            return apiRequests;
        }

        private static IEnumerable<ApiResponse> CreateApiResponses(OpenApiOperation operation)
        {
            foreach (var response in operation.Responses)
            {
                // TODO:response.Value.Content[JSON_MEDIA_TYPE].Schema.Reference.Id.throwIfNullOrEmpty();
                if (!response.Value.Content.Any())
                {
                    continue;
                }

                yield return new ApiResponse()
                {
                    StatusCode = response.Key,
                    Body = new ApiFieldType.Object()
                    {
                        ClassName = response.Value.Content[JSON_MEDIA_TYPE].Schema.Reference.Id,
                        Fields = CreateApiFields(response.Value.Content[JSON_MEDIA_TYPE])
                    }
                };               
            }
        }
    }
}