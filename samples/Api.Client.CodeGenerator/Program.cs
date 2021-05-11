using Api.Client.Generator.CSharp;
using Api.Client.Generator.Model;
using Microsoft.OpenApi.Models;
using Microsoft.OpenApi.Readers;
using System;
using System.IO;

namespace IfApi.Client.CodeGenerator
{
    class Program
    {
        private const string OutputPath = "../../../../IfApi.Client/";

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            GenerateClient();
        }

        private static void GenerateClient()
        {
            //read open api document (swagger.json)
            var openApiDocument = ReadOpenApiDocument();
            
            //create client model from the open api document
            var apiClientModel = ApiClientModel.CreateFrom(openApiDocument, new string[] { "Insurance" });
            var apiModelContext = ApiModelContext.CreateFrom(apiClientModel);


            //generate cshart code
            var csharpClientGenerator = new CSharpApiClientGenerator(apiModelContext);
            //    .GenerateResourceDefinition("IfBrandedInsuranceSe")
            //    .WriteToPath(OutputPath);
        }

        private static OpenApiDocument ReadOpenApiDocument()
        {
            string fileName = "getquote-se.json";
            string path = "C:\\GIT\\json\\";

            var jsonStream = File.ReadAllBytes($"{path}{fileName}");
            Stream stream = new MemoryStream(jsonStream);

            return new OpenApiStreamReader().Read(stream, out var diagnostic);
        }
    }





        
}
