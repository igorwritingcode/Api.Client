using Api.Client.Generator.Model;

namespace Api.Client.Generator.CSharp
{
    public class CSharpApiResourceGenerator : CSharpApiGenerator
    {
        private readonly ApiClientModel _apiClientModel;
        public CSharpApiResourceGenerator(
            ApiClientModel apiClientModel
            ) : base(apiClientModel)
        {
            _apiClientModel = apiClientModel;
        }

        public void GenerateApiResource()
        {
        }
    }
}
