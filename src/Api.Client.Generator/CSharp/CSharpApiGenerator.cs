using Api.Client.Generator.Model;

namespace Api.Client.Generator.CSharp
{
    public class CSharpApiGenerator
    {
        private readonly ApiClientModel _apiClientModel;
        public CSharpApiGenerator(ApiClientModel apiClientModel)
        {
            _apiClientModel = apiClientModel;
        }
        public void GenerateFiles(IDocumentWriter documentWriter)
        {
            var resourcesGenerator = new CSharpApiResourceGenerator(_apiClientModel);
        }
    }
}
