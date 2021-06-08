using Api.Client.Generator.Model;

namespace Api.Client.Generator.CSharp
{
    public class CSharpApiClientGenerator
    {
        private readonly ApiModelContext _context;

        public CSharpApiClientGenerator(ApiModelContext context)
        {
            _context = context;
        }

        public void GenerateFiles(IDocumentWriter documentWriter)
        {
            var resourcesGenerator = new CSharpApiModelResourceGenerator(_context);

            foreach (var request in _context.GetRequests())
            {
                if(request.Value.Body != null)
                {
                    WriteToDocument(documentWriter, $"{request.Value.Name}Body.gen.cs", resourcesGenerator.GenerateRequestBody(request));
                }
                
                WriteToDocument(documentWriter, $"{request.Value.Name}Request.gen.cs", resourcesGenerator.GenerateApiClientRequests(request));
            }

            foreach (var apiResource in _context.GetResources())
            {
                WriteToDocument(documentWriter, $"{apiResource.Key}Resource.gen.cs", resourcesGenerator.GenerateApiClientResources(apiResource));
            }


            foreach (var apiResource in _context.GetResources())
            {
                WriteToDocument(documentWriter, $"{apiResource.Key}Client.gen.cs", resourcesGenerator.GenerateApiClient(apiResource, "ClassName"));
            }
        }

        private static void WriteToDocument(IDocumentWriter documentWriter, string path, string content)
        {
            var document = new CSharpClass();
            document.AppendLine(content);
            documentWriter.WriteDocument(
                path,
                document.ToString());
        }

  
    }






}
