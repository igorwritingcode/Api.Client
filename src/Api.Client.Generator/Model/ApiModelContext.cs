using System.Collections.Generic;

namespace Api.Client.Generator.Model
{
    public class ApiModelContext
    {
        public ApiClientModel ApiClientModel { get; }
        public ApiModelContext(ApiClientModel apiClientModel)
        {
            ApiClientModel = apiClientModel;
        }

        public static ApiModelContext CreateFrom(ApiClientModel apiClientModel)
        {
            var context = new ApiModelContext(
                apiClientModel: apiClientModel
                );

            return context;
        }

        public SortedDictionary<string, ApiResource> GetResources() => ApiClientModel.Resources;
    }
}
