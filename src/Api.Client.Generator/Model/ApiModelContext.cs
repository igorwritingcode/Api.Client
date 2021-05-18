using System.Collections.Generic;
using System.Linq;

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
        public IEnumerable<KeyValuePair<string, ApiRequest>> GetRequests() => ApiClientModel.Resources.Values.SelectMany(s => s.Requests);
    }
}
