using Api.Client.Generator;
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
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(Fun.Logo);
            Console.WriteLine();
            Console.ResetColor();
            Console.WriteLine("Generate API client code from swagger.json");

            GenerateClient();
        }

        private static void GenerateClient()
        {
            //read open api document (swagger.json)
            var openApiDocument = ReadOpenApiDocument();
            
            //create client model from the open api document / resourceName is parameter.
            var apiClientModel = ApiClientModel.CreateFrom(openApiDocument, new string[] { "Insurance" });
            var apiModelContext = ApiModelContext.CreateFrom(apiClientModel);

            //generate cshart code
            var csharpClientGenerator = new CSharpApiClientGenerator(apiModelContext);
            csharpClientGenerator
                .GenerateFiles(
                    new DocumentWriter(new DirectoryInfo(Path.GetFullPath(OutputPath)))
                );
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
