using Microsoft.OpenApi.Models;
using System.Collections.Generic;
using System.Linq;

namespace Api.Client.Generator.Extensions
{
    public static class OpenApiRequestBodyExtensions
    {
        public static bool IsEligibleContentType(this OpenApiRequestBody openApiRequestBody)
        {
            List<string> eligibleContentTypes = new ()
            {
                { "application/json" },
                { "application/x-www-form-urlencoded" }
            };

            return openApiRequestBody is not null 
                && openApiRequestBody.Content.Keys.Any(s => eligibleContentTypes.Contains(s));
        }
    }
}
