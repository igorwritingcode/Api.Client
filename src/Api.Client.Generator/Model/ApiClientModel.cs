using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Collections.Immutable;

namespace Api.Client.Generator.Model
{
    /// <summary>
    /// ApiClientModel represents model of client model including body, parameters, names, types. It is simplifies OpenApi data 
    /// structure and used in <see cref="Generator"/>
    /// </summary>
    public class ApiClientModel
    {
        public OpenApiDocument OpenApiDocument { get; }
        public SortedDictionary<string, ApiResource> Resources { get; }

        private ApiClientModel(
            OpenApiDocument openApiDocument,
            SortedDictionary<string, ApiResource> resources
            )
        {
            OpenApiDocument = openApiDocument;
            Resources = resources;
        }

        public static ApiClientModel CreateFrom(OpenApiDocument openApiDocument, string[] resourceNames)
        {
            var model = new ApiClientModel(
                openApiDocument: openApiDocument,
                resources: new SortedDictionary<string, ApiResource>(
                    resourceNames.ToImmutableSortedDictionary(
                        r => r,
                        r => ApiClientModelBuilder.BuildTypesFromDocument(openApiDocument.Paths, r)
                        )!
                    )
                );

            return model;
        }
    }
}